﻿using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text;
using System.Data;
using SalesOrdersReport.CommonModules;

namespace SalesOrdersReport.Views
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            CommonFunctions.CurrentForm = this;
            InitializeComponent();
            txtUserName.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //if (txtUserName.Text == "")
            //{
            //    MessageBox.Show("Please enter user name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtUserName.Focus();
            //    return;
            //}
            //if (txtPassword.Text == "")
            //{
            //    MessageBox.Show("Please enter password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    txtPassword.Focus();
            //    return;
            //}
            try
            {
                MySqlConnection myConnection = MySQLHelper.GetMySqlHelperObj().GetDbConnection(); //CreateDBConnection();
                if (myConnection == null) return;

                //int ReturnVal = CommonFunctions.ObjUserMasterModel.LoginCheck(txtUserName.Text, txtPassword.Text, myConnection);
                int ReturnVal = 0;
                MySQLHelper.GetMySqlHelperObj().CurrentUser = "admin";
                if (ReturnVal == 0)
                {
                    CommonFunctions.CurrentUserName = MySQLHelper.GetMySqlHelperObj().CurrentUser;
                    //CommonFunctions.CurrentUserName = tmpIntegDBHelper.CurrentUser = "admin";

                    //CommonFunctions.ObjUserMasterModel.LoadAllUserMasterTables();
                    //CommonFunctions.ObjCustomerMasterModel.LoadAllCustomerMasterTables();
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