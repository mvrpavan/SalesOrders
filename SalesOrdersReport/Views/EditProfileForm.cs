using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SalesOrdersReport
{
    public partial class EditProfileForm : Form
    {
        MySQLHelper tmpMySQLHelper;
        //public EditProfileForm(UpdateOnCloseDel UpdateOnClose)
        public EditProfileForm()
        {
            InitializeComponent();
            //this.UpdateOnClose = UpdateOnClose;
            //this.FormClosed += EditProfileForm_FormClosed;
            tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
            txtOldPwd.Focus();
            //&&&&& get email id and phone into the text box
        }

        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> ListColumnNames = new List<string>();
                List<string> ListColumnValues = new List<string>();

                if (txtOldPwd.Text != "")
                {
                    if (!CheckForOldPwd()) return;

                    if (!CheckForValidNewPwd()) return;

                    if (!CheckForValidConfirmNwPwd()) return;

                    ListColumnNames.Add("PASSWORD");
                    Guid UserGuid = System.Guid.NewGuid();
                    ListColumnValues.Add(CommonFunctions.GetHashedPassword(txtbxNewPwd.Text, UserGuid));
                    ListColumnNames.Add("USERGUID");
                    ListColumnValues.Add(UserGuid.ToString());
                    ListColumnNames.Add("LASTPASSWORDCHANGED");
                    ListColumnValues.Add(DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));
                    ListColumnNames.Add("LASTUPDATEDATE");
                    ListColumnValues.Add(DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));


                }
                if (txtbxEmailID.Text != "")
                {
                    if (!CheckForValidEmailID()) return;
                    //ListColumnName.Add("EMAILID");
                    //ListColumnValues.Add(txtbxEmailID.Text);
                }
                if (txtChangePhone.Text != "")
                {
                    if (!CheckForValidChangePhone()) return;
                    //ListColumnName.Add("PHONENO");            
                    //ListColumnValues.Add(txtChangePhone.Text.Trim() == string.Empty ? "NULL" : txtChangePhone.Text.Trim());
                }
                ListColumnNames.Add("EMAILID");
                ListColumnValues.Add(txtbxEmailID.Text);
                ListColumnNames.Add("PHONENO");
                ListColumnValues.Add(txtChangePhone.Text.Trim() == string.Empty ? "NULL" : txtChangePhone.Text.Trim());
                if (ListColumnNames.Count > 0)
                {
                    tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                    string WhereCondition = "USERNAME = '" + tmpMySQLHelper.CurrentUser + "'";
                    int ResulVal = CommonFunctions.ObjUserMasterModel.UpdateAnyTableDetails("USERMASTER", ListColumnNames, ListColumnValues, WhereCondition);
                    if (ResulVal < 0) MessageBox.Show("Wasnt able to update the user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        CommonFunctions.ObjUserMasterModel.LoadAllUserMasterTables();
                        MessageBox.Show("Updated your details successfully", "Update Complete");
                        //CommonFunctions.ObjUserMasterModel.LoadAllUserMasterTables();
                        //btnReset.PerformClick();
                        txtOldPwd.Clear();
                        txtbxNewPwd.Clear();
                        txtbxConfirmNwPwd.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditProfileForm.btnEditProfile_Click()", ex);
                throw;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtOldPwd.Clear();
                txtbxNewPwd.Clear();
                txtbxConfirmNwPwd.Clear();
                txtChangePhone.Clear();
                txtbxEmailID.Clear();
                txtOldPwd.Focus();
                lblCnfrmPwdMsg.Visible = false;
                lblEmailValidMsg.Visible = false;
                lblNwPwdMsg.Visible = false;
                lblOldPwdMsg.Visible = false;
                lblPhoneMsg.Visible = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditProfileForm.btnReset_Click()", ex);
                throw;
            }
        }


        //private void txbxEmailID_Leave(object sender, EventArgs e)
        //{
        //    bool IsValid = false;
        //    if (txtbxEmailID.Text == "") IsValid = true;
        //    else IsValid = CommonFunctions.ValidateEmail(txtbxEmailID.Text);
        //    if (!IsValid)
        //    {
        //        lblEmailValidMsg.Visible = true;
        //        lblEmailValidMsg.Text = "Invalid EmailID!";
        //        txtbxEmailID.Focus();
        //        btnEditProfile.Enabled = false;
        //    }
        //    else
        //    {
        //        lblEmailValidMsg.Visible = false;
        //        btnEditProfile.Enabled = true;
        //        return;
        //    }
        //}
        private bool CheckForValidEmailID()
        {
            try
            {
                bool IsValid = false;
                if (txtbxEmailID.Text == "") IsValid = true;
                else IsValid = CommonFunctions.ValidateEmail(txtbxEmailID.Text);
                if (!IsValid)
                {
                    lblEmailValidMsg.Visible = true;
                    lblEmailValidMsg.Text = "Invalid EmailID!";
                    txtbxEmailID.Focus();
                    //btnEditProfile.Enabled = false;
                }
                else
                {
                    lblEmailValidMsg.Visible = false;
                    // btnEditProfile.Enabled = true;
                    // return;
                }
                return IsValid;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditProfileForm.CheckForValidEmailID()", ex);
                throw;
            }
        }

        //private void txtbxNewPwd_Leave(object sender, EventArgs e)
        //{

        //    bool IsValid = false;
        //    if (txtbxNewPwd.Text == "") IsValid = true;
        //    else IsValid = CommonFunctions.CheckForPasswordLength(txtbxNewPwd.Text);
        //    if (!IsValid)
        //    {
        //        lblNwPwdMsg.Visible = true;
        //        lblNwPwdMsg.Text = "Password length should be of 5 to 20 characters";
        //        txtbxNewPwd.Focus();
        //        btnEditProfile.Enabled = false;
        //    }
        //    else
        //    {
        //        lblNwPwdMsg.Visible = false;
        //        btnEditProfile.Enabled = true;
        //        return;
        //    }
        //}
        private bool CheckForValidNewPwd()
        {
            try
            {

                bool IsValid = false;
                //if (txtbxNewPwd.Text == "")
                //{
                //    //IsValid = true;
                //}
                //else 
                 IsValid = CommonFunctions.CheckForPasswordLength(txtbxNewPwd.Text);
                if (!IsValid)
                {
                    lblNwPwdMsg.Visible = true;
                    lblNwPwdMsg.Text = "Password length should be of 5 to 20 characters";
                    txtbxNewPwd.Focus();
                    //btnEditProfile.Enabled = false;
                }
                else
                {
                    lblNwPwdMsg.Visible = false;
                    //btnEditProfile.Enabled = true;
                    //return;
                }
                return IsValid;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditProfileForm.CheckForValidNewPwd()", ex);
                throw;
            }
        }

        //private void txtbxConfirmNwPwd_Leave(object sender, EventArgs e)
        //{
        //    bool IsValid = false;
        //    if (txtbxConfirmNwPwd.Text == "") IsValid = true;
        //    else IsValid = CommonFunctions.CompareNwPwdConfrmPwd(txtbxNewPwd.Text, txtbxConfirmNwPwd.Text);
        //    if (!IsValid)
        //    {
        //        lblCnfrmPwdMsg.Visible = true;
        //        lblCnfrmPwdMsg.Text = "Doesnt match with New Password!"; ;
        //        txtbxConfirmNwPwd.Focus();
        //        btnEditProfile.Enabled = false;
        //    }
        //    else
        //    {
        //        lblCnfrmPwdMsg.Visible = false;
        //        btnEditProfile.Enabled = true;
        //        return;
        //    }

        //}
        private bool CheckForValidConfirmNwPwd()
        {
            try
            {
                bool IsValid = false;
                //if (txtbxConfirmNwPwd.Text == "")
                //{
                //    //IsValid = true;
                //}
                //else
                IsValid = CommonFunctions.CompareNwPwdConfrmPwd(txtbxNewPwd.Text, txtbxConfirmNwPwd.Text);
                if (!IsValid)
                {
                    lblCnfrmPwdMsg.Visible = true;
                    lblCnfrmPwdMsg.Text = "Doesnt match with New Password!"; 
                    txtbxConfirmNwPwd.Focus();
                    //btnEditProfile.Enabled = false;
                }
                else
                {
                    lblCnfrmPwdMsg.Visible = false;
                    //btnEditProfile.Enabled = true;
                    //return;
                }
                return IsValid;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditProfileForm.CheckForValidConfirmNwPwd()", ex);
                throw;
            }
        }

        //private void txtChangePhone_Leave(object sender, EventArgs e)
        //{
        //    bool IsValid = false;
        //    if (txtChangePhone.Text == "") IsValid = true;
        //    else IsValid = CommonFunctions.ValidatePhoneNo(txtChangePhone.Text);
        //    if (!IsValid)
        //    {
        //        lblPhoneMsg.Visible = true;
        //        lblPhoneMsg.Text = "Enter Valid Phone No!"; ;
        //        txtChangePhone.Focus();
        //        btnEditProfile.Enabled = false;
        //    }
        //    else
        //    {
        //        lblPhoneMsg.Visible = false;
        //        btnEditProfile.Enabled = true;
        //        return;
        //    }
        //}

        private bool CheckForValidChangePhone()
        {
            try
            {
                bool IsValid = false;
                if (txtChangePhone.Text == "") IsValid = true;
                else IsValid = CommonFunctions.ValidatePhoneNo(txtChangePhone.Text);
                if (!IsValid)
                {
                    lblPhoneMsg.Visible = true;
                    lblPhoneMsg.Text = "Enter Valid Phone No!"; ;
                    txtChangePhone.Focus();
                    //btnEditProfile.Enabled = false;
                }
                else
                {
                    lblPhoneMsg.Visible = false;
                    //btnEditProfile.Enabled = true;
                    //return;
                }
                return IsValid;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditProfileForm.CheckForValidChangePhone()", ex);
                throw;
            }
        }
        //private void txtOldPwd_Leave(object sender, EventArgs e)
        //{
        //    bool IsValid = false;
        //    if (txtOldPwd.Text == "") IsValid = true;
        //    else IsValid = CommonFunctions.ValidateOldPassword(txtOldPwd.Text);
        //    if (!IsValid)
        //    {
        //        lblOldPwdMsg.Visible = true;
        //        lblOldPwdMsg.Text = "Wrong Old Password!"; ;
        //        txtOldPwd.Focus();
        //        btnEditProfile.Enabled = false;
        //    }
        //    else
        //    {
        //        lblOldPwdMsg.Visible = false;
        //        btnEditProfile.Enabled = true;
        //        return;
        //    }
        //}
        private bool CheckForOldPwd()
        {
            try
            {
                bool IsValid = false;
                if (txtOldPwd.Text == "") IsValid = true;
                else IsValid = CommonFunctions.ObjUserMasterModel.ValidateOldPassword(txtOldPwd.Text);
                if (!IsValid)
                {
                    lblOldPwdMsg.Visible = true;
                    lblOldPwdMsg.Text = "Wrong Old Password!"; ;
                    txtOldPwd.Focus();
                    //btnEditProfile.Enabled = false;
                }
                else
                {
                    lblOldPwdMsg.Visible = false;
                    //btnEditProfile.Enabled = true;
                    //return;
                }
                return IsValid;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditProfileForm.CheckForOldPwd()", ex);
                throw;
            }
        }

        private void EditProfileForm_Load(object sender, EventArgs e)
        {
            try
            {
                UserDetails ObjUserDetails = CommonFunctions.ObjUserMasterModel.GetUserDtlsObjBasedOnUsrName(tmpMySQLHelper.CurrentUser);

                txtbxEmailID.Text = ObjUserDetails.EmailID;
                txtChangePhone.Text = ObjUserDetails.PhoneNo == 0 ? "" : ObjUserDetails.PhoneNo.ToString();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditProfileForm.EditProfileForm_Load()", ex);
                throw;
            }
        }


        //private void EditProfileForm_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    try
        //    {
        //        UpdateOnClose(Mode: 1);
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonFunctions.ShowErrorDialog("EditProfileForm.EditProfileForm_FormClosed()", ex);
        //    }
        //}
    }
}
