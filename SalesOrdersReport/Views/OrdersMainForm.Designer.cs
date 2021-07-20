namespace SalesOrdersReport.Views
{
    partial class OrdersMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrdersMainForm));
            this.btnCreateOrder = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbBoxLine = new System.Windows.Forms.ComboBox();
            this.cmbBoxOrderStatus = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxApplyFilter = new System.Windows.Forms.CheckBox();
            this.dTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.dTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.btnSearchOrder = new System.Windows.Forms.Button();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.btnPrintOrder = new System.Windows.Forms.Button();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.btnConvertInvoice = new System.Windows.Forms.Button();
            this.btnReloadOrders = new System.Windows.Forms.Button();
            this.btnDeleteOrder = new System.Windows.Forms.Button();
            this.btnViewEditOrder = new System.Windows.Forms.Button();
            this.dtGridViewOrders = new System.Windows.Forms.DataGridView();
            this.dtGridViewOrderedProducts = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblOrdersCount = new System.Windows.Forms.Label();
            this.backgroundWorkerOrders = new System.ComponentModel.BackgroundWorker();
            this.printDocumentOrders = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialogOrders = new System.Windows.Forms.PrintPreviewDialog();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewOrderedProducts)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateOrder
            // 
            this.btnCreateOrder.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCreateOrder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnCreateOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCreateOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateOrder.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateOrder.Image = global::SalesOrdersReport.Properties.Resources.math_add_icon_32;
            this.btnCreateOrder.Location = new System.Drawing.Point(5, 3);
            this.btnCreateOrder.Name = "btnCreateOrder";
            this.btnCreateOrder.Size = new System.Drawing.Size(83, 73);
            this.btnCreateOrder.TabIndex = 0;
            this.btnCreateOrder.Text = "Create Order";
            this.btnCreateOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCreateOrder.UseVisualStyleBackColor = false;
            this.btnCreateOrder.Click += new System.EventHandler(this.btnCreateOrder_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Controls.Add(this.btnSearchOrder);
            this.panel1.Controls.Add(this.btnPrintPreview);
            this.panel1.Controls.Add(this.btnPrintOrder);
            this.panel1.Controls.Add(this.btnExportToExcel);
            this.panel1.Controls.Add(this.btnConvertInvoice);
            this.panel1.Controls.Add(this.btnReloadOrders);
            this.panel1.Controls.Add(this.btnDeleteOrder);
            this.panel1.Controls.Add(this.btnViewEditOrder);
            this.panel1.Controls.Add(this.btnCreateOrder);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1371, 84);
            this.panel1.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.cmbBoxLine);
            this.groupBox3.Controls.Add(this.cmbBoxOrderStatus);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.checkBoxApplyFilter);
            this.groupBox3.Controls.Add(this.dTimePickerTo);
            this.groupBox3.Controls.Add(this.dTimePickerFrom);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(829, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(505, 73);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // cmbBoxLine
            // 
            this.cmbBoxLine.FormattingEnabled = true;
            this.cmbBoxLine.Location = new System.Drawing.Point(79, 40);
            this.cmbBoxLine.Name = "cmbBoxLine";
            this.cmbBoxLine.Size = new System.Drawing.Size(101, 21);
            this.cmbBoxLine.TabIndex = 4;
            this.cmbBoxLine.SelectedIndexChanged += new System.EventHandler(this.cmbBoxLine_SelectedIndexChanged);
            // 
            // cmbBoxOrderStatus
            // 
            this.cmbBoxOrderStatus.FormattingEnabled = true;
            this.cmbBoxOrderStatus.Location = new System.Drawing.Point(79, 13);
            this.cmbBoxOrderStatus.Name = "cmbBoxOrderStatus";
            this.cmbBoxOrderStatus.Size = new System.Drawing.Size(101, 21);
            this.cmbBoxOrderStatus.TabIndex = 4;
            this.cmbBoxOrderStatus.SelectedIndexChanged += new System.EventHandler(this.cmbBoxOrderStatus_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Line";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Order Status";
            // 
            // checkBoxApplyFilter
            // 
            this.checkBoxApplyFilter.AutoSize = true;
            this.checkBoxApplyFilter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.checkBoxApplyFilter.Location = new System.Drawing.Point(424, 28);
            this.checkBoxApplyFilter.Name = "checkBoxApplyFilter";
            this.checkBoxApplyFilter.Size = new System.Drawing.Size(75, 17);
            this.checkBoxApplyFilter.TabIndex = 2;
            this.checkBoxApplyFilter.Text = "Apply Filter";
            this.checkBoxApplyFilter.UseVisualStyleBackColor = true;
            this.checkBoxApplyFilter.CheckedChanged += new System.EventHandler(this.checkBoxApplyFilter_CheckedChanged);
            // 
            // dTimePickerTo
            // 
            this.dTimePickerTo.Location = new System.Drawing.Point(281, 38);
            this.dTimePickerTo.Name = "dTimePickerTo";
            this.dTimePickerTo.Size = new System.Drawing.Size(134, 20);
            this.dTimePickerTo.TabIndex = 1;
            // 
            // dTimePickerFrom
            // 
            this.dTimePickerFrom.Location = new System.Drawing.Point(281, 10);
            this.dTimePickerFrom.Name = "dTimePickerFrom";
            this.dTimePickerFrom.Size = new System.Drawing.Size(134, 20);
            this.dTimePickerFrom.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(201, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Order Date to:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(193, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Order Date from:";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(803, -24);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(234, 21);
            this.comboBox2.TabIndex = 9;
            // 
            // btnSearchOrder
            // 
            this.btnSearchOrder.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSearchOrder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnSearchOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSearchOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchOrder.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnSearchOrder.Image = global::SalesOrdersReport.Properties.Resources.Search_icon;
            this.btnSearchOrder.Location = new System.Drawing.Point(523, 3);
            this.btnSearchOrder.Name = "btnSearchOrder";
            this.btnSearchOrder.Size = new System.Drawing.Size(58, 73);
            this.btnSearchOrder.TabIndex = 0;
            this.btnSearchOrder.Text = "Search Orders";
            this.btnSearchOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSearchOrder.UseVisualStyleBackColor = false;
            this.btnSearchOrder.Click += new System.EventHandler(this.btnSearchOrder_Click);
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPrintPreview.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnPrintPreview.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPrintPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintPreview.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnPrintPreview.Image = global::SalesOrdersReport.Properties.Resources.Iconshow_Hardware_Printer;
            this.btnPrintPreview.Location = new System.Drawing.Point(650, 3);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(63, 73);
            this.btnPrintPreview.TabIndex = 0;
            this.btnPrintPreview.Text = "Preview";
            this.btnPrintPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrintPreview.UseVisualStyleBackColor = false;
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // btnPrintOrder
            // 
            this.btnPrintOrder.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPrintOrder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnPrintOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPrintOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintOrder.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnPrintOrder.Image = global::SalesOrdersReport.Properties.Resources.Iconshow_Hardware_Printer;
            this.btnPrintOrder.Location = new System.Drawing.Point(587, 3);
            this.btnPrintOrder.Name = "btnPrintOrder";
            this.btnPrintOrder.Size = new System.Drawing.Size(57, 73);
            this.btnPrintOrder.TabIndex = 0;
            this.btnPrintOrder.Text = "Print Orders";
            this.btnPrintOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrintOrder.UseVisualStyleBackColor = false;
            this.btnPrintOrder.Click += new System.EventHandler(this.btnPrintOrder_Click);
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExportToExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnExportToExcel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnExportToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportToExcel.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnExportToExcel.Image = global::SalesOrdersReport.Properties.Resources.export_icon;
            this.btnExportToExcel.Location = new System.Drawing.Point(450, 3);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(67, 73);
            this.btnExportToExcel.TabIndex = 0;
            this.btnExportToExcel.Text = "Export to Excel";
            this.btnExportToExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportToExcel.UseVisualStyleBackColor = false;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnConvertInvoice
            // 
            this.btnConvertInvoice.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnConvertInvoice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnConvertInvoice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnConvertInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConvertInvoice.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnConvertInvoice.Image = global::SalesOrdersReport.Properties.Resources.import_icon;
            this.btnConvertInvoice.Location = new System.Drawing.Point(361, 3);
            this.btnConvertInvoice.Name = "btnConvertInvoice";
            this.btnConvertInvoice.Size = new System.Drawing.Size(83, 73);
            this.btnConvertInvoice.TabIndex = 0;
            this.btnConvertInvoice.Text = "Convert to Invoice";
            this.btnConvertInvoice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConvertInvoice.UseVisualStyleBackColor = false;
            this.btnConvertInvoice.Click += new System.EventHandler(this.btnConvertInvoice_Click);
            // 
            // btnReloadOrders
            // 
            this.btnReloadOrders.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnReloadOrders.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnReloadOrders.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnReloadOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReloadOrders.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnReloadOrders.Image = global::SalesOrdersReport.Properties.Resources.Refresh_icon_32;
            this.btnReloadOrders.Location = new System.Drawing.Point(272, 3);
            this.btnReloadOrders.Name = "btnReloadOrders";
            this.btnReloadOrders.Size = new System.Drawing.Size(83, 73);
            this.btnReloadOrders.TabIndex = 0;
            this.btnReloadOrders.Text = "Reload Orders";
            this.btnReloadOrders.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReloadOrders.UseVisualStyleBackColor = false;
            this.btnReloadOrders.Click += new System.EventHandler(this.btnReloadOrders_Click);
            // 
            // btnDeleteOrder
            // 
            this.btnDeleteOrder.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDeleteOrder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnDeleteOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnDeleteOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteOrder.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnDeleteOrder.Image = global::SalesOrdersReport.Properties.Resources.delete_icon_32;
            this.btnDeleteOrder.Location = new System.Drawing.Point(183, 3);
            this.btnDeleteOrder.Name = "btnDeleteOrder";
            this.btnDeleteOrder.Size = new System.Drawing.Size(83, 73);
            this.btnDeleteOrder.TabIndex = 0;
            this.btnDeleteOrder.Text = "Cancel Order";
            this.btnDeleteOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteOrder.UseVisualStyleBackColor = false;
            this.btnDeleteOrder.Click += new System.EventHandler(this.btnDeleteOrder_Click);
            // 
            // btnViewEditOrder
            // 
            this.btnViewEditOrder.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnViewEditOrder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnViewEditOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnViewEditOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewEditOrder.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnViewEditOrder.Image = global::SalesOrdersReport.Properties.Resources.edit_icon_32;
            this.btnViewEditOrder.Location = new System.Drawing.Point(94, 3);
            this.btnViewEditOrder.Name = "btnViewEditOrder";
            this.btnViewEditOrder.Size = new System.Drawing.Size(83, 73);
            this.btnViewEditOrder.TabIndex = 0;
            this.btnViewEditOrder.Text = "View/Edit Order";
            this.btnViewEditOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnViewEditOrder.UseVisualStyleBackColor = false;
            this.btnViewEditOrder.Click += new System.EventHandler(this.btnViewEditOrder_Click);
            // 
            // dtGridViewOrders
            // 
            this.dtGridViewOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGridViewOrders.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtGridViewOrders.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dtGridViewOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewOrders.Location = new System.Drawing.Point(12, 127);
            this.dtGridViewOrders.Name = "dtGridViewOrders";
            this.dtGridViewOrders.Size = new System.Drawing.Size(1330, 342);
            this.dtGridViewOrders.TabIndex = 2;
            this.dtGridViewOrders.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtGridViewOrders_CellMouseClick);
            // 
            // dtGridViewOrderedProducts
            // 
            this.dtGridViewOrderedProducts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGridViewOrderedProducts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtGridViewOrderedProducts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dtGridViewOrderedProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewOrderedProducts.Location = new System.Drawing.Point(0, 36);
            this.dtGridViewOrderedProducts.Name = "dtGridViewOrderedProducts";
            this.dtGridViewOrderedProducts.Size = new System.Drawing.Size(1330, 214);
            this.dtGridViewOrderedProducts.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(12, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Orders";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dtGridViewOrderedProducts);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 475);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1330, 256);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 20);
            this.label6.TabIndex = 4;
            this.label6.Text = "Ordered Products";
            // 
            // lblOrdersCount
            // 
            this.lblOrdersCount.AutoSize = true;
            this.lblOrdersCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrdersCount.Location = new System.Drawing.Point(91, 102);
            this.lblOrdersCount.Name = "lblOrdersCount";
            this.lblOrdersCount.Size = new System.Drawing.Size(160, 20);
            this.lblOrdersCount.TabIndex = 4;
            this.lblOrdersCount.Text = "[Displaying all Orders]";
            // 
            // backgroundWorkerOrders
            // 
            this.backgroundWorkerOrders.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerOrders_DoWork);
            this.backgroundWorkerOrders.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerOrders_ProgressChanged);
            this.backgroundWorkerOrders.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerOrders_RunWorkerCompleted);
            // 
            // printDocumentOrders
            // 
            this.printDocumentOrders.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocumentOrders_PrintPage);
            // 
            // printPreviewDialogOrders
            // 
            this.printPreviewDialogOrders.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialogOrders.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialogOrders.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialogOrders.Enabled = true;
            this.printPreviewDialogOrders.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialogOrders.Icon")));
            this.printPreviewDialogOrders.Name = "printPreviewDialogOrders";
            this.printPreviewDialogOrders.Visible = false;
            // 
            // OrdersMainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1350, 743);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblOrdersCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtGridViewOrders);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrdersMainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Orders";
            this.Shown += new System.EventHandler(this.OrdersMainForm_Shown);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewOrderedProducts)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateOrder;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnViewEditOrder;
        private System.Windows.Forms.DataGridView dtGridViewOrders;
        private System.Windows.Forms.Button btnDeleteOrder;
        private System.Windows.Forms.Button btnReloadOrders;
        private System.Windows.Forms.DataGridView dtGridViewOrderedProducts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPrintOrder;
        private System.Windows.Forms.Button btnConvertInvoice;
        private System.Windows.Forms.Button btnSearchOrder;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxApplyFilter;
        private System.Windows.Forms.DateTimePicker dTimePickerTo;
        private System.Windows.Forms.DateTimePicker dTimePickerFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOrdersCount;
        private System.Windows.Forms.ComboBox cmbBoxOrderStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbBoxLine;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.Label label6;
        private System.ComponentModel.BackgroundWorker backgroundWorkerOrders;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Drawing.Printing.PrintDocument printDocumentOrders;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialogOrders;
    }
}