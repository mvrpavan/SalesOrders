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
    public partial class AddPaymentSummaryForm : Form
    {
        UpdateOnCloseDel UpdatePaymentsOnClose = null;
        //InvoicesModel ObjInvoicesModel;
        CustomerMasterModel ObjCustomerMasterModel = CommonFunctions.ObjCustomerMasterModel;
        UserMasterModel ObjUserMasterModel = CommonFunctions.ObjUserMasterModel;
        List<string> ListOfAllCustomers = new List<string>() { "Select Customer" };
        List<string> ListOfAllUsers = new List<string>() { "Select Staff" };
        //List<InvoiceDetails> ListInvoiceDtls = new List<InvoiceDetails>();
        CustomerDetails ObjCustomerDetails = null;
        //int CurrInvoiceCacheIndex = -1;
        MySQLHelper ObjMySQLHelper;
        List<string> ListCustomerNamesToBeExcluded = new List<string>();
        DateTime CreationDate = DateTime.MinValue;

        public AddPaymentSummaryForm(UpdateOnCloseDel UpdatePaymentOnClose, List<string> ListCustomerNamesAlreadyInGrid, DateTime ToDate)
        {
            try
            {
                InitializeComponent();
                ObjMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                this.UpdatePaymentsOnClose = UpdatePaymentOnClose;
                //ObjInvoicesModel = new InvoicesModel();
                //ObjInvoicesModel.Initialize();
                ListCustomerNamesToBeExcluded = ListCustomerNamesAlreadyInGrid;
                CreationDate = ToDate;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreatePaymentForm()", ex);
            }
        }

        private void AddPaymentSummaryForm_Load(object sender, EventArgs e)
        {
            try
            {
                ListOfAllCustomers.AddRange(ObjCustomerMasterModel.GetCustomerList());
                for (int i = 0; i < ListCustomerNamesToBeExcluded.Count; i++)
                {
                    int Ind = ListOfAllCustomers.FindIndex(x => x.Trim().Equals(ListCustomerNamesToBeExcluded[i], StringComparison.InvariantCultureIgnoreCase));
                    if (Ind >= 0)
                    { 
                        ListOfAllCustomers.RemoveAt(Ind);
                    }
                }
                if (ListOfAllCustomers.Count == 0)
                {
                    MessageBox.Show("All customers already exists in the Payment Summary Grid.Please Add New Customers Under 'Customers' tab ", "No Customers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                cmbBoxDeliveryLines.Items.Add("<Choose Delivery Line>");
                cmbBoxDeliveryLines.Items.AddRange(ObjCustomerMasterModel.GetAllLineNames().ToArray());
                cmbBoxDeliveryLines.SelectedIndex = 0;
                cmbxCreatePaymentCustomerNames.DataSource = ListOfAllCustomers;
                cmbxCreatePaymentCustomerNames.SelectedIndex = 0;
                //cmbxCreatePaymentNumber.SelectedItem = 0;
                lblValidateErrMsg.Visible = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.AddPaymentSummaryForm_Load()", ex);
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
                //if (cmbxCreatePaymentNumber.SelectedIndex == 0 || cmbxCreatePaymentNumber.SelectedIndex < 0)
                //{
                //    lblValidateErrMsg.Visible = true;
                //    lblValidateErrMsg.Text = "Pls Select Number!";
                //    return;
                //}
                if (lblValidateErrMsg.Visible == true)
                {
                    lblValidateErrMsg.Visible = false;
                }
                AddPaymentSummaryRow();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnAddUpdate_Click()", ex);
            }
        }
 
        void AddPaymentSummaryRow()
        {
            try
            {
                List<string> ListColumnValues = new List<string>(), ListTempColValues = new List<string>();
                List<string> ListColumnNames = new List<string>(), ListTempColNames = new List<string>();
                List<Types> ListTypes = new List<Types>();

                ListColumnValues.Add(ObjCustomerDetails.CustomerID.ToString());
                ListColumnNames.Add("CustomerID");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add("-1");
                ListColumnNames.Add("InvoiceID");
                ListTypes.Add(Types.Number);

                //ListColumnValues.Add(ListInvoiceDtls[CurrInvoiceCacheIndex].InvoiceNumber.ToString());
                //ListColumnNames.Add("InvoiceNumber");
                //ListTypes.Add(Types.String);

                String LineID = "-1";
                if (cmbBoxDeliveryLines.SelectedIndex > 0)
                    LineID = ObjCustomerMasterModel.GetLineID(cmbBoxDeliveryLines.SelectedItem.ToString()).ToString();
                ListColumnValues.Add(LineID);
                ListColumnNames.Add("DeliveryLineID");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(MySQLHelper.GetDateTimeStringForDB(CreationDate).ToString());
                ListColumnNames.Add("CreationDate");
                ListTypes.Add(Types.String);

                int ResultVal = ObjMySQLHelper.InsertIntoTable("TempPaymentsSummary", ListColumnNames, ListColumnValues, ListTypes);

                if (ResultVal <= 0) MessageBox.Show("Wasnt able to Add the Payment Summary Row", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    ListCustomerNamesToBeExcluded.Add(ObjCustomerDetails.CustomerName);
                    MessageBox.Show("Added the Payment Summary Row successfully", " Added Payment Summary", MessageBoxButtons.OK);
                    UpdatePaymentsOnClose(Mode: 2);
                    btnReset.PerformClick();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.AddPaymentSummaryRow()", ex);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                cmbxCreatePaymentCustomerNames.SelectedIndex = 0;
                cmbBoxDeliveryLines.SelectedIndex = 0;
                //cmbxCreatePaymentNumber.SelectedIndex = 0;
                txtCreatePaymentCustName.Text = "";
                txtCreatePaymentCustAddress.Text ="";
                txtCreatePaymentsCustPhoneNo.Text = "";
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
                    if (ListCustomerNamesToBeExcluded.Contains(cmbxCreatePaymentCustomerNames.SelectedItem.ToString()))
                    {
                        MessageBox.Show("Payment record already exists for Customer. Please Select another Customer", "Already Exists", MessageBoxButtons.OK);
                        btnReset.PerformClick();
                        return;
                    }

                    ObjCustomerDetails = ObjCustomerMasterModel.GetCustomerDetails(cmbxCreatePaymentCustomerNames.SelectedItem.ToString());
                    //FillPaymentAgainst();
                    txtCreatePaymentCustName.Text = ObjCustomerDetails.CustomerName;
                    txtCreatePaymentCustAddress.Text = ObjCustomerDetails.Address;
                    txtCreatePaymentsCustPhoneNo.Text = ObjCustomerDetails.PhoneNo;
                    if (ObjCustomerDetails.LineID > 0) cmbBoxDeliveryLines.SelectedItem = ObjCustomerDetails.LineName;
                    else cmbBoxDeliveryLines.SelectedIndex = 0;
                    //if (ListInvoiceDtls != null) CurrInvoiceCacheIndex = ListInvoiceDtls.FindIndex(x => x.InvoiceNumber == cmbxCreatePaymentNumber.SelectedItem.ToString());
                    //else
                    //{
                    //    MessageBox.Show("No Invoice exists for this customer.Please Select another Customer ", " No Invoice Exists", MessageBoxButtons.OK);
                    //    btnReset.PerformClick();
                    //}
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbxCreatePaymentCustomerNames_SelectedIndexChanged()", ex);
            }
        }

        //private void cmbxCreatePaymentNumber_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //if (cmbxCreatePaymentNumber.SelectedIndex > 0)
        //        //{
        //        //    CurrInvoiceCacheIndex = ListInvoiceDtls.FindIndex(x => x.InvoiceNumber == cmbxCreatePaymentNumber.SelectedItem.ToString());
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonFunctions.ShowErrorDialog($"{this}.cmbxCreatePaymentNumber_SelectedIndexChanged()", ex);
        //    }
        //}

        //public void FillPaymentAgainst()
        //{
        //    try
        //    {
        //            string WhereCondition = " and InvoiceStatus = '" + INVOICESTATUS.Created + "' or InvoiceStatus = '" + INVOICESTATUS.Delivered + "'" ;
        //            ListInvoiceDtls = ObjInvoicesModel.GetInvoiceDetailsForCustomer(ObjCustomerDetails.CustomerID, WhereCondition);
        //            List<string> ListInvoiceNum = new List<string>() { "Select Invoice#" };
        //            if (ListInvoiceDtls != null) ListInvoiceNum.AddRange(ListInvoiceDtls.Select(x => x.InvoiceNumber).ToList());
        //            cmbxCreatePaymentNumber.DataSource = ListInvoiceNum;
        //            cmbxCreatePaymentNumber.SelectedIndex = ListInvoiceNum.Count - 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonFunctions.ShowErrorDialog($"{this}.FillPaymentAgainst()", ex);
        //    }
        //}
    }
}
