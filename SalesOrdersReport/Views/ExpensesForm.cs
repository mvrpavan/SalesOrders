using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Models;
using Excel = Microsoft.Office.Interop.Excel;

namespace SalesOrdersReport.Views
{
    public partial class ExpensesForm : Form
    {
      
        AccountsMasterModel ObjAccountsMasterModel;
        MySQLHelper ObjMySQLHelper;
        InvoicesModel ObjInvoicesModel ;

        public ExpensesForm()
        {
            try
            {
                InitializeComponent();
                ObjMySQLHelper = MySQLHelper.GetMySqlHelperObj();

                CommonFunctions.SetDataGridViewProperties(dtGridViewExpenses);
                ObjInvoicesModel = new InvoicesModel();
                ObjInvoicesModel.Initialize();

                ObjAccountsMasterModel = CommonFunctions.ObjAccountsMasterModel;

                //dTimePickerFromPayments.Value = DateTime.Today.AddDays(-30);

                LoadExpensesGridView();

               groupBoxExpenses.Visible = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ExpensesForm.ctor()", ex);
            }
        }



        private void LoadExpensesGridView()
        {
            try
            {
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadExpensesGridView()", ex);
            }
        }



        private void btnCreateExpense_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new CreateExpenseForm(), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnCreateExpense_Click()", ex);
            }
        }

 

        private void btnViewEditExpense_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new CreateExpenseForm(), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnViewEditExpense_Click()", ex);
            }
        }
        private void btnSearchExpense_Click(object sender, EventArgs e)
        {
            try
            {
                List<String> ListFindInFields = new List<String>()
                {
                    "Vendor Name",
                    "Expense Date",
                    "Expense Amount",
                    "Expense Method",
                    "Expense Paid by"
                };
                CommonFunctions.ShowDialog(new OrderInvQuotSearchForm(ListFindInFields, null, null), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnSearchExpense_Click()", ex);
            }
        }



        private void btnReloadExpenses_Click(object sender, EventArgs e)
        {
            try
            {
                LoadExpensesGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnReloadExpenses_Click()", ex);
            }
        }

        private void ExpensesMainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                this.MaximizeBox = true;
                this.WindowState = FormWindowState.Maximized;
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ExpensesMainForm_Shown()", ex);
            }
        }

        private void checkBoxApplyFilterExpenses_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!checkBoxApplyFilterExpense.Checked)
                {
                    dTimePickerFromExpenses.Value = DateTime.Today;
                    dTimePickerToExpenses.Value = dTimePickerFromExpenses.Value.AddDays(30);
                }
                LoadExpensesGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.checkBoxApplyFilterExpenses_CheckedChanged()", ex);
            }
        }

        private void dtGridViewInvoices_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewInvoices_CellMouseClick()", ex);
            }
        }

        private void btnImportFromExcel_Click(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnImportFromExcel_Click()", ex);
            }
        }

        private void btnDeleteExpense_Click(object sender, EventArgs e)
        {

        }

        private void btnPrintExpense_Click(object sender, EventArgs e)
        {

        }
    }
}
