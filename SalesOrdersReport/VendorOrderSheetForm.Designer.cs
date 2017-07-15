namespace SalesOrdersReport
{
    partial class VendorOrderSheetForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxSaveFolderPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.dateTimePickerVendorOrderDate = new System.Windows.Forms.DateTimePicker();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.bgWorkerCreateVendorOrder = new System.ComponentModel.BackgroundWorker();
            this.txtBoxProductStockHistoryFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBrowseProductStockHistoryFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbBoxTimePeriodUnits = new System.Windows.Forms.ComboBox();
            this.numUpDownTimePeriod = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.grpBoxProductDetails = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBoxProductInventoryFile = new System.Windows.Forms.TextBox();
            this.btnBrowseProductInventoryFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownTimePeriod)).BeginInit();
            this.grpBoxProductDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Date";
            // 
            // txtBoxSaveFolderPath
            // 
            this.txtBoxSaveFolderPath.Location = new System.Drawing.Point(106, 181);
            this.txtBoxSaveFolderPath.Name = "txtBoxSaveFolderPath";
            this.txtBoxSaveFolderPath.Size = new System.Drawing.Size(250, 20);
            this.txtBoxSaveFolderPath.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Save File Path";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(366, 179);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 9;
            this.btnBrowse.Text = "Br&owse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // dateTimePickerVendorOrderDate
            // 
            this.dateTimePickerVendorOrderDate.Location = new System.Drawing.Point(106, 23);
            this.dateTimePickerVendorOrderDate.Name = "dateTimePickerVendorOrderDate";
            this.dateTimePickerVendorOrderDate.Size = new System.Drawing.Size(250, 20);
            this.dateTimePickerVendorOrderDate.TabIndex = 1;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(103, 215);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(98, 37);
            this.btnCreate.TabIndex = 10;
            this.btnCreate.Text = "Create Order &Sheet";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(258, 215);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(98, 37);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 266);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Status:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(66, 266);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(35, 13);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "status";
            // 
            // bgWorkerCreateVendorOrder
            // 
            this.bgWorkerCreateVendorOrder.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerCreateVendorOrder_DoWork);
            this.bgWorkerCreateVendorOrder.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorkerCreateVendorOrder_ProgressChanged);
            this.bgWorkerCreateVendorOrder.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerCreateVendorOrder_RunWorkerCompleted);
            // 
            // txtBoxProductStockHistoryFile
            // 
            this.txtBoxProductStockHistoryFile.Location = new System.Drawing.Point(94, 58);
            this.txtBoxProductStockHistoryFile.Name = "txtBoxProductStockHistoryFile";
            this.txtBoxProductStockHistoryFile.Size = new System.Drawing.Size(250, 20);
            this.txtBoxProductStockHistoryFile.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Stock History File";
            // 
            // btnBrowseProductStockHistoryFile
            // 
            this.btnBrowseProductStockHistoryFile.Location = new System.Drawing.Point(354, 56);
            this.btnBrowseProductStockHistoryFile.Name = "btnBrowseProductStockHistoryFile";
            this.btnBrowseProductStockHistoryFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseProductStockHistoryFile.TabIndex = 5;
            this.btnBrowseProductStockHistoryFile.Text = "&Browse";
            this.btnBrowseProductStockHistoryFile.UseVisualStyleBackColor = true;
            this.btnBrowseProductStockHistoryFile.Click += new System.EventHandler(this.btnBrowseProductStockHistoryFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Include Past ";
            // 
            // cmbBoxTimePeriodUnits
            // 
            this.cmbBoxTimePeriodUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxTimePeriodUnits.FormattingEnabled = true;
            this.cmbBoxTimePeriodUnits.Location = new System.Drawing.Point(161, 89);
            this.cmbBoxTimePeriodUnits.Name = "cmbBoxTimePeriodUnits";
            this.cmbBoxTimePeriodUnits.Size = new System.Drawing.Size(77, 21);
            this.cmbBoxTimePeriodUnits.TabIndex = 7;
            // 
            // numUpDownTimePeriod
            // 
            this.numUpDownTimePeriod.Location = new System.Drawing.Point(94, 90);
            this.numUpDownTimePeriod.Name = "numUpDownTimePeriod";
            this.numUpDownTimePeriod.Size = new System.Drawing.Size(61, 20);
            this.numUpDownTimePeriod.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(244, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "of Sales";
            // 
            // grpBoxProductDetails
            // 
            this.grpBoxProductDetails.Controls.Add(this.label7);
            this.grpBoxProductDetails.Controls.Add(this.label4);
            this.grpBoxProductDetails.Controls.Add(this.txtBoxProductInventoryFile);
            this.grpBoxProductDetails.Controls.Add(this.numUpDownTimePeriod);
            this.grpBoxProductDetails.Controls.Add(this.txtBoxProductStockHistoryFile);
            this.grpBoxProductDetails.Controls.Add(this.btnBrowseProductInventoryFile);
            this.grpBoxProductDetails.Controls.Add(this.btnBrowseProductStockHistoryFile);
            this.grpBoxProductDetails.Controls.Add(this.label5);
            this.grpBoxProductDetails.Controls.Add(this.cmbBoxTimePeriodUnits);
            this.grpBoxProductDetails.Controls.Add(this.label6);
            this.grpBoxProductDetails.Location = new System.Drawing.Point(12, 49);
            this.grpBoxProductDetails.Name = "grpBoxProductDetails";
            this.grpBoxProductDetails.Size = new System.Drawing.Size(438, 124);
            this.grpBoxProductDetails.TabIndex = 2;
            this.grpBoxProductDetails.TabStop = false;
            this.grpBoxProductDetails.Text = "Product Details";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Inventory File";
            // 
            // txtBoxProductInventoryFile
            // 
            this.txtBoxProductInventoryFile.Location = new System.Drawing.Point(94, 25);
            this.txtBoxProductInventoryFile.Name = "txtBoxProductInventoryFile";
            this.txtBoxProductInventoryFile.Size = new System.Drawing.Size(250, 20);
            this.txtBoxProductInventoryFile.TabIndex = 2;
            // 
            // btnBrowseProductInventoryFile
            // 
            this.btnBrowseProductInventoryFile.Location = new System.Drawing.Point(354, 23);
            this.btnBrowseProductInventoryFile.Name = "btnBrowseProductInventoryFile";
            this.btnBrowseProductInventoryFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseProductInventoryFile.TabIndex = 3;
            this.btnBrowseProductInventoryFile.Text = "B&rowse";
            this.btnBrowseProductInventoryFile.UseVisualStyleBackColor = true;
            this.btnBrowseProductInventoryFile.Click += new System.EventHandler(this.btnBrowseProductInventoryFile_Click);
            // 
            // VendorOrderSheetForm
            // 
            this.AcceptButton = this.btnCreate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(462, 290);
            this.Controls.Add(this.grpBoxProductDetails);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.dateTimePickerVendorOrderDate);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBoxSaveFolderPath);
            this.Controls.Add(this.label1);
            this.Name = "VendorOrderSheetForm";
            this.Text = "Create Vendor Order Sheet";
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownTimePeriod)).EndInit();
            this.grpBoxProductDetails.ResumeLayout(false);
            this.grpBoxProductDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxSaveFolderPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.DateTimePicker dateTimePickerVendorOrderDate;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStatus;
        private System.ComponentModel.BackgroundWorker bgWorkerCreateVendorOrder;
        private System.Windows.Forms.TextBox txtBoxProductStockHistoryFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBrowseProductStockHistoryFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbBoxTimePeriodUnits;
        private System.Windows.Forms.NumericUpDown numUpDownTimePeriod;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox grpBoxProductDetails;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBoxProductInventoryFile;
        private System.Windows.Forms.Button btnBrowseProductInventoryFile;
    }
}