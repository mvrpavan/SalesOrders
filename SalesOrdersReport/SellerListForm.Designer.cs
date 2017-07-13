namespace SalesOrdersReport
{
    partial class SellerListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SellerListForm));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBoxLineFilter = new System.Windows.Forms.ComboBox();
            this.dtGridViewSellers = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.colSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewSellers)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Line";
            // 
            // cmbBoxLineFilter
            // 
            this.cmbBoxLineFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxLineFilter.FormattingEnabled = true;
            this.cmbBoxLineFilter.Location = new System.Drawing.Point(78, 22);
            this.cmbBoxLineFilter.Name = "cmbBoxLineFilter";
            this.cmbBoxLineFilter.Size = new System.Drawing.Size(121, 21);
            this.cmbBoxLineFilter.TabIndex = 1;
            this.cmbBoxLineFilter.SelectedIndexChanged += new System.EventHandler(this.cmbBoxLineFilter_SelectedIndexChanged);
            // 
            // dtGridViewSellers
            // 
            this.dtGridViewSellers.AllowUserToAddRows = false;
            this.dtGridViewSellers.AllowUserToDeleteRows = false;
            this.dtGridViewSellers.AllowUserToResizeRows = false;
            this.dtGridViewSellers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewSellers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelect});
            this.dtGridViewSellers.Location = new System.Drawing.Point(12, 49);
            this.dtGridViewSellers.MultiSelect = false;
            this.dtGridViewSellers.Name = "dtGridViewSellers";
            this.dtGridViewSellers.ReadOnly = true;
            this.dtGridViewSellers.RowHeadersVisible = false;
            this.dtGridViewSellers.Size = new System.Drawing.Size(423, 150);
            this.dtGridViewSellers.TabIndex = 2;
            this.dtGridViewSellers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGridViewSellers_CellClick);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(184, 227);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // colSelect
            // 
            this.colSelect.FalseValue = "false";
            this.colSelect.HeaderText = "";
            this.colSelect.Name = "colSelect";
            this.colSelect.ReadOnly = true;
            this.colSelect.ToolTipText = "Check to Select Seller";
            this.colSelect.TrueValue = "true";
            this.colSelect.Width = 20;
            // 
            // SellerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(453, 262);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dtGridViewSellers);
            this.Controls.Add(this.cmbBoxLineFilter);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SellerList";
            this.Text = "Add Sellers to List";
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewSellers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBoxLineFilter;
        private System.Windows.Forms.DataGridView dtGridViewSellers;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelect;
    }
}