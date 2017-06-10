namespace SalesOrdersReport
{
    partial class AddNewOrderSheetForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddNewOrderSheetForm));
            this.dateTimeOrderSheet = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.chkBoxMarkVendors = new System.Windows.Forms.CheckBox();
            this.radBtnExistingBook = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.grpBoxOrderSheet = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxOutputFolder = new System.Windows.Forms.TextBox();
            this.btnOutFolderBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnCreateOrderSheet = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.prgrssBarProcess = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.grpBoxOrderSheet.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimeOrderSheet
            // 
            this.dateTimeOrderSheet.CustomFormat = "";
            this.dateTimeOrderSheet.Location = new System.Drawing.Point(108, 26);
            this.dateTimeOrderSheet.Name = "dateTimeOrderSheet";
            this.dateTimeOrderSheet.Size = new System.Drawing.Size(331, 20);
            this.dateTimeOrderSheet.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Choose Date";
            // 
            // chkBoxMarkVendors
            // 
            this.chkBoxMarkVendors.AutoSize = true;
            this.chkBoxMarkVendors.Checked = true;
            this.chkBoxMarkVendors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxMarkVendors.Location = new System.Drawing.Point(36, 66);
            this.chkBoxMarkVendors.Name = "chkBoxMarkVendors";
            this.chkBoxMarkVendors.Size = new System.Drawing.Size(138, 17);
            this.chkBoxMarkVendors.TabIndex = 1;
            this.chkBoxMarkVendors.Text = "Mark Vendors For Items";
            this.chkBoxMarkVendors.UseVisualStyleBackColor = true;
            // 
            // radBtnExistingBook
            // 
            this.radBtnExistingBook.AutoSize = true;
            this.radBtnExistingBook.Checked = true;
            this.radBtnExistingBook.Location = new System.Drawing.Point(6, 29);
            this.radBtnExistingBook.Name = "radBtnExistingBook";
            this.radBtnExistingBook.Size = new System.Drawing.Size(143, 17);
            this.radBtnExistingBook.TabIndex = 0;
            this.radBtnExistingBook.TabStop = true;
            this.radBtnExistingBook.Text = "New Sheet in Master File";
            this.radBtnExistingBook.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 52);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(95, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "New Excel File";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // grpBoxOrderSheet
            // 
            this.grpBoxOrderSheet.Controls.Add(this.radioButton2);
            this.grpBoxOrderSheet.Controls.Add(this.radBtnExistingBook);
            this.grpBoxOrderSheet.Enabled = false;
            this.grpBoxOrderSheet.Location = new System.Drawing.Point(36, 89);
            this.grpBoxOrderSheet.Name = "grpBoxOrderSheet";
            this.grpBoxOrderSheet.Size = new System.Drawing.Size(403, 85);
            this.grpBoxOrderSheet.TabIndex = 2;
            this.grpBoxOrderSheet.TabStop = false;
            this.grpBoxOrderSheet.Text = "Add Order Sheet as";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Save File Path";
            // 
            // txtBoxOutputFolder
            // 
            this.txtBoxOutputFolder.Location = new System.Drawing.Point(108, 203);
            this.txtBoxOutputFolder.Name = "txtBoxOutputFolder";
            this.txtBoxOutputFolder.Size = new System.Drawing.Size(250, 20);
            this.txtBoxOutputFolder.TabIndex = 3;
            // 
            // btnOutFolderBrowse
            // 
            this.btnOutFolderBrowse.Location = new System.Drawing.Point(364, 201);
            this.btnOutFolderBrowse.Name = "btnOutFolderBrowse";
            this.btnOutFolderBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnOutFolderBrowse.TabIndex = 4;
            this.btnOutFolderBrowse.Text = "Browse";
            this.btnOutFolderBrowse.UseVisualStyleBackColor = true;
            this.btnOutFolderBrowse.Click += new System.EventHandler(this.btnOutFolderBrowse_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyDocuments;
            // 
            // btnCreateOrderSheet
            // 
            this.btnCreateOrderSheet.Location = new System.Drawing.Point(81, 259);
            this.btnCreateOrderSheet.Name = "btnCreateOrderSheet";
            this.btnCreateOrderSheet.Size = new System.Drawing.Size(124, 42);
            this.btnCreateOrderSheet.TabIndex = 5;
            this.btnCreateOrderSheet.Text = "Create Order Sheet";
            this.btnCreateOrderSheet.UseVisualStyleBackColor = true;
            this.btnCreateOrderSheet.Click += new System.EventHandler(this.btnCreateOrderSheet_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(261, 259);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(124, 42);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Close Window";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // prgrssBarProcess
            // 
            this.prgrssBarProcess.Location = new System.Drawing.Point(330, 312);
            this.prgrssBarProcess.Name = "prgrssBarProcess";
            this.prgrssBarProcess.Size = new System.Drawing.Size(100, 23);
            this.prgrssBarProcess.TabIndex = 7;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(437, 321);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(21, 13);
            this.lblProgress.TabIndex = 8;
            this.lblProgress.Text = "0%";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // AddNewOrderSheetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(486, 347);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.prgrssBarProcess);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreateOrderSheet);
            this.Controls.Add(this.btnOutFolderBrowse);
            this.Controls.Add(this.txtBoxOutputFolder);
            this.Controls.Add(this.grpBoxOrderSheet);
            this.Controls.Add(this.chkBoxMarkVendors);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimeOrderSheet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AddNewOrderSheetForm";
            this.ShowInTaskbar = false;
            this.Text = "Add New Order Sheet";
            this.TopMost = true;
            this.grpBoxOrderSheet.ResumeLayout(false);
            this.grpBoxOrderSheet.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimeOrderSheet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkBoxMarkVendors;
        private System.Windows.Forms.RadioButton radBtnExistingBook;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.GroupBox grpBoxOrderSheet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxOutputFolder;
        private System.Windows.Forms.Button btnOutFolderBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnCreateOrderSheet;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar prgrssBarProcess;
        private System.Windows.Forms.Label lblProgress;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}