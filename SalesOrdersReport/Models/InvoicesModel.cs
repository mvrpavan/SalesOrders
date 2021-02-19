using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace SalesOrdersReport.Models
{
    enum INVOICESTATUS
    {
        Created, Paid, Cancelled, Void, All
    };

    class InvoiceDetails
    {
        public Int32 InvoiceID;
        public String InvoiceNumber;
        public DateTime InvoiceDate, CreationDate, LastUpdatedDate;
        public Int32 CustomerID, OrderID;
        public String CustomerName;
        public Double NetInvoiceAmount;
        public INVOICESTATUS InvoiceStatus;
        public List<InvoiceItemDetails> ListInvoiceItems;
        public Int32 InvoiceItemCount = 0;

        public InvoiceDetails Clone()
        {
            try
            {
                InvoiceDetails ObjOrderDetailsCopy = (InvoiceDetails)MemberwiseClone();
                ObjOrderDetailsCopy.ListInvoiceItems = this.ListInvoiceItems.Select(e => e.Clone()).ToList();

                return ObjOrderDetailsCopy;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.Clone()", ex);
                throw;
            }
        }
    }

    class InvoiceItemDetails
    {
        public Int32 ProductID;
        public String ProductName;
        public Double OrderQty, SaleQty, Price, Discount, TaxableValue, CGST, SGST, IGST, NetTotal;

        public InvoiceItemDetails Clone()
        {
            try
            {
                return (InvoiceItemDetails)MemberwiseClone();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.Clone()", ex);
                throw;
            }
        }
    }

    class InvoicesModel
    {
        public DataTable dtAllInvoices;
        List<InvoiceDetails> ListInvoices;
        MySQLHelper ObjMySQLHelper;
        const String InvoiceNumberPrefix = "INV";
        ProductMasterModel ObjProductMasterModel;

        public void Initialize()
        {
            try
            {
                ListInvoices = new List<InvoiceDetails>();
                ObjMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                ObjProductMasterModel = CommonFunctions.ObjProductMaster;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.Initialize()", ex);
                throw;
            }
        }

        public String GenerateNewInvoiceNumber()
        {
            try
            {
                String MaxInvoiceNumber = ObjMySQLHelper.GetIDValue("Invoices");
                if (String.IsNullOrEmpty(MaxInvoiceNumber)) MaxInvoiceNumber = "0";
                return CommonFunctions.GenerateNextID(InvoiceNumberPrefix, MaxInvoiceNumber);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GenerateNewInvoiceNumber()", ex);
                throw;
            }
        }

        public DataTable LoadInvoiceDetails(DateTime FromDate, DateTime ToDate, INVOICESTATUS InvoiceStatus = INVOICESTATUS.Created,
                                    String SearchField = null, String SearchFieldValue = null)
        {
            try
            {
                String[] ArrDtColumns1 = new string[] { "InvoiceID", "CustomerID", "OrderID", "InvoiceNumber", "InvoiceDate" };
                String[] ArrDtColumns2 = new string[] { "InvoiceItemCount", "NetInvoiceAmount", "InvoiceStatus", "CreationDate", "LastUpdatedDate" };

                String[] ArrColumns = new string[] { "InvoiceID", "CustomerID", "OrderID", "Invoice Number", "Invoice Date", "Customer Name", "Invoice Item Count", "Net Invoice Amount",
                                                    "Invoice Status", "Creation Date", "Last Updated Date" };

                String Query = $"Select a.{String.Join(", a.", ArrDtColumns1)}, b.CustomerName, a.{String.Join(", a.", ArrDtColumns2)} from Invoices a Inner Join CUSTOMERMASTER b on a.CustomerID = b.CustomerID", WhereClause = $" Where 1 = 1";
                if (FromDate > DateTime.MinValue && ToDate > DateTime.MinValue)
                {
                    WhereClause += $" and a.InvoiceDate between '{MySQLHelper.GetDateStringForDB(FromDate)}' and '{MySQLHelper.GetDateStringForDB(ToDate)}'";
                }
                if (InvoiceStatus != INVOICESTATUS.All)
                {
                    WhereClause += $" and a.InvoiceStatus = '{InvoiceStatus}'";
                }

                if (!String.IsNullOrEmpty(SearchField) && !String.IsNullOrEmpty(SearchFieldValue.Trim()))
                {
                    switch (SearchField.ToUpper())
                    {
                        case "CUSTOMER NAME":
                            WhereClause += $" and b.CustomerName like '%{SearchFieldValue}%')";
                            break;
                        case "CUSTOMERID":
                            WhereClause += $" and a.CustomerID = {SearchFieldValue}";
                            break;
                        case "INVOICE NUMBER":
                            WhereClause += $" and a.InvoiceNumber like '%{SearchFieldValue}%'";
                            break;
                        case "INVOICEID":
                            WhereClause += $" and a.InvoiceID = {SearchFieldValue}";
                            break;
                        case "ORDERID":
                            WhereClause += $" and a.OrderID = {SearchFieldValue}";
                            break;
                        case "INVOICE STATUS":
                            WhereClause += $" and a.InvoiceStatus like '%{SearchFieldValue}%'";
                            break;
                        default:
                            break;
                    }
                }
                Query += WhereClause + " Order by a.InvoiceID";
                DataTable dtInvoices = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                dtAllInvoices = dtInvoices;

                ListInvoices.Clear();
                foreach (DataRow dtRow in dtInvoices.Rows)
                {
                    InvoiceDetails tmpInvoiceDetails = new InvoiceDetails
                    {
                        InvoiceID = Int32.Parse(dtRow["InvoiceID"].ToString()),
                        OrderID = Int32.Parse(dtRow["OrderID"].ToString()),
                        InvoiceNumber = dtRow["InvoiceNumber"].ToString(),
                        InvoiceDate = DateTime.Parse(dtRow["InvoiceDate"].ToString()),
                        InvoiceItemCount = Int32.Parse(dtRow["InvoiceItemCount"].ToString()),
                        CreationDate = DateTime.Parse(dtRow["CreationDate"].ToString()),
                        LastUpdatedDate = DateTime.Parse(dtRow["LastUpdatedDate"].ToString()),
                        CustomerID = Int32.Parse(dtRow["CustomerID"].ToString()),
                        CustomerName = dtRow["CustomerName"].ToString(),
                        NetInvoiceAmount = Double.Parse(dtRow["NetInvoiceAmount"].ToString()),
                        InvoiceStatus = (INVOICESTATUS)Enum.Parse(Type.GetType("SalesOrdersReport.Models.INVOICESTATUS"), dtRow["InvoiceStatus"].ToString())
                    };

                    ListInvoices.Add(tmpInvoiceDetails);
                }

                for (int i = 0; i < dtAllInvoices.Columns.Count; i++)
                {
                    dtAllInvoices.Columns[i].ColumnName = ArrColumns[i];
                }
                return dtAllInvoices;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadOrderDetails()", ex);
                throw;
            }
        }

        public Int32 DeleteInvoiceDetails(Int32 InvoiceID)
        {
            try
            {
                ObjMySQLHelper.UpdateTableDetails("Orders", new List<string>() { "InvoiceStatus" }, new List<string>() { INVOICESTATUS.Cancelled.ToString() }, 
                                            new List<Types>() { Types.String }, $"InvoiceID = {InvoiceID}");

                dtAllInvoices.Select($"InvoiceID = {InvoiceID}")[0]["Invoice Status"] = INVOICESTATUS.Cancelled;
                dtAllInvoices.AcceptChanges();

                ListInvoices.Find(e => e.InvoiceID == InvoiceID).InvoiceStatus = INVOICESTATUS.Cancelled;

                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.DeleteInvoiceDetails()", ex);
                return -1;
            }
        }

        public InvoiceDetails FillInvoiceItemDetails(Int32 InvoiceID)
        {
            try
            {
                return GetInvoiceDetailsForInvoiceID(InvoiceID);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.FillInvoiceItemDetails(InvoiceID)", ex);
                return null;
            }
        }

        public void FillInvoiceItemDetails(InvoiceDetails CurrInvoiceDetails)
        {
            try
            {
                String Query = $"Select * from InvoiceItems Where InvoiceID = {CurrInvoiceDetails.InvoiceID}";
                DataTable dtInvoiceItems = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                CurrInvoiceDetails.ListInvoiceItems = new List<InvoiceItemDetails>();
                foreach (DataRow item in dtInvoiceItems.Rows)
                {
                    InvoiceItemDetails tmpInvoiceItem = new InvoiceItemDetails()
                    {
                        ProductID = Int32.Parse(item["ProductID"].ToString()),
                        SaleQty = Double.Parse(item["SaleQty"].ToString()),
                        OrderQty = Double.Parse(item["OrderQty"].ToString()),
                        Price = Double.Parse(item["Price"].ToString()),
                        TaxableValue = Double.Parse(item["TaxableValue"].ToString()),
                        Discount = Double.Parse(item["Discount"].ToString()),
                        CGST = Double.Parse(item["CGST"].ToString()),
                        SGST = Double.Parse(item["SGST"].ToString()),
                        IGST = Double.Parse(item["IGST"].ToString()),
                        NetTotal = Double.Parse(item["NetTotal"].ToString()),
                    };
                    tmpInvoiceItem.ProductName = ObjProductMasterModel.GetProductDetails(tmpInvoiceItem.ProductID).ItemName;

                    CurrInvoiceDetails.ListInvoiceItems.Add(tmpInvoiceItem);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.FillInvoiceItemDetails()", ex);
                throw;
            }
        }

        public List<InvoiceDetails> GetInvoiceDetailsForCustomer(DateTime InvoiceDate, Int32 CustomerID)
        {
            try
            {
                if (ListInvoices.Count == 0) LoadInvoiceDetails(InvoiceDate, InvoiceDate, INVOICESTATUS.Created, "CustomerID", CustomerID.ToString());
                if (ListInvoices.Count == 0) return null;

                List<InvoiceDetails> ListInvoicesForCustomer = ListInvoices.FindAll(e => e.CustomerID == CustomerID);
                if (ListInvoicesForCustomer == null || ListInvoicesForCustomer.Count == 0) return null;

                for (int i = 0; i < ListInvoicesForCustomer.Count; i++)
                {
                    FillInvoiceItemDetails(ListInvoicesForCustomer[i]);
                }

                return ListInvoicesForCustomer;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetInvoiceDetailsForCustomer()", ex);
                throw;
            }
        }

        public InvoiceDetails GetInvoiceDetailsForInvoiceID(Int32 InvoiceID)
        {
            try
            {
                if (ListInvoices.Count == 0) LoadInvoiceDetails(DateTime.MinValue, DateTime.MinValue, INVOICESTATUS.Created, "InvoiceID", InvoiceID.ToString());
                if (ListInvoices.Count == 0) return null;

                InvoiceDetails ObjInvoiceDetails = ListInvoices.Find(e => e.InvoiceID == InvoiceID);
                if (ObjInvoiceDetails == null) return null;

                FillInvoiceItemDetails(ObjInvoiceDetails);

                return ObjInvoiceDetails;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetInvoiceDetailsForInvoiceID()", ex);
                throw;
            }
        }

        public InvoiceDetails CreateNewInvoiceForCustomer(Int32 CustomerID, Int32 OrderID, DateTime InvoiceDate, String InvoiceNumber, List<InvoiceItemDetails> ListInvoiceItems)
        {
            try
            {
                InvoiceDetails NewInvoiceDetails = new InvoiceDetails()
                {
                    InvoiceDate = InvoiceDate,
                    InvoiceNumber = InvoiceNumber,
                    InvoiceStatus = INVOICESTATUS.Created,
                    NetInvoiceAmount = ListInvoiceItems.Sum(e => e.Price * e.OrderQty),
                    LastUpdatedDate = DateTime.Now,
                    CustomerID = CustomerID,
                    OrderID = OrderID,
                    ListInvoiceItems = ListInvoiceItems.Select(e => e.Clone()).ToList(),
                    CreationDate = DateTime.Now,
                    InvoiceItemCount = ListInvoiceItems.Count(e => e.OrderQty > 0)
                };

                NewInvoiceDetails.OrderID = InsertInvoiceDetails(NewInvoiceDetails);
                ObjMySQLHelper.UpdateIDValue("Invoices", InvoiceNumber);

                ListInvoices.Insert(0, NewInvoiceDetails);

                return NewInvoiceDetails;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreateNewInvoiceForCustomer()", ex);
                throw;
            }
        }

        Int32 InsertInvoiceDetails(InvoiceDetails ObjInvoiceDetails)
        {
            try
            {
                String Query = "Insert into Invoices(InvoiceNumber, InvoiceDate, CreationDate, LastUpdatedDate, CustomerID, OrderID, InvoiceItemCount, NetInvoiceAmount, InvoiceStatus) Values (";
                Query += $"'{ObjInvoiceDetails.InvoiceNumber}', '{MySQLHelper.GetDateStringForDB(ObjInvoiceDetails.InvoiceDate)}', '{MySQLHelper.GetDateStringForDB(ObjInvoiceDetails.CreationDate)}',";
                Query += $"'{MySQLHelper.GetDateStringForDB(ObjInvoiceDetails.LastUpdatedDate)}', {ObjInvoiceDetails.CustomerID}, {ObjInvoiceDetails.OrderID}, {ObjInvoiceDetails.InvoiceItemCount}, " +
                         $"{ObjInvoiceDetails.NetInvoiceAmount}, '{ObjInvoiceDetails.InvoiceStatus}'";
                Query += ")";
                ObjMySQLHelper.ExecuteNonQuery(Query);

                Int32 InvoiceID = -1;
                Query = $"Select Max(InvoiceID) from Invoices where CustomerID = {ObjInvoiceDetails.CustomerID} and InvoiceDate = '{MySQLHelper.GetDateStringForDB(ObjInvoiceDetails.InvoiceDate)}'";
                foreach (var item in ObjMySQLHelper.ExecuteQuery(Query)) InvoiceID = Int32.Parse(item[0].ToString());

                InsertInvoiceItems(ObjInvoiceDetails.ListInvoiceItems, InvoiceID);

                return InvoiceID;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.InsertInvoiceDetails()", ex);
                throw;
            }
        }

        private void InsertInvoiceItems(List<InvoiceItemDetails> ListInvoiceItems, Int32 InvoiceID)
        {
            try
            {
                String Query = "";
                foreach (var item in ListInvoiceItems)
                {
                    Query = "Insert into InvoiceItems(InvoiceID, ProductID, OrderQty, Price) Values (";
                    Query += $"{InvoiceID}, {item.ProductID}, {item.OrderQty}, {item.Price}";
                    Query += ")";
                    ObjMySQLHelper.ExecuteNonQuery(Query);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.InsertInvoiceItems()", ex);
            }
        }

        public void UpdateInvoiceDetails(InvoiceDetails ObjInvoiceDetails)
        {
            try
            {
                //Find Items Modified & Added
                InvoiceDetails ObjInvoiceDetailsOrig = GetInvoiceDetailsForInvoiceID(ObjInvoiceDetails.InvoiceID);
                List<InvoiceItemDetails> ListItemsModified = new List<InvoiceItemDetails>();
                List<InvoiceItemDetails> ListItemsAdded = new List<InvoiceItemDetails>();
                Int32 InvoiceItemCount = 0;
                Double NetInvoiceAmount = 0;
                for (int i = 0; i < ObjInvoiceDetails.ListInvoiceItems.Count; i++)
                {
                    Int32 Index = ObjInvoiceDetailsOrig.ListInvoiceItems.FindIndex(e => e.ProductID == ObjInvoiceDetails.ListInvoiceItems[i].ProductID);
                    if (Index < 0) ListItemsAdded.Add(ObjInvoiceDetails.ListInvoiceItems[i]);
                    else ListItemsModified.Add(ObjInvoiceDetails.ListInvoiceItems[i]);
                    if (ObjInvoiceDetails.ListInvoiceItems[i].OrderQty > 0)
                    {
                        InvoiceItemCount++;
                        NetInvoiceAmount += (ObjInvoiceDetails.ListInvoiceItems[i].OrderQty * ObjInvoiceDetails.ListInvoiceItems[i].Price);
                    }
                }

                //Update Modified Items
                for (int i = 0; i < ListItemsModified.Count; i++)
                {
                    ObjMySQLHelper.UpdateTableDetails("InvoiceItems", new List<String>() { "InvoiceQty", "Price" },
                                                new List<String>() { ListItemsModified[i].OrderQty.ToString(), ListItemsModified[i].Price.ToString() },
                                                new List<Types>() { Types.Number, Types.Number, Types.String },
                                                $"OrderID = {ObjInvoiceDetails.InvoiceID} and ProductID = {ListItemsModified[i].ProductID}");
                }

                //Insert New Items
                InsertInvoiceItems(ListItemsAdded, ObjInvoiceDetails.InvoiceID);
                
                //Update InvoiceItemCount
                ObjMySQLHelper.UpdateTableDetails("Invoices", new List<String>() { "InvoiceItemCount", "NetInvoiceAmount" },
                                            new List<String>() { InvoiceItemCount.ToString(), NetInvoiceAmount.ToString() },
                                            new List<Types>() { Types.Number, Types.Number }, $"InvoiceID = {ObjInvoiceDetails.InvoiceID}");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateInvoiceDetails()", ex);
                throw;
            }
        }

        public void PrintInvoice(ReportType EnumReportType, Boolean IsDummyBill, InvoiceDetails ObjInvoiceDetails)
        {
            Excel.Application xlApp = new Excel.Application();
            try
            {
                Boolean PrintOldBalance = false;
                ReportSettings CurrReportSettings = null;
                String BillNumberText = "", SaveFileName = "";
                String OutputFolder = Path.GetTempPath();
                String SelectedDateTimeString = ObjInvoiceDetails.InvoiceDate.ToString("dd-MM-yyyy");
                Boolean PrintBill = false;
                Boolean CreateSummary = false;
                switch (EnumReportType)
                {
                    case ReportType.INVOICE:
                        CurrReportSettings = CommonFunctions.ObjInvoiceSettings;
                        BillNumberText = "Invoice#";
                        SaveFileName = OutputFolder + "\\Invoice_" + SelectedDateTimeString + ".xlsx";
                        PrintBill = CommonFunctions.ObjGeneralSettings.IsCustomerBillPrintFormatInvoice;
                        CreateSummary = (CommonFunctions.ObjGeneralSettings.SummaryLocation == 0);
                        break;
                    case ReportType.QUOTATION:
                        CurrReportSettings = CommonFunctions.ObjQuotationSettings;
                        PrintOldBalance = true;
                        BillNumberText = "Quotation#";
                        SaveFileName = OutputFolder + "\\Quotation_" + SelectedDateTimeString + ".xlsx";
                        PrintBill = CommonFunctions.ObjGeneralSettings.IsCustomerBillPrintFormatQuotation;
                        CreateSummary = (CommonFunctions.ObjGeneralSettings.SummaryLocation == 1);
                        break;
                    default:
                        return;
                }

                if (IsDummyBill)
                {
                    SaveFileName = Path.GetDirectoryName(SaveFileName) + "\\" + Path.GetFileNameWithoutExtension(SaveFileName) + "_Dummy" + Path.GetExtension(SaveFileName);
                }

                Int32 ValidItemCount = ObjInvoiceDetails.ListInvoiceItems.Count;
                Int32 ProgressBarCount = ValidItemCount;
                Int32 Counter = 0, SLNo = 0;
                Double Quantity, Price;
                String OrderQuantity = "";

                SLNo = 0;
                CustomerDetails ObjCurrentSeller = CommonFunctions.ObjCustomerMasterModel.GetCustomerDetails(ObjInvoiceDetails.CustomerName);
                DiscountGroupDetails1 ObjDiscountGroup = CommonFunctions.ObjCustomerMasterModel.GetCustomerDiscount(ObjCurrentSeller.CustomerName);
                Double DiscountPerc = 0, DiscountValue = 0;
                if (ObjDiscountGroup.DiscountType == DiscountTypes.PERCENT)
                    DiscountPerc = ObjDiscountGroup.Discount;
                else if (ObjDiscountGroup.DiscountType == DiscountTypes.ABSOLUTE)
                    DiscountValue = ObjDiscountGroup.Discount;

                Invoice ObjInvoice = CommonFunctions.GetInvoiceTemplate(EnumReportType);
                ObjInvoice.SerialNumber = ObjInvoiceDetails.InvoiceNumber;
                ObjInvoice.InvoiceNumberText = BillNumberText;
                ObjInvoice.ObjSellerDetails = ObjCurrentSeller.Clone();
                ObjInvoice.OldBalance = 0;//TODO:Double.Parse(lblBalanceAmountValue.Text);
                ObjInvoice.CurrReportSettings = CurrReportSettings;
                ObjInvoice.DateOfInvoice = ObjInvoiceDetails.InvoiceDate;
                ObjInvoice.PrintOldBalance = PrintOldBalance;
                ObjInvoice.UseNumberToWordsFormula = false;
                ObjInvoice.ListProducts = new List<ProductDetailsForInvoice>();

                Excel.Workbook xlWorkbook = xlApp.Workbooks.Add();
                Excel.Worksheet xlWorkSheet = xlWorkbook.Sheets[1];
                String SheetName = ObjCurrentSeller.CustomerName.Replace(":", "").Replace("\\", "").Replace("/", "").
                        Replace("?", "").Replace("*", "").Replace("[", "").Replace("]", "");
                SheetName = ((SheetName.Length > 30) ? SheetName.Substring(0, 30) : SheetName);
                ObjInvoice.SheetName = SheetName;

                #region Print Invoice Items
                String ItemName;
                for (int i = 0; i < ObjInvoiceDetails.ListInvoiceItems.Count; i++)
                {
                    Counter++;

                    OrderQuantity = ObjInvoiceDetails.ListInvoiceItems[i].OrderQty.ToString();
                    Quantity = ObjInvoiceDetails.ListInvoiceItems[i].OrderQty;
                    ItemName = ObjInvoiceDetails.ListInvoiceItems[i].ProductName;
                    if (Quantity == 0) continue;
                    Price = ObjInvoiceDetails.ListInvoiceItems[i].Price;
                    Price = Price / ((CommonFunctions.ObjProductMaster.GetTaxRatesForProduct(ItemName).Sum() + 100) / 100);

                    SLNo++;
                    ProductDetailsForInvoice ObjProductDetailsForInvoice = new ProductDetailsForInvoice();
                    ProductDetails ObjProductDetails = CommonFunctions.ObjProductMaster.GetProductDetails(ItemName);
                    ObjProductDetailsForInvoice.SerialNumber = SLNo;
                    ObjProductDetailsForInvoice.Description = ObjProductDetails.ItemName;
                    ObjProductDetailsForInvoice.HSNCode = ObjProductDetails.HSNCode;
                    ObjProductDetailsForInvoice.UnitsOfMeasurement = ObjProductDetails.UnitsOfMeasurement;
                    ObjProductDetailsForInvoice.OrderQuantity = OrderQuantity;
                    ObjProductDetailsForInvoice.SaleQuantity = (IsDummyBill ? 0 : Quantity);
                    ObjProductDetailsForInvoice.Rate = Price; //CommonFunctions.ObjProductMaster.GetPriceForProduct(ObjProductDetails.ItemName, ObjCurrentSeller.PriceGroupIndex);

                    Double[] TaxRates = CommonFunctions.ObjProductMaster.GetTaxRatesForProduct(ObjProductDetails.ItemName);
                    ObjProductDetailsForInvoice.CGSTDetails = new TaxDetails();
                    ObjProductDetailsForInvoice.CGSTDetails.TaxRate = TaxRates[0] / 100;
                    ObjProductDetailsForInvoice.SGSTDetails = new TaxDetails();
                    ObjProductDetailsForInvoice.SGSTDetails.TaxRate = TaxRates[1] / 100;
                    ObjProductDetailsForInvoice.IGSTDetails = new TaxDetails();
                    ObjProductDetailsForInvoice.IGSTDetails.TaxRate = TaxRates[2] / 100;
                    ObjProductDetailsForInvoice.DiscountGroup = ObjDiscountGroup.Clone();
                    if (DiscountPerc > 0)
                    {
                        ObjProductDetailsForInvoice.DiscountGroup.DiscountType = DiscountTypes.PERCENT;
                        ObjProductDetailsForInvoice.DiscountGroup.Discount = DiscountPerc;
                    }
                    else if (DiscountValue > 0)
                    {
                        ObjProductDetailsForInvoice.DiscountGroup.DiscountType = DiscountTypes.ABSOLUTE;
                        ObjProductDetailsForInvoice.DiscountGroup.Discount = (DiscountValue / ObjInvoiceDetails.ListInvoiceItems.Count);
                    }
                    ObjInvoice.ListProducts.Add(ObjProductDetailsForInvoice);
                }
                #endregion

                #region Discount
                if (EnumReportType == ReportType.INVOICE)
                {
                    ObjInvoice.CreateInvoice(xlWorkSheet);
                }
                else if (EnumReportType == ReportType.QUOTATION)
                {
                    //Override Discount and rollback after creating Quotation
                    DiscountGroupDetails1 OrigDiscountGroup = ObjDiscountGroup.Clone();
                    if (DiscountPerc > 0)
                    {
                        ObjDiscountGroup.DiscountType = DiscountTypes.PERCENT;
                        ObjDiscountGroup.Discount = DiscountPerc;
                    }
                    else if (DiscountValue > 0)
                    {
                        ObjDiscountGroup.DiscountType = DiscountTypes.ABSOLUTE;
                        ObjDiscountGroup.Discount = DiscountValue;
                    }
                    ObjInvoice.CreateInvoice(xlWorkSheet);
                    ObjDiscountGroup.DiscountType = OrigDiscountGroup.DiscountType;
                    ObjDiscountGroup.Discount = OrigDiscountGroup.Discount;
                }
                #endregion

                xlApp.DisplayAlerts = true;
                Excel.Worksheet FirstWorksheet = xlWorkbook.Sheets[1];
                FirstWorksheet.Select();

                #region Print Invoice
                if (PrintBill && CommonFunctions.ObjGeneralSettings.InvoiceQuotPrintCopies > 0)
                {
                    //DialogResult result = MessageBox.Show(this, "Would you like to print the " + InvoiceQuotation + "?", "Sales " + InvoiceQuotation, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    //if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        xlWorkSheet.PrintOutEx(Type.Missing, Type.Missing, CommonFunctions.ObjGeneralSettings.InvoiceQuotPrintCopies);
                    }
                }
                #endregion

                xlWorkbook.Close(SaveChanges: !IsDummyBill);
                CommonFunctions.ReleaseCOMObject(xlWorkbook);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.PrintInvoice()", ex);
            }
            finally
            {
                xlApp.Quit();
                CommonFunctions.ReleaseCOMObject(xlApp);
            }
        }
    }
}
