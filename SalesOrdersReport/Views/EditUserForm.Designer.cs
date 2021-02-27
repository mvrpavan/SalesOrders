using System;

namespace SalesOrdersReport
{
    partial class EditUserForm
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
            this.btnEditUser = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            //this.lblEditEmailIdValidMsg = new System.Windows.Forms.Label();
            this.lblCommonErrorMsg = new System.Windows.Forms.Label();
            this.lblRoleAsterik = new System.Windows.Forms.Label();
            this.lblActiveAsterik = new System.Windows.Forms.Label();
            this.lblFullNameAsterik = new System.Windows.Forms.Label();
            this.lblIsActive = new System.Windows.Forms.Label();
            this.rdbtnActiveNo = new System.Windows.Forms.RadioButton();
            this.rdbtnActiveYes = new System.Windows.Forms.RadioButton();
            this.cmbxSelectStore = new System.Windows.Forms.ComboBox();
            this.lblEditStoreName = new System.Windows.Forms.Label();
            this.txtEmailID = new System.Windows.Forms.TextBox();
            this.lblEditEmailID = new System.Windows.Forms.Label();
            this.cmbxSelectRoleID = new System.Windows.Forms.ComboBox();
            this.lblEditRoleID = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblEditPhoneNo = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblEditFullName = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(216, 230);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(97, 39);
            this.btnReset.TabIndex = 9;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnEditUser
            // 
            this.btnEditUser.Location = new System.Drawing.Point(113, 230);
            this.btnEditUser.Name = "btnEditUser";
            this.btnEditUser.Size = new System.Drawing.Size(97, 39);
            this.btnEditUser.TabIndex = 8;
            this.btnEditUser.Text = "EditUser";
            this.btnEditUser.UseVisualStyleBackColor = true;
            this.btnEditUser.Click += new System.EventHandler(this.btnEditUser_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblUserName);
            this.groupBox1.Controls.Add(this.txtUserName);
            //this.groupBox1.Controls.Add(this.lblEditEmailIdValidMsg);
            this.groupBox1.Controls.Add(this.lblCommonErrorMsg);
            this.groupBox1.Controls.Add(this.lblRoleAsterik);
            this.groupBox1.Controls.Add(this.lblActiveAsterik);
            this.groupBox1.Controls.Add(this.lblFullNameAsterik);
            this.groupBox1.Controls.Add(this.lblIsActive);
            this.groupBox1.Controls.Add(this.rdbtnActiveNo);
            this.groupBox1.Controls.Add(this.rdbtnActiveYes);
            this.groupBox1.Controls.Add(this.cmbxSelectStore);
            this.groupBox1.Controls.Add(this.lblEditStoreName);
            this.groupBox1.Controls.Add(this.txtEmailID);
            this.groupBox1.Controls.Add(this.lblEditEmailID);
            this.groupBox1.Controls.Add(this.cmbxSelectRoleID);
            this.groupBox1.Controls.Add(this.lblEditRoleID);
            this.groupBox1.Controls.Add(this.txtPhone);
            this.groupBox1.Controls.Add(this.lblEditPhoneNo);
            this.groupBox1.Controls.Add(this.txtFullName);
            this.groupBox1.Controls.Add(this.lblEditFullName);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.btnEditUser);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 282);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Edit User";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(6, 36);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(57, 13);
            this.lblUserName.TabIndex = 33;
            this.lblUserName.Text = "UserName";
            // 
            // txtUserName
            // 
            this.txtUserName.Enabled = false;
            this.txtUserName.Location = new System.Drawing.Point(113, 36);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(208, 20);
            this.txtUserName.TabIndex = 0;
            // 
            // lblEditEmailIdValidMsg
            // 
            //this.lblEditEmailIdValidMsg.AutoSize = true;
            //this.lblEditEmailIdValidMsg.ForeColor = System.Drawing.Color.Red;
            //this.lblEditEmailIdValidMsg.Location = new System.Drawing.Point(110, 124);
            //this.lblEditEmailIdValidMsg.Name = "lblEditEmailIdValidMsg";
            //this.lblEditEmailIdValidMsg.Size = new System.Drawing.Size(0, 13);
            //this.lblEditEmailIdValidMsg.TabIndex = 30;
            // 
            // lblCommonErrorMsg
            // 
            this.lblCommonErrorMsg.AutoSize = true;
            this.lblCommonErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.lblCommonErrorMsg.Location = new System.Drawing.Point(110, 214);
            this.lblCommonErrorMsg.Name = "lblCommonErrorMsg";
            this.lblCommonErrorMsg.Size = new System.Drawing.Size(0, 13);
            this.lblCommonErrorMsg.TabIndex = 29;
            // 
            // lblRoleAsterik
            // 
            this.lblRoleAsterik.AutoSize = true;
            this.lblRoleAsterik.ForeColor = System.Drawing.Color.Red;
            this.lblRoleAsterik.Location = new System.Drawing.Point(43, 193);
            this.lblRoleAsterik.Name = "lblRoleAsterik";
            this.lblRoleAsterik.Size = new System.Drawing.Size(11, 13);
            this.lblRoleAsterik.TabIndex = 28;
            this.lblRoleAsterik.Text = "*";
            // 
            // lblActiveAsterik
            // 
            this.lblActiveAsterik.AutoSize = true;
            this.lblActiveAsterik.ForeColor = System.Drawing.Color.Red;
            this.lblActiveAsterik.Location = new System.Drawing.Point(38, 166);
            this.lblActiveAsterik.Name = "lblActiveAsterik";
            this.lblActiveAsterik.Size = new System.Drawing.Size(11, 13);
            this.lblActiveAsterik.TabIndex = 27;
            this.lblActiveAsterik.Text = "*";
            // 
            // lblFullNameAsterik
            // 
            this.lblFullNameAsterik.AutoSize = true;
            this.lblFullNameAsterik.ForeColor = System.Drawing.Color.Red;
            this.lblFullNameAsterik.Location = new System.Drawing.Point(55, 61);
            this.lblFullNameAsterik.Name = "lblFullNameAsterik";
            this.lblFullNameAsterik.Size = new System.Drawing.Size(11, 13);
            this.lblFullNameAsterik.TabIndex = 26;
            this.lblFullNameAsterik.Text = "*";
            // 
            // lblIsActive
            // 
            this.lblIsActive.AutoSize = true;
            this.lblIsActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIsActive.Location = new System.Drawing.Point(6, 166);
            this.lblIsActive.Name = "lblIsActive";
            this.lblIsActive.Size = new System.Drawing.Size(37, 13);
            this.lblIsActive.TabIndex = 22;
            this.lblIsActive.Text = "Active";
            // 
            // rdbtnActiveNo
            // 
            this.rdbtnActiveNo.AutoSize = true;
            this.rdbtnActiveNo.Location = new System.Drawing.Point(192, 167);
            this.rdbtnActiveNo.Name = "rdbtnActiveNo";
            this.rdbtnActiveNo.Size = new System.Drawing.Size(39, 17);
            this.rdbtnActiveNo.TabIndex = 6;
            this.rdbtnActiveNo.Text = "No";
            this.rdbtnActiveNo.UseVisualStyleBackColor = true;
            // 
            // rdbtnActiveYes
            // 
            this.rdbtnActiveYes.AutoSize = true;
            this.rdbtnActiveYes.Location = new System.Drawing.Point(113, 167);
            this.rdbtnActiveYes.Name = "rdbtnActiveYes";
            this.rdbtnActiveYes.Size = new System.Drawing.Size(43, 17);
            this.rdbtnActiveYes.TabIndex = 5;
            this.rdbtnActiveYes.Text = "Yes";
            this.rdbtnActiveYes.UseVisualStyleBackColor = true;
            // 
            // cmbxSelectStore
            // 
            this.cmbxSelectStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxSelectStore.FormattingEnabled = true;
            this.cmbxSelectStore.Location = new System.Drawing.Point(113, 140);
            this.cmbxSelectStore.Name = "cmbxSelectStore";
            this.cmbxSelectStore.Size = new System.Drawing.Size(121, 21);
            this.cmbxSelectStore.TabIndex = 4;
            this.cmbxSelectStore.Click += new System.EventHandler(this.cmbxSelectStore_Click);
            // 
            // lblEditStoreName
            // 
            this.lblEditStoreName.AutoSize = true;
            this.lblEditStoreName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditStoreName.Location = new System.Drawing.Point(8, 140);
            this.lblEditStoreName.Name = "lblEditStoreName";
            this.lblEditStoreName.Size = new System.Drawing.Size(32, 13);
            this.lblEditStoreName.TabIndex = 18;
            this.lblEditStoreName.Text = "Store";
            // 
            // txtEmailID
            // 
            this.txtEmailID.Location = new System.Drawing.Point(113, 88);
            this.txtEmailID.Name = "txtEmailID";
            this.txtEmailID.Size = new System.Drawing.Size(208, 20);
            this.txtEmailID.TabIndex = 2;
            // 
            // lblEditEmailID
            // 
            this.lblEditEmailID.AutoSize = true;
            this.lblEditEmailID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditEmailID.Location = new System.Drawing.Point(6, 87);
            this.lblEditEmailID.Name = "lblEditEmailID";
            this.lblEditEmailID.Size = new System.Drawing.Size(46, 13);
            this.lblEditEmailID.TabIndex = 14;
            this.lblEditEmailID.Text = "Email ID";
            // 
            // cmbxSelectRoleID
            // 
            this.cmbxSelectRoleID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxSelectRoleID.FormattingEnabled = true;
            this.cmbxSelectRoleID.Location = new System.Drawing.Point(113, 190);
            this.cmbxSelectRoleID.Name = "cmbxSelectRoleID";
            this.cmbxSelectRoleID.Size = new System.Drawing.Size(121, 21);
            this.cmbxSelectRoleID.TabIndex = 7;
            this.cmbxSelectRoleID.Click += new System.EventHandler(this.cmbxSelectRoleID_Click);
            // 
            // lblEditRoleID
            // 
            this.lblEditRoleID.AutoSize = true;
            this.lblEditRoleID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditRoleID.Location = new System.Drawing.Point(8, 193);
            this.lblEditRoleID.Name = "lblEditRoleID";
            this.lblEditRoleID.Size = new System.Drawing.Size(40, 13);
            this.lblEditRoleID.TabIndex = 12;
            this.lblEditRoleID.Text = "RoleID";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(113, 114);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(208, 20);
            this.txtPhone.TabIndex = 3;
            // 
            // lblEditPhoneNo
            // 
            this.lblEditPhoneNo.AutoSize = true;
            this.lblEditPhoneNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditPhoneNo.Location = new System.Drawing.Point(6, 117);
            this.lblEditPhoneNo.Name = "lblEditPhoneNo";
            this.lblEditPhoneNo.Size = new System.Drawing.Size(38, 13);
            this.lblEditPhoneNo.TabIndex = 8;
            this.lblEditPhoneNo.Text = "Phone";
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(113, 62);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(208, 20);
            this.txtFullName.TabIndex = 1;
            // 
            // lblEditFullName
            // 
            this.lblEditFullName.AutoSize = true;
            this.lblEditFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditFullName.Location = new System.Drawing.Point(6, 61);
            this.lblEditFullName.Name = "lblEditFullName";
            this.lblEditFullName.Size = new System.Drawing.Size(54, 13);
            this.lblEditFullName.TabIndex = 6;
            this.lblEditFullName.Text = "Full Name";
            // 
            // EditUserForm
            // 
            this.AcceptButton = this.btnEditUser;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 300);
            this.Controls.Add(this.groupBox1);
            this.Name = "EditUserForm";
            this.ShowIcon = false;
            this.Text = "Edit User Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditUserForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }



        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnEditUser;
        private System.Windows.Forms.GroupBox groupBox1;
        #endregion

        private System.Windows.Forms.Label lblEditFullName;
        public System.Windows.Forms.TextBox txtFullName;
        //private System.Windows.Forms.TextBox txtAddress;
        //private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblEditPhoneNo;
        private System.Windows.Forms.Label lblEditRoleID;
        public System.Windows.Forms.ComboBox cmbxSelectRoleID;
        public System.Windows.Forms.TextBox txtEmailID;
        private System.Windows.Forms.Label lblEditEmailID;
        public System.Windows.Forms.ComboBox cmbxSelectStore;
        private System.Windows.Forms.Label lblEditStoreName;
        public System.Windows.Forms.RadioButton rdbtnActiveNo;
        public System.Windows.Forms.RadioButton rdbtnActiveYes;
        private System.Windows.Forms.Label lblIsActive;
        private System.Windows.Forms.Label lblRoleAsterik;
        private System.Windows.Forms.Label lblActiveAsterik;
        private System.Windows.Forms.Label lblFullNameAsterik;
        //private System.Windows.Forms.Label lblEditPhoneValidMsg;
        //private System.Windows.Forms.Label lblEditEmailIdValidMsg;
        private System.Windows.Forms.Label lblCommonErrorMsg;
        private System.Windows.Forms.Label lblUserName;
        public System.Windows.Forms.TextBox txtUserName;
        //private System.Windows.Forms.Label lblEditUserValidMsg;
    }
}