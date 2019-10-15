using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Xml;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel;

namespace SalesOrdersReport
{
    class CommonFunctions
    {
        public static List<ProductLine> ListProductLines;
        public static Int32 SelectedProductLineIndex;
        public static List<String> ListSellerLines, ListVendorLines, ListSelectedSellers, ListSelectedVendors;
        public static String AppDataFolder;
        public static String MasterFilePath;
        public static ToolStripProgressBar ToolStripProgressBarMainForm;
        public static ToolStripLabel ToolStripProgressBarMainFormStatus;
        public static Form CurrentForm = null;

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

                if (!File.Exists(CommonFunctions.AppDataFolder + "\\" + CommonFunctions.ObjApplicationSettings.LogoFileName))
                {
                    File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"\Images\" + CommonFunctions.ObjApplicationSettings.LogoFileName,
                        CommonFunctions.AppDataFolder + @"\" + CommonFunctions.ObjApplicationSettings.LogoFileName, false);
                }

                ListSelectedSellers = new List<String>();
                ListSelectedVendors = new List<String>();
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.Initialize()", ex);
            }
        }

        public static void ShowErrorDialog(String Method, Exception ex)
        {
            if (CurrentForm != null)
                MessageBox.Show(CurrentForm, "Following Error Occured in " + Method + ":\n" + ex.Message, "Exception occured", MessageBoxButtons.OK);
            else
                MessageBox.Show("Following Error Occured in " + Method + ":\n" + ex.Message, "Exception occured", MessageBoxButtons.OK);
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
                strCommandText = "Select " + ColumnNames + " from [" + SheetName + "$" + UsedRange + "]";
                conOleDbCon = new OleDbConnection(strConnectionString);
                cmdCommand = new OleDbCommand(strCommandText, conOleDbCon);
                conOleDbCon.Open();

                DataTable dtSheets = conOleDbCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                SheetName += "$";
                if (SheetName.Contains(" ")) SheetName = "'" + SheetName + "'";
                if (!dtSheets.Select().ToList().Exists(sheet => sheet["TABLE_NAME"].ToString().Trim().Equals(SheetName, StringComparison.InvariantCultureIgnoreCase))) return null;

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
        public static ReportSettings ObjInvoiceSettings, ObjQuotationSettings, ObjPurchaseOrderSettings;
        public static ProductMaster ObjProductMaster;
        public static SellerMaster ObjSellerMaster;
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
                    if (ObjProductLine.LoadDetailsFromNode(item))
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
                ObjInvoiceSettings = CurrProductLine.ObjSettings.InvoiceSettings;
                ObjQuotationSettings = CurrProductLine.ObjSettings.QuotationSettings;
                ObjPurchaseOrderSettings = CurrProductLine.ObjSettings.PurchaseOrderSettings;
                ObjProductMaster = CurrProductLine.ObjProductMaster;
                ObjSellerMaster = CurrProductLine.ObjSellerMaster;
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
                ObjProductLine.LoadDetailsFromNode(ProductLineNode);
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
    }
}
