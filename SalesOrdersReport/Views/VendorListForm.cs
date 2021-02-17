using SalesOrdersReport.CommonModules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SalesOrdersReport.Views
{
    public partial class VendorListForm : Form
    {
        VendorPurchaseOrderForm ObjVendorPurchaseOrderForm;
        DataTable dtVendorMaster;

        public VendorListForm(VendorPurchaseOrderForm ObjForm)
        {
            InitializeComponent();
            ObjVendorPurchaseOrderForm = ObjForm;
            dtVendorMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("VendorMaster", CommonFunctions.MasterFilePath, "VendorName,Line");
        }

        private void VendorListForm_Load(object sender, EventArgs e)
        {
            try
            {
                FillListBoxLineFilter();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorListForm.VendorListForm_Load()", ex);
            }
        }

        private void cmbBoxLineFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillDataGridVendors();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorListForm.cmbBoxLineFilter_SelectedIndexChanged()", ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                ObjVendorPurchaseOrderForm.UpdateSelectedVendorsList();
                this.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorListForm.btnClose_Click()", ex);
            }
        }

        private void dtGridViewVendors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Object VendorName = dtGridViewVendors.Rows[e.RowIndex].Cells[1].Value;
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dtGridViewVendors.Rows[e.RowIndex].Cells[0];
                if (cell.Value == null) cell.Value = cell.TrueValue;
                else if (cell.Value == cell.TrueValue) cell.Value = cell.FalseValue;
                else cell.Value = cell.TrueValue;

                if (cell.Value == cell.TrueValue)
                {
                    if (!CommonFunctions.ListSelectedVendors.Contains(VendorName))
                        CommonFunctions.ListSelectedVendors.Add(VendorName.ToString());
                }
                else if (cell.Value == cell.FalseValue)
                {
                    if (CommonFunctions.ListSelectedVendors.Contains(VendorName))
                        CommonFunctions.ListSelectedVendors.Remove(VendorName.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorListForm.dtGridViewVendors_CellClick()", ex);
            }
        }

        private void FillListBoxLineFilter()
        {
            try
            {
                cmbBoxLineFilter.Items.Clear();
                for (int i = 0; i < CommonFunctions.ListVendorLines.Count; i++)
                {
                    cmbBoxLineFilter.Items.Add(CommonFunctions.ListVendorLines[i]);
                }
                cmbBoxLineFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorListForm.FillListBoxLineFilter", ex);
            }
        }

        private void FillDataGridVendors()
        {
            try
            {
                String SelectedLine = cmbBoxLineFilter.SelectedItem.ToString();
                if (SelectedLine.Equals("<All>", StringComparison.InvariantCultureIgnoreCase))
                    SelectedLine = "";
                else if (SelectedLine.Equals("<Blanks>", StringComparison.InvariantCultureIgnoreCase))
                    SelectedLine = "Line = '' Or Line is null";
                else
                    SelectedLine = "Line = '" + SelectedLine + "'";

                dtVendorMaster.DefaultView.RowFilter = SelectedLine;
                dtGridViewVendors.DataSource = dtVendorMaster.DefaultView.ToTable();

                foreach (DataGridViewRow item in dtGridViewVendors.Rows)
                {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)item.Cells[0];
                    if (CommonFunctions.ListSelectedVendors.Contains(item.Cells[1].Value))
                        cell.Value = cell.TrueValue;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorListForm.FillDataGridVendors()", ex);
            }
        }
    }
}
