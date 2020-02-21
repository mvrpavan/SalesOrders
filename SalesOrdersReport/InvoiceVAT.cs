using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;

namespace SalesOrdersReport
{
    class InvoiceVAT : Invoice
    {
        public override void CreateInvoice(Excel.Worksheet xlWorkSheet)
        {
            try
            {
                String SheetName = this.SheetName;
                if (String.IsNullOrEmpty(SheetName))
                {
                    SheetName = ObjSellerDetails.Name.Replace(":", "").
                                        Replace("\\", "").Replace("/", "").
                                        Replace("?", "").Replace("*", "").
                                        Replace("[", "").Replace("]", "");
                }
                xlWorkSheet.Name = ((SheetName.Length > 30) ? SheetName.Substring(0, 30) : SheetName);

                Int32 InvoiceHeaderStartRow = 0;
                Int32 InvoiceStartRow = InvoiceHeaderStartRow + 5;
                Excel.Range xlRange = null;

                #region Print Invoice Items
                Int32 SlNo = 0;
                Int32 SlNoColNum = 1, ItemNameColNum = 2, OrdQtyColNum = 3, SalQtyColNum = 4, PriceColNum = 5, TotalColNum = 6;
                Int32 ReportAppendRowsAtBottom = CommonFunctions.ObjApplicationSettings.ReportAppendRowsAtBottom;
                Int32 SalesTotalRowOffset = 1 + ReportAppendRowsAtBottom, DiscountRowOffset = 2 + ReportAppendRowsAtBottom, OldBalanceRowOffset = 3 + ReportAppendRowsAtBottom, TotalCostRowOffset = 4 + ReportAppendRowsAtBottom;

                #region Print Invoice Header
                xlRange = xlWorkSheet.Cells[1 + InvoiceHeaderStartRow, 1];

                Int32 CustDetailsStartRow = 1 + InvoiceHeaderStartRow;
                xlRange = xlWorkSheet.Cells[CustDetailsStartRow, SlNoColNum];
                xlRange.Value = "Name";
                xlRange.WrapText = true;
                xlRange.Font.Bold = true;
                xlWorkSheet.Cells[CustDetailsStartRow, SlNoColNum + 1].Value = ObjSellerDetails.Name;

                xlRange = xlWorkSheet.Cells[CustDetailsStartRow + 1, SlNoColNum];
                xlRange.Value = "Address";
                xlRange.WrapText = true;
                xlRange.Font.Bold = true;
                xlWorkSheet.Cells[CustDetailsStartRow + 1, SlNoColNum + 1].Value = ObjSellerDetails.Address;
                xlRange = xlWorkSheet.Cells[CustDetailsStartRow + 1, SlNoColNum + 1];
                xlRange.WrapText = true;
                if (ObjSellerDetails.Address.Length >= 25) xlRange.EntireColumn.ColumnWidth = 25;

                if (CurrReportSettings.Type == ReportType.INVOICE)
                {
                    xlRange = xlWorkSheet.Cells[CustDetailsStartRow + 2, SlNoColNum];
                    xlRange.Value = "TIN#";
                    xlRange.WrapText = true;
                    xlRange.Font.Bold = true;
                    xlWorkSheet.Cells[CustDetailsStartRow + 2, SlNoColNum + 1].Value = ObjSellerDetails.TINNumber;

                    xlRange = xlWorkSheet.Cells[CustDetailsStartRow + 3, SlNoColNum];
                    xlRange.Value = "Phone";
                    xlRange.Font.Bold = true;
                    xlWorkSheet.Cells[CustDetailsStartRow + 3, SlNoColNum + 1].Value = ObjSellerDetails.Phone;
                }
                else
                {
                    xlRange = xlWorkSheet.Cells[CustDetailsStartRow + 2, SlNoColNum];
                    xlRange.Value = "Phone";
                    xlRange.Font.Bold = true;
                    xlWorkSheet.Cells[CustDetailsStartRow + 2, SlNoColNum + 1].Value = ObjSellerDetails.Phone;
                }

                xlRange = xlWorkSheet.Cells[CustDetailsStartRow, TotalColNum - 1];
                xlRange.Value = "Date";
                xlRange.Font.Bold = true;
                xlWorkSheet.Cells[CustDetailsStartRow, TotalColNum].Value = DateOfInvoice.ToString("dd-MMM-yyyy");

                xlRange = xlWorkSheet.Cells[1 + CustDetailsStartRow, TotalColNum - 1];
                xlRange.Value = InvoiceNumberText;
                xlRange.Font.Bold = true;
                xlWorkSheet.Cells[CustDetailsStartRow + 1, TotalColNum].Value = SerialNumber;

                xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[CustDetailsStartRow, 1], xlWorkSheet.Cells[CustDetailsStartRow + 3, TotalColNum]];
                SellerInvoiceForm.SetAllBorders(xlRange);
                #endregion

                xlWorkSheet.Cells[InvoiceStartRow + 1, SlNoColNum].Value = "Sl No";
                xlWorkSheet.Cells[InvoiceStartRow + 1, ItemNameColNum].Value = "Item Name";
                xlWorkSheet.Cells[InvoiceStartRow + 1, OrdQtyColNum].Value = "Order Quantity";
                xlWorkSheet.Cells[InvoiceStartRow + 1, SalQtyColNum].Value = "Sales Quantity";
                xlWorkSheet.Cells[InvoiceStartRow + 1, PriceColNum].Value = "Price";
                xlWorkSheet.Cells[InvoiceStartRow + 1, TotalColNum].Value = "Total";
                xlWorkSheet.Range[xlWorkSheet.Cells[InvoiceStartRow + 1, SlNoColNum], xlWorkSheet.Cells[InvoiceStartRow + 1, TotalColNum]].Font.Bold = true;

                for (int i = 0; i < ListProducts.Count; i++)
                {
                    ProductDetailsForInvoice CurrProd = ListProducts[i];

                    SlNo++;
                    xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, SlNoColNum].Value = SlNo;
                    xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, ItemNameColNum].Value = CurrProd.Description;
                    xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, OrdQtyColNum].NumberFormat = "@";
                    xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, OrdQtyColNum].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, OrdQtyColNum].Value = CurrProd.OrderQuantity;
                    xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, SalQtyColNum].Value = CurrProd.SaleQuantity;
                    xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1, PriceColNum].Value = CurrProd.Rate * (1 + CurrProd.CGSTDetails.TaxRate + CurrProd.SGSTDetails.TaxRate);
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
                if (CurrReportSettings.Type == ReportType.INVOICE)
                    TotalSalesValue = "='" + xlWorkSheet.Name + "'!" + xlRangeSaleTotal.Address[false, false];
                else if (CurrReportSettings.Type == ReportType.QUOTATION)
                    TotalSalesValue = "='" + xlWorkSheet.Name + "'!" + xlRangeSaleTotal.Address[false, false];

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

                DiscountGroupDetails ObjDiscountGroup = CommonFunctions.ObjSellerMaster.GetSellerDiscount(ObjSellerDetails.Name);

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
                TotalDiscount = "='" + xlWorkSheet.Name + "'!" + xlRange.Address[false, false];

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
                    xlRange.Value = ObjSellerDetails.OldBalance;
                    xlRange.Font.Bold = true;
                    xlRange.NumberFormat = "#,##0.00";

                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + OldBalanceRowOffset, 1], xlWorkSheet.Cells[SlNo + InvoiceStartRow + 1 + OldBalanceRowOffset, TotalColNum - 1]];
                    xlRange.Font.Bold = true;
                    xlRange.Merge();
                    xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    #endregion
                }

                if (CurrReportSettings.Type == ReportType.INVOICE)
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
                    TotalTax = "='" + xlWorkSheet.Name + "'!" + xlRange.Address[false, false];

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
                SellerInvoiceForm.SetAllBorders(xlRange);
                #endregion

                xlWorkSheet.UsedRange.Columns.AutoFit();
                SellerInvoiceForm.AddPageHeaderAndFooter(ref xlWorkSheet, CurrReportSettings.HeaderSubTitle, CurrReportSettings);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("InvoiceVAT.CreateInvoice()", ex);
                throw;
            }
        }

        public override DataTable LoadInvoice(String SheetName, String ExcelWorkbookPath)
        {
            try
            {
                DataTable dtSellerInvoice = CommonFunctions.ReturnDataTableFromExcelWorksheet(SheetName, ExcelWorkbookPath, "*", "A6:F");
                if (dtSellerInvoice == null) return null;

                dtSellerInvoice.Columns.Add(new DataColumn("TotalTax", Type.GetType("System.Double"), "0"));
                dtSellerInvoice.Columns.Add(new DataColumn("Discount", Type.GetType("System.Double"), "0"));
                dtSellerInvoice.DefaultView.RowFilter = "IsNull([Sl No], 0) > 0";

                return dtSellerInvoice;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("InvoiceVAT.LoadInvoice()", ex);
                throw;
            }
        }
    }
}
