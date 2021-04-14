namespace SalesOrdersReport.Views
{
    partial class CreateReportForm
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
            this.txtBoxReportName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoxQuery = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtGridViewPredefinedParameters = new System.Windows.Forms.DataGridView();
            this.btnAddParameter = new System.Windows.Forms.Button();
            this.dtGridViewParameters = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.btnValidateQuery = new System.Windows.Forms.Button();
            this.dtGridViewQueryResult = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCreateReport = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lblQueryStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewPredefinedParameters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewParameters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewQueryResult)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Report Name";
            // 
            // txtBoxReportName
            // 
            this.txtBoxReportName.Location = new System.Drawing.Point(114, 34);
            this.txtBoxReportName.Name = "txtBoxReportName";
            this.txtBoxReportName.Size = new System.Drawing.Size(281, 20);
            this.txtBoxReportName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Description";
            // 
            // txtBoxDescription
            // 
            this.txtBoxDescription.Location = new System.Drawing.Point(114, 60);
            this.txtBoxDescription.Multiline = true;
            this.txtBoxDescription.Name = "txtBoxDescription";
            this.txtBoxDescription.Size = new System.Drawing.Size(421, 62);
            this.txtBoxDescription.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Report Query";
            // 
            // txtBoxQuery
            // 
            this.txtBoxQuery.Location = new System.Drawing.Point(114, 128);
            this.txtBoxQuery.Multiline = true;
            this.txtBoxQuery.Name = "txtBoxQuery";
            this.txtBoxQuery.Size = new System.Drawing.Size(421, 150);
            this.txtBoxQuery.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(620, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Predefined Parameters";
            // 
            // dtGridViewPredefinedParameters
            // 
            this.dtGridViewPredefinedParameters.AllowUserToAddRows = false;
            this.dtGridViewPredefinedParameters.AllowUserToDeleteRows = false;
            this.dtGridViewPredefinedParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewPredefinedParameters.Location = new System.Drawing.Point(623, 128);
            this.dtGridViewPredefinedParameters.Name = "dtGridViewPredefinedParameters";
            this.dtGridViewPredefinedParameters.ReadOnly = true;
            this.dtGridViewPredefinedParameters.Size = new System.Drawing.Size(240, 150);
            this.dtGridViewPredefinedParameters.TabIndex = 2;
            // 
            // btnAddParameter
            // 
            this.btnAddParameter.FlatAppearance.BorderSize = 0;
            this.btnAddParameter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddParameter.Image = global::SalesOrdersReport.Properties.Resources.left_arrow;
            this.btnAddParameter.Location = new System.Drawing.Point(551, 188);
            this.btnAddParameter.Name = "btnAddParameter";
            this.btnAddParameter.Size = new System.Drawing.Size(56, 31);
            this.btnAddParameter.TabIndex = 3;
            this.btnAddParameter.UseVisualStyleBackColor = true;
            this.btnAddParameter.Click += new System.EventHandler(this.btnAddParameter_Click);
            // 
            // dtGridViewParameters
            // 
            this.dtGridViewParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewParameters.Location = new System.Drawing.Point(114, 284);
            this.dtGridViewParameters.Name = "dtGridViewParameters";
            this.dtGridViewParameters.Size = new System.Drawing.Size(421, 132);
            this.dtGridViewParameters.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 284);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "User Parameters";
            // 
            // btnValidateQuery
            // 
            this.btnValidateQuery.Location = new System.Drawing.Point(114, 422);
            this.btnValidateQuery.Name = "btnValidateQuery";
            this.btnValidateQuery.Size = new System.Drawing.Size(141, 31);
            this.btnValidateQuery.TabIndex = 6;
            this.btnValidateQuery.Text = "Validate Query";
            this.btnValidateQuery.UseVisualStyleBackColor = true;
            this.btnValidateQuery.Click += new System.EventHandler(this.btnValidateQuery_Click);
            // 
            // dtGridViewQueryResult
            // 
            this.dtGridViewQueryResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewQueryResult.Location = new System.Drawing.Point(114, 459);
            this.dtGridViewQueryResult.Name = "dtGridViewQueryResult";
            this.dtGridViewQueryResult.Size = new System.Drawing.Size(749, 214);
            this.dtGridViewQueryResult.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(38, 459);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Query Output";
            // 
            // btnCreateReport
            // 
            this.btnCreateReport.Location = new System.Drawing.Point(284, 692);
            this.btnCreateReport.Name = "btnCreateReport";
            this.btnCreateReport.Size = new System.Drawing.Size(141, 45);
            this.btnCreateReport.TabIndex = 6;
            this.btnCreateReport.Text = "Create Report";
            this.btnCreateReport.UseVisualStyleBackColor = true;
            this.btnCreateReport.Click += new System.EventHandler(this.btnCreateReport_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(494, 692);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(141, 45);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(299, 422);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Result:";
            // 
            // lblQueryStatus
            // 
            this.lblQueryStatus.AutoEllipsis = true;
            this.lblQueryStatus.Location = new System.Drawing.Point(345, 422);
            this.lblQueryStatus.Name = "lblQueryStatus";
            this.lblQueryStatus.Size = new System.Drawing.Size(518, 31);
            this.lblQueryStatus.TabIndex = 0;
            this.lblQueryStatus.Text = "Query Status";
            // 
            // CreateReportForm
            // 
            this.AcceptButton = this.btnCreateReport;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(883, 756);
            this.Controls.Add(this.dtGridViewQueryResult);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreateReport);
            this.Controls.Add(this.btnValidateQuery);
            this.Controls.Add(this.dtGridViewParameters);
            this.Controls.Add(this.btnAddParameter);
            this.Controls.Add(this.dtGridViewPredefinedParameters);
            this.Controls.Add(this.txtBoxQuery);
            this.Controls.Add(this.txtBoxDescription);
            this.Controls.Add(this.txtBoxReportName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblQueryStatus);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "CreateReportForm";
            this.Text = "Create Report";
            this.Load += new System.EventHandler(this.CreateReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewPredefinedParameters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewParameters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewQueryResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxReportName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBoxQuery;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dtGridViewPredefinedParameters;
        private System.Windows.Forms.Button btnAddParameter;
        private System.Windows.Forms.DataGridView dtGridViewParameters;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnValidateQuery;
        private System.Windows.Forms.DataGridView dtGridViewQueryResult;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCreateReport;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblQueryStatus;
    }
}