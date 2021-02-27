namespace SalesOrdersReport
{
    partial class EditDiscountGroupForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbxSelectDisGroupName = new System.Windows.Forms.ComboBox();
            this.lblValidErrMsg = new System.Windows.Forms.Label();
            this.radioBtnEditDGDisTypeAbs = new System.Windows.Forms.RadioButton();
            this.radioBtnEditDGDisTypePercent = new System.Windows.Forms.RadioButton();
            this.lblEditDGDisType = new System.Windows.Forms.Label();
            this.radioBtnEditDGDefaultNo = new System.Windows.Forms.RadioButton();
            this.radioBtnEditDGDefaultYes = new System.Windows.Forms.RadioButton();
            this.lblEditDGDefault = new System.Windows.Forms.Label();
            this.txtEditDiscountVal = new System.Windows.Forms.TextBox();
            this.lblEditDGDiscount = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnEditDisGrp = new System.Windows.Forms.Button();
            this.lblEditDGDesc = new System.Windows.Forms.Label();
            this.txtEditDisGrpDesc = new System.Windows.Forms.TextBox();
            this.lblEditDGName = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.cmbxSelectDisGroupName);
            this.groupBox1.Controls.Add(this.lblValidErrMsg);
            this.groupBox1.Controls.Add(this.radioBtnEditDGDisTypeAbs);
            this.groupBox1.Controls.Add(this.radioBtnEditDGDisTypePercent);
            this.groupBox1.Controls.Add(this.lblEditDGDisType);
            this.groupBox1.Controls.Add(this.lblEditDGDefault);
            this.groupBox1.Controls.Add(this.txtEditDiscountVal);
            this.groupBox1.Controls.Add(this.lblEditDGDiscount);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnEditDisGrp);
            this.groupBox1.Controls.Add(this.lblEditDGDesc);
            this.groupBox1.Controls.Add(this.txtEditDisGrpDesc);
            this.groupBox1.Controls.Add(this.lblEditDGName);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(389, 273);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Edit Discount Group";
            // 
            // cmbxSelectDisGroupName
            // 
            this.cmbxSelectDisGroupName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxSelectDisGroupName.FormattingEnabled = true;
            this.cmbxSelectDisGroupName.Location = new System.Drawing.Point(150, 32);
            this.cmbxSelectDisGroupName.Name = "cmbxSelectDisGroupName";
            this.cmbxSelectDisGroupName.Size = new System.Drawing.Size(208, 21);
            this.cmbxSelectDisGroupName.TabIndex = 0;
            this.cmbxSelectDisGroupName.SelectedIndexChanged += new System.EventHandler(this.cmbxSelectDisGroupName_SelectedIndexChanged);
            // 
            // lblValidErrMsg
            // 
            this.lblValidErrMsg.AutoSize = true;
            this.lblValidErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lblValidErrMsg.Location = new System.Drawing.Point(147, 199);
            this.lblValidErrMsg.Name = "lblValidErrMsg";
            this.lblValidErrMsg.Size = new System.Drawing.Size(0, 13);
            this.lblValidErrMsg.TabIndex = 40;
            this.lblValidErrMsg.Visible = false;
            // 
            // radioBtnEditDGDisTypeAbs
            // 
            this.radioBtnEditDGDisTypeAbs.AutoSize = true;
            this.radioBtnEditDGDisTypeAbs.Location = new System.Drawing.Point(218, 176);
            this.radioBtnEditDGDisTypeAbs.Name = "radioBtnEditDGDisTypeAbs";
            this.radioBtnEditDGDisTypeAbs.Size = new System.Drawing.Size(66, 17);
            this.radioBtnEditDGDisTypeAbs.TabIndex = 6;
            this.radioBtnEditDGDisTypeAbs.TabStop = true;
            this.radioBtnEditDGDisTypeAbs.Text = "Absolute";
            this.radioBtnEditDGDisTypeAbs.UseVisualStyleBackColor = true;
            // 
            // radioBtnEditDGDisTypePercent
            // 
            this.radioBtnEditDGDisTypePercent.AutoSize = true;
            this.radioBtnEditDGDisTypePercent.Location = new System.Drawing.Point(150, 176);
            this.radioBtnEditDGDisTypePercent.Name = "radioBtnEditDGDisTypePercent";
            this.radioBtnEditDGDisTypePercent.Size = new System.Drawing.Size(62, 17);
            this.radioBtnEditDGDisTypePercent.TabIndex = 5;
            this.radioBtnEditDGDisTypePercent.TabStop = true;
            this.radioBtnEditDGDisTypePercent.Text = "Percent";
            this.radioBtnEditDGDisTypePercent.UseVisualStyleBackColor = true;
            // 
            // lblEditDGDisType
            // 
            this.lblEditDGDisType.AutoSize = true;
            this.lblEditDGDisType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditDGDisType.Location = new System.Drawing.Point(25, 176);
            this.lblEditDGDisType.Name = "lblEditDGDisType";
            this.lblEditDGDisType.Size = new System.Drawing.Size(76, 13);
            this.lblEditDGDisType.TabIndex = 37;
            this.lblEditDGDisType.Text = "Discount Type";
            // 
            // radioBtnEditDGDefaultNo
            // 
            this.radioBtnEditDGDefaultNo.AutoSize = true;
            this.radioBtnEditDGDefaultNo.Location = new System.Drawing.Point(52, 3);
            this.radioBtnEditDGDefaultNo.Name = "radioBtnEditDGDefaultNo";
            this.radioBtnEditDGDefaultNo.Size = new System.Drawing.Size(39, 17);
            this.radioBtnEditDGDefaultNo.TabIndex = 4;
            this.radioBtnEditDGDefaultNo.TabStop = true;
            this.radioBtnEditDGDefaultNo.Text = "No";
            this.radioBtnEditDGDefaultNo.UseVisualStyleBackColor = true;
            // 
            // radioBtnEditDGDefaultYes
            // 
            this.radioBtnEditDGDefaultYes.AutoSize = true;
            this.radioBtnEditDGDefaultYes.Location = new System.Drawing.Point(3, 3);
            this.radioBtnEditDGDefaultYes.Name = "radioBtnEditDGDefaultYes";
            this.radioBtnEditDGDefaultYes.Size = new System.Drawing.Size(43, 17);
            this.radioBtnEditDGDefaultYes.TabIndex = 3;
            this.radioBtnEditDGDefaultYes.TabStop = true;
            this.radioBtnEditDGDefaultYes.Text = "Yes";
            this.radioBtnEditDGDefaultYes.UseVisualStyleBackColor = true;
            // 
            // lblEditDGDefault
            // 
            this.lblEditDGDefault.AutoSize = true;
            this.lblEditDGDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditDGDefault.Location = new System.Drawing.Point(204, 153);
            this.lblEditDGDefault.Name = "lblEditDGDefault";
            this.lblEditDGDefault.Size = new System.Drawing.Size(44, 13);
            this.lblEditDGDefault.TabIndex = 34;
            this.lblEditDGDefault.Text = "Default:";
            // 
            // txtEditDiscountVal
            // 
            this.txtEditDiscountVal.Location = new System.Drawing.Point(150, 150);
            this.txtEditDiscountVal.Name = "txtEditDiscountVal";
            this.txtEditDiscountVal.Size = new System.Drawing.Size(48, 20);
            this.txtEditDiscountVal.TabIndex = 2;
            // 
            // lblEditDGDiscount
            // 
            this.lblEditDGDiscount.AutoSize = true;
            this.lblEditDGDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditDGDiscount.Location = new System.Drawing.Point(25, 153);
            this.lblEditDGDiscount.Name = "lblEditDGDiscount";
            this.lblEditDGDiscount.Size = new System.Drawing.Size(49, 13);
            this.lblEditDGDiscount.TabIndex = 31;
            this.lblEditDGDiscount.Text = "Discount";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(273, 224);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 39);
            this.button1.TabIndex = 8;
            this.button1.Text = "Reset";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnEditDisGrp
            // 
            this.btnEditDisGrp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditDisGrp.Location = new System.Drawing.Point(150, 224);
            this.btnEditDisGrp.Name = "btnEditDisGrp";
            this.btnEditDisGrp.Size = new System.Drawing.Size(117, 39);
            this.btnEditDisGrp.TabIndex = 7;
            this.btnEditDisGrp.Text = "Edit DiscountGroup";
            this.btnEditDisGrp.UseVisualStyleBackColor = true;
            this.btnEditDisGrp.Click += new System.EventHandler(this.btnEditDiscountGrp_Click);
            // 
            // lblEditDGDesc
            // 
            this.lblEditDGDesc.AutoSize = true;
            this.lblEditDGDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditDGDesc.Location = new System.Drawing.Point(25, 59);
            this.lblEditDGDesc.Name = "lblEditDGDesc";
            this.lblEditDGDesc.Size = new System.Drawing.Size(60, 13);
            this.lblEditDGDesc.TabIndex = 10;
            this.lblEditDGDesc.Text = "Description";
            // 
            // txtEditDisGrpDesc
            // 
            this.txtEditDisGrpDesc.Location = new System.Drawing.Point(150, 59);
            this.txtEditDisGrpDesc.Multiline = true;
            this.txtEditDisGrpDesc.Name = "txtEditDisGrpDesc";
            this.txtEditDisGrpDesc.Size = new System.Drawing.Size(208, 85);
            this.txtEditDisGrpDesc.TabIndex = 1;
            // 
            // lblEditDGName
            // 
            this.lblEditDGName.AutoSize = true;
            this.lblEditDGName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditDGName.Location = new System.Drawing.Point(25, 32);
            this.lblEditDGName.Name = "lblEditDGName";
            this.lblEditDGName.Size = new System.Drawing.Size(112, 13);
            this.lblEditDGName.TabIndex = 9;
            this.lblEditDGName.Text = "Discount Group Name";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(65, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 13);
            this.label9.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioBtnEditDGDefaultYes);
            this.panel1.Controls.Add(this.radioBtnEditDGDefaultNo);
            this.panel1.Location = new System.Drawing.Point(254, 148);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(96, 25);
            this.panel1.TabIndex = 41;
            // 
            // EditDiscountGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 295);
            this.Controls.Add(this.groupBox1);
            this.Name = "EditDiscountGroupForm";
            this.ShowIcon = false;
            this.Text = "Edit Discount Group Form";

            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioBtnEditDGDisTypeAbs;
        private System.Windows.Forms.RadioButton radioBtnEditDGDisTypePercent;
        private System.Windows.Forms.Label lblEditDGDisType;
        private System.Windows.Forms.RadioButton radioBtnEditDGDefaultNo;
        private System.Windows.Forms.RadioButton radioBtnEditDGDefaultYes;
        private System.Windows.Forms.Label lblEditDGDefault;
        private System.Windows.Forms.TextBox txtEditDiscountVal;
        private System.Windows.Forms.Label lblEditDGDiscount;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnEditDisGrp;
        private System.Windows.Forms.Label lblEditDGDesc;
        private System.Windows.Forms.TextBox txtEditDisGrpDesc;
        private System.Windows.Forms.Label lblEditDGName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblValidErrMsg;
        private System.Windows.Forms.ComboBox cmbxSelectDisGroupName;
        private System.Windows.Forms.Panel panel1;
    }
}