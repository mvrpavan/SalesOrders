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
    public partial class VendorOrderSheetForm : Form
    {
        String MasterFilePath, ProductInventoryFile, ProductStockHistoryFile;
        List<Color> ListColors;

        public VendorOrderSheetForm()
        {
            InitializeComponent();

            MasterFilePath = CommonFunctions.MasterFilePath;
            txtBoxSaveFolderPath.Text = System.IO.Path.GetDirectoryName(MasterFilePath);

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

            CommonFunctions.ResetProgressBar();
            lblStatus.Text = "";
            cmbBoxTimePeriodUnits.Items.Clear();
            cmbBoxTimePeriodUnits.Items.Add("Days");
            cmbBoxTimePeriodUnits.Items.Add("Weeks");
            cmbBoxTimePeriodUnits.Items.Add("Months");
            cmbBoxTimePeriodUnits.Items.Add("Years");
            cmbBoxTimePeriodUnits.SelectedIndex = (Int32)CommonFunctions.ObjPurchaseOrderSettings.PastSalePeriodUnits;
            numUpDownTimePeriod.Value = CommonFunctions.ObjPurchaseOrderSettings.PastSalePeriodValue;

            openFileDialog1.Multiselect = false;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            txtBoxProductInventoryFile.Text = Path.GetDirectoryName(MasterFilePath) + @"\ProductInventory.xlsx";
            txtBoxProductStockHistoryFile.Text = Path.GetDirectoryName(MasterFilePath) + @"\ProductStockHistory.xlsx";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                folderBrowserDialog1.SelectedPath = txtBoxSaveFolderPath.Text;
                DialogResult dlgResult = folderBrowserDialog1.ShowDialog();

                if (dlgResult == System.Windows.Forms.DialogResult.OK)
                {
                    txtBoxSaveFolderPath.Text = folderBrowserDialog1.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorOrderSheetForm.btnBrowse_Click()", ex);
            }
        }

        private void btnBrowseProductInventoryFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.FileName = CommonFunctions.MasterFilePath;
                DialogResult dlgResult = openFileDialog1.ShowDialog();

                if (dlgResult == System.Windows.Forms.DialogResult.OK)
                {
                    txtBoxProductInventoryFile.Text = openFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorOrderSheetForm.btnBrowseProductInventoryFile_Click()", ex);
            }
        }

        private void btnBrowseProductStockHistoryFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.FileName = CommonFunctions.MasterFilePath;
                DialogResult dlgResult = openFileDialog1.ShowDialog();

                if (dlgResult == System.Windows.Forms.DialogResult.OK)
                {
                    txtBoxProductStockHistoryFile.Text = openFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorOrderSheetForm.btnBrowseProductStockHistoryFile_Click()", ex);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                ProductInventoryFile = txtBoxProductInventoryFile.Text.Trim();
                ProductStockHistoryFile = txtBoxProductStockHistoryFile.Text.Trim();

                if (String.IsNullOrEmpty(txtBoxSaveFolderPath.Text.Trim()) || !Directory.Exists(txtBoxSaveFolderPath.Text.Trim()))
                {
                    MessageBox.Show(this, "Save Folder Path is empty", "Create Vendor Order Sheet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (String.IsNullOrEmpty(ProductInventoryFile) || !File.Exists(ProductInventoryFile))
                {
                    MessageBox.Show(this, "Product Product Inventory file path is empty or does not exist", "Create Vendor Order Sheet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (String.IsNullOrEmpty(ProductStockHistoryFile) || !File.Exists(ProductStockHistoryFile))
                {
                    MessageBox.Show(this, "Product Stock History file path is empty or does not exist", "Create Vendor Order Sheet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //CreateVendorOrderSheet();
                ReportProgress = bgWorkerCreateVendorOrder.ReportProgress;
                bgWorkerCreateVendorOrder.RunWorkerAsync();
                bgWorkerCreateVendorOrder.WorkerReportsProgress = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorOrderSheetForm.btnCreate_Click()", ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        delegate void ReportProgressDel(Int32 ProgressState);
        ReportProgressDel ReportProgress = null;

        private void ReportProgressFunc(Int32 ProgressState)
        {
            if (ReportProgress == null) return;
            ReportProgress(ProgressState);
        }

        private void bgWorkerCreateVendorOrder_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                CommonFunctions.ToggleEnabledPropertyOfAllControls(this);
                lblStatus.Enabled = true;
                CreateVendorOrderSheet();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorOrderSheetForm.bgWorkerCreateVendorOrder_DoWork()", ex);
            }
        }

        private void bgWorkerCreateVendorOrder_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CommonFunctions.UpdateProgressBar(e.ProgressPercentage);
        }

        private void bgWorkerCreateVendorOrder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CommonFunctions.ResetProgressBar();

            CommonFunctions.ToggleEnabledPropertyOfAllControls(this);
            lblStatus.Enabled = true;
            btnClose.Focus();
        }

        private void CreateVendorOrderSheet()
        {
            Excel.Application xlApp = new Excel.Application();
            try
            {
                ProductMaster ObjProductMaster = CommonFunctions.ObjProductMaster;
                ObjProductMaster.ResetStockProducts();
                lblStatus.Text = "Loading Product Inventory file";
                DataTable dtProductInventory = CommonFunctions.ReturnDataTableFromExcelWorksheet("Inventory", ProductInventoryFile, "*");
                if (dtProductInventory == null)
                {
                    MessageBox.Show(this, "Product Inventory does not contain \"Inventory\" sheet", "Create Vendor Order Sheet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DataRow[] drProductsInventory = dtProductInventory.DefaultView.ToTable().Select("", "[StockName] asc");
                ObjProductMaster.LoadProductInventoryFile(drProductsInventory);

                lblStatus.Text = "Loading Product Sales from Stock History file";
                DataTable dtProductStockHistory = CommonFunctions.ReturnDataTableFromExcelWorksheet("Stock History", ProductStockHistoryFile, "[PO Date], [Type], [Stock Name], [Receive Qty]");
                if (dtProductStockHistory == null)
                {
                    MessageBox.Show(this, "Product Stock History does not contain \"Stock History\" sheet", "Create Vendor Order Sheet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ObjProductMaster.LoadProductPastSalesFromStockHistoryFile(dtProductStockHistory, DateTime.Today, (Int32)numUpDownTimePeriod.Value, ReportSettings.GetTimePeriodUnits(cmbBoxTimePeriodUnits.SelectedItem.ToString()));

                lblStatus.Text = "Creating Vendor Order Sheet";
                DataTable dtItemMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("ItemMaster", MasterFilePath, "*");
                DataTable dtVendorMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("VendorMaster", MasterFilePath, "*");
                List<String> ListVendors = dtItemMaster.AsEnumerable().Select(s => s.Field<String>("VendorName")).Distinct().ToList();
                DataRow[] drItems = dtItemMaster.Select("", "SlNo asc");

                Excel.Workbook xlWorkbook = xlApp.Workbooks.Add();

                Excel.Worksheet xlWorkSheet = xlWorkbook.Worksheets.Add();
                xlWorkSheet.Name = dateTimePickerVendorOrderDate.Value.ToString("dd-MM-yyyy");

                #region Print Header
                List<String> HeaderItems = new List<String>();
                HeaderItems.Add("Sl.No.");
                HeaderItems.Add("Total Items");
                HeaderItems.Add("Name");
                HeaderItems.Add("Line");
                HeaderItems.Add("Contact Details");
                Int32 ProgressBarCount = HeaderItems.Count + (drItems.Length * 2) + dtVendorMaster.Rows.Count;

                Int32 StartRow = 7, StartCol = 1, Counter = 0;
                for (int i = 0; i < HeaderItems.Count; i++)
                {
                    Excel.Range xlRange = xlWorkSheet.Cells[StartRow, StartCol + i];
                    xlRange.Value = HeaderItems[i];
                    if (!(HeaderItems[i].Equals("Name") || HeaderItems[i].Equals("Contact Details") || HeaderItems[i].Equals("Line")))
                        xlRange.Orientation = 90;
                    xlRange.Font.Bold = true;
                    xlRange.Interior.Color = Color.FromArgb(242, 220, 219);
                    Counter++;
                    ReportProgressFunc((Counter * 100) / ProgressBarCount);
                }

                Dictionary<String, DataRow> ListProducts = new Dictionary<String, DataRow>();
                for (int i = 0, j = 0; i < drItems.Length; i++)
                {
                    if (ListProducts.ContainsKey(drItems[i]["StockName"].ToString().Trim().ToUpper())) continue;

                    Excel.Range xlRange = xlWorkSheet.Cells[StartRow, StartCol + HeaderItems.Count + j];
                    xlRange.Value = drItems[i]["StockName"].ToString();
                    xlRange.Orientation = 90;
                    xlRange.Font.Bold = true;
                    xlRange.Interior.Color = Color.FromArgb(242, 220, 219);
                    Counter++;
                    j++;
                    ListProducts.Add(drItems[i]["StockName"].ToString().Trim().ToUpper(), drItems[i]);
                    ReportProgressFunc((Counter * 100) / ProgressBarCount);
                }
                #endregion

                #region Print Vendors
                DataRow[] drVendors = dtVendorMaster.Select("", "SlNo asc");
                for (int i = 0; i < drVendors.Length; i++)
                {
                    xlWorkSheet.Cells[StartRow + i + 1, StartCol].Value = (i + 1);
                    Excel.Range xlRange1 = xlWorkSheet.Cells[StartRow + i + 1, StartCol + HeaderItems.Count];
                    Excel.Range xlRange2 = xlWorkSheet.Cells[StartRow + i + 1, StartCol + HeaderItems.Count + ListProducts.Count - 1];
                    xlWorkSheet.Cells[StartRow + i + 1, StartCol + 1].Formula = "=Count(" + xlRange1.Address[false, false] + ":" + xlRange2.Address[false, false] + ")";

                    xlWorkSheet.Cells[StartRow + i + 1, StartCol + 2].Value = drVendors[i]["VendorName"].ToString();
                    xlWorkSheet.Cells[StartRow + i + 1, StartCol + 3].Value = drVendors[i]["Line"].ToString();
                    xlWorkSheet.Cells[StartRow + i + 1, StartCol + 4].Value = ((drVendors[i]["Phone"] == DBNull.Value) ? "" : drVendors[i]["Phone"].ToString());
                    Counter++;
                    ReportProgressFunc((Counter * 100) / ProgressBarCount);
                }
                #endregion

                #region Print Price, Last N Day Sale, Current stock & TotalQuantity
                Int32 PriceRowNum = StartRow - 5, PastSaleRowNum = StartRow - 4, CurrStockRowNum = StartRow - 3, TotalQtyRowNum = StartRow - 2;
                xlWorkSheet.Cells[PriceRowNum, StartCol + 2].Value = "Price";
                xlWorkSheet.Cells[PastSaleRowNum, StartCol + 2].Value = "Last " + (Int32)numUpDownTimePeriod.Value + " " + cmbBoxTimePeriodUnits.SelectedItem + " of Sales";
                xlWorkSheet.Cells[CurrStockRowNum, StartCol + 2].Value = "Current Stock";
                Excel.Range tmpxlRange = xlWorkSheet.Cells[TotalQtyRowNum, StartCol + 2];
                tmpxlRange.Value = "Total Quantity";
                tmpxlRange.Font.Bold = true;
                tmpxlRange.Interior.Color = Color.FromArgb(141, 180, 226);
                for (int i = 0; i < HeaderItems.Count; i++)
                {
                    xlWorkSheet.Cells[TotalQtyRowNum, StartCol + i].Interior.Color = Color.FromArgb(141, 180, 226);
                }

                Int32 ProductCount = 0;
                foreach (KeyValuePair<String, DataRow> item in ListProducts)
                {
                    Excel.Range xlRange1 = xlWorkSheet.Cells[StartRow + 1, StartCol + HeaderItems.Count + ProductCount];
                    Excel.Range xlRange2 = xlWorkSheet.Cells[StartRow + drVendors.Length, StartCol + HeaderItems.Count + ProductCount];
                    Excel.Range xlRange = xlWorkSheet.Cells[TotalQtyRowNum, StartCol + HeaderItems.Count + ProductCount];
                    xlRange.Formula = "=Sum(" + xlRange1.Address[false, false] + ":" + xlRange2.Address[false, false] + ")";
                    xlRange.Font.Bold = true;
                    xlRange.Interior.Color = Color.FromArgb(141, 180, 226);

                    xlWorkSheet.Cells[PriceRowNum, StartCol + HeaderItems.Count + ProductCount].Value = item.Value["PurchasePrice"].ToString();

                    StockProductDetails ObjStockProductDetails = ObjProductMaster.GetStockProductDetails(item.Key.Trim());
                    if (ObjStockProductDetails != null)
                    {
                        xlWorkSheet.Cells[PastSaleRowNum, StartCol + HeaderItems.Count + ProductCount].Value = ObjStockProductDetails.SaleQty;
                        xlWorkSheet.Cells[CurrStockRowNum, StartCol + HeaderItems.Count + ProductCount].Value = ObjStockProductDetails.Inventory;
                    }

                    Counter++;
                    ProductCount++;
                    ReportProgressFunc((Counter * 100) / ProgressBarCount);
                }
                #endregion

                xlWorkSheet.UsedRange.Columns.AutoFit();

                ReportProgressFunc(((ProgressBarCount - 1) * 100) / ProgressBarCount);
                xlWorkbook.SaveAs(txtBoxSaveFolderPath.Text + "\\VendorOrder_" + xlWorkSheet.Name + ".xlsx");
                xlWorkbook.Close();

                CommonFunctions.ReleaseCOMObject(xlWorkbook);
                ReportProgressFunc(100);
                lblStatus.Text = "Completed creation of Vendor Order Sheet";
                MessageBox.Show(this, "Created Vendor Order Sheet Successfully", "Status", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorOrderSheetForm.CreateVendorOrderSheet()", ex);
            }
            finally
            {
                xlApp.Quit();
                CommonFunctions.ReleaseCOMObject(xlApp);
            }
        }
    }
}
