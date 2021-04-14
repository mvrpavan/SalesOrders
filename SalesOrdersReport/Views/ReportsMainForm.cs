using SalesOrdersReport.CommonModules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesOrdersReport.Views
{
    public partial class ReportsMainForm : Form
    {
        public ReportsMainForm()
        {
            InitializeComponent();
        }

        private void btnAddReport_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnAddReport_Click()", ex);
                throw;
            }
        }

        private void btnViewEditReport_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteReport_Click(object sender, EventArgs e)
        {

        }

        private void btnReloadReports_Click(object sender, EventArgs e)
        {

        }

        private void btnPrintReport_Click(object sender, EventArgs e)
        {

        }

        private void btnExportReport_Click(object sender, EventArgs e)
        {

        }

        private void cmbBoxReportName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbBoxParamNames_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAddParamValue_Click(object sender, EventArgs e)
        {

        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {

        }
    }
}
