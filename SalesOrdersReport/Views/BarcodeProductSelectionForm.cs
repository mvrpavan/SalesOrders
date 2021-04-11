using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesOrdersReport.Views
{
    public partial class BarcodeProductSelectionForm : Form
    {
        private Action<int, object> updateFormOnClose;
        String Barcode;

        public BarcodeProductSelectionForm(String Barcode, Action<int, object> updateFormOnClose)
        {
            try
            {
                InitializeComponent();

                this.Barcode = Barcode;
                this.updateFormOnClose = updateFormOnClose;
                CommonFunctions.SetDataGridViewProperties(dtGridViewProducts);
                List<String> ListBarcodes = CommonFunctions.ObjProductMaster.GetAllBarcodes();
                cmbBoxBarcodes.DataSource = ListBarcodes;

                if (String.IsNullOrEmpty(Barcode))
                {
                    //txtBoxBarcode.Focus();
                    cmbBoxBarcodes.Focus();
                }
                else
                {
                    //txtBoxBarcode.Text = Barcode;
                    cmbBoxBarcodes.SelectedIndex = ListBarcodes.IndexOf(Barcode);
                    btnSearchBarcode.PerformClick();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ctor()", ex);
                throw;
            }
        }

        private void btnSearchBarcode_Click(object sender, EventArgs e)
        {
            try
            {
                ProductMasterModel ObjProductMaster = CommonFunctions.ObjProductMaster;
                String Barcode = cmbBoxBarcodes.SelectedItem.ToString();
                List<Int32> ListProductIDs = ObjProductMaster.GetProductIDListForBarcode(Barcode);
                if (ListProductIDs == null || ListProductIDs.Count == 0)
                {
                    MessageBox.Show(this, "No Products found for Barcode", "Search Barcode", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }

                DataTable dtProducts = new DataTable();
                dtProducts.Columns.Add("Barcode");
                dtProducts.Columns.Add("SKU");
                dtProducts.Columns.Add("Product Name");
                dtProducts.Columns.Add("ProductID");

                foreach (var ProductID in ListProductIDs)
                {
                    ProductDetails tmpProductDetails = ObjProductMaster.GetProductDetails(ProductID);
                    if (tmpProductDetails == null || tmpProductDetails.Active == false) continue;
                    DataRow dtRow = dtProducts.NewRow();
                    dtRow["Barcode"] = Barcode;
                    dtRow["SKU"] = tmpProductDetails.ProductSKU;
                    dtRow["Product Name"] = tmpProductDetails.ItemName;
                    dtRow["ProductID"] = tmpProductDetails.ProductID;

                    dtProducts.Rows.Add(dtRow);
                }

                if (dtProducts.Rows.Count == 0)
                {
                    MessageBox.Show(this, "No Active Products found for Barcode", "Search Barcode", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }

                dtGridViewProducts.Rows.Clear();
                dtGridViewProducts.DataSource = dtProducts;
                dtGridViewProducts.ReadOnly = true;
                dtGridViewProducts.Columns["ProductID"].Visible = false;
                dtGridViewProducts.Rows[0].Selected = true;
                dtGridViewProducts.Focus();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnSearchBarcode_Click()", ex);
                throw;
            }
        }

        private void btnSelectProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewProducts.SelectedRows.Count == 0) return;

                updateFormOnClose(2, Int32.Parse(dtGridViewProducts["ProductID", dtGridViewProducts.SelectedRows[0].Index].ToString()));
                btnCancel.PerformClick();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnSelectProduct_Click()", ex);
                throw;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnCancel_Click()", ex);
                throw;
            }
        }

        private void cmbBoxBarcodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnSearchBarcode.PerformClick();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxBarcodes_SelectedIndexChanged()", ex);
                throw;
            }
        }
    }
}
