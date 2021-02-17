using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesOrdersReport.Views
{
    public partial class WelcomeSplashForm : Form
    {
        public WelcomeSplashForm()
        {
            try
            {
                CommonFunctions.Initialize();

                InitializeComponent();

                About about = new About();
                lblTitle.Text = about.AssemblyTitle;
                lblVersion.Text = String.Format("Version {0}", about.AssemblyVersion);

                progressBarLoading.Minimum = 0;
                progressBarLoading.Maximum = 100;
                //progressBarLoading.Style = ProgressBarStyle.Continuous;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("WelcomeSplashForm.ctor()", ex);
            }
        }

        delegate void ReportProgressDel(Int32 ProgressState);
        ReportProgressDel ReportProgress = null;

        private void ReportProgressFunc(Int32 ProgressState)
        {
            if (ReportProgress == null) return;
            ReportProgress(ProgressState);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ReportProgressFunc(0);

                lblLoadingStatus.Text = "Establishing Database connection...";
                CommonFunctions.CreateDBConnection();
                lblLoadingStatus.Text = "Establishing Database connection...completed";

                if (!MySQLHelper.GetMySqlHelperObj().CheckTableExists("USERMASTER"))
                {
                    RunDBScript ObjRunDBScript = new RunDBScript();
                    ObjRunDBScript.CreateMasterTables();
                    ObjRunDBScript.CreateRunningTables();
                    ObjRunDBScript.ExecuteOneTimeExecutionScript();
                }

                //TODO: Load all tables to memory here
                lblLoadingStatus.Text = "Loading User tables...";
                CommonFunctions.ObjUserMasterModel.LoadAllUserMasterTables();
                ReportProgressFunc(25);

                lblLoadingStatus.Text = "Loading Customer tables...";
                CommonFunctions.ObjCustomerMasterModel.LoadAllCustomerMasterTables();
                ReportProgressFunc(50);

                lblLoadingStatus.Text = "Loading Product tables...";
                ProductLine CurrProductLine = CommonFunctions.ListProductLines[CommonFunctions.SelectedProductLineIndex];
                CurrProductLine.LoadAllProductMasterTables();
                lblLoadingStatus.Text = "Loading Product tables...completed";
                ReportProgressFunc(75);

                /*DataTable dtDiscountGroupMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("DiscountGroupMaster", CommonFunctions.MasterFilePath, "*");
                DataTable dtSellerMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("SellerMaster", CommonFunctions.MasterFilePath, "*");
                CurrProductLine.LoadSellerMaster(dtSellerMaster, dtDiscountGroupMaster);
                lblLoadingStatus.Text = "Completed loading Seller details";
                ReportProgressFunc(50);

                DataTable dtVendorMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("VendorMaster", CommonFunctions.MasterFilePath, "*");
                CurrProductLine.LoadVendorMaster(dtVendorMaster, dtDiscountGroupMaster);
                lblLoadingStatus.Text = "Completed loading Vendor details";
                ReportProgressFunc(75);*/

                CommonFunctions.SelectProductLine(CommonFunctions.SelectedProductLineIndex);
                ReportProgressFunc(100);

                //TODO: Print a log file

                //for (int i = 0; i < 3; i++)
                //{
                //    Thread.Sleep(1000);
                //    lblLoadingStatus.Text = "Loading completed..." + ((i + 1) * 33) + "%";
                //    backgroundWorker1.ReportProgress((i + 1) * 33);
                //}
                ReportProgressFunc(100);
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("WelcomeSplashForm.backgroundWorker1_DoWork()", ex);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                progressBarLoading.Value = e.ProgressPercentage;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("WelcomeSplashForm.backgroundWorker1_ProgressChanged()", ex);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("WelcomeSplashForm.backgroundWorker1_RunWorkerCompleted()", ex);
            }
        }

        private void WelcomeSplashForm_Shown(object sender, EventArgs e)
        {
            try
            {
#if DEBUG
                backgroundWorker1_DoWork(null, null);
                backgroundWorker1_RunWorkerCompleted(null, null);
#else
                ReportProgress = backgroundWorker1.ReportProgress;
                backgroundWorker1.RunWorkerAsync();
                backgroundWorker1.WorkerReportsProgress = true;
#endif
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("WelcomeSplashForm.WelcomeSplashForm_Shown()", ex);
                throw;
            }
        }
    }
}
