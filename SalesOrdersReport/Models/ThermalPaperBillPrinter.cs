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
                g.DrawString("Date:" + ObjPrintDetails.DateValue.ToString("dd-MM-yyyy HH:mm:ss"), ItemParticularsFont, Brushes.Black, StartX, StartY + CurrOffset);
                g.DrawString("Bill#:" + ObjPrintDetails.InvoiceNumber, ItemParticularsFont, Brushes.Black, StartX + 150, StartY + CurrOffset);
                CurrOffset += Offset;
                g.DrawString("C.Name:" + ObjPrintDetails.CustomerName, ItemParticularsFont, Brushes.Black, StartX, StartY + CurrOffset);
                g.DrawString("Cust#:" + ObjPrintDetails.CustomerPhone, ItemParticularsFont, Brushes.Black, StartX + 150, StartY + CurrOffset);
                CurrOffset += Offset;
                g.DrawString("Staff:" + ObjPrintDetails.StaffName, ItemParticularsFont, Brushes.Black, StartX, StartY + CurrOffset);

                //Print Items
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
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Far;
                for (int i = 0; i < ObjPrintDetails.ListPrintItemDetails.Count; i++)
                {
                    PrintItemDetails item = ObjPrintDetails.ListPrintItemDetails[i];
                    g.DrawString(item.HSNCode, ItemParticularsFont, Brushes.Black, StartX, StartY + CurrOffset);
                    g.DrawString(item.ItemName.Substring(0, Math.Min(item.ItemName.Length, 22)), ItemParticularsFont, Brushes.Black, StartX + 25, StartY + CurrOffset);
                    g.DrawString(item.SaleQty.ToString(), ItemParticularsFont, Brushes.Black, StartX + 110, StartY + CurrOffset);
                    g.DrawString(item.ItemMRP.ToString("F"), ItemParticularsFont, Brushes.Black, new RectangleF(StartX + 130, StartY + CurrOffset, 35, 10), stringFormat);
                    g.DrawString(item.ItemRate.ToString("F"), ItemParticularsFont, Brushes.Black, new RectangleF(StartX + 170, StartY + CurrOffset, 35, 10), stringFormat);
                    g.DrawString(item.Amount.ToString("F"), ItemParticularsFont, Brushes.Black, new RectangleF(StartX + 210, StartY + CurrOffset, 35, 10), stringFormat);
                    CurrOffset += Offset - 2;
                }

                //Print Totals
                g.DrawLine(new Pen(Brushes.Black), StartX, StartY + CurrOffset, PaperWidthInPixel - StartX, StartY + CurrOffset);
                CurrOffset += 2;
                g.DrawString("Items:" + ObjPrintDetails.ListPrintItemDetails.Count, ItemParticularsHeader, Brushes.Black, StartX + 40, StartY + CurrOffset);
                g.DrawString("Qty:" + ObjPrintDetails.ListPrintItemDetails.Sum(e => e.SaleQty), ItemParticularsHeader, Brushes.Black, StartX + 100, StartY + CurrOffset);
                g.DrawString("Amt:", ItemParticularsHeader, Brushes.Black, StartX + 180, StartY + CurrOffset);
                g.DrawString((ObjPrintDetails.GrossAmount + ObjPrintDetails.TotalTaxAmount).ToString("F"), ItemParticularsHeader, Brushes.Black, new RectangleF(StartX + 190, StartY + CurrOffset, 55, 10), stringFormat);
                CurrOffset += Offset; g.DrawLine(new Pen(Brushes.Black), StartX, StartY + CurrOffset, PaperWidthInPixel - StartX, StartY + CurrOffset);
                CurrOffset += 2;
                g.DrawString("Discount:", ItemParticularsHeader, Brushes.Black, StartX + 170, StartY + CurrOffset);
                g.DrawString(ObjPrintDetails.DiscountAmount.ToString("F"), ItemParticularsHeader, Brushes.Black, new RectangleF(StartX + 190, StartY + CurrOffset, 55, 10), stringFormat);
                CurrOffset += Offset;
                g.DrawString("Net Total:", ItemParticularsHeader, Brushes.Black, StartX + 170, StartY + CurrOffset);
                g.DrawString(ObjPrintDetails.NetAmount.ToString("F"), ItemParticularsHeader, Brushes.Black, new RectangleF(StartX + 190, StartY + CurrOffset, 55, 10), stringFormat);
                CurrOffset += Offset; g.DrawLine(new Pen(Brushes.Black), StartX, StartY + CurrOffset, PaperWidthInPixel - StartX, StartY + CurrOffset);
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
    }
}
