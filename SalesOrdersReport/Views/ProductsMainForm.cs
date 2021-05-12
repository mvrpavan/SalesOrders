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

                CommonFunctions.SetDataGridViewProperties(dtGridViewProducts);
                CommonFunctions.SetDataGridViewProperties(dtGridViewProductCategory);
                dtGridViewProducts.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                dtGridViewProducts.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dtGridViewProducts.ReadOnly = false;

                this.FormClosing += ProductsMainForm_FormClosing;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductsMainForm.ctor()", ex);
            }
        }

        private void ProductsMainForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDataGridViews();
//                BackgroundTask = 1;

//#if DEBUG
//                backgroundWorkerProducts_DoWork(null, null);
//                backgroundWorkerProducts_RunWorkerCompleted(null, null);
//#else
//                ReportProgress = backgroundWorkerProducts.ReportProgress;
//                backgroundWorkerProducts.RunWorkerAsync();
//                backgroundWorkerProducts.WorkerReportsProgress = true;
//#endif
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ProductsMainForm_Load()", ex);
            }
        }

        private void ProductsMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (ListEditedProductIDs.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show(this, "You have made some changes to Prices.\nChanges made will be lost, if you continue.\nPlease click on \"Update Prices\" to save your changes.\nDo you wish to continue?", "Products", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dialogResult == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ProductsMainForm_FormClosing()", ex);
            }
        }

        private void LoadDataGridViews(bool ReloadFromDB = false)
        {
            try
            {
                //LoadProductCategoryDataGridView(false);

                //LoadProductsDataGridView(false);
                LoadProductCategoryDataGridView(ReloadFromDB);

                LoadProductsDataGridView(ReloadFromDB);

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
                ProductDetails UpdatedProductDetails = ((ObjAddUpdated == null) ? null : (ProductDetails)ObjAddUpdated);
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

        private Int32 ImportProductsData(String ExcelFilePath, Object ObjDetails, ReportProgressDel ReportProgress)
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
                ReportProgress(10);

                Int32 Retval = ObjProductMaster.ProcessProductsDataFromExcelFile(out String ProcessStatus, out Int32 ExistingProductsCount, out Int32 ExistingProductsInventoryCount);

                if (Retval == 0)
                {
                    DialogResult result = MessageBox.Show(this, $"Processed Products data from Excel File. Following data will be imported:\n{ProcessStatus}\n\nDo you want to continue to Import this data?",
                                    "Process Status", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.No) return 1;
                    if (ExistingProductsCount > 0)
                    {
                        result = MessageBox.Show(this, $"Do you want to update data for existing Products?",
                                        "Process Status", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.No) ExistingProductsCount = 0;
                    }
                    if (ExistingProductsInventoryCount > 0)
                    {
                        result = MessageBox.Show(this, $"Do you want to update data for existing Products Inventory?",
                                        "Process Status", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.No) ExistingProductsInventoryCount = 0;
                    }
                }
                else if (Retval == 1)
                {
                    MessageBox.Show(this, $"Processed Products data from Excel File. Following data will be imported:\n{ProcessStatus}\n\nNo new data available to Import.",
                                    "Process Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                    return 1;
                }
                else
                {
                    MessageBox.Show(this, $"Following error occurred while processing Products data from Excel File.\n{ProcessStatus}\n\nPlease check.",
                                    "Process Status", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return -1;
                }
                ReportProgress(25);

                Retval = ObjProductMaster.ImportProductsDataToDatabase(out String ImportStatus, ExistingProductsCount, ExistingProductsInventoryCount, ReportProgress);

                if (Retval == 0)
                {
                    MessageBox.Show(this, $"Imported Products data from Excel File. Following is the import status:\n{ImportStatus}",
                                    "Import Status", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    ReportProgress(100);
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
                CommonFunctions.ShowErrorDialog($"{this}.ImportProductsData()", ex);
                return -1;
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new ExportToExcelForm(ExportDataTypes.Products, UpdateOnClose, ExportProductsData), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnExportToExcel_Click()", ex);
            }
        }

        private Int32 ExportProductsData(String ExcelFilePath, Object ObjDetails, Boolean Append)
        {
            try
            {
                DialogResult result = MessageBox.Show(this, $"Export Products data to Excel File. Data for {dtAllProducts.DefaultView.Count} Products will be Exported.\n\nDo you want to continue to Export this data?",
                                "Export Status", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.No) return 1;

                List<Int32> ListProductIDs = new List<Int32>();
                foreach (DataRow dtRow in dtAllProducts.DefaultView.ToTable().Rows)
                {
                    ListProductIDs.Add(Int32.Parse(dtRow["ID"].ToString()));
                }
                Int32 Retval = ObjProductMaster.ExportProductsDataToExcel(ListProductIDs, (Boolean[])ObjDetails, ExcelFilePath, Append, out String ExportStatus);

                if (Retval == 0)
                {
                    MessageBox.Show(this, $"Exported Products data to Excel File. Following is the Export status:\n{ExportStatus}",
                                    "Import Status", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return 0;
                }
                else
                {
                    MessageBox.Show(this, $"Following error occurred while exporting Products data to Excel File.\n{ExportStatus}\n\nPlease check.",
                                    "Import Status", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return -1;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ExportProductsData()", ex);
                return -1;
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
                CommonFunctions.ShowDialog(new CreateProductCategoryForm(true, null, UpdateProductOnClose), this);
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

                CommonFunctions.ShowDialog(new CreateProductCategoryForm(false, Category, UpdateProductOnClose), this);
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
                if (ListEditedProductIDs.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show(this, "You have made some changes to Prices.\nChanges made will be lost, if you continue.\nPlease click on \"Update Prices\" to save your changes.\nDo you wish to continue?", "Products", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dialogResult == DialogResult.No) return;
                    ListEditedProductIDs.Clear();
                }

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
                    if (item.Name.Equals("Purchase Price") || item.Name.Equals("Wholesale Price") || item.Name.Equals("Retail Price")
                        || item.Name.Equals("Max. Retail Price"))
                    {
                        item.ReadOnly = false;
                    }
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
                String[] ArrColumns = new String[] { "ID", "Product SKU", "Name", "SortName", "Description", "Category", "Purchase Price", "Wholesale Price",
                                                "Retail Price", "Max. Retail Price", "Units", "Current Stock", "ReOrder Stock Level",
                                                "ReOrder Stock Qty", "Vendor Name", "HSN Code", "Barcode", "Active" };
                Type[] ArrColumnsType = new Type[] { CommonFunctions.TypeInt32, CommonFunctions.TypeString, CommonFunctions.TypeString,
                                                    CommonFunctions.TypeString, CommonFunctions.TypeString, CommonFunctions.TypeString,
                                                    CommonFunctions.TypeDouble, CommonFunctions.TypeDouble, CommonFunctions.TypeDouble,
                                                    CommonFunctions.TypeDouble, CommonFunctions.TypeString, CommonFunctions.TypeString,
                                                    CommonFunctions.TypeString, CommonFunctions.TypeString, CommonFunctions.TypeString,
                                                    CommonFunctions.TypeString, CommonFunctions.TypeString, CommonFunctions.TypeString };
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
                    ArrRowItems[col++] = ListProducts[i].VendorName;
                    ArrRowItems[col++] = ListProducts[i].HSNCode;
                    ArrRowItems[col++] = (ListProducts[i].ArrBarcodes == null) ? "" : String.Join(",", ListProducts[i].ArrBarcodes);
                    ArrRowItems[col++] = ListProducts[i].Active.ToString();

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
                CommonFunctions.ShowDialog(new CreateProductForm(true, -1, UpdateProductOnClose), this);
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
                if (dtGridViewProducts.SelectedCells.Count == 0)
                {
                    MessageBox.Show(this, "Please select a Product to edit", "Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Int32 ProductID = Int32.Parse(dtGridViewProducts.Rows[dtGridViewProducts.SelectedCells[0].RowIndex].Cells["ID"].Value.ToString());

                CommonFunctions.ShowDialog(new CreateProductForm(false, ProductID, UpdateProductOnClose), this);
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
                if (dtGridViewProducts.SelectedCells.Count == 0)
                {
                    MessageBox.Show(this, "Please select a Product to delete", "Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DataGridViewRow SelectedRow = dtGridViewProducts.Rows[dtGridViewProducts.SelectedCells[0].RowIndex];
                String ProductID = SelectedRow.Cells["ID"].Value.ToString();
                String ProductName = SelectedRow.Cells["Name"].Value.ToString();

                DialogResult result = MessageBox.Show(this, $"Do you want to delete this Product:{ProductName}?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Cancel) return;

                ObjProductMaster.DeleteProduct(Int32.Parse(ProductID));

                dtAllProducts.Select($"ID = {ProductID}")[0]["Active"] = false;

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
                //LoadProductsDataGridView(true);

                BackgroundTask = 1;
#if DEBUG
                backgroundWorkerProducts_DoWork(null, null);
                backgroundWorkerProducts_RunWorkerCompleted(null, null);
#else
                ReportProgress = backgroundWorkerProducts.ReportProgress;
                backgroundWorkerProducts.RunWorkerAsync();
                backgroundWorkerProducts.WorkerReportsProgress = true;
#endif
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

                CommonFunctions.ShowDialog(new CreateProductForm(false, ProductID, UpdateProductOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewProducts_CellMouseDoubleClick()", ex);
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
                CommonFunctions.ShowDialog(new CreateTaxForm(true, -1, null), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnShowHSNList_Click()", ex);
            }
        }

        private void btnUploadToDb_Click(object sender, EventArgs e)
        {
            try
            {
                //Update Product Prices to Database
                if (ListEditedProductIDs.Count == 0)
                {
                    MessageBox.Show(this, $"No modified Products to update Prices", "Product update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                BackgroundTask = 2;
#if DEBUG
                backgroundWorkerProducts_DoWork(null, null);
                backgroundWorkerProducts_RunWorkerCompleted(null, null);
#else
                ReportProgress = backgroundWorkerProducts.ReportProgress;
                backgroundWorkerProducts.RunWorkerAsync();
                backgroundWorkerProducts.WorkerReportsProgress = true;
#endif
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnUploadToDb_Click()", ex);
            }
        }

        Boolean ValueChanged = false;
        private void dtGridViewProducts_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 6 || e.ColumnIndex > 9 || e.RowIndex < 0) return;

                Double result;
                DataGridViewCell cell = dtGridViewProducts.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ErrorText = null;
                if (!Double.TryParse(cell.Value.ToString(), out result))
                {
                    cell.ErrorText = "Must be a number";
                    return;
                }

                ValueChanged = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewProducts_CellValueChanged()", ex);
            }
        }

        private void dtGridViewProducts_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                if (e.Exception != null)
                {
                    MessageBox.Show(this, "Invalid value for Price", "Product update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewProducts_DataError()", ex);
            }
        }

        List<Int32> ListEditedProductIDs = new List<Int32>();
        private void dtGridViewProducts_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 6 || e.ColumnIndex > 9 || e.RowIndex < 0 || !ValueChanged) return;

                Int32 ProductID = Int32.Parse(dtGridViewProducts["ID", e.RowIndex].Value.ToString());
                if (!ListEditedProductIDs.Contains(ProductID)) ListEditedProductIDs.Add(ProductID);

                ValueChanged = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewProducts_CellEndEdit()", ex);
            }
        }

        ReportProgressDel ReportProgress = null;

        private void ReportProgressFunc(Int32 ProgressState)
        {
            if (ReportProgress == null) return;
            ReportProgress(ProgressState);
        }

        Int32 BackgroundTask = -1;
        private void backgroundWorkerProducts_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                CommonFunctions.ResetProgressBar();
                switch (BackgroundTask)
                {
                    case 1:     //Load Products DataGrid View
                        LoadDataGridViews(true);
                        break;
                    case 2:     //Update Products Prices
                        ProductDetails ObjProductDetails = new ProductDetails();
                        for (int i = 0; i < ListEditedProductIDs.Count; i++)
                        {
                            ObjProductDetails.ProductID = ListEditedProductIDs[i];
                            for (int j = 0; j < dtGridViewProducts.Rows.Count; j++)
                            {
                                if (dtGridViewProducts["ID", j].Value.ToString().Equals(ListEditedProductIDs[i].ToString()))
                                {
                                    ObjProductDetails.PurchasePrice = Double.Parse(dtGridViewProducts["Purchase Price", j].Value.ToString());
                                    ObjProductDetails.WholesalePrice = Double.Parse(dtGridViewProducts["Wholesale Price", j].Value.ToString());
                                    ObjProductDetails.RetailPrice = Double.Parse(dtGridViewProducts["Retail Price", j].Value.ToString());
                                    ObjProductDetails.MaxRetailPrice = Double.Parse(dtGridViewProducts["Max. Retail Price", j].Value.ToString());

                                    ObjProductMaster.UpdateProductPriceDetails(ObjProductDetails);
                                    break;
                                }
                            }
                            ReportProgressFunc((Int32)(100 * (i + 1) * 1.0 / ListEditedProductIDs.Count));
                        }
                        ReportProgressFunc(100);
                        break;
                    default: break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.backgroundWorkerProducts_DoWork()", ex);
            }
        }

        private void backgroundWorkerProducts_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                switch (BackgroundTask)
                {
                    case 1: break;
                    case 2:
                        MessageBox.Show(this, $"Update Prices for {ListEditedProductIDs.Count} Products successfully", "Product update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListEditedProductIDs.Clear();
                        break;
                    default:
                        break;
                }
                CommonFunctions.ResetProgressBar();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.backgroundWorkerProducts_RunWorkerCompleted()", ex);
            }
        }

        private void backgroundWorkerProducts_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CommonFunctions.UpdateProgressBar(e.ProgressPercentage);
        }
    }
}
