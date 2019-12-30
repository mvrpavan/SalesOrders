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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chckDeleteUser = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.txtEditPwd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbxAllRoles = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbxAllUserForEdit = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chckDeleteUser);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.btnCreate);
            this.groupBox1.Controls.Add(this.txtEditPwd);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbxAllRoles);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbxAllUserForEdit);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(532, 412);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Edit User";
            // 
            // chckDeleteUser
            // 
            this.chckDeleteUser.AutoSize = true;
            this.chckDeleteUser.Location = new System.Drawing.Point(279, 87);
            this.chckDeleteUser.Name = "chckDeleteUser";
            this.chckDeleteUser.Size = new System.Drawing.Size(109, 25);
            this.chckDeleteUser.TabIndex = 13;
            this.chckDeleteUser.Text = "Delete User";
            this.chckDeleteUser.UseVisualStyleBackColor = true;
            this.chckDeleteUser.CheckedChanged += new System.EventHandler(this.chckDeleteUser_CheckedChanged);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(279, 357);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(97, 39);
            this.btnReset.TabIndex = 12;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(159, 357);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(97, 39);
            this.btnCreate.TabIndex = 12;
            this.btnCreate.Text = "Edit User";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // txtEditPwd
            // 
            this.txtEditPwd.Location = new System.Drawing.Point(109, 130);
            this.txtEditPwd.Name = "txtEditPwd";
            this.txtEditPwd.Size = new System.Drawing.Size(208, 29);
            this.txtEditPwd.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 21);
            this.label3.TabIndex = 9;
            this.label3.Text = "Password";
            // 
            // cmbxAllRoles
            // 
            this.cmbxAllRoles.FormattingEnabled = true;
            this.cmbxAllRoles.Location = new System.Drawing.Point(109, 181);
            this.cmbxAllRoles.Name = "cmbxAllRoles";
            this.cmbxAllRoles.Size = new System.Drawing.Size(208, 29);
            this.cmbxAllRoles.TabIndex = 8;
            this.cmbxAllRoles.Text = "Select Role";
   
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 21);
            this.label2.TabIndex = 7;
            this.label2.Text = "RoleID";
            // 
            // cmbxAllUserForEdit
            // 
            this.cmbxAllUserForEdit.FormattingEnabled = true;
            this.cmbxAllUserForEdit.Location = new System.Drawing.Point(109, 38);
            this.cmbxAllUserForEdit.Name = "cmbxAllUserForEdit";
            this.cmbxAllUserForEdit.Size = new System.Drawing.Size(208, 29);
            this.cmbxAllUserForEdit.TabIndex = 6;
            this.cmbxAllUserForEdit.Text = "Select User";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "Users";
            // 
            // EditUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 448);
            this.Controls.Add(this.groupBox1);
            this.Name = "EditUserForm";
            this.ShowIcon = false;
            this.Text = "EditUser";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbxAllRoles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbxAllUserForEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEditPwd;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.CheckBox chckDeleteUser;
    }
}