namespace SalesOrdersReport.Views
{
    partial class CreateTaxForm
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
            this.components = new System.ComponentModel.Container();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxHSNCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxCGST = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoxIGST = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxSGST = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtGridViewHSNList = new System.Windows.Forms.DataGridView();
            this.errorProviderAddHSNCodes = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnDelete = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewHSNList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderAddHSNCodes)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(119, 132);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(53, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(222, 132);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(55, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "HSN Code";
            // 
            // txtBoxHSNCode
            // 
            this.txtBoxHSNCode.Location = new System.Drawing.Point(158, 12);
            this.txtBoxHSNCode.Name = "txtBoxHSNCode";
            this.txtBoxHSNCode.Size = new System.Drawing.Size(100, 20);
            this.txtBoxHSNCode.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(116, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "CGST";
            // 
            // txtBoxCGST
            // 
            this.txtBoxCGST.Location = new System.Drawing.Point(158, 38);
            this.txtBoxCGST.Name = "txtBoxCGST";
            this.txtBoxCGST.Size = new System.Drawing.Size(100, 20);
            this.txtBoxCGST.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(116, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "IGST";
            // 
            // txtBoxIGST
            // 
            this.txtBoxIGST.Location = new System.Drawing.Point(158, 90);
            this.txtBoxIGST.Name = "txtBoxIGST";
            this.txtBoxIGST.Size = new System.Drawing.Size(100, 20);
            this.txtBoxIGST.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(116, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "SGST";
            // 
            // txtBoxSGST
            // 
            this.txtBoxSGST.Location = new System.Drawing.Point(158, 64);
            this.txtBoxSGST.Name = "txtBoxSGST";
            this.txtBoxSGST.Size = new System.Drawing.Size(100, 20);
            this.txtBoxSGST.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtBoxSGST);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Controls.Add(this.txtBoxIGST);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.txtBoxCGST);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtBoxHSNCode);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(498, 168);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "HSN Details";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtGridViewHSNList);
            this.groupBox2.Location = new System.Drawing.Point(13, 187);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(497, 172);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "HSN List";
            // 
            // dtGridViewHSNList
            // 
            this.dtGridViewHSNList.AllowUserToAddRows = false;
            this.dtGridViewHSNList.AllowUserToDeleteRows = false;
            this.dtGridViewHSNList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewHSNList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtGridViewHSNList.Location = new System.Drawing.Point(3, 16);
            this.dtGridViewHSNList.Name = "dtGridViewHSNList";
            this.dtGridViewHSNList.ReadOnly = true;
            this.dtGridViewHSNList.Size = new System.Drawing.Size(491, 153);
            this.dtGridViewHSNList.TabIndex = 0;
            this.dtGridViewHSNList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtGridViewHSNList_CellMouseDoubleClick);
            // 
            // errorProviderAddHSNCodes
            // 
            this.errorProviderAddHSNCodes.ContainerControl = this;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.BackgroundImage = global::SalesOrdersReport.Properties.Resources.delete_icon_16;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(277, 10);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(32, 23);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // AddTaxForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(522, 371);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "AddTaxForm";
            this.Text = "AddTaxForm";
            this.Shown += new System.EventHandler(this.AddTaxForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewHSNList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderAddHSNCodes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxHSNCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxCGST;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBoxIGST;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxSGST;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dtGridViewHSNList;
        private System.Windows.Forms.ErrorProvider errorProviderAddHSNCodes;
        private System.Windows.Forms.Button btnDelete;
    }
}