using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;

namespace SalesOrdersReport
{
    class ProductDetailsForInvoice
    {
        public Int32 SerialNumber;
        public String Description, HSNCode;
        public Double OrderQuantity, SaleQuantity, Rate;
        public String UnitsOfMeasurement;
        public TaxDetails CGSTDetails, SGSTDetails, IGSTDetails;
        public DiscountGroupDetails DiscountGroup;
    }

    class TaxDetails
    {
        public Double TaxRate, Amount = 0;
    }

    abstract class Invoice
    {
        public String SerialNumber, InvoiceNumberText;
        public DateTime DateOfInvoice;
        public SellerDetails ObjSellerDetails;
        public List<ProductDetailsForInvoice> ListProducts;
        public String TotalSalesValue, TotalDiscount, TotalTax;
        public String TotalInvoiceValue;
        public String[] ArrTotalTaxes;
        public ReportSettings CurrReportSettings;
        public Boolean PrintOldBalance;
        public String SheetName;
        public Boolean UseNumberToWordsFormula = true;

        public abstract void CreateInvoice(Excel.Worksheet OutputWorksheet);

        public abstract DataTable LoadInvoice(String SheetName, String ExcelWorkbookPath);
    }
}
