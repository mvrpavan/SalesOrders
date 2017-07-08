using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace SalesOrdersReport
{
    public partial class SellerHistoryReportForm : Form
    {
        String SellerHistoryFilePath = "", SaveFolderPath = "";
        List<String> ListSellerNames = null;
        ToolStripProgressBar ToolStripProgressBarMainForm;
        ToolStripLabel ToolStripProgressBarStatus;

        public SellerHistoryReportForm()
        {
            InitializeComponent();

            chkBoxDateFilter.Checked = false;

            dateTimePickerStart.Format = DateTimePickerFormat.Custom;
            dateTimePickerStart.CustomFormat = "dd-MMM-yyyy";
            dateTimePickerStart.Value = DateTime.Now.Date;
            dateTimePickerStart.Enabled = false;

            dateTimePickerEnd.Format = DateTimePickerFormat.Custom;
            dateTimePickerEnd.CustomFormat = "dd-MMM-yyyy";
            dateTimePickerEnd.Value = DateTime.Now.Date;
            dateTimePickerEnd.Enabled = false;

            ToolStripProgressBarMainForm = CommonFunctions.ToolStripProgressBarMainForm;
            ToolStripProgressBarStatus = CommonFunctions.ToolStripProgressBarMainFormStatus;

            ToolStripProgressBarMainForm.Maximum = 100;
            ToolStripProgressBarMainForm.Step = 1;
            ToolStripProgressBarMainForm.Value = 0;
            ToolStripProgressBarStatus.Text = "";

            lblStatus.Text = "";
            
            FillSellerList();
        }

        #region Events
        private void btnCreateReport_Click(object sender, EventArgs e)
        {
            try
            {
                SellerHistoryFilePath = txtBoxSellerHistoryFilePath.Text.Trim();
                SaveFolderPath = txtBoxSaveFolderPath.Text.Trim();

                btnCreateReport.Enabled = false;
                btnClose.Enabled = false;

                bgWorkerSellerHistory.WorkerReportsProgress = true;
                bgWorkerSellerHistory.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerHistoryReportForm.btnCreateReport_Click()", ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkBoxDateFilter_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dateTimePickerStart.Enabled = chkBoxDateFilter.Checked;
                dateTimePickerEnd.Enabled = chkBoxDateFilter.Checked;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerHistoryReportForm.chkBoxDateFilter_CheckedChanged()", ex);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                FileDialogSellerHistory.Multiselect = false;
                FileDialogSellerHistory.FileName = "SellerHistory.xlsx";
                FileDialogSellerHistory.InitialDirectory = Path.GetDirectoryName(CommonFunctions.MasterFilePath);
                FileDialogSellerHistory.CheckPathExists = true;
                FileDialogSellerHistory.CheckFileExists = true;

                DialogResult dlgResult = FileDialogSellerHistory.ShowDialog();

                if (dlgResult == System.Windows.Forms.DialogResult.OK)
                {
                    txtBoxSellerHistoryFilePath.Text = FileDialogSellerHistory.FileName;
                    txtBoxSaveFolderPath.Text = Path.GetDirectoryName(FileDialogSellerHistory.FileName);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerHistoryReportForm.btnBrowse_Click()", ex);
            }
        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            try
            {
                folderBrowserDialogOutputFolder.ShowNewFolderButton = true;
                DialogResult dlgResilt = folderBrowserDialogOutputFolder.ShowDialog(this);

                if (dlgResilt == System.Windows.Forms.DialogResult.OK)
                {
                    txtBoxSaveFolderPath.Text = folderBrowserDialogOutputFolder.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerHistoryReportForm.btnBrowseFolder_Click()", ex);
            }
        }
        #endregion

        delegate void ReportProgressDel(Int32 ProgressState);
        ReportProgressDel ReportProgress;

        private void ReportProgressFunc(Int32 ProgressState)
        {
            if (ReportProgress == null) return;
            ReportProgress(ProgressState);
        }

        private void FillSellerList()
        {
            try
            {
                ListSellerNames = CommonFunctions.ObjSellerMaster.GetSellerList();

                cmbBoxSellerList.Items.Clear();
                cmbBoxSellerList.DataSource = ListSellerNames;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerHistoryReportForm.FillSellerList()", ex);
            }
        }

        private void CreateSellerReport()
        {
            Excel.Application xlApp = new Excel.Application();

            try
            {
                ReportProgressFunc(0);

                if (String.IsNullOrEmpty(SellerHistoryFilePath))
                {
                    MessageBox.Show(this, "Please select Seller History file!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (String.IsNullOrEmpty(SaveFolderPath))
                {
                    MessageBox.Show(this, "Please select Save Folder path!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                String SelectedSellerName = cmbBoxSellerList.SelectedItem.ToString().Trim();
                if (String.IsNullOrEmpty(SelectedSellerName))
                {
                    MessageBox.Show(this, "Please select valid Seller name from list!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (ListSellerNames.FindIndex(e => e.Trim().Equals(SelectedSellerName, StringComparison.InvariantCultureIgnoreCase)) < 0)
                {
                    MessageBox.Show(this, "Unable to find the Seller!!!\nPlease select valid Seller name from list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ReportProgressFunc(1);
                lblStatus.Text = "Reading Seller History.....";
                DataTable dtSellerSummary = CommonFunctions.ReturnDataTableFromExcelWorksheet("Seller History", SellerHistoryFilePath, "*");
                if (dtSellerSummary == null)
                {
                    MessageBox.Show(this, "Provided Seller History file doesn't contain \"Seller History\" Sheet.\nPlease provide correct file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStatus.Text = "Please provide file with \"Seller History\" sheet";
                    return;
                }
                ReportProgressFunc(10);

                dtSellerSummary.CaseSensitive = false;
                dtSellerSummary.DefaultView.RowFilter = "Trim([Seller Name]) = '" + SelectedSellerName + "'";
                if (chkBoxDateFilter.Checked)
                {
                    dtSellerSummary.DefaultView.RowFilter += " and [Create Date] >= '" + dateTimePickerStart.Value.ToString("dd-MMM-yyyy")
                                                            + "' and [Create Date] <= '" + dateTimePickerEnd.Value.ToString("dd-MMM-yyyy") + "'";
                }
                lblStatus.Text = "Filtering Seller History.....";
                DataRow[] drSellerRecords = dtSellerSummary.DefaultView.ToTable().Select("", "[Create Date] asc, [Bill#] asc");
                if (drSellerRecords == null || drSellerRecords.Length == 0)
                {
                    MessageBox.Show(this, "No data for the given filters found in Seller History.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //Create Date	Update Date	Bill#	Seller Name	Sale	Cancel	Return	Discount	Total Tax	Net Sale	OB	Cash	Balance

                Int32 ProgressBarCount = drSellerRecords.Length;

                lblStatus.Text = "Creating Seller Report.....";
                Excel.Workbook xlSellerReportWorkbook = xlApp.Workbooks.Add();
                Excel.Worksheet xlSellerReportWorksheet = xlSellerReportWorkbook.Worksheets[1];
                Excel.Worksheet xlSheet = CommonFunctions.GetWorksheet(xlSellerReportWorkbook, "Sheet2");
                if (xlSheet != null) xlSheet.Delete();
                xlSheet = CommonFunctions.GetWorksheet(xlSellerReportWorkbook, "Sheet3");
                if (xlSheet != null) xlSheet.Delete();

                Int32 StartRow = 1, StartCol = 1, LastCol = 0, SellerNameColIndex = -1;
                String SheetName = SelectedSellerName.Replace(":", "").
                        Replace("\\", "").Replace("/", "").
                        Replace("?", "").Replace("*", "").
                        Replace("[", "").Replace("]", "").
                        Replace("<", "").Replace(">", "").
                        Replace("|", "").Replace("\"", "");
                xlSellerReportWorksheet.Name = ((SheetName.Length > 30) ? SheetName.Substring(0, 30) : SheetName);

                for (int i = 0; i < dtSellerSummary.Columns.Count; i++)
                {
                    if (dtSellerSummary.Columns[i].ColumnName.Equals("Seller Name"))
                    {
                        SellerNameColIndex = i;
                        continue;
                    }
                    xlSellerReportWorksheet.Cells[StartRow, StartCol + LastCol].Value = dtSellerSummary.Columns[i].ColumnName;
                    LastCol++;
                }
                Excel.Range xlRange = xlSellerReportWorksheet.Range[xlSellerReportWorksheet.Cells[StartRow, StartCol], xlSellerReportWorksheet.Cells[StartRow, LastCol]];
                xlRange.Font.Bold = true;

                for (int i = 0; i < drSellerRecords.Length; i++)
                {
                    DataRow dtRow = drSellerRecords[i];
                    for (int j = 0, k = 0; j < dtRow.ItemArray.Length; j++)
                    {
                        if (j == SellerNameColIndex) continue;
                        xlSellerReportWorksheet.Cells[StartRow + 1 + i, StartCol + k].Value = dtRow[j];
                        if (k >= 3) xlSellerReportWorksheet.Cells[StartRow + 1 + i, StartCol + k].NumberFormat = "#,##0.00";
                        k++;
                    }

                    ReportProgressFunc(10 + ((i + 1) * 90/ProgressBarCount));
                }
                xlRange = xlSellerReportWorksheet.Range[xlSellerReportWorksheet.Cells[StartRow, StartCol], xlSellerReportWorksheet.Cells[StartRow + drSellerRecords.Length, LastCol]];
                CreateSellerInvoice.SetAllBorders(xlRange);

                xlSellerReportWorksheet.UsedRange.Columns.AutoFit();

                xlSellerReportWorkbook.SaveAs(SaveFolderPath + @"\SellerReport_" + SheetName.Replace(" ", "_") + ".xlsx");
                xlSellerReportWorkbook.Close();

                ReportProgressFunc(100);
                MessageBox.Show(this, "Created Seller Report for \"" + SelectedSellerName + "\"", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblStatus.Text = "Created Seller Report";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerHistoryReportForm.CreateSellerReport()", ex);
            }
            finally
            {
                xlApp.Quit();
                CommonFunctions.ReleaseCOMObject(xlApp);
            }
        }

        private void bgWorkerSellerHistory_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ReportProgress = bgWorkerSellerHistory.ReportProgress;
                CreateSellerReport();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerHistoryReportForm.bgWorkerSellerHistory_DoWork()", ex);
            }
        }

        private void bgWorkerSellerHistory_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                ToolStripProgressBarMainForm.Value = Math.Min(e.ProgressPercentage, 100);
                ToolStripProgressBarStatus.Text = Math.Min(e.ProgressPercentage, 100).ToString() + "%";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerHistoryReportForm.bgWorkerSellerHistory_ProgressChanged()", ex);
            }
        }

        private void bgWorkerSellerHistory_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                ToolStripProgressBarMainForm.Value = 0;
                ToolStripProgressBarStatus.Text = "";
                lblStatus.Text = "";

                btnCreateReport.Enabled = true;
                btnClose.Enabled = true;

                btnClose.Focus();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerHistoryReportForm.bgWorkerSellerHistory_RunWorkerCompleted()", ex);
            }
        }
    }
}
