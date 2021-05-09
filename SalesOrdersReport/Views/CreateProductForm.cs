using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Models;

namespace SalesOrdersReport.Views
{
    partial class CreateProductForm : Form
    {
        Boolean IsAddProduct = true;
        UpdateUsingObjectOnCloseDel UpdateOnClose = null;
        Int32 ProductIDToEdit = -1;
        ProductMasterModel ObjProductMaster = null;

        public CreateProductForm(Boolean IsAddProduct, Int32 ProductID, UpdateUsingObjectOnCloseDel UpdateOnClose)
        {
            try
            {
                InitializeComponent();
                this.IsAddProduct = IsAddProduct;
                this.UpdateOnClose = UpdateOnClose;
                FormClosed += AddProductForm_FormClosed;
                ProductIDToEdit = ProductID;

                ObjProductMaster = CommonFunctions.ListProductLines[CommonFunctions.SelectedProductLineIndex].ObjProductMaster;

                cmbBoxCategoryList.Items.Clear();
                cmbBoxCategoryList.Items.AddRange(ObjProductMaster.GetProductCategoryList().ToArray());
                if(cmbBoxCategoryList.Items.Count > 0) cmbBoxCategoryList.SelectedIndex = 0;

                String[] ArrUOM = CommonFunctions.GetUOMList().ToArray();
                cmbBoxMeasurementUnitList.Items.Clear();
                cmbBoxMeasurementUnitList.Items.AddRange(ArrUOM);
                if (cmbBoxMeasurementUnitList.Items.Count > 0) cmbBoxMeasurementUnitList.SelectedIndex = 0;

                cmbBoxStockMeasurementUnitList.Items.Clear();
                cmbBoxStockMeasurementUnitList.Items.AddRange(ArrUOM);
                if (cmbBoxStockMeasurementUnitList.Items.Count > 0) cmbBoxStockMeasurementUnitList.SelectedIndex = 0;

                List<String> ListHSNCodes = ObjProductMaster.GetHSNCodeList();
                cmbBoxHSNCodeList.Items.Clear();
                cmbBoxHSNCodeList.Items.AddRange(ListHSNCodes.ToArray());
                if (cmbBoxHSNCodeList.Items.Count > 0) cmbBoxHSNCodeList.SelectedIndex = 0;

                List<String> ListVendors = CommonFunctions.ObjVendorMaster.GetVendorList();
                cmbBoxVendors.Items.Clear();
                cmbBoxVendors.Items.Add("Select Vendor Name");
                cmbBoxVendors.Items.AddRange(ListVendors.ToArray());
                if (cmbBoxVendors.Items.Count > 0) cmbBoxVendors.SelectedIndex = 0;

                List<String> ListStockProducts = ObjProductMaster.GetStockProductsList();
                cmbBoxStockList.Items.Clear();
                cmbBoxStockList.Items.AddRange(ListStockProducts.ToArray());
                if (cmbBoxStockList.Items.Count > 0) cmbBoxStockList.SelectedIndex = 0;
                btnOK.CausesValidation = true;
                btnCreateUpdateClose.CausesValidation = true;
  
                txtBoxPurchasePrice.Validating += Value_Validating;
                txtBoxMaxRetailPrice.Validating += Value_Validating;
                txtBoxWholePrice.Validating += Value_Validating;
                txtBoxRetailPrice.Validating += Value_Validating;
                txtBoxStockQty.Validating += Value_Validating;

                if (IsAddProduct)
                {
                    Text = "Add new Product";

                    SetControlsForNewProduct();
                }
                else
                {
                    Text = "Edit Product details";

                    ProductDetails tmpProductDetails = ObjProductMaster.GetProductDetails(ProductIDToEdit);
                    txtBoxSKUID.Text = tmpProductDetails.ProductSKU;
                    txtBoxProductName.Text = tmpProductDetails.ItemName;
                    txtBoxProductDesc.Text = tmpProductDetails.ProductDesc;
                    cmbBoxCategoryList.SelectedIndex = cmbBoxCategoryList.Items.IndexOf(tmpProductDetails.CategoryName);
                    txtBoxUnits.Text = tmpProductDetails.Units.ToString();
                    cmbBoxMeasurementUnitList.SelectedIndex = cmbBoxMeasurementUnitList.Items.IndexOf(tmpProductDetails.UnitsOfMeasurement);
                    txtBoxSortName.Text = tmpProductDetails.SortName;
                    cmbBoxHSNCodeList.SelectedIndex = cmbBoxHSNCodeList.Items.IndexOf(tmpProductDetails.HSNCode);
                    txtBoxStockName.Text = tmpProductDetails.StockName;
                    txtBoxSKUID.Enabled = false;
                    txtBoxStockName.Enabled = false;
                    chkBoxChooseExistingStock.Enabled = true;
                    chkBoxChooseExistingStock.Checked = true;
                    cmbBoxStockList.Enabled = true;
                    //txtBoxReOrderLevel.Enabled = false;
                    //txtBoxReOrderQty.Enabled = false;
                    chkBoxIsActive.Checked = tmpProductDetails.Active;

                    ProductInventoryDetails tmpProductInventoryDetails = ObjProductMaster.GetProductInventoryDetails(tmpProductDetails.ProductInvID);
                    cmbBoxStockList.SelectedIndex = cmbBoxStockList.Items.IndexOf(tmpProductInventoryDetails.StockName);
                    cmbBoxVendors.SelectedIndex = tmpProductDetails.VendorName == null ? 0 : cmbBoxVendors.Items.IndexOf(tmpProductDetails.VendorName);
                    txtBoxStockQty.Text = tmpProductInventoryDetails.Inventory.ToString();
                    txtBoxStockUnits.Text = tmpProductInventoryDetails.Units.ToString();
                    cmbBoxStockMeasurementUnitList.SelectedIndex = cmbBoxMeasurementUnitList.Items.IndexOf(tmpProductInventoryDetails.UnitsOfMeasurement);
                    txtBoxReOrderLevel.Text = tmpProductInventoryDetails.ReOrderStockLevel.ToString();
                    txtBoxReOrderQty.Text = tmpProductInventoryDetails.ReOrderStockQty.ToString();

                    txtBoxPurchasePrice.Text = tmpProductDetails.PurchasePrice.ToString("F");
                    txtBoxWholePrice.Text = tmpProductDetails.WholesalePrice.ToString("F");
                    txtBoxRetailPrice.Text = tmpProductDetails.RetailPrice.ToString("F");
                    txtBoxMaxRetailPrice.Text = tmpProductDetails.MaxRetailPrice.ToString("F");

                    if (tmpProductDetails.ArrBarcodes.Length > 0)
                        txtBoxBarcodes.Text = String.Join(",", tmpProductDetails.ArrBarcodes);

                    btnOK.Text = "Update and Close";
                    btnOK.Enabled = false;
                    btnCreateUpdateClose.Text = "Update and Close";
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("AddProductForm.ctor()", ex);
            }
        }

        private void SetControlsForNewProduct()
        {
            try
            {
                txtBoxSKUID.Text = ObjProductMaster.GenerateNewSKUID();
                txtBoxUnits.Text = "1.0";
                txtBoxStockUnits.Text = "1.0";
                chkBoxIsActive.Checked = true;
                chkBoxChooseExistingStock.Checked = false;
                txtBoxStockName.Enabled = true;
                cmbBoxStockList.Enabled = false;
                txtBoxReOrderLevel.Text = "10";
                txtBoxReOrderQty.Text = "10";
                txtBoxReOrderLevel.Enabled = true;
                txtBoxReOrderQty.Enabled = true;
                txtBoxStockQty.Text = "0";
                txtBoxPurchasePrice.Text = "10.0";
                txtBoxMaxRetailPrice.Text = "10.0";
                txtBoxWholePrice.Text = "10.0";
                txtBoxRetailPrice.Text = "10.0";

                btnOK.Text = "Create and Continue";
                btnCreateUpdateClose.Text = "Create and Close";
                txtBoxSKUID.Focus();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.SetControlsForNewProduct()", ex);
            }
        }

        private void AddProductForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.AddProductForm_FormClosed()", ex);
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new CreateProductCategoryForm(true, "", UpdateOnCloseProductForm), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnAddCategory_Click()", ex);
            }
        }

        private void btnAddHSNCode_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new CreateTaxForm(true, -1, UpdateOnCloseProductForm), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("AddProductForm.btnAddHSNCode_Click()", ex);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (CreateUpdateProduct() == 0)
                {
                    SetControlsForNewProduct();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("AddProductForm.btnOK_Click()", ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnCancel_Click()", ex);
            }
        }

        Int32 CreateUpdateProduct()
        {
            try
            {
                if (!ValidateChildren(ValidationConstraints.Enabled))
                {
                    MessageBox.Show(this, "Invalid input for some of the values", "Input validation", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return -1;
                }

                if (ProductIDToEdit < 0)
                {
                    ProductDetails productDetails = ObjProductMaster.GetProductDetails(txtBoxProductName.Text.Trim());
                    if (productDetails != null)
                    {
                        MessageBox.Show(this, "Product with same name already exists.", "Add Product", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        return -1;
                    }
                }

                ProductDetails tmpProductDetails = new ProductDetails()
                {
                    ProductID = ProductIDToEdit,
                    ProductSKU = txtBoxSKUID.Text,
                    ItemName = txtBoxProductName.Text,
                    ProductDesc = txtBoxProductDesc.Text,
                    CategoryName = cmbBoxCategoryList.SelectedItem.ToString(),
                    Units = (!String.IsNullOrEmpty(txtBoxUnits.Text.Trim()) ? Double.Parse(txtBoxUnits.Text.Trim()) : 0),
                    UnitsOfMeasurement = cmbBoxMeasurementUnitList.SelectedItem.ToString(),
                    SortName = txtBoxSortName.Text,
                    HSNCode = cmbBoxHSNCodeList.SelectedItem.ToString(),
                    StockName = txtBoxStockName.Text,
                    VendorName = (cmbBoxVendors.SelectedIndex == 0) ? "" : cmbBoxVendors.SelectedItem.ToString(),
                    PurchasePrice = Double.Parse(txtBoxPurchasePrice.Text),
                    WholesalePrice = Double.Parse(txtBoxWholePrice.Text),
                    RetailPrice = Double.Parse(txtBoxRetailPrice.Text),
                    MaxRetailPrice = Double.Parse(txtBoxMaxRetailPrice.Text),
                    ArrBarcodes = txtBoxBarcodes.Text.Trim().Split(','),
                    Active = chkBoxIsActive.Checked
                };
                DateTime NowDate = DateTime.Now;
                ProductInventoryDetails tmpProductInventoryDetails = new ProductInventoryDetails()
                {
                    StockName = tmpProductDetails.StockName,
                    Inventory = Double.Parse(txtBoxStockQty.Text),
                    Units = Double.Parse(txtBoxStockUnits.Text.Trim()),
                    UnitsOfMeasurement = cmbBoxStockMeasurementUnitList.SelectedItem.ToString(),
                    ReOrderStockLevel = Double.Parse(txtBoxReOrderLevel.Text),
                    ReOrderStockQty = Double.Parse(txtBoxReOrderQty.Text),
                    LastUpdateDate = NowDate,
                    LastPODate = NowDate
                };

                tmpProductDetails = ObjProductMaster.AddUpdateProductDetails(tmpProductDetails, tmpProductInventoryDetails);

                UpdateOnClose(1, tmpProductDetails);

                CommonFunctions.ResetTextBoxesRecursive(this);

                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreateUpdateProduct()", ex);
                return -1;
            }
        }

        private void UpdateOnCloseProductForm(Int32 Mode, Object ObjAddUpdated = null)
        {
            try
            {
                String SelectedItem = "";
                switch (Mode)
                {
                    case 1:     //Category
                        SelectedItem = "";
                        if (cmbBoxCategoryList.SelectedIndex >= 0) SelectedItem = cmbBoxCategoryList.SelectedText;
                        cmbBoxCategoryList.Items.Clear();
                        cmbBoxCategoryList.Items.AddRange(ObjProductMaster.GetProductCategoryList().ToArray());
                        if (cmbBoxCategoryList.Items.Count > 0)
                        {
                            if (!String.IsNullOrEmpty(SelectedItem)) cmbBoxCategoryList.SelectedText = SelectedItem;
                            else cmbBoxCategoryList.SelectedIndex = 0;
                        }
                        break;
                    case 2:     //Add HSN code
                        HSNCodeDetails UpdatedHSNCodeDetails = (HSNCodeDetails)ObjAddUpdated;
                        List<String> ListHSNCodes = ObjProductMaster.GetHSNCodeList();
                        cmbBoxHSNCodeList.Items.Clear();
                        cmbBoxHSNCodeList.Items.AddRange(ListHSNCodes.ToArray());
                        if (cmbBoxHSNCodeList.Items.Count > 0)
                            cmbBoxHSNCodeList.SelectedIndex = ListHSNCodes.IndexOf(UpdatedHSNCodeDetails.HSNCode);
                        break;
                    case 3:     //Delete HSN code
                        String HSNCode = (String)ObjAddUpdated;
                        SelectedItem = "";
                        if (cmbBoxHSNCodeList.SelectedIndex >= 0) SelectedItem = cmbBoxHSNCodeList.SelectedItem.ToString();
                        Int32 Index = cmbBoxHSNCodeList.Items.IndexOf(HSNCode);
                        cmbBoxHSNCodeList.Items.RemoveAt(Index);
                        if (cmbBoxHSNCodeList.Items.Count > 0)
                        {
                            Index = cmbBoxHSNCodeList.Items.IndexOf(SelectedItem);
                            cmbBoxHSNCodeList.SelectedIndex = Index;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateOnClose()", ex);
            }
        }

        private void btnCreateUpdateClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (CreateUpdateProduct() == 0) this.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnCreateUpdateClose_Click()", ex);
            }
        }

        private void txtBoxProductName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtBoxSortName.Text = txtBoxProductName.Text;
                txtBoxStockName.Text = txtBoxProductName.Text;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.txtBoxProductName_TextChanged()", ex);
            }
        }

        private void chkBoxChooseExistingStock_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxStockList.Items.Count == 0)
                {
                    if (chkBoxChooseExistingStock.Checked)
                    {
                        MessageBox.Show(this, "No Stock Product is available to Choose", "Select Stock Product", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        chkBoxChooseExistingStock.Checked = false;
                    }
                    return;
                }

                txtBoxStockName.Enabled = !chkBoxChooseExistingStock.Checked;
                cmbBoxStockList.Enabled = chkBoxChooseExistingStock.Checked;
                //txtBoxStockQty.Enabled = txtBoxStockName.Enabled;
                //txtBoxReOrderLevel.Enabled = txtBoxStockName.Enabled;
                //txtBoxReOrderQty.Enabled = txtBoxStockName.Enabled;
                //txtBoxStockUnits.Enabled = txtBoxStockName.Enabled;
                //cmbBoxStockMeasurementUnitList.Enabled = txtBoxStockName.Enabled;

                if (ProductIDToEdit < 0)
                {
                    cmbBoxStockList.SelectedIndex = -1;
                    cmbBoxStockList.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.chkBoxChooseExistingStock_CheckedChanged()", ex);
            }
        }

        private void cmbBoxStockList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxStockList.SelectedIndex >= 0 && IsFormLoaded && chkBoxChooseExistingStock.Checked)
                {
                    txtBoxStockName.Text = cmbBoxStockList.SelectedItem.ToString();
                    ProductInventoryDetails tmpProductInventoryDetails = ObjProductMaster.GetStockProductDetails(txtBoxStockName.Text);
                    txtBoxStockQty.Text = tmpProductInventoryDetails.Inventory.ToString();
                    txtBoxReOrderLevel.Text = tmpProductInventoryDetails.ReOrderStockLevel.ToString();
                    txtBoxReOrderQty.Text = tmpProductInventoryDetails.ReOrderStockQty.ToString();
                    txtBoxStockUnits.Text = tmpProductInventoryDetails.Units.ToString();
                    cmbBoxStockMeasurementUnitList.SelectedIndex = cmbBoxMeasurementUnitList.Items.IndexOf(tmpProductInventoryDetails.UnitsOfMeasurement);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxStockList_SelectedIndexChanged()", ex);
            }
        }

        private void Name_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                TextBox ObjTextBox = (TextBox)sender;
                if (ObjTextBox.Text.Trim().Length == 0)
                {
                    e.Cancel = true;
                    ObjTextBox.Focus();
                    errorProviderAddProductForm.SetError(ObjTextBox, "Provide a valid Name");
                }
                else
                {
                    e.Cancel = false;
                    errorProviderAddProductForm.SetError(ObjTextBox, "");
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.Name_Validating()", ex);
            }
        }

        private void Value_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                TextBox ObjTextBox = (TextBox)sender;
                Double value = 0;
                if (ObjTextBox.Text.Trim().Length == 0 || !Double.TryParse(ObjTextBox.Text, out value))
                {
                    e.Cancel = true;
                    ((TextBox)sender).Focus();
                    errorProviderAddProductForm.SetError(ObjTextBox, "Provide a valid value");
                }
                else
                {
                    e.Cancel = false;
                    errorProviderAddProductForm.SetError(ObjTextBox, "");
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.Value_Validating()", ex);
            }
        }

        private void Price_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                TextBox ObjTextBox = (TextBox)sender;
                Double value = 0;
                if (ObjTextBox.Text.Trim().Length == 0 || !Double.TryParse(ObjTextBox.Text, out value))
                {
                    e.Cancel = true;
                    ((TextBox)sender).Focus();
                    errorProviderAddProductForm.SetError(ObjTextBox, "Provide a valid value");
                }
                else if (value <= 0)
                {
                    e.Cancel = true;
                    ((TextBox)sender).Focus();
                    errorProviderAddProductForm.SetError(ObjTextBox, "Provide a value more than 0");
                }
                else
                {
                    e.Cancel = false;
                    errorProviderAddProductForm.SetError(ObjTextBox, "");
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.Price_Validating()", ex);
            }
        }

        Boolean IsFormLoaded = false;
        private void AddProductForm_Shown(object sender, EventArgs e)
        {
            try
            {
                IsFormLoaded = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.AddProductForm_Shown()", ex);
            }
        }

        private void btnEditHSNCode_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxHSNCodeList.SelectedIndex < 0) return;

                HSNCodeDetails ObjHSNCodeDetails = ObjProductMaster.GetHSNCodeDetails(cmbBoxHSNCodeList.Items[cmbBoxHSNCodeList.SelectedIndex].ToString());

                CommonFunctions.ShowDialog(new CreateTaxForm(false, ObjHSNCodeDetails.TaxID, UpdateOnCloseProductForm), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnEditHSNCode_Click()", ex);
            }
        }
    }
}
