using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesOrdersReport.Models;
using SalesOrdersReport.CommonModules;

namespace SalesOrdersReport.Views
{
    public partial class CreatePaymentForm : Form
    {
        UpdateOnCloseDel UpdatePaymentsOnClose = null;
        PaymentsModel ObjPaymentsModel;
        InvoicesModel ObjInvoicesModel;
        AccountsMasterModel ObjAccountsMasterModel;
        CustomerAccountHistoryModel ObjAccountHistoryModel;
        CustomerMasterModel ObjCustomerMasterModel = CommonFunctions.ObjCustomerMasterModel;
        UserMasterModel ObjUserMasterModel = CommonFunctions.ObjUserMasterModel;
        List<string> ListOfAllCustomers = new List<string>() { "Select Customer" };
        List<string> ListOfAllUsers = new List<string>() { "Select Staff" };
        List<InvoiceDetails> ListInvoiceDtls = new List<InvoiceDetails>();
        CustomerDetails ObjCustomerDetails = null;
        AccountDetails ObjAccountDetails = null;
        int CurrInvoiceCacheIndex = -1;
        MySQLHelper ObjMySQLHelper;
        DateTime FromDate, Todate;
        PaymentDetails ObjPaymentDeatilsToBeEdited;

        public CreatePaymentForm(UpdateOnCloseDel UpdatePaymentOnClose, DateTime FromDate, DateTime Todate, bool CreateForm = true,DataGridViewRow SelectedDataRow=null)
        {
            try
            {
                InitializeComponent();
                ObjMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                string FormTitle = "";
                if (CreateForm)
                {
                    FormTitle = "Create Payment";
                    btnCreateUpdatePayment.Text = "Create Payment";
                    chckActive.Visible = false;
                }
                else
                {
                    FormTitle = "Update Payment";
                    btnCreateUpdatePayment.Text = "Update Payment";
                    chckActive.Visible = true;
                    ObjPaymentDeatilsToBeEdited = new PaymentDetails();
                    ExtractPaymentDetailsFromRow(SelectedDataRow);

                }
                this.Text = FormTitle;
                this.UpdatePaymentsOnClose = UpdatePaymentOnClose;
                this.FromDate = FromDate;
                this.Todate = Todate;
                txtbxCreatePaymentAmount.Text = "0";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreatePaymentForm()", ex);
            }
        }

        void ExtractPaymentDetailsFromRow(DataGridViewRow SelectedRow)
        {
            try
            {
                ObjPaymentDeatilsToBeEdited.PaymentId = int.Parse(SelectedRow.Cells["PAYMENTID"].Value.ToString());
                ObjPaymentDeatilsToBeEdited.Description = SelectedRow.Cells["DESCRIPTION"].Value.ToString();
                ObjPaymentDeatilsToBeEdited.Active = bool.Parse(SelectedRow.Cells["ACTIVE"].Value.ToString());
                ObjPaymentDeatilsToBeEdited.StaffName = SelectedRow.Cells["STAFFNAME"].Value.ToString();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ExtractPaymentDetailsFromRow()", ex);
            }
        }
        private void CreatePaymentForm_Load(object sender, EventArgs e)
        {
            try
            {
                ObjPaymentsModel = new PaymentsModel();
                ObjPaymentsModel.LoadPaymentModes();
                ObjPaymentsModel.GetPaytmentsDataTable(FromDate, Todate);
                cmbBoxPaymentModes.Items.Clear();
                cmbBoxPaymentModes.Items.AddRange(ObjPaymentsModel.GetPaymentModesList().ToArray());
                cmbBoxPaymentModes.DropDownStyle = ComboBoxStyle.DropDown;
                cmbBoxPaymentModes.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbBoxPaymentModes.AutoCompleteSource = AutoCompleteSource.ListItems;
                ObjInvoicesModel = new InvoicesModel();
                ObjInvoicesModel.Initialize();
                ObjAccountsMasterModel = CommonFunctions.ObjAccountsMasterModel;
                ObjAccountHistoryModel = new CustomerAccountHistoryModel();
                ObjAccountHistoryModel.LoadAccountHistoryModel();
                ListOfAllCustomers.AddRange(ObjCustomerMasterModel.GetCustomerList());
                cmbxCreatePaymentCustomerNames.DataSource = ListOfAllCustomers;
                ListOfAllUsers.AddRange(ObjUserMasterModel.GetAllUsers());
                cmbxcreatePaymentStaffName.DataSource = ListOfAllUsers;
                cmbxCreatePaymentCustomerNames.SelectedIndex = 0;
                cmbxcreatePaymentStaffName.SelectedIndex = 0;
                if (this.Text == "Update Payment")
                {
                    cmbxCreatePaymentNumber.SelectedItem = ObjPaymentDeatilsToBeEdited.StaffName;
                    txtCreatePaymentDesc.Text = ObjPaymentDeatilsToBeEdited.Description;
                }
                else cmbxCreatePaymentNumber.SelectedItem = 0;
                cmbxCreatePaymentPaymentAgainst.SelectedIndex = 0;
                txtbxCreatePaymentAmount.Text = "0";
                cmbBoxPaymentModes.SelectedIndex = 0;
                lblValidateErrMsg.Visible = false;

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreatePaymentForm_Load()", ex);
            }
        }

        private void btnAddUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                if (cmbxCreatePaymentCustomerNames.SelectedIndex == 0)
                {
                    lblValidateErrMsg.Visible = true;
                    lblValidateErrMsg.Text = "Pls Select a Customer!";
                    return;
                }
                if (cmbxCreatePaymentNumber.SelectedIndex == 0 || cmbxCreatePaymentNumber.SelectedIndex < 0)
                {
                    lblValidateErrMsg.Visible = true;
                    lblValidateErrMsg.Text = "Pls Select Number!";
                    return;
                }
                if (cmbxcreatePaymentStaffName.SelectedIndex == 0)
                {
                    lblValidateErrMsg.Visible = true;
                    lblValidateErrMsg.Text = "Pls Select a Staff!";
                    return;
                }
                if (txtbxCreatePaymentAmount.Text.Trim() == string.Empty)
                {
                    lblValidateErrMsg.Visible = true;
                    lblValidateErrMsg.Text = "Pls Enter Amount!";
                    return;
                }
                if (lblValidateErrMsg.Visible == true)
                {
                    lblValidateErrMsg.Visible = false;
                }
                if (this.Text == "Create Payment") CreatePaymentTable();
                else EditPaymentTable();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnAddUpdate_Click()", ex);
            }

        }

        public void CreatePaymentTable()
        {
            try
            {
                PaymentDetails tmpPaymentDetails = new PaymentDetails()
                {
                    CustomerID = ObjAccountDetails.CustomerID,
                    AccountID = ObjAccountDetails.AccountID,
                    Description = txtCreatePaymentDesc.Text.Trim(),
                    InvoiceID = ListInvoiceDtls[CurrInvoiceCacheIndex].InvoiceID,
                    InvoiceNumber = ListInvoiceDtls[CurrInvoiceCacheIndex].InvoiceNumber,
                    PaidOn = DateTime.Parse(dtpCreatePaymentPaidOn.Text.Trim()),
                    Amount = Double.Parse(txtbxCreatePaymentAmount.Text),
                    UserID = CommonFunctions.ObjUserMasterModel.GetUserID(MySQLHelper.GetMySqlHelperObj().CurrentUser),
                    PaymentModeID = ObjPaymentsModel.GetPaymentModeDetails(cmbBoxPaymentModes.SelectedItem.ToString()).PaymentModeID,
                    Active = true,
                    CreationDate = DateTime.Now,
                    LastUpdateDate = DateTime.Now

                };

                CustomerAccountHistoryDetails tmpCustomerAccountHistoryDetails = new CustomerAccountHistoryDetails()
                {
                    AccountID = ObjAccountDetails.AccountID,
                    SaleAmount = Double.Parse(txtCreatePaymentSaleAmount.Text == string.Empty ? "0" : txtCreatePaymentSaleAmount.Text),
                    DiscountAmount = Double.Parse(txtCreatePaymentSaleAmount.Text == string.Empty ? "0" : txtCreatePaymentSaleAmount.Text),
                    CancelAmount = Double.Parse(txtCreatePaymentCancelAmt.Text == string.Empty ? "0" : txtCreatePaymentCancelAmt.Text),
                    RefundAmount = Double.Parse((txtCreatePaymentRefundAmt.Text == string.Empty ? "0" : txtCreatePaymentRefundAmt.Text)),
                    BalanceAmount = Double.Parse(txtCreatePaymentOB.Text == string.Empty ? "0" : txtCreatePaymentOB.Text),
                    NewBalanceAmount = Double.Parse(txtCreatePaymentBA.Text == string.Empty ? "0" : txtCreatePaymentBA.Text),
                    AmountReceived = Double.Parse(txtbxCreatePaymentAmount.Text == string.Empty ? "0" : txtbxCreatePaymentAmount.Text),
                    NetSaleAmount = Double.Parse(txtCreatePaymentNetSaleAmt.Text == string.Empty ? "0" : txtCreatePaymentNetSaleAmt.Text),
                    TotalTaxAmount = Double.Parse(txtCreatePaymentTotalTax.Text == string.Empty ? "0" : txtCreatePaymentTotalTax.Text)
                };

                ObjPaymentsModel.CreateNewPaymentDetails(ref tmpPaymentDetails, ref tmpCustomerAccountHistoryDetails);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreatePaymentTable()", ex);
            }
        }
     

        public void EditPaymentTable()
        {
            try
            {
                List<string> ListColumnValues = new List<string>(), ListTempColValues = new List<string>();
                List<string> ListColumnNames = new List<string>(), ListTempColNames = new List<string>();

                ListColumnValues.Add(ObjAccountDetails.AccountID.ToString());
                ListColumnNames.Add("ACCOUNTID");

                ListColumnValues.Add(ObjPaymentsModel.GetPaymentModeDetails(cmbBoxPaymentModes.SelectedItem.ToString()).PaymentModeID.ToString());
                ListColumnNames.Add("PAYMENTMODEID");
                if (cmbxCreatePaymentPaymentAgainst.SelectedItem.ToString().ToUpper() == "INVOICE")
                {
                    ListColumnValues.Add(ListInvoiceDtls[CurrInvoiceCacheIndex].InvoiceID.ToString());
                    ListColumnNames.Add("INVOICEID");
                }
                else
                {
                    ListColumnValues.Add(txtCreatePaymentInvoiceNum.Text);
                    ListColumnNames.Add("QUOTATIONID");
                }

                ListColumnValues.Add(DateTime.Parse(dtpCreatePaymentPaidOn.Text).ToString("yyyy-MM-dd H:mm:ss"));
                ListColumnNames.Add("PAYMENTDATE");

                ListColumnValues.Add(txtbxCreatePaymentAmount.Text);
                ListColumnNames.Add("PAYMENTAMOUNT");

                if (chckActive.Checked)
                {
                    ListColumnValues.Add("1");
                }
                else
                {
                    ListColumnValues.Add("0");
                }
                ListColumnNames.Add("ACTIVE");
                ListColumnValues.Add(ObjUserMasterModel.GetUserID(cmbxcreatePaymentStaffName.Text).ToString());
                ListColumnNames.Add("USERID");
                if (txtCreatePaymentDesc.Text.Trim() != string.Empty)
                {
                    ListColumnValues.Add(txtCreatePaymentDesc.Text.Trim());
                    ListColumnNames.Add("DESCRIPTION");
                }

                string Now = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                ListColumnValues.Add(Now);
                ListColumnNames.Add("LASTUPDATEDATE");

                ListTempColValues.Add(txtbxCreatePaymentAmount.Text);
                ListTempColNames.Add("BALANCEAMOUNT");

                ListTempColValues.Add(Now);
                ListTempColNames.Add("LASTUPDATEDDATE");

                string WhereCondition = "ACCOUNTID = '" + ObjAccountDetails.AccountID + "'";
                int ResultVal = ObjUserMasterModel.UpdateAnyTableDetails("PAYMENTS", ListColumnNames, ListColumnValues, WhereCondition);
                if (ResultVal <= 0) MessageBox.Show("Wasnt able to update the payment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (ResultVal == 2)
                {
                    MessageBox.Show("err", "Error");
                }
                else
                {
                    MessageBox.Show("Updated Payment for :: " + txtCreatePaymentCustName.Text + " successfully", "Updated  Payment");
                    UpdateCustAccHistoryTble();
                    WhereCondition = "ACCOUNTID = '" + ObjAccountDetails.AccountID + "'";

                    ResultVal = CommonFunctions.ObjUserMasterModel.UpdateAnyTableDetails("ACCOUNTSMASTER", ListTempColNames, ListTempColValues, WhereCondition);

                    if (ResultVal < 0) MessageBox.Show("Wasnt able to Update the Account Master details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        //MessageBox.Show("Updated Account Master Details :: " + ObjAccountDetails.AccountID.ToString() + " successfully", "Update Account Details");
                        UpdatePaymentsOnClose(Mode: 1);
                    }

                    // UpdateCustomerOnClose(Mode: 1);
                    btnReset.PerformClick();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.EditPaymentTable()", ex);
            }
        }

        void UpdateCustAccHistoryTble()
        {
            try
            {

                int PaymentId = ObjPaymentDeatilsToBeEdited.PaymentId;
                List<string> ListColumnValues = new List<string>(), ListTempColValues = new List<string>();
                List<string> ListColumnNames = new List<string>(), ListTempColNames = new List<string>();

                ListColumnValues.Add(ObjAccountDetails.AccountID.ToString());
                ListColumnNames.Add("ACCOUNTID");

                ListColumnValues.Add(PaymentId.ToString());
                ListColumnNames.Add("PAYMENTID");

                ListColumnValues.Add(txtCreatePaymentSaleAmount.Text == string.Empty ? "0" : txtCreatePaymentSaleAmount.Text);
                ListColumnNames.Add("SALEAMOUNT");

                ListColumnValues.Add(txtCreatePaymentCancelAmt.Text == string.Empty ? "0" : txtCreatePaymentCancelAmt.Text);
                ListColumnNames.Add("CANCELAMOUNT");

                ListColumnValues.Add(txtCreatePaymentRefundAmt.Text == string.Empty ? "0" : txtCreatePaymentRefundAmt.Text);
                ListColumnNames.Add("RETURNAMOUNT");

                ListColumnValues.Add(txtCreatePaymentSaleAmount.Text == string.Empty ? "0" : txtCreatePaymentSaleAmount.Text);
                ListColumnNames.Add("DISCOUNTAMOUNT");

                ListColumnValues.Add(txtCreatePaymentTotalTax.Text == string.Empty ? "0" : txtCreatePaymentTotalTax.Text);
                ListColumnNames.Add("TOTALTAX");

                ListColumnValues.Add(txtCreatePaymentNetSaleAmt.Text == string.Empty ? "0" : txtCreatePaymentNetSaleAmt.Text);
                ListColumnNames.Add("NETSALEAMOUNT");

                ListColumnValues.Add(txtbxCreatePaymentAmount.Text == string.Empty ? "0" : txtbxCreatePaymentAmount.Text);
                ListColumnNames.Add("AMOUNTRECEIVED");

                ListColumnValues.Add(txtCreatePaymentBA.Text == string.Empty ? "0" : txtCreatePaymentBA.Text);
                ListColumnNames.Add("NEWBALANCEAMOUNT");

                ListColumnValues.Add(txtCreatePaymentOB.Text == string.Empty ? "0" : txtCreatePaymentOB.Text);
                ListColumnNames.Add("BALANCEAMOUNT");

                string WhereCondition = "PAYMENTID = '" + PaymentId.ToString() + "'";
                int ResultVal = ObjUserMasterModel.UpdateAnyTableDetails("CUSTOMERACCOUNTHISTORY", ListColumnNames, ListColumnValues, WhereCondition);
                if (ResultVal <= 0) MessageBox.Show("Wasnt able to Update the customer acc History", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (ResultVal == 2)
                {
                    MessageBox.Show("err.", "Error");
                }
                else
                {
                    //MessageBox.Show("Updated CustomerAccountHistory table", "Updated CustomerAccountHistory");
                    UpdatePaymentsOnClose(Mode: 1);
                    btnReset.PerformClick();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateCustAccHistoryTble()", ex);
            }
        }
        void FillCustAccHistoryTble()
        {
            try
            {

                int PaymentId = ObjMySQLHelper.GetLatestColValFromTable("PAYMENTID", "PAYMENTS");
                List<string> ListColumnValues = new List<string>(), ListTempColValues = new List<string>();
                List<string> ListColumnNames = new List<string>(), ListTempColNames = new List<string>();
                List<Types> ListTypes = new List<Types>();
                ListColumnValues.Add(ObjAccountDetails.AccountID.ToString());
                ListColumnNames.Add("ACCOUNTID");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(PaymentId.ToString());
                ListColumnNames.Add("PAYMENTID");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(txtCreatePaymentSaleAmount.Text == string.Empty ? "0" : txtCreatePaymentSaleAmount.Text);
                ListColumnNames.Add("SALEAMOUNT");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(txtCreatePaymentCancelAmt.Text == string.Empty ? "0" : txtCreatePaymentCancelAmt.Text);
                ListColumnNames.Add("CANCELAMOUNT");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(txtCreatePaymentRefundAmt.Text == string.Empty ? "0" : txtCreatePaymentRefundAmt.Text);
                ListColumnNames.Add("RETURNAMOUNT");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(txtCreatePaymentSaleAmount.Text == string.Empty ? "0" : txtCreatePaymentSaleAmount.Text);
                ListColumnNames.Add("DISCOUNTAMOUNT");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(txtCreatePaymentTotalTax.Text == string.Empty ? "0" : txtCreatePaymentTotalTax.Text);
                ListColumnNames.Add("TOTALTAX");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(txtCreatePaymentNetSaleAmt.Text == string.Empty ? "0" : txtCreatePaymentNetSaleAmt.Text);
                ListColumnNames.Add("NETSALEAMOUNT");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(txtbxCreatePaymentAmount.Text == string.Empty ? "0" : txtbxCreatePaymentAmount.Text);
                ListColumnNames.Add("AMOUNTRECEIVED");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(txtCreatePaymentBA.Text == string.Empty ? "0" : txtCreatePaymentBA.Text);
                ListColumnNames.Add("NEWBALANCEAMOUNT");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(txtCreatePaymentOB.Text == string.Empty ? "0" : txtCreatePaymentOB.Text);
                ListColumnNames.Add("BALANCEAMOUNT");
                ListTypes.Add(Types.Number);

                int ResultVal = ObjMySQLHelper.InsertIntoTable("CUSTOMERACCOUNTHISTORY", ListColumnNames, ListColumnValues, ListTypes);

                if (ResultVal <= 0) MessageBox.Show("Wasnt able to create the CustomerAccHistory", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (ResultVal == 2)
                {
                    MessageBox.Show("err.", "Error");
                }
                else
                {
                    //MessageBox.Show("Updated CustomerAccountHistory table", "Updated CustomerAccountHistory");
                    UpdatePaymentsOnClose(Mode: 1);
                    btnReset.PerformClick();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.FillCustAccHistoryTble()", ex);
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                cmbxcreatePaymentStaffName.SelectedIndex = 0;
                cmbxCreatePaymentNumber.SelectedItem = 0;
                cmbxCreatePaymentPaymentAgainst.SelectedIndex = 0;
                cmbBoxPaymentModes.SelectedIndex = 0;
                cmbxCreatePaymentCustomerNames.SelectedIndex = 0;
                txtCreatePaymentDesc.Text = "";
                txtbxCreatePaymentAmount.Text = "0";
                txtCreatePaymentRefundAmt.Text = txtCreatePaymentDiscAmt.Text = txtCreatePaymentTotalTax.Text = txtCreatePaymentCancelAmt.Text = "0";
                txtCreatePaymentInvoiceNum.Text = "";
                txtCreatePaymentInvoiceDate.Text = "";
                txtCreatePaymentInvoiceItems.Text = "";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnReset_Click()", ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnClose_Click()", ex);
            }
        }

        private void cmbxCreatePaymentCustomerNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbxCreatePaymentCustomerNames.SelectedIndex > 0)
                {
                    ObjCustomerDetails = ObjCustomerMasterModel.GetCustomerDetails(cmbxCreatePaymentCustomerNames.SelectedItem.ToString());
                    txtCreatePaymentCustName.Text = ObjCustomerDetails.CustomerName;
                    txtCreatePaymentCustAddress.Text = ObjCustomerDetails.Address;
                    txtCreatePaymentsCustPhoneNo.Text = ObjCustomerDetails.PhoneNo;
                    ObjAccountDetails = ObjAccountsMasterModel.GetAccDtlsFromCustID(ObjCustomerDetails.CustomerID);
                    if (ObjAccountDetails != null) txtCreatePaymentOB.Text = ObjAccountDetails.BalanceAmount.ToString();
                    FillPaymentAgainst();
                    if (this.Text == "Update Payment")
                    {
                       //PaymentDetails ObjPaymentDtls = new PaymentDetails();

                        if (ObjAccountDetails != null)
                        {
                            //ObjPaymentDtls = ObjPaymentsModel.GetPaymentDetailsFromAccID(ObjAccountDetails.AccountID);
                            CustomerAccountHistoryDetails ObjCustomerAccountHistoryDetails = ObjAccountHistoryModel.GetAccountHistoryDetailsFromPaymentID(ObjPaymentDeatilsToBeEdited.PaymentId);
                            txtCreatePaymentRefundAmt.Text = ObjCustomerAccountHistoryDetails.RefundAmount.ToString();
                            txtCreatePaymentDiscAmt.Text = ObjCustomerAccountHistoryDetails.DiscountAmount.ToString();
                            txtCreatePaymentTotalTax.Text = ObjCustomerAccountHistoryDetails.TotalTaxAmount.ToString();
                            txtCreatePaymentCancelAmt.Text = ObjCustomerAccountHistoryDetails.CancelAmount.ToString();
                            txtCreatePaymentNetSaleAmt.Text = ObjCustomerAccountHistoryDetails.NetSaleAmount.ToString();
                            txtCreatePaymentSaleAmount.Text = ObjCustomerAccountHistoryDetails.SaleAmount.ToString();
                            txtCreatePaymentOB.Text = ObjCustomerAccountHistoryDetails.BalanceAmount.ToString();
                        }
                        //txtCreatePaymentDesc.Text = ObjPaymentDtls.Description;
                        //txtbxCreatePaymentAmount.Text = ObjPaymentDtls.Amount.ToString();
                        cmbxcreatePaymentStaffName.SelectedItem = ObjPaymentDeatilsToBeEdited.StaffName;
                        chckActive.Checked = ObjPaymentDeatilsToBeEdited.Active;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbxCreatePaymentCustomerNames_SelectedIndexChanged()", ex);
            }
        }

        public void FillPaymentAgainst()
        {
            try
            {
                if (cmbxCreatePaymentPaymentAgainst.SelectedItem.ToString().ToUpper() == "INVOICE")
                {
                    string WhereCondition = " and InvoiceStatus = '" + INVOICESTATUS.Created + "' or InvoiceStatus = '" + INVOICESTATUS.Delivered + "'";
                    ListInvoiceDtls = ObjInvoicesModel.GetInvoiceDetailsForCustomer(ObjCustomerDetails.CustomerID, WhereCondition);
                    List<string> ListInvoiceNum = new List<string>() { "Select Invoice#" };
                    if (ListInvoiceDtls != null) ListInvoiceNum.AddRange(ListInvoiceDtls.Select(x => x.InvoiceNumber).ToList());
                    cmbxCreatePaymentNumber.DataSource = ListInvoiceNum;
                    cmbxCreatePaymentNumber.SelectedIndex = 0;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbxCreatePaymentCustomerNames_SelectedIndexChanged()", ex);
            }
        }
        private void cmbxCreatePaymentPaymentAgainst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbxCreatePaymentCustomerNames.SelectedIndex > 0)
                {
                    if (cmbxCreatePaymentPaymentAgainst.SelectedItem.ToString().ToUpper() == "INVOICE")
                    {
                        FillPaymentAgainst();
                    }
                    else if (cmbxCreatePaymentPaymentAgainst.SelectedItem.ToString().ToUpper() == "QUOTATIONS")
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbxCreatePaymentPaymentAgainst_SelectedIndexChanged()", ex);
            }
        }

        private void txtbxCreatePaymentAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtbxCreatePaymentAmount.Text = txtbxCreatePaymentAmount.Text.Trim();
                bool isValid = CommonFunctions.ValidateDoubleORIntVal(txtbxCreatePaymentAmount.Text);
                if (!isValid)
                {
                    lblValidateErrMsg.Visible = true;
                    lblValidateErrMsg.Text = "Enter Valid Amount(Integer/Decimal Values)!";
                    txtbxCreatePaymentAmount.Focus();
                    return;
                }
                lblValidateErrMsg.Visible = false;
                if (txtCreatePaymentBA.Text.Trim() != string.Empty) txtCreatePaymentBA.Text = (Double.Parse(txtbxCreatePaymentAmount.Text.Trim()) - Double.Parse(txtCreatePaymentBA.Text.Trim())).ToString();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.txtbxCreatePaymentAmount_TextChanged()", ex);
            }
        }


        private void cmbxCreatePaymentNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbxCreatePaymentNumber.SelectedIndex > 0)
                {
                    txtCreatePaymentInvoiceNum.Text = cmbxCreatePaymentNumber.SelectedItem.ToString();
                    CurrInvoiceCacheIndex = ListInvoiceDtls.FindIndex(x => x.InvoiceNumber == txtCreatePaymentInvoiceNum.Text);
                    txtCreatePaymentInvoiceDate.Text = ListInvoiceDtls[CurrInvoiceCacheIndex].InvoiceDate.ToString();
                    txtCreatePaymentInvoiceItems.Text = ListInvoiceDtls[CurrInvoiceCacheIndex].InvoiceItemCount.ToString();
                    // txtCreatePaymentNetSaleAmt.Text = ListInvoiceDtls[CurrInvoiceCacheIndex].NetInvoiceAmount.ToString();
                    txtCreatePaymentRefundAmt.Text = txtCreatePaymentDiscAmt.Text = txtCreatePaymentTotalTax.Text = txtCreatePaymentCancelAmt.Text = "0";
                    //Net Sale Amount = Sale Amount - Cancel Amount - Return Amount - Discount + Tax Amount
                    txtCreatePaymentNetSaleAmt.Text = (Double.Parse(txtCreatePaymentSaleAmount.Text.Trim() == string.Empty ? "0" : txtCreatePaymentSaleAmount.Text.Trim())
                        - Double.Parse(txtCreatePaymentCancelAmt.Text.Trim() == string.Empty ? "0" : txtCreatePaymentCancelAmt.Text.Trim())
                        - Double.Parse(txtCreatePaymentRefundAmt.Text.Trim() == string.Empty ? "0" : txtCreatePaymentRefundAmt.Text.Trim())
                        - Double.Parse(txtCreatePaymentDiscAmt.Text.Trim() == string.Empty ? "0" : txtCreatePaymentDiscAmt.Text.Trim())
                        + Double.Parse(txtCreatePaymentTotalTax.Text.Trim() == string.Empty ? "0" : txtCreatePaymentTotalTax.Text.Trim())
                        ).ToString();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbxCreatePaymentNumber_SelectedIndexChanged()", ex);
            }
        }

        private void chckbxCreatePaymentMarkInvoiceAsPaid_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                List<string> ListColumnNames = new List<string>(), ListColumnValues = new List<string>();
                ListColumnNames.Add("INVOICESTATUS");
                if (chckbxCreatePaymentMarkInvoiceAsPaid.Checked && cmbxCreatePaymentNumber.SelectedIndex > 0)
                {
                    ListColumnValues.Add("Paid");

                }
                else
                {
                    ListColumnValues.Add("Created");
                }
                string WhereCondition = "INVOICENUMBER = '" + cmbxCreatePaymentNumber.SelectedItem.ToString() + "'";
                Int32 ReturnVal = CommonFunctions.ObjUserMasterModel.UpdateAnyTableDetails("Invoices", ListColumnNames, ListColumnValues, WhereCondition);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.chckbxCreatePaymentMarkInvoiceAsPaid_CheckedChanged()", ex);
            }
        }


        private void CreatePaymentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                UpdatePaymentsOnClose(Mode: 1);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreatePaymentForm_FormClosed()", ex);
            }
        }
    }
}
