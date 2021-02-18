using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Models;

namespace SalesOrdersReport.Views
{
    public partial class InvoicesMainForm : Form
    {
        OrdersModel ObjOrdersModel;
        DataTable dtAllOrders;
        String AllOrderstatus = "<All>";
        ORDERSTATUS CurrOrderStatus;

        public InvoicesMainForm()
        {
            try
            {
                InitializeComponent();

                CommonFunctions.SetDataGridViewProperties(dtGridViewInvoices);
                CommonFunctions.SetDataGridViewProperties(dtGridViewInvoicedProducts);

                ObjOrdersModel = new OrdersModel();
                ObjOrdersModel.Initialize();

                dTimePickerFrom.Value = DateTime.Today;
                dTimePickerTo.Value = dTimePickerFrom.Value.AddDays(30);
                CurrOrderStatus = ORDERSTATUS.Created;

                cmbBoxInvoiceStatus.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbBoxInvoiceStatus.Items.Clear();
                cmbBoxInvoiceStatus.Items.Add(AllOrderstatus);
                cmbBoxInvoiceStatus.Items.Add(ORDERSTATUS.Created.ToString());
                cmbBoxInvoiceStatus.Items.Add(ORDERSTATUS.Completed.ToString());
                cmbBoxInvoiceStatus.Items.Add(ORDERSTATUS.Cancelled.ToString());
                cmbBoxInvoiceStatus.SelectedIndex = 1;
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
                dtAllOrders = GetOrdersDataTable(dTimePickerFrom.Value, dTimePickerTo.Value, CurrOrderStatus);
                LoadOrdersGridView();

                dtGridViewInvoicedProducts.DataSource = null;
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

        DataTable GetOrdersDataTable(DateTime FromDate, DateTime ToDate, ORDERSTATUS OrderStatus)
        {
            try
            {
                DataTable dtOrders = ObjOrdersModel.LoadOrderDetails(FromDate, ToDate, OrderStatus);

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
                dtGridViewInvoices.DataSource = dtAllOrders.DefaultView;

                for (int i = 0; i < dtGridViewInvoices.Columns.Count; i++)
                {
                    if (dtGridViewInvoices.Columns[i].Name.Equals("OrderID") || dtGridViewInvoices.Columns[i].Name.Equals("CustomerID"))
                        dtGridViewInvoices.Columns[i].Visible = false;
                }
                dtGridViewInvoices.ClearSelection();

                lblOrdersCount.Text = $"[Displaying {dtGridViewInvoices.Rows.Count} of {dtAllOrders.Rows.Count} Orders]";
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
                CommonFunctions.ShowDialog(new CreateOrderInvoiceForm(-1, true, false, UpdateOrdersOnClose), this.Parent.FindForm());
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
                if (dtGridViewInvoices.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select an Order to View/Update", "View Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Int32 OrderID = Int32.Parse(dtGridViewInvoices.SelectedRows[0].Cells["OrderID"].Value.ToString());

                CommonFunctions.ShowDialog(new CreateOrderInvoiceForm(OrderID, true, false, UpdateOrdersOnClose), this);

                dtGridViewInvoices.ClearSelection();
                dtGridViewInvoicedProducts.DataSource = null;
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
                if (dtGridViewInvoices.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select an Order to convert to Invoice", "Convert Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Int32 OrderID = Int32.Parse(dtGridViewInvoices.SelectedRows[0].Cells["OrderID"].Value.ToString());

                if (ObjOrdersModel.ConvertOrderToInvoice(OrderID) == 0)
                {
                    MessageBox.Show(this, "Order converted to Invoice successfully", "Convert Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtAllOrders.Select($"OrderID = {OrderID}")[0]["Order Status"] = ORDERSTATUS.Completed;
                    dtGridViewInvoices.ClearSelection();
                    dtGridViewInvoicedProducts.DataSource = null;
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
                if (dtGridViewInvoices.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select an Order to convert to Invoice", "Convert Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Int32 OrderID = Int32.Parse(dtGridViewInvoices.SelectedRows[0].Cells["OrderID"].Value.ToString());
                OrderDetails ObjOrderDetails = ObjOrdersModel.GetOrderDetailsForOrderID(OrderID);
                ObjOrdersModel.PrintOrder(ReportType.QUOTATION, true, ObjOrderDetails);
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
                if (dtGridViewInvoices.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select an Order to Delete", "Delete Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Int32 OrderID = Int32.Parse(dtGridViewInvoices.SelectedRows[0].Cells["OrderID"].Value.ToString());
                if (ObjOrdersModel.DeleteOrderDetails(OrderID) == 0)
                {
                    dtGridViewInvoicedProducts.DataSource = null;
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
                LoadGridView();
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
                    dTimePickerFrom.Value = DateTime.Today;
                    dTimePickerTo.Value = dTimePickerFrom.Value.AddDays(30);
                }
                LoadGridView();
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
                dtGridViewInvoicedProducts.DataSource = null;
                if (ObjOrderDetails.ListOrderItems == null || ObjOrderDetails.ListOrderItems.Count == 0) return;

                DataTable dtOrderProducts = new DataTable();
                String[] ArrColumns = new String[] { "ProductID", "Product Name", "Ordered Qty", "Price", "Order Status" };
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
                dtGridViewInvoicedProducts.DataSource = dtOrderProducts.DefaultView;
                dtGridViewInvoicedProducts.Columns["ProductID"].Visible = false;
                dtGridViewInvoicedProducts.ClearSelection();
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
                Int32 OrderID = Int32.Parse(dtGridViewInvoices.SelectedRows[0].Cells["OrderID"].Value.ToString());
                if (OrderIDSelected == OrderID)
                {
                    OrderIDSelected = -1;
                    dtGridViewInvoices.ClearSelection();
                    dtGridViewInvoicedProducts.DataSource = null;
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
                if(cmbBoxInvoiceStatus.SelectedIndex > 0)
                {
                    CurrOrderStatus = (ORDERSTATUS)Enum.Parse(Type.GetType("SalesOrdersReport.Models.ORDERSTATUS"), cmbBoxInvoiceStatus.SelectedItem.ToString());
                }
                else
                {
                    CurrOrderStatus = ORDERSTATUS.All;
                }
                LoadGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxOrderStatus_SelectedIndexChanged()", ex);
            }
        }
    }
}
