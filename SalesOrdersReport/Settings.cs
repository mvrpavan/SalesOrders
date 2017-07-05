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
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ApplicationSettings.UpdateSettingsToNode()", ex);
            }
        }
    }

    class ReportSettings
    {
        XmlNode SettingsNode;
        public String HeaderTitle, HeaderSubTitle, FooterTitle, Address, PhoneNumber, EMailID, VATPercent, TINNumber;
        public Int32 LastNumber;
        public Color HeaderTitleColor, HeaderSubTitleColor, FooterTitleColor, FooterTextColor;

        public void ReadSettingsFromNode(XmlNode Node)
        {
            try
            {
                SettingsNode = Node;

                String Value;
                XMLFileUtils.GetChildNodeValue(SettingsNode, "HeaderTitle", out HeaderTitle);
                XMLFileUtils.GetChildNodeValue(SettingsNode, "HeaderSubTitle", out HeaderSubTitle);
                XMLFileUtils.GetChildNodeValue(SettingsNode, "FooterTitle", out FooterTitle);
                XMLFileUtils.GetChildNodeValue(SettingsNode, "Address", out Address);
                XMLFileUtils.GetChildNodeValue(SettingsNode, "PhoneNumber", out PhoneNumber);
                XMLFileUtils.GetChildNodeValue(SettingsNode, "EMailID", out EMailID);
                XMLFileUtils.GetChildNodeValue(SettingsNode, "VATPercent", out VATPercent);
                XMLFileUtils.GetChildNodeValue(SettingsNode, "TINNumber", out TINNumber);

                if (XMLFileUtils.GetChildNodeValue(SettingsNode, "LastNumber", out Value)) LastNumber = Int32.Parse(Value);

                if (XMLFileUtils.GetChildNodeValue(SettingsNode, "HeaderTitleColor", out Value)) HeaderTitleColor = CommonFunctions.GetColor(Value);
                if (XMLFileUtils.GetChildNodeValue(SettingsNode, "HeaderSubTitleColor", out Value)) HeaderSubTitleColor = CommonFunctions.GetColor(Value);
                if (XMLFileUtils.GetChildNodeValue(SettingsNode, "FooterTitleColor", out Value)) FooterTitleColor = CommonFunctions.GetColor(Value);
                if (XMLFileUtils.GetChildNodeValue(SettingsNode, "FooterTextColor", out Value)) FooterTextColor = CommonFunctions.GetColor(Value);
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
                XMLFileUtils.SetChildNodeValue(SettingsNode, "HeaderTitle", HeaderTitle);
                XMLFileUtils.SetChildNodeValue(SettingsNode, "HeaderSubTitle", HeaderSubTitle);
                XMLFileUtils.SetChildNodeValue(SettingsNode, "FooterTitle", FooterTitle);
                XMLFileUtils.SetChildNodeValue(SettingsNode, "Address", Address);
                XMLFileUtils.SetChildNodeValue(SettingsNode, "PhoneNumber", PhoneNumber);
                XMLFileUtils.SetChildNodeValue(SettingsNode, "EMailID", EMailID);
                XMLFileUtils.SetChildNodeValue(SettingsNode, "VATPercent", VATPercent);
                XMLFileUtils.SetChildNodeValue(SettingsNode, "TINNumber", TINNumber);

                XMLFileUtils.SetChildNodeValue(SettingsNode, "LastNumber", LastNumber.ToString());

                XMLFileUtils.SetChildNodeValue(SettingsNode, "HeaderTitleColor", HeaderTitleColor.ToArgb().ToString());
                XMLFileUtils.SetChildNodeValue(SettingsNode, "HeaderSubTitleColor", HeaderSubTitleColor.ToArgb().ToString());
                XMLFileUtils.SetChildNodeValue(SettingsNode, "FooterTitleColor", FooterTitleColor.ToArgb().ToString());
                XMLFileUtils.SetChildNodeValue(SettingsNode, "FooterTextColor", FooterTextColor.ToArgb().ToString());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ReportSettings.UpdateSettingsToNode()", ex);
            }
        }
    }

    class GeneralSettings
    {
        XmlNode SettingsNode;
        public Int32 SummaryLocation = 0;

        public void ReadSettingsFromNode(XmlNode Node)
        {
            try
            {
                SettingsNode = Node;
                String Value;
                if (XMLFileUtils.GetChildNodeValue(SettingsNode, "SummaryLocation", out Value)) SummaryLocation = Int32.Parse(Value);
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
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("GeneralSettings.UpdateSettingsToNode()", ex);
            }
        }
    }

    class Settings
    {
        public ReportSettings InvoiceSettings, QuotationSettings;
        public GeneralSettings GeneralSettings;

        public Settings()
        {
            GeneralSettings = new GeneralSettings();
            InvoiceSettings = new ReportSettings();
            QuotationSettings = new ReportSettings();
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
                        InvoiceSettings.ReadSettingsFromNode(Node);
                        break;
                    case "QUOTATION":
                        QuotationSettings.ReadSettingsFromNode(Node);
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
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("Settings.UpdateSettingsToNode()", ex);
            }
        }
    }
}
