using System.Windows.Forms;

namespace SalesOrdersReport.Views
{
    partial class VendorsMainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnRedirectCreateCustomer = new System.Windows.Forms.Button();
            this.panelAllCustomerBtnTab = new System.Windows.Forms.Panel();
            this.btnImportFromExcel = new System.Windows.Forms.Button();
            this.btnReloadCustomerCache = new System.Windows.Forms.Button();
            this.btnRedirectDeleteCustomer = new System.Windows.Forms.Button();
            this.btnRedirectEditCustomer = new System.Windows.Forms.Button();
            this.dgvCustomerCache = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panelAllCustomerBtnTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerCache)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRedirectCreateCustomer
            // 
            this.btnRedirectCreateCustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRedirectCreateCustomer.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedirectCreateCustomer.Image = global::SalesOrdersReport.Properties.Resources.CreateUser_321;
            this.btnRedirectCreateCustomer.Location = new System.Drawing.Point(3, 3);
            this.btnRedirectCreateCustomer.Name = "btnRedirectCreateCustomer";
            this.btnRedirectCreateCustomer.Size = new System.Drawing.Size(75, 75);
            this.btnRedirectCreateCustomer.TabIndex = 0;
            this.btnRedirectCreateCustomer.Text = "Add Vendor";
            this.btnRedirectCreateCustomer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRedirectCreateCustomer.UseVisualStyleBackColor = false;
            this.btnRedirectCreateCustomer.Click += new System.EventHandler(this.btnRedirectCreateCustomer_Click);
            // 
            // panelAllCustomerBtnTab
            // 
            this.panelAllCustomerBtnTab.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAllCustomerBtnTab.Controls.Add(this.btnImportFromExcel);
            this.panelAllCustomerBtnTab.Controls.Add(this.button1);
            this.panelAllCustomerBtnTab.Controls.Add(this.btnReloadCustomerCache);
            this.panelAllCustomerBtnTab.Controls.Add(this.btnRedirectDeleteCustomer);
            this.panelAllCustomerBtnTab.Controls.Add(this.btnRedirectEditCustomer);
            this.panelAllCustomerBtnTab.Controls.Add(this.btnRedirectCreateCustomer);
            this.panelAllCustomerBtnTab.Location = new System.Drawing.Point(12, 12);
            this.panelAllCustomerBtnTab.Name = "panelAllCustomerBtnTab";
            this.panelAllCustomerBtnTab.Size = new System.Drawing.Size(1164, 82);
            this.panelAllCustomerBtnTab.TabIndex = 1;
            // 
            // btnImportFromExcel
            // 
            this.btnImportFromExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnImportFromExcel.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportFromExcel.Image = global::SalesOrdersReport.Properties.Resources.UploadFile2_32;
            this.btnImportFromExcel.Location = new System.Drawing.Point(246, 3);
            this.btnImportFromExcel.Name = "btnImportFromExcel";
            this.btnImportFromExcel.Size = new System.Drawing.Size(75, 75);
            this.btnImportFromExcel.TabIndex = 7;
            this.btnImportFromExcel.Text = "Import From File";
            this.btnImportFromExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImportFromExcel.UseVisualStyleBackColor = false;
            this.btnImportFromExcel.Click += new System.EventHandler(this.btnImportFromExcel_Click);
            // 
            // btnReloadCustomerCache
            // 
            this.btnReloadCustomerCache.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnReloadCustomerCache.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReloadCustomerCache.Image = global::SalesOrdersReport.Properties.Resources.Refresh_icon_32;
            this.btnReloadCustomerCache.Location = new System.Drawing.Point(327, 3);
            this.btnReloadCustomerCache.Name = "btnReloadCustomerCache";
            this.btnReloadCustomerCache.Size = new System.Drawing.Size(75, 75);
            this.btnReloadCustomerCache.TabIndex = 0;
            this.btnReloadCustomerCache.Text = "Reload Vendors";
            this.btnReloadCustomerCache.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReloadCustomerCache.UseVisualStyleBackColor = false;
            this.btnReloadCustomerCache.Click += new System.EventHandler(this.btnReloadCustomerCache_Click);
            // 
            // btnRedirectDeleteCustomer
            // 
            this.btnRedirectDeleteCustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRedirectDeleteCustomer.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedirectDeleteCustomer.Image = global::SalesOrdersReport.Properties.Resources.delete_icon_32;
            this.btnRedirectDeleteCustomer.Location = new System.Drawing.Point(165, 3);
            this.btnRedirectDeleteCustomer.Name = "btnRedirectDeleteCustomer";
            this.btnRedirectDeleteCustomer.Size = new System.Drawing.Size(75, 75);
            this.btnRedirectDeleteCustomer.TabIndex = 0;
            this.btnRedirectDeleteCustomer.Text = "Delete Vendor";
            this.btnRedirectDeleteCustomer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRedirectDeleteCustomer.UseVisualStyleBackColor = false;
            this.btnRedirectDeleteCustomer.Click += new System.EventHandler(this.btnRedirectDeleteCustomer_Click);
            // 
            // btnRedirectEditCustomer
            // 
            this.btnRedirectEditCustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRedirectEditCustomer.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedirectEditCustomer.Image = global::SalesOrdersReport.Properties.Resources.edituser3_32;
            this.btnRedirectEditCustomer.Location = new System.Drawing.Point(84, 3);
            this.btnRedirectEditCustomer.Name = "btnRedirectEditCustomer";
            this.btnRedirectEditCustomer.Size = new System.Drawing.Size(75, 75);
            this.btnRedirectEditCustomer.TabIndex = 0;
            this.btnRedirectEditCustomer.Text = "Edit Vendor";
            this.btnRedirectEditCustomer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRedirectEditCustomer.UseVisualStyleBackColor = false;
            this.btnRedirectEditCustomer.Click += new System.EventHandler(this.btnRedirectEditCustomer_Click);
            // 
            // dgvCustomerCache
            // 
            this.dgvCustomerCache.AllowUserToAddRows = false;
            this.dgvCustomerCache.AllowUserToDeleteRows = false;
            this.dgvCustomerCache.AllowUserToResizeColumns = false;
            this.dgvCustomerCache.AllowUserToResizeRows = false;
            this.dgvCustomerCache.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCustomerCache.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCustomerCache.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvCustomerCache.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomerCache.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvCustomerCache.Location = new System.Drawing.Point(7, 138);
            this.dgvCustomerCache.MultiSelect = false;
            this.dgvCustomerCache.Name = "dgvCustomerCache";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCustomerCache.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvCustomerCache.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvCustomerCache.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustomerCache.Size = new System.Drawing.Size(1164, 435);
            this.dgvCustomerCache.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(7, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Vendors";
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::SalesOrdersReport.Properties.Resources.Search_icon;
            this.button1.Location = new System.Drawing.Point(408, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 75);
            this.button1.TabIndex = 0;
            this.button1.Text = "Search Vendor";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnReloadCustomerCache_Click);
            // 
            // VendorsMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1188, 747);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvCustomerCache);
            this.Controls.Add(this.panelAllCustomerBtnTab);
            this.Name = "VendorsMainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vendors";
            this.Load += new System.EventHandler(this.ManageCustomer_Load);
            this.Shown += new System.EventHandler(this.ManageCustomerForm_Shown);
            this.panelAllCustomerBtnTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerCache)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRedirectCreateCustomer;
        private System.Windows.Forms.Panel panelAllCustomerBtnTab;
        private System.Windows.Forms.Button btnRedirectEditCustomer;
        //private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnRedirectDeleteCustomer;
        private System.Windows.Forms.Button btnReloadCustomerCache;
        private System.Windows.Forms.DataGridView dgvCustomerCache;
        private Button btnImportFromExcel;
        private Label label1;
        private Button button1;
    }
}