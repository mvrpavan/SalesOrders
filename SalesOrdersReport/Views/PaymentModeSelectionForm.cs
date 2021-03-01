using SalesOrdersReport.CommonModules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesOrdersReport.Models;

namespace SalesOrdersReport.Views
{
    public partial class PaymentModeSelectionForm : Form
    {
        PaymentsModel ObjPaymentsModel;
        UpdateUsingObjectOnCloseDel UpdateObjectOnCloseDel;
        Double RequiredPaymentAmount = 0;
        DataTable dtPayments;
        List<String> ListPaymentModes;

        public PaymentModeSelectionForm(Double PaymentAmount, UpdateUsingObjectOnCloseDel UpdateObjectOnCloseDel)
        {
            try
            {
                InitializeComponent();

                ObjPaymentsModel = new PaymentsModel();
                ObjPaymentsModel.LoadPaymentModes();

                this.UpdateObjectOnCloseDel = UpdateObjectOnCloseDel;
                RequiredPaymentAmount = PaymentAmount;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.PaymentModeSelectionForm()", ex);
            }
        }

        private void PaymentModeSelectionForm_Load(object sender, EventArgs e)
        {
            try
            {
                //Fill Payment Modes
                ListPaymentModes = ObjPaymentsModel.GetPaymentModesList();
                cmbBoxPaymentModes.Items.Clear();
                cmbBoxPaymentModes.Items.AddRange(ListPaymentModes.ToArray());
                cmbBoxPaymentModes.SelectedIndex = ListPaymentModes.FindIndex(s => s.Equals("Cash"));

                txtBoxAmount.Text = RequiredPaymentAmount.ToString("F");
                txtBoxCardNumber.Text = "";
                lblBillAmount.Text = RequiredPaymentAmount.ToString("F");

                dtPayments = new DataTable();
                dtPayments.Columns.Add("Payment Mode", CommonFunctions.TypeString);
                dtPayments.Columns.Add("Amount", CommonFunctions.TypeDouble);
                dtPayments.Columns.Add("Card#", CommonFunctions.TypeString);
                dtGridViewPaymentModes.DataSource = dtPayments;

                txtBoxAmount.Validating += Value_Validating;
                CommonFunctions.SetDataGridViewProperties(dtGridViewPaymentModes);
                UpdateSummary();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.PaymentModeSelectionForm_Load()", ex);
            }
        }

        private void UpdateSummary()
        {
            try
            {
                Double TotalAmount = 0;
                foreach (DataRow item in dtPayments.Rows)
                {
                    TotalAmount += Double.Parse(item["Amount"].ToString());
                }

                lblPaymentAmount.Text = TotalAmount.ToString("F");
                lblBalanceAmount.Text = (RequiredPaymentAmount - TotalAmount).ToString("F");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateSummary()", ex);
            }
        }

        private void btnAddPaymentMode_Click(object sender, EventArgs e)
        {
            try
            {
                String PaymentMode = cmbBoxPaymentModes.SelectedItem.ToString().Trim();
                String CardNumber = null;
                Double Amount = Double.Parse(txtBoxAmount.Text.Trim());
                if (Amount < 0) return;
                if (Double.Parse(lblBalanceAmount.Text) <= 0) return;
                Amount = Math.Min(Double.Parse(lblBalanceAmount.Text), Amount);

                if (PaymentMode.Contains("Card"))
                {
                    CardNumber = txtBoxCardNumber.Text.Trim();

                    DataRow[] dtRows = dtPayments.Select($"[Payment Mode] = '{PaymentMode}' and [Card#] = '{CardNumber}'");
                    if (dtRows != null && dtRows.Length > 0)
                    {
                        dtRows[0]["Amount"] = (Double.Parse(dtRows[0]["Amount"].ToString()) + Amount).ToString("F");
                    }
                    else
                    {
                        DataRow dtRow = dtPayments.NewRow();
                        dtRow["Payment Mode"] = PaymentMode;
                        dtRow["Amount"] = Amount.ToString("F");
                        dtRow["Card#"] = CardNumber;
                        dtPayments.Rows.Add(dtRow);
                    }
                }
                else
                {
                    DataRow[] dtRows = dtPayments.Select($"[Payment Mode] = '{PaymentMode}'");
                    if (dtRows != null && dtRows.Length > 0)
                    {
                        dtRows[0]["Amount"] = (Double.Parse(dtRows[0]["Amount"].ToString()) + Amount).ToString("F");
                    }
                    else
                    {
                        DataRow dtRow = dtPayments.NewRow();
                        dtRow["Payment Mode"] = PaymentMode;
                        dtRow["Amount"] = Amount.ToString("F");
                        dtPayments.Rows.Add(dtRow);
                    }
                }

                UpdateSummary();

                txtBoxAmount.Text = Math.Max(0, Double.Parse(lblBalanceAmount.Text)).ToString("F");
                txtBoxCardNumber.Text = "";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnAddPaymentMode_Click()", ex);
            }
        }

        private void btnDeletePaymentMode_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewPaymentModes.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select a row to delete.", "Delete Payment Mode", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }

                DataGridViewRow dataGridViewRow = dtGridViewPaymentModes.SelectedRows[0];
                String Filter = $"[Payment Mode] = '{dataGridViewRow.Cells["Payment Mode"].Value.ToString()}' " +
                                $"and Amount = {dataGridViewRow.Cells["Amount"].Value.ToString()} " +
                                ((dataGridViewRow.Cells["Card#"].Value == DBNull.Value) ? "" : $"and [Card#] = '{dataGridViewRow.Cells["Card#"].Value.ToString()}'");
                dtPayments.Select(Filter)[0].Delete();
                dtPayments.AcceptChanges();

                UpdateSummary();
                txtBoxAmount.Text = Math.Max(0, Double.Parse(lblBalanceAmount.Text)).ToString("F");
                txtBoxCardNumber.Text = "";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnDeletePaymentMode_Click()", ex);
            }
        }

        private void btnCreatePayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewPaymentModes.Rows.Count == 0) return;
                if (Double.Parse(lblBalanceAmount.Text) > 0) return;
                if (!ValidateChildren(ValidationConstraints.Enabled)) return;


                List<PaymentDetails> ListPaymentDetails = new List<PaymentDetails>();
                foreach (DataRow item in dtPayments.Rows)
                {
                    PaymentDetails tmpPaymentDetails = new PaymentDetails() {
                        PaymentMode = item["Payment Mode"].ToString(),
                        Amount = Double.Parse(item["Amount"].ToString()),
                        CardNumber = item["Card#"].ToString()
                    };
                    ListPaymentDetails.Add(tmpPaymentDetails);
                }

                this.Close();
                UpdateObjectOnCloseDel(1, ListPaymentDetails);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnDeletePaymentMode_Click()", ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnCancel_Click()", ex);
            }
        }

        private void cmbBoxPaymentModes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtBoxCardNumber.Enabled = cmbBoxPaymentModes.SelectedItem.ToString().Contains("Card");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxPaymentModes_SelectedIndexChanged()", ex);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                cmbBoxPaymentModes.SelectedIndex = ListPaymentModes.FindIndex(s => s.Equals("Cash"));
                txtBoxAmount.Text = RequiredPaymentAmount.ToString("F");
                dtPayments.Rows.Clear();
                txtBoxCardNumber.Text = "";

                UpdateSummary();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnReset_Click()", ex);
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
                    errorProviderPaymentModeForm.SetError(ObjTextBox, "Provide a valid value");
                }
                else
                {
                    e.Cancel = false;
                    errorProviderPaymentModeForm.SetError(ObjTextBox, "");
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.Value_Validating()", ex);
            }
        }
    }
}
