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
                ReportProgressFunc(0);

                lblStatus.Text = "Reading SellerSummary File...";
                DataTable dtSellerSummary = CommonFunctions.ReturnDataTableFromExcelWorksheet("Seller Summary", SellerSummaryFilePath, "*", "A2:K100000");
                if (dtSellerSummary == null)
                {
                    MessageBox.Show(this, "Provided Seller Summary file doesn't contain \"Seller Summary\" Sheet.\nPlease provide correct file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStatus.Text = "Please provide file with \"Seller Summary\" sheet";
                    return;
                }
                DataColumn NewOldBalanceColumn = new DataColumn("NewOldBalance", Type.GetType("System.Double"), "IsNull([Net Sale], 0) + IsNull([OB], 0) - IsNull([Cash], 0)");
                NewOldBalanceColumn.DefaultValue = 0;
                dtSellerSummary.Columns.Add(NewOldBalanceColumn);
                dtSellerSummary.DefaultView.RowFilter = "IsNull([Sl#], 0) > 0";
                DataRow[] drSellers = dtSellerSummary.DefaultView.ToTable().Select("", "[Sl#] asc");

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

                    xlSellerMasterWorksheet.Cells[i, OldBalanceColIndex].Value = drSellers[SellerIndex]["NewOldBalance"];

                    ReportProgressFunc((CurrSellerCount * 100) / ProgressBarCount);
                    lblStatus.Text = "Updated " + CurrSellerCount + " of " + ProgressBarCount + " Sellers data in OrderMaster";
                }

                ReportProgressFunc(100);

                xlOrderMasterWorkbook.Save();
                xlOrderMasterWorkbook.Close();

                MessageBox.Show(this, "Updated OrderMaster file successfully", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblStatus.Text = "Completed updating OrderMaster";
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
    }
}
