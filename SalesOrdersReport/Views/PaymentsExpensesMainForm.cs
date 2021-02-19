using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Models;

namespace SalesOrdersReport.Views
{
    public partial class PaymentsExpensesMainForm : Form
    {
        PaymentsModel ObjPaymentsModel;
        DataTable dtAllPayments;

        public PaymentsExpensesMainForm()
        {
            try
            {
                InitializeComponent();

                CommonFunctions.SetDataGridViewProperties(dtGridViewPayments);
                CommonFunctions.SetDataGridViewProperties(dtGridViewExpenses);

                ObjPaymentsModel = new PaymentsModel();

                dTimePickerFromPayments.Value = DateTime.Today.AddDays(-30);
                dTimePickerToPayments.Value = DateTime.Today;

                LoadPaymentsGridView();
                LoadExpensesGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("PaymentsExpensesMainForm.ctor()", ex);
            }
        }

        private void LoadPaymentsGridView()
        {
            try
            {
                //dtAllPayments = ObjPaymentsModel.GetPaytmentsDataTable(dTimePickerFromPayments.Value, dTimePickerToPayments.Value);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadPaymentsGridView()", ex);
            }
        }

        private void LoadExpensesGridView()
        {
            try
            {
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadExpensesGridView()", ex);
            }
        }

        private void PaymentsMainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                this.MaximizeBox = true;
                this.WindowState = FormWindowState.Maximized;
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.PaymentsMainForm_Shown()", ex);
            }
        }

        DataTable GetPaymentsDataTable(DateTime FromDate, DateTime ToDate, INVOICESTATUS InvoiceStatus)
        {
            try
            {
                DataTable dtPayments = null;

                return dtPayments;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetPaymentsDataTable()", ex);
                return null;
            }
        }

        void UpdatePaymentsOnClose(Int32 Mode, Object ObjAddUpdatedDetails = null)
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
                CommonFunctions.ShowErrorDialog($"{this}.UpdatePaymentsOnClose()", ex);
            }
        }

        private void btnCreatePayment_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new CreatePaymentForm(), this.Parent.FindForm());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnCreatePayment_Click()", ex);
            }
        }

        private void btnCreateExpense_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new CreateExpenseForm(), this.Parent.FindForm());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnCreateExpense_Click()", ex);
            }
        }

        private void btnViewEditPayment_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new CreatePaymentForm(), this.Parent.FindForm());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnViewEditPayment_Click()", ex);
            }
        }

        private void btnViewEditExpense_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new CreateExpenseForm(), this.Parent.FindForm());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnViewEditExpense_Click()", ex);
            }
        }

        private void btnPrintPayment_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnPrintPayment_Click()", ex);
            }
        }

        private void btnSearchPayment_Click(object sender, EventArgs e)
        {
            try
            {
                List<String> ListFindInFields = new List<String>()
                {
                    "Customer Name",
                    "Invoice Number",
                    "Invoice Date",
                    "Payment Date",
                    "Payment Amount",
                    "Payment Method",
                    "Payment Received by"
                };
                CommonFunctions.ShowDialog(new OrderInvQuotSearchForm(ListFindInFields, null, null), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnSearchPayment_Click()", ex);
            }
        }

        private void btnSearchExpense_Click(object sender, EventArgs e)
        {
            try
            {
                List<String> ListFindInFields = new List<String>()
                {
                    "Vendor Name",
                    "Expense Date",
                    "Expense Amount",
                    "Expense Method",
                    "Expense Paid by"
                };
                CommonFunctions.ShowDialog(new OrderInvQuotSearchForm(ListFindInFields, null, null), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnSearchExpense_Click()", ex);
            }
        }

        private void btnDeletePayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewPayments.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select an Invoice to Delete", "Delete Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnDeletePayment_Click()", ex);
            }
        }

        private void btnReloadPayments_Click(object sender, EventArgs e)
        {
            try
            {
                LoadPaymentsGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnReloadPayments_Click()", ex);
            }
        }

        private void btnReloadExpenses_Click(object sender, EventArgs e)
        {
            try
            {
                LoadExpensesGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnReloadExpenses_Click()", ex);
            }
        }
        private void checkBoxApplyFilterPayments_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!checkBoxApplyFilterPayment.Checked)
                {
                    dTimePickerFromPayments.Value = DateTime.Today;
                    dTimePickerToPayments.Value = dTimePickerFromPayments.Value.AddDays(30);
                }
                LoadPaymentsGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.checkBoxApplyFilterPayments_CheckedChanged()", ex);
            }
        }

        private void checkBoxApplyFilterExpenses_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!checkBoxApplyFilterExpense.Checked)
                {
                    dTimePickerFromPayments.Value = DateTime.Today;
                    dTimePickerToPayments.Value = dTimePickerFromPayments.Value.AddDays(30);
                }
                LoadExpensesGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.checkBoxApplyFilterExpenses_CheckedChanged()", ex);
            }
        }

        private void dtGridViewInvoices_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewInvoices_CellMouseClick()", ex);
            }
        }
    }
}
