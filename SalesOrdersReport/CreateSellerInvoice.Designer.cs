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
            this.txtBoxOtherFile = new System.Windows.Forms.TextBox();
            this.btnBrowseOtherFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lblProgress = new System.Windows.Forms.Label();
            this.chkBoxUseOrdQty = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkBoxCreateInvoice = new System.Windows.Forms.CheckBox();
            this.chkBoxCreateQuotation = new System.Windows.Forms.CheckBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
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
            this.chkBoxCreateSummary.Location = new System.Drawing.Point(50, 60);
            this.chkBoxCreateSummary.Name = "chkBoxCreateSummary";
            this.chkBoxCreateSummary.Size = new System.Drawing.Size(134, 17);
            this.chkBoxCreateSummary.TabIndex = 2;
            this.chkBoxCreateSummary.Text = "Create Summary Sheet";
            this.chkBoxCreateSummary.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(275, 235);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(124, 42);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Close Window";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCreateInvoice
            // 
            this.btnCreateInvoice.Location = new System.Drawing.Point(95, 235);
            this.btnCreateInvoice.Name = "btnCreateInvoice";
            this.btnCreateInvoice.Size = new System.Drawing.Size(124, 42);
            this.btnCreateInvoice.TabIndex = 6;
            this.btnCreateInvoice.Text = "Generate Invoice";
            this.btnCreateInvoice.UseVisualStyleBackColor = true;
            this.btnCreateInvoice.Click += new System.EventHandler(this.btnCreateInvoice_Click);
            // 
            // btnOutFolderBrowse
            // 
            this.btnOutFolderBrowse.Location = new System.Drawing.Point(378, 189);
            this.btnOutFolderBrowse.Name = "btnOutFolderBrowse";
            this.btnOutFolderBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnOutFolderBrowse.TabIndex = 5;
            this.btnOutFolderBrowse.Text = "Browse";
            this.btnOutFolderBrowse.UseVisualStyleBackColor = true;
            this.btnOutFolderBrowse.Click += new System.EventHandler(this.btnOutFolderBrowse_Click);
            // 
            // txtBoxOutputFolder
            // 
            this.txtBoxOutputFolder.Location = new System.Drawing.Point(122, 191);
            this.txtBoxOutputFolder.Name = "txtBoxOutputFolder";
            this.txtBoxOutputFolder.Size = new System.Drawing.Size(250, 20);
            this.txtBoxOutputFolder.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Save File Path";
            // 
            // txtBoxOtherFile
            // 
            this.txtBoxOtherFile.Location = new System.Drawing.Point(122, 150);
            this.txtBoxOtherFile.Name = "txtBoxOtherFile";
            this.txtBoxOtherFile.Size = new System.Drawing.Size(250, 20);
            this.txtBoxOtherFile.TabIndex = 0;
            // 
            // btnBrowseOtherFile
            // 
            this.btnBrowseOtherFile.Location = new System.Drawing.Point(378, 148);
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
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
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
            // chkBoxUseOrdQty
            // 
            this.chkBoxUseOrdQty.AutoSize = true;
            this.chkBoxUseOrdQty.Location = new System.Drawing.Point(250, 60);
            this.chkBoxUseOrdQty.Name = "chkBoxUseOrdQty";
            this.chkBoxUseOrdQty.Size = new System.Drawing.Size(138, 17);
            this.chkBoxUseOrdQty.TabIndex = 3;
            this.chkBoxUseOrdQty.Text = "Use Order Qty For Total";
            this.chkBoxUseOrdQty.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Order Sheet File";
            // 
            // chkBoxCreateInvoice
            // 
            this.chkBoxCreateInvoice.AutoSize = true;
            this.chkBoxCreateInvoice.Checked = true;
            this.chkBoxCreateInvoice.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxCreateInvoice.Location = new System.Drawing.Point(50, 94);
            this.chkBoxCreateInvoice.Name = "chkBoxCreateInvoice";
            this.chkBoxCreateInvoice.Size = new System.Drawing.Size(95, 17);
            this.chkBoxCreateInvoice.TabIndex = 3;
            this.chkBoxCreateInvoice.Text = "Create Invoice";
            this.chkBoxCreateInvoice.UseVisualStyleBackColor = true;
            this.chkBoxCreateInvoice.CheckedChanged += new System.EventHandler(this.chkBoxCreateInvoice_CheckedChanged);
            // 
            // chkBoxCreateQuotation
            // 
            this.chkBoxCreateQuotation.AutoSize = true;
            this.chkBoxCreateQuotation.Checked = true;
            this.chkBoxCreateQuotation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxCreateQuotation.Location = new System.Drawing.Point(50, 117);
            this.chkBoxCreateQuotation.Name = "chkBoxCreateQuotation";
            this.chkBoxCreateQuotation.Size = new System.Drawing.Size(106, 17);
            this.chkBoxCreateQuotation.TabIndex = 3;
            this.chkBoxCreateQuotation.Text = "Create Quotation";
            this.chkBoxCreateQuotation.UseVisualStyleBackColor = true;
            this.chkBoxCreateQuotation.CheckedChanged += new System.EventHandler(this.chkBoxCreateQuotation_CheckedChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(95, 315);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 314);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Status:";
            // 
            // CreateSellerInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(484, 353);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.chkBoxCreateQuotation);
            this.Controls.Add(this.chkBoxCreateInvoice);
            this.Controls.Add(this.chkBoxUseOrdQty);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.txtBoxOtherFile);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnBrowseOtherFile);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreateInvoice);
            this.Controls.Add(this.btnOutFolderBrowse);
            this.Controls.Add(this.txtBoxOutputFolder);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkBoxCreateSummary);
            this.Controls.Add(this.dateTimeInvoice);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CreateSellerInvoice";
            this.ShowInTaskbar = false;
            this.Text = "Generate Invoice";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CreateSellerInvoice_FormClosing);
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
        private System.Windows.Forms.TextBox txtBoxOtherFile;
        private System.Windows.Forms.Button btnBrowseOtherFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.CheckBox chkBoxUseOrdQty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkBoxCreateInvoice;
        private System.Windows.Forms.CheckBox chkBoxCreateQuotation;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label3;
    }
}