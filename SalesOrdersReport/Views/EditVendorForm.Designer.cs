using System;

namespace SalesOrdersReport
{
    partial class EditVendorForm
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
            this.btnReset = new System.Windows.Forms.Button();
            this.btnEditVendor = new System.Windows.Forms.Button();
            this.grpEditVendor = new System.Windows.Forms.GroupBox();
            this.cmbxEditVendorSelectState = new System.Windows.Forms.ComboBox();
            this.lblEditVendorStateCode = new System.Windows.Forms.Label();
            this.txtEditVendorAddress = new System.Windows.Forms.TextBox();
            this.lblCommonErrorMsg = new System.Windows.Forms.Label();
            this.lblVendorActiveAsterik = new System.Windows.Forms.Label();
            this.lblEditIsVendorActive = new System.Windows.Forms.Label();
            this.rdbtnEditVendorActiveNo = new System.Windows.Forms.RadioButton();
            this.rdbtnEditVendorActiveYes = new System.Windows.Forms.RadioButton();
            this.txtEditVendorPhone = new System.Windows.Forms.TextBox();
            this.lblEditVendorPhoneNo = new System.Windows.Forms.Label();
            this.txtEditGSTIN = new System.Windows.Forms.TextBox();
            this.lblEditVendorGSTIN = new System.Windows.Forms.Label();
            this.lblCreateVendorAddrs = new System.Windows.Forms.Label();
            this.lblEditVendorName = new System.Windows.Forms.Label();
            this.txtEditVendorName = new System.Windows.Forms.TextBox();
            this.grpEditVendor.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(232, 278);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(97, 39);
            this.btnReset.TabIndex = 18;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnEditCutomerReset_Click);
            // 
            // btnEditVendor
            // 
            this.btnEditVendor.Location = new System.Drawing.Point(113, 278);
            this.btnEditVendor.Name = "btnEditVendor";
            this.btnEditVendor.Size = new System.Drawing.Size(113, 39);
            this.btnEditVendor.TabIndex = 17;
            this.btnEditVendor.Text = "Edit Vendor";
            this.btnEditVendor.UseVisualStyleBackColor = true;
            this.btnEditVendor.Click += new System.EventHandler(this.btnEditVendor_Click);
            // 
            // grpEditVendor
            // 
            this.grpEditVendor.Controls.Add(this.cmbxEditVendorSelectState);
            this.grpEditVendor.Controls.Add(this.lblEditVendorStateCode);
            this.grpEditVendor.Controls.Add(this.txtEditVendorAddress);
            this.grpEditVendor.Controls.Add(this.lblCommonErrorMsg);
            this.grpEditVendor.Controls.Add(this.lblVendorActiveAsterik);
            this.grpEditVendor.Controls.Add(this.lblEditIsVendorActive);
            this.grpEditVendor.Controls.Add(this.rdbtnEditVendorActiveNo);
            this.grpEditVendor.Controls.Add(this.rdbtnEditVendorActiveYes);
            this.grpEditVendor.Controls.Add(this.txtEditVendorPhone);
            this.grpEditVendor.Controls.Add(this.lblEditVendorPhoneNo);
            this.grpEditVendor.Controls.Add(this.txtEditGSTIN);
            this.grpEditVendor.Controls.Add(this.lblEditVendorGSTIN);
            this.grpEditVendor.Controls.Add(this.btnReset);
            this.grpEditVendor.Controls.Add(this.lblCreateVendorAddrs);
            this.grpEditVendor.Controls.Add(this.btnEditVendor);
            this.grpEditVendor.Controls.Add(this.lblEditVendorName);
            this.grpEditVendor.Controls.Add(this.txtEditVendorName);
            this.grpEditVendor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpEditVendor.Location = new System.Drawing.Point(12, 12);
            this.grpEditVendor.Name = "grpEditVendor";
            this.grpEditVendor.Size = new System.Drawing.Size(344, 340);
            this.grpEditVendor.TabIndex = 0;
            this.grpEditVendor.TabStop = false;
            this.grpEditVendor.Text = "Edit Vendor";
            // 
            // cmbxEditVendorSelectState
            // 
            this.cmbxEditVendorSelectState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxEditVendorSelectState.FormattingEnabled = true;
            this.cmbxEditVendorSelectState.Location = new System.Drawing.Point(113, 201);
            this.cmbxEditVendorSelectState.Name = "cmbxEditVendorSelectState";
            this.cmbxEditVendorSelectState.Size = new System.Drawing.Size(169, 21);
            this.cmbxEditVendorSelectState.TabIndex = 5;
            // 
            // lblEditVendorStateCode
            // 
            this.lblEditVendorStateCode.AutoSize = true;
            this.lblEditVendorStateCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditVendorStateCode.Location = new System.Drawing.Point(6, 200);
            this.lblEditVendorStateCode.Name = "lblEditVendorStateCode";
            this.lblEditVendorStateCode.Size = new System.Drawing.Size(43, 13);
            this.lblEditVendorStateCode.TabIndex = 24;
            this.lblEditVendorStateCode.Text = "StateID";
            // 
            // txtEditVendorAddress
            // 
            this.txtEditVendorAddress.Location = new System.Drawing.Point(113, 54);
            this.txtEditVendorAddress.Multiline = true;
            this.txtEditVendorAddress.Name = "txtEditVendorAddress";
            this.txtEditVendorAddress.Size = new System.Drawing.Size(208, 89);
            this.txtEditVendorAddress.TabIndex = 1;
            // 
            // lblCommonErrorMsg
            // 
            this.lblCommonErrorMsg.AutoSize = true;
            this.lblCommonErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.lblCommonErrorMsg.Location = new System.Drawing.Point(113, 262);
            this.lblCommonErrorMsg.Name = "lblCommonErrorMsg";
            this.lblCommonErrorMsg.Size = new System.Drawing.Size(0, 13);
            this.lblCommonErrorMsg.TabIndex = 29;
            // 
            // lblVendorActiveAsterik
            // 
            this.lblVendorActiveAsterik.AutoSize = true;
            this.lblVendorActiveAsterik.ForeColor = System.Drawing.Color.Red;
            this.lblVendorActiveAsterik.Location = new System.Drawing.Point(38, 227);
            this.lblVendorActiveAsterik.Name = "lblVendorActiveAsterik";
            this.lblVendorActiveAsterik.Size = new System.Drawing.Size(11, 13);
            this.lblVendorActiveAsterik.TabIndex = 30;
            this.lblVendorActiveAsterik.Text = "*";
            // 
            // lblEditIsVendorActive
            // 
            this.lblEditIsVendorActive.AutoSize = true;
            this.lblEditIsVendorActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditIsVendorActive.Location = new System.Drawing.Point(6, 227);
            this.lblEditIsVendorActive.Name = "lblEditIsVendorActive";
            this.lblEditIsVendorActive.Size = new System.Drawing.Size(37, 13);
            this.lblEditIsVendorActive.TabIndex = 26;
            this.lblEditIsVendorActive.Text = "Active";
            // 
            // rdbtnEditVendorActiveNo
            // 
            this.rdbtnEditVendorActiveNo.AutoSize = true;
            this.rdbtnEditVendorActiveNo.Location = new System.Drawing.Point(169, 228);
            this.rdbtnEditVendorActiveNo.Name = "rdbtnEditVendorActiveNo";
            this.rdbtnEditVendorActiveNo.Size = new System.Drawing.Size(39, 17);
            this.rdbtnEditVendorActiveNo.TabIndex = 8;
            this.rdbtnEditVendorActiveNo.Text = "No";
            this.rdbtnEditVendorActiveNo.UseVisualStyleBackColor = true;
            // 
            // rdbtnEditVendorActiveYes
            // 
            this.rdbtnEditVendorActiveYes.AutoSize = true;
            this.rdbtnEditVendorActiveYes.Location = new System.Drawing.Point(113, 228);
            this.rdbtnEditVendorActiveYes.Name = "rdbtnEditVendorActiveYes";
            this.rdbtnEditVendorActiveYes.Size = new System.Drawing.Size(43, 17);
            this.rdbtnEditVendorActiveYes.TabIndex = 7;
            this.rdbtnEditVendorActiveYes.Text = "Yes";
            this.rdbtnEditVendorActiveYes.UseVisualStyleBackColor = true;
            // 
            // txtEditVendorPhone
            // 
            this.txtEditVendorPhone.Location = new System.Drawing.Point(113, 175);
            this.txtEditVendorPhone.Name = "txtEditVendorPhone";
            this.txtEditVendorPhone.Size = new System.Drawing.Size(208, 20);
            this.txtEditVendorPhone.TabIndex = 3;
            // 
            // lblEditVendorPhoneNo
            // 
            this.lblEditVendorPhoneNo.AutoSize = true;
            this.lblEditVendorPhoneNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditVendorPhoneNo.Location = new System.Drawing.Point(6, 178);
            this.lblEditVendorPhoneNo.Name = "lblEditVendorPhoneNo";
            this.lblEditVendorPhoneNo.Size = new System.Drawing.Size(38, 13);
            this.lblEditVendorPhoneNo.TabIndex = 22;
            this.lblEditVendorPhoneNo.Text = "Phone";
            // 
            // txtEditGSTIN
            // 
            this.txtEditGSTIN.Location = new System.Drawing.Point(113, 149);
            this.txtEditGSTIN.Name = "txtEditGSTIN";
            this.txtEditGSTIN.Size = new System.Drawing.Size(208, 20);
            this.txtEditGSTIN.TabIndex = 2;
            // 
            // lblEditVendorGSTIN
            // 
            this.lblEditVendorGSTIN.AutoSize = true;
            this.lblEditVendorGSTIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditVendorGSTIN.Location = new System.Drawing.Point(6, 148);
            this.lblEditVendorGSTIN.Name = "lblEditVendorGSTIN";
            this.lblEditVendorGSTIN.Size = new System.Drawing.Size(40, 13);
            this.lblEditVendorGSTIN.TabIndex = 21;
            this.lblEditVendorGSTIN.Text = "GSTIN";
            // 
            // lblCreateVendorAddrs
            // 
            this.lblCreateVendorAddrs.AutoSize = true;
            this.lblCreateVendorAddrs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateVendorAddrs.Location = new System.Drawing.Point(6, 54);
            this.lblCreateVendorAddrs.Name = "lblCreateVendorAddrs";
            this.lblCreateVendorAddrs.Size = new System.Drawing.Size(45, 13);
            this.lblCreateVendorAddrs.TabIndex = 20;
            this.lblCreateVendorAddrs.Text = "Address";
            // 
            // lblEditVendorName
            // 
            this.lblEditVendorName.AutoSize = true;
            this.lblEditVendorName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditVendorName.Location = new System.Drawing.Point(6, 28);
            this.lblEditVendorName.Name = "lblEditVendorName";
            this.lblEditVendorName.Size = new System.Drawing.Size(72, 13);
            this.lblEditVendorName.TabIndex = 19;
            this.lblEditVendorName.Text = "Vendor Name";
            // 
            // txtEditVendorName
            // 
            this.txtEditVendorName.Enabled = false;
            this.txtEditVendorName.Location = new System.Drawing.Point(113, 28);
            this.txtEditVendorName.Name = "txtEditVendorName";
            this.txtEditVendorName.Size = new System.Drawing.Size(208, 20);
            this.txtEditVendorName.TabIndex = 0;
            // 
            // EditVendorForm
            // 
            this.AcceptButton = this.btnEditVendor;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 370);
            this.Controls.Add(this.grpEditVendor);
            this.Name = "EditVendorForm";
            this.ShowIcon = false;
            this.Text = "Edit Vendor Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditVendorForm_FormClosed);
            this.grpEditVendor.ResumeLayout(false);
            this.grpEditVendor.PerformLayout();
            this.ResumeLayout(false);

        }



        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnEditVendor;
        private System.Windows.Forms.GroupBox grpEditVendor;
        private System.Windows.Forms.Label lblCreateVendorAddrs;
        private System.Windows.Forms.Label lblEditVendorName;
        public System.Windows.Forms.TextBox txtEditVendorName;
        #endregion

        private System.Windows.Forms.Label lblEditVendorGSTIN;
        public System.Windows.Forms.TextBox txtEditGSTIN;
        //private System.Windows.Forms.TextBox txtAddress;
        //private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtEditVendorPhone;
        private System.Windows.Forms.Label lblEditVendorPhoneNo;
        public System.Windows.Forms.RadioButton rdbtnEditVendorActiveNo;
        public System.Windows.Forms.RadioButton rdbtnEditVendorActiveYes;
        private System.Windows.Forms.Label lblEditIsVendorActive;
        private System.Windows.Forms.Label lblVendorActiveAsterik;
        private System.Windows.Forms.Label lblCommonErrorMsg;
        public System.Windows.Forms.TextBox txtEditVendorAddress;
        private System.Windows.Forms.Label lblEditVendorStateCode;
        public System.Windows.Forms.ComboBox cmbxEditVendorSelectState;
    }
}