using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SalesOrdersReport
{
    public partial class MainForm : Form
    {
        public Boolean MasterSheetSelected;
        MySQLHelper tmpMySQLHelper;
        public bool IsLoggedOut = false;
        public MainForm()
        {
            // CommonFunctions.Initialize();

            InitializeComponent();
            tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();

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
            // btnUserProfile.Text = lblCurrentUser.Text;
            fileMenu.Visible = true;
            sellerMenu.Visible = true;
            vendorMenu.Visible = true;
            productMenu.Visible = false;
            reportsMenu.Visible = true;

            addModifySellerToolStripMenuItem.Visible = false;
            discountGroupToolStripMenuItem.Visible = false;

            priceGroupsToolStripMenuItem.Visible = false;
            addModifyItemToolStripMenuItem.Visible = false;

            addModifyVendorToolStripMenuItem.Visible = false;

            vendorHistoryToolStripMenuItem.Visible = false;
            productStockToolStripMenuItem.Visible = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommonFunctions.WriteToSettingsFile();
        }

        //void LoadProductLines()
        //{
        //    try
        //    {
        //        //Load toolStripComboBoxProductLine from CommonFunctions.ListProductLines
        //        toolStripComboBoxProductLine.Items.Clear();
        //        for (int i = 1; i < CommonFunctions.ListProductLines.Count; i++)
        //        {
        //            ProductLine ObjProductLine = CommonFunctions.ListProductLines[i];
        //            toolStripComboBoxProductLine.Items.Add(ObjProductLine.Name);
        //        }
        //        toolStripComboBoxProductLine.Items.Add("<Create New>");
        //        toolStripComboBoxProductLine.SelectedIndex = CommonFunctions.SelectedProductLineIndex - 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonFunctions.ShowErrorDialog("MainForm.LoadProductLines()", ex);
        //    }
        //}

        public void ShowChildForm(Form ObjForm)
        {
            try
            {
                if (MdiChildren.Length > 0) return;

                CommonFunctions.ResetProgressBar();

                //CommonFunctions.CurrentForm = ObjForm;
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
                CommonFunctions.ShowErrorDialog("MainForm.ShowChildForm()", ex);
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

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SettingsForm ObjSettingsForm = new SettingsForm();
                ShowChildForm(ObjSettingsForm);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.OrderMasterForm_FormClosed()", ex);
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void createInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToOpenChildForm()) return;

                CustomerInvoiceSellerOrderForm ObjCustomerInvoiceForm = new CustomerInvoiceSellerOrderForm(false, true);
                ShowChildForm(ObjCustomerInvoiceForm);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.createInvoiceToolStripMenuItem_Click()", ex);
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

        private void discountGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToOpenChildForm()) return;
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
                if (!IsValidToOpenChildForm()) return;
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
        private void priceGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addModifyItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!IsValidToOpenChildForm()) return;

                ProductsMainForm productsMainForm = new ProductsMainForm();
                ShowChildForm(productsMainForm);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.addModifyItemToolStripMenuItem_Click()", ex);
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

        private void ManageVendorsToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

        #region ProductLine Toolstrip
        //private void toolStripComboBoxProductLine_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (toolStripComboBoxProductLine.SelectedIndex + 1 == CommonFunctions.SelectedProductLineIndex) return;

        //        if (MdiChildren.Length > 0)
        //        {
        //            toolStripComboBoxProductLine.SelectedIndex = CommonFunctions.SelectedProductLineIndex - 1;
        //            MessageBox.Show(this, "Cannot change Product Line, while working in Current Product Line.\nClose other windows to select another Product Line.", "Product Line", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        MasterSheetSelected = false;
        //        toolStripOrderMasterPath.Text = "";

        //        if (toolStripComboBoxProductLine.SelectedItem.ToString().Equals("<Create New>"))
        //        {
        //            ManageProductLineForm ObjManageProductLineForm = new ManageProductLineForm();
        //            ShowChildForm(ObjManageProductLineForm);
        //            ObjManageProductLineForm.FormClosed += new FormClosedEventHandler(ObjManageProductLineForm_FormClosed);
        //        }
        //        else
        //        {
        //            CommonFunctions.SelectProductLine(Int32.Parse((toolStripComboBoxProductLine.SelectedIndex + 1).ToString()));
        //            MessageBox.Show(this, "Product Line changed to " + toolStripComboBoxProductLine.SelectedItem, "Product Line", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            statusStrip.Focus();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonFunctions.ShowErrorDialog("MainForm.toolStripComboBoxProductLine_SelectedIndexChanged()", ex);
        //    }
        //}

        //void ObjManageProductLineForm_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    try
        //    {
        //        LoadProductLines();
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonFunctions.ShowErrorDialog("MainForm.ObjManageProductLineForm_FormClosed()", ex);
        //    }
        //}
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

        private void createUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToOpenChildForm()) return;

                //CreateUserForm ObjCreateUserForm = new CreateUserForm();
                //ShowChildForm(ObjCreateUserForm);

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.createUserToolStripMenuItem_Click()", ex);
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

        private void editUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToOpenChildForm()) return;

                //EditUserForm ObjEditUserForm = new EditUserForm();
                //ShowChildForm(ObjEditUserForm);

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.editUserToolStripMenuItem_Click()", ex);
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
        private void createRoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsValidToOpenChildForm()) return;

                //CreateRoleForm ObjCreateRoleForm = new CreateRoleForm();
                //ShowChildForm(ObjCreateRoleForm);


            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.createRoleToolStripMenuItem_Click()", ex);
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

        //private void dgv1_MouseClick(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Right)
        //    {
        //        ContextMenuStrip m = new ContextMenuStrip();
        //        m.Items.Add("Add");
        //        m.Items.Add("Delete");
        //        m.Show(dgv1, new Point(e.X, e.Y));

        //        m.ItemClicked += new ToolStripItemClickedEventHandler(Item_Click);

        //    }
        //}
        //private void Item_Click(object sender, ToolStripItemClickedEventArgs e)
        //{
        //    if (e.ClickedItem.Text == "Delete")
        //    {
        //        //Codes Here
        //    }
        //    else
        //    {
        //        //Codes Here
        //    }
        //}
        private void cntxtMenuStripUserProfile_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                //if (e.ClickedItem.Text == "Change Password")

                if (e.ClickedItem.Text == "Profile")
                {
                    //if (!IsValidToOpenChildForm()) return;

                    EditProfileForm ObjEditProfileForm = new EditProfileForm();
                    ShowChildForm(ObjEditProfileForm);
                    //ObjEditProfileForm.FormClosed += ObjEditProfileForm_FormClosed;
                    //ObjEditProfileForm.Show();
                }

                else if (e.ClickedItem.Text == "Log Out")
                {
                    //this.Close();
                    // if (!IsValidToOpenChildForm()) return;
                    //Application.Run(new LoginForm());
                    UpdateTableOnLogout();
                    this.Hide();
                    CommonFunctions.CurrentForm.Dispose();
                    CommonFunctions.CurrentForm.Close();
                    LoginForm ObjLog = new LoginForm();
                    ObjLog.Show();
                    //ObjLog.Dispose(); //because user has logged out so the data must be flushed, by "Disposing" it will not be in the RAM anymore, so your hanging problem will be solved
                    //ObjLog.Show();


                    //Application.Exit();

                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.cntxtMenuStripUserProfile_ItemClicked()", ex);
                throw;
            }
        }
        //private void ObjEditProfileForm_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    this.Close();
        //}
        public void UpdateTableOnLogout()
        {
            try
            {
                List<string> ListColumnValues = new List<string>(), ListColumnNames = new List<string>();
                ListColumnValues.Add(tmpMySQLHelper.LoginTime.ToString("yyyy-MM-dd H:mm:ss"));
                ListColumnNames.Add("LASTLOGIN");

                string WhereCondition = "USERNAME = '" + tmpMySQLHelper.CurrentUser + "'";

                int ResultVal = CommonFunctions.ObjUserMasterModel.UpdateAnyTableDetails("USERMASTER", ListColumnNames, ListColumnValues, WhereCondition);
                if (ResultVal < 0) MessageBox.Show("Wasnt able to Update Date Column", "Error", MessageBoxButtons.OK);
            }

            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MainForm.UpdateTableOnLogout()", ex);
                throw;
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

        public void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsLoggedOut = true ;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.UserProfileToolStripMenuItem.Text = tmpMySQLHelper.CurrentUser;
            //this.UserProfileToolStripMenuItem.Text = tmpMySQLHelper.CurrentUser = "Nisha";

             List<string> ListAssignesPrivilegeNames = CommonFunctions.ObjUserMasterModel.GetOnlyAssignedPrivilegeNamesForAnUser(tmpMySQLHelper.CurrentUser);
            //&&&&&& checkonce
            //string strControlVal = "manageUsersToolStripMenuItem"; //"SalesToolStripMenuItem" or "invoiceToolStripMenuItem" 

            foreach (ToolStripMenuItem item in administrationToolStripMenuItem.DropDownItems)
            {
                //if (strControlVal == item.Name)
                //{
                //    item.Visible = false;
                //}
            }
        }

        private void manageCustomertoolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageCustomerForm ObjManageCustomerForm = new ManageCustomerForm();
            ShowChildForm(ObjManageCustomerForm);
        }


        //private void ObjManageUsersForm_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    this.Close();
        //}
    }
}
