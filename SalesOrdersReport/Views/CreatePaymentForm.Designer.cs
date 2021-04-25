namespace SalesOrdersReport.Views
{
    partial class CreatePaymentForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.cmbxCreatePaymentPaymentAgainst = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbxCreatePaymentNumber = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbBoxPaymentModes = new System.Windows.Forms.ComboBox();
            this.txtbxCreatePaymentAmount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpCreatePaymentPaidOn = new System.Windows.Forms.DateTimePicker();
            this.grpbxCreatePaymentPaymentDtls = new System.Windows.Forms.GroupBox();
            this.chckActive = new System.Windows.Forms.CheckBox();
            this.txtCreatePaymentDesc = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.cmbxcreatePaymentStaffName = new System.Windows.Forms.ComboBox();
            this.grpbxCreatePaymentInvoiceDtls = new System.Windows.Forms.GroupBox();
            this.chckbxCreatePaymentMarkInvoiceAsPaid = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCreatePaymentNetSaleAmt = new System.Windows.Forms.TextBox();
            this.txtCreatePaymentTotalTax = new System.Windows.Forms.TextBox();
            this.txtCreatePaymentDiscAmt = new System.Windows.Forms.TextBox();
            this.txtCreatePaymentRefundAmt = new System.Windows.Forms.TextBox();
            this.txtCreatePaymentCancelAmt = new System.Windows.Forms.TextBox();
            this.txtCreatePaymentSaleAmount = new System.Windows.Forms.TextBox();
            this.txtCreatePaymentInvoiceItems = new System.Windows.Forms.TextBox();
            this.txtCreatePaymentInvoiceDate = new System.Windows.Forms.TextBox();
            this.txtCreatePaymentInvoiceNum = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.grpbxCreatePaymentCustDtls = new System.Windows.Forms.GroupBox();
            this.txtCreatePaymentsCustPhoneNo = new System.Windows.Forms.TextBox();
            this.lblCreatePaymentCustPhoneno = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtCreatePaymentCustAddress = new System.Windows.Forms.TextBox();
            this.txtCreatePaymentOB = new System.Windows.Forms.TextBox();
            this.txtCreatePaymentBA = new System.Windows.Forms.TextBox();
            this.txtCreatePaymentCustName = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.btnCreateUpdatePayment = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblValidateErrMsg = new System.Windows.Forms.Label();
            this.grpbxCreatePaymentPaymentDtls.SuspendLayout();
            this.grpbxCreatePaymentInvoiceDtls.SuspendLayout();
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Payment against";
            // 
            // cmbxCreatePaymentPaymentAgainst
            // 
            this.cmbxCreatePaymentPaymentAgainst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxCreatePaymentPaymentAgainst.FormattingEnabled = true;
            this.cmbxCreatePaymentPaymentAgainst.Items.AddRange(new object[] {
            "Select Payment Against",
            "Invoice",
            "Quotations"});
            this.cmbxCreatePaymentPaymentAgainst.Location = new System.Drawing.Point(109, 46);
            this.cmbxCreatePaymentPaymentAgainst.Name = "cmbxCreatePaymentPaymentAgainst";
            this.cmbxCreatePaymentPaymentAgainst.Size = new System.Drawing.Size(175, 21);
            this.cmbxCreatePaymentPaymentAgainst.TabIndex = 1;
            this.cmbxCreatePaymentPaymentAgainst.SelectedIndexChanged += new System.EventHandler(this.cmbxCreatePaymentPaymentAgainst_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Number";
            // 
            // cmbxCreatePaymentNumber
            // 
            this.cmbxCreatePaymentNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxCreatePaymentNumber.FormattingEnabled = true;
            this.cmbxCreatePaymentNumber.Location = new System.Drawing.Point(109, 73);
            this.cmbxCreatePaymentNumber.Name = "cmbxCreatePaymentNumber";
            this.cmbxCreatePaymentNumber.Size = new System.Drawing.Size(134, 21);
            this.cmbxCreatePaymentNumber.TabIndex = 1;
            this.cmbxCreatePaymentNumber.SelectedIndexChanged += new System.EventHandler(this.cmbxCreatePaymentNumber_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Amount";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Payment Mode";
            // 
            // cmbBoxPaymentModes
            // 
            this.cmbBoxPaymentModes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxPaymentModes.FormattingEnabled = true;
            this.cmbBoxPaymentModes.Location = new System.Drawing.Point(109, 126);
            this.cmbBoxPaymentModes.Name = "cmbBoxPaymentModes";
            this.cmbBoxPaymentModes.Size = new System.Drawing.Size(105, 21);
            this.cmbBoxPaymentModes.TabIndex = 1;
            // 
            // txtbxCreatePaymentAmount
            // 
            this.txtbxCreatePaymentAmount.Location = new System.Drawing.Point(109, 153);
            this.txtbxCreatePaymentAmount.Name = "txtbxCreatePaymentAmount";
            this.txtbxCreatePaymentAmount.Size = new System.Drawing.Size(105, 20);
            this.txtbxCreatePaymentAmount.TabIndex = 2;
            this.txtbxCreatePaymentAmount.TextChanged += new System.EventHandler(this.txtbxCreatePaymentAmount_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(60, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Paid on";
            // 
            // dtpCreatePaymentPaidOn
            // 
            this.dtpCreatePaymentPaidOn.Location = new System.Drawing.Point(109, 100);
            this.dtpCreatePaymentPaidOn.Name = "dtpCreatePaymentPaidOn";
            this.dtpCreatePaymentPaidOn.Size = new System.Drawing.Size(157, 20);
            this.dtpCreatePaymentPaidOn.TabIndex = 3;
            // 
            // grpbxCreatePaymentPaymentDtls
            // 
            this.grpbxCreatePaymentPaymentDtls.Controls.Add(this.chckActive);
            this.grpbxCreatePaymentPaymentDtls.Controls.Add(this.label1);
            this.grpbxCreatePaymentPaymentDtls.Controls.Add(this.txtCreatePaymentDesc);
            this.grpbxCreatePaymentPaymentDtls.Controls.Add(this.label2);
            this.grpbxCreatePaymentPaymentDtls.Controls.Add(this.dtpCreatePaymentPaidOn);
            this.grpbxCreatePaymentPaymentDtls.Controls.Add(this.label16);
            this.grpbxCreatePaymentPaymentDtls.Controls.Add(this.label4);
            this.grpbxCreatePaymentPaymentDtls.Controls.Add(this.txtbxCreatePaymentAmount);
            this.grpbxCreatePaymentPaymentDtls.Controls.Add(this.label18);
            this.grpbxCreatePaymentPaymentDtls.Controls.Add(this.label5);
            this.grpbxCreatePaymentPaymentDtls.Controls.Add(this.cmbxcreatePaymentStaffName);
            this.grpbxCreatePaymentPaymentDtls.Controls.Add(this.cmbBoxPaymentModes);
            this.grpbxCreatePaymentPaymentDtls.Controls.Add(this.label6);
            this.grpbxCreatePaymentPaymentDtls.Controls.Add(this.cmbxCreatePaymentNumber);
            this.grpbxCreatePaymentPaymentDtls.Controls.Add(this.label3);
            this.grpbxCreatePaymentPaymentDtls.Controls.Add(this.cmbxCreatePaymentPaymentAgainst);
            this.grpbxCreatePaymentPaymentDtls.Controls.Add(this.cmbxCreatePaymentCustomerNames);
            this.grpbxCreatePaymentPaymentDtls.Location = new System.Drawing.Point(12, 12);
            this.grpbxCreatePaymentPaymentDtls.Name = "grpbxCreatePaymentPaymentDtls";
            this.grpbxCreatePaymentPaymentDtls.Size = new System.Drawing.Size(359, 305);
            this.grpbxCreatePaymentPaymentDtls.TabIndex = 5;
            this.grpbxCreatePaymentPaymentDtls.TabStop = false;
            this.grpbxCreatePaymentPaymentDtls.Text = "Payment Details";
            // 
            // chckActive
            // 
            this.chckActive.AutoSize = true;
            this.chckActive.Checked = true;
            this.chckActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckActive.Location = new System.Drawing.Point(221, 270);
            this.chckActive.Name = "chckActive";
            this.chckActive.Size = new System.Drawing.Size(56, 17);
            this.chckActive.TabIndex = 4;
            this.chckActive.Text = "Active";
            this.chckActive.UseVisualStyleBackColor = true;
            // 
            // txtCreatePaymentDesc
            // 
            this.txtCreatePaymentDesc.Location = new System.Drawing.Point(109, 179);
            this.txtCreatePaymentDesc.Multiline = true;
            this.txtCreatePaymentDesc.Name = "txtCreatePaymentDesc";
            this.txtCreatePaymentDesc.Size = new System.Drawing.Size(203, 84);
            this.txtCreatePaymentDesc.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(43, 182);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(60, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Description";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(43, 272);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(60, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "Staff Name";
            // 
            // cmbxcreatePaymentStaffName
            // 
            this.cmbxcreatePaymentStaffName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxcreatePaymentStaffName.FormattingEnabled = true;
            this.cmbxcreatePaymentStaffName.Location = new System.Drawing.Point(109, 269);
            this.cmbxcreatePaymentStaffName.Name = "cmbxcreatePaymentStaffName";
            this.cmbxcreatePaymentStaffName.Size = new System.Drawing.Size(105, 21);
            this.cmbxcreatePaymentStaffName.TabIndex = 1;
            // 
            // grpbxCreatePaymentInvoiceDtls
            // 
            this.grpbxCreatePaymentInvoiceDtls.Controls.Add(this.chckbxCreatePaymentMarkInvoiceAsPaid);
            this.grpbxCreatePaymentInvoiceDtls.Controls.Add(this.label15);
            this.grpbxCreatePaymentInvoiceDtls.Controls.Add(this.label14);
            this.grpbxCreatePaymentInvoiceDtls.Controls.Add(this.label13);
            this.grpbxCreatePaymentInvoiceDtls.Controls.Add(this.label12);
            this.grpbxCreatePaymentInvoiceDtls.Controls.Add(this.label10);
            this.grpbxCreatePaymentInvoiceDtls.Controls.Add(this.label11);
            this.grpbxCreatePaymentInvoiceDtls.Controls.Add(this.label9);
            this.grpbxCreatePaymentInvoiceDtls.Controls.Add(this.txtCreatePaymentNetSaleAmt);
            this.grpbxCreatePaymentInvoiceDtls.Controls.Add(this.txtCreatePaymentTotalTax);
            this.grpbxCreatePaymentInvoiceDtls.Controls.Add(this.txtCreatePaymentDiscAmt);
            this.grpbxCreatePaymentInvoiceDtls.Controls.Add(this.txtCreatePaymentRefundAmt);
            this.grpbxCreatePaymentInvoiceDtls.Controls.Add(this.txtCreatePaymentCancelAmt);
            this.grpbxCreatePaymentInvoiceDtls.Controls.Add(this.txtCreatePaymentSaleAmount);
            this.grpbxCreatePaymentInvoiceDtls.Controls.Add(this.txtCreatePaymentInvoiceItems);
            this.grpbxCreatePaymentInvoiceDtls.Controls.Add(this.txtCreatePaymentInvoiceDate);
            this.grpbxCreatePaymentInvoiceDtls.Controls.Add(this.txtCreatePaymentInvoiceNum);
            this.grpbxCreatePaymentInvoiceDtls.Controls.Add(this.label7);
            this.grpbxCreatePaymentInvoiceDtls.Controls.Add(this.label8);
            this.grpbxCreatePaymentInvoiceDtls.Location = new System.Drawing.Point(12, 323);
            this.grpbxCreatePaymentInvoiceDtls.Name = "grpbxCreatePaymentInvoiceDtls";
            this.grpbxCreatePaymentInvoiceDtls.Size = new System.Drawing.Size(692, 170);
            this.grpbxCreatePaymentInvoiceDtls.TabIndex = 5;
            this.grpbxCreatePaymentInvoiceDtls.TabStop = false;
            this.grpbxCreatePaymentInvoiceDtls.Text = "Invoice Details";
            // 
            // chckbxCreatePaymentMarkInvoiceAsPaid
            // 
            this.chckbxCreatePaymentMarkInvoiceAsPaid.AutoSize = true;
            this.chckbxCreatePaymentMarkInvoiceAsPaid.Location = new System.Drawing.Point(399, 130);
            this.chckbxCreatePaymentMarkInvoiceAsPaid.Name = "chckbxCreatePaymentMarkInvoiceAsPaid";
            this.chckbxCreatePaymentMarkInvoiceAsPaid.Size = new System.Drawing.Size(126, 17);
            this.chckbxCreatePaymentMarkInvoiceAsPaid.TabIndex = 5;
            this.chckbxCreatePaymentMarkInvoiceAsPaid.Text = "Mark Invoice as Paid";
            this.chckbxCreatePaymentMarkInvoiceAsPaid.UseVisualStyleBackColor = true;
            this.chckbxCreatePaymentMarkInvoiceAsPaid.CheckedChanged += new System.EventHandler(this.chckbxCreatePaymentMarkInvoiceAsPaid_CheckedChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 131);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(87, 13);
            this.label15.TabIndex = 4;
            this.label15.Text = "Net Sale Amount";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(339, 105);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "Total Tax";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(305, 79);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Discount Amount";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(315, 53);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "Refund Amount";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(314, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Cancel Amount";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(32, 105);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Sale Amount";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Invoice Items";
            // 
            // txtCreatePaymentNetSaleAmt
            // 
            this.txtCreatePaymentNetSaleAmt.Location = new System.Drawing.Point(105, 128);
            this.txtCreatePaymentNetSaleAmt.Name = "txtCreatePaymentNetSaleAmt";
            this.txtCreatePaymentNetSaleAmt.ReadOnly = true;
            this.txtCreatePaymentNetSaleAmt.Size = new System.Drawing.Size(105, 20);
            this.txtCreatePaymentNetSaleAmt.TabIndex = 2;
            // 
            // txtCreatePaymentTotalTax
            // 
            this.txtCreatePaymentTotalTax.Location = new System.Drawing.Point(399, 102);
            this.txtCreatePaymentTotalTax.Name = "txtCreatePaymentTotalTax";
            this.txtCreatePaymentTotalTax.ReadOnly = true;
            this.txtCreatePaymentTotalTax.Size = new System.Drawing.Size(105, 20);
            this.txtCreatePaymentTotalTax.TabIndex = 2;
            // 
            // txtCreatePaymentDiscAmt
            // 
            this.txtCreatePaymentDiscAmt.Location = new System.Drawing.Point(399, 76);
            this.txtCreatePaymentDiscAmt.Name = "txtCreatePaymentDiscAmt";
            this.txtCreatePaymentDiscAmt.ReadOnly = true;
            this.txtCreatePaymentDiscAmt.Size = new System.Drawing.Size(105, 20);
            this.txtCreatePaymentDiscAmt.TabIndex = 2;
            // 
            // txtCreatePaymentRefundAmt
            // 
            this.txtCreatePaymentRefundAmt.Location = new System.Drawing.Point(399, 50);
            this.txtCreatePaymentRefundAmt.Name = "txtCreatePaymentRefundAmt";
            this.txtCreatePaymentRefundAmt.ReadOnly = true;
            this.txtCreatePaymentRefundAmt.Size = new System.Drawing.Size(105, 20);
            this.txtCreatePaymentRefundAmt.TabIndex = 2;
            // 
            // txtCreatePaymentCancelAmt
            // 
            this.txtCreatePaymentCancelAmt.Location = new System.Drawing.Point(399, 24);
            this.txtCreatePaymentCancelAmt.Name = "txtCreatePaymentCancelAmt";
            this.txtCreatePaymentCancelAmt.ReadOnly = true;
            this.txtCreatePaymentCancelAmt.Size = new System.Drawing.Size(105, 20);
            this.txtCreatePaymentCancelAmt.TabIndex = 2;
            // 
            // txtCreatePaymentSaleAmount
            // 
            this.txtCreatePaymentSaleAmount.Location = new System.Drawing.Point(105, 102);
            this.txtCreatePaymentSaleAmount.Name = "txtCreatePaymentSaleAmount";
            this.txtCreatePaymentSaleAmount.ReadOnly = true;
            this.txtCreatePaymentSaleAmount.Size = new System.Drawing.Size(105, 20);
            this.txtCreatePaymentSaleAmount.TabIndex = 2;
            // 
            // txtCreatePaymentInvoiceItems
            // 
            this.txtCreatePaymentInvoiceItems.Location = new System.Drawing.Point(105, 76);
            this.txtCreatePaymentInvoiceItems.Name = "txtCreatePaymentInvoiceItems";
            this.txtCreatePaymentInvoiceItems.ReadOnly = true;
            this.txtCreatePaymentInvoiceItems.Size = new System.Drawing.Size(105, 20);
            this.txtCreatePaymentInvoiceItems.TabIndex = 2;
            // 
            // txtCreatePaymentInvoiceDate
            // 
            this.txtCreatePaymentInvoiceDate.Location = new System.Drawing.Point(105, 50);
            this.txtCreatePaymentInvoiceDate.Name = "txtCreatePaymentInvoiceDate";
            this.txtCreatePaymentInvoiceDate.ReadOnly = true;
            this.txtCreatePaymentInvoiceDate.Size = new System.Drawing.Size(105, 20);
            this.txtCreatePaymentInvoiceDate.TabIndex = 2;
            // 
            // txtCreatePaymentInvoiceNum
            // 
            this.txtCreatePaymentInvoiceNum.Location = new System.Drawing.Point(105, 24);
            this.txtCreatePaymentInvoiceNum.Name = "txtCreatePaymentInvoiceNum";
            this.txtCreatePaymentInvoiceNum.ReadOnly = true;
            this.txtCreatePaymentInvoiceNum.Size = new System.Drawing.Size(105, 20);
            this.txtCreatePaymentInvoiceNum.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Invoice Date";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Invoice Number";
            // 
            // grpbxCreatePaymentCustDtls
            // 
            this.grpbxCreatePaymentCustDtls.Controls.Add(this.txtCreatePaymentsCustPhoneNo);
            this.grpbxCreatePaymentCustDtls.Controls.Add(this.lblCreatePaymentCustPhoneno);
            this.grpbxCreatePaymentCustDtls.Controls.Add(this.label22);
            this.grpbxCreatePaymentCustDtls.Controls.Add(this.txtCreatePaymentCustAddress);
            this.grpbxCreatePaymentCustDtls.Controls.Add(this.txtCreatePaymentOB);
            this.grpbxCreatePaymentCustDtls.Controls.Add(this.txtCreatePaymentBA);
            this.grpbxCreatePaymentCustDtls.Controls.Add(this.txtCreatePaymentCustName);
            this.grpbxCreatePaymentCustDtls.Controls.Add(this.label17);
            this.grpbxCreatePaymentCustDtls.Controls.Add(this.label23);
            this.grpbxCreatePaymentCustDtls.Controls.Add(this.label24);
            this.grpbxCreatePaymentCustDtls.Location = new System.Drawing.Point(377, 12);
            this.grpbxCreatePaymentCustDtls.Name = "grpbxCreatePaymentCustDtls";
            this.grpbxCreatePaymentCustDtls.Size = new System.Drawing.Size(327, 291);
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
            // txtCreatePaymentOB
            // 
            this.txtCreatePaymentOB.Location = new System.Drawing.Point(105, 166);
            this.txtCreatePaymentOB.Name = "txtCreatePaymentOB";
            this.txtCreatePaymentOB.ReadOnly = true;
            this.txtCreatePaymentOB.Size = new System.Drawing.Size(105, 20);
            this.txtCreatePaymentOB.TabIndex = 2;
            // 
            // txtCreatePaymentBA
            // 
            this.txtCreatePaymentBA.Location = new System.Drawing.Point(105, 192);
            this.txtCreatePaymentBA.Name = "txtCreatePaymentBA";
            this.txtCreatePaymentBA.ReadOnly = true;
            this.txtCreatePaymentBA.Size = new System.Drawing.Size(105, 20);
            this.txtCreatePaymentBA.TabIndex = 2;
            // 
            // txtCreatePaymentCustName
            // 
            this.txtCreatePaymentCustName.Enabled = false;
            this.txtCreatePaymentCustName.Location = new System.Drawing.Point(105, 24);
            this.txtCreatePaymentCustName.Name = "txtCreatePaymentCustName";
            this.txtCreatePaymentCustName.Size = new System.Drawing.Size(105, 20);
            this.txtCreatePaymentCustName.TabIndex = 2;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(34, 169);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 13);
            this.label17.TabIndex = 4;
            this.label17.Text = "Old Balance";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(14, 195);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(85, 13);
            this.label23.TabIndex = 4;
            this.label23.Text = "Balance Amount";
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
            // btnCreateUpdatePayment
            // 
            this.btnCreateUpdatePayment.Location = new System.Drawing.Point(139, 519);
            this.btnCreateUpdatePayment.Name = "btnCreateUpdatePayment";
            this.btnCreateUpdatePayment.Size = new System.Drawing.Size(133, 23);
            this.btnCreateUpdatePayment.TabIndex = 7;
            this.btnCreateUpdatePayment.Text = "Create Payment";
            this.btnCreateUpdatePayment.UseVisualStyleBackColor = true;
            this.btnCreateUpdatePayment.Click += new System.EventHandler(this.btnAddUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(466, 519);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(133, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(307, 519);
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
            this.lblValidateErrMsg.Location = new System.Drawing.Point(136, 499);
            this.lblValidateErrMsg.Name = "lblValidateErrMsg";
            this.lblValidateErrMsg.Size = new System.Drawing.Size(0, 13);
            this.lblValidateErrMsg.TabIndex = 4;
            this.lblValidateErrMsg.Visible = false;
            // 
            // CreatePaymentForm
            // 
            this.AcceptButton = this.btnCreateUpdatePayment;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(720, 561);
            this.Controls.Add(this.lblValidateErrMsg);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCreateUpdatePayment);
            this.Controls.Add(this.grpbxCreatePaymentCustDtls);
            this.Controls.Add(this.grpbxCreatePaymentInvoiceDtls);
            this.Controls.Add(this.grpbxCreatePaymentPaymentDtls);
            this.Name = "CreatePaymentForm";
            this.Text = "Create Payment";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CreatePaymentForm_FormClosed);
            this.Load += new System.EventHandler(this.CreatePaymentForm_Load);
            this.grpbxCreatePaymentPaymentDtls.ResumeLayout(false);
            this.grpbxCreatePaymentPaymentDtls.PerformLayout();
            this.grpbxCreatePaymentInvoiceDtls.ResumeLayout(false);
            this.grpbxCreatePaymentInvoiceDtls.PerformLayout();
            this.grpbxCreatePaymentCustDtls.ResumeLayout(false);
            this.grpbxCreatePaymentCustDtls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbxCreatePaymentCustomerNames;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbxCreatePaymentPaymentAgainst;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbxCreatePaymentNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbBoxPaymentModes;
        private System.Windows.Forms.TextBox txtbxCreatePaymentAmount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpCreatePaymentPaidOn;
        private System.Windows.Forms.GroupBox grpbxCreatePaymentPaymentDtls;
        private System.Windows.Forms.GroupBox grpbxCreatePaymentInvoiceDtls;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCreatePaymentDiscAmt;
        private System.Windows.Forms.TextBox txtCreatePaymentRefundAmt;
        private System.Windows.Forms.TextBox txtCreatePaymentCancelAmt;
        private System.Windows.Forms.TextBox txtCreatePaymentSaleAmount;
        private System.Windows.Forms.TextBox txtCreatePaymentInvoiceItems;
        private System.Windows.Forms.TextBox txtCreatePaymentInvoiceDate;
        private System.Windows.Forms.TextBox txtCreatePaymentInvoiceNum;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtCreatePaymentTotalTax;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtCreatePaymentNetSaleAmt;
        private System.Windows.Forms.GroupBox grpbxCreatePaymentCustDtls;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtCreatePaymentCustAddress;
        private System.Windows.Forms.TextBox txtCreatePaymentBA;
        private System.Windows.Forms.TextBox txtCreatePaymentCustName;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btnCreateUpdatePayment;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TextBox txtCreatePaymentDesc;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox chckbxCreatePaymentMarkInvoiceAsPaid;
        private System.Windows.Forms.TextBox txtCreatePaymentOB;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cmbxcreatePaymentStaffName;
        private System.Windows.Forms.Label lblValidateErrMsg;
        private System.Windows.Forms.CheckBox chckActive;
        private System.Windows.Forms.TextBox txtCreatePaymentsCustPhoneNo;
        private System.Windows.Forms.Label lblCreatePaymentCustPhoneno;
    }
}