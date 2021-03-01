namespace SalesOrdersReport.Views
{
    partial class POSBillsMainForm
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
            this.btnCreateBill = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbBoxBillStatus = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxApplyFilter = new System.Windows.Forms.CheckBox();
            this.dTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.dTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.btnExportBill = new System.Windows.Forms.Button();
            this.btnCloseCounter = new System.Windows.Forms.Button();
            this.btnSearchBill = new System.Windows.Forms.Button();
            this.btnPrintBill = new System.Windows.Forms.Button();
            this.btnReloadBills = new System.Windows.Forms.Button();
            this.btnCancelBill = new System.Windows.Forms.Button();
            this.btnViewEditBill = new System.Windows.Forms.Button();
            this.dtGridViewBills = new System.Windows.Forms.DataGridView();
            this.dtGridViewBilledProducts = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblOrdersCount = new System.Windows.Forms.Label();
            this.backgroundWorkerBills = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewBills)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewBilledProducts)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateBill
            // 
            this.btnCreateBill.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCreateBill.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnCreateBill.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCreateBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateBill.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateBill.Image = global::SalesOrdersReport.Properties.Resources.math_add_icon_32;
            this.btnCreateBill.Location = new System.Drawing.Point(5, 3);
            this.btnCreateBill.Name = "btnCreateBill";
            this.btnCreateBill.Size = new System.Drawing.Size(57, 73);
            this.btnCreateBill.TabIndex = 0;
            this.btnCreateBill.Text = "Create Bill";
            this.btnCreateBill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCreateBill.UseVisualStyleBackColor = false;
            this.btnCreateBill.Click += new System.EventHandler(this.btnCreateBill_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Controls.Add(this.btnExportBill);
            this.panel1.Controls.Add(this.btnCloseCounter);
            this.panel1.Controls.Add(this.btnSearchBill);
            this.panel1.Controls.Add(this.btnPrintBill);
            this.panel1.Controls.Add(this.btnReloadBills);
            this.panel1.Controls.Add(this.btnCancelBill);
            this.panel1.Controls.Add(this.btnViewEditBill);
            this.panel1.Controls.Add(this.btnCreateBill);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1269, 84);
            this.panel1.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.cmbBoxBillStatus);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.checkBoxApplyFilter);
            this.groupBox3.Controls.Add(this.dTimePickerTo);
            this.groupBox3.Controls.Add(this.dTimePickerFrom);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(744, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(521, 73);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // cmbBoxBillStatus
            // 
            this.cmbBoxBillStatus.FormattingEnabled = true;
            this.cmbBoxBillStatus.Location = new System.Drawing.Point(109, 13);
            this.cmbBoxBillStatus.Name = "cmbBoxBillStatus";
            this.cmbBoxBillStatus.Size = new System.Drawing.Size(87, 21);
            this.cmbBoxBillStatus.TabIndex = 4;
            this.cmbBoxBillStatus.SelectedIndexChanged += new System.EventHandler(this.cmbBoxBillStatus_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Bill Status";
            // 
            // checkBoxApplyFilter
            // 
            this.checkBoxApplyFilter.AutoSize = true;
            this.checkBoxApplyFilter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.checkBoxApplyFilter.Location = new System.Drawing.Point(437, 28);
            this.checkBoxApplyFilter.Name = "checkBoxApplyFilter";
            this.checkBoxApplyFilter.Size = new System.Drawing.Size(75, 17);
            this.checkBoxApplyFilter.TabIndex = 2;
            this.checkBoxApplyFilter.Text = "Apply Filter";
            this.checkBoxApplyFilter.UseVisualStyleBackColor = true;
            this.checkBoxApplyFilter.CheckedChanged += new System.EventHandler(this.checkBoxApplyFilter_CheckedChanged);
            // 
            // dTimePickerTo
            // 
            this.dTimePickerTo.Location = new System.Drawing.Point(293, 38);
            this.dTimePickerTo.Name = "dTimePickerTo";
            this.dTimePickerTo.Size = new System.Drawing.Size(134, 20);
            this.dTimePickerTo.TabIndex = 1;
            // 
            // dTimePickerFrom
            // 
            this.dTimePickerFrom.Location = new System.Drawing.Point(293, 10);
            this.dTimePickerFrom.Name = "dTimePickerFrom";
            this.dTimePickerFrom.Size = new System.Drawing.Size(134, 20);
            this.dTimePickerFrom.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(226, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Bill Date to:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Bill Date from:";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(803, -24);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(234, 21);
            this.comboBox2.TabIndex = 9;
            // 
            // btnExportBill
            // 
            this.btnExportBill.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExportBill.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnExportBill.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnExportBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportBill.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnExportBill.Image = global::SalesOrdersReport.Properties.Resources.export_icon;
            this.btnExportBill.Location = new System.Drawing.Point(336, 3);
            this.btnExportBill.Name = "btnExportBill";
            this.btnExportBill.Size = new System.Drawing.Size(63, 73);
            this.btnExportBill.TabIndex = 0;
            this.btnExportBill.Text = "Export Bills";
            this.btnExportBill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportBill.UseVisualStyleBackColor = false;
            this.btnExportBill.Click += new System.EventHandler(this.btnExportBill_Click);
            // 
            // btnCloseCounter
            // 
            this.btnCloseCounter.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCloseCounter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnCloseCounter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCloseCounter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseCounter.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnCloseCounter.Image = global::SalesOrdersReport.Properties.Resources.close_40;
            this.btnCloseCounter.Location = new System.Drawing.Point(474, 3);
            this.btnCloseCounter.Name = "btnCloseCounter";
            this.btnCloseCounter.Size = new System.Drawing.Size(99, 73);
            this.btnCloseCounter.TabIndex = 0;
            this.btnCloseCounter.Text = "Close Counter";
            this.btnCloseCounter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCloseCounter.UseVisualStyleBackColor = false;
            this.btnCloseCounter.Click += new System.EventHandler(this.btnCloseCounter_Click);
            // 
            // btnSearchBill
            // 
            this.btnSearchBill.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSearchBill.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnSearchBill.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSearchBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchBill.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnSearchBill.Image = global::SalesOrdersReport.Properties.Resources.Search_icon;
            this.btnSearchBill.Location = new System.Drawing.Point(405, 3);
            this.btnSearchBill.Name = "btnSearchBill";
            this.btnSearchBill.Size = new System.Drawing.Size(63, 73);
            this.btnSearchBill.TabIndex = 0;
            this.btnSearchBill.Text = "Search Bills";
            this.btnSearchBill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSearchBill.UseVisualStyleBackColor = false;
            this.btnSearchBill.Click += new System.EventHandler(this.btnSearchBill_Click);
            // 
            // btnPrintBill
            // 
            this.btnPrintBill.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPrintBill.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnPrintBill.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPrintBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintBill.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnPrintBill.Image = global::SalesOrdersReport.Properties.Resources.Iconshow_Hardware_Printer;
            this.btnPrintBill.Location = new System.Drawing.Point(270, 3);
            this.btnPrintBill.Name = "btnPrintBill";
            this.btnPrintBill.Size = new System.Drawing.Size(60, 73);
            this.btnPrintBill.TabIndex = 0;
            this.btnPrintBill.Text = "Print Bill";
            this.btnPrintBill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrintBill.UseVisualStyleBackColor = false;
            this.btnPrintBill.Click += new System.EventHandler(this.btnPrintBill_Click);
            // 
            // btnReloadBills
            // 
            this.btnReloadBills.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnReloadBills.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnReloadBills.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnReloadBills.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReloadBills.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnReloadBills.Image = global::SalesOrdersReport.Properties.Resources.Refresh_icon_32;
            this.btnReloadBills.Location = new System.Drawing.Point(201, 3);
            this.btnReloadBills.Name = "btnReloadBills";
            this.btnReloadBills.Size = new System.Drawing.Size(63, 73);
            this.btnReloadBills.TabIndex = 0;
            this.btnReloadBills.Text = "Reload";
            this.btnReloadBills.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReloadBills.UseVisualStyleBackColor = false;
            this.btnReloadBills.Click += new System.EventHandler(this.btnReloadBills_Click);
            // 
            // btnCancelBill
            // 
            this.btnCancelBill.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCancelBill.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnCancelBill.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCancelBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelBill.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnCancelBill.Image = global::SalesOrdersReport.Properties.Resources.delete_icon_32;
            this.btnCancelBill.Location = new System.Drawing.Point(137, 3);
            this.btnCancelBill.Name = "btnCancelBill";
            this.btnCancelBill.Size = new System.Drawing.Size(58, 73);
            this.btnCancelBill.TabIndex = 0;
            this.btnCancelBill.Text = "Cancel Bill";
            this.btnCancelBill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancelBill.UseVisualStyleBackColor = false;
            this.btnCancelBill.Click += new System.EventHandler(this.btnCancelBill_Click);
            // 
            // btnViewEditBill
            // 
            this.btnViewEditBill.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnViewEditBill.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnViewEditBill.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnViewEditBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewEditBill.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnViewEditBill.Image = global::SalesOrdersReport.Properties.Resources.edit_icon_32;
            this.btnViewEditBill.Location = new System.Drawing.Point(68, 3);
            this.btnViewEditBill.Name = "btnViewEditBill";
            this.btnViewEditBill.Size = new System.Drawing.Size(63, 73);
            this.btnViewEditBill.TabIndex = 0;
            this.btnViewEditBill.Text = "View Bill";
            this.btnViewEditBill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnViewEditBill.UseVisualStyleBackColor = false;
            this.btnViewEditBill.Click += new System.EventHandler(this.btnViewEditInvoice_Click);
            // 
            // dtGridViewBills
            // 
            this.dtGridViewBills.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGridViewBills.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtGridViewBills.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dtGridViewBills.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewBills.Location = new System.Drawing.Point(12, 127);
            this.dtGridViewBills.Name = "dtGridViewBills";
            this.dtGridViewBills.Size = new System.Drawing.Size(1269, 291);
            this.dtGridViewBills.TabIndex = 2;
            this.dtGridViewBills.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtGridViewBills_CellMouseClick);
            // 
            // dtGridViewBilledProducts
            // 
            this.dtGridViewBilledProducts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGridViewBilledProducts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtGridViewBilledProducts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dtGridViewBilledProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewBilledProducts.Location = new System.Drawing.Point(0, 36);
            this.dtGridViewBilledProducts.Name = "dtGridViewBilledProducts";
            this.dtGridViewBilledProducts.Size = new System.Drawing.Size(1269, 265);
            this.dtGridViewBilledProducts.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(12, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Bills";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dtGridViewBilledProducts);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.groupBox1.Location = new System.Drawing.Point(12, 424);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1269, 307);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 20);
            this.label6.TabIndex = 4;
            this.label6.Text = "Billed Products";
            // 
            // lblOrdersCount
            // 
            this.lblOrdersCount.AutoSize = true;
            this.lblOrdersCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrdersCount.Location = new System.Drawing.Point(102, 103);
            this.lblOrdersCount.Name = "lblOrdersCount";
            this.lblOrdersCount.Size = new System.Drawing.Size(140, 20);
            this.lblOrdersCount.TabIndex = 4;
            this.lblOrdersCount.Text = "[Displaying all Bills]";
            // 
            // backgroundWorkerBills
            // 
            this.backgroundWorkerBills.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerBills_DoWork);
            this.backgroundWorkerBills.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerBills_ProgressChanged);
            this.backgroundWorkerBills.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerBills_RunWorkerCompleted);
            // 
            // POSBillsMainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1289, 743);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblOrdersCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtGridViewBills);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "POSBillsMainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "POS Billing";
            this.Shown += new System.EventHandler(this.POSBillsMainForm_Shown);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewBills)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewBilledProducts)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateBill;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnViewEditBill;
        private System.Windows.Forms.DataGridView dtGridViewBills;
        private System.Windows.Forms.Button btnReloadBills;
        private System.Windows.Forms.DataGridView dtGridViewBilledProducts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPrintBill;
        private System.Windows.Forms.Button btnSearchBill;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxApplyFilter;
        private System.Windows.Forms.DateTimePicker dTimePickerTo;
        private System.Windows.Forms.DateTimePicker dTimePickerFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOrdersCount;
        private System.Windows.Forms.ComboBox cmbBoxBillStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.ComponentModel.BackgroundWorker backgroundWorkerBills;
        private System.Windows.Forms.Button btnExportBill;
        private System.Windows.Forms.Button btnCancelBill;
        private System.Windows.Forms.Button btnCloseCounter;
    }
}