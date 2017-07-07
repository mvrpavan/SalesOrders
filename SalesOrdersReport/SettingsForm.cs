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

            //Load ProductLines from CommonFunctions Module
            cmbBoxProductLines.Items.Clear();
            for (int i = 1; i < CommonFunctions.ListProductLines.Count; i++)
            {
                ProductLine ObjProductLine = CommonFunctions.ListProductLines[i];
                cmbBoxProductLines.Items.Add(ObjProductLine.Name);
            }
            cmbBoxProductLines.SelectedIndex = CommonFunctions.SelectedProductLineIndex - 1;
            
            LoadSettings();
        }

        void LoadSettings()
        {
            try
            {
                //Load General Settings from CommonFunctions Module
                ddlSummaryLocation.Items.Clear();
                ddlSummaryLocation.Items.Add("Invoice");
                ddlSummaryLocation.Items.Add("Quotation");
                ddlSummaryLocation.SelectedIndex = CommonFunctions.ObjGeneralSettings.SummaryLocation;

                //Load Invoice Settings from CommonFunctions Module
                txtBoxHeaderTitleInv.Text = CommonFunctions.ObjInvoiceSettings.HeaderTitle;
                txtBoxHeaderSubTitleInv.Text = CommonFunctions.ObjInvoiceSettings.HeaderSubTitle;
                txtBoxHeaderTitleColorInv.BackColor = CommonFunctions.ObjInvoiceSettings.HeaderTitleColor;
                txtBoxHeaderSubTitleColorInv.BackColor = CommonFunctions.ObjInvoiceSettings.HeaderSubTitleColor;
                txtBoxFooterTitleInv.Text = CommonFunctions.ObjInvoiceSettings.FooterTitle;
                txtBoxFooterTitleColorInv.BackColor = CommonFunctions.ObjInvoiceSettings.FooterTitleColor;
                txtBoxFooterTextColorInv.BackColor = CommonFunctions.ObjInvoiceSettings.FooterTextColor;
                txtBoxAddressInv.Text = CommonFunctions.ObjInvoiceSettings.Address;
                txtBoxPhoneNumberInv.Text = CommonFunctions.ObjInvoiceSettings.PhoneNumber;
                txtBoxEMailIDInv.Text = CommonFunctions.ObjInvoiceSettings.EMailID;
                txtBoxVATPercentInv.Text = CommonFunctions.ObjInvoiceSettings.VATPercent;
                txtBoxTINNumberInv.Text = CommonFunctions.ObjInvoiceSettings.TINNumber;
                txtBoxLastInvoiceNumberInv.Text = CommonFunctions.ObjInvoiceSettings.LastNumber.ToString();

                //Load Quotation Settings from CommonFunctions Module
                txtBoxHeaderTitleQuot.Text = CommonFunctions.ObjQuotationSettings.HeaderTitle;
                txtBoxHeaderSubTitleQuot.Text = CommonFunctions.ObjQuotationSettings.HeaderSubTitle;
                txtBoxHeaderTitleColorQuot.BackColor = CommonFunctions.ObjQuotationSettings.HeaderTitleColor;
                txtBoxHeaderSubTitleColorQuot.BackColor = CommonFunctions.ObjQuotationSettings.HeaderSubTitleColor;
                txtBoxFooterTitleQuot.Text = CommonFunctions.ObjQuotationSettings.FooterTitle;
                txtBoxFooterTitleColorQuot.BackColor = CommonFunctions.ObjQuotationSettings.FooterTitleColor;
                txtBoxFooterTextColorQuot.BackColor = CommonFunctions.ObjQuotationSettings.FooterTextColor;
                txtBoxAddressQuot.Text = CommonFunctions.ObjQuotationSettings.Address;
                txtBoxPhoneNumberQuot.Text = CommonFunctions.ObjQuotationSettings.PhoneNumber;
                txtBoxEMailIDQuot.Text = CommonFunctions.ObjQuotationSettings.EMailID;
                txtBoxTINNumberQuot.Text = CommonFunctions.ObjQuotationSettings.TINNumber;
                txtBoxLastQuotationNumberQuot.Text = CommonFunctions.ObjQuotationSettings.LastNumber.ToString();
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
                //Apply General Settings to CommonFunctions Module
                CommonFunctions.ObjGeneralSettings.SummaryLocation = ddlSummaryLocation.SelectedIndex;

                //Apply Invoice Settings to CommonFunctions Module
                ReportSettings CurrSettings = CommonFunctions.ObjInvoiceSettings;
                CurrSettings.HeaderTitle = txtBoxHeaderTitleInv.Text;
                CurrSettings.HeaderSubTitle = txtBoxHeaderSubTitleInv.Text;
                CurrSettings.HeaderTitleColor = txtBoxHeaderTitleColorInv.BackColor;
                CurrSettings.HeaderSubTitleColor = txtBoxHeaderSubTitleColorInv.BackColor;
                CurrSettings.FooterTitle = txtBoxFooterTitleInv.Text;
                CurrSettings.FooterTitleColor = txtBoxFooterTitleColorInv.BackColor;
                CurrSettings.FooterTextColor = txtBoxFooterTextColorInv.BackColor;
                CurrSettings.Address = txtBoxAddressInv.Text;
                CurrSettings.PhoneNumber = txtBoxPhoneNumberInv.Text;
                CurrSettings.EMailID = txtBoxEMailIDInv.Text;
                CurrSettings.VATPercent = txtBoxVATPercentInv.Text;
                CurrSettings.TINNumber = txtBoxTINNumberInv.Text;
                CurrSettings.LastNumber = Int32.Parse(txtBoxLastInvoiceNumberInv.Text);

                //Apply Quotation Settings to CommonFunctions Module
                CurrSettings = CommonFunctions.ObjQuotationSettings;
                CurrSettings.HeaderTitle = txtBoxHeaderTitleQuot.Text;
                CurrSettings.HeaderSubTitle = txtBoxHeaderSubTitleQuot.Text;
                CurrSettings.HeaderTitleColor = txtBoxHeaderTitleColorQuot.BackColor;
                CurrSettings.HeaderSubTitleColor = txtBoxHeaderSubTitleColorQuot.BackColor;
                CurrSettings.FooterTitle = txtBoxFooterTitleQuot.Text;
                CurrSettings.FooterTitleColor = txtBoxFooterTitleColorQuot.BackColor;
                CurrSettings.FooterTextColor = txtBoxFooterTextColorQuot.BackColor;
                CurrSettings.Address = txtBoxAddressQuot.Text;
                CurrSettings.PhoneNumber = txtBoxPhoneNumberQuot.Text;
                CurrSettings.EMailID = txtBoxEMailIDQuot.Text;
                CurrSettings.TINNumber = txtBoxTINNumberQuot.Text;
                CurrSettings.LastNumber = Int32.Parse(txtBoxLastQuotationNumberQuot.Text);

                CommonFunctions.WriteToSettingsFile();      //Save to Settings.xml file
                //CommonFunctions.LoadSettingsFile();         //Reload from Settings.xml file
                this.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SettingsForm.btnApplySettings_Click", ex);
                throw;
            }
        }

        #region Color Settings
        private void btnFooterTitleColorInv_Click(object sender, EventArgs e)
        {
            DialogResult Result = colorDialogSettings.ShowDialog(this);
            if (Result == System.Windows.Forms.DialogResult.OK || Result == System.Windows.Forms.DialogResult.Yes)
                txtBoxFooterTitleColorInv.BackColor = colorDialogSettings.Color;
        }

        private void btnFooterTextColorInv_Click(object sender, EventArgs e)
        {
            DialogResult Result = colorDialogSettings.ShowDialog(this);
            if (Result == System.Windows.Forms.DialogResult.OK || Result == System.Windows.Forms.DialogResult.Yes)
                txtBoxFooterTextColorInv.BackColor = colorDialogSettings.Color;
        }

        private void btnHeaderTitleColorInv_Click(object sender, EventArgs e)
        {
            DialogResult Result = colorDialogSettings.ShowDialog(this);
            if (Result == System.Windows.Forms.DialogResult.OK || Result == System.Windows.Forms.DialogResult.Yes)
                txtBoxHeaderTitleColorInv.BackColor = colorDialogSettings.Color;
        }

        private void btnHeaderSubTitleColorInv_Click(object sender, EventArgs e)
        {
            DialogResult Result = colorDialogSettings.ShowDialog(this);
            if (Result == System.Windows.Forms.DialogResult.OK || Result == System.Windows.Forms.DialogResult.Yes)
                txtBoxHeaderSubTitleColorInv.BackColor = colorDialogSettings.Color;
        }

        private void btnFooterTitleColorQuot_Click(object sender, EventArgs e)
        {
            DialogResult Result = colorDialogSettings.ShowDialog(this);
            if (Result == System.Windows.Forms.DialogResult.OK || Result == System.Windows.Forms.DialogResult.Yes)
                txtBoxFooterTitleColorQuot.BackColor = colorDialogSettings.Color;
        }

        private void btnFooterTextColorQuot_Click(object sender, EventArgs e)
        {
            DialogResult Result = colorDialogSettings.ShowDialog(this);
            if (Result == System.Windows.Forms.DialogResult.OK || Result == System.Windows.Forms.DialogResult.Yes)
                txtBoxFooterTextColorQuot.BackColor = colorDialogSettings.Color;
        }

        private void btnHeaderTitleColorQuot_Click(object sender, EventArgs e)
        {
            DialogResult Result = colorDialogSettings.ShowDialog(this);
            if (Result == System.Windows.Forms.DialogResult.OK || Result == System.Windows.Forms.DialogResult.Yes)
                txtBoxHeaderTitleColorQuot.BackColor = colorDialogSettings.Color;
        }

        private void btnHeaderSubTitleColorQuot_Click(object sender, EventArgs e)
        {
            DialogResult Result = colorDialogSettings.ShowDialog(this);
            if (Result == System.Windows.Forms.DialogResult.OK || Result == System.Windows.Forms.DialogResult.Yes)
                txtBoxHeaderSubTitleColorQuot.BackColor = colorDialogSettings.Color;
        }
#endregion

        private void cmbBoxProductLines_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxProductLines.SelectedIndex + 1 == CommonFunctions.SelectedProductLineIndex) return;
                CommonFunctions.SelectProductLine(Int32.Parse((cmbBoxProductLines.SelectedIndex + 1).ToString()), true);
                LoadSettings();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SettingsForm.cmbBoxProductLines_SelectedIndexChanged()", ex);
            }
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                CommonFunctions.SelectProductLine(CommonFunctions.SelectedProductLineIndex, false);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SettingsForm.SettingsForm_FormClosed()", ex);
            }
        }
    }
}
