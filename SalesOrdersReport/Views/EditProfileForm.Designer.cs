namespace SalesOrdersReport
{
    partial class EditProfileForm
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
            this.grpEditProfile = new System.Windows.Forms.GroupBox();
            this.grpGeneralSettings = new System.Windows.Forms.GroupBox();
            this.lblPhoneMsg = new System.Windows.Forms.Label();
            this.lblEmailValidMsg = new System.Windows.Forms.Label();
            this.lblEmailID = new System.Windows.Forms.Label();
            this.txtbxEmailID = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtChangePhone = new System.Windows.Forms.TextBox();
            this.gpbxPwdSettings = new System.Windows.Forms.GroupBox();
            this.lblOldPwdMsg = new System.Windows.Forms.Label();
            this.lblCnfrmPwdMsg = new System.Windows.Forms.Label();
            this.lblNwPwdMsg = new System.Windows.Forms.Label();
            this.txtbxConfirmNwPwd = new System.Windows.Forms.TextBox();
            this.txtbxNewPwd = new System.Windows.Forms.TextBox();
            this.txtOldPwd = new System.Windows.Forms.TextBox();
            this.lblConfmNwPwd = new System.Windows.Forms.Label();
            this.lblNwPwd = new System.Windows.Forms.Label();
            this.lblOldPwd = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnEditProfile = new System.Windows.Forms.Button();
            this.grpEditProfile.SuspendLayout();
            this.grpGeneralSettings.SuspendLayout();
            this.gpbxPwdSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpEditProfile
            // 
            this.grpEditProfile.Controls.Add(this.grpGeneralSettings);
            this.grpEditProfile.Controls.Add(this.gpbxPwdSettings);
            this.grpEditProfile.Controls.Add(this.btnReset);
            this.grpEditProfile.Controls.Add(this.btnEditProfile);
            this.grpEditProfile.Location = new System.Drawing.Point(12, 29);
            this.grpEditProfile.Name = "grpEditProfile";
            this.grpEditProfile.Size = new System.Drawing.Size(416, 394);
            this.grpEditProfile.TabIndex = 0;
            this.grpEditProfile.TabStop = false;
            this.grpEditProfile.Text = "EditProfile";
            // 
            // grpGeneralSettings
            // 
            this.grpGeneralSettings.Controls.Add(this.lblPhoneMsg);
            this.grpGeneralSettings.Controls.Add(this.lblEmailValidMsg);
            this.grpGeneralSettings.Controls.Add(this.lblEmailID);
            this.grpGeneralSettings.Controls.Add(this.txtbxEmailID);
            this.grpGeneralSettings.Controls.Add(this.lblPhone);
            this.grpGeneralSettings.Controls.Add(this.txtChangePhone);
            this.grpGeneralSettings.Location = new System.Drawing.Point(6, 190);
            this.grpGeneralSettings.Name = "grpGeneralSettings";
            this.grpGeneralSettings.Size = new System.Drawing.Size(392, 118);
            this.grpGeneralSettings.TabIndex = 29;
            this.grpGeneralSettings.TabStop = false;
            this.grpGeneralSettings.Text = "General Settings";
            // 
            // lblPhoneMsg
            // 
            this.lblPhoneMsg.AutoSize = true;
            this.lblPhoneMsg.ForeColor = System.Drawing.Color.Red;
            this.lblPhoneMsg.Location = new System.Drawing.Point(171, 100);
            this.lblPhoneMsg.Name = "lblPhoneMsg";
            this.lblPhoneMsg.Size = new System.Drawing.Size(0, 13);
            this.lblPhoneMsg.TabIndex = 29;
            this.lblPhoneMsg.Visible = false;
            // 
            // lblEmailValidMsg
            // 
            this.lblEmailValidMsg.AutoSize = true;
            this.lblEmailValidMsg.ForeColor = System.Drawing.Color.Red;
            this.lblEmailValidMsg.Location = new System.Drawing.Point(171, 55);
            this.lblEmailValidMsg.Name = "lblEmailValidMsg";
            this.lblEmailValidMsg.Size = new System.Drawing.Size(0, 13);
            this.lblEmailValidMsg.TabIndex = 28;
            this.lblEmailValidMsg.Visible = false;
            // 
            // lblEmailID
            // 
            this.lblEmailID.AutoSize = true;
            this.lblEmailID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailID.Location = new System.Drawing.Point(6, 31);
            this.lblEmailID.Name = "lblEmailID";
            this.lblEmailID.Size = new System.Drawing.Size(46, 13);
            this.lblEmailID.TabIndex = 26;
            this.lblEmailID.Text = "Email ID";
            // 
            // txtbxEmailID
            // 
            this.txtbxEmailID.Location = new System.Drawing.Point(171, 28);
            this.txtbxEmailID.Name = "txtbxEmailID";
            this.txtbxEmailID.Size = new System.Drawing.Size(208, 20);
            this.txtbxEmailID.TabIndex = 3;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.Location = new System.Drawing.Point(6, 80);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(38, 13);
            this.lblPhone.TabIndex = 13;
            this.lblPhone.Text = "Phone";
            // 
            // txtChangePhone
            // 
            this.txtChangePhone.Location = new System.Drawing.Point(171, 77);
            this.txtChangePhone.Name = "txtChangePhone";
            this.txtChangePhone.Size = new System.Drawing.Size(208, 20);
            this.txtChangePhone.TabIndex = 4;
            // 
            // gpbxPwdSettings
            // 
            this.gpbxPwdSettings.Controls.Add(this.lblOldPwdMsg);
            this.gpbxPwdSettings.Controls.Add(this.lblCnfrmPwdMsg);
            this.gpbxPwdSettings.Controls.Add(this.lblNwPwdMsg);
            this.gpbxPwdSettings.Controls.Add(this.txtbxConfirmNwPwd);
            this.gpbxPwdSettings.Controls.Add(this.txtbxNewPwd);
            this.gpbxPwdSettings.Controls.Add(this.txtOldPwd);
            this.gpbxPwdSettings.Controls.Add(this.lblConfmNwPwd);
            this.gpbxPwdSettings.Controls.Add(this.lblNwPwd);
            this.gpbxPwdSettings.Controls.Add(this.lblOldPwd);
            this.gpbxPwdSettings.Location = new System.Drawing.Point(6, 32);
            this.gpbxPwdSettings.Name = "gpbxPwdSettings";
            this.gpbxPwdSettings.Size = new System.Drawing.Size(392, 152);
            this.gpbxPwdSettings.TabIndex = 28;
            this.gpbxPwdSettings.TabStop = false;
            this.gpbxPwdSettings.Text = "Password Settings";
            // 
            // lblOldPwdMsg
            // 
            this.lblOldPwdMsg.AutoSize = true;
            this.lblOldPwdMsg.ForeColor = System.Drawing.Color.Red;
            this.lblOldPwdMsg.Location = new System.Drawing.Point(171, 57);
            this.lblOldPwdMsg.Name = "lblOldPwdMsg";
            this.lblOldPwdMsg.Size = new System.Drawing.Size(0, 13);
            this.lblOldPwdMsg.TabIndex = 28;
            // 
            // lblCnfrmPwdMsg
            // 
            this.lblCnfrmPwdMsg.AutoSize = true;
            this.lblCnfrmPwdMsg.ForeColor = System.Drawing.Color.Red;
            this.lblCnfrmPwdMsg.Location = new System.Drawing.Point(171, 135);
            this.lblCnfrmPwdMsg.Name = "lblCnfrmPwdMsg";
            this.lblCnfrmPwdMsg.Size = new System.Drawing.Size(0, 13);
            this.lblCnfrmPwdMsg.TabIndex = 27;
            // 
            // lblNwPwdMsg
            // 
            this.lblNwPwdMsg.AutoSize = true;
            this.lblNwPwdMsg.ForeColor = System.Drawing.Color.Red;
            this.lblNwPwdMsg.Location = new System.Drawing.Point(159, 96);
            this.lblNwPwdMsg.Name = "lblNwPwdMsg";
            this.lblNwPwdMsg.Size = new System.Drawing.Size(0, 13);
            this.lblNwPwdMsg.TabIndex = 26;
            // 
            // txtbxConfirmNwPwd
            // 
            this.txtbxConfirmNwPwd.Location = new System.Drawing.Point(171, 112);
            this.txtbxConfirmNwPwd.Name = "txtbxConfirmNwPwd";
            this.txtbxConfirmNwPwd.PasswordChar = '*';
            this.txtbxConfirmNwPwd.Size = new System.Drawing.Size(208, 20);
            this.txtbxConfirmNwPwd.TabIndex = 2;
            // 
            // txtbxNewPwd
            // 
            this.txtbxNewPwd.Location = new System.Drawing.Point(171, 73);
            this.txtbxNewPwd.Name = "txtbxNewPwd";
            this.txtbxNewPwd.PasswordChar = '*';
            this.txtbxNewPwd.Size = new System.Drawing.Size(208, 20);
            this.txtbxNewPwd.TabIndex = 1;
            // 
            // txtOldPwd
            // 
            this.txtOldPwd.Location = new System.Drawing.Point(171, 34);
            this.txtOldPwd.Name = "txtOldPwd";
            this.txtOldPwd.PasswordChar = '*';
            this.txtOldPwd.Size = new System.Drawing.Size(208, 20);
            this.txtOldPwd.TabIndex = 0;
            // 
            // lblConfmNwPwd
            // 
            this.lblConfmNwPwd.AutoSize = true;
            this.lblConfmNwPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfmNwPwd.Location = new System.Drawing.Point(6, 112);
            this.lblConfmNwPwd.Name = "lblConfmNwPwd";
            this.lblConfmNwPwd.Size = new System.Drawing.Size(116, 13);
            this.lblConfmNwPwd.TabIndex = 24;
            this.lblConfmNwPwd.Text = "Confirm New Password";
            // 
            // lblNwPwd
            // 
            this.lblNwPwd.AutoSize = true;
            this.lblNwPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNwPwd.Location = new System.Drawing.Point(6, 73);
            this.lblNwPwd.Name = "lblNwPwd";
            this.lblNwPwd.Size = new System.Drawing.Size(78, 13);
            this.lblNwPwd.TabIndex = 22;
            this.lblNwPwd.Text = "New Password";
            // 
            // lblOldPwd
            // 
            this.lblOldPwd.AutoSize = true;
            this.lblOldPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOldPwd.Location = new System.Drawing.Point(6, 34);
            this.lblOldPwd.Name = "lblOldPwd";
            this.lblOldPwd.Size = new System.Drawing.Size(72, 13);
            this.lblOldPwd.TabIndex = 11;
            this.lblOldPwd.Text = "Old Password";
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(294, 325);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(97, 39);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnEditProfile
            // 
            this.btnEditProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditProfile.Location = new System.Drawing.Point(171, 325);
            this.btnEditProfile.Name = "btnEditProfile";
            this.btnEditProfile.Size = new System.Drawing.Size(117, 39);
            this.btnEditProfile.TabIndex = 5;
            this.btnEditProfile.Text = "Edit Profile";
            this.btnEditProfile.UseVisualStyleBackColor = true;
            this.btnEditProfile.Click += new System.EventHandler(this.btnEditProfile_Click);
            // 
            // EditProfileForm
            // 
            this.AcceptButton = this.btnEditProfile;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 446);
            this.Controls.Add(this.grpEditProfile);
            this.Name = "EditProfileForm";
            this.ShowIcon = false;
            this.Text = "Edit Profile Form";
            this.Load += new System.EventHandler(this.EditProfileForm_Load);
            this.grpEditProfile.ResumeLayout(false);
            this.grpGeneralSettings.ResumeLayout(false);
            this.grpGeneralSettings.PerformLayout();
            this.gpbxPwdSettings.ResumeLayout(false);
            this.gpbxPwdSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpEditProfile;
        private System.Windows.Forms.TextBox txtOldPwd;
        private System.Windows.Forms.Label lblOldPwd;
        private System.Windows.Forms.TextBox txtChangePhone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnEditProfile;
        private System.Windows.Forms.TextBox txtbxNewPwd;
        private System.Windows.Forms.Label lblNwPwd;
        private System.Windows.Forms.TextBox txtbxConfirmNwPwd;
        private System.Windows.Forms.Label lblConfmNwPwd;
        private System.Windows.Forms.TextBox txtbxEmailID;
        private System.Windows.Forms.Label lblEmailID;
        private System.Windows.Forms.GroupBox gpbxPwdSettings;
        private System.Windows.Forms.GroupBox grpGeneralSettings;
        private System.Windows.Forms.Label lblEmailValidMsg;
        private System.Windows.Forms.Label lblCnfrmPwdMsg;
        private System.Windows.Forms.Label lblNwPwdMsg;
        private System.Windows.Forms.Label lblPhoneMsg;
        private System.Windows.Forms.Label lblOldPwdMsg;
    }
}