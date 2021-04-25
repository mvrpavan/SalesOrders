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
            this.panelCreateUpdateReport = new System.Windows.Forms.Panel();
            this.lblErrValidMsg = new System.Windows.Forms.Label();
            this.dtGridViewQueryResult = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCreateReport = new System.Windows.Forms.Button();
            this.btnValidateQuery = new System.Windows.Forms.Button();
            this.dtGridViewUserDefinedParameters = new System.Windows.Forms.DataGridView();
            this.btnAddParameter = new System.Windows.Forms.Button();
            this.dtGridViewPredefinedParameters = new System.Windows.Forms.DataGridView();
            this.txtBoxQuery = new System.Windows.Forms.TextBox();
            this.txtBoxDescription = new System.Windows.Forms.TextBox();
            this.txtBoxReportName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblQueryStatus = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelCreateUpdateReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewQueryResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewUserDefinedParameters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewPredefinedParameters)).BeginInit();
            this.SuspendLayout();
            // 
            // panelCreateUpdateReport
            // 
            this.panelCreateUpdateReport.AutoScroll = true;
            this.panelCreateUpdateReport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelCreateUpdateReport.Controls.Add(this.lblErrValidMsg);
            this.panelCreateUpdateReport.Controls.Add(this.dtGridViewQueryResult);
            this.panelCreateUpdateReport.Controls.Add(this.btnCancel);
            this.panelCreateUpdateReport.Controls.Add(this.btnCreateReport);
            this.panelCreateUpdateReport.Controls.Add(this.btnValidateQuery);
            this.panelCreateUpdateReport.Controls.Add(this.dtGridViewUserDefinedParameters);
            this.panelCreateUpdateReport.Controls.Add(this.btnAddParameter);
            this.panelCreateUpdateReport.Controls.Add(this.dtGridViewPredefinedParameters);
            this.panelCreateUpdateReport.Controls.Add(this.txtBoxQuery);
            this.panelCreateUpdateReport.Controls.Add(this.txtBoxDescription);
            this.panelCreateUpdateReport.Controls.Add(this.txtBoxReportName);
            this.panelCreateUpdateReport.Controls.Add(this.label2);
            this.panelCreateUpdateReport.Controls.Add(this.lblQueryStatus);
            this.panelCreateUpdateReport.Controls.Add(this.label7);
            this.panelCreateUpdateReport.Controls.Add(this.label4);
            this.panelCreateUpdateReport.Controls.Add(this.label6);
            this.panelCreateUpdateReport.Controls.Add(this.label5);
            this.panelCreateUpdateReport.Controls.Add(this.label3);
            this.panelCreateUpdateReport.Controls.Add(this.label1);
            this.panelCreateUpdateReport.Location = new System.Drawing.Point(12, 12);
            this.panelCreateUpdateReport.Name = "panelCreateUpdateReport";
            this.panelCreateUpdateReport.Size = new System.Drawing.Size(859, 725);
            this.panelCreateUpdateReport.TabIndex = 0;
            // 
            // lblErrValidMsg
            // 
            this.lblErrValidMsg.AutoSize = true;
            this.lblErrValidMsg.ForeColor = System.Drawing.Color.Red;
            this.lblErrValidMsg.Location = new System.Drawing.Point(271, 652);
            this.lblErrValidMsg.Name = "lblErrValidMsg";
            this.lblErrValidMsg.Size = new System.Drawing.Size(0, 13);
            this.lblErrValidMsg.TabIndex = 27;
            // 
            // dtGridViewQueryResult
            // 
            this.dtGridViewQueryResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewQueryResult.Location = new System.Drawing.Point(100, 431);
            this.dtGridViewQueryResult.Name = "dtGridViewQueryResult";
            this.dtGridViewQueryResult.Size = new System.Drawing.Size(749, 214);
            this.dtGridViewQueryResult.TabIndex = 26;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(480, 673);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(141, 45);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCreateReport
            // 
            this.btnCreateReport.Location = new System.Drawing.Point(270, 673);
            this.btnCreateReport.Name = "btnCreateReport";
            this.btnCreateReport.Size = new System.Drawing.Size(141, 45);
            this.btnCreateReport.TabIndex = 24;
            this.btnCreateReport.Text = "Create Report";
            this.btnCreateReport.UseVisualStyleBackColor = true;
            this.btnCreateReport.Click += new System.EventHandler(this.btnCreateReport_Click);
            // 
            // btnValidateQuery
            // 
            this.btnValidateQuery.Enabled = false;
            this.btnValidateQuery.Location = new System.Drawing.Point(100, 394);
            this.btnValidateQuery.Name = "btnValidateQuery";
            this.btnValidateQuery.Size = new System.Drawing.Size(141, 31);
            this.btnValidateQuery.TabIndex = 23;
            this.btnValidateQuery.Text = "Validate Query";
            this.btnValidateQuery.UseVisualStyleBackColor = true;
            this.btnValidateQuery.Click += new System.EventHandler(this.btnValidateQuery_Click);
            // 
            // dtGridViewUserDefinedParameters
            // 
            this.dtGridViewUserDefinedParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewUserDefinedParameters.Location = new System.Drawing.Point(100, 256);
            this.dtGridViewUserDefinedParameters.Name = "dtGridViewUserDefinedParameters";
            this.dtGridViewUserDefinedParameters.Size = new System.Drawing.Size(421, 132);
            this.dtGridViewUserDefinedParameters.TabIndex = 22;
            // 
            // btnAddParameter
            // 
            this.btnAddParameter.FlatAppearance.BorderSize = 0;
            this.btnAddParameter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddParameter.Image = global::SalesOrdersReport.Properties.Resources.left_arrow;
            this.btnAddParameter.Location = new System.Drawing.Point(537, 160);
            this.btnAddParameter.Name = "btnAddParameter";
            this.btnAddParameter.Size = new System.Drawing.Size(56, 31);
            this.btnAddParameter.TabIndex = 21;
            this.btnAddParameter.UseVisualStyleBackColor = true;
            this.btnAddParameter.Click += new System.EventHandler(this.btnAddParameter_Click);
            // 
            // dtGridViewPredefinedParameters
            // 
            this.dtGridViewPredefinedParameters.AllowUserToAddRows = false;
            this.dtGridViewPredefinedParameters.AllowUserToDeleteRows = false;
            this.dtGridViewPredefinedParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewPredefinedParameters.Location = new System.Drawing.Point(609, 100);
            this.dtGridViewPredefinedParameters.MultiSelect = false;
            this.dtGridViewPredefinedParameters.Name = "dtGridViewPredefinedParameters";
            this.dtGridViewPredefinedParameters.ReadOnly = true;
            this.dtGridViewPredefinedParameters.Size = new System.Drawing.Size(240, 150);
            this.dtGridViewPredefinedParameters.TabIndex = 20;
            // 
            // txtBoxQuery
            // 
            this.txtBoxQuery.Location = new System.Drawing.Point(100, 100);
            this.txtBoxQuery.Multiline = true;
            this.txtBoxQuery.Name = "txtBoxQuery";
            this.txtBoxQuery.Size = new System.Drawing.Size(421, 150);
            this.txtBoxQuery.TabIndex = 19;
            // 
            // txtBoxDescription
            // 
            this.txtBoxDescription.Location = new System.Drawing.Point(100, 32);
            this.txtBoxDescription.Multiline = true;
            this.txtBoxDescription.Name = "txtBoxDescription";
            this.txtBoxDescription.Size = new System.Drawing.Size(421, 62);
            this.txtBoxDescription.TabIndex = 18;
            // 
            // txtBoxReportName
            // 
            this.txtBoxReportName.Location = new System.Drawing.Point(100, 6);
            this.txtBoxReportName.Name = "txtBoxReportName";
            this.txtBoxReportName.Size = new System.Drawing.Size(281, 20);
            this.txtBoxReportName.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Description";
            // 
            // lblQueryStatus
            // 
            this.lblQueryStatus.AutoEllipsis = true;
            this.lblQueryStatus.Location = new System.Drawing.Point(331, 394);
            this.lblQueryStatus.Name = "lblQueryStatus";
            this.lblQueryStatus.Size = new System.Drawing.Size(518, 31);
            this.lblQueryStatus.TabIndex = 14;
            this.lblQueryStatus.Text = "Query Status";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(285, 394);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Result:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(606, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Predefined Parameters";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 431);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Query Output";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 256);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "User Parameters";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Report Query";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Report Name";
            // 
            // CreateReportForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(883, 749);
            this.Controls.Add(this.panelCreateUpdateReport);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateReportForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Report";
            this.Load += new System.EventHandler(this.CreateReportForm_Load);
            this.panelCreateUpdateReport.ResumeLayout(false);
            this.panelCreateUpdateReport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewQueryResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewUserDefinedParameters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewPredefinedParameters)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCreateUpdateReport;
        private System.Windows.Forms.Label lblErrValidMsg;
        private System.Windows.Forms.DataGridView dtGridViewQueryResult;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCreateReport;
        private System.Windows.Forms.Button btnValidateQuery;
        private System.Windows.Forms.DataGridView dtGridViewUserDefinedParameters;
        private System.Windows.Forms.Button btnAddParameter;
        private System.Windows.Forms.DataGridView dtGridViewPredefinedParameters;
        private System.Windows.Forms.TextBox txtBoxQuery;
        private System.Windows.Forms.TextBox txtBoxDescription;
        private System.Windows.Forms.TextBox txtBoxReportName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblQueryStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
    }
}