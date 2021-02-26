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
    public partial class OrdersMainForm : Form
    {
        OrdersModel ObjOrdersModel;
        DataTable dtAllOrders;
        String AllOrderstatus = "<All>";
        ORDERSTATUS CurrOrderStatus;
        DateTime FilterFromDate, FilterToDate;

        public OrdersMainForm()
        {
            try
            {
                InitializeComponent();

                CommonFunctions.SetDataGridViewProperties(dtGridViewOrders);
                CommonFunctions.SetDataGridViewProperties(dtGridViewOrderedProducts);

                ObjOrdersModel = new OrdersModel();
                ObjOrdersModel.Initialize();

                dTimePickerFrom.Value = DateTime.Today;
                dTimePickerTo.Value = DateTime.Today.AddDays(30);
                FilterFromDate = DateTime.MinValue;
                FilterToDate = DateTime.MinValue;
                CurrOrderStatus = ORDERSTATUS.Created;

                cmbBoxOrderStatus.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbBoxOrderStatus.Items.Clear();
                cmbBoxOrderStatus.Items.Add(AllOrderstatus);
                cmbBoxOrderStatus.Items.Add(ORDERSTATUS.Created.ToString());
                cmbBoxOrderStatus.Items.Add(ORDERSTATUS.Completed.ToString());
                cmbBoxOrderStatus.Items.Add(ORDERSTATUS.Cancelled.ToString());
                cmbBoxOrderStatus.SelectedIndex = 1;

                cmbBoxLine.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbBoxLine.Items.Clear();
                cmbBoxLine.Items.Add(AllOrderstatus);
                cmbBoxLine.Items.AddRange(CommonFunctions.ObjCustomerMasterModel.GetAllLineNames().ToArray());
                cmbBoxLine.SelectedIndex = 0;

                btnPrintPreview.Visible = false;
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
                if (cmbBoxLine.SelectedIndex > 0)
                    dtAllOrders = GetOrdersDataTable(FilterFromDate, FilterToDate, CurrOrderStatus, cmbBoxLine.SelectedItem.ToString());
                else
                    dtAllOrders = GetOrdersDataTable(FilterFromDate, FilterToDate, CurrOrderStatus, null);
                LoadOrdersGridView();

                dtGridViewOrderedProducts.DataSource = null;
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
                OrderDetails tmpOrderDetails = (OrderDetails)ObjAddUpdatedDetails;
                switch (Mode)
                {
                    case 1:     //Add Order
                        Object[] ArrItems = new Object[] {
                            tmpOrderDetails.OrderID,
                            tmpOrderDetails.CustomerID,
                            tmpOrderDetails.OrderNumber,
                            new MySql.Data.Types.MySqlDateTime(tmpOrderDetails.OrderDate),
                            CommonFunctions.ObjCustomerMasterModel.GetCustomerDetails(tmpOrderDetails.CustomerID).CustomerName,
                            tmpOrderDetails.OrderItemCount,
                            tmpOrderDetails.EstimateOrderAmount,
                            tmpOrderDetails.OrderStatus,
                            new MySql.Data.Types.MySqlDateTime(tmpOrderDetails.CreationDate),
                            new MySql.Data.Types.MySqlDateTime(tmpOrderDetails.LastUpdatedDate),
                            null, null
                        };
                        dtAllOrders.Rows.Add(ArrItems);
                        break;
                    case 2:     //Update Order
                        DataRow dtRow = dtAllOrders.Select($"OrderID = {tmpOrderDetails.OrderID}")[0];
                        dtRow["Order Item Count"] = tmpOrderDetails.OrderItemCount;
                        dtRow["Estimate Order Amount"] = tmpOrderDetails.EstimateOrderAmount;
                        dtRow["Order Status"] = tmpOrderDetails.EstimateOrderAmount;
                        dtRow["Last Updated Date"] = new MySql.Data.Types.MySqlDateTime(tmpOrderDetails.LastUpdatedDate);
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
                CommonFunctions.ShowDialog(new CreateOrderInvoiceForm(-1, true, false, UpdateOrdersOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnCreateOrder_Click()", ex);
            }
        }

        private void btnViewEditOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewOrders.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select an Order to View/Update", "View Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!dtGridViewOrders.SelectedRows[0].Cells["Order Status"].Value.ToString().Equals(ORDERSTATUS.Created.ToString()))
                {
                    MessageBox.Show(this, "Please select an Order which is not Completed/Cancelled to edit.\nIf the Order is already completed then you can view/update in Invoices.", "View Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Int32 OrderID = Int32.Parse(dtGridViewOrders.SelectedRows[0].Cells["OrderID"].Value.ToString());

                CommonFunctions.ShowDialog(new CreateOrderInvoiceForm(OrderID, true, false, UpdateOrdersOnClose), this);

                dtGridViewOrders.ClearSelection();
                dtGridViewOrderedProducts.DataSource = null;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnViewEditOrder_Click()", ex);
            }
        }

        private void btnConvertInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewOrders.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select an Order to convert to Invoice", "Convert Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!dtGridViewOrders.SelectedRows[0].Cells["Order Status"].Value.ToString().Equals(ORDERSTATUS.Created.ToString()))
                {
                    MessageBox.Show(this, "Please select an Order which is not Completed/Cancelled to convert to Invoice.", "Convert Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DialogResult dialogResult = MessageBox.Show(this, "This Order will be created as Invoice. Do you want to continue?", "Convert Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dialogResult == DialogResult.No) return;

                Int32 OrderID = Int32.Parse(dtGridViewOrders.SelectedRows[0].Cells["OrderID"].Value.ToString());

                Int32 RetVal = ObjOrdersModel.ConvertOrderToInvoice(OrderID);
                if (RetVal == 0)
                {
                    MessageBox.Show(this, "Order converted to Invoice successfully", "Convert Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtAllOrders.Select($"OrderID = {OrderID}")[0]["Order Status"] = ORDERSTATUS.Completed;
                    dtGridViewOrders.ClearSelection();
                    dtGridViewOrderedProducts.DataSource = null;

                    dialogResult = MessageBox.Show(this, "Would you like to open the new Invoice?", "Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (dialogResult == DialogResult.Yes)
                    {
                        InvoicesMainForm invoicesMainForm = new InvoicesMainForm(OrderID);
                        ((MainForm)this.MdiParent).ShowChildForm(invoicesMainForm);
                    }
                }
                else if (RetVal == -2)
                {
                    MessageBox.Show(this, "Unable to find Order to convert to Invoice", "Convert Order", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(this, "Unable to convert Order to Invoice", "Convert Order", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnConvertInvoice_Click()", ex);
            }
        }

        private void btnPrintOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewOrders.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select an Order to Print.", "Print Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                BackgroundTask = 1;
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
                CommonFunctions.ShowErrorDialog($"{this}.btnPrintOrder_Click()", ex);
            }
        }

        private void btnSearchOrder_Click(object sender, EventArgs e)
        {
            try
            {
                List<String> ListFindInFields = new List<String>()
                {
                    "Customer Name",
                    "Order Number",
                    "Order Status",
                    "Order Date"
                };
                CommonFunctions.ShowDialog(new OrderInvQuotSearchForm(ListFindInFields, null, null), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnSearchOrder_Click()", ex);
            }
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewOrders.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select an Order to Delete", "Delete Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!dtGridViewOrders.SelectedRows[0].Cells["Order Status"].Value.ToString().Equals(ORDERSTATUS.Created.ToString()))
                {
                    MessageBox.Show(this, "Please select an Order which is not Completed/Cancelled to delete.", "Delete Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DialogResult dialogResult = MessageBox.Show(this, "Are you sure to Cancel the selected Order?", "Delete Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }

                Int32 OrderID = Int32.Parse(dtGridViewOrders.SelectedRows[0].Cells["OrderID"].Value.ToString());
                if (ObjOrdersModel.DeleteOrderDetails(OrderID) == 0)
                {
                    dtGridViewOrderedProducts.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnDeleteOrder_Click()", ex);
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

        private void checkBoxApplyFilter_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!checkBoxApplyFilter.Checked)
                {
                    FilterFromDate = DateTime.MinValue;
                    FilterToDate = DateTime.MinValue;
                }
                else
                {
                    FilterFromDate = dTimePickerFrom.Value;
                    FilterToDate = dTimePickerTo.Value;
                }
                LoadGridViewBG();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.checkBoxApplyFilter_CheckedChanged()", ex);
            }
        }

        private void LoadGridViewOrderItems(OrderDetails ObjOrderDetails)
        {
            try
            {
                dtGridViewOrderedProducts.DataSource = null;
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
                dtGridViewOrderedProducts.DataSource = dtOrderProducts.DefaultView;
                dtGridViewOrderedProducts.Columns["ProductID"].Visible = false;
                dtGridViewOrderedProducts.ClearSelection();
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
                    dtGridViewOrderedProducts.DataSource = null;
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

        private void cmbBoxOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(cmbBoxOrderStatus.SelectedIndex > 0)
                {
                    CurrOrderStatus = (ORDERSTATUS)Enum.Parse(Type.GetType("SalesOrdersReport.Models.ORDERSTATUS"), cmbBoxOrderStatus.SelectedItem.ToString());
                }
                else
                {
                    CurrOrderStatus = ORDERSTATUS.All;
                }
                LoadGridViewBG();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxOrderStatus_SelectedIndexChanged()", ex);
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new ExportToExcelForm(ExportDataTypes.Orders, null, ExportOrders), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnExportToExcel_Click()", ex);
            }
        }

        Int32 ExportOption = -1;
        String ExportFolderPath = "";
        private Int32 ExportOrders(String FilePath, Object ObjDetails, Boolean Append)
        {
            try
            {
                ExportOption = (Int32)ObjDetails;
                ExportFolderPath = FilePath;

                if ((ExportOption & 2) > 0 && dtGridViewOrders.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "No Order was selected. Please select an Order.", "Export Order", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return -1;
                }

                BackgroundTask = 2;
#if DEBUG
                backgroundWorkerOrders_DoWork(null, null);
                backgroundWorkerOrders_RunWorkerCompleted(null, null);
#else
                ReportProgress = backgroundWorkerOrders.ReportProgress;
                backgroundWorkerOrders.RunWorkerAsync();
                backgroundWorkerOrders.WorkerReportsProgress = true;
#endif
                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ExportOrders()", ex);
                return -1;
            }
        }

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
                LoadGridView();
                /*BackgroundTask = 3;
#if DEBUG
                backgroundWorkerOrders_DoWork(null, null);
                backgroundWorkerOrders_RunWorkerCompleted(null, null);
#else
                ReportProgress = backgroundWorkerOrders.ReportProgress;
                backgroundWorkerOrders.RunWorkerAsync();
                backgroundWorkerOrders.WorkerReportsProgress = true;
#endif
                */
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadGridViewBG()", ex);
            }
        }

        private void cmbBoxLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadGridViewBG();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxLine_SelectedIndexChanged()", ex);
            }
        }

        private void printDocumentOrders_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(250, 600);
            this.DrawToBitmap(bm, new Rectangle(0, 0, 250, 600));
            e.Graphics.DrawImage(bm, new Point(0, 0));

            //e.Graphics.DrawString("Test Print Document", new System.Drawing.Font(new System.Drawing.FontFamily("Times New Roman"), 8, System.Drawing.FontStyle.Bold), Brushes.Black, new Point(10, 20));
            //e.Graphics.DrawLine(new Pen(Brushes.Red), new Point(10, 40), new Point(100, 40));
            //e.Graphics.DrawString("This is in Line 2", new System.Drawing.Font(new System.Drawing.FontFamily("Times New Roman"), 8, System.Drawing.FontStyle.Bold), Brushes.Black, new Point(10, 60));
            //e.Graphics.DrawLine(new Pen(Brushes.Red), new Point(10, 80), new Point(100, 80));
            //e.Graphics.DrawString("This is in Line 3", new System.Drawing.Font(new System.Drawing.FontFamily("Times New Roman"), 8, System.Drawing.FontStyle.Bold), Brushes.Black, new Point(10, 100));
            //e.Graphics.DrawLine(new Pen(Brushes.Red), new Point(10, 120), new Point(100, 120));
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                printPreviewDialogOrders.Document = printDocumentOrders;
                printDocumentOrders.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("", 250, 600);
                printPreviewDialogOrders.ShowDialog();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnPrintPreview_Click()", ex);
            }
        }
    }
}
