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
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = openFileDialog1.ShowDialog();
            if (dlgResult == System.Windows.Forms.DialogResult.OK || dlgResult == System.Windows.Forms.DialogResult.Yes)
            {
                txtBoxMasterFilePath.Text = openFileDialog1.FileName;
                btnOK.Enabled = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CommonFunctions.MasterFilePath = txtBoxMasterFilePath.Text;

            DataTable dtProductMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("ItemMaster", CommonFunctions.MasterFilePath, "*");
            DataTable dtPriceGroupMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("PriceGroupMaster", CommonFunctions.MasterFilePath, "*");
            CommonFunctions.ListProductLines[CommonFunctions.SelectedProductLineIndex].LoadProductMaster(dtProductMaster, dtPriceGroupMaster);

            DataTable dtDiscountGroupMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("DiscountGroupMaster", CommonFunctions.MasterFilePath, "*");
            DataTable dtSellerMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("SellerMaster", CommonFunctions.MasterFilePath, "*");
            CommonFunctions.ListProductLines[CommonFunctions.SelectedProductLineIndex].LoadSellerMaster(dtSellerMaster, dtDiscountGroupMaster);

            CommonFunctions.SelectProductLine(CommonFunctions.SelectedProductLineIndex);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
