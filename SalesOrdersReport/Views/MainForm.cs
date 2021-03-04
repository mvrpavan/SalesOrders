using SalesOrdersReport.CommonModules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
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
            // CommonFunctions.Initialize();
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

            toolStripOrderMasterPath.Text = "";
            CommonFunctions.ToolStripProgressBarMainForm = this.toolStripProgressBar;
            CommonFunctions.ToolStripProgressBarMainFormStatus = this.toolStripProgress;
            this.toolStripProgress.Text = "";

            MasterSheetSelected = false;

            //LoadProductLines();

            sellerMenu.Visible = false;
            vendorMenu.Visible = false;
            productMenu.Visible = true;
            reportsMenu.Visible = true;

            addModifyVendorToolStripMenuItem.Visible = false;
            vendorHistoryToolStripMenuItem.Visible = false;
            productStockToolStripMenuItem.Visible = false;

            FillShortcuts();
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
                PaymentsExpensesToolStripMenuItem.ShortcutKeys = Keys.F6;
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

        #region File Menu Item
        private void chooseMasterFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OrderMasterForm ObjOrderMasterForm = new OrderMasterForm();
                ObjOrderMasterForm.FormClosed += new FormClosedEventHandler(OrderMasterForm_FormClosed);
                ShowChildForm(ObjOrderMasterForm);
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
        #endregion

        #region Seller Menu Item
        private void createSalesOrderSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToOpenChildForm()) return;

                SellerOrderSheetForm ObjAddNewOrderSheetForm = new SellerOrderSheetForm();
                ShowChildForm(ObjAddNewOrderSheetForm);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.createSalesOrderSheetToolStripMenuItem_Click()", ex);
            }
        }

        private void createInvoiceFromSalesOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToOpenChildForm()) return;

                SellerInvoiceForm ObjInvoiceForm = new SellerInvoiceForm();
                ShowChildForm(ObjInvoiceForm);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.createInvoiceFromSalesOrderToolStripMenuItem_Click()", ex);
            }
        }

        private void ShowCreateCustomerBillForm(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToOpenChildForm()) return;

                CustomerInvoiceSellerOrderForm ObjCustomerInvoiceForm = new CustomerInvoiceSellerOrderForm(false, true);
                ShowChildForm(ObjCustomerInvoiceForm);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.ShowCreateCustomerBillForm()", ex);
            }
        }

        private void createSellerOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToOpenChildForm()) return;

                CustomerInvoiceSellerOrderForm ObjCustomerInvoiceForm = new CustomerInvoiceSellerOrderForm(true, false);
                ShowChildForm(ObjCustomerInvoiceForm);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.createSellerOrderToolStripMenuItem_Click()", ex);
            }
        }

        private void updateSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToOpenChildForm()) return;

                UpdateOrderMasterForm ObjUpdateOrderMasterForm = new UpdateOrderMasterForm();
                ShowChildForm(ObjUpdateOrderMasterForm);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.updateSalesToolStripMenuItem_Click()", ex);
            }
        }
        #endregion

        #region Products Menu Item
        private void productMenu_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!IsValidToOpenChildForm()) return;

                Views.ProductsMainForm productsMainForm = new Views.ProductsMainForm();
                ShowChildForm(productsMainForm);
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
        private void vendorHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

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

        private void productStockToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Help Menu Item
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About ObjAbout = new About();
            ShowChildForm(ObjAbout);
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

        private void mergeSalesOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToOpenChildForm()) return;

                MergeSalesOrdersForm ObjMergeSalesOrdersForm = new MergeSalesOrdersForm();
                ShowChildForm(ObjMergeSalesOrdersForm);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.mergeSalesOrderToolStripMenuItem_Click()", ex);
            }
        }

        private void ProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                EditProfileForm ObjEditProfileForm = new EditProfileForm();
                ShowChildForm(ObjEditProfileForm);

                //EditProfileForm ObjEditProfileForm = new EditProfileForm();
                //ShowChildForm(ObjEditProfileForm);

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
                this.Close();
                DialogResult = DialogResult.OK;
                //this.Close();
                // if (!IsValidToOpenChildForm()) return;
                //Application.Run(new LoginForm());
                //UpdateTableOnLogout();
                //this.Hide();
                //CommonFunctions.CurrentForm.Dispose();
                //CommonFunctions.CurrentForm.Close();
                //LoginForm ObjLog = new LoginForm();
                //ObjLog.Show();
                //ObjLog.Dispose(); //because user has logged out so the data must be flushed, by "Disposing" it will not be in the RAM anymore, so your hanging problem will be solved
                //ObjLog.Show();


                //Application.Exit();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.LogOutToolStripMenuItem_Click()", ex);
            }
        }

        private void btnUserProfile_Click(object sender, EventArgs e)
        {
            try
            {
                //cntxtMenuStripUserProfile.Items.Clear();
                ////cntxtMenuStripUserProfile.Items.Add("Change Password");
                //cntxtMenuStripUserProfile.Items.Add("Profile");
                // cntxtMenuStripUserProfile.Items.Add("Log Out");

                //cntxtMenuStripUserProfile.Show(btnUserProfile, new Point(0, btnUserProfile.Height));
                //cntxtMenuStripUserProfile.ItemClicked += new ToolStripItemClickedEventHandler(cntxtMenuStripUserProfile_ItemClicked);// cntxtMenuStripUserProfile_ItemClicked(null, cntxtMenuStripUserProfile;//new System.EventHandler(this.cntxtMenuStripUserProfile_ItemClicked);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.btnUserProfile_Click()", ex);
                throw;
            }

        }

        private void showAllOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Views.OrdersMainForm ordersMainForm = new Views.OrdersMainForm();
                ShowChildForm(ordersMainForm);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.showAllOrdersToolStripMenuItem_Click()", ex);
            }
        }

        private void manageUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!IsValidToOpenChildForm()) return;

                ManageUsersForm ObjManageUsersForm = new ManageUsersForm();
                ShowChildForm(ObjManageUsersForm);
                //ObjManageUsersForm.FormClosed += ObjManageUsersForm_FormClosed;
                //ObjManageUsersForm.Show();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.manageUsersToolStripMenuItem_Click()", ex);
                throw;
            }
        }

        private void btnUserProfile_Paint(object sender, PaintEventArgs e)
        {
            try
            {


                System.Drawing.Drawing2D.GraphicsPath buttonPath =
               new System.Drawing.Drawing2D.GraphicsPath();

                //// Set a new rectangle to the same size as the button's 
                //// ClientRectangle property.
                //System.Drawing.Rectangle newRectangle = btnUserProfile.ClientRectangle;

                //// Decrease the size of the rectangle.
                //newRectangle.Inflate(-10, -10);

                //// Draw the button's border.
                //e.Graphics.DrawEllipse(System.Drawing.Pens.Black, newRectangle);

                //// Increase the size of the rectangle to include the border.
                //newRectangle.Inflate(1, 1);

                //// Create a circle within the new rectangle.
                //buttonPath.AddEllipse(newRectangle);

                //// Set the button's Region property to the newly created 
                //// circle region.
                //btnUserProfile.Region = new System.Drawing.Region(buttonPath);
            }

            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnUserProfile_Paint()", ex);
            }
        }

        public void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //IsLoggedOut = true ;
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

        private void manageCustomertoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ManageCustomerForm ObjManageCustomerForm = new ManageCustomerForm();
                ShowChildForm(ObjManageCustomerForm);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.manageCustomertoolStripMenuItem_Click()", ex);
            }
        }

        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Views.OrdersMainForm ordersMainForm = new Views.OrdersMainForm();
                ShowChildForm(ordersMainForm);
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
                Views.InvoicesMainForm invoicesMainForm = new Views.InvoicesMainForm();
                ShowChildForm(invoicesMainForm);
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
                SettingsForm ObjSettingsForm = new SettingsForm();
                ShowChildForm(ObjSettingsForm);
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
                ManageCustomerForm ObjManageCustomerForm = new ManageCustomerForm();
                ShowChildForm(ObjManageCustomerForm);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.customerToolStripMenuItem_Click()", ex);
            }
        }

        private void PaymentsExpensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PaymentsExpensesMainForm ObjPaymentsExpensesMainForm = new PaymentsExpensesMainForm();
                ShowChildForm(ObjPaymentsExpensesMainForm);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.PaymentsExpensesToolStripMenuItem_Click()", ex);
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
                ShowChildForm(new VendorsMainForm());
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
    }
}
