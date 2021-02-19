using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesOrdersReport.Models;
using SalesOrdersReport.CommonModules;

namespace SalesOrdersReport.Views
{
    public partial class CreatePaymentForm : Form
    {
        PaymentsModel ObjPaymentsModel;

        public CreatePaymentForm()
        {
            InitializeComponent();
        }

        private void CreatePaymentForm_Load(object sender, EventArgs e)
        {
            try
            {
                ObjPaymentsModel = new PaymentsModel();
                ObjPaymentsModel.LoadPaymentModes();

                cmbBoxPaymentModes.Items.Clear();
                cmbBoxPaymentModes.Items.AddRange(ObjPaymentsModel.GetPaymentModesList().ToArray());
                cmbBoxPaymentModes.DropDownStyle = ComboBoxStyle.DropDown;
                cmbBoxPaymentModes.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbBoxPaymentModes.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreatePaymentForm_Load()", ex);
            }
        }

        private void btnAddUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
