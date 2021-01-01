using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SalesOrdersReport
{
    public partial class LoginForm : Form
    {
        MySQLHelper tmpIntegDBHelper;
        public LoginForm()
        {
            //CommonFunctions.Initialize();
            CommonFunctions.CurrentForm = this;
            InitializeComponent();
            tmpIntegDBHelper = MySQLHelper.GetMySqlHelperObj();
        }

        //private void GetDBConnectionConfigForm_Closed(object sender, FormClosedEventArgs e)
        //{
        //    this.Show();
        //}

        private void btnLogin_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "a"; txtPassword.Text = "a";
            if (txtUserName.Text == "")
            {
                MessageBox.Show("Please enter user name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Focus();
                return;
            }
            if (txtPassword.Text == "")
            {
                MessageBox.Show("Please enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }

            try
            {               
                if (!tmpIntegDBHelper.CheckTableExists("Users"))
                {
                    RunDBScript objRunDBScript = new RunDBScript();
                    objRunDBScript.CreateNecessaryTables();
                }
                bool ReturnVal = true; //tmpIntegDBHelper.LoginCheck(txtUserName.Text, txtUserName.Text, myConnection);

                if (ReturnVal)
                {
                    MessageBox.Show("You have logged in successfully " + txtUserName.Text);
                    //Hide the login form
                    MainForm objMainForm = new MainForm();
                    objMainForm.lblCurrentUser.Text = tmpIntegDBHelper.CurrentUser;
                    objMainForm.FormClosed += ObjMainForm_FormClosed;
                    objMainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Login Failed...Try again !", "Login Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUserName.Clear();
                    txtPassword.Clear();
                    txtUserName.Focus();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("LoginForm.btnLogin_Click()", ex);
            }
        }

        private void ObjMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
