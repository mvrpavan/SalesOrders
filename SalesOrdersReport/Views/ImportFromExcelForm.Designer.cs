namespace SalesOrdersReport.Views
{
    partial class ImportFromExcelForm
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
            this.btnImportFrmExcelBrowse = new System.Windows.Forms.Button();
            this.txtImportFrmExclFilePath = new System.Windows.Forms.TextBox();
            this.btnImportFrmExclUploadFile = new System.Windows.Forms.Button();
            this.OFDImportExcelFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.bgWorkerImportExcel = new System.ComponentModel.BackgroundWorker();
            this.chkListBoxDataToImport = new System.Windows.Forms.CheckedListBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnImportFrmExcelBrowse
            // 
            this.btnImportFrmExcelBrowse.Location = new System.Drawing.Point(397, 25);
            this.btnImportFrmExcelBrowse.Name = "btnImportFrmExcelBrowse";
            this.btnImportFrmExcelBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnImportFrmExcelBrowse.TabIndex = 0;
            this.btnImportFrmExcelBrowse.Text = "Browse";
            this.btnImportFrmExcelBrowse.UseVisualStyleBackColor = true;
            this.btnImportFrmExcelBrowse.Click += new System.EventHandler(this.btnImportFrmExcelBrowse_Click);
            // 
            // txtImportFrmExclFilePath
            // 
            this.txtImportFrmExclFilePath.Location = new System.Drawing.Point(73, 27);
            this.txtImportFrmExclFilePath.Name = "txtImportFrmExclFilePath";
            this.txtImportFrmExclFilePath.Size = new System.Drawing.Size(319, 20);
            this.txtImportFrmExclFilePath.TabIndex = 1;
            // 
            // btnImportFrmExclUploadFile
            // 
            this.btnImportFrmExclUploadFile.Location = new System.Drawing.Point(70, 185);
            this.btnImportFrmExclUploadFile.Name = "btnImportFrmExclUploadFile";
            this.btnImportFrmExclUploadFile.Size = new System.Drawing.Size(107, 23);
            this.btnImportFrmExclUploadFile.TabIndex = 2;
            this.btnImportFrmExclUploadFile.Text = "Import Data";
            this.btnImportFrmExclUploadFile.UseVisualStyleBackColor = true;
            this.btnImportFrmExclUploadFile.Click += new System.EventHandler(this.btnImportFrmExclUploadFile_Click);
            // 
            // bgWorkerImportExcel
            // 
            this.bgWorkerImportExcel.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerImportExcel_DoWork);
            this.bgWorkerImportExcel.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerImportExcel_RunWorkerCompleted);
            // 
            // chkListBoxDataToImport
            // 
            this.chkListBoxDataToImport.FormattingEnabled = true;
            this.chkListBoxDataToImport.Location = new System.Drawing.Point(13, 88);
            this.chkListBoxDataToImport.Name = "chkListBoxDataToImport";
            this.chkListBoxDataToImport.Size = new System.Drawing.Size(459, 79);
            this.chkListBoxDataToImport.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(284, 185);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Import File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Select Data to Import";
            // 
            // ImportFromExcelForm
            // 
            this.AcceptButton = this.btnImportFrmExclUploadFile;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(484, 230);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkListBoxDataToImport);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnImportFrmExclUploadFile);
            this.Controls.Add(this.txtImportFrmExclFilePath);
            this.Controls.Add(this.btnImportFrmExcelBrowse);
            this.Name = "ImportFromExcelForm";
            this.ShowIcon = false;
            this.Text = "Import From Excel Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

 
        private System.Windows.Forms.Label lblImportFrmExcelValidateErrMsg;
        private System.Windows.Forms.Button btnImportFrmExcelBrowse;
        private System.Windows.Forms.TextBox txtImportFrmExclFilePath;
        private System.Windows.Forms.Button btnImportFrmExclUploadFile;
        private System.Windows.Forms.OpenFileDialog OFDImportExcelFileDialog;
        private System.ComponentModel.BackgroundWorker bgWorkerImportExcel;
        private System.Windows.Forms.CheckedListBox chkListBoxDataToImport;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}