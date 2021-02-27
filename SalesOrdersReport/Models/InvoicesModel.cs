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
        Created, Delivered, Paid, Cancelled, Void, All
    };

    enum INVOICEITEMSTATUS
    {
        Invoiced, Cancelled, OutOfStock
    };

    class InvoiceDetails
    {
        public Int32 InvoiceID;
        public String InvoiceNumber;
        public DateTime InvoiceDate, CreationDate, LastUpdatedDate;
        public Int32 CustomerID, OrderID;
        public String CustomerName;
        public Double GrossInvoiceAmount, DiscountAmount, NetInvoiceAmount;
        public INVOICESTATUS InvoiceStatus;
        public List<InvoiceItemDetails> ListInvoiceItems;
        public Int32 InvoiceItemCount = 0;

        public InvoiceDetails Clone()
        {
            try
            {
                InvoiceDetails ObjInvoiceDetailsCopy = (InvoiceDetails)MemberwiseClone();
                ObjInvoiceDetailsCopy.ListInvoiceItems = this.ListInvoiceItems.Select(e => e.Clone()).ToList();

                return ObjInvoiceDetailsCopy;
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
        public Int32 InvoiceID, ProductID;
        public String ProductName;
        public Double OrderQty, SaleQty, Price, TaxableValue, CGST, SGST, IGST, NetTotal;
        public INVOICEITEMSTATUS InvoiceItemStatus;

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
        DataTable dtAllInvoices;
        List<InvoiceDetails> ListInvoices;
        MySQLHelper ObjMySQLHelper;
        const String InvoiceNumberPrefix = "INV", BillNumberPrefix = "BILL";
        ProductMasterModel ObjProductMasterModel;
        Boolean IsBill = false;

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

        public String GenerateNewBillNumber()
        {
            try
            {
                String MaxInvoiceNumber = ObjMySQLHelper.GetIDValue("Bills");
                if (String.IsNullOrEmpty(MaxInvoiceNumber)) MaxInvoiceNumber = "0";
                IsBill = true;
                return CommonFunctions.GenerateNextID(BillNumberPrefix, MaxInvoiceNumber);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GenerateNewBillNumber()", ex);
                throw;
            }
        }

        public DataTable LoadInvoiceDetails(DateTime FromDate, DateTime ToDate, INVOICESTATUS InvoiceStatus = INVOICESTATUS.Created,
                                    String SearchField = null, String SearchFieldValue = null, string WhereCondition = null)
        {
            try
            {
                String[] ArrDtColumns1 = new string[] { "InvoiceID", "CustomerID", "OrderID", "InvoiceNumber", "InvoiceDate" };
                String[] ArrDtColumns2 = new string[] { "InvoiceItemCount", "GrossInvoiceAmount", "DiscountAmount", "NetInvoiceAmount", "InvoiceStatus", "CreationDate", "LastUpdatedDate" };

                String[] ArrColumns = new string[] { "InvoiceID", "CustomerID", "OrderID", "Invoice Number", "Invoice Date", "Customer Name", "Invoice Item Count", "Gross Invoice Amount", "Discount Amount", "Net Invoice Amount",
                                                    "Invoice Status", "Creation Date", "Last Updated Date" };

                String Query = $"Select a.{String.Join(", a.", ArrDtColumns1)}, b.CustomerName, a.{String.Join(", a.", ArrDtColumns2)} from Invoices a Inner Join CUSTOMERMASTER b on a.CustomerID = b.CustomerID", WhereClause = $" Where 1 = 1";
                if (FromDate > DateTime.MinValue && ToDate > DateTime.MinValue)
                {
                    WhereClause += $" and DATE(a.InvoiceDate) between '{MySQLHelper.GetDateStringForDB(FromDate)}' and '{MySQLHelper.GetDateStringForDB(ToDate)}'";
                }
                if (InvoiceStatus != INVOICESTATUS.All)
                {
                    WhereClause += $" and a.InvoiceStatus = '{InvoiceStatus}'";
                }
                if (WhereCondition != null) WhereClause += WhereCondition;
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
                        case "LINE":
                            Query += $" Inner Join LINEMASTER c on b.LineID = c.LineID";
                            WhereClause += $" and c.LineName = '{SearchFieldValue}'";
                            break;
                        case "LINEID":
                            WhereClause += $" and b.LineID = '{SearchFieldValue}'";
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
                        InvoiceNumber = dtRow["InvoiceNumber"].ToString(),
                        InvoiceDate = DateTime.Parse(dtRow["InvoiceDate"].ToString()),
                        CustomerID = Int32.Parse(dtRow["CustomerID"].ToString()),
                        CustomerName = dtRow["CustomerName"].ToString(),
                        OrderID = Int32.Parse(dtRow["OrderID"].ToString()),
                        InvoiceItemCount = Int32.Parse(dtRow["InvoiceItemCount"].ToString()),
                        GrossInvoiceAmount = Double.Parse(dtRow["GrossInvoiceAmount"].ToString()),
                        DiscountAmount = Double.Parse(dtRow["DiscountAmount"].ToString()),
                        NetInvoiceAmount = Double.Parse(dtRow["NetInvoiceAmount"].ToString()),
                        InvoiceStatus = (INVOICESTATUS)Enum.Parse(Type.GetType("SalesOrdersReport.Models.INVOICESTATUS"), dtRow["InvoiceStatus"].ToString()),
                        CreationDate = DateTime.Parse(dtRow["CreationDate"].ToString()),
                        LastUpdatedDate = DateTime.Parse(dtRow["LastUpdatedDate"].ToString())
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
                CommonFunctions.ShowErrorDialog($"{this}.LoadInvoiceDetails()", ex);
                throw;
            }
        }

        public Int32 DeleteInvoiceDetails(Int32 InvoiceID)
        {
            try
            {
                ObjMySQLHelper.UpdateTableDetails("Invoices", new List<string>() { "InvoiceStatus" }, new List<string>() { INVOICESTATUS.Cancelled.ToString() }, 
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
                        InvoiceID = Int32.Parse(item["InvoiceID"].ToString()),
                        ProductID = Int32.Parse(item["ProductID"].ToString()),
                        OrderQty = Double.Parse(item["OrderQty"].ToString()),
                        SaleQty = Double.Parse(item["SaleQty"].ToString()),
                        Price = Double.Parse(item["Price"].ToString()),
                        TaxableValue = Double.Parse(item["TaxableValue"].ToString()),
                        CGST = Double.Parse(item["CGST"].ToString()),
                        SGST = Double.Parse(item["SGST"].ToString()),
                        IGST = Double.Parse(item["IGST"].ToString()),
                        NetTotal = Double.Parse(item["NetTotal"].ToString())
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

        public List<InvoiceDetails> GetInvoiceDetailsForCustomer(Int32 CustomerID, string WhereCondition = null)
        {
            try
            {
                if (ListInvoices.Count == 0) LoadInvoiceDetails(DateTime.MinValue, DateTime.MinValue, INVOICESTATUS.All, "CustomerID", CustomerID.ToString(), WhereCondition);
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

        public List<InvoiceDetails> GetInvoiceDetailsForCustomer(DateTime InvoiceDate, Int32 CustomerID)
        {
            try
            {
                if (ListInvoices.Count == 0) LoadInvoiceDetails(InvoiceDate, InvoiceDate, INVOICESTATUS.All, "CustomerID", CustomerID.ToString());
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
                if (ListInvoices.Count == 0) LoadInvoiceDetails(DateTime.MinValue, DateTime.MinValue, INVOICESTATUS.All, "InvoiceID", InvoiceID.ToString());
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

        public InvoiceDetails CreateNewInvoiceForCustomer(Int32 CustomerID, Int32 OrderID, DateTime InvoiceDate, String InvoiceNumber, 
                                                    List<InvoiceItemDetails> ListInvoiceItems, Double Discount)
        {
            try
            {
                InvoiceDetails NewInvoiceDetails = new InvoiceDetails()
                {
                    InvoiceDate = InvoiceDate,
                    InvoiceNumber = InvoiceNumber,
                    InvoiceStatus = INVOICESTATUS.Created,
                    GrossInvoiceAmount = ListInvoiceItems.Sum(e => e.Price * e.SaleQty),
                    DiscountAmount = Discount,
                    NetInvoiceAmount = ListInvoiceItems.Sum(e => e.Price * e.SaleQty) - Discount,
                    LastUpdatedDate = DateTime.Now,
                    CustomerID = CustomerID,
                    OrderID = OrderID,
                    ListInvoiceItems = ListInvoiceItems.Select(e => e.Clone()).ToList(),
                    CreationDate = DateTime.Now,
                    InvoiceItemCount = ListInvoiceItems.Count(e => e.OrderQty > 0),
                };

                NewInvoiceDetails.CustomerName = CommonFunctions.ObjCustomerMasterModel.GetCustomerDetails(CustomerID).CustomerName;
                NewInvoiceDetails.InvoiceID = InsertInvoiceDetails(NewInvoiceDetails);
                if (IsBill) ObjMySQLHelper.UpdateIDValue("Bills", InvoiceNumber);
                else ObjMySQLHelper.UpdateIDValue("Invoices", InvoiceNumber);

                Object[] ArrItems = new Object[] {
                    NewInvoiceDetails.InvoiceID,
                    NewInvoiceDetails.CustomerID,
                    NewInvoiceDetails.OrderID,
                    NewInvoiceDetails.InvoiceNumber,
                    new MySql.Data.Types.MySqlDateTime(NewInvoiceDetails.InvoiceDate),
                    NewInvoiceDetails.CustomerName,
                    NewInvoiceDetails.InvoiceItemCount,
                    NewInvoiceDetails.GrossInvoiceAmount,
                    NewInvoiceDetails.DiscountAmount,
                    NewInvoiceDetails.NetInvoiceAmount,
                    NewInvoiceDetails.InvoiceStatus,
                    new MySql.Data.Types.MySqlDateTime(NewInvoiceDetails.CreationDate),
                    new MySql.Data.Types.MySqlDateTime(NewInvoiceDetails.LastUpdatedDate)
                };
                if (dtAllInvoices != null)
                {
                    ListInvoices.Add(NewInvoiceDetails);
                    dtAllInvoices.Rows.Add(ArrItems);
                }

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
                String Query = "Insert into Invoices(InvoiceNumber, InvoiceDate, CustomerID, OrderID, InvoiceItemCount, GrossInvoiceAmount, " +
                                "DiscountAmount, NetInvoiceAmount, InvoiceStatus, CreationDate, LastUpdatedDate) Values (";
                Query += $"'{ObjInvoiceDetails.InvoiceNumber}', '{MySQLHelper.GetDateTimeStringForDB(ObjInvoiceDetails.InvoiceDate)}',";
                Query += $"{ObjInvoiceDetails.CustomerID}, {ObjInvoiceDetails.OrderID}, {ObjInvoiceDetails.InvoiceItemCount}, " +
                         $"{ObjInvoiceDetails.GrossInvoiceAmount}, {ObjInvoiceDetails.DiscountAmount}, " +
                         $"{ObjInvoiceDetails.NetInvoiceAmount}, '{ObjInvoiceDetails.InvoiceStatus}', " +
                         $"'{MySQLHelper.GetDateTimeStringForDB(ObjInvoiceDetails.CreationDate)}', '{MySQLHelper.GetDateTimeStringForDB(ObjInvoiceDetails.LastUpdatedDate)}'";
                Query += ")";
                ObjMySQLHelper.ExecuteNonQuery(Query);

                Int32 InvoiceID = -1;
                Query = $"Select Max(InvoiceID) from Invoices where CustomerID = {ObjInvoiceDetails.CustomerID} and InvoiceDate = '{MySQLHelper.GetDateTimeStringForDB(ObjInvoiceDetails.InvoiceDate)}'";
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
                    Double[] TaxRates = ObjProductMasterModel.GetTaxRatesForProduct(item.ProductName);
                    item.TaxableValue = item.SaleQty * item.Price / ((100.0 + TaxRates.Sum()) / 100.0);
                    item.CGST = item.TaxableValue * TaxRates[0] / 100.0;
                    item.SGST = item.TaxableValue * TaxRates[1] / 100.0;
                    item.IGST = item.TaxableValue * TaxRates[2] / 100.0;
                    item.NetTotal = item.TaxableValue + (item.TaxableValue * TaxRates.Sum() / 100.0);

                    Query = "Insert into InvoiceItems(InvoiceID, ProductID, OrderQty, SaleQty, Price, " +
                            "TaxableValue, CGST, SGST, IGST, NetTotal, InvoiceItemStatus) Values (";
                    Query += $"{InvoiceID}, {item.ProductID}, {item.OrderQty}, {item.SaleQty}, {item.Price}, " +
                            $"{item.TaxableValue}, {item.CGST}, {item.SGST}, {item.IGST}, {item.NetTotal}, '{item.InvoiceItemStatus}')";
                    ObjMySQLHelper.ExecuteNonQuery(Query);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.InsertInvoiceItems()", ex);
                throw;
            }
        }

        public InvoiceDetails UpdateInvoiceDetails(InvoiceDetails ObjInvoiceDetails)
        {
            try
            {
                //Find Items Modified & Added
                InvoiceDetails ObjInvoiceDetailsOrig = GetInvoiceDetailsForInvoiceID(ObjInvoiceDetails.InvoiceID);
                List<InvoiceItemDetails> ListItemsModified = new List<InvoiceItemDetails>();
                List<InvoiceItemDetails> ListItemsAdded = new List<InvoiceItemDetails>();
                Int32 InvoiceItemCount = 0;
                Double NetInvoiceAmount = 0;
                for (Int32 i = 0; i < ObjInvoiceDetails.ListInvoiceItems.Count; i++)
                {
                    Int32 Index = ObjInvoiceDetailsOrig.ListInvoiceItems.FindIndex(e => e.ProductID == ObjInvoiceDetails.ListInvoiceItems[i].ProductID);
                    if (Index < 0) ListItemsAdded.Add(ObjInvoiceDetails.ListInvoiceItems[i]);
                    else ListItemsModified.Add(ObjInvoiceDetails.ListInvoiceItems[i]);
                    if (ObjInvoiceDetails.ListInvoiceItems[i].SaleQty > 0)
                    {
                        InvoiceItemCount++;
                        NetInvoiceAmount += (ObjInvoiceDetails.ListInvoiceItems[i].SaleQty * ObjInvoiceDetails.ListInvoiceItems[i].Price);
                    }
                }

                //Find Deleted Items
                List<InvoiceItemDetails> ListItemsDeleted = new List<InvoiceItemDetails>();
                for (int i = 0; i < ObjInvoiceDetailsOrig.ListInvoiceItems.Count; i++)
                {
                    Int32 Index = ObjInvoiceDetails.ListInvoiceItems.FindIndex(e => e.ProductID == ObjInvoiceDetailsOrig.ListInvoiceItems[i].ProductID);
                    if (Index < 0) ListItemsDeleted.Add(ObjInvoiceDetailsOrig.ListInvoiceItems[i]);
                }

                //Update Modified Items
                for (Int32 i = 0; i < ListItemsModified.Count; i++)
                {
                    ObjMySQLHelper.UpdateTableDetails("InvoiceItems", new List<String>() { "OrderQty", "SaleQty", "Price", "TaxableValue",
                                                                                    "CGST", "SGST", "IGST", "NetTotal", "InvoiceItemStatus" },
                                                new List<String>() {
                                                ListItemsModified[i].OrderQty.ToString(),
                                                ListItemsModified[i].SaleQty.ToString(),
                                                ListItemsModified[i].Price.ToString(),
                                                ListItemsModified[i].TaxableValue.ToString(),
                                                ListItemsModified[i].CGST.ToString(),
                                                ListItemsModified[i].CGST.ToString(),
                                                ListItemsModified[i].IGST.ToString(),
                                                ListItemsModified[i].NetTotal.ToString(),
                                                ListItemsModified[i].InvoiceItemStatus.ToString()
                                                },
                                                new List<Types>() { Types.Number, Types.Number, Types.Number, Types.Number,
                                                                Types.Number, Types.Number, Types.Number, Types.Number, Types.String},
                                                $"InvoiceID = {ObjInvoiceDetails.InvoiceID} and ProductID = {ListItemsModified[i].ProductID}");
                }


                //Update status for Deleted Items
                for (int i = 0; i < ListItemsDeleted.Count; i++)
                {
                    ObjMySQLHelper.UpdateTableDetails("InvoiceItems", new List<String>() { "InvoiceItemStatus" },
                                                new List<String>() { INVOICEITEMSTATUS.Cancelled.ToString() },
                                                new List<Types>() { Types.String },
                                                $"InvoiceID = {ObjInvoiceDetails.InvoiceID} and ProductID = {ListItemsDeleted[i].ProductID}");
                }

                //Insert New Items
                InsertInvoiceItems(ListItemsAdded, ObjInvoiceDetails.InvoiceID);

                //Update InvoiceItemCount
                ObjMySQLHelper.UpdateTableDetails("Invoices", new List<String>() { "InvoiceItemCount", "NetInvoiceAmount", "InvoiceStatus" },
                                            new List<String>() { InvoiceItemCount.ToString(), NetInvoiceAmount.ToString(), ObjInvoiceDetails.InvoiceStatus.ToString() },
                                            new List<Types>() { Types.Number, Types.Number, Types.String }, $"InvoiceID = {ObjInvoiceDetails.InvoiceID}");

                FillInvoiceItemDetails(ObjInvoiceDetails);

                return ObjInvoiceDetails;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateInvoiceDetails()", ex);
                throw;
            }
        }

        private void UpdateInvoiceStatus(Int32 InvoiceID, INVOICESTATUS NewInvoiceStatus)
        {
            try
            {
                ObjMySQLHelper.UpdateTableDetails("Invoices", new List<String>() { "InvoiceStatus" },
                            new List<String>() { NewInvoiceStatus.ToString() },
                            new List<Types>() { Types.String }, $"InvoiceID = {InvoiceID}");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateInvoiceStatus()", ex);
            }
        }

        public void ExportInvoice(ReportType EnumReportType, Boolean IsDummyBill, Excel.Workbook ExcelWorkbook, InvoiceDetails ObjInvoiceDetails)
        {
            try
            {
                Boolean PrintOldBalance = false;
                ReportSettings CurrReportSettings = null;
                String BillNumberText = "";
                String SelectedDateTimeString = ObjInvoiceDetails.InvoiceDate.ToString("dd-MM-yyyy");
                Boolean CreateSummary = false;
                switch (EnumReportType)
                {
                    case ReportType.INVOICE:
                        CurrReportSettings = CommonFunctions.ObjInvoiceSettings;
                        PrintOldBalance = true;
                        BillNumberText = "Invoice#";
                        CreateSummary = (CommonFunctions.ObjGeneralSettings.SummaryLocation == 0);
                        break;
                    case ReportType.QUOTATION:
                        CurrReportSettings = CommonFunctions.ObjQuotationSettings;
                        PrintOldBalance = false;
                        BillNumberText = "Quotation#";
                        CreateSummary = (CommonFunctions.ObjGeneralSettings.SummaryLocation == 1);
                        break;
                    default:
                        break;
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

                Excel.Worksheet xlWorkSheet = ExcelWorkbook.Sheets.Add(Type.Missing, ExcelWorkbook.Sheets[ExcelWorkbook.Sheets.Count]);
                String SheetName = ObjCurrentSeller.CustomerName.Replace(":", "").Replace("\\", "").Replace("/", "").
                        Replace("?", "").Replace("*", "").Replace("[", "").Replace("]", "");
                SheetName = ((SheetName.Length > 30) ? SheetName.Substring(0, 30) : SheetName);
                ObjInvoice.SheetName = CommonFunctions.GetWorksheetNameToAppend(SheetName, ExcelWorkbook);

                #region Print Invoice Items
                String ItemName;
                for (int i = 0; i < ObjInvoiceDetails.ListInvoiceItems.Count; i++)
                {
                    Counter++;

                    OrderQuantity = ObjInvoiceDetails.ListInvoiceItems[i].OrderQty.ToString();
                    Quantity = ObjInvoiceDetails.ListInvoiceItems[i].SaleQty;
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

                ObjInvoice.CreateInvoice(xlWorkSheet);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ExportInvoice()", ex);
                throw;
            }
        }

        public void ExportCustomerSummary(Excel.Workbook xlWorkbook, List<InvoiceDetails> ListInvoiceDetails)
        {
            try
            {
                String SheetName = "Customer Summary";
                SheetName = CommonFunctions.GetWorksheetNameToAppend(SheetName, xlWorkbook);

                Excel.Worksheet xlWorksheet = xlWorkbook.Worksheets.Add(xlWorkbook.Worksheets[1]);
                xlWorksheet.Name = SheetName;

                String InvoiceIDs = String.Join(",", ListInvoiceDetails.Select(e => e.InvoiceID));

                String Query = "Select row_number() over () 'Sl#', a.* from (Select d.LineName Line, b.InvoiceNumber 'Invoice#', " +
                                "c.CustomerName, Sum(a.SaleQty * a.Price) Sale, Sum(null) Cancel, Sum(null) 'Return', Sum(a.Discount) Discount, " +
                                "Sum(a.CGST + a.SGST + a.IGST) 'Total Tax', Sum((a.SaleQty * a.Price) - a.Discount + (a.CGST + a.SGST + a.IGST)) 'Net Sale', " +
                                "Sum(0) OB, Sum(null) Cash";
                Query += " from InvoiceItems a Inner join Invoices b on a.InvoiceID = b.InvoiceID";
                Query += " Inner Join CUSTOMERMASTER c on b.CustomerID = c.CustomerID";
                Query += " Inner Join LINEMASTER d on c.LineID = d.LineID";
                Query += $" Where a.InvoiceID in ({InvoiceIDs}) and a.InvoiceItemStatus = 'Invoiced' Group by d.LineName, b.InvoiceNumber, c.CustomerName " +
                            "Order by b.InvoiceNumber) a;";
                DataTable dtCustomerSummary = ObjMySQLHelper.GetQueryResultInDataTable(Query);

                Int32 SummaryStartRow = 2;
                Int32 RetVal = CommonFunctions.ExportDataTableToExcelWorksheet(dtCustomerSummary, xlWorksheet, SummaryStartRow, 1);
                if (RetVal < 0) return;

                Excel.Range xlRange = null;
                xlRange = xlWorksheet.Range[xlWorksheet.Cells[SummaryStartRow, 1], xlWorksheet.Cells[SummaryStartRow, 12]];
                xlRange.Font.Bold = true;

                xlWorksheet.Cells[1, 1].Value = "Date";
                xlWorksheet.Cells[1, 1].Font.Bold = true;
                xlWorksheet.Cells[1, 2].Value = DateTime.Now.ToShortDateString();

                xlRange = xlWorksheet.Range[xlWorksheet.Cells[SummaryStartRow + 1, 5], xlWorksheet.Cells[SummaryStartRow + 1 + dtCustomerSummary.Rows.Count + 1, 12]];
                xlRange.NumberFormat = "#,##0.00";
                xlRange = xlWorksheet.Range[xlWorksheet.Cells[SummaryStartRow, 1], xlWorksheet.Cells[SummaryStartRow + dtCustomerSummary.Rows.Count + 1, 12]];
                SellerInvoiceForm.SetAllBorders(xlRange);

                xlWorksheet.UsedRange.Columns.AutoFit();

                xlRange = xlWorksheet.Columns["B"];
                xlRange.ColumnWidth = 7;
                xlRange = xlWorksheet.Columns["C"];
                xlRange.ColumnWidth = 7;
                xlRange = xlWorksheet.Columns["D"];
                xlRange.ColumnWidth = 24;

                SellerInvoiceForm.AddPageHeaderAndFooter(ref xlWorksheet, "Customer Summary", CommonFunctions.ObjInvoiceSettings);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ExportCustomerSummary()", ex);
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

        public Int32 MarkInvoicesAsPaid(List<Int32> ListInvoiceIDsToUpdate)
        {
            try
            {
                List<Int32> ListInvoiceIDsToDeliver = new List<Int32>();
                List<InvoiceDetails> ListInvoiceDetails = new List<InvoiceDetails>();
                for (int i = 0; i < ListInvoiceIDsToUpdate.Count; i++)
                {
                    InvoiceDetails tmpInvoiceDetails = GetInvoiceDetailsForInvoiceID(ListInvoiceIDsToUpdate[i]);
                    if (tmpInvoiceDetails.InvoiceStatus == INVOICESTATUS.Paid) continue;

                    ListInvoiceDetails.Add(tmpInvoiceDetails);

                    if (tmpInvoiceDetails.InvoiceStatus == INVOICESTATUS.Created)
                    {
                        ListInvoiceIDsToDeliver.Add(ListInvoiceIDsToUpdate[i]);
                    }
                }

                if (ListInvoiceIDsToDeliver.Count > 0)
                {
                    Int32 RetVal = MarkInvoicesAsDelivered(ListInvoiceIDsToDeliver);
                    if (RetVal < 0) return RetVal;
                }

                //Update InvoiceStatus as Paid
                for (int i = 0; i < ListInvoiceDetails.Count; i++)
                {
                    ListInvoiceDetails[i].InvoiceStatus = INVOICESTATUS.Paid;
                    UpdateInvoiceStatus(ListInvoiceDetails[i].InvoiceID, ListInvoiceDetails[i].InvoiceStatus);
                }

                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.MarkInvoicesAsPaid()", ex);
                return -1;
            }
        }

        public Int32 MarkInvoicesAsDelivered(List<Int32> ListInvoiceIDsToUpdate, Boolean IsUpdateInvoiceStatus = true)
        {
            try
            {
                DateTime MinInvoiceDate = DateTime.MaxValue;
                List<InvoiceItemDetails> ListAllInvoiceItems = new List<InvoiceItemDetails>();
                List<InvoiceDetails> ListInvoiceDetails = new List<InvoiceDetails>();
                for (int i = 0; i < ListInvoiceIDsToUpdate.Count; i++)
                {
                    InvoiceDetails tmpInvoiceDetails = GetInvoiceDetailsForInvoiceID(ListInvoiceIDsToUpdate[i]);
                    MinInvoiceDate = (tmpInvoiceDetails.InvoiceDate < MinInvoiceDate ? tmpInvoiceDetails.InvoiceDate : MinInvoiceDate);

                    for (int j = 0; j < tmpInvoiceDetails.ListInvoiceItems.Count; j++)
                    {
                        InvoiceItemDetails tmpItem = tmpInvoiceDetails.ListInvoiceItems[j];
                        if (tmpItem.InvoiceItemStatus != INVOICEITEMSTATUS.Invoiced) continue;

                        Int32 ItemIndex = ListAllInvoiceItems.FindIndex(e => e.ProductID == tmpItem.ProductID);
                        if (ItemIndex < 0)
                        {
                            ListAllInvoiceItems.Add(tmpItem.Clone());
                            ItemIndex = ListAllInvoiceItems.Count - 1;
                        }
                        else
                        {
                            ListAllInvoiceItems[ItemIndex].OrderQty += tmpItem.OrderQty;
                            ListAllInvoiceItems[ItemIndex].SaleQty += tmpItem.SaleQty;
                        }
                    }
                    ListInvoiceDetails.Add(tmpInvoiceDetails.Clone());
                }

                ObjProductMasterModel.UpdateProductInventoryDataFromInvoice(ListAllInvoiceItems, MinInvoiceDate);

                if (IsUpdateInvoiceStatus)
                {
                    //Update InvoiceStatus as Delivered
                    for (int i = 0; i < ListInvoiceDetails.Count; i++)
                    {
                        ListInvoiceDetails[i].InvoiceStatus = INVOICESTATUS.Delivered;
                        UpdateInvoiceStatus(ListInvoiceDetails[i].InvoiceID, ListInvoiceDetails[i].InvoiceStatus);
                        FillInvoiceItemDetails(ListInvoiceDetails[i]);
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.MarkInvoicesAsDelivered()", ex);
                return -1;
            }
        }

        public Int32 GetInvoiceIDFromNum(string InvoiceNum, string WhereCondition = null)
        {
            try
            {
                if (ListInvoices.Count == 0) LoadInvoiceDetails(DateTime.MinValue, DateTime.MinValue, INVOICESTATUS.All, "INVOICE NUMBER", InvoiceNum.ToString(), WhereCondition);
                int Index = ListInvoices.FindIndex(e => e.InvoiceNumber == InvoiceNum);
                if (Index < 0) return -1;
                else return ListInvoices[Index].InvoiceID;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetInvoiceIDFromNum()", ex);
                return -1;
            }
        }

        public static void PrintBill(String BillNumber)
        {
            try
            {
                InvoicesModel ObjInvoicesModel = new InvoicesModel();
                ObjInvoicesModel.Initialize();

                PrintBase ObjPrintBase = new ThermalPaperBillPrinter(288);

                Int32 InvoiceID = ObjInvoicesModel.GetInvoiceIDFromNum(BillNumber);
                PrintBill(InvoiceID);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"CreatePOSBillForm.btnPrintBill_Click()", ex);
            }
        }

        public static void PrintBill(Int32 InvoiceID)
        {
            try
            {
                InvoicesModel ObjInvoicesModel = new InvoicesModel();
                ObjInvoicesModel.Initialize();

                PrintBase ObjPrintBase = new ThermalPaperBillPrinter(288);

                InvoiceDetails ObjInvoiceDetails = ObjInvoicesModel.GetInvoiceDetailsForInvoiceID(InvoiceID);
                CustomerDetails ObjCustomerDetails = CommonFunctions.ObjCustomerMasterModel.GetCustomerDetails(ObjInvoiceDetails.CustomerID);

                PrintDetails ObjPrintDetails = new PrintDetails()
                {
                    CustomerName = ObjCustomerDetails.CustomerName,
                    CustomerPhone = ObjCustomerDetails.PhoneNo.ToString(),
                    DateValue = ObjInvoiceDetails.InvoiceDate,
                    InvoiceNumber = ObjInvoiceDetails.InvoiceNumber,
                    GrossAmount = ObjInvoiceDetails.GrossInvoiceAmount,
                    DiscountAmount = ObjInvoiceDetails.DiscountAmount,
                    TotalTaxAmount = 0,
                    NetAmount = ObjInvoiceDetails.NetInvoiceAmount,
                    StaffName = MySQLHelper.GetMySqlHelperObj().CurrentUser,
                    Header1 = "Kachatathapa Kerala Super Store",
                    Header2 = "Customer Bill",
                    ListSubHeaderLines = new List<string>() { "Veerannapalaya", "Mobile: 8147354960" },
                    ListFooterLines = new List<string>() { "Thank you and Visit again" }
                };

                for (int i = 0; i < ObjInvoiceDetails.ListInvoiceItems.Count; i++)
                {
                    InvoiceItemDetails item = ObjInvoiceDetails.ListInvoiceItems[i];
                    ObjPrintDetails.ListPrintItemDetails.Add(new PrintItemDetails()
                    {
                        ItemName = item.ProductName,
                        ItemMRP = CommonFunctions.ObjProductMaster.GetPriceForProduct(item.ProductName, 2),
                        ItemRate = item.Price,
                        SaleQty = item.SaleQty,
                        Tax = 0,
                        Amount = item.Price * item.SaleQty,
                        HSNCode = CommonFunctions.ObjProductMaster.GetProductDetails(item.ProductID).HSNCode
                    });
                }

                ObjPrintBase.Print(ObjPrintDetails);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"CreatePOSBillForm.btnPrintBill_Click()", ex);
            }
        }
    }
}
