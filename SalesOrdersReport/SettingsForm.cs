using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SalesOrdersReport
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        void LoadSettings()
        {
            try
            {
                //Load Invoice Settings from CommonFunctions Module
                txtBoxHeaderTitleInv.Text = CommonFunctions.InvoiceSettings.HeaderTitle;
                txtBoxHeaderSubTitleInv.Text = CommonFunctions.InvoiceSettings.HeaderSubTitle;
                txtBoxHeaderTitleColorInv.BackColor = CommonFunctions.InvoiceSettings.HeaderTitleColor;
                txtBoxHeaderSubTitleColorInv.BackColor = CommonFunctions.InvoiceSettings.HeaderSubTitleColor;
                txtBoxFooterTitleInv.Text = CommonFunctions.InvoiceSettings.FooterTitle;
                txtBoxFooterTitleColorInv.BackColor = CommonFunctions.InvoiceSettings.FooterTitleColor;
                txtBoxFooterTextColorInv.BackColor = CommonFunctions.InvoiceSettings.FooterTextColor;
                txtBoxAddressInv.Text = CommonFunctions.InvoiceSettings.Address;
                txtBoxPhoneNumberInv.Text = CommonFunctions.InvoiceSettings.PhoneNumber;
                txtBoxEMailIDInv.Text = CommonFunctions.InvoiceSettings.EMailID;
                txtBoxVATPercentInv.Text = CommonFunctions.InvoiceSettings.VATPercent;
                txtBoxTINNumberInv.Text = CommonFunctions.InvoiceSettings.TINNumber;
                txtBoxLastInvoiceNumberInv.Text = CommonFunctions.InvoiceSettings.LastNumber.ToString();

                //Load Quotation Settings from CommonFunctions Module
                txtBoxHeaderTitleQuot.Text = CommonFunctions.QuotationSettings.HeaderTitle;
                txtBoxHeaderSubTitleQuot.Text = CommonFunctions.QuotationSettings.HeaderSubTitle;
                txtBoxHeaderTitleColorQuot.BackColor = CommonFunctions.QuotationSettings.HeaderTitleColor;
                txtBoxHeaderSubTitleColorQuot.BackColor = CommonFunctions.QuotationSettings.HeaderSubTitleColor;
                txtBoxFooterTitleQuot.Text = CommonFunctions.QuotationSettings.FooterTitle;
                txtBoxFooterTitleColorQuot.BackColor = CommonFunctions.QuotationSettings.FooterTitleColor;
                txtBoxFooterTextColorQuot.BackColor = CommonFunctions.QuotationSettings.FooterTextColor;
                txtBoxAddressQuot.Text = CommonFunctions.QuotationSettings.Address;
                txtBoxPhoneNumberQuot.Text = CommonFunctions.QuotationSettings.PhoneNumber;
                txtBoxEMailIDQuot.Text = CommonFunctions.QuotationSettings.EMailID;
                txtBoxTINNumberQuot.Text = CommonFunctions.QuotationSettings.TINNumber;
                txtBoxLastQuotationNumberQuot.Text = CommonFunctions.QuotationSettings.LastNumber.ToString();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SettingsForm.LoadSettings", ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApplySettings_Click(object sender, EventArgs e)
        {
            try
            {
                //Apply Invoice Settings to CommonFunctions Module
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Invoice/HeaderTitle", txtBoxHeaderTitleInv.Text);
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Invoice/HeaderSubTitle", txtBoxHeaderSubTitleInv.Text);
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Invoice/HeaderTitleColor", txtBoxHeaderTitleColorInv.BackColor.ToArgb().ToString());
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Invoice/HeaderSubTitleColor", txtBoxHeaderSubTitleColorInv.BackColor.ToArgb().ToString());
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Invoice/FooterTitle", txtBoxFooterTitleInv.Text);
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Invoice/FooterTitleColor", txtBoxFooterTitleColorInv.BackColor.ToArgb().ToString());
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Invoice/FooterTextColor", txtBoxFooterTextColorInv.BackColor.ToArgb().ToString());
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Invoice/Address", txtBoxAddressInv.Text);
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Invoice/PhoneNumber", txtBoxPhoneNumberInv.Text);
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Invoice/EMailID", txtBoxEMailIDInv.Text);
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Invoice/VATPercent", txtBoxVATPercentInv.Text);
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Invoice/TINNumber", txtBoxTINNumberInv.Text);
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Invoice/LastInvoiceNumber", txtBoxLastInvoiceNumberInv.Text);

                //Apply Quotation Settings to CommonFunctions Module
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Quotation/HeaderTitle", txtBoxHeaderTitleQuot.Text);
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Quotation/HeaderSubTitle", txtBoxHeaderSubTitleQuot.Text);
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Quotation/HeaderTitleColor", txtBoxHeaderTitleColorQuot.BackColor.ToArgb().ToString());
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Quotation/HeaderSubTitleColor", txtBoxHeaderSubTitleColorQuot.BackColor.ToArgb().ToString());
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Quotation/FooterTitle", txtBoxFooterTitleQuot.Text);
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Quotation/FooterTitleColor", txtBoxFooterTitleColorQuot.BackColor.ToArgb().ToString());
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Quotation/FooterTextColor", txtBoxFooterTextColorQuot.BackColor.ToArgb().ToString());
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Quotation/Address", txtBoxAddressQuot.Text);
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Quotation/PhoneNumber", txtBoxPhoneNumberQuot.Text);
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Quotation/EMailID", txtBoxEMailIDQuot.Text);
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Quotation/TINNumber", txtBoxTINNumberQuot.Text);
                CommonFunctions.UpdateSettingsFileEntry("//Settings/Quotation/LastQuotationNumber", txtBoxLastQuotationNumberQuot.Text);

                CommonFunctions.WriteToSettingsFile();      //Save to Settings.xml file
                CommonFunctions.LoadSettingsFile();         //Reload from Settings.xml file
                this.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SettingsForm.btnApplySettings_Click", ex);
                throw;
            }
        }

        private void btnFooterTitleColorInv_Click(object sender, EventArgs e)
        {
            colorDialogSettings.ShowDialog(this);
            txtBoxFooterTitleColorInv.BackColor = colorDialogSettings.Color;
        }

        private void btnFooterTextColorInv_Click(object sender, EventArgs e)
        {
            colorDialogSettings.ShowDialog(this);
            txtBoxFooterTextColorInv.BackColor = colorDialogSettings.Color;
        }

        private void btnHeaderTitleColorInv_Click(object sender, EventArgs e)
        {
            colorDialogSettings.ShowDialog(this);
            txtBoxHeaderTitleColorInv.BackColor = colorDialogSettings.Color;
        }

        private void btnHeaderSubTitleColorInv_Click(object sender, EventArgs e)
        {
            colorDialogSettings.ShowDialog(this);
            txtBoxHeaderSubTitleColorInv.BackColor = colorDialogSettings.Color;
        }

        private void btnFooterTitleColorQuot_Click(object sender, EventArgs e)
        {
            colorDialogSettings.ShowDialog(this);
            txtBoxFooterTitleColorQuot.BackColor = colorDialogSettings.Color;
        }

        private void btnFooterTextColorQuot_Click(object sender, EventArgs e)
        {
            colorDialogSettings.ShowDialog(this);
            txtBoxFooterTextColorQuot.BackColor = colorDialogSettings.Color;
        }

        private void btnHeaderTitleColorQuot_Click(object sender, EventArgs e)
        {
            colorDialogSettings.ShowDialog(this);
            txtBoxHeaderTitleColorQuot.BackColor = colorDialogSettings.Color;
        }

        private void btnHeaderSubTitleColorQuot_Click(object sender, EventArgs e)
        {
            colorDialogSettings.ShowDialog(this);
            txtBoxHeaderSubTitleColorQuot.BackColor = colorDialogSettings.Color;
        }
    }
}
