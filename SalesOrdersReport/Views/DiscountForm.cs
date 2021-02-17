using SalesOrdersReport.CommonModules;
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
    public partial class DiscountForm : Form
    {
        public DiscountForm()
        {
            InitializeComponent();
        }

        public Double DiscountPerc { get; set; }
        public Double DiscountValue { get; set; }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                DiscountPerc = -999; DiscountValue = -999;
                if (radBtnDiscPerc.Checked) DiscountPerc = (Double)numUpDownDiscPerc.Value;
                if (radBtnDiscVal.Checked) DiscountValue = (Double)numUpDownDiscValue.Value;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("DiscountForm.btnOk_Click()", ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DiscountForm_Shown(object sender, EventArgs e)
        {
            try
            {
                numUpDownDiscPerc.Enabled = false;
                numUpDownDiscValue.Enabled = false;
                radBtnDiscPerc.Checked = false;
                if (DiscountPerc != 0)
                {
                    radBtnDiscPerc.Checked = true;
                    numUpDownDiscPerc.Value = (Decimal)DiscountPerc;
                }

                radBtnDiscVal.Checked = false;
                if (DiscountValue != 0)
                {
                    radBtnDiscVal.Checked = true;
                    numUpDownDiscValue.Value = (Decimal)DiscountValue;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("DiscountForm.DiscountForm_Shown()", ex);
            }
        }

        private void radBtnDiscPerc_CheckedChanged(object sender, EventArgs e)
        {
            numUpDownDiscPerc.Enabled = radBtnDiscPerc.Checked;
        }

        private void radBtnDiscVal_CheckedChanged(object sender, EventArgs e)
        {
            numUpDownDiscValue.Enabled = radBtnDiscVal.Checked;
        }
    }
}
