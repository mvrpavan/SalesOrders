using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Models;
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
    public partial class EditDiscountGroupForm : Form
    {
        UpdateOnCloseDel UpdateCustomerOnClose = null;
        MySQLHelper tmpMySQLHelper = null;
        public EditDiscountGroupForm(UpdateOnCloseDel UpdateCustomerOnClose)
        {
            InitializeComponent();
            FillDiscGrp();
            cmbxSelectDisGroupName.SelectedIndex = 0;
            cmbxSelectDisGroupName.Focus();
            this.UpdateCustomerOnClose = UpdateCustomerOnClose;
            this.FormClosed += EditDiscountGrpForm_FormClosed;
        }

        private void FillDiscGrp()
        {
            try
            {
                tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                List<string> ListDiscGrp = CommonFunctions.ObjCustomerMasterModel.GetAllDiscGrp();
                cmbxSelectDisGroupName.Items.Add("Select Discount Group");
                foreach (var item in ListDiscGrp)
                {
                    cmbxSelectDisGroupName.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditDiscountGroupForm.FillDiscGrp()", ex);
                throw ex;
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                cmbxSelectDisGroupName.SelectedIndex = 0;
                cmbxSelectDisGroupName.Focus();
                txtEditDisGrpDesc.Clear();
                txtEditDiscountVal.Text = "";
                lblValidErrMsg.Visible = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditDiscountGroupForm.btnReset_Click()", ex);
                throw ex;
            }
        }

        private void EditDiscountGrpForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateCustomerOnClose(Mode: 1);
        }

        private void btnEditDiscountGrp_Click(object sender, EventArgs e)
        {
            try
            {

                if (cmbxSelectDisGroupName.SelectedIndex == 0)
                {
                    //MessageBox.Show("User Name Or Password Cannot be empty ");
                    lblValidErrMsg.Visible = true;
                    lblValidErrMsg.Text = "Choose a Discount Group Name to Edit!";
                    return;
                }
                //if (txtEditDisGrpDesc.Text.Trim() == string.Empty)
                //{
                //    lblValidErrMsg.Visible = true;
                //    lblValidErrMsg.Text = "Description Name cant be empty! ";
                //    return;
                //}
                if (radioBtnEditDGDefaultNo.Checked == false && radioBtnEditDGDefaultYes.Checked == false)
                {
                    lblValidErrMsg.Visible = true;
                    lblValidErrMsg.Text = "Pls Set Default value is Yes/No";
                    return;
                }
                if (radioBtnEditDGDisTypeAbs.Checked == false && radioBtnEditDGDisTypePercent.Checked == false)
                {
                    lblValidErrMsg.Visible = true;
                    lblValidErrMsg.Text = "Pls Select DiscountType";
                    return;
                }
                if (txtEditDiscountVal.Text != string.Empty && !CommonFunctions.ValidateDoubleORIntVal(txtEditDiscountVal.Text))
                {
                    lblValidErrMsg.Visible = true;
                    lblValidErrMsg.Text = "Enter Valid Integer/Decimal Values!";
                    return;
                }

                if (lblValidErrMsg.Visible == true)
                {
                    lblValidErrMsg.Visible = false;
                }
                List<string> ListColumnValues = new List<string>();
                List<string> ListColumnNames = new List<string>();

                ListColumnValues.Add(txtEditDisGrpDesc.Text);
                ListColumnNames.Add("DESCRIPTION");

                ListColumnNames.Add("DISCOUNT");
                ListColumnValues.Add(txtEditDiscountVal.Text.Trim() == string.Empty ? "0" : txtEditDiscountVal.Text.Trim());

                ListColumnNames.Add("ISDEFAULT");

                if (radioBtnEditDGDefaultNo.Checked == true)
                {
                    ListColumnValues.Add("0");
                }
                else ListColumnValues.Add("1");


                ListColumnNames.Add("DISCOUNTTYPE");

                if (radioBtnEditDGDisTypeAbs.Checked == true)
                {
                    ListColumnValues.Add("ABSOLUTE");
                }
                else ListColumnValues.Add("PERCENT");

                string WhereCondition = "DISCOUNTGROUPNAME = '" + cmbxSelectDisGroupName.SelectedItem.ToString() + "'";
                tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                int ResultVal = CommonFunctions.ObjUserMasterModel.UpdateAnyTableDetails("DISCOUNTGROUPMASTER", ListColumnNames, ListColumnValues, WhereCondition);

                if (ResultVal < 0) MessageBox.Show("Wasnt able to Edit the Discount Group", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                else
                {
                    MessageBox.Show("Updated Discount Group :: " + cmbxSelectDisGroupName.SelectedItem.ToString() + " successfully", "Update Discount Group");
                    UpdateCustomerOnClose(Mode: 1);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditDiscountGroupForm.btnEditDiscountGrp_Click()", ex);
                throw ex;
            }
        }

        private void cmbxSelectDisGroupName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBox = (ComboBox)sender;
                if (comboBox.SelectedIndex != 0)
                {
                    string DiscountName = (string)comboBox.SelectedItem;
                    DiscountGroupDetails1 ObjDiscountGroupDetails = CommonFunctions.ObjCustomerMasterModel.GetDiscountGrpDetails(DiscountName);
                    txtEditDisGrpDesc.Text = ObjDiscountGroupDetails.Description;
                    txtEditDiscountVal.Text = ObjDiscountGroupDetails.Discount.ToString();
                    if (ObjDiscountGroupDetails.DiscountType == DiscountTypes.ABSOLUTE) radioBtnEditDGDisTypeAbs.Checked = true;
                    else radioBtnEditDGDisTypePercent.Checked = true;

                    if (ObjDiscountGroupDetails.IsDefault) radioBtnEditDGDefaultYes.Checked = true;
                    else radioBtnEditDGDefaultNo.Checked = true;
                }
            }

            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditDiscountGroupForm.cmbxSelectDisGroupName_SelectedIndexChanged()", ex);
                throw;
            }
        }
    }
}
