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
            CommonFunctions.Initialize();
            CommonFunctions.CurrentForm = this;
            InitializeComponent();
        }

        private MySqlConnection CreateDBConnection()
        {
            try
            {
                 tmpIntegDBHelper = MySQLHelper.GetMySqlHelperObj();

                // EventProcessorMain.WriteToLogFileFunc("Connecting to IntegrationDB started");
                //SQLConnection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = SubscriptionPriceDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False;";
                //tmpIntegDBHelper.OpenConnection(ObjConfig.ObjDatabaseConfig.IntegServer, ObjConfig.ObjDatabaseConfig.IntegDatabase,
                //    ObjConfig.ObjDatabaseConfig.IntegUserID, ObjConfig.ObjDatabaseConfig.IntegPassword);

                if (CommonFunctions.ObjApplicationSettings.Server == null || CommonFunctions.ObjApplicationSettings.Server == string.Empty)
                {
                    var Result = MessageBox.Show("DB is not Configured! Configure Now?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    // If the no button was pressed ...
                    if (Result == DialogResult.Yes)
                    {
                      
                        GetDBConnectionConfigForm ObjGetDBConnectionConfigForm = new GetDBConnectionConfigForm();
                        //Application.Run(new GetDBConnectionConfigForm());
                        //ObjGetDBConnectionConfigForm.Show();
                        //Form tmp =  CommonFunctions.CurrentForm;
                        //CommonFunctions.CurrentForm.Hide();
                        //.CurrentForm = ObjGetDBConnectionConfigForm;
                        //ObjGetDBConnectionConfigForm.ShowIcon = true;
                        //ObjGetDBConnectionConfigForm.ShowInTaskbar = true;
                        //ObjGetDBConnectionConfigForm.MinimizeBox = true;
                        //ObjGetDBConnectionConfigForm.MaximizeBox = true;
                        //ObjGetDBConnectionConfigForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                        //ObjGetDBConnectionConfigForm.StartPosition = FormStartPosition.CenterScreen;
                        ObjGetDBConnectionConfigForm.FormClosed += new FormClosedEventHandler(GetDBConnectionConfigForm_Closed); //add handler to catch when child form is closed
                        ObjGetDBConnectionConfigForm.Show();
                        this.Hide();
                    }
                }
                tmpIntegDBHelper.OpenConnection(CommonFunctions.ObjApplicationSettings.Server, CommonFunctions.ObjApplicationSettings.DatabaseName, CommonFunctions.ObjApplicationSettings.UserName, CommonFunctions.ObjApplicationSettings.Password);
                
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
            this.Show();
        }

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
               
                MySqlConnection myConnection = CreateDBConnection();
                //myConnection = new SqlConnection(cs);

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
