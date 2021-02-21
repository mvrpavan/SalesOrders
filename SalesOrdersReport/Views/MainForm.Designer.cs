using System;

namespace SalesOrdersReport.Views
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
            this.ordersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invoicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.customerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sellerMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.createOrderSheetToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.createInvoiceFromSalesOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createSellerOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeSalesOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateSalesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vendorMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.createOrderSheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addModifyVendorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatePurchasesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PaymentsExpensesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.sellerHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vendorHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productStockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UserProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LogOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cntxtMenuStripUserProfile = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripOrderMasterPath = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.picBoxBackgroundLogo = new System.Windows.Forms.PictureBox();
            this.pnlShortcuts = new System.Windows.Forms.Panel();
            this.lblShortcuts = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBackgroundLogo)).BeginInit();
            this.pnlShortcuts.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ordersToolStripMenuItem,
            this.invoicesToolStripMenuItem,
            this.productMenu,
            this.customerToolStripMenuItem,
            this.sellerMenu,
            this.vendorMenu,
            this.PaymentsExpensesToolStripMenuItem,
            this.reportsMenu,
            this.administrationToolStripMenuItem,
            this.helpMenu,
            this.exitToolStripMenuItem,
            this.UserProfileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1144, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // ordersToolStripMenuItem
            // 
            this.ordersToolStripMenuItem.Name = "ordersToolStripMenuItem";
            this.ordersToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.ordersToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.ordersToolStripMenuItem.Text = "&Orders";
            this.ordersToolStripMenuItem.Click += new System.EventHandler(this.ordersToolStripMenuItem_Click);
            // 
            // invoicesToolStripMenuItem
            // 
            this.invoicesToolStripMenuItem.Name = "invoicesToolStripMenuItem";
            this.invoicesToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.invoicesToolStripMenuItem.Text = "&Invoices";
            this.invoicesToolStripMenuItem.Click += new System.EventHandler(this.invoicesToolStripMenuItem_Click);
            // 
            // productMenu
            // 
            this.productMenu.Name = "productMenu";
            this.productMenu.Size = new System.Drawing.Size(66, 20);
            this.productMenu.Text = "&Products";
            this.productMenu.Click += new System.EventHandler(this.productMenu_Click);
            // 
            // customerToolStripMenuItem
            // 
            this.customerToolStripMenuItem.Name = "customerToolStripMenuItem";
            this.customerToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.customerToolStripMenuItem.Text = "&Customers";
            this.customerToolStripMenuItem.Click += new System.EventHandler(this.customerToolStripMenuItem_Click);
            // 
            // sellerMenu
            // 
            this.sellerMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createOrderSheetToolStripMenuItem1,
            this.createInvoiceFromSalesOrderToolStripMenuItem,
            this.createSellerOrderToolStripMenuItem,
            this.mergeSalesOrderToolStripMenuItem,
            this.updateSalesToolStripMenuItem});
            this.sellerMenu.Name = "sellerMenu";
            this.sellerMenu.Size = new System.Drawing.Size(81, 20);
            this.sellerMenu.Text = "&Seller Menu";
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
            // vendorMenu
            // 
            this.vendorMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createOrderSheetToolStripMenuItem,
            this.createToolStripMenuItem,
            this.addModifyVendorToolStripMenuItem,
            this.updatePurchasesToolStripMenuItem});
            this.vendorMenu.Name = "vendorMenu";
            this.vendorMenu.Size = new System.Drawing.Size(61, 20);
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
            // PaymentsExpensesToolStripMenuItem
            // 
            this.PaymentsExpensesToolStripMenuItem.Name = "PaymentsExpensesToolStripMenuItem";
            this.PaymentsExpensesToolStripMenuItem.Size = new System.Drawing.Size(124, 20);
            this.PaymentsExpensesToolStripMenuItem.Text = "Payments/&Expenses";
            this.PaymentsExpensesToolStripMenuItem.Click += new System.EventHandler(this.PaymentsExpensesToolStripMenuItem_Click);
            // 
            // reportsMenu
            // 
            this.reportsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sellerHistoryToolStripMenuItem,
            this.vendorHistoryToolStripMenuItem,
            this.productStockToolStripMenuItem});
            this.reportsMenu.Name = "reportsMenu";
            this.reportsMenu.Size = new System.Drawing.Size(59, 20);
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
            this.appSettingsToolStripMenuItem});
            this.administrationToolStripMenuItem.Name = "administrationToolStripMenuItem";
            this.administrationToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.administrationToolStripMenuItem.Text = "&Administration";
            // 
            // manageUsersToolStripMenuItem
            // 
            this.manageUsersToolStripMenuItem.Name = "manageUsersToolStripMenuItem";
            this.manageUsersToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.manageUsersToolStripMenuItem.Text = "Manage Users";
            this.manageUsersToolStripMenuItem.Click += new System.EventHandler(this.manageUsersToolStripMenuItem_Click);
            // 
            // appSettingsToolStripMenuItem
            // 
            this.appSettingsToolStripMenuItem.Image = global::SalesOrdersReport.Properties.Resources.Settings;
            this.appSettingsToolStripMenuItem.Name = "appSettingsToolStripMenuItem";
            this.appSettingsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.appSettingsToolStripMenuItem.Text = "Application Settings";
            this.appSettingsToolStripMenuItem.Click += new System.EventHandler(this.appSettingsToolStripMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(44, 20);
            this.helpMenu.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // UserProfileToolStripMenuItem
            // 
            this.UserProfileToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.UserProfileToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.UserProfileToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.UserProfileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ProfileToolStripMenuItem,
            this.LogOutToolStripMenuItem});
            this.UserProfileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserProfileToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.UserProfileToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.UserProfileToolStripMenuItem.Name = "UserProfileToolStripMenuItem";
            this.UserProfileToolStripMenuItem.Size = new System.Drawing.Size(12, 20);
            this.UserProfileToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.UserProfileToolStripMenuItem.Click += new System.EventHandler(this.btnUserProfile_Click);
            this.UserProfileToolStripMenuItem.Paint += new System.Windows.Forms.PaintEventHandler(this.btnUserProfile_Paint);
            // 
            // ProfileToolStripMenuItem
            // 
            this.ProfileToolStripMenuItem.Name = "ProfileToolStripMenuItem";
            this.ProfileToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.ProfileToolStripMenuItem.Text = "Profile";
            this.ProfileToolStripMenuItem.Click += new System.EventHandler(this.ProfileToolStripMenuItem_Click);
            // 
            // LogOutToolStripMenuItem
            // 
            this.LogOutToolStripMenuItem.Name = "LogOutToolStripMenuItem";
            this.LogOutToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.LogOutToolStripMenuItem.Text = "Log Out";
            this.LogOutToolStripMenuItem.Click += new System.EventHandler(this.LogOutToolStripMenuItem_Click);
            // 
            // cntxtMenuStripUserProfile
            // 
            this.cntxtMenuStripUserProfile.Name = "cntxtMenuStripUserProfile";
            this.cntxtMenuStripUserProfile.Size = new System.Drawing.Size(61, 4);
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
            this.statusStrip.Size = new System.Drawing.Size(1144, 22);
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
            this.toolStripStatusLabel.Size = new System.Drawing.Size(874, 17);
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
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1144, 23);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // picBoxBackgroundLogo
            // 
            this.picBoxBackgroundLogo.BackColor = System.Drawing.SystemColors.Control;
            this.picBoxBackgroundLogo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.picBoxBackgroundLogo.Location = new System.Drawing.Point(0, 527);
            this.picBoxBackgroundLogo.Name = "picBoxBackgroundLogo";
            this.picBoxBackgroundLogo.Size = new System.Drawing.Size(1144, 23);
            this.picBoxBackgroundLogo.TabIndex = 6;
            this.picBoxBackgroundLogo.TabStop = false;
            // 
            // pnlShortcuts
            // 
            this.pnlShortcuts.Controls.Add(this.lblShortcuts);
            this.pnlShortcuts.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlShortcuts.Location = new System.Drawing.Point(0, 487);
            this.pnlShortcuts.Name = "pnlShortcuts";
            this.pnlShortcuts.Size = new System.Drawing.Size(1144, 40);
            this.pnlShortcuts.TabIndex = 10;
            // 
            // lblShortcuts
            // 
            this.lblShortcuts.AutoSize = true;
            this.lblShortcuts.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShortcuts.Location = new System.Drawing.Point(12, 9);
            this.lblShortcuts.Name = "lblShortcuts";
            this.lblShortcuts.Size = new System.Drawing.Size(63, 15);
            this.lblShortcuts.TabIndex = 0;
            this.lblShortcuts.Text = "Shortcuts";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 572);
            this.Controls.Add(this.pnlShortcuts);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.picBoxBackgroundLogo);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBackgroundLogo)).EndInit();
            this.pnlShortcuts.ResumeLayout(false);
            this.pnlShortcuts.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

 


        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolTip toolTip;
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
        private System.Windows.Forms.ToolStripStatusLabel toolStripProgress;
        private System.Windows.Forms.ContextMenuStrip cntxtMenuStripUserProfile;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripOrderMasterPath;
        private System.Windows.Forms.ToolStripMenuItem updatePurchasesToolStripMenuItem;
        private System.Windows.Forms.PictureBox picBoxBackgroundLogo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem createInvoiceFromSalesOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createSellerOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeSalesOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem administrationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageUsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem appSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LogOutToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem UserProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ordersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invoicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel pnlShortcuts;
        private System.Windows.Forms.Label lblShortcuts;
        private System.Windows.Forms.ToolStripMenuItem PaymentsExpensesToolStripMenuItem;
    }
}



