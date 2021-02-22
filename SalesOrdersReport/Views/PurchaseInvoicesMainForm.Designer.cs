namespace SalesOrdersReport.Views
{
    partial class PurchaseInvoicesMainForm
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
            this.btnCreateInvoice = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbBoxInvoiceStatus = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxApplyFilter = new System.Windows.Forms.CheckBox();
            this.dTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.dTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.btnSearchInvoice = new System.Windows.Forms.Button();
            this.btnPrintQuotation = new System.Windows.Forms.Button();
            this.btnPrintInvoice = new System.Windows.Forms.Button();
            this.btnReloadInvoices = new System.Windows.Forms.Button();
            this.btnDeleteInvoice = new System.Windows.Forms.Button();
            this.btnViewEditInvoice = new System.Windows.Forms.Button();
            this.dtGridViewInvoices = new System.Windows.Forms.DataGridView();
            this.dtGridViewInvoicedProducts = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblOrdersCount = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewInvoices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewInvoicedProducts)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateInvoice
            // 
            this.btnCreateInvoice.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCreateInvoice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnCreateInvoice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCreateInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateInvoice.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateInvoice.Image = global::SalesOrdersReport.Properties.Resources.math_add_icon_32;
            this.btnCreateInvoice.Location = new System.Drawing.Point(5, 3);
            this.btnCreateInvoice.Name = "btnCreateInvoice";
            this.btnCreateInvoice.Size = new System.Drawing.Size(83, 73);
            this.btnCreateInvoice.TabIndex = 0;
            this.btnCreateInvoice.Text = "Create Invoice";
            this.btnCreateInvoice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCreateInvoice.UseVisualStyleBackColor = false;
            this.btnCreateInvoice.Click += new System.EventHandler(this.btnCreateInvoice_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Controls.Add(this.btnSearchInvoice);
            this.panel1.Controls.Add(this.btnPrintQuotation);
            this.panel1.Controls.Add(this.btnPrintInvoice);
            this.panel1.Controls.Add(this.btnReloadInvoices);
            this.panel1.Controls.Add(this.btnDeleteInvoice);
            this.panel1.Controls.Add(this.btnViewEditInvoice);
            this.panel1.Controls.Add(this.btnCreateInvoice);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1164, 84);
            this.panel1.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.cmbBoxInvoiceStatus);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.checkBoxApplyFilter);
            this.groupBox3.Controls.Add(this.dTimePickerTo);
            this.groupBox3.Controls.Add(this.dTimePickerFrom);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(642, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(518, 73);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // cmbBoxInvoiceStatus
            // 
            this.cmbBoxInvoiceStatus.FormattingEnabled = true;
            this.cmbBoxInvoiceStatus.Location = new System.Drawing.Point(79, 13);
            this.cmbBoxInvoiceStatus.Name = "cmbBoxInvoiceStatus";
            this.cmbBoxInvoiceStatus.Size = new System.Drawing.Size(87, 21);
            this.cmbBoxInvoiceStatus.TabIndex = 4;
            this.cmbBoxInvoiceStatus.SelectedIndexChanged += new System.EventHandler(this.cmbBoxInvoiceStatus_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(-2, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Invoice Status";
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
            this.label3.Location = new System.Drawing.Point(204, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Invoice Date to:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(193, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Invoice Date from:";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(803, -24);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(234, 21);
            this.comboBox2.TabIndex = 9;
            // 
            // btnSearchInvoice
            // 
            this.btnSearchInvoice.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSearchInvoice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnSearchInvoice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSearchInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchInvoice.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnSearchInvoice.Image = global::SalesOrdersReport.Properties.Resources.Search_icon;
            this.btnSearchInvoice.Location = new System.Drawing.Point(539, 3);
            this.btnSearchInvoice.Name = "btnSearchInvoice";
            this.btnSearchInvoice.Size = new System.Drawing.Size(83, 73);
            this.btnSearchInvoice.TabIndex = 0;
            this.btnSearchInvoice.Text = "Search Invoices";
            this.btnSearchInvoice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSearchInvoice.UseVisualStyleBackColor = false;
            this.btnSearchInvoice.Click += new System.EventHandler(this.btnSearchInvoice_Click);
            // 
            // btnPrintQuotation
            // 
            this.btnPrintQuotation.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPrintQuotation.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnPrintQuotation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPrintQuotation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintQuotation.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnPrintQuotation.Image = global::SalesOrdersReport.Properties.Resources.Iconshow_Hardware_Printer;
            this.btnPrintQuotation.Location = new System.Drawing.Point(450, 3);
            this.btnPrintQuotation.Name = "btnPrintQuotation";
            this.btnPrintQuotation.Size = new System.Drawing.Size(83, 73);
            this.btnPrintQuotation.TabIndex = 0;
            this.btnPrintQuotation.Text = "Print as Quotation";
            this.btnPrintQuotation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrintQuotation.UseVisualStyleBackColor = false;
            this.btnPrintQuotation.Click += new System.EventHandler(this.btnPrintInvoice_Click);
            // 
            // btnPrintInvoice
            // 
            this.btnPrintInvoice.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPrintInvoice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnPrintInvoice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPrintInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintInvoice.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnPrintInvoice.Image = global::SalesOrdersReport.Properties.Resources.Iconshow_Hardware_Printer;
            this.btnPrintInvoice.Location = new System.Drawing.Point(361, 3);
            this.btnPrintInvoice.Name = "btnPrintInvoice";
            this.btnPrintInvoice.Size = new System.Drawing.Size(83, 73);
            this.btnPrintInvoice.TabIndex = 0;
            this.btnPrintInvoice.Text = "Print as Invoice";
            this.btnPrintInvoice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrintInvoice.UseVisualStyleBackColor = false;
            this.btnPrintInvoice.Click += new System.EventHandler(this.btnPrintInvoice_Click);
            // 
            // btnReloadInvoices
            // 
            this.btnReloadInvoices.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnReloadInvoices.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnReloadInvoices.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnReloadInvoices.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReloadInvoices.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnReloadInvoices.Image = global::SalesOrdersReport.Properties.Resources.Refresh_icon_32;
            this.btnReloadInvoices.Location = new System.Drawing.Point(272, 3);
            this.btnReloadInvoices.Name = "btnReloadInvoices";
            this.btnReloadInvoices.Size = new System.Drawing.Size(83, 73);
            this.btnReloadInvoices.TabIndex = 0;
            this.btnReloadInvoices.Text = "Reload Invoices";
            this.btnReloadInvoices.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReloadInvoices.UseVisualStyleBackColor = false;
            this.btnReloadInvoices.Click += new System.EventHandler(this.btnReloadInvoices_Click);
            // 
            // btnDeleteInvoice
            // 
            this.btnDeleteInvoice.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDeleteInvoice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnDeleteInvoice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnDeleteInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteInvoice.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnDeleteInvoice.Image = global::SalesOrdersReport.Properties.Resources.delete_icon_32;
            this.btnDeleteInvoice.Location = new System.Drawing.Point(183, 3);
            this.btnDeleteInvoice.Name = "btnDeleteInvoice";
            this.btnDeleteInvoice.Size = new System.Drawing.Size(83, 73);
            this.btnDeleteInvoice.TabIndex = 0;
            this.btnDeleteInvoice.Text = "Delete Invoice";
            this.btnDeleteInvoice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteInvoice.UseVisualStyleBackColor = false;
            this.btnDeleteInvoice.Click += new System.EventHandler(this.btnDeleteInvoice_Click);
            // 
            // btnViewEditInvoice
            // 
            this.btnViewEditInvoice.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnViewEditInvoice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnViewEditInvoice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnViewEditInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewEditInvoice.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnViewEditInvoice.Image = global::SalesOrdersReport.Properties.Resources.edit_icon_32;
            this.btnViewEditInvoice.Location = new System.Drawing.Point(94, 3);
            this.btnViewEditInvoice.Name = "btnViewEditInvoice";
            this.btnViewEditInvoice.Size = new System.Drawing.Size(83, 73);
            this.btnViewEditInvoice.TabIndex = 0;
            this.btnViewEditInvoice.Text = "View/Edit Invoice";
            this.btnViewEditInvoice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnViewEditInvoice.UseVisualStyleBackColor = false;
            this.btnViewEditInvoice.Click += new System.EventHandler(this.btnViewEditInvoice_Click);
            // 
            // dtGridViewInvoices
            // 
            this.dtGridViewInvoices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGridViewInvoices.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtGridViewInvoices.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dtGridViewInvoices.Location = new System.Drawing.Point(12, 127);
            this.dtGridViewInvoices.Name = "dtGridViewInvoices";
            this.dtGridViewInvoices.Size = new System.Drawing.Size(1164, 291);
            this.dtGridViewInvoices.TabIndex = 2;
            this.dtGridViewInvoices.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtGridViewInvoices_CellMouseClick);
            // 
            // dtGridViewInvoicedProducts
            // 
            this.dtGridViewInvoicedProducts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGridViewInvoicedProducts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtGridViewInvoicedProducts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dtGridViewInvoicedProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewInvoicedProducts.Location = new System.Drawing.Point(0, 22);
            this.dtGridViewInvoicedProducts.Name = "dtGridViewInvoicedProducts";
            this.dtGridViewInvoicedProducts.Size = new System.Drawing.Size(1164, 279);
            this.dtGridViewInvoicedProducts.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(12, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Invoices";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dtGridViewInvoicedProducts);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox1.Location = new System.Drawing.Point(12, 424);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1164, 307);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Invoiced Products";
            // 
            // lblOrdersCount
            // 
            this.lblOrdersCount.AutoSize = true;
            this.lblOrdersCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrdersCount.Location = new System.Drawing.Point(102, 103);
            this.lblOrdersCount.Name = "lblOrdersCount";
            this.lblOrdersCount.Size = new System.Drawing.Size(170, 20);
            this.lblOrdersCount.TabIndex = 4;
            this.lblOrdersCount.Text = "[Displaying all Invoices]";
            // 
            // InvoicesMainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1184, 743);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblOrdersCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtGridViewInvoices);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InvoicesMainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Invoices";
            this.Shown += new System.EventHandler(this.InvoicesMainForm_Shown);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewInvoices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewInvoicedProducts)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateInvoice;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnViewEditInvoice;
        private System.Windows.Forms.DataGridView dtGridViewInvoices;
        private System.Windows.Forms.Button btnDeleteInvoice;
        private System.Windows.Forms.Button btnReloadInvoices;
        private System.Windows.Forms.DataGridView dtGridViewInvoicedProducts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPrintQuotation;
        private System.Windows.Forms.Button btnPrintInvoice;
        private System.Windows.Forms.Button btnSearchInvoice;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxApplyFilter;
        private System.Windows.Forms.DateTimePicker dTimePickerTo;
        private System.Windows.Forms.DateTimePicker dTimePickerFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOrdersCount;
        private System.Windows.Forms.ComboBox cmbBoxInvoiceStatus;
        private System.Windows.Forms.Label label4;
    }
}