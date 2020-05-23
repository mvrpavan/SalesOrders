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
    public partial class EditProfileForm : Form
    {
        MySQLHelper tmpMySQlHelper;
        public EditProfileForm()
        {
            InitializeComponent();
        }

        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            List<string> ListColumnName = new List<string>();
            List<string> ListColumnValues = new List<string>();

            if (txtChangePwd.Text != "")
            {
                ListColumnName.Add("PASSWORD");
                ListColumnValues.Add(txtChangePwd.Text);
            }
            if (txtChangePhone.Text!= "")
            {
                ListColumnName.Add("PHONENO");
                ListColumnValues.Add("'" + txtChangePhone.Text + "'");
            }
            if (txtChangeAddress.Text != "")
            {
                ListColumnName.Add("ADDRESS");
                ListColumnValues.Add(txtChangeAddress.Text);
            }
  
            if (ListColumnName.Count > 0)
            {
                tmpMySQlHelper = MySQLHelper.GetMySqlHelperObj();
                string WhereCondition = "USERNAME = '" + tmpMySQlHelper.CurrentUser + "'";
                int ResulVal = tmpMySQlHelper.UpdateAnyTableDetails("USERS", ListColumnName, ListColumnValues, WhereCondition);
                if (ResulVal <= 0) MessageBox.Show("Wasnt able to update the user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else MessageBox.Show("Updated your details successfully");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtChangePwd.Clear();
            txtChangePhone.Clear();
            txtChangeAddress.Clear();
            txtChangePwd.Focus();
        }
    }
}
