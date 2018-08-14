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
            this.createInvoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addModifySellerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discountGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateSalesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vendorMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.createOrderSheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addModifyVendorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatePurchasesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.addModifyItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.priceGroupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.sellerHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vendorHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productStockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxProductLine = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripOrderMasterPath = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.picBoxBackgroundLogo = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBackgroundLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.sellerMenu,
            this.vendorMenu,
            this.productMenu,
            this.reportsMenu,
            this.helpMenu,
            this.toolStripComboBoxProductLine,
            this.toolStripTextBox1});
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
            this.createOrderSheetToolStripMenuItem1,
            this.createInvoiceFromSalesOrderToolStripMenuItem,
            this.createSellerOrderToolStripMenuItem,
            this.createInvoiceToolStripMenuItem,
            this.addModifySellerToolStripMenuItem,
            this.discountGroupToolStripMenuItem,
            this.updateSalesToolStripMenuItem});
            this.sellerMenu.Name = "sellerMenu";
            this.sellerMenu.Size = new System.Drawing.Size(47, 23);
            this.sellerMenu.Text = "&Seller";
            // 
            // createOrderSheetToolStripMenuItem1
            // 
            this.createOrderSheetToolStripMenuItem1.Name = "createOrderSheetToolStripMenuItem1";
            this.createOrderSheetToolStripMenuItem1.Size = new System.Drawing.Size(208, 22);
            this.createOrderSheetToolStripMenuItem1.Text = "Create Sales &Order Sheet";
            this.createOrderSheetToolStripMenuItem1.Click += new System.EventHandler(this.createSalesOrderSheetToolStripMenuItem_Click);
            // 
            // createInvoiceFromSalesOrderToolStripMenuItem
            // 
            this.createInvoiceFromSalesOrderToolStripMenuItem.Name = "createInvoiceFromSalesOrderToolStripMenuItem";
            this.createInvoiceFromSalesOrderToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.createInvoiceFromSalesOrderToolStripMenuItem.Text = "Create Seller &Invoices";
            this.createInvoiceFromSalesOrderToolStripMenuItem.Click += new System.EventHandler(this.createInvoiceFromSalesOrderToolStripMenuItem_Click);
            // 
            // createSellerOrderToolStripMenuItem
            // 
            this.createSellerOrderToolStripMenuItem.Name = "createSellerOrderToolStripMenuItem";
            this.createSellerOrderToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.createSellerOrderToolStripMenuItem.Text = "Create &Seller Order";
            this.createSellerOrderToolStripMenuItem.Click += new System.EventHandler(this.createSellerOrderToolStripMenuItem_Click);
            // 
            // createInvoiceToolStripMenuItem
            // 
            this.createInvoiceToolStripMenuItem.Name = "createInvoiceToolStripMenuItem";
            this.createInvoiceToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.createInvoiceToolStripMenuItem.Text = "Create Customer I&nvoice";
            this.createInvoiceToolStripMenuItem.Click += new System.EventHandler(this.createInvoiceToolStripMenuItem_Click);
            // 
            // addModifySellerToolStripMenuItem
            // 
            this.addModifySellerToolStripMenuItem.Name = "addModifySellerToolStripMenuItem";
            this.addModifySellerToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.addModifySellerToolStripMenuItem.Text = "&Manage Sellers";
            this.addModifySellerToolStripMenuItem.Click += new System.EventHandler(this.addModifySellerToolStripMenuItem_Click);
            // 
            // discountGroupToolStripMenuItem
            // 
            this.discountGroupToolStripMenuItem.Name = "discountGroupToolStripMenuItem";
            this.discountGroupToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.discountGroupToolStripMenuItem.Text = "Manage &Discount Groups";
            this.discountGroupToolStripMenuItem.Click += new System.EventHandler(this.discountGroupToolStripMenuItem_Click);
            // 
            // updateSalesToolStripMenuItem
            // 
            this.updateSalesToolStripMenuItem.Name = "updateSalesToolStripMenuItem";
            this.updateSalesToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
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
            this.vendorMenu.Size = new System.Drawing.Size(57, 23);
            this.vendorMenu.Text = "&Vendor";
            // 
            // createOrderSheetToolStripMenuItem
            // 
            this.createOrderSheetToolStripMenuItem.Name = "createOrderSheetToolStripMenuItem";
            this.createOrderSheetToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.createOrderSheetToolStripMenuItem.Text = "Create Vendor &Order Sheet";
            this.createOrderSheetToolStripMenuItem.Click += new System.EventHandler(this.createOrderSheetToolStripMenuItem_Click);
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.createToolStripMenuItem.Text = "Create &Purchase Orders";
            this.createToolStripMenuItem.Click += new System.EventHandler(this.createPurchaseOrderToolStripMenuItem_Click);
            // 
            // addModifyVendorToolStripMenuItem
            // 
            this.addModifyVendorToolStripMenuItem.Name = "addModifyVendorToolStripMenuItem";
            this.addModifyVendorToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.addModifyVendorToolStripMenuItem.Text = "&Manage Vendors";
            this.addModifyVendorToolStripMenuItem.Click += new System.EventHandler(this.ManageVendorsToolStripMenuItem_Click);
            // 
            // updatePurchasesToolStripMenuItem
            // 
            this.updatePurchasesToolStripMenuItem.Name = "updatePurchasesToolStripMenuItem";
            this.updatePurchasesToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.updatePurchasesToolStripMenuItem.Text = "&Update Purchases";
            this.updatePurchasesToolStripMenuItem.Click += new System.EventHandler(this.updatePurchasesToolStripMenuItem_Click);
            // 
            // productMenu
            // 
            this.productMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addModifyItemToolStripMenuItem,
            this.priceGroupsToolStripMenuItem});
            this.productMenu.Name = "productMenu";
            this.productMenu.Size = new System.Drawing.Size(61, 23);
            this.productMenu.Text = "&Product";
            // 
            // addModifyItemToolStripMenuItem
            // 
            this.addModifyItemToolStripMenuItem.Name = "addModifyItemToolStripMenuItem";
            this.addModifyItemToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.addModifyItemToolStripMenuItem.Text = "&Manage Products";
            this.addModifyItemToolStripMenuItem.Click += new System.EventHandler(this.addModifyItemToolStripMenuItem_Click);
            // 
            // priceGroupsToolStripMenuItem
            // 
            this.priceGroupsToolStripMenuItem.Name = "priceGroupsToolStripMenuItem";
            this.priceGroupsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.priceGroupsToolStripMenuItem.Text = "Manage &Price Groups";
            this.priceGroupsToolStripMenuItem.Click += new System.EventHandler(this.priceGroupsToolStripMenuItem_Click);
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
            this.sellerHistoryToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.sellerHistoryToolStripMenuItem.Text = "&Seller History";
            this.sellerHistoryToolStripMenuItem.Click += new System.EventHandler(this.sellerHistoryToolStripMenuItem_Click);
            // 
            // vendorHistoryToolStripMenuItem
            // 
            this.vendorHistoryToolStripMenuItem.Name = "vendorHistoryToolStripMenuItem";
            this.vendorHistoryToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.vendorHistoryToolStripMenuItem.Text = "&Vendor History";
            this.vendorHistoryToolStripMenuItem.Click += new System.EventHandler(this.vendorHistoryToolStripMenuItem_Click);
            // 
            // productStockToolStripMenuItem
            // 
            this.productStockToolStripMenuItem.Name = "productStockToolStripMenuItem";
            this.productStockToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.productStockToolStripMenuItem.Text = "&Product Stock";
            this.productStockToolStripMenuItem.Click += new System.EventHandler(this.productStockToolStripMenuItem_Click);
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
            // toolStripComboBoxProductLine
            // 
            this.toolStripComboBoxProductLine.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripComboBoxProductLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxProductLine.Items.AddRange(new object[] {
            "Vegetables",
            "Bakery"});
            this.toolStripComboBoxProductLine.Name = "toolStripComboBoxProductLine";
            this.toolStripComboBoxProductLine.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBoxProductLine.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxProductLine_SelectedIndexChanged);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox1.BackColor = System.Drawing.SystemColors.Menu;
            this.toolStripTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.toolStripTextBox1.Margin = new System.Windows.Forms.Padding(1, 2, 10, 0);
            this.toolStripTextBox1.MaxLength = 20;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.ReadOnly = true;
            this.toolStripTextBox1.ShortcutsEnabled = false;
            this.toolStripTextBox1.Size = new System.Drawing.Size(80, 21);
            this.toolStripTextBox1.Text = "Product Line";
            this.toolStripTextBox1.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolStripTextBox1.ToolTipText = "Select Product Line";
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
            this.toolStripOrderMasterPath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripOrderMasterPath.Name = "toolStripOrderMasterPath";
            this.toolStripOrderMasterPath.Size = new System.Drawing.Size(31, 17);
            this.toolStripOrderMasterPath.Text = "Path";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(606, 17);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 572);
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
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBackgroundLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem createInvoiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addModifySellerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addModifyItemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem discountGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem priceGroupsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsMenu;
        private System.Windows.Forms.ToolStripMenuItem vendorHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sellerHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productStockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateSalesToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripMenuItem chooseMasterFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripProgress;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxProductLine;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripOrderMasterPath;
        private System.Windows.Forms.ToolStripMenuItem updatePurchasesToolStripMenuItem;
        private System.Windows.Forms.PictureBox picBoxBackgroundLogo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem createInvoiceFromSalesOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createSellerOrderToolStripMenuItem;
    }
}



