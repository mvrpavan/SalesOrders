﻿using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Models;
using System;
using System.ComponentModel;
using System.Threading;
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
                while (CommonFunctions.CreateDBConnection() == false)
                {
                    DialogResult dialogResult = MessageBox.Show(this, "Unable to Connect to Database. Please check Internet connection and retry.\nDo you want to re-connect to database?", "Connectivity issue", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (dialogResult == DialogResult.Cancel)
                    {
                        Application.Exit();
                    }
                }
                lblLoadingStatus.Text = "Establishing Database connection...completed";

                if (!MySQLHelper.GetMySqlHelperObj().CheckTableExists("USERMASTER"))
                {
                    lblLoadingStatus.Text = "Creating required tables...";
                    RunDBScript ObjRunDBScript = new RunDBScript();
                    ObjRunDBScript.CreateMasterTables();
                    ObjRunDBScript.CreateRunningTables();
                    ObjRunDBScript.ExecuteOneTimeExecutionScript();
                    lblLoadingStatus.Text = "Creating required tables...completed";
                }

                //{
                //    RunDBScript ObjRunDBScript = new RunDBScript();
                //    ObjRunDBScript.CreateAccountsMasterTables();
                //    ObjRunDBScript.CreateTransactionTables();
                //    ObjRunDBScript.CreateInventoryTables();
                //    ObjRunDBScript.CreatePurchaseOrderInvoiceTables();
                //}

                //TODO: Load all tables to memory here
                lblLoadingStatus.Text = "Loading User tables...";
                CommonFunctions.ObjUserMasterModel.LoadAllUserMasterTables();
                lblLoadingStatus.Text = "Loading User tables...completed";
                ReportProgressFunc(25);

                lblLoadingStatus.Text = "Loading Customer tables...";
                CommonFunctions.ObjCustomerMasterModel.LoadAllCustomerMasterTables();
                CommonFunctions.ObjAccountsMasterModel.LoadAccountDetails();
                lblLoadingStatus.Text = "Loading Customer tables...completed";
                ReportProgressFunc(50);

                lblLoadingStatus.Text = "Loading Vendor tables...";
                ProductLine CurrProductLine = CommonFunctions.ListProductLines[CommonFunctions.SelectedProductLineIndex];
                CurrProductLine.LoadVendorMasterTable();
                lblLoadingStatus.Text = "Loading Vendor tables...completed";
                ReportProgressFunc(60);

                lblLoadingStatus.Text = "Loading Product tables...";
                CurrProductLine.LoadAllProductMasterTables();
                lblLoadingStatus.Text = "Loading Product tables...completed";
                ReportProgressFunc(80);

                CommonFunctions.SelectProductLine(CommonFunctions.SelectedProductLineIndex);
                ReportProgressFunc(100);

                //TODO: Print a log file
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
