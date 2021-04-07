namespace SalesOrdersReport.Views
{
    partial class AddPaymentSummaryForm
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
            this.cmbxCreatePaymentCustomerNames = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbxCreatePaymentNumber = new System.Windows.Forms.ComboBox();
            this.grpbxCreatePaymentPaymentDtls = new System.Windows.Forms.GroupBox();
            this.grpbxCreatePaymentCustDtls = new System.Windows.Forms.GroupBox();
            this.txtCreatePaymentsCustPhoneNo = new System.Windows.Forms.TextBox();
            this.lblCreatePaymentCustPhoneno = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtCreatePaymentCustAddress = new System.Windows.Forms.TextBox();
            this.txtCreatePaymentCustName = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.btnAddPaymentSummary = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblValidateErrMsg = new System.Windows.Forms.Label();
            this.grpbxCreatePaymentPaymentDtls.SuspendLayout();
            this.grpbxCreatePaymentCustDtls.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Customer";
            // 
            // cmbxCreatePaymentCustomerNames
            // 
            this.cmbxCreatePaymentCustomerNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxCreatePaymentCustomerNames.FormattingEnabled = true;
            this.cmbxCreatePaymentCustomerNames.Location = new System.Drawing.Point(109, 19);
            this.cmbxCreatePaymentCustomerNames.Name = "cmbxCreatePaymentCustomerNames";
            this.cmbxCreatePaymentCustomerNames.Size = new System.Drawing.Size(220, 21);
            this.cmbxCreatePaymentCustomerNames.TabIndex = 1;
            this.cmbxCreatePaymentCustomerNames.SelectedIndexChanged += new System.EventHandler(this.cmbxCreatePaymentCustomerNames_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Number";
            // 
            // cmbxCreatePaymentNumber
            // 
            this.cmbxCreatePaymentNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxCreatePaymentNumber.FormattingEnabled = true;
            this.cmbxCreatePaymentNumber.Location = new System.Drawing.Point(109, 53);
            this.cmbxCreatePaymentNumber.Name = "cmbxCreatePaymentNumber";
            this.cmbxCreatePaymentNumber.Size = new System.Drawing.Size(134, 21);
            this.cmbxCreatePaymentNumber.TabIndex = 1;
            this.cmbxCreatePaymentNumber.SelectedIndexChanged += new System.EventHandler(this.cmbxCreatePaymentNumber_SelectedIndexChanged);
            // 
            // grpbxCreatePaymentPaymentDtls
            // 
            this.grpbxCreatePaymentPaymentDtls.Controls.Add(this.label1);
            this.grpbxCreatePaymentPaymentDtls.Controls.Add(this.cmbxCreatePaymentNumber);
            this.grpbxCreatePaymentPaymentDtls.Controls.Add(this.label3);
            this.grpbxCreatePaymentPaymentDtls.Controls.Add(this.cmbxCreatePaymentCustomerNames);
            this.grpbxCreatePaymentPaymentDtls.Location = new System.Drawing.Point(12, 12);
            this.grpbxCreatePaymentPaymentDtls.Name = "grpbxCreatePaymentPaymentDtls";
            this.grpbxCreatePaymentPaymentDtls.Size = new System.Drawing.Size(359, 195);
            this.grpbxCreatePaymentPaymentDtls.TabIndex = 5;
            this.grpbxCreatePaymentPaymentDtls.TabStop = false;
            this.grpbxCreatePaymentPaymentDtls.Text = "Payment Details";
            // 
            // grpbxCreatePaymentCustDtls
            // 
            this.grpbxCreatePaymentCustDtls.Controls.Add(this.txtCreatePaymentsCustPhoneNo);
            this.grpbxCreatePaymentCustDtls.Controls.Add(this.lblCreatePaymentCustPhoneno);
            this.grpbxCreatePaymentCustDtls.Controls.Add(this.label22);
            this.grpbxCreatePaymentCustDtls.Controls.Add(this.txtCreatePaymentCustAddress);
            this.grpbxCreatePaymentCustDtls.Controls.Add(this.txtCreatePaymentCustName);
            this.grpbxCreatePaymentCustDtls.Controls.Add(this.label24);
            this.grpbxCreatePaymentCustDtls.Location = new System.Drawing.Point(377, 12);
            this.grpbxCreatePaymentCustDtls.Name = "grpbxCreatePaymentCustDtls";
            this.grpbxCreatePaymentCustDtls.Size = new System.Drawing.Size(327, 195);
            this.grpbxCreatePaymentCustDtls.TabIndex = 6;
            this.grpbxCreatePaymentCustDtls.TabStop = false;
            this.grpbxCreatePaymentCustDtls.Text = "Customer Details";
            // 
            // txtCreatePaymentsCustPhoneNo
            // 
            this.txtCreatePaymentsCustPhoneNo.Enabled = false;
            this.txtCreatePaymentsCustPhoneNo.Location = new System.Drawing.Point(105, 50);
            this.txtCreatePaymentsCustPhoneNo.Name = "txtCreatePaymentsCustPhoneNo";
            this.txtCreatePaymentsCustPhoneNo.Size = new System.Drawing.Size(105, 20);
            this.txtCreatePaymentsCustPhoneNo.TabIndex = 6;
            // 
            // lblCreatePaymentCustPhoneno
            // 
            this.lblCreatePaymentCustPhoneno.AutoSize = true;
            this.lblCreatePaymentCustPhoneno.Location = new System.Drawing.Point(14, 53);
            this.lblCreatePaymentCustPhoneno.Name = "lblCreatePaymentCustPhoneno";
            this.lblCreatePaymentCustPhoneno.Size = new System.Drawing.Size(85, 13);
            this.lblCreatePaymentCustPhoneno.TabIndex = 5;
            this.lblCreatePaymentCustPhoneno.Text = "Customer Phone";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(7, 79);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(92, 13);
            this.label22.TabIndex = 4;
            this.label22.Text = "Customer Address";
            // 
            // txtCreatePaymentCustAddress
            // 
            this.txtCreatePaymentCustAddress.Enabled = false;
            this.txtCreatePaymentCustAddress.Location = new System.Drawing.Point(105, 76);
            this.txtCreatePaymentCustAddress.Multiline = true;
            this.txtCreatePaymentCustAddress.Name = "txtCreatePaymentCustAddress";
            this.txtCreatePaymentCustAddress.Size = new System.Drawing.Size(203, 84);
            this.txtCreatePaymentCustAddress.TabIndex = 2;
            // 
            // txtCreatePaymentCustName
            // 
            this.txtCreatePaymentCustName.Enabled = false;
            this.txtCreatePaymentCustName.Location = new System.Drawing.Point(105, 24);
            this.txtCreatePaymentCustName.Name = "txtCreatePaymentCustName";
            this.txtCreatePaymentCustName.Size = new System.Drawing.Size(105, 20);
            this.txtCreatePaymentCustName.TabIndex = 2;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(17, 27);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(82, 13);
            this.label24.TabIndex = 4;
            this.label24.Text = "Customer Name";
            // 
            // btnAddPaymentSummary
            // 
            this.btnAddPaymentSummary.Location = new System.Drawing.Point(142, 237);
            this.btnAddPaymentSummary.Name = "btnAddPaymentSummary";
            this.btnAddPaymentSummary.Size = new System.Drawing.Size(133, 23);
            this.btnAddPaymentSummary.TabIndex = 7;
            this.btnAddPaymentSummary.Text = "Add Payment Summary Row";
            this.btnAddPaymentSummary.UseVisualStyleBackColor = true;
            this.btnAddPaymentSummary.Click += new System.EventHandler(this.btnAddUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(469, 237);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(133, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(310, 237);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(133, 23);
            this.btnReset.TabIndex = 7;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblValidateErrMsg
            // 
            this.lblValidateErrMsg.AutoSize = true;
            this.lblValidateErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lblValidateErrMsg.Location = new System.Drawing.Point(139, 217);
            this.lblValidateErrMsg.Name = "lblValidateErrMsg";
            this.lblValidateErrMsg.Size = new System.Drawing.Size(0, 13);
            this.lblValidateErrMsg.TabIndex = 4;
            this.lblValidateErrMsg.Visible = false;
            // 
            // AddPaymentSummaryForm
            // 
            this.AcceptButton = this.btnAddPaymentSummary;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(720, 291);
            this.Controls.Add(this.lblValidateErrMsg);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAddPaymentSummary);
            this.Controls.Add(this.grpbxCreatePaymentCustDtls);
            this.Controls.Add(this.grpbxCreatePaymentPaymentDtls);
            this.Name = "AddPaymentSummaryForm";
            this.Text = "Add Payment Summary Row";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddPaymentSummaryForm_FormClosed);
            this.Load += new System.EventHandler(this.AddPaymentSummaryForm_Load);
            this.grpbxCreatePaymentPaymentDtls.ResumeLayout(false);
            this.grpbxCreatePaymentPaymentDtls.PerformLayout();
            this.grpbxCreatePaymentCustDtls.ResumeLayout(false);
            this.grpbxCreatePaymentCustDtls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbxCreatePaymentCustomerNames;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbxCreatePaymentNumber;
        private System.Windows.Forms.GroupBox grpbxCreatePaymentPaymentDtls;
        private System.Windows.Forms.GroupBox grpbxCreatePaymentCustDtls;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtCreatePaymentCustAddress;
        private System.Windows.Forms.TextBox txtCreatePaymentCustName;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btnAddPaymentSummary;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblValidateErrMsg;
        private System.Windows.Forms.TextBox txtCreatePaymentsCustPhoneNo;
        private System.Windows.Forms.Label lblCreatePaymentCustPhoneno;
    }
}