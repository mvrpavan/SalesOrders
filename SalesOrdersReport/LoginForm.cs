using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Web;

namespace SalesOrdersReport
{
    public partial class LoginForm : Form
    {
        // String cs = @"Data Source=(LocalDB)\v11.0;Integrated Security=True;  AttachDbFilename=|DataDirectory|\Login.mdf; Connect Timeout=30";
        MySQLHelper tmpIntegDBHelper;
        public LoginForm()
        {
            CommonFunctions.Initialize();
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

                //tmpIntegDBHelper.OpenConnection("APEXDEV0719", "SALES",
//    "nisha", "nisha");
                tmpIntegDBHelper.OpenConnection(CommonFunctions.ObjApplicationSettings.Server, CommonFunctions.ObjApplicationSettings.DatabaseName, CommonFunctions.ObjApplicationSettings.UserName, CommonFunctions.ObjApplicationSettings.Password);
                
                //EventProcessorMain.WriteToLogFileFunc("Connecting to IntegrationDB completed");

               return tmpIntegDBHelper.GetDbConnection();
            }
            catch (Exception ex)
            {
               
                //EventProcessorMain.WriteToLogFileFunc(String.Format("Error occured in {0}.CreateDBConnection()", this));
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
                //myConnection = new SqlConnection(cs);

                if (!tmpIntegDBHelper.CheckTableExists("Users"))
                {
                    RunDBScript objRunDBScript = new RunDBScript();
                    objRunDBScript.CreateNecessaryTables();
                }


                    MySqlCommand myCommand = new MySqlCommand("SELECT Username,Password FROM Users WHERE Username = @Username AND Password = @Password", myConnection);

                MySqlParameter uName = new MySqlParameter("@Username", MySqlDbType.VarChar);
                MySqlParameter uPassword = new MySqlParameter("@Password", MySqlDbType.VarChar);

                uName.Value = txtUserName.Text;
                uPassword.Value = txtPassword.Text;

                myCommand.Parameters.Add(uName);
                myCommand.Parameters.Add(uPassword);

                //myCommand.Connection.Open();

                MySqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read() == true)
                {
                    // string strSessionID;
                    //HttpContext.Session["CurrentUserSessionID"] = Session.SessionID;
                    //Session["SessionID"] = Guid.NewGuid().ToString();
                    //session["firstname"] = fistname;
                    //session["lastnane"] = lastname;
                    //Session["MySession"] = "This is my session value";
                    //var se = HttpContext.Current.Session;
                    //HttpContext.Current.Session["SessionID"] = Guid.NewGuid().ToString();
                    //HttpContext.Current.Session["USER"] = uName.Value;
                    tmpIntegDBHelper.CurrentUser = txtUserName.Text;
                    
                    MessageBox.Show("You have logged in successfully " + txtUserName.Text);
                    //Hide the login form
                    MainForm objMainForm = new MainForm();
                    objMainForm.lblCurrentUser.Text = tmpIntegDBHelper.CurrentUser;
                    this.Hide();
                    objMainForm.Show();
                }
                else
                {
                    MessageBox.Show("Login Failed...Try again !", "Login Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUserName.Clear();
                    txtPassword.Clear();
                    txtUserName.Focus();
                }
                myReader.Close();
                //if (myConnection.State == ConnectionState.Open)
                //{
                //    myConnection.Dispose();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
    }
}
