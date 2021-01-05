using System.Windows.Forms;

namespace SalesOrdersReport
{
    partial class ManageCustomerForm
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
            this.btnRedirectCreateCustomer = new System.Windows.Forms.Button();
            this.panelAllCustomerBtnTab = new System.Windows.Forms.Panel();
            this.btnImportFromExcel = new System.Windows.Forms.Button();
            this.btnEditDiscountGrp = new System.Windows.Forms.Button();
            this.btnReloadCustomerCache = new System.Windows.Forms.Button();
            this.btnCreateDiscountGrp = new System.Windows.Forms.Button();
            this.btnEditPriceGrp = new System.Windows.Forms.Button();
            this.btnCreatePriceGrp = new System.Windows.Forms.Button();
            this.btnEditLine = new System.Windows.Forms.Button();
            this.btnRedirectCreateLine = new System.Windows.Forms.Button();
            this.btnRedirectDeleteCustomer = new System.Windows.Forms.Button();
            this.btnRedirectEditCustomer = new System.Windows.Forms.Button();
            this.dgvCustomerCache = new System.Windows.Forms.DataGridView();
            this.panelAllCustomerBtnTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerCache)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRedirectCreateUser
            // 
            this.btnRedirectCreateCustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRedirectCreateCustomer.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedirectCreateCustomer.Image = global::SalesOrdersReport.Properties.Resources.CreateUser_321;
            this.btnRedirectCreateCustomer.Location = new System.Drawing.Point(11, 3);
            this.btnRedirectCreateCustomer.Name = "btnRedirectCreateUser";
            this.btnRedirectCreateCustomer.Size = new System.Drawing.Size(80, 80);
            this.btnRedirectCreateCustomer.TabIndex = 0;
            this.btnRedirectCreateCustomer.Text = "Create Customer";
            this.btnRedirectCreateCustomer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRedirectCreateCustomer.UseVisualStyleBackColor = false;
            this.btnRedirectCreateCustomer.Click += new System.EventHandler(this.btnRedirectCreateCustomer_Click);
            // 
            // panelAllCustomerBtnTab
            // 
            this.panelAllCustomerBtnTab.Controls.Add(this.btnImportFromExcel);
            this.panelAllCustomerBtnTab.Controls.Add(this.btnEditDiscountGrp);
            this.panelAllCustomerBtnTab.Controls.Add(this.btnReloadCustomerCache);
            this.panelAllCustomerBtnTab.Controls.Add(this.btnCreateDiscountGrp);
            this.panelAllCustomerBtnTab.Controls.Add(this.btnEditPriceGrp);
            this.panelAllCustomerBtnTab.Controls.Add(this.btnCreatePriceGrp);
            this.panelAllCustomerBtnTab.Controls.Add(this.btnEditLine);
            this.panelAllCustomerBtnTab.Controls.Add(this.btnRedirectCreateLine);
            this.panelAllCustomerBtnTab.Controls.Add(this.btnRedirectDeleteCustomer);
            this.panelAllCustomerBtnTab.Controls.Add(this.btnRedirectEditCustomer);
            this.panelAllCustomerBtnTab.Controls.Add(this.btnRedirectCreateCustomer);
            this.panelAllCustomerBtnTab.Location = new System.Drawing.Point(12, 12);
            this.panelAllCustomerBtnTab.Name = "panelAllCustomerBtnTab";
            this.panelAllCustomerBtnTab.Size = new System.Drawing.Size(897, 93);
            this.panelAllCustomerBtnTab.TabIndex = 1;
            // 
            // btnImportFromExcel
            // 
            this.btnImportFromExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnImportFromExcel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportFromExcel.Image = global::SalesOrdersReport.Properties.Resources.UploadFile2_32;
            this.btnImportFromExcel.Location = new System.Drawing.Point(722, 3);
            this.btnImportFromExcel.Name = "btnImportFromExcel";
            this.btnImportFromExcel.Size = new System.Drawing.Size(80, 80);
            this.btnImportFromExcel.TabIndex = 7;
            this.btnImportFromExcel.Text = "Import From File";
            this.btnImportFromExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImportFromExcel.UseVisualStyleBackColor = false;
            this.btnImportFromExcel.Click += new System.EventHandler(this.btnImportFromExcel_Click);
            // 
            // btnEditDiscountGrp
            // 
            this.btnEditDiscountGrp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEditDiscountGrp.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditDiscountGrp.Image = global::SalesOrdersReport.Properties.Resources.edit_1;
            this.btnEditDiscountGrp.Location = new System.Drawing.Point(643, 3);
            this.btnEditDiscountGrp.Name = "btnEditDiscountGrp";
            this.btnEditDiscountGrp.Size = new System.Drawing.Size(80, 80);
            this.btnEditDiscountGrp.TabIndex = 6;
            this.btnEditDiscountGrp.Text = "Edit Discount Group";
            this.btnEditDiscountGrp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEditDiscountGrp.UseVisualStyleBackColor = false;
            this.btnEditDiscountGrp.Click += new System.EventHandler(this.btnEditDiscountGrp_Click);
            // 
            // btnReloadCustomerCache
            // 
            this.btnReloadCustomerCache.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnReloadCustomerCache.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReloadCustomerCache.Image = global::SalesOrdersReport.Properties.Resources.Refresh_icon_32;
            this.btnReloadCustomerCache.Location = new System.Drawing.Point(801, 3);
            this.btnReloadCustomerCache.Name = "btnReloadCustomerCache";
            this.btnReloadCustomerCache.Size = new System.Drawing.Size(80, 80);
            this.btnReloadCustomerCache.TabIndex = 0;
            this.btnReloadCustomerCache.Text = "Reload";
            this.btnReloadCustomerCache.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReloadCustomerCache.UseVisualStyleBackColor = false;
            this.btnReloadCustomerCache.Click += new System.EventHandler(this.btnReloadCustomerCache_Click);
            // 
            // btnCreateDiscountGrp
            // 
            this.btnCreateDiscountGrp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCreateDiscountGrp.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateDiscountGrp.Image = global::SalesOrdersReport.Properties.Resources.discount1;
            this.btnCreateDiscountGrp.Location = new System.Drawing.Point(564, 3);
            this.btnCreateDiscountGrp.Name = "btnCreateDiscountGrp";
            this.btnCreateDiscountGrp.Size = new System.Drawing.Size(80, 80);
            this.btnCreateDiscountGrp.TabIndex = 5;
            this.btnCreateDiscountGrp.Text = "Create Discount Group";
            this.btnCreateDiscountGrp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCreateDiscountGrp.UseVisualStyleBackColor = false;
            this.btnCreateDiscountGrp.Click += new System.EventHandler(this.btnCreateDiscountGrp_Click);
            // 
            // btnEditPriceGrp
            // 
            this.btnEditPriceGrp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEditPriceGrp.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditPriceGrp.Image = global::SalesOrdersReport.Properties.Resources.edit_icon_321;
            this.btnEditPriceGrp.Location = new System.Drawing.Point(485, 3);
            this.btnEditPriceGrp.Name = "btnEditPriceGrp";
            this.btnEditPriceGrp.Size = new System.Drawing.Size(80, 80);
            this.btnEditPriceGrp.TabIndex = 4;
            this.btnEditPriceGrp.Text = "Edit Price Group";
            this.btnEditPriceGrp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEditPriceGrp.UseVisualStyleBackColor = false;
            this.btnEditPriceGrp.Click += new System.EventHandler(this.btnEditPriceGrp_Click);
            // 
            // btnCreatePriceGrp
            // 
            this.btnCreatePriceGrp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCreatePriceGrp.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreatePriceGrp.Image = global::SalesOrdersReport.Properties.Resources.add2;
            this.btnCreatePriceGrp.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCreatePriceGrp.Location = new System.Drawing.Point(406, 3);
            this.btnCreatePriceGrp.Name = "btnCreatePriceGrp";
            this.btnCreatePriceGrp.Size = new System.Drawing.Size(80, 80);
            this.btnCreatePriceGrp.TabIndex = 3;
            this.btnCreatePriceGrp.Text = "Create Price Group";
            this.btnCreatePriceGrp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCreatePriceGrp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCreatePriceGrp.UseVisualStyleBackColor = false;
            this.btnCreatePriceGrp.Click += new System.EventHandler(this.btnCreatePriceGrp_Click);
            // 
            // btnEditLine
            // 
            this.btnEditLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEditLine.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditLine.Image = global::SalesOrdersReport.Properties.Resources.EditLine1_321;
            this.btnEditLine.Location = new System.Drawing.Point(327, 3);
            this.btnEditLine.Name = "btnEditLine";
            this.btnEditLine.Size = new System.Drawing.Size(80, 80);
            this.btnEditLine.TabIndex = 2;
            this.btnEditLine.Text = "Edit Line";
            this.btnEditLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEditLine.UseVisualStyleBackColor = false;
            this.btnEditLine.Click += new System.EventHandler(this.btnEditLine_Click);
            // 
            // btnRedirectCreateLine
            // 
            this.btnRedirectCreateLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRedirectCreateLine.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedirectCreateLine.Image = global::SalesOrdersReport.Properties.Resources.adddelivery2_32;
            this.btnRedirectCreateLine.Location = new System.Drawing.Point(248, 3);
            this.btnRedirectCreateLine.Name = "btnRedirectCreateLine";
            this.btnRedirectCreateLine.Size = new System.Drawing.Size(80, 80);
            this.btnRedirectCreateLine.TabIndex = 1;
            this.btnRedirectCreateLine.Text = "Create Line";
            this.btnRedirectCreateLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRedirectCreateLine.UseVisualStyleBackColor = false;
            this.btnRedirectCreateLine.Click += new System.EventHandler(this.btnCreateLine_Click);
            // 
            // btnRedirectDeleteCustomer
            // 
            this.btnRedirectDeleteCustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRedirectDeleteCustomer.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedirectDeleteCustomer.Image = global::SalesOrdersReport.Properties.Resources.delete_icon_32;
            this.btnRedirectDeleteCustomer.Location = new System.Drawing.Point(169, 3);
            this.btnRedirectDeleteCustomer.Name = "btnRedirectDeleteCustomer";
            this.btnRedirectDeleteCustomer.Size = new System.Drawing.Size(80, 80);
            this.btnRedirectDeleteCustomer.TabIndex = 0;
            this.btnRedirectDeleteCustomer.Text = "Delete Customer";
            this.btnRedirectDeleteCustomer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRedirectDeleteCustomer.UseVisualStyleBackColor = false;
            this.btnRedirectDeleteCustomer.Click += new System.EventHandler(this.btnRedirectDeleteCustomer_Click);
            // 
            // btnRedirectEditCustomer
            // 
            this.btnRedirectEditCustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRedirectEditCustomer.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedirectEditCustomer.Image = global::SalesOrdersReport.Properties.Resources.edituser3_32;
            this.btnRedirectEditCustomer.Location = new System.Drawing.Point(90, 3);
            this.btnRedirectEditCustomer.Name = "btnRedirectEditCustomer";
            this.btnRedirectEditCustomer.Size = new System.Drawing.Size(80, 80);
            this.btnRedirectEditCustomer.TabIndex = 0;
            this.btnRedirectEditCustomer.Text = "Edit Customer";
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
            this.dgvCustomerCache.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomerCache.Location = new System.Drawing.Point(12, 111);
            this.dgvCustomerCache.MultiSelect = false;
            this.dgvCustomerCache.Name = "dgvCustomerCache";
            this.dgvCustomerCache.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvCustomerCache.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustomerCache.Size = new System.Drawing.Size(897, 207);
            this.dgvCustomerCache.TabIndex = 3;
            // 
            // ManageCustomerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 510);
            this.Controls.Add(this.dgvCustomerCache);
            this.Controls.Add(this.panelAllCustomerBtnTab);
            this.Name = "ManageCustomerForm";
            this.ShowIcon = false;
            this.Text = "Manage Customers";
            this.Load += new System.EventHandler(this.ManageCustomer_Load);
            this.panelAllCustomerBtnTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerCache)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRedirectCreateCustomer;
        private System.Windows.Forms.Panel panelAllCustomerBtnTab;
        private System.Windows.Forms.Button btnRedirectEditCustomer;
        //private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnRedirectDeleteCustomer;
        private System.Windows.Forms.Button btnReloadCustomerCache;
        private System.Windows.Forms.DataGridView dgvCustomerCache;
        private System.Windows.Forms.Button btnRedirectCreateLine;
        private System.Windows.Forms.Button btnEditLine;
        private System.Windows.Forms.Button btnEditPriceGrp;
        private System.Windows.Forms.Button btnCreatePriceGrp;
        private System.Windows.Forms.Button btnEditDiscountGrp;
        private System.Windows.Forms.Button btnCreateDiscountGrp;
        private Button btnImportFromExcel;
    }
}