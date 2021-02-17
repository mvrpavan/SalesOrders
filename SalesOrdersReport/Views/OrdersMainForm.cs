using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Models;

namespace SalesOrdersReport.Views
{
    public partial class OrdersMainForm : Form
    {
        ProductMasterModel ObjProductMasterModel;
        OrdersModel ObjOrdersModel;
        DataTable dtAllOrders;

        public OrdersMainForm()
        {
            try
            {
                InitializeComponent();

                CommonFunctions.SetDataGridViewProperties(dtGridViewOrders);
                CommonFunctions.SetDataGridViewProperties(dtGridViewOrderedProducts);

                dtAllOrders = GetOrdersDataTable();
                LoadOrdersGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("OrdersMainForm.ctor()", ex);
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

        DataTable GetOrdersDataTable()
        {
            try
            {
                DataTable dtOrders = new DataTable();

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
                    if (dtGridViewOrders.Columns[i].Name.Equals("OrderID"))
                        dtGridViewOrders.Columns[i].Visible = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        void UpdateOnClose(Int32 Mode)
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
                CommonFunctions.ShowDialog(new CreateOrderInvoiceForm(-1, new Models.OrdersModel(), true, false), this.Parent.FindForm());
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
                    MessageBox.Show(this, "Please select a Product to edit", "Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Int32 OrderID = Int32.Parse(dtGridViewOrders.SelectedRows[0].Cells["ID"].Value.ToString());

                CommonFunctions.ShowDialog(new CreateOrderInvoiceForm(OrderID, new Models.OrdersModel(), true, false), this);
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
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.checkBoxApplyFilter_CheckedChanged()", ex);
            }
        }
    }
}
