namespace SalesOrdersReport.Views
{
    partial class ExportToExcelForm
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
            this.btnExportToExcelBrowse = new System.Windows.Forms.Button();
            this.txtExportToExcelFilePath = new System.Windows.Forms.TextBox();
            this.btnExportToExcelFile = new System.Windows.Forms.Button();
            this.bgWorkerExportExcel = new System.ComponentModel.BackgroundWorker();
            this.chkListBoxDataToExport = new System.Windows.Forms.CheckedListBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblExport = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.saveFileDialogExportToExcel = new System.Windows.Forms.SaveFileDialog();
            this.chkBoxAppend = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialogExport = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // btnExportToExcelBrowse
            // 
            this.btnExportToExcelBrowse.Location = new System.Drawing.Point(397, 25);
            this.btnExportToExcelBrowse.Name = "btnExportToExcelBrowse";
            this.btnExportToExcelBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnExportToExcelBrowse.TabIndex = 0;
            this.btnExportToExcelBrowse.Text = "Browse";
            this.btnExportToExcelBrowse.UseVisualStyleBackColor = true;
            this.btnExportToExcelBrowse.Click += new System.EventHandler(this.btnExportToExcelBrowse_Click);
            // 
            // txtExportToExcelFilePath
            // 
            this.txtExportToExcelFilePath.Location = new System.Drawing.Point(97, 27);
            this.txtExportToExcelFilePath.Name = "txtExportToExcelFilePath";
            this.txtExportToExcelFilePath.Size = new System.Drawing.Size(295, 20);
            this.txtExportToExcelFilePath.TabIndex = 1;
            // 
            // btnExportToExcelFile
            // 
            this.btnExportToExcelFile.Location = new System.Drawing.Point(69, 200);
            this.btnExportToExcelFile.Name = "btnExportToExcelFile";
            this.btnExportToExcelFile.Size = new System.Drawing.Size(107, 23);
            this.btnExportToExcelFile.TabIndex = 2;
            this.btnExportToExcelFile.Text = "Export Data";
            this.btnExportToExcelFile.UseVisualStyleBackColor = true;
            this.btnExportToExcelFile.Click += new System.EventHandler(this.btnExportToExcelFile_Click);
            // 
            // bgWorkerExportExcel
            // 
            this.bgWorkerExportExcel.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerExportExcel_DoWork);
            this.bgWorkerExportExcel.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerExportExcel_RunWorkerCompleted);
            // 
            // chkListBoxDataToExport
            // 
            this.chkListBoxDataToExport.FormattingEnabled = true;
            this.chkListBoxDataToExport.Location = new System.Drawing.Point(12, 103);
            this.chkListBoxDataToExport.Name = "chkListBoxDataToExport";
            this.chkListBoxDataToExport.Size = new System.Drawing.Size(459, 79);
            this.chkListBoxDataToExport.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(283, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblExport
            // 
            this.lblExport.AutoSize = true;
            this.lblExport.Location = new System.Drawing.Point(9, 30);
            this.lblExport.Name = "lblExport";
            this.lblExport.Size = new System.Drawing.Size(68, 13);
            this.lblExport.TabIndex = 4;
            this.lblExport.Text = "Export to File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Select Data to Export";
            // 
            // chkBoxAppend
            // 
            this.chkBoxAppend.AutoSize = true;
            this.chkBoxAppend.Location = new System.Drawing.Point(97, 54);
            this.chkBoxAppend.Name = "chkBoxAppend";
            this.chkBoxAppend.Size = new System.Drawing.Size(317, 17);
            this.chkBoxAppend.TabIndex = 8;
            this.chkBoxAppend.Text = "Append Sheet (If same sheet exists, then it will be overwritten)";
            this.chkBoxAppend.UseVisualStyleBackColor = true;
            // 
            // ExportToExcelForm
            // 
            this.AcceptButton = this.btnExportToExcelFile;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(484, 241);
            this.Controls.Add(this.chkBoxAppend);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblExport);
            this.Controls.Add(this.chkListBoxDataToExport);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExportToExcelFile);
            this.Controls.Add(this.txtExportToExcelFilePath);
            this.Controls.Add(this.btnExportToExcelBrowse);
            this.Name = "ExportToExcelForm";
            this.ShowIcon = false;
            this.Text = "Export to Excel File";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

 
        private System.Windows.Forms.Button btnExportToExcelBrowse;
        private System.Windows.Forms.TextBox txtExportToExcelFilePath;
        private System.Windows.Forms.Button btnExportToExcelFile;
        private System.ComponentModel.BackgroundWorker bgWorkerExportExcel;
        private System.Windows.Forms.CheckedListBox chkListBoxDataToExport;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblExport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SaveFileDialog saveFileDialogExportToExcel;
        private System.Windows.Forms.CheckBox chkBoxAppend;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogExport;
    }
}