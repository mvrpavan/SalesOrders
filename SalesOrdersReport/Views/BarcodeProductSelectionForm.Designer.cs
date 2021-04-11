namespace SalesOrdersReport.Views
{
    partial class BarcodeProductSelectionForm
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
            this.dtGridViewProducts = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearchBarcode = new System.Windows.Forms.Button();
            this.btnSelectProduct = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbBoxBarcodes = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // dtGridViewProducts
            // 
            this.dtGridViewProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewProducts.Location = new System.Drawing.Point(13, 42);
            this.dtGridViewProducts.Name = "dtGridViewProducts";
            this.dtGridViewProducts.Size = new System.Drawing.Size(498, 168);
            this.dtGridViewProducts.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter Barcode";
            // 
            // btnSearchBarcode
            // 
            this.btnSearchBarcode.Location = new System.Drawing.Point(333, 13);
            this.btnSearchBarcode.Name = "btnSearchBarcode";
            this.btnSearchBarcode.Size = new System.Drawing.Size(75, 23);
            this.btnSearchBarcode.TabIndex = 3;
            this.btnSearchBarcode.Text = "Search";
            this.btnSearchBarcode.UseVisualStyleBackColor = true;
            this.btnSearchBarcode.Click += new System.EventHandler(this.btnSearchBarcode_Click);
            // 
            // btnSelectProduct
            // 
            this.btnSelectProduct.Location = new System.Drawing.Point(135, 216);
            this.btnSelectProduct.Name = "btnSelectProduct";
            this.btnSelectProduct.Size = new System.Drawing.Size(95, 23);
            this.btnSelectProduct.TabIndex = 4;
            this.btnSelectProduct.Text = "Select Product";
            this.btnSelectProduct.UseVisualStyleBackColor = true;
            this.btnSelectProduct.Click += new System.EventHandler(this.btnSelectProduct_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(277, 216);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cmbBoxBarcodes
            // 
            this.cmbBoxBarcodes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBoxBarcodes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBoxBarcodes.FormattingEnabled = true;
            this.cmbBoxBarcodes.Location = new System.Drawing.Point(108, 15);
            this.cmbBoxBarcodes.Name = "cmbBoxBarcodes";
            this.cmbBoxBarcodes.Size = new System.Drawing.Size(208, 21);
            this.cmbBoxBarcodes.TabIndex = 6;
            this.cmbBoxBarcodes.SelectedIndexChanged += new System.EventHandler(this.cmbBoxBarcodes_SelectedIndexChanged);
            // 
            // BarcodeProductSelectionForm
            // 
            this.AcceptButton = this.btnSelectProduct;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(525, 254);
            this.Controls.Add(this.cmbBoxBarcodes);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelectProduct);
            this.Controls.Add(this.btnSearchBarcode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtGridViewProducts);
            this.Name = "BarcodeProductSelectionForm";
            this.Text = "Search Barcode";
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtGridViewProducts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearchBarcode;
        private System.Windows.Forms.Button btnSelectProduct;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbBoxBarcodes;
    }
}