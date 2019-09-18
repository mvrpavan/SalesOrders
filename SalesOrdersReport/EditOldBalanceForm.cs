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
    public partial class EditOldBalanceForm : Form
    {
        CustomerInvoiceSellerOrderForm objCustomerForm = null;

        public EditOldBalanceForm(CustomerInvoiceSellerOrderForm ObjForm, Double BalanceAmount)
        {
            InitializeComponent();
            objCustomerForm = ObjForm;
            txtBoxBalanceAmount.Text = BalanceAmount.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                String Value = txtBoxBalanceAmount.Text.Trim();
                if (String.IsNullOrEmpty(Value))
                {
                    MessageBox.Show(this, "Invalid Balance amount. Please provide numeric value.", "Balance Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }

                Double result;
                if (!Double.TryParse(Value, out result))
                {
                    MessageBox.Show(this, "Invalid Balance amount. Please provide numeric value.", "Balance Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                }

                objCustomerForm.UpdateBalanceAmount(result);

                this.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditOldBalanceForm.btnUpdate_Click()", ex);
            }
        }
    }
}
