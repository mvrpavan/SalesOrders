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
    public partial class EditLineForm : Form
    {
        UpdateOnCloseDel UpdateCustomerOnClose = null;
        MySQLHelper tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
        public EditLineForm(UpdateOnCloseDel UpdateCustomerOnClose)
        {
            InitializeComponent();
            FillLines();
            cmbxSelectLine.Focus();
            cmbxSelectLine.SelectedIndex = 0;
            this.UpdateCustomerOnClose = UpdateCustomerOnClose;
            this.FormClosed += EditLineForm_FormClosed;
        }

        private void FillLines()
        {
            try
            {
                tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                List<string> ListLines = CommonFunctions.ObjCustomerMasterModel.GetAllLineNames();
                cmbxSelectLine.Items.Add("Select Line");
                foreach (var item in ListLines)
                {
                    cmbxSelectLine.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditLinesForm.FillLines()", ex);
                throw ex;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                cmbxSelectLine.SelectedIndex = 0;
                txtEditLineDesc.Clear();
                cmbxSelectLine.Focus();
                lblValidErrMsg.Visible = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditLineForm.btnReset_Click()", ex);
                throw ex;
            }
        }

        private void EditLineForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                UpdateCustomerOnClose(Mode: 1);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditLineForm.EditLineForm_FormClosed()", ex);
            }
        }

        private void btnEditLine_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtEditLineName.Text.Trim() == string.Empty)
                //{
                //    lblValidErrMsg.Visible = true;
                //    lblValidErrMsg.Text = "Line Name Cant be empty!";
                //    txtEditLineName.Focus();
                //    return;
                //}
                if (cmbxSelectLine.SelectedIndex == 0)
                {
                    lblValidErrMsg.Visible = true;
                    lblValidErrMsg.Text = "Choose a Line Name to Edit!";
                    return;
                }
                //if (txtEditLineDesc.Text.Trim() == string.Empty)
                //{
                //    lblValidErrMsg.Visible = true;
                //    lblValidErrMsg.Text = "Description Cant be empty!";
                //    txtEditLineDesc.Focus();
                //    return;
                //}
                List<string> ListColumnValues = new List<string>();
                List<string> ListColumnNames = new List<string>();

                ListColumnValues.Add(txtEditLineDesc.Text);
                ListColumnNames.Add("DESCRIPTION");

                string WhereCondition = "LINENAME = '" + cmbxSelectLine.SelectedItem.ToString() + "'";
                tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                int ResultVal = CommonFunctions.ObjUserMasterModel.UpdateAnyTableDetails("LINEMASTER", ListColumnNames, ListColumnValues, WhereCondition);

                if (ResultVal < 0) MessageBox.Show("Wasnt able to Edit the Line", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    MessageBox.Show("Updated Line Details :: " + cmbxSelectLine.SelectedItem.ToString() + " successfully", "Update Line Details");
                    UpdateCustomerOnClose(Mode: 1);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditLineForm.btnEditLine_Click()", ex);
            }
        }

        private void cmbxSelectLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBox = (ComboBox)sender;
                if (comboBox.SelectedIndex != 0)
                {
                    string LineName = (string)comboBox.SelectedItem;
                    LineDetails ObjLineDetails = CommonFunctions.ObjCustomerMasterModel.GetLineDetails(LineName);
                    txtEditLineDesc.Text = ObjLineDetails.LineDescription;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("EditLineForm.cmbxSelectLine_SelectedIndexChanged()", ex);
            }
        }
    }
}
