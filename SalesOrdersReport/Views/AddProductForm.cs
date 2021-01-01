using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesOrdersReport.Views
{
    public partial class AddProductForm : Form
    {
        Boolean IsAddProduct = true;
        UpdateOnCloseDel UpdateOnClose = null;
        Int32 ProductIDToEdit = -1;
        ProductMasterModel ObjProductMaster = null;

        public AddProductForm(Boolean IsAddProduct, Int32 ProductID, UpdateOnCloseDel UpdateOnClose)
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

                if (IsAddProduct)
                {
                    Text = "Add new Product";

                    dtGridViewPrices.Rows.Clear();
                    DataTable dtProductPrices = ObjProductMaster.GetPriceColumnPricesForProduct(-1);
                    dtGridViewPrices.DataSource = dtProductPrices;
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
                    chkBoxChooseExistingStock.Enabled = false;
                    cmbBoxStockList.Enabled = false;
                    ProductInventoryDetails tmpProductInventoryDetails = ObjProductMaster.GetProductInventoryDetails(tmpProductDetails.ProductInvID);
                    txtBoxStockUnits.Text = tmpProductInventoryDetails.Units.ToString();
                    cmbBoxStockMeasurementUnitList.SelectedIndex = cmbBoxMeasurementUnitList.Items.IndexOf(tmpProductInventoryDetails.UnitsOfMeasurement);
                    txtBoxReOrderLevel.Text = tmpProductInventoryDetails.ReOrderStockLevel.ToString();
                    txtBoxReOrderQty.Text = tmpProductInventoryDetails.ReOrderStockQty.ToString();

                    dtGridViewPrices.Rows.Clear();
                    DataTable dtProductPrices = ObjProductMaster.GetPriceColumnPricesForProduct(ProductID);
                    dtGridViewPrices.DataSource = dtProductPrices;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("AddProductForm.ctor()", ex);
            }
        }

        private void AddProductForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                UpdateOnClose(Mode: 1);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ImportFromExcelForm.AddProductForm_FormClosed()", ex);
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new AddProductCategoryForm(true, "", UpdateOnCloseProductForm), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("AddProductForm.btnAddCategory_Click()", ex);
            }
        }

        private void btnAddHSNCode_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new AddTaxForm("Add New HSN Code", true, UpdateOnCloseProductForm), this);
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
                CommonFunctions.ShowErrorDialog("AddProductForm.btnCancel_Click()", ex);
            }
        }

        private void UpdateOnCloseProductForm(Int32 Mode)
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
                    case 2:     //HSN code
                        SelectedItem = "";
                        if (cmbBoxHSNCodeList.SelectedIndex >= 0) SelectedItem = cmbBoxHSNCodeList.SelectedText;
                        List<String> ListHSNCodes = ObjProductMaster.GetHSNCodeList();
                        cmbBoxHSNCodeList.Items.Clear();
                        cmbBoxHSNCodeList.Items.AddRange(ListHSNCodes.ToArray());
                        if (cmbBoxHSNCodeList.Items.Count > 0)
                        {
                            if (!String.IsNullOrEmpty(SelectedItem)) cmbBoxHSNCodeList.SelectedText = SelectedItem;
                            else cmbBoxHSNCodeList.SelectedIndex = 0;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("AddProductForm.UpdateOnClose()", ex);
            }
        }
    }
}
