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
    public partial class CreateStoreForm : Form
    {

        MySQLHelper tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
        UpdateOnCloseDel UpdateOnClose = null;
        public CreateStoreForm(UpdateOnCloseDel UpdateOnClose)
        {
            try
            {
                InitializeComponent();
                txtCreateStoreName.Focus();
                this.UpdateOnClose = UpdateOnClose;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateStoreForm.CreateStoreForm()", ex);
                throw ex;
            }
        }


        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtCreateStoreName.Clear();
                txtStoreAddress.Clear();
                txtStoreExecutiveName.Clear();
                txtStoreExcutivePhone.Clear();
                //txtAddress.Clear();
                txtCreateStoreName.Focus();
                lblCreateStoreCommonValidMsg.Visible = false;
                //lblCreateStoreValidMsg.Visible = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateStoreForm.btnReset_Click()", ex);
                throw ex;
            }
        }
        private void btnCreateStore_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCreateStoreName.Text.Trim() == string.Empty)
                {
                    lblCreateStoreCommonValidMsg.Visible = true;
                    lblCreateStoreCommonValidMsg.Text = "Store Name Cannot be empty!";
                    return;
                }

                List<string> ListColumnValues = new List<string>();
                List<string> ListColumnNamesWithDataType = new List<string>();

                if (txtStoreAddress.Text.Trim() != string.Empty)
                {
                    ListColumnValues.Add(txtStoreAddress.Text);
                    ListColumnNamesWithDataType.Add("ADDRESS,VARCHAR");
                }
                if (txtStoreExecutiveName.Text.Trim() != string.Empty)
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

                int ResultVal = CommonFunctions.ObjUserMasterModel.CreateNewStore(txtCreateStoreName.Text, ListColumnNamesWithDataType, ListColumnValues);
                if (ResultVal <= 0) MessageBox.Show("Wasnt able to create the store", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (ResultVal == 2)
                {
                    MessageBox.Show("Store Name already exists! Pls Provide Another Store Name.", "Error");
                }
                else
                {
                    MessageBox.Show("Added New Store :: " + txtCreateStoreName.Text + " successfully", "Added Store");
                    UpdateOnClose(Mode: 2);
                    btnReset.PerformClick();
                }

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateStoreForm.btnCreateStore_Click()", ex);
                throw;
            }
        }


        private bool CheckForValidPhone()
        {
            try
            {
                bool IsValid = IsValid = CommonFunctions.ValidatePhoneNo(txtStoreExcutivePhone.Text);
                if (!IsValid)
                {
                    lblCreateStoreCommonValidMsg.Visible = true;
                    lblCreateStoreCommonValidMsg.Text = "Enter Valid Phone No!"; ;
                    txtStoreExcutivePhone.Focus();
                }
                else
                {
                    lblCreateStoreCommonValidMsg.Visible = false;
                }
                return IsValid;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateStoreForm.CheckForValidPhone()", ex);
                throw;
            }
        }
    }
}
