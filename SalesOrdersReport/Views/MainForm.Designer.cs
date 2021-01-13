using System;

namespace SalesOrdersReport
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseMasterFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sellerMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.createOrderSheetToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.createInvoiceFromSalesOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createSellerOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeSalesOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateSalesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invoicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createInvoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.vendorMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.createOrderSheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addModifyVendorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatePurchasesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.sellerHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vendorHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productStockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageProductsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createRoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxProductLine = new System.Windows.Forms.ToolStripComboBox();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripOrderMasterPath = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.picBoxBackgroundLogo = new System.Windows.Forms.PictureBox();
            this.lblCurrentUser = new System.Windows.Forms.Label();
            this.showAllOrdersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBackgroundLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.sellerMenu,
            this.invoicesToolStripMenuItem,
            this.customersToolStripMenuItem,
            this.productMenu,
            this.vendorMenu,
            this.reportsMenu,
            this.administrationToolStripMenuItem,
            this.userToolStripMenuItem,
            this.toolStripComboBoxProductLine,
            this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(844, 27);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chooseMasterFileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 23);
            this.fileMenu.Text = "&File";
            // 
            // chooseMasterFileToolStripMenuItem
            // 
            this.chooseMasterFileToolStripMenuItem.Image = global::SalesOrdersReport.Properties.Resources.database_5_16;
            this.chooseMasterFileToolStripMenuItem.Name = "chooseMasterFileToolStripMenuItem";
            this.chooseMasterFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.chooseMasterFileToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.chooseMasterFileToolStripMenuItem.Text = "&Choose Master File";
            this.chooseMasterFileToolStripMenuItem.Click += new System.EventHandler(this.chooseMasterFileToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Image = global::SalesOrdersReport.Properties.Resources.Settings;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.settingsToolStripMenuItem.Text = "S&ettings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::SalesOrdersReport.Properties.Resources.Close_Black_VerySmall;
            this.exitToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
            // 
            // sellerMenu
            // 
            this.sellerMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showAllOrdersToolStripMenuItem,
            this.createOrderSheetToolStripMenuItem1,
            this.createInvoiceFromSalesOrderToolStripMenuItem,
            this.createSellerOrderToolStripMenuItem,
            this.mergeSalesOrderToolStripMenuItem,
            this.updateSalesToolStripMenuItem});
            this.sellerMenu.Name = "sellerMenu";
            this.sellerMenu.Size = new System.Drawing.Size(54, 23);
            this.sellerMenu.Text = "&Orders";
            // 
            // createOrderSheetToolStripMenuItem1
            // 
            this.createOrderSheetToolStripMenuItem1.Name = "createOrderSheetToolStripMenuItem1";
            this.createOrderSheetToolStripMenuItem1.Size = new System.Drawing.Size(202, 22);
            this.createOrderSheetToolStripMenuItem1.Text = "Create Sales &Order Sheet";
            this.createOrderSheetToolStripMenuItem1.Click += new System.EventHandler(this.createSalesOrderSheetToolStripMenuItem_Click);
            // 
            // createInvoiceFromSalesOrderToolStripMenuItem
            // 
            this.createInvoiceFromSalesOrderToolStripMenuItem.Name = "createInvoiceFromSalesOrderToolStripMenuItem";
            this.createInvoiceFromSalesOrderToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.createInvoiceFromSalesOrderToolStripMenuItem.Text = "Create Seller &Invoices";
            this.createInvoiceFromSalesOrderToolStripMenuItem.Click += new System.EventHandler(this.createInvoiceFromSalesOrderToolStripMenuItem_Click);
            // 
            // createSellerOrderToolStripMenuItem
            // 
            this.createSellerOrderToolStripMenuItem.Name = "createSellerOrderToolStripMenuItem";
            this.createSellerOrderToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.createSellerOrderToolStripMenuItem.Text = "Create &Seller Order";
            this.createSellerOrderToolStripMenuItem.Click += new System.EventHandler(this.createSellerOrderToolStripMenuItem_Click);
            // 
            // mergeSalesOrderToolStripMenuItem
            // 
            this.mergeSalesOrderToolStripMenuItem.Name = "mergeSalesOrderToolStripMenuItem";
            this.mergeSalesOrderToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.mergeSalesOrderToolStripMenuItem.Text = "M&erge Sales Order";
            this.mergeSalesOrderToolStripMenuItem.Click += new System.EventHandler(this.mergeSalesOrderToolStripMenuItem_Click);
            // 
            // updateSalesToolStripMenuItem
            // 
            this.updateSalesToolStripMenuItem.Name = "updateSalesToolStripMenuItem";
            this.updateSalesToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.updateSalesToolStripMenuItem.Text = "&Update Sales";
            this.updateSalesToolStripMenuItem.Click += new System.EventHandler(this.updateSalesToolStripMenuItem_Click);
            // 
            // invoicesToolStripMenuItem
            // 
            this.invoicesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createInvoiceToolStripMenuItem});
            this.invoicesToolStripMenuItem.Name = "invoicesToolStripMenuItem";
            this.invoicesToolStripMenuItem.Size = new System.Drawing.Size(62, 23);
            this.invoicesToolStripMenuItem.Text = "&Invoices";
            // 
            // createInvoiceToolStripMenuItem
            // 
            this.createInvoiceToolStripMenuItem.Name = "createInvoiceToolStripMenuItem";
            this.createInvoiceToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.createInvoiceToolStripMenuItem.Text = "Create &Customer Bill";
            this.createInvoiceToolStripMenuItem.Click += new System.EventHandler(this.createInvoiceToolStripMenuItem_Click);
            // 
            // customersToolStripMenuItem
            // 
            this.customersToolStripMenuItem.Name = "customersToolStripMenuItem";
            this.customersToolStripMenuItem.Size = new System.Drawing.Size(76, 23);
            this.customersToolStripMenuItem.Text = "&Customers";
            this.customersToolStripMenuItem.Click += new System.EventHandler(this.customersToolStripMenuItem_Click);
            // 
            // productMenu
            // 
            this.productMenu.Name = "productMenu";
            this.productMenu.Size = new System.Drawing.Size(66, 23);
            this.productMenu.Text = "&Products";
            this.productMenu.Click += new System.EventHandler(this.productMenu_Click);
            // 
            // vendorMenu
            // 
            this.vendorMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createOrderSheetToolStripMenuItem,
            this.createToolStripMenuItem,
            this.addModifyVendorToolStripMenuItem,
            this.updatePurchasesToolStripMenuItem});
            this.vendorMenu.Name = "vendorMenu";
            this.vendorMenu.Size = new System.Drawing.Size(61, 23);
            this.vendorMenu.Text = "&Vendors";
            // 
            // createOrderSheetToolStripMenuItem
            // 
            this.createOrderSheetToolStripMenuItem.Name = "createOrderSheetToolStripMenuItem";
            this.createOrderSheetToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.createOrderSheetToolStripMenuItem.Text = "Create Vendor &Order Sheet";
            this.createOrderSheetToolStripMenuItem.Click += new System.EventHandler(this.createOrderSheetToolStripMenuItem_Click);
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.createToolStripMenuItem.Text = "Create &Purchase Orders";
            this.createToolStripMenuItem.Click += new System.EventHandler(this.createPurchaseOrderToolStripMenuItem_Click);
            // 
            // addModifyVendorToolStripMenuItem
            // 
            this.addModifyVendorToolStripMenuItem.Name = "addModifyVendorToolStripMenuItem";
            this.addModifyVendorToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.addModifyVendorToolStripMenuItem.Text = "&Manage Vendors";
            this.addModifyVendorToolStripMenuItem.Click += new System.EventHandler(this.ManageVendorsToolStripMenuItem_Click);
            // 
            // updatePurchasesToolStripMenuItem
            // 
            this.updatePurchasesToolStripMenuItem.Name = "updatePurchasesToolStripMenuItem";
            this.updatePurchasesToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.updatePurchasesToolStripMenuItem.Text = "&Update Purchases";
            this.updatePurchasesToolStripMenuItem.Click += new System.EventHandler(this.updatePurchasesToolStripMenuItem_Click);
            // 
            // reportsMenu
            // 
            this.reportsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sellerHistoryToolStripMenuItem,
            this.vendorHistoryToolStripMenuItem,
            this.productStockToolStripMenuItem});
            this.reportsMenu.Name = "reportsMenu";
            this.reportsMenu.Size = new System.Drawing.Size(59, 23);
            this.reportsMenu.Text = "&Reports";
            // 
            // sellerHistoryToolStripMenuItem
            // 
            this.sellerHistoryToolStripMenuItem.Name = "sellerHistoryToolStripMenuItem";
            this.sellerHistoryToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sellerHistoryToolStripMenuItem.Text = "&Seller History";
            this.sellerHistoryToolStripMenuItem.Click += new System.EventHandler(this.sellerHistoryToolStripMenuItem_Click);
            // 
            // vendorHistoryToolStripMenuItem
            // 
            this.vendorHistoryToolStripMenuItem.Name = "vendorHistoryToolStripMenuItem";
            this.vendorHistoryToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.vendorHistoryToolStripMenuItem.Text = "&Vendor History";
            this.vendorHistoryToolStripMenuItem.Click += new System.EventHandler(this.vendorHistoryToolStripMenuItem_Click);
            // 
            // productStockToolStripMenuItem
            // 
            this.productStockToolStripMenuItem.Name = "productStockToolStripMenuItem";
            this.productStockToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.productStockToolStripMenuItem.Text = "&Product Stock";
            this.productStockToolStripMenuItem.Click += new System.EventHandler(this.productStockToolStripMenuItem_Click);
            // 
            // administrationToolStripMenuItem
            // 
            this.administrationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageUsersToolStripMenuItem,
            this.manageProductsToolStripMenuItem,
            this.applicationSettingsToolStripMenuItem});
            this.administrationToolStripMenuItem.Name = "administrationToolStripMenuItem";
            this.administrationToolStripMenuItem.Size = new System.Drawing.Size(98, 23);
            this.administrationToolStripMenuItem.Text = "Administration";
            // 
            // manageUsersToolStripMenuItem
            // 
            this.manageUsersToolStripMenuItem.Name = "manageUsersToolStripMenuItem";
            this.manageUsersToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.manageUsersToolStripMenuItem.Text = "Manage Users";
            // 
            // manageProductsToolStripMenuItem
            // 
            this.manageProductsToolStripMenuItem.Name = "manageProductsToolStripMenuItem";
            this.manageProductsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.manageProductsToolStripMenuItem.Text = "Organization Settings";
            // 
            // applicationSettingsToolStripMenuItem
            // 
            this.applicationSettingsToolStripMenuItem.Name = "applicationSettingsToolStripMenuItem";
            this.applicationSettingsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.applicationSettingsToolStripMenuItem.Text = "Application Settings";
            // 
            // userToolStripMenuItem
            // 
            this.userToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createUserToolStripMenuItem,
            this.editProfileToolStripMenuItem,
            this.editUserToolStripMenuItem,
            this.createRoleToolStripMenuItem});
            this.userToolStripMenuItem.Name = "userToolStripMenuItem";
            this.userToolStripMenuItem.Size = new System.Drawing.Size(42, 23);
            this.userToolStripMenuItem.Text = "&User";
            // 
            // createUserToolStripMenuItem
            // 
            this.createUserToolStripMenuItem.Name = "createUserToolStripMenuItem";
            this.createUserToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.createUserToolStripMenuItem.Text = "Create User";
            this.createUserToolStripMenuItem.Click += new System.EventHandler(this.createUserToolStripMenuItem_Click);
            // 
            // editProfileToolStripMenuItem
            // 
            this.editProfileToolStripMenuItem.Name = "editProfileToolStripMenuItem";
            this.editProfileToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.editProfileToolStripMenuItem.Text = "Edit Profile";
            this.editProfileToolStripMenuItem.Click += new System.EventHandler(this.editProfileToolStripMenuItem_Click);
            // 
            // editUserToolStripMenuItem
            // 
            this.editUserToolStripMenuItem.Name = "editUserToolStripMenuItem";
            this.editUserToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.editUserToolStripMenuItem.Text = "Edit User";
            this.editUserToolStripMenuItem.Click += new System.EventHandler(this.editUserToolStripMenuItem_Click);
            // 
            // createRoleToolStripMenuItem
            // 
            this.createRoleToolStripMenuItem.Name = "createRoleToolStripMenuItem";
            this.createRoleToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.createRoleToolStripMenuItem.Text = "Create Role";
            this.createRoleToolStripMenuItem.Click += new System.EventHandler(this.createRoleToolStripMenuItem_Click);
            // 
            // toolStripComboBoxProductLine
            // 
            this.toolStripComboBoxProductLine.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripComboBoxProductLine.Items.AddRange(new object[] {
            "Vegetables",
            "Bakery"});
            this.toolStripComboBoxProductLine.Name = "toolStripComboBoxProductLine";
            this.toolStripComboBoxProductLine.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBoxProductLine.Visible = false;
            this.toolStripComboBoxProductLine.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxProductLine_SelectedIndexChanged);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(44, 23);
            this.helpMenu.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripOrderMasterPath,
            this.toolStripStatusLabel,
            this.toolStripProgressBar,
            this.toolStripProgress});
            this.statusStrip.Location = new System.Drawing.Point(0, 550);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(844, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "Status Strip";
            this.statusStrip.SizeChanged += new System.EventHandler(this.statusStrip_SizeChanged);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(67, 17);
            this.toolStripStatusLabel1.Text = "Master File:";
            // 
            // toolStripOrderMasterPath
            // 
            this.toolStripOrderMasterPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.toolStripOrderMasterPath.Name = "toolStripOrderMasterPath";
            this.toolStripOrderMasterPath.Size = new System.Drawing.Size(32, 17);
            this.toolStripOrderMasterPath.Text = "Path";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(605, 17);
            this.toolStripStatusLabel.Spring = true;
            this.toolStripStatusLabel.Text = "Status";
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar.Step = 1;
            this.toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // toolStripProgress
            // 
            this.toolStripProgress.Name = "toolStripProgress";
            this.toolStripProgress.Size = new System.Drawing.Size(23, 17);
            this.toolStripProgress.Text = "0%";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(844, 23);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // picBoxBackgroundLogo
            // 
            this.picBoxBackgroundLogo.BackColor = System.Drawing.SystemColors.Control;
            this.picBoxBackgroundLogo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.picBoxBackgroundLogo.Location = new System.Drawing.Point(0, 527);
            this.picBoxBackgroundLogo.Name = "picBoxBackgroundLogo";
            this.picBoxBackgroundLogo.Size = new System.Drawing.Size(844, 23);
            this.picBoxBackgroundLogo.TabIndex = 6;
            this.picBoxBackgroundLogo.TabStop = false;
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.AutoSize = true;
            this.lblCurrentUser.Location = new System.Drawing.Point(590, 9);
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(0, 13);
            this.lblCurrentUser.TabIndex = 10;
            // 
            // showAllOrdersToolStripMenuItem
            // 
            this.showAllOrdersToolStripMenuItem.Name = "showAllOrdersToolStripMenuItem";
            this.showAllOrdersToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.showAllOrdersToolStripMenuItem.Text = "S&how All Orders";
            this.showAllOrdersToolStripMenuItem.Click += new System.EventHandler(this.showAllOrdersToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 572);
            this.Controls.Add(this.lblCurrentUser);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.picBoxBackgroundLogo);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(860, 610);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Form";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBackgroundLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

  
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vendorMenu;
        private System.Windows.Forms.ToolStripMenuItem sellerMenu;
        private System.Windows.Forms.ToolStripMenuItem productMenu;
        private System.Windows.Forms.ToolStripMenuItem createOrderSheetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addModifyVendorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createOrderSheetToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem reportsMenu;
        private System.Windows.Forms.ToolStripMenuItem vendorHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sellerHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productStockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateSalesToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripMenuItem chooseMasterFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripProgress;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxProductLine;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripOrderMasterPath;
        private System.Windows.Forms.ToolStripMenuItem updatePurchasesToolStripMenuItem;
        private System.Windows.Forms.PictureBox picBoxBackgroundLogo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem createInvoiceFromSalesOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createSellerOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invoicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createInvoiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeSalesOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createRoleToolStripMenuItem;
        public System.Windows.Forms.Label lblCurrentUser;
        private System.Windows.Forms.ToolStripMenuItem administrationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageUsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageProductsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAllOrdersToolStripMenuItem;
    }
}



