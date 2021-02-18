using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Models;

namespace SalesOrdersReport.Views
{
    public delegate void UpdateUsingObjectOnCloseDel(Int32 Mode, Object ObjAddUpdated = null);

    public partial class ProductsMainForm : Form
    {
        ProductLine CurrProductLine;
        ProductMasterModel ObjProductMaster;
        DataTable dtAllProducts;
        public const String AllKeyword = "<ALL>";

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

                LoadDataGridViews();

                btnExportToExcel.Enabled = false;
                btnExportToExcel.Visible = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductsMainForm.ctor()", ex);
            }
        }

        private void LoadDataGridViews()
        {
            try
            {
                LoadProductCategoryDataGridView(false);

                LoadProductsDataGridView(false);

                cmbBoxCategoryFilterList.Items.Clear();
                cmbBoxCategoryFilterList.Items.Add(AllKeyword);
                foreach (DataGridViewRow item in dtGridViewProductCategory.Rows)
                {
                    cmbBoxCategoryFilterList.Items.Add(item.Cells["Name"].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadDataGridViews()", ex);
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
                CommonFunctions.ShowErrorDialog($"{this}.ProductsMainForm_Shown()", ex);
            }
        }

        public void UpdateOnClose(Int32 Mode)
        {
            try
            {
                switch (Mode)
                {
                    case 1:     //Import from Excel file
                        LoadDataGridViews();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateOnClose()", ex);
            }
        }

        void UpdateProductOnClose(Int32 Mode, Object ObjAddUpdated = null)
        {
            try
            {
                ProductDetails UpdatedProductDetails = (ProductDetails)ObjAddUpdated;
                switch (Mode)
                {
                    case 1:     //Add/Update Product
                        DataTable dtProduct = GetProductsDataTable(new List<ProductDetails>() { UpdatedProductDetails });
                        Boolean ProductFound = false;
                        foreach (DataRow item in dtAllProducts.Rows)
                        {
                            if (Int32.Parse(item["ID"].ToString()) == UpdatedProductDetails.ProductID)
                            {
                                item.ItemArray = dtProduct.Rows[0].ItemArray;
                                ProductFound = true;
                                break;
                            }
                        }
                        if (!ProductFound) dtAllProducts.Rows.Add(dtProduct.Rows[0].ItemArray);
                        LoadProductsDataGridViewFromDataTable(null);
                        break;
                    case 2:
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
                CommonFunctions.ShowErrorDialog($"{this}.UpdateProductOnClose()", ex);
            }
        }

        #region Import/Export from/to Excel methods
        private void btnImportFromExcel_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new ImportFromExcelForm(ImportDataTypes.Products, UpdateOnClose, ImportProductsData), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnImportFromExcel_Click()", ex);
            }
        }

        private Int32 ImportProductsData(String ExcelFilePath, Object ObjDetails)
        {
            try
            {
                String ValidationStatus = ObjProductMaster.ValidateExcelFileToImport(ExcelFilePath, (Boolean[])ObjDetails);

                if (!String.IsNullOrEmpty(ValidationStatus))
                {
                    MessageBox.Show(this, $"Validation failed.\n{ValidationStatus}\n\nPlease check.",
                                    "Validation Status", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return -1;
                }

                Int32 Retval = ObjProductMaster.ProcessProductsDataFromExcelFile(out String ProcessStatus);

                if (Retval == 0)
                {
                    DialogResult result = MessageBox.Show(this, $"Processed Products data from Excel File. Following data will be imported:\n{ProcessStatus}\n\nDo you want to continue to Import this data?",
                                    "Process Status", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.No) return 1;
                }
                else if (Retval == 1)
                {
                    MessageBox.Show(this, $"Processed Products data from Excel File. Following data will be imported:\n{ProcessStatus}\n\nNo new data available to Import?",
                                    "Process Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                    return 1;
                }
                else
                {
                    MessageBox.Show(this, $"Following error occurred while processing Products data from Excel File.\n{ProcessStatus}\n\nPlease check.",
                                    "Process Status", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return -1;
                }

                Retval = ObjProductMaster.ImportProductsDataToDatabase(out String ImportStatus);

                if (Retval == 0)
                {
                    MessageBox.Show(this, $"Imported Products data from Excel File. Following is the import status:\n{ImportStatus}",
                                    "Import Status", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return 0;
                }
                else
                {
                    MessageBox.Show(this, $"Following error occurred while importing Products data from Excel File.\n{ImportStatus}\n\nPlease check.",
                                    "Import Status", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return -1;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnExportToExcel_Click()", ex);
                return -1;
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnExportToExcel_Click()", ex);
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
                CommonFunctions.ShowErrorDialog($"{this}.LoadProductCategoryDataGridView()", ex);
                throw;
            }
        }

        private void btnAddProductCategory_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new AddProductCategoryForm(true, null, UpdateProductOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnAddProductCategory_Click()", ex);
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

                CommonFunctions.ShowDialog(new AddProductCategoryForm(false, Category, UpdateProductOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnEditProductCategory_Click()", ex);
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
                    MessageBox.Show(this, "Cannot delete " + CategoryName + " as there are Products belonging to it.", "Category", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                DialogResult result = MessageBox.Show(this, "Are you sure you want to delete " + CategoryName + "?", "Category", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No) return;

                ObjProductMaster.DeleteProductCategory(Int32.Parse(CategoryID));
                UpdateProductOnClose(3, null);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnDelProductCategory_Click()", ex);
            }
        }
        #endregion

        #region Product related methods
        void LoadProductsDataGridView(Boolean ReloadFromDB)
        {
            try
            {
                if (ReloadFromDB) CurrProductLine.LoadAllProductMasterTables();

                List<ProductDetails> ListAllProducts = ObjProductMaster.GetProductListForCategory(AllKeyword);
                dtAllProducts = GetProductsDataTable(ListAllProducts);
                LoadProductsDataGridViewFromDataTable(null);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadProductsDataGridView()", ex);
            }
        }

        void LoadProductsDataGridViewFromDataTable(String DataFilter = "", String Sort = "")
        {
            try
            {
                List<Int32> ListSelectedIDs = new List<Int32>();
                foreach (DataGridViewRow item in dtGridViewProducts.SelectedRows)
                {
                    ListSelectedIDs.Add(Int32.Parse(item.Cells["ID"].Value.ToString()));
                }

                if (DataFilter != null) dtAllProducts.DefaultView.RowFilter = DataFilter;
                if (Sort != null) dtAllProducts.DefaultView.Sort = Sort;
                dtGridViewProducts.DataSource = dtAllProducts.DefaultView;
                lblProductCount.Text = $"[Displaying {dtAllProducts.DefaultView.Count} of {dtAllProducts.Rows.Count} Products]";

                foreach (DataGridViewColumn item in dtGridViewProducts.Columns)
                {
                    item.ReadOnly = true;
                    if (item.HeaderText.Equals("ID") || item.HeaderText.Equals("SortName")) item.Visible = false;
                }

                foreach (DataGridViewRow item in dtGridViewProducts.Rows)
                {
                    if (ListSelectedIDs.Contains(Int32.Parse(item.Cells["ID"].Value.ToString())))
                    {
                        item.Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadProductsDataGridViewFromDataTable()", ex);
            }
        }

        DataTable GetProductsDataTable(List<ProductDetails> ListProducts)
        {
            try
            {
                DataTable dtProducts = new DataTable();
                String[] ArrColumns = new String[] { "ID", "SKU", "Name", "SortName", "Description", "Category", "Purchase Price", "Wholesale Price",
                                                "Retail Price", "Max. Retail Price", "Units", "Current Stock", "ReOrder Stock Level",
                                                "ReOrder Stock Qty", "HSN Code", "Active" };
                Type[] ArrColumnsType = new Type[] { Type.GetType("System.Int32"), Type.GetType("System.String"), Type.GetType("System.String"),
                                                    Type.GetType("System.String"), Type.GetType("System.String"), Type.GetType("System.String"),
                                                    Type.GetType("System.Double"), Type.GetType("System.Double"), Type.GetType("System.Double"),
                                                    Type.GetType("System.Double"), Type.GetType("System.String"), Type.GetType("System.String"),
                                                    Type.GetType("System.String"), Type.GetType("System.String"), Type.GetType("System.String"), Type.GetType("System.Boolean") };
                for (int i = 0; i < ArrColumns.Length; i++)
                {
                    dtProducts.Columns.Add(new DataColumn(ArrColumns[i], ArrColumnsType[i]));
                }

                for (Int32 i = 0; i < ListProducts.Count; i++)
                {
                    Int32 col = 0;
                    Object[] ArrRowItems = new Object[ArrColumns.Length];
                    ArrRowItems[col++] = ListProducts[i].ProductID;
                    ArrRowItems[col++] = ListProducts[i].ProductSKU;
                    ArrRowItems[col++] = ListProducts[i].ItemName;
                    ArrRowItems[col++] = ListProducts[i].SortName;
                    ArrRowItems[col++] = ListProducts[i].ProductDesc;
                    ArrRowItems[col++] = ListProducts[i].CategoryName;
                    ArrRowItems[col++] = ListProducts[i].PurchasePrice;
                    ArrRowItems[col++] = ListProducts[i].WholesalePrice;
                    ArrRowItems[col++] = ListProducts[i].RetailPrice;
                    ArrRowItems[col++] = ListProducts[i].MaxRetailPrice;
                    ArrRowItems[col++] = ListProducts[i].Units + " " + ListProducts[i].UnitsOfMeasurement;
                    ProductInventoryDetails productInventoryDetails = ObjProductMaster.GetProductInventoryDetails(ListProducts[i].ProductInvID);
                    ArrRowItems[col++] = (productInventoryDetails.Inventory * productInventoryDetails.Units) + " " + productInventoryDetails.UnitsOfMeasurement;
                    ArrRowItems[col++] = (productInventoryDetails.ReOrderStockLevel * productInventoryDetails.Units) + " " + productInventoryDetails.UnitsOfMeasurement;
                    ArrRowItems[col++] = (productInventoryDetails.ReOrderStockQty * productInventoryDetails.Units) + " " + productInventoryDetails.UnitsOfMeasurement; ;
                    ArrRowItems[col++] = ListProducts[i].HSNCode;
                    ArrRowItems[col++] = ListProducts[i].Active;

                    dtProducts.Rows.Add(ArrRowItems);
                }

                dtProducts.DefaultView.Sort = "SortName";

                return dtProducts;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetProductsDataTable()", ex);
                return null;
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new AddProductForm(true, -1, UpdateProductOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnAddProduct_Click()", ex);
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

                CommonFunctions.ShowDialog(new AddProductForm(false, ProductID, UpdateProductOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnEditProduct_Click()", ex);
            }
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            try
            {
                String SearchString = txtBoxProductSearchString.Text.Trim();
                if (String.IsNullOrEmpty(SearchString)) return;

                LoadProductsDataGridViewFromDataTable(DataFilter: $"Name like '%{SearchString}%'");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnSearchProduct_Click()", ex);
            }
        }

        private void btnClearSearchProduct_Click(object sender, EventArgs e)
        {
            try
            {
                txtBoxProductSearchString.Text = "";
                if (cmbBoxCategoryFilterList.SelectedIndex != 0) cmbBoxCategoryFilterList.SelectedIndex = 0;
                LoadProductsDataGridViewFromDataTable();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnClearSearchProduct_Click()", ex);
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewProducts.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select a Product to delete", "Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                String ProductID = dtGridViewProducts.SelectedRows[0].Cells["ID"].Value.ToString();
                String ProductName = dtGridViewProducts.SelectedRows[0].Cells["Name"].Value.ToString();

                DialogResult result = MessageBox.Show(this, $"Do you want to delete this Product:{ProductName}?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Cancel) return;

                ObjProductMaster.DeleteProduct(Int32.Parse(ProductID));

                dtAllProducts.Select($"ID = {ProductID}")[0]["Active"] = false;
                //dtAllProducts.Select($"ID = {ProductID}")[0].Delete();
                //dtAllProducts.AcceptChanges();

                LoadProductsDataGridViewFromDataTable(null);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnDeleteProduct_Click()", ex);
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
                CommonFunctions.ShowErrorDialog($"{this}.btnReloadProducts_Click()", ex);
            }
        }
        #endregion

        private void dtGridViewProducts_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                if (e.Button != MouseButtons.Left) return;

                Int32 ProductID = Int32.Parse(dtGridViewProducts.Rows[e.RowIndex].Cells["ID"].Value.ToString());

                CommonFunctions.ShowDialog(new AddProductForm(false, ProductID, UpdateProductOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewProducts_CellMouseDoubleClick()", ex);
            }
        }

        private void editProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Point point = cntxtMenuProducts.Bounds.Location;
                MessageBox.Show("Edit Button " + dtGridViewProducts.HitTest(point.X, point.Y).RowIndex + $" X:{point.X} Y:{point.Y}");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.editProductToolStripMenuItem_Click()", ex);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Point point = cntxtMenuProducts.Bounds.Location;
                MessageBox.Show("Delete Button " + dtGridViewProducts.HitTest(point.X, point.Y).RowIndex + $" X:{point.X} Y:{point.Y}");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.deleteToolStripMenuItem_Click()", ex);
            }
        }

        private void cmbBoxCategoryFilterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxCategoryFilterList.SelectedIndex < 0) return;

                if (cmbBoxCategoryFilterList.SelectedIndex == 0)
                    LoadProductsDataGridViewFromDataTable("");
                else
                    LoadProductsDataGridViewFromDataTable($"Category = '{cmbBoxCategoryFilterList.SelectedItem.ToString()}'");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxCategoryFilterList_SelectedIndexChanged()", ex);
            }
        }

        private void btnShowHSNList_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new AddTaxForm(true, -1, null), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnShowHSNList_Click()", ex);
            }
        }
    }
}
