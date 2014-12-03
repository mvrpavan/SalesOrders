namespace SalesOrdersReport
{
    partial class CreateSellerInvoice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateSellerInvoice));
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimeInvoice = new System.Windows.Forms.DateTimePicker();
            this.chkBoxCreateSummary = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCreateInvoice = new System.Windows.Forms.Button();
            this.btnOutFolderBrowse = new System.Windows.Forms.Button();
            this.txtBoxOutputFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radBtnOrderFromOtherFile = new System.Windows.Forms.RadioButton();
            this.radBtnOrderFromMasterFile = new System.Windows.Forms.RadioButton();
            this.txtBoxOtherFile = new System.Windows.Forms.TextBox();
            this.btnBrowseOtherFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoxInvoiceStartNumber = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lblProgress = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose Date";
            // 
            // dateTimeInvoice
            // 
            this.dateTimeInvoice.Location = new System.Drawing.Point(122, 22);
            this.dateTimeInvoice.Name = "dateTimeInvoice";
            this.dateTimeInvoice.Size = new System.Drawing.Size(331, 20);
            this.dateTimeInvoice.TabIndex = 0;
            // 
            // chkBoxCreateSummary
            // 
            this.chkBoxCreateSummary.AutoSize = true;
            this.chkBoxCreateSummary.Checked = true;
            this.chkBoxCreateSummary.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxCreateSummary.Location = new System.Drawing.Point(50, 85);
            this.chkBoxCreateSummary.Name = "chkBoxCreateSummary";
            this.chkBoxCreateSummary.Size = new System.Drawing.Size(134, 17);
            this.chkBoxCreateSummary.TabIndex = 2;
            this.chkBoxCreateSummary.Text = "Create Summary Sheet";
            this.chkBoxCreateSummary.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(275, 247);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(124, 42);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Close Window";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCreateInvoice
            // 
            this.btnCreateInvoice.Location = new System.Drawing.Point(95, 247);
            this.btnCreateInvoice.Name = "btnCreateInvoice";
            this.btnCreateInvoice.Size = new System.Drawing.Size(124, 42);
            this.btnCreateInvoice.TabIndex = 6;
            this.btnCreateInvoice.Text = "Generate Invoice";
            this.btnCreateInvoice.UseVisualStyleBackColor = true;
            this.btnCreateInvoice.Click += new System.EventHandler(this.btnCreateInvoice_Click);
            // 
            // btnOutFolderBrowse
            // 
            this.btnOutFolderBrowse.Location = new System.Drawing.Point(378, 201);
            this.btnOutFolderBrowse.Name = "btnOutFolderBrowse";
            this.btnOutFolderBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnOutFolderBrowse.TabIndex = 5;
            this.btnOutFolderBrowse.Text = "Browse";
            this.btnOutFolderBrowse.UseVisualStyleBackColor = true;
            this.btnOutFolderBrowse.Click += new System.EventHandler(this.btnOutFolderBrowse_Click);
            // 
            // txtBoxOutputFolder
            // 
            this.txtBoxOutputFolder.Location = new System.Drawing.Point(122, 203);
            this.txtBoxOutputFolder.Name = "txtBoxOutputFolder";
            this.txtBoxOutputFolder.Size = new System.Drawing.Size(250, 20);
            this.txtBoxOutputFolder.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Save File Path";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radBtnOrderFromOtherFile);
            this.groupBox1.Controls.Add(this.radBtnOrderFromMasterFile);
            this.groupBox1.Controls.Add(this.txtBoxOtherFile);
            this.groupBox1.Controls.Add(this.btnBrowseOtherFile);
            this.groupBox1.Location = new System.Drawing.Point(50, 109);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(403, 77);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose Order Sheet from";
            // 
            // radBtnOrderFromOtherFile
            // 
            this.radBtnOrderFromOtherFile.AutoSize = true;
            this.radBtnOrderFromOtherFile.Location = new System.Drawing.Point(6, 43);
            this.radBtnOrderFromOtherFile.Name = "radBtnOrderFromOtherFile";
            this.radBtnOrderFromOtherFile.Size = new System.Drawing.Size(70, 17);
            this.radBtnOrderFromOtherFile.TabIndex = 1;
            this.radBtnOrderFromOtherFile.Text = "Other File";
            this.radBtnOrderFromOtherFile.UseVisualStyleBackColor = true;
            this.radBtnOrderFromOtherFile.CheckedChanged += new System.EventHandler(this.radBtnOrderFromOtherFile_CheckedChanged);
            // 
            // radBtnOrderFromMasterFile
            // 
            this.radBtnOrderFromMasterFile.AutoSize = true;
            this.radBtnOrderFromMasterFile.Checked = true;
            this.radBtnOrderFromMasterFile.Location = new System.Drawing.Point(7, 20);
            this.radBtnOrderFromMasterFile.Name = "radBtnOrderFromMasterFile";
            this.radBtnOrderFromMasterFile.Size = new System.Drawing.Size(76, 17);
            this.radBtnOrderFromMasterFile.TabIndex = 0;
            this.radBtnOrderFromMasterFile.TabStop = true;
            this.radBtnOrderFromMasterFile.Text = "Master File";
            this.radBtnOrderFromMasterFile.UseVisualStyleBackColor = true;
            this.radBtnOrderFromMasterFile.CheckedChanged += new System.EventHandler(this.radBtnOrderFromMasterFile_CheckedChanged);
            // 
            // txtBoxOtherFile
            // 
            this.txtBoxOtherFile.Enabled = false;
            this.txtBoxOtherFile.Location = new System.Drawing.Point(82, 43);
            this.txtBoxOtherFile.Name = "txtBoxOtherFile";
            this.txtBoxOtherFile.Size = new System.Drawing.Size(229, 20);
            this.txtBoxOtherFile.TabIndex = 0;
            // 
            // btnBrowseOtherFile
            // 
            this.btnBrowseOtherFile.Enabled = false;
            this.btnBrowseOtherFile.Location = new System.Drawing.Point(322, 41);
            this.btnBrowseOtherFile.Name = "btnBrowseOtherFile";
            this.btnBrowseOtherFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseOtherFile.TabIndex = 1;
            this.btnBrowseOtherFile.Text = "Browse";
            this.btnBrowseOtherFile.UseVisualStyleBackColor = true;
            this.btnBrowseOtherFile.Click += new System.EventHandler(this.btnBrowseOtherFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Invoice Number";
            // 
            // txtBoxInvoiceStartNumber
            // 
            this.txtBoxInvoiceStartNumber.Location = new System.Drawing.Point(132, 52);
            this.txtBoxInvoiceStartNumber.Name = "txtBoxInvoiceStartNumber";
            this.txtBoxInvoiceStartNumber.Size = new System.Drawing.Size(87, 20);
            this.txtBoxInvoiceStartNumber.TabIndex = 1;
            this.txtBoxInvoiceStartNumber.Text = "1";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(335, 315);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 15;
            this.progressBar1.UseWaitCursor = true;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.BackColor = System.Drawing.Color.Transparent;
            this.lblProgress.Location = new System.Drawing.Point(445, 321);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(21, 13);
            this.lblProgress.TabIndex = 16;
            this.lblProgress.Text = "0%";
            // 
            // CreateSellerInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 353);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.txtBoxInvoiceStartNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreateInvoice);
            this.Controls.Add(this.btnOutFolderBrowse);
            this.Controls.Add(this.txtBoxOutputFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkBoxCreateSummary);
            this.Controls.Add(this.dateTimeInvoice);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateSellerInvoice";
            this.Text = "Generate Invoice";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimeInvoice;
        private System.Windows.Forms.CheckBox chkBoxCreateSummary;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCreateInvoice;
        private System.Windows.Forms.Button btnOutFolderBrowse;
        private System.Windows.Forms.TextBox txtBoxOutputFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radBtnOrderFromMasterFile;
        private System.Windows.Forms.RadioButton radBtnOrderFromOtherFile;
        private System.Windows.Forms.TextBox txtBoxOtherFile;
        private System.Windows.Forms.Button btnBrowseOtherFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBoxInvoiceStartNumber;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblProgress;
    }
}