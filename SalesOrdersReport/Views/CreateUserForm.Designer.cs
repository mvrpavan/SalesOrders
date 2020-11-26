using System;

namespace SalesOrdersReport
{
    partial class CreateUserForm
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
            this.btnCreateUser = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCreatePwdValidMsg = new System.Windows.Forms.Label();
            this.lblCreatePhoneValidMsg = new System.Windows.Forms.Label();
            this.lblCreateEmailIdValidMsg = new System.Windows.Forms.Label();
            this.lblCommonErrorMsg = new System.Windows.Forms.Label();
            this.lblRoleAsterik = new System.Windows.Forms.Label();
            this.lblActiveAsterik = new System.Windows.Forms.Label();
            this.lblFullNameAsterik = new System.Windows.Forms.Label();
            this.lblCreatePwdAsterik = new System.Windows.Forms.Label();
            this.lblCreateUserAsterik = new System.Windows.Forms.Label();
            this.lblIsActive = new System.Windows.Forms.Label();
            this.rdbtnActiveNo = new System.Windows.Forms.RadioButton();
            this.rdbtnActiveYes = new System.Windows.Forms.RadioButton();
            this.cmbxSelectStore = new System.Windows.Forms.ComboBox();
            this.lblCreateStoreName = new System.Windows.Forms.Label();
            this.txtEmailID = new System.Windows.Forms.TextBox();
            this.lblCreateEmailID = new System.Windows.Forms.Label();
            this.cmbxSelectRoleID = new System.Windows.Forms.ComboBox();
            this.lblCreateRoleID = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblCreatePhoneNo = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblCreateFullName = new System.Windows.Forms.Label();
            this.lblCreatePassword = new System.Windows.Forms.Label();
            this.lblCreateUserName = new System.Windows.Forms.Label();
            this.txtCreatePassword = new System.Windows.Forms.TextBox();
            this.txtCreateUserName = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(214, 361);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(97, 39);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnCreateUser
            // 
            this.btnCreateUser.Location = new System.Drawing.Point(111, 361);
            this.btnCreateUser.Name = "btnCreateUser";
            this.btnCreateUser.Size = new System.Drawing.Size(97, 39);
            this.btnCreateUser.TabIndex = 9;
            this.btnCreateUser.Text = "CreateUser";
            this.btnCreateUser.UseVisualStyleBackColor = true;
            this.btnCreateUser.Click += new System.EventHandler(this.btnCreateUser_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCreatePwdValidMsg);
            this.groupBox1.Controls.Add(this.lblCreatePhoneValidMsg);
            this.groupBox1.Controls.Add(this.lblCreateEmailIdValidMsg);
            this.groupBox1.Controls.Add(this.lblCommonErrorMsg);
            this.groupBox1.Controls.Add(this.lblRoleAsterik);
            this.groupBox1.Controls.Add(this.lblActiveAsterik);
            this.groupBox1.Controls.Add(this.lblFullNameAsterik);
            this.groupBox1.Controls.Add(this.lblCreatePwdAsterik);
            this.groupBox1.Controls.Add(this.lblCreateUserAsterik);
            this.groupBox1.Controls.Add(this.lblIsActive);
            this.groupBox1.Controls.Add(this.rdbtnActiveNo);
            this.groupBox1.Controls.Add(this.rdbtnActiveYes);
            this.groupBox1.Controls.Add(this.cmbxSelectStore);
            this.groupBox1.Controls.Add(this.lblCreateStoreName);
            this.groupBox1.Controls.Add(this.txtEmailID);
            this.groupBox1.Controls.Add(this.lblCreateEmailID);
            this.groupBox1.Controls.Add(this.cmbxSelectRoleID);
            this.groupBox1.Controls.Add(this.lblCreateRoleID);
            this.groupBox1.Controls.Add(this.txtPhone);
            this.groupBox1.Controls.Add(this.lblCreatePhoneNo);
            this.groupBox1.Controls.Add(this.txtFullName);
            this.groupBox1.Controls.Add(this.lblCreateFullName);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.lblCreatePassword);
            this.groupBox1.Controls.Add(this.btnCreateUser);
            this.groupBox1.Controls.Add(this.lblCreateUserName);
            this.groupBox1.Controls.Add(this.txtCreatePassword);
            this.groupBox1.Controls.Add(this.txtCreateUserName);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 444);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create User";
            // 
            // lblCreatePwdValidMsg
            // 
            this.lblCreatePwdValidMsg.AutoSize = true;
            this.lblCreatePwdValidMsg.ForeColor = System.Drawing.Color.Red;
            this.lblCreatePwdValidMsg.Location = new System.Drawing.Point(108, 87);
            this.lblCreatePwdValidMsg.Name = "lblCreatePwdValidMsg";
            this.lblCreatePwdValidMsg.Size = new System.Drawing.Size(0, 13);
            this.lblCreatePwdValidMsg.TabIndex = 32;
            // 
            // lblCreatePhoneValidMsg
            // 
            this.lblCreatePhoneValidMsg.AutoSize = true;
            this.lblCreatePhoneValidMsg.ForeColor = System.Drawing.Color.Red;
            this.lblCreatePhoneValidMsg.Location = new System.Drawing.Point(110, 208);
            this.lblCreatePhoneValidMsg.Name = "lblCreatePhoneValidMsg";
            this.lblCreatePhoneValidMsg.Size = new System.Drawing.Size(0, 13);
            this.lblCreatePhoneValidMsg.TabIndex = 31;
            // 
            // lblCreateEmailIdValidMsg
            // 
            this.lblCreateEmailIdValidMsg.AutoSize = true;
            this.lblCreateEmailIdValidMsg.ForeColor = System.Drawing.Color.Red;
            this.lblCreateEmailIdValidMsg.Location = new System.Drawing.Point(110, 168);
            this.lblCreateEmailIdValidMsg.Name = "lblCreateEmailIdValidMsg";
            this.lblCreateEmailIdValidMsg.Size = new System.Drawing.Size(0, 13);
            this.lblCreateEmailIdValidMsg.TabIndex = 30;
            // 
            // lblCommonErrorMsg
            // 
            this.lblCommonErrorMsg.AutoSize = true;
            this.lblCommonErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.lblCommonErrorMsg.Location = new System.Drawing.Point(108, 328);
            this.lblCommonErrorMsg.Name = "lblCommonErrorMsg";
            this.lblCommonErrorMsg.Size = new System.Drawing.Size(0, 13);
            this.lblCommonErrorMsg.TabIndex = 29;
            // 
            // lblRoleAsterik
            // 
            this.lblRoleAsterik.AutoSize = true;
            this.lblRoleAsterik.ForeColor = System.Drawing.Color.Red;
            this.lblRoleAsterik.Location = new System.Drawing.Point(41, 296);
            this.lblRoleAsterik.Name = "lblRoleAsterik";
            this.lblRoleAsterik.Size = new System.Drawing.Size(11, 13);
            this.lblRoleAsterik.TabIndex = 28;
            this.lblRoleAsterik.Text = "*";
            // 
            // lblActiveAsterik
            // 
            this.lblActiveAsterik.AutoSize = true;
            this.lblActiveAsterik.ForeColor = System.Drawing.Color.Red;
            this.lblActiveAsterik.Location = new System.Drawing.Point(38, 262);
            this.lblActiveAsterik.Name = "lblActiveAsterik";
            this.lblActiveAsterik.Size = new System.Drawing.Size(11, 13);
            this.lblActiveAsterik.TabIndex = 27;
            this.lblActiveAsterik.Text = "*";
            // 
            // lblFullNameAsterik
            // 
            this.lblFullNameAsterik.AutoSize = true;
            this.lblFullNameAsterik.ForeColor = System.Drawing.Color.Red;
            this.lblFullNameAsterik.Location = new System.Drawing.Point(55, 102);
            this.lblFullNameAsterik.Name = "lblFullNameAsterik";
            this.lblFullNameAsterik.Size = new System.Drawing.Size(11, 13);
            this.lblFullNameAsterik.TabIndex = 26;
            this.lblFullNameAsterik.Text = "*";
            // 
            // lblCreatePwdAsterik
            // 
            this.lblCreatePwdAsterik.AutoSize = true;
            this.lblCreatePwdAsterik.ForeColor = System.Drawing.Color.Red;
            this.lblCreatePwdAsterik.Location = new System.Drawing.Point(55, 66);
            this.lblCreatePwdAsterik.Name = "lblCreatePwdAsterik";
            this.lblCreatePwdAsterik.Size = new System.Drawing.Size(11, 13);
            this.lblCreatePwdAsterik.TabIndex = 25;
            this.lblCreatePwdAsterik.Text = "*";
            // 
            // lblCreateUserAsterik
            // 
            this.lblCreateUserAsterik.AutoSize = true;
            this.lblCreateUserAsterik.ForeColor = System.Drawing.Color.Red;
            this.lblCreateUserAsterik.Location = new System.Drawing.Point(61, 28);
            this.lblCreateUserAsterik.Name = "lblCreateUserAsterik";
            this.lblCreateUserAsterik.Size = new System.Drawing.Size(11, 13);
            this.lblCreateUserAsterik.TabIndex = 24;
            this.lblCreateUserAsterik.Text = "*";
            // 
            // lblIsActive
            // 
            this.lblIsActive.AutoSize = true;
            this.lblIsActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIsActive.Location = new System.Drawing.Point(6, 262);
            this.lblIsActive.Name = "lblIsActive";
            this.lblIsActive.Size = new System.Drawing.Size(37, 13);
            this.lblIsActive.TabIndex = 22;
            this.lblIsActive.Text = "Active";
            // 
            // rdbtnActiveNo
            // 
            this.rdbtnActiveNo.AutoSize = true;
            this.rdbtnActiveNo.Location = new System.Drawing.Point(192, 263);
            this.rdbtnActiveNo.Name = "rdbtnActiveNo";
            this.rdbtnActiveNo.Size = new System.Drawing.Size(39, 17);
            this.rdbtnActiveNo.TabIndex = 7;
            this.rdbtnActiveNo.Text = "No";
            this.rdbtnActiveNo.UseVisualStyleBackColor = true;
            // 
            // rdbtnActiveYes
            // 
            this.rdbtnActiveYes.AutoSize = true;
            this.rdbtnActiveYes.Location = new System.Drawing.Point(113, 263);
            this.rdbtnActiveYes.Name = "rdbtnActiveYes";
            this.rdbtnActiveYes.Size = new System.Drawing.Size(43, 17);
            this.rdbtnActiveYes.TabIndex = 6;
            this.rdbtnActiveYes.Text = "Yes";
            this.rdbtnActiveYes.UseVisualStyleBackColor = true;
            // 
            // cmbxSelectStore
            // 
            this.cmbxSelectStore.FormattingEnabled = true;
            this.cmbxSelectStore.Location = new System.Drawing.Point(111, 226);
            this.cmbxSelectStore.Name = "cmbxSelectStore";
            this.cmbxSelectStore.Size = new System.Drawing.Size(121, 21);
            this.cmbxSelectStore.TabIndex = 5;
            this.cmbxSelectStore.Text = "Select Store";
            this.cmbxSelectStore.Click += new System.EventHandler(this.cmbxSelectStore_Click);
            // 
            // lblCreateStoreName
            // 
            this.lblCreateStoreName.AutoSize = true;
            this.lblCreateStoreName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateStoreName.Location = new System.Drawing.Point(6, 226);
            this.lblCreateStoreName.Name = "lblCreateStoreName";
            this.lblCreateStoreName.Size = new System.Drawing.Size(32, 13);
            this.lblCreateStoreName.TabIndex = 18;
            this.lblCreateStoreName.Text = "Store";
            // 
            // txtEmailID
            // 
            this.txtEmailID.Location = new System.Drawing.Point(113, 145);
            this.txtEmailID.Name = "txtEmailID";
            this.txtEmailID.Size = new System.Drawing.Size(208, 20);
            this.txtEmailID.TabIndex = 3;
            // 
            // lblCreateEmailID
            // 
            this.lblCreateEmailID.AutoSize = true;
            this.lblCreateEmailID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateEmailID.Location = new System.Drawing.Point(6, 144);
            this.lblCreateEmailID.Name = "lblCreateEmailID";
            this.lblCreateEmailID.Size = new System.Drawing.Size(46, 13);
            this.lblCreateEmailID.TabIndex = 14;
            this.lblCreateEmailID.Text = "Email ID";
            // 
            // cmbxSelectRoleID
            // 
            this.cmbxSelectRoleID.FormattingEnabled = true;
            this.cmbxSelectRoleID.Location = new System.Drawing.Point(111, 293);
            this.cmbxSelectRoleID.Name = "cmbxSelectRoleID";
            this.cmbxSelectRoleID.Size = new System.Drawing.Size(121, 21);
            this.cmbxSelectRoleID.TabIndex = 8;
            this.cmbxSelectRoleID.Text = "Select Role";
            this.cmbxSelectRoleID.Click += new System.EventHandler(this.cmbxSelectRoleID_Click);
            // 
            // lblCreateRoleID
            // 
            this.lblCreateRoleID.AutoSize = true;
            this.lblCreateRoleID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateRoleID.Location = new System.Drawing.Point(6, 296);
            this.lblCreateRoleID.Name = "lblCreateRoleID";
            this.lblCreateRoleID.Size = new System.Drawing.Size(40, 13);
            this.lblCreateRoleID.TabIndex = 12;
            this.lblCreateRoleID.Text = "RoleID";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(113, 185);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(208, 20);
            this.txtPhone.TabIndex = 4;
            // 
            // lblCreatePhoneNo
            // 
            this.lblCreatePhoneNo.AutoSize = true;
            this.lblCreatePhoneNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatePhoneNo.Location = new System.Drawing.Point(6, 188);
            this.lblCreatePhoneNo.Name = "lblCreatePhoneNo";
            this.lblCreatePhoneNo.Size = new System.Drawing.Size(38, 13);
            this.lblCreatePhoneNo.TabIndex = 8;
            this.lblCreatePhoneNo.Text = "Phone";
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(113, 103);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(208, 20);
            this.txtFullName.TabIndex = 2;
            // 
            // lblCreateFullName
            // 
            this.lblCreateFullName.AutoSize = true;
            this.lblCreateFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateFullName.Location = new System.Drawing.Point(6, 102);
            this.lblCreateFullName.Name = "lblCreateFullName";
            this.lblCreateFullName.Size = new System.Drawing.Size(54, 13);
            this.lblCreateFullName.TabIndex = 6;
            this.lblCreateFullName.Text = "Full Name";
            // 
            // lblCreatePassword
            // 
            this.lblCreatePassword.AutoSize = true;
            this.lblCreatePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatePassword.Location = new System.Drawing.Point(6, 66);
            this.lblCreatePassword.Name = "lblCreatePassword";
            this.lblCreatePassword.Size = new System.Drawing.Size(53, 13);
            this.lblCreatePassword.TabIndex = 12;
            this.lblCreatePassword.Text = "Password";
            // 
            // lblCreateUserName
            // 
            this.lblCreateUserName.AutoSize = true;
            this.lblCreateUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateUserName.Location = new System.Drawing.Point(6, 28);
            this.lblCreateUserName.Name = "lblCreateUserName";
            this.lblCreateUserName.Size = new System.Drawing.Size(60, 13);
            this.lblCreateUserName.TabIndex = 11;
            this.lblCreateUserName.Text = "User Name";
            // 
            // txtCreatePassword
            // 
            this.txtCreatePassword.Location = new System.Drawing.Point(113, 66);
            this.txtCreatePassword.Name = "txtCreatePassword";
            this.txtCreatePassword.PasswordChar = '*';
            this.txtCreatePassword.Size = new System.Drawing.Size(208, 20);
            this.txtCreatePassword.TabIndex = 1;
            // 
            // txtCreateUserName
            // 
            this.txtCreateUserName.Location = new System.Drawing.Point(113, 28);
            this.txtCreateUserName.Name = "txtCreateUserName";
            this.txtCreateUserName.Size = new System.Drawing.Size(208, 20);
            this.txtCreateUserName.TabIndex = 0;
            // 
            // CreateUserForm
            // 
            this.AcceptButton = this.btnCreateUser;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 471);
            this.Controls.Add(this.groupBox1);
            this.Name = "CreateUserForm";
            this.ShowIcon = false;
            this.Text = "Create User Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CreateUserForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

     

        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCreateUser;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCreatePassword;
        private System.Windows.Forms.Label lblCreateUserName;
        private System.Windows.Forms.TextBox txtCreatePassword;
        private System.Windows.Forms.TextBox txtCreateUserName;
        #endregion

        private System.Windows.Forms.Label lblCreateFullName;
        private System.Windows.Forms.TextBox txtFullName;
        //private System.Windows.Forms.TextBox txtAddress;
        //private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblCreatePhoneNo;
        private System.Windows.Forms.Label lblCreateRoleID;
        private System.Windows.Forms.ComboBox cmbxSelectRoleID;
        private System.Windows.Forms.TextBox txtEmailID;
        private System.Windows.Forms.Label lblCreateEmailID;
        private System.Windows.Forms.ComboBox cmbxSelectStore;
        private System.Windows.Forms.Label lblCreateStoreName;
        private System.Windows.Forms.RadioButton rdbtnActiveNo;
        private System.Windows.Forms.RadioButton rdbtnActiveYes;
        private System.Windows.Forms.Label lblIsActive;
        private System.Windows.Forms.Label lblCreateUserAsterik;
        private System.Windows.Forms.Label lblRoleAsterik;
        private System.Windows.Forms.Label lblActiveAsterik;
        private System.Windows.Forms.Label lblFullNameAsterik;
        private System.Windows.Forms.Label lblCreatePwdAsterik;
        private System.Windows.Forms.Label lblCreatePhoneValidMsg;
        private System.Windows.Forms.Label lblCreateEmailIdValidMsg;
        private System.Windows.Forms.Label lblCommonErrorMsg;
        private System.Windows.Forms.Label lblCreatePwdValidMsg;
    }
}