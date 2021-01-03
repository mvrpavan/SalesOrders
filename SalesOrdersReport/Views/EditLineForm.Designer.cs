namespace SalesOrdersReport
{
    partial class EditLineForm
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
            this.btnEditLine = new System.Windows.Forms.Button();
            this.lblEditLineDesc = new System.Windows.Forms.Label();
            this.txtEditLineDesc = new System.Windows.Forms.TextBox();
            this.lblEditLineName = new System.Windows.Forms.Label();
            this.lblValidErrMsg = new System.Windows.Forms.Label();
            this.cmbxSelectLine = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(199, 165);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(85, 39);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnEditLine
            // 
            this.btnEditLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditLine.Location = new System.Drawing.Point(96, 165);
            this.btnEditLine.Name = "btnEditLine";
            this.btnEditLine.Size = new System.Drawing.Size(85, 39);
            this.btnEditLine.TabIndex = 2;
            this.btnEditLine.Text = "Edit Line";
            this.btnEditLine.UseVisualStyleBackColor = true;
            this.btnEditLine.Click += new System.EventHandler(this.btnEditLine_Click);
            // 
            // lblEditLineDesc
            // 
            this.lblEditLineDesc.AutoSize = true;
            this.lblEditLineDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditLineDesc.Location = new System.Drawing.Point(12, 52);
            this.lblEditLineDesc.Name = "lblEditLineDesc";
            this.lblEditLineDesc.Size = new System.Drawing.Size(60, 13);
            this.lblEditLineDesc.TabIndex = 23;
            this.lblEditLineDesc.Text = "Description";
            // 
            // txtEditLineDesc
            // 
            this.txtEditLineDesc.Location = new System.Drawing.Point(96, 52);
            this.txtEditLineDesc.Multiline = true;
            this.txtEditLineDesc.Name = "txtEditLineDesc";
            this.txtEditLineDesc.Size = new System.Drawing.Size(208, 85);
            this.txtEditLineDesc.TabIndex = 1;
            // 
            // lblEditLineName
            // 
            this.lblEditLineName.AutoSize = true;
            this.lblEditLineName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEditLineName.Location = new System.Drawing.Point(12, 22);
            this.lblEditLineName.Name = "lblEditLineName";
            this.lblEditLineName.Size = new System.Drawing.Size(58, 13);
            this.lblEditLineName.TabIndex = 20;
            this.lblEditLineName.Text = "Line Name";
            // 
            // lblValidErrMsg
            // 
            this.lblValidErrMsg.AutoSize = true;
            this.lblValidErrMsg.ForeColor = System.Drawing.Color.Red;
            this.lblValidErrMsg.Location = new System.Drawing.Point(93, 140);
            this.lblValidErrMsg.Name = "lblValidErrMsg";
            this.lblValidErrMsg.Size = new System.Drawing.Size(0, 13);
            this.lblValidErrMsg.TabIndex = 26;
            this.lblValidErrMsg.Visible = false;
            // 
            // cmbxSelectLine
            // 
            this.cmbxSelectLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxSelectLine.FormattingEnabled = true;
            this.cmbxSelectLine.Location = new System.Drawing.Point(96, 22);
            this.cmbxSelectLine.Name = "cmbxSelectLine";
            this.cmbxSelectLine.Size = new System.Drawing.Size(153, 21);
            this.cmbxSelectLine.TabIndex = 0;
            this.cmbxSelectLine.SelectedIndexChanged += new System.EventHandler(this.cmbxSelectLine_SelectedIndexChanged);
            // 
            // EditLineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 215);
            this.Controls.Add(this.cmbxSelectLine);
            this.Controls.Add(this.lblValidErrMsg);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnEditLine);
            this.Controls.Add(this.lblEditLineDesc);
            this.Controls.Add(this.txtEditLineDesc);
            this.Controls.Add(this.lblEditLineName);
            this.Name = "EditLineForm";
            this.ShowIcon = false;
            this.Text = "Edit Line Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditLineForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnEditLine;
        private System.Windows.Forms.Label lblEditLineDesc;
        private System.Windows.Forms.TextBox txtEditLineDesc;
        private System.Windows.Forms.Label lblEditLineName;
        private System.Windows.Forms.Label lblValidErrMsg;
        private System.Windows.Forms.ComboBox cmbxSelectLine;
    }
}