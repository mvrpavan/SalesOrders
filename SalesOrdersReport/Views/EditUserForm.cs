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
       MySQLHelper tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
        public EditUserForm()
        {
            InitializeComponent();
            FillUsers();
            FillRoles();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtEditPwd.Clear();
            FillUsers();
            FillRoles();

            chckDeleteUser.Checked = false;
            cmbxAllUserForEdit.SelectedIndex = 0;
            cmbxAllRoles.SelectedIndex = 0;
            //txtEditPwd.Focus();
        }

        private void FillUsers()
        {
            try
            {
               
                List<string> ListRoles = tmpMySQLHelper.GetAllUsers();
                cmbxAllUserForEdit.Items.Add("Select User");
                foreach (var item in ListRoles)
                {
                    //cmbxSelectRoleID.Items.Add(item.Cast<object>());
                    cmbxAllUserForEdit.Items.Add(item);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void FillRoles()
        {
            try
            {
                //MySQLHelper tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                List<string> ListRoles = tmpMySQLHelper.GetAllRoles();
                cmbxAllRoles.Items.Add("Select Role");
                foreach (var item in ListRoles)
                {
                    //cmbxAllRoles.Items.Add(item.Cast<object>());
                    cmbxAllRoles.Items.Add(item);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            List<string> ListColumnName = new List<string>();
            List<string> ListColumnValues = new List<string>();

            if (cmbxAllUserForEdit.SelectedIndex <= 0)
            {
                MessageBox.Show("Please Select an user");
                return;
            }
            if (txtEditPwd.Text != "")
            {
                ListColumnName.Add("PASSWORD");
                ListColumnValues.Add(txtEditPwd.Text);
            }
            if (cmbxAllRoles.SelectedIndex != 0 || cmbxAllRoles.SelectedIndex!=-1)
            {
                ListColumnName.Add("ROLE");
                ListColumnValues.Add(cmbxAllRoles.Text);
            }
            string WhereCondition = "USERNAME = '" + cmbxAllUserForEdit.SelectedText + "'";
            if (ListColumnName.Count > 0)
            {
                //MySQLHelper tmpMySQlHelper = MySQLHelper.GetMySqlHelperObj();
                int ResulVal = tmpMySQLHelper.UpdateAnyTableDetails("USERS", ListColumnName, ListColumnValues, WhereCondition);
                if (ResulVal <= 0) MessageBox.Show("Wasnt able to update the user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else MessageBox.Show("Updated User :: " + cmbxAllUserForEdit.Text + " details successfully");
            }
        }

        private void chckDeleteUser_CheckedChanged(object sender, EventArgs e)
        {
            if (chckDeleteUser.Checked == true )
            {
                if (cmbxAllUserForEdit.SelectedIndex <= 0)
                {
                    MessageBox.Show("Please select user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    chckDeleteUser.Checked = false;
                    return;
                }
                else
                {
                    var Result = MessageBox.Show("Are you sure to delete the user", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    // If the no button was pressed ...
                    if (Result == DialogResult.Yes)
                    {
                        // cancel the closure of the form.
                        //MySQLHelper tmpMySQlHelper = MySQLHelper.GetMySqlHelperObj();
                        int ResulVal = tmpMySQLHelper.DeleteRow("USERS", "USERNAME", cmbxAllUserForEdit.Text.ToString());
                        if (ResulVal <= 0) MessageBox.Show("Wasnt able to delete the user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else 
                        {
                            MessageBox.Show("Deleted User :: " + cmbxAllUserForEdit.Text + "  successfully");
                            FillRoles();
                            cmbxAllUserForEdit.SelectedIndex = 0;
                            cmbxAllRoles.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        txtEditPwd.Focus();
                    }
                    chckDeleteUser.Checked = false;
                  
                }
            }

        }
    }
}
