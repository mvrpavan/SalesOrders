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
    public partial class CreateUserForm : Form
    {

        MySQLHelper tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
        UpdateOnCloseDel UpdateOnClose = null;
        public CreateUserForm(UpdateOnCloseDel UpdateOnClose)
        {
            try
            { 
            InitializeComponent();
            txtCreateUserName.Focus();
            cmbxSelectRoleID.Items.Clear();
            FillRoles();
            cmbxSelectRoleID.SelectedIndex = 0;
            FillStores();
            cmbxSelectStore.SelectedIndex = 0;
            this.UpdateOnClose = UpdateOnClose;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateUserForm.CreateUserForm()", ex);
                throw ex;
            }
        }
        private void FillRoles()
        {
            try
            {
                tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                List<string> ListRoles = CommonFunctions.ObjUserMasterModel.GetAllRoles();
                cmbxSelectRoleID.Items.Add("Select Role");
                foreach (var item in ListRoles)
                {
                    cmbxSelectRoleID.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateUserForm.FillRoles()", ex);
                throw ex;
            }
        }

        private void FillStores()
        {
            try
            {
                tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                List<string> ListStores = CommonFunctions.ObjUserMasterModel.GetAllStores();
                cmbxSelectStore.Items.Add("Select Store");
                foreach (var item in ListStores)
                {
                    cmbxSelectStore.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateUserForm.FillStores()", ex);
                throw ex;
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtCreateUserName.Clear();
                txtCreatePassword.Clear();
                txtFullName.Clear();
                txtPhone.Clear();
                txtEmailID.Clear();
                cmbxSelectRoleID.SelectedIndex = 0;
                cmbxSelectStore.SelectedIndex = 0;
                txtCreateUserName.Focus();
                //lblCreateEmailIdValidMsg.Visible = false;
                //lblCreatePhoneValidMsg.Visible = false;
                lblCommonErrorMsg.Visible = false;
                //lblCreatePwdValidMsg.Visible = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateUserForm.btnReset_Click()", ex);
                throw ex;
            }
        }
        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCreateUserName.Text.Trim() == string.Empty || txtCreatePassword.Text.Trim() == string.Empty)
                {
                    lblCommonErrorMsg.Visible = true;
                    lblCommonErrorMsg.Text = "User Name Or Password Cannot be empty!";
                    return;
                }
                if (txtCreatePassword.Text.Trim() != string.Empty)
                {
                    if (!CheckForValidPwd()) return;
                }
                if (cmbxSelectRoleID.SelectedIndex == 0)
                {
                    lblCommonErrorMsg.Visible = true;
                    lblCommonErrorMsg.Text = "Select a Role for the User!! ";
                    return;
                }
                if (txtFullName.Text.Trim() == string.Empty)
                {
                    lblCommonErrorMsg.Visible = true;
                    lblCommonErrorMsg.Text = "Full Name cannot be empty! ";
                    return;
                }
                if (rdbtnActiveYes.Checked == false && rdbtnActiveNo.Checked == false)
                {
                    lblCommonErrorMsg.Visible = true;
                    lblCommonErrorMsg.Text = "Pls Set whether User Is Active or Not ";
                    return;
                }
                if (lblCommonErrorMsg.Visible == true)
                {
                    lblCommonErrorMsg.Visible = false;
                }
                List<string> ListColumnValues = new List<string>();
                List<string> ListColumnNamesWithDataType = new List<string>();
                if (txtEmailID.Text.Trim() != string.Empty)
                {
                    if (!CheckForValidEmailId()) return;
                    ListColumnValues.Add(txtEmailID.Text);
                    ListColumnNamesWithDataType.Add("EMAILID,VARCHAR");
                }
                if (txtPhone.Text.Trim() != string.Empty)
                {
                    if (!CheckForValidPhone()) return;
                    ListColumnValues.Add(txtPhone.Text);
                    ListColumnNamesWithDataType.Add("PHONENO,BIGINT");
                }
                if (cmbxSelectStore.SelectedIndex > 0)
                {
                    ListColumnValues.Add(CommonFunctions.ObjUserMasterModel.GetStoreID(cmbxSelectStore.SelectedItem.ToString()).ToString());
                    ListColumnNamesWithDataType.Add("STOREID,INT");
                }
                ListColumnValues.Add(CommonFunctions.ObjUserMasterModel.GetUserID(tmpMySQLHelper.CurrentUser).ToString());
                ListColumnNamesWithDataType.Add("CREATEDBY,INT");

                ListColumnValues.Add(DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));
                ListColumnNamesWithDataType.Add("LASTUPDATEDATE,DATETIME");

                int ResultVal = CommonFunctions.ObjUserMasterModel.CreateNewUser(txtCreateUserName.Text, txtCreatePassword.Text, txtFullName.Text, rdbtnActiveYes.Checked, cmbxSelectRoleID.Text, ListColumnNamesWithDataType, ListColumnValues);
                if (ResultVal <= 0) MessageBox.Show("Wasnt able to create the user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (ResultVal == 2)
                {
                    MessageBox.Show("User Name already exists! Pls Provide Another User Name.", "Error");
                }
                else
                {
                    MessageBox.Show("Added New User :: " + txtCreateUserName.Text + " successfully", "Added User");
                    UpdateOnClose(Mode: 1);
                    btnReset.PerformClick();
                }

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateUserForm.btnCreateUser_Click()", ex);
                throw ex;
            }

        }

        private bool CheckForValidPwd()
        {
            try
            {
                bool IsValid = CommonFunctions.CheckForPasswordLength(txtCreatePassword.Text);
                if (!IsValid)
                {
                    lblCommonErrorMsg.Visible = true;
                    lblCommonErrorMsg.Text = "Password length should be of 5 to 20 characters";
                    txtCreatePassword.Focus();
                }
                else
                {
                    lblCommonErrorMsg.Visible = false;
                }
                return IsValid;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateUserForm.CheckForValidPwd()", ex);
                throw ex;
            }
        }

        private bool CheckForValidEmailId()
        {
            try
            {
                bool IsValid = CommonFunctions.ValidateEmail(txtEmailID.Text);
                if (!IsValid)
                {
                    lblCommonErrorMsg.Visible = true;
                    lblCommonErrorMsg.Text = "Invalid EmailID!";
                    txtEmailID.Focus();
                }
                else
                {
                    lblCommonErrorMsg.Visible = false;
                    btnCreateUser.Enabled = true;
                }
                return IsValid;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateUserForm.CheckForValidEmailId()", ex);
                throw ex;
            }
        }

        private bool CheckForValidPhone()
        {
            try
            {
                bool IsValid = IsValid = CommonFunctions.ValidatePhoneNo(txtPhone.Text);
                if (!IsValid)
                {
                    lblCommonErrorMsg.Visible = true;
                    lblCommonErrorMsg.Text = "Enter Valid Phone No!"; ;
                    txtPhone.Focus();
                }
                else
                {
                    lblCommonErrorMsg.Visible = false;
                }
                return IsValid;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateUserForm.CheckForValidPhone()", ex);
                throw ex;
            }
        }
        private void cmbxSelectRoleID_Click(object sender, EventArgs e)
        {
            try
            {
                cmbxSelectRoleID.DroppedDown = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateUserForm.cmbxSelectRoleID_Click()", ex);
                throw ex;
            }
        }

        private void cmbxSelectStore_Click(object sender, EventArgs e)
        {
            try
            {
                cmbxSelectStore.DroppedDown = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateUserForm.cmbxSelectStore_Click()", ex);
                throw ex;
            }
        }

    }
}
