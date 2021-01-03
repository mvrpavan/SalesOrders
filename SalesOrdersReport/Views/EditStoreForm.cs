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
            FillStores();
            cmbxAllStoreNames.SelectedIndex = 0;
            cmbxAllStoreNames.Focus();
            this.UpdateOnClose = UpdateOnClose;
            this.FormClosed += EditStoreForm_FormClosed;
        }

        private void FillStores()
        {
            try
            {
                tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                List<string> ListStores = CommonFunctions.ObjUserMasterModel.GetAllStores();
                cmbxAllStoreNames.Items.Add("Select Store");
                foreach (var item in ListStores)
                {
                    //cmbxSelectRoleID.Items.Add(item.Cast<object>());
                    cmbxAllStoreNames.Items.Add(item);
                }
            }

            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditStoreForm.FillStores()", ex);
                throw;
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                cmbxAllStoreNames.SelectedIndex = 0;
                txtEditStoreAddress.Clear();
                txtEditStoreExecutiveName.Clear();
                txtEditStoreExcutivePhone.Clear();
                cmbxAllStoreNames.Focus();
                lblEditStoreCommonValidMsg.Visible = false;
                //lblEditStoreValidMsg.Visible = false;
               
            }

            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditStoreForm.btnReset_Click()", ex);
                throw;
            }
        }
        private void btnEditStore_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbxAllStoreNames.SelectedIndex == 0)
                {
                    //MessageBox.Show("User Name Or Password Cannot be empty ");
                    lblEditStoreCommonValidMsg.Visible = true;
                    lblEditStoreCommonValidMsg.Text = "Choose a Store to Edit!";
                    return;
                }


                //if (txtStoreExceutiveName.Text.Trim() == string.Empty)
                //{
                //    lblCommonErrorMsg.Visible = true;
                //    lblCommonErrorMsg.Text = "Full Name cannot be empty! ";
                //    return;
                //}

                List<string> ListColumnValues = new List<string>();
                List<string> ListColumnNames = new List<string>();

                //if (txtEditStoreAddress.Text.Trim() != string.Empty)
                //{
                ListColumnValues.Add(txtEditStoreAddress.Text);
                ListColumnNames.Add("ADDRESS");
                // }
                //if (txtEditStoreExecutiveName.Text.Trim() != string.Empty)
                //{
                ListColumnValues.Add(txtEditStoreExecutiveName.Text);
                ListColumnNames.Add("STOREEXECUTIVE");
                //}
                if (txtEditStoreExcutivePhone.Text.Trim() != string.Empty)
                {
                    if (!CheckForValidPhone()) return;
                    //ListColumnValues.Add(txtEditStoreExcutivePhone.Text.Trim() == string.Empty ? "NULL" : txtEditStoreExcutivePhone.Text.Trim());
                    //ListColumnNames.Add("PHONENO");

                }
                ListColumnValues.Add(txtEditStoreExcutivePhone.Text.Trim() == string.Empty ? "NULL" : txtEditStoreExcutivePhone.Text.Trim());
                ListColumnNames.Add("PHONENO");
                ListColumnNames.Add("LASTUPDATEDATE");
                ListColumnValues.Add(DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));
                string WhereCondition = "STORENAME = '" + cmbxAllStoreNames.SelectedItem + "'";
                int ResultVal = CommonFunctions.ObjUserMasterModel.UpdateAnyTableDetails("STOREMASTER", ListColumnNames, ListColumnValues, WhereCondition);
                //int ResultVal = 0;
                if (ResultVal < 0) MessageBox.Show("Wasnt able to create the store", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (ResultVal == 2)
                {
                    MessageBox.Show("Store Name already exists! Pls Provide Another Store Name.", "Error");
                }
                else
                {
                    MessageBox.Show("Edited New Store :: " + cmbxAllStoreNames.SelectedItem + " successfully", "Edit Store");
                    UpdateOnClose(1);
                    btnReset.PerformClick();
                    //if (ObjManageUsers != null) ObjManageUsers.BindGrid();//&&&&&

                }

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditStoreForm.btnEditStore_Click()", ex);
                throw;
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
            try
            {
                bool IsValid = IsValid = CommonFunctions.ValidatePhoneNo(txtEditStoreExcutivePhone.Text);
                if (!IsValid)
                {
                    lblEditStoreCommonValidMsg.Visible = true;
                    lblEditStoreCommonValidMsg.Text = "Enter Valid Phone No!"; ;
                    txtEditStoreExcutivePhone.Focus();
                    // btnCreateUser.Enabled = false;
                }
                else
                {
                    lblEditStoreCommonValidMsg.Visible = false;
                    //btnCreateUser.Enabled = true;
                    //return;
                }
                return IsValid;
            }

            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditStoreForm.CheckForValidPhone()", ex);
                throw;
            }
        }





        private void EditStoreForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                UpdateOnClose(Mode: 1);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditStoreForm.EditStoreForm_FormClosed()", ex);
            }
        }

        private void cmbxAllStoreNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBox = (ComboBox)sender;
                if (comboBox.SelectedIndex != 0)
                {
                    string StoreName = (string)comboBox.SelectedItem;
                    StoreDetails ObjStoreDetails = CommonFunctions.ObjUserMasterModel.GetStoreDetails(StoreName);
                    txtEditStoreAddress.Text = ObjStoreDetails.Address;
                    txtEditStoreExecutiveName.Text = ObjStoreDetails.StoreExecutive;
                    txtEditStoreExcutivePhone.Text = ObjStoreDetails.PhoneNo == 0 ? "" : ObjStoreDetails.PhoneNo.ToString();
                }
            }

            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditStoreForm.cmbxAllStoreNames_SelectedIndexChanged()", ex);
                throw;
            }
        }
    }
}
