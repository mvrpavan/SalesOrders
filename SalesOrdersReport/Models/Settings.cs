using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Data;

namespace SalesOrdersReport
{
    class ApplicationSettings
    {
        XmlNode SettingsNode;
        public String MainFormTitleText, LogoFileName;
        public Int32 ReportRowsFromTop, ReportAppendRowsAtBottom, LogoImageHeight;
        public string Server="", DatabaseName="", UserName="", Password="";
        public void ReadSettingsFromNode(XmlNode Node)
        {
            try
            {
                SettingsNode = Node;

                String Value;
                XMLFileUtils.GetChildNodeValue(SettingsNode, "MainFormTitle", out MainFormTitleText);
                if (XMLFileUtils.GetChildNodeValue(SettingsNode, "ReportRowsFromTop", out Value)) ReportRowsFromTop = Int32.Parse(Value);
                if (XMLFileUtils.GetChildNodeValue(SettingsNode, "ReportAppendRowsAtBottom", out Value)) ReportAppendRowsAtBottom = Int32.Parse(Value);
                XMLFileUtils.GetChildNodeValue(SettingsNode, "LogoFileName", out LogoFileName);
                if (XMLFileUtils.GetChildNodeValue(SettingsNode, "LogoImageHeight", out Value)) LogoImageHeight = Int32.Parse(Value);
                //Server = "" DatabaseName = "" User = "" Password = ""
                XmlNode DatabaseNode;
                XMLFileUtils.GetChildNode(SettingsNode, "Database", out DatabaseNode);
                

                XMLFileUtils.GetAttributeValue(DatabaseNode, "Server", out Server);
                XMLFileUtils.GetAttributeValue(DatabaseNode, "DatabaseName", out DatabaseName);
                XMLFileUtils.GetAttributeValue(DatabaseNode, "UserName", out UserName);
                XMLFileUtils.GetAttributeValue(DatabaseNode, "Password", out Password);

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ApplicationSettings.ReadSettingsFromNode()", ex);
            }
        }

        public void UpdateSettingsToNode()
        {
            try
            {
                XMLFileUtils.SetChildNodeValue(SettingsNode, "MainFormTitle", MainFormTitleText);
                XMLFileUtils.SetChildNodeValue(SettingsNode, "ReportRowsFromTop", ReportRowsFromTop.ToString());
                XMLFileUtils.SetChildNodeValue(SettingsNode, "ReportAppendRowsAtBottom", ReportAppendRowsAtBottom.ToString());
                XMLFileUtils.SetChildNodeValue(SettingsNode, "LogoFileName", LogoFileName);
                XMLFileUtils.SetChildNodeValue(SettingsNode, "LogoImageHeight", LogoImageHeight.ToString());

                //XmlNode DatabaseNode;
                //XMLFileUtils.SetChildNodeValue(SettingsNode, "Database", "");
                //XMLFileUtils.SetAttributeValue(DatabaseNode, "Server", Server);
                //XMLFileUtils.SetAttributeValue(DatabaseNode, "DatabaseName", out DatabaseName);
                //XMLFileUtils.SetAttributeValue(DatabaseNode, "UserName", out UserName);
                //XMLFileUtils.SetAttributeValue(DatabaseNode, "Password", out Password);
              
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ApplicationSettings.UpdateSettingsToNode()", ex);
            }
        }
    }

    enum TimePeriodUnits
    {
        Days = 0, Weeks, Months, Years, None
    }

    class ReportSettings
    {
        XmlNode SettingsNode;
        public String HeaderTitle, HeaderSubTitle, FooterTitle, Address, PhoneNumber, EMailID, VATPercent, TINNumber, GSTINumber;
        public Int32 LastNumber;
        public Color HeaderTitleColor, HeaderSubTitleColor, FooterTitleColor, FooterTextColor;
        public Int32 PastSalePeriodValue;
        public TimePeriodUnits PastSalePeriodUnits;
        public ReportType Type;

        public void ReadSettingsFromNode(XmlNode Node, ReportType Type)
        {
            try
            {
                SettingsNode = Node;
                this.Type = Type;

                String Value;
                XMLFileUtils.GetChildNodeValue(SettingsNode, "HeaderTitle", out HeaderTitle);
                XMLFileUtils.GetChildNodeValue(SettingsNode, "HeaderSubTitle", out HeaderSubTitle);
                XMLFileUtils.GetChildNodeValue(SettingsNode, "FooterTitle", out FooterTitle);
                XMLFileUtils.GetChildNodeValue(SettingsNode, "Address", out Address);
                XMLFileUtils.GetChildNodeValue(SettingsNode, "PhoneNumber", out PhoneNumber);
                XMLFileUtils.GetChildNodeValue(SettingsNode, "EMailID", out EMailID);
                XMLFileUtils.GetChildNodeValue(SettingsNode, "VATPercent", out VATPercent);
                XMLFileUtils.GetChildNodeValue(SettingsNode, "TINNumber", out TINNumber);
                XMLFileUtils.GetChildNodeValue(SettingsNode, "GSTINumber", out GSTINumber);

                if (XMLFileUtils.GetChildNodeValue(SettingsNode, "LastNumber", out Value)) LastNumber = Int32.Parse(Value);

                if (XMLFileUtils.GetChildNodeValue(SettingsNode, "HeaderTitleColor", out Value)) HeaderTitleColor = CommonFunctions.GetColor(Value);
                if (XMLFileUtils.GetChildNodeValue(SettingsNode, "HeaderSubTitleColor", out Value)) HeaderSubTitleColor = CommonFunctions.GetColor(Value);
                if (XMLFileUtils.GetChildNodeValue(SettingsNode, "FooterTitleColor", out Value)) FooterTitleColor = CommonFunctions.GetColor(Value);
                if (XMLFileUtils.GetChildNodeValue(SettingsNode, "FooterTextColor", out Value)) FooterTextColor = CommonFunctions.GetColor(Value);
                XmlNode PastSalesPeriodNode;
                if (XMLFileUtils.GetChildNode(SettingsNode, "PastSalesPeriod", out PastSalesPeriodNode))
                {
                    if (XMLFileUtils.GetChildNodeValue(PastSalesPeriodNode, "Value", out Value)) PastSalePeriodValue = Int32.Parse(Value);
                    if (XMLFileUtils.GetChildNodeValue(PastSalesPeriodNode, "Units", out Value))
                    {
                        PastSalePeriodUnits = GetTimePeriodUnits(Value);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ReportSettings.ReadSettingsFromNode()", ex);
            }
        }

        public void UpdateSettingsToNode()
        {
            try
            {
                if (SettingsNode == null) return;

                XMLFileUtils.SetChildNodeValue(SettingsNode, "HeaderTitle", HeaderTitle);
                XMLFileUtils.SetChildNodeValue(SettingsNode, "HeaderSubTitle", HeaderSubTitle);
                XMLFileUtils.SetChildNodeValue(SettingsNode, "FooterTitle", FooterTitle);
                XMLFileUtils.SetChildNodeValue(SettingsNode, "Address", Address);
                XMLFileUtils.SetChildNodeValue(SettingsNode, "PhoneNumber", PhoneNumber);
                XMLFileUtils.SetChildNodeValue(SettingsNode, "EMailID", EMailID);
                XMLFileUtils.SetChildNodeValue(SettingsNode, "VATPercent", VATPercent);
                XMLFileUtils.SetChildNodeValue(SettingsNode, "TINNumber", TINNumber);
                XMLFileUtils.SetChildNodeValue(SettingsNode, "GSTINumber", GSTINumber);

                XMLFileUtils.SetChildNodeValue(SettingsNode, "LastNumber", LastNumber.ToString());

                XMLFileUtils.SetChildNodeValue(SettingsNode, "HeaderTitleColor", HeaderTitleColor.ToArgb().ToString());
                XMLFileUtils.SetChildNodeValue(SettingsNode, "HeaderSubTitleColor", HeaderSubTitleColor.ToArgb().ToString());
                XMLFileUtils.SetChildNodeValue(SettingsNode, "FooterTitleColor", FooterTitleColor.ToArgb().ToString());
                XMLFileUtils.SetChildNodeValue(SettingsNode, "FooterTextColor", FooterTextColor.ToArgb().ToString());

                XmlNode PastSalesPeriodNode;
                if (XMLFileUtils.GetChildNode(SettingsNode, "PastSalesPeriod", out PastSalesPeriodNode))
                {
                    XMLFileUtils.SetChildNodeValue(PastSalesPeriodNode, "Value", PastSalePeriodValue.ToString());
                    XMLFileUtils.SetChildNodeValue(PastSalesPeriodNode, "Units", PastSalePeriodUnits.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ReportSettings.UpdateSettingsToNode()", ex);
            }
        }

        public static TimePeriodUnits GetTimePeriodUnits(String Value)
        {
            try
            {
                switch (Value.Trim().ToUpper())
                {
                    case "DAYS": return TimePeriodUnits.Days;
                    case "WEEKS": return TimePeriodUnits.Weeks;
                    case "MONTHS": return TimePeriodUnits.Months;
                    case "YEARS": return TimePeriodUnits.Years;
                    default: break;
                }
                return TimePeriodUnits.None;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ReportSettings.GetTimePeriodUnits()", ex);
            }
            return TimePeriodUnits.None;
        }
    }

    class GeneralSettings
    {
        XmlNode SettingsNode;
        public Int32 SummaryLocation = 0;
        public Boolean IsCustomerBillGenFormatInvoice, IsCustomerBillGenFormatQuotation;
        public Boolean IsCustomerBillPrintFormatInvoice, IsCustomerBillPrintFormatQuotation;
        public Int32 InvoiceQuotPrintCopies = 1;

        public void ReadSettingsFromNode(XmlNode Node)
        {
            try
            {
                SettingsNode = Node;
                String Value;
                if (XMLFileUtils.GetChildNodeValue(SettingsNode, "SummaryLocation", out Value)) SummaryLocation = Int32.Parse(Value);
                if (XMLFileUtils.GetChildNodeValue(SettingsNode, "CustomerBillGenFormatInvoice", out Value)) IsCustomerBillGenFormatInvoice = Boolean.Parse(Value);
                if (XMLFileUtils.GetChildNodeValue(SettingsNode, "CustomerBillGenFormatQuotation", out Value)) IsCustomerBillGenFormatQuotation = Boolean.Parse(Value);
                if (XMLFileUtils.GetChildNodeValue(SettingsNode, "CustomerBillPrintFormatInvoice", out Value)) IsCustomerBillPrintFormatInvoice = Boolean.Parse(Value);
                if (XMLFileUtils.GetChildNodeValue(SettingsNode, "CustomerBillPrintFormatQuotation", out Value)) IsCustomerBillPrintFormatQuotation = Boolean.Parse(Value);
                if (XMLFileUtils.GetChildNodeValue(SettingsNode, "InvoiceQuotationNoOfCopiesToPrint", out Value)) InvoiceQuotPrintCopies = Int32.Parse(Value);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("GeneralSettings.ReadSettingsFromNode()", ex);
            }
        }

        public void UpdateSettingsToNode()
        {
            try
            {
                XMLFileUtils.SetChildNodeValue(SettingsNode, "SummaryLocation", SummaryLocation.ToString());
                XMLFileUtils.SetChildNodeValue(SettingsNode, "CustomerBillGenFormatInvoice", IsCustomerBillGenFormatInvoice.ToString());
                XMLFileUtils.SetChildNodeValue(SettingsNode, "CustomerBillGenFormatQuotation", IsCustomerBillGenFormatQuotation.ToString());
                XMLFileUtils.SetChildNodeValue(SettingsNode, "CustomerBillPrintFormatInvoice", IsCustomerBillPrintFormatInvoice.ToString());
                XMLFileUtils.SetChildNodeValue(SettingsNode, "CustomerBillPrintFormatQuotation", IsCustomerBillPrintFormatQuotation.ToString());
                XMLFileUtils.SetChildNodeValue(SettingsNode, "InvoiceQuotationNoOfCopiesToPrint", InvoiceQuotPrintCopies.ToString());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("GeneralSettings.UpdateSettingsToNode()", ex);
            }
        }
    }

    class Settings
    {
        public ReportSettings InvoiceSettings, QuotationSettings, PurchaseOrderSettings;
        public GeneralSettings GeneralSettings;

        public Settings()
        {
            GeneralSettings = new GeneralSettings();
            InvoiceSettings = new ReportSettings();
            QuotationSettings = new ReportSettings();
            PurchaseOrderSettings = new ReportSettings();
        }

        public void LoadSettingsFromNode(XmlNode Node)
        {
            try
            {
                switch (Node.Name.ToUpper())
                {
                    case "GENERAL":
                        GeneralSettings.ReadSettingsFromNode(Node);
                        break;
                    case "INVOICE":
                        InvoiceSettings.ReadSettingsFromNode(Node, ReportType.INVOICE);
                        break;
                    case "QUOTATION":
                        QuotationSettings.ReadSettingsFromNode(Node, ReportType.QUOTATION);
                        break;
                    case "PURCHASEORDER":
                        PurchaseOrderSettings.ReadSettingsFromNode(Node, ReportType.PURCHASEORDER);
                        break;
                    default: break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("Settings.LoadSettingsFromNode()", ex);
            }
        }

        public void UpdateSettingsToNode()
        {
            try
            {
                GeneralSettings.UpdateSettingsToNode();
                InvoiceSettings.UpdateSettingsToNode();
                QuotationSettings.UpdateSettingsToNode();
                PurchaseOrderSettings.UpdateSettingsToNode();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("Settings.UpdateSettingsToNode()", ex);
            }
        }
    }
}
