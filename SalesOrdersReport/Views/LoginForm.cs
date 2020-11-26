using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text;
using System.Data;

namespace SalesOrdersReport
{
    public partial class LoginForm : Form
    {
        MySQLHelper tmpIntegDBHelper;
        public LoginForm()
        {
            CommonFunctions.Initialize();
            CommonFunctions.CurrentForm = this;
            InitializeComponent();
            txtUserName.Focus();
        }

        private MySqlConnection CreateDBConnection()
        {
            try
            {
                tmpIntegDBHelper = MySQLHelper.GetMySqlHelperObj();

                if (CommonFunctions.ObjApplicationSettings.Server == null || CommonFunctions.ObjApplicationSettings.Server == string.Empty)
                {
                    MessageBox.Show("DB is not Configured! Please Configure");
                    return null;
                }
                if (tmpIntegDBHelper.ObjDbConnection == null || tmpIntegDBHelper.ObjDbConnection.State == ConnectionState.Closed)
                {
                    tmpIntegDBHelper.OpenConnection(CommonFunctions.ObjApplicationSettings.Server, CommonFunctions.ObjApplicationSettings.DatabaseName, CommonFunctions.ObjApplicationSettings.UserName, CommonFunctions.ObjApplicationSettings.Password);
                }
                return tmpIntegDBHelper.GetDbConnection();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("LoginForm.CreateDBConnection()", ex);
                throw ex;
            }
        }

        private void GetDBConnectionConfigForm_Closed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.Show();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("LoginForm.GetDBConnectionConfigForm_Closed()", ex);
                throw ex;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
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
                MySqlConnection myConnection = CreateDBConnection();
                if (myConnection == null) return;
                if (!tmpIntegDBHelper.CheckTableExists("USERMASTER"))
                {
                    RunDBScript ObjRunDBScript = new RunDBScript();
                    ObjRunDBScript.CreateNecessaryTables();
                }

                int ReturnVal = CommonFunctions.ObjUserMasterModel.LoginCheck(txtUserName.Text, txtPassword.Text, myConnection);

                if (ReturnVal == 0)
                {
                    CommonFunctions.CurrentUserName = tmpIntegDBHelper.CurrentUser;
                    CommonFunctions.ObjUserMasterModel.LoadAllUserMasterTables();
                    this.Close();
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    if (ReturnVal == -2) MessageBox.Show("User InActive! Pls Contact Admin", "InActive User",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else MessageBox.Show("Login Failed...Try again !", "Login Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("LoginForm.ObjMainForm_FormClosed()", ex);
                throw ex;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("LoginForm.btnExit_Click()", ex);
                throw ex;
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
        }
    }
}
