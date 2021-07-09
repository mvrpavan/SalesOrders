using SalesOrdersReport.CommonModules;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrdersReport.Models
{
    class ThermalPaperBillPrinter : PrintBase
    {
        public ThermalPaperBillPrinter(Single PaperWidthInMM) : base(PaperWidthInMM) { }

        public override Int32 FormatPrintDocument(PrintPageEventArgs ObjPrintPageEventArgs)
        {
            try
            {
                Graphics g = ObjPrintPageEventArgs.Graphics;
                float StartX = 20, StartY = 30, CurrOffset = 0, Offset = 12;
                Single HeaderFontWidthInPixel = GetFontWidthInPixel(HeaderFont.SizeInPoints);
                Single SubHeaderFontWidthInPixel = GetFontWidthInPixel(SubHeaderFont.SizeInPoints);
                Single FooterFontWidthInPixel = GetFontWidthInPixel(FooterFont.SizeInPoints);
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Far;
                StringFormat stringFormat1 = new StringFormat();
                stringFormat1.Alignment = StringAlignment.Near;

                //Print Header1
                Int32 Length = ObjPrintDetails.Header1.Length;
                float X = PaperCenterX - (Length * HeaderFontWidthInPixel / 4);
                g.DrawString(ObjPrintDetails.Header1, HeaderFont, Brushes.Black, X, StartY);
                CurrOffset += Offset + 2;
                for (int i = 0; i < ObjPrintDetails.ListSubHeaderLines.Count; i++)
                {
                    Length = ObjPrintDetails.ListSubHeaderLines[i].Length;
                    X = PaperCenterX - (Length * SubHeaderFontWidthInPixel / 4);
                    g.DrawString(ObjPrintDetails.ListSubHeaderLines[i], SubHeaderFont, Brushes.Black, X, StartY + CurrOffset);
                    CurrOffset += Offset - 2;
                }
                g.DrawLine(new Pen(Brushes.Black), StartX, StartY + CurrOffset, PaperWidthInPixel - StartX, StartY + CurrOffset);
                CurrOffset += 2;

                //Print Header2
                Length = ObjPrintDetails.Header2.Length;
                X = PaperCenterX - (Length * HeaderFontWidthInPixel / 4);
                g.DrawString(ObjPrintDetails.Header2, HeaderFont, Brushes.Black, X, StartY + CurrOffset);
                CurrOffset += Offset; g.DrawLine(new Pen(Brushes.Black), StartX, StartY + CurrOffset, PaperWidthInPixel - StartX, StartY + CurrOffset);
                CurrOffset += 2;

                //Print Bill & Customer Details
                g.DrawString("Date: " + ObjPrintDetails.DateValue.ToString("dd-MM-yyyy HH:mm:ss"), ItemParticularsFont, Brushes.Black, StartX, StartY + CurrOffset);
                g.DrawString("Bill#: " + ObjPrintDetails.InvoiceNumber, ItemParticularsFont, Brushes.Black, StartX + 150, StartY + CurrOffset);
                CurrOffset += Offset;
                if (ObjPrintDetails.CustomerName.Length > 25)
                    g.DrawString("C.Name: " + ObjPrintDetails.CustomerName.Substring(0, Math.Min(44, ObjPrintDetails.CustomerName.Length)), ItemParticularsFont, Brushes.Black, new RectangleF(StartX, StartY + CurrOffset, 85, 20), stringFormat1);
                else
                    g.DrawString("C.Name: " + ObjPrintDetails.CustomerName, ItemParticularsFont, Brushes.Black, StartX, StartY + CurrOffset);
                g.DrawString("Cust#: " + ObjPrintDetails.CustomerPhone, ItemParticularsFont, Brushes.Black, StartX + 150, StartY + CurrOffset);
                if (ObjPrintDetails.CustomerName.Length > 25) CurrOffset += Offset - 2;
                CurrOffset += Offset;
                g.DrawString("Staff: " + ObjPrintDetails.StaffName.Substring(0, Math.Min(25, ObjPrintDetails.StaffName.Length)), ItemParticularsFont, Brushes.Black, StartX, StartY + CurrOffset);
                g.DrawString("POS#: " + ObjPrintDetails.POSNumber, ItemParticularsFont, Brushes.Black, StartX + 150, StartY + CurrOffset);

                //Print Items
                List<String> ListTaxGroups = new List<String>();
                List<List<Int32>> ListTaxGroupItemIndexes = new List<List<Int32>>();
                List<Tuple<Double, Double, Double, Double>> ListTaxGroupAmounts = new List<Tuple<Double, Double, Double, Double>>();
                for (int i = 0; i < ObjPrintDetails.ListPrintItemDetails.Count; i++)
                {
                    PrintItemDetails tmpItem = ObjPrintDetails.ListPrintItemDetails[i];
                    String TaxGroup = $"SGST@{tmpItem.SGSTPerc.ToString("N1")}% CGST@{tmpItem.CGSTPerc.ToString("N1")}%";
                    Int32 Index = ListTaxGroups.BinarySearch(TaxGroup);
                    if (Index >= 0)
                    {
                        ListTaxGroupItemIndexes[Index].Add(i);
                    }
                    else
                    {
                        ListTaxGroups.Insert(~Index, TaxGroup);
                        ListTaxGroupItemIndexes.Insert(~Index, new List<Int32>());
                        ListTaxGroupItemIndexes[~Index].Add(i);
                    }
                }
                for (int i = 0; i < ListTaxGroupItemIndexes.Count; i++)
                {
                    Double TotalAmountBeforeTax = 0, TotalSGSTAmount = 0, TotalCGSTAmount = 0, TotalAmountAfterTax = 0;
                    for (int j = 0; j < ListTaxGroupItemIndexes[i].Count; j++)
                    {
                        PrintItemDetails tmpItem = ObjPrintDetails.ListPrintItemDetails[ListTaxGroupItemIndexes[i][j]];
                        TotalAmountBeforeTax += tmpItem.Amount - (tmpItem.CGSTAmout + tmpItem.SGSTAmout);
                        TotalSGSTAmount += tmpItem.SGSTAmout;
                        TotalCGSTAmount += tmpItem.CGSTAmout;
                        TotalAmountAfterTax += tmpItem.Amount;
                    }
                    TotalAmountBeforeTax -= ObjPrintDetails.DiscountAmount;
                    TotalAmountAfterTax -= ObjPrintDetails.DiscountAmount;
                    Tuple<Double, Double, Double, Double> tmpTaxGroupTotal = Tuple.Create(TotalAmountBeforeTax, TotalSGSTAmount, TotalCGSTAmount, TotalAmountAfterTax);
                    ListTaxGroupAmounts.Add(tmpTaxGroupTotal);
                }

                CurrOffset += Offset; g.DrawLine(new Pen(Brushes.Black), StartX, StartY + CurrOffset, PaperWidthInPixel - StartX, StartY + CurrOffset);
                CurrOffset += 2;
                g.DrawString("HSN", ItemParticularsHeader, Brushes.Black, StartX, StartY + CurrOffset);
                g.DrawString("Item Desc", ItemParticularsHeader, Brushes.Black, StartX + 25, StartY + CurrOffset);
                g.DrawString("Qty", ItemParticularsHeader, Brushes.Black, StartX + 110, StartY + CurrOffset);
                g.DrawString("MRP", ItemParticularsHeader, Brushes.Black, StartX + 140, StartY + CurrOffset);
                g.DrawString("Rate", ItemParticularsHeader, Brushes.Black, StartX + 180, StartY + CurrOffset);
                g.DrawString("Amount", ItemParticularsHeader, Brushes.Black, StartX + 210, StartY + CurrOffset);
                CurrOffset += Offset; g.DrawLine(new Pen(Brushes.Black), StartX, StartY + CurrOffset, PaperWidthInPixel - StartX, StartY + CurrOffset);
                CurrOffset += 2;

                for (int j = 0; j < ListTaxGroups.Count; j++)
                {
                    g.DrawString((j + 1).ToString(), ItemParticularsFont, Brushes.Black, StartX, StartY + CurrOffset);
                    g.DrawString(ListTaxGroups[j], ItemParticularsFont, Brushes.Black, StartX + 10, StartY + CurrOffset);
                    CurrOffset += Offset - 2;

                    for (int i = 0; i < ObjPrintDetails.ListPrintItemDetails.Count; i++)
                    {
                        if (!ListTaxGroupItemIndexes[j].Contains(i)) continue;
                        PrintItemDetails item = ObjPrintDetails.ListPrintItemDetails[i];
                        g.DrawString(item.HSNCode, ItemParticularsFont, Brushes.Black, StartX, StartY + CurrOffset);
                        if (item.ItemName.Length > 22)
                            g.DrawString(item.ItemName.Substring(0, Math.Min(44, item.ItemName.Length)), ItemParticularsFont, Brushes.Black, new RectangleF(StartX + 25, StartY + CurrOffset, 85, 20), stringFormat1);
                        else
                            g.DrawString(item.ItemName, ItemParticularsFont, Brushes.Black, StartX + 25, StartY + CurrOffset);
                        g.DrawString(item.SaleQty.ToString(), ItemParticularsFont, Brushes.Black, StartX + 110, StartY + CurrOffset);
                        g.DrawString(item.ItemMRP.ToString("F"), ItemParticularsFont, Brushes.Black, new RectangleF(StartX + 130, StartY + CurrOffset, 35, 10), stringFormat);
                        g.DrawString(item.ItemRate.ToString("F"), ItemParticularsFont, Brushes.Black, new RectangleF(StartX + 170, StartY + CurrOffset, 35, 10), stringFormat);
                        g.DrawString(item.Amount.ToString("F"), ItemParticularsFont, Brushes.Black, new RectangleF(StartX + 210, StartY + CurrOffset, 35, 10), stringFormat);
                        if (item.ItemName.Length > 22) CurrOffset += Offset - 2;
                        CurrOffset += Offset - 2;
                    }
                }

                //Print Totals
                g.DrawLine(new Pen(Brushes.Black), StartX, StartY + CurrOffset, PaperWidthInPixel - StartX, StartY + CurrOffset);
                CurrOffset += 2;
                g.DrawString("Items:" + ObjPrintDetails.ListPrintItemDetails.Count, ItemParticularsHeader, Brushes.Black, StartX + 40, StartY + CurrOffset);
                g.DrawString("Qty:" + ObjPrintDetails.ListPrintItemDetails.Sum(e => e.SaleQty), ItemParticularsHeader, Brushes.Black, StartX + 100, StartY + CurrOffset);
                g.DrawString("Amt:", ItemParticularsHeader, Brushes.Black, StartX + 180, StartY + CurrOffset);
                g.DrawString(ObjPrintDetails.GrossAmount.ToString("F"), ItemParticularsHeader, Brushes.Black, new RectangleF(StartX + 190, StartY + CurrOffset, 55, 10), stringFormat);
                CurrOffset += Offset; g.DrawLine(new Pen(Brushes.Black), StartX, StartY + CurrOffset, PaperWidthInPixel - StartX, StartY + CurrOffset);
                CurrOffset += 2;
                g.DrawString("Discount:", ItemParticularsHeader, Brushes.Black, StartX + 170, StartY + CurrOffset);
                g.DrawString(ObjPrintDetails.DiscountAmount.ToString("F"), ItemParticularsHeader, Brushes.Black, new RectangleF(StartX + 190, StartY + CurrOffset, 55, 10), stringFormat);
                CurrOffset += Offset;
                //g.DrawString("Tax Amount:", ItemParticularsHeader, Brushes.Black, StartX + 170, StartY + CurrOffset);
                //g.DrawString(ObjPrintDetails.TotalTaxAmount.ToString("F"), ItemParticularsHeader, Brushes.Black, new RectangleF(StartX + 190, StartY + CurrOffset, 55, 10), stringFormat);
                //CurrOffset += Offset;
                g.DrawString("Net Total:", ItemParticularsHeader, Brushes.Black, StartX + 170, StartY + CurrOffset);
                g.DrawString(ObjPrintDetails.NetAmount.ToString("F"), ItemParticularsHeader, Brushes.Black, new RectangleF(StartX + 190, StartY + CurrOffset, 55, 10), stringFormat);
                CurrOffset += Offset; g.DrawLine(new Pen(Brushes.Black), StartX, StartY + CurrOffset, PaperWidthInPixel - StartX, StartY + CurrOffset);
                CurrOffset += 2;

                //Print GST Summary
                g.DrawString("Sl#", ItemParticularsFont, Brushes.Black, StartX, StartY + CurrOffset);
                g.DrawString("Amount", ItemParticularsFont, Brushes.Black, StartX + 35, StartY + CurrOffset);
                g.DrawString("SGST", ItemParticularsFont, Brushes.Black, StartX + 95, StartY + CurrOffset);
                g.DrawString("CGST", ItemParticularsFont, Brushes.Black, StartX + 145, StartY + CurrOffset);
                g.DrawString("Total Amount", ItemParticularsFont, Brushes.Black, StartX + 200, StartY + CurrOffset);
                //CurrOffset += Offset; g.DrawLine(new Pen(Brushes.Black), StartX, StartY + CurrOffset, PaperWidthInPixel - StartX, StartY + CurrOffset);
                CurrOffset += Offset - 2;
                for (int i = 0; i < ListTaxGroupAmounts.Count; i++)
                {
                    g.DrawString((i + 1).ToString(), ItemParticularsFont, Brushes.Black, StartX, StartY + CurrOffset);
                    g.DrawString(ListTaxGroupAmounts[i].Item1.ToString("F"), ItemParticularsFont, Brushes.Black, new RectangleF(StartX + 30, StartY + CurrOffset, 35, 10), stringFormat);
                    g.DrawString(ListTaxGroupAmounts[i].Item2.ToString("F"), ItemParticularsFont, Brushes.Black, new RectangleF(StartX + 80, StartY + CurrOffset, 38, 10), stringFormat);
                    g.DrawString(ListTaxGroupAmounts[i].Item3.ToString("F"), ItemParticularsFont, Brushes.Black, new RectangleF(StartX + 130, StartY + CurrOffset, 38, 10), stringFormat);
                    g.DrawString(ListTaxGroupAmounts[i].Item4.ToString("F"), ItemParticularsFont, Brushes.Black, new RectangleF(StartX + 210, StartY + CurrOffset, 35, 10), stringFormat);
                    CurrOffset += Offset - 2;
                }
                g.DrawLine(new Pen(Brushes.Black), StartX, StartY + CurrOffset, PaperWidthInPixel - StartX, StartY + CurrOffset);
                CurrOffset += 2;

                //Print Payment Modes
                g.DrawString("Payment Mode", ItemParticularsFont, Brushes.Black, StartX + 140, StartY + CurrOffset);
                g.DrawString("Amount", ItemParticularsFont, Brushes.Black, StartX + 215, StartY + CurrOffset);
                CurrOffset += Offset - 2;
                foreach (var item in ObjPrintDetails.ListPrintPaymentDetails)
                {
                    g.DrawString(item.PaymentMode, ItemParticularsFont, Brushes.Black, StartX + 140, StartY + CurrOffset);
                    g.DrawString(item.Amount.ToString("F"), ItemParticularsFont, Brushes.Black, new RectangleF(StartX + 210, StartY + CurrOffset, 35, 10), stringFormat);
                    CurrOffset += Offset - 2;
                }
                g.DrawLine(new Pen(Brushes.Black), StartX, StartY + CurrOffset, PaperWidthInPixel - StartX, StartY + CurrOffset);
                CurrOffset += 2;

                //Print Footer
                for (int i = 0; i < ObjPrintDetails.ListFooterLines.Count; i++)
                {
                    Length = ObjPrintDetails.ListFooterLines[i].Length;
                    X = PaperCenterX - (Length * FooterFontWidthInPixel / 4);
                    g.DrawString(ObjPrintDetails.ListFooterLines[i], FooterFont, Brushes.Black, X, StartY + CurrOffset);
                    CurrOffset += Offset - 2;
                }

                return (Int32)(StartY + CurrOffset);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.FormatPrintDocument()", ex);
                throw;
            }
        }

        public override Int32 FormatPrintSummaryDocument(PrintPageEventArgs ObjPrintPageEventArgs)
        {
            try
            {
                Graphics g = ObjPrintPageEventArgs.Graphics;
                float StartX = 20, StartY = 30, CurrOffset = 0, Offset = 12;
                Single HeaderFontWidthInPixel = GetFontWidthInPixel(HeaderFont.SizeInPoints);
                Single SubHeaderFontWidthInPixel = GetFontWidthInPixel(SubHeaderFont.SizeInPoints);
                Single FooterFontWidthInPixel = GetFontWidthInPixel(FooterFont.SizeInPoints);
                StringFormat numberFormat = new StringFormat();
                numberFormat.Alignment = StringAlignment.Far;
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Near;

                //Print Header1
                Int32 Length = ObjPrintSummaryDetails.Header.Length;
                float X = PaperCenterX - (Length * HeaderFontWidthInPixel / 4);
                g.DrawString(ObjPrintSummaryDetails.Header, HeaderFont, Brushes.Black, X, StartY);
                CurrOffset += Offset + 2;
                for (int i = 0; i < ObjPrintSummaryDetails.ListSubHeaderLines.Count; i++)
                {
                    Length = ObjPrintSummaryDetails.ListSubHeaderLines[i].Length;
                    X = PaperCenterX - (Length * SubHeaderFontWidthInPixel / 4);
                    g.DrawString(ObjPrintSummaryDetails.ListSubHeaderLines[i], SubHeaderFont, Brushes.Black, X, StartY + CurrOffset);
                    CurrOffset += Offset - 2;
                }
                g.DrawLine(new Pen(Brushes.Black), StartX, StartY + CurrOffset, PaperWidthInPixel - StartX, StartY + CurrOffset);
                CurrOffset += 2;

                //Print Date & Staff Details
                g.DrawString("Date:" + ObjPrintSummaryDetails.DateValue.ToString("dd-MM-yyyy HH:mm:ss"), ItemParticularsFont, Brushes.Black, StartX, StartY + CurrOffset);
                CurrOffset += Offset;
                g.DrawString("Staff:" + ObjPrintSummaryDetails.StaffName.Substring(0, Math.Min(25, ObjPrintSummaryDetails.StaffName.Length)), ItemParticularsFont, Brushes.Black, StartX, StartY + CurrOffset);
                g.DrawString("POS#: " + ObjPrintSummaryDetails.POSNumber, ItemParticularsFont, Brushes.Black, StartX + 150, StartY + CurrOffset);

                //Print SummaryByInvoiceStatus Details
                CurrOffset += Offset; g.DrawLine(new Pen(Brushes.Black), StartX, StartY + CurrOffset, PaperWidthInPixel - StartX, StartY + CurrOffset);
                CurrOffset += 2;
                g.DrawString("Status", ItemParticularsHeader, Brushes.Black, StartX, StartY + CurrOffset);
                g.DrawString("#Bills", ItemParticularsHeader, Brushes.Black, StartX + 45, StartY + CurrOffset);
                g.DrawString("Gross Amt", ItemParticularsHeader, Brushes.Black, StartX + 80, StartY + CurrOffset);
                g.DrawString("Disc Amt", ItemParticularsHeader, Brushes.Black, StartX + 140, StartY + CurrOffset);
                g.DrawString("Net Amt", ItemParticularsHeader, Brushes.Black, StartX + 200, StartY + CurrOffset);
                CurrOffset += Offset; g.DrawLine(new Pen(Brushes.Black), StartX, StartY + CurrOffset, PaperWidthInPixel - StartX, StartY + CurrOffset);
                CurrOffset += 2;
                for (int i = 0; i < ObjPrintSummaryDetails.ListSummaryByInvoiceStatusDetails.Count; i++)
                {
                    SummaryByInvoiceStatusDetails Summary = ObjPrintSummaryDetails.ListSummaryByInvoiceStatusDetails[i];
                    g.DrawString(Summary.Status, ItemParticularsFont, Brushes.Black, new RectangleF(StartX, StartY + CurrOffset, 40, 10), stringFormat);
                    g.DrawString(Summary.InvoiceCount.ToString(), ItemParticularsFont, Brushes.Black, new RectangleF(StartX + 30, StartY + CurrOffset, 35, 10), numberFormat);
                    g.DrawString(Summary.GrossAmount.ToString("F"), ItemParticularsFont, Brushes.Black, new RectangleF(StartX + 85, StartY + CurrOffset, 35, 10), numberFormat);
                    g.DrawString(Summary.DiscountAmount.ToString("F"), ItemParticularsFont, Brushes.Black, new RectangleF(StartX + 140, StartY + CurrOffset, 35, 10), numberFormat);
                    g.DrawString(Summary.NetAmount.ToString("F"), ItemParticularsFont, Brushes.Black, new RectangleF(StartX + 200, StartY + CurrOffset, 35, 10), numberFormat);
                    CurrOffset += Offset - 2;
                }

                //Print SummaryByPaymentMode Details
                g.DrawLine(new Pen(Brushes.Black), StartX, StartY + CurrOffset, PaperWidthInPixel - StartX, StartY + CurrOffset);
                CurrOffset += 2;
                g.DrawString("Paymt Mode", ItemParticularsHeader, Brushes.Black, StartX, StartY + CurrOffset);
                g.DrawString("#Bills", ItemParticularsHeader, Brushes.Black, StartX + 90, StartY + CurrOffset);
                g.DrawString("#Payments", ItemParticularsHeader, Brushes.Black, StartX + 140, StartY + CurrOffset);
                g.DrawString("Total Amt", ItemParticularsHeader, Brushes.Black, StartX + 200, StartY + CurrOffset);
                CurrOffset += Offset; g.DrawLine(new Pen(Brushes.Black), StartX, StartY + CurrOffset, PaperWidthInPixel - StartX, StartY + CurrOffset);
                CurrOffset += 2;
                for (int i = 0; i < ObjPrintSummaryDetails.ListSummaryByPaymentModeDetails.Count; i++)
                {
                    SummaryByPaymentModeDetails Summary = ObjPrintSummaryDetails.ListSummaryByPaymentModeDetails[i];
                    g.DrawString(Summary.PaymentMode, ItemParticularsFont, Brushes.Black, new RectangleF(StartX, StartY + CurrOffset, 50, 10), stringFormat);
                    g.DrawString(Summary.BillCount.ToString(), ItemParticularsFont, Brushes.Black, new RectangleF(StartX + 80, StartY + CurrOffset, 25, 10), numberFormat);
                    g.DrawString(Summary.PaymentCount.ToString(), ItemParticularsFont, Brushes.Black, new RectangleF(StartX + 140, StartY + CurrOffset, 25, 10), numberFormat);
                    g.DrawString(Summary.TotalAmount.ToString("F"), ItemParticularsFont, Brushes.Black, new RectangleF(StartX + 200, StartY + CurrOffset, 40, 10), numberFormat);
                    CurrOffset += Offset - 2;
                }
                g.DrawLine(new Pen(Brushes.Black), StartX, StartY + CurrOffset, PaperWidthInPixel - StartX, StartY + CurrOffset);
                CurrOffset += 2;

                //Print Footer
                for (int i = 0; i < ObjPrintSummaryDetails.ListFooterLines.Count; i++)
                {
                    Length = ObjPrintSummaryDetails.ListFooterLines[i].Length;
                    X = PaperCenterX - (Length * FooterFontWidthInPixel / 4);
                    g.DrawString(ObjPrintSummaryDetails.ListFooterLines[i], FooterFont, Brushes.Black, X, StartY + CurrOffset);
                    CurrOffset += Offset - 2;
                }
                return (Int32)(StartY + CurrOffset);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.FormatPrintSummaryDocument()", ex);
                throw;
            }
        }
    }
}
