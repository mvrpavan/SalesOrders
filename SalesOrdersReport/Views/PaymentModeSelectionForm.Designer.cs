namespace SalesOrdersReport.Views
{
    partial class PaymentModeSelectionForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBoxPaymentModes = new System.Windows.Forms.ComboBox();
            this.dtGridViewPaymentModes = new System.Windows.Forms.DataGridView();
            this.btnAddPaymentMode = new System.Windows.Forms.Button();
            this.btnDeletePaymentMode = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblBalanceAmount = new System.Windows.Forms.Label();
            this.lblPaymentAmount = new System.Windows.Forms.Label();
            this.lblBillAmount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreatePayment = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtBoxCardNumber = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBoxAmount = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.errorProviderPaymentModeForm = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewPaymentModes)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPaymentModeForm)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Payment Mode";
            // 
            // cmbBoxPaymentModes
            // 
            this.cmbBoxPaymentModes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxPaymentModes.FormattingEnabled = true;
            this.cmbBoxPaymentModes.Location = new System.Drawing.Point(97, 20);
            this.cmbBoxPaymentModes.Name = "cmbBoxPaymentModes";
            this.cmbBoxPaymentModes.Size = new System.Drawing.Size(105, 21);
            this.cmbBoxPaymentModes.TabIndex = 1;
            this.cmbBoxPaymentModes.SelectedIndexChanged += new System.EventHandler(this.cmbBoxPaymentModes_SelectedIndexChanged);
            // 
            // dtGridViewPaymentModes
            // 
            this.dtGridViewPaymentModes.AllowUserToAddRows = false;
            this.dtGridViewPaymentModes.AllowUserToDeleteRows = false;
            this.dtGridViewPaymentModes.AllowUserToResizeRows = false;
            this.dtGridViewPaymentModes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtGridViewPaymentModes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewPaymentModes.Location = new System.Drawing.Point(12, 83);
            this.dtGridViewPaymentModes.MultiSelect = false;
            this.dtGridViewPaymentModes.Name = "dtGridViewPaymentModes";
            this.dtGridViewPaymentModes.ReadOnly = true;
            this.dtGridViewPaymentModes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGridViewPaymentModes.Size = new System.Drawing.Size(260, 150);
            this.dtGridViewPaymentModes.TabIndex = 2;
            // 
            // btnAddPaymentMode
            // 
            this.btnAddPaymentMode.FlatAppearance.BorderSize = 0;
            this.btnAddPaymentMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPaymentMode.Image = global::SalesOrdersReport.Properties.Resources.add;
            this.btnAddPaymentMode.Location = new System.Drawing.Point(273, 20);
            this.btnAddPaymentMode.Name = "btnAddPaymentMode";
            this.btnAddPaymentMode.Size = new System.Drawing.Size(27, 23);
            this.btnAddPaymentMode.TabIndex = 3;
            this.btnAddPaymentMode.UseVisualStyleBackColor = true;
            this.btnAddPaymentMode.Click += new System.EventHandler(this.btnAddPaymentMode_Click);
            // 
            // btnDeletePaymentMode
            // 
            this.btnDeletePaymentMode.FlatAppearance.BorderSize = 0;
            this.btnDeletePaymentMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeletePaymentMode.Image = global::SalesOrdersReport.Properties.Resources.negative;
            this.btnDeletePaymentMode.Location = new System.Drawing.Point(278, 83);
            this.btnDeletePaymentMode.Name = "btnDeletePaymentMode";
            this.btnDeletePaymentMode.Size = new System.Drawing.Size(22, 23);
            this.btnDeletePaymentMode.TabIndex = 3;
            this.btnDeletePaymentMode.UseVisualStyleBackColor = true;
            this.btnDeletePaymentMode.Click += new System.EventHandler(this.btnDeletePaymentMode_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblBalanceAmount);
            this.groupBox1.Controls.Add(this.lblPaymentAmount);
            this.groupBox1.Controls.Add(this.lblBillAmount);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 239);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 100);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // lblBalanceAmount
            // 
            this.lblBalanceAmount.AutoSize = true;
            this.lblBalanceAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceAmount.ForeColor = System.Drawing.Color.DarkRed;
            this.lblBalanceAmount.Location = new System.Drawing.Point(165, 74);
            this.lblBalanceAmount.Name = "lblBalanceAmount";
            this.lblBalanceAmount.Size = new System.Drawing.Size(89, 17);
            this.lblBalanceAmount.TabIndex = 0;
            this.lblBalanceAmount.Text = "Bill Amount";
            // 
            // lblPaymentAmount
            // 
            this.lblPaymentAmount.AutoSize = true;
            this.lblPaymentAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentAmount.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblPaymentAmount.Location = new System.Drawing.Point(165, 45);
            this.lblPaymentAmount.Name = "lblPaymentAmount";
            this.lblPaymentAmount.Size = new System.Drawing.Size(89, 17);
            this.lblPaymentAmount.TabIndex = 0;
            this.lblPaymentAmount.Text = "Bill Amount";
            // 
            // lblBillAmount
            // 
            this.lblBillAmount.AutoSize = true;
            this.lblBillAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillAmount.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblBillAmount.Location = new System.Drawing.Point(165, 16);
            this.lblBillAmount.Name = "lblBillAmount";
            this.lblBillAmount.Size = new System.Drawing.Size(89, 17);
            this.lblBillAmount.TabIndex = 0;
            this.lblBillAmount.Text = "Bill Amount";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(6, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Balance Amount";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(6, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Payment Amount";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Bill Amount";
            // 
            // btnCreatePayment
            // 
            this.btnCreatePayment.Location = new System.Drawing.Point(25, 346);
            this.btnCreatePayment.Name = "btnCreatePayment";
            this.btnCreatePayment.Size = new System.Drawing.Size(75, 23);
            this.btnCreatePayment.TabIndex = 3;
            this.btnCreatePayment.Text = "OK";
            this.btnCreatePayment.UseVisualStyleBackColor = true;
            this.btnCreatePayment.Click += new System.EventHandler(this.btnCreatePayment_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(197, 346);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtBoxCardNumber
            // 
            this.txtBoxCardNumber.Location = new System.Drawing.Point(213, 47);
            this.txtBoxCardNumber.Name = "txtBoxCardNumber";
            this.txtBoxCardNumber.Size = new System.Drawing.Size(36, 20);
            this.txtBoxCardNumber.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Card Number";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(94, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "XXXX - XXXX - XXXX - ";
            // 
            // txtBoxAmount
            // 
            this.txtBoxAmount.Location = new System.Drawing.Point(213, 20);
            this.txtBoxAmount.Name = "txtBoxAmount";
            this.txtBoxAmount.Size = new System.Drawing.Size(54, 20);
            this.txtBoxAmount.TabIndex = 5;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(111, 346);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // errorProviderPaymentModeForm
            // 
            this.errorProviderPaymentModeForm.ContainerControl = this;
            // 
            // PaymentModeSelectionForm
            // 
            this.AcceptButton = this.btnCreatePayment;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(307, 381);
            this.Controls.Add(this.txtBoxAmount);
            this.Controls.Add(this.txtBoxCardNumber);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnCreatePayment);
            this.Controls.Add(this.btnDeletePaymentMode);
            this.Controls.Add(this.btnAddPaymentMode);
            this.Controls.Add(this.dtGridViewPaymentModes);
            this.Controls.Add(this.cmbBoxPaymentModes);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Name = "PaymentModeSelectionForm";
            this.Text = "PaymentModeSelectionForm";
            this.Load += new System.EventHandler(this.PaymentModeSelectionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewPaymentModes)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderPaymentModeForm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBoxPaymentModes;
        private System.Windows.Forms.DataGridView dtGridViewPaymentModes;
        private System.Windows.Forms.Button btnAddPaymentMode;
        private System.Windows.Forms.Button btnDeletePaymentMode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCreatePayment;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblBillAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblBalanceAmount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxCardNumber;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBoxAmount;
        private System.Windows.Forms.Label lblPaymentAmount;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ErrorProvider errorProviderPaymentModeForm;
    }
}