using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Models;

namespace SalesOrdersReport.Views
{
    public partial class InvoicesMainForm : Form
    {
        ProductLine CurrProductLine;
        ProductMasterModel ObjProductMaster;

        public InvoicesMainForm()
        {
            try
            {
                InitializeComponent();

                CurrProductLine = CommonFunctions.ListProductLines[CommonFunctions.SelectedProductLineIndex];
                ObjProductMaster = CurrProductLine.ObjProductMaster;

                dtGridViewInvoiceItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dtGridViewInvoiceItems.MultiSelect = false;
                dtGridViewInvoiceItems.AllowUserToAddRows = false;
                dtGridViewInvoiceItems.AllowUserToDeleteRows = false;
                dtGridViewInvoiceItems.AllowUserToOrderColumns = false;
                dtGridViewInvoiceItems.AllowUserToResizeColumns = true;
                dtGridViewInvoiceItems.AllowUserToResizeRows = false;

                dtGridViewInvoices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dtGridViewInvoices.MultiSelect = false;
                dtGridViewInvoices.AllowUserToAddRows = false;
                dtGridViewInvoices.AllowUserToDeleteRows = false;
                dtGridViewInvoices.AllowUserToOrderColumns = false;
                dtGridViewInvoices.AllowUserToResizeColumns = true;
                dtGridViewInvoices.AllowUserToResizeRows = false;

                LoadProductCategoryDataGridView(false);

                LoadProductsDataGridView(false);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("InvoicesMainForm.ctor()", ex);
            }
        }

        private void OrdersMainForm_Shown(object sender, EventArgs e)
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
                CommonFunctions.ShowErrorDialog($"{this}.UpdateOnClose()", ex);
            }
        }

        #region Import/Export from/to Excel methods
        private void btnImportFromExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //CommonFunctions.ShowDialog(new ImportFromExcelForm(IMPORTDATATYPES.PRODUCTS, UpdateOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnImportFromExcel_Click()", ex);
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

                dtGridViewInvoiceItems.Rows.Clear();
                dtGridViewInvoiceItems.Columns.Clear();
                String[] ArrColumnNames = new String[] { "ID", "Name", "Desc", "Active" };
                String[] ArrColumnHeaders = new String[] { "Category ID", "Category Name", "Description", "Active" };
                for (int i = 0; i < ArrColumnNames.Length; i++)
                {
                    dtGridViewInvoiceItems.Columns.Add(ArrColumnNames[i], ArrColumnHeaders[i]);
                    DataGridViewColumn CurrentCol = dtGridViewInvoiceItems.Columns[dtGridViewInvoiceItems.Columns.Count - 1];
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

                    dtGridViewInvoiceItems.Rows.Add(ArrRowItems);
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
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductsMainForm.btnAddProductCategory_Click()", ex);
            }
        }
        #endregion

        #region Product related methods
        void LoadProductsDataGridView(Boolean ReloadFromDB)
        {
            try
            {
                List<Int32> ListSelectedIDs = new List<Int32>();
                foreach (DataGridViewRow item in dtGridViewInvoices.SelectedRows)
                {
                    ListSelectedIDs.Add(Int32.Parse(item.Cells["ID"].Value.ToString()));
                }
                if (ReloadFromDB) CurrProductLine.LoadAllProductMasterTables();

                dtGridViewInvoices.Rows.Clear();
                dtGridViewInvoices.Columns.Clear();
                String[] ArrColumnNames = new String[] { "ID", "SKU", "Name", "Desc", "Category", "PP", "SP", "Units", "UOM", "StockQty", "HSN", "Active" };
                String[] ArrColumnHeaders = new String[] { "ID", "SKU", "Name", "Description", "Category", "Purchase Price", "Selling Price", "Units", "Units of Measurement", "Current Stock", "HSN Code", "Active" };
                for (Int32 i = 0; i < ArrColumnNames.Length; i++)
                {
                    dtGridViewInvoices.Columns.Add(ArrColumnNames[i], ArrColumnHeaders[i]);
                    DataGridViewColumn CurrentCol = dtGridViewInvoices.Columns[dtGridViewInvoices.Columns.Count - 1];
                    CurrentCol.ReadOnly = true;
                    if (ArrColumnNames[i].Equals("ID")) CurrentCol.Visible = false;
                    //if (ArrColumnNames[i].Equals("Active")) CurrentCol.CellTemplate = new DataGridViewCheckBoxCell();
                }

                List<ProductDetails> ListAllProducts = new List<ProductDetails>();
                foreach (DataGridViewRow row in dtGridViewInvoiceItems.Rows)
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
                    ArrRowItems[6] = ListAllProducts[i].RetailPrice;
                    ArrRowItems[7] = ListAllProducts[i].Units;
                    ArrRowItems[8] = ListAllProducts[i].UnitsOfMeasurement;
                    ArrRowItems[9] = ListAllProducts[i].StockName;
                    ArrRowItems[10] = ListAllProducts[i].HSNCode;
                    ArrRowItems[11] = ListAllProducts[i].Active;

                    dtGridViewInvoices.Rows.Add(ArrRowItems);

                    if (ListSelectedIDs.Contains(ListAllProducts[i].ProductID))
                    {
                        dtGridViewInvoices.Rows[dtGridViewInvoices.Rows.Count - 1].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductsMainForm.LoadProductsDataGridView()", ex);
            }
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new CreateOrderInvoiceForm(1, true, false, null), this.Parent.FindForm());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnCreateOrder_Click()", ex);
            }
        }

        private void btnViewEditOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewInvoices.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select a Product to edit", "Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Int32 ProductID = Int32.Parse(dtGridViewInvoices.SelectedRows[0].Cells["ID"].Value.ToString());

                //CommonFunctions.ShowDialog(new AddProductForm(false, ProductID, UpdateOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnViewEditOrder_Click()", ex);
            }
        }

        private void btnConvertInvoice_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnConvertInvoice_Click()", ex);
            }
        }

        private void btnPrintOrder_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnPrintOrder_Click()", ex);
            }
        }

        private void btnSearchOrder_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnSearchOrder_Click()", ex);
            }
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnDeleteOrder_Click()", ex);
            }
        }

        private void btnReloadOrders_Click(object sender, EventArgs e)
        {
            try
            {
                LoadProductsDataGridView(true);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnReloadOrders_Click()", ex);
            }
        }
        #endregion

        private void checkBoxApplyFilter_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.checkBoxApplyFilter_CheckedChanged()", ex);
            }
        }
    }
}
