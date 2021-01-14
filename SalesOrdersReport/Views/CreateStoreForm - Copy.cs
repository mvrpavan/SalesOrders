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
    public partial class EditStoreForm : Form
    {

        MySQLHelper tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
        UpdateOnCloseDel UpdateOnClose = null;
        public EditStoreForm(UpdateOnCloseDel UpdateOnClose)
        {
            InitializeComponent();
            txtCreateStoreName.Focus();
            this.UpdateOnClose = UpdateOnClose;
            this.FormClosed += CreateStoreForm_FormClosed;
        }
    

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtCreateStoreName.Clear();
            txtStoreAddress.Clear();
            txtStoreExecutiveName.Clear();
            txtStoreExcutivePhone.Clear();
            //txtAddress.Clear();
            txtCreateStoreName.Focus();
        }
        private void btnEditStore_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCreateStoreName.Text.Trim() == string.Empty)
                {
                    //MessageBox.Show("User Name Or Password Cannot be empty ");
                    lblCreateStoreValidMsg.Visible = true;
                    lblCreateStoreValidMsg.Text = "Store Name Cannot be empty!";
                    return;
                }
      

                //if (txtStoreExceutiveName.Text.Trim() == string.Empty)
                //{
                //    lblCommonErrorMsg.Visible = true;
                //    lblCommonErrorMsg.Text = "Full Name cannot be empty! ";
                //    return;
                //}

                List<string> ListColumnValues = new List<string>();
                List<string> ListColumnNamesWithDataType = new List<string>();

                if (txtStoreAddress.Text.Trim() != string.Empty)
                {
                    ListColumnValues.Add(txtStoreAddress.Text);
                    ListColumnNamesWithDataType.Add("STOREADDRESS,VARCHAR");
                }
                if (txtStoreExecutiveName.Text.Trim() == string.Empty)
                {
                    ListColumnValues.Add(txtStoreExecutiveName.Text);
                    ListColumnNamesWithDataType.Add("STOREEXECUTIVE,VARCHAR");
                }
                if (txtStoreExcutivePhone.Text.Trim() != string.Empty)
                {
                    if (!CheckForValidPhone()) return;
                    ListColumnValues.Add(txtStoreExcutivePhone.Text);
                    ListColumnNamesWithDataType.Add("PHONENO,BIGINT");

                }

                int ResultVal = CommonFunctions.ObjUserMasterModel.CreateNewStore(txtCreateStoreName.Text,ListColumnNamesWithDataType, ListColumnValues);
                //int ResultVal = 0;
                if (ResultVal <= 0) MessageBox.Show("Wasnt able to create the store", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (ResultVal == 2)
                {
                    MessageBox.Show("Store Name already exists! Pls Provide Another Store Name.", "Error");
                }
                else
                {
                    MessageBox.Show("Added New Store :: " + txtCreateStoreName.Text + " successfully", "Added Store");
                    btnReset.PerformClick();
                    //if (ObjManageUsers != null) ObjManageUsers.BindGrid();//&&&&&
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //private void txtCreatePassword_Leave(object sender, EventArgs e)
        //{
        //    bool IsValid = false;
        //    if (txtCreatePassword.Text == "") IsValid = true;
        //    else IsValid = CommonFunctions.CheckForPasswordLength(txtCreatePassword.Text);
        //    if (!IsValid)
        //    {
        //        lblCreatePwdValidMsg.Visible = true;
        //        lblCreatePwdValidMsg.Text = "Password length should be of 5 to 20 characters";
        //        txtCreatePassword.Focus();
        //        btnCreateUser.Enabled = false;
        //    }
        //    else
        //    {
        //        lblCreatePwdValidMsg.Visible = false;
        //        btnCreateUser.Enabled = true;
        //        return;
        //    }
        //}
        private bool CheckForValidPwd()
        {
            bool IsValid = CommonFunctions.CheckForPasswordLength(txtStoreAddress.Text);
            if (!IsValid)
            {
                lblCreateStoreValidMsg.Visible = true;
                lblCreateStoreValidMsg.Text = "Password length should be of 5 to 20 characters";
                txtStoreAddress.Focus();
                // btnCreateUser.Enabled = false;
            }
            else
            {
                lblCreateStoreValidMsg.Visible = false;
                //btnCreateUser.Enabled = true;
                //return;
            }
            return IsValid;
        }

        //private void txtEmailID_Leave(object sender, EventArgs e)
        //{
        //    bool IsValid = false;
        //    if (txtEmailID.Text == "") IsValid = true;
        //    else IsValid = CommonFunctions.ValidateEmail(txtEmailID.Text);
        //    if (!IsValid)
        //    {
        //        lblCreateEmailIdValidMsg.Visible = true;
        //        lblCreateEmailIdValidMsg.Text = "Invalid EmailID!";
        //        txtEmailID.Focus();
        //        btnCreateUser.Enabled = false;
        //    }
        //    else
        //    {
        //        lblCreateEmailIdValidMsg.Visible = false;
        //        btnCreateUser.Enabled = true;
        //        return;
        //    }
        //}


        //private void txtPhone_Leave(object sender, EventArgs e)
        //{
        //    bool IsValid = false;
        //    if (txtPhone.Text == "") IsValid = true;
        //    else IsValid = CommonFunctions.ValidatePhoneNo(txtPhone.Text);
        //    if (!IsValid)
        //    {
        //        lblCreatePhoneValidMsg.Visible = true;
        //        lblCreatePhoneValidMsg.Text = "Enter Valid Phone No!"; ;
        //        txtPhone.Focus();
        //        btnCreateUser.Enabled = false;
        //    }
        //    else
        //    {
        //        lblCreatePhoneValidMsg.Visible = false;
        //        btnCreateUser.Enabled = true;
        //        return;
        //    }
        //}

        private bool CheckForValidPhone()
        {
            bool IsValid = IsValid = CommonFunctions.ValidatePhoneNo(txtStoreExcutivePhone.Text);
            if (!IsValid)
            {
                lblCreateExecutivePhoneValidMsg.Visible = true;
                lblCreateExecutivePhoneValidMsg.Text = "Enter Valid Phone No!"; ;
                txtStoreExcutivePhone.Focus();
                // btnCreateUser.Enabled = false;
            }
            else
            {
                lblCreateExecutivePhoneValidMsg.Visible = false;
                //btnCreateUser.Enabled = true;
                //return;
            }
            return IsValid;
        }





        private void EditStoreForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                UpdateOnClose(Mode: 1);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateUserForm.CreateStoreForm_FormClosed()", ex);
            }
        }
    }
}
