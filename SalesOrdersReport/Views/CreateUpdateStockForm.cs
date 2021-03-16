using SalesOrdersReport.CommonModules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SalesOrdersReport.Models;

namespace SalesOrdersReport
{
    public partial class CreateUpdateStockForm : Form
    {
        UpdateOnCloseDel UpdateStockOnClose = null;
        ProductInventoryDetails ObjProductInventoryDetailsToBeEdited;
        ProductMasterModel ObjProductMaster = null;
        public CreateUpdateStockForm(UpdateOnCloseDel UpdateStockOnClose, bool CreateForm = true, DataGridViewRow SelectedDataRow = null)
        {
            try
            {
                InitializeComponent();
                ObjProductMaster = CommonFunctions.ListProductLines[CommonFunctions.SelectedProductLineIndex].ObjProductMaster;
                String[] ArrUOM = CommonFunctions.GetUOMList().ToArray();
                cmbBoxStockMeasurementUnitList.Items.Clear();
                cmbBoxStockMeasurementUnitList.Items.AddRange(ArrUOM);
                if (cmbBoxStockMeasurementUnitList.Items.Count > 0) cmbBoxStockMeasurementUnitList.SelectedIndex = 0;

                string FormTitle = "";
                if (CreateForm)
                {
                    FormTitle = "Create Stock Product";
                    btnCreateStock.Text = "Create Stock Product";
                    rdbtnStockActiveYes.Checked = true;
                    rdbtnStockActiveNo.Enabled = false;
                }
                else
                {
                    FormTitle = "Update Stock Product";
                    btnCreateStock.Text = "Update Stock Product";
                    ObjProductInventoryDetailsToBeEdited = new ProductInventoryDetails();
                    ExtractStockDetailsFromRow(SelectedDataRow);

                }
                this.Text = FormTitle;
                txtBoxStockName.Focus();
                this.UpdateStockOnClose = UpdateStockOnClose;
                
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CreateRole.CreateUpdateStockForm()", ex);
                throw ex;
            }
        }

        void ExtractStockDetailsFromRow(DataGridViewRow SelectedRow)
        {
            try
            {
                ObjProductInventoryDetailsToBeEdited.ProductInvID = int.Parse(SelectedRow.Cells["ProductInvID"].Value.ToString());
                ObjProductInventoryDetailsToBeEdited.StockName = SelectedRow.Cells["StockName"].Value.ToString();
                ObjProductInventoryDetailsToBeEdited.ReOrderStockLevel = Double.Parse(SelectedRow.Cells["ReOrderStockLevel"].Value.ToString());
                ObjProductInventoryDetailsToBeEdited.ReOrderStockQty = Double.Parse(SelectedRow.Cells["ReorderStockQty"].Value.ToString());
                ObjProductInventoryDetailsToBeEdited.Units = Double.Parse(SelectedRow.Cells["Units"].Value.ToString());
                ObjProductInventoryDetailsToBeEdited.UnitsOfMeasurement = SelectedRow.Cells["UnitsOfMeasurement"].Value.ToString();
                ObjProductInventoryDetailsToBeEdited.Active = bool.Parse(SelectedRow.Cells["Active"].Value.ToString());

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ExtractStockDetailsFromRow()", ex);
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Text == "Create Stock Product")
                {
                    txtBoxStockName.Clear();
                    txtBoxStockName.Focus();
                }

                txtBoxStockQty.Clear();
                txtBoxStockUnits.Clear();
                txtBoxReOrderLevel.Clear();
                cmbBoxStockMeasurementUnitList.SelectedIndex = 0;
                txtBoxReOrderQty.Clear();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnReset_Click()", ex);
                throw ex;
            }
        }


        private void btnCreateStockProduct_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtBoxStockName.Text.Trim() == string.Empty)
                {
                    lblCommonValidateErrMsg.Visible = true;
                    lblCommonValidateErrMsg.Text = "Stock Name Cannot be empty!";
                    txtBoxStockName.Focus();
                    return;
                }
                if (lblCommonValidateErrMsg.Visible == true)
                {
                    lblCommonValidateErrMsg.Visible = false;
                }


                ProductInventoryDetails tmpProductInventoryDetails = new ProductInventoryDetails();
              
                tmpProductInventoryDetails.StockName = txtBoxStockName.Text;
                tmpProductInventoryDetails.Inventory = (txtBoxStockQty.Text.Trim() == string.Empty) ? 0 : Double.Parse(txtBoxStockQty.Text.Trim());
                tmpProductInventoryDetails.Units = (txtBoxStockUnits.Text.Trim() == string.Empty) ? 0 : Double.Parse(txtBoxStockUnits.Text.Trim());
                tmpProductInventoryDetails.UnitsOfMeasurement = cmbBoxStockMeasurementUnitList.SelectedItem.ToString();
                tmpProductInventoryDetails.ReOrderStockLevel = (txtBoxReOrderLevel.Text.Trim() == string.Empty) ? 0 : Double.Parse(txtBoxReOrderLevel.Text.Trim());
                tmpProductInventoryDetails.ReOrderStockQty = (txtBoxReOrderQty.Text.Trim() == string.Empty) ? 0 : Double.Parse(txtBoxReOrderQty.Text.Trim());
                tmpProductInventoryDetails.Active = (rdbtnStockActiveNo.Checked == true) ? false : true;
                tmpProductInventoryDetails.LastPODate = DateTime.Now;
                if (this.Text == "Create Stock Product")
                {
                    if (ObjProductMaster.AddNewProductInventoryDetails(tmpProductInventoryDetails) != null)
                    {
                        MessageBox.Show("New Stock :: " + txtBoxStockName.Text + " added successfully", "Stock Added");
                        UpdateStockOnClose(Mode:1);
                    }
                }
                else
                {
                    tmpProductInventoryDetails.ProductInvID = ObjProductInventoryDetailsToBeEdited.ProductInvID;
                    if (ObjProductMaster.UpdateProductInventoryDatatoDB(tmpProductInventoryDetails) == 0)
                    {
                        MessageBox.Show("Updated Stock  :: " + txtBoxStockName.Text + "  successfully", "Updated Stock");
                        UpdateStockOnClose(Mode: 1);
                    }
                }

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnCreateStockProduct_Click()", ex);
            }
        }

        private void txtBoxStockQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtBoxStockQty.Text = txtBoxStockQty.Text.Trim();
                bool isValid = CommonFunctions.ValidateDoubleORIntVal(txtBoxStockQty.Text);
                if (!isValid)
                {
                    lblCommonValidateErrMsg.Visible = true;
                    lblCommonValidateErrMsg.Text = "Enter Valid Amount(Integer/Decimal Values)!";
                    txtBoxStockQty.Focus();
                    return;
                }
                lblCommonValidateErrMsg.Visible = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("${this}.txtBoxStockQty_TextChanged()", ex);
                throw ex;
            }
        }

        private void txtBoxStockUnits_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtBoxStockUnits.Text = txtBoxStockUnits.Text.Trim();
                bool isValid = CommonFunctions.ValidateDoubleORIntVal(txtBoxStockUnits.Text);
                if (!isValid)
                {
                    lblCommonValidateErrMsg.Visible = true;
                    lblCommonValidateErrMsg.Text = "Enter Valid Amount(Integer/Decimal Values)!";
                    txtBoxStockUnits.Focus();
                    return;
                }
                lblCommonValidateErrMsg.Visible = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("${this}.txtBoxStockUnits_TextChanged()", ex);
                throw ex;
            }
        }

        private void txtBoxReOrderLevel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtBoxReOrderLevel.Text = txtBoxReOrderLevel.Text.Trim();
                bool isValid = CommonFunctions.ValidateDoubleORIntVal(txtBoxReOrderLevel.Text);
                if (!isValid)
                {
                    lblCommonValidateErrMsg.Visible = true;
                    lblCommonValidateErrMsg.Text = "Enter Valid Amount(Integer/Decimal Values)!";
                    txtBoxReOrderLevel.Focus();
                    return;
                }
                lblCommonValidateErrMsg.Visible = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("${this}.txtBoxReOrderLevel_TextChanged()", ex);
                throw ex;
            }
        }

        private void txtBoxReOrderQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtBoxReOrderQty.Text = txtBoxReOrderQty.Text.Trim();
                bool isValid = CommonFunctions.ValidateDoubleORIntVal(txtBoxReOrderQty.Text);
                if (!isValid)
                {
                    lblCommonValidateErrMsg.Visible = true;
                    lblCommonValidateErrMsg.Text = "Enter Valid Amount(Integer/Decimal Values)!";
                    txtBoxReOrderQty.Focus();
                    return;
                }
                lblCommonValidateErrMsg.Visible = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("${this}.txtBoxReOrderQty_TextChanged()", ex);
                throw ex;
            }
        }

        private void CreateUpdateStockForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Text == "Update Stock Product")
                {
                    txtBoxStockName.Enabled = false;
                    txtBoxReOrderLevel.Text = ObjProductInventoryDetailsToBeEdited.ReOrderStockLevel.ToString();
                    txtBoxReOrderQty.Text = ObjProductInventoryDetailsToBeEdited.ReOrderStockQty.ToString();
                    txtBoxStockName.Text = ObjProductInventoryDetailsToBeEdited.StockName;
                    txtBoxStockQty.Text = ObjProductInventoryDetailsToBeEdited.Inventory.ToString();
                    txtBoxStockUnits.Text = ObjProductInventoryDetailsToBeEdited.Units.ToString();
                    if (ObjProductInventoryDetailsToBeEdited.Active) rdbtnStockActiveYes.Checked = true;
                    else rdbtnStockActiveNo.Checked = true;
                    cmbBoxStockMeasurementUnitList.SelectedItem = ObjProductInventoryDetailsToBeEdited.UnitsOfMeasurement;
                }
                else txtBoxStockName.Enabled = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("${this}.CreateUpdateStockForm_Load()", ex);
                throw ex;
            }
        }
    }
}
