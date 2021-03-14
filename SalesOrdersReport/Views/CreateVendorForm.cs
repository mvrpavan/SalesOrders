using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Models;
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
    public partial class CreateVendorForm : Form
    {
        UpdateOnCloseDel UpdateVendorOnClose = null;
        UpdateUsingObjectOnCloseDel UpdateObjectOnClose = null;
        Boolean IsRetailVendor = false;

        public CreateVendorForm(UpdateOnCloseDel UpdateVendorOnClose, UpdateUsingObjectOnCloseDel UpdateObjectOnClose = null, Boolean IsRetailVendor = false)
        {
            try
            {
                InitializeComponent();
                txtCreateVendorName.Focus();
                FillStates();
                cmbxCreateCustSelectState.SelectedIndex = 0;
                rdbtnCustActiveYes.Checked = true;

                this.UpdateVendorOnClose = UpdateVendorOnClose;

                this.UpdateObjectOnClose = UpdateObjectOnClose;
                this.IsRetailVendor = IsRetailVendor;
                //cmbxVendorType.DataSource = CommonFunctions.ObjVendorMasterModel.GetAllVendorTypes();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateVendorForm()", ex);
                throw ex;
            }
        }
        private void FillStates()
        {
            try
            {
                List<string> ListStates = CommonFunctions.ObjCustomerMasterModel.GetAllStatesOfIndia();
                cmbxCreateCustSelectState.Items.Add("Select State");
                foreach (var item in ListStates)
                {
                    cmbxCreateCustSelectState.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateVendorForm.FillStates()", ex);
                throw ex;
            }
        }

        private void btnCreateVendorReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtCreateVendorName.Clear();
                txtCustAddress.Clear();
                txtGSTIN.Clear();
                txtCreateCustPhone.Clear();
                cmbxCreateCustSelectState.SelectedIndex = 0; ;
                txtCreateVendorName.Focus();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateVendorForm.btnCreateVendorReset_Click()", ex);
                throw ex;
            }
        }
        private void btnCreateVendor_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCreateVendorName.Text.Trim() == string.Empty)
                {
                    lblCommonErrorMsg.Visible = true;
                    lblCommonErrorMsg.Text = "Vendor Name Cant be empty!";
                    return;
                }
                if (rdbtnCustActiveYes.Checked == false && rdbtnCustActiveNo.Checked == false)
                {
                    lblCommonErrorMsg.Visible = true;
                    lblCommonErrorMsg.Text = "Pls Set whether Vendor Is Active or Not ";
                    return;
                }

                if (lblCommonErrorMsg.Visible == true)
                {
                    lblCommonErrorMsg.Visible = false;
                }
                //&&&&&
                //if (txtCreateCustPhone.Text != String.Empty)
                //{
                //    VendorDetails VendorDetails = CommonFunctions.ObjVendorMaster.GetVendorDetailsByPhoneNo(txtCreateCustPhone.Text);
                //    if (VendorDetails != null)
                //    {
                //        MessageBox.Show(this, "Vendor with same Phone number already exists.\nCannot create another Vendor with same Phone number.", "Create Vendor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        return;
                //    }
                //}

                List<string> ListColumnValues = new List<string>();
                List<string> ListColumnNamesWithDataType = new List<string>();
                VendorDetails ObjVendorDetails = new VendorDetails();
                ObjVendorDetails.VendorName = txtCreateVendorName.Text.Trim();
                ObjVendorDetails.Address = txtCustAddress.Text.Trim();
                ObjVendorDetails.GSTIN = txtGSTIN.Text.Trim();

                if (txtCreateCustPhone.Text.Trim() != string.Empty)
                {
                    if (!CheckForValidPhone()) return;
                    ObjVendorDetails.PhoneNo = txtCreateCustPhone.Text;
                }
                if (cmbxCreateCustSelectState.SelectedIndex > 0)
                {
                    ObjVendorDetails.StateID = CommonFunctions.ObjCustomerMasterModel.GetStateID(cmbxCreateCustSelectState.SelectedItem.ToString());
                }
                if (rdbtnCustActiveNo.Checked == true) ObjVendorDetails.Active = false;
                else ObjVendorDetails.Active = true;

                ObjVendorDetails.LastUpdateDate = ObjVendorDetails.AddedDate = DateTime.Now;

                VendorDetails ObjVendorTmp = CommonFunctions.ObjVendorMaster.CreateNewVendor(ObjVendorDetails);
                if (ObjVendorTmp == null) MessageBox.Show("Wasnt able to create the Vendor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ////&&&&&
                //else if (ResultVal == 2)
                //{
                //    MessageBox.Show("Vendor Name already exists! Pls Provide Another Vendor Name.", "Error");
                //}
                else
                {
                    MessageBox.Show("Added New Vendor :: " + txtCreateVendorName.Text + " successfully", "Added Vendor");
                    if (UpdateVendorOnClose != null) UpdateVendorOnClose(Mode: 1);
                    VendorDetails tmpVendorDetails = CommonFunctions.ObjVendorMaster.GetVendorDetails(txtCreateVendorName.Text);
                    if (UpdateObjectOnClose != null)
                    {
                        UpdateObjectOnClose(1, tmpVendorDetails);
                        this.Close();
                    }
                    else
                    {
                        btnReset.PerformClick();
                    }
                }

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateVendorForm.btnCreateVendor_Click()", ex);
                throw ex;
            }

        }

        private bool CheckForValidPhone()
        {
            try
            {
                bool IsValid = IsValid = CommonFunctions.ValidatePhoneNo(txtCreateCustPhone.Text);
                if (!IsValid)
                {
                    lblCommonErrorMsg.Visible = true;
                    lblCommonErrorMsg.Text = "Enter Valid Phone No!"; ;
                    txtCreateCustPhone.Focus();
                }
                else
                {
                    lblCommonErrorMsg.Visible = false;
                }
                return IsValid;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateVendorForm.CheckForValidPhone()", ex);
                throw ex;
            }
        }
    }
}
