using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Models;
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
            //cmbBoxProductLines.Items.Clear();
            //for (int i = 1; i < CommonFunctions.ListProductLines.Count; i++)
            //{
            //    ProductLine ObjProductLine = CommonFunctions.ListProductLines[i];
            //    cmbBoxProductLines.Items.Add(ObjProductLine.Name);
            //}
            //cmbBoxProductLines.SelectedIndex = CommonFunctions.SelectedProductLineIndex - 1;

            ////Load cmbBoxPeriodUnits
            //cmbBoxPeriodUnits.Items.Clear();
            //cmbBoxPeriodUnits.Items.Add("Days");
            //cmbBoxPeriodUnits.Items.Add("Weeks");
            //cmbBoxPeriodUnits.Items.Add("Months");
            //cmbBoxPeriodUnits.Items.Add("Years");
            //cmbBoxPeriodUnits.SelectedIndex = 0;

            //LoadSettings();
        }

        void LoadSettings()
        {
            try
            {
                //Load General Settings from CommonFunctions Module
                ddlSummaryLocation.Items.Clear();
                ddlSummaryLocation.Items.Add("Order");
                ddlSummaryLocation.Items.Add("Invoice");
                ddlSummaryLocation.Items.Add("Quotation");
                ddlSummaryLocation.SelectedIndex = CommonFunctions.ObjGeneralSettings.SummaryLocation;
                chkBoxInstBillGenInvoice.Checked = CommonFunctions.ObjGeneralSettings.IsCustomerBillGenFormatInvoice;
                chkBoxInstBillGenQuot.Checked = CommonFunctions.ObjGeneralSettings.IsCustomerBillGenFormatQuotation;
                chkBoxInstBillPrintInvoice.Checked = CommonFunctions.ObjGeneralSettings.IsCustomerBillPrintFormatInvoice;
                chkBoxInstBillPrintQuot.Checked = CommonFunctions.ObjGeneralSettings.IsCustomerBillPrintFormatQuotation;
                txtBoxPrintCopies.Text = CommonFunctions.ObjGeneralSettings.InvoiceQuotPrintCopies.ToString();

                //Load Invoice Settings from CommonFunctions Module
                ReportSettings CurrSettings = CommonFunctions.ObjInvoiceSettings;
                txtBoxHeaderTitleInv.Text = CurrSettings.HeaderTitle;
                txtBoxHeaderSubTitleInv.Text = CurrSettings.HeaderSubTitle;
                txtBoxHeaderTitleColorInv.BackColor = CurrSettings.HeaderTitleColor;
                txtBoxHeaderSubTitleColorInv.BackColor = CurrSettings.HeaderSubTitleColor;
                txtBoxFooterTitleInv.Text = CurrSettings.FooterTitle;
                txtBoxFooterTitleColorInv.BackColor = CurrSettings.FooterTitleColor;
                txtBoxFooterTextColorInv.BackColor = CurrSettings.FooterTextColor;
                txtBoxAddressInv.Text = CurrSettings.Address;
                txtBoxPhoneNumberInv.Text = CurrSettings.PhoneNumber;
                txtBoxEMailIDInv.Text = CurrSettings.EMailID;
                txtBoxVATPercentInv.Text = CurrSettings.VATPercent;
                txtBoxTINNumberInv.Text = CurrSettings.TINNumber;
                txtBoxGSTINumberInv.Text = CurrSettings.GSTINumber;
                txtBoxLastInvoiceNumberInv.Text = CurrSettings.LastNumber.ToString();

                //Load Quotation Settings from CommonFunctions Module
                CurrSettings = CommonFunctions.ObjQuotationSettings;
                txtBoxHeaderTitleQuot.Text = CurrSettings.HeaderTitle;
                txtBoxHeaderSubTitleQuot.Text = CurrSettings.HeaderSubTitle;
                txtBoxHeaderTitleColorQuot.BackColor = CurrSettings.HeaderTitleColor;
                txtBoxHeaderSubTitleColorQuot.BackColor = CurrSettings.HeaderSubTitleColor;
                txtBoxFooterTitleQuot.Text = CurrSettings.FooterTitle;
                txtBoxFooterTitleColorQuot.BackColor = CurrSettings.FooterTitleColor;
                txtBoxFooterTextColorQuot.BackColor = CurrSettings.FooterTextColor;
                txtBoxAddressQuot.Text = CurrSettings.Address;
                txtBoxPhoneNumberQuot.Text = CurrSettings.PhoneNumber;
                txtBoxEMailIDQuot.Text = CurrSettings.EMailID;
                txtBoxTINNumberQuot.Text = CurrSettings.TINNumber;
                txtBoxGSTINumberQuot.Text = CurrSettings.GSTINumber;
                txtBoxLastQuotationNumberQuot.Text = CurrSettings.LastNumber.ToString();

                //Load Purchase Order Settings from CommonFunctions Module
                CurrSettings = CommonFunctions.ObjPurchaseOrderSettings;
                txtBoxHeaderTitlePO.Text = CurrSettings.HeaderTitle;
                txtBoxHeaderSubTitlePO.Text = CurrSettings.HeaderSubTitle;
                txtBoxHeaderTitleColorPO.BackColor = CurrSettings.HeaderTitleColor;
                txtBoxHeaderSubTitleColorPO.BackColor = CurrSettings.HeaderSubTitleColor;
                txtBoxFooterTitlePO.Text = CurrSettings.FooterTitle;
                txtBoxFooterTitleColorPO.BackColor = CurrSettings.FooterTitleColor;
                txtBoxFooterTextColorPO.BackColor = CurrSettings.FooterTextColor;
                txtBoxAddressPO.Text = CurrSettings.Address;
                txtBoxPhoneNumberPO.Text = CurrSettings.PhoneNumber;
                txtBoxEMailIDPO.Text = CurrSettings.EMailID;
                txtBoxVATPercentPO.Text = CurrSettings.VATPercent;
                txtBoxTINNumberPO.Text = CurrSettings.TINNumber;
                txtBoxGSTINumberPO.Text = CurrSettings.GSTINumber;
                txtBoxLastPONumber.Text = CurrSettings.LastNumber.ToString();
                numUpDownPeriodValue.Value = CurrSettings.PastSalePeriodValue;
                cmbBoxPeriodUnits.SelectedIndex = (Int32)CurrSettings.PastSalePeriodUnits;

                //Load Printers
                List<PrinterDetails> ListPrinters = CommonFunctions.GetPrinterList();
                cmbBoxPrinters.Items.Clear();
                cmbBoxPrinters.Items.Add("<Select Printer>");
                foreach (var item in ListPrinters)
                    cmbBoxPrinters.Items.Add(item.Name);

                Int32 DefaultPrinterIndex = ListPrinters.FindIndex(e => e.IsDefault == true);
                if (!String.IsNullOrEmpty(CommonFunctions.ObjGeneralSettings.PrinterName))
                    cmbBoxPrinters.SelectedItem = CommonFunctions.ObjGeneralSettings.PrinterName;
                else if (DefaultPrinterIndex >= 0)
                {
                    cmbBoxPrinters.SelectedItem = ListPrinters[DefaultPrinterIndex].Name;
                    CommonFunctions.ObjGeneralSettings.PrinterName = ListPrinters[DefaultPrinterIndex].Name;
                    CommonFunctions.WriteToSettingsFile();
                }
                else
                    cmbBoxPrinters.SelectedIndex = 0;

                if (DefaultPrinterIndex >= 0) cmbBoxPrinters.Items[DefaultPrinterIndex + 1] += " (Default)";
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
                Int32 result;
                if (Int32.TryParse(txtBoxPrintCopies.Text, out result))
                {
                    CommonFunctions.ObjGeneralSettings.InvoiceQuotPrintCopies = result;
                }
                else
                {
                    MessageBox.Show(this, "Invalid Print Copies setting!", "Print Invoice/Quotation", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }
                CommonFunctions.ObjGeneralSettings.SummaryLocation = ddlSummaryLocation.SelectedIndex;
                CommonFunctions.ObjGeneralSettings.IsCustomerBillGenFormatInvoice = chkBoxInstBillGenInvoice.Checked;
                CommonFunctions.ObjGeneralSettings.IsCustomerBillGenFormatQuotation = chkBoxInstBillGenQuot.Checked;
                CommonFunctions.ObjGeneralSettings.IsCustomerBillPrintFormatInvoice = chkBoxInstBillPrintInvoice.Checked;
                CommonFunctions.ObjGeneralSettings.IsCustomerBillPrintFormatQuotation = chkBoxInstBillPrintQuot.Checked;
                if (cmbBoxPrinters.SelectedIndex > 0) CommonFunctions.ObjGeneralSettings.PrinterName = cmbBoxPrinters.SelectedItem.ToString().Replace(" (Default)", "");

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
                CurrSettings.GSTINumber = txtBoxGSTINumberInv.Text;
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
                CurrSettings.GSTINumber = txtBoxGSTINumberQuot.Text;
                CurrSettings.LastNumber = Int32.Parse(txtBoxLastQuotationNumberQuot.Text);

                //Apply Purchase Order Settings to CommonFunctions Module
                CurrSettings = CommonFunctions.ObjPurchaseOrderSettings;
                CurrSettings.HeaderTitle = txtBoxHeaderTitlePO.Text;
                CurrSettings.HeaderSubTitle = txtBoxHeaderSubTitlePO.Text;
                CurrSettings.HeaderTitleColor = txtBoxHeaderTitleColorPO.BackColor;
                CurrSettings.HeaderSubTitleColor = txtBoxHeaderSubTitleColorPO.BackColor;
                CurrSettings.FooterTitle = txtBoxFooterTitlePO.Text;
                CurrSettings.FooterTitleColor = txtBoxFooterTitleColorPO.BackColor;
                CurrSettings.FooterTextColor = txtBoxFooterTextColorPO.BackColor;
                CurrSettings.Address = txtBoxAddressPO.Text;
                CurrSettings.PhoneNumber = txtBoxPhoneNumberPO.Text;
                CurrSettings.EMailID = txtBoxEMailIDPO.Text;
                CurrSettings.VATPercent = txtBoxVATPercentPO.Text;
                CurrSettings.TINNumber = txtBoxTINNumberPO.Text;
                CurrSettings.GSTINumber = txtBoxGSTINumberPO.Text;
                CurrSettings.LastNumber = Int32.Parse(txtBoxLastPONumber.Text);
                CurrSettings.PastSalePeriodValue = (Int32)numUpDownPeriodValue.Value;
                CurrSettings.PastSalePeriodUnits = ReportSettings.GetTimePeriodUnits(cmbBoxPeriodUnits.SelectedItem.ToString());

                CommonFunctions.WriteToSettingsFile();      //Save to Settings.xml file

                MessageBox.Show(this, "Settings Updated Successfully", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnHeaderTitleColorPO_Click(object sender, EventArgs e)
        {
            DialogResult Result = colorDialogSettings.ShowDialog(this);
            if (Result == System.Windows.Forms.DialogResult.OK || Result == System.Windows.Forms.DialogResult.Yes)
                txtBoxHeaderTitleColorPO.BackColor = colorDialogSettings.Color;
        }

        private void btnHeaderSubTitleColorPO_Click(object sender, EventArgs e)
        {
            DialogResult Result = colorDialogSettings.ShowDialog(this);
            if (Result == System.Windows.Forms.DialogResult.OK || Result == System.Windows.Forms.DialogResult.Yes)
                txtBoxHeaderSubTitleColorPO.BackColor = colorDialogSettings.Color;
        }

        private void btnFooterTitleColorPO_Click(object sender, EventArgs e)
        {
            DialogResult Result = colorDialogSettings.ShowDialog(this);
            if (Result == System.Windows.Forms.DialogResult.OK || Result == System.Windows.Forms.DialogResult.Yes)
                btnFooterTitleColorPO.BackColor = colorDialogSettings.Color;
        }

        private void btnFooterTextColorPO_Click(object sender, EventArgs e)
        {
            DialogResult Result = colorDialogSettings.ShowDialog(this);
            if (Result == System.Windows.Forms.DialogResult.OK || Result == System.Windows.Forms.DialogResult.Yes)
                btnFooterTextColorPO.BackColor = colorDialogSettings.Color;
        }
        #endregion

        private void cmbBoxProductLines_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (cmbBoxProductLines.SelectedIndex + 1 == CommonFunctions.SelectedProductLineIndex) return;

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

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            //Load cmbBoxPeriodUnits
            cmbBoxPeriodUnits.Items.Clear();
            cmbBoxPeriodUnits.Items.Add("Days");
            cmbBoxPeriodUnits.Items.Add("Weeks");
            cmbBoxPeriodUnits.Items.Add("Months");
            cmbBoxPeriodUnits.Items.Add("Years");
            cmbBoxPeriodUnits.SelectedIndex = 0;

            //Load ProductLines from CommonFunctions Module
            cmbBoxProductLines.Items.Clear();
            for (int i = 1; i < CommonFunctions.ListProductLines.Count; i++)
            {
                ProductLine ObjProductLine = CommonFunctions.ListProductLines[i];
                cmbBoxProductLines.Items.Add(ObjProductLine.Name);
            }
            cmbBoxProductLines.SelectedIndex = CommonFunctions.SelectedProductLineIndex - 1;
        }

        private void chkBoxInstBillGenInvoice_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                chkBoxInstBillPrintInvoice.Enabled = false;
                if (!chkBoxInstBillGenInvoice.Checked)
                {
                    if (!chkBoxInstBillGenQuot.Checked) chkBoxInstBillGenQuot.Checked = true;
                    chkBoxInstBillPrintInvoice.Checked = false;
                }
                else
                {
                    chkBoxInstBillPrintInvoice.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SettingsForm.chkBoxInstBillGenInvoice_CheckedChanged()", ex);
            }
        }

        private void chkBoxInstBillGenQuot_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                chkBoxInstBillPrintQuot.Enabled = false;
                if (!chkBoxInstBillGenQuot.Checked)
                {
                    if (!chkBoxInstBillGenInvoice.Checked) chkBoxInstBillGenInvoice.Checked = true;
                    chkBoxInstBillPrintQuot.Checked = false;
                }
                else
                {
                    chkBoxInstBillPrintQuot.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SettingsForm.chkBoxInstBillGenQuot_CheckedChanged()", ex);
            }
        }
    }
}
