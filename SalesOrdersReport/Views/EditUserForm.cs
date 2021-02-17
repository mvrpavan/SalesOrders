using SalesOrdersReport.CommonModules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SalesOrdersReport
{
    public partial class EditUserForm : Form
    {
        MySQLHelper tmpMySQLHelper = null;
        UpdateOnCloseDel UpdateOnClose = null;
        public EditUserForm(UpdateOnCloseDel UpdateOnClose)
        {
            InitializeComponent();
            txtFullName.Focus();
            cmbxSelectRoleID.Items.Clear();
            FillRoles();
            cmbxSelectRoleID.SelectedIndex = 0;
            FillStores();
            cmbxSelectStore.SelectedIndex = 0;
            this.UpdateOnClose = UpdateOnClose;
            this.FormClosed += EditUserForm_FormClosed;
            tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
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
                    //cmbxSelectRoleID.Items.Add(item.Cast<object>());
                    cmbxSelectRoleID.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditUserForm.FillRoles()", ex);
                throw;
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
                    //cmbxSelectRoleID.Items.Add(item.Cast<object>());
                    cmbxSelectStore.Items.Add(item);
                }
            }

            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditUserForm.FillStores()", ex);
                throw;
            }
        }

        //private void FillStatus()
        //{
        //    try
        //    {
        //        MySQLHelper tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
        //        List<string> ListStatus = tmpMySQLHelper.GetAllStatus();
        //        cmbxSelectStatus.Items.Add("Select Status");
        //        foreach (var item in ListStatus)
        //        {
        //            //cmbxSelectRoleID.Items.Add(item.Cast<object>());
        //            cmbxSelectStatus.Items.Add(item);
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtFullName.Clear();
                txtPhone.Clear();
                txtEmailID.Clear();
                cmbxSelectRoleID.SelectedIndex = 0;
                cmbxSelectStore.SelectedIndex = 0;
                lblCommonErrorMsg.Visible = false;               
                //txtAddress.Clear();
            }

            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditUserForm.btnReset_Click()", ex);
                throw;
            }
        }
        private void btnEditUser_Click(object sender, EventArgs e)
        {
            try
            {

                if (cmbxSelectRoleID.Text == "Select Role")
                {
                    //MessageBox.Show("Select a Role for the User!! ");
                    lblCommonErrorMsg.Visible = true;
                    lblCommonErrorMsg.Text = "Select a Role for the User!! ";
                    return;
                }
                //if (cmbxSelectStore.Text == "Select Store")    
                //{
                //    //MessageBox.Show("Select a Role for the User!! ");
                //    lblCommonErrorMsg.Visible = true;
                //    lblCommonErrorMsg.Text = "Select a Store for the User!! ";
                //    return;
                //}
                if (txtFullName.Text.Trim() == string.Empty)
                {
                    lblCommonErrorMsg.Visible = true;
                    lblCommonErrorMsg.Text = "Full Name cannot be empty! ";
                    return;
                }
                if (txtEmailID.Text.Trim() != string.Empty)
                {
                    if (!CheckForValidEmailID()) return;
                }
                if (txtPhone.Text.Trim() != string.Empty)
                {
                    if (!CheckForValidPhoneNo()) return;
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
                List<string> ListColumnNames = new List<string>();
                ListColumnValues.Add(txtFullName.Text);
                ListColumnNames.Add("FULLNAME");
                //if (txtEmailID.Text.Trim()!=string.Empty)
                //{
                ListColumnValues.Add(txtEmailID.Text);
                ListColumnNames.Add("EMAILID");
                //}
                //if (txtPhone.Text.Trim() != string.Empty)
                //{
                ListColumnNames.Add("PHONENO");
                ////ListColumnValues.Add("'" + txtPhone.Text + "'");
                ListColumnValues.Add(txtPhone.Text.Trim() == string.Empty ? "NULL" : txtPhone.Text.Trim());
                //}
                ListColumnNames.Add("ACTIVE");

                if (rdbtnActiveNo.Checked == true)
                {
                    ListColumnValues.Add("0");
                }
                else ListColumnValues.Add("1");

                ListColumnValues.Add(DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));
                ListColumnNames.Add("LASTUPDATEDATE");
                //if (cmbxSelectStore.Text != "Select Store")
                //{
                ListColumnNames.Add("STOREID");
                int storeID = CommonFunctions.ObjUserMasterModel.GetStoreID(cmbxSelectStore.SelectedItem.ToString());
                ListColumnValues.Add(storeID == -1 ? "NULL" : storeID.ToString());
                //}


                string WhereCondition = "USERNAME = '" + txtUserName.Text + "'";
                tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                int ResultVal = CommonFunctions.ObjUserMasterModel.UpdateAnyTableDetails("USERMASTER", ListColumnNames, ListColumnValues, WhereCondition);
                //tmpMySQLHelper.EditNewUser(txtEditUserName.Text, txtEditPassword.Text, txtFullName.Text, (chkbxActiveYes.Checked == true ? "YES" : "NO"), cmbxSelectRoleID.Text, ListColumnNamesWithDataType, ListColumnValues);
                if (ResultVal < 0) MessageBox.Show("Wasnt able to Edit the user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //else if (ResultVal == 2)
                //{
                //    MessageBox.Show("User Name already exists! Pls Provide Another User Name.");
                //}
                else
                {
                    MessageBox.Show("Updated User Details :: " + txtUserName.Text + " successfully", "Update User Details");
                    UpdateOnClose(Mode: 1);
                    // if(ObjManageUsers!=null) ObjManageUsers.BindGrid();
                    //btnReset.PerformClick();
                }
            }

            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditUserForm.btnEditUser_Click()", ex);
                throw;
            }
        }



        //private void txtEmailID_Leave(object sender, EventArgs e)
        //{
        //    bool IsValid = false;
        //    if (txtEmailID.Text == "") IsValid = true;
        //    else IsValid = CommonFunctions.ValidateEmail(txtEmailID.Text);
        //    if (!IsValid)
        //    {
        //        lblEditEmailIdValidMsg.Visible = true;
        //        lblEditEmailIdValidMsg.Text = "Invalid EmailID!";
        //        txtEmailID.Focus();
        //        btnEditUser.Enabled = false;
        //    }
        //    else
        //    {
        //        lblEditEmailIdValidMsg.Visible = false;
        //        btnEditUser.Enabled = true;
        //        return;
        //    }
        //}

        private bool CheckForValidEmailID()
        {
            try
            {
                bool IsValid = false;
                if (txtEmailID.Text.Trim() == "") IsValid = true;
                else IsValid = CommonFunctions.ValidateEmail(txtEmailID.Text);
                if (!IsValid)
                {
                    lblCommonErrorMsg.Visible = true;
                    lblCommonErrorMsg.Text = "Invalid EmailID!";
                    txtEmailID.Focus();
                    //return;
                    // btnEditUser.Enabled = false;
                }
                else
                {
                    lblCommonErrorMsg.Visible = false;
                    //btnEditUser.Enabled = true;
                    //return;
                }
                return IsValid;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditUserForm.CheckForValidEmailID()", ex);
                throw;
            }
        }

        //private void txtPhone_Leave(object sender, EventArgs e)
        //{
        //    bool IsValid = false;
        //    if (txtPhone.Text == "") IsValid = true;
        //    else IsValid = CommonFunctions.ValidatePhoneNo(txtPhone.Text);
        //    if (!IsValid)
        //    {
        //        lblEditPhoneValidMsg.Visible = true;
        //        lblEditPhoneValidMsg.Text = "Enter Valid Phone No!"; ;
        //        txtPhone.Focus();
        //        btnEditUser.Enabled = false;
        //    }
        //    else
        //    {
        //        lblEditPhoneValidMsg.Visible = false;
        //        btnEditUser.Enabled = true;
        //        return;
        //    }
        //}
        private bool CheckForValidPhoneNo()
        {
            try
            {
                bool IsValid = false;
                if (txtPhone.Text.Trim() == "") IsValid = true;
                else IsValid = CommonFunctions.ValidatePhoneNo(txtPhone.Text);
                if (!IsValid)
                {
                    lblCommonErrorMsg.Visible = true;
                    lblCommonErrorMsg.Text = "Enter Valid Phone No!"; ;
                    txtPhone.Focus();
                    // btnEditUser.Enabled = false;
                }
                else
                {
                    lblCommonErrorMsg.Visible = false;
                    //btnEditUser.Enabled = true;
                    //return;
                }
                return IsValid;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditUserForm.CheckForValidPhoneNo()", ex);
                throw;
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
                CommonFunctions.ShowErrorDialog("EditUserForm.cmbxSelectRoleID_Click()", ex);
                throw;
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
                CommonFunctions.ShowErrorDialog("EditUserForm.cmbxSelectStore_Click()", ex);
                throw;
            }
        }

        private void EditUserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //UpdateOnClose(Mode: 1);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditUserForm.EditUserForm_FormClosed()", ex);
                throw;
            }
        }

        private void cmbxSelectStore_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblEditStoreName_Click(object sender, EventArgs e)
        {

        }
    }
}
