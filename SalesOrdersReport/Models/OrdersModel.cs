using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrdersReport.Models
{
    enum ORDERSTATUS
    {
        Created, Completed, Cancelled, Void
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
        List<OrderDetails> ListOrders;
        MySQLHelper ObjMySQLHelper;

        public void Initialize()
        {
            try
            {
                ListOrders = new List<OrderDetails>();
                ObjMySQLHelper = MySQLHelper.GetMySqlHelperObj();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.Initialize()", ex);
                throw;
            }
        }

        public void LoadOrderDetails(DateTime FromDate, DateTime ToDate, ORDERSTATUS OrderStatus = ORDERSTATUS.Created,
                                    String SearchField = null, String SearchFieldValue = null)
        {
            try
            {
                String Query = "Select a.*, b.CustomerName from Orders a Inner Join CustomerMaster b on a.CustomerID = b.CustomerID", WhereClause = $" Where a.OrderStatus = '{OrderStatus}'";
                if (FromDate > DateTime.MinValue && ToDate > DateTime.MinValue)
                {
                    WhereClause += $" and a.OrderDate between '{FromDate.ToString("yyyy-MM-dd")}' and '{FromDate.ToString("yyyy-MM-dd")}'";
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
                        default:
                            break;
                    }
                }
                Query += WhereClause + " Order by a.OrderID";
                DataTable dtOrders = ObjMySQLHelper.GetQueryResultInDataTable(Query);

                ListOrders.Clear();
                foreach (DataRow dtRow in dtOrders.Rows)
                {
                    OrderDetails tmpOrderDetails = new OrderDetails
                    {
                        OrderID = Int32.Parse(dtRow["OrderID"].ToString()),
                        OrderNumber = dtRow["OrderNumber"].ToString(),
                        OrderDate = DateTime.Parse(dtRow["OrderDate"].ToString()),
                        CreationDate = DateTime.Parse(dtRow["CreationDate"].ToString()),
                        LastUpdatedDate = DateTime.Parse(dtRow["LastUpdatedDate"].ToString()),
                        CustomerID = Int32.Parse(dtRow["CustomerID"].ToString()),
                        CustomerName = dtRow["CustomerName"].ToString(),
                        EstimateOrderAmount = Double.Parse(dtRow["EstimateOrderAmount"].ToString()),
                        OrderStatus = (ORDERSTATUS)Enum.Parse(Type.GetType("ORDERSTATUS"), dtRow["OrderStatus"].ToString()),
                        DateDelivered = DateTime.Parse(dtRow["DateDelivered"].ToString()),
                        DateInvoiceCreated = DateTime.Parse(dtRow["DateInvoiceCreated"].ToString()),
                        DateQuotationCreated = DateTime.Parse(dtRow["DateQuotationCreated"].ToString()),
                    };
                    FillOrderItemDetails(tmpOrderDetails);
                    ListOrders.Add(tmpOrderDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadOrderDetails()", ex);
                throw;
            }
        }

        void FillOrderItemDetails(OrderDetails CurrOrderDetails)
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
                        OrderItemStatus = (ORDERITEMSTATUS)Enum.Parse(Type.GetType("ORDERITEMSTATUS"), item["OrderItemStatus"].ToString())
                    };
                    tmpOrderItem.ProductName = CommonFunctions.ObjProductMaster.GetProductDetails(tmpOrderItem.ProductID).ItemName;

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

        String GenerateNewOrderNumber()
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GenerateNewOrderNumber()", ex);
                throw;
            }
        }

        public OrderDetails CreateNewOrderForCustomer(Int32 CustomerID, DateTime OrderDate, List<OrderItemDetails> ListOrderItems)
        {
            try
            {
                OrderDetails NewOrderDetails = new OrderDetails()
                {
                    OrderDate = OrderDate,
                    OrderNumber = GenerateNewOrderNumber(),
                    OrderStatus = ORDERSTATUS.Created,
                    EstimateOrderAmount = ListOrderItems.Sum(e => e.Price * e.OrderQty),
                    LastUpdatedDate = DateTime.Now,
                    CustomerID = CustomerID,
                    ListOrderItems = ListOrderItems.Select(e => e.Clone()).ToList(),
                    CreationDate = DateTime.Now
                };

                ListOrders.Insert(0, NewOrderDetails);
                NewOrderDetails.OrderID = InsertOrderDetailsToDB(NewOrderDetails);

                return NewOrderDetails;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreateNewOrderForCustomer()", ex);
                throw;
            }
        }

        Int32 InsertOrderDetailsToDB(OrderDetails ObjOrderDetails)
        {
            try
            {
                String Query = "Insert into Orders(OrderNumber, OrderDate, CreationDate, LastUpdatedDate, CustomerID, EstimateOrderAmount, OrderStatus) Values (";
                Query += $"'{ObjOrderDetails.OrderNumber}', '{MySQLHelper.GetDateStringForDB(ObjOrderDetails.OrderDate)}', '{MySQLHelper.GetDateStringForDB(ObjOrderDetails.CreationDate)}'";
                Query += $"'{MySQLHelper.GetDateStringForDB(ObjOrderDetails.LastUpdatedDate)}', {ObjOrderDetails.CustomerID}, {ObjOrderDetails.EstimateOrderAmount}, '{ObjOrderDetails.OrderStatus}'";
                Query += ")";
                ObjMySQLHelper.ExecuteNonQuery(Query);

                Int32 OrderID = -1;
                Query = $"Select Max(OrderID) from Orders where CustomerID = {ObjOrderDetails.CustomerID} and OrderDate = '{MySQLHelper.GetDateStringForDB(ObjOrderDetails.OrderDate)}'";
                foreach (var item in ObjMySQLHelper.ExecuteQuery(Query)) OrderID = Int32.Parse(item[0].ToString());

                foreach (var item in ObjOrderDetails.ListOrderItems)
                {
                    Query = "Insert into OrderItems(OrderID, ProductID, OrderQty, Price, OrderItemStatus) Values (";
                    Query += $"{OrderID}, {item.ProductID}, {item.OrderQty}, {item.Price}, '{item.OrderItemStatus}'";
                    Query += ")";
                    ObjMySQLHelper.ExecuteNonQuery(Query);
                }

                return OrderID;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.InsertOrderDetailsToDB()", ex);
                throw;
            }
        }

        public void UpdateOrderDetails(OrderDetails ObjOrderDetails)
        {
            try
            {
                String Query = $"Update OrderItems Set ";

                ObjMySQLHelper.ExecuteNonQuery(Query);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateOrderDetails()", ex);
                throw;
            }
        }
    }
}
