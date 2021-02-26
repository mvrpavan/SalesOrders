using SalesOrdersReport.CommonModules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SalesOrdersReport
{
    public partial class CreateCustomerForm : Form
    {
        UpdateOnCloseDel UpdateCustomerOnClose = null;
		UpdateUsingObjectOnCloseDel UpdateObjectOnClose = null;
        Boolean IsRetailCustomer = false;

        public CreateCustomerForm(UpdateOnCloseDel UpdateCustomerOnClose, UpdateUsingObjectOnCloseDel UpdateObjectOnClose = null, Boolean IsRetailCustomer = false)
        {
            try
            {


                InitializeComponent();
                txtCreateCustomerName.Focus();
                FillStates();
                cmbxCreateCustSelectState.SelectedIndex = 0;
                FillDiscGrp();
                cmbxCreateCustSelectDiscGrp.SelectedIndex = 0;
                FillPriceGrp();
                cmbxCreateCustSelectPriceGrp.SelectedIndex = 0;
                FillLines();
                cmbxCreateCustSelectLine.SelectedIndex = 0;
                rdbtnCustActiveYes.Checked = true;

                chbxMonday.Checked = true;
                this.UpdateCustomerOnClose = UpdateCustomerOnClose;

	            this.UpdateObjectOnClose = UpdateObjectOnClose;
	            this.IsRetailCustomer = IsRetailCustomer;
	
	            if (IsRetailCustomer)
	            {
	                cmbxCreateCustSelectState.SelectedIndex = cmbxCreateCustSelectState.FindStringExact("Karnataka");
	                cmbxCreateCustSelectPriceGrp.SelectedIndex = cmbxCreateCustSelectPriceGrp.FindStringExact("Retail Price");
	                //cmbxCreateCustSelectDiscGrp.SelectedIndex = cmbxCreateCustSelectDiscGrp.FindStringExact("Retail Discount");
	                rdbtnCustActiveYes.Checked = true;
				}
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateCustomerForm()", ex);
                throw ex;
            }
        }
        private void FillStates()
        {
            try
            {
                List<string> ListStates = CommonFunctions.ObjCustomerMasterModel.GetAllStatesOfIndia();
                cmbxCreateCustSelectState.Items.Add("Select State");
                foreach (var item in ListStates)
                {
                    cmbxCreateCustSelectState.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateCustomerForm.FillStates()", ex);
                throw ex;
            }
        }

        private void FillDiscGrp()
        {
            try
            {
                List<string> ListDiscGrp = CommonFunctions.ObjCustomerMasterModel.GetAllDiscGrp();
                cmbxCreateCustSelectDiscGrp.Items.Add("Select Discount Group");
                foreach (var item in ListDiscGrp)
                {
                    cmbxCreateCustSelectDiscGrp.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateCustomerForm.FillDiscGrp()", ex);
                throw ex;
            }
        }
        private void FillPriceGrp()
        {
            try
            {
                List<string> ListPriceGrp = CommonFunctions.ObjCustomerMasterModel.GetAllPriceGrp();
                cmbxCreateCustSelectPriceGrp.Items.Add("Select Price Group");
                foreach (var item in ListPriceGrp)
                {
                    cmbxCreateCustSelectPriceGrp.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateCustomerForm.FillPriceGrp()", ex);
                throw ex;
            }
        }

        private void FillLines()
        {
            try
            {
                List<string> ListLines = CommonFunctions.ObjCustomerMasterModel.GetAllLineNames();
                cmbxCreateCustSelectLine.Items.Add("Select Line");
                foreach (var item in ListLines)
                {
                    cmbxCreateCustSelectLine.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateCustomerForm.FillLines()", ex);
                throw ex;
            }
        }
        private void btnCreateCutomerReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtCreateCustomerName.Clear();
                txtCustAddress.Clear();
                txtGSTIN.Clear();
                txtCreateCustPhone.Clear();
                cmbxCreateCustSelectState.SelectedIndex = 0; ;
                cmbxCreateCustSelectLine.SelectedIndex = 0;
                cmbxCreateCustSelectDiscGrp.SelectedIndex = 0;
                cmbxCreateCustSelectPriceGrp.SelectedIndex = 0;
                txtCreateCustomerName.Focus();
                foreach (Control itemControl in flpCreateCustOrderDays.Controls)
                {
                    CheckBox chbxControl = (CheckBox)itemControl;
                    chbxControl.Checked = false;
                }
                chbxMonday.Checked = true;

                if (IsRetailCustomer)
                {
                    cmbxCreateCustSelectState.SelectedIndex = cmbxCreateCustSelectState.FindStringExact("Karnataka");
                    cmbxCreateCustSelectPriceGrp.SelectedIndex = cmbxCreateCustSelectPriceGrp.FindStringExact("Retail Price");
                    //cmbxCreateCustSelectDiscGrp.SelectedIndex = cmbxCreateCustSelectDiscGrp.FindStringExact("Retail Discount");
                    rdbtnCustActiveYes.Checked = true;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateCustomerForm.btnReset_Click()", ex);
                throw ex;
            }
        }
        private void btnCreateCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCreateCustomerName.Text.Trim() == string.Empty)
                {
                    lblCommonErrorMsg.Visible = true;
                    lblCommonErrorMsg.Text = "Customer Name Cant be empty!";
                    return;
                }

                //if (cmbxCreateCustSelectLine.SelectedIndex == 0)
                //{
                //    lblCommonErrorMsg.Visible = true;
                //    lblCommonErrorMsg.Text = "Select a Line for the Customer!! ";
                //    return;
                //}
                if (rdbtnCustActiveYes.Checked == false && rdbtnCustActiveNo.Checked == false)
                {
                    lblCommonErrorMsg.Visible = true;
                    lblCommonErrorMsg.Text = "Pls Set whether Customer Is Active or Not ";
                    return;
                }
                if (lblCommonErrorMsg.Visible == true)
                {
                    lblCommonErrorMsg.Visible = false;
                }
                List<string> ListColumnValues = new List<string>();
                List<string> ListColumnNamesWithDataType = new List<string>();
                if (txtCustAddress.Text.Trim() != string.Empty)
                {
                    ListColumnValues.Add(txtCustAddress.Text);
                    ListColumnNamesWithDataType.Add("ADDRESS,VARCHAR");
                }
                if (txtGSTIN.Text.Trim() != string.Empty)
                {
                    ListColumnValues.Add(txtGSTIN.Text);
                    ListColumnNamesWithDataType.Add("GSTIN,VARCHAR");
                }
                if (txtCreateCustPhone.Text.Trim() != string.Empty)
                {
                    if (!CheckForValidPhone()) return;
                    ListColumnValues.Add(txtCreateCustPhone.Text);
                    ListColumnNamesWithDataType.Add("PHONENO,BIGINT");
                }
                if (cmbxCreateCustSelectLine.SelectedIndex > 0)
                {
                    ListColumnValues.Add(CommonFunctions.ObjCustomerMasterModel.GetLineID(cmbxCreateCustSelectLine.SelectedItem.ToString()).ToString());
                    ListColumnNamesWithDataType.Add("LINEID,INT");
                }
                if (cmbxCreateCustSelectDiscGrp.SelectedIndex > 0)
                {
                    ListColumnValues.Add(CommonFunctions.ObjCustomerMasterModel.GetDisGrpID(cmbxCreateCustSelectDiscGrp.SelectedItem.ToString()).ToString());
                    ListColumnNamesWithDataType.Add("DISCOUNTGROUPID,INT");
                }
                if (cmbxCreateCustSelectPriceGrp.SelectedIndex > 0)
                {
                    ListColumnValues.Add(CommonFunctions.ObjCustomerMasterModel.GetPriceGrpID(cmbxCreateCustSelectPriceGrp.SelectedItem.ToString()).ToString());
                    ListColumnNamesWithDataType.Add("PRICEGROUPID,INT");
                }
                if (cmbxCreateCustSelectState.SelectedIndex > 0)
                {
                    ListColumnValues.Add(CommonFunctions.ObjCustomerMasterModel.GetStateID(cmbxCreateCustSelectState.SelectedItem.ToString()).ToString());
                    ListColumnNamesWithDataType.Add("STATEID,VARCHAR");
                }
                string SelectedOrderEndDays = ""; bool AtleastOneChecked = false;
                foreach (Control itemControl in flpCreateCustOrderDays.Controls)
                {
                    CheckBox chbxControl = (CheckBox)(itemControl);
                    if (chbxControl.Checked)
                    {
                        SelectedOrderEndDays += CommonFunctions.ObjCustomerMasterModel.GetOrderDaysCode(chbxControl.Text) + ",";
                        if (!AtleastOneChecked) AtleastOneChecked = true;
                    }
                }
                if (AtleastOneChecked)
                {
                    SelectedOrderEndDays = SelectedOrderEndDays.Remove(SelectedOrderEndDays.Length - 1, 1);
                    ListColumnValues.Add(SelectedOrderEndDays);
                    ListColumnNamesWithDataType.Add("ORDERDAYS,VARCHAR");
                }

                ListColumnValues.Add(DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));
                ListColumnNamesWithDataType.Add("LASTUPDATEDATE,DATETIME");
                ListColumnValues.Add(DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));
                ListColumnNamesWithDataType.Add("ADDEDDATE,DATETIME");
                

                int ResultVal = CommonFunctions.ObjCustomerMasterModel.CreateNewCustomer(txtCreateCustomerName.Text, rdbtnCustActiveYes.Checked, ListColumnNamesWithDataType, ListColumnValues);
                if (ResultVal <= 0) MessageBox.Show("Wasnt able to create the customer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (ResultVal == 2)
                {
                    MessageBox.Show("Customer Name already exists! Pls Provide Another Customer Name.", "Error");
                }
                else
                {
                    MessageBox.Show("Added New Customer :: " + txtCreateCustomerName.Text + " successfully", "Added Customer");
                    if (UpdateCustomerOnClose != null) UpdateCustomerOnClose(Mode: 1);
                    btnReset.PerformClick();
                }

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateCustomerForm.btnCreateCustomer_Click()", ex);
                throw ex;
            }

        }

        private bool CheckForValidPhone()
        {
            try
            {
                bool IsValid = IsValid = CommonFunctions.ValidatePhoneNo(txtCreateCustPhone.Text);
                if (!IsValid)
                {
                    lblCommonErrorMsg.Visible = true;
                    lblCommonErrorMsg.Text = "Enter Valid Phone No!"; ;
                    txtCreateCustPhone.Focus();
                }
                else
                {
                    lblCommonErrorMsg.Visible = false;
                }
                return IsValid;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateCustomerForm.CheckForValidPhone()", ex);
                throw ex;
            }
        }
        private void cmbxSelectRoleID_Click(object sender, EventArgs e)
        {
            try
            {
                cmbxCreateCustSelectLine.DroppedDown = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateCustomerForm.cmbxSelectRoleID_Click()", ex);
                throw ex;
            }
        }

        private void cmbxSelectStore_Click(object sender, EventArgs e)
        {
            try
            {
                cmbxCreateCustSelectDiscGrp.DroppedDown = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateCustomerForm.cmbxSelectStore_Click()", ex);
                throw ex;
            }
        }
    }
}
