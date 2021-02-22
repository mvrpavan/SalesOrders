namespace SalesOrdersReport.Views
{
    partial class ProductInventoryMainForm
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
            this.btnCreateOrder = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearchProduct = new System.Windows.Forms.Button();
            this.cmbBoxCategoryFilterList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClearSearchProduct = new System.Windows.Forms.Button();
            this.txtBoxProductSearchString = new System.Windows.Forms.TextBox();
            this.btnReloadOrders = new System.Windows.Forms.Button();
            this.dtGridViewOrders = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDeleteProduct = new System.Windows.Forms.Button();
            this.lblOrdersCount = new System.Windows.Forms.Label();
            this.backgroundWorkerOrders = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCreateOrder
            // 
            this.btnCreateOrder.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCreateOrder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnCreateOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCreateOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateOrder.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateOrder.Image = global::SalesOrdersReport.Properties.Resources.math_add_icon_32;
            this.btnCreateOrder.Location = new System.Drawing.Point(5, 3);
            this.btnCreateOrder.Name = "btnCreateOrder";
            this.btnCreateOrder.Size = new System.Drawing.Size(83, 73);
            this.btnCreateOrder.TabIndex = 0;
            this.btnCreateOrder.Text = "Add Stock Product";
            this.btnCreateOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCreateOrder.UseVisualStyleBackColor = false;
            this.btnCreateOrder.Click += new System.EventHandler(this.btnCreateOrder_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.btnDeleteProduct);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.btnReloadOrders);
            this.panel1.Controls.Add(this.btnCreateOrder);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(864, 79);
            this.panel1.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.btnSearchProduct);
            this.panel4.Controls.Add(this.cmbBoxCategoryFilterList);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.btnClearSearchProduct);
            this.panel4.Controls.Add(this.txtBoxProductSearchString);
            this.panel4.Location = new System.Drawing.Point(445, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(416, 73);
            this.panel4.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label3.Location = new System.Drawing.Point(10, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Filter Category";
            // 
            // btnSearchProduct
            // 
            this.btnSearchProduct.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSearchProduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSearchProduct.FlatAppearance.BorderSize = 0;
            this.btnSearchProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchProduct.Image = global::SalesOrdersReport.Properties.Resources.Search_icon;
            this.btnSearchProduct.Location = new System.Drawing.Point(330, 36);
            this.btnSearchProduct.Name = "btnSearchProduct";
            this.btnSearchProduct.Size = new System.Drawing.Size(33, 29);
            this.btnSearchProduct.TabIndex = 6;
            this.btnSearchProduct.UseVisualStyleBackColor = false;
            // 
            // cmbBoxCategoryFilterList
            // 
            this.cmbBoxCategoryFilterList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxCategoryFilterList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbBoxCategoryFilterList.FormattingEnabled = true;
            this.cmbBoxCategoryFilterList.Location = new System.Drawing.Point(90, 3);
            this.cmbBoxCategoryFilterList.Name = "cmbBoxCategoryFilterList";
            this.cmbBoxCategoryFilterList.Size = new System.Drawing.Size(234, 21);
            this.cmbBoxCategoryFilterList.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label2.Location = new System.Drawing.Point(3, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Search Product";
            // 
            // btnClearSearchProduct
            // 
            this.btnClearSearchProduct.FlatAppearance.BorderSize = 0;
            this.btnClearSearchProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSearchProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearSearchProduct.Image = global::SalesOrdersReport.Properties.Resources.Trash_icon;
            this.btnClearSearchProduct.Location = new System.Drawing.Point(369, 35);
            this.btnClearSearchProduct.Name = "btnClearSearchProduct";
            this.btnClearSearchProduct.Size = new System.Drawing.Size(33, 29);
            this.btnClearSearchProduct.TabIndex = 7;
            this.btnClearSearchProduct.UseVisualStyleBackColor = true;
            // 
            // txtBoxProductSearchString
            // 
            this.txtBoxProductSearchString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtBoxProductSearchString.Location = new System.Drawing.Point(90, 39);
            this.txtBoxProductSearchString.Name = "txtBoxProductSearchString";
            this.txtBoxProductSearchString.Size = new System.Drawing.Size(234, 20);
            this.txtBoxProductSearchString.TabIndex = 8;
            // 
            // btnReloadOrders
            // 
            this.btnReloadOrders.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnReloadOrders.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnReloadOrders.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnReloadOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReloadOrders.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnReloadOrders.Image = global::SalesOrdersReport.Properties.Resources.Refresh_icon_32;
            this.btnReloadOrders.Location = new System.Drawing.Point(183, 3);
            this.btnReloadOrders.Name = "btnReloadOrders";
            this.btnReloadOrders.Size = new System.Drawing.Size(102, 73);
            this.btnReloadOrders.TabIndex = 0;
            this.btnReloadOrders.Text = "Reload Stock Products";
            this.btnReloadOrders.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReloadOrders.UseVisualStyleBackColor = false;
            this.btnReloadOrders.Click += new System.EventHandler(this.btnReloadOrders_Click);
            // 
            // dtGridViewOrders
            // 
            this.dtGridViewOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGridViewOrders.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtGridViewOrders.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dtGridViewOrders.Location = new System.Drawing.Point(9, 121);
            this.dtGridViewOrders.Name = "dtGridViewOrders";
            this.dtGridViewOrders.Size = new System.Drawing.Size(864, 298);
            this.dtGridViewOrders.TabIndex = 2;
            this.dtGridViewOrders.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtGridViewOrders_CellMouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(12, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Product Stock Inventory";
            // 
            // btnDeleteProduct
            // 
            this.btnDeleteProduct.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDeleteProduct.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnDeleteProduct.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnDeleteProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteProduct.Font = new System.Drawing.Font("Calibri", 10F);
            this.btnDeleteProduct.Image = global::SalesOrdersReport.Properties.Resources.delete_icon_32;
            this.btnDeleteProduct.Location = new System.Drawing.Point(94, 3);
            this.btnDeleteProduct.Name = "btnDeleteProduct";
            this.btnDeleteProduct.Size = new System.Drawing.Size(83, 73);
            this.btnDeleteProduct.TabIndex = 12;
            this.btnDeleteProduct.Text = "Delete Product";
            this.btnDeleteProduct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteProduct.UseVisualStyleBackColor = false;
            // 
            // lblOrdersCount
            // 
            this.lblOrdersCount.AutoSize = true;
            this.lblOrdersCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblOrdersCount.Location = new System.Drawing.Point(237, 98);
            this.lblOrdersCount.Name = "lblOrdersCount";
            this.lblOrdersCount.Size = new System.Drawing.Size(93, 20);
            this.lblOrdersCount.TabIndex = 4;
            this.lblOrdersCount.Text = "[Displaying ]";
            // 
            // backgroundWorkerOrders
            // 
            this.backgroundWorkerOrders.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerOrders_DoWork);
            this.backgroundWorkerOrders.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerOrders_ProgressChanged);
            this.backgroundWorkerOrders.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerOrders_RunWorkerCompleted);
            // 
            // ProductInventoryMainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(888, 611);
            this.Controls.Add(this.lblOrdersCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtGridViewOrders);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProductInventoryMainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Product Inventory";
            this.Shown += new System.EventHandler(this.OrdersMainForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateOrder;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dtGridViewOrders;
        private System.Windows.Forms.Button btnReloadOrders;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearchProduct;
        private System.Windows.Forms.ComboBox cmbBoxCategoryFilterList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClearSearchProduct;
        private System.Windows.Forms.TextBox txtBoxProductSearchString;
        private System.Windows.Forms.Button btnDeleteProduct;
        private System.Windows.Forms.Label lblOrdersCount;
        private System.ComponentModel.BackgroundWorker backgroundWorkerOrders;
    }
}