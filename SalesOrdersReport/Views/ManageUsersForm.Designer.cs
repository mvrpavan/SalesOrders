using System.Windows.Forms;

namespace SalesOrdersReport
{
    partial class ManageUsersForm
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
            this.btnRedirectCreateUser = new System.Windows.Forms.Button();
            this.panelAllBtnTab = new System.Windows.Forms.Panel();
            this.btnEditStore = new System.Windows.Forms.Button();
            this.btnCreateStore = new System.Windows.Forms.Button();
            this.btnDefineRole = new System.Windows.Forms.Button();
            this.btnRedirectCreateRole = new System.Windows.Forms.Button();
            this.btnReloadUserCache = new System.Windows.Forms.Button();
            this.btnRedirectDeleteUser = new System.Windows.Forms.Button();
            this.btnRedirectEditUser = new System.Windows.Forms.Button();
            this.dgvUserCache = new System.Windows.Forms.DataGridView();
            this.panelAllBtnTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserCache)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRedirectCreateUser
            // 
            this.btnRedirectCreateUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRedirectCreateUser.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedirectCreateUser.Image = global::SalesOrdersReport.Properties.Resources.math_add_icon_32;
            this.btnRedirectCreateUser.Location = new System.Drawing.Point(11, 3);
            this.btnRedirectCreateUser.Name = "btnRedirectCreateUser";
            this.btnRedirectCreateUser.Size = new System.Drawing.Size(63, 80);
            this.btnRedirectCreateUser.TabIndex = 0;
            this.btnRedirectCreateUser.Text = "Create User";
            this.btnRedirectCreateUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRedirectCreateUser.UseVisualStyleBackColor = false;
            this.btnRedirectCreateUser.Click += new System.EventHandler(this.btnRedirectCreateUser_Click);
            // 
            // panelAllBtnTab
            // 
            this.panelAllBtnTab.Controls.Add(this.btnEditStore);
            this.panelAllBtnTab.Controls.Add(this.btnCreateStore);
            this.panelAllBtnTab.Controls.Add(this.btnDefineRole);
            this.panelAllBtnTab.Controls.Add(this.btnRedirectCreateRole);
            this.panelAllBtnTab.Controls.Add(this.btnReloadUserCache);
            this.panelAllBtnTab.Controls.Add(this.btnRedirectDeleteUser);
            this.panelAllBtnTab.Controls.Add(this.btnRedirectEditUser);
            this.panelAllBtnTab.Controls.Add(this.btnRedirectCreateUser);
            this.panelAllBtnTab.Location = new System.Drawing.Point(12, 12);
            this.panelAllBtnTab.Name = "panelAllBtnTab";
            this.panelAllBtnTab.Size = new System.Drawing.Size(719, 93);
            this.panelAllBtnTab.TabIndex = 1;
            // 
            // btnEditStore
            // 
            this.btnEditStore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEditStore.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditStore.Image = global::SalesOrdersReport.Properties.Resources.editstore_32;
            this.btnEditStore.Location = new System.Drawing.Point(383, 3);
            this.btnEditStore.Name = "btnEditStore";
            this.btnEditStore.Size = new System.Drawing.Size(63, 80);
            this.btnEditStore.TabIndex = 4;
            this.btnEditStore.Text = "Edit Store";
            this.btnEditStore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEditStore.UseVisualStyleBackColor = false;
            this.btnEditStore.Click += new System.EventHandler(this.btnEditStore_Click);
            // 
            // btnCreateStore
            // 
            this.btnCreateStore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCreateStore.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateStore.Image = global::SalesOrdersReport.Properties.Resources.store_32;
            this.btnCreateStore.Location = new System.Drawing.Point(321, 3);
            this.btnCreateStore.Name = "btnCreateStore";
            this.btnCreateStore.Size = new System.Drawing.Size(63, 80);
            this.btnCreateStore.TabIndex = 3;
            this.btnCreateStore.Text = "Create Store";
            this.btnCreateStore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCreateStore.UseVisualStyleBackColor = false;
            this.btnCreateStore.Click += new System.EventHandler(this.btnCreateStore_Click);
            // 
            // btnDefineRole
            // 
            this.btnDefineRole.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDefineRole.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefineRole.Image = global::SalesOrdersReport.Properties.Resources.editrole23_321;
            this.btnDefineRole.Location = new System.Drawing.Point(259, 3);
            this.btnDefineRole.Name = "btnDefineRole";
            this.btnDefineRole.Size = new System.Drawing.Size(63, 80);
            this.btnDefineRole.TabIndex = 2;
            this.btnDefineRole.Text = "Define Role";
            this.btnDefineRole.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDefineRole.UseVisualStyleBackColor = false;
            this.btnDefineRole.Click += new System.EventHandler(this.btnDefineRole_Click);
            // 
            // btnRedirectCreateRole
            // 
            this.btnRedirectCreateRole.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRedirectCreateRole.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedirectCreateRole.Image = global::SalesOrdersReport.Properties.Resources.rolenew1232;
            this.btnRedirectCreateRole.Location = new System.Drawing.Point(197, 3);
            this.btnRedirectCreateRole.Name = "btnRedirectCreateRole";
            this.btnRedirectCreateRole.Size = new System.Drawing.Size(63, 80);
            this.btnRedirectCreateRole.TabIndex = 1;
            this.btnRedirectCreateRole.Text = "Create Role";
            this.btnRedirectCreateRole.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRedirectCreateRole.UseVisualStyleBackColor = false;
            this.btnRedirectCreateRole.Click += new System.EventHandler(this.btnCreateRole_Click);
            // 
            // btnReloadUserCache
            // 
            this.btnReloadUserCache.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnReloadUserCache.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReloadUserCache.Image = global::SalesOrdersReport.Properties.Resources.Refresh_icon_321;
            this.btnReloadUserCache.Location = new System.Drawing.Point(445, 3);
            this.btnReloadUserCache.Name = "btnReloadUserCache";
            this.btnReloadUserCache.Size = new System.Drawing.Size(63, 80);
            this.btnReloadUserCache.TabIndex = 0;
            this.btnReloadUserCache.Text = "Reload";
            this.btnReloadUserCache.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReloadUserCache.UseVisualStyleBackColor = false;
            this.btnReloadUserCache.Click += new System.EventHandler(this.btnReloadUserCache_Click);
            // 
            // btnRedirectDeleteUser
            // 
            this.btnRedirectDeleteUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRedirectDeleteUser.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedirectDeleteUser.Image = global::SalesOrdersReport.Properties.Resources.delete_icon_32;
            this.btnRedirectDeleteUser.Location = new System.Drawing.Point(135, 3);
            this.btnRedirectDeleteUser.Name = "btnRedirectDeleteUser";
            this.btnRedirectDeleteUser.Size = new System.Drawing.Size(63, 80);
            this.btnRedirectDeleteUser.TabIndex = 0;
            this.btnRedirectDeleteUser.Text = "Delete User";
            this.btnRedirectDeleteUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRedirectDeleteUser.UseVisualStyleBackColor = false;
            this.btnRedirectDeleteUser.Click += new System.EventHandler(this.btnRedirectDeleteUser_Click);
            // 
            // btnRedirectEditUser
            // 
            this.btnRedirectEditUser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRedirectEditUser.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedirectEditUser.Image = global::SalesOrdersReport.Properties.Resources.edit_icon_32;
            this.btnRedirectEditUser.Location = new System.Drawing.Point(73, 3);
            this.btnRedirectEditUser.Name = "btnRedirectEditUser";
            this.btnRedirectEditUser.Size = new System.Drawing.Size(63, 80);
            this.btnRedirectEditUser.TabIndex = 0;
            this.btnRedirectEditUser.Text = "Edit User";
            this.btnRedirectEditUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRedirectEditUser.UseVisualStyleBackColor = false;
            this.btnRedirectEditUser.Click += new System.EventHandler(this.btnRedirectEditUser_Click);
            // 
            // dgvUserCache
            // 
            this.dgvUserCache.AllowUserToAddRows = false;
            this.dgvUserCache.AllowUserToResizeColumns = false;
            this.dgvUserCache.AllowUserToOrderColumns = false;
            this.dgvUserCache.AllowUserToDeleteRows = false;
            this.dgvUserCache.AllowUserToResizeRows = false;
            this.dgvUserCache.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserCache.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvUserCache.Location = new System.Drawing.Point(12, 111);
            this.dgvUserCache.MultiSelect = false;
            this.dgvUserCache.Name = "dgvUserCache";
            this.dgvUserCache.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUserCache.Size = new System.Drawing.Size(719, 207);
            this.dgvUserCache.TabIndex = 3;
            
            // 
            // ManageUsersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 510);
            this.Controls.Add(this.dgvUserCache);
            this.Controls.Add(this.panelAllBtnTab);
            this.Name = "ManageUsersForm";
            this.ShowIcon = false;
            this.Text = "Manage Users";
            this.Load += new System.EventHandler(this.ManageUsers_Load);
            this.panelAllBtnTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserCache)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRedirectCreateUser;
        private System.Windows.Forms.Panel panelAllBtnTab;
        private System.Windows.Forms.Button btnRedirectEditUser;
        //private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnRedirectDeleteUser;
        private System.Windows.Forms.Button btnReloadUserCache;
        private System.Windows.Forms.DataGridView dgvUserCache;
        private System.Windows.Forms.Button btnRedirectCreateRole;
        private System.Windows.Forms.Button btnDefineRole;
        private System.Windows.Forms.Button btnEditStore;
        private System.Windows.Forms.Button btnCreateStore;
    }
}