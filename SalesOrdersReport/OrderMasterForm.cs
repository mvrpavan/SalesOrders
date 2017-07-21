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
                openFileDialog1.Multiselect = false;
                openFileDialog1.FileName = "OrderMaster.xlsx";
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

                //LoadDetailsFromOrderMaster();
                ReportProgress = bgWorkerOrderMaster.ReportProgress;
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

        delegate void ReportProgressDel(Int32 ProgressState);
        ReportProgressDel ReportProgress = null;

        private void ReportProgressFunc(Int32 ProgressState)
        {
            if (ReportProgress == null) return;
            ReportProgress(ProgressState);
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
                ProductLine CurrProductLine = CommonFunctions.ListProductLines[CommonFunctions.SelectedProductLineIndex];

                DataTable dtProductMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("ItemMaster", CommonFunctions.MasterFilePath, "*");
                DataTable dtPriceGroupMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("PriceGroupMaster", CommonFunctions.MasterFilePath, "*");
                DataTable dtHSNMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("HSNMaster", CommonFunctions.MasterFilePath, "*");
                CurrProductLine.LoadProductMaster(dtProductMaster, dtPriceGroupMaster, dtHSNMaster);
                lblStatus.Text = "Completed loading Product details";
                ReportProgressFunc(25);

                DataTable dtDiscountGroupMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("DiscountGroupMaster", CommonFunctions.MasterFilePath, "*");
                DataTable dtSellerMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("SellerMaster", CommonFunctions.MasterFilePath, "*");
                CurrProductLine.LoadSellerMaster(dtSellerMaster, dtDiscountGroupMaster);
                lblStatus.Text = "Completed loading Seller details";
                ReportProgressFunc(50);

                DataTable dtVendorMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("VendorMaster", CommonFunctions.MasterFilePath, "*");
                CurrProductLine.LoadVendorMaster(dtVendorMaster, dtDiscountGroupMaster);
                lblStatus.Text = "Completed loading Vendor details";
                ReportProgressFunc(75);

                CommonFunctions.SelectProductLine(CommonFunctions.SelectedProductLineIndex);
                ReportProgressFunc(100);

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
