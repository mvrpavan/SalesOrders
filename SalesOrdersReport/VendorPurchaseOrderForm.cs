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
    public partial class VendorPurchaseOrderForm : Form
    {
        public String MasterFilePath;
        Excel.Application xlApp;
        CheckState PrevAllLinesCheckState = CheckState.Indeterminate;

        public VendorPurchaseOrderForm()
        {
            InitializeComponent();

            MasterFilePath = CommonFunctions.MasterFilePath;
            txtBoxOutputFolder.Text = System.IO.Path.GetDirectoryName(MasterFilePath);

            CommonFunctions.ResetProgressBar();

            lblStatus.Text = "";
            CommonFunctions.ListSelectedVendors.Clear();

            FillLineFromOrderMaster();
        }

        private void FillLineFromOrderMaster()
        {
            try
            {
                chkListBoxLine.Items.Clear();
                for (int i = 0; i < CommonFunctions.ListLines.Count; i++)
                {
                    chkListBoxLine.Items.Add(CommonFunctions.ListLines[i], true);
                }

                PrevAllLinesCheckState = CheckState.Checked;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorPurchaseOrderForm.FillLineFromOrderMaster()", ex);
            }
        }

        delegate void ReportProgressDel(Int32 ProgressState);
        ReportProgressDel ReportProgress = null;

        private void ReportProgressFunc(Int32 ProgressState)
        {
            if (ReportProgress == null) return;
            ReportProgress(ProgressState);
        }

        private void btnAddVendor_Click(object sender, EventArgs e)
        {
            try
            {
                VendorListForm ObjVendorListForm = new VendorListForm(this);
                ObjVendorListForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorPurchaseOrderForm.btnAddVendor_Click()", ex);
            }
        }

        private void btnBrowseVendorOrderFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Multiselect = false;
                openFileDialog1.FileName = MasterFilePath;
                DialogResult dlgResult = openFileDialog1.ShowDialog();

                if (dlgResult == System.Windows.Forms.DialogResult.OK)
                {
                    txtBoxVendorOrderSheet.Text = openFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorPurchaseOrderForm.BrowseOtherFile_Click()", ex);
            }
        }

        private void btnSaveFolderBrowse_Click(object sender, EventArgs e)
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
                CommonFunctions.ShowErrorDialog("VendorPurchaseOrderForm.btnSaveFolderBrowse_Click()", ex);
            }
        }

        private void btnCreatePurchaseOrder_Click(object sender, EventArgs e)
        {
            try
            {
                //CreatePurchaseOrders();
                ReportProgress = bgWorkerCreatePO.ReportProgress;
                bgWorkerCreatePO.WorkerReportsProgress = true;
                bgWorkerCreatePO.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorPurchaseOrderForm.btnCreatePurchaseOrder_Click()", ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
                CommonFunctions.ShowErrorDialog("VendorPurchaseOrderForm.chkListBoxLine_SelectedIndexChanged", ex);
            }
        }

        private void chkListBoxLine_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                chkListBoxLine_SelectedIndexChanged(null, null);
            }
        }

        private void bgWorkerCreatePO_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                CommonFunctions.ToggleEnabledPropertyOfAllControls(this);
                lblStatus.Enabled = true;

                CreatePurchaseOrders();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorPurchaseOrderForm.bgWorkerCreatePO_DoWork()", ex);
            }
        }

        private void bgWorkerCreatePO_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CommonFunctions.UpdateProgressBar(e.ProgressPercentage);
        }

        private void bgWorkerCreatePO_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CommonFunctions.ResetProgressBar();

            CommonFunctions.ToggleEnabledPropertyOfAllControls(this);
            lblStatus.Enabled = true;
            btnClose.Focus();
        }

        private void CreatePurchaseOrders()
        {
            xlApp = new Excel.Application();
            try
            {
                DataTable dtItemMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("ItemMaster", MasterFilePath, "*");
                DataTable dtVendorMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("VendorMaster", MasterFilePath, "*");
                String[] SelectedLine = new String[chkListBoxLine.CheckedItems.Count];
                chkListBoxLine.CheckedItems.CopyTo(SelectedLine, 0);

                if (SelectedLine.Length == 0 && CommonFunctions.ListSelectedVendors.Count == 0)
                {
                    btnClose.Enabled = true;
                    btnCreatePurchaseOrder.Enabled = true;
                    MessageBox.Show(this, "No Line/Vendor are selected\nUnable to create Purchase Orders", "Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblStatus.Text = "Select any Line/Vendor";
                    return;
                }

                dtItemMaster.Columns.Add("Quantity", Type.GetType("System.Double"));
                DataRow[] drItems = dtItemMaster.Select("", "SlNo asc");
                for (int i = 0; i < drItems.Length; i++)
                {
                    drItems[i]["Quantity"] = 0;
                }

                dtVendorMaster.Columns.Add("Quantity", Type.GetType("System.String"));
                dtVendorMaster.Columns.Add("Total", Type.GetType("System.String"));
                dtVendorMaster.Columns.Add("PONumber", Type.GetType("System.Int32"));
                dtVendorMaster.Columns.Add("TotalDiscount", Type.GetType("System.String"));
                dtVendorMaster.Columns.Add("TotalTax", Type.GetType("System.String"));
                DataRow[] drVendors = dtVendorMaster.Select("", "SlNo asc");

                String SelectedDateTimeString = dateTimePO.Value.ToString("dd-MM-yyyy");

                String VendorOrderFile = txtBoxVendorOrderSheet.Text;

                Excel.Workbook xlPOWorkbook = xlApp.Workbooks.Open(VendorOrderFile);
                Excel.Worksheet xlPOWorksheet = CommonFunctions.GetWorksheet(xlPOWorkbook, SelectedDateTimeString);
                Int32 StartRow = 7, StartColumn = 1, DetailsCount = 5;

                #region Identify StockItems in PurchaseOrderSheet
                List<Int32> ListItemIndexes = new List<Int32>();
                Int32 ColumnCount = xlPOWorksheet.UsedRange.Columns.Count;
                for (int i = StartColumn + DetailsCount; i <= ColumnCount; i++)
                {
                    String ItemName = xlPOWorksheet.Cells[StartRow, i].Value;
                    Int32 ItemIndex = -1;
                    for (int j = 0; j < drItems.Length; j++)
                    {
                        if (drItems[j]["StockName"].ToString().Equals(ItemName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            ItemIndex = j;
                            break;
                        }
                    }
                    ListItemIndexes.Add(ItemIndex);
                }
                #endregion

                #region Identify Vendors in SalesOrderSheet
                List<Int32> ListVendorIndexes = new List<Int32>();
                Int32 RowCount = xlPOWorksheet.UsedRange.Rows.Count + 1;
                for (int i = StartRow + 1; i <= RowCount; i++)
                {
                    if (xlPOWorksheet.Cells[i, StartColumn + 1].Value == null) continue;
                    if (xlPOWorksheet.Cells[i, StartColumn + 2].Value == null) continue;
                    String VendorName = xlPOWorksheet.Cells[i, StartColumn + 2].Value;
                    Int32 VendorIndex = -1;
                    for (int j = 0; j < drVendors.Length; j++)
                    {
                        if (drVendors[j]["VendorName"].ToString().Equals(VendorName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            VendorIndex = j;
                            break;
                        }
                    }

                    if (VendorIndex < 0) continue;

                    String Line = drVendors[VendorIndex]["Line"].ToString().Replace("<", "").Replace(">", "").ToUpper();
                    if (Line.Trim().Length == 0) Line = "<Blanks>";

                    if (!SelectedLine.Contains(Line) && !CommonFunctions.ListSelectedVendors.Contains(VendorName))
                    {
                        ListVendorIndexes.Add(-1);
                        continue;
                    }

                    Excel.Range CountCell = xlPOWorksheet.Cells[i, StartColumn + 1];
                    Double CountItems = Double.Parse(CountCell.Value.ToString());
                    if (CountItems <= 1E-6)
                    {
                        ListVendorIndexes.Add(-1);
                        continue;
                    }

                    ListVendorIndexes.Add(VendorIndex);
                }
                #endregion

                Excel.Workbook xlWorkbook = null;
                xlPOWorksheet.Copy();
                xlWorkbook = xlApp.Workbooks[2];

                CreateVendorPurchaseOrder(ReportType.PURCHASEORDER, drItems, drVendors, SelectedDateTimeString, 
                    StartRow, StartColumn, ListItemIndexes, ListVendorIndexes, xlWorkbook);
                xlPOWorkbook.Close(false);

                MessageBox.Show(this, "Purchase Order generated successfully", "Status", MessageBoxButtons.OK);
                lblStatus.Text = "Click \"Close Window\" to close this window";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorPurchaseOrderForm.CreatePurchaseOrders()", ex);
            }
            finally
            {
                xlApp.Quit();
                CommonFunctions.ReleaseCOMObject(xlApp);
            }
        }

        private void CreateVendorPurchaseOrder(ReportType EnumReportType, DataRow[] drItems, DataRow[] drVendors, 
                            String SelectedDateTimeString, Int32 StartRow, Int32 StartColumn, 
                            List<Int32> ListItemIndexes, List<Int32> ListVendorIndexes, Excel.Workbook xlWorkbook)
        {
            try
            {
                Boolean PrintVATPercent = false;
                ReportSettings CurrReportSettings = null;
                String ReportTypeName = "", BillNumberText = "", SaveFileName = "";
                switch (EnumReportType)
                {
                    case ReportType.PURCHASEORDER:
                        CurrReportSettings = CommonFunctions.ObjPurchaseOrderSettings;
                        ReportTypeName = "Purchase Order";
                        PrintVATPercent = true;
                        BillNumberText = "PO#";
                        SaveFileName = txtBoxOutputFolder.Text + "\\PurchaseOrder_" + SelectedDateTimeString + ".xlsx";
                        break;
                    default:
                        return;
                }
                Excel.Worksheet xlPOWorksheet = xlWorkbook.Sheets[1];

                #region Print PO Sheet for each Vendor
                Double Quantity;

                Int32 POHeaderStartRow = 0;
                Int32 POStartRow = POHeaderStartRow + 5;
                Int32 PONumber = CurrReportSettings.LastNumber;
                Int32 ValidVendorCount = ListVendorIndexes.Where(s => (s >= 0)).ToList().Count;
                Int32 ValidItemCount = ListItemIndexes.Where(s => (s >= 0)).ToList().Count;
                Int32 ProgressBarCount = (ValidVendorCount * ValidItemCount);
                Int32 Counter = 0;
                Int32 SlNoColNum = 1, ItemNameColNum = 2, OrdQtyColNum = 3, RecdQtyColNum = 4, PriceColNum = 5, TotalColNum = 6;
                Int32 ReportAppendRowsAtBottom = CommonFunctions.ObjApplicationSettings.ReportAppendRowsAtBottom;
                Int32 OrderTotalRowOffset = 1 + ReportAppendRowsAtBottom, DiscountRowOffset = 2 + ReportAppendRowsAtBottom, OldBalanceRowOffset = 3 + ReportAppendRowsAtBottom, TotalCostRowOffset = 4 + ReportAppendRowsAtBottom;
                Int32 VendorCount = 0, PODetailsCount = 5;
                for (int i = 0; i < ListVendorIndexes.Count; i++)
                {
                    if (ListVendorIndexes[i] < 0) continue;
                    VendorCount++;
                    lblStatus.Text = "Creating " + ReportTypeName + " for Vendor " + VendorCount + " of " + ValidVendorCount;
                    Excel.Worksheet xlWorkSheet = xlWorkbook.Worksheets.Add(Type.Missing, xlWorkbook.Sheets[xlWorkbook.Sheets.Count]);
                    String SheetName = drVendors[ListVendorIndexes[i]]["VendorName"].ToString().Replace(":", "").
                                            Replace("\\", "").Replace("/", "").
                                            Replace("?", "").Replace("*", "").
                                            Replace("[", "").Replace("]", "");
                    xlWorkSheet.Name = ((SheetName.Length > 30) ? SheetName.Substring(0, 30) : SheetName);

                    VendorDetails ObjCurrentVendor = CommonFunctions.ObjVendorMaster.GetVendorDetails(drVendors[ListVendorIndexes[i]]["VendorName"].ToString());

                    #region Print PO Items
                    Int32 SlNo = 0;

                    #region Print PO Header
                    Excel.Range xlRange = xlWorkSheet.Cells[1 + POHeaderStartRow, 1];

                    Int32 CustDetailsStartRow = 1 + POHeaderStartRow;
                    xlRange = xlWorkSheet.Cells[CustDetailsStartRow, SlNoColNum];
                    xlRange.Value = "Name";
                    xlRange.WrapText = true;
                    xlRange.Font.Bold = true;
                    xlWorkSheet.Cells[CustDetailsStartRow, SlNoColNum + 1].Value = drVendors[ListVendorIndexes[i]]["VendorName"].ToString();

                    xlRange = xlWorkSheet.Cells[CustDetailsStartRow + 1, SlNoColNum];
                    xlRange.Value = "Address";
                    xlRange.WrapText = true;
                    xlRange.Font.Bold = true;
                    xlWorkSheet.Cells[CustDetailsStartRow + 1, SlNoColNum + 1].Value = drVendors[ListVendorIndexes[i]]["Address"].ToString();
                    xlRange = xlWorkSheet.Cells[CustDetailsStartRow + 1, SlNoColNum + 1];
                    xlRange.WrapText = true;
                    if (drVendors[ListVendorIndexes[i]]["Address"].ToString().Length >= 25) xlRange.EntireColumn.ColumnWidth = 25;

                    xlRange = xlWorkSheet.Cells[CustDetailsStartRow + 2, SlNoColNum];
                    xlRange.Value = "TIN#";
                    xlRange.WrapText = true;
                    xlRange.Font.Bold = true;
                    xlWorkSheet.Cells[CustDetailsStartRow + 2, SlNoColNum + 1].Value = drVendors[ListVendorIndexes[i]]["TINNumber"].ToString();

                    xlRange = xlWorkSheet.Cells[CustDetailsStartRow + 3, SlNoColNum];
                    xlRange.Value = "Phone";
                    xlRange.Font.Bold = true;
                    xlWorkSheet.Cells[CustDetailsStartRow + 3, SlNoColNum + 1].Value = drVendors[ListVendorIndexes[i]]["Phone"].ToString();

                    xlRange = xlWorkSheet.Cells[CustDetailsStartRow, TotalColNum - 1];
                    xlRange.Value = "Date";
                    xlRange.Font.Bold = true;
                    xlWorkSheet.Cells[CustDetailsStartRow, TotalColNum].Value = DateTime.Today.ToString("dd-MMM-yyyy");

                    PONumber++;
                    xlRange = xlWorkSheet.Cells[1 + CustDetailsStartRow, TotalColNum - 1];
                    xlRange.Value = BillNumberText;
                    xlRange.Font.Bold = true;
                    xlWorkSheet.Cells[CustDetailsStartRow + 1, TotalColNum].Value = PONumber;
                    drVendors[ListVendorIndexes[i]]["PONumber"] = PONumber;

                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[CustDetailsStartRow, 1], xlWorkSheet.Cells[CustDetailsStartRow + 3, TotalColNum]];
                    SellerInvoiceForm.SetAllBorders(xlRange);
                    #endregion

                    xlWorkSheet.Cells[POStartRow + 1, SlNoColNum].Value = "Sl.No.";
                    xlWorkSheet.Cells[POStartRow + 1, ItemNameColNum].Value = "Item Name";
                    xlWorkSheet.Cells[POStartRow + 1, OrdQtyColNum].Value = "Order Quantity";
                    xlWorkSheet.Cells[POStartRow + 1, RecdQtyColNum].Value = "Received Quantity";
                    xlWorkSheet.Cells[POStartRow + 1, PriceColNum].Value = "Price";
                    xlWorkSheet.Cells[POStartRow + 1, TotalColNum].Value = "Total";
                    xlWorkSheet.Range[xlWorkSheet.Cells[POStartRow + 1, SlNoColNum], xlWorkSheet.Cells[POStartRow + 1, TotalColNum]].Font.Bold = true;

                    for (int j = 0; j < ListItemIndexes.Count; j++)
                    {
                        if (ListItemIndexes[j] < 0) continue;
                        Counter++;
                        ReportProgressFunc((Counter * 100) / ProgressBarCount);
                        if (xlPOWorksheet.Cells[StartRow + 1 + i, StartColumn + PODetailsCount + j].Value == null) continue;
                        if (String.IsNullOrEmpty(xlPOWorksheet.Cells[StartRow + 1 + i, StartColumn + PODetailsCount + j].Value.ToString())) continue;

                        Quantity = Double.Parse(xlPOWorksheet.Cells[StartRow + 1 + i, StartColumn + PODetailsCount + j].Value.ToString());
                        drItems[ListItemIndexes[j]]["Quantity"] = Double.Parse(drItems[ListItemIndexes[j]]["Quantity"].ToString()) + Quantity;

                        SlNo++;
                        xlWorkSheet.Cells[SlNo + POStartRow + 1, SlNoColNum].Value = SlNo;
                        xlWorkSheet.Cells[SlNo + POStartRow + 1, ItemNameColNum].Value = drItems[ListItemIndexes[j]]["StockName"].ToString();
                        xlWorkSheet.Cells[SlNo + POStartRow + 1, OrdQtyColNum].Value = Quantity;
                        if (chkBoxUseOrdQty.Checked == true) xlWorkSheet.Cells[SlNo + POStartRow + 1, RecdQtyColNum].Value = Quantity;
                        xlWorkSheet.Cells[SlNo + POStartRow + 1, PriceColNum].Value = CommonFunctions.ObjProductMaster.GetPriceForProduct(drItems[ListItemIndexes[j]]["ItemName"].ToString(), ObjCurrentVendor.PriceGroupIndex);
                        xlWorkSheet.Cells[SlNo + POStartRow + 1, PriceColNum].NumberFormat = "#,##0.00";
                        Excel.Range xlRangeSaleQty = xlWorkSheet.Cells[SlNo + POStartRow + 1, RecdQtyColNum];
                        Excel.Range xlRangePrice = xlWorkSheet.Cells[SlNo + POStartRow + 1, PriceColNum];
                        Excel.Range xlRangeTotal = xlWorkSheet.Cells[SlNo + POStartRow + 1, TotalColNum];
                        xlRangeTotal.Formula = "=(" + xlRangeSaleQty.Address[false, false] + "*" + xlRangePrice.Address[false, false] + ")";
                        xlWorkSheet.Cells[SlNo + POStartRow + 1, TotalColNum].NumberFormat = "#,##0.00";
                    }

                    Excel.Range xlRangeSaleQtyFrom = xlWorkSheet.Cells[1 + POStartRow + 1, RecdQtyColNum];
                    Excel.Range xlRangeSaleQtyTo = xlWorkSheet.Cells[SlNo + POStartRow + 1 + OrderTotalRowOffset - 1, RecdQtyColNum];
                    Excel.Range xlRangeTotalCost = xlWorkSheet.Cells[SlNo + POStartRow + 1 + TotalCostRowOffset, TotalColNum];
                    Excel.Range xlRangeSaleTotal = xlWorkSheet.Cells[SlNo + POStartRow + 1 + OrderTotalRowOffset, TotalColNum];
                    drVendors[ListVendorIndexes[i]]["Total"] = "='" + xlWorkSheet.Name + "'!" + xlRangeSaleTotal.Address[false, false];
                    drVendors[ListVendorIndexes[i]]["Quantity"] = "=Sum('" + xlWorkSheet.Name + "'!" + xlRangeSaleQtyFrom.Address[false, false] + ":" + xlRangeSaleQtyTo.Address[false, false] + ")";

                    #region Sales Total Row
                    xlRange = xlWorkSheet.Cells[SlNo + POStartRow + 1 + OrderTotalRowOffset, TotalColNum - 1];
                    xlRange.Value = "Sales Total";
                    xlRange.Font.Bold = true;

                    xlRange = xlWorkSheet.Cells[SlNo + POStartRow + 1 + OrderTotalRowOffset, TotalColNum];
                    Excel.Range xlRangeSalesTotalFrom = xlWorkSheet.Cells[1 + POStartRow + 1, TotalColNum];
                    Excel.Range xlRangeSalesTotalTo = xlWorkSheet.Cells[SlNo + POStartRow + 1 + OrderTotalRowOffset - 1, TotalColNum];
                    xlRange.Formula = "=Sum(" + xlRangeSalesTotalFrom.Address[false, false] + ":" + xlRangeSalesTotalTo.Address[false, false] + ")";
                    xlRange.Font.Bold = true;
                    xlRange.NumberFormat = "#,##0.00";

                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[SlNo + POStartRow + 1 + OrderTotalRowOffset, 1], xlWorkSheet.Cells[SlNo + POStartRow + 1 + OrderTotalRowOffset, TotalColNum - 1]];
                    xlRange.Font.Bold = true;
                    xlRange.Merge();
                    xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    #endregion

                    #region Discount Row
                    xlRange = xlWorkSheet.Cells[SlNo + POStartRow + 1 + DiscountRowOffset, TotalColNum - 1];
                    xlRange.Value = "Discount";
                    xlRange.Font.Bold = true;

                    DiscountGroupDetails ObjDiscountGroup = CommonFunctions.ObjVendorMaster.GetVendorDiscount(ObjCurrentVendor.VendorName);

                    xlRange = xlWorkSheet.Cells[SlNo + POStartRow + 1 + DiscountRowOffset, TotalColNum];
                    Excel.Range xlSalesTotal1 = xlWorkSheet.Cells[SlNo + POStartRow + 1 + OrderTotalRowOffset, TotalColNum];
                    if (ObjDiscountGroup.DiscountType == DiscountTypes.PERCENT)
                        xlRange.Formula = "=" + xlSalesTotal1.Address[false, false] + "*" + ObjDiscountGroup.Discount + "/100";
                    else if (ObjDiscountGroup.DiscountType == DiscountTypes.ABSOLUTE)
                        xlRange.Value = ObjDiscountGroup.Discount;
                    else
                        xlRange.Formula = "=" + xlSalesTotal1.Address[false, false];
                    xlRange.Font.Bold = true;
                    xlRange.NumberFormat = "#,##0.00";
                    drVendors[ListVendorIndexes[i]]["TotalDiscount"] = "='" + xlWorkSheet.Name + "'!" + xlRange.Address[false, false];

                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[SlNo + POStartRow + 1 + DiscountRowOffset, 1], xlWorkSheet.Cells[SlNo + POStartRow + 1 + DiscountRowOffset, TotalColNum - 1]];
                    xlRange.Font.Bold = true;
                    xlRange.Merge();
                    xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    #endregion

                    if (PrintVATPercent)
                    {
                        #region VAT Percent Row
                        xlRange = xlWorkSheet.Cells[SlNo + POStartRow + 1 + OldBalanceRowOffset, TotalColNum - 1];
                        xlRange.Value = "VAT Percent " + CurrReportSettings.VATPercent + "%";
                        xlRange.Font.Bold = true;

                        xlRange = xlWorkSheet.Cells[SlNo + POStartRow + 1 + OldBalanceRowOffset, TotalColNum];
                        Excel.Range xlSalesTotal = xlWorkSheet.Cells[SlNo + POStartRow + 1 + OrderTotalRowOffset, TotalColNum];
                        Excel.Range xlDiscount = xlWorkSheet.Cells[SlNo + POStartRow + 1 + DiscountRowOffset, TotalColNum];
                        xlRange.Formula = "=(" + xlSalesTotal.Address[false, false] + "-" + xlDiscount.Address[false, false] + ")*" + CurrReportSettings.VATPercent + "/100";
                        xlRange.Font.Bold = true;
                        xlRange.NumberFormat = "#,##0.00";
                        drVendors[ListVendorIndexes[i]]["TotalTax"] = "='" + xlWorkSheet.Name + "'!" + xlRange.Address[false, false];

                        xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[SlNo + POStartRow + 1 + OldBalanceRowOffset, 1], xlWorkSheet.Cells[SlNo + POStartRow + 1 + OldBalanceRowOffset, TotalColNum - 1]];
                        xlRange.Font.Bold = true;
                        xlRange.Merge();
                        xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                        #endregion
                    }

                    #region Total Cost Row
                    xlRange = xlWorkSheet.Cells[SlNo + POStartRow + 1 + TotalCostRowOffset, TotalColNum - 1];
                    xlRange.Value = "Total";
                    xlRange.Font.Bold = true;

                    xlRange = xlWorkSheet.Cells[SlNo + POStartRow + 1 + TotalCostRowOffset, TotalColNum];
                    Excel.Range xlRangeSalesTotal = xlWorkSheet.Cells[SlNo + POStartRow + 1 + OrderTotalRowOffset, TotalColNum];
                    Excel.Range xlRangeDiscount = xlWorkSheet.Cells[SlNo + POStartRow + 1 + DiscountRowOffset, TotalColNum];
                    Excel.Range xlRangeOldBalance = xlWorkSheet.Cells[SlNo + POStartRow + 1 + OldBalanceRowOffset, TotalColNum];
                    xlRange.Formula = "=Round(" + xlRangeSalesTotal.Address[false, false] + "+" + xlRangeOldBalance.Address[false, false] + "-" + xlRangeDiscount.Address[false, false] + ", 0)";
                    xlRange.Font.Bold = true;
                    xlRange.NumberFormat = "#,##0.00";

                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[SlNo + POStartRow + 1 + TotalCostRowOffset, 1], xlWorkSheet.Cells[SlNo + POStartRow + 1 + TotalCostRowOffset, TotalColNum - 1]];
                    xlRange.Font.Bold = true;
                    xlRange.Merge();
                    xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    #endregion

                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[POStartRow + 1, 1], xlWorkSheet.Cells[SlNo + POStartRow + 1 + TotalCostRowOffset, TotalColNum]];
                    SellerInvoiceForm.SetAllBorders(xlRange);
                    #endregion

                    xlWorkSheet.UsedRange.Columns.AutoFit();
                    SellerInvoiceForm.AddPageHeaderAndFooter(ref xlWorkSheet, CurrReportSettings.HeaderSubTitle, CurrReportSettings);
                }
                #endregion

                if (chkBoxCreateSummary.Checked)
                {
                    CreateVendorSummarySheet(drVendors, xlWorkbook, CurrReportSettings);
                }

                xlApp.DisplayAlerts = false;
                xlWorkbook.Sheets[SelectedDateTimeString].Delete();
                xlApp.DisplayAlerts = true;
                Excel.Worksheet FirstWorksheet = xlWorkbook.Sheets[1];
                FirstWorksheet.Select();

                #region Write PONumber to Settings File
                CurrReportSettings.LastNumber = PONumber;
                #endregion

                ReportProgressFunc(100);
                xlWorkbook.SaveAs(SaveFileName);
                xlWorkbook.Close();
                lblStatus.Text = "Completed creation of " + ReportTypeName + "s for all Vendors";

                CommonFunctions.ReleaseCOMObject(xlWorkbook);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorPurchaseOrderForm.CreateVendorPurchaseOrder", ex);
                xlApp.Quit();
                CommonFunctions.ReleaseCOMObject(xlApp);
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
                xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 2].Value = "Stock Name";
                xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 3].Value = "Vendor Name";
                xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 4].Value = "Quantity";
                xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 5].Value = "Price";
                xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 6].Value = "Total";
                Excel.Range xlRange1 = xlSummaryWorkSheet.Range[xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 1], xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 6]];
                xlRange1.Font.Bold = true;

                HashSet<String> ListItems = new HashSet<String>();
                for (int i = 0, j = 0; i < drItems.Length; i++)
                {
                    if (ListItems.Contains(drItems[i]["StockName"].ToString())) continue;
                    xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 1].Value = (j + 1);
                    xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 2].Value = drItems[i]["StockName"].ToString();
                    xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 3].Value = drItems[i]["VendorName"].ToString();
                    xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 4].Value = drItems[i]["Quantity"].ToString();
                    xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 5].Value = drItems[i]["PurchasePrice"].ToString();
                    xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 5].NumberFormat = "#,##0.00";
                    xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 6].Value = Double.Parse(drItems[i]["Quantity"].ToString()) * Double.Parse(drItems[i]["PurchasePrice"].ToString());
                    xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 6].NumberFormat = "#,##0.00";
                    Total += Double.Parse(xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 6].Value.ToString());
                    j++;
                    ListItems.Add(drItems[i]["StockName"].ToString());
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
                SellerInvoiceForm.AddPageHeaderAndFooter(ref xlSummaryWorkSheet, "Itemwise Summary", CurrReportSettings);
                xlApp.DisplayAlerts = true;
                #endregion
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorPurchaseOrderForm.CreateItemSummarySheet()", ex);
                xlApp.Quit();
                CommonFunctions.ReleaseCOMObject(xlApp);
            }
        }

        private void CreateVendorSummarySheet(DataRow[] drVendors, Excel.Workbook xlWorkbook, ReportSettings CurrReportSettings)
        {
            try
            {
                lblStatus.Text = "Creating Vendor Summary Sheet";
                #region Print Vendor Summary Sheet
                Int32 SummaryStartRow = 0, CurrRow = 0, CurrCol = 0;
                Excel.Worksheet xlVendorSummaryWorkSheet = xlWorkbook.Worksheets.Add(xlWorkbook.Sheets[1]);
                xlVendorSummaryWorkSheet.Name = "Vendor Summary";

                SummaryStartRow++;
                Excel.Range xlRange1 = xlVendorSummaryWorkSheet.Cells[SummaryStartRow, 1];
                xlRange1.Value = "Date";
                xlRange1.Font.Bold = true;
                xlRange1 = xlVendorSummaryWorkSheet.Cells[SummaryStartRow, 2];
                xlRange1.Value = DateTime.Today.ToString("dd-MMM-yyyy");
                xlRange1 = xlVendorSummaryWorkSheet.Range[xlVendorSummaryWorkSheet.Cells[SummaryStartRow, 2], xlVendorSummaryWorkSheet.Cells[SummaryStartRow, 3]];
                xlRange1.Merge();
                xlRange1.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

                CurrRow = SummaryStartRow + 1;
                CurrCol++; xlVendorSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Sl#";
                CurrCol++; xlVendorSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Bill#";
                CurrCol++; xlVendorSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Vendor Name";
                CurrCol++; xlVendorSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Purchase";
                CurrCol++; xlVendorSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Cancel";
                CurrCol++; xlVendorSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Return";
                CurrCol++; xlVendorSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Discount";
                CurrCol++; xlVendorSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Total Tax";
                CurrCol++; xlVendorSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Net Sale";
                CurrCol++; xlVendorSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Cash";
                Int32 LastCol = CurrCol;
                xlRange1 = xlVendorSummaryWorkSheet.Range[xlVendorSummaryWorkSheet.Cells[CurrRow, 1], xlVendorSummaryWorkSheet.Cells[CurrRow, LastCol]];
                xlRange1.Font.Bold = true;

                Int32 VendorsCount = 0;
                for (int i = 0; i < drVendors.Length; i++)
                {
                    if (String.IsNullOrEmpty(drVendors[i]["PONumber"].ToString().Trim())) continue;
                    VendorsCount++;

                    CurrRow = VendorsCount + SummaryStartRow + 1;
                    CurrCol = 0;
                    CurrCol++; xlVendorSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = VendorsCount;
                    CurrCol++; xlVendorSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = drVendors[i]["PONumber"].ToString();
                    CurrCol++; xlVendorSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = drVendors[i]["VendorName"].ToString();
                    CurrCol++; Excel.Range xlRangePurchase = xlVendorSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    xlRangePurchase.Formula = drVendors[i]["Total"].ToString();
                    xlRangePurchase.NumberFormat = "#,##0.00";
                    CurrCol++; Excel.Range xlRangeCancel = xlVendorSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    CurrCol++; Excel.Range xlRangeReturn = xlVendorSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    CurrCol++; Excel.Range xlRangeDiscount = xlVendorSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    CurrCol++; Excel.Range xlRangeTotalTax = xlVendorSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    CurrCol++; Excel.Range xlRangeNetSale = xlVendorSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    xlRangeDiscount.Formula = drVendors[i]["TotalDiscount"].ToString();
                    if (CommonFunctions.ObjGeneralSettings.SummaryLocation == 0) xlRangeTotalTax.Formula = drVendors[i]["TotalTax"].ToString();
                    xlRangeNetSale.Formula = "=Round(" + xlRangePurchase.Address[false, false]
                                                + "-" + xlRangeCancel.Address[false, false] 
                                                + "-" + xlRangeReturn.Address[false, false] 
                                                + "-" + xlRangeDiscount.Address[false, false]
                                                + "+" + xlRangeTotalTax.Address[false, false] + ", 0)";
                    xlRangeNetSale.NumberFormat = "#,##0.00";
                    CurrCol++; Excel.Range xlRangeCash = xlVendorSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    xlRangeCash.NumberFormat = "#,##0.00";
                }
                CurrRow = VendorsCount + SummaryStartRow + 2;
                Excel.Range xlRange = xlVendorSummaryWorkSheet.Cells[CurrRow, 3];
                xlRange.Value = "Total";
                xlRange.Font.Bold = true;
                xlRange.Font.Color = Color.Red;

                for (int i = 4; i <= LastCol; i++)
                {
                    CurrCol = i;
                    Excel.Range xlRangeTotal = xlVendorSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    Excel.Range xlRangeTotalFrom = xlVendorSummaryWorkSheet.Cells[SummaryStartRow + 2, CurrCol];
                    Excel.Range xlRangeTotalTo = xlVendorSummaryWorkSheet.Cells[CurrRow - 1, CurrCol];
                    xlRangeTotal.Formula = "=Sum(" + xlRangeTotalFrom.Address[false, false] + ":" + xlRangeTotalTo.Address[false, false] + ")";
                    xlRangeTotal.NumberFormat = "#,##0.00";
                    xlRangeTotal.Font.Bold = true;
                }

                xlRange = xlVendorSummaryWorkSheet.Range[xlVendorSummaryWorkSheet.Cells[SummaryStartRow + 1, 1], xlVendorSummaryWorkSheet.Cells[CurrRow + 1, LastCol]];
                SellerInvoiceForm.SetAllBorders(xlRange);

                xlVendorSummaryWorkSheet.UsedRange.Columns.AutoFit();

                xlRange = xlVendorSummaryWorkSheet.Columns["B"];
                xlRange.ColumnWidth = 7;
                xlRange = xlVendorSummaryWorkSheet.Columns["C"];
                xlRange.ColumnWidth = 24;

                SellerInvoiceForm.AddPageHeaderAndFooter(ref xlVendorSummaryWorkSheet, "Vendorwise Summary", CurrReportSettings);
                #endregion
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorPurchaseOrderForm.CreateVendorSummarySheet()", ex);
                xlApp.Quit();
                CommonFunctions.ReleaseCOMObject(xlApp);
            }
        }

        public void UpdateSelectedVendorsList()
        {
            try
            {
                listBoxVendors.Items.Clear();
                for (int i = 0; i < CommonFunctions.ListSelectedVendors.Count; i++)
                {
                    listBoxVendors.Items.Add(CommonFunctions.ListSelectedVendors[i]);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorPurchaseOrderForm.UpdateSelectedVendorsList()", ex);
            }
        }

        private void VendorPurchaseOrderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommonFunctions.WriteToSettingsFile();
        }
    }
}
