namespace SalesOrdersReport.Views
{
    partial class ProductInventoryMainForm
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
            this.btnCreateStock = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUploadToDb = new System.Windows.Forms.Button();
            this.btnImportFromExcel = new System.Windows.Forms.Button();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.btnEditStockProduct = new System.Windows.Forms.Button();
            this.btnDeleteStock = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearchProduct = new System.Windows.Forms.Button();
            this.cmbBoxCategoryFilterList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClearSearchProduct = new System.Windows.Forms.Button();
            this.txtBoxProductSearchString = new System.Windows.Forms.TextBox();
            this.btnReloadStocks = new System.Windows.Forms.Button();
            this.dgvGridViewStocks = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStocksCount = new System.Windows.Forms.Label();
            this.backgroundWorkerOrders = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGridViewStocks)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCreateStock
            // 
            this.btnCreateStock.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCreateStock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnCreateStock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCreateStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateStock.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateStock.Image = global::SalesOrdersReport.Properties.Resources.math_add_icon_32;
            this.btnCreateStock.Location = new System.Drawing.Point(5, 3);
            this.btnCreateStock.Name = "btnCreateStock";
            this.btnCreateStock.Size = new System.Drawing.Size(83, 73);
            this.btnCreateStock.TabIndex = 0;
            this.btnCreateStock.Text = "Add Stock Product";
            this.btnCreateStock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCreateStock.UseVisualStyleBackColor = false;
            this.btnCreateStock.Click += new System.EventHandler(this.btnAddStock_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.btnUploadToDb);
            this.panel1.Controls.Add(this.btnImportFromExcel);
            this.panel1.Controls.Add(this.btnExportToExcel);
            this.panel1.Controls.Add(this.btnEditStockProduct);
            this.panel1.Controls.Add(this.btnDeleteStock);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.btnReloadStocks);
            this.panel1.Controls.Add(this.btnCreateStock);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1100, 79);
            this.panel1.TabIndex = 1;
            // 
            // btnUploadToDb
            // 
            this.btnUploadToDb.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnUploadToDb.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnUploadToDb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnUploadToDb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadToDb.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnUploadToDb.Image = global::SalesOrdersReport.Properties.Resources.database_upload_32;
            this.btnUploadToDb.Location = new System.Drawing.Point(561, 3);
            this.btnUploadToDb.Name = "btnUploadToDb";
            this.btnUploadToDb.Size = new System.Drawing.Size(82, 73);
            this.btnUploadToDb.TabIndex = 5;
            this.btnUploadToDb.Text = "Update Stocks";
            this.btnUploadToDb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUploadToDb.UseVisualStyleBackColor = false;
            this.btnUploadToDb.Click += new System.EventHandler(this.btnUploadToDb_Click);
            // 
            // btnImportFromExcel
            // 
            this.btnImportFromExcel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnImportFromExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnImportFromExcel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnImportFromExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportFromExcel.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportFromExcel.Image = global::SalesOrdersReport.Properties.Resources.import_icon;
            this.btnImportFromExcel.Location = new System.Drawing.Point(382, 3);
            this.btnImportFromExcel.Name = "btnImportFromExcel";
            this.btnImportFromExcel.Size = new System.Drawing.Size(83, 73);
            this.btnImportFromExcel.TabIndex = 15;
            this.btnImportFromExcel.Text = "Import From Excel";
            this.btnImportFromExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImportFromExcel.UseVisualStyleBackColor = false;
            this.btnImportFromExcel.Click += new System.EventHandler(this.btnImportFromExcel_Click);
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExportToExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnExportToExcel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnExportToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportToExcel.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnExportToExcel.Image = global::SalesOrdersReport.Properties.Resources.export_icon;
            this.btnExportToExcel.Location = new System.Drawing.Point(472, 3);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(83, 73);
            this.btnExportToExcel.TabIndex = 14;
            this.btnExportToExcel.Text = "Export To Excel";
            this.btnExportToExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportToExcel.UseVisualStyleBackColor = false;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnEditStockProduct
            // 
            this.btnEditStockProduct.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnEditStockProduct.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnEditStockProduct.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnEditStockProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditStockProduct.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditStockProduct.Image = global::SalesOrdersReport.Properties.Resources.edit_icon_32;
            this.btnEditStockProduct.Location = new System.Drawing.Point(94, 3);
            this.btnEditStockProduct.Name = "btnEditStockProduct";
            this.btnEditStockProduct.Size = new System.Drawing.Size(83, 73);
            this.btnEditStockProduct.TabIndex = 13;
            this.btnEditStockProduct.Text = "Edit Stock Product";
            this.btnEditStockProduct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEditStockProduct.UseVisualStyleBackColor = false;
            this.btnEditStockProduct.Click += new System.EventHandler(this.btnEditStockProduct_Click);
            // 
            // btnDeleteStock
            // 
            this.btnDeleteStock.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDeleteStock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnDeleteStock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnDeleteStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteStock.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnDeleteStock.Image = global::SalesOrdersReport.Properties.Resources.delete_icon_32;
            this.btnDeleteStock.Location = new System.Drawing.Point(184, 3);
            this.btnDeleteStock.Name = "btnDeleteStock";
            this.btnDeleteStock.Size = new System.Drawing.Size(103, 73);
            this.btnDeleteStock.TabIndex = 12;
            this.btnDeleteStock.Text = "Delete Stock Product";
            this.btnDeleteStock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteStock.UseVisualStyleBackColor = false;
            this.btnDeleteStock.Click += new System.EventHandler(this.btnDeleteStock_Click);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.btnSearchProduct);
            this.panel4.Controls.Add(this.cmbBoxCategoryFilterList);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.btnClearSearchProduct);
            this.panel4.Controls.Add(this.txtBoxProductSearchString);
            this.panel4.Location = new System.Drawing.Point(681, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(416, 73);
            this.panel4.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label3.Location = new System.Drawing.Point(10, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Filter Category";
            // 
            // btnSearchProduct
            // 
            this.btnSearchProduct.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSearchProduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSearchProduct.FlatAppearance.BorderSize = 0;
            this.btnSearchProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchProduct.Image = global::SalesOrdersReport.Properties.Resources.Search_icon;
            this.btnSearchProduct.Location = new System.Drawing.Point(330, 36);
            this.btnSearchProduct.Name = "btnSearchProduct";
            this.btnSearchProduct.Size = new System.Drawing.Size(33, 29);
            this.btnSearchProduct.TabIndex = 6;
            this.btnSearchProduct.UseVisualStyleBackColor = false;
            this.btnSearchProduct.Click += new System.EventHandler(this.btnSearchProduct_Click);
            // 
            // cmbBoxCategoryFilterList
            // 
            this.cmbBoxCategoryFilterList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxCategoryFilterList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbBoxCategoryFilterList.FormattingEnabled = true;
            this.cmbBoxCategoryFilterList.Location = new System.Drawing.Point(90, 3);
            this.cmbBoxCategoryFilterList.Name = "cmbBoxCategoryFilterList";
            this.cmbBoxCategoryFilterList.Size = new System.Drawing.Size(234, 21);
            this.cmbBoxCategoryFilterList.TabIndex = 9;
            this.cmbBoxCategoryFilterList.SelectedIndexChanged += new System.EventHandler(this.cmbBoxCategoryFilterList_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label2.Location = new System.Drawing.Point(3, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Search Product";
            // 
            // btnClearSearchProduct
            // 
            this.btnClearSearchProduct.FlatAppearance.BorderSize = 0;
            this.btnClearSearchProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSearchProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearSearchProduct.Image = global::SalesOrdersReport.Properties.Resources.Trash_icon;
            this.btnClearSearchProduct.Location = new System.Drawing.Point(369, 35);
            this.btnClearSearchProduct.Name = "btnClearSearchProduct";
            this.btnClearSearchProduct.Size = new System.Drawing.Size(33, 29);
            this.btnClearSearchProduct.TabIndex = 7;
            this.btnClearSearchProduct.UseVisualStyleBackColor = true;
            this.btnClearSearchProduct.Click += new System.EventHandler(this.btnClearSearchProduct_Click);
            // 
            // txtBoxProductSearchString
            // 
            this.txtBoxProductSearchString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtBoxProductSearchString.Location = new System.Drawing.Point(90, 39);
            this.txtBoxProductSearchString.Name = "txtBoxProductSearchString";
            this.txtBoxProductSearchString.Size = new System.Drawing.Size(234, 20);
            this.txtBoxProductSearchString.TabIndex = 8;
            // 
            // btnReloadStocks
            // 
            this.btnReloadStocks.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnReloadStocks.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnReloadStocks.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnReloadStocks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReloadStocks.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnReloadStocks.Image = global::SalesOrdersReport.Properties.Resources.Refresh_icon_32;
            this.btnReloadStocks.Location = new System.Drawing.Point(293, 3);
            this.btnReloadStocks.Name = "btnReloadStocks";
            this.btnReloadStocks.Size = new System.Drawing.Size(83, 73);
            this.btnReloadStocks.TabIndex = 0;
            this.btnReloadStocks.Text = "Reload";
            this.btnReloadStocks.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReloadStocks.UseVisualStyleBackColor = false;
            this.btnReloadStocks.Click += new System.EventHandler(this.btnReloadStocks_Click);
            // 
            // dgvGridViewStocks
            // 
            this.dgvGridViewStocks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvGridViewStocks.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvGridViewStocks.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgvGridViewStocks.Location = new System.Drawing.Point(9, 121);
            this.dgvGridViewStocks.Name = "dgvGridViewStocks";
            this.dgvGridViewStocks.Size = new System.Drawing.Size(1100, 298);
            this.dgvGridViewStocks.TabIndex = 2;
            this.dgvGridViewStocks.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGridViewStocks_CellEndEdit);
            this.dgvGridViewStocks.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvGridViewStocks_CellMouseDoubleClick);
            this.dgvGridViewStocks.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGridViewStocks_CellValueChanged);
            this.dgvGridViewStocks.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvGridViewStocks_DataError);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(12, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Product Stock Inventory";
            // 
            // lblStocksCount
            // 
            this.lblStocksCount.AutoSize = true;
            this.lblStocksCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblStocksCount.Location = new System.Drawing.Point(237, 98);
            this.lblStocksCount.Name = "lblStocksCount";
            this.lblStocksCount.Size = new System.Drawing.Size(93, 20);
            this.lblStocksCount.TabIndex = 4;
            this.lblStocksCount.Text = "[Displaying ]";
            // 
            // backgroundWorkerOrders
            // 
            this.backgroundWorkerOrders.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerStocks_DoWork);
            this.backgroundWorkerOrders.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerStocks_ProgressChanged);
            this.backgroundWorkerOrders.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerStocks_RunWorkerCompleted);
            // 
            // ProductInventoryMainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1124, 611);
            this.Controls.Add(this.lblStocksCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvGridViewStocks);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProductInventoryMainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Product Inventory";
            this.Shown += new System.EventHandler(this.ProductInventoryMainForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGridViewStocks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateStock;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvGridViewStocks;
        private System.Windows.Forms.Button btnReloadStocks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearchProduct;
        private System.Windows.Forms.ComboBox cmbBoxCategoryFilterList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClearSearchProduct;
        private System.Windows.Forms.TextBox txtBoxProductSearchString;
        private System.Windows.Forms.Button btnDeleteStock;
        private System.Windows.Forms.Label lblStocksCount;
        private System.ComponentModel.BackgroundWorker backgroundWorkerOrders;
        private System.Windows.Forms.Button btnEditStockProduct;
        private System.Windows.Forms.Button btnImportFromExcel;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.Button btnUploadToDb;
    }
}