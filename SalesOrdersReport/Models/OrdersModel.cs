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
        Ordered, Delivered, Cancelled, OutOfStock
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
        public DateTime DateDelivered, DateInvoiceCreated;
        public List<OrderItemDetails> ListOrderItems;
        public Int32 OrderItemCount = 0;
        public Int32 DeliveryLineID = -1;
        public string DeliveryLineName = "";


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
        public string Comments = "";
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

        public void AddOrderDetailsToCache(OrderDetails ObjOrderDetails)
        {
            try
            {
                if (dtAllOrders != null)
                {
                    ListOrders.Add(ObjOrderDetails);
                    Object[] ArrItems = GetDataRowForOrder(ObjOrderDetails);
                    dtAllOrders.Rows.Add(ArrItems);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.AddOrderDetailsToCache()", ex);
            }
        }

        public Object[] GetDataRowForOrder(OrderDetails ObjOrderDetails)
        {
            try
            {
                return new Object[] {
                    ObjOrderDetails.OrderID,
                    ObjOrderDetails.CustomerID,
                    ObjOrderDetails.DeliveryLineID,
                    ObjOrderDetails.OrderNumber,
                    new MySql.Data.Types.MySqlDateTime(ObjOrderDetails.OrderDate),
                    ObjOrderDetails.CustomerName,
                    ObjOrderDetails.OrderItemCount,
                    ObjOrderDetails.EstimateOrderAmount,
                    ObjOrderDetails.OrderStatus,
                    new MySql.Data.Types.MySqlDateTime(ObjOrderDetails.CreationDate),
                    new MySql.Data.Types.MySqlDateTime(ObjOrderDetails.LastUpdatedDate),
                    new MySql.Data.Types.MySqlDateTime(ObjOrderDetails.DateDelivered),
                    new MySql.Data.Types.MySqlDateTime(ObjOrderDetails.DateInvoiceCreated),
                    ObjOrderDetails.DeliveryLineName
                };
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetDataRowForOrder()", ex);
                return null;
            }
        }

        public String GenerateNewOrderNumber()
        {
            try
            {
                String MaxOrderNumber = ObjMySQLHelper.GetIDValue(CommonFunctions.ObjApplicationSettings.POSNumber + "-Orders");
                if (String.IsNullOrEmpty(MaxOrderNumber)) MaxOrderNumber = "0";
                return CommonFunctions.GenerateNextID(CommonFunctions.ObjApplicationSettings.POSNumber + "-" + OrderNumberPrefix, MaxOrderNumber);
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
                String[] ArrDtColumns1 = new string[] { "OrderID", "CustomerID", "DeliveryLineID", "OrderNumber", "OrderDate" };
                String[] ArrDtColumns2 = new string[] { "OrderItemCount", "EstimateOrderAmount", "OrderStatus", "CreationDate", "LastUpdatedDate", "DateDelivered", "DateInvoiceCreated" };

                String[] ArrColumns = new string[] { "OrderID", "CustomerID","DeliveryLineID", "Order Number", "Order Date", "Customer Name", "Order Item Count", "Estimate Order Amount",
                                                    "Order Status", "Creation Date", "Last Updated Date", "Delivered Date", "Invoice Created Date","DeliveryLine"};

                String Query = $"Select a.{String.Join(", a.", ArrDtColumns1)}, b.CustomerName, a.{String.Join(", a.", ArrDtColumns2)} , e.LineName as DeliveryLine from Orders a Inner Join CUSTOMERMASTER b on a.CustomerID = b.CustomerID", WhereClause = $" Where 1 = 1";
                if (FromDate > DateTime.MinValue && ToDate > DateTime.MinValue)
                {
                    WhereClause += $" and Date(a.OrderDate) between '{MySQLHelper.GetDateStringForDB(FromDate)}' and '{MySQLHelper.GetDateStringForDB(ToDate)}'";
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
                Query += $" left Join LINEMASTER e on a.DeliveryLineID = e.LineID";
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
                        DeliveryLineID = (dtRow["DeliveryLineID"].ToString() == string.Empty || dtRow["DeliveryLineID"].ToString() == null) ? -1 : Int32.Parse(dtRow["DeliveryLineID"].ToString()),
                        DeliveryLineName = dtRow["DeliveryLine"].ToString()
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
                OrderDetails ObjOrderDetails = GetOrderDetailsForOrderID(orderID).Clone();
                if (ObjOrderDetails == null) return -2;

                FillOrderItemDetails(ObjOrderDetails);

                InvoicesModel ObjInvoicesModel = new InvoicesModel();
                ObjInvoicesModel.Initialize();

                CustomerDetails ObjCustomerDetails = CommonFunctions.ObjCustomerMasterModel.GetCustomerDetails(ObjOrderDetails.CustomerID);
                List<InvoiceItemDetails> ListInvoiceItems = new List<InvoiceItemDetails>();
                for (int i = 0; i < ObjOrderDetails.ListOrderItems.Count; i++)
                {
                    OrderItemDetails tmpOrderItem = ObjOrderDetails.ListOrderItems[i];
                    if (tmpOrderItem.OrderItemStatus != ORDERITEMSTATUS.Ordered) continue;
                    tmpOrderItem.OrderItemStatus = ORDERITEMSTATUS.Delivered;
                    ObjOrderDetails.EstimateOrderAmount = 0;

                    InvoiceItemDetails tmpInvoiceItem = new InvoiceItemDetails()
                    {
                        ProductName = tmpOrderItem.ProductName,
                        ProductID = tmpOrderItem.ProductID,
                        //OrderQty = tmpOrderItem.OrderQty,
                        OrderQty = tmpOrderItem.OrderQty.ToString(),
                        SaleQty = 0,
                        //SaleQty = tmpOrderItem.OrderQty,
                        //Price = tmpOrderItem.Price,
                        Price = ObjProductMasterModel.GetPriceForProduct(tmpOrderItem.ProductName, ObjCustomerDetails.PriceGroupIndex),
                        InvoiceItemStatus = INVOICEITEMSTATUS.Invoiced
                        //TaxableValue = tmpOrderItem.Price * tmpOrderItem.OrderQty,
                        //NetTotal = tmpOrderItem.Price * tmpOrderItem.OrderQty
                    };

                    ListInvoiceItems.Add(tmpInvoiceItem);
                    ObjOrderDetails.EstimateOrderAmount += tmpInvoiceItem.TaxableValue;
                }

                ObjOrderDetails.OrderStatus = ORDERSTATUS.Completed;
                UpdateOrderDetails(ObjOrderDetails);

                Double DiscountPerc = 0, DiscountValue = 0;
                DiscountGroupDetails CustomerDiscountGroup = CommonFunctions.ObjCustomerMasterModel.GetCustomerDiscount(ObjOrderDetails.CustomerName);
                if (CustomerDiscountGroup.DiscountType == DiscountTypes.PERCENT)
                    DiscountPerc = CustomerDiscountGroup.Discount;
                else if (CustomerDiscountGroup.DiscountType == DiscountTypes.ABSOLUTE)
                    DiscountValue = CustomerDiscountGroup.Discount;

                Double Discount = DiscountValue;
                if (DiscountPerc > 0) Discount = ObjOrderDetails.EstimateOrderAmount * DiscountPerc;

                InvoiceDetails ObjInvoiceDetails = ObjInvoicesModel.CreateNewInvoiceForCustomer(ObjOrderDetails.CustomerID, ObjOrderDetails.OrderID, DateTime.Now, ObjInvoicesModel.GenerateNewInvoiceNumber(), ObjOrderDetails.DeliveryLineName, ListInvoiceItems, Discount);

                if (ObjInvoiceDetails != null) return 0;
                else return -1;
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

                ObjMySQLHelper.UpdateTableDetails("OrderItems", new List<string>() { "OrderItemStatus" }, new List<string>() { ORDERITEMSTATUS.Cancelled.ToString() },
                            new List<Types>() { Types.String }, $"OrderID = {OrderID}");

                dtAllOrders.Select($"OrderID = {OrderID}")[0]["Order Status"] = ORDERSTATUS.Cancelled;
                dtAllOrders.AcceptChanges();

                OrderDetails orderDetails = ListOrders.Find(e => e.OrderID == OrderID);
                orderDetails.OrderStatus = ORDERSTATUS.Cancelled;

                FillOrderItemDetails(orderDetails);

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
                        OrderItemStatus = (ORDERITEMSTATUS)Enum.Parse(Type.GetType("SalesOrdersReport.Models.ORDERITEMSTATUS"), item["OrderItemStatus"].ToString()),
                        Comments = item["Comments"].ToString()
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

        public List<OrderDetails> GetOrderDetailsForCustomer(DateTime OrderDate, Int32 CustomerID)
        {
            try
            {
                if (ListOrders.Count == 0) LoadOrderDetails(OrderDate, OrderDate, ORDERSTATUS.Created, "CustomerID", CustomerID.ToString());
                if (ListOrders.Count == 0) return null;

                Int32 Index = ListOrders.FindIndex(e => e.CustomerID == CustomerID);
                if (Index < 0) return null;

                for (int i = 0; i < ListOrders.Count; i++)
                {
                    FillOrderItemDetails(ListOrders[i]);
                }

                return ListOrders;
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

        public OrderDetails CreateNewOrderForCustomer(Int32 CustomerID, DateTime OrderDate, String OrderNumber, string DeliveryLineName, List<OrderItemDetails> ListOrderItems)
        {
            try
            {
                ObjMySQLHelper.SetAutoCloseConnection(false);
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
                    OrderItemCount = ListOrderItems.Count(e => e.OrderQty > 0),
                    CustomerName = CommonFunctions.ObjCustomerMasterModel.GetCustomerName(CustomerID),
                    DeliveryLineName = DeliveryLineName,
                    DeliveryLineID = DeliveryLineName == "" ? -1 : CommonFunctions.ObjCustomerMasterModel.GetLineID(DeliveryLineName)
                };

                NewOrderDetails.OrderID = InsertOrderDetails(NewOrderDetails);
                ObjMySQLHelper.UpdateIDValue(CommonFunctions.ObjApplicationSettings.POSNumber + "-Orders", OrderNumber);

                ListOrders.Insert(0, NewOrderDetails);

                return NewOrderDetails;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreateNewOrderForCustomer()", ex);
                throw;
            }
            finally
            {
                ObjMySQLHelper.SetAutoCloseConnection(true);
                ObjMySQLHelper.CloseConnection();
            }
        }

        Int32 InsertOrderDetails(OrderDetails ObjOrderDetails)
        {
            try
            {
                String Query = "Insert into Orders(OrderNumber, OrderDate, CreationDate, LastUpdatedDate, CustomerID, OrderItemCount, EstimateOrderAmount, OrderStatus, DeliveryLineID) Values (";
                Query += $"'{ObjOrderDetails.OrderNumber}', '{MySQLHelper.GetDateTimeStringForDB(ObjOrderDetails.OrderDate)}', '{MySQLHelper.GetDateTimeStringForDB(ObjOrderDetails.CreationDate)}',";
                Query += $"'{MySQLHelper.GetDateTimeStringForDB(ObjOrderDetails.LastUpdatedDate)}', {ObjOrderDetails.CustomerID}, {ObjOrderDetails.OrderItemCount}, {ObjOrderDetails.EstimateOrderAmount}, '{ObjOrderDetails.OrderStatus}',{ObjOrderDetails.DeliveryLineID }";
                Query += ")";
                ObjMySQLHelper.ExecuteNonQuery(Query);

                Int32 OrderID = -1;
                Query = $"Select Max(OrderID) from Orders where CustomerID = {ObjOrderDetails.CustomerID} and OrderDate = '{MySQLHelper.GetDateTimeStringForDB(ObjOrderDetails.OrderDate)}'";
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
                    Query = "Insert into OrderItems(OrderID, ProductID, OrderQty, Price, OrderItemStatus,Comments) Values (";
                    Query += $"{OrderID}, {item.ProductID}, {item.OrderQty}, {item.Price}, '{item.OrderItemStatus}','{item.Comments}'";
                    Query += ")";
                    ObjMySQLHelper.ExecuteNonQuery(Query);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.InsertOrderItems()", ex);
            }
        }

        public OrderDetails UpdateOrderDetails(OrderDetails ObjOrderDetails)
        {
            try
            {
                ObjMySQLHelper.SetAutoCloseConnection(false);

                //Find Items Modified & Added
                OrderDetails ObjOrderDetailsOrig = GetOrderDetailsForOrderID(ObjOrderDetails.OrderID);
                List<OrderItemDetails> ListItemsModified = new List<OrderItemDetails>();
                List<OrderItemDetails> ListItemsAdded = new List<OrderItemDetails>();
                Int32 OrderItemCount = 0;
                Double EstimatedOrderAmount = 0;
                ObjOrderDetails.ListOrderItems.RemoveAll(e => e.OrderItemStatus == ORDERITEMSTATUS.Cancelled);
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

                //Find Deleted Items
                List<OrderItemDetails> ListItemsDeleted = new List<OrderItemDetails>();
                for (int i = 0; i < ObjOrderDetailsOrig.ListOrderItems.Count; i++)
                {
                    Int32 Index = ObjOrderDetails.ListOrderItems.FindIndex(e => e.ProductID == ObjOrderDetailsOrig.ListOrderItems[i].ProductID);
                    if (Index < 0) ListItemsDeleted.Add(ObjOrderDetailsOrig.ListOrderItems[i]);
                }

                //Update Modified Items
                for (int i = 0; i < ListItemsModified.Count; i++)
                {
                    ObjMySQLHelper.UpdateTableDetails("OrderItems", new List<String>() { "OrderQty", "Price", "OrderItemStatus","Comments" },
                                                new List<String>() { ListItemsModified[i].OrderQty.ToString(), ListItemsModified[i].Price.ToString(), ListItemsModified[i].OrderItemStatus.ToString(), ListItemsModified[i].Comments.ToString() },
                                                new List<Types>() { Types.Number, Types.Number, Types.String, Types.String },
                                                $"OrderID = {ObjOrderDetails.OrderID} and ProductID = {ListItemsModified[i].ProductID}");
                }

                //Update status for Deleted Items
                for (int i = 0; i < ListItemsDeleted.Count; i++)
                {
                    ObjMySQLHelper.UpdateTableDetails("OrderItems", new List<String>() { "OrderItemStatus" },
                                                new List<String>() { ORDERITEMSTATUS.Cancelled.ToString() },
                                                new List<Types>() { Types.String },
                                                $"OrderID = {ObjOrderDetails.OrderID} and ProductID = {ListItemsDeleted[i].ProductID}");
                }

                //Insert New Items
                InsertOrderItems(ListItemsAdded, ObjOrderDetails.OrderID);

                //Update OrderItemCount
                ObjMySQLHelper.UpdateTableDetails("Orders", new List<String>() { "OrderItemCount", "EstimateOrderAmount", "OrderStatus", "DeliveryLineID" },
                                            new List<String>() { OrderItemCount.ToString(), EstimatedOrderAmount.ToString(), ObjOrderDetails.OrderStatus.ToString(), ObjOrderDetails.DeliveryLineID.ToString() },
                                            new List<Types>() { Types.Number, Types.Number, Types.String, Types.Number }, $"OrderID = {ObjOrderDetails.OrderID}");

                FillOrderItemDetails(ObjOrderDetails);
                return ObjOrderDetails;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateOrderDetails()", ex);
                throw;
            }
            finally
            {
                ObjMySQLHelper.SetAutoCloseConnection(true);
                ObjMySQLHelper.CloseConnection();
            }
        }

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
                CurrReportSettings = CommonFunctions.ObjOrderSettings;
                PrintOldBalance = true;
                BillNumberText = "Order#";
                CreateSummary = (CommonFunctions.ObjGeneralSettings.SummaryLocation == 2);

                Int32 ValidItemCount = ObjOrderDetails.ListOrderItems.Count;
                Int32 ProgressBarCount = ValidItemCount;
                Int32 Counter = 0, SLNo = 0;
                Double Quantity, Price;
                String OrderQuantity = "";
                string Comments = "";

                SLNo = 0;
                CustomerDetails ObjCurrentSeller = CommonFunctions.ObjCustomerMasterModel.GetCustomerDetails(ObjOrderDetails.CustomerName);
                DiscountGroupDetails ObjDiscountGroup = CommonFunctions.ObjCustomerMasterModel.GetCustomerDiscount(ObjCurrentSeller.CustomerName);
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
                    Comments = ObjOrderDetails.ListOrderItems[i].Comments;

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
                    ObjProductDetailsForInvoice.Comments = Comments;
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
                //Override Discount and rollback after creating Quotation
                DiscountGroupDetails OrigDiscountGroup = ObjDiscountGroup.Clone();
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
                #endregion
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ExportOrder()", ex);
                throw;
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
                CommonFunctions.AddPageHeaderAndFooter(ref xlWorksheet, "Item Summary", CommonFunctions.ObjOrderSettings);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ExportItemSummary()", ex);
                throw;
            }
        }
    }
}
