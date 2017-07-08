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
        String OrderMasterFilePath, SellerSummaryFilePath;
        ToolStripProgressBar ToolStripProgressBarMainForm;
        ToolStripLabel ToolStripProgressBarStatus;

        public UpdateOrderMasterForm()
        {
            InitializeComponent();

            OrderMasterFilePath = CommonFunctions.MasterFilePath;
            lblStatus.Text = "";

            ToolStripProgressBarMainForm = CommonFunctions.ToolStripProgressBarMainForm;
            ToolStripProgressBarStatus = CommonFunctions.ToolStripProgressBarMainFormStatus;

            ToolStripProgressBarMainForm.Maximum = 100;
            ToolStripProgressBarMainForm.Step = 1;
            ToolStripProgressBarMainForm.Value = 0;
            ToolStripProgressBarStatus.Text = "";

            chkBoxUpdSellerMaster.Checked = true;
            chkBoxUpdSellerHistory.Checked = false;
            txtBoxSellerHistoryFile.Enabled = false;
            btnBrowsSellerHistory.Enabled = false;
            lblSellerHistoryFile.Enabled = false;
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
                SellerSummaryFilePath = txtBoxSellerSummaryFile.Text;
                //UpdateOrderMasterFile();
                // Start the BackgroundWorker.
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
            ToolStripProgressBarMainForm.Value = Math.Min(e.ProgressPercentage, 100);
            ToolStripProgressBarStatus.Text = Math.Min(e.ProgressPercentage, 100).ToString() + "%";
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
                if (txtBoxSellerSummaryFile.Text.Trim().Length == 0)
                {
                    MessageBox.Show(this, "Seller Summary file cannot be blank!!!\nPlease choose Seller Summary File.", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (chkBoxUpdSellerHistory.Checked)
                {
                    if (txtBoxSellerHistoryFile.Text.Trim().Length == 0)
                    {
                        String SellerHistoryFilePath = Path.GetDirectoryName(SellerSummaryFilePath) + @"\SellerHistory.xlsx";
                        if (File.Exists(SellerHistoryFilePath))
                        {
                            DialogResult result = MessageBox.Show(this, "\"" + SellerHistoryFilePath + "\" already exist.\nDo you want to append Seller History to this file?\nYes - Append to it\nNo - Backup it & Create new",
                                                    "Seller History", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                            switch (result)
                            {
                                case DialogResult.Cancel: return;
                                case DialogResult.No:
                                    String BackupFilePath = Path.GetDirectoryName(SellerSummaryFilePath) + @"\SellerHistory.bkp.xlsx";
                                    if (File.Exists(BackupFilePath)) File.Delete(BackupFilePath);
                                    File.Move(SellerHistoryFilePath, BackupFilePath);
                                    break;
                                case DialogResult.Yes:
                                    txtBoxSellerHistoryFile.Text = SellerHistoryFilePath;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

                ReportProgressFunc(0);

                lblStatus.Text = "Reading SellerSummary File...";
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

                if (chkBoxUpdSellerMaster.Checked)
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
                        lblStatus.Text = "Updated " + CurrSellerCount + " of " + ProgressBarCount + " Sellers data in OrderMaster";
                    }

                    ReportProgressFunc(100);

                    xlOrderMasterWorkbook.Save();
                    xlOrderMasterWorkbook.Close();

                    MessageBox.Show(this, "Updated OrderMaster file successfully", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblStatus.Text = "Completed updating OrderMaster";
                }

                if (chkBoxUpdSellerHistory.Checked)
                {
                    lblStatus.Text = "Updating Seller History file";
                    UpdateSellerHistory(xlApp, drSellers, SummaryCreationDate);
                    lblStatus.Text = "Completed updating Seller History file";
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateOrderMasterForm.UpdateOrderMasterFile()", ex);
            }
            finally
            {
                xlApp.Quit();
                CommonFunctions.ReleaseCOMObject(xlApp);
            }
        }

        void UpdateSellerHistory(Excel.Application xlApp, DataRow[] drSellers, DateTime SummaryCreationDate)
        {
            try
            {
                ReportProgressFunc(0);

                Excel.Workbook xlSellerHistoryWorkbook;
                Excel.Worksheet xlSellerHistoryWorksheet;

                Int32 ProgressBarCount = drSellers.Length, CurrSellerCount = 0;
                String SellerHistoryFilePath = txtBoxSellerHistoryFile.Text.Trim();
                Boolean SellerHistoryFileExists = true;
                String[] Header = new String[] { "Create Date", "Update Date", "Bill#", "Seller Name", "Sale", "Cancel", "Return", "Discount", "Total Tax", "Net Sale", "OB", "Cash", "Balance" };
                if (String.IsNullOrEmpty(SellerHistoryFilePath) || !File.Exists(SellerHistoryFilePath))
                {
                    SellerHistoryFilePath = Path.GetDirectoryName(SellerSummaryFilePath) + @"\SellerHistory.xlsx";
                    SellerHistoryFileExists = false;

                    xlSellerHistoryWorkbook = xlApp.Workbooks.Add();
                    xlSellerHistoryWorksheet = xlSellerHistoryWorkbook.Worksheets.Add();
                    xlSellerHistoryWorksheet.Name = "Seller History";
                    for (int i = 0; i < Header.Length; i++)
                    {
                        xlSellerHistoryWorksheet.Cells[1, i + 1].Value = Header[i];
                    }

                    Excel.Range xlRange1 = xlSellerHistoryWorksheet.Range[xlSellerHistoryWorksheet.Cells[1, 1], xlSellerHistoryWorksheet.Cells[1, Header.Length]];
                    xlRange1.Font.Bold = true;
                    CreateSellerInvoice.SetAllBorders(xlRange1);
                    xlSellerHistoryWorkbook.SaveAs(SellerHistoryFilePath);

                    Excel.Worksheet xlSheet = CommonFunctions.GetWorksheet(xlSellerHistoryWorkbook, "Sheet1");
                    if (xlSheet != null) xlSheet.Delete();
                    xlSheet = CommonFunctions.GetWorksheet(xlSellerHistoryWorkbook, "Sheet2");
                    if (xlSheet != null) xlSheet.Delete();
                    xlSheet = CommonFunctions.GetWorksheet(xlSellerHistoryWorkbook, "Sheet3");
                    if (xlSheet != null) xlSheet.Delete();
                }
                else
                {
                    xlSellerHistoryWorkbook = xlApp.Workbooks.Open(SellerHistoryFilePath);
                    xlSellerHistoryWorksheet = CommonFunctions.GetWorksheet(xlSellerHistoryWorkbook, "Seller History");
                }

                Int32 RowCount = xlSellerHistoryWorksheet.UsedRange.Rows.Count + 1;
                Int32 ColumnCount = xlSellerHistoryWorksheet.UsedRange.Columns.Count;
                Int32 StartRow = RowCount, StartColumn = 1, LastRow = 0;

                for (int i = 0; i < drSellers.Length; i++)
                {
                    CurrSellerCount++;
                    DataRow dtRow = drSellers[i];
                    if (dtRow[0] == DBNull.Value) continue;
                    if (String.IsNullOrEmpty(dtRow[0].ToString())) continue;
                    LastRow++;

                    xlSellerHistoryWorksheet.Cells[StartRow + i, StartColumn].Value = SummaryCreationDate.ToString("dd-MMM-yyyy");
                    xlSellerHistoryWorksheet.Cells[StartRow + i, StartColumn + 1].Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    for (int j = 1; j < dtRow.ItemArray.Length; j++)
                    {
                        xlSellerHistoryWorksheet.Cells[StartRow + i, StartColumn + 1 + j].Value = dtRow[j].ToString();
                        if (j >= 3) xlSellerHistoryWorksheet.Cells[StartRow + i, StartColumn + 1 + j].NumberFormat = "#,##0.00";
                    }

                    ReportProgressFunc((CurrSellerCount * 100) / ProgressBarCount);
                    lblStatus.Text = "Updated " + CurrSellerCount + " of " + ProgressBarCount + " Sellers data in Seller History";
                }
                ReportProgressFunc(100);

                Excel.Range xlRange = xlSellerHistoryWorksheet.Range[xlSellerHistoryWorksheet.Cells[StartRow - 1, StartColumn], xlSellerHistoryWorksheet.Cells[StartRow + LastRow - 1, StartColumn + Header.Length - 1]];
                CreateSellerInvoice.SetAllBorders(xlRange);

                xlSellerHistoryWorkbook.Save();
                xlSellerHistoryWorkbook.Close();

                String Message;
                if (SellerHistoryFileExists)
                {
                    Message = "Sellery History file is updated with Seller Summary details";
                }
                else
                {
                    Message = "Sellery History file is updated with Seller Summary details\n";
                    Message += "Seller History files is created at " + SellerHistoryFilePath;
                }
                MessageBox.Show(this, Message, "Seller History", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateOrderMasterForm.UpdateSellerHistory()", ex);
                throw ex;
            }
        }

        private void btnBrowsSellerHistory_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialgSellerSummary.Multiselect = false;
                FileDialgSellerSummary.FileName = OrderMasterFilePath;
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

        private void chkBoxUpdSellerMaster_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!chkBoxUpdSellerHistory.Checked && !chkBoxUpdSellerMaster.Checked) chkBoxUpdSellerHistory.Checked = true;
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
                if (!chkBoxUpdSellerHistory.Checked && !chkBoxUpdSellerMaster.Checked) chkBoxUpdSellerMaster.Checked = true;
                txtBoxSellerHistoryFile.Enabled = chkBoxUpdSellerHistory.Checked;
                btnBrowsSellerHistory.Enabled = chkBoxUpdSellerHistory.Checked;
                lblSellerHistoryFile.Enabled = chkBoxUpdSellerHistory.Checked;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateOrderMasterForm.chkBoxUpdSellerHistory_CheckedChanged()", ex);
            }
        }
    }
}
