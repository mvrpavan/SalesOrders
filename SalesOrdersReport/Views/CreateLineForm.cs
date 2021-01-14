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
    public partial class CreateLineForm : Form
    {
        UpdateOnCloseDel UpdateCustomerOnClose = null;
        public CreateLineForm(UpdateOnCloseDel UpdateCustomerOnClose)
        {
            InitializeComponent();
            txtNewLineName.Focus();
            this.UpdateCustomerOnClose = UpdateCustomerOnClose;
            this.FormClosed += CreateLineForm_FormClosed;
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtNewLineName.Clear();
                txtLineDesc.Clear();
                txtNewLineName.Focus();
                lblCreateLineValidateErrMsg.Visible = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateRole.btnReset_Click()", ex);
                throw ex;
            }
        }

        private void CreateLineForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                UpdateCustomerOnClose(Mode: 1);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateLineForm.CreateLineForm_FormClosed()", ex);
            }
        }

        private void btnCreateLine_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewLineName.Text.Trim() == "")
                {
                    lblCreateLineValidateErrMsg.Visible = true;
                    lblCreateLineValidateErrMsg.Text = "Line Name Cannot be empty!";
                    txtNewLineName.Focus();
                    return;
                }
                if (txtLineDesc.Text.Trim() == "")
                {
                    lblCreateLineValidateErrMsg.Visible = true;
                    lblCreateLineValidateErrMsg.Text = "Please add description.";
                    txtLineDesc.Focus();
                    return;
                }
                int ResultVal = CommonFunctions.ObjCustomerMasterModel.CreateNewLine(txtNewLineName.Text, txtLineDesc.Text);
                if (ResultVal < 0) MessageBox.Show("Wasnt able to create line", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (ResultVal == 2) MessageBox.Show("Line already Exists, Please try adding new Line", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    MessageBox.Show("New Line :: " + txtNewLineName.Text + " added successfully", "Line Added");
                    UpdateCustomerOnClose(Mode: 2);
                    btnReset.PerformClick();
                }

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateLineForm.btnCreateLine_Click()", ex);
            }
        }
    }
}
