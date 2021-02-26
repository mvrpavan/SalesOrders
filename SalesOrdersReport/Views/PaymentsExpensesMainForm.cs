using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Models;
using Excel = Microsoft.Office.Interop.Excel;

namespace SalesOrdersReport.Views
{
    public partial class PaymentsExpensesMainForm : Form
    {
        PaymentsModel ObjPaymentsModel;
        AccountsMasterModel ObjAccountsMasterModel;
        DataTable dtAllPayments;
        MySQLHelper ObjMySQLHelper;
        InvoicesModel ObjInvoicesModel ;

        public PaymentsExpensesMainForm()
        {
            try
            {
                InitializeComponent();
                ObjMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                CommonFunctions.SetDataGridViewProperties(dtGridViewPayments);
                CommonFunctions.SetDataGridViewProperties(dtGridViewExpenses);

                ObjPaymentsModel = new PaymentsModel();
                ObjPaymentsModel.LoadPaymentModes();

                ObjInvoicesModel = new InvoicesModel();
                ObjInvoicesModel.Initialize();

                ObjAccountsMasterModel = new AccountsMasterModel();
                ObjAccountsMasterModel.LoadAccountDetails();

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

        private Int32 ImportPaymentsData(String ExcelFilePath, Object ObjDetails, ReportProgressDel ReportProgress)
        {
            try
            {
                Excel.Application xlApp = new Excel.Application();
                DataTable dtCustomerSummary = CommonFunctions.ReturnDataTableFromExcelWorksheet("Customer Summary", ExcelFilePath, "*", "A2:L100000");
                if (dtCustomerSummary == null)
                {
                    MessageBox.Show(this, "Provided Customer Summary file doesn't contain \"Customer Summary\" Sheet.\nPlease provide correct file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   
                    return -1;
                }
                

                dtCustomerSummary.DefaultView.RowFilter = "IsNull([Sl#], 0) > 0 AND Convert(IsNull([Cash], 0),'System.Double')>0 ";
                DataRow[] drSellers = dtCustomerSummary.DefaultView.ToTable().Select("", "[Sl#] asc");

                Excel.Workbook xlSellerSummaryWorkbook = xlApp.Workbooks.Open(ExcelFilePath);
                Excel.Worksheet xlSellerSummaryWorksheet = CommonFunctions.GetWorksheet(xlSellerSummaryWorkbook, "Customer Summary");
                DateTime SummaryCreationDate = DateTime.Parse(xlSellerSummaryWorksheet.Cells[1, 2].Value.ToString());
                xlSellerSummaryWorkbook.Close(false);

             

                PaymentModeDetails ObjPayModeDtls = ObjPaymentsModel.GetPaymentModeDetails("Cash");
                string WhereCondition = " and InvoiceStatus = '" + INVOICESTATUS.Created + "' or InvoiceStatus = '" + INVOICESTATUS.Delivered + "'";
                List<string> ListNotAdded = new List<string>();
                for (int i = 0; i < drSellers.Length; i++)
                {
                    DataRow dr = drSellers[i];

                    string CustomerName = dr["Customer Name"].ToString();
                    string InvoiceNumb = dr["Invoice#"].ToString();
                    CustomerDetails ObjCustomerDtls = CommonFunctions.ObjCustomerMasterModel.GetCustomerDetails(CustomerName);
                    if (ObjCustomerDtls == null)
                    {
                        ListNotAdded.Add(CustomerName + " :: CustomerID Doesnt Exists in DB");
                        continue;
                    }
                
                    AccountDetails ObjAccountDetails = ObjAccountsMasterModel.GetAccDtlsFromCustID(ObjCustomerDtls.CustomerID);
                    if (ObjAccountDetails == null)
                    {
                        ListNotAdded.Add(CustomerName + " :: Account details not available for the cutomer");
                        continue;
                    }

                    int InvoiceID = ObjInvoicesModel.GetInvoiceIDFromNum(InvoiceNumb, WhereCondition);
                    if (InvoiceID < 0)
                    {
                        ListNotAdded.Add(CustomerName + ":: InvoiceID not available for the invoice Number: "+ InvoiceNumb);
                        continue;
                    }

                    //Insert Payment Table
                    CreatePaymentTable(dr, ObjAccountDetails.AccountID,InvoiceID,ObjPayModeDtls.PaymentModeID,SummaryCreationDate);

                }
                MessageBox.Show(this, "Was not able add following customers to table : \n" + string.Join("\n",ListNotAdded), "Update Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ImportPaymentsData()", ex);
                return -1;
            }
        }
        void FillCustAccHistoryTble(DataRow dr,double AccountID)
        {
            try
            {

                int PaymentId = CommonFunctions.ObjCustomerMasterModel.GetLatestColValFromTable("PAYMENTID", "PAYMENTS");
                List<string> ListColumnValues = new List<string>(), ListTempColValues = new List<string>();
                List<string> ListColumnNames = new List<string>(), ListTempColNames = new List<string>();
                List<Types> ListTypes = new List<Types>();
                ListColumnValues.Add(AccountID.ToString());
                ListColumnNames.Add("ACCOUNTID");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(PaymentId.ToString());
                ListColumnNames.Add("PAYMENTID");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(dr["Sale"].ToString()==string.Empty?"0": dr["Sale"].ToString());
                ListColumnNames.Add("SALEAMOUNT");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(dr["Cancel"].ToString() == string.Empty ? "0" : dr["Cancel"].ToString());
                ListColumnNames.Add("CANCELAMOUNT");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(dr["Return"].ToString() == string.Empty ? "0" : dr["Return"].ToString());
                ListColumnNames.Add("RETURNAMOUNT");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(dr["Discount"].ToString() == string.Empty ? "0" : dr["Discount"].ToString());
                ListColumnNames.Add("DISCOUNTAMOUNT");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(dr["Total Tax"].ToString() == string.Empty ? "0" : dr["Total Tax"].ToString());
                ListColumnNames.Add("TOTALTAX");
                ListTypes.Add(Types.Number);

                double NwBalanceAmnt = (double.Parse(dr["Net Sale"].ToString() == string.Empty ? "0" : dr["Net Sale"].ToString()))
                                       + double.Parse(dr["OB"].ToString() == string.Empty ? "0" : dr["OB"].ToString())
                                       - double.Parse(dr["Cash"].ToString());

                ListColumnValues.Add(NwBalanceAmnt.ToString());
                ListColumnNames.Add("NETSALEAMOUNT");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(dr["Cash"].ToString() == string.Empty ? "0" : dr["Cash"].ToString());
                ListColumnNames.Add("AMOUNTRECEIVED");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(NwBalanceAmnt.ToString()); 
                ListColumnNames.Add("NEWBALANCEAMOUNT");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(dr["OB"].ToString() == string.Empty ? "0" : dr["OB"].ToString());
                ListColumnNames.Add("BALANCEAMOUNT");
                ListTypes.Add(Types.Number);

                int ResultVal = ObjMySQLHelper.InsertIntoTable("CUSTOMERACCOUNTHISTORY", ListColumnNames, ListColumnValues, ListTypes);

                if (ResultVal <= 0) MessageBox.Show("Wasnt able to add data to CustomerAccHistory", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (ResultVal == 2)
                {
                    MessageBox.Show("err.", "Error");
                }
                else
                {
                    //MessageBox.Show("Updated CustomerAccountHistory table", "Updated CustomerAccountHistory");
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.FillCustAccHistoryTble()", ex);
            }
        }
        public void CreatePaymentTable(DataRow dr,double AccountID, int InvoiceID,int PaymentMode,DateTime SummaryCreationDate)
        {
            try
            {
                List<string> ListColumnValues = new List<string>(), ListTempColValues = new List<string>();
                List<string> ListColumnNames = new List<string>(), ListTempColNames = new List<string>();
                List<Types> ListTypes = new List<Types>();



                ListColumnValues.Add(AccountID.ToString());
                ListColumnNames.Add("ACCOUNTID");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(PaymentMode.ToString());
                ListColumnNames.Add("PAYMENTMODEID");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(InvoiceID.ToString());
                ListColumnNames.Add("INVOICEID");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add("1");
                ListColumnNames.Add("ACTIVE");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(SummaryCreationDate.ToString("yyyy-MM-dd H:mm:ss"));
                ListColumnNames.Add("PAYMENTDATE");
                ListTypes.Add(Types.String);

                ListColumnValues.Add(dr["Cash"].ToString());
                ListColumnNames.Add("PAYMENTAMOUNT");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(CommonFunctions.ObjUserMasterModel.GetUserID(CommonFunctions.CurrentUserName).ToString());
                ListColumnNames.Add("USERID");
                ListTypes.Add(Types.Number);

                string Now = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                ListColumnValues.Add(Now);
                ListColumnNames.Add("LASTUPDATEDATE");
                ListTypes.Add(Types.String);

                ListColumnValues.Add(SummaryCreationDate.ToString("yyyy-MM-dd H:mm:ss"));
                ListColumnNames.Add("CREATIONDATE");
                ListTypes.Add(Types.String);
              
                ListTempColValues.Add(dr["OB"].ToString() == string.Empty ? "0" : dr["OB"].ToString());
                ListTempColNames.Add("BALANCEAMOUNT");

                ListTempColValues.Add(Now);
                ListTempColNames.Add("LASTUPDATEDDATE");

                int ResultVal = ObjMySQLHelper.InsertIntoTable("PAYMENTS", ListColumnNames, ListColumnValues, ListTypes);
                ObjInvoicesModel.MarkInvoicesAsPaid(new List<int>() { InvoiceID });
                if (ResultVal <= 0) MessageBox.Show("Wasnt able to create the payment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (ResultVal == 2)
                {
                    MessageBox.Show("err", "Error");
                }
                else
                {
                    //MessageBox.Show("Added New Payment for :: " + dr["Customer Name"].ToString() + " successfully", "Added New Payment");
                    FillCustAccHistoryTble(dr,AccountID);
                    string WhereCondition = "ACCOUNTID = '" + AccountID.ToString() + "'";

                    ResultVal = CommonFunctions.ObjUserMasterModel.UpdateAnyTableDetails("ACCOUNTSMASTER", ListTempColNames, ListTempColValues, WhereCondition);

                    if (ResultVal < 0) MessageBox.Show("Wasnt able to Update the Account Master details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        //MessageBox.Show("Updated Account Master Details :: " + ObjAccountDetails.AccountID.ToString() + " successfully", "Update Account Details");
                    }
                }

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreatePaymentTable()", ex);
            }
        }
        Int32 UpdatePaymentsTable()
        {
            try
            {
                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdatePaymentsTable()", ex);
                return -1;
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

        void UpdatePaymentsOnClose(Int32 Mode)
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

        private void btnImportFromExcel_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new ImportFromExcelForm(ImportDataTypes.Payments, UpdatePaymentsOnClose, ImportPaymentsData), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnImportFromExcel_Click()", ex);
            }
        }
    }
}
