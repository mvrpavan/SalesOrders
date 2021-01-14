namespace SalesOrdersReport
{
    partial class DefineRoleForm
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
            this.lblSelectRoleValidMsg = new System.Windows.Forms.Label();
            this.lblAddPrivilegeValidmsg = new System.Windows.Forms.Label();
            this.cmbSelectRole = new System.Windows.Forms.ComboBox();
            this.flpChsePrivilege = new System.Windows.Forms.FlowLayoutPanel();
            this.lblRolePrevilege = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnDefineRole = new System.Windows.Forms.Button();
            this.lblNewRoleName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSelectRoleValidMsg);
            this.groupBox1.Controls.Add(this.lblAddPrivilegeValidmsg);
            this.groupBox1.Controls.Add(this.cmbSelectRole);
            this.groupBox1.Controls.Add(this.flpChsePrivilege);
            this.groupBox1.Controls.Add(this.lblRolePrevilege);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.btnDefineRole);
            this.groupBox1.Controls.Add(this.lblNewRoleName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(43, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(772, 429);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Define Role";
            // 
            // lblSelectRoleValidMsg
            // 
            this.lblSelectRoleValidMsg.AutoSize = true;
            this.lblSelectRoleValidMsg.Location = new System.Drawing.Point(190, 71);
            this.lblSelectRoleValidMsg.Name = "lblSelectRoleValidMsg";
            this.lblSelectRoleValidMsg.Size = new System.Drawing.Size(0, 13);
            this.lblSelectRoleValidMsg.TabIndex = 25;
            // 
            // lblAddPrivilegeValidmsg
            // 
            this.lblAddPrivilegeValidmsg.AutoSize = true;
            this.lblAddPrivilegeValidmsg.Location = new System.Drawing.Point(190, 396);
            this.lblAddPrivilegeValidmsg.Name = "lblAddPrivilegeValidmsg";
            this.lblAddPrivilegeValidmsg.Size = new System.Drawing.Size(0, 13);
            this.lblAddPrivilegeValidmsg.TabIndex = 24;
            // 
            // cmbSelectRole
            // 
            this.cmbSelectRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectRole.FormattingEnabled = true;
            this.cmbSelectRole.Location = new System.Drawing.Point(190, 43);
            this.cmbSelectRole.Name = "cmbSelectRole";
            this.cmbSelectRole.Size = new System.Drawing.Size(121, 21);
            this.cmbSelectRole.TabIndex = 0;
            this.cmbSelectRole.SelectedIndexChanged += new System.EventHandler(this.cmbSelectRole_SelectedIndexChanged);
            // 
            // flpChsePrivilege
            // 
            this.flpChsePrivilege.Location = new System.Drawing.Point(68, 115);
            this.flpChsePrivilege.Name = "flpChsePrivilege";
            this.flpChsePrivilege.Size = new System.Drawing.Size(681, 222);
            this.flpChsePrivilege.TabIndex = 1;
            // 
            // lblRolePrevilege
            // 
            this.lblRolePrevilege.AutoSize = true;
            this.lblRolePrevilege.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRolePrevilege.Location = new System.Drawing.Point(65, 91);
            this.lblRolePrevilege.Name = "lblRolePrevilege";
            this.lblRolePrevilege.Size = new System.Drawing.Size(86, 13);
            this.lblRolePrevilege.TabIndex = 20;
            this.lblRolePrevilege.Text = "Choose Privilege";
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(514, 361);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(97, 39);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnDefineRole
            // 
            this.btnDefineRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefineRole.Location = new System.Drawing.Point(391, 361);
            this.btnDefineRole.Name = "btnDefineRole";
            this.btnDefineRole.Size = new System.Drawing.Size(117, 39);
            this.btnDefineRole.TabIndex = 2;
            this.btnDefineRole.Text = "Define Role";
            this.btnDefineRole.UseVisualStyleBackColor = true;
            this.btnDefineRole.Click += new System.EventHandler(this.btnDefineRole_Click);
            // 
            // lblNewRoleName
            // 
            this.lblNewRoleName.AutoSize = true;
            this.lblNewRoleName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewRoleName.Location = new System.Drawing.Point(65, 46);
            this.lblNewRoleName.Name = "lblNewRoleName";
            this.lblNewRoleName.Size = new System.Drawing.Size(63, 13);
            this.lblNewRoleName.TabIndex = 13;
            this.lblNewRoleName.Text = " Role Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(65, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 12;
            // 
            // DefineRoleForm
            // 
            this.AcceptButton = this.btnDefineRole;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 479);
            this.Controls.Add(this.groupBox1);
            this.Name = "DefineRoleForm";
            this.ShowIcon = false;
            this.Text = "Define Role Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DefineRoleForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblNewRoleName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnDefineRole;
        private System.Windows.Forms.Label lblRolePrevilege;
        private System.Windows.Forms.FlowLayoutPanel flpChsePrivilege;
        private System.Windows.Forms.ComboBox cmbSelectRole;
        private System.Windows.Forms.Label lblSelectRoleValidMsg;
        private System.Windows.Forms.Label lblAddPrivilegeValidmsg;
    }
}