namespace SalesOrdersReport
{
    partial class GetDBConnectionConfigForm
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
            this.btnReset = new System.Windows.Forms.Button();
            this.btnCreateDBConnection = new System.Windows.Forms.Button();
            this.txtDBPwd = new System.Windows.Forms.TextBox();
            this.lblDBPwd = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblDBUsrName = new System.Windows.Forms.Label();
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.lblDBName = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.btnCreateDBConnection);
            this.groupBox1.Controls.Add(this.txtDBPwd);
            this.groupBox1.Controls.Add(this.lblDBPwd);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.lblDBUsrName);
            this.groupBox1.Controls.Add(this.txtDatabaseName);
            this.groupBox1.Controls.Add(this.lblDBName);
            this.groupBox1.Controls.Add(this.lblServer);
            this.groupBox1.Controls.Add(this.txtServerName);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 283);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DB Config";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(267, 215);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(66, 39);
            this.btnReset.TabIndex = 19;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnCreateDBConnection
            // 
            this.btnCreateDBConnection.Location = new System.Drawing.Point(144, 215);
            this.btnCreateDBConnection.Name = "btnCreateDBConnection";
            this.btnCreateDBConnection.Size = new System.Drawing.Size(117, 39);
            this.btnCreateDBConnection.TabIndex = 18;
            this.btnCreateDBConnection.Text = "CreateDBConnection";
            this.btnCreateDBConnection.UseVisualStyleBackColor = true;
            this.btnCreateDBConnection.Click += new System.EventHandler(this.btnCreateDBConnection_Click);
            // 
            // txtDBPwd
            // 
            this.txtDBPwd.Location = new System.Drawing.Point(144, 177);
            this.txtDBPwd.Name = "txtDBPwd";
            this.txtDBPwd.Size = new System.Drawing.Size(189, 20);
            this.txtDBPwd.TabIndex = 17;
            // 
            // lblDBPwd
            // 
            this.lblDBPwd.AutoSize = true;
            this.lblDBPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBPwd.Location = new System.Drawing.Point(18, 174);
            this.lblDBPwd.Name = "lblDBPwd";
            this.lblDBPwd.Size = new System.Drawing.Size(53, 13);
            this.lblDBPwd.TabIndex = 16;
            this.lblDBPwd.Text = "Password";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(144, 125);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(189, 20);
            this.txtUserName.TabIndex = 15;
            // 
            // lblDBUsrName
            // 
            this.lblDBUsrName.AutoSize = true;
            this.lblDBUsrName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBUsrName.Location = new System.Drawing.Point(18, 122);
            this.lblDBUsrName.Name = "lblDBUsrName";
            this.lblDBUsrName.Size = new System.Drawing.Size(57, 13);
            this.lblDBUsrName.TabIndex = 14;
            this.lblDBUsrName.Text = "UserName";
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Location = new System.Drawing.Point(144, 76);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(189, 20);
            this.txtDatabaseName.TabIndex = 13;
            // 
            // lblDBName
            // 
            this.lblDBName.AutoSize = true;
            this.lblDBName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBName.Location = new System.Drawing.Point(18, 73);
            this.lblDBName.Name = "lblDBName";
            this.lblDBName.Size = new System.Drawing.Size(84, 13);
            this.lblDBName.TabIndex = 12;
            this.lblDBName.Text = "Database Name";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServer.Location = new System.Drawing.Point(18, 27);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(69, 13);
            this.lblServer.TabIndex = 10;
            this.lblServer.Text = "Server Name";
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(144, 27);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(189, 20);
            this.txtServerName.TabIndex = 11;
            // 
            // GetDBConnectionConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 315);
            this.Controls.Add(this.groupBox1);
            this.Name = "GetDBConnectionConfigForm";
            this.ShowIcon = false;
            this.Text = "DB Config Details Form";

            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblDBUsrName;
        private System.Windows.Forms.TextBox txtDatabaseName;
        private System.Windows.Forms.Label lblDBName;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.TextBox txtDBPwd;
        private System.Windows.Forms.Label lblDBPwd;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCreateDBConnection;
    }
}