using System;

namespace SalesOrdersReport
{
    partial class EditCustomerForm
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
            this.btnEditCustomer = new System.Windows.Forms.Button();
            this.grpEditCustomer = new System.Windows.Forms.GroupBox();
            this.flpEditCustOrderDays = new System.Windows.Forms.FlowLayoutPanel();
            this.chbxMonday = new System.Windows.Forms.CheckBox();
            this.chbxThursday = new System.Windows.Forms.CheckBox();
            this.chbxTuesday = new System.Windows.Forms.CheckBox();
            this.chbxFriday = new System.Windows.Forms.CheckBox();
            this.chbxWednesday = new System.Windows.Forms.CheckBox();
            this.chbxSaturday = new System.Windows.Forms.CheckBox();
            this.chbxSunday = new System.Windows.Forms.CheckBox();
            this.cmbxEditCustSelectState = new System.Windows.Forms.ComboBox();
            this.lblEditCustOrderDays = new System.Windows.Forms.Label();
            this.lblEditCustStateCode = new System.Windows.Forms.Label();
            this.cmbxEditCustSelectPriceGrp = new System.Windows.Forms.ComboBox();
            this.lblEditCustPriceGrp = new System.Windows.Forms.Label();
            this.txtEditCustAddress = new System.Windows.Forms.TextBox();
            this.lblCommonErrorMsg = new System.Windows.Forms.Label();
            this.lblCustActiveAsterik = new System.Windows.Forms.Label();
            this.lblEditIsCustActive = new System.Windows.Forms.Label();
            this.rdbtnEditCustActiveNo = new System.Windows.Forms.RadioButton();
            this.rdbtnEditCustActiveYes = new System.Windows.Forms.RadioButton();
            this.cmbxEditCustSelectDiscGrp = new System.Windows.Forms.ComboBox();
            this.lblEditCustDiscGrpName = new System.Windows.Forms.Label();
            this.cmbxEditCustSelectLine = new System.Windows.Forms.ComboBox();
            this.lblEditCustLineID = new System.Windows.Forms.Label();
            this.txtEditCustPhone = new System.Windows.Forms.TextBox();
            this.lblEditCustPhoneNo = new System.Windows.Forms.Label();
            this.txtEditGSTIN = new System.Windows.Forms.TextBox();
            this.lblEditCustGSTIN = new System.Windows.Forms.Label();
            this.lblCreateCustAddrs = new System.Windows.Forms.Label();
            this.lblEditCustName = new System.Windows.Forms.Label();
            this.txtEditCustomerName = new System.Windows.Forms.TextBox();
            this.cmbxEditCustType = new System.Windows.Forms.ComboBox();
            this.lblEditCustomerType = new System.Windows.Forms.Label();
            this.grpEditCustomer.SuspendLayout();
            this.flpEditCustOrderDays.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(232, 467);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(97, 39);
            this.btnReset.TabIndex = 18;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnEditCutomerReset_Click);
            // 
            // btnEditCustomer
            // 
            this.btnEditCustomer.Location = new System.Drawing.Point(113, 467);
            this.btnEditCustomer.Name = "btnEditCustomer";
            this.btnEditCustomer.Size = new System.Drawing.Size(113, 39);
            this.btnEditCustomer.TabIndex = 17;
            this.btnEditCustomer.Text = "Edit Customer";
            this.btnEditCustomer.UseVisualStyleBackColor = true;
            this.btnEditCustomer.Click += new System.EventHandler(this.btnEditCustomer_Click);
            // 
            // grpEditCustomer
            // 
            this.grpEditCustomer.Controls.Add(this.cmbxEditCustType);
            this.grpEditCustomer.Controls.Add(this.lblEditCustomerType);
            this.grpEditCustomer.Controls.Add(this.flpEditCustOrderDays);
            this.grpEditCustomer.Controls.Add(this.cmbxEditCustSelectState);
            this.grpEditCustomer.Controls.Add(this.lblEditCustOrderDays);
            this.grpEditCustomer.Controls.Add(this.lblEditCustStateCode);
            this.grpEditCustomer.Controls.Add(this.cmbxEditCustSelectPriceGrp);
            this.grpEditCustomer.Controls.Add(this.lblEditCustPriceGrp);
            this.grpEditCustomer.Controls.Add(this.txtEditCustAddress);
            this.grpEditCustomer.Controls.Add(this.lblCommonErrorMsg);
            this.grpEditCustomer.Controls.Add(this.lblCustActiveAsterik);
            this.grpEditCustomer.Controls.Add(this.lblEditIsCustActive);
            this.grpEditCustomer.Controls.Add(this.rdbtnEditCustActiveNo);
            this.grpEditCustomer.Controls.Add(this.rdbtnEditCustActiveYes);
            this.grpEditCustomer.Controls.Add(this.cmbxEditCustSelectDiscGrp);
            this.grpEditCustomer.Controls.Add(this.lblEditCustDiscGrpName);
            this.grpEditCustomer.Controls.Add(this.cmbxEditCustSelectLine);
            this.grpEditCustomer.Controls.Add(this.lblEditCustLineID);
            this.grpEditCustomer.Controls.Add(this.txtEditCustPhone);
            this.grpEditCustomer.Controls.Add(this.lblEditCustPhoneNo);
            this.grpEditCustomer.Controls.Add(this.txtEditGSTIN);
            this.grpEditCustomer.Controls.Add(this.lblEditCustGSTIN);
            this.grpEditCustomer.Controls.Add(this.btnReset);
            this.grpEditCustomer.Controls.Add(this.lblCreateCustAddrs);
            this.grpEditCustomer.Controls.Add(this.btnEditCustomer);
            this.grpEditCustomer.Controls.Add(this.lblEditCustName);
            this.grpEditCustomer.Controls.Add(this.txtEditCustomerName);
            this.grpEditCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpEditCustomer.Location = new System.Drawing.Point(12, 12);
            this.grpEditCustomer.Name = "grpEditCustomer";
            this.grpEditCustomer.Size = new System.Drawing.Size(344, 513);
            this.grpEditCustomer.TabIndex = 0;
            this.grpEditCustomer.TabStop = false;
            this.grpEditCustomer.Text = "Edit Customer";
            // 
            // flpEditCustOrderDays
            // 
            this.flpEditCustOrderDays.Controls.Add(this.chbxMonday);
            this.flpEditCustOrderDays.Controls.Add(this.chbxThursday);
            this.flpEditCustOrderDays.Controls.Add(this.chbxTuesday);
            this.flpEditCustOrderDays.Controls.Add(this.chbxFriday);
            this.flpEditCustOrderDays.Controls.Add(this.chbxWednesday);
            this.flpEditCustOrderDays.Controls.Add(this.chbxSaturday);
            this.flpEditCustOrderDays.Controls.Add(this.chbxSunday);
            this.flpEditCustOrderDays.Location = new System.Drawing.Point(113, 358);
            this.flpEditCustOrderDays.Name = "flpEditCustOrderDays";
            this.flpEditCustOrderDays.Size = new System.Drawing.Size(208, 90);
            this.flpEditCustOrderDays.TabIndex = 31;
            // 
            // chbxMonday
            // 
            this.chbxMonday.AutoSize = true;
            this.chbxMonday.Location = new System.Drawing.Point(3, 3);
            this.chbxMonday.Name = "chbxMonday";
            this.chbxMonday.Size = new System.Drawing.Size(64, 17);
            this.chbxMonday.TabIndex = 10;
            this.chbxMonday.Text = "Monday";
            this.chbxMonday.UseVisualStyleBackColor = true;
            // 
            // chbxThursday
            // 
            this.chbxThursday.AutoSize = true;
            this.chbxThursday.Location = new System.Drawing.Point(73, 3);
            this.chbxThursday.Name = "chbxThursday";
            this.chbxThursday.Size = new System.Drawing.Size(70, 17);
            this.chbxThursday.TabIndex = 13;
            this.chbxThursday.Text = "Thursday";
            this.chbxThursday.UseVisualStyleBackColor = true;
            // 
            // chbxTuesday
            // 
            this.chbxTuesday.AutoSize = true;
            this.chbxTuesday.Location = new System.Drawing.Point(3, 26);
            this.chbxTuesday.Name = "chbxTuesday";
            this.chbxTuesday.Size = new System.Drawing.Size(67, 17);
            this.chbxTuesday.TabIndex = 11;
            this.chbxTuesday.Text = "Tuesday";
            this.chbxTuesday.UseVisualStyleBackColor = true;
            // 
            // chbxFriday
            // 
            this.chbxFriday.AutoSize = true;
            this.chbxFriday.Location = new System.Drawing.Point(76, 26);
            this.chbxFriday.Name = "chbxFriday";
            this.chbxFriday.Size = new System.Drawing.Size(54, 17);
            this.chbxFriday.TabIndex = 14;
            this.chbxFriday.Text = "Friday";
            this.chbxFriday.UseVisualStyleBackColor = true;
            // 
            // chbxWednesday
            // 
            this.chbxWednesday.AutoSize = true;
            this.chbxWednesday.Location = new System.Drawing.Point(3, 49);
            this.chbxWednesday.Name = "chbxWednesday";
            this.chbxWednesday.Size = new System.Drawing.Size(83, 17);
            this.chbxWednesday.TabIndex = 12;
            this.chbxWednesday.Text = "Wednesday";
            this.chbxWednesday.UseVisualStyleBackColor = true;
            // 
            // chbxSaturday
            // 
            this.chbxSaturday.AutoSize = true;
            this.chbxSaturday.Location = new System.Drawing.Point(92, 49);
            this.chbxSaturday.Name = "chbxSaturday";
            this.chbxSaturday.Size = new System.Drawing.Size(68, 17);
            this.chbxSaturday.TabIndex = 15;
            this.chbxSaturday.Text = "Saturday";
            this.chbxSaturday.UseVisualStyleBackColor = true;
            // 
            // chbxSunday
            // 
            this.chbxSunday.AutoSize = true;
            this.chbxSunday.Location = new System.Drawing.Point(3, 72);
            this.chbxSunday.Name = "chbxSunday";
            this.chbxSunday.Size = new System.Drawing.Size(62, 17);
            this.chbxSunday.TabIndex = 16;
            this.chbxSunday.Text = "Sunday";
            this.chbxSunday.UseVisualStyleBackColor = true;
            // 
            // cmbxEditCustSelectState
            // 
            this.cmbxEditCustSelectState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxEditCustSelectState.FormattingEnabled = true;
            this.cmbxEditCustSelectState.Location = new System.Drawing.Point(113, 228);
            this.cmbxEditCustSelectState.Name = "cmbxEditCustSelectState";
            this.cmbxEditCustSelectState.Size = new System.Drawing.Size(169, 21);
            this.cmbxEditCustSelectState.TabIndex = 5;
            // 
            // lblEditCustOrderDays
            // 
            this.lblEditCustOrderDays.AutoSize = true;
            this.lblEditCustOrderDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditCustOrderDays.Location = new System.Drawing.Point(6, 358);
            this.lblEditCustOrderDays.Name = "lblEditCustOrderDays";
            this.lblEditCustOrderDays.Size = new System.Drawing.Size(62, 13);
            this.lblEditCustOrderDays.TabIndex = 28;
            this.lblEditCustOrderDays.Text = "OrdersDays";
            // 
            // lblEditCustStateCode
            // 
            this.lblEditCustStateCode.AutoSize = true;
            this.lblEditCustStateCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditCustStateCode.Location = new System.Drawing.Point(6, 227);
            this.lblEditCustStateCode.Name = "lblEditCustStateCode";
            this.lblEditCustStateCode.Size = new System.Drawing.Size(43, 13);
            this.lblEditCustStateCode.TabIndex = 24;
            this.lblEditCustStateCode.Text = "StateID";
            // 
            // cmbxEditCustSelectPriceGrp
            // 
            this.cmbxEditCustSelectPriceGrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxEditCustSelectPriceGrp.FormattingEnabled = true;
            this.cmbxEditCustSelectPriceGrp.Location = new System.Drawing.Point(113, 201);
            this.cmbxEditCustSelectPriceGrp.Name = "cmbxEditCustSelectPriceGrp";
            this.cmbxEditCustSelectPriceGrp.Size = new System.Drawing.Size(169, 21);
            this.cmbxEditCustSelectPriceGrp.TabIndex = 4;
            // 
            // lblEditCustPriceGrp
            // 
            this.lblEditCustPriceGrp.AutoSize = true;
            this.lblEditCustPriceGrp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditCustPriceGrp.Location = new System.Drawing.Point(6, 201);
            this.lblEditCustPriceGrp.Name = "lblEditCustPriceGrp";
            this.lblEditCustPriceGrp.Size = new System.Drawing.Size(63, 13);
            this.lblEditCustPriceGrp.TabIndex = 23;
            this.lblEditCustPriceGrp.Text = "Price Group";
            // 
            // txtEditCustAddress
            // 
            this.txtEditCustAddress.Location = new System.Drawing.Point(113, 54);
            this.txtEditCustAddress.Multiline = true;
            this.txtEditCustAddress.Name = "txtEditCustAddress";
            this.txtEditCustAddress.Size = new System.Drawing.Size(208, 89);
            this.txtEditCustAddress.TabIndex = 1;
            // 
            // lblCommonErrorMsg
            // 
            this.lblCommonErrorMsg.AutoSize = true;
            this.lblCommonErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.lblCommonErrorMsg.Location = new System.Drawing.Point(113, 451);
            this.lblCommonErrorMsg.Name = "lblCommonErrorMsg";
            this.lblCommonErrorMsg.Size = new System.Drawing.Size(0, 13);
            this.lblCommonErrorMsg.TabIndex = 29;
            // 
            // lblCustActiveAsterik
            // 
            this.lblCustActiveAsterik.AutoSize = true;
            this.lblCustActiveAsterik.ForeColor = System.Drawing.Color.Red;
            this.lblCustActiveAsterik.Location = new System.Drawing.Point(38, 282);
            this.lblCustActiveAsterik.Name = "lblCustActiveAsterik";
            this.lblCustActiveAsterik.Size = new System.Drawing.Size(11, 13);
            this.lblCustActiveAsterik.TabIndex = 30;
            this.lblCustActiveAsterik.Text = "*";
            // 
            // lblEditIsCustActive
            // 
            this.lblEditIsCustActive.AutoSize = true;
            this.lblEditIsCustActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditIsCustActive.Location = new System.Drawing.Point(6, 282);
            this.lblEditIsCustActive.Name = "lblEditIsCustActive";
            this.lblEditIsCustActive.Size = new System.Drawing.Size(37, 13);
            this.lblEditIsCustActive.TabIndex = 26;
            this.lblEditIsCustActive.Text = "Active";
            // 
            // rdbtnEditCustActiveNo
            // 
            this.rdbtnEditCustActiveNo.AutoSize = true;
            this.rdbtnEditCustActiveNo.Location = new System.Drawing.Point(169, 283);
            this.rdbtnEditCustActiveNo.Name = "rdbtnEditCustActiveNo";
            this.rdbtnEditCustActiveNo.Size = new System.Drawing.Size(39, 17);
            this.rdbtnEditCustActiveNo.TabIndex = 8;
            this.rdbtnEditCustActiveNo.Text = "No";
            this.rdbtnEditCustActiveNo.UseVisualStyleBackColor = true;
            // 
            // rdbtnEditCustActiveYes
            // 
            this.rdbtnEditCustActiveYes.AutoSize = true;
            this.rdbtnEditCustActiveYes.Location = new System.Drawing.Point(113, 283);
            this.rdbtnEditCustActiveYes.Name = "rdbtnEditCustActiveYes";
            this.rdbtnEditCustActiveYes.Size = new System.Drawing.Size(43, 17);
            this.rdbtnEditCustActiveYes.TabIndex = 7;
            this.rdbtnEditCustActiveYes.Text = "Yes";
            this.rdbtnEditCustActiveYes.UseVisualStyleBackColor = true;
            // 
            // cmbxEditCustSelectDiscGrp
            // 
            this.cmbxEditCustSelectDiscGrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxEditCustSelectDiscGrp.FormattingEnabled = true;
            this.cmbxEditCustSelectDiscGrp.Location = new System.Drawing.Point(113, 254);
            this.cmbxEditCustSelectDiscGrp.Name = "cmbxEditCustSelectDiscGrp";
            this.cmbxEditCustSelectDiscGrp.Size = new System.Drawing.Size(169, 21);
            this.cmbxEditCustSelectDiscGrp.TabIndex = 6;
            this.cmbxEditCustSelectDiscGrp.Click += new System.EventHandler(this.cmbxEditCustSelectDiscGrp_Click);
            // 
            // lblEditCustDiscGrpName
            // 
            this.lblEditCustDiscGrpName.AutoSize = true;
            this.lblEditCustDiscGrpName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditCustDiscGrpName.Location = new System.Drawing.Point(6, 254);
            this.lblEditCustDiscGrpName.Name = "lblEditCustDiscGrpName";
            this.lblEditCustDiscGrpName.Size = new System.Drawing.Size(81, 13);
            this.lblEditCustDiscGrpName.TabIndex = 25;
            this.lblEditCustDiscGrpName.Text = "Discount Group";
            // 
            // cmbxEditCustSelectLine
            // 
            this.cmbxEditCustSelectLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxEditCustSelectLine.FormattingEnabled = true;
            this.cmbxEditCustSelectLine.Location = new System.Drawing.Point(113, 328);
            this.cmbxEditCustSelectLine.Name = "cmbxEditCustSelectLine";
            this.cmbxEditCustSelectLine.Size = new System.Drawing.Size(169, 21);
            this.cmbxEditCustSelectLine.TabIndex = 9;
            this.cmbxEditCustSelectLine.Click += new System.EventHandler(this.cmbxEditCustSelectLine_Click);
            // 
            // lblEditCustLineID
            // 
            this.lblEditCustLineID.AutoSize = true;
            this.lblEditCustLineID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditCustLineID.Location = new System.Drawing.Point(6, 331);
            this.lblEditCustLineID.Name = "lblEditCustLineID";
            this.lblEditCustLineID.Size = new System.Drawing.Size(27, 13);
            this.lblEditCustLineID.TabIndex = 27;
            this.lblEditCustLineID.Text = "Line";
            // 
            // txtEditCustPhone
            // 
            this.txtEditCustPhone.Location = new System.Drawing.Point(113, 175);
            this.txtEditCustPhone.Name = "txtEditCustPhone";
            this.txtEditCustPhone.Size = new System.Drawing.Size(208, 20);
            this.txtEditCustPhone.TabIndex = 3;
            // 
            // lblEditCustPhoneNo
            // 
            this.lblEditCustPhoneNo.AutoSize = true;
            this.lblEditCustPhoneNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditCustPhoneNo.Location = new System.Drawing.Point(6, 178);
            this.lblEditCustPhoneNo.Name = "lblEditCustPhoneNo";
            this.lblEditCustPhoneNo.Size = new System.Drawing.Size(38, 13);
            this.lblEditCustPhoneNo.TabIndex = 22;
            this.lblEditCustPhoneNo.Text = "Phone";
            // 
            // txtEditGSTIN
            // 
            this.txtEditGSTIN.Location = new System.Drawing.Point(113, 149);
            this.txtEditGSTIN.Name = "txtEditGSTIN";
            this.txtEditGSTIN.Size = new System.Drawing.Size(208, 20);
            this.txtEditGSTIN.TabIndex = 2;
            // 
            // lblEditCustGSTIN
            // 
            this.lblEditCustGSTIN.AutoSize = true;
            this.lblEditCustGSTIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditCustGSTIN.Location = new System.Drawing.Point(6, 148);
            this.lblEditCustGSTIN.Name = "lblEditCustGSTIN";
            this.lblEditCustGSTIN.Size = new System.Drawing.Size(40, 13);
            this.lblEditCustGSTIN.TabIndex = 21;
            this.lblEditCustGSTIN.Text = "GSTIN";
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
            // lblEditCustName
            // 
            this.lblEditCustName.AutoSize = true;
            this.lblEditCustName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditCustName.Location = new System.Drawing.Point(6, 28);
            this.lblEditCustName.Name = "lblEditCustName";
            this.lblEditCustName.Size = new System.Drawing.Size(82, 13);
            this.lblEditCustName.TabIndex = 19;
            this.lblEditCustName.Text = "Customer Name";
            // 
            // txtEditCustomerName
            // 
            this.txtEditCustomerName.Enabled = false;
            this.txtEditCustomerName.Location = new System.Drawing.Point(113, 28);
            this.txtEditCustomerName.Name = "txtEditCustomerName";
            this.txtEditCustomerName.Size = new System.Drawing.Size(208, 20);
            this.txtEditCustomerName.TabIndex = 0;
            // 
            // cmbxEditCustType
            // 
            this.cmbxEditCustType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxEditCustType.FormattingEnabled = true;
            this.cmbxEditCustType.Location = new System.Drawing.Point(113, 301);
            this.cmbxEditCustType.Name = "cmbxEditCustType";
            this.cmbxEditCustType.Size = new System.Drawing.Size(169, 21);
            this.cmbxEditCustType.TabIndex = 32;
            // 
            // lblEditCustomerType
            // 
            this.lblEditCustomerType.AutoSize = true;
            this.lblEditCustomerType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditCustomerType.Location = new System.Drawing.Point(6, 304);
            this.lblEditCustomerType.Name = "lblEditCustomerType";
            this.lblEditCustomerType.Size = new System.Drawing.Size(78, 13);
            this.lblEditCustomerType.TabIndex = 33;
            this.lblEditCustomerType.Text = "Customer Type";
            // 
            // EditCustomerForm
            // 
            this.AcceptButton = this.btnEditCustomer;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 529);
            this.Controls.Add(this.grpEditCustomer);
            this.Name = "EditCustomerForm";
            this.ShowIcon = false;
            this.Text = "Edit Customer Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditCustomerForm_FormClosed);
            this.grpEditCustomer.ResumeLayout(false);
            this.grpEditCustomer.PerformLayout();
            this.flpEditCustOrderDays.ResumeLayout(false);
            this.flpEditCustOrderDays.PerformLayout();
            this.ResumeLayout(false);

        }



        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnEditCustomer;
        private System.Windows.Forms.GroupBox grpEditCustomer;
        private System.Windows.Forms.Label lblCreateCustAddrs;
        private System.Windows.Forms.Label lblEditCustName;
        public System.Windows.Forms.TextBox txtEditCustomerName;
        #endregion

        private System.Windows.Forms.Label lblEditCustGSTIN;
        public System.Windows.Forms.TextBox txtEditGSTIN;
        //private System.Windows.Forms.TextBox txtAddress;
        //private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtEditCustPhone;
        private System.Windows.Forms.Label lblEditCustPhoneNo;
        private System.Windows.Forms.Label lblEditCustLineID;
        public System.Windows.Forms.ComboBox cmbxEditCustSelectLine;
        public System.Windows.Forms.ComboBox cmbxEditCustSelectDiscGrp;
        private System.Windows.Forms.Label lblEditCustDiscGrpName;
        public System.Windows.Forms.RadioButton rdbtnEditCustActiveNo;
        public System.Windows.Forms.RadioButton rdbtnEditCustActiveYes;
        private System.Windows.Forms.Label lblEditIsCustActive;
        private System.Windows.Forms.Label lblCustActiveAsterik;
        private System.Windows.Forms.Label lblCommonErrorMsg;
        public System.Windows.Forms.ComboBox cmbxEditCustSelectPriceGrp;
        private System.Windows.Forms.Label lblEditCustPriceGrp;
        public System.Windows.Forms.TextBox txtEditCustAddress;
        private System.Windows.Forms.Label lblEditCustStateCode;
        private System.Windows.Forms.Label lblEditCustOrderDays;
        public System.Windows.Forms.CheckBox chbxSunday;
        public System.Windows.Forms.CheckBox chbxMonday;
        public System.Windows.Forms.CheckBox chbxTuesday;
        public System.Windows.Forms.CheckBox chbxWednesday;
        public System.Windows.Forms.CheckBox chbxThursday;
        public System.Windows.Forms.CheckBox chbxFriday;
        public System.Windows.Forms.CheckBox chbxSaturday;
        public System.Windows.Forms.ComboBox cmbxEditCustSelectState;
        public System.Windows.Forms.FlowLayoutPanel flpEditCustOrderDays;
        public System.Windows.Forms.ComboBox cmbxEditCustType;
        private System.Windows.Forms.Label lblEditCustomerType;
    }
}