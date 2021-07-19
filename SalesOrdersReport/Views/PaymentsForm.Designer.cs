namespace SalesOrdersReport.Views
{
    partial class PaymentsForm
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
            this.btnCreatePayment = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.btnImportFromExcel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxApplyFilterPayment = new System.Windows.Forms.CheckBox();
            this.dTimePickerToPayments = new System.Windows.Forms.DateTimePicker();
            this.dTimePickerFromPayments = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.btnSearchPayment = new System.Windows.Forms.Button();
            this.btnPrintPayment = new System.Windows.Forms.Button();
            this.btnReloadPayments = new System.Windows.Forms.Button();
            this.btnDeletePayment = new System.Windows.Forms.Button();
            this.btnViewEditPayment = new System.Windows.Forms.Button();
            this.dtGridViewPayments = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvPaymentSummary = new System.Windows.Forms.DataGridView();
            this.cmbxDeliveryLine = new System.Windows.Forms.ComboBox();
            this.lblSelectDeliveryLine = new System.Windows.Forms.Label();
            this.cmbxStaffName = new System.Windows.Forms.ComboBox();
            this.lblStaffName = new System.Windows.Forms.Label();
            this.btnAddToDB = new System.Windows.Forms.Button();
            this.lblPaymentSummary = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPaymentSummaryExportToExcel = new System.Windows.Forms.Button();
            this.toolTipForEPaymentSummaryxportToExcel = new System.Windows.Forms.ToolTip(this.components);
            this.btnSaveSummaryDB = new System.Windows.Forms.Button();
            this.dtGridViewPaymentsSummaryTotal = new System.Windows.Forms.DataGridView();
            this.btnAddPaymentSummaryRow = new System.Windows.Forms.Button();
            this.backgroundWorkerPayments = new System.ComponentModel.BackgroundWorker();
            this.chkListBoxDeliveryLines = new System.Windows.Forms.CheckedListBox();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewPayments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymentSummary)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewPaymentsSummaryTotal)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCreatePayment
            // 
            this.btnCreatePayment.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCreatePayment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnCreatePayment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCreatePayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreatePayment.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreatePayment.Image = global::SalesOrdersReport.Properties.Resources.math_add_icon_32;
            this.btnCreatePayment.Location = new System.Drawing.Point(5, 3);
            this.btnCreatePayment.Name = "btnCreatePayment";
            this.btnCreatePayment.Size = new System.Drawing.Size(83, 73);
            this.btnCreatePayment.TabIndex = 0;
            this.btnCreatePayment.Text = "Create Payment";
            this.btnCreatePayment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCreatePayment.UseVisualStyleBackColor = false;
            this.btnCreatePayment.Click += new System.EventHandler(this.btnCreatePayment_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.btnExportToExcel);
            this.panel1.Controls.Add(this.btnImportFromExcel);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Controls.Add(this.btnSearchPayment);
            this.panel1.Controls.Add(this.btnPrintPayment);
            this.panel1.Controls.Add(this.btnReloadPayments);
            this.panel1.Controls.Add(this.btnDeletePayment);
            this.panel1.Controls.Add(this.btnViewEditPayment);
            this.panel1.Controls.Add(this.btnCreatePayment);
            this.panel1.Location = new System.Drawing.Point(12, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1181, 84);
            this.panel1.TabIndex = 1;
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExportToExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnExportToExcel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnExportToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportToExcel.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnExportToExcel.Image = global::SalesOrdersReport.Properties.Resources.export_icon;
            this.btnExportToExcel.Location = new System.Drawing.Point(628, 3);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(83, 73);
            this.btnExportToExcel.TabIndex = 13;
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
            this.btnImportFromExcel.Location = new System.Drawing.Point(539, 3);
            this.btnImportFromExcel.Name = "btnImportFromExcel";
            this.btnImportFromExcel.Size = new System.Drawing.Size(83, 73);
            this.btnImportFromExcel.TabIndex = 12;
            this.btnImportFromExcel.Text = "Import from Excel";
            this.btnImportFromExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImportFromExcel.UseVisualStyleBackColor = false;
            this.btnImportFromExcel.Click += new System.EventHandler(this.btnImportFromExcel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.checkBoxApplyFilterPayment);
            this.groupBox3.Controls.Add(this.dTimePickerToPayments);
            this.groupBox3.Controls.Add(this.dTimePickerFromPayments);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(858, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(298, 73);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // checkBoxApplyFilterPayment
            // 
            this.checkBoxApplyFilterPayment.AutoSize = true;
            this.checkBoxApplyFilterPayment.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.checkBoxApplyFilterPayment.Location = new System.Drawing.Point(211, 28);
            this.checkBoxApplyFilterPayment.Name = "checkBoxApplyFilterPayment";
            this.checkBoxApplyFilterPayment.Size = new System.Drawing.Size(75, 17);
            this.checkBoxApplyFilterPayment.TabIndex = 2;
            this.checkBoxApplyFilterPayment.Text = "Apply Filter";
            this.checkBoxApplyFilterPayment.UseVisualStyleBackColor = true;
            this.checkBoxApplyFilterPayment.CheckedChanged += new System.EventHandler(this.checkBoxApplyFilterPayments_CheckedChanged);
            // 
            // dTimePickerToPayments
            // 
            this.dTimePickerToPayments.Location = new System.Drawing.Point(67, 38);
            this.dTimePickerToPayments.Name = "dTimePickerToPayments";
            this.dTimePickerToPayments.Size = new System.Drawing.Size(134, 20);
            this.dTimePickerToPayments.TabIndex = 1;
            // 
            // dTimePickerFromPayments
            // 
            this.dTimePickerFromPayments.Location = new System.Drawing.Point(67, 10);
            this.dTimePickerFromPayments.Name = "dTimePickerFromPayments";
            this.dTimePickerFromPayments.Size = new System.Drawing.Size(134, 20);
            this.dTimePickerFromPayments.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Date to:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Date from:";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(803, -24);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(234, 21);
            this.comboBox2.TabIndex = 9;
            // 
            // btnSearchPayment
            // 
            this.btnSearchPayment.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSearchPayment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnSearchPayment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSearchPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchPayment.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnSearchPayment.Image = global::SalesOrdersReport.Properties.Resources.Search_icon;
            this.btnSearchPayment.Location = new System.Drawing.Point(450, 3);
            this.btnSearchPayment.Name = "btnSearchPayment";
            this.btnSearchPayment.Size = new System.Drawing.Size(83, 73);
            this.btnSearchPayment.TabIndex = 0;
            this.btnSearchPayment.Text = "Search Payment";
            this.btnSearchPayment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSearchPayment.UseVisualStyleBackColor = false;
            this.btnSearchPayment.Click += new System.EventHandler(this.btnSearchPayment_Click);
            // 
            // btnPrintPayment
            // 
            this.btnPrintPayment.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPrintPayment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnPrintPayment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPrintPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintPayment.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnPrintPayment.Image = global::SalesOrdersReport.Properties.Resources.Iconshow_Hardware_Printer;
            this.btnPrintPayment.Location = new System.Drawing.Point(361, 3);
            this.btnPrintPayment.Name = "btnPrintPayment";
            this.btnPrintPayment.Size = new System.Drawing.Size(83, 73);
            this.btnPrintPayment.TabIndex = 0;
            this.btnPrintPayment.Text = "Print";
            this.btnPrintPayment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrintPayment.UseVisualStyleBackColor = false;
            this.btnPrintPayment.Click += new System.EventHandler(this.btnPrintPayment_Click);
            // 
            // btnReloadPayments
            // 
            this.btnReloadPayments.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnReloadPayments.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnReloadPayments.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnReloadPayments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReloadPayments.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnReloadPayments.Image = global::SalesOrdersReport.Properties.Resources.Refresh_icon_32;
            this.btnReloadPayments.Location = new System.Drawing.Point(272, 3);
            this.btnReloadPayments.Name = "btnReloadPayments";
            this.btnReloadPayments.Size = new System.Drawing.Size(83, 73);
            this.btnReloadPayments.TabIndex = 0;
            this.btnReloadPayments.Text = "Reload";
            this.btnReloadPayments.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReloadPayments.UseVisualStyleBackColor = false;
            this.btnReloadPayments.Click += new System.EventHandler(this.btnReloadPayments_Click);
            // 
            // btnDeletePayment
            // 
            this.btnDeletePayment.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDeletePayment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnDeletePayment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnDeletePayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeletePayment.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnDeletePayment.Image = global::SalesOrdersReport.Properties.Resources.delete_icon_32;
            this.btnDeletePayment.Location = new System.Drawing.Point(183, 3);
            this.btnDeletePayment.Name = "btnDeletePayment";
            this.btnDeletePayment.Size = new System.Drawing.Size(83, 73);
            this.btnDeletePayment.TabIndex = 0;
            this.btnDeletePayment.Text = "Delete Payment";
            this.btnDeletePayment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeletePayment.UseVisualStyleBackColor = false;
            this.btnDeletePayment.Click += new System.EventHandler(this.btnDeletePayment_Click);
            // 
            // btnViewEditPayment
            // 
            this.btnViewEditPayment.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnViewEditPayment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnViewEditPayment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnViewEditPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewEditPayment.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnViewEditPayment.Image = global::SalesOrdersReport.Properties.Resources.edit_icon_32;
            this.btnViewEditPayment.Location = new System.Drawing.Point(94, 3);
            this.btnViewEditPayment.Name = "btnViewEditPayment";
            this.btnViewEditPayment.Size = new System.Drawing.Size(83, 73);
            this.btnViewEditPayment.TabIndex = 0;
            this.btnViewEditPayment.Text = "View/Edit Payment";
            this.btnViewEditPayment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnViewEditPayment.UseVisualStyleBackColor = false;
            this.btnViewEditPayment.Click += new System.EventHandler(this.btnViewEditPayment_Click);
            // 
            // dtGridViewPayments
            // 
            this.dtGridViewPayments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGridViewPayments.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtGridViewPayments.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dtGridViewPayments.Location = new System.Drawing.Point(12, 127);
            this.dtGridViewPayments.MultiSelect = false;
            this.dtGridViewPayments.Name = "dtGridViewPayments";
            this.dtGridViewPayments.Size = new System.Drawing.Size(1164, 228);
            this.dtGridViewPayments.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Payments";
            // 
            // dgvPaymentSummary
            // 
            this.dgvPaymentSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPaymentSummary.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvPaymentSummary.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgvPaymentSummary.Location = new System.Drawing.Point(12, 423);
            this.dgvPaymentSummary.MultiSelect = false;
            this.dgvPaymentSummary.Name = "dgvPaymentSummary";
            this.dgvPaymentSummary.Size = new System.Drawing.Size(1164, 235);
            this.dgvPaymentSummary.TabIndex = 5;
            this.dgvPaymentSummary.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPaymentSummary_CellEndEdit);
            this.dgvPaymentSummary.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPaymentSummary_CellValueChanged);
            this.dgvPaymentSummary.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvPaymentSummary_DataError);
            this.dgvPaymentSummary.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvPaymentSummary_Scroll);
            // 
            // cmbxDeliveryLine
            // 
            this.cmbxDeliveryLine.FormattingEnabled = true;
            this.cmbxDeliveryLine.Location = new System.Drawing.Point(937, 391);
            this.cmbxDeliveryLine.Name = "cmbxDeliveryLine";
            this.cmbxDeliveryLine.Size = new System.Drawing.Size(121, 21);
            this.cmbxDeliveryLine.TabIndex = 6;
            this.cmbxDeliveryLine.Visible = false;
            // 
            // lblSelectDeliveryLine
            // 
            this.lblSelectDeliveryLine.AutoSize = true;
            this.lblSelectDeliveryLine.Location = new System.Drawing.Point(299, 384);
            this.lblSelectDeliveryLine.Name = "lblSelectDeliveryLine";
            this.lblSelectDeliveryLine.Size = new System.Drawing.Size(68, 13);
            this.lblSelectDeliveryLine.TabIndex = 7;
            this.lblSelectDeliveryLine.Text = "Delivery Line";
            // 
            // cmbxStaffName
            // 
            this.cmbxStaffName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbxStaffName.FormattingEnabled = true;
            this.cmbxStaffName.Location = new System.Drawing.Point(921, 13);
            this.cmbxStaffName.Name = "cmbxStaffName";
            this.cmbxStaffName.Size = new System.Drawing.Size(121, 21);
            this.cmbxStaffName.TabIndex = 8;
            // 
            // lblStaffName
            // 
            this.lblStaffName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStaffName.AutoSize = true;
            this.lblStaffName.Location = new System.Drawing.Point(803, 16);
            this.lblStaffName.Name = "lblStaffName";
            this.lblStaffName.Size = new System.Drawing.Size(112, 13);
            this.lblStaffName.TabIndex = 9;
            this.lblStaffName.Text = "Payment Recived By :";
            // 
            // btnAddToDB
            // 
            this.btnAddToDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddToDB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAddToDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddToDB.Image = global::SalesOrdersReport.Properties.Resources.math_add_icon_24;
            this.btnAddToDB.Location = new System.Drawing.Point(1048, 5);
            this.btnAddToDB.Name = "btnAddToDB";
            this.btnAddToDB.Size = new System.Drawing.Size(108, 34);
            this.btnAddToDB.TabIndex = 10;
            this.btnAddToDB.Text = "Add Payments";
            this.btnAddToDB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddToDB.UseVisualStyleBackColor = false;
            this.btnAddToDB.Click += new System.EventHandler(this.btnAddToDB_Click);
            // 
            // lblPaymentSummary
            // 
            this.lblPaymentSummary.AutoSize = true;
            this.lblPaymentSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblPaymentSummary.Location = new System.Drawing.Point(12, 385);
            this.lblPaymentSummary.Name = "lblPaymentSummary";
            this.lblPaymentSummary.Size = new System.Drawing.Size(188, 25);
            this.lblPaymentSummary.TabIndex = 12;
            this.lblPaymentSummary.Text = "Payments Summary";
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.btnAddToDB);
            this.panel2.Controls.Add(this.lblStaffName);
            this.panel2.Controls.Add(this.cmbxStaffName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 703);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1184, 42);
            this.panel2.TabIndex = 11;
            // 
            // btnPaymentSummaryExportToExcel
            // 
            this.btnPaymentSummaryExportToExcel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPaymentSummaryExportToExcel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnPaymentSummaryExportToExcel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPaymentSummaryExportToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPaymentSummaryExportToExcel.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnPaymentSummaryExportToExcel.Image = global::SalesOrdersReport.Properties.Resources.exporttoexcel321;
            this.btnPaymentSummaryExportToExcel.Location = new System.Drawing.Point(689, 384);
            this.btnPaymentSummaryExportToExcel.Name = "btnPaymentSummaryExportToExcel";
            this.btnPaymentSummaryExportToExcel.Size = new System.Drawing.Size(43, 32);
            this.btnPaymentSummaryExportToExcel.TabIndex = 14;
            this.btnPaymentSummaryExportToExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPaymentSummaryExportToExcel.UseVisualStyleBackColor = false;
            this.btnPaymentSummaryExportToExcel.Click += new System.EventHandler(this.btnPaymentSummaryExportToExcel_Click);
            // 
            // btnSaveSummaryDB
            // 
            this.btnSaveSummaryDB.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSaveSummaryDB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnSaveSummaryDB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSaveSummaryDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveSummaryDB.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnSaveSummaryDB.Image = global::SalesOrdersReport.Properties.Resources.save_32;
            this.btnSaveSummaryDB.Location = new System.Drawing.Point(640, 384);
            this.btnSaveSummaryDB.Name = "btnSaveSummaryDB";
            this.btnSaveSummaryDB.Size = new System.Drawing.Size(43, 32);
            this.btnSaveSummaryDB.TabIndex = 15;
            this.btnSaveSummaryDB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveSummaryDB.UseCompatibleTextRendering = true;
            this.btnSaveSummaryDB.UseVisualStyleBackColor = false;
            this.btnSaveSummaryDB.Click += new System.EventHandler(this.btnSaveSummaryDB_Click);
            // 
            // dtGridViewPaymentsSummaryTotal
            // 
            this.dtGridViewPaymentsSummaryTotal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewPaymentsSummaryTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dtGridViewPaymentsSummaryTotal.Location = new System.Drawing.Point(0, 664);
            this.dtGridViewPaymentsSummaryTotal.Name = "dtGridViewPaymentsSummaryTotal";
            this.dtGridViewPaymentsSummaryTotal.Size = new System.Drawing.Size(1184, 39);
            this.dtGridViewPaymentsSummaryTotal.TabIndex = 16;
            // 
            // btnAddPaymentSummaryRow
            // 
            this.btnAddPaymentSummaryRow.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAddPaymentSummaryRow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnAddPaymentSummaryRow.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAddPaymentSummaryRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPaymentSummaryRow.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnAddPaymentSummaryRow.Image = global::SalesOrdersReport.Properties.Resources.math_add_icon_32;
            this.btnAddPaymentSummaryRow.Location = new System.Drawing.Point(591, 384);
            this.btnAddPaymentSummaryRow.Name = "btnAddPaymentSummaryRow";
            this.btnAddPaymentSummaryRow.Size = new System.Drawing.Size(43, 32);
            this.btnAddPaymentSummaryRow.TabIndex = 17;
            this.btnAddPaymentSummaryRow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddPaymentSummaryRow.UseCompatibleTextRendering = true;
            this.btnAddPaymentSummaryRow.UseVisualStyleBackColor = false;
            this.btnAddPaymentSummaryRow.Click += new System.EventHandler(this.btnAddPaymentSummaryRow_Click);
            // 
            // backgroundWorkerPayments
            // 
            this.backgroundWorkerPayments.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerPayments_DoWork);
            this.backgroundWorkerPayments.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerPayments_ProgressChanged);
            this.backgroundWorkerPayments.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerPayments_RunWorkerCompleted);
            // 
            // chkListBoxDeliveryLines
            // 
            this.chkListBoxDeliveryLines.CheckOnClick = true;
            this.chkListBoxDeliveryLines.FormattingEnabled = true;
            this.chkListBoxDeliveryLines.Location = new System.Drawing.Point(373, 368);
            this.chkListBoxDeliveryLines.Name = "chkListBoxDeliveryLines";
            this.chkListBoxDeliveryLines.Size = new System.Drawing.Size(204, 49);
            this.chkListBoxDeliveryLines.TabIndex = 18;
            this.chkListBoxDeliveryLines.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkListBoxDeliveryLines_ItemCheck);
            // 
            // PaymentsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1184, 745);
            this.Controls.Add(this.chkListBoxDeliveryLines);
            this.Controls.Add(this.btnAddPaymentSummaryRow);
            this.Controls.Add(this.dtGridViewPaymentsSummaryTotal);
            this.Controls.Add(this.btnSaveSummaryDB);
            this.Controls.Add(this.btnPaymentSummaryExportToExcel);
            this.Controls.Add(this.lblPaymentSummary);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblSelectDeliveryLine);
            this.Controls.Add(this.cmbxDeliveryLine);
            this.Controls.Add(this.dgvPaymentSummary);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtGridViewPayments);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaymentsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Payments";
            this.Shown += new System.EventHandler(this.PaymentsMainForm_Shown);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewPayments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymentSummary)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewPaymentsSummaryTotal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreatePayment;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnViewEditPayment;
        private System.Windows.Forms.DataGridView dtGridViewPayments;
        private System.Windows.Forms.Button btnDeletePayment;
        private System.Windows.Forms.Button btnReloadPayments;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button btnPrintPayment;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxApplyFilterPayment;
        private System.Windows.Forms.DateTimePicker dTimePickerToPayments;
        private System.Windows.Forms.DateTimePicker dTimePickerFromPayments;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearchPayment;
        private System.Windows.Forms.Button btnImportFromExcel;
        private System.Windows.Forms.DataGridView dgvPaymentSummary;
        private System.Windows.Forms.ComboBox cmbxDeliveryLine;
        private System.Windows.Forms.Label lblSelectDeliveryLine;
        private System.Windows.Forms.ComboBox cmbxStaffName;
        private System.Windows.Forms.Label lblStaffName;
        private System.Windows.Forms.Button btnAddToDB;
        private System.Windows.Forms.Label lblPaymentSummary;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.Button btnPaymentSummaryExportToExcel;
        private System.Windows.Forms.ToolTip toolTipForEPaymentSummaryxportToExcel;
        private System.Windows.Forms.Button btnSaveSummaryDB;
        private System.Windows.Forms.DataGridView dtGridViewPaymentsSummaryTotal;
        private System.Windows.Forms.Button btnAddPaymentSummaryRow;
        private System.ComponentModel.BackgroundWorker backgroundWorkerPayments;
        private System.Windows.Forms.CheckedListBox chkListBoxDeliveryLines;
    }
}