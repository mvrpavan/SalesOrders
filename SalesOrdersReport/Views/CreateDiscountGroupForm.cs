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
    public partial class CreateDiscountGroupForm : Form
    {
        UpdateCustomerOnCloseDel UpdateCustomerOnClose = null;
        public CreateDiscountGroupForm(UpdateCustomerOnCloseDel UpdateCustomerOnClose)
        {
            InitializeComponent();
            txtCreateDisGrpName.Focus();
            this.UpdateCustomerOnClose = UpdateCustomerOnClose;
            this.FormClosed += CreateDiscountGrpForm_FormClosed;
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtCreateDisGrpName.Clear();
                txtCreateDisGrpName.Clear();
                txtCreateDisGrpName.Focus();
                lblCreateDisGrpValidateMsg.Visible = false;
                
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateRole.btnReset_Click()", ex);
                throw ex;
            }
        }

        private void btnCreateDiscountGrp_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCreateDisGrpName.Text.Trim() == string.Empty)
                {
                    lblCreateDisGrpValidateMsg.Visible = true;
                    lblCreateDisGrpValidateMsg.Text = "Discount Group Name Cant be empty!";
                    txtCreateDisGrpName.Focus();
                    return;
                }
                if (txtCreateDisGrpDesc.Text.Trim() == string.Empty)
                {
                    lblCreateDisGrpValidateMsg.Visible = true;
                    lblCreateDisGrpValidateMsg.Text = "Description Cant be empty!";
                    txtCreateDisGrpDesc.Focus();
                    return;
                }
                if (radioBtnDGDefaultNo.Checked == false && radioBtnDGDefaultYes.Checked == false)
                {
                    lblCreateDisGrpValidateMsg.Visible = true;
                    lblCreateDisGrpValidateMsg.Text = "Pls Set Default value is Yes/No";
                    return;
                }
                if (radioBtnDGDisTypePercent.Checked == false && radioBtnDGDisTypeAbs.Checked == false)
                {
                    lblCreateDisGrpValidateMsg.Visible = true;
                    lblCreateDisGrpValidateMsg.Text = "Pls Select DiscountType";
                    return;
                }
                List<string> ListColumnValues = new List<string>();
                List<string> ListColumnNamesWithDataType = new List<string>();
                txtCreateDGDiscountVal.Text = txtCreateDGDiscountVal.Text.Trim();
                if (txtCreateDGDiscountVal.Text != string.Empty)
                {
                    if (!ValidateDicountVal()) return;

                    ListColumnValues.Add(txtCreateDGDiscountVal.Text);
                    ListColumnNamesWithDataType.Add("DISCOUNT,DECIMAL");

                }

                if (radioBtnDGDefaultYes.Checked == true) ListColumnValues.Add("1");
                else ListColumnValues.Add("0");

                ListColumnNamesWithDataType.Add("ISDEFAULT,BIT");

                if (radioBtnDGDisTypeAbs.Checked == true) ListColumnValues.Add("ABSOLUTE");
                else ListColumnValues.Add("PERCENT");

                ListColumnNamesWithDataType.Add("DISCOUNTTYPE,VARCHAR");

                int ResultVal = CommonFunctions.ObjCustomerMasterModel.CreateNewDiscountGrp(txtCreateDisGrpName.Text, txtCreateDisGrpDesc.Text, ListColumnNamesWithDataType, ListColumnValues);
                if (ResultVal <= 0) MessageBox.Show("Wasnt able to create the Discount Group", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (ResultVal == 2)
                {
                    MessageBox.Show("Discount Group Name already exists! Pls Provide Another Discount Group Name.", "Error");
                }
                else
                {
                    MessageBox.Show("Added New Discount Group :: " + txtCreateDisGrpName.Text + " successfully", "Added Discount Group");
                    UpdateCustomerOnClose(Mode: 2);
                    btnReset.PerformClick();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        bool ValidateDicountVal()
        {
            try
            {

                txtCreateDGDiscountVal.Text = txtCreateDGDiscountVal.Text.Trim();
                bool isValid = CommonFunctions.ValidateDoubleORIntVal(txtCreateDGDiscountVal.Text);

                if (!isValid)
                {
                    lblCreateDisGrpValidateMsg.Visible = true;
                    lblCreateDisGrpValidateMsg.Text = "Enter Valid Integer/Decimal Values!";
                    txtCreateDGDiscountVal.Focus();
                }
                else
                {
                    lblCreateDisGrpValidateMsg.Visible = false;
                }
                //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                //    (e.KeyChar != '.'))
                //{
                //    e.Handled = true;
                //}

                //// only allow one decimal point
                //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                //{
                //    e.Handled = true;
                //}



                //// allows 0-9, backspace, and decimal
                //if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
                //{
                //    e.Handled = true;
                //    return;
                //}

                //// checks to make sure only 1 decimal is allowed
                //if (e.KeyChar == 46)
                //{
                //    if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                //        e.Handled = true;
                //}

                return isValid;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateDiscountGroupForm.ValidateDicountVal()", ex);
                throw ex;
            }

        }

        private void CreateDiscountGrpForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateCustomerOnClose(Mode: 1);
        }
    }
}
