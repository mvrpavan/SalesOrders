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
    class SettingDetails
    {
        public String Name, InnerText, XPath;
        public String[] Values;
    }

    class ReportSettings
    {
        public String HeaderTitle, HeaderSubTitle, FooterTitle, Address, PhoneNumber, EMailID, VATPercent, TINNumber;
        public Int32 LastNumber;
        public Color HeaderTitleColor, HeaderSubTitleColor, FooterTitleColor, FooterTextColor;
    }

    class GeneralSettings
    {
        public Int32 SummaryLocation = 0;
    }

    class CommonFunctions
    {
        public static String MainFormTitleText, LogoFileName;
        public static Int32 ReportRowsFromTop, ReportAppendRowsAtBottom, LogoImageHeight;
        public static List<String> ListLines, ListSelectedSellers;
        public static String AppDataFolder;

        public static void Initialize()
        {
            AppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SalesOrders";
            if (!Directory.Exists(AppDataFolder)) Directory.CreateDirectory(AppDataFolder);
            SettingsFilePath = CommonFunctions.AppDataFolder + @"\Settings.xml";

            LoadSettingsFile();

            ListSelectedSellers = new List<String>();
        }

        public static void ShowErrorDialog(String Method, Exception ex)
        {
            MessageBox.Show("Following Error Occured in " + Method + "():\n" + ex.Message, "Exception occured", MessageBoxButtons.OK);
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

                OleDbDataAdapter dapAdapter = new OleDbDataAdapter(strCommandText, conOleDbCon);
                DataTable dtbInput = new DataTable();
                dapAdapter.Fill(dtbInput);
                return dtbInput;
            }
            catch (Exception)
            {
                Console.WriteLine("Error Occured in CommonFunctions.ReturnDataTableFromExcelWorksheet()");
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
                CommonFunctions.ShowErrorDialog("CommonFunctions.GetWorksheet", ex);
            }
            return null;
        }
        #endregion

        #region "Settings file related methods"
        static Dictionary<String, SettingDetails> DictSettingsFileEntries = new Dictionary<String, SettingDetails>();
        static String SettingsFilePath;
        static Boolean SettingsFileEntryModified = false;
        public static XmlDocument SettingXmlDoc;
        public static ReportSettings InvoiceSettings, QuotationSettings;
        public static GeneralSettings GeneralSettings;

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
                foreach (XmlNode item in ApplicationNode.ChildNodes)
                {
                    if (item.NodeType == XmlNodeType.CDATA || item.NodeType == XmlNodeType.Comment) continue;
                    if (!String.IsNullOrEmpty(item.InnerText)) AddKeyValueToDictionarySettings("//Settings/Application/" + item.Name, item.InnerText);
                    foreach (XmlAttribute Attribute in item.Attributes)
                    {
                        if (Attribute.NodeType == XmlNodeType.CDATA || Attribute.NodeType == XmlNodeType.Comment) continue;
                        AddKeyValueToDictionarySettings("//Settings/Application/" + item.Name + "/@" + Attribute.Name, Attribute.Value);
                    }
                }

                XmlNode GeneralNode;
                XMLFileUtils.GetChildNode(SettingsNode, "General", out GeneralNode);
                foreach (XmlNode item in GeneralNode.ChildNodes)
                {
                    if (item.NodeType == XmlNodeType.CDATA || item.NodeType == XmlNodeType.Comment) continue;
                    if (!String.IsNullOrEmpty(item.InnerText)) AddKeyValueToDictionarySettings("//Settings/General/" + item.Name, item.InnerText);
                    foreach (XmlAttribute Attribute in item.Attributes)
                    {
                        if (Attribute.NodeType == XmlNodeType.CDATA || Attribute.NodeType == XmlNodeType.Comment) continue;
                        AddKeyValueToDictionarySettings("//Settings/General/" + item.Name + "/@" + Attribute.Name, Attribute.Value);
                    }
                }

                XmlNode InvoiceNode;
                XMLFileUtils.GetChildNode(SettingsNode, "Invoice", out InvoiceNode);
                foreach (XmlNode item in InvoiceNode.ChildNodes)
                {
                    if (item.NodeType == XmlNodeType.CDATA || item.NodeType == XmlNodeType.Comment) continue;
                    if (!String.IsNullOrEmpty(item.InnerText)) AddKeyValueToDictionarySettings("//Settings/Invoice/" + item.Name, item.InnerText);
                    foreach (XmlAttribute Attribute in item.Attributes)
                    {
                        if (Attribute.NodeType == XmlNodeType.CDATA || Attribute.NodeType == XmlNodeType.Comment) continue;
                        AddKeyValueToDictionarySettings("//Settings/Invoice/" + item.Name + "/@" + Attribute.Name, Attribute.Value);
                    }
                }

                XmlNode QuotationNode;
                XMLFileUtils.GetChildNode(SettingsNode, "Quotation", out QuotationNode);
                foreach (XmlNode item in QuotationNode.ChildNodes)
                {
                    if (item.NodeType == XmlNodeType.CDATA || item.NodeType == XmlNodeType.Comment) continue;
                    if (!String.IsNullOrEmpty(item.InnerText)) AddKeyValueToDictionarySettings("//Settings/Quotation/" + item.Name, item.InnerText);
                    foreach (XmlAttribute Attribute in item.Attributes)
                    {
                        if (Attribute.NodeType == XmlNodeType.CDATA || Attribute.NodeType == XmlNodeType.Comment) continue;
                        AddKeyValueToDictionarySettings("//Settings/Quotation/" + item.Name + "/@" + Attribute.Name, Attribute.Value);
                    }
                }

                SetAllParameters();
            }
            catch (Exception ex)
            {
                ShowErrorDialog("LoadSettingsFile", ex);
            }
        }

        public static Color GetColor(String ColorArgb)
        {
            if (String.IsNullOrEmpty(ColorArgb))
                return Color.Black;
            else
                return Color.FromArgb(Int32.Parse(ColorArgb));
        }

        private static void SetAllParameters()
        {
            try
            {
                #region Set all Parameters
                MainFormTitleText = GetSettingsFileEntry("//Settings/Application/MainFormTitle").InnerText;
                ReportRowsFromTop = Int32.Parse(GetSettingsFileEntry("//Settings/Application/ReportRowsFromTop").InnerText);
                ReportAppendRowsAtBottom = Int32.Parse(GetSettingsFileEntry("//Settings/Application/ReportAppendRowsAtBottom").InnerText);
                LogoFileName = GetSettingsFileEntry("//Settings/Application/LogoFileName").InnerText;
                LogoImageHeight = Int32.Parse(GetSettingsFileEntry("//Settings/Application/LogoImageHeight").InnerText);

                //General Settings
                GeneralSettings = new GeneralSettings();
                GeneralSettings.SummaryLocation = Int32.Parse(GetSettingsFileEntry("//Settings/General/SummaryLocation").InnerText);

                //Invoice Settings
                InvoiceSettings = new ReportSettings();
                InvoiceSettings.HeaderTitle = GetSettingsFileEntry("//Settings/Invoice/HeaderTitle").InnerText;
                InvoiceSettings.HeaderSubTitle = GetSettingsFileEntry("//Settings/Invoice/HeaderSubTitle").InnerText;
                InvoiceSettings.HeaderTitleColor = GetColor(GetSettingsFileEntry("//Settings/Invoice/HeaderTitleColor").InnerText);
                InvoiceSettings.HeaderSubTitleColor = GetColor(GetSettingsFileEntry("//Settings/Invoice/HeaderSubTitleColor").InnerText);
                InvoiceSettings.FooterTitle = GetSettingsFileEntry("//Settings/Invoice/FooterTitle").InnerText;
                InvoiceSettings.FooterTitleColor = GetColor(GetSettingsFileEntry("//Settings/Invoice/FooterTitleColor").InnerText);
                InvoiceSettings.FooterTextColor = GetColor(GetSettingsFileEntry("//Settings/Invoice/FooterTextColor").InnerText);
                InvoiceSettings.Address = GetSettingsFileEntry("//Settings/Invoice/Address").InnerText;
                InvoiceSettings.PhoneNumber = GetSettingsFileEntry("//Settings/Invoice/PhoneNumber").InnerText;
                InvoiceSettings.EMailID = GetSettingsFileEntry("//Settings/Invoice/EMailID").InnerText;
                InvoiceSettings.VATPercent = GetSettingsFileEntry("//Settings/Invoice/VATPercent").InnerText;
                InvoiceSettings.TINNumber = GetSettingsFileEntry("//Settings/Invoice/TINNumber").InnerText;
                if (String.IsNullOrEmpty(GetSettingsFileEntry("//Settings/Invoice/LastInvoiceNumber").InnerText))
                    InvoiceSettings.LastNumber = 0;
                else
                    InvoiceSettings.LastNumber = Int32.Parse(GetSettingsFileEntry("//Settings/Invoice/LastInvoiceNumber").InnerText);

                //Quotation Settings
                QuotationSettings = new ReportSettings();
                QuotationSettings.HeaderTitle = GetSettingsFileEntry("//Settings/Quotation/HeaderTitle").InnerText;
                QuotationSettings.HeaderSubTitle = GetSettingsFileEntry("//Settings/Quotation/HeaderSubTitle").InnerText;
                QuotationSettings.HeaderTitleColor = GetColor(GetSettingsFileEntry("//Settings/Quotation/HeaderTitleColor").InnerText);
                QuotationSettings.HeaderSubTitleColor = GetColor(GetSettingsFileEntry("//Settings/Quotation/HeaderSubTitleColor").InnerText);
                QuotationSettings.FooterTitle = GetSettingsFileEntry("//Settings/Quotation/FooterTitle").InnerText;
                QuotationSettings.FooterTitleColor = GetColor(GetSettingsFileEntry("//Settings/Quotation/FooterTitleColor").InnerText);
                QuotationSettings.FooterTextColor = GetColor(GetSettingsFileEntry("//Settings/Quotation/FooterTextColor").InnerText);
                QuotationSettings.Address = GetSettingsFileEntry("//Settings/Quotation/Address").InnerText;
                QuotationSettings.PhoneNumber = GetSettingsFileEntry("//Settings/Quotation/PhoneNumber").InnerText;
                QuotationSettings.EMailID = GetSettingsFileEntry("//Settings/Quotation/EMailID").InnerText;
                QuotationSettings.TINNumber = GetSettingsFileEntry("//Settings/Quotation/TINNumber").InnerText;
                if (String.IsNullOrEmpty(GetSettingsFileEntry("//Settings/Quotation/LastQuotationNumber").InnerText))
                    QuotationSettings.LastNumber = 0;
                else
                    QuotationSettings.LastNumber = Int32.Parse(GetSettingsFileEntry("//Settings/Quotation/LastQuotationNumber").InnerText);
                #endregion
            }
            catch (Exception ex)
            {
                ShowErrorDialog("CommonFunctions.SetAllParameters(", ex);
            }
        }

        static void AddKeyValueToDictionarySettings(String XPath, String Value)
        {
            try
            {
                if (DictSettingsFileEntries.ContainsKey(XPath.Trim().ToUpper()))
                {
                    DictSettingsFileEntries[XPath.ToUpper()].InnerText = Value;
                    DictSettingsFileEntries[XPath.ToUpper()].Values = Value.Split('|');
                }
                else
                {
                    SettingDetails tmpSettingDetails = new SettingDetails();
                    tmpSettingDetails.XPath = XPath;
                    tmpSettingDetails.Name = XPath.Substring(XPath.LastIndexOf('/') + 1).Replace("@", "");
                    tmpSettingDetails.InnerText = Value;
                    tmpSettingDetails.Values = Value.Split('|');
                    DictSettingsFileEntries.Add(XPath.Trim().ToUpper(), tmpSettingDetails);
                }
            }
            catch (Exception ex)
            {
                ShowErrorDialog("AddKeyValueToDictionarySettings", ex);
            }
        }

        public static void UpdateSettingsFileEntry(String Key, String Value)
        {
            try
            {
                AddKeyValueToDictionarySettings(Key, Value);

                SettingsFileEntryModified = true;
            }
            catch (Exception ex)
            {
                ShowErrorDialog("UpdateSettingsFileEntry", ex);
            }
        }

        public static SettingDetails GetSettingsFileEntry(String Key)
        {
            try
            {
                if (DictSettingsFileEntries.ContainsKey(Key.Trim().ToUpper()))
                    return DictSettingsFileEntries[Key.Trim().ToUpper()];
                else
                {
                    SettingDetails tmpSettingDetails = new SettingDetails();
                    tmpSettingDetails.Values = new String[0];
                    tmpSettingDetails.InnerText = "";
                    return tmpSettingDetails;
                }
            }
            catch (Exception ex)
            {
                ShowErrorDialog("GetSettingsFileEntry", ex);
                return null;
            }
        }

        public static void WriteToSettingsFile()
        {
            try
            {
                if (!SettingsFileEntryModified) return;

                foreach (KeyValuePair<String, SettingDetails> item in DictSettingsFileEntries)
                {
                    if (item.Key.Contains('@'))
                    {
                        XMLFileUtils.SetAttributeValueInXMLFile(SettingsFilePath, item.Value.XPath, item.Value.InnerText);
                    }
                    else
                    {
                        XMLFileUtils.SetElementValueInXMLFile(SettingsFilePath, item.Value.XPath, item.Value.InnerText);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorDialog("WriteToSettingsFile", ex);
            }
        }
        #endregion

        public static string GetColorHexCode(Color color)
        {
            return ColorTranslator.ToHtml(color).Replace("#", "");
        }
    }
}
