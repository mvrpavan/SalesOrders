using System;
using System.Windows.Forms;

namespace SalesOrdersReport.Views
{
    public partial class AddTaxForm : Form
    {
        Boolean IsAddTax = false;
        UpdateOnCloseDel UpdateOnClose = null;

        public AddTaxForm(String FormTitle, Boolean IsAddTax, UpdateOnCloseDel UpdateOnClose)
        {
            InitializeComponent();
            this.IsAddTax = IsAddTax;
            this.UpdateOnClose = UpdateOnClose;
            Text = FormTitle;
        }

        private void AddTaxForm_Shown(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("AddTaxForm.AddTaxForm_Shown()", ex);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
