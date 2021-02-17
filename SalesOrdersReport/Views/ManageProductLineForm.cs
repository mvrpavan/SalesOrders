using SalesOrdersReport.CommonModules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SalesOrdersReport
{
    public partial class ManageProductLineForm : Form
    {
        public ManageProductLineForm()
        {
            InitializeComponent();

            cmbBoxProductLine.Items.Clear();
            cmbBoxProductLine.DataSource = CommonFunctions.ListProductLines.Select(e => e.Name).ToArray();
            cmbBoxProductLine.SelectedIndex = 0;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBoxName.Text.Trim().Length == 0)
                {
                    MessageBox.Show(this, "Product Line Name cannot be empty", "Manage Product Line", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (CommonFunctions.ListProductLines.FindIndex(s => s.Name.Equals(txtBoxName.Text.Trim(), StringComparison.InvariantCultureIgnoreCase)) >= 0)
                {
                    MessageBox.Show(this, "Specified Product Line already exists", "Manage Product Line", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                CommonFunctions.AddNewProductLine(txtBoxName.Text.Trim(), cmbBoxProductLine.SelectedIndex);

                MessageBox.Show(this, "New ProductLine \"" + txtBoxName.Text + "\" created successfully", "Manage Product Line", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageProductLineForm.btnAddNew_Click()", ex);
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
                CommonFunctions.ShowErrorDialog("ManageProductLineForm.btnCancel_Click()", ex);
            }
        }
    }
}
