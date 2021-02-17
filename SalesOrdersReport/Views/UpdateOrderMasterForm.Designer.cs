namespace SalesOrdersReport.Views
{
    partial class UpdateOrderMasterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateOrderMasterForm));
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtBoxSellerSummaryFile = new System.Windows.Forms.TextBox();
            this.FileDialgSellerSummary = new System.Windows.Forms.OpenFileDialog();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lblSellerHistoryFile = new System.Windows.Forms.Label();
            this.txtBoxSellerHistoryFile = new System.Windows.Forms.TextBox();
            this.btnBrowseSellerHistory = new System.Windows.Forms.Button();
            this.chkBoxUpdSellerMaster = new System.Windows.Forms.CheckBox();
            this.chkBoxUpdSellerHistory = new System.Windows.Forms.CheckBox();
            this.groupBoxHistoryUpdate = new System.Windows.Forms.GroupBox();
            this.lblProductStockHistoryFile = new System.Windows.Forms.Label();
            this.txtBoxProductStockHistoryFile = new System.Windows.Forms.TextBox();
            this.lblProductInventoryFile = new System.Windows.Forms.Label();
            this.txtBoxProductInventoryFile = new System.Windows.Forms.TextBox();
            this.btnBrowseProductStockHistoryFile = new System.Windows.Forms.Button();
            this.btnBrowseProductInvFile = new System.Windows.Forms.Button();
            this.chkBoxUpdProductSales = new System.Windows.Forms.CheckBox();
            this.groupBoxHistoryUpdate.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Seller Invoice File";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(149, 204);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtBoxSellerSummaryFile
            // 
            this.txtBoxSellerSummaryFile.Location = new System.Drawing.Point(134, 25);
            this.txtBoxSellerSummaryFile.Name = "txtBoxSellerSummaryFile";
            this.txtBoxSellerSummaryFile.Size = new System.Drawing.Size(220, 20);
            this.txtBoxSellerSummaryFile.TabIndex = 0;
            // 
            // FileDialgSellerSummary
            // 
            this.FileDialgSellerSummary.FileName = "openFileDialog1";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(360, 23);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 231);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Status:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(69, 231);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(35, 13);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "status";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(267, 204);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // lblSellerHistoryFile
            // 
            this.lblSellerHistoryFile.AutoSize = true;
            this.lblSellerHistoryFile.Location = new System.Drawing.Point(11, 22);
            this.lblSellerHistoryFile.Name = "lblSellerHistoryFile";
            this.lblSellerHistoryFile.Size = new System.Drawing.Size(87, 13);
            this.lblSellerHistoryFile.TabIndex = 0;
            this.lblSellerHistoryFile.Text = "Seller History File";
            // 
            // txtBoxSellerHistoryFile
            // 
            this.txtBoxSellerHistoryFile.Location = new System.Drawing.Point(122, 19);
            this.txtBoxSellerHistoryFile.Name = "txtBoxSellerHistoryFile";
            this.txtBoxSellerHistoryFile.Size = new System.Drawing.Size(220, 20);
            this.txtBoxSellerHistoryFile.TabIndex = 5;
        
            // 
            // btnBrowseSellerHistory
            // 
            this.btnBrowseSellerHistory.Location = new System.Drawing.Point(348, 17);
            this.btnBrowseSellerHistory.Name = "btnBrowseSellerHistory";
            this.btnBrowseSellerHistory.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseSellerHistory.TabIndex = 6;
            this.btnBrowseSellerHistory.Text = "B&rowse";
            this.btnBrowseSellerHistory.UseVisualStyleBackColor = true;
            this.btnBrowseSellerHistory.Click += new System.EventHandler(this.btnBrowsSellerHistory_Click);
            // 
            // chkBoxUpdSellerMaster
            // 
            this.chkBoxUpdSellerMaster.AutoSize = true;
            this.chkBoxUpdSellerMaster.Location = new System.Drawing.Point(26, 58);
            this.chkBoxUpdSellerMaster.Name = "chkBoxUpdSellerMaster";
            this.chkBoxUpdSellerMaster.Size = new System.Drawing.Size(125, 17);
            this.chkBoxUpdSellerMaster.TabIndex = 2;
            this.chkBoxUpdSellerMaster.Text = "Update Seller Master";
            this.chkBoxUpdSellerMaster.UseVisualStyleBackColor = true;
            this.chkBoxUpdSellerMaster.CheckedChanged += new System.EventHandler(this.chkBoxUpdSellerMaster_CheckedChanged);
            // 
            // chkBoxUpdSellerHistory
            // 
            this.chkBoxUpdSellerHistory.AutoSize = true;
            this.chkBoxUpdSellerHistory.Location = new System.Drawing.Point(157, 58);
            this.chkBoxUpdSellerHistory.Name = "chkBoxUpdSellerHistory";
            this.chkBoxUpdSellerHistory.Size = new System.Drawing.Size(125, 17);
            this.chkBoxUpdSellerHistory.TabIndex = 3;
            this.chkBoxUpdSellerHistory.Text = "Update Seller History";
            this.chkBoxUpdSellerHistory.UseVisualStyleBackColor = true;
            this.chkBoxUpdSellerHistory.CheckedChanged += new System.EventHandler(this.chkBoxUpdSellerHistory_CheckedChanged);
            // 
            // groupBoxHistoryUpdate
            // 
            this.groupBoxHistoryUpdate.Controls.Add(this.lblProductStockHistoryFile);
            this.groupBoxHistoryUpdate.Controls.Add(this.txtBoxProductStockHistoryFile);
            this.groupBoxHistoryUpdate.Controls.Add(this.lblProductInventoryFile);
            this.groupBoxHistoryUpdate.Controls.Add(this.txtBoxProductInventoryFile);
            this.groupBoxHistoryUpdate.Controls.Add(this.btnBrowseProductStockHistoryFile);
            this.groupBoxHistoryUpdate.Controls.Add(this.lblSellerHistoryFile);
            this.groupBoxHistoryUpdate.Controls.Add(this.btnBrowseProductInvFile);
            this.groupBoxHistoryUpdate.Controls.Add(this.txtBoxSellerHistoryFile);
            this.groupBoxHistoryUpdate.Controls.Add(this.btnBrowseSellerHistory);
            this.groupBoxHistoryUpdate.Location = new System.Drawing.Point(12, 73);
            this.groupBoxHistoryUpdate.Name = "groupBoxHistoryUpdate";
            this.groupBoxHistoryUpdate.Size = new System.Drawing.Size(437, 120);
            this.groupBoxHistoryUpdate.TabIndex = 13;
            this.groupBoxHistoryUpdate.TabStop = false;
            // 
            // lblProductStockHistoryFile
            // 
            this.lblProductStockHistoryFile.AutoSize = true;
            this.lblProductStockHistoryFile.Location = new System.Drawing.Point(11, 88);
            this.lblProductStockHistoryFile.Name = "lblProductStockHistoryFile";
            this.lblProductStockHistoryFile.Size = new System.Drawing.Size(110, 13);
            this.lblProductStockHistoryFile.TabIndex = 0;
            this.lblProductStockHistoryFile.Text = "Product Stock History";
            // 
            // txtBoxProductStockHistoryFile
            // 
            this.txtBoxProductStockHistoryFile.Location = new System.Drawing.Point(122, 85);
            this.txtBoxProductStockHistoryFile.Name = "txtBoxProductStockHistoryFile";
            this.txtBoxProductStockHistoryFile.Size = new System.Drawing.Size(220, 20);
            this.txtBoxProductStockHistoryFile.TabIndex = 9;
            // 
            // lblProductInventoryFile
            // 
            this.lblProductInventoryFile.AutoSize = true;
            this.lblProductInventoryFile.Location = new System.Drawing.Point(11, 56);
            this.lblProductInventoryFile.Name = "lblProductInventoryFile";
            this.lblProductInventoryFile.Size = new System.Drawing.Size(110, 13);
            this.lblProductInventoryFile.TabIndex = 0;
            this.lblProductInventoryFile.Text = "Product Inventory File";
            // 
            // txtBoxProductInventoryFile
            // 
            this.txtBoxProductInventoryFile.Location = new System.Drawing.Point(122, 53);
            this.txtBoxProductInventoryFile.Name = "txtBoxProductInventoryFile";
            this.txtBoxProductInventoryFile.Size = new System.Drawing.Size(220, 20);
            this.txtBoxProductInventoryFile.TabIndex = 7;
            // 
            // btnBrowseProductStockHistoryFile
            // 
            this.btnBrowseProductStockHistoryFile.Location = new System.Drawing.Point(348, 83);
            this.btnBrowseProductStockHistoryFile.Name = "btnBrowseProductStockHistoryFile";
            this.btnBrowseProductStockHistoryFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseProductStockHistoryFile.TabIndex = 10;
            this.btnBrowseProductStockHistoryFile.Text = "Bro&wse";
            this.btnBrowseProductStockHistoryFile.UseVisualStyleBackColor = true;
            this.btnBrowseProductStockHistoryFile.Click += new System.EventHandler(this.btnBrowseProductStockHistoryFile_Click);
            // 
            // btnBrowseProductInvFile
            // 
            this.btnBrowseProductInvFile.Location = new System.Drawing.Point(348, 51);
            this.btnBrowseProductInvFile.Name = "btnBrowseProductInvFile";
            this.btnBrowseProductInvFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseProductInvFile.TabIndex = 8;
            this.btnBrowseProductInvFile.Text = "Br&owse";
            this.btnBrowseProductInvFile.UseVisualStyleBackColor = true;
            this.btnBrowseProductInvFile.Click += new System.EventHandler(this.btnBrowseProductInvFile_Click);
            // 
            // chkBoxUpdProductSales
            // 
            this.chkBoxUpdProductSales.AutoSize = true;
            this.chkBoxUpdProductSales.Location = new System.Drawing.Point(288, 58);
            this.chkBoxUpdProductSales.Name = "chkBoxUpdProductSales";
            this.chkBoxUpdProductSales.Size = new System.Drawing.Size(130, 17);
            this.chkBoxUpdProductSales.TabIndex = 4;
            this.chkBoxUpdProductSales.Text = "Update Product Sales";
            this.chkBoxUpdProductSales.UseVisualStyleBackColor = true;
            this.chkBoxUpdProductSales.CheckedChanged += new System.EventHandler(this.chkBoxUpdProductSales_CheckedChanged);
            // 
            // UpdateOrderMasterForm
            // 
            this.AcceptButton = this.btnUpdate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(461, 265);
            this.Controls.Add(this.chkBoxUpdProductSales);
            this.Controls.Add(this.chkBoxUpdSellerHistory);
            this.Controls.Add(this.groupBoxHistoryUpdate);
            this.Controls.Add(this.chkBoxUpdSellerMaster);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtBoxSellerSummaryFile);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UpdateOrderMasterForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update Order Master";
            this.groupBoxHistoryUpdate.ResumeLayout(false);
            this.groupBoxHistoryUpdate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txtBoxSellerSummaryFile;
        private System.Windows.Forms.OpenFileDialog FileDialgSellerSummary;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnClose;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblSellerHistoryFile;
        private System.Windows.Forms.TextBox txtBoxSellerHistoryFile;
        private System.Windows.Forms.Button btnBrowseSellerHistory;
        private System.Windows.Forms.CheckBox chkBoxUpdSellerMaster;
        private System.Windows.Forms.CheckBox chkBoxUpdSellerHistory;
        private System.Windows.Forms.GroupBox groupBoxHistoryUpdate;
        private System.Windows.Forms.CheckBox chkBoxUpdProductSales;
        private System.Windows.Forms.Label lblProductInventoryFile;
        private System.Windows.Forms.TextBox txtBoxProductInventoryFile;
        private System.Windows.Forms.Button btnBrowseProductInvFile;
        private System.Windows.Forms.Label lblProductStockHistoryFile;
        private System.Windows.Forms.TextBox txtBoxProductStockHistoryFile;
        private System.Windows.Forms.Button btnBrowseProductStockHistoryFile;
    }
}