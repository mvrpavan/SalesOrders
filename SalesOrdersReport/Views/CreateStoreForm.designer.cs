using System;

namespace SalesOrdersReport
{
    partial class CreateStoreForm
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
            this.btnCreateStore = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCreateStoreCommonValidMsg = new System.Windows.Forms.Label();
            this.lblCreateStoreAsterik = new System.Windows.Forms.Label();
            this.txtStoreExcutivePhone = new System.Windows.Forms.TextBox();
            this.lblStorePhoneNo = new System.Windows.Forms.Label();
            this.txtStoreExecutiveName = new System.Windows.Forms.TextBox();
            this.lblStoreExecutiveName = new System.Windows.Forms.Label();
            this.lblStoreAddress = new System.Windows.Forms.Label();
            this.lblCreateStoreName = new System.Windows.Forms.Label();
            this.txtStoreAddress = new System.Windows.Forms.TextBox();
            this.txtCreateStoreName = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(216, 225);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(97, 39);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnCreateStore
            // 
            this.btnCreateStore.Location = new System.Drawing.Point(113, 225);
            this.btnCreateStore.Name = "btnCreateStore";
            this.btnCreateStore.Size = new System.Drawing.Size(97, 39);
            this.btnCreateStore.TabIndex = 9;
            this.btnCreateStore.Text = "Create Store";
            this.btnCreateStore.UseVisualStyleBackColor = true;
            this.btnCreateStore.Click += new System.EventHandler(this.btnCreateStore_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCreateStoreCommonValidMsg);
            this.groupBox1.Controls.Add(this.lblCreateStoreAsterik);
            this.groupBox1.Controls.Add(this.txtStoreExcutivePhone);
            this.groupBox1.Controls.Add(this.lblStorePhoneNo);
            this.groupBox1.Controls.Add(this.txtStoreExecutiveName);
            this.groupBox1.Controls.Add(this.lblStoreExecutiveName);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.lblStoreAddress);
            this.groupBox1.Controls.Add(this.btnCreateStore);
            this.groupBox1.Controls.Add(this.lblCreateStoreName);
            this.groupBox1.Controls.Add(this.txtStoreAddress);
            this.groupBox1.Controls.Add(this.txtCreateStoreName);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 279);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create Store";
            // 
            // lblCreateStoreCommonValidMsg
            // 
            this.lblCreateStoreCommonValidMsg.AutoSize = true;
            this.lblCreateStoreCommonValidMsg.ForeColor = System.Drawing.Color.Red;
            this.lblCreateStoreCommonValidMsg.Location = new System.Drawing.Point(110, 209);
            this.lblCreateStoreCommonValidMsg.Name = "lblCreateStoreCommonValidMsg";
            this.lblCreateStoreCommonValidMsg.Size = new System.Drawing.Size(0, 13);
            this.lblCreateStoreCommonValidMsg.TabIndex = 31;
            this.lblCreateStoreCommonValidMsg.Visible = false;
            // 
            // lblCreateStoreAsterik
            // 
            this.lblCreateStoreAsterik.AutoSize = true;
            this.lblCreateStoreAsterik.ForeColor = System.Drawing.Color.Red;
            this.lblCreateStoreAsterik.Location = new System.Drawing.Point(64, 28);
            this.lblCreateStoreAsterik.Name = "lblCreateStoreAsterik";
            this.lblCreateStoreAsterik.Size = new System.Drawing.Size(11, 13);
            this.lblCreateStoreAsterik.TabIndex = 24;
            this.lblCreateStoreAsterik.Text = "*";
            // 
            // txtStoreExcutivePhone
            // 
            this.txtStoreExcutivePhone.Location = new System.Drawing.Point(113, 186);
            this.txtStoreExcutivePhone.Name = "txtStoreExcutivePhone";
            this.txtStoreExcutivePhone.Size = new System.Drawing.Size(208, 20);
            this.txtStoreExcutivePhone.TabIndex = 4;
            // 
            // lblStorePhoneNo
            // 
            this.lblStorePhoneNo.AutoSize = true;
            this.lblStorePhoneNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStorePhoneNo.Location = new System.Drawing.Point(6, 189);
            this.lblStorePhoneNo.Name = "lblStorePhoneNo";
            this.lblStorePhoneNo.Size = new System.Drawing.Size(38, 13);
            this.lblStorePhoneNo.TabIndex = 8;
            this.lblStorePhoneNo.Text = "Phone";
            // 
            // txtStoreExecutiveName
            // 
            this.txtStoreExecutiveName.Location = new System.Drawing.Point(113, 160);
            this.txtStoreExecutiveName.Name = "txtStoreExecutiveName";
            this.txtStoreExecutiveName.Size = new System.Drawing.Size(208, 20);
            this.txtStoreExecutiveName.TabIndex = 2;
            // 
            // lblStoreExecutiveName
            // 
            this.lblStoreExecutiveName.AutoSize = true;
            this.lblStoreExecutiveName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStoreExecutiveName.Location = new System.Drawing.Point(4, 159);
            this.lblStoreExecutiveName.Name = "lblStoreExecutiveName";
            this.lblStoreExecutiveName.Size = new System.Drawing.Size(85, 13);
            this.lblStoreExecutiveName.TabIndex = 6;
            this.lblStoreExecutiveName.Text = "Executive Name";
            // 
            // lblStoreAddress
            // 
            this.lblStoreAddress.AutoSize = true;
            this.lblStoreAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStoreAddress.Location = new System.Drawing.Point(6, 54);
            this.lblStoreAddress.Name = "lblStoreAddress";
            this.lblStoreAddress.Size = new System.Drawing.Size(45, 13);
            this.lblStoreAddress.TabIndex = 12;
            this.lblStoreAddress.Text = "Address";
            // 
            // lblCreateStoreName
            // 
            this.lblCreateStoreName.AutoSize = true;
            this.lblCreateStoreName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateStoreName.Location = new System.Drawing.Point(6, 28);
            this.lblCreateStoreName.Name = "lblCreateStoreName";
            this.lblCreateStoreName.Size = new System.Drawing.Size(63, 13);
            this.lblCreateStoreName.TabIndex = 11;
            this.lblCreateStoreName.Text = "Store Name";
            // 
            // txtStoreAddress
            // 
            this.txtStoreAddress.Location = new System.Drawing.Point(113, 54);
            this.txtStoreAddress.Multiline = true;
            this.txtStoreAddress.Name = "txtStoreAddress";
            this.txtStoreAddress.Size = new System.Drawing.Size(208, 100);
            this.txtStoreAddress.TabIndex = 1;
            // 
            // txtCreateStoreName
            // 
            this.txtCreateStoreName.Location = new System.Drawing.Point(113, 28);
            this.txtCreateStoreName.Name = "txtCreateStoreName";
            this.txtCreateStoreName.Size = new System.Drawing.Size(208, 20);
            this.txtCreateStoreName.TabIndex = 0;
            // 
            // CreateStoreForm
            // 
            this.AcceptButton = this.btnCreateStore;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 312);
            this.Controls.Add(this.groupBox1);
            this.Name = "CreateStoreForm";
            this.ShowIcon = false;
            this.Text = "Create Store Form";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

     

        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCreateStore;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblStoreAddress;
        private System.Windows.Forms.Label lblCreateStoreName;
        private System.Windows.Forms.TextBox txtStoreAddress;
        private System.Windows.Forms.TextBox txtCreateStoreName;
        #endregion

        private System.Windows.Forms.Label lblStoreExecutiveName;
        private System.Windows.Forms.TextBox txtStoreExecutiveName;
        //private System.Windows.Forms.TextBox txtAddress;
        //private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStoreExcutivePhone;
        private System.Windows.Forms.Label lblStorePhoneNo;
        private System.Windows.Forms.Label lblCreateStoreAsterik;
        private System.Windows.Forms.Label lblCreateStoreCommonValidMsg;
    }
}