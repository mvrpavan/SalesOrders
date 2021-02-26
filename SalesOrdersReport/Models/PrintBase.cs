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
        public Double SaleQty, ItemMRP, ItemRate, Tax, Amount;
    }

    class PrintDetails
    {
        public String Header1, Header2, CustomerName, CustomerPhone, StaffName, InvoiceNumber;
        public List<String> ListSubHeaderLines = new List<string>();
        public DateTime DateValue;
        public List<PrintItemDetails> ListPrintItemDetails = new List<PrintItemDetails>();
        public Double GrossAmount, TotalTaxAmount, NetAmount;
        public List<String> ListFooterLines = new List<string>();
    }

    abstract class PrintBase
    {
        protected PrintDocument ObjPrintDocument;
        protected PrintPreviewDialog printPreviewDialog;
        protected PrintDetails ObjPrintDetails;
        protected Single PaperWidth, PaperWidthInPixel, PaperCenterX;
        protected Font HeaderFont = new System.Drawing.Font(new System.Drawing.FontFamily("Times New Roman"), 8, System.Drawing.FontStyle.Bold);
        protected Font SubHeaderFont = new System.Drawing.Font(new System.Drawing.FontFamily("Times New Roman"), 6, System.Drawing.FontStyle.Regular);
        protected Font ItemParticularsHeader = new System.Drawing.Font(new System.Drawing.FontFamily("Times New Roman"), (float)6, System.Drawing.FontStyle.Bold);
        protected Font ItemParticularsFont = new System.Drawing.Font(new System.Drawing.FontFamily("Times New Roman"), 6, System.Drawing.FontStyle.Regular);
        protected Font FooterFont = new System.Drawing.Font(new System.Drawing.FontFamily("Times New Roman"), 6, System.Drawing.FontStyle.Regular);

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
                ShowPrintPreview();
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
                ObjPrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("", (Int32)PaperWidth, 600);
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
                Int32 Height = FormatPrintDocument(e);
                ObjPrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("", (Int32)PaperWidth, Height);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ObjPrintDocument_PrintPage()", ex);
            }
        }

        public abstract Int32 FormatPrintDocument(PrintPageEventArgs ObjPrintPageEventArgs);
    }
}
