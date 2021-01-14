using System;

namespace SalesOrdersReport
{
    partial class CreateCustomerForm
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
            this.btnCreateCustomer = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flpCreateCustOrderDays = new System.Windows.Forms.FlowLayoutPanel();
            this.chbxMonday = new System.Windows.Forms.CheckBox();
            this.chbxThursday = new System.Windows.Forms.CheckBox();
            this.chbxTuesday = new System.Windows.Forms.CheckBox();
            this.chbxFriday = new System.Windows.Forms.CheckBox();
            this.chbxWednesday = new System.Windows.Forms.CheckBox();
            this.chbxSaturday = new System.Windows.Forms.CheckBox();
            this.chbxSunday = new System.Windows.Forms.CheckBox();
            this.cmbxCreateCustSelectState = new System.Windows.Forms.ComboBox();
            this.lblCreateCustOrderDays = new System.Windows.Forms.Label();
            this.lblCreateCustStateCode = new System.Windows.Forms.Label();
            this.cmbxCreateCustSelectPriceGrp = new System.Windows.Forms.ComboBox();
            this.lblCreateCustPriceGrp = new System.Windows.Forms.Label();
            this.txtCustAddress = new System.Windows.Forms.TextBox();
            this.lblCommonErrorMsg = new System.Windows.Forms.Label();
            this.lblCustActiveAsterik = new System.Windows.Forms.Label();
            this.lblCreateCustomerAsterik = new System.Windows.Forms.Label();
            this.lblIsCustActive = new System.Windows.Forms.Label();
            this.rdbtnCustActiveNo = new System.Windows.Forms.RadioButton();
            this.rdbtnCustActiveYes = new System.Windows.Forms.RadioButton();
            this.cmbxCreateCustSelectDiscGrp = new System.Windows.Forms.ComboBox();
            this.lblCreateCustDiscGrpName = new System.Windows.Forms.Label();
            this.cmbxCreateCustSelectLine = new System.Windows.Forms.ComboBox();
            this.lblCreateCustLineID = new System.Windows.Forms.Label();
            this.txtCreateCustPhone = new System.Windows.Forms.TextBox();
            this.lblCreateCustPhoneNo = new System.Windows.Forms.Label();
            this.txtGSTIN = new System.Windows.Forms.TextBox();
            this.lblCreateCustGSTIN = new System.Windows.Forms.Label();
            this.lblCreateCustAddrs = new System.Windows.Forms.Label();
            this.lblCreateCustName = new System.Windows.Forms.Label();
            this.txtCreateCustomerName = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.flpCreateCustOrderDays.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(232, 445);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(97, 39);
            this.btnReset.TabIndex = 18;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnCreateCutomerReset_Click);
            // 
            // btnCreateCustomer
            // 
            this.btnCreateCustomer.Location = new System.Drawing.Point(113, 445);
            this.btnCreateCustomer.Name = "btnCreateCustomer";
            this.btnCreateCustomer.Size = new System.Drawing.Size(113, 39);
            this.btnCreateCustomer.TabIndex = 17;
            this.btnCreateCustomer.Text = "Create Customer";
            this.btnCreateCustomer.UseVisualStyleBackColor = true;
            this.btnCreateCustomer.Click += new System.EventHandler(this.btnCreateCustomer_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flpCreateCustOrderDays);
            this.groupBox1.Controls.Add(this.cmbxCreateCustSelectState);
            this.groupBox1.Controls.Add(this.lblCreateCustOrderDays);
            this.groupBox1.Controls.Add(this.lblCreateCustStateCode);
            this.groupBox1.Controls.Add(this.cmbxCreateCustSelectPriceGrp);
            this.groupBox1.Controls.Add(this.lblCreateCustPriceGrp);
            this.groupBox1.Controls.Add(this.txtCustAddress);
            this.groupBox1.Controls.Add(this.lblCommonErrorMsg);
            this.groupBox1.Controls.Add(this.lblCustActiveAsterik);
            this.groupBox1.Controls.Add(this.lblCreateCustomerAsterik);
            this.groupBox1.Controls.Add(this.lblIsCustActive);
            this.groupBox1.Controls.Add(this.rdbtnCustActiveNo);
            this.groupBox1.Controls.Add(this.rdbtnCustActiveYes);
            this.groupBox1.Controls.Add(this.cmbxCreateCustSelectDiscGrp);
            this.groupBox1.Controls.Add(this.lblCreateCustDiscGrpName);
            this.groupBox1.Controls.Add(this.cmbxCreateCustSelectLine);
            this.groupBox1.Controls.Add(this.lblCreateCustLineID);
            this.groupBox1.Controls.Add(this.txtCreateCustPhone);
            this.groupBox1.Controls.Add(this.lblCreateCustPhoneNo);
            this.groupBox1.Controls.Add(this.txtGSTIN);
            this.groupBox1.Controls.Add(this.lblCreateCustGSTIN);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.lblCreateCustAddrs);
            this.groupBox1.Controls.Add(this.btnCreateCustomer);
            this.groupBox1.Controls.Add(this.lblCreateCustName);
            this.groupBox1.Controls.Add(this.txtCreateCustomerName);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 494);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create Customer";
            // 
            // flpCreateCustOrderDays
            // 
            this.flpCreateCustOrderDays.Controls.Add(this.chbxMonday);
            this.flpCreateCustOrderDays.Controls.Add(this.chbxThursday);
            this.flpCreateCustOrderDays.Controls.Add(this.chbxTuesday);
            this.flpCreateCustOrderDays.Controls.Add(this.chbxFriday);
            this.flpCreateCustOrderDays.Controls.Add(this.chbxWednesday);
            this.flpCreateCustOrderDays.Controls.Add(this.chbxSaturday);
            this.flpCreateCustOrderDays.Controls.Add(this.chbxSunday);
            this.flpCreateCustOrderDays.Location = new System.Drawing.Point(113, 336);
            this.flpCreateCustOrderDays.Name = "flpCreateCustOrderDays";
            this.flpCreateCustOrderDays.Size = new System.Drawing.Size(208, 90);
            this.flpCreateCustOrderDays.TabIndex = 32;
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
            // cmbxCreateCustSelectState
            // 
            this.cmbxCreateCustSelectState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxCreateCustSelectState.FormattingEnabled = true;
            this.cmbxCreateCustSelectState.Location = new System.Drawing.Point(113, 228);
            this.cmbxCreateCustSelectState.Name = "cmbxCreateCustSelectState";
            this.cmbxCreateCustSelectState.Size = new System.Drawing.Size(171, 21);
            this.cmbxCreateCustSelectState.TabIndex = 5;
            // 
            // lblCreateCustOrderDays
            // 
            this.lblCreateCustOrderDays.AutoSize = true;
            this.lblCreateCustOrderDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateCustOrderDays.Location = new System.Drawing.Point(6, 336);
            this.lblCreateCustOrderDays.Name = "lblCreateCustOrderDays";
            this.lblCreateCustOrderDays.Size = new System.Drawing.Size(62, 13);
            this.lblCreateCustOrderDays.TabIndex = 28;
            this.lblCreateCustOrderDays.Text = "OrdersDays";
            // 
            // lblCreateCustStateCode
            // 
            this.lblCreateCustStateCode.AutoSize = true;
            this.lblCreateCustStateCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateCustStateCode.Location = new System.Drawing.Point(6, 227);
            this.lblCreateCustStateCode.Name = "lblCreateCustStateCode";
            this.lblCreateCustStateCode.Size = new System.Drawing.Size(32, 13);
            this.lblCreateCustStateCode.TabIndex = 24;
            this.lblCreateCustStateCode.Text = "State";
            // 
            // cmbxCreateCustSelectPriceGrp
            // 
            this.cmbxCreateCustSelectPriceGrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxCreateCustSelectPriceGrp.FormattingEnabled = true;
            this.cmbxCreateCustSelectPriceGrp.Location = new System.Drawing.Point(113, 201);
            this.cmbxCreateCustSelectPriceGrp.Name = "cmbxCreateCustSelectPriceGrp";
            this.cmbxCreateCustSelectPriceGrp.Size = new System.Drawing.Size(171, 21);
            this.cmbxCreateCustSelectPriceGrp.TabIndex = 4;
            // 
            // lblCreateCustPriceGrp
            // 
            this.lblCreateCustPriceGrp.AutoSize = true;
            this.lblCreateCustPriceGrp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateCustPriceGrp.Location = new System.Drawing.Point(6, 201);
            this.lblCreateCustPriceGrp.Name = "lblCreateCustPriceGrp";
            this.lblCreateCustPriceGrp.Size = new System.Drawing.Size(63, 13);
            this.lblCreateCustPriceGrp.TabIndex = 23;
            this.lblCreateCustPriceGrp.Text = "Price Group";
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
            this.lblCommonErrorMsg.Location = new System.Drawing.Point(113, 429);
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
            // lblCreateCustomerAsterik
            // 
            this.lblCreateCustomerAsterik.AutoSize = true;
            this.lblCreateCustomerAsterik.ForeColor = System.Drawing.Color.Red;
            this.lblCreateCustomerAsterik.Location = new System.Drawing.Point(83, 28);
            this.lblCreateCustomerAsterik.Name = "lblCreateCustomerAsterik";
            this.lblCreateCustomerAsterik.Size = new System.Drawing.Size(11, 13);
            this.lblCreateCustomerAsterik.TabIndex = 29;
            this.lblCreateCustomerAsterik.Text = "*";
            // 
            // lblIsCustActive
            // 
            this.lblIsCustActive.AutoSize = true;
            this.lblIsCustActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIsCustActive.Location = new System.Drawing.Point(6, 282);
            this.lblIsCustActive.Name = "lblIsCustActive";
            this.lblIsCustActive.Size = new System.Drawing.Size(37, 13);
            this.lblIsCustActive.TabIndex = 26;
            this.lblIsCustActive.Text = "Active";
            // 
            // rdbtnCustActiveNo
            // 
            this.rdbtnCustActiveNo.AutoSize = true;
            this.rdbtnCustActiveNo.Location = new System.Drawing.Point(169, 283);
            this.rdbtnCustActiveNo.Name = "rdbtnCustActiveNo";
            this.rdbtnCustActiveNo.Size = new System.Drawing.Size(39, 17);
            this.rdbtnCustActiveNo.TabIndex = 8;
            this.rdbtnCustActiveNo.Text = "No";
            this.rdbtnCustActiveNo.UseVisualStyleBackColor = true;
            // 
            // rdbtnCustActiveYes
            // 
            this.rdbtnCustActiveYes.AutoSize = true;
            this.rdbtnCustActiveYes.Location = new System.Drawing.Point(113, 283);
            this.rdbtnCustActiveYes.Name = "rdbtnCustActiveYes";
            this.rdbtnCustActiveYes.Size = new System.Drawing.Size(43, 17);
            this.rdbtnCustActiveYes.TabIndex = 7;
            this.rdbtnCustActiveYes.Text = "Yes";
            this.rdbtnCustActiveYes.UseVisualStyleBackColor = true;
            // 
            // cmbxCreateCustSelectDiscGrp
            // 
            this.cmbxCreateCustSelectDiscGrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxCreateCustSelectDiscGrp.FormattingEnabled = true;
            this.cmbxCreateCustSelectDiscGrp.Location = new System.Drawing.Point(113, 254);
            this.cmbxCreateCustSelectDiscGrp.Name = "cmbxCreateCustSelectDiscGrp";
            this.cmbxCreateCustSelectDiscGrp.Size = new System.Drawing.Size(171, 21);
            this.cmbxCreateCustSelectDiscGrp.TabIndex = 6;
            this.cmbxCreateCustSelectDiscGrp.Click += new System.EventHandler(this.cmbxSelectStore_Click);
            // 
            // lblCreateCustDiscGrpName
            // 
            this.lblCreateCustDiscGrpName.AutoSize = true;
            this.lblCreateCustDiscGrpName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateCustDiscGrpName.Location = new System.Drawing.Point(6, 254);
            this.lblCreateCustDiscGrpName.Name = "lblCreateCustDiscGrpName";
            this.lblCreateCustDiscGrpName.Size = new System.Drawing.Size(81, 13);
            this.lblCreateCustDiscGrpName.TabIndex = 25;
            this.lblCreateCustDiscGrpName.Text = "Discount Group";
            // 
            // cmbxCreateCustSelectLine
            // 
            this.cmbxCreateCustSelectLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxCreateCustSelectLine.FormattingEnabled = true;
            this.cmbxCreateCustSelectLine.Location = new System.Drawing.Point(113, 306);
            this.cmbxCreateCustSelectLine.Name = "cmbxCreateCustSelectLine";
            this.cmbxCreateCustSelectLine.Size = new System.Drawing.Size(171, 21);
            this.cmbxCreateCustSelectLine.TabIndex = 9;
            this.cmbxCreateCustSelectLine.Click += new System.EventHandler(this.cmbxSelectRoleID_Click);
            // 
            // lblCreateCustLineID
            // 
            this.lblCreateCustLineID.AutoSize = true;
            this.lblCreateCustLineID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateCustLineID.Location = new System.Drawing.Point(6, 309);
            this.lblCreateCustLineID.Name = "lblCreateCustLineID";
            this.lblCreateCustLineID.Size = new System.Drawing.Size(27, 13);
            this.lblCreateCustLineID.TabIndex = 27;
            this.lblCreateCustLineID.Text = "Line";
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
            this.lblCreateCustName.Size = new System.Drawing.Size(82, 13);
            this.lblCreateCustName.TabIndex = 19;
            this.lblCreateCustName.Text = "Customer Name";
            // 
            // txtCreateCustomerName
            // 
            this.txtCreateCustomerName.Location = new System.Drawing.Point(113, 28);
            this.txtCreateCustomerName.Name = "txtCreateCustomerName";
            this.txtCreateCustomerName.Size = new System.Drawing.Size(208, 20);
            this.txtCreateCustomerName.TabIndex = 0;
            // 
            // CreateCustomerForm
            // 
            this.AcceptButton = this.btnCreateCustomer;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 508);
            this.Controls.Add(this.groupBox1);
            this.Name = "CreateCustomerForm";
            this.ShowIcon = false;
            this.Text = "Create Customer Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CreateCustomerForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.flpCreateCustOrderDays.ResumeLayout(false);
            this.flpCreateCustOrderDays.PerformLayout();
            this.ResumeLayout(false);

        }



        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCreateCustomer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCreateCustAddrs;
        private System.Windows.Forms.Label lblCreateCustName;
        private System.Windows.Forms.TextBox txtCreateCustomerName;
        #endregion

        private System.Windows.Forms.Label lblCreateCustGSTIN;
        private System.Windows.Forms.TextBox txtGSTIN;
        //private System.Windows.Forms.TextBox txtAddress;
        //private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCreateCustPhone;
        private System.Windows.Forms.Label lblCreateCustPhoneNo;
        private System.Windows.Forms.Label lblCreateCustLineID;
        private System.Windows.Forms.ComboBox cmbxCreateCustSelectLine;
        private System.Windows.Forms.ComboBox cmbxCreateCustSelectDiscGrp;
        private System.Windows.Forms.Label lblCreateCustDiscGrpName;
        private System.Windows.Forms.RadioButton rdbtnCustActiveNo;
        private System.Windows.Forms.RadioButton rdbtnCustActiveYes;
        private System.Windows.Forms.Label lblIsCustActive;
        private System.Windows.Forms.Label lblCreateCustomerAsterik;
        private System.Windows.Forms.Label lblCustActiveAsterik;
        private System.Windows.Forms.Label lblCommonErrorMsg;
        private System.Windows.Forms.ComboBox cmbxCreateCustSelectPriceGrp;
        private System.Windows.Forms.Label lblCreateCustPriceGrp;
        private System.Windows.Forms.TextBox txtCustAddress;
        private System.Windows.Forms.Label lblCreateCustStateCode;
        private System.Windows.Forms.Label lblCreateCustOrderDays;
        private System.Windows.Forms.ComboBox cmbxCreateCustSelectState;
        private System.Windows.Forms.FlowLayoutPanel flpCreateCustOrderDays;
        public System.Windows.Forms.CheckBox chbxMonday;
        public System.Windows.Forms.CheckBox chbxThursday;
        public System.Windows.Forms.CheckBox chbxTuesday;
        public System.Windows.Forms.CheckBox chbxFriday;
        public System.Windows.Forms.CheckBox chbxWednesday;
        public System.Windows.Forms.CheckBox chbxSaturday;
        public System.Windows.Forms.CheckBox chbxSunday;
    }
}