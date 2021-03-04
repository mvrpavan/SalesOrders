using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Views;

namespace SalesOrdersReport
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());

            //Application.Run(new LoginForm());
            //Application.Run(new GetDBConnectionConfigForm());
            CommonFunctions.WriteToLogFile("==============================================================\nApplication started");
            Application.Run(new WelcomeSplashForm());
            Login();
        }

        private static bool UserLogOut = false;

        private static void Login()
        {
            try
            {
                LoginForm ObjLoginForm = new LoginForm();
                MainForm ObjMainForm = new MainForm();
                ObjMainForm.FormClosed += new FormClosedEventHandler(ObjMainForm.MainForm_FormClosed);
                if (ObjLoginForm.ShowDialog(ObjMainForm) == DialogResult.OK)
                {
                    Application.Run(ObjMainForm);
                    UserLogOut = ObjMainForm.IsLoggedOut;
                    if (UserLogOut) Login();
                }
                else
                    Application.Exit();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("Program.Login()", ex);
                throw ex;
            }
        }
    }
}
