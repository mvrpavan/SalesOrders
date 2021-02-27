using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Views;

namespace SalesOrdersReport.Models
{
    class InvoiceGST : Invoice
    {
        public override void CreateInvoice(Excel.Worksheet xlWorkSheet)
        {
            try
            {
                String SheetName = this.SheetName;
                if (String.IsNullOrEmpty(SheetName))
                {
                    SheetName = ObjSellerDetails.CustomerName.Replace(":", "").Replace("\\", "").Replace("/", "").
                                            Replace("?", "").Replace("*", "").Replace("[", "").Replace("]", "");
                }
                xlWorkSheet.Name = ((SheetName.Length > 30) ? SheetName.Substring(0, 30) : SheetName);

                Int32 InvoiceHeaderStartRow = 0, InvoiceStartCol = 1;
                Excel.Range xlRange = null;

                #region Print Invoice Header
                #region Print Seller Details
                Int32 CustDetailsStartRow = 1 + InvoiceHeaderStartRow, CustDetailsStartCol = InvoiceStartCol;
                xlRange = xlWorkSheet.Cells[CustDetailsStartRow, CustDetailsStartCol];
                xlRange.Value = "Details of Receiver";
                xlRange.Font.Bold = true;
                xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[CustDetailsStartRow, CustDetailsStartCol], xlWorkSheet.Cells[CustDetailsStartRow, CustDetailsStartCol + 6]];
                xlRange.Merge();

                xlRange = xlWorkSheet.Cells[CustDetailsStartRow + 1, CustDetailsStartCol];
                xlRange.Value = "Name";
                xlWorkSheet.Cells[CustDetailsStartRow + 1, CustDetailsStartCol + 2].Value = ObjSellerDetails.CustomerName;

                xlRange = xlWorkSheet.Cells[CustDetailsStartRow + 2, CustDetailsStartCol];
                xlRange.Value = "Address";
                xlRange = xlWorkSheet.Cells[CustDetailsStartRow + 2, CustDetailsStartCol + 2];
                xlRange.Value = ObjSellerDetails.Address;
                xlRange.WrapText = true;

                xlRange = xlWorkSheet.Cells[CustDetailsStartRow + 3, CustDetailsStartCol];
                xlRange.Value = "State";
                xlWorkSheet.Cells[CustDetailsStartRow + 3, CustDetailsStartCol + 2].Value = ObjSellerDetails.State;

                xlRange = xlWorkSheet.Cells[CustDetailsStartRow + 4, CustDetailsStartCol];
                xlRange.Value = "State code";
                xlWorkSheet.Cells[CustDetailsStartRow + 4, CustDetailsStartCol + 2].Value = ObjSellerDetails.StateCode;

                xlRange = xlWorkSheet.Cells[CustDetailsStartRow + 5, CustDetailsStartCol];
                xlRange.Value = "GSTIN";
                xlWorkSheet.Cells[CustDetailsStartRow + 5, CustDetailsStartCol + 2].Value = ObjSellerDetails.GSTIN;

                for (int i = 0; i < 5; i++)
                {
                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[CustDetailsStartRow + 1 + i, CustDetailsStartCol], xlWorkSheet.Cells[CustDetailsStartRow + 1 + i, CustDetailsStartCol + 1]];
                    xlRange.Merge();
                    xlRange.Font.Bold = true;
                    xlRange.Font.Name = "Calibri";
                    xlRange.Font.Size = 10;
                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[CustDetailsStartRow + 1 + i, CustDetailsStartCol + 2], xlWorkSheet.Cells[CustDetailsStartRow + 1 + i, CustDetailsStartCol + 6]];
                    xlRange.Merge();
                }
                #endregion

                #region Print Date & Invoice#
                xlWorkSheet.Cells[CustDetailsStartRow, CustDetailsStartCol + 7].Value = "Date";
                xlWorkSheet.Cells[CustDetailsStartRow, CustDetailsStartCol + 9].Value = DateOfInvoice.ToString("dd-MMM-yyyy");

                xlWorkSheet.Cells[CustDetailsStartRow + 1, CustDetailsStartCol + 7].Value = InvoiceNumberText;
                xlWorkSheet.Cells[CustDetailsStartRow + 1, CustDetailsStartCol + 9].Value = SerialNumber;

                for (int i = 0; i < 2; i++)
                {
                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[CustDetailsStartRow + i, CustDetailsStartCol + 7], xlWorkSheet.Cells[CustDetailsStartRow + i, CustDetailsStartCol + 8]];
                    xlRange.Merge();
                    xlRange.Font.Bold = true;
                    xlRange.Font.Name = "Calibri";
                    xlRange.Font.Size = 10;
                    xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[CustDetailsStartRow + i, CustDetailsStartCol + 9], xlWorkSheet.Cells[CustDetailsStartRow + i, CustDetailsStartCol + 13]];
                    xlRange.Merge();
                    xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                }

                xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[CustDetailsStartRow + 2, CustDetailsStartCol + 7], xlWorkSheet.Cells[CustDetailsStartRow + 5, CustDetailsStartCol + 13]];
                xlRange.Merge();

                xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[CustDetailsStartRow + 6, CustDetailsStartCol], xlWorkSheet.Cells[CustDetailsStartRow + 6, CustDetailsStartCol + 13]];
                xlRange.Merge();
                #endregion
                #endregion

                #region Item Details
                Int32 InvoiceStartRow = InvoiceHeaderStartRow + 8;

                #region Item Details Header
                Dictionary<String, Int32> DictColNumbers = new Dictionary<String, Int32>();
                String SlNoCol = "SL NO", ItemNameCol = "Description of Goods", HSNCol = "HSN Code", OrderQtyCol = "Ord Qty", SaleQtyCol = "Sale Qty", UnitsCol = "Units", RateCol = "Rate";
                String ItemTotalCol = "Total", ItemDiscCol = "Discount", ItemTaxableValueCol = "Taxable Value";
                String[] ArrHeader = new String[] { SlNoCol, ItemNameCol, HSNCol, OrderQtyCol, SaleQtyCol, UnitsCol, RateCol, ItemTotalCol, ItemDiscCol, ItemTaxableValueCol };
                for (int i = 0; i < ArrHeader.Length; i++)
                {
                    DictColNumbers.Add(ArrHeader[i], InvoiceStartCol + i);
                }

                String[] ArrTaxesHeader = new String[] { "CGSTRate", "CGSTAmount", "SGSTRate", "SGSTAmount"/*, "IGSTRate", "IGSTAmount"*/ };
                for (int i = 0; i < ArrTaxesHeader.Length; i++)
                {
                    DictColNumbers.Add(ArrTaxesHeader[i], InvoiceStartCol + ArrHeader.Length + i);
                }

                for (int i = 0; i < ArrHeader.Length; i++)
                {
                    xlWorkSheet.Cells[InvoiceStartRow, DictColNumbers[ArrHeader[i]]].Value = ArrHeader[i];
                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[InvoiceStartRow, DictColNumbers[ArrHeader[i]]], xlWorkSheet.Cells[InvoiceStartRow + 1, DictColNumbers[ArrHeader[i]]]];
                    xlRange.Merge();
                    xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                }

                for (int i = 0; i < ArrTaxesHeader.Length; i += 2)
                {
                    xlWorkSheet.Cells[InvoiceStartRow, DictColNumbers[ArrTaxesHeader[i]]].Value = ArrTaxesHeader[i].Replace("Rate", "");
                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[InvoiceStartRow, DictColNumbers[ArrTaxesHeader[i]]], xlWorkSheet.Cells[InvoiceStartRow, DictColNumbers[ArrTaxesHeader[i + 1]]]];
                    xlRange.Merge();
                    xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                    xlWorkSheet.Cells[InvoiceStartRow + 1, DictColNumbers[ArrTaxesHeader[i]]].Value = "Rate";
                    xlWorkSheet.Cells[InvoiceStartRow + 1, DictColNumbers[ArrTaxesHeader[i + 1]]].Value = "Amount";
                }

                xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[InvoiceStartRow, DictColNumbers[SlNoCol]], xlWorkSheet.Cells[InvoiceStartRow + 1, DictColNumbers[ArrTaxesHeader[ArrTaxesHeader.Length - 1]]]];
                xlRange.Font.Bold = true;
                xlRange.WrapText = true;
                #endregion

                Int32 ItemDetailsStartRow = InvoiceStartRow + 2;
                for (int i = 0; i < ListProducts.Count; i++)
                {
                    ProductDetailsForInvoice CurrProd = ListProducts[i];
                    xlWorkSheet.Cells[ItemDetailsStartRow + i, DictColNumbers[SlNoCol]].Value = CurrProd.SerialNumber;
                    xlRange = xlWorkSheet.Cells[ItemDetailsStartRow + i, DictColNumbers[ItemNameCol]];
                    xlRange.Value = CurrProd.Description; xlRange.WrapText = true;
                    xlWorkSheet.Cells[ItemDetailsStartRow + i, DictColNumbers[HSNCol]].Value = CurrProd.HSNCode;
                    xlWorkSheet.Cells[ItemDetailsStartRow + i, DictColNumbers[OrderQtyCol]].NumberFormat = "@";
                    xlWorkSheet.Cells[ItemDetailsStartRow + i, DictColNumbers[OrderQtyCol]].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlWorkSheet.Cells[ItemDetailsStartRow + i, DictColNumbers[OrderQtyCol]].Value = CurrProd.OrderQuantity;
                    xlWorkSheet.Cells[ItemDetailsStartRow + i, DictColNumbers[SaleQtyCol]].Value = CurrProd.SaleQuantity;
                    xlRange = xlWorkSheet.Cells[ItemDetailsStartRow + i, DictColNumbers[UnitsCol]];
                    xlRange.Value = CurrProd.UnitsOfMeasurement;
                    xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlRange = xlWorkSheet.Cells[ItemDetailsStartRow + i, DictColNumbers[RateCol]];
                    xlRange.Value = CurrProd.Rate; xlRange.NumberFormat = "#,##0.00";

                    xlRange = xlWorkSheet.Cells[ItemDetailsStartRow + i, DictColNumbers[ItemTotalCol]];
                    xlRange.NumberFormat = "#,##0.00";
                    Excel.Range xlRangeSaleQty = xlWorkSheet.Cells[ItemDetailsStartRow + i, DictColNumbers[SaleQtyCol]];
                    Excel.Range xlRangePrice = xlWorkSheet.Cells[ItemDetailsStartRow + i, DictColNumbers[RateCol]];
                    xlRange.Formula = "=(" + xlRangeSaleQty.Address[false, false] + "*" + xlRangePrice.Address[false, false] + ")";

                    xlRange = xlWorkSheet.Cells[ItemDetailsStartRow + i, DictColNumbers[ItemDiscCol]];
                    xlRange.NumberFormat = "#,##0.00";
                    Excel.Range xlRangeTotal = xlWorkSheet.Cells[ItemDetailsStartRow + i, DictColNumbers[ItemTotalCol]];
                    if (CurrProd.DiscountGroup.DiscountType == DiscountTypes.PERCENT)
                        xlRange.Formula = "=" + xlRangeTotal.Address[false, false] + "*" + CurrProd.DiscountGroup.Discount + "/100";
                    else if (CurrProd.DiscountGroup.DiscountType == DiscountTypes.ABSOLUTE)
                        xlRange.Value = CurrProd.DiscountGroup.Discount;
                    else
                        xlRange.Formula = "=" + xlRangeTotal.Address[false, false];

                    TaxDetails[] ArrTaxDetails = new TaxDetails[] { CurrProd.CGSTDetails, CurrProd.SGSTDetails, CurrProd.IGSTDetails };
                    xlRange = xlWorkSheet.Cells[ItemDetailsStartRow + i, DictColNumbers[ItemTaxableValueCol]];
                    xlRange.NumberFormat = "#,##0.00";
                    Excel.Range xlRangeDiscount = xlWorkSheet.Cells[ItemDetailsStartRow + i, DictColNumbers[ItemDiscCol]];
                    //xlRange.Formula = "=(" + xlRangeTotal.Address[false, false] + "-" + xlRangeDiscount.Address[false, false] + $")/{1 + ArrTaxDetails.Sum(e => e.TaxRate)}";
                    xlRange.Formula = "=(" + xlRangeTotal.Address[false, false] + "-" + xlRangeDiscount.Address[false, false] + ")";

                    for (int j = 0; j < ArrTaxesHeader.Length; j += 2)
                    {
                        xlRange = xlWorkSheet.Cells[ItemDetailsStartRow + i, DictColNumbers[ArrTaxesHeader[j]]];
                        xlRange.Value = ArrTaxDetails[j / 2].TaxRate; xlRange.NumberFormat = "#0.0%";

                        xlRange = xlWorkSheet.Cells[ItemDetailsStartRow + i, DictColNumbers[ArrTaxesHeader[j + 1]]];
                        xlRange.NumberFormat = "#,##0.00";
                        Excel.Range xlRangeTaxableValue = xlWorkSheet.Cells[ItemDetailsStartRow + i, DictColNumbers[ItemTaxableValueCol]];
                        Excel.Range xlRangeRate = xlWorkSheet.Cells[ItemDetailsStartRow + i, DictColNumbers[ArrTaxesHeader[j]]];
                        xlRange.Formula = "=(" + xlRangeTaxableValue.Address[false, false] + "*" + xlRangeRate.Address[false, false] + ")";
                    }
                }
                #endregion

                #region TotalSale & Other Details
                Int32 ReportAppendRowsAtBottom = CommonFunctions.ObjApplicationSettings.ReportAppendRowsAtBottom;
                Int32 TotalRowNum = ItemDetailsStartRow + ListProducts.Count + ReportAppendRowsAtBottom + 1;

                xlRange = xlWorkSheet.Cells[TotalRowNum, DictColNumbers[SlNoCol]];
                xlRange.Value = "Total";
                xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[TotalRowNum, DictColNumbers[SlNoCol]], xlWorkSheet.Cells[TotalRowNum, DictColNumbers[RateCol]]];
                xlRange.Merge();
                xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                Excel.Range xlRangeSaleTotalFrom = xlWorkSheet.Cells[ItemDetailsStartRow, DictColNumbers[ItemTotalCol]];
                Excel.Range xlRangeSaleTotalTo = xlWorkSheet.Cells[TotalRowNum - 1, DictColNumbers[ItemTotalCol]];
                xlRange = xlWorkSheet.Cells[TotalRowNum, DictColNumbers[ItemTotalCol]];
                xlRange.Formula = "=Sum(" + xlRangeSaleTotalFrom.Address[false, false] + ":" + xlRangeSaleTotalTo.Address[false, false] + ")";
                xlRange.NumberFormat = "#,##0.00";
                TotalSalesValue = "='" + SheetName + "'!" + xlRange.Address[false, false];

                Excel.Range xlRangeDiscountFrom = xlWorkSheet.Cells[ItemDetailsStartRow, DictColNumbers[ItemDiscCol]];
                Excel.Range xlRangeDiscountTo = xlWorkSheet.Cells[TotalRowNum - 1, DictColNumbers[ItemDiscCol]];
                xlRange = xlWorkSheet.Cells[TotalRowNum, DictColNumbers[ItemDiscCol]];
                xlRange.Formula = "=Sum(" + xlRangeDiscountFrom.Address[false, false] + ":" + xlRangeDiscountTo.Address[false, false] + ")";
                xlRange.NumberFormat = "#,##0.00";
                //TotalDiscount = "=Sum('" + SheetName + "'!" + xlRangeDiscountFrom.Address[false, false] + ":" + xlRangeDiscountTo.Address[false, false] + ")";
                TotalDiscount = "='" + SheetName + "'!" + xlRange.Address[false, false];

                Excel.Range xlRangeTaxableValFrom = xlWorkSheet.Cells[ItemDetailsStartRow, DictColNumbers[ItemTaxableValueCol]];
                Excel.Range xlRangeTaxableValTo = xlWorkSheet.Cells[TotalRowNum - 1, DictColNumbers[ItemTaxableValueCol]];
                xlRange = xlWorkSheet.Cells[TotalRowNum, DictColNumbers[ItemTaxableValueCol]];
                xlRange.Formula = "=Sum(" + xlRangeTaxableValFrom.Address[false, false] + ":" + xlRangeTaxableValTo.Address[false, false] + ")";
                xlRange.NumberFormat = "#,##0.00";

                ArrTotalTaxes = new String[ArrTaxesHeader.Length / 2];
                for (int j = 0; j < ArrTaxesHeader.Length; j += 2)
                {
                    Excel.Range xlRangeTaxAmtFrom = xlWorkSheet.Cells[ItemDetailsStartRow, DictColNumbers[ArrTaxesHeader[j + 1]]];
                    Excel.Range xlRangeTaxAmtTo = xlWorkSheet.Cells[TotalRowNum - 1, DictColNumbers[ArrTaxesHeader[j + 1]]];
                    xlRange = xlWorkSheet.Cells[TotalRowNum, DictColNumbers[ArrTaxesHeader[j]]];
                    xlRange.Formula = "=Sum(" + xlRangeTaxAmtFrom.Address[false, false] + ":" + xlRangeTaxAmtTo.Address[false, false] + ")";
                    xlRange.NumberFormat = "#,##0.00";
                    ArrTotalTaxes[j / 2] = "=Sum('" + SheetName + "'!" + xlRangeTaxAmtFrom.Address[false, false] + ":" + xlRangeTaxAmtTo.Address[false, false] + ")";

                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[TotalRowNum, DictColNumbers[ArrTaxesHeader[j]]], xlWorkSheet.Cells[TotalRowNum, DictColNumbers[ArrTaxesHeader[j + 1]]]];
                    xlRange.Merge();
                }

                Excel.Range xlRangeTaxFrom = xlWorkSheet.Cells[TotalRowNum, DictColNumbers[ArrTaxesHeader[0]]];
                Excel.Range xlRangeTaxTo = xlWorkSheet.Cells[TotalRowNum, DictColNumbers[ArrTaxesHeader[ArrTaxesHeader.Length - 1]]];
                TotalTax = "=Sum('" + SheetName + "'!" + xlRangeTaxFrom.Address[false, false] + ":" + xlRangeTaxTo.Address[false, false] + ")";
                #endregion

                #region Total Invoice Value
                Int32 TotalInvoiceValueRow = TotalRowNum + 1, TotalInvoiceValueCol = DictColNumbers[ItemDiscCol], LastInvoiceCol = DictColNumbers[ArrTaxesHeader[ArrTaxesHeader.Length - 1]];
                Int32 TotalInvoiceTextCol = DictColNumbers[ItemTotalCol];

                xlRange = xlWorkSheet.Cells[TotalInvoiceValueRow, DictColNumbers[SlNoCol]];
                xlRange.Value = "Total Invoice Value(in figures)";
                xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[TotalInvoiceValueRow, DictColNumbers[SlNoCol]], xlWorkSheet.Cells[TotalInvoiceValueRow, TotalInvoiceTextCol]];
                xlRange.Merge();
                xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                Excel.Range xlRangeTaxableValueCell = xlWorkSheet.Cells[TotalRowNum, DictColNumbers[ItemTaxableValueCol]];
                Excel.Range xlRangeLastTotalTaxAmtCell = xlWorkSheet.Cells[TotalRowNum, LastInvoiceCol];
                xlRange = xlWorkSheet.Cells[TotalInvoiceValueRow, TotalInvoiceValueCol];
                xlRange.Formula = "=Round(Sum(" + xlRangeTaxableValueCell.Address[false, false] + ":" + xlRangeLastTotalTaxAmtCell.Address[false, false] + "), 0)";
                xlRange.NumberFormat = "#,##0.00";
                xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[TotalInvoiceValueRow, TotalInvoiceValueCol], xlWorkSheet.Cells[TotalInvoiceValueRow, LastInvoiceCol]];
                xlRange.Merge();
                //TotalInvoiceValue = "=Sum('" + SheetName + "'!" + xlRangeTaxableValueCell.Address[false, false] + ":" + xlRangeLastTotalTaxAmtCell.Address[false, false] + ")";
                TotalInvoiceValue = "='" + SheetName + "'!" + xlWorkSheet.Cells[TotalInvoiceValueRow, TotalInvoiceValueCol].Address[false, false];

                xlRange = xlWorkSheet.Cells[TotalInvoiceValueRow + 1, DictColNumbers[SlNoCol]];
                xlRange.Value = "Total Invoice Value(in words)";
                xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[TotalInvoiceValueRow + 1, DictColNumbers[SlNoCol]], xlWorkSheet.Cells[TotalInvoiceValueRow + 1, TotalInvoiceTextCol]];
                xlRange.Merge();
                xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                xlRange = xlWorkSheet.Cells[TotalInvoiceValueRow + 1, TotalInvoiceValueCol];
                if (UseNumberToWordsFormula)
                    xlRange.Formula = "=NumberToWords(" + xlWorkSheet.Cells[TotalInvoiceValueRow, TotalInvoiceValueCol].Address[false, false] + ") & \" only\"";
                else
                    xlRange.Value = CommonFunctions.NumberToWords(xlWorkSheet.Cells[TotalInvoiceValueRow, TotalInvoiceValueCol].Value.ToString()) + " only";
                xlRange.WrapText = true;
                xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[TotalInvoiceValueRow + 1, TotalInvoiceValueCol], xlWorkSheet.Cells[TotalInvoiceValueRow + 1, LastInvoiceCol]];
                xlRange.Merge();
                xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                Int32 LastInvoiceRowNum = TotalInvoiceValueRow + 2;
                #region Old Balance Row
                if (PrintOldBalance)
                {
                    xlRange = xlWorkSheet.Cells[TotalInvoiceValueRow + 2, DictColNumbers[SlNoCol]];
                    xlRange.Value = "Old Balance";
                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[TotalInvoiceValueRow + 2, DictColNumbers[SlNoCol]], xlWorkSheet.Cells[TotalInvoiceValueRow + 2, TotalInvoiceTextCol]];
                    xlRange.Merge();
                    xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                    xlRange = xlWorkSheet.Cells[TotalInvoiceValueRow + 2, TotalInvoiceValueCol];
                    xlRange.Value = OldBalance;
                    xlRange.NumberFormat = "#,##0.00";
                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[TotalInvoiceValueRow + 2, TotalInvoiceValueCol], xlWorkSheet.Cells[TotalInvoiceValueRow + 2, LastInvoiceCol]];
                    xlRange.Merge();

                    xlRange = xlWorkSheet.Cells[TotalInvoiceValueRow + 3, DictColNumbers[SlNoCol]];
                    xlRange.Value = "Net Total";
                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[TotalInvoiceValueRow + 3, DictColNumbers[SlNoCol]], xlWorkSheet.Cells[TotalInvoiceValueRow + 3, TotalInvoiceTextCol]];
                    xlRange.Merge();
                    xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                    Excel.Range xlRangeTotalInvoiceValueCell = xlWorkSheet.Cells[TotalInvoiceValueRow, TotalInvoiceValueCol];
                    Excel.Range xlRangeOBCell = xlWorkSheet.Cells[TotalInvoiceValueRow + 2, TotalInvoiceValueCol];
                    xlRange = xlWorkSheet.Cells[TotalInvoiceValueRow + 3, TotalInvoiceValueCol];
                    xlRange.Formula = "=Round((" + xlRangeTotalInvoiceValueCell.Address[false, false] + "+" + xlRangeOBCell.Address[false, false] + "), 0)";
                    xlRange.NumberFormat = "#,##0.00";
                    xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[TotalInvoiceValueRow + 3, TotalInvoiceValueCol], xlWorkSheet.Cells[TotalInvoiceValueRow + 3, LastInvoiceCol]];
                    xlRange.Merge();
                    xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                    LastInvoiceRowNum += 2;
                }
                #endregion

                xlRange = xlWorkSheet.Cells[LastInvoiceRowNum, DictColNumbers[SlNoCol]];
                xlRange.Value = "Amount of Tax Subject to Reverse Charges";
                xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[LastInvoiceRowNum, DictColNumbers[SlNoCol]], xlWorkSheet.Cells[LastInvoiceRowNum, TotalInvoiceTextCol]];
                xlRange.Merge();
                xlRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                xlRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                xlRange = xlWorkSheet.Cells[LastInvoiceRowNum, TotalInvoiceValueCol];
                xlRange.Value = "0";
                xlRange.WrapText = true;
                xlRange.NumberFormat = "#,##0.00";
                xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[LastInvoiceRowNum, TotalInvoiceValueCol], xlWorkSheet.Cells[LastInvoiceRowNum, LastInvoiceCol]];
                xlRange.Merge();

                xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[TotalInvoiceValueRow, DictColNumbers[SlNoCol]], xlWorkSheet.Cells[LastInvoiceRowNum, LastInvoiceCol]];
                xlRange.Font.Bold = true;
                #endregion

                xlRange = xlWorkSheet.Range[xlWorkSheet.Cells[CustDetailsStartRow, DictColNumbers[SlNoCol]], xlWorkSheet.Cells[LastInvoiceRowNum, LastInvoiceCol]];
                xlRange.Font.Name = "Calibri";
                xlRange.Font.Size = 10;
                SellerInvoiceForm.SetAllBorders(xlRange);

                #region Set Column Width & Row Height
                Double[] ArrColumnWidths = new Double[] { 3.14, 13.86, 6.29, 3.57, 3.57, 6.57, 5.71, 8.14, 8.14, 8.14, 4.86, 7.14, 4.86, 7.14/*, 4.86, 7.14*/ };
                for (int i = 0; i < ArrColumnWidths.Length; i++)
                {
                    xlRange = xlWorkSheet.Columns[Char.ConvertFromUtf32(65 + i)];
                    xlRange.ColumnWidth = ArrColumnWidths[i];
                }

                xlWorkSheet.UsedRange.Rows.AutoFit();

                xlRange = xlWorkSheet.Rows[TotalInvoiceValueRow + 1];
                xlRange.RowHeight = 29.25;
                #endregion

                SellerInvoiceForm.AddPageHeaderAndFooter(ref xlWorkSheet, CurrReportSettings.HeaderSubTitle, CurrReportSettings);

                xlWorkSheet.UsedRange.Rows.AutoFit();

                xlWorkSheet.PageSetup.Zoom = false;
                xlWorkSheet.PageSetup.FitToPagesTall = false;
                xlWorkSheet.PageSetup.FitToPagesWide = 1;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("InvoiceGST.CreateInvoice()", ex);
                throw;
            }
        }

        public override DataTable LoadInvoice(String SheetName, String ExcelWorkbookPath)
        {
            try
            {
                DataTable dtSellerInvoice = CommonFunctions.ReturnDataTableFromExcelWorksheet(SheetName, ExcelWorkbookPath, "*", "A8:P");
                if (dtSellerInvoice == null) return null;
                dtSellerInvoice.Rows.RemoveAt(0);
                dtSellerInvoice.Columns["Description of Goods"].ColumnName = "Item Name";
                dtSellerInvoice.Columns["Ord Qty"].ColumnName = "Order Quantity";
                dtSellerInvoice.Columns["Sale Qty"].ColumnName = "Sales Quantity";
                dtSellerInvoice.Columns.Add("TotalTax", Type.GetType("System.Double"), "Convert([F12], 'System.Double') + Convert([F14], 'System.Double')");
                dtSellerInvoice.DefaultView.RowFilter = "IsNull([Sl No], 0) > 0";

                return dtSellerInvoice;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("InvoiceGST.LoadInvoice()", ex);
                throw;
            }
        }
    }
}
