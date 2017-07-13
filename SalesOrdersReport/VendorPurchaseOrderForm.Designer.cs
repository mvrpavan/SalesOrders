namespace SalesOrdersReport
{
    partial class VendorPurchaseOrderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VendorPurchaseOrderForm));
            this.listBoxVendors = new System.Windows.Forms.ListBox();
            this.chkListBoxLine = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.chkBoxUseOrdQty = new System.Windows.Forms.CheckBox();
            this.txtBoxVendorOrderSheet = new System.Windows.Forms.TextBox();
            this.btnBrowseVendorOrderFile = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddVendor = new System.Windows.Forms.Button();
            this.btnCreatePurchaseOrder = new System.Windows.Forms.Button();
            this.btnSaveFolderBrowse = new System.Windows.Forms.Button();
            this.txtBoxOutputFolder = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkBoxCreateSummary = new System.Windows.Forms.CheckBox();
            this.dateTimePO = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.bgWorkerCreatePO = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxVendors
            // 
            this.listBoxVendors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxVendors.FormattingEnabled = true;
            this.listBoxVendors.Location = new System.Drawing.Point(213, 32);
            this.listBoxVendors.Name = "listBoxVendors";
            this.listBoxVendors.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxVendors.Size = new System.Drawing.Size(147, 67);
            this.listBoxVendors.TabIndex = 41;
            // 
            // chkListBoxLine
            // 
            this.chkListBoxLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chkListBoxLine.CheckOnClick = true;
            this.chkListBoxLine.FormattingEnabled = true;
            this.chkListBoxLine.Location = new System.Drawing.Point(31, 32);
            this.chkListBoxLine.Name = "chkListBoxLine";
            this.chkListBoxLine.Size = new System.Drawing.Size(149, 62);
            this.chkListBoxLine.TabIndex = 40;
            this.chkListBoxLine.SelectedIndexChanged += new System.EventHandler(this.chkListBoxLine_SelectedIndexChanged);
            this.chkListBoxLine.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.chkListBoxLine_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 378);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "Status:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(89, 378);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 38;
            this.lblStatus.Text = "Status";
            // 
            // chkBoxUseOrdQty
            // 
            this.chkBoxUseOrdQty.AutoSize = true;
            this.chkBoxUseOrdQty.Location = new System.Drawing.Point(228, 59);
            this.chkBoxUseOrdQty.Name = "chkBoxUseOrdQty";
            this.chkBoxUseOrdQty.Size = new System.Drawing.Size(175, 17);
            this.chkBoxUseOrdQty.TabIndex = 24;
            this.chkBoxUseOrdQty.Text = "&Use Order Qty as Recevied Qty";
            this.chkBoxUseOrdQty.UseVisualStyleBackColor = true;
            // 
            // txtBoxVendorOrderSheet
            // 
            this.txtBoxVendorOrderSheet.Location = new System.Drawing.Point(138, 19);
            this.txtBoxVendorOrderSheet.Name = "txtBoxVendorOrderSheet";
            this.txtBoxVendorOrderSheet.Size = new System.Drawing.Size(225, 20);
            this.txtBoxVendorOrderSheet.TabIndex = 27;
            // 
            // btnBrowseVendorOrderFile
            // 
            this.btnBrowseVendorOrderFile.Location = new System.Drawing.Point(369, 17);
            this.btnBrowseVendorOrderFile.Name = "btnBrowseVendorOrderFile";
            this.btnBrowseVendorOrderFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseVendorOrderFile.TabIndex = 28;
            this.btnBrowseVendorOrderFile.Text = "&Browse";
            this.btnBrowseVendorOrderFile.UseVisualStyleBackColor = true;
            this.btnBrowseVendorOrderFile.Click += new System.EventHandler(this.btnBrowseVendorOrderFile_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(279, 313);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(124, 42);
            this.btnClose.TabIndex = 37;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAddVendor
            // 
            this.btnAddVendor.BackColor = System.Drawing.SystemColors.Control;
            this.btnAddVendor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddVendor.BackgroundImage")));
            this.btnAddVendor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAddVendor.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnAddVendor.FlatAppearance.BorderSize = 0;
            this.btnAddVendor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnAddVendor.Location = new System.Drawing.Point(366, 32);
            this.btnAddVendor.Name = "btnAddVendor";
            this.btnAddVendor.Size = new System.Drawing.Size(56, 26);
            this.btnAddVendor.TabIndex = 35;
            this.btnAddVendor.Text = "Add";
            this.btnAddVendor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddVendor.UseVisualStyleBackColor = false;
            this.btnAddVendor.Click += new System.EventHandler(this.btnAddVendor_Click);
            // 
            // btnCreatePurchaseOrder
            // 
            this.btnCreatePurchaseOrder.Location = new System.Drawing.Point(103, 313);
            this.btnCreatePurchaseOrder.Name = "btnCreatePurchaseOrder";
            this.btnCreatePurchaseOrder.Size = new System.Drawing.Size(124, 42);
            this.btnCreatePurchaseOrder.TabIndex = 36;
            this.btnCreatePurchaseOrder.Text = "&Generate Purchase Order";
            this.btnCreatePurchaseOrder.UseVisualStyleBackColor = true;
            this.btnCreatePurchaseOrder.Click += new System.EventHandler(this.btnCreatePurchaseOrder_Click);
            // 
            // btnSaveFolderBrowse
            // 
            this.btnSaveFolderBrowse.Location = new System.Drawing.Point(369, 58);
            this.btnSaveFolderBrowse.Name = "btnSaveFolderBrowse";
            this.btnSaveFolderBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnSaveFolderBrowse.TabIndex = 34;
            this.btnSaveFolderBrowse.Text = "B&rowse";
            this.btnSaveFolderBrowse.UseVisualStyleBackColor = true;
            this.btnSaveFolderBrowse.Click += new System.EventHandler(this.btnSaveFolderBrowse_Click);
            // 
            // txtBoxOutputFolder
            // 
            this.txtBoxOutputFolder.Location = new System.Drawing.Point(138, 60);
            this.txtBoxOutputFolder.Name = "txtBoxOutputFolder";
            this.txtBoxOutputFolder.Size = new System.Drawing.Size(225, 20);
            this.txtBoxOutputFolder.TabIndex = 30;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(210, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Custom Vendor list";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Choose Line";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Vendor Order Sheet";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Save Folder Path";
            // 
            // chkBoxCreateSummary
            // 
            this.chkBoxCreateSummary.AutoSize = true;
            this.chkBoxCreateSummary.Checked = true;
            this.chkBoxCreateSummary.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxCreateSummary.Location = new System.Drawing.Point(46, 59);
            this.chkBoxCreateSummary.Name = "chkBoxCreateSummary";
            this.chkBoxCreateSummary.Size = new System.Drawing.Size(134, 17);
            this.chkBoxCreateSummary.TabIndex = 23;
            this.chkBoxCreateSummary.Text = "Create &Summary Sheet";
            this.chkBoxCreateSummary.UseVisualStyleBackColor = true;
            // 
            // dateTimePO
            // 
            this.dateTimePO.Location = new System.Drawing.Point(118, 21);
            this.dateTimePO.Name = "dateTimePO";
            this.dateTimePO.Size = new System.Drawing.Size(257, 20);
            this.dateTimePO.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Choose Date";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBoxVendorOrderSheet);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtBoxOutputFolder);
            this.groupBox1.Controls.Add(this.btnSaveFolderBrowse);
            this.groupBox1.Controls.Add(this.btnBrowseVendorOrderFile);
            this.groupBox1.Location = new System.Drawing.Point(12, 197);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(465, 99);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkListBoxLine);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.listBoxVendors);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.btnAddVendor);
            this.groupBox2.Location = new System.Drawing.Point(12, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(465, 109);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // bgWorkerCreatePO
            // 
            this.bgWorkerCreatePO.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerCreatePO_DoWork);
            this.bgWorkerCreatePO.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorkerCreatePO_ProgressChanged);
            this.bgWorkerCreatePO.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerCreatePO_RunWorkerCompleted);
            // 
            // VendorPurchaseOrderForm
            // 
            this.AcceptButton = this.btnCreatePurchaseOrder;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(489, 407);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.chkBoxUseOrdQty);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCreatePurchaseOrder);
            this.Controls.Add(this.chkBoxCreateSummary);
            this.Controls.Add(this.dateTimePO);
            this.Controls.Add(this.label1);
            this.Name = "VendorPurchaseOrderForm";
            this.Text = "Create Purchase Orders";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VendorPurchaseOrderForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxVendors;
        private System.Windows.Forms.CheckedListBox chkListBoxLine;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.CheckBox chkBoxUseOrdQty;
        private System.Windows.Forms.TextBox txtBoxVendorOrderSheet;
        private System.Windows.Forms.Button btnBrowseVendorOrderFile;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAddVendor;
        private System.Windows.Forms.Button btnCreatePurchaseOrder;
        private System.Windows.Forms.Button btnSaveFolderBrowse;
        private System.Windows.Forms.TextBox txtBoxOutputFolder;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkBoxCreateSummary;
        private System.Windows.Forms.DateTimePicker dateTimePO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.ComponentModel.BackgroundWorker bgWorkerCreatePO;

    }
}