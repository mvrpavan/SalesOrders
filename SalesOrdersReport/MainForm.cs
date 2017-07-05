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
    public partial class MainForm : Form
    {
        public Boolean MasterSheetSelected;

        public MainForm()
        {
            CommonFunctions.Initialize();
#if RELEASE
            this.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + CommonFunctions.ObjApplicationSettings.LogoFileName);
            this.BackgroundImageLayout = ImageLayout.Center;
#endif  
            InitializeComponent();
            this.Text = CommonFunctions.ObjApplicationSettings.MainFormTitleText;

            toolStripOrderMasterPath.Text = "";
            CommonFunctions.ToolStripProgressBarMainForm = this.toolStripProgressBar;
            CommonFunctions.ToolStripProgressBarMainFormStatus = this.toolStripProgress;

            MasterSheetSelected = false;

            LoadProductLines();

            productToolStripMenuItem.Visible = true;
            vendorToolStripMenuItem.Visible = false;
            reportsToolStripMenuItem.Visible = false;
        }

        void LoadProductLines()
        {
            try
            {
                //Load toolStripComboBoxProductLine from CommonFunctions.ListProductLines
                toolStripComboBoxProductLine.Items.Clear();
                for (int i = 1; i < CommonFunctions.ListProductLines.Count; i++)
                {
                    ProductLine ObjProductLine = CommonFunctions.ListProductLines[i];
                    toolStripComboBoxProductLine.Items.Add(ObjProductLine.Name);
                }
                toolStripComboBoxProductLine.Items.Add("<Create New>");
                toolStripComboBoxProductLine.SelectedIndex = CommonFunctions.SelectedProductLineIndex - 1;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.LoadProductLines()", ex);
            }
        }

        void SetChildFormProperties(Form ObjForm)
        {
            try
            {
                if (MdiChildren.Length > 0) return;

                ObjForm.MdiParent = this;
                ObjForm.ShowIcon = false;
                ObjForm.ShowInTaskbar = false;
                ObjForm.MinimizeBox = false;
                ObjForm.MaximizeBox = false;
                ObjForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                ObjForm.StartPosition = FormStartPosition.CenterScreen;
                ObjForm.FormClosed += new FormClosedEventHandler(ChildFormClosed);
                ObjForm.Show();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.SetChildFormProperties()", ex);
            }
        }

        void EnableDisableAllMenuItems(Boolean Enable, Boolean EnableFileMenu)
        {
            try
            {
                foreach (ToolStripItem item in menuStrip.Items)
                {
                    if (item.Name.Equals("fileMenu", StringComparison.InvariantCultureIgnoreCase))
                    {
                        item.Enabled = EnableFileMenu;
                    }
                    else
                    {
                        item.Enabled = Enable;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.EnableDisableAllMenuItems()", ex);
            }
        }

        void ChildFormClosed(object sender, FormClosedEventArgs e)
        {
            EnableDisableAllMenuItems(true, true);
        }

        Boolean IsValidToOpenForm()
        {
            try
            {
                if (!MasterSheetSelected)
                {
                    MessageBox.Show(this, "Please Select Order Master File using \"File->Choose Master File\" before choosing this menu item", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return MasterSheetSelected;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.createInvoiceToolStripMenuItem_Click()", ex);
                return false;
            }
        }

        #region File Menu Item
        private void chooseMasterFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OrderMasterForm ObjOrderMasterForm = new OrderMasterForm();
                ObjOrderMasterForm.FormClosed += new FormClosedEventHandler(OrderMasterForm_FormClosed);
                SetChildFormProperties(ObjOrderMasterForm);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.chooseMasterFileToolStripMenuItem_Click()", ex);
            }
        }

        void OrderMasterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(CommonFunctions.MasterFilePath))
                {
                    toolStripOrderMasterPath.Text = CommonFunctions.MasterFilePath;
                    MasterSheetSelected = true;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.OrderMasterForm_FormClosed()", ex);
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SettingsForm ObjSettingsForm = new SettingsForm();
                SetChildFormProperties(ObjSettingsForm);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.OrderMasterForm_FormClosed()", ex);
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        #endregion

        #region Seller Menu Item
        private void createSalesOrderSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToOpenForm()) return;

                AddNewOrderSheetForm ObjAddNewOrderSheetForm = new AddNewOrderSheetForm();
                SetChildFormProperties(ObjAddNewOrderSheetForm);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.createSalesOrderSheetToolStripMenuItem_Click()", ex);
            }
        }

        private void createInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToOpenForm()) return;

                CreateSellerInvoice ObjInvoiceForm = new CreateSellerInvoice();
                SetChildFormProperties(ObjInvoiceForm);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.createInvoiceToolStripMenuItem_Click()", ex);
            }
        }

        private void discountGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToOpenForm()) return;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.discountGroupToolStripMenuItem_Click()", ex);
            }
        }

        private void addModifySellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToOpenForm()) return;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.addModifySellerToolStripMenuItem_Click()", ex);
            }
        }

        private void updateSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToOpenForm()) return;

                UpdateOrderMasterForm ObjUpdateOrderMasterForm = new UpdateOrderMasterForm();
                SetChildFormProperties(ObjUpdateOrderMasterForm);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.updateSalesToolStripMenuItem_Click()", ex);
            }
        }
        #endregion

        #region Products Menu Item
        private void priceGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void updateStockToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addModifyItemToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Vendor Menu Item
        private void createOrderSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void createPurchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addModifyVendorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void updateOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Reports Menu Item
        private void vendorHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sellerHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void productStockToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Help Menu Item
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About ObjAbout = new About();
            SetChildFormProperties(ObjAbout);
        }
        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommonFunctions.WriteToSettingsFile();
        }

        private void toolStripComboBoxProductLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (toolStripComboBoxProductLine.SelectedIndex + 1 == CommonFunctions.SelectedProductLineIndex) return;

                MasterSheetSelected = false;
                toolStripOrderMasterPath.Text = "";

                if (toolStripComboBoxProductLine.SelectedItem.ToString().Equals("<Create New>"))
                {
                    ManageProductLineForm ObjManageProductLineForm = new ManageProductLineForm();
                    SetChildFormProperties(ObjManageProductLineForm);
                    ObjManageProductLineForm.FormClosed += new FormClosedEventHandler(ObjManageProductLineForm_FormClosed);
                }
                else
                {
                    CommonFunctions.SelectProductLine(Int32.Parse((toolStripComboBoxProductLine.SelectedIndex + 1).ToString()));
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.toolStripComboBoxProductLine_SelectedIndexChanged()", ex);
            }
        }

        void ObjManageProductLineForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadProductLines();
        }
    }
}
