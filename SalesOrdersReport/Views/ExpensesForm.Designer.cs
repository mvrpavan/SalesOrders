namespace SalesOrdersReport.Views
{
    partial class ExpensesForm
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
            this.dtGridViewExpenses = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxApplyFilterExpense = new System.Windows.Forms.CheckBox();
            this.dTimePickerToExpenses = new System.Windows.Forms.DateTimePicker();
            this.dTimePickerFromExpenses = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.btnSearchExpense = new System.Windows.Forms.Button();
            this.btnPrintExpense = new System.Windows.Forms.Button();
            this.btnReloadExpenses = new System.Windows.Forms.Button();
            this.btnDeleteExpense = new System.Windows.Forms.Button();
            this.btnViewEditExpense = new System.Windows.Forms.Button();
            this.btnCreateExpense = new System.Windows.Forms.Button();
            this.groupBoxExpenses = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewExpenses)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxExpenses.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtGridViewExpenses
            // 
            this.dtGridViewExpenses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGridViewExpenses.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtGridViewExpenses.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dtGridViewExpenses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewExpenses.Location = new System.Drawing.Point(0, 126);
            this.dtGridViewExpenses.Name = "dtGridViewExpenses";
            this.dtGridViewExpenses.Size = new System.Drawing.Size(1164, 226);
            this.dtGridViewExpenses.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label8.Location = new System.Drawing.Point(6, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 25);
            this.label8.TabIndex = 4;
            this.label8.Text = "Expenses";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.comboBox3);
            this.panel2.Controls.Add(this.btnSearchExpense);
            this.panel2.Controls.Add(this.btnPrintExpense);
            this.panel2.Controls.Add(this.btnReloadExpenses);
            this.panel2.Controls.Add(this.btnDeleteExpense);
            this.panel2.Controls.Add(this.btnViewEditExpense);
            this.panel2.Controls.Add(this.btnCreateExpense);
            this.panel2.Location = new System.Drawing.Point(0, 44);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1164, 82);
            this.panel2.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.checkBoxApplyFilterExpense);
            this.groupBox2.Controls.Add(this.dTimePickerToExpenses);
            this.groupBox2.Controls.Add(this.dTimePickerFromExpenses);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(862, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(296, 73);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            // 
            // checkBoxApplyFilterExpense
            // 
            this.checkBoxApplyFilterExpense.AutoSize = true;
            this.checkBoxApplyFilterExpense.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.checkBoxApplyFilterExpense.Location = new System.Drawing.Point(212, 28);
            this.checkBoxApplyFilterExpense.Name = "checkBoxApplyFilterExpense";
            this.checkBoxApplyFilterExpense.Size = new System.Drawing.Size(75, 17);
            this.checkBoxApplyFilterExpense.TabIndex = 2;
            this.checkBoxApplyFilterExpense.Text = "Apply Filter";
            this.checkBoxApplyFilterExpense.UseVisualStyleBackColor = true;
            this.checkBoxApplyFilterExpense.CheckedChanged += new System.EventHandler(this.checkBoxApplyFilterExpenses_CheckedChanged);
            // 
            // dTimePickerToExpenses
            // 
            this.dTimePickerToExpenses.Location = new System.Drawing.Point(68, 38);
            this.dTimePickerToExpenses.Name = "dTimePickerToExpenses";
            this.dTimePickerToExpenses.Size = new System.Drawing.Size(134, 20);
            this.dTimePickerToExpenses.TabIndex = 1;
            // 
            // dTimePickerFromExpenses
            // 
            this.dTimePickerFromExpenses.Location = new System.Drawing.Point(68, 10);
            this.dTimePickerFromExpenses.Name = "dTimePickerFromExpenses";
            this.dTimePickerFromExpenses.Size = new System.Drawing.Size(134, 20);
            this.dTimePickerFromExpenses.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Date to:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Date from:";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(803, -24);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(234, 21);
            this.comboBox3.TabIndex = 9;
            // 
            // btnSearchExpense
            // 
            this.btnSearchExpense.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSearchExpense.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnSearchExpense.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSearchExpense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchExpense.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnSearchExpense.Image = global::SalesOrdersReport.Properties.Resources.Search_icon;
            this.btnSearchExpense.Location = new System.Drawing.Point(450, 3);
            this.btnSearchExpense.Name = "btnSearchExpense";
            this.btnSearchExpense.Size = new System.Drawing.Size(83, 73);
            this.btnSearchExpense.TabIndex = 0;
            this.btnSearchExpense.Text = "Search Expense";
            this.btnSearchExpense.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSearchExpense.UseVisualStyleBackColor = false;
            this.btnSearchExpense.Click += new System.EventHandler(this.btnSearchExpense_Click);
            // 
            // btnPrintExpense
            // 
            this.btnPrintExpense.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPrintExpense.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnPrintExpense.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPrintExpense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintExpense.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnPrintExpense.Image = global::SalesOrdersReport.Properties.Resources.Iconshow_Hardware_Printer;
            this.btnPrintExpense.Location = new System.Drawing.Point(361, 3);
            this.btnPrintExpense.Name = "btnPrintExpense";
            this.btnPrintExpense.Size = new System.Drawing.Size(83, 73);
            this.btnPrintExpense.TabIndex = 0;
            this.btnPrintExpense.Text = "Print";
            this.btnPrintExpense.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrintExpense.UseVisualStyleBackColor = false;
            this.btnPrintExpense.Click += new System.EventHandler(this.btnPrintExpense_Click);
            // 
            // btnReloadExpenses
            // 
            this.btnReloadExpenses.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnReloadExpenses.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnReloadExpenses.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnReloadExpenses.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReloadExpenses.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnReloadExpenses.Image = global::SalesOrdersReport.Properties.Resources.Refresh_icon_32;
            this.btnReloadExpenses.Location = new System.Drawing.Point(272, 3);
            this.btnReloadExpenses.Name = "btnReloadExpenses";
            this.btnReloadExpenses.Size = new System.Drawing.Size(83, 73);
            this.btnReloadExpenses.TabIndex = 0;
            this.btnReloadExpenses.Text = "Reload";
            this.btnReloadExpenses.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReloadExpenses.UseVisualStyleBackColor = false;
            this.btnReloadExpenses.Click += new System.EventHandler(this.btnReloadExpenses_Click);
            // 
            // btnDeleteExpense
            // 
            this.btnDeleteExpense.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDeleteExpense.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnDeleteExpense.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnDeleteExpense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteExpense.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnDeleteExpense.Image = global::SalesOrdersReport.Properties.Resources.delete_icon_32;
            this.btnDeleteExpense.Location = new System.Drawing.Point(183, 3);
            this.btnDeleteExpense.Name = "btnDeleteExpense";
            this.btnDeleteExpense.Size = new System.Drawing.Size(83, 73);
            this.btnDeleteExpense.TabIndex = 0;
            this.btnDeleteExpense.Text = "Delete Expense";
            this.btnDeleteExpense.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteExpense.UseVisualStyleBackColor = false;
            this.btnDeleteExpense.Click += new System.EventHandler(this.btnDeleteExpense_Click);
            // 
            // btnViewEditExpense
            // 
            this.btnViewEditExpense.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnViewEditExpense.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnViewEditExpense.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnViewEditExpense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewEditExpense.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnViewEditExpense.Image = global::SalesOrdersReport.Properties.Resources.edit_icon_32;
            this.btnViewEditExpense.Location = new System.Drawing.Point(94, 3);
            this.btnViewEditExpense.Name = "btnViewEditExpense";
            this.btnViewEditExpense.Size = new System.Drawing.Size(83, 73);
            this.btnViewEditExpense.TabIndex = 0;
            this.btnViewEditExpense.Text = "View/Edit Expense";
            this.btnViewEditExpense.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnViewEditExpense.UseVisualStyleBackColor = false;
            this.btnViewEditExpense.Click += new System.EventHandler(this.btnViewEditExpense_Click);
            // 
            // btnCreateExpense
            // 
            this.btnCreateExpense.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCreateExpense.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnCreateExpense.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCreateExpense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateExpense.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateExpense.Image = global::SalesOrdersReport.Properties.Resources.math_add_icon_32;
            this.btnCreateExpense.Location = new System.Drawing.Point(5, 3);
            this.btnCreateExpense.Name = "btnCreateExpense";
            this.btnCreateExpense.Size = new System.Drawing.Size(83, 73);
            this.btnCreateExpense.TabIndex = 0;
            this.btnCreateExpense.Text = "Create Expense";
            this.btnCreateExpense.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCreateExpense.UseVisualStyleBackColor = false;
            this.btnCreateExpense.Click += new System.EventHandler(this.btnCreateExpense_Click);
            // 
            // groupBoxExpenses
            // 
            this.groupBoxExpenses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxExpenses.Controls.Add(this.dtGridViewExpenses);
            this.groupBoxExpenses.Controls.Add(this.label8);
            this.groupBoxExpenses.Controls.Add(this.panel2);
            this.groupBoxExpenses.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.groupBoxExpenses.Location = new System.Drawing.Point(8, 24);
            this.groupBoxExpenses.Name = "groupBoxExpenses";
            this.groupBoxExpenses.Size = new System.Drawing.Size(1164, 358);
            this.groupBoxExpenses.TabIndex = 5;
            this.groupBoxExpenses.TabStop = false;
            // 
            // ExpensesForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1184, 743);
            this.Controls.Add(this.groupBoxExpenses);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExpensesForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Expenses";
            this.Shown += new System.EventHandler(this.ExpensesMainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewExpenses)).EndInit();
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxExpenses.ResumeLayout(false);
            this.groupBoxExpenses.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dtGridViewExpenses;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxApplyFilterExpense;
        private System.Windows.Forms.DateTimePicker dTimePickerToExpenses;
        private System.Windows.Forms.DateTimePicker dTimePickerFromExpenses;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Button btnPrintExpense;
        private System.Windows.Forms.Button btnReloadExpenses;
        private System.Windows.Forms.Button btnDeleteExpense;
        private System.Windows.Forms.Button btnViewEditExpense;
        private System.Windows.Forms.Button btnCreateExpense;
        private System.Windows.Forms.Button btnSearchExpense;
        private System.Windows.Forms.GroupBox groupBoxExpenses;
    }
}