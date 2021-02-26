namespace SalesOrdersReport.Views
{
    partial class CreatePOSBillForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreatePOSBillForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblSelectName = new System.Windows.Forms.Label();
            this.cmbBoxCustomers = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBoxProduct = new System.Windows.Forms.ComboBox();
            this.cmbBoxProdCat = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtTmPckrInvOrdDate = new System.Windows.Forms.DateTimePicker();
            this.lblInvoiceDate = new System.Windows.Forms.Label();
            this.lblInvoiceNumber = new System.Windows.Forms.Label();
            this.txtBoxInvOrdNumber = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnCancelChanges = new System.Windows.Forms.Button();
            this.btnDiscount = new System.Windows.Forms.Button();
            this.btnRemItem = new System.Windows.Forms.Button();
            this.btnItemDiscount = new System.Windows.Forms.Button();
            this.btnSelectAllToRemove = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.opnFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label10 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.picBoxLoading = new System.Windows.Forms.PictureBox();
            this.btnAddCustomer = new System.Windows.Forms.Button();
            this.cmbBoxBillNumber = new System.Windows.Forms.ComboBox();
            this.btnPrintBill = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbBoxPhoneNumbers = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblGrandTtl = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblTaxAmount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotalQty = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNoOfItems = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCustomerDetails = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCreateInvOrd = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dtGridViewProdListForSelection = new System.Windows.Forms.DataGridView();
            this.CategoryCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuantityCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectCol = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dtGridViewInvOrdProdList = new System.Windows.Forms.DataGridView();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrdQtyCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panelOrderControls = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxLoading)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewProdListForSelection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewInvOrdProdList)).BeginInit();
            this.panelOrderControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSelectName
            // 
            this.lblSelectName.Location = new System.Drawing.Point(30, 46);
            this.lblSelectName.Name = "lblSelectName";
            this.lblSelectName.Size = new System.Drawing.Size(115, 13);
            this.lblSelectName.TabIndex = 0;
            this.lblSelectName.Text = "Customer Name";
            this.lblSelectName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbBoxCustomers
            // 
            this.cmbBoxCustomers.FormattingEnabled = true;
            this.cmbBoxCustomers.Location = new System.Drawing.Point(151, 43);
            this.cmbBoxCustomers.Name = "cmbBoxCustomers";
            this.cmbBoxCustomers.Size = new System.Drawing.Size(212, 21);
            this.cmbBoxCustomers.TabIndex = 1;
            this.cmbBoxCustomers.SelectedIndexChanged += new System.EventHandler(this.cmbBoxCustomers_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(438, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Select Item";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbBoxProduct
            // 
            this.cmbBoxProduct.FormattingEnabled = true;
            this.cmbBoxProduct.Location = new System.Drawing.Point(504, 96);
            this.cmbBoxProduct.Name = "cmbBoxProduct";
            this.cmbBoxProduct.Size = new System.Drawing.Size(212, 21);
            this.cmbBoxProduct.TabIndex = 3;
            this.cmbBoxProduct.SelectedIndexChanged += new System.EventHandler(this.cmbBoxProduct_SelectedIndexChanged);
            // 
            // cmbBoxProdCat
            // 
            this.cmbBoxProdCat.FormattingEnabled = true;
            this.cmbBoxProdCat.Location = new System.Drawing.Point(151, 96);
            this.cmbBoxProdCat.Name = "cmbBoxProdCat";
            this.cmbBoxProdCat.Size = new System.Drawing.Size(212, 21);
            this.cmbBoxProdCat.TabIndex = 2;
            this.cmbBoxProdCat.SelectedIndexChanged += new System.EventHandler(this.cmbBoxProdCat_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(63, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Select Category";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtTmPckrInvOrdDate
            // 
            this.dtTmPckrInvOrdDate.CustomFormat = "dd-MMM-yyyy HH:mm:ss";
            this.dtTmPckrInvOrdDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTmPckrInvOrdDate.Location = new System.Drawing.Point(151, 17);
            this.dtTmPckrInvOrdDate.Name = "dtTmPckrInvOrdDate";
            this.dtTmPckrInvOrdDate.Size = new System.Drawing.Size(212, 20);
            this.dtTmPckrInvOrdDate.TabIndex = 0;
            // 
            // lblInvoiceDate
            // 
            this.lblInvoiceDate.Location = new System.Drawing.Point(77, 20);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Size = new System.Drawing.Size(68, 13);
            this.lblInvoiceDate.TabIndex = 0;
            this.lblInvoiceDate.Text = "Bill Date";
            this.lblInvoiceDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblInvoiceNumber
            // 
            this.lblInvoiceNumber.Location = new System.Drawing.Point(416, 46);
            this.lblInvoiceNumber.Name = "lblInvoiceNumber";
            this.lblInvoiceNumber.Size = new System.Drawing.Size(82, 13);
            this.lblInvoiceNumber.TabIndex = 0;
            this.lblInvoiceNumber.Text = "Bill Number";
            this.lblInvoiceNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBoxInvOrdNumber
            // 
            this.txtBoxInvOrdNumber.Location = new System.Drawing.Point(504, 43);
            this.txtBoxInvOrdNumber.Name = "txtBoxInvOrdNumber";
            this.txtBoxInvOrdNumber.Size = new System.Drawing.Size(84, 20);
            this.txtBoxInvOrdNumber.TabIndex = 13;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // btnCancelChanges
            // 
            this.btnCancelChanges.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancelChanges.BackColor = System.Drawing.Color.DarkOrange;
            this.btnCancelChanges.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelChanges.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelChanges.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCancelChanges.Location = new System.Drawing.Point(842, 234);
            this.btnCancelChanges.Name = "btnCancelChanges";
            this.btnCancelChanges.Size = new System.Drawing.Size(89, 38);
            this.btnCancelChanges.TabIndex = 3;
            this.btnCancelChanges.Text = "Cancel";
            this.toolTip1.SetToolTip(this.btnCancelChanges, "Cancel Changes");
            this.btnCancelChanges.UseVisualStyleBackColor = false;
            this.btnCancelChanges.Click += new System.EventHandler(this.btnCancelChanges_Click);
            // 
            // btnDiscount
            // 
            this.btnDiscount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDiscount.BackColor = System.Drawing.Color.DarkSalmon;
            this.btnDiscount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDiscount.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDiscount.Location = new System.Drawing.Point(728, 234);
            this.btnDiscount.Name = "btnDiscount";
            this.btnDiscount.Size = new System.Drawing.Size(108, 38);
            this.btnDiscount.TabIndex = 1;
            this.btnDiscount.Text = "Discount";
            this.btnDiscount.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.toolTip1.SetToolTip(this.btnDiscount, "Add Discount");
            this.btnDiscount.UseCompatibleTextRendering = true;
            this.btnDiscount.UseVisualStyleBackColor = false;
            this.btnDiscount.Click += new System.EventHandler(this.btnDiscount_Click);
            // 
            // btnRemItem
            // 
            this.btnRemItem.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnRemItem.FlatAppearance.BorderSize = 0;
            this.btnRemItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemItem.Image = ((System.Drawing.Image)(resources.GetObject("btnRemItem.Image")));
            this.btnRemItem.Location = new System.Drawing.Point(659, 73);
            this.btnRemItem.Name = "btnRemItem";
            this.btnRemItem.Size = new System.Drawing.Size(30, 27);
            this.btnRemItem.TabIndex = 4;
            this.btnRemItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnRemItem, "Remove Item");
            this.btnRemItem.UseVisualStyleBackColor = true;
            this.btnRemItem.Click += new System.EventHandler(this.btnRemItem_Click);
            // 
            // btnItemDiscount
            // 
            this.btnItemDiscount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnItemDiscount.FlatAppearance.BorderSize = 0;
            this.btnItemDiscount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnItemDiscount.Image = ((System.Drawing.Image)(resources.GetObject("btnItemDiscount.Image")));
            this.btnItemDiscount.Location = new System.Drawing.Point(659, 103);
            this.btnItemDiscount.Name = "btnItemDiscount";
            this.btnItemDiscount.Size = new System.Drawing.Size(30, 27);
            this.btnItemDiscount.TabIndex = 4;
            this.btnItemDiscount.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnItemDiscount, "Discount");
            this.btnItemDiscount.UseVisualStyleBackColor = true;
            this.btnItemDiscount.Visible = false;
            this.btnItemDiscount.Click += new System.EventHandler(this.btnItemDiscount_Click);
            // 
            // btnSelectAllToRemove
            // 
            this.btnSelectAllToRemove.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSelectAllToRemove.FlatAppearance.BorderSize = 0;
            this.btnSelectAllToRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAllToRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectAllToRemove.Image")));
            this.btnSelectAllToRemove.Location = new System.Drawing.Point(659, 40);
            this.btnSelectAllToRemove.Name = "btnSelectAllToRemove";
            this.btnSelectAllToRemove.Size = new System.Drawing.Size(30, 27);
            this.btnSelectAllToRemove.TabIndex = 4;
            this.btnSelectAllToRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSelectAllToRemove, "Select All Items");
            this.btnSelectAllToRemove.UseVisualStyleBackColor = true;
            this.btnSelectAllToRemove.Click += new System.EventHandler(this.btnSelectAllToRemove_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnAddItem.FlatAppearance.BorderSize = 0;
            this.btnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddItem.Image = ((System.Drawing.Image)(resources.GetObject("btnAddItem.Image")));
            this.btnAddItem.Location = new System.Drawing.Point(659, 60);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(30, 27);
            this.btnAddItem.TabIndex = 4;
            this.btnAddItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnAddItem, "Add Item");
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSelectAll.FlatAppearance.BorderSize = 0;
            this.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectAll.Image")));
            this.btnSelectAll.Location = new System.Drawing.Point(659, 27);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(30, 27);
            this.btnSelectAll.TabIndex = 4;
            this.btnSelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSelectAll, "Select All Items");
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // opnFileDialog
            // 
            this.opnFileDialog.Filter = "Excel File|*.xls;*.xlsx";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(9, 576);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 16);
            this.label10.TabIndex = 13;
            this.label10.Text = "Status:";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(70, 576);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(51, 16);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.Text = "Status";
            // 
            // picBoxLoading
            // 
            this.picBoxLoading.Image = ((System.Drawing.Image)(resources.GetObject("picBoxLoading.Image")));
            this.picBoxLoading.Location = new System.Drawing.Point(824, 570);
            this.picBoxLoading.Name = "picBoxLoading";
            this.picBoxLoading.Size = new System.Drawing.Size(24, 24);
            this.picBoxLoading.TabIndex = 14;
            this.picBoxLoading.TabStop = false;
            // 
            // btnAddCustomer
            // 
            this.btnAddCustomer.FlatAppearance.BorderSize = 0;
            this.btnAddCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCustomer.Image = global::SalesOrdersReport.Properties.Resources.add;
            this.btnAddCustomer.Location = new System.Drawing.Point(369, 40);
            this.btnAddCustomer.Name = "btnAddCustomer";
            this.btnAddCustomer.Size = new System.Drawing.Size(32, 24);
            this.btnAddCustomer.TabIndex = 15;
            this.btnAddCustomer.UseVisualStyleBackColor = true;
            this.btnAddCustomer.Click += new System.EventHandler(this.btnAddCustomer_Click);
            // 
            // cmbBoxBillNumber
            // 
            this.cmbBoxBillNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxBillNumber.FormattingEnabled = true;
            this.cmbBoxBillNumber.Location = new System.Drawing.Point(594, 43);
            this.cmbBoxBillNumber.Name = "cmbBoxBillNumber";
            this.cmbBoxBillNumber.Size = new System.Drawing.Size(97, 21);
            this.cmbBoxBillNumber.TabIndex = 1;
            this.cmbBoxBillNumber.SelectedIndexChanged += new System.EventHandler(this.cmbBoxBillNumber_SelectedIndexChanged);
            // 
            // btnPrintBill
            // 
            this.btnPrintBill.FlatAppearance.BorderSize = 0;
            this.btnPrintBill.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrintBill.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintBill.Image = global::SalesOrdersReport.Properties.Resources.Iconshow_Hardware_Printer;
            this.btnPrintBill.Location = new System.Drawing.Point(937, 80);
            this.btnPrintBill.Name = "btnPrintBill";
            this.btnPrintBill.Size = new System.Drawing.Size(111, 41);
            this.btnPrintBill.TabIndex = 16;
            this.btnPrintBill.Text = "Print Bill";
            this.btnPrintBill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrintBill.UseVisualStyleBackColor = true;
            this.btnPrintBill.Click += new System.EventHandler(this.btnPrintBill_Click);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(61, 72);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Phone Number";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbBoxPhoneNumbers
            // 
            this.cmbBoxPhoneNumbers.FormattingEnabled = true;
            this.cmbBoxPhoneNumbers.Location = new System.Drawing.Point(151, 69);
            this.cmbBoxPhoneNumbers.Name = "cmbBoxPhoneNumbers";
            this.cmbBoxPhoneNumbers.Size = new System.Drawing.Size(212, 21);
            this.cmbBoxPhoneNumbers.TabIndex = 1;
            this.cmbBoxPhoneNumbers.SelectedIndexChanged += new System.EventHandler(this.cmbBoxPhoneNumbers_SelectedIndexChanged);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.BackColor = System.Drawing.Color.IndianRed;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnClose.Location = new System.Drawing.Point(937, 234);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(99, 38);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblGrandTtl);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lblTaxAmount);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lblDiscount);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lblSubTotal);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblTotalQty);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblNoOfItems);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(728, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(308, 180);
            this.panel1.TabIndex = 8;
            // 
            // lblGrandTtl
            // 
            this.lblGrandTtl.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrandTtl.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblGrandTtl.Location = new System.Drawing.Point(156, 139);
            this.lblGrandTtl.Name = "lblGrandTtl";
            this.lblGrandTtl.Size = new System.Drawing.Size(117, 18);
            this.lblGrandTtl.TabIndex = 0;
            this.lblGrandTtl.Text = "Grand Total";
            this.lblGrandTtl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label8.Location = new System.Drawing.Point(3, 139);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 19);
            this.label8.TabIndex = 0;
            this.label8.Text = "Grand Total";
            // 
            // lblTaxAmount
            // 
            this.lblTaxAmount.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxAmount.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblTaxAmount.Location = new System.Drawing.Point(156, 112);
            this.lblTaxAmount.Name = "lblTaxAmount";
            this.lblTaxAmount.Size = new System.Drawing.Size(117, 18);
            this.lblTaxAmount.TabIndex = 0;
            this.lblTaxAmount.Text = "Tax";
            this.lblTaxAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label7.Location = new System.Drawing.Point(3, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 19);
            this.label7.TabIndex = 0;
            this.label7.Text = "Total Tax";
            // 
            // lblDiscount
            // 
            this.lblDiscount.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiscount.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblDiscount.Location = new System.Drawing.Point(159, 85);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(114, 18);
            this.lblDiscount.TabIndex = 0;
            this.lblDiscount.Text = "Discount";
            this.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label6.Location = new System.Drawing.Point(3, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 19);
            this.label6.TabIndex = 0;
            this.label6.Text = "Discount";
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblSubTotal.Location = new System.Drawing.Point(156, 56);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(117, 18);
            this.lblSubTotal.TabIndex = 0;
            this.lblSubTotal.Text = "Sub Total";
            this.lblSubTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label5.Location = new System.Drawing.Point(3, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 19);
            this.label5.TabIndex = 0;
            this.label5.Text = "Sub Total";
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalQty.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblTotalQty.Location = new System.Drawing.Point(153, 29);
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.Size = new System.Drawing.Size(120, 18);
            this.lblTotalQty.TabIndex = 0;
            this.lblTotalQty.Text = "Total Quantity";
            this.lblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label1.Location = new System.Drawing.Point(3, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total Quantity";
            // 
            // lblNoOfItems
            // 
            this.lblNoOfItems.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoOfItems.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblNoOfItems.Location = new System.Drawing.Point(156, 3);
            this.lblNoOfItems.Name = "lblNoOfItems";
            this.lblNoOfItems.Size = new System.Drawing.Size(117, 18);
            this.lblNoOfItems.TabIndex = 0;
            this.lblNoOfItems.Text = "No. of Items";
            this.lblNoOfItems.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "No. of Items";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.SeaShell;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblCustomerDetails);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Location = new System.Drawing.Point(728, 278);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(308, 157);
            this.panel2.TabIndex = 9;
            // 
            // lblCustomerDetails
            // 
            this.lblCustomerDetails.AutoSize = true;
            this.lblCustomerDetails.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerDetails.ForeColor = System.Drawing.Color.Gray;
            this.lblCustomerDetails.Location = new System.Drawing.Point(3, 35);
            this.lblCustomerDetails.Name = "lblCustomerDetails";
            this.lblCustomerDetails.Size = new System.Drawing.Size(120, 15);
            this.lblCustomerDetails.TabIndex = 0;
            this.lblCustomerDetails.Text = "Customer Details";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DarkOrange;
            this.label9.Location = new System.Drawing.Point(3, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(145, 18);
            this.label9.TabIndex = 0;
            this.label9.Text = "Customer Details";
            // 
            // btnCreateInvOrd
            // 
            this.btnCreateInvOrd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCreateInvOrd.BackColor = System.Drawing.Color.CadetBlue;
            this.btnCreateInvOrd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateInvOrd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateInvOrd.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCreateInvOrd.Location = new System.Drawing.Point(728, 190);
            this.btnCreateInvOrd.Name = "btnCreateInvOrd";
            this.btnCreateInvOrd.Size = new System.Drawing.Size(308, 38);
            this.btnCreateInvOrd.TabIndex = 0;
            this.btnCreateInvOrd.Text = "Create Order";
            this.btnCreateInvOrd.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnCreateInvOrd.UseCompatibleTextRendering = true;
            this.btnCreateInvOrd.UseVisualStyleBackColor = false;
            this.btnCreateInvOrd.Click += new System.EventHandler(this.btnCreateBill_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Location = new System.Drawing.Point(0, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.splitContainer1.Panel1.Controls.Add(this.btnSelectAll);
            this.splitContainer1.Panel1.Controls.Add(this.btnAddItem);
            this.splitContainer1.Panel1.Controls.Add(this.dtGridViewProdListForSelection);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.splitContainer1.Panel2.Controls.Add(this.btnSelectAllToRemove);
            this.splitContainer1.Panel2.Controls.Add(this.btnItemDiscount);
            this.splitContainer1.Panel2.Controls.Add(this.btnRemItem);
            this.splitContainer1.Panel2.Controls.Add(this.dtGridViewInvOrdProdList);
            this.splitContainer1.Size = new System.Drawing.Size(722, 432);
            this.splitContainer1.SplitterDistance = 207;
            this.splitContainer1.TabIndex = 7;
            // 
            // dtGridViewProdListForSelection
            // 
            this.dtGridViewProdListForSelection.AllowUserToAddRows = false;
            this.dtGridViewProdListForSelection.AllowUserToDeleteRows = false;
            this.dtGridViewProdListForSelection.AllowUserToResizeRows = false;
            this.dtGridViewProdListForSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGridViewProdListForSelection.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtGridViewProdListForSelection.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtGridViewProdListForSelection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewProdListForSelection.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CategoryCol,
            this.ItemCol,
            this.PriceCol,
            this.QuantityCol,
            this.SelectCol});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(1);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtGridViewProdListForSelection.DefaultCellStyle = dataGridViewCellStyle1;
            this.dtGridViewProdListForSelection.Location = new System.Drawing.Point(3, 3);
            this.dtGridViewProdListForSelection.MultiSelect = false;
            this.dtGridViewProdListForSelection.Name = "dtGridViewProdListForSelection";
            this.dtGridViewProdListForSelection.RowTemplate.Height = 30;
            this.dtGridViewProdListForSelection.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtGridViewProdListForSelection.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGridViewProdListForSelection.Size = new System.Drawing.Size(650, 191);
            this.dtGridViewProdListForSelection.TabIndex = 0;
            this.dtGridViewProdListForSelection.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGridViewProdListForSelection_CellContentClick);
            this.dtGridViewProdListForSelection.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGridViewProdListForSelection_CellEndEdit);
            this.dtGridViewProdListForSelection.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGridViewProdListForSelection_CellValueChanged);
            // 
            // CategoryCol
            // 
            this.CategoryCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CategoryCol.HeaderText = "Category";
            this.CategoryCol.MinimumWidth = 76;
            this.CategoryCol.Name = "CategoryCol";
            this.CategoryCol.ReadOnly = true;
            this.CategoryCol.Width = 76;
            // 
            // ItemCol
            // 
            this.ItemCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ItemCol.HeaderText = "Item";
            this.ItemCol.MinimumWidth = 54;
            this.ItemCol.Name = "ItemCol";
            this.ItemCol.ReadOnly = true;
            this.ItemCol.Width = 54;
            // 
            // PriceCol
            // 
            this.PriceCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PriceCol.HeaderText = "Price";
            this.PriceCol.MinimumWidth = 58;
            this.PriceCol.Name = "PriceCol";
            this.PriceCol.ReadOnly = true;
            this.PriceCol.Width = 58;
            // 
            // QuantityCol
            // 
            this.QuantityCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.QuantityCol.HeaderText = "Quantity";
            this.QuantityCol.MinimumWidth = 200;
            this.QuantityCol.Name = "QuantityCol";
            this.QuantityCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.QuantityCol.Width = 200;
            // 
            // SelectCol
            // 
            this.SelectCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SelectCol.HeaderText = "Select";
            this.SelectCol.Name = "SelectCol";
            // 
            // dtGridViewInvOrdProdList
            // 
            this.dtGridViewInvOrdProdList.AllowUserToAddRows = false;
            this.dtGridViewInvOrdProdList.AllowUserToDeleteRows = false;
            this.dtGridViewInvOrdProdList.AllowUserToResizeRows = false;
            this.dtGridViewInvOrdProdList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGridViewInvOrdProdList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtGridViewInvOrdProdList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtGridViewInvOrdProdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewInvOrdProdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column10,
            this.Column11,
            this.OrdQtyCol,
            this.Column12,
            this.Column14});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(1);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtGridViewInvOrdProdList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dtGridViewInvOrdProdList.Location = new System.Drawing.Point(3, 6);
            this.dtGridViewInvOrdProdList.MultiSelect = false;
            this.dtGridViewInvOrdProdList.Name = "dtGridViewInvOrdProdList";
            this.dtGridViewInvOrdProdList.RowTemplate.Height = 30;
            this.dtGridViewInvOrdProdList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtGridViewInvOrdProdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGridViewInvOrdProdList.Size = new System.Drawing.Size(650, 204);
            this.dtGridViewInvOrdProdList.TabIndex = 0;
            this.dtGridViewInvOrdProdList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGridViewInvOrdProdList_CellContentClick);
            this.dtGridViewInvOrdProdList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGridViewInvOrdProdList_CellEndEdit);
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column8.HeaderText = "Category";
            this.Column8.MinimumWidth = 76;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 76;
            // 
            // Column10
            // 
            this.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column10.HeaderText = "Item";
            this.Column10.MinimumWidth = 54;
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 54;
            // 
            // Column11
            // 
            this.Column11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column11.HeaderText = "Price";
            this.Column11.MinimumWidth = 58;
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 58;
            // 
            // OrdQtyCol
            // 
            this.OrdQtyCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.OrdQtyCol.HeaderText = "Order Quantity";
            this.OrdQtyCol.MinimumWidth = 100;
            this.OrdQtyCol.Name = "OrdQtyCol";
            this.OrdQtyCol.Width = 102;
            // 
            // Column12
            // 
            this.Column12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column12.HeaderText = "Sale Quantity";
            this.Column12.MinimumWidth = 100;
            this.Column12.Name = "Column12";
            // 
            // Column14
            // 
            this.Column14.HeaderText = "Select";
            this.Column14.Name = "Column14";
            // 
            // panelOrderControls
            // 
            this.panelOrderControls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOrderControls.Controls.Add(this.splitContainer1);
            this.panelOrderControls.Controls.Add(this.btnCreateInvOrd);
            this.panelOrderControls.Controls.Add(this.panel2);
            this.panelOrderControls.Controls.Add(this.btnDiscount);
            this.panelOrderControls.Controls.Add(this.panel1);
            this.panelOrderControls.Controls.Add(this.btnCancelChanges);
            this.panelOrderControls.Controls.Add(this.btnClose);
            this.panelOrderControls.Enabled = false;
            this.panelOrderControls.Location = new System.Drawing.Point(12, 123);
            this.panelOrderControls.Name = "panelOrderControls";
            this.panelOrderControls.Size = new System.Drawing.Size(1055, 441);
            this.panelOrderControls.TabIndex = 12;
            // 
            // CreatePOSBillForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1079, 601);
            this.Controls.Add(this.btnPrintBill);
            this.Controls.Add(this.btnAddCustomer);
            this.Controls.Add(this.picBoxLoading);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBoxInvOrdNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtTmPckrInvOrdDate);
            this.Controls.Add(this.cmbBoxBillNumber);
            this.Controls.Add(this.cmbBoxPhoneNumbers);
            this.Controls.Add(this.cmbBoxCustomers);
            this.Controls.Add(this.cmbBoxProdCat);
            this.Controls.Add(this.lblInvoiceNumber);
            this.Controls.Add(this.cmbBoxProduct);
            this.Controls.Add(this.lblInvoiceDate);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblSelectName);
            this.Controls.Add(this.panelOrderControls);
            this.Name = "CreatePOSBillForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "\';;";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CustomerInvoiceSellerOrderForm_FormClosing);
            this.Load += new System.EventHandler(this.CreatePOSBillForm_Load);
            this.Shown += new System.EventHandler(this.CreatePOSBillForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxLoading)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewProdListForSelection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewInvOrdProdList)).EndInit();
            this.panelOrderControls.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSelectName;
        private System.Windows.Forms.ComboBox cmbBoxCustomers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbBoxProduct;
        private System.Windows.Forms.ComboBox cmbBoxProdCat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtTmPckrInvOrdDate;
        private System.Windows.Forms.Label lblInvoiceDate;
        private System.Windows.Forms.Label lblInvoiceNumber;
        private System.Windows.Forms.TextBox txtBoxInvOrdNumber;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.OpenFileDialog opnFileDialog;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.PictureBox picBoxLoading;
        private System.Windows.Forms.Button btnAddCustomer;
        private System.Windows.Forms.ComboBox cmbBoxBillNumber;
        private System.Windows.Forms.Button btnPrintBill;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbBoxPhoneNumbers;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancelChanges;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblGrandTtl;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTaxAmount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSubTotal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotalQty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNoOfItems;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDiscount;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblCustomerDetails;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCreateInvOrd;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.DataGridView dtGridViewProdListForSelection;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuantityCol;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectCol;
        private System.Windows.Forms.Button btnSelectAllToRemove;
        private System.Windows.Forms.Button btnItemDiscount;
        private System.Windows.Forms.Button btnRemItem;
        private System.Windows.Forms.DataGridView dtGridViewInvOrdProdList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrdQtyCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column14;
        private System.Windows.Forms.Panel panelOrderControls;
    }
}