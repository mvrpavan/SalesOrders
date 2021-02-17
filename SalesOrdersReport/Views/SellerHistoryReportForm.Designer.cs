namespace SalesOrdersReport.Views
{
    partial class SellerHistoryReportForm
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
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtBoxSellerHistoryFilePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreateReport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cmbBoxSellerList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.chkBoxDateFilter = new System.Windows.Forms.CheckBox();
            this.FileDialogSellerHistory = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBoxSaveFolderPath = new System.Windows.Forms.TextBox();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialogOutputFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.bgWorkerSellerHistory = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Seller History File";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(410, 22);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtBoxSellerHistoryFilePath
            // 
            this.txtBoxSellerHistoryFilePath.Location = new System.Drawing.Point(154, 24);
            this.txtBoxSellerHistoryFilePath.Name = "txtBoxSellerHistoryFilePath";
            this.txtBoxSellerHistoryFilePath.Size = new System.Drawing.Size(250, 20);
            this.txtBoxSellerHistoryFilePath.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Select Seller Name";
            // 
            // btnCreateReport
            // 
            this.btnCreateReport.Location = new System.Drawing.Point(154, 222);
            this.btnCreateReport.Name = "btnCreateReport";
            this.btnCreateReport.Size = new System.Drawing.Size(94, 23);
            this.btnCreateReport.TabIndex = 9;
            this.btnCreateReport.Text = "Create &Report";
            this.btnCreateReport.UseVisualStyleBackColor = true;
            this.btnCreateReport.Click += new System.EventHandler(this.btnCreateReport_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(299, 222);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cmbBoxSellerList
            // 
            this.cmbBoxSellerList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbBoxSellerList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBoxSellerList.FormattingEnabled = true;
            this.cmbBoxSellerList.Location = new System.Drawing.Point(154, 62);
            this.cmbBoxSellerList.Name = "cmbBoxSellerList";
            this.cmbBoxSellerList.Size = new System.Drawing.Size(250, 21);
            this.cmbBoxSellerList.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Start Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(246, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "End Date";
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerStart.Location = new System.Drawing.Point(123, 35);
            this.dateTimePickerStart.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(100, 20);
            this.dateTimePickerStart.TabIndex = 5;
            this.dateTimePickerStart.Value = new System.DateTime(2017, 7, 7, 0, 0, 0, 0);
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerEnd.Location = new System.Drawing.Point(304, 35);
            this.dateTimePickerEnd.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(100, 20);
            this.dateTimePickerEnd.TabIndex = 6;
            this.dateTimePickerEnd.Value = new System.DateTime(2017, 7, 7, 0, 0, 0, 0);
            // 
            // chkBoxDateFilter
            // 
            this.chkBoxDateFilter.AutoSize = true;
            this.chkBoxDateFilter.Location = new System.Drawing.Point(19, 9);
            this.chkBoxDateFilter.Name = "chkBoxDateFilter";
            this.chkBoxDateFilter.Size = new System.Drawing.Size(74, 17);
            this.chkBoxDateFilter.TabIndex = 4;
            this.chkBoxDateFilter.Text = "Date Filter";
            this.chkBoxDateFilter.UseVisualStyleBackColor = true;
            this.chkBoxDateFilter.CheckedChanged += new System.EventHandler(this.chkBoxDateFilter_CheckedChanged);
            // 
            // FileDialogSellerHistory
            // 
            this.FileDialogSellerHistory.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateTimePickerStart);
            this.groupBox1.Controls.Add(this.chkBoxDateFilter);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dateTimePickerEnd);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(31, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 68);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Save Folder Path";
            // 
            // txtBoxSaveFolderPath
            // 
            this.txtBoxSaveFolderPath.Location = new System.Drawing.Point(154, 174);
            this.txtBoxSaveFolderPath.Name = "txtBoxSaveFolderPath";
            this.txtBoxSaveFolderPath.Size = new System.Drawing.Size(250, 20);
            this.txtBoxSaveFolderPath.TabIndex = 7;
            // 
            // btnBrowseFolder
            // 
            this.btnBrowseFolder.Location = new System.Drawing.Point(410, 172);
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseFolder.TabIndex = 8;
            this.btnBrowseFolder.Text = "B&rowse";
            this.btnBrowseFolder.UseVisualStyleBackColor = true;
            this.btnBrowseFolder.Click += new System.EventHandler(this.btnBrowseFolder_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 257);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Status: ";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(89, 257);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(35, 13);
            this.lblStatus.TabIndex = 16;
            this.lblStatus.Text = "status";
            // 
            // bgWorkerSellerHistory
            // 
            this.bgWorkerSellerHistory.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerSellerHistory_DoWork);
            this.bgWorkerSellerHistory.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorkerSellerHistory_ProgressChanged);
            this.bgWorkerSellerHistory.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerSellerHistory_RunWorkerCompleted);
            // 
            // SellerHistoryReportForm
            // 
            this.AcceptButton = this.btnCreateReport;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(514, 282);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnBrowseFolder);
            this.Controls.Add(this.txtBoxSaveFolderPath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCreateReport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbBoxSellerList);
            this.Controls.Add(this.txtBoxSellerHistoryFilePath);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label1);
            this.Name = "SellerHistoryReportForm";
            this.Text = "Seller Report";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtBoxSellerHistoryFilePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCreateReport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cmbBoxSellerList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.CheckBox chkBoxDateFilter;
        private System.Windows.Forms.OpenFileDialog FileDialogSellerHistory;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBoxSaveFolderPath;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogOutputFolder;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblStatus;
        private System.ComponentModel.BackgroundWorker bgWorkerSellerHistory;
    }
}