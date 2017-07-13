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
    public partial class OrderMasterForm : Form
    {
        public OrderMasterForm()
        {
            InitializeComponent();

            btnOK.Enabled = false;
            lblStatus.Text = "Please select OrderMaster file";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlgResult = openFileDialog1.ShowDialog();
                if (dlgResult == System.Windows.Forms.DialogResult.OK || dlgResult == System.Windows.Forms.DialogResult.Yes)
                {
                    txtBoxMasterFilePath.Text = openFileDialog1.FileName;
                    btnOK.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("OrderMasterForm.btnBrowse_Click()", ex);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.MasterFilePath = txtBoxMasterFilePath.Text;
                CommonFunctions.ResetProgressBar();

                btnOK.Enabled = false;

                //LoadFromOrderMaster();
                bgWorkerOrderMaster.RunWorkerAsync();
                bgWorkerOrderMaster.WorkerReportsProgress = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("OrderMasterForm.btnOK_Click()", ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bgWorkerOrderMaster_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                LoadDetailsFromOrderMaster();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("OrderMasterForm.bgWorkerOrderMaster_DoWork()", ex);
            }
        }

        private void bgWorkerOrderMaster_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                CommonFunctions.UpdateProgressBar(e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("OrderMasterForm.bgWorkerOrderMaster_ProgressChanged()", ex);
            }
        }

        private void bgWorkerOrderMaster_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                CommonFunctions.ResetProgressBar();
                btnClose.Focus();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("OrderMasterForm.bgWorkerOrderMaster_RunWorkerCompleted()", ex);
            }
        }

        private void LoadDetailsFromOrderMaster()
        {
            try
            {
                DataTable dtProductMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("ItemMaster", CommonFunctions.MasterFilePath, "*");
                DataTable dtPriceGroupMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("PriceGroupMaster", CommonFunctions.MasterFilePath, "*");
                CommonFunctions.ListProductLines[CommonFunctions.SelectedProductLineIndex].LoadProductMaster(dtProductMaster, dtPriceGroupMaster);
                lblStatus.Text = "Completed loading Product details";
                bgWorkerOrderMaster.ReportProgress(25);

                DataTable dtDiscountGroupMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("DiscountGroupMaster", CommonFunctions.MasterFilePath, "*");
                DataTable dtSellerMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("SellerMaster", CommonFunctions.MasterFilePath, "*");
                CommonFunctions.ListProductLines[CommonFunctions.SelectedProductLineIndex].LoadSellerMaster(dtSellerMaster, dtDiscountGroupMaster);
                lblStatus.Text = "Completed loading Seller details";
                bgWorkerOrderMaster.ReportProgress(50);

                DataTable dtVendorMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("VendorMaster", CommonFunctions.MasterFilePath, "*");
                CommonFunctions.ListProductLines[CommonFunctions.SelectedProductLineIndex].LoadVendorMaster(dtVendorMaster, dtDiscountGroupMaster);
                lblStatus.Text = "Completed loading Vendor details";
                bgWorkerOrderMaster.ReportProgress(75);

                CommonFunctions.SelectProductLine(CommonFunctions.SelectedProductLineIndex);
                bgWorkerOrderMaster.ReportProgress(100);

                lblStatus.Text = "Completed loading details from OrderMaster file";
                MessageBox.Show(this, "Completed loading details from OrderMaster file", "Order Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("OrderMasterForm.LoadDetailsFromOrderMaster()", ex);
            }
        }
    }
}
