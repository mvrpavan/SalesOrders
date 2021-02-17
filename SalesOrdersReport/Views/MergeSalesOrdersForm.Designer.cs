namespace SalesOrdersReport.Views
{
    partial class MergeSalesOrdersForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MergeSalesOrdersForm));
            this.ObjFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.ObjOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxOutputFolder = new System.Windows.Forms.TextBox();
            this.btnOutputFolderBrowse = new System.Windows.Forms.Button();
            this.dtGridViewSelectedFiles = new System.Windows.Forms.DataGridView();
            this.InputFileCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnMerge = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dateTimeSalesOrder = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewSelectedFiles)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ObjOpenFileDialog
            // 
            this.ObjOpenFileDialog.Multiselect = true;
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.BackColor = System.Drawing.Color.Transparent;
            this.btnSelectFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSelectFolder.FlatAppearance.BorderSize = 0;
            this.btnSelectFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFolder.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectFolder.Image")));
            this.btnSelectFolder.Location = new System.Drawing.Point(685, 144);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(72, 58);
            this.btnSelectFolder.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnSelectFolder, "Select Folder with SalesOrder Files");
            this.btnSelectFolder.UseVisualStyleBackColor = false;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // btnAddFile
            // 
            this.btnAddFile.BackColor = System.Drawing.Color.Transparent;
            this.btnAddFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAddFile.FlatAppearance.BorderSize = 0;
            this.btnAddFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddFile.Image = global::SalesOrdersReport.Properties.Resources.add_file_48;
            this.btnAddFile.Location = new System.Drawing.Point(697, 81);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(56, 57);
            this.btnAddFile.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnAddFile, "Add Sales Order File");
            this.btnAddFile.UseVisualStyleBackColor = false;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Output File:";
            // 
            // txtBoxOutputFolder
            // 
            this.txtBoxOutputFolder.Location = new System.Drawing.Point(129, 14);
            this.txtBoxOutputFolder.Multiline = true;
            this.txtBoxOutputFolder.Name = "txtBoxOutputFolder";
            this.txtBoxOutputFolder.Size = new System.Drawing.Size(469, 36);
            this.txtBoxOutputFolder.TabIndex = 2;
            // 
            // btnOutputFolderBrowse
            // 
            this.btnOutputFolderBrowse.Location = new System.Drawing.Point(604, 17);
            this.btnOutputFolderBrowse.Name = "btnOutputFolderBrowse";
            this.btnOutputFolderBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnOutputFolderBrowse.TabIndex = 3;
            this.btnOutputFolderBrowse.Text = "Browse";
            this.btnOutputFolderBrowse.UseVisualStyleBackColor = true;
            this.btnOutputFolderBrowse.Click += new System.EventHandler(this.btnOutputFolderBrowse_Click);
            // 
            // dtGridViewSelectedFiles
            // 
            this.dtGridViewSelectedFiles.AllowUserToAddRows = false;
            this.dtGridViewSelectedFiles.AllowUserToDeleteRows = false;
            this.dtGridViewSelectedFiles.AllowUserToResizeRows = false;
            this.dtGridViewSelectedFiles.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtGridViewSelectedFiles.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dtGridViewSelectedFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewSelectedFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InputFileCol});
            this.dtGridViewSelectedFiles.Location = new System.Drawing.Point(35, 77);
            this.dtGridViewSelectedFiles.Name = "dtGridViewSelectedFiles";
            this.dtGridViewSelectedFiles.ReadOnly = true;
            this.dtGridViewSelectedFiles.Size = new System.Drawing.Size(644, 244);
            this.dtGridViewSelectedFiles.TabIndex = 4;
            // 
            // InputFileCol
            // 
            this.InputFileCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.InputFileCol.HeaderText = "Input Files";
            this.InputFileCol.Name = "InputFileCol";
            this.InputFileCol.ReadOnly = true;
            // 
            // btnMerge
            // 
            this.btnMerge.Location = new System.Drawing.Point(220, 444);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(150, 50);
            this.btnMerge.TabIndex = 5;
            this.btnMerge.Text = "Merge Sales Orders";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(453, 444);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(152, 50);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Add Sales Order Files to Merge";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = global::SalesOrdersReport.Properties.Resources.delete_48;
            this.btnDelete.Location = new System.Drawing.Point(685, 208);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(72, 58);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dateTimeSalesOrder
            // 
            this.dateTimeSalesOrder.Location = new System.Drawing.Point(165, 65);
            this.dateTimeSalesOrder.Name = "dateTimeSalesOrder";
            this.dateTimeSalesOrder.Size = new System.Drawing.Size(169, 20);
            this.dateTimeSalesOrder.TabIndex = 7;
            this.dateTimeSalesOrder.ValueChanged += new System.EventHandler(this.dateTimeSalesOrder_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Sales Order Date:";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnSelectFolder);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnAddFile);
            this.panel1.Controls.Add(this.dtGridViewSelectedFiles);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnOutputFolderBrowse);
            this.panel1.Controls.Add(this.txtBoxOutputFolder);
            this.panel1.Location = new System.Drawing.Point(36, 103);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 335);
            this.panel1.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 515);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Status: ";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(68, 515);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(72, 13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Status Text";
            // 
            // MergeSalesOrdersForm
            // 
            this.AcceptButton = this.btnMerge;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(849, 537);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dateTimeSalesOrder);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnMerge);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MergeSalesOrdersForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Merge Sales Orders";
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewSelectedFiles)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog ObjFolderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog ObjOpenFileDialog;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Button btnAddFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxOutputFolder;
        private System.Windows.Forms.Button btnOutputFolderBrowse;
        private System.Windows.Forms.DataGridView dtGridViewSelectedFiles;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputFileCol;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DateTimePicker dateTimeSalesOrder;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblStatus;
    }
}