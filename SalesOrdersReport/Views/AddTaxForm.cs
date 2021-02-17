using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SalesOrdersReport.Models;
using SalesOrdersReport.CommonModules;

namespace SalesOrdersReport.Views
{
    public partial class AddTaxForm : Form
    {
        Boolean IsAddTax = false;
        UpdateProductOnCloseDel UpdateOnClose = null;
        DataTable dtAllHSNCodes = new DataTable();
        ProductMasterModel ObjProductMasterModel = CommonFunctions.ObjProductMaster;
        Int32 TaxIDToEdit = -1;

        public AddTaxForm(Boolean IsAddTax, Int32 TaxID, UpdateProductOnCloseDel UpdateOnClose)
        {
            try
            {
                InitializeComponent();
                this.IsAddTax = IsAddTax;
                this.UpdateOnClose = UpdateOnClose;

                //Fill DataGridView
                dtGridViewHSNList.AllowUserToAddRows = false;
                dtGridViewHSNList.AllowUserToDeleteRows = false;
                dtGridViewHSNList.AllowUserToOrderColumns = false;
                dtGridViewHSNList.AllowUserToResizeRows = false;
                dtGridViewHSNList.MultiSelect = false;
                dtGridViewHSNList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dtAllHSNCodes = GetHSNCodesDataTable();
                LoadDataGridView();

                if (this.IsAddTax)
                {
                    TaxIDToEdit = -1;
                    Text = "Add New HSN Code";
                    btnOK.Text = "Add";
                    txtBoxHSNCode.Enabled = true;
                    btnDelete.Enabled = false;
                }
                else
                {
                    TaxIDToEdit = TaxID;
                    EditTaxID();
                    Text = "Update HSN Code";
                    btnOK.Text = "Update";
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("AddTaxForm.ctor()", ex);
            }
        }

        private void LoadDataGridView()
        {
            try
            {
                dtGridViewHSNList.DataSource = dtAllHSNCodes;
                dtGridViewHSNList.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadDataGridView()", ex);
            }
        }

        void EditTaxID()
        {
            try
            {
                HSNCodeDetails ObjHSNCodeDetails = ObjProductMasterModel.GetTaxDetails(TaxIDToEdit);
                txtBoxHSNCode.Text = ObjHSNCodeDetails.HSNCode;
                txtBoxHSNCode.Enabled = false;
                txtBoxCGST.Text = ObjHSNCodeDetails.ListTaxRates[ObjProductMasterModel.ListTaxGroupDetails.FindIndex(e => e.Name.Equals("CGST"))].ToString();
                txtBoxSGST.Text = ObjHSNCodeDetails.ListTaxRates[ObjProductMasterModel.ListTaxGroupDetails.FindIndex(e => e.Name.Equals("SGST"))].ToString();
                txtBoxIGST.Text = ObjHSNCodeDetails.ListTaxRates[ObjProductMasterModel.ListTaxGroupDetails.FindIndex(e => e.Name.Equals("IGST"))].ToString();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.EditTaxID()", ex);
            }
        }

        DataTable GetHSNCodesDataTable()
        {
            try
            {
                DataTable dtHSNCodes = new DataTable();
                String[] TaxColumns = ObjProductMasterModel.ListTaxGroupDetails.Select(e => e.Name).ToArray();
                List<String> ListColumns = new List<String>();
                ListColumns.Add("TaxID");
                ListColumns.Add("HSN Code");
                ListColumns.AddRange(TaxColumns);

                for (int i = 0; i < ListColumns.Count; i++)
                {
                    dtHSNCodes.Columns.Add(ListColumns[i]);
                }

                List<HSNCodeDetails> ListHSNCodes = ObjProductMasterModel.GetAllHSNCodeDetails();

                for (int i = 0; i < ListHSNCodes.Count; i++)
                {
                    Object[] newRow = new Object[ListColumns.Count];
                    newRow[0] = ListHSNCodes[i].TaxID;
                    newRow[1] = ListHSNCodes[i].HSNCode;
                    for (int j = 0; j < ListHSNCodes[i].ListTaxRates.Length; j++)
                    {
                        newRow[j + 2] = ListHSNCodes[i].ListTaxRates[j];
                    }

                    dtHSNCodes.Rows.Add(newRow);
                }

                return dtHSNCodes;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetHSNCodesDataTable()", ex);
                return null;
            }
        }

        private void AddTaxForm_Shown(object sender, EventArgs e)
        {
            try
            {
                txtBoxCGST.Validating += Value_Validating;
                txtBoxSGST.Validating += Value_Validating;
                txtBoxIGST.Validating += Value_Validating;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("AddTaxForm.AddTaxForm_Shown()", ex);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateChildren(ValidationConstraints.Enabled))
                {
                    MessageBox.Show(this, "Invalid input for some of the values", "Input validation", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }

                HSNCodeDetails ObjHSNCodeDetails = new HSNCodeDetails()
                {
                    TaxID = TaxIDToEdit,
                    HSNCode = txtBoxHSNCode.Text.Trim(),
                    ListTaxRates = new double[] { Double.Parse(txtBoxCGST.Text.Trim()), Double.Parse(txtBoxSGST.Text.Trim()), Double.Parse(txtBoxIGST.Text.Trim()) }
                };

                if (ObjProductMasterModel.GetHSNCodeDetails(ObjHSNCodeDetails.HSNCode) != null)
                {
                    MessageBox.Show(this, "HSNCode already exists", "HSNCode error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }

                HSNCodeDetails ObjHSNCodeDetailsUpdated = ObjProductMasterModel.AddUpdateHSNCodeDetails(ObjHSNCodeDetails);

                if (ObjHSNCodeDetailsUpdated != null)
                {
                    if (UpdateOnClose != null && IsAddTax) UpdateOnClose(2, ObjHSNCodeDetailsUpdated);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(this, "Unknown error occurred, Unable to create new HSNCode", "HSNCode error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnOK_Click()", ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtGridViewHSNList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                TaxIDToEdit = Int32.Parse(dtGridViewHSNList.Rows[e.RowIndex].Cells[0].Value.ToString());
                EditTaxID();
                Text = "Update HSN Code";
                btnOK.Text = "Update and Close";
                btnDelete.Enabled = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewHSNList_CellMouseDoubleClick()", ex);
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
                    errorProviderAddHSNCodes.SetError(ObjTextBox, "Provide a valid value");
                }
                else
                {
                    e.Cancel = false;
                    errorProviderAddHSNCodes.SetError(ObjTextBox, "");
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.Value_Validating()", ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                String HSNCode = txtBoxHSNCode.Text;
                if (String.IsNullOrEmpty(HSNCode)) return;

                DialogResult dialogResult = MessageBox.Show(this, "This will remove HSN Code completely. Do you want to continue?", "HSNCode Deletion", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Cancel) return;

                Int32 Result = ObjProductMasterModel.DeleteHSNCodeDetails(HSNCode);

                if (Result == -1)
                {
                    MessageBox.Show(this, "There are Products with this HSN Code. Unable to delete it.", "HSNCode Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else if (Result == -2)
                {
                    MessageBox.Show(this, "Unknown error occurred. Unable to delete it.", "HSNCode Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    if (UpdateOnClose != null) UpdateOnClose(3, HSNCode);
                    dtAllHSNCodes.Select($"[HSN Code] = '{HSNCode}'")[0].Delete();
                    dtAllHSNCodes.AcceptChanges();
                    LoadDataGridView();
                    CommonFunctions.ResetTextBoxesRecursive(this);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnDelete_Click()", ex);
            }
        }
    }
}
