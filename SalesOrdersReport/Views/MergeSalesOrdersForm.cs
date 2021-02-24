using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using SalesOrdersReport.CommonModules;

namespace SalesOrdersReport.Views
{
    public partial class MergeSalesOrdersForm : Form
    {
        List<String> ListFiles = new List<String>();
        List<ProductDetails> ListAllProducts;
        List<String> ListSellerNames;

        public MergeSalesOrdersForm()
        {
            try
            {
                InitializeComponent();
                dateTimeSalesOrder.Value = DateTime.Now;
                ObjFolderBrowserDialog.SelectedPath = Path.GetDirectoryName(CommonFunctions.MasterFilePath);
                txtBoxOutputFolder.Text = ObjFolderBrowserDialog.SelectedPath + @"\SalesOrder_" + dateTimeSalesOrder.Value.ToString("dd-MM-yyyy") + "_Merged.xlsx";
                CommonFunctions.ResetProgressBar();
                btnMerge.Enabled = false;
                panel1.Enabled = false;

                //Populate Seller Names
                ListSellerNames = CommonFunctions.ObjCustomerMasterModel.GetCustomerList();

                //Populate Item Names
                ListAllProducts = CommonFunctions.ObjProductMaster.GetProductListForCategory("<ALL>");
                ListAllProducts = ListAllProducts.OrderBy(p => p.ProductID).ToList();

                ObjOpenFileDialog.Filter = "Excel File|*.xls;*.xlsx";
                ObjOpenFileDialog.CheckFileExists = true;
                ObjOpenFileDialog.CheckPathExists = true;

                SetStatus("Please choose Sales Order Date to Merge");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MergeSalesOrdersForm.ctor()", ex);
                throw;
            }
        }

        ReportProgressDel ReportProgress = null;

        private void ReportProgressFunc(Int32 ProgressState)
        {
            if (ReportProgress == null) return;
            ReportProgress(ProgressState);
        }

        private void btnOutputFolderBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = ObjFolderBrowserDialog.ShowDialog(this);
                if (dialogResult == DialogResult.OK || dialogResult == DialogResult.Yes)
                {
                    txtBoxOutputFolder.Text = ObjFolderBrowserDialog.SelectedPath + @"\SalesOrder_" + dateTimeSalesOrder.Value.ToString("dd-MM-yyyy") + "_Merged.xlsx";
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MergeSalesOrdersForm.btnOutputFolderBrowse_Click()", ex);
                throw;
            }
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            try
            {
                ObjOpenFileDialog.Title = "Select Sales Order File(s) to Merge";
                DialogResult dialogResult = ObjOpenFileDialog.ShowDialog(this);
                if (dialogResult == DialogResult.OK || dialogResult == DialogResult.Yes)
                {
                    foreach (var file in ObjOpenFileDialog.FileNames)
                    {
                        if (!ListFiles.Contains(file))
                        {
                            dtGridViewSelectedFiles.Rows.Add(new String[] { file });
                            ListFiles.Add(file);
                        }
                    }
                }
                btnMerge.Enabled = (dtGridViewSelectedFiles.Rows.Count > 1);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MergeSalesOrdersForm.btnAddFile_Click()", ex);
                throw;
            }
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = ObjFolderBrowserDialog.ShowDialog(this);
                if (dialogResult == DialogResult.OK || dialogResult == DialogResult.Yes)
                {
                    String[] FileNames = Directory.GetFiles(ObjFolderBrowserDialog.SelectedPath);
                    foreach (var file in FileNames)
                    {
                        if (!ListFiles.Contains(file))
                        {
                            dtGridViewSelectedFiles.Rows.Add(new String[] { file });
                            ListFiles.Add(file);
                        }
                    }
                }
                btnMerge.Enabled = (dtGridViewSelectedFiles.Rows.Count > 1);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MergeSalesOrdersForm.btnSelectFolder_Click()", ex);
                throw;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<DataGridViewRow> ListRows = new List<DataGridViewRow>();
                for (int i = 0; i < dtGridViewSelectedFiles.SelectedCells.Count; i++)
                {
                    if (!ListRows.Contains(dtGridViewSelectedFiles.SelectedCells[i].OwningRow))
                    {
                        ListRows.Add(dtGridViewSelectedFiles.SelectedCells[i].OwningRow);
                        ListFiles.Remove(dtGridViewSelectedFiles.SelectedCells[i].Value.ToString());
                        dtGridViewSelectedFiles.Rows.Remove(ListRows[ListRows.Count - 1]);
                    }
                }

                btnMerge.Enabled = (dtGridViewSelectedFiles.Rows.Count > 1);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MergeSalesOrdersForm.btnDelete_Click()", ex);
                throw;
            }
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            try
            {
                panel1.Enabled = false;
                btnMerge.Enabled = false;
                btnCancel.Enabled = false;

#if DEBUG
                backgroundWorker1_DoWork(null, null);
                backgroundWorker1_RunWorkerCompleted(null, null);
#else
                ReportProgress = backgroundWorker1.ReportProgress;
                backgroundWorker1.RunWorkerAsync();
                backgroundWorker1.WorkerReportsProgress = true;
#endif
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MergeSalesOrdersForm.btnMerge_Click()", ex);
                throw;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimeSalesOrder_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtBoxOutputFolder.Text = ObjFolderBrowserDialog.SelectedPath + @"\SalesOrder_" + dateTimeSalesOrder.Value.ToString("dd-MM-yyyy") + "_Merged.xlsx";
                panel1.Enabled = true;
                SetStatus("Select multiple Sales Order Files to Merge");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MergeSalesOrdersForm.dateTimeSalesOrder_ValueChanged()", ex);
                throw;
            }
        }

        List<String> ListProductNames = new List<String>();

        private void LoadSalesOrderSheet(Excel.Application xlApp, String SalesOrderWorkbookPath, 
            out Dictionary<String, Int32> DictItemToColIndexes, out Dictionary<String, Int32> DictSellerToRowIndexes, out List<CustomerOrderInvoiceDetails> ListSellerOrderDetails,
            out List<String> ListProductNames)
        {
            try
            {
                SetStatus("Loading Sales Order File:" + Path.GetFileName(SalesOrderWorkbookPath));

                Excel.Workbook xlSalesOrderWorkbook = xlApp.Workbooks.Open(SalesOrderWorkbookPath);
                Excel.Worksheet xlSalesOrderWorksheet = CommonFunctions.GetWorksheet(xlSalesOrderWorkbook, dateTimeSalesOrder.Value.ToString("dd-MM-yyyy"));
                Int32 StartRow = 5, StartColumn = 1, DetailsCount = 5;

                #region Identify Items in SalesOrderSheet
                DictItemToColIndexes = new Dictionary<String, Int32>();
                Int32 ColumnCount = xlSalesOrderWorksheet.UsedRange.Columns.Count;
                Int32 RowCount = xlSalesOrderWorksheet.UsedRange.Rows.Count + 1;
                Int32 ProgressBarCount = ((ColumnCount - (StartColumn + DetailsCount)) + (RowCount - (StartRow + 1)));
                ListProductNames = new List<String>();
                for (int i = StartColumn + DetailsCount; i <= ColumnCount; i++)
                {
                    String ItemName = xlSalesOrderWorksheet.Cells[StartRow, i].Value;
                    ItemName = ItemName.Trim();
                    Int32 ItemIndex = ListAllProducts.FindIndex(e => e.ItemName.Equals(ItemName, StringComparison.InvariantCultureIgnoreCase));
                    if (ItemIndex < 0)
                    {
                        DictItemToColIndexes.Add(ItemName.ToUpper(), -1);
                        MessageBox.Show(this, "Item:" + ItemName + " not found in ItemMaster, Skipping this Item", "Item error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else DictItemToColIndexes.Add(ListAllProducts[ItemIndex].ItemName.ToUpper(), i - (StartColumn + DetailsCount));
                    //ReportProgressFunc((i - (StartColumn + DetailsCount)) * 100 / ProgressBarCount);
                    ListProductNames.Add(ItemName.Trim().ToUpper());
                }
                #endregion

                #region Identify Sellers in SalesOrderSheet
                ListSellerOrderDetails = new List<CustomerOrderInvoiceDetails>();
                DictSellerToRowIndexes = new Dictionary<String, Int32>();
                for (int i = StartRow + 1; i <= RowCount; i++)
                {
                    if (xlSalesOrderWorksheet.Cells[i, StartColumn + 1].Value == null) continue;
                    if (xlSalesOrderWorksheet.Cells[i, StartColumn + 2].Value == null) continue;
                    String SellerName = xlSalesOrderWorksheet.Cells[i, StartColumn + 2].Value;
                    SellerName = SellerName.Trim();
                    Int32 SellerIndex = ListSellerNames.FindIndex(e => e.Equals(SellerName, StringComparison.InvariantCultureIgnoreCase));
                    //ReportProgressFunc((i - (StartColumn + 1) + (ColumnCount - (StartColumn + DetailsCount))) * 100 / ProgressBarCount);
                    if (SellerIndex < 0)
                    {
                        MessageBox.Show(this, "Seller:" + SellerName + " not found in SellerMaster, Skipping this seller", "Seller error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    DictSellerToRowIndexes.Add(ListSellerNames[SellerIndex].ToUpper(), i);
                    CustomerOrderInvoiceDetails tmpSellerOrderDetails = new CustomerOrderInvoiceDetails();
                    tmpSellerOrderDetails.CustomerName = ListSellerNames[SellerIndex];
                    tmpSellerOrderDetails.SellerRowIndex = i;
                    if (xlSalesOrderWorksheet.Cells[i, StartColumn + 1].Value != null)
                        tmpSellerOrderDetails.OrderItemCount = Int32.Parse(xlSalesOrderWorksheet.Cells[i, StartColumn + 1].Value.ToString());
                    else tmpSellerOrderDetails.OrderItemCount = 0;

                    tmpSellerOrderDetails.ListItemQuantity = new List<Double>();
                    if (tmpSellerOrderDetails.OrderItemCount == 0)
                    {
                        for (int j = StartColumn + DetailsCount; j <= ColumnCount; j++)
                            tmpSellerOrderDetails.ListItemQuantity.Add(0);
                    }
                    else
                    {
                        for (int j = StartColumn + DetailsCount; j <= ColumnCount; j++)
                        {
                            String Value = null;
                            if (xlSalesOrderWorksheet.Cells[i, j].Value != null)
                                Value = xlSalesOrderWorksheet.Cells[i, j].Value.ToString();

                            if (String.IsNullOrEmpty(Value))
                            {
                                tmpSellerOrderDetails.ListItemQuantity.Add(0);
                            }
                            else
                            {
                                Double result;
                                if (Double.TryParse(Value, out result))
                                    tmpSellerOrderDetails.ListItemQuantity.Add(result);
                                else
                                {
                                    KeyValuePair<String, Int32> item = DictItemToColIndexes.ElementAt(j - (StartColumn + DetailsCount));
                                    MessageBox.Show(this, "Invalid Quantity in Sales Order sheet\nSeller:" + tmpSellerOrderDetails.CustomerName + ", Item:" + item.Key + ", Quantity:" + Value + "\nIgnoring quantity for this item",
                                                    "Quantity Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    tmpSellerOrderDetails.ListItemQuantity.Add(0);
                                }
                            }
                        }
                    }

                    ListSellerOrderDetails.Add(tmpSellerOrderDetails);
                }
                #endregion

                xlSalesOrderWorkbook.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MergeSalesOrdersForm.LoadSalesOrderSheet()", ex);
                throw;
            }
        }

        void SetStatus(String Status)
        {
            try
            {
                lblStatus.Text = Status;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MergeSalesOrdersForm.SetStatus()", ex);
                throw;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Excel.Application xlApp = new Excel.Application();
            try
            {
                ReportProgressFunc(0);

                File.Copy(ListFiles[0], txtBoxOutputFolder.Text, true);
                Dictionary<String, Int32> DictItemToColIndexesToUpdate, DictSellerToRowIndexesToUpdate;
                List<CustomerOrderInvoiceDetails> ListSellerOrderDetailsToUpdate;
                List<String> ListProductNamesToUpdate;
                LoadSalesOrderSheet(xlApp, ListFiles[0], out DictItemToColIndexesToUpdate, out DictSellerToRowIndexesToUpdate, out ListSellerOrderDetailsToUpdate, out ListProductNamesToUpdate);
                Int32 ProgressBarCount = (ListFiles.Count + 1);

                ReportProgressFunc(1 * 100 / ProgressBarCount);
                for (int i = 1; i < ListFiles.Count; i++)
                {
                    Dictionary<String, Int32> DictItemToColIndexes, DictSellerToRowIndexes;
                    List<CustomerOrderInvoiceDetails> ListSellerOrderDetails;
                    List<String> ListProductNames;
                    LoadSalesOrderSheet(xlApp, ListFiles[i], out DictItemToColIndexes, out DictSellerToRowIndexes, out ListSellerOrderDetails, out ListProductNames);

                    for (int j = 0; j < ListSellerOrderDetailsToUpdate.Count; j++)
                    {
                        CustomerOrderInvoiceDetails CurrSellerOrder = ListSellerOrderDetailsToUpdate[j];

                        Int32 SellerIndex = -1;
                        if (j < ListSellerOrderDetails.Count
                            && ListSellerOrderDetails[j].CustomerName.Equals(CurrSellerOrder.CustomerName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            SellerIndex = j;
                        }
                        else
                        {
                            SellerIndex = ListSellerOrderDetails.FindIndex(s => s.CustomerName.Equals(CurrSellerOrder.CustomerName, StringComparison.InvariantCultureIgnoreCase));
                        }

                        if (SellerIndex < 0) continue;
                        if (ListSellerOrderDetails[SellerIndex].OrderItemCount == 0) continue;

                        Int32 OrderItemCount = 0;
                        for (int k = 0; k < ListProductNamesToUpdate.Count; k++)
                        {
                            if (DictItemToColIndexes.ContainsKey(ListProductNamesToUpdate[k]))
                                CurrSellerOrder.ListItemQuantity[k] += ListSellerOrderDetails[SellerIndex].ListItemQuantity[DictItemToColIndexes[ListProductNamesToUpdate[k]]];
                            if (CurrSellerOrder.ListItemQuantity[k] > 0) OrderItemCount++;
                        }
                        CurrSellerOrder.OrderItemCount = OrderItemCount;
                    }
                    ReportProgressFunc((i + 1) * 100 / ProgressBarCount);
                }

                #region Print to merged SalesOrder file
                SetStatus("Creating Merged Sales Order File:" + Path.GetFileName(txtBoxOutputFolder.Text));
                Excel.Workbook xlSalesOrderWorkbook = xlApp.Workbooks.Open(txtBoxOutputFolder.Text);
                Excel.Worksheet xlSalesOrderWorksheet = CommonFunctions.GetWorksheet(xlSalesOrderWorkbook, dateTimeSalesOrder.Value.ToString("dd-MM-yyyy"));
                Int32 StartRow = 5, StartColumn = 1, DetailsCount = 5, ColumnCount = xlSalesOrderWorksheet.UsedRange.Columns.Count;

                for (int i = 0, row = StartRow + 1; i < ListSellerOrderDetailsToUpdate.Count; i++, row++)
                {
                    CustomerOrderInvoiceDetails CurrSellerOrder = ListSellerOrderDetailsToUpdate[i];
                    if (CurrSellerOrder.OrderItemCount == 0) continue;
                    //xlSalesOrderWorksheet.Cells[i + StartRow + 1, StartColumn + 1].Value = CurrSellerOrder.OrderItemCount;
                    for (int j = 0, col = StartColumn + DetailsCount; col <= ColumnCount; j++, col++)
                    {
                        if (CurrSellerOrder.ListItemQuantity[j] > 0)
                            xlSalesOrderWorksheet.Cells[row, col].Value = CurrSellerOrder.ListItemQuantity[j];
                        else
                            xlSalesOrderWorksheet.Cells[row, col].Value = DBNull.Value;
                    }
                }
                CommonFunctions.ReleaseCOMObject(xlSalesOrderWorksheet);
                xlSalesOrderWorkbook.Close(SaveChanges: true);
                CommonFunctions.ReleaseCOMObject(xlSalesOrderWorkbook);
                ReportProgressFunc(100);
                #endregion
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MergeSalesOrdersForm.backgroundWorker1_DoWork()", ex);
                throw;
            }
            finally
            {
                xlApp.Quit();
                CommonFunctions.ReleaseCOMObject(xlApp);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                CommonFunctions.UpdateProgressBar(e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MergeSalesOrdersForm.backgroundWorker1_ProgressChanged()", ex);
                throw;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                ListFiles.Clear();
                dtGridViewSelectedFiles.Rows.Clear();
                btnMerge.Enabled = false;
                btnCancel.Enabled = true;
                SetStatus("Merged Sales Order File:" + Path.GetFileName(txtBoxOutputFolder.Text) + " created Successfully");

                MessageBox.Show(this, "Merging of SalesOrders file Completed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetStatus("Please choose Sales Order Date to Merge");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MergeSalesOrdersForm.backgroundWorker1_RunWorkerCompleted()", ex);
                throw;
            }
        }
    }
}
