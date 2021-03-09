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
    public partial class PaymentsForm : Form
    {
        PaymentsModel ObjPaymentsModel;
        AccountsMasterModel ObjAccountsMasterModel;
        DataTable dtAllPayments;
        DataTable dtPaymentSummary;
        MySQLHelper ObjMySQLHelper;
        InvoicesModel ObjInvoicesModel ;
        List<string> ListPaymentModeNames;
        List<Type> ListPaymentModeColTypes;
        List<int> ListEditedInvoiceIDs = new List<int>();
        Boolean ValueChanged = false;
        public PaymentsForm()
        {
            try
            {
                InitializeComponent();
                ObjMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                CommonFunctions.SetDataGridViewProperties(dtGridViewPayments);
                CommonFunctions.SetDataGridViewProperties(dgvPaymentSummary);

                ObjPaymentsModel = new PaymentsModel();
                ObjPaymentsModel.LoadPaymentModes();
                ListPaymentModeNames = ObjPaymentsModel.GetAllPaymentsModeNames();
                ListPaymentModeColTypes = new List<Type>();
                for (int i = 0; i < ListPaymentModeNames.Count; i++)
                {
                    ListPaymentModeColTypes.Add(CommonFunctions.TypeDouble);
                }
                ObjInvoicesModel = new InvoicesModel();
                ObjInvoicesModel.Initialize();

                ObjAccountsMasterModel = CommonFunctions.ObjAccountsMasterModel;

                //dTimePickerFromPayments.Value = DateTime.Today.AddDays(-30);

                dTimePickerFromPayments.Value = DateTime.Today;
                dTimePickerToPayments.Value = DateTime.Today;

                LoadPaymentsGridView(dTimePickerFromPayments.Value, dTimePickerToPayments.Value);
              
                cmbxStaffName.DataSource = CommonFunctions.ObjUserMasterModel.GetAllUsers();
                cmbxStaffName.SelectedItem = CommonFunctions.CurrentUserName;
               

                LoadInputPaymentSummaryGridView();
                List<string> ListTempNames = new List<string>() { "All" };
                ListTempNames.AddRange(CommonFunctions.ObjCustomerMasterModel.GetAllLineNames());
                cmbxDeliveryLine.DataSource = ListTempNames;
                cmbxDeliveryLine.SelectedItem = "All";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("PaymentsMainForm.ctor()", ex);
            }
        }

        private void LoadPaymentsGridView(DateTime FromDate,DateTime ToDate)
        {
            try
            {
                dtAllPayments = new DataTable();
                dtAllPayments = ObjPaymentsModel.GetPaytmentsDataTable(FromDate, ToDate);
                dtGridViewPayments.DataSource = null;
                if (dtGridViewPayments.Columns.Count > 0) dtGridViewPayments.Columns.Clear();
                if (dtAllPayments.Rows.Count == 0)
                {
                    var dataTable = new DataTable();
                    dataTable.Columns.Add("Message", typeof(string));
                    dataTable.Rows.Add("No Payments Added/found in DB");
                    if (dtGridViewPayments.Columns.Count > 0) dtGridViewPayments.Columns.Clear();
                    dtGridViewPayments.DataSource = new BindingSource { DataSource = dataTable };
                    dtGridViewPayments.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                else
                {
                    List<PaymentDetails> ListPaymentDtlsCache = ObjPaymentsModel.GetPaymentDtlsCache();
                    if (ListPaymentDtlsCache.Count > 0)
                    {
                        //PaymentID, PaymentDate, InvoiceID, QuotationID, AccountID, PaymentModeID, PaymentAmount, Description, CreationDate, LastUpdateDate, UserID
                        String[] ArrColumnNames = new String[] { "PAYMENTID", "INVOICEID", "QUOTATIONID", "ACCOUNTID", "PAYMENTMODEID", "INVOICENUMBER", "CUSTOMERNAME", "PHONEN0","PAYMENTDATE", "PAYMENTMODE", "PAYMENTAMOUNT", "DESCRIPTION", "CREATIONDATE", "LASTUPDATEDATE", "STAFFNAME", "ACTIVE" };
                        String[] ArrColumnHeaders = new String[] { "Payment ID", "Invoice ID", "Quotation  ID", "AccountID", "PaymentMode ID", "InvoiceNumber", "Customer Name","PhoneNo", "Payment Date", "Payment Mode", "Amount", "Description", "CreationDate", "LastUpdateDate", "Staff Name", "Active" };
                        for (int i = 0; i < ArrColumnNames.Length; i++)
                        {
                            dtGridViewPayments.Columns.Add(ArrColumnNames[i], ArrColumnHeaders[i]);
                            DataGridViewColumn CurrentCol = dtGridViewPayments.Columns[dtGridViewPayments.Columns.Count - 1];
                            CurrentCol.ReadOnly = true;
                            if (i <= 4) CurrentCol.Visible = false;    //CustomerID, LineID", "Discount Group ID, Price Group ID,State ID
                        }
                        foreach (PaymentDetails ObjPaymentDetails in ListPaymentDtlsCache)
                        {
                            Object[] ArrRowItems = new Object[16];
                            ArrRowItems[0] = ObjPaymentDetails.PaymentId;
                            ArrRowItems[1] = ObjPaymentDetails.InvoiceID;
                            ArrRowItems[2] = ObjPaymentDetails.QuotationID;
                            ArrRowItems[3] = ObjPaymentDetails.AccountID;
                            ArrRowItems[4] = ObjPaymentDetails.PaymentModeID;
                            ArrRowItems[5] = ObjPaymentDetails.InvoiceNumber;
                            ArrRowItems[6] = ObjPaymentDetails.CustomerName;
                            ArrRowItems[7] = ObjPaymentDetails.CustPhoneNo;
                            ArrRowItems[8] = ObjPaymentDetails.PaidOn;

                            ArrRowItems[9] = ObjPaymentDetails.PaymentMode;
                            ArrRowItems[10] = ObjPaymentDetails.Amount;
                            ArrRowItems[11] = ObjPaymentDetails.Description;
                            ArrRowItems[12] = ObjPaymentDetails.PaidOn;
                            ArrRowItems[13] = ObjPaymentDetails.LastUpdateDate;
                            ArrRowItems[14] = ObjPaymentDetails.StaffName;
                            ArrRowItems[15] = ObjPaymentDetails.Active;
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


        DataTable GetPaymentsSummaryDataTable()
        {
            try
            {
                DataTable dtPaymentSummary = new DataTable();
                List<string> ListColumns = new List<string> { "Sl#", "InvoiceID","Line", "Invoice#", "Customer Name", "Sale", "Cancel", "Return", "Discount", "Total Tax", "Net Sale", "OB" };// Cash, UPI, Credit Card, Debit Card, Check};
                ListColumns.AddRange(ListPaymentModeNames);
                List<Type> ListColumnsType = new List<Type> { CommonFunctions.TypeInt32, CommonFunctions.TypeString, CommonFunctions.TypeString,
                                                    CommonFunctions.TypeString,CommonFunctions.TypeString, CommonFunctions.TypeDouble, CommonFunctions.TypeDouble,
                                                    CommonFunctions.TypeDouble, CommonFunctions.TypeDouble, CommonFunctions.TypeDouble,
                                                    CommonFunctions.TypeDouble, CommonFunctions.TypeDouble
                                                   };
                ListColumnsType.AddRange(ListPaymentModeColTypes);

                for (int i = 0; i < ListColumns.Count; i++)
                {
                    dtPaymentSummary.Columns.Add(new DataColumn(ListColumns[i], ListColumnsType[i]));
                }
                DataTable tmpdt = ObjPaymentsModel.GetPaymentSummaryTable();

                for (int i = 0; i < tmpdt.Rows.Count; i++)
                {
                    DataRow dr = tmpdt.Rows[i];
                    Int32 col = 0;
                    Object[] ArrRowItems = new Object[ListColumns.Count];
                    ArrRowItems[col++] = (i + 1);
                    ArrRowItems[col++] = int.Parse(dr["INVOICEID"].ToString());
                    ArrRowItems[col++] = dr["LINENAME"].ToString();
                    ArrRowItems[col++] = dr["INVOICE#"].ToString();
                    ArrRowItems[col++] = dr["CUSTOMERNAME"].ToString();
                    ArrRowItems[col++] = dr["SALE"].ToString();
                    ArrRowItems[col++] = "0";  //cancel
                    ArrRowItems[col++] = "0";   //return
                    ArrRowItems[col++] = dr["DISCOUNT"].ToString();
                    ArrRowItems[col++] = "0";  //total tax
                    ArrRowItems[col++] = dr["NET SALE"].ToString();
                    ArrRowItems[col++] = dr["OB"].ToString();
                    for (int mn = 0; mn < ListPaymentModeNames.Count; mn++)
                    {
                        ArrRowItems[col++] = "0";
                    }
                    dtPaymentSummary.Rows.Add(ArrRowItems);
                }
                return dtPaymentSummary;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetPaymentsSummaryDataTable()", ex);
                return null;
            }
        }
        private void LoadInputPaymentSummaryGridView(string DataFilter="")
        {
            try
            {
                //Sl#	Line	Invoice#	Customer Name	Sale	Cancel	Return	Discount	Total Tax	Net Sale	OB	Cash
         
                dtPaymentSummary = GetPaymentsSummaryDataTable();

                if (ListEditedInvoiceIDs.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show(this, "You have made some changes to Payment Summary.\nChanges made will be lost, if you continue.\nPlease click on \"Update Payments\" to save your changes.\nDo you wish to continue?", "Payments", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dialogResult == DialogResult.No) return;
                    ListEditedInvoiceIDs.Clear();
                }

                List<Int32> ListSelectedIDs = new List<Int32>();
                foreach (DataGridViewRow item in dgvPaymentSummary.SelectedRows)
                {
                    ListSelectedIDs.Add(Int32.Parse(item.Cells["INVOICEID"].Value.ToString()));
                }

                if (DataFilter != null) dtPaymentSummary.DefaultView.RowFilter = DataFilter;

                dgvPaymentSummary.DataSource = dtPaymentSummary.DefaultView;
           

                foreach (DataGridViewColumn item in dgvPaymentSummary.Columns)
                {
                    item.ReadOnly = true;
                    if (item.HeaderText.Equals("INVOICEID")) item.Visible = false;
                    if (item.Name.Equals("Cancel") || item.Name.Equals("Return")
                        || ListPaymentModeNames.Contains(item.Name))
                    {
                        item.ReadOnly = false;
                    }
                }

                if (ListSelectedIDs.Count > 0)
                {
                    foreach (DataGridViewRow item in dgvPaymentSummary.Rows)
                    {
                        if (ListSelectedIDs.Contains(Int32.Parse(item.Cells["INVOICEID"].Value.ToString())))
                        {
                            item.Selected = true;
                        }
                    }
                }              
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadInputPaymentSummaryGridView()", ex);
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

                int PaymentId = ObjMySQLHelper.GetLatestColValFromTable("PAYMENTID", "PAYMENTS");
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
                        LoadPaymentsGridView(dTimePickerToPayments.Value, dTimePickerToPayments.Value);
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


        private void btnViewEditPayment_Click(object sender, EventArgs e)
        {
            try
            {
                //if ()
                //{
                    //DataGridViewRow row = this.dtGridViewPayments.SelectedRows[0];
                //string val = dtGridViewPayments.CurrentRow.Cells["PAYMENTID"].Value.ToString();
                    //row.Cells["PAYMENTID"].Value
                    // }
                if(dtGridViewPayments.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please Select a Payment Row To Edit!", "Error");
                    return;
                 }
                CommonFunctions.ShowDialog(new CreatePaymentForm(UpdatePaymentsOnClose, dTimePickerFromPayments.Value, dTimePickerToPayments.Value, false, this.dtGridViewPayments.SelectedRows[0]), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnViewEditPayment_Click()", ex);
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
                    "Customer Phone",
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
                checkBoxApplyFilterPayment.Checked = false;
                LoadPaymentsGridView(DateTime.MinValue, DateTime.MinValue);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnReloadPayments_Click()", ex);
            }
        }

    
        private void checkBoxApplyFilterPayments_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!checkBoxApplyFilterPayment.Checked)
                {
                    dTimePickerFromPayments.Value = DateTime.Today;
                    //dTimePickerToPayments.Value = dTimePickerFromPayments.Value.AddDays(30);
                    dTimePickerToPayments.Value = dTimePickerFromPayments.Value;
                }
                LoadPaymentsGridView(dTimePickerFromPayments.Value, dTimePickerToPayments.Value);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.checkBoxApplyFilterPayments_CheckedChanged()", ex);
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

        private void cmbxDeliveryLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbxStaffName.SelectedIndex < 0) return;

                if (cmbxDeliveryLine.SelectedItem.ToString() == "All")
                    LoadInputPaymentSummaryGridView("");
                else
                    LoadInputPaymentSummaryGridView($"LINE = '{cmbxDeliveryLine.SelectedItem.ToString()}'");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxCategoryFilterList_SelectedIndexChanged()", ex);
            }
        }

        private void dgvPaymentSummary_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 6 || (e.ColumnIndex > 7 && e.ColumnIndex <= 11) || e.RowIndex < 0) return;

                Double result;
                DataGridViewCell cell = dgvPaymentSummary.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ErrorText = null;
                if (!Double.TryParse(cell.Value.ToString(), out result))
                {
                    cell.ErrorText = "Must be a number";
                    return;
                }

                ValueChanged = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dgvPaymentSummary_CellValueChanged()", ex);
            }
        }

        private void dgvPaymentSummary_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 6 ||( e.ColumnIndex > 7 && e.ColumnIndex <= 11) || e.RowIndex < 0 || !ValueChanged) return;

                Int32 InvoiceID = Int32.Parse(dgvPaymentSummary["INVOICEID", e.RowIndex].Value.ToString());
                if (!ListEditedInvoiceIDs.Contains(InvoiceID)) ListEditedInvoiceIDs.Add(InvoiceID);

                ValueChanged = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dgvPaymentSummary_CellEndEdit()", ex);
            }
        }

        private void dgvPaymentSummary_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                if (e.Exception != null)
                {
                    MessageBox.Show(this, "Invalid value!Pls enter integer/double value", "Payment update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dgvPaymentSummary_DataError()", ex);
            }
        }

        private void btnAddToDB_Click(object sender, EventArgs e)
        {
            try
            {
                
                for (int i = 0; i < ListEditedInvoiceIDs.Count; i++)
                {
                  
                    for (int j = 0; j < dgvPaymentSummary.Rows.Count; j++)
                    {
                        if (dgvPaymentSummary["INVOICEID", j].Value.ToString().Equals(ListEditedInvoiceIDs[i].ToString()))
                        {
                            PaymentDetails tmpPaymentDetails = new PaymentDetails();
                            tmpPaymentDetails.CustomerID = CommonFunctions.ObjCustomerMasterModel.GetCustomerDetails(dgvPaymentSummary["CUSTOMER NAME", j].Value.ToString()).CustomerID;
                            tmpPaymentDetails.AccountID = CommonFunctions.ObjAccountsMasterModel.GetAccDtlsFromCustID(tmpPaymentDetails.CustomerID).AccountID;
                            tmpPaymentDetails.InvoiceID = ListEditedInvoiceIDs[i];
                            tmpPaymentDetails.InvoiceNumber = dgvPaymentSummary["INVOICE#", j].Value.ToString();
                            tmpPaymentDetails.PaidOn = DateTime.Parse(MySQLHelper.GetDateTimeStringForDB(DateTime.Now));
                            tmpPaymentDetails.Amount = 0;
                            tmpPaymentDetails.Active = true;
                            tmpPaymentDetails.CreationDate = DateTime.Now;
                            tmpPaymentDetails.LastUpdateDate = DateTime.Now;
                            tmpPaymentDetails.UserID = CommonFunctions.ObjUserMasterModel.GetUserID(cmbxStaffName.SelectedItem.ToString());

                            CustomerAccountHistoryDetails tmpCustomerAccountHistoryDetails = new CustomerAccountHistoryDetails();
                            tmpCustomerAccountHistoryDetails.AccountID = tmpPaymentDetails.AccountID;
                            tmpCustomerAccountHistoryDetails.SaleAmount = Double.Parse(dgvPaymentSummary["SALE", j].Value.ToString());
                            tmpCustomerAccountHistoryDetails.DiscountAmount = Double.Parse(dgvPaymentSummary["DISCOUNT", j].Value.ToString());
                            tmpCustomerAccountHistoryDetails.CancelAmount = Double.Parse(dgvPaymentSummary["Cancel", j].Value.ToString());
                            tmpCustomerAccountHistoryDetails.RefundAmount = Double.Parse(dgvPaymentSummary["Return", j].Value.ToString());
                            


                            tmpCustomerAccountHistoryDetails.NetSaleAmount = Double.Parse(dgvPaymentSummary["NET SALE", j].Value.ToString());
                            tmpCustomerAccountHistoryDetails.TotalTaxAmount = 0.0;

                            bool Found = false;

                            for (int mk = 0; mk < ListPaymentModeNames.Count; mk++)
                            {

                                string AmountReceived = dgvPaymentSummary[ListPaymentModeNames[mk], j].Value.ToString().Trim();
                                string Balance = (dgvPaymentSummary["OB", j].Value.ToString() == string.Empty) ? "0.0" : dgvPaymentSummary["OB", j].Value.ToString();
                                tmpCustomerAccountHistoryDetails.AmountReceived = (AmountReceived == string.Empty) ? 0.0 : Double.Parse(AmountReceived);
                                if (tmpCustomerAccountHistoryDetails.AmountReceived != 0.0)
                                {
                                    tmpPaymentDetails.PaymentModeID = ObjPaymentsModel.GetPaymentModeDetails(ListPaymentModeNames[mk]).PaymentModeID;
                                    tmpCustomerAccountHistoryDetails.BalanceAmount = Math.Abs(Double.Parse(AmountReceived) - Double.Parse(Balance));
                                    dgvPaymentSummary["OB", j].Value = tmpCustomerAccountHistoryDetails.BalanceAmount.ToString();
                                    if (Found)
                                    {
                                        tmpCustomerAccountHistoryDetails.AccountID = tmpPaymentDetails.AccountID;
                                        tmpCustomerAccountHistoryDetails.SaleAmount = 0;
                                        tmpCustomerAccountHistoryDetails.DiscountAmount = 0;
                                        tmpCustomerAccountHistoryDetails.CancelAmount = 0;
                                        tmpCustomerAccountHistoryDetails.RefundAmount = 0;
                                        tmpCustomerAccountHistoryDetails.NetSaleAmount = 0;
                                        tmpCustomerAccountHistoryDetails.TotalTaxAmount = 0.0;
                                    }
                                    ObjPaymentsModel.CreateNewPaymentDetails(ref tmpPaymentDetails, ref tmpCustomerAccountHistoryDetails);
                                    Found = true;
                                }
                            }
                            break;
                        }
                    }
                }

                ListEditedInvoiceIDs.Clear();
                LoadInputPaymentSummaryGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnAddToDB_Click()", ex);
            }
        }
    }
}
