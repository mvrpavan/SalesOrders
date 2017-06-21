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
    public partial class AddNewOrderSheetForm : Form
    {
        Form MainForm = null;
        String MasterFilePath;
        List<Color> ListColors;

        public AddNewOrderSheetForm(Form ParentForm)
        {
            try
            {
                InitializeComponent();
                MainForm = ParentForm;
                MasterFilePath = MainForm.Controls["txtBoxFileName"].Text;
                txtBoxOutputFolder.Text = System.IO.Path.GetDirectoryName(MasterFilePath);
                
                ListColors = new List<Color>();
                ListColors.Add(Color.FromArgb(184, 204, 228));
                ListColors.Add(Color.FromArgb(218, 238, 243));
                ListColors.Add(Color.FromArgb(216, 228, 188));
                ListColors.Add(Color.FromArgb(228, 223, 236));
                ListColors.Add(Color.FromArgb(255, 255, 153));
                ListColors.Add(Color.FromArgb(224, 224, 224));
                ListColors.Add(Color.FromArgb(178, 255, 102));
                ListColors.Add(Color.FromArgb(255, 178, 102));
                ListColors.Add(Color.FromArgb(255, 153, 153));
                ListColors.Add(Color.FromArgb(51, 255, 153));

                prgrssBarProcess.Maximum = 100;
                prgrssBarProcess.Step = 1;
                prgrssBarProcess.Value = 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("AddNewOrderSheetForm", ex);
            }
        }

        private void btnOutFolderBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                folderBrowserDialog1.SelectedPath = txtBoxOutputFolder.Text;
                DialogResult dlgResult = folderBrowserDialog1.ShowDialog();

                if (dlgResult == System.Windows.Forms.DialogResult.OK)
                {
                    txtBoxOutputFolder.Text = folderBrowserDialog1.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("OutFolderBrowse_Click", ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreateOrderSheet_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            backgroundWorker1.WorkerReportsProgress = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Excel.Application xlApp = new Excel.Application();
            try
            {
                DataTable dtItemMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("ItemMaster", MasterFilePath, "*");
                DataTable dtSellerMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("SellerMaster", MasterFilePath, "*");
                List<String> ListVendors = dtItemMaster.AsEnumerable().Select(s => s.Field<String>("VendorName")).Distinct().ToList();

                Excel.Workbook xlWorkbook = xlApp.Workbooks.Add();//.Open(MasterFilePath);

                Excel.Worksheet xlWorkSheet = xlWorkbook.Worksheets.Add(); //xlWorkbook.Worksheets[xlWorkbook.Worksheets.Count - 1];
                xlWorkSheet.Name = dateTimeOrderSheet.Value.ToString("dd-MM-yyyy");

                #region Print Header
                List<String> HeaderItems = new List<String>();
                HeaderItems.Add("Sl.No.");
                HeaderItems.Add("Total Items");
                HeaderItems.Add("Name");
                HeaderItems.Add("Contact Details");
                DataRow[] drItems = dtItemMaster.Select("", "SlNo asc");
                Int32 ProgressBarCount = HeaderItems.Count + (drItems.Length * 2) + dtSellerMaster.Rows.Count;

                Int32 StartRow = 5, StartCol = 1, Counter = 0;
                for (int i = 0; i < HeaderItems.Count; i++)
                {
                    Excel.Range xlRange = xlWorkSheet.Cells[StartRow, StartCol + i];
                    xlRange.Value = HeaderItems[i];
                    if (!(HeaderItems[i].Equals("Name") || HeaderItems[i].Equals("Contact Details")))
                        xlRange.Orientation = 90;
                    xlRange.Font.Bold = true;
                    xlRange.Interior.Color = Color.FromArgb(242, 220, 219);
                    Counter++;
                    backgroundWorker1.ReportProgress((Counter * 100) / ProgressBarCount);
                }

                for (int i = 0; i < drItems.Length; i++)
                {
                    Excel.Range xlRange = xlWorkSheet.Cells[StartRow, StartCol + HeaderItems.Count + i];
                    xlRange.Value = drItems[i]["ItemName"].ToString();
                    xlRange.Orientation = 90;
                    xlRange.Font.Bold = true;
                    if (chkBoxMarkVendors.Checked)
                        xlRange.Interior.Color = ListColors[ListVendors.IndexOf(drItems[i]["VendorName"].ToString()) % ListColors.Count];
                    else
                        xlRange.Interior.Color = Color.FromArgb(242, 220, 219);
                    Counter++;
                    backgroundWorker1.ReportProgress((Counter * 100) / ProgressBarCount);
                }
                #endregion

                #region Print Sellers
                DataRow[] drSellers = dtSellerMaster.Select("", "SlNo asc");
                for (int i = 0; i < drSellers.Length; i++)
                {
                    xlWorkSheet.Cells[StartRow + i + 1, StartCol].Value = (i + 1);
                    Excel.Range xlRange1 = xlWorkSheet.Cells[StartRow + i + 1, StartCol + 4];
                    Excel.Range xlRange2 = xlWorkSheet.Cells[StartRow + i + 1, StartCol + 4 + drItems.Length - 1];
                    xlWorkSheet.Cells[StartRow + i + 1, StartCol + 1].Formula = "=Count(" + xlRange1.Address[false, false] + ":" + xlRange2.Address[false, false] + ")";

                    xlWorkSheet.Cells[StartRow + i + 1, StartCol + 2].Value = drSellers[i]["SellerName"].ToString();
                    xlWorkSheet.Cells[StartRow + i + 1, StartCol + 3].Value = ((drSellers[i]["Phone"] == DBNull.Value) ? "" : drSellers[i]["Phone"].ToString());
                    Counter++;
                    backgroundWorker1.ReportProgress((Counter * 100) / ProgressBarCount);
                }
                #endregion

                #region Print Total Quantity & Price
                xlWorkSheet.Cells[StartRow - 3, StartCol + 2].Value = "Price";
                Excel.Range tmpxlRange = xlWorkSheet.Cells[StartRow - 2, StartCol + 2];
                tmpxlRange.Value = "Total Quantity";
                tmpxlRange.Font.Bold = true;
                tmpxlRange.Interior.Color = Color.FromArgb(141, 180, 226);
                xlWorkSheet.Cells[StartRow - 2, StartCol].Interior.Color = Color.FromArgb(141, 180, 226);
                xlWorkSheet.Cells[StartRow - 2, StartCol + 1].Interior.Color = Color.FromArgb(141, 180, 226);
                xlWorkSheet.Cells[StartRow - 2, StartCol + 3].Interior.Color = Color.FromArgb(141, 180, 226);
                for (int i = 0; i < drItems.Length; i++)
                {
                    Excel.Range xlRange1 = xlWorkSheet.Cells[StartRow + 1, StartCol + 4 + i];
                    Excel.Range xlRange2 = xlWorkSheet.Cells[StartRow + drSellers.Length, StartCol + 4 + i];
                    Excel.Range xlRange = xlWorkSheet.Cells[StartRow - 2, StartCol + 4 + i];
                    xlRange.Formula = "=Sum(" + xlRange1.Address[false, false] + ":" + xlRange2.Address[false, false] + ")";
                    xlRange.Font.Bold = true;
                    xlRange.Interior.Color = Color.FromArgb(141, 180, 226);

                    xlWorkSheet.Cells[StartRow - 3, StartCol + 4 + i].Value = drItems[i]["SellingPrice"].ToString();
                    Counter++;
                    backgroundWorker1.ReportProgress((Counter * 100) / ProgressBarCount);
                }
                #endregion

                xlWorkSheet.UsedRange.Columns.AutoFit();

                backgroundWorker1.ReportProgress(((ProgressBarCount - 1) * 100) / ProgressBarCount);
                xlWorkbook.SaveAs(txtBoxOutputFolder.Text + "\\SalesOrder_" + xlWorkSheet.Name + ".xlsx");
                xlWorkbook.Close();

                CommonFunctions.ReleaseCOMObject(xlWorkbook);
                backgroundWorker1.ReportProgress(100);
                MessageBox.Show(this, "Created Sales Order Sheet Successfully", "Status", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateOrderSheet_Click", ex);
            }
            finally
            {
                xlApp.Quit();
                CommonFunctions.ReleaseCOMObject(xlApp);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            prgrssBarProcess.Value = e.ProgressPercentage;

            // Set the text.
            lblProgress.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            prgrssBarProcess.Value = 0;
            lblProgress.Text = "";
            btnCancel.Focus();
        }
    }
}
