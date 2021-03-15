using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Models;

namespace SalesOrdersReport.Views
{
    public partial class ProductInventoryMainForm : Form
    {
        OrdersModel ObjOrdersModel;
        DataTable dtAllOrders;
        ORDERSTATUS CurrOrderStatus;
        DateTime FilterFromDate, FilterToDate;

        public ProductInventoryMainForm()
        {
            try
            {
                InitializeComponent();

                CommonFunctions.SetDataGridViewProperties(dtGridViewOrders);

                ObjOrdersModel = new OrdersModel();
                ObjOrdersModel.Initialize();

                FilterFromDate = DateTime.MinValue;
                FilterToDate = DateTime.MinValue;
                CurrOrderStatus = ORDERSTATUS.Created;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("OrdersMainForm.ctor()", ex);
            }
        }

        private void LoadGridView()
        {
            try
            {
                dtAllOrders = GetOrdersDataTable(FilterFromDate, FilterToDate, CurrOrderStatus, null);
                LoadOrdersGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadGridView()", ex);
            }
        }

        private void OrdersMainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                this.MaximizeBox = true;
                this.WindowState = FormWindowState.Maximized;
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.OrdersMainForm_Shown()", ex);
            }
        }

        DataTable GetOrdersDataTable(DateTime FromDate, DateTime ToDate, ORDERSTATUS OrderStatus, String LineName)
        {
            try
            {
                DataTable dtOrders = null;

                if (String.IsNullOrEmpty(LineName)) 
                    dtOrders = ObjOrdersModel.LoadOrderDetails(FromDate, ToDate, OrderStatus);
                else
                    dtOrders = ObjOrdersModel.LoadOrderDetails(FromDate, ToDate, OrderStatus, "Line", LineName);

                return dtOrders;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetOrdersDataTable()", ex);
                return null;
            }
        }

        void LoadOrdersGridView()
        {
            try
            {
                dtGridViewOrders.DataSource = dtAllOrders.DefaultView;

                for (int i = 0; i < dtGridViewOrders.Columns.Count; i++)
                {
                    if (dtGridViewOrders.Columns[i].Name.Equals("OrderID") || dtGridViewOrders.Columns[i].Name.Equals("CustomerID"))
                        dtGridViewOrders.Columns[i].Visible = false;
                }
                dtGridViewOrders.ClearSelection();

                lblOrdersCount.Text = $"[Displaying {dtGridViewOrders.Rows.Count} of {dtAllOrders.Rows.Count} Orders]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        void UpdateOrdersOnClose(Int32 Mode, Object ObjAddUpdatedDetails = null)
        {
            try
            {
                switch (Mode)
                {
                    case 1:     //Add Order
                        break;
                    case 2:
                        break;
                    case 3:     //Reload Orders
                        break;
                    default:
                        break;
                }
                
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateOnClose()", ex);
            }
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new CreateOrderForm(-1, UpdateOrdersOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnCreateOrder_Click()", ex);
            }
        }

        private void btnReloadOrders_Click(object sender, EventArgs e)
        {
            try
            {
                LoadGridViewBG();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnReloadOrders_Click()", ex);
            }
        }

        private void LoadGridViewOrderItems(OrderDetails ObjOrderDetails)
        {
            try
            {
                if (ObjOrderDetails.ListOrderItems == null || ObjOrderDetails.ListOrderItems.Count == 0) return;

                DataTable dtOrderProducts = new DataTable();
                String[] ArrColumns = new String[] { "ProductID", "Product Name", "Ordered Qty", "Price", "Order Item Status" };
                Type[] ArrColumnTypes = new Type[] { CommonFunctions.TypeInt32, CommonFunctions.TypeString, CommonFunctions.TypeDouble, CommonFunctions.TypeDouble, CommonFunctions.TypeString };

                for (int i = 0; i < ArrColumns.Length; i++)
                {
                    dtOrderProducts.Columns.Add(ArrColumns[i], ArrColumnTypes[i]);
                }

                for (int i = 0; i < ObjOrderDetails.ListOrderItems.Count; i++)
                {
                    OrderItemDetails tmpOrderItem = ObjOrderDetails.ListOrderItems[i];
                    Object[] ArrObjects = new Object[]
                    {
                        tmpOrderItem.ProductID,
                        tmpOrderItem.ProductName,
                        tmpOrderItem.OrderQty,
                        tmpOrderItem.Price,
                        tmpOrderItem.OrderItemStatus
                    };
                    dtOrderProducts.Rows.Add(ArrObjects);
                }

                dtOrderProducts.DefaultView.Sort = "Product Name";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadGridViewOrderItems()", ex);
            }
        }

        Int32 OrderIDSelected = -1;
        private void dtGridViewOrders_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Int32 OrderID = Int32.Parse(dtGridViewOrders.SelectedRows[0].Cells["OrderID"].Value.ToString());
                if (OrderIDSelected == OrderID)
                {
                    OrderIDSelected = -1;
                    dtGridViewOrders.ClearSelection();
                    return;
                }

                OrderDetails tmpOrderDetails = ObjOrdersModel.FillOrderItemDetails(OrderID);
                LoadGridViewOrderItems(tmpOrderDetails);
                OrderIDSelected = OrderID;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewOrders_CellMouseClick()", ex);
            }
        }

        Int32 ExportOption = -1;
        String ExportFolderPath = "";
        Int32 BackgroundTask = 0;
        ReportProgressDel ReportProgress = null;

        private void ReportProgressFunc(Int32 ProgressState)
        {
            if (ReportProgress == null) return;
            ReportProgress(ProgressState);
        }

        private void backgroundWorkerOrders_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                switch (BackgroundTask)
                {
                    case 1: //Print Order
                        {
                            ReportType EnumReportType = ReportType.ORDER;
                            Boolean PrintOldBalance = false;
                            Boolean CreateSummary = false;
                            Int32 PrintCopies = 1;

                            Int32 OrderID = Int32.Parse(dtGridViewOrders.SelectedRows[0].Cells["OrderID"].Value.ToString());
                            OrderDetails ObjOrderDetails = ObjOrdersModel.GetOrderDetailsForOrderID(OrderID);
                            CommonFunctions.PrintOrderInvoiceQuotation(EnumReportType, true, ObjOrdersModel, new List<Object>() { ObjOrderDetails }, ObjOrderDetails.OrderDate, PrintCopies, CreateSummary, PrintOldBalance, ReportProgressFunc);
                        }
                        break;
                    case 2: //Export Orders
                        {
                            ReportType EnumReportType = ReportType.ORDER;
                            Boolean PrintOldBalance = true;
                            Boolean CreateSummary = (CommonFunctions.ObjGeneralSettings.SummaryLocation == 2) || ((ExportOption & 4) > 0);

                            List<Object> ListOrdersToExport = new List<Object>();
                            if ((ExportOption & 1) > 0)      //Export all displayed Orders
                            {
                                for (int i = 0; i < dtGridViewOrders.Rows.Count; i++)
                                {
                                    Int32 OrderID = Int32.Parse(dtGridViewOrders.Rows[i].Cells["OrderID"].Value.ToString());
                                    OrderDetails tmpOrderDetails = ObjOrdersModel.GetOrderDetailsForOrderID(OrderID);
                                    ListOrdersToExport.Add(tmpOrderDetails);
                                }
                            }
                            else if ((ExportOption & 2) > 0) //Export only selected Order
                            {
                                Int32 OrderID = Int32.Parse(dtGridViewOrders.SelectedRows[0].Cells["OrderID"].Value.ToString());
                                OrderDetails tmpOrderDetails = ObjOrdersModel.GetOrderDetailsForOrderID(OrderID);
                                ListOrdersToExport.Add(tmpOrderDetails);
                            }
                            String ExportedFilePath = CommonFunctions.ExportOrdInvQuotToExcel(EnumReportType, true, 
                                        ((OrderDetails)ListOrdersToExport[0]).OrderDate, ObjOrdersModel, ListOrdersToExport, ExportFolderPath, 
                                        CreateSummary, PrintOldBalance, ReportProgressFunc);

                            MessageBox.Show(this, $"Exported Orders file is created successfully at:{ExportedFilePath}", "Export Orders", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    case 3:     //Load Grid View
                        LoadGridView();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.backgroundWorkerOrders_DoWork()", ex);
            }
        }

        private void backgroundWorkerOrders_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                CommonFunctions.UpdateProgressBar(e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.backgroundWorkerOrders_ProgressChanged()", ex);
            }
        }

        private void backgroundWorkerOrders_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                switch (BackgroundTask)
                {
                    case 1: //Print Order
                        break;
                    case 2:
                        ExportOption = -1; ExportFolderPath = "";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.backgroundWorkerOrders_RunWorkerCompleted()", ex);
            }
        }

        private void LoadGridViewBG()
        {
            try
            {
                BackgroundTask = 3;
#if DEBUG
                backgroundWorkerOrders_DoWork(null, null);
                backgroundWorkerOrders_RunWorkerCompleted(null, null);
#else
                ReportProgress = backgroundWorkerOrders.ReportProgress;
                backgroundWorkerOrders.RunWorkerAsync();
                backgroundWorkerOrders.WorkerReportsProgress = true;
#endif
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadGridViewBG()", ex);
            }
        }    
    }
}
