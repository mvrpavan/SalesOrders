namespace SalesOrdersReport
{
    partial class CreateRoleForm
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
            this.flpChsePrivilege = new System.Windows.Forms.FlowLayoutPanel();
            this.lblRoleDescAsterik = new System.Windows.Forms.Label();
            this.lblCreateRoleAsterik = new System.Windows.Forms.Label();
            this.lblRolePrevilege = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnCreateRole = new System.Windows.Forms.Button();
            this.lblRoleDesc = new System.Windows.Forms.Label();
            this.txtRoleDesc = new System.Windows.Forms.TextBox();
            this.lblNewRoleName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNewRoleName = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flpChsePrivilege);
            this.groupBox1.Controls.Add(this.lblRoleDescAsterik);
            this.groupBox1.Controls.Add(this.lblCreateRoleAsterik);
            this.groupBox1.Controls.Add(this.lblRolePrevilege);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.btnCreateRole);
            this.groupBox1.Controls.Add(this.lblRoleDesc);
            this.groupBox1.Controls.Add(this.txtRoleDesc);
            this.groupBox1.Controls.Add(this.lblNewRoleName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtNewRoleName);
            this.groupBox1.Location = new System.Drawing.Point(43, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(531, 466);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New Role";
            // 
            // flpChsePrivilege
            // 
            this.flpChsePrivilege.Location = new System.Drawing.Point(190, 192);
            this.flpChsePrivilege.Name = "flpChsePrivilege";
            this.flpChsePrivilege.Size = new System.Drawing.Size(323, 202);
            this.flpChsePrivilege.TabIndex = 27;
            // 
            // lblRoleDescAsterik
            // 
            this.lblRoleDescAsterik.AutoSize = true;
            this.lblRoleDescAsterik.ForeColor = System.Drawing.Color.Red;
            this.lblRoleDescAsterik.Location = new System.Drawing.Point(120, 90);
            this.lblRoleDescAsterik.Name = "lblRoleDescAsterik";
            this.lblRoleDescAsterik.Size = new System.Drawing.Size(11, 13);
            this.lblRoleDescAsterik.TabIndex = 26;
            this.lblRoleDescAsterik.Text = "*";
            // 
            // lblCreateRoleAsterik
            // 
            this.lblCreateRoleAsterik.AutoSize = true;
            this.lblCreateRoleAsterik.ForeColor = System.Drawing.Color.Red;
            this.lblCreateRoleAsterik.Location = new System.Drawing.Point(114, 46);
            this.lblCreateRoleAsterik.Name = "lblCreateRoleAsterik";
            this.lblCreateRoleAsterik.Size = new System.Drawing.Size(11, 13);
            this.lblCreateRoleAsterik.TabIndex = 25;
            this.lblCreateRoleAsterik.Text = "*";
            // 
            // lblRolePrevilege
            // 
            this.lblRolePrevilege.AutoSize = true;
            this.lblRolePrevilege.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRolePrevilege.Location = new System.Drawing.Point(65, 192);
            this.lblRolePrevilege.Name = "lblRolePrevilege";
            this.lblRolePrevilege.Size = new System.Drawing.Size(86, 13);
            this.lblRolePrevilege.TabIndex = 20;
            this.lblRolePrevilege.Text = "Choose Privilege";
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(304, 411);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(97, 39);
            this.btnReset.TabIndex = 19;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnCreateRole
            // 
            this.btnCreateRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateRole.Location = new System.Drawing.Point(181, 411);
            this.btnCreateRole.Name = "btnCreateRole";
            this.btnCreateRole.Size = new System.Drawing.Size(117, 39);
            this.btnCreateRole.TabIndex = 18;
            this.btnCreateRole.Text = "Create Role";
            this.btnCreateRole.UseVisualStyleBackColor = true;
            this.btnCreateRole.Click += new System.EventHandler(this.btnCreateRole_Click);
            // 
            // lblRoleDesc
            // 
            this.lblRoleDesc.AutoSize = true;
            this.lblRoleDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoleDesc.Location = new System.Drawing.Point(65, 90);
            this.lblRoleDesc.Name = "lblRoleDesc";
            this.lblRoleDesc.Size = new System.Drawing.Size(60, 13);
            this.lblRoleDesc.TabIndex = 17;
            this.lblRoleDesc.Text = "Description";
            // 
            // txtRoleDesc
            // 
            this.txtRoleDesc.Location = new System.Drawing.Point(190, 90);
            this.txtRoleDesc.Multiline = true;
            this.txtRoleDesc.Name = "txtRoleDesc";
            this.txtRoleDesc.Size = new System.Drawing.Size(208, 85);
            this.txtRoleDesc.TabIndex = 15;
            // 
            // lblNewRoleName
            // 
            this.lblNewRoleName.AutoSize = true;
            this.lblNewRoleName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewRoleName.Location = new System.Drawing.Point(65, 46);
            this.lblNewRoleName.Name = "lblNewRoleName";
            this.lblNewRoleName.Size = new System.Drawing.Size(54, 13);
            this.lblNewRoleName.TabIndex = 13;
            this.lblNewRoleName.Text = "New Role";
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
            // txtNewRoleName
            // 
            this.txtNewRoleName.Location = new System.Drawing.Point(190, 49);
            this.txtNewRoleName.Name = "txtNewRoleName";
            this.txtNewRoleName.Size = new System.Drawing.Size(208, 20);
            this.txtNewRoleName.TabIndex = 16;
            // 
            // CreateRoleForm
            // 
            this.AcceptButton = this.btnCreateRole;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 510);
            this.Controls.Add(this.groupBox1);
            this.Name = "CreateRoleForm";
            this.ShowIcon = false;
            this.Text = "Create Role Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CreateRoleForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtRoleDesc;
        private System.Windows.Forms.Label lblNewRoleName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNewRoleName;
        private System.Windows.Forms.Label lblRoleDesc;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCreateRole;
        private System.Windows.Forms.Label lblRolePrevilege;
        private System.Windows.Forms.Label lblRoleDescAsterik;
        private System.Windows.Forms.Label lblCreateRoleAsterik;
        private System.Windows.Forms.FlowLayoutPanel flpChsePrivilege;
    }
}