using System;

namespace SalesOrdersReport
{
    partial class EditStoreForm
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
            this.btnEditStore = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbxAllStoreNames = new System.Windows.Forms.ComboBox();
            this.lblEditStoreCommonValidMsg = new System.Windows.Forms.Label();
            this.lblEditStoreAsterik = new System.Windows.Forms.Label();
            this.txtEditStoreExcutivePhone = new System.Windows.Forms.TextBox();
            this.lblStorePhoneNo = new System.Windows.Forms.Label();
            this.txtEditStoreExecutiveName = new System.Windows.Forms.TextBox();
            this.lblStoreExecutiveName = new System.Windows.Forms.Label();
            this.lblStoreAddress = new System.Windows.Forms.Label();
            this.lblEditStoreName = new System.Windows.Forms.Label();
            this.txtEditStoreAddress = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(216, 226);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(97, 39);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnEditStore
            // 
            this.btnEditStore.Location = new System.Drawing.Point(113, 226);
            this.btnEditStore.Name = "btnEditStore";
            this.btnEditStore.Size = new System.Drawing.Size(97, 39);
            this.btnEditStore.TabIndex = 9;
            this.btnEditStore.Text = "Edit Store";
            this.btnEditStore.UseVisualStyleBackColor = true;
            this.btnEditStore.Click += new System.EventHandler(this.btnEditStore_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbxAllStoreNames);
            this.groupBox1.Controls.Add(this.lblEditStoreCommonValidMsg);
            this.groupBox1.Controls.Add(this.lblEditStoreAsterik);
            this.groupBox1.Controls.Add(this.txtEditStoreExcutivePhone);
            this.groupBox1.Controls.Add(this.lblStorePhoneNo);
            this.groupBox1.Controls.Add(this.txtEditStoreExecutiveName);
            this.groupBox1.Controls.Add(this.lblStoreExecutiveName);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.lblStoreAddress);
            this.groupBox1.Controls.Add(this.btnEditStore);
            this.groupBox1.Controls.Add(this.lblEditStoreName);
            this.groupBox1.Controls.Add(this.txtEditStoreAddress);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 286);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Edit Store";
            // 
            // cmbxAllStoreNames
            // 
            this.cmbxAllStoreNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxAllStoreNames.FormattingEnabled = true;
            this.cmbxAllStoreNames.Location = new System.Drawing.Point(113, 28);
            this.cmbxAllStoreNames.Name = "cmbxAllStoreNames";
            this.cmbxAllStoreNames.Size = new System.Drawing.Size(121, 21);
            this.cmbxAllStoreNames.TabIndex = 33;
            this.cmbxAllStoreNames.SelectedIndexChanged += new System.EventHandler(this.cmbxAllStoreNames_SelectedIndexChanged);
            // 
            // lblEditStoreCommonValidMsg
            // 
            this.lblEditStoreCommonValidMsg.AutoSize = true;
            this.lblEditStoreCommonValidMsg.ForeColor = System.Drawing.Color.Red;
            this.lblEditStoreCommonValidMsg.Location = new System.Drawing.Point(110, 210);
            this.lblEditStoreCommonValidMsg.Name = "lblEditStoreCommonValidMsg";
            this.lblEditStoreCommonValidMsg.Size = new System.Drawing.Size(0, 13);
            this.lblEditStoreCommonValidMsg.TabIndex = 31;
            this.lblEditStoreCommonValidMsg.Visible = false;
            // 
            // lblEditStoreAsterik
            // 
            this.lblEditStoreAsterik.AutoSize = true;
            this.lblEditStoreAsterik.ForeColor = System.Drawing.Color.Red;
            this.lblEditStoreAsterik.Location = new System.Drawing.Point(64, 28);
            this.lblEditStoreAsterik.Name = "lblEditStoreAsterik";
            this.lblEditStoreAsterik.Size = new System.Drawing.Size(11, 13);
            this.lblEditStoreAsterik.TabIndex = 24;
            this.lblEditStoreAsterik.Text = "*";
            // 
            // txtEditStoreExcutivePhone
            // 
            this.txtEditStoreExcutivePhone.Location = new System.Drawing.Point(113, 187);
            this.txtEditStoreExcutivePhone.Name = "txtEditStoreExcutivePhone";
            this.txtEditStoreExcutivePhone.Size = new System.Drawing.Size(208, 20);
            this.txtEditStoreExcutivePhone.TabIndex = 4;
            // 
            // lblStorePhoneNo
            // 
            this.lblStorePhoneNo.AutoSize = true;
            this.lblStorePhoneNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStorePhoneNo.Location = new System.Drawing.Point(6, 190);
            this.lblStorePhoneNo.Name = "lblStorePhoneNo";
            this.lblStorePhoneNo.Size = new System.Drawing.Size(38, 13);
            this.lblStorePhoneNo.TabIndex = 8;
            this.lblStorePhoneNo.Text = "Phone";
            // 
            // txtEditStoreExecutiveName
            // 
            this.txtEditStoreExecutiveName.Location = new System.Drawing.Point(113, 161);
            this.txtEditStoreExecutiveName.Name = "txtEditStoreExecutiveName";
            this.txtEditStoreExecutiveName.Size = new System.Drawing.Size(206, 20);
            this.txtEditStoreExecutiveName.TabIndex = 2;
            // 
            // lblStoreExecutiveName
            // 
            this.lblStoreExecutiveName.AutoSize = true;
            this.lblStoreExecutiveName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStoreExecutiveName.Location = new System.Drawing.Point(4, 160);
            this.lblStoreExecutiveName.Name = "lblStoreExecutiveName";
            this.lblStoreExecutiveName.Size = new System.Drawing.Size(85, 13);
            this.lblStoreExecutiveName.TabIndex = 6;
            this.lblStoreExecutiveName.Text = "Executive Name";
            // 
            // lblStoreAddress
            // 
            this.lblStoreAddress.AutoSize = true;
            this.lblStoreAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStoreAddress.Location = new System.Drawing.Point(6, 55);
            this.lblStoreAddress.Name = "lblStoreAddress";
            this.lblStoreAddress.Size = new System.Drawing.Size(45, 13);
            this.lblStoreAddress.TabIndex = 12;
            this.lblStoreAddress.Text = "Address";
            // 
            // lblEditStoreName
            // 
            this.lblEditStoreName.AutoSize = true;
            this.lblEditStoreName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditStoreName.Location = new System.Drawing.Point(6, 28);
            this.lblEditStoreName.Name = "lblEditStoreName";
            this.lblEditStoreName.Size = new System.Drawing.Size(63, 13);
            this.lblEditStoreName.TabIndex = 11;
            this.lblEditStoreName.Text = "Store Name";
            // 
            // txtEditStoreAddress
            // 
            this.txtEditStoreAddress.Location = new System.Drawing.Point(113, 55);
            this.txtEditStoreAddress.Multiline = true;
            this.txtEditStoreAddress.Name = "txtEditStoreAddress";
            this.txtEditStoreAddress.Size = new System.Drawing.Size(208, 100);
            this.txtEditStoreAddress.TabIndex = 1;
            // 
            // EditStoreForm
            // 
            this.AcceptButton = this.btnEditStore;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 314);
            this.Controls.Add(this.groupBox1);
            this.Name = "EditStoreForm";
            this.ShowIcon = false;
            this.Text = "Edit Store Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditStoreForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }



        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnEditStore;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblStoreAddress;
        private System.Windows.Forms.Label lblEditStoreName;
        private System.Windows.Forms.TextBox txtEditStoreAddress;
        #endregion

        private System.Windows.Forms.Label lblStoreExecutiveName;
        private System.Windows.Forms.TextBox txtEditStoreExecutiveName;
        //private System.Windows.Forms.TextBox txtAddress;
        //private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEditStoreExcutivePhone;
        private System.Windows.Forms.Label lblStorePhoneNo;
        private System.Windows.Forms.Label lblEditStoreAsterik;
        private System.Windows.Forms.Label lblEditStoreCommonValidMsg;
        private System.Windows.Forms.ComboBox cmbxAllStoreNames;
    }
}