namespace SalesOrdersReport
{
    partial class CreateLineForm
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
            this.lblLineDescAsterik = new System.Windows.Forms.Label();
            this.lblCreateLineAsterik = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnCreateLine = new System.Windows.Forms.Button();
            this.lblLineDesc = new System.Windows.Forms.Label();
            this.txtLineDesc = new System.Windows.Forms.TextBox();
            this.lblNewLineName = new System.Windows.Forms.Label();
            this.txtNewLineName = new System.Windows.Forms.TextBox();
            this.lblCreateLineValidateErrMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLineDescAsterik
            // 
            this.lblLineDescAsterik.AutoSize = true;
            this.lblLineDescAsterik.ForeColor = System.Drawing.Color.Red;
            this.lblLineDescAsterik.Location = new System.Drawing.Point(71, 66);
            this.lblLineDescAsterik.Name = "lblLineDescAsterik";
            this.lblLineDescAsterik.Size = new System.Drawing.Size(11, 13);
            this.lblLineDescAsterik.TabIndex = 34;
            this.lblLineDescAsterik.Text = "*";
            // 
            // lblCreateLineAsterik
            // 
            this.lblCreateLineAsterik.AutoSize = true;
            this.lblCreateLineAsterik.ForeColor = System.Drawing.Color.Red;
            this.lblCreateLineAsterik.Location = new System.Drawing.Point(69, 22);
            this.lblCreateLineAsterik.Name = "lblCreateLineAsterik";
            this.lblCreateLineAsterik.Size = new System.Drawing.Size(11, 13);
            this.lblCreateLineAsterik.TabIndex = 33;
            this.lblCreateLineAsterik.Text = "*";
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(225, 177);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(97, 39);
            this.btnReset.TabIndex = 32;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnCreateLine
            // 
            this.btnCreateLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateLine.Location = new System.Drawing.Point(102, 177);
            this.btnCreateLine.Name = "btnCreateLine";
            this.btnCreateLine.Size = new System.Drawing.Size(117, 39);
            this.btnCreateLine.TabIndex = 31;
            this.btnCreateLine.Text = "Create Line";
            this.btnCreateLine.UseVisualStyleBackColor = true;
            this.btnCreateLine.Click += new System.EventHandler(this.btnCreateLine_Click);
            // 
            // lblLineDesc
            // 
            this.lblLineDesc.AutoSize = true;
            this.lblLineDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLineDesc.Location = new System.Drawing.Point(16, 66);
            this.lblLineDesc.Name = "lblLineDesc";
            this.lblLineDesc.Size = new System.Drawing.Size(60, 13);
            this.lblLineDesc.TabIndex = 30;
            this.lblLineDesc.Text = "Description";
            // 
            // txtLineDesc
            // 
            this.txtLineDesc.Location = new System.Drawing.Point(102, 63);
            this.txtLineDesc.Multiline = true;
            this.txtLineDesc.Name = "txtLineDesc";
            this.txtLineDesc.Size = new System.Drawing.Size(208, 85);
            this.txtLineDesc.TabIndex = 28;
            // 
            // lblNewLineName
            // 
            this.lblNewLineName.AutoSize = true;
            this.lblNewLineName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewLineName.Location = new System.Drawing.Point(16, 22);
            this.lblNewLineName.Name = "lblNewLineName";
            this.lblNewLineName.Size = new System.Drawing.Size(58, 13);
            this.lblNewLineName.TabIndex = 27;
            this.lblNewLineName.Text = "Line Name";
            // 
            // txtNewLineName
            // 
            this.txtNewLineName.Location = new System.Drawing.Point(102, 22);
            this.txtNewLineName.Name = "txtNewLineName";
            this.txtNewLineName.Size = new System.Drawing.Size(208, 20);
            this.txtNewLineName.TabIndex = 29;
            // 
            // lblCreateLineValidateErrMsg
            // 
            this.lblCreateLineValidateErrMsg.AutoSize = true;
            this.lblCreateLineValidateErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lblCreateLineValidateErrMsg.Location = new System.Drawing.Point(102, 158);
            this.lblCreateLineValidateErrMsg.Name = "lblCreateLineValidateErrMsg";
            this.lblCreateLineValidateErrMsg.Size = new System.Drawing.Size(0, 13);
            this.lblCreateLineValidateErrMsg.TabIndex = 35;
            // 
            // CreateLineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 229);
            this.Controls.Add(this.lblCreateLineValidateErrMsg);
            this.Controls.Add(this.lblLineDescAsterik);
            this.Controls.Add(this.lblCreateLineAsterik);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnCreateLine);
            this.Controls.Add(this.lblLineDesc);
            this.Controls.Add(this.txtLineDesc);
            this.Controls.Add(this.lblNewLineName);
            this.Controls.Add(this.txtNewLineName);
            this.Name = "CreateLineForm";
            this.ShowIcon = false;
            this.Text = "Create Line Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CreateLineForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLineDescAsterik;
        private System.Windows.Forms.Label lblCreateLineAsterik;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCreateLine;
        private System.Windows.Forms.Label lblLineDesc;
        private System.Windows.Forms.TextBox txtLineDesc;
        private System.Windows.Forms.Label lblNewLineName;
        private System.Windows.Forms.TextBox txtNewLineName;
        private System.Windows.Forms.Label lblCreateLineValidateErrMsg;
    }
}