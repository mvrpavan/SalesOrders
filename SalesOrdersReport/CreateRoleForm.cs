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
    public partial class CreateRoleForm : Form
    {
        public CreateRoleForm()
        {
            InitializeComponent();
            txtCreateNewRole.Focus();
        }

        private void btnCreateRole_Click(object sender, EventArgs e)
        {
            if (txtCreateNewRole.Text == "")
            {
                MessageBox.Show("Please enter role name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCreateNewRole.Focus();
                return;
            }
            //if (txtNewPrevileage.Text == "")
            //{
            //    MessageBox.Show("Please enter privileage/s", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtNewPrevileage.Focus();
            //    return;
            //}
            MySQLHelper tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
             int ResultVal= tmpMySQLHelper.CreateNewRole(txtCreateNewRole.Text, txtNewPrevileage.Text);
            if (ResultVal < 0) MessageBox.Show("Wasnt able to create  role", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (ResultVal == 0) MessageBox.Show("Role already Exists, Please try adding new Role", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else MessageBox.Show("New Role :: " + txtCreateNewRole.Text + " added successfully");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtCreateNewRole.Clear();
            txtNewPrevileage.Clear();
            txtCreateNewRole.Focus();
        }
    }
}
