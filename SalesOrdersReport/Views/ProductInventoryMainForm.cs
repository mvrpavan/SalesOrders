using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Models;
using Excel = Microsoft.Office.Interop.Excel;

namespace SalesOrdersReport.Views
{
    public partial class ProductInventoryMainForm : Form
    {
        DataTable dtAllStocks;
        ProductMasterModel ObjProductMaster = null;
        public ProductInventoryMainForm()
        {
            try
            {
                InitializeComponent();

                CommonFunctions.SetDataGridViewProperties(dgvGridViewStocks);
                ObjProductMaster = CommonFunctions.ListProductLines[CommonFunctions.SelectedProductLineIndex].ObjProductMaster;
                cmbBoxCategoryFilterList.Items.Clear();
                cmbBoxCategoryFilterList.Items.Add("ALL");
                cmbBoxCategoryFilterList.Items.AddRange(ObjProductMaster.GetProductCategoryList().ToArray());
                if (cmbBoxCategoryFilterList.Items.Count > 0) cmbBoxCategoryFilterList.SelectedIndex = 0;
                dgvGridViewStocks.ReadOnly = false;
                LoadGridView(cmbBoxCategoryFilterList.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductInventoryMainForm.ctor()", ex);
            }
        }

        private void LoadGridView(string FilterString = "")
        {
            try
            {
                dtAllStocks = GetStockDataTable(FilterString);
                LoadStocksGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadGridView()", ex);
            }
        }

        private void ProductInventoryMainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                this.MaximizeBox = true;
                this.WindowState = FormWindowState.Maximized;
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ProductInventoryMainForm_Shown()", ex);
            }
        }

        DataTable GetStockDataTable(string Filterstr = "")
        {
            try
            {
                DataTable dtStocks = null;

                dtStocks = ObjProductMaster.LoadNGetProdInvDataTable(Filterstr);

                return dtStocks;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetStockDataTable()", ex);
                return null;
            }
        }
        Boolean ValueChanged = false;
        private void dgvGridViewStocks_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != 2 || e.RowIndex < 0) return;

                Double result;
                DataGridViewCell cell = dgvGridViewStocks.Rows[e.RowIndex].Cells[e.ColumnIndex];
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
                CommonFunctions.ShowErrorDialog($"{this}.dgvGridViewStocks_CellValueChanged()", ex);
            }
        }

        private void dgvGridViewStocks_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                if (e.Exception != null)
                {
                    MessageBox.Show(this, "Invalid value,Must Be number", "Stock update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dgvGridViewStocks_DataError()", ex);
            }
        }

        List<Int32> ListEditedProductIDs = new List<Int32>();
        private void dgvGridViewStocks_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != 2 || !ValueChanged) return;

                Int32 ProductID = Int32.Parse(dgvGridViewStocks["ProductInvID", e.RowIndex].Value.ToString());
                if (!ListEditedProductIDs.Contains(ProductID)) ListEditedProductIDs.Add(ProductID);

                ValueChanged = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dgvGridViewStocks_CellEndEdit()", ex);
            }
        }

        void LoadStocksGridView(string DataFilter = "")
        {
            try
            {

                if (ListEditedProductIDs.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show(this, "You have made some changes to Stocks.\nChanges made will be lost, if you continue.\nPlease click on \"Update Stocks\" to save your changes.\nDo you wish to continue?", "Stocks", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dialogResult == DialogResult.No) return;
                    ListEditedProductIDs.Clear();
                }

                List<Int32> ListSelectedIDs = new List<Int32>();
                foreach (DataGridViewRow item in dgvGridViewStocks.SelectedRows)
                {
                    ListSelectedIDs.Add(Int32.Parse(item.Cells["ProductInvID"].Value.ToString()));
                }

                if (DataFilter != null) dtAllStocks.DefaultView.RowFilter = DataFilter;

                dgvGridViewStocks.DataSource = dtAllStocks.DefaultView;

                for (int i = 0; i < dgvGridViewStocks.Columns.Count; i++)
                {
                    dgvGridViewStocks.Columns[i].ReadOnly = true;
                    if (dgvGridViewStocks.Columns[i].Name.Equals("ProductInvID"))
                        dgvGridViewStocks.Columns[i].Visible = false;

                    if (dgvGridViewStocks.Columns[i].Name.Equals("Inventory"))
                        dgvGridViewStocks.Columns[i].ReadOnly = false;
                }
                dgvGridViewStocks.ClearSelection();

                lblStocksCount.Text = $"[Displaying {dgvGridViewStocks.Rows.Count} of {dtAllStocks.Rows.Count} stocks]";
                foreach (DataGridViewRow item in dgvGridViewStocks.Rows)
                {
                    if (ListSelectedIDs.Contains(Int32.Parse(item.Cells["ProductInvID"].Value.ToString())))
                    {
                        item.Selected = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        void UpdateStocksOnClose(Int32 Mode)
        {
            try
            {
                switch (Mode)
                {
                    case 1:     //Add Order
                        LoadGridView(cmbBoxCategoryFilterList.SelectedItem.ToString());
                        break;
                    case 2:
                        break;
                    case 3:     //Reload Orders
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

        private void btnAddStock_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new CreateUpdateStockForm(UpdateStocksOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnAddStock_Click()", ex);
            }
        }

        private void btnReloadStocks_Click(object sender, EventArgs e)
        {
            try
            {
                txtBoxProductSearchString.Text = "";
                if (cmbBoxCategoryFilterList.SelectedIndex != 0) cmbBoxCategoryFilterList.SelectedIndex = 0;
                LoadGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnReloadStocks_Click()", ex);
            }
        }


        private void dgvGridViewStocks_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                if (e.Button != MouseButtons.Left) return;

                CommonFunctions.ShowDialog(new CreateUpdateStockForm(UpdateStocksOnClose, false, dgvGridViewStocks.Rows[e.RowIndex]), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewProducts_CellMouseDoubleClick()", ex);
            }
        }



        Int32 ExportOption = -1;
        String ExportFolderPath = "";
        Int32 BackgroundTask = 0;
        ReportProgressDel ReportProgress = null;

        private void ReportProgressFunc(Int32 ProgressState)
        {
            if (ReportProgress == null) return;
            ReportProgress(ProgressState);
        }

        private void backgroundWorkerStocks_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                switch (BackgroundTask)
                {
                    case 1:
                        LoadGridView(cmbBoxCategoryFilterList.SelectedItem.ToString());
                        break;
                    case 2:
                        ProductInventoryDetails ObjProductInventoryDetails = new ProductInventoryDetails();
                        for (int i = 0; i < ListEditedProductIDs.Count; i++)
                        {
                            ObjProductInventoryDetails.ProductInvID = ListEditedProductIDs[i];
                            for (int j = 0; j < dgvGridViewStocks.Rows.Count; j++)
                            {
                                if (dgvGridViewStocks["ProductInvID", j].Value.ToString().Equals(ListEditedProductIDs[i].ToString()))
                                {
                                    ObjProductInventoryDetails.StockName = dgvGridViewStocks["StockName", j].Value.ToString();
                                    ObjProductInventoryDetails.ReOrderStockLevel = Double.Parse(dgvGridViewStocks["ReOrderStockLevel", j].Value.ToString());
                                    ObjProductInventoryDetails.ReOrderStockQty = Double.Parse(dgvGridViewStocks["ReorderStockQty", j].Value.ToString());
                                    ObjProductInventoryDetails.Units = Double.Parse(dgvGridViewStocks["Units", j].Value.ToString());
                                    ObjProductInventoryDetails.UnitsOfMeasurement = dgvGridViewStocks["UnitsOfMeasurement", j].Value.ToString();
                                    ObjProductInventoryDetails.Inventory = Double.Parse(dgvGridViewStocks["Inventory", j].Value.ToString());
                                    ObjProductInventoryDetails.Active = bool.Parse(dgvGridViewStocks["Active", j].Value.ToString());
                                    ObjProductMaster.UpdateProductInventoryDatatoDB(ObjProductInventoryDetails);
                                    break;
                                }
                            }
                            ReportProgressFunc((Int32)(100 * (i + 1) * 1.0 / ListEditedProductIDs.Count));
                        }
                        ReportProgressFunc(100);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.backgroundWorkerStocks_DoWork()", ex);
            }
        }

        private void backgroundWorkerStocks_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                CommonFunctions.UpdateProgressBar(e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.backgroundWorkerStocks_ProgressChanged()", ex);
            }
        }

        private void backgroundWorkerStocks_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                switch (BackgroundTask)
                {
                    case 1:
                        break;
                    case 2:
                        MessageBox.Show(this, $"Update Inventory for {ListEditedProductIDs.Count} for Stocks  successfully", "Stock update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListEditedProductIDs.Clear();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.backgroundWorkerStocks_RunWorkerCompleted()", ex);
            }
        }

        private void cmbBoxCategoryFilterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxCategoryFilterList.SelectedIndex < 0) return;

                LoadGridView(cmbBoxCategoryFilterList.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxCategoryFilterList_SelectedIndexChanged()", ex);
            }
        }

        private void btnEditStockProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvGridViewStocks.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please Select a Stock Row To Edit!", "Error");
                    return;
                }
                CommonFunctions.ShowDialog(new CreateUpdateStockForm(UpdateStocksOnClose, false, dgvGridViewStocks.SelectedRows[0]), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnEditStockProduct_Click()", ex);
            }
        }

        private void btnImportFromExcel_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new ImportFromExcelForm(ImportDataTypes.Stocks, UpdateStocksOnClose, ImportStocksData), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnImportFromExcel_Click()", ex);
            }
        }
        private Int32 ImportStocksData(String ExcelFilePath, Object ObjDetails, ReportProgressDel ReportProgress)
        {
            try
            {

                Int32 Retval = ObjProductMaster.ProcessStocksDataFromExcelFile(ExcelFilePath, out string Errmsg, out String ProcessStatus, out Int32 ExistingProductsInventoryCount);
                if (Errmsg != "")
                {
                    MessageBox.Show(this, Errmsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                if (Retval == 0)
                {
                    DialogResult result = MessageBox.Show(this, $"Processed Stocks data from Excel File. Following data will be imported:\n{ProcessStatus}\n\nDo you want to continue to Import this data?",
                                    "Process Status", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.No) return 1;

                    if (ExistingProductsInventoryCount > 0)
                    {
                        result = MessageBox.Show(this, $"Do you want to update data for existing Products Inventory?",
                                        "Process Status", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.No) ExistingProductsInventoryCount = 0;
                    }
                }
                else
                {
                    MessageBox.Show(this, $"Following error occurred while processing Stocks data from Excel File.\n{ProcessStatus}\n\nPlease check.",
                                    "Process Status", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return -1;
                }
               

                Retval = ObjProductMaster.ImportStocksDataToDatabase(out String ImportStatus, ExistingProductsInventoryCount, ReportProgress);
                LoadGridView();
                if (Retval == 0)
                {
                    MessageBox.Show(this, $"Imported Stocks data from Excel File. Following is the import status:\n{ImportStatus}",
                                    "Import Status", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return 0;
                }
                else
                {
                    MessageBox.Show(this, $"Following error occurred while importing Stocks data from Excel File.\n{ImportStatus}\n\nPlease check.",
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

        private Int32 ImportStocksData1(String ExcelFilePath, Object ObjDetails, ReportProgressDel ReportProgress)
        {
            try
            {
                Excel.Application xlApp = new Excel.Application();
                DataTable dtStockSummary = CommonFunctions.ReturnDataTableFromExcelWorksheet("Stock Details", ExcelFilePath, "*");
                if (dtStockSummary == null)
                {
                    MessageBox.Show(this, "Provided Stock Summary file doesn't contain \"Stock Details\" Sheet.\nPlease provide correct file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return -1;
                }
                Int32 ExistingProductInventoryCount = 0;
                Int32 TotalRecordCount = 0;
                string ProcessStatus = "", ImportStatus = "";
                DataTable dtProductInventoryToUpdate = new DataTable();
                foreach (DataColumn item in dtStockSummary.Columns)
                {
                    dtProductInventoryToUpdate.Columns.Add(new DataColumn(item.ColumnName, item.DataType));
                }
                for (int i = dtStockSummary.Rows.Count - 1; i >= 0; i--)
                {
                    ProductInventoryDetails tmpProductInventoryDetails = ObjProductMaster.GetStockProductDetails(dtStockSummary.Rows[i]["StockName"].ToString());
                    if (tmpProductInventoryDetails != null)
                    {
                        ExistingProductInventoryCount++;
                        dtProductInventoryToUpdate.Rows.Add(dtStockSummary.Rows[i].ItemArray.ToArray());//up
                        dtStockSummary.Rows[i].Delete();//in
                    }
                }

                dtStockSummary.AcceptChanges();

                ProcessStatus += $"{(!String.IsNullOrEmpty(ProcessStatus) ? "\n" : "")}Inventory:: New:{dtStockSummary.Rows.Count} Existing:{ExistingProductInventoryCount}";
                TotalRecordCount += dtStockSummary.Rows.Count;

                if (ExistingProductInventoryCount > 0)
                {
                    DialogResult result = MessageBox.Show(this, $"Do you want to update data for existing Products Inventory?",
                                    "Process Status", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.No) ExistingProductInventoryCount = 0;
                }

                Int32 ErrorProductsInventoryUpdateCount = 0, ErrorInventoryCount = 0;
                if (ExistingProductInventoryCount == 0) dtStockSummary.AcceptChanges();
                else
                {
                    foreach (DataRow item in dtProductInventoryToUpdate.Rows)
                    {
                        ProductInventoryDetails tmpProductInventoryDetails = ObjProductMaster.GetStockProductDetails(item["StockName"].ToString());
                        if (tmpProductInventoryDetails == null)
                        {
                            ErrorProductsInventoryUpdateCount++;
                            continue;
                        }
                        tmpProductInventoryDetails = tmpProductInventoryDetails.Clone();
                        tmpProductInventoryDetails.Inventory = (item["Inventory"].ToString() == "") ? 0 : Double.Parse(item["Inventory"].ToString());
                        tmpProductInventoryDetails.Units = (item["Units"].ToString() == "") ? 0 : Double.Parse(item["Units"].ToString());
                        tmpProductInventoryDetails.UnitsOfMeasurement = item["UnitsOfMeasurement"].ToString();
                        tmpProductInventoryDetails.ReOrderStockLevel = (item["ReOrderStockLevel"].ToString() == "") ? 0 : Double.Parse(item["ReOrderStockLevel"].ToString());
                        tmpProductInventoryDetails.ReOrderStockQty = (item["ReOrderStockQty"].ToString() == "") ? 0 : Double.Parse(item["ReOrderStockQty"].ToString());
                        tmpProductInventoryDetails.Active = bool.Parse(item["Active"].ToString());
                        ObjProductMaster.UpdateProductInventoryDatatoDB(tmpProductInventoryDetails);
                    }
                }

                foreach (DataRow item in dtStockSummary.Rows)
                {
                    ProductInventoryDetails tmpProductInventoryDetails = new ProductInventoryDetails()
                    {
                        StockName = item["StockName"].ToString(),
                        Inventory = (item["Inventory"].ToString() == "") ? 0 : Double.Parse(item["Inventory"].ToString()),
                        Units = (item["Units"].ToString() == "") ? 0 : Double.Parse(item["Units"].ToString()),
                        UnitsOfMeasurement = item["UnitsOfMeasurement"].ToString(),
                        ReOrderStockLevel = (item["ReOrderStockLevel"].ToString() == "") ? 0 : Double.Parse(item["ReOrderStockLevel"].ToString()),
                        ReOrderStockQty = (item["ReOrderStockQty"].ToString() == "") ? 0 : Double.Parse(item["ReOrderStockQty"].ToString()),
                        LastPODate = DateTime.Now
                    };

                    if (ObjProductMaster.AddProductInvDetailstoDB(tmpProductInventoryDetails) == -1) ErrorInventoryCount++;
                }

                ImportStatus += $"{(!String.IsNullOrEmpty(ImportStatus) ? "\n" : "")}Inventory:: Imported:{dtStockSummary.Rows.Count - ErrorInventoryCount} Updated:{ExistingProductInventoryCount - ErrorProductsInventoryUpdateCount} Error:{ErrorInventoryCount + ErrorProductsInventoryUpdateCount}";
                if (ErrorInventoryCount > 0 && ErrorProductsInventoryUpdateCount > 0)
                {
                    MessageBox.Show(this, $"Imported Stocks data from Excel File. Following is the import status:\n{ImportStatus}",
                                    "Import Status", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    ReportProgress(100);
                    return 0;
                }
                else
                {
                    MessageBox.Show(this, $"Following error occurred while importing Stocks data from Excel File.\n{ImportStatus}\n\nPlease check.",
                                    "Import Status", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return -1;
                }

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ImportPaymentsData()", ex);
                return -1;
            }
        }


        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvGridViewStocks.Columns.Count == 1)
                {
                    MessageBox.Show("No Data in the grid! Pls Choose valid Date Filter and fill the grid", "ERROR NO DATA", MessageBoxButtons.OK);
                    return;
                }
                CommonFunctions.ShowDialog(new ExportToExcelForm(ExportDataTypes.Stocks, UpdateStocksOnClose, ExportStockData), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnExportToExcel_Click()", ex);
            }
        }
        private Int32 ExportStockData(String ExcelFilePath, Object ObjDetails, Boolean Append)
        {
            try
            {
                DialogResult result = MessageBox.Show(this, $"Export Stock data to Excel File. {dgvGridViewStocks.Rows.Count} rows of Stock Data will be Exported.\n\nDo you want to continue to Export this data?",
                                "Export Status", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.No) return 1;

                DataTable dtTempStock = new DataTable();
                List<string> ListOfColumnsToBeExcluded = new List<string>() { "ProductInvID" };
                List<int> ListOfColumnIndexesNotAdded = new List<int>();
                foreach (DataGridViewColumn col in dgvGridViewStocks.Columns)
                {
                    if (!ListOfColumnsToBeExcluded.Contains(col.Name))
                    {
                        dtTempStock.Columns.Add(col.Name);
                    }
                    else ListOfColumnIndexesNotAdded.Add(col.Index);
                }
                dtTempStock.Columns.Add("OrderQuantity", typeof(System.Int32));//new column

                foreach (DataGridViewRow row in dgvGridViewStocks.Rows)
                {
                    DataRow dRow = dtTempStock.NewRow();
                    int index = 0;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (!ListOfColumnIndexesNotAdded.Contains(cell.ColumnIndex))
                        {
                            dRow[index] = cell.Value;
                            index++;
                        }
                    }
                    double Inventory = row.Cells["Inventory"].Value.ToString() == string.Empty ? 0 : Double.Parse(row.Cells["Inventory"].Value.ToString());
                    double ReOrderStockLevel = row.Cells["ReOrderStockLevel"].Value.ToString() == string.Empty ? 0 : Double.Parse(row.Cells["ReOrderStockLevel"].Value.ToString());
                    double ReOrderStockQty = row.Cells["ReOrderStockQty"].Value.ToString() == string.Empty ? 0 : Double.Parse(row.Cells["ReOrderStockQty"].Value.ToString());
                    if (Inventory < ReOrderStockLevel) dRow[index] = ReOrderStockQty - Inventory;   //OrderQuantity column

                    dtTempStock.Rows.Add(dRow);
                }

                string ExportStatus = ""; dtTempStock.TableName = "Stock Details";
                Int32 RetVal = CommonFunctions.ExportDataTableToExcelFile(dtTempStock, ExcelFilePath, dtTempStock.TableName, Append);

                if (RetVal < 0) ExportStatus += $"{(!String.IsNullOrEmpty(ExportStatus) ? "\n" : "")}Stocks:: Failed export";
                else ExportStatus += $"{(!String.IsNullOrEmpty(ExportStatus) ? "\n" : "")}Stocks:: Exported:{dtTempStock.Rows.Count}";

                if (RetVal == 0)
                {
                    MessageBox.Show(this, $"Exported Stocks data to Excel File. Following is the Export status:\n{ExportStatus}",
                                    "Export Status", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return 0;
                }
                else
                {
                    MessageBox.Show(this, $"Following error occurred while exporting Stocks data to Excel File.\n{ExportStatus}\n\nPlease check.",
                                    "Export Status", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return -1;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ExportStockData()", ex);
                return -1;
            }
        }

        private void LoadGridViewBG()
        {
            try
            {
                BackgroundTask = 3;
#if DEBUG
                backgroundWorkerStocks_DoWork(null, null);
                backgroundWorkerStocks_RunWorkerCompleted(null, null);
#else
                ReportProgress = backgroundWorkerStocks.ReportProgress;
                backgroundWorkerStocks.RunWorkerAsync();
                backgroundWorkerStocks.WorkerReportsProgress = true;
#endif
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadGridViewBG()", ex);
            }
        }

        private void btnDeleteStock_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvGridViewStocks.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select an Stock to Delete", "Delete Stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                if (dgvGridViewStocks.SelectedRows[0].Cells["Active"].Value.ToString() == "false")
                {
                    MessageBox.Show(this, "Unable to delete as Selected Stock is already in Inactive state.", "Delete Stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                bool RetVal = ObjProductMaster.CanStockBeDeleted(int.Parse(dgvGridViewStocks.SelectedRows[0].Cells["ProductInvID"].Value.ToString()));
                if (RetVal)
                {
                    var Result = MessageBox.Show("Are sure to set the Stock to Inactive State? ", "InActive Vendor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //If the no button was pressed ...
                    if (Result == DialogResult.No) return;


                    string WhereCondition = "ProductInvID = '" + dgvGridViewStocks.SelectedRows[0].Cells["ProductInvID"].Value.ToString() + "'";
                    List<string> ListColumnNames = new List<string>() { "Active" };
                    List<string> ListColumnValues = new List<string>() { "0" };
                    int ResultVal=CommonFunctions.ObjUserMasterModel.UpdateAnyTableDetails("ProductInventory", ListColumnNames, ListColumnValues, WhereCondition);
                    if (ResultVal < 0) MessageBox.Show("Wasnt able to Delete the stock", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        MessageBox.Show(" Updated Stock :: " + dgvGridViewStocks.SelectedRows[0].Cells["StockName"].Value.ToString() + " To Inactive State successfully", "Update Stock");
                        UpdateStocksOnClose(Mode: 1);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Unable to delete as there exists Products under the  Selected Stock.", "Delete Stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnDeleteStock_Click()", ex);
            }
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            try
            {
                String SearchString = txtBoxProductSearchString.Text.Trim();
                if (String.IsNullOrEmpty(SearchString)) return;

                LoadGridView(FilterString: $"like '%{SearchString}%'");
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
                LoadGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnClearSearchProduct_Click()", ex);
            }
        }

        private void btnUploadToDb_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListEditedProductIDs.Count == 0)
                {
                    MessageBox.Show(this, $"No modified stocks to update ", "Stock update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                BackgroundTask = 2;
#if DEBUG
                backgroundWorkerStocks_DoWork(null, null);
                backgroundWorkerStocks_RunWorkerCompleted(null, null);
#else
                ReportProgress = backgroundWorkerStocks.ReportProgress;
                backgroundWorkerStocks.RunWorkerAsync();
                backgroundWorkerStocks.WorkerReportsProgress = true;
#endif
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnUploadToDb_Click()", ex);
            }
        }
    }
}
