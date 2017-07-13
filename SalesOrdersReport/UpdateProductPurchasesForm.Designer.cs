namespace SalesOrdersReport
{
    partial class UpdateProductPurchasesForm
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
            this.chkBoxUpdVendorHistory = new System.Windows.Forms.CheckBox();
            this.groupBoxProductVendorFiles = new System.Windows.Forms.GroupBox();
            this.lblProductInventoryFile = new System.Windows.Forms.Label();
            this.lblProductStockHistoryFile = new System.Windows.Forms.Label();
            this.lblVendorHistoryFile = new System.Windows.Forms.Label();
            this.txtBoxProductStockHistoryFile = new System.Windows.Forms.TextBox();
            this.txtBoxProductInventoryFile = new System.Windows.Forms.TextBox();
            this.txtBoxVendorHistoryFile = new System.Windows.Forms.TextBox();
            this.btnBrowseProductStockHistFile = new System.Windows.Forms.Button();
            this.btnBrowseProductInvFile = new System.Windows.Forms.Button();
            this.btnBrowseVendorHistory = new System.Windows.Forms.Button();
            this.chkBoxUpdProductInventory = new System.Windows.Forms.CheckBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBrowseVendorPOFile = new System.Windows.Forms.Button();
            this.txtBoxVendorPOFile = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.bgWorkerUpdPurchasesForm = new System.ComponentModel.BackgroundWorker();
            this.chkBoxUpdStockHistory = new System.Windows.Forms.CheckBox();
            this.groupBoxProductVendorFiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkBoxUpdVendorHistory
            // 
            this.chkBoxUpdVendorHistory.AutoSize = true;
            this.chkBoxUpdVendorHistory.Checked = true;
            this.chkBoxUpdVendorHistory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxUpdVendorHistory.Location = new System.Drawing.Point(316, 52);
            this.chkBoxUpdVendorHistory.Name = "chkBoxUpdVendorHistory";
            this.chkBoxUpdVendorHistory.Size = new System.Drawing.Size(133, 17);
            this.chkBoxUpdVendorHistory.TabIndex = 4;
            this.chkBoxUpdVendorHistory.Text = "Update &Vendor History";
            this.chkBoxUpdVendorHistory.UseVisualStyleBackColor = true;
            this.chkBoxUpdVendorHistory.CheckedChanged += new System.EventHandler(this.chkBoxUpdVendorHistory_CheckedChanged);
            // 
            // groupBoxProductVendorFiles
            // 
            this.groupBoxProductVendorFiles.Controls.Add(this.lblProductInventoryFile);
            this.groupBoxProductVendorFiles.Controls.Add(this.lblProductStockHistoryFile);
            this.groupBoxProductVendorFiles.Controls.Add(this.lblVendorHistoryFile);
            this.groupBoxProductVendorFiles.Controls.Add(this.txtBoxProductStockHistoryFile);
            this.groupBoxProductVendorFiles.Controls.Add(this.txtBoxProductInventoryFile);
            this.groupBoxProductVendorFiles.Controls.Add(this.txtBoxVendorHistoryFile);
            this.groupBoxProductVendorFiles.Controls.Add(this.btnBrowseProductStockHistFile);
            this.groupBoxProductVendorFiles.Controls.Add(this.btnBrowseProductInvFile);
            this.groupBoxProductVendorFiles.Controls.Add(this.btnBrowseVendorHistory);
            this.groupBoxProductVendorFiles.Location = new System.Drawing.Point(12, 67);
            this.groupBoxProductVendorFiles.Name = "groupBoxProductVendorFiles";
            this.groupBoxProductVendorFiles.Size = new System.Drawing.Size(437, 136);
            this.groupBoxProductVendorFiles.TabIndex = 23;
            this.groupBoxProductVendorFiles.TabStop = false;
            // 
            // lblProductInventoryFile
            // 
            this.lblProductInventoryFile.AutoSize = true;
            this.lblProductInventoryFile.Location = new System.Drawing.Point(11, 26);
            this.lblProductInventoryFile.Name = "lblProductInventoryFile";
            this.lblProductInventoryFile.Size = new System.Drawing.Size(110, 13);
            this.lblProductInventoryFile.TabIndex = 0;
            this.lblProductInventoryFile.Text = "Product Inventory File";
            // 
            // lblProductStockHistoryFile
            // 
            this.lblProductStockHistoryFile.AutoSize = true;
            this.lblProductStockHistoryFile.Location = new System.Drawing.Point(11, 63);
            this.lblProductStockHistoryFile.Name = "lblProductStockHistoryFile";
            this.lblProductStockHistoryFile.Size = new System.Drawing.Size(89, 13);
            this.lblProductStockHistoryFile.TabIndex = 0;
            this.lblProductStockHistoryFile.Text = "Stock History File";
            // 
            // lblVendorHistoryFile
            // 
            this.lblVendorHistoryFile.AutoSize = true;
            this.lblVendorHistoryFile.Location = new System.Drawing.Point(11, 98);
            this.lblVendorHistoryFile.Name = "lblVendorHistoryFile";
            this.lblVendorHistoryFile.Size = new System.Drawing.Size(95, 13);
            this.lblVendorHistoryFile.TabIndex = 0;
            this.lblVendorHistoryFile.Text = "Vendor History File";
            // 
            // txtBoxProductStockHistoryFile
            // 
            this.txtBoxProductStockHistoryFile.Location = new System.Drawing.Point(122, 60);
            this.txtBoxProductStockHistoryFile.Name = "txtBoxProductStockHistoryFile";
            this.txtBoxProductStockHistoryFile.Size = new System.Drawing.Size(220, 20);
            this.txtBoxProductStockHistoryFile.TabIndex = 7;
            // 
            // txtBoxProductInventoryFile
            // 
            this.txtBoxProductInventoryFile.Location = new System.Drawing.Point(122, 23);
            this.txtBoxProductInventoryFile.Name = "txtBoxProductInventoryFile";
            this.txtBoxProductInventoryFile.Size = new System.Drawing.Size(220, 20);
            this.txtBoxProductInventoryFile.TabIndex = 5;
            // 
            // txtBoxVendorHistoryFile
            // 
            this.txtBoxVendorHistoryFile.Location = new System.Drawing.Point(122, 95);
            this.txtBoxVendorHistoryFile.Name = "txtBoxVendorHistoryFile";
            this.txtBoxVendorHistoryFile.Size = new System.Drawing.Size(220, 20);
            this.txtBoxVendorHistoryFile.TabIndex = 9;
            // 
            // btnBrowseProductStockHistFile
            // 
            this.btnBrowseProductStockHistFile.Location = new System.Drawing.Point(348, 58);
            this.btnBrowseProductStockHistFile.Name = "btnBrowseProductStockHistFile";
            this.btnBrowseProductStockHistFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseProductStockHistFile.TabIndex = 8;
            this.btnBrowseProductStockHistFile.Text = "Br&owse";
            this.btnBrowseProductStockHistFile.UseVisualStyleBackColor = true;
            this.btnBrowseProductStockHistFile.Click += new System.EventHandler(this.btnBrowseVendorHistory_Click);
            // 
            // btnBrowseProductInvFile
            // 
            this.btnBrowseProductInvFile.Location = new System.Drawing.Point(348, 21);
            this.btnBrowseProductInvFile.Name = "btnBrowseProductInvFile";
            this.btnBrowseProductInvFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseProductInvFile.TabIndex = 6;
            this.btnBrowseProductInvFile.Text = "B&rowse";
            this.btnBrowseProductInvFile.UseVisualStyleBackColor = true;
            this.btnBrowseProductInvFile.Click += new System.EventHandler(this.btnBrowseProductInvFile_Click);
            // 
            // btnBrowseVendorHistory
            // 
            this.btnBrowseVendorHistory.Location = new System.Drawing.Point(348, 93);
            this.btnBrowseVendorHistory.Name = "btnBrowseVendorHistory";
            this.btnBrowseVendorHistory.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseVendorHistory.TabIndex = 10;
            this.btnBrowseVendorHistory.Text = "Bro&wse";
            this.btnBrowseVendorHistory.UseVisualStyleBackColor = true;
            this.btnBrowseVendorHistory.Click += new System.EventHandler(this.btnBrowseVendorHistory_Click);
            // 
            // chkBoxUpdProductInventory
            // 
            this.chkBoxUpdProductInventory.AutoSize = true;
            this.chkBoxUpdProductInventory.Checked = true;
            this.chkBoxUpdProductInventory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxUpdProductInventory.Location = new System.Drawing.Point(26, 52);
            this.chkBoxUpdProductInventory.Name = "chkBoxUpdProductInventory";
            this.chkBoxUpdProductInventory.Size = new System.Drawing.Size(148, 17);
            this.chkBoxUpdProductInventory.TabIndex = 3;
            this.chkBoxUpdProductInventory.Text = "Update &Product Inventory";
            this.chkBoxUpdProductInventory.UseVisualStyleBackColor = true;
            this.chkBoxUpdProductInventory.CheckedChanged += new System.EventHandler(this.chkBoxUpdProductInventory_CheckedChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(68, 241);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 18;
            this.lblStatus.Text = "Status";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Status:";
            // 
            // btnBrowseVendorPOFile
            // 
            this.btnBrowseVendorPOFile.Location = new System.Drawing.Point(360, 17);
            this.btnBrowseVendorPOFile.Name = "btnBrowseVendorPOFile";
            this.btnBrowseVendorPOFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseVendorPOFile.TabIndex = 2;
            this.btnBrowseVendorPOFile.Text = "&Browse";
            this.btnBrowseVendorPOFile.UseVisualStyleBackColor = true;
            this.btnBrowseVendorPOFile.Click += new System.EventHandler(this.btnBrowseVendorPOFile_Click);
            // 
            // txtBoxVendorPOFile
            // 
            this.txtBoxVendorPOFile.Location = new System.Drawing.Point(134, 19);
            this.txtBoxVendorPOFile.Name = "txtBoxVendorPOFile";
            this.txtBoxVendorPOFile.Size = new System.Drawing.Size(220, 20);
            this.txtBoxVendorPOFile.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(266, 209);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(148, 209);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Purchase Order File";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // bgWorkerUpdPurchasesForm
            // 
            this.bgWorkerUpdPurchasesForm.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerUpdPurchasesForm_DoWork);
            this.bgWorkerUpdPurchasesForm.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorkerUpdPurchasesForm_ProgressChanged);
            this.bgWorkerUpdPurchasesForm.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerUpdPurchasesForm_RunWorkerCompleted);
            // 
            // chkBoxUpdStockHistory
            // 
            this.chkBoxUpdStockHistory.AutoSize = true;
            this.chkBoxUpdStockHistory.Checked = true;
            this.chkBoxUpdStockHistory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxUpdStockHistory.Location = new System.Drawing.Point(180, 52);
            this.chkBoxUpdStockHistory.Name = "chkBoxUpdStockHistory";
            this.chkBoxUpdStockHistory.Size = new System.Drawing.Size(127, 17);
            this.chkBoxUpdStockHistory.TabIndex = 4;
            this.chkBoxUpdStockHistory.Text = "Update &Stock History";
            this.chkBoxUpdStockHistory.UseVisualStyleBackColor = true;
            this.chkBoxUpdStockHistory.CheckedChanged += new System.EventHandler(this.chkBoxUpdStockHistory_CheckedChanged);
            // 
            // UpdateProductPurchasesForm
            // 
            this.AcceptButton = this.btnUpdate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(465, 274);
            this.Controls.Add(this.chkBoxUpdStockHistory);
            this.Controls.Add(this.chkBoxUpdVendorHistory);
            this.Controls.Add(this.groupBoxProductVendorFiles);
            this.Controls.Add(this.chkBoxUpdProductInventory);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBrowseVendorPOFile);
            this.Controls.Add(this.txtBoxVendorPOFile);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.label1);
            this.Name = "UpdateProductPurchasesForm";
            this.Text = "Update Product Purchases";
            this.groupBoxProductVendorFiles.ResumeLayout(false);
            this.groupBoxProductVendorFiles.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkBoxUpdVendorHistory;
        private System.Windows.Forms.GroupBox groupBoxProductVendorFiles;
        private System.Windows.Forms.Label lblVendorHistoryFile;
        private System.Windows.Forms.TextBox txtBoxVendorHistoryFile;
        private System.Windows.Forms.Button btnBrowseVendorHistory;
        private System.Windows.Forms.CheckBox chkBoxUpdProductInventory;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBrowseVendorPOFile;
        private System.Windows.Forms.TextBox txtBoxVendorPOFile;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblProductInventoryFile;
        private System.Windows.Forms.TextBox txtBoxProductInventoryFile;
        private System.Windows.Forms.Button btnBrowseProductInvFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.ComponentModel.BackgroundWorker bgWorkerUpdPurchasesForm;
        private System.Windows.Forms.Label lblProductStockHistoryFile;
        private System.Windows.Forms.TextBox txtBoxProductStockHistoryFile;
        private System.Windows.Forms.Button btnBrowseProductStockHistFile;
        private System.Windows.Forms.CheckBox chkBoxUpdStockHistory;
    }
}