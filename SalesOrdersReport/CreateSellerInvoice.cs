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
    public partial class CreateSellerInvoice : Form
    {
        Form MainForm = null;
        String MasterFilePath;

        public CreateSellerInvoice(Form ParentForm)
        {
            try
            {
                InitializeComponent();
                MainForm = ParentForm;
                MasterFilePath = MainForm.Controls["txtBoxFileName"].Text;
                txtBoxOutputFolder.Text = System.IO.Path.GetDirectoryName(MasterFilePath);

                progressBar1.Maximum = 100;
                progressBar1.Step = 1;
                progressBar1.Value = 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("AddNewOrderSheetForm", ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private Excel.Worksheet GetWorksheet(Excel.Workbook ObjWorkbook, String Sheetname)
        {
            try
            {
                for (int i = 1; i <= ObjWorkbook.Sheets.Count; i++)
                {
                    if (ObjWorkbook.Worksheets[i].Name.Equals(Sheetname, StringComparison.InvariantCultureIgnoreCase))
                        return ObjWorkbook.Worksheets[i];
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("GetWorksheet", ex);
            }
            return null;
        }

        private void btnCreateInvoice_Click(object sender, EventArgs e)
        {
            // Start the BackgroundWorker.
            backgroundWorker1.RunWorkerAsync();
            backgroundWorker1.WorkerReportsProgress = true;
        }

        private void btnBrowseOtherFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Multiselect = false;
                openFileDialog1.FileName = MasterFilePath;
                DialogResult dlgResult = openFileDialog1.ShowDialog();

                if (dlgResult == System.Windows.Forms.DialogResult.OK)
                {
                    txtBoxOtherFile.Text = openFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("BrowseOtherFile_Click", ex);
            }
        }

        private void radBtnOrderFromMasterFile_CheckedChanged(object sender, EventArgs e)
        {
            if (radBtnOrderFromMasterFile.Checked)
            {
                txtBoxOtherFile.Enabled = false;
                btnBrowseOtherFile.Enabled = false;
            }
        }

        private void radBtnOrderFromOtherFile_CheckedChanged(object sender, EventArgs e)
        {
            if (radBtnOrderFromOtherFile.Checked)
            {
                txtBoxOtherFile.Enabled = true;
                btnBrowseOtherFile.Enabled = true;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            btnCreateInvoice.Enabled = false;
            btnCancel.Enabled = false;
            Excel.Application xlApp = new Excel.Application();
            try
            {
                DataTable dtItemMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("ItemMaster", MasterFilePath, "*");
                DataTable dtSellerMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("SellerMaster", MasterFilePath, "*");
                List<String> ListVendors = dtItemMaster.AsEnumerable().Select(s => s.Field<String>("VendorName")).Distinct().ToList();

                dtItemMaster.Columns.Add("Quantity", Type.GetType("System.Double"));
                DataRow[] drItems = dtItemMaster.Select("", "SlNo asc");
                for (int i = 0; i < drItems.Length; i++)
                {
                    drItems[i]["Quantity"] = 0;
                }

                dtSellerMaster.Columns.Add("Quantity", Type.GetType("System.Double"));
                dtSellerMaster.Columns.Add("Total", Type.GetType("System.Double"));
                DataRow[] drSellers = dtSellerMaster.Select("", "SlNo asc");
                for (int i = 0; i < drSellers.Length; i++)
                {
                    drSellers[i]["Quantity"] = 0;
                    drSellers[i]["Total"] = 0;
                }

                String SelectedDateTimeString = dateTimeInvoice.Value.ToString("dd-MM-yyyy");

                String SalesOrderFile = "";
                if (radBtnOrderFromMasterFile.Checked)
                    SalesOrderFile = MasterFilePath;
                else
                    SalesOrderFile = txtBoxOtherFile.Text;

                Excel.Workbook xlSalesOrderWorkbook = xlApp.Workbooks.Open(SalesOrderFile);
                Excel.Worksheet xlSalesOrderWorksheet = GetWorksheet(xlSalesOrderWorkbook, SelectedDateTimeString);
                Int32 StartRow = 5, StartColumn = 1;

                #region Identify Items in SalesOrderSheet
                List<Int32> ListItemIndexes = new List<Int32>();
                Int32 ColumnCount = xlSalesOrderWorksheet.UsedRange.Columns.Count;
                for (int i = StartColumn + 4; i <= ColumnCount; i++)
                {
                    String ItemName = xlSalesOrderWorksheet.Cells[StartRow, i].Value;
                    Int32 ItemIndex = -1;
                    for (int j = 0; j < drItems.Length; j++)
                    {
                        if (drItems[j]["ItemName"].ToString().Equals(ItemName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            ItemIndex = j;
                            break;
                        }
                    }
                    ListItemIndexes.Add(ItemIndex);
                }
                #endregion

                #region Identify Sellers in SalesOrderSheet
                List<Int32> ListSellerIndexes = new List<Int32>();
                Int32 RowCount = xlSalesOrderWorksheet.UsedRange.Rows.Count;
                for (int i = StartRow + 1; i <= RowCount; i++)
                {
                    if (xlSalesOrderWorksheet.Cells[i, StartColumn + 1].Value == null) continue;
                    if (xlSalesOrderWorksheet.Cells[i, StartColumn + 2].Value == null) continue;

                    Excel.Range CountCell = xlSalesOrderWorksheet.Cells[i, StartColumn + 1];
                    Double CountItems = Double.Parse(CountCell.Value.ToString());
                    if (CountItems <= 1E-6)
                    {
                        ListSellerIndexes.Add(-1);
                        continue;
                    }

                    String SellerName = xlSalesOrderWorksheet.Cells[i, StartColumn + 2].Value;
                    Int32 SellerIndex = -1;
                    for (int j = 0; j < drItems.Length; j++)
                    {
                        if (drSellers[j]["SellerName"].ToString().Equals(SellerName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            SellerIndex = j;
                            break;
                        }
                    }
                    ListSellerIndexes.Add(SellerIndex);
                }
                #endregion

                #region Print Invoice Sheet for each Seller
                Double Quantity, Total, TotalQuantity;
                //Excel.Workbook xlWorkbook = xlApp.Workbooks.Add();

                xlSalesOrderWorksheet.Copy();
                Excel.Workbook xlWorkbook = xlApp.Workbooks[2];
                xlSalesOrderWorksheet = xlWorkbook.Sheets[1];
                xlSalesOrderWorkbook.Close();

                Int32 InvoiceHeaderStartRow = 0; //CommonFunctions.InvoiceRowsFromTop;
                Int32 InvoiceStartRow = InvoiceHeaderStartRow + 3;
                Int32 InvoiceNumber = Int32.Parse(txtBoxInvoiceStartNumber.Text);
                Int32 ValidSellerCount = ListSellerIndexes.Where(s => (s >= 0)).ToList().Count;
                Int32 ValidItemCount = ListItemIndexes.Where(s => (s >= 0)).ToList().Count;
                Int32 ProgressBarCount = (ValidSellerCount * ValidItemCount + drSellers.Length + drItems.Length + 2);
                Int32 Counter = 0;
                for (int i = 0; i < ListSellerIndexes.Count; i++)
                {
                    if (ListSellerIndexes[i] < 0) continue;
                    Counter++;
                    backgroundWorker1.ReportProgress((Counter * 100) / ProgressBarCount);
                    Excel.Worksheet xlWorkSheet = xlWorkbook.Worksheets.Add(Type.Missing, xlWorkbook.Sheets[xlWorkbook.Sheets.Count]);
                    String SheetName = drSellers[ListSellerIndexes[i]]["SellerName"].ToString().Replace(":", "").
                                            Replace("\\", "").Replace("/", "").
                                            Replace("?", "").Replace("*", "").
                                            Replace("[", "").Replace("]", "");
                    xlWorkSheet.Name = ((SheetName.Length > 30) ? SheetName.Substring(0, 30) : SheetName);

                    #region Print Invoice Items
                    Int32 SlNo = 0;
                    Total = 0; TotalQuantity = 0;

                    #region Print Invoice Header
                    Excel.Range xlRange = xlWorkSheet.Cells[1 + InvoiceHeaderStartRow, 1];
                    //xlRange.Value = CommonFunctions.InvoiceTitle;
                    //xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[1 + InvoiceHeaderStartRow, 1], xlWorkSheet.Cells[2 + InvoiceHeaderStartRow, 5]];
                    //xlRange.Font.Bold = true;
                    //xlRange.Merge();
                    //xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                    Int32 CustDetailsStartRow = 1 + InvoiceHeaderStartRow;
                    xlRange = xlWorkSheet.Cells[CustDetailsStartRow, 1];
                    xlRange.Value = "Customer Name";
                    xlRange.WrapText = true;
                    xlRange.Font.Bold = true;
                    xlWorkSheet.Cells[CustDetailsStartRow, 2].Value = drSellers[ListSellerIndexes[i]]["SellerName"].ToString();

                    xlRange = xlWorkSheet.Cells[CustDetailsStartRow + 1, 1];
                    xlRange.Value = "Phone";
                    xlRange.Font.Bold = true;
                    xlWorkSheet.Cells[1 + CustDetailsStartRow, 2].Value = drSellers[ListSellerIndexes[i]]["Phone"].ToString();

                    xlRange = xlWorkSheet.Cells[CustDetailsStartRow, 4];
                    xlRange.Value = "Date";
                    xlRange.Font.Bold = true;
                    xlWorkSheet.Cells[CustDetailsStartRow, 5].Value = DateTime.Today.ToString("dd-MMM-yyyy");

                    xlRange = xlWorkSheet.Cells[1 + CustDetailsStartRow, 4];
                    xlRange.Value = "Invoice#";
                    xlRange.Font.Bold = true;
                    xlWorkSheet.Cells[1 + CustDetailsStartRow, 5].Value = InvoiceNumber;
                    InvoiceNumber++;

                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[CustDetailsStartRow, 1], xlWorkSheet.Cells[1 + CustDetailsStartRow, 5]];
                    xlRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic);
                    xlRange.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlRange.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
                    #endregion

                    xlWorkSheet.Cells[InvoiceStartRow + 1, 1].Value = "Sl.No.";
                    xlWorkSheet.Cells[InvoiceStartRow + 1, 2].Value = "Item Name";
                    xlWorkSheet.Cells[InvoiceStartRow + 1, 3].Value = "Quantity";
                    xlWorkSheet.Cells[InvoiceStartRow + 1, 4].Value = "Price";
                    xlWorkSheet.Cells[InvoiceStartRow + 1, 5].Value = "Total";
                    xlWorkSheet.Range[xlWorkSheet.Cells[InvoiceStartRow + 1, 1], xlWorkSheet.Cells[InvoiceStartRow + 1, 5]].Font.Bold = true;

                    for (int j = 0; j < ListItemIndexes.Count; j++)
                    {
                        if (ListItemIndexes[j] < 0) continue;
                        Counter++;
                        backgroundWorker1.ReportProgress((Counter * 100) / ProgressBarCount);
                        if (xlSalesOrderWorksheet.Cells[StartRow + 1 + i, StartColumn + 4 + j].Value == null) continue;

                        Quantity = Double.Parse(xlSalesOrderWorksheet.Cells[StartRow + 1 + i, StartColumn + 4 + j].Value.ToString());
                        drItems[ListItemIndexes[j]]["Quantity"] = Double.Parse(drItems[ListItemIndexes[j]]["Quantity"].ToString()) + Quantity;

                        SlNo++;
                        xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, 1].Value = SlNo;
                        xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, 2].Value = drItems[ListItemIndexes[j]]["ItemName"].ToString();
                        xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, 3].Value = Quantity;
                        xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, 4].Value = drItems[ListItemIndexes[j]]["SellingPrice"].ToString();
                        xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, 4].NumberFormat = "#,##0.00";
                        xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, 5].Value = Double.Parse(drItems[ListItemIndexes[j]]["SellingPrice"].ToString()) * Quantity;
                        xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, 5].NumberFormat = "#,##0.00";

                        TotalQuantity += Quantity;
                        Total += Double.Parse(xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, 5].Value.ToString());
                    }
                    drSellers[ListSellerIndexes[i]]["Total"] = Total;
                    drSellers[ListSellerIndexes[i]]["Quantity"] = TotalQuantity;

                    xlRange = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 2, 4];
                    xlRange.Value = "Total";
                    xlRange.Font.Bold = true;

                    xlRange = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 2, 5];
                    xlRange.Value = Total;
                    xlRange.Font.Bold = true;
                    xlRange.NumberFormat = "#,##0.00";

                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[SlNo + InvoiceStartRow + 2, 1], xlWorkSheet.Cells[SlNo + InvoiceStartRow + 2, 4]];
                    xlRange.Font.Bold = true;
                    xlRange.Merge();
                    xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[InvoiceStartRow + 1, 1], xlWorkSheet.Cells[SlNo + InvoiceStartRow + 2, 5]];
                    xlRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic);
                    xlRange.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                    xlRange.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
                    #endregion

                    xlWorkSheet.UsedRange.Columns.AutoFit();
                    AddPageHeaderAndFooter(ref xlWorkSheet, "Invoice");
                }
                #endregion

                if (chkBoxCreateSummary.Checked)
                {
                    #region Print Seller Summary Sheet
                    Int32 SummaryStartRow = 0;
                    Excel.Worksheet xlSellerSummaryWorkSheet = xlWorkbook.Worksheets.Add(xlWorkbook.Sheets[1]);
                    xlSellerSummaryWorkSheet.Name = "Seller Summary";
                    xlSellerSummaryWorkSheet.Cells[SummaryStartRow + 1, 1].Value = "Sl.No.";
                    xlSellerSummaryWorkSheet.Cells[SummaryStartRow + 1, 2].Value = "Seller Name";
                    xlSellerSummaryWorkSheet.Cells[SummaryStartRow + 1, 3].Value = "Phone";
                    xlSellerSummaryWorkSheet.Cells[SummaryStartRow + 1, 4].Value = "Total Quantity";
                    xlSellerSummaryWorkSheet.Cells[SummaryStartRow + 1, 5].Value = "Total Amount";
                    Excel.Range xlRange1 = xlSellerSummaryWorkSheet.Range[xlSellerSummaryWorkSheet.Cells[SummaryStartRow + 1, 1], xlSellerSummaryWorkSheet.Cells[SummaryStartRow + 1, 5]];
                    xlRange1.Font.Bold = true;

                    for (int i = 0; i < drSellers.Length; i++)
                    {
                        backgroundWorker1.ReportProgress(((i + ValidSellerCount * ValidItemCount) * 100) / ProgressBarCount);
                        xlSellerSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 1].Value = drSellers[i]["SlNo"].ToString();
                        xlSellerSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 2].Value = drSellers[i]["SellerName"].ToString();
                        xlSellerSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 3].Value = drSellers[i]["Phone"].ToString();
                        xlSellerSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 4].Value = drSellers[i]["Quantity"].ToString();
                        xlSellerSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 5].Value = drSellers[i]["Total"].ToString();
                        xlSellerSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 5].NumberFormat = "#,##0.00";
                    }
                    xlSellerSummaryWorkSheet.UsedRange.Columns.AutoFit();
                    AddPageHeaderAndFooter(ref xlSellerSummaryWorkSheet, "Sellerwise Summary");
                    #endregion

                    #region Print Item Summary Sheet
                    Total = 0;
                    Excel.Worksheet xlSummaryWorkSheet = xlWorkbook.Worksheets.Add(xlWorkbook.Sheets[1]);
                    xlSummaryWorkSheet.Name = "Item Summary";
                    xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 1].Value = "Sl.No.";
                    xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 2].Value = "Item Name";
                    xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 3].Value = "Vendor Name";
                    xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 4].Value = "Quantity";
                    xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 5].Value = "Price";
                    xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 6].Value = "Total";
                    xlRange1 = xlSummaryWorkSheet.Range[xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 1], xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 6]];
                    xlRange1.Font.Bold = true;

                    for (int i = 0; i < drItems.Length; i++)
                    {
                        backgroundWorker1.ReportProgress(((i + ValidSellerCount * ValidItemCount + drSellers.Length) * 100) / ProgressBarCount);
                        xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 1].Value = drItems[i]["SlNo"].ToString();
                        xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 2].Value = drItems[i]["ItemName"].ToString();
                        xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 3].Value = drItems[i]["VendorName"].ToString();
                        xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 4].Value = drItems[i]["Quantity"].ToString();
                        xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 5].Value = drItems[i]["PurchasePrice"].ToString();
                        xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 5].NumberFormat = "#,##0.00";
                        xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 6].Value = Double.Parse(drItems[i]["Quantity"].ToString()) * Double.Parse(drItems[i]["PurchasePrice"].ToString());
                        xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 6].NumberFormat = "#,##0.00";
                        Total += Double.Parse(xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 6].Value.ToString());
                    }

                    Excel.Range tmpxlRange = xlSummaryWorkSheet.Cells[drItems.Length + SummaryStartRow + 2, 5];
                    tmpxlRange.Value = "Total";
                    tmpxlRange.Font.Bold = true;

                    tmpxlRange = xlSummaryWorkSheet.Cells[drItems.Length + SummaryStartRow + 2, 6];
                    tmpxlRange.Value = Total;
                    tmpxlRange.Font.Bold = true;
                    tmpxlRange.NumberFormat = "#,##0.00";
                    xlSummaryWorkSheet.UsedRange.Columns.AutoFit();
                    xlApp.DisplayAlerts = false;
                    AddPageHeaderAndFooter(ref xlSummaryWorkSheet, "Itemwise Summary");
                    xlApp.DisplayAlerts = true;
                    #endregion
                }

                xlApp.DisplayAlerts = false;
                xlWorkbook.Sheets[SelectedDateTimeString].Delete();
                xlApp.DisplayAlerts = true;

                backgroundWorker1.ReportProgress(((ProgressBarCount - 1) * 100) / ProgressBarCount);
                xlWorkbook.SaveAs(txtBoxOutputFolder.Text + "\\Invoice_" + SelectedDateTimeString + ".xlsx");
                xlWorkbook.Close();
                backgroundWorker1.ReportProgress(100);

                btnCancel.Enabled = true;
                btnCreateInvoice.Enabled = true;
                CommonFunctions.ReleaseCOMObject(xlWorkbook);
                MessageBox.Show("Invoice Generated Sucessfully", "Status", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateInvoice_Click", ex);
            }
            finally
            {
                xlApp.Quit();
                CommonFunctions.ReleaseCOMObject(xlApp);
            }
        }

        private void AddPageHeaderAndFooter(ref Excel.Worksheet xlWorksheet, String PageHeaderTitle)
        {
            try
            {
                xlWorksheet.PageSetup.RightHeaderPicture.Filename = AppDomain.CurrentDomain.BaseDirectory + "\\Images\\LAPLogo.jpg";
                xlWorksheet.PageSetup.RightHeaderPicture.ColorType = Microsoft.Office.Core.MsoPictureColorType.msoPictureAutomatic;
                xlWorksheet.PageSetup.RightHeaderPicture.CropBottom = 0;
                xlWorksheet.PageSetup.RightHeaderPicture.CropLeft = 0;
                xlWorksheet.PageSetup.RightHeaderPicture.CropRight = 0;
                xlWorksheet.PageSetup.RightHeaderPicture.CropTop = 0;
                xlWorksheet.PageSetup.RightHeaderPicture.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;
                xlWorksheet.PageSetup.RightHeaderPicture.Height = 50;
                //xlWorksheet.PageSetup.RightHeaderPicture.Width = 30;
                //xlWorksheet.PageSetup.Application.PrintCommunication = false;
                //xlWorksheet.PageSetup.PrintArea = "";
                /*xlWorksheet.PageSetup.PrintTitleRows = "";
                xlWorksheet.PageSetup.PrintTitleColumns = "";

                xlWorksheet.PageSetup.Application.PrintCommunication = true;
                xlWorksheet.PageSetup.PrintArea = "";
                xlWorksheet.PageSetup.Application.PrintCommunication = false;*/

                xlWorksheet.PageSetup.LeftHeader = "";
                xlWorksheet.PageSetup.CenterHeader = "\n&\"Gill Sans MT,Bold\"&18&K088DA5Kerala Vegetables\n&\"Gill Sans MT,Regular\"&16&K000000" + PageHeaderTitle;
                xlWorksheet.PageSetup.RightHeader = "&G";
                xlWorksheet.PageSetup.CenterFooter = "&\"Gill Sans MT,Bold\"&16&KF5A158Lap Business Group"
                        + "\n&\"Gill Sans MT,Italic\"&14&K088DA5No.8, 2nd Main Road, Ganganagar Extn., R.T.Nagar, Bangalore-560032"
                        + "\nCell : +91 7044037919 Email : lapbusinessgroup@gmail.com";
                if (xlWorksheet.PageSetup.Pages.Count > 1)
                    xlWorksheet.PageSetup.RightFooter = "&P";
                xlWorksheet.PageSetup.PrintGridlines = true;
                xlWorksheet.PageSetup.CenterHorizontally = true;
                xlWorksheet.PageSetup.TopMargin = xlWorksheet.PageSetup.Application.InchesToPoints(1.5);
                xlWorksheet.PageSetup.BottomMargin = xlWorksheet.PageSetup.Application.InchesToPoints(1.5);
                xlWorksheet.PageSetup.FooterMargin = xlWorksheet.PageSetup.Application.InchesToPoints(0.5);
                xlWorksheet.PageSetup.HeaderMargin = xlWorksheet.PageSetup.Application.InchesToPoints(0.5);
                xlWorksheet.PageSetup.LeftMargin = xlWorksheet.PageSetup.Application.InchesToPoints(0.7);
                xlWorksheet.PageSetup.RightMargin = xlWorksheet.PageSetup.Application.InchesToPoints(0.7);

                /*xlWorksheet.PageSetup.RightHeader = "";
                xlWorksheet.PageSetup.LeftFooter = "";
                xlWorksheet.PageSetup.CenterFooter = "";
                xlWorksheet.PageSetup.RightFooter = "";
                xlWorksheet.PageSetup.LeftMargin = xlWorksheet.PageSetup.Application.InchesToPoints(0.7);
                xlWorksheet.PageSetup.RightMargin = xlWorksheet.PageSetup.Application.InchesToPoints(0.7);
                xlWorksheet.PageSetup.TopMargin = xlWorksheet.PageSetup.Application.InchesToPoints(0.75);
                xlWorksheet.PageSetup.BottomMargin = xlWorksheet.PageSetup.Application.InchesToPoints(0.75);
                xlWorksheet.PageSetup.HeaderMargin = xlWorksheet.PageSetup.Application.InchesToPoints(0.3);
                xlWorksheet.PageSetup.FooterMargin = xlWorksheet.PageSetup.Application.InchesToPoints(0.3);
                xlWorksheet.PageSetup.PrintHeadings = false;
                xlWorksheet.PageSetup.PrintGridlines = false;
                xlWorksheet.PageSetup.PrintComments = Excel.XlPrintLocation.xlPrintNoComments;
                xlWorksheet.PageSetup.PrintQuality = 600;
                xlWorksheet.PageSetup.CenterHorizontally = false;
                xlWorksheet.PageSetup.CenterVertically = false;

                xlWorksheet.PageSetup.Orientation = Excel.XlPageOrientation.xlPortrait;
                xlWorksheet.PageSetup.Draft = false;
                xlWorksheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperLetter;
                xlWorksheet.PageSetup.FirstPageNumber = 1;
                xlWorksheet.PageSetup.Order = Excel.XlOrder.xlDownThenOver;
                xlWorksheet.PageSetup.BlackAndWhite = false;
                xlWorksheet.PageSetup.Zoom = 100;
                xlWorksheet.PageSetup.PrintErrors = Excel.XlPrintErrors.xlPrintErrorsDisplayed;
                xlWorksheet.PageSetup.OddAndEvenPagesHeaderFooter = false;
                xlWorksheet.PageSetup.DifferentFirstPageHeaderFooter = false;
                xlWorksheet.PageSetup.ScaleWithDocHeaderFooter = true;
                xlWorksheet.PageSetup.AlignMarginsHeaderFooter = true;

                /*xlWorksheet.PageSetup.EvenPage.LeftHeader.Text = "";
                xlWorksheet.PageSetup.EvenPage.CenterHeader.Text = "";
                xlWorksheet.PageSetup.EvenPage.RightHeader.Text = "";
                xlWorksheet.PageSetup.EvenPage.LeftFooter.Text = "";
                xlWorksheet.PageSetup.EvenPage.CenterFooter.Text = "";
                xlWorksheet.PageSetup.EvenPage.RightFooter.Text = "";

                xlWorksheet.PageSetup.FirstPage.LeftHeader.Text = "";
                xlWorksheet.PageSetup.FirstPage.CenterHeader.Text = "";
                xlWorksheet.PageSetup.FirstPage.RightHeader.Text = "";
                xlWorksheet.PageSetup.FirstPage.LeftFooter.Text = "";
                xlWorksheet.PageSetup.FirstPage.CenterFooter.Text = "";
                xlWorksheet.PageSetup.FirstPage.RightFooter.Text = "";
                */
                //xlWorksheet.PageSetup.Application.PrintCommunication = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            progressBar1.Value = e.ProgressPercentage;

            // Set the text.
            lblProgress.Text = e.ProgressPercentage.ToString() + "%";
        }
    }
}
