using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesOrdersReport
{
    public partial class GetDBConnectionConfigForm : Form
    {
        public GetDBConnectionConfigForm()
        {
            InitializeComponent();
            txtServerName.Focus();
        }

        private void btnCreateDBConnection_Click(object sender, EventArgs e)
        {
            CommonFunctions.ObjApplicationSettings.Server = txtServerName.Text;
            CommonFunctions.ObjApplicationSettings.DatabaseName = txtDatabaseName.Text;
            CommonFunctions.ObjApplicationSettings.UserName = txtUserName.Text;
            CommonFunctions.ObjApplicationSettings.Password = txtDBPwd.Text;

        }
        
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtDBPwd.Clear();
            txtDatabaseName.Clear();
            txtServerName.Clear();
            txtUserName.Focus();
        }

     
    }
}
