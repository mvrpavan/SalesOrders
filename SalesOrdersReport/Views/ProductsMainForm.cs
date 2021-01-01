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
    public delegate void UpdateOnCloseDel(Int32 Mode);

    public partial class ProductsMainForm : Form
    {
        ProductLine CurrProductLine;
        ProductMasterModel ObjProductMaster;

        public ProductsMainForm()
        {
            try
            {
                InitializeComponent();

                CurrProductLine = CommonFunctions.ListProductLines[CommonFunctions.SelectedProductLineIndex];
                ObjProductMaster = CurrProductLine.ObjProductMaster;

                dtGridViewProductCategory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dtGridViewProductCategory.MultiSelect = false;
                dtGridViewProductCategory.AllowUserToAddRows = false;
                dtGridViewProductCategory.AllowUserToDeleteRows = false;
                dtGridViewProductCategory.AllowUserToOrderColumns = false;
                dtGridViewProductCategory.AllowUserToResizeColumns = true;
                dtGridViewProductCategory.AllowUserToResizeRows = false;

                dtGridViewProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dtGridViewProducts.MultiSelect = false;
                dtGridViewProducts.AllowUserToAddRows = false;
                dtGridViewProducts.AllowUserToDeleteRows = false;
                dtGridViewProducts.AllowUserToOrderColumns = false;
                dtGridViewProducts.AllowUserToResizeColumns = true;
                dtGridViewProducts.AllowUserToResizeRows = false;

                LoadProductCategoryDataGridView(false);

                LoadProductsDataGridView(false);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductsMainForm.ctor()", ex);
            }
        }

        private void ProductsMainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                this.MaximizeBox = true;
                this.WindowState = FormWindowState.Maximized;
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductsMainForm.ProductsMainForm_Shown()", ex);
            }
        }

        void UpdateOnClose(Int32 Mode)
        {
            try
            {
                switch (Mode)
                {
                    case 1:     //Add Product
                        break;
                    case 2:     //Import Products from Excel
                        LoadProductsDataGridView(true);
                        break;
                    case 3:     //Reload Product Category from DB
                        LoadProductCategoryDataGridView(true);
                        break;
                    default:
                        break;
                }
                
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductsMainForm.UpdateOnClose()", ex);
            }
        }

        #region Import/Export from/to Excel methods
        private void btnImportFromExcel_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new ImportFromExcelForm(IMPORTDATATYPES.PRODUCTS, UpdateOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductsMainForm.btnImportFromExcel_Click()", ex);
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductsMainForm.btnExportToExcel_Click()", ex);
            }
        }
        #endregion

        #region Product Category related methods
        void LoadProductCategoryDataGridView(Boolean ReloadFromDB)
        {
            try
            {
                if (ReloadFromDB) CurrProductLine.LoadAllProductMasterTables();

                dtGridViewProductCategory.Rows.Clear();
                dtGridViewProductCategory.Columns.Clear();
                String[] ArrColumnNames = new String[] { "ID", "Name", "Desc", "Active" };
                String[] ArrColumnHeaders = new String[] { "Category ID", "Category Name", "Description", "Active" };
                for (int i = 0; i < ArrColumnNames.Length; i++)
                {
                    dtGridViewProductCategory.Columns.Add(ArrColumnNames[i], ArrColumnHeaders[i]);
                    DataGridViewColumn CurrentCol = dtGridViewProductCategory.Columns[dtGridViewProductCategory.Columns.Count - 1];
                    CurrentCol.ReadOnly = true;
                    if (i == 0) CurrentCol.Visible = false;
                    //if (i == 2) CurrentCol.CellTemplate = new DataGridViewCheckBoxCell();
                }

                foreach (Int32 CategoryID in ObjProductMaster.GetProductCategoryIDList())
                {
                    Object[] ArrRowItems = new Object[4];
                    ProductCategoryDetails tmpCategory = ObjProductMaster.GetCategoryDetails(CategoryID);
                    ArrRowItems[0] = tmpCategory.CategoryID;
                    ArrRowItems[1] = tmpCategory.CategoryName;
                    ArrRowItems[2] = tmpCategory.Description;
                    ArrRowItems[3] = tmpCategory.Active;

                    dtGridViewProductCategory.Rows.Add(ArrRowItems);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductsMainForm.LoadProductCategoryDataGridView()", ex);
                throw;
            }
        }

        private void btnAddProductCategory_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new AddProductCategoryForm(true, null, UpdateOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductsMainForm.btnAddProductCategory_Click()", ex);
            }
        }

        private void btnEditProductCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewProductCategory.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select a Category to edit", "Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                String Category = dtGridViewProductCategory.SelectedRows[0].Cells["Name"].Value.ToString();

                CommonFunctions.ShowDialog(new AddProductCategoryForm(false, Category, UpdateOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductsMainForm.btnEditProductCategory_Click()", ex);
            }
        }

        private void btnDelProductCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewProductCategory.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select a Category to delete", "Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                String CategoryID = dtGridViewProductCategory.SelectedRows[0].Cells["ID"].Value.ToString();
                String CategoryName = dtGridViewProductCategory.SelectedRows[0].Cells["Name"].Value.ToString();

                List<ProductDetails> ListProducts = ObjProductMaster.GetProductListForCategory(CategoryName);
                if (ListProducts.Count > 0)
                {
                    MessageBox.Show(this, "Cannot delete " + CategoryName + " as there are Products belong to it.", "Category", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                DialogResult result = MessageBox.Show(this, "Are you sure you want to delete " + CategoryName + "?", "Category", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No) return;

                ObjProductMaster.DeleteProductCategory(Int32.Parse(CategoryID));
                UpdateOnClose(3);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductsMainForm.btnDelProductCategory_Click()", ex);
            }
        }
        #endregion

        #region Product related methods
        void LoadProductsDataGridView(Boolean ReloadFromDB)
        {
            try
            {
                List<Int32> ListSelectedIDs = new List<Int32>();
                foreach (DataGridViewRow item in dtGridViewProducts.SelectedRows)
                {
                    ListSelectedIDs.Add(Int32.Parse(item.Cells["ID"].Value.ToString()));
                }
                if (ReloadFromDB) CurrProductLine.LoadAllProductMasterTables();

                dtGridViewProducts.Rows.Clear();
                dtGridViewProducts.Columns.Clear();
                String[] ArrColumnNames = new String[] { "ID", "SKU", "Name", "Desc", "Category", "PP", "SP", "Units", "UOM", "StockQty", "HSN", "Active" };
                String[] ArrColumnHeaders = new String[] { "ID", "SKU", "Name", "Description", "Category", "Purchase Price", "Selling Price", "Units", "Units of Measurement", "Current Stock", "HSN Code", "Active" };
                for (Int32 i = 0; i < ArrColumnNames.Length; i++)
                {
                    dtGridViewProducts.Columns.Add(ArrColumnNames[i], ArrColumnHeaders[i]);
                    DataGridViewColumn CurrentCol = dtGridViewProducts.Columns[dtGridViewProducts.Columns.Count - 1];
                    CurrentCol.ReadOnly = true;
                    if (ArrColumnNames[i].Equals("ID")) CurrentCol.Visible = false;
                    //if (ArrColumnNames[i].Equals("Active")) CurrentCol.CellTemplate = new DataGridViewCheckBoxCell();
                }

                List<ProductDetails> ListAllProducts = new List<ProductDetails>();
                foreach (DataGridViewRow row in dtGridViewProductCategory.Rows)
                {
                    Int32 CategoryID = Int32.Parse(row.Cells[0].Value.ToString());
                    String CategoryName = row.Cells[1].Value.ToString();
                    List<ProductDetails> ListProducts = ObjProductMaster.GetProductListForCategory(CategoryName);
                    ListAllProducts.AddRange(ListProducts);
                }
                ListAllProducts.OrderBy(e => e.SortName);

                for (Int32 i = 0; i < ListAllProducts.Count; i++)
                {
                    Object[] ArrRowItems = new Object[ArrColumnNames.Length];
                    ArrRowItems[0] = ListAllProducts[i].ProductID;
                    ArrRowItems[1] = ListAllProducts[i].ProductSKU;
                    ArrRowItems[2] = ListAllProducts[i].ItemName;
                    ArrRowItems[3] = ListAllProducts[i].ProductDesc;
                    ArrRowItems[4] = ListAllProducts[i].CategoryName;
                    ArrRowItems[5] = ListAllProducts[i].PurchasePrice;
                    ArrRowItems[6] = ListAllProducts[i].SellingPrice;
                    ArrRowItems[7] = ListAllProducts[i].Units;
                    ArrRowItems[8] = ListAllProducts[i].UnitsOfMeasurement;
                    ArrRowItems[9] = ListAllProducts[i].StockName;
                    ArrRowItems[10] = ListAllProducts[i].HSNCode;
                    ArrRowItems[11] = ListAllProducts[i].Active;

                    dtGridViewProducts.Rows.Add(ArrRowItems);

                    if (ListSelectedIDs.Contains(ListAllProducts[i].ProductID))
                    {
                        dtGridViewProducts.Rows[dtGridViewProducts.Rows.Count - 1].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductsMainForm.LoadProductsDataGridView()", ex);
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new AddProductForm(true, -1, UpdateOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductsMainForm.btnAddProduct_Click()", ex);
            }
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewProducts.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select a Product to edit", "Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Int32 ProductID = Int32.Parse(dtGridViewProducts.SelectedRows[0].Cells["ID"].Value.ToString());

                CommonFunctions.ShowDialog(new AddProductForm(false, ProductID, UpdateOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductsMainForm.btnEditProduct_Click()", ex);
            }
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            try
            {
                String SearchString = txtBoxProductSearchString.Text.Trim();
                if (String.IsNullOrEmpty(SearchString)) return;


            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductsMainForm.btnSearchProduct_Click()", ex);
            }
        }

        private void btnClearSearchProduct_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductsMainForm.btnClearSearchProduct_Click()", ex);
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductsMainForm.btnDeleteProduct_Click()", ex);
            }
        }

        private void btnReloadProducts_Click(object sender, EventArgs e)
        {
            try
            {
                LoadProductsDataGridView(true);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductsMainForm.btnReloadProducts_Click()", ex);
            }
        }
        #endregion
    }
}
