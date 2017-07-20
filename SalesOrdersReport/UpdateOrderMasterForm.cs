using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace SalesOrdersReport
{
    public partial class UpdateOrderMasterForm : Form
    {
        String OrderMasterFilePath, SellerSummaryFilePath, SellerHistoryFilePath, ProductInventoryFilePath, ProductStockHistoryFilePath;

        public UpdateOrderMasterForm()
        {
            InitializeComponent();

            OrderMasterFilePath = CommonFunctions.MasterFilePath;
            lblStatus.Text = "";

            CommonFunctions.ResetProgressBar();

            chkBoxUpdSellerMaster.Checked = true;
            chkBoxUpdSellerHistory.Checked = true;
            chkBoxUpdProductSales.Checked = true;
            groupBoxHistoryUpdate.Enabled = false;
            btnUpdate.Enabled = false;
            FileDialgSellerSummary.Multiselect = false;

            CommonFunctions.ObjProductMaster.ResetStockProducts();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                backgroundWorker1_ProgressChanged(null, new ProgressChangedEventArgs(0, null));
                this.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateOrderMasterForm.btnClose_Click()", ex);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                SellerSummaryFilePath = txtBoxSellerSummaryFile.Text.Trim();
                SellerHistoryFilePath = txtBoxSellerHistoryFile.Text.Trim();
                ProductInventoryFilePath = txtBoxProductInventoryFile.Text.Trim();
                ProductStockHistoryFilePath = txtBoxProductStockHistoryFile.Text.Trim();

                //UpdateSellerMaster();
                // Start the BackgroundWorker.
                ReportProgress = backgroundWorker1.ReportProgress;
                backgroundWorker1.RunWorkerAsync();
                backgroundWorker1.WorkerReportsProgress = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateOrderMasterForm.btnUpdate_Click()", ex);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialgSellerSummary.FileName = "Invoice File";
                DialogResult dlgResult = FileDialgSellerSummary.ShowDialog();

                if (dlgResult == System.Windows.Forms.DialogResult.OK)
                {
                    txtBoxSellerSummaryFile.Text = FileDialgSellerSummary.FileName;
                    SellerSummaryFilePath = txtBoxSellerSummaryFile.Text;
                    txtBoxSellerHistoryFile.Text = Path.GetDirectoryName(SellerSummaryFilePath) + @"\SellerHistory.xlsx";
                    txtBoxProductInventoryFile.Text = Path.GetDirectoryName(SellerSummaryFilePath) + @"\ProductInventory.xlsx";
                    txtBoxProductStockHistoryFile.Text = Path.GetDirectoryName(SellerSummaryFilePath) + @"\ProductStockHistory.xlsx";
                    groupBoxHistoryUpdate.Enabled = true;
                    btnUpdate.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateOrderMasterForm.btnBrowse_Click", ex);
            }
        }

        private void btnBrowsSellerHistory_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialgSellerSummary.FileName = "SellerHistory.xlsx";
                DialogResult dlgResult = FileDialgSellerSummary.ShowDialog();

                if (dlgResult == System.Windows.Forms.DialogResult.OK)
                {
                    txtBoxSellerHistoryFile.Text = FileDialgSellerSummary.FileName;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateOrderMasterForm.btnBrowsSellerHistory_Click()", ex);
            }
        }

        private void btnBrowseProductInvFile_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialgSellerSummary.FileName = "ProductInventory.xlsx";
                DialogResult dlgResult = FileDialgSellerSummary.ShowDialog();

                if (dlgResult == System.Windows.Forms.DialogResult.OK)
                {
                    txtBoxProductInventoryFile.Text = FileDialgSellerSummary.FileName;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateOrderMasterForm.btnBrowseProductInvFile_Click()", ex);
            }
        }

        private void btnBrowseProductStockHistoryFile_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialgSellerSummary.FileName = "ProductStockHistory.xlsx";
                DialogResult dlgResult = FileDialgSellerSummary.ShowDialog();

                if (dlgResult == System.Windows.Forms.DialogResult.OK)
                {
                    txtBoxProductStockHistoryFile.Text = FileDialgSellerSummary.FileName;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateOrderMasterForm.btnBrowseProductStockHistoryFile_Click()", ex);
            }
        }

        private void chkBoxUpdSellerMaster_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!chkBoxUpdSellerHistory.Checked && !chkBoxUpdSellerMaster.Checked && !chkBoxUpdProductSales.Checked)
                    chkBoxUpdSellerHistory.Checked = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateOrderMasterForm.chkBoxUpdSellerMaster_CheckedChanged()", ex);
            }
        }

        private void chkBoxUpdSellerHistory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!chkBoxUpdSellerHistory.Checked && !chkBoxUpdSellerMaster.Checked && !chkBoxUpdProductSales.Checked)
                    chkBoxUpdProductSales.Checked = true;
                lblSellerHistoryFile.Enabled = chkBoxUpdSellerHistory.Checked;
                txtBoxSellerHistoryFile.Enabled = chkBoxUpdSellerHistory.Checked;
                btnBrowseSellerHistory.Enabled = chkBoxUpdSellerHistory.Checked;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateOrderMasterForm.chkBoxUpdSellerHistory_CheckedChanged()", ex);
            }
        }

        private void chkBoxUpdProductSales_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!chkBoxUpdSellerHistory.Checked && !chkBoxUpdSellerMaster.Checked && !chkBoxUpdProductSales.Checked)
                    chkBoxUpdSellerMaster.Checked = true;
                lblProductInventoryFile.Enabled = chkBoxUpdProductSales.Checked;
                txtBoxProductInventoryFile.Enabled = chkBoxUpdProductSales.Checked;
                btnBrowseProductInvFile.Enabled = chkBoxUpdProductSales.Checked;
                lblProductStockHistoryFile.Enabled = chkBoxUpdProductSales.Checked;
                txtBoxProductStockHistoryFile.Enabled = chkBoxUpdProductSales.Checked;
                btnBrowseProductStockHistoryFile.Enabled = chkBoxUpdProductSales.Checked;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateOrderMasterForm.chkBoxUpdProductSales_CheckedChanged()", ex);
            }
        }

        delegate void ReportProgressDel(Int32 ProgressState);
        ReportProgressDel ReportProgress = null;

        private void ReportProgressFunc(Int32 ProgressState)
        {
            if (ReportProgress == null) return;
            ReportProgress(ProgressState);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            btnUpdate.Enabled = false;
            btnClose.Enabled = false;

            UpdateSellerMaster();
            
            btnUpdate.Enabled = true;
            btnClose.Enabled = true;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CommonFunctions.UpdateProgressBar(e.ProgressPercentage);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CommonFunctions.ResetProgressBar();
            btnClose.Focus();
        }

        private void UpdateSellerMaster()
        {
            Excel.Application xlApp = new Excel.Application();

            try
            {
                if (txtBoxSellerSummaryFile.Text.Trim().Length == 0)
                {
                    MessageBox.Show(this, "Seller Summary file cannot be blank!!!\nPlease choose Seller Summary File.", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (chkBoxUpdSellerHistory.Checked)
                {
                    if (!CommonFunctions.ValidateFile_Overwrite_TakeBackup(this, Path.GetDirectoryName(SellerSummaryFilePath), ref SellerHistoryFilePath, "SellerHistory.xlsx", "Seller History")) return;
                    txtBoxSellerHistoryFile.Text = SellerHistoryFilePath;
                }

                ReportProgressFunc(0);

                lblStatus.Text = "Reading Seller Summary...";
                DataTable dtSellerSummary = CommonFunctions.ReturnDataTableFromExcelWorksheet("Seller Summary", SellerSummaryFilePath, "*", "A2:K100000");
                if (dtSellerSummary == null)
                {
                    MessageBox.Show(this, "Provided Seller Summary file doesn't contain \"Seller Summary\" Sheet.\nPlease provide correct file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStatus.Text = "Please provide file with \"Seller Summary\" sheet";
                    return;
                }
                DataColumn BalanceColumn = new DataColumn("Balance", Type.GetType("System.Double"), "IsNull([Net Sale], 0) + IsNull([OB], 0) - IsNull([Cash], 0)");
                BalanceColumn.DefaultValue = 0;
                dtSellerSummary.Columns.Add(BalanceColumn);
                dtSellerSummary.DefaultView.RowFilter = "IsNull([Sl#], 0) > 0";
                DataRow[] drSellers = dtSellerSummary.DefaultView.ToTable().Select("", "[Sl#] asc");

                Excel.Workbook xlSellerSummaryWorkbook = xlApp.Workbooks.Open(SellerSummaryFilePath);
                Excel.Worksheet xlSellerSummaryWorksheet = CommonFunctions.GetWorksheet(xlSellerSummaryWorkbook, "Seller Summary");
                DateTime SummaryCreationDate = DateTime.Parse(xlSellerSummaryWorksheet.Cells[1, 2].Value.ToString());
                xlSellerSummaryWorkbook.Close(false);

                String Message = "";
                if (chkBoxUpdSellerMaster.Checked)
                {
                    lblStatus.Text = "Updating Seller Master file";
                    UpdateSellerMasterData(xlApp, drSellers);
                    lblStatus.Text = "Completed updating Seller Master file";
                    Message += "\nSeller Master";
                }

                if (chkBoxUpdSellerHistory.Checked)
                {
                    lblStatus.Text = "Updating Seller History file";
                    UpdateSellerHistory(xlApp, drSellers, SummaryCreationDate);
                    lblStatus.Text = "Completed updating Seller History file";
                    Message += "\nSeller History";
                }

                if (chkBoxUpdProductSales.Checked)
                {
                    lblStatus.Text = "Updating Product Sales file";
                    UpdateProductData(xlApp, drSellers, SummaryCreationDate);
                    lblStatus.Text = "Completed updating Product Sales file";
                    Message += "\nProduct Inventory\nProduct Stock History";
                }

                MessageBox.Show(this, "Updated following details successfully:" + Message, "Update Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateOrderMasterForm.UpdateSellerMaster()", ex);
            }
            finally
            {
                xlApp.Quit();
                CommonFunctions.ReleaseCOMObject(xlApp);
            }
        }

        private void UpdateSellerMasterData(Excel.Application xlApp, DataRow[] drSellers)
        {
            try
            {
                Excel.Workbook xlOrderMasterWorkbook = xlApp.Workbooks.Open(OrderMasterFilePath);
                Excel.Worksheet xlSellerMasterWorksheet = CommonFunctions.GetWorksheet(xlOrderMasterWorkbook, "SellerMaster");

                Int32 RowCount = xlSellerMasterWorksheet.UsedRange.Rows.Count + 1;
                Int32 ColumnCount = xlSellerMasterWorksheet.UsedRange.Columns.Count;
                Int32 StartRow = 1, StartColumn = 1, OldBalanceColIndex = -1;
                for (int i = 0; i < ColumnCount; i++)
                {
                    if (xlSellerMasterWorksheet.Cells[StartRow, StartColumn + i].Value == null) continue;
                    String ColName = xlSellerMasterWorksheet.Cells[StartRow, StartColumn + i].Value;
                    if (ColName.Equals("OldBalance", StringComparison.InvariantCultureIgnoreCase))
                    {
                        OldBalanceColIndex = StartColumn + i;
                        break;
                    }
                }
                if (OldBalanceColIndex < 0)
                {
                    xlSellerMasterWorksheet.Cells[StartRow, ColumnCount + 1].Value = "OldBalance";
                    ColumnCount++;
                    OldBalanceColIndex = ColumnCount;
                }

                lblStatus.Text = "Processing SellerMaster...";
                Int32 ProgressBarCount = drSellers.Length, CurrSellerCount = 0;
                for (int i = StartRow + 1; i <= RowCount; i++)
                {
                    if (xlSellerMasterWorksheet.Cells[i, StartColumn + 1].Value == null) continue;
                    String SellerName = xlSellerMasterWorksheet.Cells[i, StartColumn + 1].Value;
                    Int32 SellerIndex = -1;
                    for (int j = 0; j < drSellers.Length; j++)
                    {
                        if (drSellers[j]["Seller Name"].ToString().Equals(SellerName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            SellerIndex = j;
                            break;
                        }
                    }

                    if (SellerIndex < 0) continue;
                    CurrSellerCount++;

                    xlSellerMasterWorksheet.Cells[i, OldBalanceColIndex].Value = drSellers[SellerIndex]["Balance"];

                    ReportProgressFunc((CurrSellerCount * 100) / ProgressBarCount);
                    lblStatus.Text = "Updated " + CurrSellerCount + " of " + ProgressBarCount + " Sellers data in OrderMaster";
                }
                ReportProgressFunc(100);

                xlOrderMasterWorkbook.Save();
                xlOrderMasterWorkbook.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateOrderMasterForm.UpdateSellerMasterData()", ex);
                throw;
            }
        }

        private void UpdateSellerHistory(Excel.Application xlApp, DataRow[] drSellers, DateTime SummaryCreationDate)
        {
            try
            {
                ReportProgressFunc(0);

                Excel.Workbook xlSellerHistoryWorkbook;
                Excel.Worksheet xlSellerHistoryWorksheet;

                Int32 ProgressBarCount = drSellers.Length, CurrSellerCount = 0;
                Boolean SellerHistoryFileExists = File.Exists(SellerHistoryFilePath);
                List<String> ListSellerKeys;
                String[] Header = new String[] { "Create Date", "Update Date", "Bill#", "Seller Name", "Sale", "Cancel", "Return", "Discount", "Total Tax", "Net Sale", "OB", "Cash", "Balance" };
                if (!SellerHistoryFileExists)
                {
                    xlSellerHistoryWorkbook = xlApp.Workbooks.Add();
                    xlSellerHistoryWorksheet = xlSellerHistoryWorkbook.Worksheets.Add();
                    xlSellerHistoryWorksheet.Name = "Seller History";
                    for (int i = 0; i < Header.Length; i++)
                    {
                        xlSellerHistoryWorksheet.Cells[1, i + 1].Value = Header[i];
                    }

                    Excel.Range xlRange1 = xlSellerHistoryWorksheet.Range[xlSellerHistoryWorksheet.Cells[1, 1], xlSellerHistoryWorksheet.Cells[1, Header.Length]];
                    xlRange1.Font.Bold = true;
                    SellerInvoiceForm.SetAllBorders(xlRange1);
                    xlSellerHistoryWorkbook.SaveAs(SellerHistoryFilePath);

                    Excel.Worksheet xlSheet = CommonFunctions.GetWorksheet(xlSellerHistoryWorkbook, "Sheet1");
                    if (xlSheet != null) xlSheet.Delete();
                    xlSheet = CommonFunctions.GetWorksheet(xlSellerHistoryWorkbook, "Sheet2");
                    if (xlSheet != null) xlSheet.Delete();
                    xlSheet = CommonFunctions.GetWorksheet(xlSellerHistoryWorkbook, "Sheet3");
                    if (xlSheet != null) xlSheet.Delete();

                    ListSellerKeys = new List<String>();
                }
                else
                {
                    DataTable dtSellerHistory = CommonFunctions.ReturnDataTableFromExcelWorksheet("Seller History", SellerHistoryFilePath, "[Create Date], [Bill#], [Seller Name]");
                    ListSellerKeys = dtSellerHistory.AsEnumerable().Select(s => s.Field<DateTime>("Create Date").ToString("dd-MMM-yyyy")
                                                + "||" + s.Field<Double>("Bill#").ToString()
                                                + "||" + s.Field<String>("Seller Name").Trim().ToUpper()).Distinct().ToList();

                    xlSellerHistoryWorkbook = xlApp.Workbooks.Open(SellerHistoryFilePath);
                    xlSellerHistoryWorksheet = CommonFunctions.GetWorksheet(xlSellerHistoryWorkbook, "Seller History");
                }

                Int32 RowCount = xlSellerHistoryWorksheet.UsedRange.Rows.Count;
                Int32 ColumnCount = xlSellerHistoryWorksheet.UsedRange.Columns.Count;
                Int32 StartRow = RowCount + 1, StartColumn = 1, LastRow = 0;
                Int32 OBColumnIndex = drSellers[0].Table.Columns.IndexOf("OB");

                Boolean InsertRecord = false;
                for (int i = 0; i < drSellers.Length; i++)
                {
                    CurrSellerCount++;
                    InsertRecord = true;
                    DataRow dtRow = drSellers[i];
                    if (dtRow[0] == DBNull.Value) continue;
                    if (String.IsNullOrEmpty(dtRow[0].ToString())) continue;
                    if (ListSellerKeys.Contains(SummaryCreationDate.ToString("dd-MMM-yyyy")
                                            + "||" + dtRow["Bill#"].ToString().Trim().ToUpper()
                                            + "||" + dtRow["Seller Name"].ToString().Trim().ToUpper())) InsertRecord = false;
                    if (InsertRecord)
                    {
                        InsertRecord = false;
                        for (int j = 4; j < dtRow.ItemArray.Length - 1; j++)
                        {
                            if (OBColumnIndex == j) continue;
                            if (dtRow[j] != DBNull.Value && !String.IsNullOrEmpty(dtRow[j].ToString().Trim())
                                && Double.Parse(dtRow[j].ToString().Trim()) > 1E-4)
                            {
                                InsertRecord = true;
                                break;
                            }
                        }
                    }

                    if (InsertRecord)
                    {
                        xlSellerHistoryWorksheet.Cells[StartRow + LastRow, StartColumn].Value = SummaryCreationDate.ToString("dd-MMM-yyyy");
                        xlSellerHistoryWorksheet.Cells[StartRow + LastRow, StartColumn + 1].Value = DateTime.Now.ToString("dd-MMM-yyyy");
                        for (int j = 1; j < dtRow.ItemArray.Length; j++)
                        {
                            xlSellerHistoryWorksheet.Cells[StartRow + LastRow, StartColumn + 1 + j].Value = dtRow[j].ToString();
                            if (j >= 3) xlSellerHistoryWorksheet.Cells[StartRow + LastRow, StartColumn + 1 + j].NumberFormat = "#,##0.00";
                        }

                        LastRow++;
                    }
                    ReportProgressFunc((CurrSellerCount * 100) / ProgressBarCount);
                    lblStatus.Text = "Updated " + CurrSellerCount + " of " + ProgressBarCount + " Sellers data in Seller History";
                }
                ReportProgressFunc(100);

                Excel.Range xlRange = xlSellerHistoryWorksheet.Range[xlSellerHistoryWorksheet.Cells[StartRow, StartColumn], xlSellerHistoryWorksheet.Cells[StartRow + LastRow - 1, StartColumn + Header.Length - 1]];
                SellerInvoiceForm.SetAllBorders(xlRange);

                xlSellerHistoryWorkbook.Save();
                xlSellerHistoryWorkbook.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateOrderMasterForm.UpdateSellerHistory()", ex);
                throw ex;
            }
        }

        private void UpdateProductData(Excel.Application xlApp, DataRow[] drSellers, DateTime SummaryCreationDate)
        {
            try
            {
                ProductMaster ObjProductMaster = CommonFunctions.ObjProductMaster;
                lblStatus.Text = "Loading Product Inventory file";
                DataTable dtProductInventory = CommonFunctions.ReturnDataTableFromExcelWorksheet("Inventory", ProductInventoryFilePath, "*");
                DataRow[] drProductsInventory = dtProductInventory.DefaultView.ToTable().Select("", "[StockName] asc");
                ObjProductMaster.LoadProductInventoryFile(drProductsInventory);

                lblStatus.Text = "Processing Product Inventory file";
                for (int i = 0; i < drSellers.Length; i++)
                {
                    lblStatus.Text = "Updating Product Inventory file for Seller " + (i + 1) + " of " + drSellers.Length;
                    String SheetName = drSellers[i]["Seller Name"].ToString().Replace(":", "").
                                        Replace("\\", "").Replace("/", "").
                                        Replace("?", "").Replace("*", "").
                                        Replace("[", "").Replace("]", "");
                    //DataTable dtSellerInvoice = CommonFunctions.ReturnDataTableFromExcelWorksheet(SheetName, SellerSummaryFilePath, "*", "A6:F100000");
                    Invoice ObjInvoice = new InvoiceGST();
                    DataTable dtSellerInvoice = ObjInvoice.LoadInvoice(SheetName, SellerSummaryFilePath);
                    if (dtSellerInvoice == null) continue;
                    dtSellerInvoice.DefaultView.RowFilter = "IsNull([Sl No], 0) > 0";
                    DataRow[] drProducts = dtSellerInvoice.DefaultView.ToTable().Select("", "[Sl No] asc");

                    ObjProductMaster.UpdateProductInventoryDataFromInvoice(drProducts);
                }
                ObjProductMaster.ComputeStockNetData("Sale");

                lblStatus.Text = "Updating Product Inventory file";
                ObjProductMaster.UpdateProductInventoryFile(xlApp, SummaryCreationDate, ProductInventoryFilePath);
                lblStatus.Text = "Completed updating Product Inventory file";

                lblStatus.Text = "Updating Product Stock History file";
                ObjProductMaster.UpdateProductStockHistoryFile(xlApp, SummaryCreationDate, "Sale", ProductStockHistoryFilePath);
                lblStatus.Text = "Completed updating Product Stock History file";

                CommonFunctions.ObjProductMaster.ResetStockProducts();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateOrderMasterForm.UpdateProductData()", ex);
                throw;
            }
        }
    }
}
