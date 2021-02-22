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
    enum ORDERSTATUS
    {
        Created, Completed, Cancelled, Void, All
    };

    enum ORDERITEMSTATUS
    {
        Ordered, Cancelled, OutOfStock
    };

    class OrderDetails
    {
        public Int32 OrderID;
        public String OrderNumber;
        public DateTime OrderDate, CreationDate, LastUpdatedDate;
        public Int32 CustomerID;
        public String CustomerName;
        public Double EstimateOrderAmount;
        public ORDERSTATUS OrderStatus;
        public DateTime DateDelivered, DateInvoiceCreated, DateQuotationCreated;
        public List<OrderItemDetails> ListOrderItems;
        public Int32 OrderItemCount = 0;

        public OrderDetails Clone()
        {
            try
            {
                OrderDetails ObjOrderDetailsCopy = (OrderDetails)MemberwiseClone();
                ObjOrderDetailsCopy.ListOrderItems = this.ListOrderItems.Select(e => e.Clone()).ToList();

                return ObjOrderDetailsCopy;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.Clone()", ex);
                throw;
            }
        }
    }

    class OrderItemDetails
    {
        public Int32 ProductID;
        public String ProductName;
        public Double OrderQty, Price;
        public ORDERITEMSTATUS OrderItemStatus;

        public OrderItemDetails Clone()
        {
            try
            {
                return (OrderItemDetails)MemberwiseClone();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.Clone()", ex);
                throw;
            }
        }
    }

    class OrdersModel
    {
        public DataTable dtAllOrders;
        List<OrderDetails> ListOrders;
        MySQLHelper ObjMySQLHelper;
        const String OrderNumberPrefix = "ORD";
        ProductMasterModel ObjProductMasterModel;

        public void Initialize()
        {
            try
            {
                ListOrders = new List<OrderDetails>();
                ObjMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                ObjProductMasterModel = CommonFunctions.ObjProductMaster;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.Initialize()", ex);
                throw;
            }
        }

        public String GenerateNewOrderNumber()
        {
            try
            {
                String MaxOrderNumber = ObjMySQLHelper.GetIDValue("Orders");
                if (String.IsNullOrEmpty(MaxOrderNumber)) MaxOrderNumber = "0";
                return CommonFunctions.GenerateNextID(OrderNumberPrefix, MaxOrderNumber);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GenerateNewOrderNumber()", ex);
                throw;
            }
        }

        public DataTable LoadOrderDetails(DateTime FromDate, DateTime ToDate, ORDERSTATUS OrderStatus = ORDERSTATUS.Created,
                                    String SearchField = null, String SearchFieldValue = null)
        {
            try
            {
                String[] ArrDtColumns1 = new string[] { "OrderID", "CustomerID", "OrderNumber", "OrderDate" };
                String[] ArrDtColumns2 = new string[] { "OrderItemCount", "EstimateOrderAmount", "OrderStatus", "CreationDate", "LastUpdatedDate", "DateDelivered", "DateInvoiceCreated",
                                                    "DateQuotationCreated" };

                String[] ArrColumns = new string[] { "OrderID", "CustomerID", "Order Number", "Order Date", "Customer Name", "Order Item Count", "Estimate Order Amount",
                                                    "Order Status", "Creation Date", "Last Updated Date", "Delivered Date", "Invoice Created Date",
                                                    "Quotation Created Date" };

                String Query = $"Select a.{String.Join(", a.", ArrDtColumns1)}, b.CustomerName, a.{String.Join(", a.", ArrDtColumns2)} from Orders a Inner Join CUSTOMERMASTER b on a.CustomerID = b.CustomerID", WhereClause = $" Where 1 = 1";
                if (FromDate > DateTime.MinValue && ToDate > DateTime.MinValue)
                {
                    WhereClause += $" and a.OrderDate between '{MySQLHelper.GetDateStringForDB(FromDate)}' and '{MySQLHelper.GetDateStringForDB(ToDate)}'";
                }
                if (OrderStatus != ORDERSTATUS.All)
                {
                    WhereClause += $" and a.OrderStatus = '{OrderStatus}'";
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
                        case "ORDER NUMBER":
                            WhereClause += $" and a.OrderNumber like '%{SearchFieldValue}%'";
                            break;
                        case "ORDERID":
                            WhereClause += $" and a.OrderID = {SearchFieldValue}";
                            break;
                        case "ORDER STATUS":
                            WhereClause += $" and a.OrderStatus like '%{SearchFieldValue}%'";
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
                Query += WhereClause + " Order by a.OrderID";
                DataTable dtOrders = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                dtAllOrders = dtOrders;

                ListOrders.Clear();
                foreach (DataRow dtRow in dtOrders.Rows)
                {
                    OrderDetails tmpOrderDetails = new OrderDetails
                    {
                        OrderID = Int32.Parse(dtRow["OrderID"].ToString()),
                        OrderNumber = dtRow["OrderNumber"].ToString(),
                        OrderDate = DateTime.Parse(dtRow["OrderDate"].ToString()),
                        OrderItemCount = Int32.Parse(dtRow["OrderItemCount"].ToString()),
                        CreationDate = DateTime.Parse(dtRow["CreationDate"].ToString()),
                        LastUpdatedDate = DateTime.Parse(dtRow["LastUpdatedDate"].ToString()),
                        CustomerID = Int32.Parse(dtRow["CustomerID"].ToString()),
                        CustomerName = dtRow["CustomerName"].ToString(),
                        EstimateOrderAmount = Double.Parse(dtRow["EstimateOrderAmount"].ToString()),
                        OrderStatus = (ORDERSTATUS)Enum.Parse(Type.GetType("SalesOrdersReport.Models.ORDERSTATUS"), dtRow["OrderStatus"].ToString()),
                        DateDelivered = ((dtRow["DateDelivered"] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(dtRow["DateDelivered"].ToString())),
                        DateInvoiceCreated = ((dtRow["DateInvoiceCreated"] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(dtRow["DateInvoiceCreated"].ToString())),
                        DateQuotationCreated = ((dtRow["DateQuotationCreated"] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(dtRow["DateQuotationCreated"].ToString())),
                    };

                    ListOrders.Add(tmpOrderDetails);
                }

                for (int i = 0; i < dtAllOrders.Columns.Count; i++)
                {
                    dtAllOrders.Columns[i].ColumnName = ArrColumns[i];
                }
                return dtAllOrders;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadOrderDetails()", ex);
                throw;
            }
        }

        public Int32 ConvertOrderToInvoice(int orderID)
        {
            try
            {
                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ConvertOrderToInvoice()", ex);
                return -1;
            }
        }

        public Int32 DeleteOrderDetails(Int32 OrderID)
        {
            try
            {
                ObjMySQLHelper.UpdateTableDetails("Orders", new List<string>() { "OrderStatus" }, new List<string>() { ORDERSTATUS.Cancelled.ToString() }, 
                                            new List<Types>() { Types.String }, $"OrderID = {OrderID}");

                dtAllOrders.Select($"OrderID = {OrderID}")[0]["Order Status"] = ORDERSTATUS.Cancelled;
                dtAllOrders.AcceptChanges();

                ListOrders.Find(e => e.OrderID == OrderID).OrderStatus = ORDERSTATUS.Cancelled;

                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.DeleteOrderDetails()", ex);
                return -1;
            }
        }

        public OrderDetails FillOrderItemDetails(Int32 OrderID)
        {
            try
            {
                OrderDetails ObjOrderDetails = GetOrderDetailsForOrderID(OrderID);
                FillOrderItemDetails(ObjOrderDetails);

                return ObjOrderDetails;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.FillOrderItemDetails(OrderID)", ex);
                return null;
            }
        }

        public void FillOrderItemDetails(OrderDetails CurrOrderDetails)
        {
            try
            {
                String Query = $"Select * from OrderItems Where OrderID = {CurrOrderDetails.OrderID}";
                DataTable dtOrderItems = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                CurrOrderDetails.ListOrderItems = new List<OrderItemDetails>();
                foreach (DataRow item in dtOrderItems.Rows)
                {
                    OrderItemDetails tmpOrderItem = new OrderItemDetails()
                    {
                        ProductID = Int32.Parse(item["ProductID"].ToString()),
                        OrderQty = Double.Parse(item["OrderQty"].ToString()),
                        Price = Double.Parse(item["Price"].ToString()),
                        OrderItemStatus = (ORDERITEMSTATUS)Enum.Parse(Type.GetType("SalesOrdersReport.Models.ORDERITEMSTATUS"), item["OrderItemStatus"].ToString())
                    };
                    tmpOrderItem.ProductName = ObjProductMasterModel.GetProductDetails(tmpOrderItem.ProductID).ItemName;

                    CurrOrderDetails.ListOrderItems.Add(tmpOrderItem);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.FillOrderItemDetails()", ex);
                throw;
            }
        }

        public OrderDetails GetOrderDetailsForCustomer(DateTime OrderDate, Int32 CustomerID)
        {
            try
            {
                if (ListOrders.Count == 0) LoadOrderDetails(OrderDate, OrderDate, ORDERSTATUS.Created, "CustomerID", CustomerID.ToString());
                if (ListOrders.Count == 0) return null;

                Int32 Index = ListOrders.FindIndex(e => e.CustomerID == CustomerID);
                if (Index < 0) return null;

                FillOrderItemDetails(ListOrders[Index]);    //Assuming there will be only one Order for a given Customer on a given OrderDate

                return ListOrders[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetOrderDetailsForCustomer()", ex);
                throw;
            }
        }

        public OrderDetails GetOrderDetailsForOrderID(Int32 OrderID)
        {
            try
            {
                if (ListOrders.Count == 0) LoadOrderDetails(DateTime.MinValue, DateTime.MinValue, ORDERSTATUS.Created, "OrderID", OrderID.ToString());
                if (ListOrders.Count == 0) return null;

                Int32 Index = ListOrders.FindIndex(e => e.OrderID == OrderID);
                if (Index < 0) return null;

                FillOrderItemDetails(ListOrders[Index]);

                return ListOrders[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetOrderDetailsForOrderID()", ex);
                throw;
            }
        }

        public OrderDetails CreateNewOrderForCustomer(Int32 CustomerID, DateTime OrderDate, String OrderNumber, List<OrderItemDetails> ListOrderItems)
        {
            try
            {
                OrderDetails NewOrderDetails = new OrderDetails()
                {
                    OrderDate = OrderDate,
                    OrderNumber = OrderNumber,
                    OrderStatus = ORDERSTATUS.Created,
                    EstimateOrderAmount = ListOrderItems.Sum(e => e.Price * e.OrderQty),
                    LastUpdatedDate = DateTime.Now,
                    CustomerID = CustomerID,
                    ListOrderItems = ListOrderItems.Select(e => e.Clone()).ToList(),
                    CreationDate = DateTime.Now,
                    OrderItemCount = ListOrderItems.Count(e => e.OrderQty > 0)
                };

                NewOrderDetails.OrderID = InsertOrderDetails(NewOrderDetails);
                ObjMySQLHelper.UpdateIDValue("Orders", OrderNumber);

                ListOrders.Insert(0, NewOrderDetails);

                return NewOrderDetails;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreateNewOrderForCustomer()", ex);
                throw;
            }
        }

        Int32 InsertOrderDetails(OrderDetails ObjOrderDetails)
        {
            try
            {
                String Query = "Insert into Orders(OrderNumber, OrderDate, CreationDate, LastUpdatedDate, CustomerID, OrderItemCount, EstimateOrderAmount, OrderStatus) Values (";
                Query += $"'{ObjOrderDetails.OrderNumber}', '{MySQLHelper.GetDateStringForDB(ObjOrderDetails.OrderDate)}', '{MySQLHelper.GetDateStringForDB(ObjOrderDetails.CreationDate)}',";
                Query += $"'{MySQLHelper.GetDateStringForDB(ObjOrderDetails.LastUpdatedDate)}', {ObjOrderDetails.CustomerID}, {ObjOrderDetails.OrderItemCount}, {ObjOrderDetails.EstimateOrderAmount}, '{ObjOrderDetails.OrderStatus}'";
                Query += ")";
                ObjMySQLHelper.ExecuteNonQuery(Query);

                Int32 OrderID = -1;
                Query = $"Select Max(OrderID) from Orders where CustomerID = {ObjOrderDetails.CustomerID} and OrderDate = '{MySQLHelper.GetDateStringForDB(ObjOrderDetails.OrderDate)}'";
                foreach (var item in ObjMySQLHelper.ExecuteQuery(Query)) OrderID = Int32.Parse(item[0].ToString());

                InsertOrderItems(ObjOrderDetails.ListOrderItems, OrderID);

                return OrderID;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.InsertOrderDetails()", ex);
                throw;
            }
        }

        private void InsertOrderItems(List<OrderItemDetails> ListOrderItems, Int32 OrderID)
        {
            try
            {
                String Query = "";
                foreach (var item in ListOrderItems)
                {
                    Query = "Insert into OrderItems(OrderID, ProductID, OrderQty, Price, OrderItemStatus) Values (";
                    Query += $"{OrderID}, {item.ProductID}, {item.OrderQty}, {item.Price}, '{item.OrderItemStatus}'";
                    Query += ")";
                    ObjMySQLHelper.ExecuteNonQuery(Query);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.InsertOrderItems()", ex);
            }
        }

        public void UpdateOrderDetails(OrderDetails ObjOrderDetails)
        {
            try
            {
                //Find Items Modified & Added
                OrderDetails ObjOrderDetailsOrig = GetOrderDetailsForOrderID(ObjOrderDetails.OrderID);
                List<OrderItemDetails> ListItemsModified = new List<OrderItemDetails>();
                List<OrderItemDetails> ListItemsAdded = new List<OrderItemDetails>();
                Int32 OrderItemCount = 0;
                Double EstimatedOrderAmount = 0;
                for (int i = 0; i < ObjOrderDetails.ListOrderItems.Count; i++)
                {
                    Int32 Index = ObjOrderDetailsOrig.ListOrderItems.FindIndex(e => e.ProductID == ObjOrderDetails.ListOrderItems[i].ProductID);
                    if (Index < 0) ListItemsAdded.Add(ObjOrderDetails.ListOrderItems[i]);
                    else ListItemsModified.Add(ObjOrderDetails.ListOrderItems[i]);
                    if (ObjOrderDetails.ListOrderItems[i].OrderQty > 0)
                    {
                        OrderItemCount++;
                        EstimatedOrderAmount += (ObjOrderDetails.ListOrderItems[i].OrderQty * ObjOrderDetails.ListOrderItems[i].Price);
                    }
                }

                //Update Modified Items
                for (int i = 0; i < ListItemsModified.Count; i++)
                {
                    ObjMySQLHelper.UpdateTableDetails("OrderItems", new List<String>() { "OrderQty", "Price", "OrderItemStatus" },
                                                new List<String>() { ListItemsModified[i].OrderQty.ToString(), ListItemsModified[i].Price.ToString(), ListItemsModified[i].OrderItemStatus.ToString() },
                                                new List<Types>() { Types.Number, Types.Number, Types.String },
                                                $"OrderID = {ObjOrderDetails.OrderID} and ProductID = {ListItemsModified[i].ProductID}");
                }

                //Insert New Items
                InsertOrderItems(ListItemsAdded, ObjOrderDetails.OrderID);

                //Update OrderItemCount
                ObjMySQLHelper.UpdateTableDetails("Orders", new List<String>() { "OrderItemCount", "EstimateOrderAmount" },
                                            new List<String>() { OrderItemCount.ToString(), EstimatedOrderAmount.ToString() },
                                            new List<Types>() { Types.Number, Types.Number }, $"OrderID = {ObjOrderDetails.OrderID}");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateOrderDetails()", ex);
                throw;
            }
        }

        /*public void PrintOrderInvoiceQuotation(ReportType EnumReportType, Boolean IsDummyBill, List<Object> ListObjects, DateTime OrdInvQuotDate, Int32 PrintCopies = 1, Boolean CreateSummary = false, Boolean PrintOldBalance = false)
        {
            try
            {
                String OutputFolder = Path.GetTempPath();
                String ExcelFilePath = ExportOrdInvQuotToExcel(EnumReportType, IsDummyBill, OrdInvQuotDate, ListObjects, OutputFolder);

                if (PrintCopies > 0)
                {
                    Excel.Application xlApp = new Excel.Application();
                    try
                    {
                        xlApp.Visible = false;
                        xlApp.DisplayAlerts = false;

                        Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(ExcelFilePath);

                        xlWorkbook.PrintOutEx(Type.Missing, Type.Missing, PrintCopies);

                        xlWorkbook.Close(false);
                        CommonFunctions.ReleaseCOMObject(xlWorkbook);
                    }
                    catch (Exception ex)
                    {
                        CommonFunctions.ShowErrorDialog($"{this}.PrintOrder()", ex);
                    }
                    finally
                    {
                        xlApp.Quit();
                        CommonFunctions.ReleaseCOMObject(xlApp);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.PrintOrderInvoiceQuotation()", ex);
            }
        }

        public String ExportOrdInvQuotToExcel(ReportType EnumReportType, Boolean IsDummyBill, DateTime OrdInvQuotDate, List<Object> ListObjects, String ExportFolderPath)
        {
            Excel.Application xlApp = null;
            try
            {
                xlApp = new Excel.Application();
                xlApp.Visible = false;
                xlApp.DisplayAlerts = false;

                String SaveFilePath = "";
                String OutputFolder = ExportFolderPath;
                String SelectedDateTimeString = OrdInvQuotDate.ToString("dd-MM-yyyy");
                switch (EnumReportType)
                {
                    case ReportType.ORDER:
                        SaveFilePath = OutputFolder + "\\Order_" + SelectedDateTimeString + ".xlsx";
                        break;
                    case ReportType.INVOICE:
                        SaveFilePath = OutputFolder + "\\Invoice_" + SelectedDateTimeString + ".xlsx";
                        break;
                    case ReportType.QUOTATION:
                        SaveFilePath = OutputFolder + "\\Quotation_" + SelectedDateTimeString + ".xlsx";
                        break;
                    default:
                        return null;
                }

                if (IsDummyBill)
                {
                    SaveFilePath = Path.GetDirectoryName(SaveFilePath) + "\\" + Path.GetFileNameWithoutExtension(SaveFilePath) + "_Dummy" + Path.GetExtension(SaveFilePath);
                }

                Excel.Workbook xlWorkbook = xlApp.Workbooks.Add();
                Excel.Worksheet xlWorkSheet = null;
                for (int i = 1; i <= 3 && xlWorkbook.Sheets.Count > 1; i++)
                {
                    xlWorkSheet = CommonFunctions.GetWorksheet(xlWorkbook, "Sheet" + i);
                    if (xlWorkSheet != null) xlWorkSheet.Delete();
                }

                for (int i = 0; i < ListObjects.Count; i++)
                {
                    if (i == 0) xlWorkSheet = xlWorkbook.Sheets[1];
                    else xlWorkSheet = xlWorkbook.Sheets.Add();

                    switch (EnumReportType)
                    {
                        case ReportType.ORDER:
                            ExportOrder(EnumReportType, IsDummyBill, xlWorkSheet, (OrderDetails)ListObjects[i]);
                            break;
                        case ReportType.INVOICE:
                            break;
                        case ReportType.QUOTATION:
                            break;
                        default:
                            return null;
                    }
                }

                Excel.Worksheet FirstWorksheet = xlWorkbook.Sheets[1];
                FirstWorksheet.Select();

                xlWorkbook.SaveAs(SaveFilePath);
                xlWorkbook.Close(true);

                xlApp.DisplayAlerts = true;
                CommonFunctions.ReleaseCOMObject(xlWorkbook);

                return SaveFilePath;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ExportOrdInvQuotToExcel()", ex);
                if (xlApp != null)
                {
                    xlApp.Visible = true;
                    xlApp.DisplayAlerts = true;
                }
                return null;
            }
            finally
            {
                if (xlApp != null)
                {
                    xlApp.Quit();
                    CommonFunctions.ReleaseCOMObject(xlApp);
                }
            }
        }*/

        public void ExportOrder(ReportType EnumReportType, Boolean IsDummyBill, Excel.Workbook ExcelWorkbook, OrderDetails ObjOrderDetails)
        {
            try
            {
                Boolean PrintOldBalance = false;
                ReportSettings CurrReportSettings = null;
                String BillNumberText = "";
                String OutputFolder = Path.GetTempPath();
                String SelectedDateTimeString = ObjOrderDetails.OrderDate.ToString("dd-MM-yyyy");
                Boolean CreateSummary = false;
                EnumReportType = ReportType.QUOTATION;
                switch (EnumReportType)
                {
                    case ReportType.ORDER:
                        CurrReportSettings = CommonFunctions.ObjOrderSettings;
                        PrintOldBalance = true;
                        BillNumberText = "Order#";
                        CreateSummary = (CommonFunctions.ObjGeneralSettings.SummaryLocation == 2);
                        break;
                    case ReportType.INVOICE:
                        CurrReportSettings = CommonFunctions.ObjInvoiceSettings;
                        BillNumberText = "Invoice#";
                        CreateSummary = (CommonFunctions.ObjGeneralSettings.SummaryLocation == 0);
                        break;
                    case ReportType.QUOTATION:
                        CurrReportSettings = CommonFunctions.ObjQuotationSettings;
                        PrintOldBalance = true;
                        BillNumberText = "Quotation#";
                        CreateSummary = (CommonFunctions.ObjGeneralSettings.SummaryLocation == 1);
                        break;
                    default:
                        return;
                }

                Int32 ValidItemCount = ObjOrderDetails.ListOrderItems.Count;
                Int32 ProgressBarCount = ValidItemCount;
                Int32 Counter = 0, SLNo = 0;
                Double Quantity, Price;
                String OrderQuantity = "";

                SLNo = 0;
                CustomerDetails ObjCurrentSeller = CommonFunctions.ObjCustomerMasterModel.GetCustomerDetails(ObjOrderDetails.CustomerName);
                DiscountGroupDetails1 ObjDiscountGroup = CommonFunctions.ObjCustomerMasterModel.GetCustomerDiscount(ObjCurrentSeller.CustomerName);
                Double DiscountPerc = 0, DiscountValue = 0;
                if (ObjDiscountGroup.DiscountType == DiscountTypes.PERCENT)
                    DiscountPerc = ObjDiscountGroup.Discount;
                else if (ObjDiscountGroup.DiscountType == DiscountTypes.ABSOLUTE)
                    DiscountValue = ObjDiscountGroup.Discount;

                Invoice ObjInvoice = CommonFunctions.GetInvoiceTemplate(EnumReportType);
                ObjInvoice.SerialNumber = ObjOrderDetails.OrderNumber;
                ObjInvoice.InvoiceNumberText = BillNumberText;
                ObjInvoice.ObjSellerDetails = ObjCurrentSeller.Clone();
                ObjInvoice.OldBalance = 0;//TODO:Double.Parse(lblBalanceAmountValue.Text);
                ObjInvoice.CurrReportSettings = CurrReportSettings;
                ObjInvoice.DateOfInvoice = ObjOrderDetails.OrderDate;
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
                for (int i = 0; i < ObjOrderDetails.ListOrderItems.Count; i++)
                {
                    Counter++;

                    OrderQuantity = ObjOrderDetails.ListOrderItems[i].OrderQty.ToString();
                    Quantity = ObjOrderDetails.ListOrderItems[i].OrderQty;
                    ItemName = ObjOrderDetails.ListOrderItems[i].ProductName;
                    if (Quantity == 0) continue;
                    Price = ObjOrderDetails.ListOrderItems[i].Price;
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
                        ObjProductDetailsForInvoice.DiscountGroup.Discount = (DiscountValue / ObjOrderDetails.ListOrderItems.Count);
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
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ExportOrder()", ex);
            }
        }

        public void ExportItemSummary(Excel.Workbook xlWorkbook, List<OrderDetails> ListOrderDetails)
        {
            try
            {
                String SheetName = "Item Summary";
                SheetName = CommonFunctions.GetWorksheetNameToAppend(SheetName, xlWorkbook);

                Excel.Worksheet xlWorksheet = xlWorkbook.Worksheets.Add(xlWorkbook.Worksheets[1]);
                xlWorksheet.Name = SheetName;

                String OrderIDs = String.Join(",", ListOrderDetails.Select(e => e.OrderID));

                String Query = "Select row_number() over () 'Sl.No.', a.* from (Select b.ProductName 'Item Name', Sum(a.OrderQty) TotalQuantity ";
                List<String> ListLines = CommonFunctions.ObjCustomerMasterModel.GetAllLineNames();
                for (int i = 0; i < ListLines.Count; i++)
                {
                    Int32 LineID = CommonFunctions.ObjCustomerMasterModel.GetLineID(ListLines[i]);
                    Query += $", Sum(Case When d.LineID = {LineID} then a.OrderQty else 0 end) '{ListLines[i]}'";
                }
                Query += " from OrderItems a Inner Join ProductMaster b on a.ProductID = b.ProductID";
                Query += " Inner Join Orders c on a.OrderID = c.OrderID";
                Query += " Inner Join CUSTOMERMASTER d on c.CustomerID = d.CustomerID";
                Query += $" Where a.OrderID in ({OrderIDs}) and a.OrderItemStatus = 'Ordered' Group by b.ProductName Order by b.ProductName) a;";
                DataTable dtItemSummary = ObjMySQLHelper.GetQueryResultInDataTable(Query);

                Int32 RetVal = CommonFunctions.ExportDataTableToExcelWorksheet(dtItemSummary, xlWorksheet, 1, 1);
                if (RetVal < 0) return;

                Excel.Range xlRange = null;
                xlRange = xlWorksheet.Range[xlWorksheet.Cells[1, 1], xlWorksheet.Cells[1, 3 + ListLines.Count]];
                xlRange.Font.Bold = true;

                xlWorksheet.UsedRange.Columns.AutoFit();
                SellerInvoiceForm.AddPageHeaderAndFooter(ref xlWorksheet, CommonFunctions.ObjOrderSettings.HeaderSubTitle, CommonFunctions.ObjOrderSettings);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ExportItemSummary()", ex);

                throw;
            }
        }
    }
}
