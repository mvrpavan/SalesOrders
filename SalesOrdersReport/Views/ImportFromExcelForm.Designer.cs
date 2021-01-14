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
            this.bgWorkerImportExcelCreatePO = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // btnImportFrmExcelBrowse
            // 
            this.btnImportFrmExcelBrowse.Location = new System.Drawing.Point(242, 27);
            this.btnImportFrmExcelBrowse.Name = "btnImportFrmExcelBrowse";
            this.btnImportFrmExcelBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnImportFrmExcelBrowse.TabIndex = 0;
            this.btnImportFrmExcelBrowse.Text = "Browse";
            this.btnImportFrmExcelBrowse.UseVisualStyleBackColor = true;
            this.btnImportFrmExcelBrowse.Click += new System.EventHandler(this.btnImportFrmExcelBrowse_Click);
            // 
            // txtImportFrmExclFilePath
            // 
            this.txtImportFrmExclFilePath.Location = new System.Drawing.Point(12, 27);
            this.txtImportFrmExclFilePath.Name = "txtImportFrmExclFilePath";
            this.txtImportFrmExclFilePath.Size = new System.Drawing.Size(224, 20);
            this.txtImportFrmExclFilePath.TabIndex = 1;
            // 
            // btnImportFrmExclUploadFile
            // 
            this.btnImportFrmExclUploadFile.Location = new System.Drawing.Point(67, 74);
            this.btnImportFrmExclUploadFile.Name = "btnImportFrmExclUploadFile";
            this.btnImportFrmExclUploadFile.Size = new System.Drawing.Size(153, 23);
            this.btnImportFrmExclUploadFile.TabIndex = 2;
            this.btnImportFrmExclUploadFile.Text = "Fill Customer DB";
            this.btnImportFrmExclUploadFile.UseVisualStyleBackColor = true;
            this.btnImportFrmExclUploadFile.Click += new System.EventHandler(this.btnImportFrmExclUploadFile_Click);
            // 
            // bgWorkerImportExcelCreatePO
            // 
            this.bgWorkerImportExcelCreatePO.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerImportExcelCreatePO_DoWork);
            this.bgWorkerImportExcelCreatePO.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerImportExcelCreatePO_RunWorkerCompleted);
            // 
            // ImportFromExcelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 144);
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
        private System.ComponentModel.BackgroundWorker bgWorkerImportExcelCreatePO;
    }
}