using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
