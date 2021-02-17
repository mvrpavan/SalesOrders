namespace SalesOrdersReport.Views
{
    partial class VendorListForm
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
            this.btnClose = new System.Windows.Forms.Button();
            this.dtGridViewVendors = new System.Windows.Forms.DataGridView();
            this.colSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cmbBoxLineFilter = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewVendors)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(194, 223);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dtGridViewVendors
            // 
            this.dtGridViewVendors.AllowUserToAddRows = false;
            this.dtGridViewVendors.AllowUserToDeleteRows = false;
            this.dtGridViewVendors.AllowUserToResizeRows = false;
            this.dtGridViewVendors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewVendors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelect});
            this.dtGridViewVendors.Location = new System.Drawing.Point(22, 45);
            this.dtGridViewVendors.MultiSelect = false;
            this.dtGridViewVendors.Name = "dtGridViewVendors";
            this.dtGridViewVendors.ReadOnly = true;
            this.dtGridViewVendors.RowHeadersVisible = false;
            this.dtGridViewVendors.Size = new System.Drawing.Size(423, 150);
            this.dtGridViewVendors.TabIndex = 6;
            this.dtGridViewVendors.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGridViewVendors_CellClick);
            // 
            // colSelect
            // 
            this.colSelect.FalseValue = "false";
            this.colSelect.HeaderText = "";
            this.colSelect.Name = "colSelect";
            this.colSelect.ReadOnly = true;
            this.colSelect.ToolTipText = "Check to Select Vendor";
            this.colSelect.TrueValue = "true";
            this.colSelect.Width = 20;
            // 
            // cmbBoxLineFilter
            // 
            this.cmbBoxLineFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxLineFilter.FormattingEnabled = true;
            this.cmbBoxLineFilter.Location = new System.Drawing.Point(88, 18);
            this.cmbBoxLineFilter.Name = "cmbBoxLineFilter";
            this.cmbBoxLineFilter.Size = new System.Drawing.Size(121, 21);
            this.cmbBoxLineFilter.TabIndex = 5;
            this.cmbBoxLineFilter.SelectedIndexChanged += new System.EventHandler(this.cmbBoxLineFilter_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select Line";
            // 
            // VendorListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(466, 256);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dtGridViewVendors);
            this.Controls.Add(this.cmbBoxLineFilter);
            this.Controls.Add(this.label1);
            this.Name = "VendorListForm";
            this.Text = "Vendor List";
            this.Load += new System.EventHandler(this.VendorListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewVendors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dtGridViewVendors;
        private System.Windows.Forms.ComboBox cmbBoxLineFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelect;
    }
}