namespace SalesOrdersReport
{
    partial class CreatePriceGroupForm
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
            this.txtNewPriceGrpName = new System.Windows.Forms.TextBox();
            this.lblNewPriceGrpName = new System.Windows.Forms.Label();
            this.txtNwPriceGrpDesc = new System.Windows.Forms.TextBox();
            this.lblNwPriceGrpDesc = new System.Windows.Forms.Label();
            this.btnCreatePriceGrp = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblCreatePriceGrpNameAsterik = new System.Windows.Forms.Label();
            this.lblPriceGrpDescAsterik = new System.Windows.Forms.Label();
            this.lblPriceGrpCol = new System.Windows.Forms.Label();
            this.lblPriceGrpDiscount = new System.Windows.Forms.Label();
            this.txtPriceGrpDiscVal = new System.Windows.Forms.TextBox();
            this.lblPriceGrpDefault = new System.Windows.Forms.Label();
            this.radioBtnDefaultYes = new System.Windows.Forms.RadioButton();
            this.radioBtnDefaultNo = new System.Windows.Forms.RadioButton();
            this.lblPGDicountType = new System.Windows.Forms.Label();
            this.radioBtnDisTypePercent = new System.Windows.Forms.RadioButton();
            this.radioBtnDisTypeAbs = new System.Windows.Forms.RadioButton();
            this.grpbxCreatePriceGrp = new System.Windows.Forms.GroupBox();
            this.lblValidatingErrMsg = new System.Windows.Forms.Label();
            this.cmbxPriceGrpCol = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpbxCreatePriceGrp.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNewPriceGrpName
            // 
            this.txtNewPriceGrpName.Location = new System.Drawing.Point(181, 36);
            this.txtNewPriceGrpName.Name = "txtNewPriceGrpName";
            this.txtNewPriceGrpName.Size = new System.Drawing.Size(208, 20);
            this.txtNewPriceGrpName.TabIndex = 16;
            // 
            // lblNewPriceGrpName
            // 
            this.lblNewPriceGrpName.AutoSize = true;
            this.lblNewPriceGrpName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewPriceGrpName.Location = new System.Drawing.Point(56, 33);
            this.lblNewPriceGrpName.Name = "lblNewPriceGrpName";
            this.lblNewPriceGrpName.Size = new System.Drawing.Size(94, 13);
            this.lblNewPriceGrpName.TabIndex = 13;
            this.lblNewPriceGrpName.Text = "Price Group Name";
            // 
            // txtNwPriceGrpDesc
            // 
            this.txtNwPriceGrpDesc.Location = new System.Drawing.Point(181, 77);
            this.txtNwPriceGrpDesc.Multiline = true;
            this.txtNwPriceGrpDesc.Name = "txtNwPriceGrpDesc";
            this.txtNwPriceGrpDesc.Size = new System.Drawing.Size(208, 85);
            this.txtNwPriceGrpDesc.TabIndex = 15;
            // 
            // lblNwPriceGrpDesc
            // 
            this.lblNwPriceGrpDesc.AutoSize = true;
            this.lblNwPriceGrpDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNwPriceGrpDesc.Location = new System.Drawing.Point(56, 77);
            this.lblNwPriceGrpDesc.Name = "lblNwPriceGrpDesc";
            this.lblNwPriceGrpDesc.Size = new System.Drawing.Size(60, 13);
            this.lblNwPriceGrpDesc.TabIndex = 17;
            this.lblNwPriceGrpDesc.Text = "Description";
            // 
            // btnCreatePriceGrp
            // 
            this.btnCreatePriceGrp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreatePriceGrp.Location = new System.Drawing.Point(181, 307);
            this.btnCreatePriceGrp.Name = "btnCreatePriceGrp";
            this.btnCreatePriceGrp.Size = new System.Drawing.Size(117, 39);
            this.btnCreatePriceGrp.TabIndex = 18;
            this.btnCreatePriceGrp.Text = "Create Price Group";
            this.btnCreatePriceGrp.UseVisualStyleBackColor = true;
            this.btnCreatePriceGrp.Click += new System.EventHandler(this.btnCreatePriceGrp_Click);
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(304, 307);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(97, 39);
            this.btnReset.TabIndex = 19;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblCreatePriceGrpNameAsterik
            // 
            this.lblCreatePriceGrpNameAsterik.AutoSize = true;
            this.lblCreatePriceGrpNameAsterik.ForeColor = System.Drawing.Color.Red;
            this.lblCreatePriceGrpNameAsterik.Location = new System.Drawing.Point(146, 33);
            this.lblCreatePriceGrpNameAsterik.Name = "lblCreatePriceGrpNameAsterik";
            this.lblCreatePriceGrpNameAsterik.Size = new System.Drawing.Size(11, 13);
            this.lblCreatePriceGrpNameAsterik.TabIndex = 25;
            this.lblCreatePriceGrpNameAsterik.Text = "*";
            // 
            // lblPriceGrpDescAsterik
            // 
            this.lblPriceGrpDescAsterik.AutoSize = true;
            this.lblPriceGrpDescAsterik.ForeColor = System.Drawing.Color.Red;
            this.lblPriceGrpDescAsterik.Location = new System.Drawing.Point(111, 77);
            this.lblPriceGrpDescAsterik.Name = "lblPriceGrpDescAsterik";
            this.lblPriceGrpDescAsterik.Size = new System.Drawing.Size(11, 13);
            this.lblPriceGrpDescAsterik.TabIndex = 26;
            this.lblPriceGrpDescAsterik.Text = "*";
            // 
            // lblPriceGrpCol
            // 
            this.lblPriceGrpCol.AutoSize = true;
            this.lblPriceGrpCol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriceGrpCol.Location = new System.Drawing.Point(56, 178);
            this.lblPriceGrpCol.Name = "lblPriceGrpCol";
            this.lblPriceGrpCol.Size = new System.Drawing.Size(101, 13);
            this.lblPriceGrpCol.TabIndex = 30;
            this.lblPriceGrpCol.Text = "Price Group Column";
            // 
            // lblPriceGrpDiscount
            // 
            this.lblPriceGrpDiscount.AutoSize = true;
            this.lblPriceGrpDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriceGrpDiscount.Location = new System.Drawing.Point(56, 213);
            this.lblPriceGrpDiscount.Name = "lblPriceGrpDiscount";
            this.lblPriceGrpDiscount.Size = new System.Drawing.Size(49, 13);
            this.lblPriceGrpDiscount.TabIndex = 31;
            this.lblPriceGrpDiscount.Text = "Discount";
            // 
            // txtPriceGrpDiscVal
            // 
            this.txtPriceGrpDiscVal.Location = new System.Drawing.Point(181, 213);
            this.txtPriceGrpDiscVal.Name = "txtPriceGrpDiscVal";
            this.txtPriceGrpDiscVal.Size = new System.Drawing.Size(48, 20);
            this.txtPriceGrpDiscVal.TabIndex = 33;
            // 
            // lblPriceGrpDefault
            // 
            this.lblPriceGrpDefault.AutoSize = true;
            this.lblPriceGrpDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriceGrpDefault.Location = new System.Drawing.Point(235, 213);
            this.lblPriceGrpDefault.Name = "lblPriceGrpDefault";
            this.lblPriceGrpDefault.Size = new System.Drawing.Size(44, 13);
            this.lblPriceGrpDefault.TabIndex = 34;
            this.lblPriceGrpDefault.Text = "Default:";
            // 
            // radioBtnDefaultYes
            // 
            this.radioBtnDefaultYes.AutoSize = true;
            this.radioBtnDefaultYes.Location = new System.Drawing.Point(3, 3);
            this.radioBtnDefaultYes.Name = "radioBtnDefaultYes";
            this.radioBtnDefaultYes.Size = new System.Drawing.Size(43, 17);
            this.radioBtnDefaultYes.TabIndex = 35;
            this.radioBtnDefaultYes.TabStop = true;
            this.radioBtnDefaultYes.Text = "Yes";
            this.radioBtnDefaultYes.UseVisualStyleBackColor = true;
            // 
            // radioBtnDefaultNo
            // 
            this.radioBtnDefaultNo.AutoSize = true;
            this.radioBtnDefaultNo.Location = new System.Drawing.Point(58, 3);
            this.radioBtnDefaultNo.Name = "radioBtnDefaultNo";
            this.radioBtnDefaultNo.Size = new System.Drawing.Size(39, 17);
            this.radioBtnDefaultNo.TabIndex = 36;
            this.radioBtnDefaultNo.TabStop = true;
            this.radioBtnDefaultNo.Text = "No";
            this.radioBtnDefaultNo.UseVisualStyleBackColor = true;
            // 
            // lblPGDicountType
            // 
            this.lblPGDicountType.AutoSize = true;
            this.lblPGDicountType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPGDicountType.Location = new System.Drawing.Point(56, 255);
            this.lblPGDicountType.Name = "lblPGDicountType";
            this.lblPGDicountType.Size = new System.Drawing.Size(76, 13);
            this.lblPGDicountType.TabIndex = 37;
            this.lblPGDicountType.Text = "Discount Type";
            // 
            // radioBtnDisTypePercent
            // 
            this.radioBtnDisTypePercent.AutoSize = true;
            this.radioBtnDisTypePercent.Location = new System.Drawing.Point(181, 255);
            this.radioBtnDisTypePercent.Name = "radioBtnDisTypePercent";
            this.radioBtnDisTypePercent.Size = new System.Drawing.Size(62, 17);
            this.radioBtnDisTypePercent.TabIndex = 38;
            this.radioBtnDisTypePercent.TabStop = true;
            this.radioBtnDisTypePercent.Text = "Percent";
            this.radioBtnDisTypePercent.UseVisualStyleBackColor = true;
            // 
            // radioBtnDisTypeAbs
            // 
            this.radioBtnDisTypeAbs.AutoSize = true;
            this.radioBtnDisTypeAbs.Location = new System.Drawing.Point(249, 255);
            this.radioBtnDisTypeAbs.Name = "radioBtnDisTypeAbs";
            this.radioBtnDisTypeAbs.Size = new System.Drawing.Size(66, 17);
            this.radioBtnDisTypeAbs.TabIndex = 39;
            this.radioBtnDisTypeAbs.TabStop = true;
            this.radioBtnDisTypeAbs.Text = "Absolute";
            this.radioBtnDisTypeAbs.UseVisualStyleBackColor = true;
            // 
            // grpbxCreatePriceGrp
            // 
            this.grpbxCreatePriceGrp.Controls.Add(this.panel1);
            this.grpbxCreatePriceGrp.Controls.Add(this.lblValidatingErrMsg);
            this.grpbxCreatePriceGrp.Controls.Add(this.cmbxPriceGrpCol);
            this.grpbxCreatePriceGrp.Controls.Add(this.radioBtnDisTypeAbs);
            this.grpbxCreatePriceGrp.Controls.Add(this.radioBtnDisTypePercent);
            this.grpbxCreatePriceGrp.Controls.Add(this.lblPGDicountType);
            this.grpbxCreatePriceGrp.Controls.Add(this.lblPriceGrpDefault);
            this.grpbxCreatePriceGrp.Controls.Add(this.txtPriceGrpDiscVal);
            this.grpbxCreatePriceGrp.Controls.Add(this.lblPriceGrpDiscount);
            this.grpbxCreatePriceGrp.Controls.Add(this.lblPriceGrpCol);
            this.grpbxCreatePriceGrp.Controls.Add(this.lblPriceGrpDescAsterik);
            this.grpbxCreatePriceGrp.Controls.Add(this.lblCreatePriceGrpNameAsterik);
            this.grpbxCreatePriceGrp.Controls.Add(this.btnReset);
            this.grpbxCreatePriceGrp.Controls.Add(this.btnCreatePriceGrp);
            this.grpbxCreatePriceGrp.Controls.Add(this.lblNwPriceGrpDesc);
            this.grpbxCreatePriceGrp.Controls.Add(this.txtNwPriceGrpDesc);
            this.grpbxCreatePriceGrp.Controls.Add(this.lblNewPriceGrpName);
            this.grpbxCreatePriceGrp.Controls.Add(this.txtNewPriceGrpName);
            this.grpbxCreatePriceGrp.Location = new System.Drawing.Point(22, 21);
            this.grpbxCreatePriceGrp.Name = "grpbxCreatePriceGrp";
            this.grpbxCreatePriceGrp.Size = new System.Drawing.Size(449, 363);
            this.grpbxCreatePriceGrp.TabIndex = 0;
            this.grpbxCreatePriceGrp.TabStop = false;
            this.grpbxCreatePriceGrp.Text = "New Price Group";
            // 
            // lblValidatingErrMsg
            // 
            this.lblValidatingErrMsg.AutoSize = true;
            this.lblValidatingErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lblValidatingErrMsg.Location = new System.Drawing.Point(178, 291);
            this.lblValidatingErrMsg.Name = "lblValidatingErrMsg";
            this.lblValidatingErrMsg.Size = new System.Drawing.Size(0, 13);
            this.lblValidatingErrMsg.TabIndex = 41;
            this.lblValidatingErrMsg.Visible = false;
            // 
            // cmbxPriceGrpCol
            // 
            this.cmbxPriceGrpCol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxPriceGrpCol.FormattingEnabled = true;
            this.cmbxPriceGrpCol.Items.AddRange(new object[] {
            "Purchase Price",
            "Wholesale Price",
            "Retail Price",
            "Max Retail Price"});
            this.cmbxPriceGrpCol.Location = new System.Drawing.Point(181, 175);
            this.cmbxPriceGrpCol.Name = "cmbxPriceGrpCol";
            this.cmbxPriceGrpCol.Size = new System.Drawing.Size(178, 21);
            this.cmbxPriceGrpCol.TabIndex = 40;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioBtnDefaultYes);
            this.panel1.Controls.Add(this.radioBtnDefaultNo);
            this.panel1.Location = new System.Drawing.Point(285, 208);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(104, 25);
            this.panel1.TabIndex = 42;
            // 
            // CreatePriceGroupForm
            // 
            this.AcceptButton = this.btnCreatePriceGrp;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 407);
            this.Controls.Add(this.grpbxCreatePriceGrp);
            this.Name = "CreatePriceGroupForm";
            this.ShowIcon = false;
            this.Text = "Create Price Group Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CreatePriceGrpForm_FormClosed);
            this.grpbxCreatePriceGrp.ResumeLayout(false);
            this.grpbxCreatePriceGrp.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtNewPriceGrpName;
        private System.Windows.Forms.Label lblNewPriceGrpName;
        private System.Windows.Forms.TextBox txtNwPriceGrpDesc;
        private System.Windows.Forms.Label lblNwPriceGrpDesc;
        private System.Windows.Forms.Button btnCreatePriceGrp;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblCreatePriceGrpNameAsterik;
        private System.Windows.Forms.Label lblPriceGrpDescAsterik;
        private System.Windows.Forms.Label lblPriceGrpCol;
        private System.Windows.Forms.Label lblPriceGrpDiscount;
        private System.Windows.Forms.TextBox txtPriceGrpDiscVal;
        private System.Windows.Forms.Label lblPriceGrpDefault;
        private System.Windows.Forms.RadioButton radioBtnDefaultYes;
        private System.Windows.Forms.RadioButton radioBtnDefaultNo;
        private System.Windows.Forms.Label lblPGDicountType;
        private System.Windows.Forms.RadioButton radioBtnDisTypePercent;
        private System.Windows.Forms.RadioButton radioBtnDisTypeAbs;
        private System.Windows.Forms.GroupBox grpbxCreatePriceGrp;
        private System.Windows.Forms.ComboBox cmbxPriceGrpCol;
        private System.Windows.Forms.Label lblValidatingErrMsg;
        private System.Windows.Forms.Panel panel1;
    }
}