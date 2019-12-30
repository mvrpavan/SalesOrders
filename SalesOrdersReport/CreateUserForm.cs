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
    public partial class CreateUserForm : Form
    {
        public CreateUserForm()
        {
            InitializeComponent();
            cmbxSelectRoleID.Items.Clear();
            FillRoles();
            cmbxSelectRoleID.SelectedIndex = 0;
        }
        private void FillRoles()
        {
            try
            {
                MySQLHelper tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                List<string> ListRoles = tmpMySQLHelper.GetAllRoles();
                cmbxSelectRoleID.Items.Add("Select Role");
                foreach (var item in ListRoles)
                {
                    //cmbxSelectRoleID.Items.Add(item.Cast<object>());
                    cmbxSelectRoleID.Items.Add(item);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtCreateUserName.Clear();
            txtCreatePassword.Clear();
            txtFullName.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtCreateUserName.Focus();
        }
        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            if (txtCreateUserName.Text == string.Empty || txtCreatePassword.Text == string.Empty)
            {
                MessageBox.Show("User Name Or Password Cannot be empty ");
                return;
            }
            if (cmbxSelectRoleID.Text == "Select Role")
            {
                MessageBox.Show("Select a Role for the User!! ");
                return;
            }
            MySQLHelper tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
            int ResultVal = tmpMySQLHelper.CreateNewUser(txtCreateUserName.Text, txtCreatePassword.Text, txtPhone.Text, txtAddress.Text, cmbxSelectRoleID.Text);
            if (ResultVal <= 0) MessageBox.Show("Wasnt able to create the user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (ResultVal == 2) { }
            else MessageBox.Show("Added new User :: " + txtCreateUserName.Text + " successfully");
        }
    }
}
