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
    public partial class CreateProductCategoryForm : Form
    {
        Boolean IsAddProductCategory = true;
        ProductMasterModel ObjProductMaster = null;
        UpdateUsingObjectOnCloseDel UpdateOnClose;
        ProductCategoryDetails ObjCategoryDetailsForEdit = null;

        public CreateProductCategoryForm(Boolean IsAddProductCategory, String CategoryName, UpdateUsingObjectOnCloseDel UpdateOnClose)
        {
            try
            {
                InitializeComponent();
                this.IsAddProductCategory = IsAddProductCategory;
                this.UpdateOnClose = UpdateOnClose;
                chkBoxActive.Checked = true;

                ObjProductMaster = CommonFunctions.ListProductLines[CommonFunctions.SelectedProductLineIndex].ObjProductMaster;

                if (IsAddProductCategory)
                {
                    this.Text = "Add new Product Category";
                }
                else
                {
                    this.Text = "Edit Product Category details";

                    ProductCategoryDetails tmpCategoryDetails = ObjProductMaster.GetCategoryDetails(CategoryName);
                    txtBoxName.Text = tmpCategoryDetails.CategoryName;
                    txtBoxDescription.Text = tmpCategoryDetails.Description;
                    chkBoxActive.Checked = tmpCategoryDetails.Active;

                    ObjCategoryDetailsForEdit = tmpCategoryDetails;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("AddProductCategoryForm.ctor()", ex);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsAddProductCategory)
                {
                    if (String.IsNullOrEmpty(txtBoxName.Text.Trim()))
                    {
                        errorProvider1.SetError(txtBoxName, "Name cannot be empty");
                        return;
                    }

                    String CategoryName = txtBoxName.Text.Trim();
                    ProductCategoryDetails tmpCategory = ObjProductMaster.GetCategoryDetails(CategoryName);
                    if (tmpCategory != null)
                    {
                        MessageBox.Show(this, "Category:" + CategoryName + " already exists, Please choose another name.", "Add Category", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    ObjProductMaster.CreateNewProductCategory(CategoryName, txtBoxDescription.Text.Trim(), chkBoxActive.Checked);
                }
                else
                {
                    if (String.IsNullOrEmpty(txtBoxName.Text.Trim()))
                    {
                        errorProvider1.SetError(txtBoxName, "Name cannot be empty");
                        return;
                    }

                    String CategoryName = txtBoxName.Text.Trim();
                    if (!CategoryName.Equals(ObjCategoryDetailsForEdit.CategoryName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        ProductCategoryDetails tmpCategory = ObjProductMaster.GetCategoryDetails(CategoryName);
                        if (tmpCategory != null)
                        {
                            MessageBox.Show(this, "Category:" + CategoryName + " already exists, Please choose another name.", "Update Category", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtBoxName.Text = ObjCategoryDetailsForEdit.CategoryName;
                            return;
                        }
                    }

                    ObjProductMaster.EditProductCategory(ObjCategoryDetailsForEdit.CategoryID, CategoryName, txtBoxDescription.Text.Trim(), chkBoxActive.Checked);
                }
                UpdateOnClose(3);
                this.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("AddProductCategoryForm.btnOK_Click()", ex);
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
                CommonFunctions.ShowErrorDialog("AddProductCategoryForm.btnCancel_Click()", ex);
            }
        }
    }
}
