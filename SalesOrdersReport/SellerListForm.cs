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
    public partial class SellerListForm : Form
    {
        SellerInvoiceForm ObjCreateSellerInvoice;
        DataTable dtSellerMaster;

        public SellerListForm(SellerInvoiceForm ObjForm)
        {
            InitializeComponent();
            ObjCreateSellerInvoice = ObjForm;
            dtSellerMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("SellerMaster", ObjCreateSellerInvoice.MasterFilePath, "SellerName,Line");
        }

        private void SellerListForm_Load(object sender, EventArgs e)
        {
            try
            {
                FillListBoxLineFilter();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerListForm.SellerListForm_Load()", ex);
            }
        }

        private void FillDataGridSellers()
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

                dtSellerMaster.DefaultView.RowFilter = SelectedLine;
                dtGridViewSellers.DataSource = dtSellerMaster.DefaultView.ToTable();

                foreach (DataGridViewRow item in dtGridViewSellers.Rows)
                {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)item.Cells[0];
                    if (CommonFunctions.ListSelectedSellers.Contains(item.Cells[1].Value))
                        cell.Value = cell.TrueValue;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerListForm.FillDataGridSellers()", ex);
            }
        }

        private void FillListBoxLineFilter()
        {
            try
            {
                cmbBoxLineFilter.Items.Clear();
                for (int i = 0; i < CommonFunctions.ListSellerLines.Count; i++)
                {
                    cmbBoxLineFilter.Items.Add(CommonFunctions.ListSellerLines[i]);
                }
                cmbBoxLineFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerListForm.FillListBoxLineFilter()", ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                ObjCreateSellerInvoice.UpdateSelectedSellersList();
                this.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerListForm.btnClose_Click()", ex);
            }
        }

        private void cmbBoxLineFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillDataGridSellers();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerListForm.cmbBoxLineFilter_SelectedIndexChanged()", ex);
            }
        }

        private void dtGridViewSellers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Object SellerName = dtGridViewSellers.Rows[e.RowIndex].Cells[1].Value;
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dtGridViewSellers.Rows[e.RowIndex].Cells[0];
                if (cell.Value == null) cell.Value = cell.TrueValue;
                else if (cell.Value == cell.TrueValue) cell.Value = cell.FalseValue;
                else cell.Value = cell.TrueValue;

                if (cell.Value == cell.TrueValue)
                {
                    if (!CommonFunctions.ListSelectedSellers.Contains(SellerName))
                        CommonFunctions.ListSelectedSellers.Add(SellerName.ToString());
                }
                else if (cell.Value == cell.FalseValue)
                {
                    if (CommonFunctions.ListSelectedSellers.Contains(SellerName))
                        CommonFunctions.ListSelectedSellers.Remove(SellerName.ToString());
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerListForm.dtGridViewSellers_CellClick()", ex);
            }
        }
    }
}
