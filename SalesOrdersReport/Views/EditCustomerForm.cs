using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SalesOrdersReport
{
    public partial class EditCustomerForm : Form
    {

        MySQLHelper tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
        UpdateOnCloseDel UpdateCustomerOnClose = null;
        public EditCustomerForm(UpdateOnCloseDel UpdateCustomerOnClose)
        {
            InitializeComponent();
            txtEditCustomerName.Focus();
            FillState();//&&&&& check everytime needs to be filled or one time
            cmbxEditCustSelectState.SelectedIndex = 0;
            FillDiscGrp();
            cmbxEditCustSelectDiscGrp.SelectedIndex = 0;
            FillPriceGrp();
            cmbxEditCustSelectPriceGrp.SelectedIndex = 0;
            FillLines();
            cmbxEditCustSelectLine.SelectedIndex = 0;

            this.UpdateCustomerOnClose = UpdateCustomerOnClose;
            this.FormClosed += EditCustomerForm_FormClosed;
        }
        private void FillState()
        {
            try
            {
                tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                List<string> ListStates = CommonFunctions.ObjCustomerMasterModel.GetAllStatesOfIndia();
                cmbxEditCustSelectState.Items.Add("Select State");
                foreach (var item in ListStates)
                {
                    cmbxEditCustSelectState.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditCustomerForm.FillState()", ex);
                throw ex;
            }
        }

        private void FillDiscGrp()
        {
            try
            {
                tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                List<string> ListDiscGrp = CommonFunctions.ObjCustomerMasterModel.GetAllDiscGrp();
                cmbxEditCustSelectDiscGrp.Items.Add("Select Discount Group");
                foreach (var item in ListDiscGrp)
                {
                    cmbxEditCustSelectDiscGrp.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditCustomerForm.FillDiscGrp()", ex);
                throw ex;
            }
        }
        private void FillPriceGrp()
        {
            try
            {
                tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                List<string> ListPriceGrp = CommonFunctions.ObjCustomerMasterModel.GetAllPriceGrp();
                cmbxEditCustSelectPriceGrp.Items.Add("Select Price Group");
                foreach (var item in ListPriceGrp)
                {
                    cmbxEditCustSelectPriceGrp.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditCustomerForm.FillPriceGrp()", ex);
                throw ex;
            }
        }

        private void FillLines()
        {
            try
            {
                tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                List<string> ListLines = CommonFunctions.ObjCustomerMasterModel.GetAllLineNames();
                cmbxEditCustSelectLine.Items.Add("Select Line");
                foreach (var item in ListLines)
                {
                    cmbxEditCustSelectLine.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditCustomerForm.FillLines()", ex);
                throw ex;
            }
        }
        private void btnEditCutomerReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtEditCustAddress.Clear();
                txtEditGSTIN.Clear();
                txtEditCustPhone.Clear();
                cmbxEditCustSelectState.SelectedIndex = 0;
                cmbxEditCustSelectLine.SelectedIndex = 0;
                cmbxEditCustSelectDiscGrp.SelectedIndex = 0;
                cmbxEditCustSelectPriceGrp.SelectedIndex = 0;
                txtEditCustomerName.Focus();
                //chbxMonday.Checked = false;
                //chbxTuesday.Checked = false;
                //chbxWednesday.Checked = false;
                //chbxThursday.Checked = false;
                //chbxFriday.Checked = false;
                //chbxSaturday.Checked = false;
                //chbxSunday.Checked = false;
                foreach (Control itemControl in flpEditCustOrderDays.Controls)
                {
                    CheckBox chbxControl = (CheckBox)itemControl;
                    chbxControl.Checked = false;
                }
                chbxMonday.Checked = true;

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditCustomerForm.btnReset_Click()", ex);
                throw ex;
            }
        }
        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                //if (cmbxEditCustSelectLine.SelectedIndex == 0)
                //{
                //    lblCommonErrorMsg.Visible = true;
                //    lblCommonErrorMsg.Text = "Select a Line for the Customer!! ";
                //    return;
                //}
                if (rdbtnEditCustActiveYes.Checked == false && rdbtnEditCustActiveNo.Checked == false)
                {
                    lblCommonErrorMsg.Visible = true;
                    lblCommonErrorMsg.Text = "Pls Set whether Customer Is Active or Not ";
                    return;
                }
                if (txtEditCustPhone.Text.Trim() != string.Empty)
                {
                    if (!CheckForValidPhone()) return;
                }
                if (lblCommonErrorMsg.Visible == true)
                {
                    lblCommonErrorMsg.Visible = false;
                }

                List<string> ListColumnValues = new List<string>();
                List<string> ListColumnNames = new List<string>();
                //if (txtEditCustAddress.Text.Trim() != string.Empty)
                //{
                ListColumnValues.Add(txtEditCustAddress.Text);
                ListColumnNames.Add("ADDRESS");
                //}
                //if (txtEditGSTIN.Text.Trim() != string.Empty)
                //{
                ListColumnValues.Add(txtEditGSTIN.Text);
                ListColumnNames.Add("GSTIN");
                //}

                ListColumnValues.Add(txtEditCustPhone.Text.Trim() == string.Empty ? "NULL" : txtEditCustPhone.Text.Trim());
                ListColumnNames.Add("PHONENO");
                //}
                //if (cmbxEditCustSelectLine.SelectedIndex > 0)
                //{
                int LineID = CommonFunctions.ObjCustomerMasterModel.GetLineID(cmbxEditCustSelectLine.SelectedItem.ToString());
                ListColumnValues.Add(LineID == -1 ? "NULL" : LineID.ToString());
                ListColumnNames.Add("LINEID");
                //}
                //if (cmbxEditCustSelectDiscGrp.SelectedIndex > 0)
                //{
                int DiscGrpID = CommonFunctions.ObjCustomerMasterModel.GetDisGrpID(cmbxEditCustSelectDiscGrp.SelectedItem.ToString());
                ListColumnValues.Add(DiscGrpID == -1 ? "NULL" : DiscGrpID.ToString());
                ListColumnNames.Add("DISCOUNTGROUPID");
                //}
                //if (cmbxEditCustSelectPriceGrp.SelectedIndex > 0)
                //{
                int PriceGrpID = CommonFunctions.ObjCustomerMasterModel.GetPriceGrpID(cmbxEditCustSelectPriceGrp.SelectedItem.ToString());
                ListColumnValues.Add(PriceGrpID == -1 ? "NULL" : PriceGrpID.ToString());
                ListColumnNames.Add("PRICEGROUPID");
                //}
                //if (cmbxEditCustSelectState.SelectedIndex > 0)
                //{
                int StateID = CommonFunctions.ObjCustomerMasterModel.GetStateID(cmbxEditCustSelectState.SelectedItem.ToString());
                ListColumnValues.Add(StateID == -1 ? "NULL" : StateID.ToString());
                ListColumnNames.Add("STATEID");
                //}
                string SelectedOrderEndDays = ""; bool AtleastOneChecked = false;
                foreach (Control itemControl in flpEditCustOrderDays.Controls)
                {
                    CheckBox chbxControl = (CheckBox)(itemControl);
                    if (chbxControl.Checked)
                    {
                        SelectedOrderEndDays += CommonFunctions.ObjCustomerMasterModel.GetOrderDaysCode(chbxControl.Text) + ",";
                        if (!AtleastOneChecked) AtleastOneChecked = true;
                    }
                }
                if (AtleastOneChecked) SelectedOrderEndDays = SelectedOrderEndDays.Remove(SelectedOrderEndDays.Length - 1, 1);

                ListColumnValues.Add(SelectedOrderEndDays);
                ListColumnNames.Add("ORDERDAYS");


                ListColumnValues.Add(DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));
                ListColumnNames.Add("LASTUPDATEDATE");


                string WhereCondition = "CUSTOMERNAME = '" + txtEditCustomerName.Text + "'";
                tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                int ResultVal = CommonFunctions.ObjUserMasterModel.UpdateAnyTableDetails("CUSTOMERMASTER", ListColumnNames, ListColumnValues, WhereCondition);

                if (ResultVal < 0) MessageBox.Show("Wasnt able to Edit the Customer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                else
                {
                    MessageBox.Show("Updated Customer :: " + txtEditCustomerName.Text + " successfully", "Update Customer");
                    UpdateCustomerOnClose(Mode: 1);
                }

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditCustomerForm.btnEditCustomer_Click()", ex);
                throw ex;
            }
        }



        private bool CheckForValidPhone()
        {
            try
            {
                bool IsValid = IsValid = CommonFunctions.ValidatePhoneNo(txtEditCustPhone.Text);
                if (!IsValid)
                {
                    lblCommonErrorMsg.Visible = true;
                    lblCommonErrorMsg.Text = "Enter Valid Phone No!"; ;
                    txtEditCustPhone.Focus();
                }
                else
                {
                    lblCommonErrorMsg.Visible = false;
                }
                return IsValid;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditCustomerForm.CheckForValidPhone()", ex);
                throw ex;
            }
        }
        private void cmbxEditCustSelectLine_Click(object sender, EventArgs e)
        {
            try
            {
                cmbxEditCustSelectLine.DroppedDown = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditCustomerForm.cmbxEditCustSelectLine_Click()", ex);
                throw ex;
            }
        }

        private void cmbxEditCustSelectDiscGrp_Click(object sender, EventArgs e)
        {
            try
            {
                cmbxEditCustSelectDiscGrp.DroppedDown = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditCustomerForm.cmbxEditCustSelectDiscGrp_Click()", ex);
                throw ex;
            }
        }
        private void EditCustomerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                UpdateCustomerOnClose(Mode: 1);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditCustomerForm.EditCustomerForm_FormClosed()", ex);
            }
        }
    }
}
