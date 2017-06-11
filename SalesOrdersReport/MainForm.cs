using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SalesOrdersReport
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            CommonFunctions.Initialize();

            InitializeComponent();
            btnNewOrderSheet.Enabled = false;
            btnCreateEachSellerInvoice.Enabled = false;
            this.Text = CommonFunctions.MainFormTitleText;
            lblVersion.Text = Application.ProductVersion;
        }

        private void btnMasterFileBrowse_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = openFileDialog1.ShowDialog();
            if (dlgResult == System.Windows.Forms.DialogResult.OK || dlgResult == System.Windows.Forms.DialogResult.Yes)
            {
                txtBoxFileName.Text = openFileDialog1.FileName;
                btnNewOrderSheet.Enabled = true;
                btnCreateEachSellerInvoice.Enabled = true;
            }
        }

        private void btnNewOrderSheet_Click(object sender, EventArgs e)
        {
            AddNewOrderSheetForm ObjOrderForm = new AddNewOrderSheetForm(this);
            ObjOrderForm.ShowDialog(this);
        }

        private void btnCreateEachSellerInvoice_Click(object sender, EventArgs e)
        {
            CreateSellerInvoice ObjInvoiceForm = new CreateSellerInvoice(this);
            ObjInvoiceForm.ShowDialog(this);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommonFunctions.WriteToSettingsFile();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm ObjSettingsForm = new SettingsForm();
            ObjSettingsForm.ShowDialog(this);
        }
    }
}
