namespace SalesOrdersReport
{
    partial class DiscountForm
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.numUpDownDiscPerc = new System.Windows.Forms.NumericUpDown();
            this.numUpDownDiscValue = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radBtnDiscVal = new System.Windows.Forms.RadioButton();
            this.radBtnDiscPerc = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownDiscPerc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownDiscValue)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.ForestGreen;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOk.Location = new System.Drawing.Point(42, 93);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 33);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.OrangeRed;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCancel.Location = new System.Drawing.Point(159, 93);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 33);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(237, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "%";
            // 
            // numUpDownDiscPerc
            // 
            this.numUpDownDiscPerc.Location = new System.Drawing.Point(163, 17);
            this.numUpDownDiscPerc.Name = "numUpDownDiscPerc";
            this.numUpDownDiscPerc.Size = new System.Drawing.Size(72, 20);
            this.numUpDownDiscPerc.TabIndex = 5;
            // 
            // numUpDownDiscValue
            // 
            this.numUpDownDiscValue.Location = new System.Drawing.Point(163, 43);
            this.numUpDownDiscValue.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numUpDownDiscValue.Name = "numUpDownDiscValue";
            this.numUpDownDiscValue.Size = new System.Drawing.Size(72, 20);
            this.numUpDownDiscValue.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radBtnDiscVal);
            this.groupBox1.Controls.Add(this.radBtnDiscPerc);
            this.groupBox1.Controls.Add(this.numUpDownDiscValue);
            this.groupBox1.Controls.Add(this.numUpDownDiscPerc);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 75);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // radBtnDiscVal
            // 
            this.radBtnDiscVal.AutoSize = true;
            this.radBtnDiscVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radBtnDiscVal.Location = new System.Drawing.Point(6, 40);
            this.radBtnDiscVal.Name = "radBtnDiscVal";
            this.radBtnDiscVal.Size = new System.Drawing.Size(116, 20);
            this.radBtnDiscVal.TabIndex = 6;
            this.radBtnDiscVal.TabStop = true;
            this.radBtnDiscVal.Text = "Discount Value";
            this.radBtnDiscVal.UseVisualStyleBackColor = true;
            this.radBtnDiscVal.CheckedChanged += new System.EventHandler(this.radBtnDiscVal_CheckedChanged);
            // 
            // radBtnDiscPerc
            // 
            this.radBtnDiscPerc.AutoSize = true;
            this.radBtnDiscPerc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radBtnDiscPerc.Location = new System.Drawing.Point(6, 17);
            this.radBtnDiscPerc.Name = "radBtnDiscPerc";
            this.radBtnDiscPerc.Size = new System.Drawing.Size(151, 20);
            this.radBtnDiscPerc.TabIndex = 6;
            this.radBtnDiscPerc.TabStop = true;
            this.radBtnDiscPerc.Text = "Discount Percentage";
            this.radBtnDiscPerc.UseVisualStyleBackColor = true;
            this.radBtnDiscPerc.CheckedChanged += new System.EventHandler(this.radBtnDiscPerc_CheckedChanged);
            // 
            // DiscountForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(288, 137);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DiscountForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Enter Discount Details";
            this.Shown += new System.EventHandler(this.DiscountForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownDiscPerc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownDiscValue)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numUpDownDiscPerc;
        private System.Windows.Forms.NumericUpDown numUpDownDiscValue;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radBtnDiscVal;
        private System.Windows.Forms.RadioButton radBtnDiscPerc;
    }
}