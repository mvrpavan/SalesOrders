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
    public partial class UpdateProductPurchasesForm : Form
    {

        Excel.Application xlApp;
        String VendorPOFile, VendorHistoryFile, ProductInventoryFile, ProductStockHistoryFile;
        
        public UpdateProductPurchasesForm()
        {
            InitializeComponent();

            CommonFunctions.ResetProgressBar();

            lblStatus.Text = "";

            openFileDialog1.Multiselect = false;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            groupBoxProductVendorFiles.Enabled = false;
            btnUpdate.Enabled = false;
            btnClose.Enabled = true;
            chkBoxUpdProductInventory.Checked = true;
            chkBoxUpdStockHistory.Checked = true;
            chkBoxUpdVendorHistory.Checked = true;

            CommonFunctions.ObjProductMaster.ResetStockProducts();
        }

        delegate void ReportProgressDel(Int32 ProgressState);
        ReportProgressDel ReportProgress = null;

        private void ReportProgressFunc(Int32 ProgressState)
        {
            if (ReportProgress == null) return;
            ReportProgress(ProgressState);
        }

        private void btnBrowseVendorPOFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.FileName = CommonFunctions.MasterFilePath;
                DialogResult dlgResult = openFileDialog1.ShowDialog();

                if (dlgResult == System.Windows.Forms.DialogResult.OK)
                {
                    txtBoxVendorPOFile.Text = openFileDialog1.FileName;
                    txtBoxVendorHistoryFile.Text = Path.GetDirectoryName(txtBoxVendorPOFile.Text) + @"\VendorHistory.xlsx";
                    txtBoxProductInventoryFile.Text = Path.GetDirectoryName(txtBoxVendorPOFile.Text) + @"\ProductInventory.xlsx";
                    txtBoxProductStockHistoryFile.Text = Path.GetDirectoryName(txtBoxVendorPOFile.Text) + @"\ProductStockHistory.xlsx";
                    groupBoxProductVendorFiles.Enabled = true;
                    btnUpdate.Enabled = true;
                    btnClose.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateProductPurchasesForm.btnBrowseVendorPOFile_Click()", ex);
            }
        }

        private void chkBoxUpdProductInventory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!chkBoxUpdProductInventory.Checked && !chkBoxUpdVendorHistory.Checked && !chkBoxUpdStockHistory.Checked)
                    chkBoxUpdStockHistory.Checked = true;

                btnBrowseProductInvFile.Enabled = chkBoxUpdProductInventory.Checked;
                txtBoxProductInventoryFile.Enabled = chkBoxUpdProductInventory.Checked;
                lblProductInventoryFile.Enabled = chkBoxUpdProductInventory.Checked;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateProductPurchasesForm.chkBoxUpdProductInventory_CheckedChanged()", ex);
            }
        }

        private void chkBoxUpdStockHistory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!chkBoxUpdProductInventory.Checked && !chkBoxUpdVendorHistory.Checked && !chkBoxUpdStockHistory.Checked)
                    chkBoxUpdVendorHistory.Checked = true;
                
                btnBrowseProductStockHistFile.Enabled = chkBoxUpdStockHistory.Checked;
                txtBoxProductStockHistoryFile.Enabled = chkBoxUpdStockHistory.Checked;
                lblProductStockHistoryFile.Enabled = chkBoxUpdStockHistory.Checked;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateProductPurchasesForm.chkBoxUpdStockHistory_CheckedChanged()", ex);
            }
        }

        private void chkBoxUpdVendorHistory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!chkBoxUpdProductInventory.Checked && !chkBoxUpdVendorHistory.Checked && !chkBoxUpdStockHistory.Checked)
                    chkBoxUpdProductInventory.Checked = true;

                btnBrowseVendorHistory.Enabled = chkBoxUpdVendorHistory.Checked;
                txtBoxVendorHistoryFile.Enabled = chkBoxUpdVendorHistory.Checked;
                lblVendorHistoryFile.Enabled = chkBoxUpdVendorHistory.Checked;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateProductPurchasesForm.chkBoxUpdVendorHistory_CheckedChanged()", ex);
            }
        }

        private void btnBrowseProductInvFile_Click(object sender, EventArgs e)
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
                CommonFunctions.ShowErrorDialog("UpdateProductPurchasesForm.btnBrowseProductInvFile_Click()", ex);
            }
        }

        private void btnBrowseVendorHistory_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Multiselect = false;
                openFileDialog1.FileName = CommonFunctions.MasterFilePath;
                DialogResult dlgResult = openFileDialog1.ShowDialog();

                if (dlgResult == System.Windows.Forms.DialogResult.OK)
                {
                    txtBoxVendorHistoryFile.Text = openFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateProductPurchasesForm.btnBrowseVendorHistory_Click()", ex);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                VendorPOFile = txtBoxVendorPOFile.Text.Trim();
                VendorHistoryFile = txtBoxVendorHistoryFile.Text.Trim();
                ProductInventoryFile = txtBoxProductInventoryFile.Text.Trim();
                ProductStockHistoryFile = txtBoxProductStockHistoryFile.Text.Trim();

                //UpdateDetailsFromVendorPOFile();
                ReportProgress = bgWorkerUpdPurchasesForm.ReportProgress;
                bgWorkerUpdPurchasesForm.WorkerReportsProgress = true;
                bgWorkerUpdPurchasesForm.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateProductPurchasesForm.btnUpdate_Click()", ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bgWorkerUpdPurchasesForm_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                btnUpdate.Enabled = false;
                btnClose.Enabled = false;
                btnBrowseVendorPOFile.Enabled = false;
                btnBrowseVendorHistory.Enabled = false;
                btnBrowseProductInvFile.Enabled = false;
                btnBrowseProductStockHistFile.Enabled = false;

                UpdateDetailsFromVendorPOFile();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateProductPurchasesForm.bgWorkerUpdPurchasesForm_DoWork()", ex);
            }
        }

        private void bgWorkerUpdPurchasesForm_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                CommonFunctions.UpdateProgressBar(e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateProductPurchasesForm.bgWorkerUpdPurchasesForm_ProgressChanged()", ex);
            }
        }

        private void bgWorkerUpdPurchasesForm_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                CommonFunctions.ResetProgressBar();
                btnUpdate.Enabled = true;
                btnClose.Enabled = true;
                btnBrowseVendorPOFile.Enabled = true;
                btnBrowseVendorHistory.Enabled = true;
                btnBrowseProductInvFile.Enabled = true;
                btnBrowseProductStockHistFile.Enabled = true;
                btnClose.Focus();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateProductPurchasesForm.bgWorkerUpdPurchasesForm_RunWorkerCompleted()", ex);
            }
        }

        private void UpdateDetailsFromVendorPOFile()
        {
            xlApp = new Excel.Application();
            try
            {
                if (txtBoxVendorPOFile.Text.Trim().Length == 0)
                {
                    MessageBox.Show(this, "Vendor Purchase Orders file cannot be blank!!!\nPlease choose Vendor PO File.", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                #region Check Vendor History file
                if (chkBoxUpdVendorHistory.Checked)
                {
                    if (!CommonFunctions.ValidateFile_Overwrite_TakeBackup(this, Path.GetDirectoryName(VendorPOFile), ref VendorHistoryFile, "VendorHistory.xlsx", "Vendor History")) return;
                    txtBoxVendorHistoryFile.Text = VendorHistoryFile;
                }
                #endregion

                #region Check Product Inventory file
                if (chkBoxUpdProductInventory.Checked)
                {
                    if (ProductInventoryFile.Length == 0)
                    {
                        ProductInventoryFile = Path.GetDirectoryName(VendorPOFile) + @"\ProductInventory.xlsx";
                        txtBoxProductInventoryFile.Text = ProductInventoryFile;
                    }

                    if (!File.Exists(ProductInventoryFile))
                    {
                        MessageBox.Show(this, "Cannot find \"" + ProductInventoryFile + "\" file.\nPlease Provide Valid Product Inventory file.",
                                                "Product Inventory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                #endregion

                #region Check Product Stock History file
                if (chkBoxUpdStockHistory.Checked)
                {
                    if (!CommonFunctions.ValidateFile_Overwrite_TakeBackup(this, Path.GetDirectoryName(VendorPOFile), ref ProductStockHistoryFile, "ProductStockHistory.xlsx", "Stock History")) return;
                    txtBoxProductStockHistoryFile.Text = ProductStockHistoryFile;
                }
                #endregion

                ReportProgressFunc(0);
                lblStatus.Text = "Reading Vendor Summary...";
                DataTable dtVendorSummary = CommonFunctions.ReturnDataTableFromExcelWorksheet("Vendor Summary", VendorPOFile, "*", "A2:K100000");
                if (dtVendorSummary == null)
                {
                    MessageBox.Show(this, "Provided Vendor PO file doesn't contain \"Vendor Summary\" Sheet.\nPlease provide correct file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStatus.Text = "Please provide file with \"Vendor Summary\" sheet";
                    return;
                }

                dtVendorSummary.DefaultView.RowFilter = "IsNull([Sl#], 0) > 0";
                DataRow[] drVendors = dtVendorSummary.DefaultView.ToTable().Select("", "[Sl#] asc");

                Excel.Workbook xlVendorSummaryWorkbook = xlApp.Workbooks.Open(VendorPOFile);
                Excel.Worksheet xlVendorSummaryWorksheet = CommonFunctions.GetWorksheet(xlVendorSummaryWorkbook, "Vendor Summary");
                DateTime SummaryCreationDate = DateTime.Parse(xlVendorSummaryWorksheet.Cells[1, 2].Value.ToString());
                xlVendorSummaryWorkbook.Close(false);

                if (chkBoxUpdVendorHistory.Checked)
                {
                    lblStatus.Text = "Updating Vendor History file";
                    UpdateVendorHistoryFile(xlApp, drVendors, SummaryCreationDate);
                    lblStatus.Text = "Completed updating Vendor History file";
                }

                if (chkBoxUpdStockHistory.Checked || chkBoxUpdProductInventory.Checked)
                {
                    ProductMaster ObjProductMaster = CommonFunctions.ObjProductMaster;
                    lblStatus.Text = "Loading Product Inventory file";
                    DataTable dtProductInventory = CommonFunctions.ReturnDataTableFromExcelWorksheet("Inventory", ProductInventoryFile, "*");
                    DataRow[] drProductsInventory = dtProductInventory.DefaultView.ToTable().Select("", "[StockName] asc");
                    ObjProductMaster.LoadProductInventoryFile(drProductsInventory);

                    lblStatus.Text = "Processing Product Inventory file";
                    for (int i = 0; i < drVendors.Length; i++)
                    {
                        lblStatus.Text = "Updating Product Inventory file for Vendor " + (i + 1) + " of " + drVendors.Length;
                        String SheetName = drVendors[i]["Vendor Name"].ToString().Replace(":", "").
                                            Replace("\\", "").Replace("/", "").
                                            Replace("?", "").Replace("*", "").
                                            Replace("[", "").Replace("]", "");
                        DataTable dtVendorPO = CommonFunctions.ReturnDataTableFromExcelWorksheet(SheetName, VendorPOFile, "*", "A6:F100000");
                        dtVendorPO.DefaultView.RowFilter = "IsNull([Sl#No#], 0) > 0";
                        DataRow[] drProducts = dtVendorPO.DefaultView.ToTable().Select("", "[Sl#No#] asc");

                        if (SheetName.Equals("Stock", StringComparison.InvariantCultureIgnoreCase))
                            ObjProductMaster.UpdateProductInventoryDataFromPO(drProducts, true);
                        else
                            ObjProductMaster.UpdateProductInventoryDataFromPO(drProducts, false);
                    }
                    ObjProductMaster.ComputeStockNetData("Purchase");

                    if (chkBoxUpdProductInventory.Checked)
                    {
                        lblStatus.Text = "Updating Product Inventory file";
                        ObjProductMaster.UpdateProductInventoryFile(this, xlApp, SummaryCreationDate, ProductInventoryFile);
                        lblStatus.Text = "Completed updating Product Inventory file";
                    }

                    if (chkBoxUpdStockHistory.Checked)
                    {
                        lblStatus.Text = "Updating Product Stock History file";
                        ObjProductMaster.UpdateProductStockHistoryFile(this, xlApp, SummaryCreationDate, "Purchase", ProductStockHistoryFile);
                        lblStatus.Text = "Completed updating Product Stock History file";
                    }

                    CommonFunctions.ObjProductMaster.ResetStockProducts();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateProductPurchasesForm.UpdateDetailsFromVendorPOFile()", ex);
            }
            finally
            {
                xlApp.Quit();
                CommonFunctions.ReleaseCOMObject(xlApp);
            }
        }

        private void UpdateVendorHistoryFile(Excel.Application xlApp, DataRow[] drVendors, DateTime SummaryCreationDate)
        {
            try
            {
                ReportProgressFunc(0);

                Excel.Workbook xlVendorHistoryWorkbook;
                Excel.Worksheet xlVendorHistoryWorksheet;

                Int32 ProgressBarCount = drVendors.Length, CurrVendorCount = 0;
                Boolean VendorHistoryFileExists = true;
                List<String> ListVendorKeys;
                String[] Header = new String[] { "Create Date", "Update Date", "Bill#", "Vendor Name", "Sale", "Cancel", "Return", "Discount", "Total Tax", "Net Sale", "Cash"};
                if (!File.Exists(VendorHistoryFile))
                {
                    VendorHistoryFileExists = false;

                    xlVendorHistoryWorkbook = xlApp.Workbooks.Add();
                    xlVendorHistoryWorksheet = xlVendorHistoryWorkbook.Worksheets.Add();
                    xlVendorHistoryWorksheet.Name = "Vendor History";
                    for (int i = 0; i < Header.Length; i++)
                    {
                        xlVendorHistoryWorksheet.Cells[1, i + 1].Value = Header[i];
                    }

                    Excel.Range xlRange1 = xlVendorHistoryWorksheet.Range[xlVendorHistoryWorksheet.Cells[1, 1], xlVendorHistoryWorksheet.Cells[1, Header.Length]];
                    xlRange1.Font.Bold = true;
                    SellerInvoiceForm.SetAllBorders(xlRange1);
                    xlVendorHistoryWorkbook.SaveAs(VendorHistoryFile);

                    Excel.Worksheet xlSheet = CommonFunctions.GetWorksheet(xlVendorHistoryWorkbook, "Sheet1");
                    if (xlSheet != null) xlSheet.Delete();
                    xlSheet = CommonFunctions.GetWorksheet(xlVendorHistoryWorkbook, "Sheet2");
                    if (xlSheet != null) xlSheet.Delete();
                    xlSheet = CommonFunctions.GetWorksheet(xlVendorHistoryWorkbook, "Sheet3");
                    if (xlSheet != null) xlSheet.Delete();

                    ListVendorKeys = new List<String>();
                }
                else
                {
                    DataTable dtVendorHistory = CommonFunctions.ReturnDataTableFromExcelWorksheet("Vendor History", VendorHistoryFile, "[Create Date], [Bill#], [Vendor Name]");
                    ListVendorKeys = dtVendorHistory.AsEnumerable().Select(s => s.Field<DateTime>("Create Date").ToString("dd-MMM-yyyy")
                                                + "||" + s.Field<Double>("Bill#").ToString()
                                                + "||" + s.Field<String>("Vendor Name").Trim().ToUpper()).Distinct().ToList();

                    xlVendorHistoryWorkbook = xlApp.Workbooks.Open(VendorHistoryFile);
                    xlVendorHistoryWorksheet = CommonFunctions.GetWorksheet(xlVendorHistoryWorkbook, "Vendor History");
                }

                Int32 RowCount = xlVendorHistoryWorksheet.UsedRange.Rows.Count;
                Int32 ColumnCount = xlVendorHistoryWorksheet.UsedRange.Columns.Count;
                Int32 StartRow = RowCount, StartColumn = 1, LastRow = 0;

                for (int i = 0; i < drVendors.Length; i++)
                {
                    CurrVendorCount++;
                    DataRow dtRow = drVendors[i];
                    if (dtRow[0] == DBNull.Value) continue;
                    if (String.IsNullOrEmpty(dtRow[0].ToString())) continue;
                    if (ListVendorKeys.Contains(SummaryCreationDate.ToString("dd-MMM-yyyy")
                                            + "||" + dtRow["Bill#"].ToString().Trim().ToUpper()
                                            + "||" + dtRow["Vendor Name"].ToString().Trim().ToUpper())) continue;
                    LastRow++;

                    xlVendorHistoryWorksheet.Cells[StartRow + LastRow, StartColumn].Value = SummaryCreationDate.ToString("dd-MMM-yyyy");
                    xlVendorHistoryWorksheet.Cells[StartRow + LastRow, StartColumn + 1].Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    for (int j = 1; j < dtRow.ItemArray.Length; j++)
                    {
                        xlVendorHistoryWorksheet.Cells[StartRow + LastRow, StartColumn + 1 + j].Value = dtRow[j].ToString();
                        if (j >= 3) xlVendorHistoryWorksheet.Cells[StartRow + LastRow, StartColumn + 1 + j].NumberFormat = "#,##0.00";
                    }

                    ReportProgressFunc((CurrVendorCount * 100) / ProgressBarCount);
                    lblStatus.Text = "Updated " + CurrVendorCount + " of " + ProgressBarCount + " Vendors data in Vendor History";
                }
                ReportProgressFunc(100);

                Excel.Range xlRange = xlVendorHistoryWorksheet.Range[xlVendorHistoryWorksheet.Cells[StartRow, StartColumn], xlVendorHistoryWorksheet.Cells[StartRow + LastRow, StartColumn + Header.Length - 1]];
                SellerInvoiceForm.SetAllBorders(xlRange);

                xlVendorHistoryWorkbook.Save();
                xlVendorHistoryWorkbook.Close();

                String Message;
                if (VendorHistoryFileExists)
                {
                    Message = "Vendor History file is updated with Vendor Summary details";
                }
                else
                {
                    Message = "Vendor History file is updated with Vendor Summary details\n";
                    Message += "Vendor History files is created at " + VendorHistoryFile;
                }
                MessageBox.Show(this, Message, "Vendor History", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateProductPurchasesForm.UpdateVendorHistoryFile()", ex);
                throw;
            }
        }
    }
}
