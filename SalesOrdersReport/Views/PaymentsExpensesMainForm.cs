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
                dtAllPayments = ObjPaymentsModel.GetPaytmentsDataTable(dTimePickerFromPayments.Value, dTimePickerToPayments.Value);
                if (dtAllPayments.Rows.Count == 0)
                {
                    var dataTable = new DataTable();
                    dataTable.Columns.Add("Message", typeof(string));
                    dataTable.Rows.Add("No Payments Added/found in DB");

                    dtGridViewPayments.DataSource = new BindingSource { DataSource = dataTable };
                    dtGridViewPayments.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                else
                {
                    //dtGridViewPayments.DataSource = dtAllPayments;
                    //dtGridViewPayments.Rows.Clear();
                    //dtGridViewPayments.Columns.Clear();
                    dtGridViewPayments.DataSource = null;
                    List<PaymentDetails> ListPaymentDtlsCache = ObjPaymentsModel.GetPaymentDtlsCache();
                    if (ListPaymentDtlsCache.Count > 0)
                    {
                        //PaymentID, PaymentDate, InvoiceID, QuotationID, AccountID, PaymentModeID, PaymentAmount, Description, CreationDate, LastUpdateDate, UserID
                        String[] ArrColumnNames = new String[] { "PAYMENTID", "INVOICEID", "QUOTATIONID", "ACCOUNTID", "PAYMENTMODEID", "INVOICENUMBER", "CUSTOMERNAME", "PAYMENTDATE", "PAYMENTMODE", "PAYMENTAMOUNT", "DESCRIPTION", "CREATIONDATE", "LASTUPDATEDATE", "STAFFNAME", "ACTIVE" };
                        String[] ArrColumnHeaders = new String[] { "Payment ID", "Invoice ID", "Quotation  ID", "AccountID", "PaymentMode ID", "InvoiceNumber", "Customer Name", "Payment Date", "Payment Mode", "Amount", "Description", "CreationDate", "LastUpdateDate", "Staff Name", "Active" };
                        for (int i = 0; i < ArrColumnNames.Length; i++)
                        {
                            dtGridViewPayments.Columns.Add(ArrColumnNames[i], ArrColumnHeaders[i]);
                            DataGridViewColumn CurrentCol = dtGridViewPayments.Columns[dtGridViewPayments.Columns.Count - 1];
                            CurrentCol.ReadOnly = true;
                            if (i <= 4) CurrentCol.Visible = false;    //CustomerID, LineID", "Discount Group ID, Price Group ID,State ID
                        }
                        foreach (PaymentDetails ObjPaymentDetails in ListPaymentDtlsCache)
                        {
                            Object[] ArrRowItems = new Object[15];
                            ArrRowItems[0] = ObjPaymentDetails.PaymentId;
                            ArrRowItems[1] = ObjPaymentDetails.InvoiceID;
                            ArrRowItems[2] = ObjPaymentDetails.QuotationID;
                            ArrRowItems[3] = ObjPaymentDetails.AccountID;
                            ArrRowItems[4] = ObjPaymentDetails.PaymentModeID;
                            ArrRowItems[5] = ObjPaymentDetails.InvoiceNumber;
                            ArrRowItems[6] = ObjPaymentDetails.CustomerName;
                            ArrRowItems[7] = ObjPaymentDetails.PaidOn;

                            ArrRowItems[8] = ObjPaymentDetails.PaymentMode;
                            ArrRowItems[9] = ObjPaymentDetails.Amount;
                            ArrRowItems[10] = ObjPaymentDetails.Description;
                            ArrRowItems[11] = ObjPaymentDetails.PaidOn;
                            ArrRowItems[12] = ObjPaymentDetails.LastUpdateDate;
                            ArrRowItems[13] = ObjPaymentDetails.StaffName;
                            ArrRowItems[14] = ObjPaymentDetails.Active;
                            dtGridViewPayments.Rows.Add(ArrRowItems);
                        }
                    }
                }
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
                    case 1:     //Add Payment
                        LoadPaymentsGridView();
                        break;
                    case 2:
                        break;
                    case 3:     //Reload 
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
                CommonFunctions.ShowDialog(new CreatePaymentForm(UpdatePaymentsOnClose, dTimePickerFromPayments.Value, dTimePickerToPayments.Value), this);
                //CommonFunctions.ShowDialog(new CreatePaymentForm(UpdatePaymentsOnClose), this);
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
                CommonFunctions.ShowDialog(new CreatePaymentForm(UpdatePaymentsOnClose, dTimePickerFromPayments.Value, dTimePickerToPayments.Value, false), this);
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
                CommonFunctions.ShowDialog(new OrderInvQuotSearchForm(ListFindInFields, PerformSearch, null), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnSearchPayment_Click()", ex);
            }
        }

        public void PerformSearch(SearchDetails ObjSearchDtls)
        {
            try
            {
                //      DataTable tblFiltered = dtAllPayments.AsEnumerable()
                //.Where(row => row.Field<String>("Nachname") == username
                //         && row.Field<String>("Ort") == location)
                //.OrderByDescending(row => row.Field<String>("Nachname"))
                //.CopyToDataTable();
                //DataTable tblFiltered=  dtAllPayments.Select("WinCom like '%A%'").CopyToDataTable();
                //return tblFiltered;
                AssignFilterDataTableToGrid(ObjSearchDtls);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.PerformSearch()", ex);
                //return null;
            }
        }
        public string GetModifiedStringBasedOnMatchPatterns(string SearchStr, MatchPatterns MatchPttrn)
        {
            try
            {
                switch (MatchPttrn)
                {
                    case MatchPatterns.StartsWith:
                        return SearchStr + "%";
                    case MatchPatterns.EndsWith:
                        return "%" + SearchStr;
                    case MatchPatterns.Contains:
                        return "%" + SearchStr + "%";
                    case MatchPatterns.Whole:
                        return SearchStr;
                }
                return SearchStr + "%";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetModifiedStringBasedOnMatchPatterns()", ex);
                return SearchStr + "%";
            }
        }
        public void AssignFilterDataTableToGrid(SearchDetails ObjSearchDtls)
        {
            try
            {

                string ModifiedStr = GetModifiedStringBasedOnMatchPatterns(ObjSearchDtls.SearchString, ObjSearchDtls.MatchPattern);

                var rows = dtAllPayments.Select(CommonFunctions.DictFilterNamesWithActualDBColNames[ObjSearchDtls.SearchIn] + " like '" + ModifiedStr + "'");
                if (rows.Any())
                {
                    dtGridViewPayments.DataSource = rows.CopyToDataTable();
                }
                else
                {
                    dtGridViewPayments.Rows.Clear();

                    dtGridViewPayments.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.FilterDataTable()", ex);

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
                    MessageBox.Show(this, "Please select an Payment to Delete", "Delete Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Int32 PaymentID = Int32.Parse(dtGridViewPayments.SelectedRows[0].Cells["PaymentID"].Value.ToString());
                var Result = MessageBox.Show("Are sure to set the payemnt to Inactive State? ", "InActive Payment", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //If the no button was pressed ...
                if (Result == DialogResult.No) return;
                if (ObjPaymentsModel.DeletePaymentDetails(PaymentID) == 0)
                {
                    dtGridViewPayments.DataSource = null;
                    UpdatePaymentsOnClose(1);
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
