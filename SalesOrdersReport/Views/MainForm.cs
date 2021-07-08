using SalesOrdersReport.CommonModules;
using System;
using System.Drawing;
using System.Net.Sockets;
using System.Windows.Forms;

namespace SalesOrdersReport.Views
{
    public partial class MainForm : Form
    {
        public Boolean MasterSheetSelected;
        MySQLHelper tmpMySQLHelper;
        public bool IsLoggedOut = false;

        public MainForm()
        {
            tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
            InitializeComponent();
#if RELEASE
            if (!String.IsNullOrEmpty(CommonFunctions.ObjApplicationSettings.LogoFileName.Trim()))
            {
                //this.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + CommonFunctions.ObjApplicationSettings.LogoFileName);
                this.BackgroundImage = Image.FromFile(CommonFunctions.AppDataFolder + @"\" + CommonFunctions.ObjApplicationSettings.LogoFileName);
                this.BackgroundImageLayout = ImageLayout.Center;
            }
#endif
            this.Text = CommonFunctions.ObjApplicationSettings.MainFormTitleText;

            CommonFunctions.ToolStripProgressBarMainForm = this.toolStripProgressBar;
            CommonFunctions.ToolStripProgressBarMainFormStatus = this.toolStripProgress;
            this.toolStripProgress.Text = "";

            MasterSheetSelected = false;

            vendorMenu.Visible = false;
            //productMenu.Visible = true;
            //reportsMenu.Visible = true;

            FillShortcuts();
            statusStrip.ShowItemToolTips = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommonFunctions.WriteToLogFile("Application closed\n==============================================================");
            CommonFunctions.WriteToSettingsFile();
        }

        void FillShortcuts()
        {
            try
            {
                lblShortcuts.Text = "";
                lblShortcuts.Text += "F1:POS Bills    ";
                lblShortcuts.Text += "F2:Orders    ";
                lblShortcuts.Text += "F3:Invoices    ";
                lblShortcuts.Text += "F4:Products    ";
                lblShortcuts.Text += "F5:Customers    ";
                lblShortcuts.Text += "F6:Payments & Expenses    ";
                //lblShortcuts.Text += "F7:Vendors    ";

                POSbillingToolStripMenuItem.ShortcutKeys = Keys.F1;
                ordersToolStripMenuItem.ShortcutKeys = Keys.F2;
                invoicesToolStripMenuItem.ShortcutKeys = Keys.F3;
                productMenu.ShortcutKeys = Keys.F4;
                customerToolStripMenuItem.ShortcutKeys = Keys.F5;
                PaymentsToolStripMenuItem.ShortcutKeys = Keys.F6;
                //vendorHistoryToolStripMenuItem.ShortcutKeys = Keys.F7;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.FillShortcuts()", ex);
                throw;
            }
        }

        public void ShowChildForm(Form ObjForm)
        {
            try
            {
                if (MdiChildren.Length > 0)
                {
                    if (ObjForm.GetType() == CommonFunctions.CurrentForm.GetType()) return;
                    CommonFunctions.CurrentForm.Close();
                    if (!CommonFunctions.CurrentForm.IsDisposed) return;
                }

                CommonFunctions.ResetProgressBar();
                CommonFunctions.WriteToLogFile($"Form:{ObjForm.Name} opened from {this.Name}");

                CommonFunctions.CurrentForm = ObjForm;
                ObjForm.MdiParent = this;
                ObjForm.ShowIcon = false;
                ObjForm.ShowInTaskbar = false;
                ObjForm.MinimizeBox = false;
                ObjForm.MaximizeBox = false;
                ObjForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                ObjForm.StartPosition = FormStartPosition.CenterScreen;
                ObjForm.FormClosed += new FormClosedEventHandler(ChildFormClosed);
                CommonFunctions.ApplyPrivilegeControl(ObjForm);
                ObjForm.Show();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ShowChildForm()", ex);
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
            CommonFunctions.WriteToLogFile($"Form:{CommonFunctions.CurrentForm.Name} closed");
            EnableDisableAllMenuItems(true, true);
        }

        Boolean IsValidToOpenChildForm()
        {
            try
            {
                if (MdiChildren.Length > 0) return false;

                if (!MasterSheetSelected)
                {
                    MessageBox.Show(this, "Please Select Order Master File using \"File->Choose Master File\" before choosing this menu item", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return MasterSheetSelected;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.IsValidToOpenChildForm()", ex);
                return false;
            }
        }

        #region Products Menu Item
        private void productMenu_Click(object sender, EventArgs e)
        {
            try
            {
                ShowChildForm(new ProductsMainForm());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.productMenu_Click()", ex);
            }
        }
        #endregion

        #region Vendor Menu Item
        private void createOrderSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToOpenChildForm()) return;
                VendorOrderSheetForm ObjVendorOrderSheetForm = new VendorOrderSheetForm();
                ShowChildForm(ObjVendorOrderSheetForm);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.createOrderSheetToolStripMenuItem_Click()", ex);
            }
        }

        private void createPurchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToOpenChildForm()) return;
                VendorPurchaseOrderForm ObjVendorPurchaseOrderForm = new VendorPurchaseOrderForm();
                ShowChildForm(ObjVendorPurchaseOrderForm);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.createPurchaseOrderToolStripMenuItem_Click()", ex);
            }
        }

        private void updatePurchasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToOpenChildForm()) return;
                UpdateProductPurchasesForm ObjUpdateProductPurchasesForm = new UpdateProductPurchasesForm();
                ShowChildForm(ObjUpdateProductPurchasesForm);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.updatePurchasesToolStripMenuItem_Click()", ex);
            }
        }
        #endregion

        #region Reports Menu Item
        private void sellerHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToOpenChildForm()) return;

                SellerHistoryReportForm ObjSellerHistoryReportForm = new SellerHistoryReportForm();
                ShowChildForm(ObjSellerHistoryReportForm);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.sellerHistoryToolStripMenuItem_Click()", ex);
            }
        }
        #endregion

        #region Help Menu Item
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowChildForm(new About());
        }
        #endregion

        #region Status Strip
        private void statusStrip_SizeChanged(object sender, EventArgs e)
        {
            try
            {
#if RELEASE
                if (!String.IsNullOrEmpty(CommonFunctions.ObjApplicationSettings.LogoFileName.Trim()))
                {
                    this.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + CommonFunctions.ObjApplicationSettings.LogoFileName);
                    this.BackgroundImageLayout = ImageLayout.Center;
                }
#endif
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.statusStrip_SizeChanged()", ex);
            }
        }
        #endregion

        private void ProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShowChildForm(new EditProfileForm());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.ProfileToolStripMenuItem_Click()", ex);
            }
        }

        private void LogOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.IsLoggedOut = true;
                timerDBConnection.Enabled = false;
                this.Close();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.LogOutToolStripMenuItem_Click()", ex);
            }
        }

        private void manageUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShowChildForm(new ManageUsersForm());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.manageUsersToolStripMenuItem_Click()", ex);
                throw;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.UserProfileToolStripMenuItem.Text = tmpMySQLHelper.CurrentUser;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.MainForm_Load()", ex);
            }
        }

        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShowChildForm(new OrdersMainForm());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ordersToolStripMenuItem_Click()", ex);
            }
        }

        private void invoicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShowChildForm(new InvoicesMainForm());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.invoicesToolStripMenuItem_Click()", ex);
            }
        }

        private void appSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShowChildForm(new SettingsForm());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.appSettingsToolStripMenuItem_Click()", ex);
                throw;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show(this, "Are you sure to exit?", "Exit Sales Orders", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes) this.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.exitToolStripMenuItem_Click()", ex);
            }
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShowChildForm(new ManageCustomerForm());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.customerToolStripMenuItem_Click()", ex);
            }
        }

        private void PaymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShowChildForm(new PaymentsForm());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.PaymentsToolStripMenuItem_Click()", ex);
            }
        }

        private void manageInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShowChildForm(new ProductInventoryMainForm());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.manageInventoryToolStripMenuItem_Click()", ex);
            }
        }

        private void manageVendorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShowChildForm(new ManageVendorForm());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.manageVendorsToolStripMenuItem_Click()", ex);
            }
        }

        private void purchaseOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShowChildForm(new PurchaseOrdersMainForm());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.purchaseOrdersToolStripMenuItem_Click()", ex);
            }
        }

        private void purchaseInvoicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShowChildForm(new PurchaseInvoicesMainForm());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.purchaseInvoicesToolStripMenuItem_Click()", ex);
            }
        }

        private void POSbillingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShowChildForm(new POSBillsMainForm());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.POSbillingToolStripMenuItem_Click()", ex);
            }
        }

        private void ExpensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ShowChildForm(new ExpensesForm());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ExpensesToolStripMenuItem_Click()", ex);
            }
        }

        private void reportsMenu_Click(object sender, EventArgs e)
        {
            try
            {
                ShowChildForm(new ReportsMainForm());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.reportsMenu_Click()", ex);
            }
        }

        Boolean IsConnected = false;
        private void timerDBConnection_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!timerDBConnection.Enabled) return;

                Boolean tmpIsConnected = false;
                using (TcpClient tcpClient = new TcpClient())
                {
                    try
                    {
                        tcpClient.Connect(CommonFunctions.ObjApplicationSettings.Server, 3306);
                        tmpIsConnected = true;
                    }
                    catch (Exception)
                    {
                        tmpIsConnected = false;
                    }
                }

                if (IsConnected != tmpIsConnected)
                {
                    IsConnected = tmpIsConnected;
                    toolStripConnStatusLabel.Image = IsConnected ? Properties.Resources.connected_icon_2_16 : Properties.Resources.disconnected_icon_14_16;
                    toolStripConnStatusLabel.ToolTipText = IsConnected ? "Connected" : "Disconnected";
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.timerDBConnection_Tick()", ex);
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                timerDBConnection.Enabled = true;
                timerDBConnection_Tick(null, null);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.MainForm_Shown()", ex);
            }
        }
    }
}
