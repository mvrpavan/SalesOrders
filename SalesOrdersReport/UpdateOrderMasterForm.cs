using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace SalesOrdersReport
{
    public partial class UpdateOrderMasterForm : Form
    {
        MainForm ObjMainForm;
        String OrderMasterFilePath, SellerSummaryFilePath;

        public UpdateOrderMasterForm(MainForm _ObjMainForm)
        {
            InitializeComponent();

            ObjMainForm = _ObjMainForm;
            OrderMasterFilePath = ObjMainForm.Controls["txtBoxFileName"].Text;
            lblStatus.Text = "";
            lblProgress.Text = "0%";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                SellerSummaryFilePath = txtBoxSellerSummaryFile.Text;
                //UpdateOrderMasterFile();
                // Start the BackgroundWorker.
                backgroundWorker1.RunWorkerAsync();
                backgroundWorker1.WorkerReportsProgress = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateOrderMasterForm.btnUpdate_Click", ex);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialgSellerSummary.Multiselect = false;
                FileDialgSellerSummary.FileName = OrderMasterFilePath;
                DialogResult dlgResult = FileDialgSellerSummary.ShowDialog();

                if (dlgResult == System.Windows.Forms.DialogResult.OK)
                {
                    txtBoxSellerSummaryFile.Text = FileDialgSellerSummary.FileName;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateOrderMasterForm.btnBrowse_Click", ex);
            }
        }

        delegate void ReportProgressDel(Int32 ProgressState);
        ReportProgressDel ReportProgress;

        private void ReportProgressFunc(Int32 ProgressState)
        {
            if (ReportProgress == null) return;
            ReportProgress(ProgressState);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            btnUpdate.Enabled = false;
            btnClose.Enabled = false;
            
            ReportProgress = backgroundWorker1.ReportProgress;
            UpdateOrderMasterFile();
            
            btnUpdate.Enabled = true;
            btnClose.Enabled = true;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            prgsBarUpdate.Value = Math.Min(e.ProgressPercentage, 100);
            lblProgress.Text = Math.Min(e.ProgressPercentage, 100).ToString() + "%";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnClose.Focus();
        }

        private void UpdateOrderMasterFile()
        {
            Excel.Application xlApp = new Excel.Application();

            try
            {
                ReportProgressFunc(0);

                #region Update Seller Summary sheet
                lblStatus.Text = "Reading SellerSummary Sheet...";
                DataTable dtSellerSummary = CommonFunctions.ReturnDataTableFromExcelWorksheet("Seller Summary", SellerSummaryFilePath, "*", "A2:L100000");
                if (dtSellerSummary == null)
                {
                    MessageBox.Show(this, "Provided Seller Summary file doesn't contain \"Seller Summary\" Sheet.\nPlease provide correct file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStatus.Text = "Please provide file with \"Seller Summary\" sheet";
                    return;
                }

                dtSellerSummary.DefaultView.RowFilter = "IsNull([Sl#], 0) > 0";
                DataRow[] drSellers = dtSellerSummary.DefaultView.ToTable().Select("", "[Sl#] asc");

                UpdateSellerMasterSheet(drSellers, xlApp);
                #endregion

                #region Update Item Summary sheet
                lblStatus.Text = "Reading ItemSummary Sheet...";
                DataTable dtItemSummary = CommonFunctions.ReturnDataTableFromExcelWorksheet("Item Summary", SellerSummaryFilePath, "*", "A2:L100000");
                if (dtItemSummary == null)
                {
                    MessageBox.Show(this, "Provided Seller Summary file doesn't contain \"Item Summary\" Sheet.\nPlease provide correct file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStatus.Text = "Please provide file with \"Item Summary\" sheet";
                    return;
                }

                dtItemSummary.DefaultView.RowFilter = "IsNull([Sl#No#], 0) > 0";
                DataRow[] drItems = dtItemSummary.DefaultView.ToTable().Select("", "[Sl#No#] asc");

                UpdateItemSummarySheet(drSellers, drItems, xlApp);
                #endregion

                MessageBox.Show(this, "Updated OrderMaster & ItemSummary files successfully", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblStatus.Text = "Updated OrderMaster and ItemSummary";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateOrderMasterForm.UpdateOrderMasterFile", ex);
            }
            finally
            {
                xlApp.Quit();
                CommonFunctions.ReleaseCOMObject(xlApp);
            }
        }

        private void UpdateItemSummarySheet(DataRow[] drSellers, DataRow[] drItems, Excel.Application xlApp)
        {
            try
            {
                ReportProgressFunc(0);

                for (int i = 0; i < drItems.Length; i++)
                {
                    drItems[i]["Quantity"] = 0;
                    drItems[i]["Total"] = 0;
                }

                for (int i = 0; i < drSellers.Length; i++)
                {
                    String SellerName = drSellers[i]["Seller Name"].ToString();
                    String SheetName = SellerName.Replace(":", "").Replace("\\", "").Replace("/", "").
                        Replace("?", "").Replace("*", "").Replace("[", "").Replace("]", "");
                    SheetName = ((SheetName.Length > 30) ? SheetName.Substring(0, 30) : SheetName);

                    DataTable dtSellerInvoice = CommonFunctions.ReturnDataTableFromExcelWorksheet(SheetName, SellerSummaryFilePath, "*", "A6:L100000");
                    dtSellerInvoice.DefaultView.RowFilter = "IsNull([Sl#No#], 0) > 0";
                    DataRow[] drInvoiceItems = dtSellerInvoice.DefaultView.ToTable().Select("", "[Sl#No#] asc");

                    Double SaleQty = -1, TotalItemCost = -1;
                    for (int j = 0; j < drInvoiceItems.Length; j++)
                    {
                        if (drInvoiceItems[j]["Sales Qty"] != DBNull.Value && drInvoiceItems[j]["Sales Qty"].ToString().Trim().Length > 0)
                        {
                            if (!Double.TryParse(drInvoiceItems[j]["Sales Qty"].ToString().Trim(), out SaleQty)) continue;
                            if (!Double.TryParse(drInvoiceItems[j]["Total"].ToString().Trim(), out TotalItemCost)) continue;
                            if (Math.Abs(SaleQty) < 1E-4) continue;

                            for (int k = 0; k < drItems.Length; k++)
                            {
                                if (drItems[k]["Item Name"].ToString().Trim().Equals(drInvoiceItems[j]["Item Name"].ToString().Trim(), StringComparison.InvariantCultureIgnoreCase))
                                {
                                    drItems[k]["Quantity"] = Double.Parse(drItems[k]["Quantity"].ToString()) + SaleQty;
                                    drItems[k]["Total"] = Double.Parse(drItems[k]["Total"].ToString()) + TotalItemCost;
                                    break;
                                }
                            }
                        }
                    }
                }

                #region Update Quantity & Total in ItemSummary
                Excel.Workbook xlItemSummaryWorkbook = xlApp.Workbooks.Open(SellerSummaryFilePath);
                Excel.Worksheet xlItemSummaryWorksheet = CommonFunctions.GetWorksheet(xlItemSummaryWorkbook, "Item Summary");

                Int32 RowCount = xlItemSummaryWorksheet.UsedRange.Rows.Count + 1;
                Int32 ColumnCount = xlItemSummaryWorksheet.UsedRange.Columns.Count;
                Int32 StartRow = 2, StartColumn = 1;
                List<String> ListCols = new List<String>();
                for (int i = 0; i < ColumnCount; i++)
                {
                    if (xlItemSummaryWorksheet.Cells[StartRow, StartColumn + i].Value == null) continue;
                    String ColName = xlItemSummaryWorksheet.Cells[StartRow, StartColumn + i].Value;
                    ListCols.Add(ColName.Trim());
                }

                Int32 ItemNameColIndex = ListCols.FindIndex(e => e.Equals("Item Name", StringComparison.InvariantCultureIgnoreCase)) + 1;
                Int32 QuantityColIndex = ListCols.FindIndex(e => e.Equals("Quantity", StringComparison.InvariantCultureIgnoreCase)) + 1;
                Int32 TotalColIndex = ListCols.FindIndex(e => e.Equals("Total", StringComparison.InvariantCultureIgnoreCase)) + 1;

                lblStatus.Text = "Processing Item Summary File...";
                Int32 ProgressBarCount = drItems.Length, CurrItemCount = 0;
                Double TotalCost = 0;
                for (int i = StartRow + 1; i <= RowCount; i++)
                {
                    if (xlItemSummaryWorksheet.Cells[i, ItemNameColIndex].Value == null) continue;
                    String ItemName = xlItemSummaryWorksheet.Cells[i, ItemNameColIndex].Value;
                    Int32 ItemIndex = -1;
                    for (int j = 0; j < drItems.Length; j++)
                    {
                        if (drItems[j]["Item Name"].ToString().Equals(ItemName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            ItemIndex = j;
                            break;
                        }
                    }

                    CurrItemCount++;
                    if (ItemIndex < 0)
                    {
                        xlItemSummaryWorksheet.Cells[i, QuantityColIndex].Value = 0;
                        xlItemSummaryWorksheet.Cells[i, TotalColIndex].Value = 0;
                        ReportProgressFunc((CurrItemCount * 100) / ProgressBarCount);
                        continue;
                    }

                    xlItemSummaryWorksheet.Cells[i, QuantityColIndex].Value = drItems[ItemIndex]["Quantity"].ToString();
                    xlItemSummaryWorksheet.Cells[i, TotalColIndex].Value = drItems[ItemIndex]["Total"].ToString();
                    TotalCost += Double.Parse(drItems[ItemIndex]["Total"].ToString());

                    ReportProgressFunc((CurrItemCount * 100) / ProgressBarCount);
                    lblStatus.Text = "Updated " + CurrItemCount + " of " + ProgressBarCount + " Items data";
                }

                xlItemSummaryWorksheet.Cells[drItems.Length + 3, TotalColIndex].Value = TotalCost;
                #endregion

                xlItemSummaryWorkbook.Save();
                xlItemSummaryWorkbook.Close();

                ReportProgressFunc(100);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateOrderMasterForm.UpdateItemSummarySheet()", ex);
                throw;
            }
        }

        private void UpdateSellerMasterSheet(DataRow[] drSellers, Excel.Application xlApp)
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

                lblStatus.Text = "Processing OrderMaster File...";
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
                    lblStatus.Text = "Updated " + CurrSellerCount + " of " + ProgressBarCount + " Sellers data";
                }

                xlOrderMasterWorkbook.Save();
                xlOrderMasterWorkbook.Close();
                
                ReportProgressFunc(100);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateOrderMasterForm.UpdateSellerMasterSheet()", ex);
                throw;
            }
        }
    }
}
