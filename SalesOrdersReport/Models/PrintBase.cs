using SalesOrdersReport.CommonModules;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesOrdersReport.Models
{
    class PrintItemDetails
    {
        public String ItemName, HSNCode;
        public Double SaleQty, ItemMRP, ItemRate, SGSTPerc, SGSTAmout, CGSTAmout, CGSTPerc, Amount;
    }

    class PrintPaymentDetails
    {
        public String PaymentMode, CardNumber;
        public Double Amount;
    }

    class PrintDetails
    {
        public String Header1, Header2, CustomerName, CustomerPhone, StaffName, POSNumber, InvoiceNumber;
        public List<String> ListSubHeaderLines = new List<string>();
        public DateTime DateValue;
        public List<PrintItemDetails> ListPrintItemDetails = new List<PrintItemDetails>();
        public List<PrintPaymentDetails> ListPrintPaymentDetails = new List<PrintPaymentDetails>();
        public Double GrossAmount, DiscountAmount, TotalTaxAmount, NetAmount;
        public List<String> ListFooterLines = new List<string>();
    }

    class SummaryByInvoiceStatusDetails
    {
        public String Status;
        public Int32 InvoiceCount;
        public Double GrossAmount, DiscountAmount, NetAmount;
    }

    class SummaryByPaymentModeDetails
    {
        public String PaymentMode;
        public Int32 PaymentCount, BillCount;
        public Double TotalAmount;
    }

    class PrintSummaryDetails
    {
        public String Header, SummaryHeader, StaffName, POSNumber;
        public List<String> ListSubHeaderLines = new List<string>();
        public DateTime DateValue;
        public List<SummaryByInvoiceStatusDetails> ListSummaryByInvoiceStatusDetails = new List<SummaryByInvoiceStatusDetails>();
        public List<SummaryByPaymentModeDetails> ListSummaryByPaymentModeDetails = new List<SummaryByPaymentModeDetails>();
        public List<String> ListFooterLines = new List<string>();
    }

    abstract class PrintBase
    {
        protected PrintDocument ObjPrintDocument;
        protected PrintPreviewDialog printPreviewDialog;
        protected PrintDetails ObjPrintDetails;
        protected PrintSummaryDetails ObjPrintSummaryDetails;
        protected Single PaperWidth, PaperWidthInPixel, PaperCenterX;
        FontFamily fontFamily = new FontFamily("Times New Roman");
        protected Font HeaderFont, SubHeaderFont, ItemParticularsHeader, ItemParticularsFont, FooterFont;

        public PrintBase(Single PaperWidth)
        {
            try
            {
                this.PaperWidth = PaperWidth;
                PaperWidthInPixel = PaperWidth; // (Single)(PaperWidth / 0.2645833333);
                PaperCenterX = (Single)(PaperWidthInPixel / 2.0);

                printPreviewDialog = new PrintPreviewDialog();
                ObjPrintDocument = new PrintDocument();
                ObjPrintDocument.PrintPage += ObjPrintDocument_PrintPage;

                HeaderFont = new Font(fontFamily, 8, FontStyle.Bold);
                SubHeaderFont = new Font(fontFamily, 6, FontStyle.Regular);
                ItemParticularsHeader = new Font(fontFamily, (float)6, FontStyle.Bold);
                ItemParticularsFont = new Font(fontFamily, 6, FontStyle.Regular);
                FooterFont = new Font(fontFamily, 6, FontStyle.Regular);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ctor()", ex);
            }
        }

        protected float GetFontWidthInPixel(float FontSizeInPt)
        {
            return (float)(FontSizeInPt * 1.333);
        }

        public void Print(PrintDetails ObjPrintDetails)
        {
            try
            {
                this.ObjPrintDetails = ObjPrintDetails;
                ObjPrintDocument.DefaultPageSettings.PaperSize = new PaperSize("", (Int32)PaperWidth, 600);
                ObjPrintDocument.Print();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.Print()", ex);
            }
        }

        public void Print(PrintSummaryDetails ObjPrintSummaryDetails)
        {
            try
            {
                this.ObjPrintSummaryDetails = ObjPrintSummaryDetails;
                ObjPrintDocument.DefaultPageSettings.PaperSize = new PaperSize("", (Int32)PaperWidth, 600);
                ObjPrintDocument.Print();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.Print()", ex);
            }
        }

        public void ShowPrintPreview()
        {
            try
            {
                printPreviewDialog.Document = ObjPrintDocument;
                ObjPrintDocument.DefaultPageSettings.PaperSize = new PaperSize("", (Int32)PaperWidth, 600);
                printPreviewDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ShowPrintPreview()", ex);
            }
        }

        private void ObjPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                Int32 Height = -1;
                if (ObjPrintDetails != null) Height = FormatPrintDocument(e);
                if (ObjPrintSummaryDetails != null) Height = FormatPrintSummaryDocument(e);
                //Height = (Int32)(((Height / 8.0) / 25.4) * 100);
                //Height = (Int32)(Height * 0.010416667 * 100);
                ObjPrintDocument.DefaultPageSettings.PaperSize = new PaperSize("", (Int32)PaperWidth, Height);
                e.HasMorePages = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ObjPrintDocument_PrintPage()", ex);
            }
        }

        public abstract Int32 FormatPrintDocument(PrintPageEventArgs ObjPrintPageEventArgs);

        public abstract Int32 FormatPrintSummaryDocument(PrintPageEventArgs ObjPrintPageEventArgs);
    }
}
