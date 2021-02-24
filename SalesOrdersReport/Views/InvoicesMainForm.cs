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
        InvoicesModel ObjInvoicesModel;
        DataTable dtAllInvoices;
        String AllInvoicestatus = "<All>";
        INVOICESTATUS CurrInvoiceStatus;
        DateTime FilterFromDate, FilterToDate;
        Boolean IsFormLoaded = false;

        public InvoicesMainForm()
        {
            try
            {
                InitializeComponent();

                CommonFunctions.SetDataGridViewProperties(dtGridViewInvoices);
                CommonFunctions.SetDataGridViewProperties(dtGridViewInvoicedProducts);

                ObjInvoicesModel = new InvoicesModel();
                ObjInvoicesModel.Initialize();

                dTimePickerFrom.Value = DateTime.Today.AddDays(-30);
                dTimePickerTo.Value = DateTime.Today.AddDays(30);
                FilterFromDate = DateTime.MinValue;
                FilterToDate = DateTime.MinValue;
                CurrInvoiceStatus = INVOICESTATUS.Created;

                cmbBoxInvoiceStatus.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbBoxInvoiceStatus.Items.Clear();
                cmbBoxInvoiceStatus.Items.Add(AllInvoicestatus);
                cmbBoxInvoiceStatus.Items.Add(INVOICESTATUS.Created.ToString());
                cmbBoxInvoiceStatus.Items.Add(INVOICESTATUS.Paid.ToString());
                cmbBoxInvoiceStatus.Items.Add(INVOICESTATUS.Cancelled.ToString());
                cmbBoxInvoiceStatus.SelectedIndex = 1;

                cmbBoxLine.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbBoxLine.Items.Clear();
                cmbBoxLine.Items.Add(AllInvoicestatus);
                cmbBoxLine.Items.AddRange(CommonFunctions.ObjCustomerMasterModel.GetAllLineNames().ToArray());
                cmbBoxLine.SelectedIndex = 0;

                LoadGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("InvoicesMainForm.ctor()", ex);
            }
        }

        private void LoadGridView()
        {
            try
            {
                if (cmbBoxLine.SelectedIndex == 0)
                    dtAllInvoices = GetInvoicesDataTable(FilterFromDate, FilterToDate, CurrInvoiceStatus, null);
                else
                    dtAllInvoices = GetInvoicesDataTable(FilterFromDate, FilterToDate, CurrInvoiceStatus, cmbBoxLine.SelectedItem.ToString());
                LoadInvoicesGridView();

                dtGridViewInvoicedProducts.DataSource = null;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadGridView()", ex);
            }
        }

        private void InvoicesMainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                this.MaximizeBox = true;
                this.WindowState = FormWindowState.Maximized;
                this.StartPosition = FormStartPosition.CenterScreen;

                IsFormLoaded = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.InvoicesMainForm_Shown()", ex);
            }
        }

        DataTable GetInvoicesDataTable(DateTime FromDate, DateTime ToDate, INVOICESTATUS InvoiceStatus, String LineName)
        {
            try
            {
                DataTable dtInvoices = null;

                if (String.IsNullOrEmpty(LineName))
                    dtInvoices = ObjInvoicesModel.LoadInvoiceDetails(FromDate, ToDate, InvoiceStatus);
                else
                    dtInvoices = ObjInvoicesModel.LoadInvoiceDetails(FromDate, ToDate, InvoiceStatus, "Line", LineName);

                return dtInvoices;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetInvoicesDataTable()", ex);
                return null;
            }
        }

        void LoadInvoicesGridView()
        {
            try
            {
                dtGridViewInvoices.DataSource = dtAllInvoices.DefaultView;

                for (int i = 0; i < dtGridViewInvoices.Columns.Count; i++)
                {
                    if (dtGridViewInvoices.Columns[i].Name.Equals("InvoiceID") || 
                        dtGridViewInvoices.Columns[i].Name.Equals("OrderID") || dtGridViewInvoices.Columns[i].Name.Equals("CustomerID"))
                        dtGridViewInvoices.Columns[i].Visible = false;
                }
                dtGridViewInvoices.ClearSelection();

                lblOrdersCount.Text = $"[Displaying {dtGridViewInvoices.Rows.Count} of {dtAllInvoices.Rows.Count} Invoices]";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadInvoicesGridView()", ex);
            }
        }

        void UpdateInvoicesOnClose(Int32 Mode, Object ObjAddUpdatedDetails = null)
        {
            try
            {
                switch (Mode)
                {
                    case 1:     //Add Invoice
                        break;
                    case 2:
                        break;
                    case 3:     //Reload Invoices
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateInvoicesOnClose()", ex);
            }
        }

        private void btnCreateInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new CreateOrderInvoiceForm(-1, false, true, UpdateInvoicesOnClose), this.Parent.FindForm());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnCreateInvoice_Click()", ex);
            }
        }

        private void btnViewEditInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewInvoices.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select an Invoice to View/Update", "View Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Int32 InvoiceID = Int32.Parse(dtGridViewInvoices.SelectedRows[0].Cells["InvoiceID"].Value.ToString());

                CommonFunctions.ShowDialog(new CreateOrderInvoiceForm(InvoiceID, false, true, UpdateInvoicesOnClose), this);

                dtGridViewInvoices.ClearSelection();
                dtGridViewInvoicedProducts.DataSource = null;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnViewEditInvoice_Click()", ex);
            }
        }

        private void btnPrintInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewInvoices.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select an Invoice to Print", "Print Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Int32 OrderID = Int32.Parse(dtGridViewInvoices.SelectedRows[0].Cells["OrderID"].Value.ToString());
                InvoiceDetails ObjInvoiceDetails = ObjInvoicesModel.GetInvoiceDetailsForInvoiceID(OrderID);
                ObjInvoicesModel.PrintInvoice(ReportType.QUOTATION, true, ObjInvoiceDetails);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnPrintInvoice_Click()", ex);
            }
        }

        private void btnSearchInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                List<String> ListFindInFields = new List<String>()
                {
                    "Customer Name",
                    "Invoice Number",
                    "Invoice Status",
                    "Invoice Date"
                };
                CommonFunctions.ShowDialog(new OrderInvQuotSearchForm(ListFindInFields, null, null), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnSearchInvoice_Click()", ex);
            }
        }

        private void btnDeleteInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewInvoices.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select an Invoice to Delete", "Delete Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Int32 InvoiceID = Int32.Parse(dtGridViewInvoices.SelectedRows[0].Cells["InvoiceID"].Value.ToString());
                if (ObjInvoicesModel.DeleteInvoiceDetails(InvoiceID) == 0)
                {
                    dtGridViewInvoicedProducts.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnDeleteInvoice_Click()", ex);
            }
        }

        private void btnReloadInvoices_Click(object sender, EventArgs e)
        {
            try
            {
                IsFormLoaded = false;
                CurrInvoiceStatus = INVOICESTATUS.Created;
                FilterFromDate = DateTime.MinValue;
                FilterToDate = DateTime.MinValue;
                checkBoxApplyFilter.Checked = false;
                cmbBoxInvoiceStatus.SelectedIndex = 1;
                cmbBoxLine.SelectedIndex = 0;
                IsFormLoaded = true;

                LoadGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnReloadInvoices_Click()", ex);
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
                    dTimePickerFrom.Value = DateTime.Today.AddDays(-30);
                    dTimePickerTo.Value = DateTime.Today.AddDays(30);
                }

                LoadGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.checkBoxApplyFilter_CheckedChanged()", ex);
            }
        }

        private void LoadGridViewInvoiceItems(InvoiceDetails ObjInvoiceDetails)
        {
            try
            {
                dtGridViewInvoicedProducts.DataSource = null;
                if (ObjInvoiceDetails.ListInvoiceItems == null || ObjInvoiceDetails.ListInvoiceItems.Count == 0) return;

                DataTable dtInvoiceProducts = new DataTable();
                String[] ArrColumns = new String[] { "ProductID", "Product Name", "Ordered Qty", "Sale Qty", "Price",
                                                    "Gross Total", "Tax", "Net Total", "Invoice Item Status" };
                Type[] ArrColumnTypes = new Type[] { CommonFunctions.TypeInt32, CommonFunctions.TypeString, CommonFunctions.TypeDouble,
                                                    CommonFunctions.TypeDouble, CommonFunctions.TypeDouble, CommonFunctions.TypeDouble,
                                                    CommonFunctions.TypeDouble, CommonFunctions.TypeDouble, CommonFunctions.TypeString };

                for (int i = 0; i < ArrColumns.Length; i++)
                {
                    dtInvoiceProducts.Columns.Add(ArrColumns[i], ArrColumnTypes[i]);
                }

                for (int i = 0; i < ObjInvoiceDetails.ListInvoiceItems.Count; i++)
                {
                    InvoiceItemDetails tmpInvoiceItem = ObjInvoiceDetails.ListInvoiceItems[i];
                    Object[] ArrObjects = new Object[]
                    {
                        tmpInvoiceItem.ProductID,
                        tmpInvoiceItem.ProductName,
                        tmpInvoiceItem.OrderQty,
                        tmpInvoiceItem.SaleQty,
                        tmpInvoiceItem.Price,
                        tmpInvoiceItem.TaxableValue,
                        tmpInvoiceItem.CGST + tmpInvoiceItem.SGST + tmpInvoiceItem.IGST,
                        tmpInvoiceItem.NetTotal,
                        tmpInvoiceItem.InvoiceItemStatus
                    };
                    dtInvoiceProducts.Rows.Add(ArrObjects);
                }

                dtInvoiceProducts.DefaultView.Sort = "Product Name";
                dtGridViewInvoicedProducts.DataSource = dtInvoiceProducts.DefaultView;
                dtGridViewInvoicedProducts.Columns["ProductID"].Visible = false;
                dtGridViewInvoicedProducts.ClearSelection();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadGridViewInvoiceItems()", ex);
            }
        }

        Int32 InvoiceIDSelected = -1;
        private void dtGridViewInvoices_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Int32 InvoiceID = Int32.Parse(dtGridViewInvoices.SelectedRows[0].Cells["InvoiceID"].Value.ToString());
                if (InvoiceIDSelected == InvoiceID)
                {
                    InvoiceIDSelected = -1;
                    dtGridViewInvoices.ClearSelection();
                    dtGridViewInvoicedProducts.DataSource = null;
                    return;
                }

                InvoiceDetails tmpInvoiceDetails = ObjInvoicesModel.FillInvoiceItemDetails(InvoiceID);
                LoadGridViewInvoiceItems(tmpInvoiceDetails);
                InvoiceIDSelected = InvoiceID;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewInvoices_CellMouseClick()", ex);
            }
        }

        private void cmbBoxInvoiceStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsFormLoaded) return;

                if(cmbBoxInvoiceStatus.SelectedIndex > 0)
                {
                    CurrInvoiceStatus = (INVOICESTATUS)Enum.Parse(Type.GetType("SalesOrdersReport.Models.INVOICESTATUS"), cmbBoxInvoiceStatus.SelectedItem.ToString());
                }
                else
                {
                    CurrInvoiceStatus = INVOICESTATUS.All;
                }
                LoadGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxInvoiceStatus_SelectedIndexChanged()", ex);
            }
        }

        private void cmbBoxLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsFormLoaded) return;

                LoadGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxLine_SelectedIndexChanged()", ex);
            }
        }
    }
}
