using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Xml;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using SalesOrdersReport.Models;
using SalesOrdersReport.Views;

namespace SalesOrdersReport.CommonModules
{
    delegate void ReportProgressDel(Int32 ProgressState);
    public delegate void UpdateUsingObjectOnCloseDel(Int32 Mode, Object ObjAddUpdated = null);
    public delegate void UpdateOnCloseDel(Int32 Mode);

    class CommonFunctions
    {
        public static List<ProductLine> ListProductLines;
        public static Int32 SelectedProductLineIndex;
        public static List<String> ListCustomerLines, ListVendorLines, ListSelectedCustomer, ListSelectedVendors;//&&&&& listCustomerlines
        public static String AppDataFolder;
        public static String MasterFilePath;
        public static ToolStripProgressBar ToolStripProgressBarMainForm;
        public static ToolStripLabel ToolStripProgressBarMainFormStatus;
        public static Form CurrentForm = null;
        public static UserMasterModel ObjUserMasterModel;
        public static CustomerMasterModel ObjCustomerMasterModel;
        public static Char PaddingChar = ' ', CurrencyChar = '\u20B9';
        public static Type TypeString = Type.GetType("System.String"), TypeInt32 = Type.GetType("System.Int32");
        public static Type TypeDouble = Type.GetType("System.Double"), TypeBoolean = Type.GetType("System.Boolean");

        
        public static Dictionary<string, string> DictFilterNamesWithActualDBColNames;

        public static string CurrentUserName = "";

        public static void Initialize()
        {
            try
            {
                AppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SalesOrders";
                if (!Directory.Exists(AppDataFolder)) Directory.CreateDirectory(AppDataFolder);
                SettingsFilePath = CommonFunctions.AppDataFolder + @"\Settings.xml";

                ListProductLines = new List<ProductLine>();
                SelectedProductLineIndex = 1;

                LoadSettingsFile();
                ObjUserMasterModel = new UserMasterModel();
                ObjUserMasterModel.Initialize();
                ObjCustomerMasterModel = new CustomerMasterModel();
                ObjCustomerMasterModel.Initialize();

                if (!File.Exists(CommonFunctions.AppDataFolder + "\\" + CommonFunctions.ObjApplicationSettings.LogoFileName))
                {
                    File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"\Images\" + CommonFunctions.ObjApplicationSettings.LogoFileName,
                        CommonFunctions.AppDataFolder + @"\" + CommonFunctions.ObjApplicationSettings.LogoFileName, false);
                }

                ListSelectedCustomer = new List<String>();
                ListSelectedVendors = new List<String>();
                DictFilterNamesWithActualDBColNames = new Dictionary<string, string>()
                {
                    // { 111, new StudentName { FirstName="Sachin", LastName="Karnik", ID=211 } },
                    { "Customer Name","CUSTOMERNAME" } ,
                    { "Invoice Number","INVOICENUMBER"  },
                    { "Invoice Status","INVOICESTATUS"  },
                    { "Invoice Date","INVOICEDATE"  },
                    { "Order Number","ORDERNUMBER"  },
                    { "Order Status","ORDERSTATUS"  },
                    { "Order Date","ORDERDATE" } ,
                    { "Payment Date","PAYMENTDATE" } ,
                    {"Payment Amount","PAYMENTAMOUNT" } ,
                    {"Payment Method","PAYMENTMODE" } ,
                    {"Payment Received by","USERNAME" }
                };
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.Initialize()", ex);
            }
        }

        public static void ShowErrorDialog(String Method, Exception ex)
        {
            try
            {
                if (CurrentForm != null)
                    MessageBox.Show(CurrentForm, "Following Error Occured in " + Method + ":\n" + ex.Message, "Exception occured", MessageBoxButtons.OK);
                else
                    MessageBox.Show("Following Error Occured in " + Method + ":\n" + ex.Message, "Exception occured", MessageBoxButtons.OK);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static char GetColSeparator(String Header)
        {
            try
            {
                char SEP = '\t';

                int TabCount = Header.Split('\t').ToArray().Length;
                int CommaCount = Header.Split(',').ToArray().Length;

                if (CommaCount > TabCount)
                {
                    SEP = ',';
                }

                return SEP;
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.GetColSeparator", ex);
            }
            return '\t';
        }

        public static List<Control> GetAllControlsOfAForm(Control root)
        {
            try
            {
                List<Control> ListTemp = new List<Control>();
                foreach (Control control in root.Controls)
                {
                    ListTemp.Add(control);
                    if (control.Controls != null)
                    {
                        ListTemp.AddRange(GetAllControlsOfAForm(control));
                    }
                }
                return ListTemp;
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.GetAllControlsOfAForm", ex);
                throw ex;
            }

        }

        public static void ApplyPrivilegeControl(Form ObjForm)
        {
            try
            {
                List<PrivilegeControlDetails> ListPrivilegeControlDtls = ObjUserMasterModel.GetPrivilegeControlDetailsForAnUser(CurrentUserName);

                bool FirstTime = true;
                List<Control> ListAllControls = new List<Control>();
                foreach (PrivilegeControlDetails item in ListPrivilegeControlDtls)
                {
                    if (ObjForm.Name == item.FormName)
                    {
                        if (FirstTime)
                        {
                            foreach (Control FormRootControl in ObjForm.Controls)
                            {
                                ListAllControls.AddRange(GetAllControlsOfAForm(FormRootControl));
                            }
                            FirstTime = false;
                        }
                        foreach (var FormAllChildControlItem in ListAllControls)
                        {
                            if (FormAllChildControlItem.Name == item.ControlName)
                            {
                                FormAllChildControlItem.Enabled = item.IsEnabled;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.ApplyPrivilegeControl", ex);
            }
        }

        public static void ReleaseCOMObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                ShowErrorDialog("ReleaseCOMObject", ex);
            }
            finally
            {
                GC.Collect();
            }
        }

        #region "Get DataTable from Worksheet of an Excel file "
        public static DataTable ReturnDataTableFromExcelWorksheet(String SheetName, String FilePath, String ColumnNames, String UsedRange = "")
        {
            try
            {
                String strConnectionString, strCommandText;
                OleDbConnection conOleDbCon;
                OleDbCommand cmdCommand;

                if (System.IO.Path.GetExtension(FilePath).Equals(".xlsx", StringComparison.InvariantCultureIgnoreCase))
                {
                    strConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1';";
                }
                else
                {
                    strConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';";
                }

                //strConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=Excel 14.0";
                String OrigSheetName = SheetName;
                SheetName = SheetName.Replace(".", "#").Replace("!", "_");
                strCommandText = "Select " + ColumnNames + " from [" + SheetName + "$" + UsedRange + "]";
                conOleDbCon = new OleDbConnection(strConnectionString);
                cmdCommand = new OleDbCommand(strCommandText, conOleDbCon);
                conOleDbCon.Open();

                DataTable dtSheets = conOleDbCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                SheetName += "$";
                if (SheetName.Contains(" ") || SheetName.Contains("#") || SheetName.Contains(",") || OrigSheetName.Contains("!") || SheetName.Contains("&"))
                    SheetName = "'" + SheetName + "'";
                if (!dtSheets.Select().ToList().Exists(sheet => sheet["TABLE_NAME"].ToString().Trim().Equals(SheetName, StringComparison.InvariantCultureIgnoreCase)))
                {
                    conOleDbCon.Close();
                    return null;
                }

                OleDbDataAdapter dapAdapter = new OleDbDataAdapter(strCommandText, conOleDbCon);
                DataTable dtbInput = new DataTable();
                dapAdapter.Fill(dtbInput);
                conOleDbCon.Close();
                return dtbInput;
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.ReturnDataTableFromExcelWorksheet", ex);
                //Console.WriteLine("Error Occured in CommonFunctions.ReturnDataTableFromExcelWorksheet()");
                return null;
            }
        }

        public static bool ValidateDoubleORIntVal(string DoubleORIntValToBeChecked)
        {
            try
            {
                int num;
                float numFloat;
                bool isValid = false;
                if (Int32.TryParse(DoubleORIntValToBeChecked, out num)) isValid = true;
                else if (float.TryParse(DoubleORIntValToBeChecked, out numFloat)) isValid = true;


                //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                //    (e.KeyChar != '.'))
                //{
                //    e.Handled = true;
                //}

                //// only allow one decimal point
                //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                //{
                //    e.Handled = true;
                //}



                //// allows 0-9, backspace, and decimal
                //if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
                //{
                //    e.Handled = true;
                //    return;
                //}

                //// checks to make sure only 1 decimal is allowed
                //if (e.KeyChar == 46)
                //{
                //    if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                //        e.Handled = true;
                //}

                return isValid;
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.ValidateDoubleORIntVal", ex);
                throw ex;
            }
        }
        
        public static Excel.Worksheet GetWorksheet(Excel.Workbook ObjWorkbook, String Sheetname)
        {
            try
            {
                for (int i = 1; i <= ObjWorkbook.Sheets.Count; i++)
                {
                    if (ObjWorkbook.Worksheets[i].Name.Equals(Sheetname, StringComparison.InvariantCultureIgnoreCase))
                        return ObjWorkbook.Worksheets[i];
                }
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.GetWorksheet", ex);
            }
            return null;
        }
        #endregion

        #region "Settings file related methods"
        static XmlDocument SettingXmlDoc;
        static String SettingsFilePath;
        static XmlNode ProductLinesNode;
        public static ApplicationSettings ObjApplicationSettings;
        public static GeneralSettings ObjGeneralSettings;
        public static ReportSettings ObjOrderSettings, ObjInvoiceSettings, ObjQuotationSettings, ObjPurchaseOrderSettings;
        public static ProductMasterModel ObjProductMaster;
        public static VendorMaster ObjVendorMaster;
        static Boolean SettingsFileUpdated = false;

        public static void LoadSettingsFile()
        {
            try
            {
                if (!File.Exists(SettingsFilePath))
                {
                    File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"\Settings.xml", SettingsFilePath, false);
                }

                SettingXmlDoc = new XmlDocument();
                SettingXmlDoc.Load(SettingsFilePath);

                XmlNode SettingsNode;
                XMLFileUtils.GetChildNode(SettingXmlDoc, "Settings", out SettingsNode);

                XmlNode ApplicationNode;
                XMLFileUtils.GetChildNode(SettingsNode, "Application", out ApplicationNode);
                ObjApplicationSettings = new ApplicationSettings();
                ObjApplicationSettings.ReadSettingsFromNode(ApplicationNode);

                ListProductLines.Clear();
                XMLFileUtils.GetChildNode(SettingsNode, "ProductLines", out ProductLinesNode);
                foreach (XmlNode item in ProductLinesNode.ChildNodes)
                {
                    if (item.NodeType == XmlNodeType.CDATA || item.NodeType == XmlNodeType.Comment) continue;
                    ProductLine ObjProductLine = new ProductLine();
                    if (ObjProductLine.LoadConfigDetailsFromNode(item))
                    {
                        ListProductLines.Add(ObjProductLine);
                    }
                }

                SelectProductLine(SelectedProductLineIndex);

                SettingXmlDoc.NodeChanged += new XmlNodeChangedEventHandler(SettingXmlDoc_NodeChanged);
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.LoadSettingsFile()", ex);
            }
        }

        static void SettingXmlDoc_NodeChanged(object sender, XmlNodeChangedEventArgs e)
        {
            try
            {
                if (SettingsFileUpdated) return;
                if (e.OldValue == null && e.NewValue == null) return;
                if ((e.OldValue != null && e.NewValue == null) || (e.OldValue == null && e.NewValue != null))
                {
                    SettingsFileUpdated = true;
                    return;
                }
                if (e.OldValue.Equals(e.NewValue)) return;
                SettingsFileUpdated = true;
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.SettingXmlDoc_NodeChanged()", ex);
            }
        }

        public static void SelectProductLine(Int32 Index, Boolean Temporary = false)
        {
            try
            {
                ProductLine CurrProductLine;
                if (Temporary)
                {
                    CurrProductLine = ListProductLines[Index];
                }
                else
                {
                    SelectedProductLineIndex = Index;
                    CurrProductLine = ListProductLines[SelectedProductLineIndex];
                }

                ObjGeneralSettings = CurrProductLine.ObjSettings.GeneralSettings;
                ObjOrderSettings = CurrProductLine.ObjSettings.OrderSettings;
                ObjInvoiceSettings = CurrProductLine.ObjSettings.InvoiceSettings;
                ObjQuotationSettings = CurrProductLine.ObjSettings.QuotationSettings;
                ObjPurchaseOrderSettings = CurrProductLine.ObjSettings.PurchaseOrderSettings;
                ObjProductMaster = CurrProductLine.ObjProductMaster;
                //ObjSellerMaster = CurrProductLine.ObjSellerMaster;
                ObjVendorMaster = CurrProductLine.ObjVendorMaster;
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.SelectProductLine()", ex);
                throw;
            }
        }

        public static void AddNewProductLine(String Name, Int32 UseSettingsOfProductLineIndex)
        {
            try
            {
                XmlNode ProductLineNode = ListProductLines[UseSettingsOfProductLineIndex].ProductLineNode.CloneNode(true);
                ProductLine ObjProductLine = new ProductLine();
                XMLFileUtils.SetAttributeValue(ProductLineNode, "Name", Name);
                ObjProductLine.LoadConfigDetailsFromNode(ProductLineNode);
                ListProductLines.Add(ObjProductLine);
                ProductLinesNode.AppendChild(ProductLineNode);
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.AddNewProductLine()", ex);
            }
        }

        public static Color GetColor(String ColorArgb)
        {
            if (String.IsNullOrEmpty(ColorArgb))
                return Color.Black;
            else
                return Color.FromArgb(Int32.Parse(ColorArgb));
        }

        public static void WriteToSettingsFile()
        {
            try
            {
                if (SettingXmlDoc == null) return;

                ObjApplicationSettings.UpdateSettingsToNode();
                for (int i = 0; i < ListProductLines.Count; i++)
                {
                    ListProductLines[i].ObjSettings.UpdateSettingsToNode();
                }

                if (SettingsFileUpdated) SettingXmlDoc.Save(SettingsFilePath);
                SettingsFileUpdated = false;
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.WriteToSettingsFile()", ex);
            }
        }
        #endregion

        public static String GetExcelColumnNameForColumnNumber(Int32 ColumnNumber)
        {
            try
            {
                Int32 Count = ColumnNumber;
                String Column = "";
                if (Count / 26 >= 26)
                {
                    Column = ((Char)('A' + ((Count / (26 * 26)) - 1))).ToString();
                    Column += ((Char)('A' + ((Count / 26) - 1))).ToString();
                    Column += ((Char)('A' + ((Count % 26) - 1))).ToString();
                }
                else if (Count >= 26)
                {
                    Column = ((Char)('A' + ((Count / 26) - 1))).ToString();
                    Column += ((Char)('A' + ((Count % 26) - 1))).ToString();
                }
                else
                {
                    Column = ('A' + ((Count % 26) - 1)).ToString();
                }

                return Column;
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.GetExcelColumnNameForColumnNumber()", ex);
            }
            return "";
        }

        public static string GetColorHexCode(Color color)
        {
            return ColorTranslator.ToHtml(color).Replace("#", "");
        }

        public static void ResetProgressBar(Int32 ProgressState = 0, Int32 Maximum = 100, Int32 Step = 1, String Status = "")
        {
            try
            {
                ToolStripProgressBarMainForm.Maximum = Maximum;
                ToolStripProgressBarMainForm.Step = Step;
                ToolStripProgressBarMainForm.Value = ProgressState;
                ToolStripProgressBarMainFormStatus.Text = Status;
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.ResetProgressBar()", ex);
            }
        }

        public static void UpdateProgressBar(Int32 ProgressState)
        {
            try
            {
                if (ToolStripProgressBarMainForm == null || ToolStripProgressBarMainFormStatus == null) return;

                ToolStripProgressBarMainForm.Value = Math.Min(ProgressState, 100);
                ToolStripProgressBarMainFormStatus.Text = Math.Min(ProgressState, 100).ToString() + "%";
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.UpdateProgressBar()", ex);
            }
        }

        public static Boolean ValidateFile_Overwrite_TakeBackup(Form ParentForm, String DirectoryPath, ref String FilePath, 
                                                String ValidFileName, String FileDesc)
        {
            try
            {
                if (FilePath.Length == 0)
                {
                    FilePath = DirectoryPath + @"\" + ValidFileName;
                    if (File.Exists(FilePath))
                    {
                        DialogResult result = MessageBox.Show(ParentForm, "\"" + FilePath + "\" already exist.\nDo you want to append " + FileDesc + " to this file?\nYes - Append to it\nNo - Backup it & Create new",
                                                FileDesc, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        switch (result)
                        {
                            case DialogResult.Cancel: return false;
                            case DialogResult.No:
                                String BackupFilePath = DirectoryPath + @"\" + ValidFileName + ".bkp";
                                if (File.Exists(BackupFilePath)) File.Delete(BackupFilePath);
                                File.Move(FilePath, BackupFilePath);
                                break;
                            case DialogResult.Yes:
                            default: break;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.ValidateFile_Overwrite_TakeBackup()", ex);
                throw;
            }
        }

        public static void ToggleEnabledPropertyOfAllControls(Form CurrentForm, Boolean Override = false, Boolean Enabled = false)
        {
            try
            {
                foreach (Control item in CurrentForm.Controls)
                {
                    item.Enabled = (Override ? Enabled : !item.Enabled);
                }
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.ToggleEnabledPropertyOfAllControls()", ex);
                throw;
            }
        }

        public static String NumberToWords(String number)
        {
            try
            {
                Int32 Number = (Int32)Math.Round(Double.Parse(number), 0);
                return NumberToWords(Number);
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.NumberToWords(String)", ex);
                throw;
            }
        }

        public static String NumberToWords(Double number)
        {
            try
            {
                Int32 Number = (Int32)Math.Round(number, 0);
                return NumberToWords(Number);
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.NumberToWords(Double)", ex);
                throw;
            }
        }

        public static String NumberToWords(Int32 number)
        {
            try
            {
                if (number == 0) return "Zero";

                if (number < 0) return "minus " + NumberToWords(Math.Abs(number));

                string words = "";

                if ((number / 100000) > 0)
                {
                    words += NumberToWords(number / 100000) + " Lakh ";
                    number %= 100000;
                }

                if ((number / 1000) > 0)
                {
                    words += NumberToWords(number / 1000) + " Thousand ";
                    number %= 1000;
                }

                if ((number / 100) > 0)
                {
                    words += NumberToWords(number / 100) + " Hundred ";
                    number %= 100;
                }

                if (number > 0)
                {
                    if (words != "") words += "and ";

                    String[] unitsMap = new String[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                    String[] tensMap = new String[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                    if (number < 20)
                        words += unitsMap[number];
                    else
                    {
                        words += tensMap[number / 10];
                        if ((number % 10) > 0)
                            words += "-" + unitsMap[number % 10];
                    }
                }

                return words;
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.NumberToWords()", ex);
                throw;
            }
        }

        public static Invoice GetInvoiceTemplate(ReportType EnumReportType)
        {
            try
            {
                if (EnumReportType == ReportType.INVOICE)
                    return new InvoiceGST();
                else if (EnumReportType == ReportType.QUOTATION)
                    return new InvoiceVAT();

                return null;
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.GetInvoiceTemplate()", ex);
                throw;
            }
        }

        public static Boolean CreateDBConnection()
        {
            try
            {
                MySQLHelper ObjMySQLHelper = MySQLHelper.GetMySqlHelperObj();

                if (ObjApplicationSettings.Server == null || ObjApplicationSettings.Server == string.Empty)
                {
                    MessageBox.Show("Database settings are not found in Settings.xml. Please check!!!", "Invalid Database Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    ObjMySQLHelper.OpenConnection(ObjApplicationSettings.Server, ObjApplicationSettings.DatabaseName, ObjApplicationSettings.UserName, ObjApplicationSettings.Password);
                    return true;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CommonFunctions.CreateDBConnection()", ex);
                throw ex;
            }
        }

		public static bool CheckForPasswordLength(string PwdStr)
        {
            try
            {
                if (PwdStr.Length < 5 || PwdStr.Length > 20)
                {
                    return false;
                }
                else return true;
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.CheckForPasswordLength()", ex);
                throw;
            }
        }

        public static string GetHashedPassword(string Password, Guid UserGuid)
        {
            try
            {
                return CryptoGraphy.HashSHA1(Password + UserGuid.ToString());
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.GetHashedPassword()", ex);
                throw;
            }
        }

        public static bool CompareNwPwdConfrmPwd(string NwPwdStr, string ConfirmPwd)
        {
            try
            {
                if (NwPwdStr == ConfirmPwd)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.CompareNwPwdConfrmPwd()", ex);
                throw;
            }
        }

        public static bool ValidateEmail(string EmailIdStr)
        {
            try
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(EmailIdStr);
                if (match.Success)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.ValidateEmail()", ex);
                throw;
            }
        }

        public static void ShowDialog(Form ObjForm, Form Owner)
        {
            try
            {
                ObjForm.ShowInTaskbar = false;
                ObjForm.ShowIcon = false;
                ObjForm.StartPosition = FormStartPosition.CenterParent;
                ObjForm.MaximizeBox = false;
                ObjForm.MinimizeBox = false;
                //ObjForm.ControlBox = false;
                ObjForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                ApplyPrivilegeControl(ObjForm);
                ObjForm.ShowDialog(Owner);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CommonFunctions.ShowDialog()", ex);
            }
        }

        public static List<String> GetUOMList()
        {
            try
            {
                List<String> ListUOM = new List<String>();
                ListUOM.Add("PIECES");
                ListUOM.Add("KG");
                ListUOM.Add("GM");
                ListUOM.Add("LITRE");
                ListUOM.Add("ML");

                return ListUOM;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CommonFunctions.GetUOMList()", ex);
                throw;
            }
        }

		public static bool ValidatePhoneNo(string PhoneNoStr)
        {
            try
            {
                /*
                 9775876662
                0 9754845789
                0-9778545896
                +91 9456211568
                91 9857842356
                919578965389
                03595-259506
                03592 245902
                03598245785
                 */
                //Regex regex = new Regex(@"((\+*)((0[ -]+)*|(91 )*)(\d{12}+|\d{10}+))|\d{5}([- ]*)\d{6}");
                Regex regex = new Regex(@"((\+*)((0[ -]+)*|(91 )*)(\d{12}|\d{10}))|\d{5}([- ]*)\d{6}");
                Match match = regex.Match(PhoneNoStr);
                if (match.Success)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.ValidatePhoneNo()", ex);
                throw;
            }
        }

        public static String GenerateNextID(String Prefix, String MaxIDString, String Suffix = "", Int32 Padding = 5, Char PaddingChar = '0')
        {
            try
            {
                if (Prefix.Length > 0) MaxIDString = MaxIDString.Trim().ToUpper().Replace(Prefix.ToUpper(), "");
                if (Suffix.Length > 0) MaxIDString = MaxIDString.Trim().ToUpper().Replace(Suffix.ToUpper(), "");

                Int32 MaxID = Int32.Parse(MaxIDString) + 1;

                String NewMaxIDString = Prefix + MaxID.ToString().PadLeft(Padding, PaddingChar) + Suffix;

                return NewMaxIDString;
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.GenerateNextID()", ex);
                throw;
            }
        }

        public static void ResetTextBoxesRecursive(Control ObjControl)
        {
            try
            {
                foreach (Control item in ObjControl.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                    else if (item is GroupBox || item is Panel)
                    {
                        ResetTextBoxesRecursive(item);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CommonFunctions.ResetTextBoxesRecursive()", ex);
            }
        }

        public static void SetDataGridViewProperties(DataGridView ObjDataGridView)
        {
            try
            {
                ObjDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                ObjDataGridView.MultiSelect = false;
                ObjDataGridView.AllowUserToAddRows = false;
                ObjDataGridView.AllowUserToDeleteRows = false;
                ObjDataGridView.AllowUserToOrderColumns = false;
                ObjDataGridView.AllowUserToResizeColumns = true;
                ObjDataGridView.AllowUserToResizeRows = false;
                ObjDataGridView.ReadOnly = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"CommonFunctions.SetDataGridViewProperties()", ex);
            }
        }

        public static Int32 ExportDataTableToExcelFile(DataTable dtDataToExport, String ExcelFilePath, String SheetName, Boolean AppendSheet = false, Int32 StartRow = 1, Int32 StartCol = 1)
        {
            Excel.Application xlApp = new Excel.Application();
            try
            {
                xlApp.Visible = false;
                xlApp.DisplayAlerts = false;

                Excel.Workbook ExcelWorkbook = null;
                Excel.Worksheet ExcelWorksheet = null;

                if (File.Exists(ExcelFilePath))
                {
                    if (AppendSheet)
                    {
                        ExcelWorkbook = xlApp.Workbooks.Open(ExcelFilePath);
                        ExcelWorksheet = GetWorksheet(ExcelWorkbook, SheetName);
                        if (ExcelWorksheet != null)
                        {
                            ExcelWorksheet.Cells.Clear();
                        }
                        else
                        {
                            ExcelWorksheet = ExcelWorkbook.Worksheets.Add();
                        }
                    }
                    else
                    {
                        File.Delete(ExcelFilePath);
                        ExcelWorkbook = xlApp.Workbooks.Add();
                        for (int i = 1; i <= 3 && ExcelWorkbook.Sheets.Count > 1; i++)
                        {
                            ExcelWorksheet = GetWorksheet(ExcelWorkbook, "Sheet" + i);
                            if (ExcelWorksheet != null) ExcelWorksheet.Delete();
                        }
                        ExcelWorksheet = ExcelWorkbook.Worksheets[1];
                    }
                }
                else
                {
                    ExcelWorkbook = xlApp.Workbooks.Add();
                    for (int i = 1; i <= 3 && ExcelWorkbook.Sheets.Count > 1; i++)
                    {
                        ExcelWorksheet = GetWorksheet(ExcelWorkbook, "Sheet" + i);
                        if (ExcelWorksheet != null) ExcelWorksheet.Delete();
                    }
                    ExcelWorksheet = ExcelWorkbook.Worksheets[1];
                }
                ExcelWorksheet.Name = SheetName;
                ExcelWorkbook.SaveAs(ExcelFilePath);

                Int32 RetVal = ExportDataTableToExcelWorksheet(dtDataToExport, ExcelWorksheet, StartRow, StartCol);

                ExcelWorkbook.Close(true);

                xlApp.DisplayAlerts = true;
                return RetVal;
            }
            catch (Exception ex)
            {
                ShowErrorDialog($"CommonFunctions.ExportDataTableToExcelFile()", ex);
                xlApp.Visible = true;
                xlApp.DisplayAlerts = true;
                return -1;
            }
            finally
            {
                xlApp.Quit();
                ReleaseCOMObject(xlApp);
            }
        }

        public static Int32 ExportDataTableToExcelWorksheet(DataTable dtDataToExport, Excel.Worksheet ExcelWorksheet, Int32 StartRow = 1, Int32 StartCol = 1)
        {
            try
            {
                Int32 CurrCol = StartCol, CurrRow = StartRow;
                foreach (DataColumn item in dtDataToExport.Columns)
                {
                    ExcelWorksheet.Cells[CurrRow, CurrCol] = item.ColumnName;
                    CurrCol++;
                }

                CurrRow = StartRow + 1; CurrCol = StartCol;
                foreach (DataRow item in dtDataToExport.Rows)
                {
                    for (int i = 0; i < item.ItemArray.Length; i++)
                    {
                        ExcelWorksheet.Cells[CurrRow, CurrCol + i] = item.ItemArray[i].ToString();
                    }
                    CurrRow++;
                }

                return 0;
            }
            catch (Exception ex)
            {
                ShowErrorDialog($"CommonFunctions.ExportDataTableToExcelWorksheet()", ex);
                return -1;
            }
        }


        public static void PrintOrderInvoiceQuotation(ReportType EnumReportType, Boolean IsDummyBill, Object ObjectModel, List<Object> ListObjects, 
            DateTime OrdInvQuotDate, Int32 PrintCopies = 1, Boolean CreateSummary = false, Boolean PrintOldBalance = false, ReportProgressDel ReportProgress = null)
        {
            try
            {
                String OutputFolder = Path.GetTempPath();
                String ExcelFilePath = ExportOrdInvQuotToExcel(EnumReportType, IsDummyBill, OrdInvQuotDate, ObjectModel, ListObjects, OutputFolder, CreateSummary, PrintOldBalance, ReportProgress);

                if (PrintCopies > 0)
                {
                    Excel.Application xlApp = new Excel.Application();
                    try
                    {
                        xlApp.Visible = false;
                        xlApp.DisplayAlerts = false;

                        Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(ExcelFilePath);

                        xlWorkbook.PrintOutEx(Type.Missing, Type.Missing, PrintCopies);

                        xlWorkbook.Close(false);
                        CommonFunctions.ReleaseCOMObject(xlWorkbook);
                    }
                    catch (Exception ex)
                    {
                        CommonFunctions.ShowErrorDialog($"CommonFunctions.PrintOrderInvoiceQuotation()", ex);
                    }
                    finally
                    {
                        xlApp.Quit();
                        CommonFunctions.ReleaseCOMObject(xlApp);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"CommonFunctions.PrintOrderInvoiceQuotation()", ex);
            }
        }

        public static String ExportOrdInvQuotToExcel(ReportType EnumReportType, Boolean IsDummyBill, DateTime OrdInvQuotDate, Object ObjectModel, 
                            List<Object> ListObjects, String ExportFolderPath, Boolean CreateSummary = false, Boolean PrintOldBalance = false, ReportProgressDel ReportProgress = null)
        {
            Excel.Application xlApp = null;
            try
            {
                xlApp = new Excel.Application();
                xlApp.Visible = false;
                xlApp.DisplayAlerts = false;

                String SaveFilePath = "";
                String OutputFolder = ExportFolderPath;
                String SelectedDateTimeString = OrdInvQuotDate.ToString("dd-MM-yyyy");
                switch (EnumReportType)
                {
                    case ReportType.ORDER:
                        SaveFilePath = OutputFolder + "\\Order_" + SelectedDateTimeString + ".xlsx";
                        break;
                    case ReportType.INVOICE:
                        SaveFilePath = OutputFolder + "\\Invoice_" + SelectedDateTimeString + ".xlsx";
                        break;
                    case ReportType.QUOTATION:
                        SaveFilePath = OutputFolder + "\\Quotation_" + SelectedDateTimeString + ".xlsx";
                        break;
                    default:
                        return null;
                }

                if (IsDummyBill)
                {
                    SaveFilePath = Path.GetDirectoryName(SaveFilePath) + "\\" + Path.GetFileNameWithoutExtension(SaveFilePath) + "_Dummy" + Path.GetExtension(SaveFilePath);
                }

                Excel.Workbook xlWorkbook = xlApp.Workbooks.Add();

                for (int i = 0; i < ListObjects.Count; i++)
                {
                    switch (EnumReportType)
                    {
                        case ReportType.ORDER:
                            ((OrdersModel)ObjectModel).ExportOrder(EnumReportType, IsDummyBill, xlWorkbook, (OrderDetails)ListObjects[i]);
                            break;
                        case ReportType.INVOICE:
                            break;
                        case ReportType.QUOTATION:
                            break;
                        default:
                            break;
                    }
                    ReportProgress((Int32)(100 * (i + 1) * 1.0 / ListObjects.Count));
                }

                switch (EnumReportType)
                {
                    case ReportType.ORDER:
                        ((OrdersModel)ObjectModel).ExportItemSummary(xlWorkbook, ListObjects.Select(e => (OrderDetails)e).ToList());
                        break;
                    case ReportType.INVOICE:
                        break;
                    case ReportType.QUOTATION:
                        break;
                    default:
                        break;
                }

                for (int i = 1; i <= 3 && xlWorkbook.Sheets.Count > 1; i++)
                {
                    Excel.Worksheet xlWorkSheet = CommonFunctions.GetWorksheet(xlWorkbook, "Sheet" + i);
                    if (xlWorkSheet != null) xlWorkSheet.Delete();
                }

                Excel.Worksheet FirstWorksheet = xlWorkbook.Sheets[1];
                FirstWorksheet.Select();

                xlWorkbook.SaveAs(SaveFilePath);
                xlWorkbook.Close(true);

                xlApp.DisplayAlerts = true;
                CommonFunctions.ReleaseCOMObject(xlWorkbook);

                ReportProgress(100);
                return SaveFilePath;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"CommonFunctions.ExportOrdInvQuotToExcel()", ex);
                if (xlApp != null)
                {
                    xlApp.Visible = true;
                    xlApp.DisplayAlerts = true;
                }
                return null;
            }
            finally
            {
                if (xlApp != null)
                {
                    xlApp.Quit();
                    CommonFunctions.ReleaseCOMObject(xlApp);
                }
            }
        }

        public static String GetWorksheetNameToAppend(String SheetName, Excel.Workbook ExcelWorkbook)
        {
            try
            {
                List<String> ListSheetNames = new List<String>();
                for (int i = 1; i <= ExcelWorkbook.Sheets.Count; i++)
                {
                    String tmpSheetName = ExcelWorkbook.Worksheets[i].Name;
                    ListSheetNames.Add(tmpSheetName);
                }

                Int32 SheetSuffix = 0;
                Boolean ContainsCustomerSheet = false;
                if (ListSheetNames.Count > 0)
                {
                    for (int i = 0; i < ListSheetNames.Count; i++)
                    {
                        if (ListSheetNames[i].Contains(SheetName))
                        {
                            String NumberStr = ListSheetNames[i].Replace(SheetName, "").Trim();
                            Int32 Number;
                            if (Int32.TryParse(NumberStr, out Number))
                            {
                                SheetSuffix = Math.Max(Number, SheetSuffix);
                            }
                            ContainsCustomerSheet = true;
                        }
                    }

                    if (ContainsCustomerSheet || SheetSuffix > 0)
                    {
                        SheetSuffix++;
                        SheetName += " " + SheetSuffix;
                    }
                }
                return SheetName;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"CommonFunctions.GetWorksheetNameToAppend()", ex);
                throw ex;
            }
        }
    }
}
