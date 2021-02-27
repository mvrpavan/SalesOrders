using SalesOrdersReport.CommonModules;
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
        UpdateOnCloseDel UpdateCustomerOnClose = null;
        public CreateDiscountGroupForm(UpdateOnCloseDel UpdateCustomerOnClose)
        {
            try
            {
                InitializeComponent();
                txtCreateDisGrpName.Focus();
                this.UpdateCustomerOnClose = UpdateCustomerOnClose;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateRole.CreateDiscountGroupForm()", ex);
                throw ex;
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtCreateDisGrpName.Clear();
                txtCreateDisGrpDesc.Clear();
                txtCreateDGDiscountVal.Clear();
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
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateRole.btnCreateDiscountGrp_Click()", ex);
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
               
                return isValid;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateDiscountGroupForm.ValidateDicountVal()", ex);
                throw ex;
            }

        }
    }
}
