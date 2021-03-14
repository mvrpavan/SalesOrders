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
    public partial class EditVendorForm : Form
    {

        MySQLHelper tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
        UpdateOnCloseDel UpdateVendorOnClose = null;
        public EditVendorForm(UpdateOnCloseDel UpdateVendorOnClose)
        {
            InitializeComponent();
            txtEditVendorName.Focus();
            FillState();
            cmbxEditVendorSelectState.SelectedIndex = 0;
            this.UpdateVendorOnClose = UpdateVendorOnClose;
            this.FormClosed += EditVendorForm_FormClosed;
        }
        private void FillState()
        {
            try
            {
                tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                List<string> ListStates = CommonFunctions.ObjCustomerMasterModel.GetAllStatesOfIndia();
                cmbxEditVendorSelectState.Items.Add("Select State");
                foreach (var item in ListStates)
                {
                    cmbxEditVendorSelectState.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditVendorForm.FillState()", ex);
                throw ex;
            }
        }

    
        private void btnEditCutomerReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtEditVendorAddress.Clear();
                txtEditGSTIN.Clear();
                txtEditVendorPhone.Clear();
                cmbxEditVendorSelectState.SelectedIndex = 0;

                txtEditVendorName.Focus();


            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditVendorForm.btnReset_Click()", ex);
                throw ex;
            }
        }
        private void btnEditVendor_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdbtnEditVendorActiveYes.Checked == false && rdbtnEditVendorActiveNo.Checked == false)
                {
                    lblCommonErrorMsg.Visible = true;
                    lblCommonErrorMsg.Text = "Pls Set whether Vendor Is Active or Not ";
                    return;
                }
                if (txtEditVendorPhone.Text.Trim() != string.Empty)
                {
                    if (!CheckForValidPhone()) return;
                }

                if (lblCommonErrorMsg.Visible == true)
                {
                    lblCommonErrorMsg.Visible = false;
                }

                List<string> ListColumnValues = new List<string>();
                List<string> ListColumnNames = new List<string>();

                ListColumnValues.Add(txtEditVendorAddress.Text);
                ListColumnNames.Add("Address");

                ListColumnValues.Add(txtEditGSTIN.Text);
                ListColumnNames.Add("GSTIN");

                ListColumnValues.Add(txtEditVendorPhone.Text.Trim() == string.Empty ? "NULL" : txtEditVendorPhone.Text.Trim());
                ListColumnNames.Add("PhoneNo");

                ListColumnNames.Add("Active");

                if (rdbtnEditVendorActiveNo.Checked == true)
                {
                    ListColumnValues.Add("0");
                }
                else ListColumnValues.Add("1");

                int StateID = CommonFunctions.ObjCustomerMasterModel.GetStateID(cmbxEditVendorSelectState.SelectedItem.ToString());
                ListColumnValues.Add(StateID == -1 ? "NULL" : StateID.ToString());
                ListColumnNames.Add("StateID");

                ListColumnValues.Add(DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));
                ListColumnNames.Add("LastUpdateDate");


                string WhereCondition = "VendorName = '" + txtEditVendorName.Text + "'";
                tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                int ResultVal = CommonFunctions.ObjUserMasterModel.UpdateAnyTableDetails("VendorMaster", ListColumnNames, ListColumnValues, WhereCondition);

                if (ResultVal < 0) MessageBox.Show("Wasnt able to Edit the Vendor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                else
                {
                    MessageBox.Show("Updated Vendor :: " + txtEditVendorName.Text + " successfully", "Update Vendor");
                    UpdateVendorOnClose(Mode: 1);
                }

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditVendorForm.btnEditVendor_Click()", ex);
                throw ex;
            }
        }



        private bool CheckForValidPhone()
        {
            try
            {
                bool IsValid = IsValid = CommonFunctions.ValidatePhoneNo(txtEditVendorPhone.Text);
                if (!IsValid)
                {
                    lblCommonErrorMsg.Visible = true;
                    lblCommonErrorMsg.Text = "Enter Valid Phone No!"; ;
                    txtEditVendorPhone.Focus();
                }
                else
                {
                    lblCommonErrorMsg.Visible = false;
                }
                return IsValid;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditVendorForm.CheckForValidPhone()", ex);
                throw ex;
            }
        }

        private void EditVendorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                UpdateVendorOnClose(Mode: 1);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditVendorForm.EditVendorForm_FormClosed()", ex);
            }
        }
    }
}
