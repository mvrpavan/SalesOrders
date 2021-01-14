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
    public partial class CreatePriceGroupForm : Form
    {
        UpdateOnCloseDel UpdateCustomerOnClose = null;
        public CreatePriceGroupForm(UpdateOnCloseDel UpdateCustomerOnClose)
        {
            InitializeComponent();
            txtNewPriceGrpName.Focus();
            cmbxPriceGrpCol.SelectedIndex = 0;
            this.UpdateCustomerOnClose = UpdateCustomerOnClose;
            this.FormClosed += CreatePriceGrpForm_FormClosed;
        }





        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtNewPriceGrpName.Clear();
                txtNwPriceGrpDesc.Clear();
                txtNewPriceGrpName.Focus();
                lblValidatingErrMsg.Visible = false;
                txtPriceGrpDiscVal.Clear();
                cmbxPriceGrpCol.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreatePriceGroupForm.btnReset_Click()", ex);
                throw ex;
            }
        }

        private void btnCreatePriceGrp_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewPriceGrpName.Text.Trim() == string.Empty)
                {
                    lblValidatingErrMsg.Visible = true;
                    lblValidatingErrMsg.Text = "Price Group Name Cant be empty!";
                    txtNewPriceGrpName.Focus();
                    return;
                }
                if (txtNwPriceGrpDesc.Text.Trim() == string.Empty)
                {
                    lblValidatingErrMsg.Visible = true;
                    lblValidatingErrMsg.Text = "Description Cant be empty!";
                    txtNwPriceGrpDesc.Focus();
                    return;
                }
                if (radioBtnDefaultNo.Checked == false && radioBtnDefaultYes.Checked == false)
                {
                    lblValidatingErrMsg.Visible = true;
                    lblValidatingErrMsg.Text = "Pls Set Deafult value is Yes/No ";
                    return;
                }
                if (radioBtnDisTypePercent.Checked == false && radioBtnDisTypeAbs.Checked == false)
                {
                    lblValidatingErrMsg.Visible = true;
                    lblValidatingErrMsg.Text = "Pls Select DiscountType";
                    return;
                }
                List<string> ListColumnValues = new List<string>();
                List<string> ListColumnNamesWithDataType = new List<string>();
                txtPriceGrpDiscVal.Text = txtPriceGrpDiscVal.Text.Trim();
                if (txtPriceGrpDiscVal.Text != string.Empty)
                {
                    if (!ValidateDicountVal()) return;

                    ListColumnValues.Add(txtPriceGrpDiscVal.Text);
                    ListColumnNamesWithDataType.Add("DISCOUNT,DECIMAL");

                }

                ListColumnValues.Add(cmbxPriceGrpCol.SelectedItem.ToString());
                ListColumnNamesWithDataType.Add("PRICEGROUPCOLUMNNAME,VARCHAR");

                if (radioBtnDefaultYes.Checked == true) ListColumnValues.Add("1");
                else ListColumnValues.Add("0");

                ListColumnNamesWithDataType.Add("ISDEFAULT,BIT");

                if (radioBtnDisTypeAbs.Checked == true) ListColumnValues.Add("ABSOLUTE");
                else ListColumnValues.Add("PERCENT");

                ListColumnNamesWithDataType.Add("DISCOUNTTYPE,VARCHAR");

                int ResultVal = CommonFunctions.ObjCustomerMasterModel.CreateNewPriceGrp(txtNewPriceGrpName.Text, txtNwPriceGrpDesc.Text, ListColumnNamesWithDataType, ListColumnValues);
                if (ResultVal <= 0) MessageBox.Show("Wasnt able to create the Price Group", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (ResultVal == 2)
                {
                    MessageBox.Show("Price Group Name already exists! Pls Provide Another Price Group Name.", "Error");
                }
                else
                {
                    MessageBox.Show("Added New Price Group :: " + txtNewPriceGrpName.Text + " successfully", "Added Price Group");
                    UpdateCustomerOnClose(Mode: 2);
                    btnReset.PerformClick();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreatePriceGroupForm.btnCreatePriceGrp_Click()", ex);
                throw ex;
            }

        }

        private void CreatePriceGrpForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateCustomerOnClose(Mode: 1);
        }

        bool ValidateDicountVal()
        {
            try
            {
                //int num;
                //float numFloat;
                //bool isValid = false;
                //txtPriceGrpDiscVal.Text = txtPriceGrpDiscVal.Text.Trim();
                //if (Int32.TryParse(txtPriceGrpDiscVal.Text, out num)) isValid = true;
                //else if (float.TryParse(txtPriceGrpDiscVal.Text, out numFloat)) isValid = true;
                txtPriceGrpDiscVal.Text = txtPriceGrpDiscVal.Text.Trim();
                bool isValid = CommonFunctions.ValidateDoubleORIntVal(txtPriceGrpDiscVal.Text);

                if (!isValid)
                {
                    lblValidatingErrMsg.Visible = true;
                    lblValidatingErrMsg.Text = "Enter Valid Integer/Decimal Values!";
                    txtPriceGrpDiscVal.Focus();
                }
                else
                {
                    lblValidatingErrMsg.Visible = false;
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
                CommonFunctions.ShowErrorDialog("CreatePriceGroupForm.ValidateDicountVal()", ex);
                throw ex;
            }

        }
    }
}
