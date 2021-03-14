using System;

namespace SalesOrdersReport
{
    partial class CreateVendorForm
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
            this.btnCreateVendor = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbxCreateCustSelectState = new System.Windows.Forms.ComboBox();
            this.lblCreateCustStateCode = new System.Windows.Forms.Label();
            this.txtCustAddress = new System.Windows.Forms.TextBox();
            this.lblCommonErrorMsg = new System.Windows.Forms.Label();
            this.lblCustActiveAsterik = new System.Windows.Forms.Label();
            this.lblCreateVendorAsterik = new System.Windows.Forms.Label();
            this.lblIsCustActive = new System.Windows.Forms.Label();
            this.rdbtnCustActiveNo = new System.Windows.Forms.RadioButton();
            this.rdbtnCustActiveYes = new System.Windows.Forms.RadioButton();
            this.txtCreateCustPhone = new System.Windows.Forms.TextBox();
            this.lblCreateCustPhoneNo = new System.Windows.Forms.Label();
            this.txtGSTIN = new System.Windows.Forms.TextBox();
            this.lblCreateCustGSTIN = new System.Windows.Forms.Label();
            this.lblCreateCustAddrs = new System.Windows.Forms.Label();
            this.lblCreateCustName = new System.Windows.Forms.Label();
            this.txtCreateVendorName = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(232, 282);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(97, 39);
            this.btnReset.TabIndex = 18;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnCreateVendorReset_Click);
            // 
            // btnCreateVendor
            // 
            this.btnCreateVendor.Location = new System.Drawing.Point(113, 282);
            this.btnCreateVendor.Name = "btnCreateVendor";
            this.btnCreateVendor.Size = new System.Drawing.Size(113, 39);
            this.btnCreateVendor.TabIndex = 17;
            this.btnCreateVendor.Text = "Create Vendor";
            this.btnCreateVendor.UseVisualStyleBackColor = true;
            this.btnCreateVendor.Click += new System.EventHandler(this.btnCreateVendor_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbxCreateCustSelectState);
            this.groupBox1.Controls.Add(this.lblCreateCustStateCode);
            this.groupBox1.Controls.Add(this.txtCustAddress);
            this.groupBox1.Controls.Add(this.lblCommonErrorMsg);
            this.groupBox1.Controls.Add(this.lblCustActiveAsterik);
            this.groupBox1.Controls.Add(this.lblCreateVendorAsterik);
            this.groupBox1.Controls.Add(this.lblIsCustActive);
            this.groupBox1.Controls.Add(this.rdbtnCustActiveNo);
            this.groupBox1.Controls.Add(this.rdbtnCustActiveYes);
            this.groupBox1.Controls.Add(this.txtCreateCustPhone);
            this.groupBox1.Controls.Add(this.lblCreateCustPhoneNo);
            this.groupBox1.Controls.Add(this.txtGSTIN);
            this.groupBox1.Controls.Add(this.lblCreateCustGSTIN);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.lblCreateCustAddrs);
            this.groupBox1.Controls.Add(this.btnCreateVendor);
            this.groupBox1.Controls.Add(this.lblCreateCustName);
            this.groupBox1.Controls.Add(this.txtCreateVendorName);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 347);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create Vendor";
            // 
            // cmbxCreateCustSelectState
            // 
            this.cmbxCreateCustSelectState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxCreateCustSelectState.FormattingEnabled = true;
            this.cmbxCreateCustSelectState.Location = new System.Drawing.Point(113, 201);
            this.cmbxCreateCustSelectState.Name = "cmbxCreateCustSelectState";
            this.cmbxCreateCustSelectState.Size = new System.Drawing.Size(171, 21);
            this.cmbxCreateCustSelectState.TabIndex = 5;
            // 
            // lblCreateCustStateCode
            // 
            this.lblCreateCustStateCode.AutoSize = true;
            this.lblCreateCustStateCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateCustStateCode.Location = new System.Drawing.Point(6, 200);
            this.lblCreateCustStateCode.Name = "lblCreateCustStateCode";
            this.lblCreateCustStateCode.Size = new System.Drawing.Size(32, 13);
            this.lblCreateCustStateCode.TabIndex = 24;
            this.lblCreateCustStateCode.Text = "State";
            // 
            // txtCustAddress
            // 
            this.txtCustAddress.Location = new System.Drawing.Point(113, 54);
            this.txtCustAddress.Multiline = true;
            this.txtCustAddress.Name = "txtCustAddress";
            this.txtCustAddress.Size = new System.Drawing.Size(208, 89);
            this.txtCustAddress.TabIndex = 1;
            // 
            // lblCommonErrorMsg
            // 
            this.lblCommonErrorMsg.AutoSize = true;
            this.lblCommonErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.lblCommonErrorMsg.Location = new System.Drawing.Point(110, 260);
            this.lblCommonErrorMsg.Name = "lblCommonErrorMsg";
            this.lblCommonErrorMsg.Size = new System.Drawing.Size(0, 13);
            this.lblCommonErrorMsg.TabIndex = 29;
            // 
            // lblCustActiveAsterik
            // 
            this.lblCustActiveAsterik.AutoSize = true;
            this.lblCustActiveAsterik.ForeColor = System.Drawing.Color.Red;
            this.lblCustActiveAsterik.Location = new System.Drawing.Point(38, 231);
            this.lblCustActiveAsterik.Name = "lblCustActiveAsterik";
            this.lblCustActiveAsterik.Size = new System.Drawing.Size(11, 13);
            this.lblCustActiveAsterik.TabIndex = 30;
            this.lblCustActiveAsterik.Text = "*";
            // 
            // lblCreateVendorAsterik
            // 
            this.lblCreateVendorAsterik.AutoSize = true;
            this.lblCreateVendorAsterik.ForeColor = System.Drawing.Color.Red;
            this.lblCreateVendorAsterik.Location = new System.Drawing.Point(74, 27);
            this.lblCreateVendorAsterik.Name = "lblCreateVendorAsterik";
            this.lblCreateVendorAsterik.Size = new System.Drawing.Size(11, 13);
            this.lblCreateVendorAsterik.TabIndex = 29;
            this.lblCreateVendorAsterik.Text = "*";
            // 
            // lblIsCustActive
            // 
            this.lblIsCustActive.AutoSize = true;
            this.lblIsCustActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIsCustActive.Location = new System.Drawing.Point(6, 231);
            this.lblIsCustActive.Name = "lblIsCustActive";
            this.lblIsCustActive.Size = new System.Drawing.Size(37, 13);
            this.lblIsCustActive.TabIndex = 26;
            this.lblIsCustActive.Text = "Active";
            // 
            // rdbtnCustActiveNo
            // 
            this.rdbtnCustActiveNo.AutoSize = true;
            this.rdbtnCustActiveNo.Location = new System.Drawing.Point(169, 232);
            this.rdbtnCustActiveNo.Name = "rdbtnCustActiveNo";
            this.rdbtnCustActiveNo.Size = new System.Drawing.Size(39, 17);
            this.rdbtnCustActiveNo.TabIndex = 8;
            this.rdbtnCustActiveNo.Text = "No";
            this.rdbtnCustActiveNo.UseVisualStyleBackColor = true;
            // 
            // rdbtnCustActiveYes
            // 
            this.rdbtnCustActiveYes.AutoSize = true;
            this.rdbtnCustActiveYes.Location = new System.Drawing.Point(113, 232);
            this.rdbtnCustActiveYes.Name = "rdbtnCustActiveYes";
            this.rdbtnCustActiveYes.Size = new System.Drawing.Size(43, 17);
            this.rdbtnCustActiveYes.TabIndex = 7;
            this.rdbtnCustActiveYes.Text = "Yes";
            this.rdbtnCustActiveYes.UseVisualStyleBackColor = true;
            // 
            // txtCreateCustPhone
            // 
            this.txtCreateCustPhone.Location = new System.Drawing.Point(113, 175);
            this.txtCreateCustPhone.Name = "txtCreateCustPhone";
            this.txtCreateCustPhone.Size = new System.Drawing.Size(208, 20);
            this.txtCreateCustPhone.TabIndex = 3;
            // 
            // lblCreateCustPhoneNo
            // 
            this.lblCreateCustPhoneNo.AutoSize = true;
            this.lblCreateCustPhoneNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateCustPhoneNo.Location = new System.Drawing.Point(6, 178);
            this.lblCreateCustPhoneNo.Name = "lblCreateCustPhoneNo";
            this.lblCreateCustPhoneNo.Size = new System.Drawing.Size(38, 13);
            this.lblCreateCustPhoneNo.TabIndex = 22;
            this.lblCreateCustPhoneNo.Text = "Phone";
            // 
            // txtGSTIN
            // 
            this.txtGSTIN.Location = new System.Drawing.Point(113, 149);
            this.txtGSTIN.Name = "txtGSTIN";
            this.txtGSTIN.Size = new System.Drawing.Size(208, 20);
            this.txtGSTIN.TabIndex = 2;
            // 
            // lblCreateCustGSTIN
            // 
            this.lblCreateCustGSTIN.AutoSize = true;
            this.lblCreateCustGSTIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateCustGSTIN.Location = new System.Drawing.Point(6, 148);
            this.lblCreateCustGSTIN.Name = "lblCreateCustGSTIN";
            this.lblCreateCustGSTIN.Size = new System.Drawing.Size(40, 13);
            this.lblCreateCustGSTIN.TabIndex = 21;
            this.lblCreateCustGSTIN.Text = "GSTIN";
            // 
            // lblCreateCustAddrs
            // 
            this.lblCreateCustAddrs.AutoSize = true;
            this.lblCreateCustAddrs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateCustAddrs.Location = new System.Drawing.Point(6, 54);
            this.lblCreateCustAddrs.Name = "lblCreateCustAddrs";
            this.lblCreateCustAddrs.Size = new System.Drawing.Size(45, 13);
            this.lblCreateCustAddrs.TabIndex = 20;
            this.lblCreateCustAddrs.Text = "Address";
            // 
            // lblCreateCustName
            // 
            this.lblCreateCustName.AutoSize = true;
            this.lblCreateCustName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateCustName.Location = new System.Drawing.Point(6, 28);
            this.lblCreateCustName.Name = "lblCreateCustName";
            this.lblCreateCustName.Size = new System.Drawing.Size(72, 13);
            this.lblCreateCustName.TabIndex = 19;
            this.lblCreateCustName.Text = "Vendor Name";
            // 
            // txtCreateVendorName
            // 
            this.txtCreateVendorName.Location = new System.Drawing.Point(113, 28);
            this.txtCreateVendorName.Name = "txtCreateVendorName";
            this.txtCreateVendorName.Size = new System.Drawing.Size(208, 20);
            this.txtCreateVendorName.TabIndex = 0;
            // 
            // CreateVendorForm
            // 
            this.AcceptButton = this.btnCreateVendor;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 368);
            this.Controls.Add(this.groupBox1);
            this.Name = "CreateVendorForm";
            this.ShowIcon = false;
            this.Text = "Create Vendor Form";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }



        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCreateVendor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCreateCustAddrs;
        private System.Windows.Forms.Label lblCreateCustName;
        private System.Windows.Forms.TextBox txtCreateVendorName;
        #endregion

        private System.Windows.Forms.Label lblCreateCustGSTIN;
        private System.Windows.Forms.TextBox txtGSTIN;
        //private System.Windows.Forms.TextBox txtAddress;
        //private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCreateCustPhone;
        private System.Windows.Forms.Label lblCreateCustPhoneNo;
        private System.Windows.Forms.RadioButton rdbtnCustActiveNo;
        private System.Windows.Forms.RadioButton rdbtnCustActiveYes;
        private System.Windows.Forms.Label lblIsCustActive;
        private System.Windows.Forms.Label lblCreateVendorAsterik;
        private System.Windows.Forms.Label lblCustActiveAsterik;
        private System.Windows.Forms.Label lblCommonErrorMsg;
        private System.Windows.Forms.TextBox txtCustAddress;
        private System.Windows.Forms.Label lblCreateCustStateCode;
        private System.Windows.Forms.ComboBox cmbxCreateCustSelectState;
    }
}