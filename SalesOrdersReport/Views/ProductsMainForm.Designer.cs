namespace SalesOrdersReport.Views
{
    partial class ProductsMainForm
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
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.btnUploadToDb = new System.Windows.Forms.Button();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.btnImportFromExcel = new System.Windows.Forms.Button();
            this.btnShowHSNList = new System.Windows.Forms.Button();
            this.btnReloadProducts = new System.Windows.Forms.Button();
            this.btnDeleteProduct = new System.Windows.Forms.Button();
            this.btnEditProduct = new System.Windows.Forms.Button();
            this.cmbBoxCategoryFilterList = new System.Windows.Forms.ComboBox();
            this.txtBoxProductSearchString = new System.Windows.Forms.TextBox();
            this.btnClearSearchProduct = new System.Windows.Forms.Button();
            this.btnSearchProduct = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtGridViewProducts = new System.Windows.Forms.DataGridView();
            this.dtGridViewProductCategory = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDelProductCategory = new System.Windows.Forms.Button();
            this.btnEditroductCategory = new System.Windows.Forms.Button();
            this.btnAddProductCategory = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cntxtMenuProducts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editProductToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblProductCount = new System.Windows.Forms.Label();
            this.backgroundWorkerProducts = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewProductCategory)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.cntxtMenuProducts.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAddProduct.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnAddProduct.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAddProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddProduct.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddProduct.Image = global::SalesOrdersReport.Properties.Resources.math_add_icon_32;
            this.btnAddProduct.Location = new System.Drawing.Point(5, 3);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(83, 73);
            this.btnAddProduct.TabIndex = 0;
            this.btnAddProduct.Text = "Add Product";
            this.btnAddProduct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddProduct.UseVisualStyleBackColor = false;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Controls.Add(this.btnUploadToDb);
            this.panel1.Controls.Add(this.btnExportToExcel);
            this.panel1.Controls.Add(this.btnImportFromExcel);
            this.panel1.Controls.Add(this.btnShowHSNList);
            this.panel1.Controls.Add(this.btnReloadProducts);
            this.panel1.Controls.Add(this.btnDeleteProduct);
            this.panel1.Controls.Add(this.btnEditProduct);
            this.panel1.Controls.Add(this.btnAddProduct);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(713, 84);
            this.panel1.TabIndex = 1;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(803, -24);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(234, 21);
            this.comboBox2.TabIndex = 9;
            // 
            // btnUploadToDb
            // 
            this.btnUploadToDb.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnUploadToDb.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnUploadToDb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnUploadToDb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadToDb.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnUploadToDb.Image = global::SalesOrdersReport.Properties.Resources.database_upload_32;
            this.btnUploadToDb.Location = new System.Drawing.Point(628, 3);
            this.btnUploadToDb.Name = "btnUploadToDb";
            this.btnUploadToDb.Size = new System.Drawing.Size(82, 73);
            this.btnUploadToDb.TabIndex = 0;
            this.btnUploadToDb.Text = "Update Prices";
            this.btnUploadToDb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUploadToDb.UseVisualStyleBackColor = false;
            this.btnUploadToDb.Click += new System.EventHandler(this.btnUploadToDb_Click);
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExportToExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnExportToExcel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnExportToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportToExcel.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnExportToExcel.Image = global::SalesOrdersReport.Properties.Resources.export_icon;
            this.btnExportToExcel.Location = new System.Drawing.Point(539, 3);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(83, 73);
            this.btnExportToExcel.TabIndex = 0;
            this.btnExportToExcel.Text = "Export to Excel";
            this.btnExportToExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportToExcel.UseVisualStyleBackColor = false;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnImportFromExcel
            // 
            this.btnImportFromExcel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnImportFromExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnImportFromExcel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnImportFromExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportFromExcel.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnImportFromExcel.Image = global::SalesOrdersReport.Properties.Resources.import_icon;
            this.btnImportFromExcel.Location = new System.Drawing.Point(450, 3);
            this.btnImportFromExcel.Name = "btnImportFromExcel";
            this.btnImportFromExcel.Size = new System.Drawing.Size(83, 73);
            this.btnImportFromExcel.TabIndex = 0;
            this.btnImportFromExcel.Text = "Import from Excel";
            this.btnImportFromExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImportFromExcel.UseVisualStyleBackColor = false;
            this.btnImportFromExcel.Click += new System.EventHandler(this.btnImportFromExcel_Click);
            // 
            // btnShowHSNList
            // 
            this.btnShowHSNList.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnShowHSNList.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnShowHSNList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnShowHSNList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowHSNList.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnShowHSNList.Image = global::SalesOrdersReport.Properties.Resources.tax_menu;
            this.btnShowHSNList.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnShowHSNList.Location = new System.Drawing.Point(272, 3);
            this.btnShowHSNList.Name = "btnShowHSNList";
            this.btnShowHSNList.Size = new System.Drawing.Size(83, 73);
            this.btnShowHSNList.TabIndex = 0;
            this.btnShowHSNList.Text = "HSN Codes";
            this.btnShowHSNList.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnShowHSNList.UseVisualStyleBackColor = false;
            this.btnShowHSNList.Click += new System.EventHandler(this.btnShowHSNList_Click);
            // 
            // btnReloadProducts
            // 
            this.btnReloadProducts.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnReloadProducts.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnReloadProducts.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnReloadProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReloadProducts.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnReloadProducts.Image = global::SalesOrdersReport.Properties.Resources.Refresh_icon_32;
            this.btnReloadProducts.Location = new System.Drawing.Point(361, 3);
            this.btnReloadProducts.Name = "btnReloadProducts";
            this.btnReloadProducts.Size = new System.Drawing.Size(83, 73);
            this.btnReloadProducts.TabIndex = 0;
            this.btnReloadProducts.Text = "Reload";
            this.btnReloadProducts.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReloadProducts.UseVisualStyleBackColor = false;
            this.btnReloadProducts.Click += new System.EventHandler(this.btnReloadProducts_Click);
            // 
            // btnDeleteProduct
            // 
            this.btnDeleteProduct.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDeleteProduct.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnDeleteProduct.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnDeleteProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteProduct.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnDeleteProduct.Image = global::SalesOrdersReport.Properties.Resources.delete_icon_32;
            this.btnDeleteProduct.Location = new System.Drawing.Point(183, 3);
            this.btnDeleteProduct.Name = "btnDeleteProduct";
            this.btnDeleteProduct.Size = new System.Drawing.Size(83, 73);
            this.btnDeleteProduct.TabIndex = 0;
            this.btnDeleteProduct.Text = "Delete Product";
            this.btnDeleteProduct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteProduct.UseVisualStyleBackColor = false;
            this.btnDeleteProduct.Click += new System.EventHandler(this.btnDeleteProduct_Click);
            // 
            // btnEditProduct
            // 
            this.btnEditProduct.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnEditProduct.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnEditProduct.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnEditProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditProduct.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnEditProduct.Image = global::SalesOrdersReport.Properties.Resources.edit_icon_32;
            this.btnEditProduct.Location = new System.Drawing.Point(94, 3);
            this.btnEditProduct.Name = "btnEditProduct";
            this.btnEditProduct.Size = new System.Drawing.Size(83, 73);
            this.btnEditProduct.TabIndex = 0;
            this.btnEditProduct.Text = "Edit Product";
            this.btnEditProduct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEditProduct.UseVisualStyleBackColor = false;
            this.btnEditProduct.Click += new System.EventHandler(this.btnEditProduct_Click);
            // 
            // cmbBoxCategoryFilterList
            // 
            this.cmbBoxCategoryFilterList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxCategoryFilterList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbBoxCategoryFilterList.FormattingEnabled = true;
            this.cmbBoxCategoryFilterList.Location = new System.Drawing.Point(90, 18);
            this.cmbBoxCategoryFilterList.Name = "cmbBoxCategoryFilterList";
            this.cmbBoxCategoryFilterList.Size = new System.Drawing.Size(234, 21);
            this.cmbBoxCategoryFilterList.TabIndex = 9;
            this.cmbBoxCategoryFilterList.SelectedIndexChanged += new System.EventHandler(this.cmbBoxCategoryFilterList_SelectedIndexChanged);
            // 
            // txtBoxProductSearchString
            // 
            this.txtBoxProductSearchString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtBoxProductSearchString.Location = new System.Drawing.Point(90, 54);
            this.txtBoxProductSearchString.Name = "txtBoxProductSearchString";
            this.txtBoxProductSearchString.Size = new System.Drawing.Size(234, 20);
            this.txtBoxProductSearchString.TabIndex = 8;
            // 
            // btnClearSearchProduct
            // 
            this.btnClearSearchProduct.FlatAppearance.BorderSize = 0;
            this.btnClearSearchProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSearchProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearSearchProduct.Image = global::SalesOrdersReport.Properties.Resources.Trash_icon;
            this.btnClearSearchProduct.Location = new System.Drawing.Point(369, 50);
            this.btnClearSearchProduct.Name = "btnClearSearchProduct";
            this.btnClearSearchProduct.Size = new System.Drawing.Size(33, 29);
            this.btnClearSearchProduct.TabIndex = 7;
            this.btnClearSearchProduct.UseVisualStyleBackColor = true;
            this.btnClearSearchProduct.Click += new System.EventHandler(this.btnClearSearchProduct_Click);
            // 
            // btnSearchProduct
            // 
            this.btnSearchProduct.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSearchProduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSearchProduct.FlatAppearance.BorderSize = 0;
            this.btnSearchProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchProduct.Image = global::SalesOrdersReport.Properties.Resources.Search_icon;
            this.btnSearchProduct.Location = new System.Drawing.Point(330, 51);
            this.btnSearchProduct.Name = "btnSearchProduct";
            this.btnSearchProduct.Size = new System.Drawing.Size(33, 29);
            this.btnSearchProduct.TabIndex = 6;
            this.btnSearchProduct.UseVisualStyleBackColor = false;
            this.btnSearchProduct.Click += new System.EventHandler(this.btnSearchProduct_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label3.Location = new System.Drawing.Point(10, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Filter Category";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label2.Location = new System.Drawing.Point(3, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Search Product";
            // 
            // dtGridViewProducts
            // 
            this.dtGridViewProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGridViewProducts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtGridViewProducts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dtGridViewProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewProducts.Location = new System.Drawing.Point(12, 127);
            this.dtGridViewProducts.Name = "dtGridViewProducts";
            this.dtGridViewProducts.Size = new System.Drawing.Size(1164, 369);
            this.dtGridViewProducts.TabIndex = 2;
            this.dtGridViewProducts.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGridViewProducts_CellEndEdit);
            this.dtGridViewProducts.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtGridViewProducts_CellMouseDoubleClick);
            this.dtGridViewProducts.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGridViewProducts_CellValueChanged);
            this.dtGridViewProducts.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dtGridViewProducts_DataError);
            // 
            // dtGridViewProductCategory
            // 
            this.dtGridViewProductCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGridViewProductCategory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtGridViewProductCategory.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dtGridViewProductCategory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewProductCategory.Location = new System.Drawing.Point(6, 75);
            this.dtGridViewProductCategory.Name = "dtGridViewProductCategory";
            this.dtGridViewProductCategory.Size = new System.Drawing.Size(1152, 139);
            this.dtGridViewProductCategory.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(12, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Product List";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDelProductCategory);
            this.panel2.Controls.Add(this.btnEditroductCategory);
            this.panel2.Controls.Add(this.btnAddProductCategory);
            this.panel2.Location = new System.Drawing.Point(6, 22);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(438, 50);
            this.panel2.TabIndex = 1;
            // 
            // btnDelProductCategory
            // 
            this.btnDelProductCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDelProductCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelProductCategory.Image = global::SalesOrdersReport.Properties.Resources.delete_icon_24;
            this.btnDelProductCategory.Location = new System.Drawing.Point(287, 3);
            this.btnDelProductCategory.Name = "btnDelProductCategory";
            this.btnDelProductCategory.Size = new System.Drawing.Size(135, 44);
            this.btnDelProductCategory.TabIndex = 0;
            this.btnDelProductCategory.Text = "Delete Category";
            this.btnDelProductCategory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelProductCategory.UseVisualStyleBackColor = false;
            this.btnDelProductCategory.Click += new System.EventHandler(this.btnDelProductCategory_Click);
            // 
            // btnEditroductCategory
            // 
            this.btnEditroductCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEditroductCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditroductCategory.Image = global::SalesOrdersReport.Properties.Resources.edit_icon_24;
            this.btnEditroductCategory.Location = new System.Drawing.Point(146, 3);
            this.btnEditroductCategory.Name = "btnEditroductCategory";
            this.btnEditroductCategory.Size = new System.Drawing.Size(135, 44);
            this.btnEditroductCategory.TabIndex = 0;
            this.btnEditroductCategory.Text = "Edit Category";
            this.btnEditroductCategory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEditroductCategory.UseVisualStyleBackColor = false;
            this.btnEditroductCategory.Click += new System.EventHandler(this.btnEditProductCategory_Click);
            // 
            // btnAddProductCategory
            // 
            this.btnAddProductCategory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAddProductCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddProductCategory.Image = global::SalesOrdersReport.Properties.Resources.math_add_icon_24;
            this.btnAddProductCategory.Location = new System.Drawing.Point(5, 3);
            this.btnAddProductCategory.Name = "btnAddProductCategory";
            this.btnAddProductCategory.Size = new System.Drawing.Size(135, 44);
            this.btnAddProductCategory.TabIndex = 0;
            this.btnAddProductCategory.Text = "Add Category";
            this.btnAddProductCategory.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddProductCategory.UseVisualStyleBackColor = false;
            this.btnAddProductCategory.Click += new System.EventHandler(this.btnAddProductCategory_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.dtGridViewProductCategory);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox1.Location = new System.Drawing.Point(12, 511);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1164, 220);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Category List";
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
            this.panel4.Location = new System.Drawing.Point(770, 12);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(406, 84);
            this.panel4.TabIndex = 10;
            // 
            // cntxtMenuProducts
            // 
            this.cntxtMenuProducts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editProductToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.cntxtMenuProducts.Name = "cntxtMenuProducts";
            this.cntxtMenuProducts.Size = new System.Drawing.Size(125, 48);
            // 
            // editProductToolStripMenuItem
            // 
            this.editProductToolStripMenuItem.Name = "editProductToolStripMenuItem";
            this.editProductToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.editProductToolStripMenuItem.Text = "Edit/View";
            this.editProductToolStripMenuItem.Click += new System.EventHandler(this.editProductToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // lblProductCount
            // 
            this.lblProductCount.AutoSize = true;
            this.lblProductCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductCount.Location = new System.Drawing.Point(132, 101);
            this.lblProductCount.Name = "lblProductCount";
            this.lblProductCount.Size = new System.Drawing.Size(175, 20);
            this.lblProductCount.TabIndex = 4;
            this.lblProductCount.Text = "[Displaying all Products]";
            // 
            // backgroundWorkerProducts
            // 
            this.backgroundWorkerProducts.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerProducts_DoWork);
            this.backgroundWorkerProducts.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerProducts_ProgressChanged);
            this.backgroundWorkerProducts.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerProducts_RunWorkerCompleted);
            // 
            // ProductsMainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1184, 743);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblProductCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtGridViewProducts);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "ProductsMainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Products";
            this.Load += new System.EventHandler(this.ProductsMainForm_Load);
            this.Shown += new System.EventHandler(this.ProductsMainForm_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewProductCategory)).EndInit();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.cntxtMenuProducts.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEditProduct;
        private System.Windows.Forms.DataGridView dtGridViewProducts;
        private System.Windows.Forms.Button btnDeleteProduct;
        private System.Windows.Forms.Button btnReloadProducts;
        private System.Windows.Forms.DataGridView dtGridViewProductCategory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSearchProduct;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnDelProductCategory;
        private System.Windows.Forms.Button btnEditroductCategory;
        private System.Windows.Forms.Button btnAddProductCategory;
        private System.Windows.Forms.TextBox txtBoxProductSearchString;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.Button btnImportFromExcel;
        private System.Windows.Forms.Button btnClearSearchProduct;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox cmbBoxCategoryFilterList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ContextMenuStrip cntxtMenuProducts;
        private System.Windows.Forms.ToolStripMenuItem editProductToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Button btnShowHSNList;
        private System.Windows.Forms.Label lblProductCount;
        private System.Windows.Forms.Button btnUploadToDb;
        private System.ComponentModel.BackgroundWorker backgroundWorkerProducts;
    }
}