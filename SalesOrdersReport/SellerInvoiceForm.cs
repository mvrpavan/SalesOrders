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
    enum ReportType
    {
        INVOICE, QUOTATION, PURCHASEORDER
    }

    public partial class SellerInvoiceForm : Form
    {
        public String MasterFilePath;
        Excel.Application xlApp;
        Boolean SummaryPrinted = false;
        CheckState PrevAllLinesCheckState = CheckState.Indeterminate;

        public SellerInvoiceForm()
        {
            try
            {
                InitializeComponent();
                MasterFilePath = CommonFunctions.MasterFilePath;
                txtBoxOutputFolder.Text = System.IO.Path.GetDirectoryName(MasterFilePath);

                CommonFunctions.ResetProgressBar();

                lblStatus.Text = "";
                CommonFunctions.ListSelectedSellers.Clear();

                dateTimeInvoice.Value = DateTime.Now;
                txtBoxOtherFile.Text = txtBoxOutputFolder.Text + @"\SalesOrder_" + dateTimeInvoice.Value.ToString("dd-MM-yyyy") + ".xlsx";

                FillLineFromOrderMaster();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerInvoiceForm.ctor", ex);
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

        private void FillLineFromOrderMaster()
        {
            try
            {
                chkListBoxLine.Items.Clear();
                for (int i = 0; i < CommonFunctions.ListSellerLines.Count; i++)
                {
                    chkListBoxLine.Items.Add(CommonFunctions.ListSellerLines[i], true);
                }

                PrevAllLinesCheckState = CheckState.Checked;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("FillLineFromOrderMaster", ex);
            }
        }

        public void UpdateSelectedSellersList()
        {
            try
            {
                listBoxSellers.Items.Clear();
                for (int i = 0; i < CommonFunctions.ListSelectedSellers.Count; i++)
                {
                    listBoxSellers.Items.Add(CommonFunctions.ListSelectedSellers[i]);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UpdateSelectedSellersList", ex);
            }
        }

        private void btnCreateInvoice_Click(object sender, EventArgs e)
        {
            //backgroundWorker1_DoWork(null, null);
            //return;
            // Start the BackgroundWorker.
            backgroundWorker1.RunWorkerAsync();
            backgroundWorker1.WorkerReportsProgress = true;
        }

        private void btnBrowseOtherFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Multiselect = false;
                openFileDialog1.FileName = "";
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            btnCreateInvoice.Enabled = false;
            btnCancel.Enabled = false;
            xlApp = new Excel.Application();
            SummaryPrinted = false;
            try
            {
                DataTable dtItemMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("ItemMaster", MasterFilePath, "*");
                DataTable dtSellerMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("SellerMaster", MasterFilePath, "*");
                String[] SelectedLine = new String[chkListBoxLine.CheckedItems.Count];
                chkListBoxLine.CheckedItems.CopyTo(SelectedLine, 0);

                if (SelectedLine.Length == 0 && CommonFunctions.ListSelectedSellers.Count == 0)
                {
                    btnCancel.Enabled = true;
                    btnCreateInvoice.Enabled = true;
                    MessageBox.Show(this, "No Line/Sellers are selected\nUnable to create Invoice/Quoation", "Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblStatus.Text = "Select any Line/Sellers";
                    return;
                }

                dtItemMaster.Columns.Add("Quantity", Type.GetType("System.Double"));
                DataRow[] drItems = dtItemMaster.Select("", "SlNo asc");
                for (int i = 0; i < drItems.Length; i++)
                {
                    drItems[i]["Quantity"] = 0;
                }

                dtSellerMaster.Columns.Add("Quantity", Type.GetType("System.String"));
                dtSellerMaster.Columns.Add("Total", Type.GetType("System.String"));
                dtSellerMaster.Columns.Add("InvoiceNumber", Type.GetType("System.Int32"));
                dtSellerMaster.Columns.Add("TotalDiscount", Type.GetType("System.String"));
                dtSellerMaster.Columns.Add("TotalTax", Type.GetType("System.String"));
                DataRow[] drSellers = dtSellerMaster.Select("", "SlNo asc");

                String SelectedDateTimeString = dateTimeInvoice.Value.ToString("dd-MM-yyyy");

                String SalesOrderFile = txtBoxOtherFile.Text;

                Excel.Workbook xlSalesOrderWorkbook = xlApp.Workbooks.Open(SalesOrderFile);
                Excel.Worksheet xlSalesOrderWorksheet = CommonFunctions.GetWorksheet(xlSalesOrderWorkbook, SelectedDateTimeString);
                Int32 StartRow = 5, StartColumn = 1, DetailsCount = 5;

                #region Identify Items in SalesOrderSheet
                List<Int32> ListItemIndexes = new List<Int32>();
                Int32 ColumnCount = xlSalesOrderWorksheet.UsedRange.Columns.Count;
                for (int i = StartColumn + DetailsCount; i <= ColumnCount; i++)
                {
                    String ItemName = xlSalesOrderWorksheet.Cells[StartRow, i].Value;
                    Int32 ItemIndex = -1;
                    for (int j = 0; j < drItems.Length; j++)
                    {
                        if (drItems[j]["ItemName"].ToString().Trim().Equals(ItemName.Trim(), StringComparison.InvariantCultureIgnoreCase))
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
                Int32 RowCount = xlSalesOrderWorksheet.UsedRange.Rows.Count + 1;
                for (int i = StartRow + 1; i <= RowCount; i++)
                {
                    if (xlSalesOrderWorksheet.Cells[i, StartColumn + 1].Value == null) continue;
                    if (xlSalesOrderWorksheet.Cells[i, StartColumn + 2].Value == null) continue;
                    String SellerName = xlSalesOrderWorksheet.Cells[i, StartColumn + 2].Value;
                    Int32 SellerIndex = -1;
                    for (int j = 0; j < drSellers.Length; j++)
                    {
                        if (drSellers[j]["SellerName"].ToString().Trim().Equals(SellerName.Trim(), StringComparison.InvariantCultureIgnoreCase))
                        {
                            SellerIndex = j;
                            break;
                        }
                    }

                    if (SellerIndex < 0) continue;

                    String Line = drSellers[SellerIndex]["Line"].ToString().Replace("<", "").Replace(">", "").ToUpper();
                    if (Line.Trim().Length == 0) Line = "<Blanks>";

                    //if (!SelectedLine.Contains(Line) && !CommonFunctions.ListSelectedSellers.Contains(SellerName))
                    //{
                    //    ListSellerIndexes.Add(-1);
                    //    continue;
                    //}

                    Excel.Range CountCell = xlSalesOrderWorksheet.Cells[i, StartColumn + 1];
                    Double CountItems = Double.Parse(CountCell.Value.ToString());
                    if (CountItems <= 1E-6)
                    {
                        if (SelectedLine.Contains(Line) || CommonFunctions.ListSelectedSellers.Contains(SellerName))
                        {
                            drSellers[SellerIndex]["InvoiceNumber"] = Int32.MinValue;
                        }
                        ListSellerIndexes.Add(-1);
                        continue;
                    }

                    ListSellerIndexes.Add(SellerIndex);
                }
                #endregion

                Excel.Workbook xlWorkbook = null;
                if (chkBoxCreateInvoice.Checked)
                {
                    xlSalesOrderWorksheet.Copy();
                    xlWorkbook = xlApp.Workbooks[2];

                    CreateSellerReport(ReportType.INVOICE, drItems, drSellers, SelectedDateTimeString, 
                        StartRow, StartColumn, ListItemIndexes, ListSellerIndexes, xlWorkbook);
                }

                if (chkBoxCreateQuotation.Checked)
                {
                    xlSalesOrderWorksheet.Copy();
                    xlWorkbook = xlApp.Workbooks[2];

                    CreateSellerReport(ReportType.QUOTATION, drItems, drSellers, SelectedDateTimeString,
                        StartRow, StartColumn, ListItemIndexes, ListSellerIndexes, xlWorkbook);
                }
                xlSalesOrderWorkbook.Close();

                btnCancel.Enabled = true;
                btnCreateInvoice.Enabled = true;
                MessageBox.Show(this, "Invoice/Quotation generated successfully", "Status", MessageBoxButtons.OK);
                lblStatus.Text = "Click \"Close Window\" to close this window";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerInvoiceForm.backgroundWorker1_DoWork()", ex);
            }
            finally
            {
                xlApp.Quit();
                CommonFunctions.ReleaseCOMObject(xlApp);
            }
        }

        private void CreateSellerReportOld(ReportType EnumReportType, DataRow[] drItems, DataRow[] drSellers, String SelectedDateTimeString, Int32 StartRow, Int32 StartColumn, List<Int32> ListItemIndexes, List<Int32> ListSellerIndexes, Excel.Workbook xlWorkbook)
        {
            try
            {
                Boolean PrintVATPercent = false, PrintOldBalance = false, CreateSummary = false;
                ReportSettings CurrReportSettings = null;
                String ReportTypeName = "", BillNumberText = "", SaveFileName = "";
                switch (EnumReportType)
                {
                    case ReportType.INVOICE:
                        CurrReportSettings = CommonFunctions.ObjInvoiceSettings;
                        ReportTypeName = "Invoice";
                        PrintVATPercent = true;
                        BillNumberText = "Invoice#";
                        SaveFileName = txtBoxOutputFolder.Text + "\\Invoice_" + SelectedDateTimeString + ".xlsx";
                        if (CommonFunctions.ObjGeneralSettings.SummaryLocation == 0) CreateSummary = true;
                        break;
                    case ReportType.QUOTATION:
                        CurrReportSettings = CommonFunctions.ObjQuotationSettings;
                        ReportTypeName = "Quotation";
                        PrintOldBalance = true;
                        BillNumberText = "Quotation#";
                        SaveFileName = txtBoxOutputFolder.Text + "\\Quotation_" + SelectedDateTimeString + ".xlsx";
                        if (CommonFunctions.ObjGeneralSettings.SummaryLocation == 1) CreateSummary = true;
                        break;
                    default:
                        return;
                }
                Excel.Worksheet xlSalesOrderWorksheet = xlWorkbook.Sheets[1];

                #region Print Invoice Sheet for each Seller
                Double Quantity;//, Total;//, TotalQuantity;

                Int32 InvoiceHeaderStartRow = 0; //CommonFunctions.ReportRowsFromTop;
                Int32 InvoiceStartRow = InvoiceHeaderStartRow + 5;// ((EnumReportType == ReportType.INVOICE) ? InvoiceHeaderStartRow + 5 : InvoiceHeaderStartRow + 4);
                Int32 InvoiceNumber = CurrReportSettings.LastNumber;
                Int32 ValidSellerCount = ListSellerIndexes.Where(s => (s >= 0)).ToList().Count;
                Int32 ValidItemCount = ListItemIndexes.Where(s => (s >= 0)).ToList().Count;
                Int32 ProgressBarCount = (ValidSellerCount * ValidItemCount);
                Int32 Counter = 0;
                Int32 SlNoColNum = 1, ItemNameColNum = 2, OrdQtyColNum = 3, SalQtyColNum = 4, PriceColNum = 5, TotalColNum = 6;
                Int32 ReportAppendRowsAtBottom = CommonFunctions.ObjApplicationSettings.ReportAppendRowsAtBottom;
                Int32 SalesTotalRowOffset = 1 + ReportAppendRowsAtBottom, DiscountRowOffset = 2 + ReportAppendRowsAtBottom, OldBalanceRowOffset = 3 + ReportAppendRowsAtBottom, TotalCostRowOffset = 4 + ReportAppendRowsAtBottom;
                Int32 SellerCount = 0, SalesOrderDetailsCount = 5;
                for (int i = 0; i < ListSellerIndexes.Count; i++)
                {
                    if (ListSellerIndexes[i] < 0) continue;
                    SellerCount++;
                    lblStatus.Text = "Creating " + ReportTypeName + " for Seller " + SellerCount + " of " + ValidSellerCount;
                    Excel.Worksheet xlWorkSheet = xlWorkbook.Worksheets.Add(Type.Missing, xlWorkbook.Sheets[xlWorkbook.Sheets.Count]);
                    String SheetName = drSellers[ListSellerIndexes[i]]["SellerName"].ToString().Replace(":", "").
                                            Replace("\\", "").Replace("/", "").
                                            Replace("?", "").Replace("*", "").
                                            Replace("[", "").Replace("]", "");
                    xlWorkSheet.Name = ((SheetName.Length > 30) ? SheetName.Substring(0, 30) : SheetName);

                    SellerDetails ObjCurrentSeller = CommonFunctions.ObjSellerMaster.GetSellerDetails(drSellers[ListSellerIndexes[i]]["SellerName"].ToString());

                    #region Print Invoice Items
                    Int32 SlNo = 0;

                    #region Print Invoice Header
                    Excel.Range xlRange = xlWorkSheet.Cells[1 + InvoiceHeaderStartRow, 1];

                    Int32 CustDetailsStartRow = 1 + InvoiceHeaderStartRow;
                    xlRange = xlWorkSheet.Cells[CustDetailsStartRow, SlNoColNum];
                    xlRange.Value = "Name";
                    xlRange.WrapText = true;
                    xlRange.Font.Bold = true;
                    xlWorkSheet.Cells[CustDetailsStartRow, SlNoColNum + 1].Value = drSellers[ListSellerIndexes[i]]["SellerName"].ToString();

                    xlRange = xlWorkSheet.Cells[CustDetailsStartRow + 1, SlNoColNum];
                    xlRange.Value = "Address";
                    xlRange.WrapText = true;
                    xlRange.Font.Bold = true;
                    xlWorkSheet.Cells[CustDetailsStartRow + 1, SlNoColNum + 1].Value = drSellers[ListSellerIndexes[i]]["Address"].ToString();
                    xlRange = xlWorkSheet.Cells[CustDetailsStartRow + 1, SlNoColNum + 1];
                    xlRange.WrapText = true;
                    if (drSellers[ListSellerIndexes[i]]["Address"].ToString().Length >= 25) xlRange.EntireColumn.ColumnWidth = 25;

                    if (EnumReportType == ReportType.INVOICE)
                    {
                        xlRange = xlWorkSheet.Cells[CustDetailsStartRow + 2, SlNoColNum];
                        xlRange.Value = "TIN#";
                        xlRange.WrapText = true;
                        xlRange.Font.Bold = true;
                        xlWorkSheet.Cells[CustDetailsStartRow + 2, SlNoColNum + 1].Value = drSellers[ListSellerIndexes[i]]["TINNumber"].ToString();

                        xlRange = xlWorkSheet.Cells[CustDetailsStartRow + 3, SlNoColNum];
                        xlRange.Value = "Phone";
                        xlRange.Font.Bold = true;
                        xlWorkSheet.Cells[CustDetailsStartRow + 3, SlNoColNum + 1].Value = drSellers[ListSellerIndexes[i]]["Phone"].ToString();
                    }
                    else
                    {
                        xlRange = xlWorkSheet.Cells[CustDetailsStartRow + 2, SlNoColNum];
                        xlRange.Value = "Phone";
                        xlRange.Font.Bold = true;
                        xlWorkSheet.Cells[CustDetailsStartRow + 2, SlNoColNum + 1].Value = drSellers[ListSellerIndexes[i]]["Phone"].ToString();
                    }

                    xlRange = xlWorkSheet.Cells[CustDetailsStartRow, TotalColNum - 1];
                    xlRange.Value = "Date";
                    xlRange.Font.Bold = true;
                    xlWorkSheet.Cells[CustDetailsStartRow, TotalColNum].Value = DateTime.Today.ToString("dd-MMM-yyyy");

                    InvoiceNumber++;
                    xlRange = xlWorkSheet.Cells[1 + CustDetailsStartRow, TotalColNum - 1];
                    xlRange.Value = BillNumberText;
                    xlRange.Font.Bold = true;
                    xlWorkSheet.Cells[CustDetailsStartRow + 1, TotalColNum].Value = InvoiceNumber;
                    drSellers[ListSellerIndexes[i]]["InvoiceNumber"] = InvoiceNumber;

                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[CustDetailsStartRow, 1], xlWorkSheet.Cells[CustDetailsStartRow + 3, TotalColNum]];
                    SetAllBorders(xlRange);
                    #endregion

                    xlWorkSheet.Cells[InvoiceStartRow + 1, SlNoColNum].Value = "Sl.No.";
                    xlWorkSheet.Cells[InvoiceStartRow + 1, ItemNameColNum].Value = "Item Name";
                    xlWorkSheet.Cells[InvoiceStartRow + 1, OrdQtyColNum].Value = "Order Quantity";
                    xlWorkSheet.Cells[InvoiceStartRow + 1, SalQtyColNum].Value = "Sales Quantity";
                    xlWorkSheet.Cells[InvoiceStartRow + 1, PriceColNum].Value = "Price";
                    xlWorkSheet.Cells[InvoiceStartRow + 1, TotalColNum].Value = "Total";
                    xlWorkSheet.Range[xlWorkSheet.Cells[InvoiceStartRow + 1, SlNoColNum], xlWorkSheet.Cells[InvoiceStartRow + 1, TotalColNum]].Font.Bold = true;

                    for (int j = 0; j < ListItemIndexes.Count; j++)
                    {
                        if (ListItemIndexes[j] < 0) continue;
                        Counter++;
                        backgroundWorker1.ReportProgress((Counter * 100) / ProgressBarCount);
                        if (xlSalesOrderWorksheet.Cells[StartRow + 1 + i, StartColumn + SalesOrderDetailsCount + j].Value == null) continue;

                        Quantity = Double.Parse(xlSalesOrderWorksheet.Cells[StartRow + 1 + i, StartColumn + SalesOrderDetailsCount + j].Value.ToString());
                        drItems[ListItemIndexes[j]]["Quantity"] = Double.Parse(drItems[ListItemIndexes[j]]["Quantity"].ToString()) + Quantity;

                        SlNo++;
                        xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, SlNoColNum].Value = SlNo;
                        xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, ItemNameColNum].Value = drItems[ListItemIndexes[j]]["ItemName"].ToString();
                        xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, OrdQtyColNum].Value = Quantity;
                        if (chkBoxUseOrdQty.Checked == true) xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, SalQtyColNum].Value = Quantity;
                        xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, PriceColNum].Value = CommonFunctions.ObjProductMaster.GetPriceForProduct(drItems[ListItemIndexes[j]]["ItemName"].ToString(), ObjCurrentSeller.PriceGroupIndex);
                        xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, PriceColNum].NumberFormat = "#,##0.00";
                        Excel.Range xlRangeSaleQty = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, SalQtyColNum];
                        Excel.Range xlRangePrice = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, PriceColNum];
                        Excel.Range xlRangeTotal = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, TotalColNum];
                        xlRangeTotal.Formula = "=(" + xlRangeSaleQty.Address[false, false] + "*" + xlRangePrice.Address[false, false] + ")";
                        xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, TotalColNum].NumberFormat = "#,##0.00";
                    }

                    Excel.Range xlRangeSaleQtyFrom = xlWorkSheet.Cells[1 + InvoiceStartRow + 1, SalQtyColNum];
                    Excel.Range xlRangeSaleQtyTo = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + SalesTotalRowOffset - 1, SalQtyColNum];
                    Excel.Range xlRangeTotalCost = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + TotalCostRowOffset, TotalColNum];
                    Excel.Range xlRangeSaleTotal = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + SalesTotalRowOffset, TotalColNum];
                    if (EnumReportType == ReportType.INVOICE)
                        drSellers[ListSellerIndexes[i]]["Total"] = "='" + xlWorkSheet.Name + "'!" + xlRangeSaleTotal.Address[false, false];
                    else if (EnumReportType == ReportType.QUOTATION)
                        drSellers[ListSellerIndexes[i]]["Total"] = "='" + xlWorkSheet.Name + "'!" + xlRangeSaleTotal.Address[false, false];
                    drSellers[ListSellerIndexes[i]]["Quantity"] = "=Sum('" + xlWorkSheet.Name + "'!" + xlRangeSaleQtyFrom.Address[false, false] + ":" + xlRangeSaleQtyTo.Address[false, false] + ")";

                    #region Sales Total Row
                    xlRange = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + SalesTotalRowOffset, TotalColNum - 1];
                    xlRange.Value = "Sales Total";
                    xlRange.Font.Bold = true;

                    xlRange = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + SalesTotalRowOffset, TotalColNum];
                    Excel.Range xlRangeSalesTotalFrom = xlWorkSheet.Cells[1 + InvoiceStartRow + 1, TotalColNum];
                    Excel.Range xlRangeSalesTotalTo = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + SalesTotalRowOffset - 1, TotalColNum];
                    xlRange.Formula = "=Sum(" + xlRangeSalesTotalFrom.Address[false, false] + ":" + xlRangeSalesTotalTo.Address[false, false] + ")";
                    xlRange.Font.Bold = true;
                    xlRange.NumberFormat = "#,##0.00";

                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + SalesTotalRowOffset, 1], xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + SalesTotalRowOffset, TotalColNum - 1]];
                    xlRange.Font.Bold = true;
                    xlRange.Merge();
                    xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    #endregion

                    #region Discount Row
                    xlRange = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + DiscountRowOffset, TotalColNum - 1];
                    xlRange.Value = "Discount";
                    xlRange.Font.Bold = true;

                    DiscountGroupDetails ObjDiscountGroup = CommonFunctions.ObjSellerMaster.GetSellerDiscount(ObjCurrentSeller.Name);

                    xlRange = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + DiscountRowOffset, TotalColNum];
                    Excel.Range xlSalesTotal1 = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + SalesTotalRowOffset, TotalColNum];
                    if (ObjDiscountGroup.DiscountType == DiscountTypes.PERCENT)
                        xlRange.Formula = "=" + xlSalesTotal1.Address[false, false] + "*" + ObjDiscountGroup.Discount + "/100";
                    else if (ObjDiscountGroup.DiscountType == DiscountTypes.ABSOLUTE)
                        xlRange.Value = ObjDiscountGroup.Discount;
                    else
                        xlRange.Formula = "=" + xlSalesTotal1.Address[false, false];
                    xlRange.Font.Bold = true;
                    xlRange.NumberFormat = "#,##0.00";
                    drSellers[ListSellerIndexes[i]]["TotalDiscount"] = "='" + xlWorkSheet.Name + "'!" + xlRange.Address[false, false];

                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + DiscountRowOffset, 1], xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + DiscountRowOffset, TotalColNum - 1]];
                    xlRange.Font.Bold = true;
                    xlRange.Merge();
                    xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    #endregion

                    if (PrintOldBalance)
                    {
                        #region Old Balance Row
                        xlRange = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + OldBalanceRowOffset, TotalColNum - 1];
                        xlRange.Value = "Old Balance";
                        xlRange.Font.Bold = true;

                        xlRange = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + OldBalanceRowOffset, TotalColNum];
                        xlRange.Value = drSellers[ListSellerIndexes[i]]["OldBalance"].ToString();
                        xlRange.Font.Bold = true;
                        xlRange.NumberFormat = "#,##0.00";

                        xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + OldBalanceRowOffset, 1], xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + OldBalanceRowOffset, TotalColNum - 1]];
                        xlRange.Font.Bold = true;
                        xlRange.Merge();
                        xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        #endregion
                    }

                    if (PrintVATPercent)
                    {
                        #region VAT Percent Row
                        xlRange = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + OldBalanceRowOffset, TotalColNum - 1];
                        xlRange.Value = "VAT Percent " + CurrReportSettings.VATPercent + "%";
                        xlRange.Font.Bold = true;

                        xlRange = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + OldBalanceRowOffset, TotalColNum];
                        Excel.Range xlSalesTotal = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + SalesTotalRowOffset, TotalColNum];
                        Excel.Range xlDiscount = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + DiscountRowOffset, TotalColNum];
                        xlRange.Formula = "=(" + xlSalesTotal.Address[false, false] + "-" + xlDiscount.Address[false, false] + ")*" + CurrReportSettings.VATPercent + "/100";
                        xlRange.Font.Bold = true;
                        xlRange.NumberFormat = "#,##0.00";
                        drSellers[ListSellerIndexes[i]]["TotalTax"] = "='" + xlWorkSheet.Name + "'!" + xlRange.Address[false, false];

                        xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + OldBalanceRowOffset, 1], xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + OldBalanceRowOffset, TotalColNum - 1]];
                        xlRange.Font.Bold = true;
                        xlRange.Merge();
                        xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        #endregion
                    }

                    #region Total Cost Row
                    xlRange = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + TotalCostRowOffset, TotalColNum - 1];
                    xlRange.Value = "Total";
                    xlRange.Font.Bold = true;

                    xlRange = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + TotalCostRowOffset, TotalColNum];
                    Excel.Range xlRangeSalesTotal = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + SalesTotalRowOffset, TotalColNum];
                    Excel.Range xlRangeDiscount = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + DiscountRowOffset, TotalColNum];
                    Excel.Range xlRangeOldBalance = xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + OldBalanceRowOffset, TotalColNum];
                    xlRange.Formula = "=Round(" + xlRangeSalesTotal.Address[false, false] + "+" + xlRangeOldBalance.Address[false, false] + "-" + xlRangeDiscount.Address[false, false] + ", 0)";
                    xlRange.Font.Bold = true;
                    xlRange.NumberFormat = "#,##0.00";

                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + TotalCostRowOffset, 1], xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + TotalCostRowOffset, TotalColNum - 1]];
                    xlRange.Font.Bold = true;
                    xlRange.Merge();
                    xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    #endregion

                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[InvoiceStartRow + 1, 1], xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + TotalCostRowOffset, TotalColNum]];
                    SetAllBorders(xlRange);
                    #endregion

                    xlWorkSheet.UsedRange.Columns.AutoFit();
                    AddPageHeaderAndFooter(ref xlWorkSheet, CurrReportSettings.HeaderSubTitle, CurrReportSettings);
                }
                #endregion

                if (chkBoxCreateSummary.Checked && !SummaryPrinted && CreateSummary)
                {
                    CreateSellerSummarySheet(drSellers, xlWorkbook, CurrReportSettings);
                    CreateItemSummarySheet(drItems, xlWorkbook, CurrReportSettings);
                    SummaryPrinted = true;
                }

                xlApp.DisplayAlerts = false;
                xlWorkbook.Sheets[SelectedDateTimeString].Delete();
                xlApp.DisplayAlerts = true;
                Excel.Worksheet FirstWorksheet = xlWorkbook.Sheets[1];
                FirstWorksheet.Select();

                #region Write InvoiceNumber to Settings File
                CurrReportSettings.LastNumber = InvoiceNumber;
                #endregion

                backgroundWorker1.ReportProgress(100);
                xlWorkbook.SaveAs(SaveFileName);
                xlWorkbook.Close();
                lblStatus.Text = "Completed creation of " + ReportTypeName + "s for all Sellers";

                CommonFunctions.ReleaseCOMObject(xlWorkbook);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateSellerReport", ex);
                xlApp.Quit();
                CommonFunctions.ReleaseCOMObject(xlApp);
            }
        }

        private void CreateSellerReport(ReportType EnumReportType, DataRow[] drItems, DataRow[] drSellers, String SelectedDateTimeString, Int32 StartRow, Int32 StartColumn, List<Int32> ListItemIndexes, List<Int32> ListSellerIndexes, Excel.Workbook xlWorkbook)
        {
            try
            {
                Boolean PrintOldBalance = false, CreateSummary = false;
                ReportSettings CurrReportSettings = null;
                String ReportTypeName = "", BillNumberText = "", SaveFileName = "";
                switch (EnumReportType)
                {
                    case ReportType.INVOICE:
                        CurrReportSettings = CommonFunctions.ObjInvoiceSettings;
                        ReportTypeName = "Invoice";
                        BillNumberText = "Invoice#";
                        SaveFileName = txtBoxOutputFolder.Text + "\\Invoice_" + SelectedDateTimeString + ".xlsx";
                        if (CommonFunctions.ObjGeneralSettings.SummaryLocation == 0) CreateSummary = true;
                        break;
                    case ReportType.QUOTATION:
                        CurrReportSettings = CommonFunctions.ObjQuotationSettings;
                        ReportTypeName = "Quotation";
                        PrintOldBalance = true;
                        BillNumberText = "Quotation#";
                        SaveFileName = txtBoxOutputFolder.Text + "\\Quotation_" + SelectedDateTimeString + ".xlsx";
                        if (CommonFunctions.ObjGeneralSettings.SummaryLocation == 1) CreateSummary = true;
                        break;
                    default:
                        return;
                }
                Excel.Worksheet xlSalesOrderWorksheet = xlWorkbook.Sheets[1];

                #region Print Invoice Sheet for each Seller

                Int32 InvoiceNumber = CurrReportSettings.LastNumber;
                Int32 ValidSellerCount = ListSellerIndexes.Where(s => (s >= 0)).ToList().Count;
                Int32 ValidItemCount = ListItemIndexes.Where(s => (s >= 0)).ToList().Count;
                Int32 ProgressBarCount = (ValidSellerCount * ValidItemCount);
                Int32 Counter = 0, SLNo = 0;
                Int32 SellerCount = 0, SalesOrderDetailsCount = 5;
                Double Quantity;
                for (int i = 0; i < ListSellerIndexes.Count; i++)
                {
                    if (ListSellerIndexes[i] < 0) continue;
                    SellerCount++;
                    lblStatus.Text = "Creating " + ReportTypeName + " for Seller " + SellerCount + " of " + ValidSellerCount;
                    Excel.Worksheet xlWorkSheet = xlWorkbook.Worksheets.Add(Type.Missing, xlWorkbook.Sheets[xlWorkbook.Sheets.Count]);

                    SLNo = 0;
                    InvoiceNumber++;
                    SellerDetails ObjCurrentSeller = CommonFunctions.ObjSellerMaster.GetSellerDetails(drSellers[ListSellerIndexes[i]]["SellerName"].ToString());
                    DiscountGroupDetails ObjDiscountGroup = CommonFunctions.ObjSellerMaster.GetSellerDiscount(ObjCurrentSeller.Name);

                    Invoice ObjInvoice = CommonFunctions.GetInvoiceTemplate(EnumReportType);
                    ObjInvoice.SerialNumber = InvoiceNumber.ToString();
                    ObjInvoice.InvoiceNumberText = BillNumberText;
                    ObjInvoice.ObjSellerDetails = ObjCurrentSeller;
                    ObjInvoice.CurrReportSettings = CurrReportSettings;
                    ObjInvoice.DateOfInvoice = DateTime.Now;
                    ObjInvoice.PrintOldBalance = PrintOldBalance;
                    ObjInvoice.ListProducts = new List<ProductDetailsForInvoice>();

                    #region Print Invoice Items
                    for (int j = 0; j < ListItemIndexes.Count; j++)
                    {
                        if (ListItemIndexes[j] < 0) continue;
                        Counter++;
                        backgroundWorker1.ReportProgress((Counter * 100) / ProgressBarCount);

                        if (xlSalesOrderWorksheet.Cells[StartRow + 1 + i, StartColumn + SalesOrderDetailsCount + j].Value == null) continue;

                        SLNo++;
                        Quantity = Double.Parse(xlSalesOrderWorksheet.Cells[StartRow + 1 + i, StartColumn + SalesOrderDetailsCount + j].Value.ToString());
                        if (CreateSummary) drItems[ListItemIndexes[j]]["Quantity"] = Double.Parse(drItems[ListItemIndexes[j]]["Quantity"].ToString()) + Quantity;

                        ProductDetailsForInvoice ObjProductDetailsForInvoice = new ProductDetailsForInvoice();
                        ProductDetails ObjProductDetails = CommonFunctions.ObjProductMaster.GetProductDetails(drItems[ListItemIndexes[j]]["ItemName"].ToString());
                        if (ObjProductDetails == null)
                        {
                            xlWorkbook.Close();

                            CommonFunctions.ReleaseCOMObject(xlWorkbook);
                            MessageBox.Show(this, "Unable to find Item \"" + drItems[ListItemIndexes[j]]["ItemName"].ToString() + "\" in ItemMaster", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            lblStatus.Text = "Unable to find Item \"" + drItems[ListItemIndexes[j]]["ItemName"].ToString() + "\" in ItemMaster";
                            return;
                        }
                        ObjProductDetailsForInvoice.SerialNumber = SLNo;
                        ObjProductDetailsForInvoice.Description = ObjProductDetails.ItemName;
                        ObjProductDetailsForInvoice.HSNCode = ObjProductDetails.HSNCode;
                        ObjProductDetailsForInvoice.UnitsOfMeasurement = ObjProductDetails.UnitsOfMeasurement;
                        ObjProductDetailsForInvoice.OrderQuantity = Quantity;
                        ObjProductDetailsForInvoice.SaleQuantity = 0;
                        if (chkBoxUseOrdQty.Checked == true) ObjProductDetailsForInvoice.SaleQuantity = Quantity;
                        ObjProductDetailsForInvoice.Rate = CommonFunctions.ObjProductMaster.GetPriceForProduct(ObjProductDetails.ItemName, ObjCurrentSeller.PriceGroupIndex);

                        Double[] TaxRates = CommonFunctions.ObjProductMaster.GetTaxRatesForProduct(ObjProductDetails.ItemName);
                        ObjProductDetailsForInvoice.CGSTDetails = new TaxDetails();
                        ObjProductDetailsForInvoice.CGSTDetails.TaxRate = TaxRates[0] / 100;
                        ObjProductDetailsForInvoice.SGSTDetails = new TaxDetails();
                        ObjProductDetailsForInvoice.SGSTDetails.TaxRate = TaxRates[1] / 100;
                        ObjProductDetailsForInvoice.IGSTDetails = new TaxDetails();
                        ObjProductDetailsForInvoice.IGSTDetails.TaxRate = TaxRates[2] / 100;
                        ObjProductDetailsForInvoice.DiscountGroup = ObjDiscountGroup;
                        ObjInvoice.ListProducts.Add(ObjProductDetailsForInvoice);
                    }
                    #endregion
                    ObjInvoice.CreateInvoice(xlWorkSheet);

                    if (CreateSummary)
                    {
                        drSellers[ListSellerIndexes[i]]["InvoiceNumber"] = ObjInvoice.SerialNumber;
                        drSellers[ListSellerIndexes[i]]["Total"] = ObjInvoice.TotalSalesValue;
                        drSellers[ListSellerIndexes[i]]["TotalDiscount"] = ObjInvoice.TotalDiscount;
                        drSellers[ListSellerIndexes[i]]["TotalTax"] = ObjInvoice.TotalTax;
                    }
                }
                #endregion

                if (chkBoxCreateSummary.Checked && !SummaryPrinted && CreateSummary)
                {
                    CreateSellerSummarySheet(drSellers, xlWorkbook, CurrReportSettings);
                    CreateItemSummarySheet(drItems, xlWorkbook, CurrReportSettings);
                    SummaryPrinted = true;
                }

                xlApp.DisplayAlerts = false;
                xlWorkbook.Sheets[SelectedDateTimeString].Delete();
                xlApp.DisplayAlerts = true;
                Excel.Worksheet FirstWorksheet = xlWorkbook.Sheets[1];
                FirstWorksheet.Select();

                #region Write InvoiceNumber to Settings File
                CurrReportSettings.LastNumber = InvoiceNumber;
                #endregion

                backgroundWorker1.ReportProgress(100);
                xlWorkbook.SaveAs(SaveFileName);
                xlWorkbook.Close();
                lblStatus.Text = "Completed creation of " + ReportTypeName + "s for all Sellers";

                CommonFunctions.ReleaseCOMObject(xlWorkbook);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateSellerReport", ex);
                xlApp.Quit();
                CommonFunctions.ReleaseCOMObject(xlApp);
            }
        }

        public static void SetAllBorders(Excel.Range xlRange)
        {
            try
            {
                xlRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic);
                xlRange.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                xlRange.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
                xlRange.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                xlRange.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                xlRange.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                xlRange.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateSellerReport", ex);
                throw ex;
            }
        }

        public static void SetBorders(Excel.Range xlRange)
        {
            try
            {
                xlRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic);
                xlRange.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                xlRange.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                xlRange.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                xlRange.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateSellerReport", ex);
                throw ex;
            }
        }

        private void CreateItemSummarySheet(DataRow[] drItems, Excel.Workbook xlWorkbook, ReportSettings CurrReportSettings)
        {
            try
            {
                lblStatus.Text = "Creating Item Summary Sheet";
                #region Print Item Summary Sheet
                Int32 SummaryStartRow = 0;
                Double Total = 0;
                Excel.Worksheet xlSummaryWorkSheet = xlWorkbook.Worksheets.Add(xlWorkbook.Sheets[1]);
                xlSummaryWorkSheet.Name = "Item Summary";
                xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 1].Value = "Sl.No.";
                xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 2].Value = "Item Name";
                xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 3].Value = "Vendor Name";
                xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 4].Value = "Quantity";
                xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 5].Value = "Price";
                xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 6].Value = "Total";
                Excel.Range xlRange1 = xlSummaryWorkSheet.Range[xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 1], xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 6]];
                xlRange1.Font.Bold = true;

                for (int i = 0; i < drItems.Length; i++)
                {
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
                AddPageHeaderAndFooter(ref xlSummaryWorkSheet, "Itemwise Summary", CurrReportSettings);
                xlApp.DisplayAlerts = true;
                #endregion
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateItemSummarySheet", ex);
                xlApp.Quit();
                CommonFunctions.ReleaseCOMObject(xlApp);
            }
        }

        private void CreateSellerSummarySheet(DataRow[] drSellers, Excel.Workbook xlWorkbook, ReportSettings CurrReportSettings)
        {
            try
            {
                lblStatus.Text = "Creating Seller Summary Sheet";
                #region Print Seller Summary Sheet
                Int32 SummaryStartRow = 0, CurrRow = 0, CurrCol = 0;
                Excel.Worksheet xlSellerSummaryWorkSheet = xlWorkbook.Worksheets.Add(xlWorkbook.Sheets[1]);
                xlSellerSummaryWorkSheet.Name = "Seller Summary";

                SummaryStartRow++;
                Excel.Range xlRange1 = xlSellerSummaryWorkSheet.Cells[SummaryStartRow, 1];
                xlRange1.Value = "Date";
                xlRange1.Font.Bold = true;
                xlRange1 = xlSellerSummaryWorkSheet.Cells[SummaryStartRow, 2];
                xlRange1.Value = DateTime.Today.ToString("dd-MMM-yyyy");
                xlRange1 = xlSellerSummaryWorkSheet.Range[xlSellerSummaryWorkSheet.Cells[SummaryStartRow, 2], xlSellerSummaryWorkSheet.Cells[SummaryStartRow, 3]];
                xlRange1.Merge();
                xlRange1.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

                CurrRow = SummaryStartRow + 1;
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Sl#";
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Bill#";
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Seller Name";
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Sale";
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Cancel";
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Return";
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Discount";
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Total Tax";
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Net Sale";
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "OB";
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Cash";
                Int32 LastCol = CurrCol;
                xlRange1 = xlSellerSummaryWorkSheet.Range[xlSellerSummaryWorkSheet.Cells[CurrRow, 1], xlSellerSummaryWorkSheet.Cells[CurrRow, LastCol]];
                xlRange1.Font.Bold = true;

                Int32 SellersCount = 0;
                Boolean IsSellerOnlyInSummary = false;
                List<DataRow> tmpdrSellers = new List<DataRow>();
                for (int i = 0; i < drSellers.Length; i++)
                {
                    if (String.IsNullOrEmpty(drSellers[i]["InvoiceNumber"].ToString().Trim())) continue;
                    if (Int32.Parse(drSellers[i]["InvoiceNumber"].ToString()) == Int32.MinValue) continue;
                    tmpdrSellers.Add(drSellers[i]);
                }
                for (int i = 0; i < drSellers.Length; i++)
                {
                    if (String.IsNullOrEmpty(drSellers[i]["InvoiceNumber"].ToString().Trim())) continue;
                    if (Int32.Parse(drSellers[i]["InvoiceNumber"].ToString()) == Int32.MinValue)
                        tmpdrSellers.Add(drSellers[i]);
                }
                drSellers = tmpdrSellers.ToArray();

                for (int i = 0; i < drSellers.Length; i++)
                {
                    if (String.IsNullOrEmpty(drSellers[i]["InvoiceNumber"].ToString().Trim())) continue;
                    IsSellerOnlyInSummary = false;
                    if (Int32.Parse(drSellers[i]["InvoiceNumber"].ToString()) == Int32.MinValue) IsSellerOnlyInSummary = true;
                    SellersCount++;

                    CurrRow = SellersCount + SummaryStartRow + 1;
                    CurrCol = 0;
                    CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = SellersCount;
                    CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = (IsSellerOnlyInSummary ? "-1" : drSellers[i]["InvoiceNumber"].ToString());
                    CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = drSellers[i]["SellerName"].ToString();
                    CurrCol++; Excel.Range xlRangeSale = xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    CurrCol++; Excel.Range xlRangeCancel = xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    CurrCol++; Excel.Range xlRangeReturn = xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    CurrCol++; Excel.Range xlRangeDiscount = xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    CurrCol++; Excel.Range xlRangeTotalTax = xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    CurrCol++; Excel.Range xlRangeNetSale = xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    CurrCol++; Excel.Range xlRangeOldBalance = xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    CurrCol++; Excel.Range xlRangeCash = xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    if (!IsSellerOnlyInSummary)
                    {
                        xlRangeSale.Formula = drSellers[i]["Total"].ToString();
                        xlRangeDiscount.Formula = drSellers[i]["TotalDiscount"].ToString();
                        xlRangeTotalTax.Formula = drSellers[i]["TotalTax"].ToString();
                    }
                    xlRangeNetSale.Formula = "=Round(" + xlRangeSale.Address[false, false]
                                                + "-" + xlRangeCancel.Address[false, false]
                                                + "-" + xlRangeReturn.Address[false, false]
                                                + "-" + xlRangeDiscount.Address[false, false]
                                                + "+" + xlRangeTotalTax.Address[false, false] + ", 0)";
                    xlRangeOldBalance.Value = drSellers[i]["OldBalance"].ToString();
                    xlRangeSale.NumberFormat = "#,##0.00"; xlRangeCancel.NumberFormat = "#,##0.00";
                    xlRangeReturn.NumberFormat = "#,##0.00"; xlRangeDiscount.NumberFormat = "#,##0.00";
                    xlRangeTotalTax.NumberFormat = "#,##0.00"; xlRangeNetSale.NumberFormat = "#,##0.00";
                    xlRangeOldBalance.NumberFormat = "#,##0.00"; xlRangeCash.NumberFormat = "#,##0.00";
                }
                CurrRow = SellersCount + SummaryStartRow + 2;
                Excel.Range xlRange = xlSellerSummaryWorkSheet.Cells[CurrRow, 3];
                xlRange.Value = "Total";
                xlRange.Font.Bold = true;
                xlRange.Font.Color = Color.Red;

                for (int i = 4; i <= LastCol; i++)
                {
                    CurrCol = i;
                    Excel.Range xlRangeTotal = xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    Excel.Range xlRangeTotalFrom = xlSellerSummaryWorkSheet.Cells[SummaryStartRow + 2, CurrCol];
                    Excel.Range xlRangeTotalTo = xlSellerSummaryWorkSheet.Cells[CurrRow - 1, CurrCol];
                    xlRangeTotal.Formula = "=Sum(" + xlRangeTotalFrom.Address[false, false] + ":" + xlRangeTotalTo.Address[false, false] + ")";
                    xlRangeTotal.NumberFormat = "#,##0.00";
                    xlRangeTotal.Font.Bold = true;
                }

                xlRange = xlSellerSummaryWorkSheet.Range[xlSellerSummaryWorkSheet.Cells[SummaryStartRow + 1, 1], xlSellerSummaryWorkSheet.Cells[CurrRow + 1, LastCol]];
                SetAllBorders(xlRange);

                xlSellerSummaryWorkSheet.UsedRange.Columns.AutoFit();

                xlRange = xlSellerSummaryWorkSheet.Columns["B"];
                xlRange.ColumnWidth = 7;
                xlRange = xlSellerSummaryWorkSheet.Columns["C"];
                xlRange.ColumnWidth = 24;
                
                AddPageHeaderAndFooter(ref xlSellerSummaryWorkSheet, "Sellerwise Summary", CurrReportSettings);
                #endregion
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateSellerSummarySheet", ex);
                xlApp.Quit();
                CommonFunctions.ReleaseCOMObject(xlApp);
            }
        }

        internal static void AddPageHeaderAndFooter(ref Excel.Worksheet xlWorksheet, String PageHeaderTitle, ReportSettings CurrReportSettings)
        {
            try
            {
                if (!String.IsNullOrEmpty(CommonFunctions.ObjApplicationSettings.LogoFileName))
                {
                    xlWorksheet.PageSetup.RightHeaderPicture.Filename = AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + CommonFunctions.ObjApplicationSettings.LogoFileName;
                    xlWorksheet.PageSetup.RightHeaderPicture.ColorType = Microsoft.Office.Core.MsoPictureColorType.msoPictureAutomatic;
                    xlWorksheet.PageSetup.RightHeaderPicture.CropBottom = 0;
                    xlWorksheet.PageSetup.RightHeaderPicture.CropLeft = 0;
                    xlWorksheet.PageSetup.RightHeaderPicture.CropRight = 0;
                    xlWorksheet.PageSetup.RightHeaderPicture.CropTop = 0;
                    xlWorksheet.PageSetup.RightHeaderPicture.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;
                    xlWorksheet.PageSetup.RightHeaderPicture.Height = CommonFunctions.ObjApplicationSettings.LogoImageHeight;
                    //xlWorksheet.PageSetup.RightHeaderPicture.Width = 30;
                    //xlWorksheet.PageSetup.Application.PrintCommunication = false;
                    //xlWorksheet.PageSetup.PrintArea = "";
                    /*xlWorksheet.PageSetup.PrintTitleRows = "";
                    xlWorksheet.PageSetup.PrintTitleColumns = "";

                    xlWorksheet.PageSetup.Application.PrintCommunication = true;
                    xlWorksheet.PageSetup.PrintArea = "";
                    xlWorksheet.PageSetup.Application.PrintCommunication = false;*/
                }

                xlWorksheet.PageSetup.LeftHeader = "";
                //xlWorksheet.PageSetup.CenterHeader = "\n&\"Gill Sans MT,Bold\"&12&K" + CommonFunctions.GetColorHexCode(CurrReportSettings.HeaderTitleColor) + CurrReportSettings.HeaderTitle;
                xlWorksheet.PageSetup.CenterHeader = "\n&\"Arial,Bold\"&16&K" + CommonFunctions.GetColorHexCode(CurrReportSettings.HeaderTitleColor) + CurrReportSettings.HeaderTitle;
                if (!String.IsNullOrEmpty(PageHeaderTitle))
                {
                    xlWorksheet.PageSetup.CenterHeader += "\n&\"Arial,Regular\"&14&K" + CommonFunctions.GetColorHexCode(CurrReportSettings.HeaderSubTitleColor) + PageHeaderTitle;
                }
                xlWorksheet.PageSetup.CenterHeader += "\n\n";
                xlWorksheet.PageSetup.RightHeader = "&G";
                xlWorksheet.PageSetup.CenterFooter = "";
                if (!String.IsNullOrEmpty(CurrReportSettings.FooterTitle))
                {
                    xlWorksheet.PageSetup.CenterFooter += "\n&\"Arial,Bold\"&14&K" + CommonFunctions.GetColorHexCode(CurrReportSettings.FooterTitleColor) + CurrReportSettings.FooterTitle;
                }
                if (!String.IsNullOrEmpty(CurrReportSettings.Address))
                {
                    xlWorksheet.PageSetup.CenterFooter += "\n&\"Arial,Italic\"&10&K" + CommonFunctions.GetColorHexCode(CurrReportSettings.FooterTextColor) + CurrReportSettings.Address;
                }
                if (!String.IsNullOrEmpty(CurrReportSettings.GSTINumber))
                {
                    xlWorksheet.PageSetup.CenterFooter += "\nGSTIN:" + CurrReportSettings.GSTINumber;
                }
                if (!String.IsNullOrEmpty(CurrReportSettings.PhoneNumber))
                {
                    xlWorksheet.PageSetup.CenterFooter += "\nPhone:" + CurrReportSettings.PhoneNumber;
                }
                if (!String.IsNullOrEmpty(CurrReportSettings.EMailID))
                {
                    if (String.IsNullOrEmpty(CurrReportSettings.PhoneNumber)) xlWorksheet.PageSetup.CenterFooter += "\n";
                    else xlWorksheet.PageSetup.CenterFooter += " | ";
                    xlWorksheet.PageSetup.CenterFooter += "Email:" + CurrReportSettings.EMailID;
                }
                if (xlWorksheet.PageSetup.Pages.Count > 1)
                    xlWorksheet.PageSetup.RightFooter = "&P";
                xlWorksheet.PageSetup.PrintGridlines = true;
                xlWorksheet.PageSetup.CenterHorizontally = true;
                xlWorksheet.PageSetup.TopMargin = xlWorksheet.PageSetup.Application.InchesToPoints(1.5);
                xlWorksheet.PageSetup.BottomMargin = xlWorksheet.PageSetup.Application.InchesToPoints(1.5);
                xlWorksheet.PageSetup.FooterMargin = xlWorksheet.PageSetup.Application.InchesToPoints(0.25);
                xlWorksheet.PageSetup.HeaderMargin = xlWorksheet.PageSetup.Application.InchesToPoints(0.25);
                xlWorksheet.PageSetup.LeftMargin = xlWorksheet.PageSetup.Application.InchesToPoints(0.7);
                xlWorksheet.PageSetup.RightMargin = xlWorksheet.PageSetup.Application.InchesToPoints(0.7);

                /*xlWorksheet.PageSetup.PrintHeadings = false;
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
            CommonFunctions.UpdateProgressBar(e.ProgressPercentage);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CommonFunctions.ResetProgressBar();
            btnCancel.Focus();
        }

        private void chkBoxCreateInvoice_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxCreateInvoice.Checked) return;
            else if (!chkBoxCreateQuotation.Checked) chkBoxCreateQuotation.Checked = true;
        }

        private void chkBoxCreateQuotation_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxCreateQuotation.Checked) return;
            else if (!chkBoxCreateInvoice.Checked) chkBoxCreateInvoice.Checked = true;
        }

        private void chkListBoxLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkListBoxLine.SelectedIndex == 0)
                {
                    CheckState checkstate = chkListBoxLine.GetItemCheckState(0);
                    if (PrevAllLinesCheckState != checkstate)
                    {
                        for (int i = 1; i < chkListBoxLine.Items.Count; i++)
                        {
                            chkListBoxLine.SetItemCheckState(i, checkstate);
                        }
                    }
                }
                else if (chkListBoxLine.GetItemCheckState(chkListBoxLine.SelectedIndex) == CheckState.Unchecked)
                {
                    chkListBoxLine.SetItemCheckState(0, CheckState.Unchecked);
                }
                else if (chkListBoxLine.CheckedItems.Count == chkListBoxLine.Items.Count - 1)
                {
                    chkListBoxLine.SetItemCheckState(0, CheckState.Checked);
                }

                PrevAllLinesCheckState = chkListBoxLine.GetItemCheckState(0);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("chkListBoxLine_SelectedIndexChanged", ex);
            }
        }

        private void btnAddSeller_Click(object sender, EventArgs e)
        {
            SellerListForm ObjSellersListForm = new SellerListForm(this);
            ObjSellersListForm.ShowDialog(this);
        }

        private void chkListBoxLine_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                chkListBoxLine_SelectedIndexChanged(null, null);
            }
        }

        private void CreateSellerInvoice_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommonFunctions.WriteToSettingsFile();
        }

        private void dateTimeInvoice_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtBoxOtherFile.Text = txtBoxOutputFolder.Text + @"\SalesOrder_" + dateTimeInvoice.Value.ToString("dd-MM-yyyy") + ".xlsx";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerInvoiceForm.dateTimeInvoice_ValueChanged()", ex);
            }
        }
    }
}
