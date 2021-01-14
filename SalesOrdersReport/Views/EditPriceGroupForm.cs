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
    public partial class EditPriceGroupForm : Form
    {
        UpdateOnCloseDel UpdateCustomerOnClose = null;
        MySQLHelper tmpMySQLHelper = null;
        public EditPriceGroupForm(UpdateOnCloseDel UpdateCustomerOnClose)
        {
            InitializeComponent();
            FillPriceGrp();
            cmbxSelectPriceGrpName.Focus();
            cmbxSelectPriceGrpName.SelectedIndex = 0;
            cmbxEditPriceGrpCol.SelectedIndex = 0;
            this.UpdateCustomerOnClose = UpdateCustomerOnClose;
            this.FormClosed += EditPriceGrpForm_FormClosed;
        }

        private void FillPriceGrp()
        {
            try
            {
                tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                List<string> ListPriceGrp = CommonFunctions.ObjCustomerMasterModel.GetAllPriceGrp();
                cmbxSelectPriceGrpName.Items.Add("Select Price Group");
                foreach (var item in ListPriceGrp)
                {
                    cmbxSelectPriceGrpName.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditPriceGroupForm.FillPriceGrp()", ex);
                throw ex;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                cmbxSelectPriceGrpName.SelectedIndex = 0;
                txtEditPriceGrpDesc.Clear();
                cmbxSelectPriceGrpName.Focus();
                lblValidatingErrMsg.Visible = false;
                txtEditPriceGrpDiscVal.Clear();
                cmbxEditPriceGrpCol.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditPriceGroupForm.btnReset_Click()", ex);
                throw ex;
            }
        }



        private void EditPriceGrpForm_FormClosed(object sender, FormClosedEventArgs e)
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
                //txtEditPriceGrpDiscVal.Text = txtEditPriceGrpDiscVal.Text.Trim();
                //if (Int32.TryParse(txtEditPriceGrpDiscVal.Text, out num)) isValid = true;
                //else if (float.TryParse(txtEditPriceGrpDiscVal.Text, out numFloat)) isValid = true;
                txtEditPriceGrpDiscVal.Text = txtEditPriceGrpDiscVal.Text.Trim();
                bool isValid = CommonFunctions.ValidateDoubleORIntVal(txtEditPriceGrpDiscVal.Text);
                if (!isValid)
                {
                    lblValidatingErrMsg.Visible = true;
                    lblValidatingErrMsg.Text = "Enter Valid Integer/Decimal Values!";
                    txtEditPriceGrpDiscVal.Focus();
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
                CommonFunctions.ShowErrorDialog("EditPriceGroupForm.ValidateDicountVal()", ex);
                throw ex;
            }

        }

        private void btnEditPriceGrp_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbxSelectPriceGrpName.SelectedIndex == 0)
                {
                    lblValidatingErrMsg.Visible = true;
                    lblValidatingErrMsg.Text = "Choose a Price Group Name to Edit!";
                    return;
                }
                //if (txtEditPriceGrpDesc.Text.Trim() == string.Empty)
                //{
                //    lblValidatingErrMsg.Visible = true;
                //    lblValidatingErrMsg.Text = "Description Name cant be empty! ";
                //    return;
                //}
                if (radioBtnEditDefaultFalse.Checked == false && radioBtnEditDefaultTrue.Checked == false)
                {
                    lblValidatingErrMsg.Visible = true;
                    lblValidatingErrMsg.Text = "Pls Set Default value is Yes/No";
                    return;
                }
                if (radioBtnEditDisTypeAbs.Checked == false && radioBtnEditDisTypePercent.Checked == false)
                {
                    lblValidatingErrMsg.Visible = true;
                    lblValidatingErrMsg.Text = "Pls Select DiscountType";
                    return;
                }
                if (txtEditPriceGrpDiscVal.Text != string.Empty && !ValidateDicountVal()) return;

                if (lblValidatingErrMsg.Visible == true)
                {
                    lblValidatingErrMsg.Visible = false;
                }
                List<string> ListColumnValues = new List<string>();
                List<string> ListColumnNames = new List<string>();

                ListColumnValues.Add(txtEditPriceGrpDesc.Text);
                ListColumnNames.Add("DESCRIPTION");

                ListColumnValues.Add(cmbxEditPriceGrpCol.SelectedItem.ToString());
                ListColumnNames.Add("PRICEGROUPCOLUMNNAME");

                ListColumnNames.Add("DISCOUNT");
                ListColumnValues.Add(txtEditPriceGrpDiscVal.Text.Trim() == string.Empty ? "0" : txtEditPriceGrpDiscVal.Text.Trim());

                ListColumnNames.Add("ISDEFAULT");

                if (radioBtnEditDefaultFalse.Checked == true)
                {
                    ListColumnValues.Add("0");
                }
                else ListColumnValues.Add("1");


                ListColumnNames.Add("DISCOUNTTYPE");

                if (radioBtnEditDisTypeAbs.Checked == true)
                {
                    ListColumnValues.Add("ABSOLUTE");
                }
                else ListColumnValues.Add("PERCENT");

                string WhereCondition = "PRICEGROUPNAME = '" + cmbxSelectPriceGrpName.SelectedItem.ToString() + "'";
                tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                int ResultVal = CommonFunctions.ObjUserMasterModel.UpdateAnyTableDetails("PRICEGROUPMASTER", ListColumnNames, ListColumnValues, WhereCondition);

                if (ResultVal < 0) MessageBox.Show("Wasnt able to Edit the Price Group", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    MessageBox.Show("Updated Price Group :: " + cmbxSelectPriceGrpName.SelectedItem.ToString() + " successfully", "Update Price Group");
                    UpdateCustomerOnClose(Mode: 1);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditPriceGroupForm.btnEditPriceGrp_Click()", ex);
                throw ex;
            }
        }

        private void cmbxSelectPriceGrpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBox = (ComboBox)sender;
                if (comboBox.SelectedIndex != 0)
                {
                    string PriceGrpName = (string)comboBox.SelectedItem;
                    PriceGroupDetails ObjPriceGroupDetails = CommonFunctions.ObjCustomerMasterModel.GetPriceGrpDetails(PriceGrpName);
                    txtEditPriceGrpDesc.Text = ObjPriceGroupDetails.Description;
                    txtEditPriceGrpDiscVal.Text = ObjPriceGroupDetails.Discount.ToString();
                    cmbxEditPriceGrpCol.SelectedItem = ObjPriceGroupDetails.PriceColumn;
                    if (ObjPriceGroupDetails.DiscountType == DiscountTypes.ABSOLUTE) radioBtnEditDisTypeAbs.Checked = true;
                    else radioBtnEditDisTypePercent.Checked = true;

                    if (ObjPriceGroupDetails.IsDefault) radioBtnEditDefaultTrue.Checked = true;
                    else radioBtnEditDefaultFalse.Checked = true;
                }
            }

            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditPriceGroupForm.cmbxSelectPriceGrpName_SelectedIndexChanged()", ex);
                throw;
            }
        }
    }
}
