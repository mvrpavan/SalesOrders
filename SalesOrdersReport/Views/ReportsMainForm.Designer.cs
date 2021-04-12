namespace SalesOrdersReport.Views
{
    partial class ReportsMainForm
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
            this.dtGridViewReportData = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExportReport = new System.Windows.Forms.Button();
            this.btnPrintReport = new System.Windows.Forms.Button();
            this.btnReloadReports = new System.Windows.Forms.Button();
            this.btnDeleteReport = new System.Windows.Forms.Button();
            this.btnViewEditReport = new System.Windows.Forms.Button();
            this.btnAddReport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBoxReportName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBoxParamNames = new System.Windows.Forms.ComboBox();
            this.txtBoxParamValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddParamValue = new System.Windows.Forms.Button();
            this.dtGridViewParams = new System.Windows.Forms.DataGridView();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxReportDesc = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePickerParamValue = new System.Windows.Forms.DateTimePicker();
            this.btnReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewReportData)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewParams)).BeginInit();
            this.SuspendLayout();
            // 
            // dtGridViewReportData
            // 
            this.dtGridViewReportData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewReportData.Location = new System.Drawing.Point(12, 275);
            this.dtGridViewReportData.Name = "dtGridViewReportData";
            this.dtGridViewReportData.Size = new System.Drawing.Size(838, 262);
            this.dtGridViewReportData.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.btnExportReport);
            this.panel1.Controls.Add(this.btnPrintReport);
            this.panel1.Controls.Add(this.btnReloadReports);
            this.panel1.Controls.Add(this.btnDeleteReport);
            this.panel1.Controls.Add(this.btnViewEditReport);
            this.panel1.Controls.Add(this.btnAddReport);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(838, 84);
            this.panel1.TabIndex = 2;
            // 
            // btnExportReport
            // 
            this.btnExportReport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExportReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnExportReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnExportReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportReport.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnExportReport.Image = global::SalesOrdersReport.Properties.Resources.export_icon;
            this.btnExportReport.Location = new System.Drawing.Point(336, 3);
            this.btnExportReport.Name = "btnExportReport";
            this.btnExportReport.Size = new System.Drawing.Size(63, 73);
            this.btnExportReport.TabIndex = 0;
            this.btnExportReport.Text = "Export Report";
            this.btnExportReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportReport.UseVisualStyleBackColor = false;
            this.btnExportReport.Click += new System.EventHandler(this.btnExportReport_Click);
            // 
            // btnPrintReport
            // 
            this.btnPrintReport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPrintReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnPrintReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPrintReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintReport.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnPrintReport.Image = global::SalesOrdersReport.Properties.Resources.Iconshow_Hardware_Printer;
            this.btnPrintReport.Location = new System.Drawing.Point(270, 3);
            this.btnPrintReport.Name = "btnPrintReport";
            this.btnPrintReport.Size = new System.Drawing.Size(60, 73);
            this.btnPrintReport.TabIndex = 0;
            this.btnPrintReport.Text = "Print Report";
            this.btnPrintReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrintReport.UseVisualStyleBackColor = false;
            this.btnPrintReport.Click += new System.EventHandler(this.btnPrintReport_Click);
            // 
            // btnReloadReports
            // 
            this.btnReloadReports.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnReloadReports.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnReloadReports.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnReloadReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReloadReports.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnReloadReports.Image = global::SalesOrdersReport.Properties.Resources.Refresh_icon_32;
            this.btnReloadReports.Location = new System.Drawing.Point(201, 3);
            this.btnReloadReports.Name = "btnReloadReports";
            this.btnReloadReports.Size = new System.Drawing.Size(63, 73);
            this.btnReloadReports.TabIndex = 0;
            this.btnReloadReports.Text = "Reload";
            this.btnReloadReports.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReloadReports.UseVisualStyleBackColor = false;
            this.btnReloadReports.Click += new System.EventHandler(this.btnReloadReports_Click);
            // 
            // btnDeleteReport
            // 
            this.btnDeleteReport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDeleteReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnDeleteReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnDeleteReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteReport.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnDeleteReport.Image = global::SalesOrdersReport.Properties.Resources.delete_icon_32;
            this.btnDeleteReport.Location = new System.Drawing.Point(137, 3);
            this.btnDeleteReport.Name = "btnDeleteReport";
            this.btnDeleteReport.Size = new System.Drawing.Size(58, 73);
            this.btnDeleteReport.TabIndex = 0;
            this.btnDeleteReport.Text = "Delete Report";
            this.btnDeleteReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteReport.UseVisualStyleBackColor = false;
            this.btnDeleteReport.Click += new System.EventHandler(this.btnDeleteReport_Click);
            // 
            // btnViewEditReport
            // 
            this.btnViewEditReport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnViewEditReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnViewEditReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnViewEditReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewEditReport.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnViewEditReport.Image = global::SalesOrdersReport.Properties.Resources.edit_icon_32;
            this.btnViewEditReport.Location = new System.Drawing.Point(68, 3);
            this.btnViewEditReport.Name = "btnViewEditReport";
            this.btnViewEditReport.Size = new System.Drawing.Size(63, 73);
            this.btnViewEditReport.TabIndex = 0;
            this.btnViewEditReport.Text = "View Report";
            this.btnViewEditReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnViewEditReport.UseVisualStyleBackColor = false;
            this.btnViewEditReport.Click += new System.EventHandler(this.btnViewEditReport_Click);
            // 
            // btnAddReport
            // 
            this.btnAddReport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAddReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnAddReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAddReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddReport.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddReport.Image = global::SalesOrdersReport.Properties.Resources.math_add_icon_32;
            this.btnAddReport.Location = new System.Drawing.Point(5, 3);
            this.btnAddReport.Name = "btnAddReport";
            this.btnAddReport.Size = new System.Drawing.Size(57, 73);
            this.btnAddReport.TabIndex = 0;
            this.btnAddReport.Text = "Add Report";
            this.btnAddReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddReport.UseVisualStyleBackColor = false;
            this.btnAddReport.Click += new System.EventHandler(this.btnAddReport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Report Name";
            // 
            // cmbBoxReportName
            // 
            this.cmbBoxReportName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxReportName.FormattingEnabled = true;
            this.cmbBoxReportName.Location = new System.Drawing.Point(90, 111);
            this.cmbBoxReportName.Name = "cmbBoxReportName";
            this.cmbBoxReportName.Size = new System.Drawing.Size(191, 21);
            this.cmbBoxReportName.TabIndex = 4;
            this.cmbBoxReportName.SelectedIndexChanged += new System.EventHandler(this.cmbBoxReportName_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(337, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Parameter Name";
            // 
            // cmbBoxParamNames
            // 
            this.cmbBoxParamNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxParamNames.FormattingEnabled = true;
            this.cmbBoxParamNames.Location = new System.Drawing.Point(429, 111);
            this.cmbBoxParamNames.Name = "cmbBoxParamNames";
            this.cmbBoxParamNames.Size = new System.Drawing.Size(110, 21);
            this.cmbBoxParamNames.TabIndex = 4;
            this.cmbBoxParamNames.SelectedIndexChanged += new System.EventHandler(this.cmbBoxParamNames_SelectedIndexChanged);
            // 
            // txtBoxParamValue
            // 
            this.txtBoxParamValue.Location = new System.Drawing.Point(429, 138);
            this.txtBoxParamValue.Name = "txtBoxParamValue";
            this.txtBoxParamValue.Size = new System.Drawing.Size(110, 20);
            this.txtBoxParamValue.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(338, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Parameter Value";
            // 
            // btnAddParamValue
            // 
            this.btnAddParamValue.Location = new System.Drawing.Point(545, 136);
            this.btnAddParamValue.Name = "btnAddParamValue";
            this.btnAddParamValue.Size = new System.Drawing.Size(45, 23);
            this.btnAddParamValue.TabIndex = 6;
            this.btnAddParamValue.Text = "Add";
            this.btnAddParamValue.UseVisualStyleBackColor = true;
            this.btnAddParamValue.Click += new System.EventHandler(this.btnAddParamValue_Click);
            // 
            // dtGridViewParams
            // 
            this.dtGridViewParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewParams.Location = new System.Drawing.Point(599, 102);
            this.dtGridViewParams.Name = "dtGridViewParams";
            this.dtGridViewParams.Size = new System.Drawing.Size(251, 147);
            this.dtGridViewParams.TabIndex = 7;
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.Location = new System.Drawing.Point(340, 205);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(127, 44);
            this.btnGenerateReport.TabIndex = 8;
            this.btnGenerateReport.Text = "Generate Report";
            this.btnGenerateReport.UseVisualStyleBackColor = true;
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Description";
            // 
            // txtBoxReportDesc
            // 
            this.txtBoxReportDesc.Location = new System.Drawing.Point(90, 148);
            this.txtBoxReportDesc.Multiline = true;
            this.txtBoxReportDesc.Name = "txtBoxReportDesc";
            this.txtBoxReportDesc.Size = new System.Drawing.Size(191, 89);
            this.txtBoxReportDesc.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 259);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Report Data";
            // 
            // dateTimePickerParamValue
            // 
            this.dateTimePickerParamValue.Location = new System.Drawing.Point(429, 165);
            this.dateTimePickerParamValue.Name = "dateTimePickerParamValue";
            this.dateTimePickerParamValue.Size = new System.Drawing.Size(161, 20);
            this.dateTimePickerParamValue.TabIndex = 10;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(508, 205);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(82, 44);
            this.btnReset.TabIndex = 8;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // ReportsMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 549);
            this.Controls.Add(this.dateTimePickerParamValue);
            this.Controls.Add(this.txtBoxReportDesc);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnGenerateReport);
            this.Controls.Add(this.dtGridViewParams);
            this.Controls.Add(this.btnAddParamValue);
            this.Controls.Add(this.txtBoxParamValue);
            this.Controls.Add(this.cmbBoxParamNames);
            this.Controls.Add(this.cmbBoxReportName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dtGridViewReportData);
            this.Name = "ReportsMainForm";
            this.Text = "Reports";
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewReportData)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewParams)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtGridViewReportData;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExportReport;
        private System.Windows.Forms.Button btnPrintReport;
        private System.Windows.Forms.Button btnReloadReports;
        private System.Windows.Forms.Button btnDeleteReport;
        private System.Windows.Forms.Button btnViewEditReport;
        private System.Windows.Forms.Button btnAddReport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBoxReportName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbBoxParamNames;
        private System.Windows.Forms.TextBox txtBoxParamValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddParamValue;
        private System.Windows.Forms.DataGridView dtGridViewParams;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxReportDesc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePickerParamValue;
        private System.Windows.Forms.Button btnReset;
    }
}