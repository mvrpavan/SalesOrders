using System.Windows.Forms;

namespace SalesOrdersReport.Views
{
    partial class ManageVendorForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnRedirectCreateVendor = new System.Windows.Forms.Button();
            this.panelAllVendorBtnTab = new System.Windows.Forms.Panel();
            this.btnSearchVendor = new System.Windows.Forms.Button();
            this.btnImportFromExcel = new System.Windows.Forms.Button();
            this.btnReloadVendorCache = new System.Windows.Forms.Button();
            this.btnRedirectDeleteVendor = new System.Windows.Forms.Button();
            this.btnRedirectEditVendor = new System.Windows.Forms.Button();
            this.dgvVendorCache = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.panelAllVendorBtnTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVendorCache)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRedirectCreateVendor
            // 
            this.btnRedirectCreateVendor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRedirectCreateVendor.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedirectCreateVendor.Image = global::SalesOrdersReport.Properties.Resources.CreateUser_321;
            this.btnRedirectCreateVendor.Location = new System.Drawing.Point(11, 3);
            this.btnRedirectCreateVendor.Name = "btnRedirectCreateVendor";
            this.btnRedirectCreateVendor.Size = new System.Drawing.Size(80, 80);
            this.btnRedirectCreateVendor.TabIndex = 0;
            this.btnRedirectCreateVendor.Text = "Create Vendor";
            this.btnRedirectCreateVendor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRedirectCreateVendor.UseVisualStyleBackColor = false;
            this.btnRedirectCreateVendor.Click += new System.EventHandler(this.btnRedirectCreateVendor_Click);
            // 
            // panelAllVendorBtnTab
            // 
            this.panelAllVendorBtnTab.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAllVendorBtnTab.Controls.Add(this.btnSearchVendor);
            this.panelAllVendorBtnTab.Controls.Add(this.btnImportFromExcel);
            this.panelAllVendorBtnTab.Controls.Add(this.btnReloadVendorCache);
            this.panelAllVendorBtnTab.Controls.Add(this.btnRedirectDeleteVendor);
            this.panelAllVendorBtnTab.Controls.Add(this.btnRedirectEditVendor);
            this.panelAllVendorBtnTab.Controls.Add(this.btnRedirectCreateVendor);
            this.panelAllVendorBtnTab.Location = new System.Drawing.Point(12, 12);
            this.panelAllVendorBtnTab.Name = "panelAllVendorBtnTab";
            this.panelAllVendorBtnTab.Size = new System.Drawing.Size(1164, 93);
            this.panelAllVendorBtnTab.TabIndex = 1;
            // 
            // btnSearchVendor
            // 
            this.btnSearchVendor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSearchVendor.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchVendor.Image = global::SalesOrdersReport.Properties.Resources.Search_icon;
            this.btnSearchVendor.Location = new System.Drawing.Point(406, 3);
            this.btnSearchVendor.Name = "btnSearchVendor";
            this.btnSearchVendor.Size = new System.Drawing.Size(80, 80);
            this.btnSearchVendor.TabIndex = 9;
            this.btnSearchVendor.Text = "Search Vendor";
            this.btnSearchVendor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSearchVendor.UseVisualStyleBackColor = false;
            this.btnSearchVendor.Click += new System.EventHandler(this.btnSearchVendor_Click);
            // 
            // btnImportFromExcel
            // 
            this.btnImportFromExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnImportFromExcel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportFromExcel.Image = global::SalesOrdersReport.Properties.Resources.UploadFile2_32;
            this.btnImportFromExcel.Location = new System.Drawing.Point(248, 3);
            this.btnImportFromExcel.Name = "btnImportFromExcel";
            this.btnImportFromExcel.Size = new System.Drawing.Size(80, 80);
            this.btnImportFromExcel.TabIndex = 7;
            this.btnImportFromExcel.Text = "Import From File";
            this.btnImportFromExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImportFromExcel.UseVisualStyleBackColor = false;
            this.btnImportFromExcel.Click += new System.EventHandler(this.btnImportFromExcel_Click);
            // 
            // btnReloadVendorCache
            // 
            this.btnReloadVendorCache.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnReloadVendorCache.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReloadVendorCache.Image = global::SalesOrdersReport.Properties.Resources.Refresh_icon_32;
            this.btnReloadVendorCache.Location = new System.Drawing.Point(327, 3);
            this.btnReloadVendorCache.Name = "btnReloadVendorCache";
            this.btnReloadVendorCache.Size = new System.Drawing.Size(80, 80);
            this.btnReloadVendorCache.TabIndex = 0;
            this.btnReloadVendorCache.Text = "Reload";
            this.btnReloadVendorCache.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReloadVendorCache.UseVisualStyleBackColor = false;
            this.btnReloadVendorCache.Click += new System.EventHandler(this.btnReloadVendorCache_Click);
            // 
            // btnRedirectDeleteVendor
            // 
            this.btnRedirectDeleteVendor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRedirectDeleteVendor.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedirectDeleteVendor.Image = global::SalesOrdersReport.Properties.Resources.delete_icon_32;
            this.btnRedirectDeleteVendor.Location = new System.Drawing.Point(169, 3);
            this.btnRedirectDeleteVendor.Name = "btnRedirectDeleteVendor";
            this.btnRedirectDeleteVendor.Size = new System.Drawing.Size(80, 80);
            this.btnRedirectDeleteVendor.TabIndex = 0;
            this.btnRedirectDeleteVendor.Text = "Delete Vendor";
            this.btnRedirectDeleteVendor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRedirectDeleteVendor.UseVisualStyleBackColor = false;
            this.btnRedirectDeleteVendor.Click += new System.EventHandler(this.btnRedirectDeleteVendor_Click);
            // 
            // btnRedirectEditVendor
            // 
            this.btnRedirectEditVendor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRedirectEditVendor.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedirectEditVendor.Image = global::SalesOrdersReport.Properties.Resources.edituser3_32;
            this.btnRedirectEditVendor.Location = new System.Drawing.Point(90, 3);
            this.btnRedirectEditVendor.Name = "btnRedirectEditVendor";
            this.btnRedirectEditVendor.Size = new System.Drawing.Size(80, 80);
            this.btnRedirectEditVendor.TabIndex = 0;
            this.btnRedirectEditVendor.Text = "Edit Vendor";
            this.btnRedirectEditVendor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRedirectEditVendor.UseVisualStyleBackColor = false;
            this.btnRedirectEditVendor.Click += new System.EventHandler(this.btnRedirectEditVendor_Click);
            // 
            // dgvVendorCache
            // 
            this.dgvVendorCache.AllowUserToAddRows = false;
            this.dgvVendorCache.AllowUserToDeleteRows = false;
            this.dgvVendorCache.AllowUserToResizeColumns = false;
            this.dgvVendorCache.AllowUserToResizeRows = false;
            this.dgvVendorCache.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVendorCache.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvVendorCache.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvVendorCache.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvVendorCache.Location = new System.Drawing.Point(12, 154);
            this.dgvVendorCache.MultiSelect = false;
            this.dgvVendorCache.Name = "dgvVendorCache";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVendorCache.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvVendorCache.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvVendorCache.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVendorCache.Size = new System.Drawing.Size(1164, 435);
            this.dgvVendorCache.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(12, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Vendor List";
            // 
            // ManageVendorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1188, 747);
            this.Controls.Add(this.dgvVendorCache);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelAllVendorBtnTab);
            this.Name = "ManageVendorForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Vendors";
            this.Load += new System.EventHandler(this.ManageVendor_Load);
            this.Shown += new System.EventHandler(this.ManageVendorForm_Shown);
            this.panelAllVendorBtnTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVendorCache)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRedirectCreateVendor;
        private System.Windows.Forms.Panel panelAllVendorBtnTab;
        private System.Windows.Forms.Button btnRedirectEditVendor;
        //private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnRedirectDeleteVendor;
        private System.Windows.Forms.Button btnReloadVendorCache;
        private System.Windows.Forms.DataGridView dgvVendorCache;
        private Button btnImportFromExcel;
        private Label label1;
        private Button btnSearchVendor;
    }
}