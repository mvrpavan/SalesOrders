namespace SalesOrdersReport
{
    partial class CustomerInvoiceSellerOrderForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblSelectName = new System.Windows.Forms.Label();
            this.cmbBoxSellerCustomer = new System.Windows.Forms.ComboBox();
            this.dtGridViewProdListForSelection = new System.Windows.Forms.DataGridView();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBoxProduct = new System.Windows.Forms.ComboBox();
            this.cmbBoxProdCat = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtTmPckrInvOrdDate = new System.Windows.Forms.DateTimePicker();
            this.lblInvoiceDate = new System.Windows.Forms.Label();
            this.lblInvoiceNumber = new System.Windows.Forms.Label();
            this.txtBoxInvOrdNumber = new System.Windows.Forms.TextBox();
            this.btnCnclInvOrd = new System.Windows.Forms.Button();
            this.btnCreateInvOrd = new System.Windows.Forms.Button();
            this.dtGridViewInvOrdProdList = new System.Windows.Forms.DataGridView();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnSelectAllToRemove = new System.Windows.Forms.Button();
            this.btnItemDiscount = new System.Windows.Forms.Button();
            this.btnRemItem = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
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
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCustomerDetails = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnDiscount = new System.Windows.Forms.Button();
            this.btnHoldOrder = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtSalesOrderFilePath = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.opnFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panelOrderControls = new System.Windows.Forms.Panel();
            this.btnBrowseSalesOrderFile = new System.Windows.Forms.Button();
            this.btnUpdateSalesOrder = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewProdListForSelection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewInvOrdProdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelOrderControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSelectName
            // 
            this.lblSelectName.AutoSize = true;
            this.lblSelectName.Location = new System.Drawing.Point(30, 72);
            this.lblSelectName.Name = "lblSelectName";
            this.lblSelectName.Size = new System.Drawing.Size(115, 13);
            this.lblSelectName.TabIndex = 0;
            this.lblSelectName.Text = "Select Seller/Customer";
            // 
            // cmbBoxSellerCustomer
            // 
            this.cmbBoxSellerCustomer.FormattingEnabled = true;
            this.cmbBoxSellerCustomer.Location = new System.Drawing.Point(151, 69);
            this.cmbBoxSellerCustomer.Name = "cmbBoxSellerCustomer";
            this.cmbBoxSellerCustomer.Size = new System.Drawing.Size(212, 21);
            this.cmbBoxSellerCustomer.TabIndex = 1;
            this.cmbBoxSellerCustomer.SelectedIndexChanged += new System.EventHandler(this.cmbBoxSellerCustomer_SelectedIndexChanged);
            // 
            // dtGridViewProdListForSelection
            // 
            this.dtGridViewProdListForSelection.AllowUserToAddRows = false;
            this.dtGridViewProdListForSelection.AllowUserToDeleteRows = false;
            this.dtGridViewProdListForSelection.AllowUserToResizeRows = false;
            this.dtGridViewProdListForSelection.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtGridViewProdListForSelection.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtGridViewProdListForSelection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewProdListForSelection.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.Column2,
            this.Column4,
            this.Column3,
            this.Column6});
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
            this.dtGridViewProdListForSelection.Size = new System.Drawing.Size(460, 195);
            this.dtGridViewProdListForSelection.TabIndex = 2;
            this.dtGridViewProdListForSelection.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGridViewProdListForSelection_CellContentClick);
            this.dtGridViewProdListForSelection.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGridViewProdListForSelection_CellEndEdit);
            this.dtGridViewProdListForSelection.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGridViewProdListForSelection_CellValueChanged);
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column7.HeaderText = "Category";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 76;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.HeaderText = "Item";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 54;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column4.HeaderText = "Price";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 58;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column3.HeaderText = "Quantity";
            this.Column3.Name = "Column3";
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 54;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column6.HeaderText = "Select";
            this.Column6.Name = "Column6";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(351, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Select Item";
            // 
            // cmbBoxProduct
            // 
            this.cmbBoxProduct.FormattingEnabled = true;
            this.cmbBoxProduct.Location = new System.Drawing.Point(413, 96);
            this.cmbBoxProduct.Name = "cmbBoxProduct";
            this.cmbBoxProduct.Size = new System.Drawing.Size(253, 21);
            this.cmbBoxProduct.TabIndex = 1;
            this.cmbBoxProduct.SelectedIndexChanged += new System.EventHandler(this.cmbBoxProduct_SelectedIndexChanged);
            // 
            // cmbBoxProdCat
            // 
            this.cmbBoxProdCat.FormattingEnabled = true;
            this.cmbBoxProdCat.Location = new System.Drawing.Point(151, 96);
            this.cmbBoxProdCat.Name = "cmbBoxProdCat";
            this.cmbBoxProdCat.Size = new System.Drawing.Size(173, 21);
            this.cmbBoxProdCat.TabIndex = 0;
            this.cmbBoxProdCat.SelectedIndexChanged += new System.EventHandler(this.cmbBoxProdCat_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Select Category";
            // 
            // dtTmPckrInvOrdDate
            // 
            this.dtTmPckrInvOrdDate.Location = new System.Drawing.Point(151, 17);
            this.dtTmPckrInvOrdDate.Name = "dtTmPckrInvOrdDate";
            this.dtTmPckrInvOrdDate.Size = new System.Drawing.Size(212, 20);
            this.dtTmPckrInvOrdDate.TabIndex = 5;
            this.dtTmPckrInvOrdDate.ValueChanged += new System.EventHandler(this.dtTmPckrInvOrdDate_ValueChanged);
            // 
            // lblInvoiceDate
            // 
            this.lblInvoiceDate.AutoSize = true;
            this.lblInvoiceDate.Location = new System.Drawing.Point(30, 20);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Size = new System.Drawing.Size(68, 13);
            this.lblInvoiceDate.TabIndex = 0;
            this.lblInvoiceDate.Text = "Invoice Date";
            // 
            // lblInvoiceNumber
            // 
            this.lblInvoiceNumber.Location = new System.Drawing.Point(416, 20);
            this.lblInvoiceNumber.Name = "lblInvoiceNumber";
            this.lblInvoiceNumber.Size = new System.Drawing.Size(82, 13);
            this.lblInvoiceNumber.TabIndex = 0;
            this.lblInvoiceNumber.Text = "Invoice Number";
            this.lblInvoiceNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBoxInvOrdNumber
            // 
            this.txtBoxInvOrdNumber.Location = new System.Drawing.Point(504, 17);
            this.txtBoxInvOrdNumber.Name = "txtBoxInvOrdNumber";
            this.txtBoxInvOrdNumber.Size = new System.Drawing.Size(84, 20);
            this.txtBoxInvOrdNumber.TabIndex = 6;
            // 
            // btnCnclInvOrd
            // 
            this.btnCnclInvOrd.BackColor = System.Drawing.Color.IndianRed;
            this.btnCnclInvOrd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCnclInvOrd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCnclInvOrd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCnclInvOrd.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCnclInvOrd.Location = new System.Drawing.Point(674, 234);
            this.btnCnclInvOrd.Name = "btnCnclInvOrd";
            this.btnCnclInvOrd.Size = new System.Drawing.Size(91, 38);
            this.btnCnclInvOrd.TabIndex = 3;
            this.btnCnclInvOrd.Text = "Void Order";
            this.btnCnclInvOrd.UseVisualStyleBackColor = false;
            this.btnCnclInvOrd.Click += new System.EventHandler(this.btnCnclInvOrd_Click);
            // 
            // btnCreateInvOrd
            // 
            this.btnCreateInvOrd.BackColor = System.Drawing.Color.CadetBlue;
            this.btnCreateInvOrd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCreateInvOrd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateInvOrd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateInvOrd.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCreateInvOrd.Location = new System.Drawing.Point(528, 190);
            this.btnCreateInvOrd.Name = "btnCreateInvOrd";
            this.btnCreateInvOrd.Size = new System.Drawing.Size(308, 38);
            this.btnCreateInvOrd.TabIndex = 3;
            this.btnCreateInvOrd.Text = "Create Order";
            this.btnCreateInvOrd.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnCreateInvOrd.UseCompatibleTextRendering = true;
            this.btnCreateInvOrd.UseVisualStyleBackColor = false;
            this.btnCreateInvOrd.Click += new System.EventHandler(this.btnCreateInvOrd_Click);
            // 
            // dtGridViewInvOrdProdList
            // 
            this.dtGridViewInvOrdProdList.AllowUserToAddRows = false;
            this.dtGridViewInvOrdProdList.AllowUserToDeleteRows = false;
            this.dtGridViewInvOrdProdList.AllowUserToResizeRows = false;
            this.dtGridViewInvOrdProdList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtGridViewInvOrdProdList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtGridViewInvOrdProdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewInvOrdProdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column10,
            this.Column11,
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
            this.dtGridViewInvOrdProdList.Location = new System.Drawing.Point(3, 3);
            this.dtGridViewInvOrdProdList.Name = "dtGridViewInvOrdProdList";
            this.dtGridViewInvOrdProdList.RowTemplate.Height = 30;
            this.dtGridViewInvOrdProdList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtGridViewInvOrdProdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGridViewInvOrdProdList.Size = new System.Drawing.Size(460, 203);
            this.dtGridViewInvOrdProdList.TabIndex = 2;
            this.dtGridViewInvOrdProdList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGridViewInvOrdProdList_CellContentClick);
            this.dtGridViewInvOrdProdList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGridViewInvOrdProdList_CellEndEdit);
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column8.HeaderText = "Category";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 76;
            // 
            // Column10
            // 
            this.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column10.HeaderText = "Item";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 54;
            // 
            // Column11
            // 
            this.Column11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column11.HeaderText = "Price";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 58;
            // 
            // Column12
            // 
            this.Column12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column12.HeaderText = "Quantity";
            this.Column12.Name = "Column12";
            this.Column12.Width = 73;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "Select";
            this.Column14.Name = "Column14";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
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
            this.splitContainer1.Size = new System.Drawing.Size(511, 432);
            this.splitContainer1.SplitterDistance = 211;
            this.splitContainer1.TabIndex = 7;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.FlatAppearance.BorderSize = 0;
            this.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAll.Image = global::SalesOrdersReport.Properties.Resources.list__2_;
            this.btnSelectAll.Location = new System.Drawing.Point(469, 26);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(30, 27);
            this.btnSelectAll.TabIndex = 4;
            this.btnSelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSelectAll, "Select All Items");
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.FlatAppearance.BorderSize = 0;
            this.btnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddItem.Image = global::SalesOrdersReport.Properties.Resources.add;
            this.btnAddItem.Location = new System.Drawing.Point(469, 59);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(30, 27);
            this.btnAddItem.TabIndex = 4;
            this.btnAddItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnAddItem, "Add Item");
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnSelectAllToRemove
            // 
            this.btnSelectAllToRemove.FlatAppearance.BorderSize = 0;
            this.btnSelectAllToRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAllToRemove.Image = global::SalesOrdersReport.Properties.Resources.list__2_;
            this.btnSelectAllToRemove.Location = new System.Drawing.Point(469, 25);
            this.btnSelectAllToRemove.Name = "btnSelectAllToRemove";
            this.btnSelectAllToRemove.Size = new System.Drawing.Size(30, 27);
            this.btnSelectAllToRemove.TabIndex = 4;
            this.btnSelectAllToRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnSelectAllToRemove, "Select All Items");
            this.btnSelectAllToRemove.UseVisualStyleBackColor = true;
            this.btnSelectAllToRemove.Click += new System.EventHandler(this.btnSelectAllToRemove_Click);
            // 
            // btnItemDiscount
            // 
            this.btnItemDiscount.FlatAppearance.BorderSize = 0;
            this.btnItemDiscount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnItemDiscount.Image = global::SalesOrdersReport.Properties.Resources.discount;
            this.btnItemDiscount.Location = new System.Drawing.Point(469, 88);
            this.btnItemDiscount.Name = "btnItemDiscount";
            this.btnItemDiscount.Size = new System.Drawing.Size(30, 27);
            this.btnItemDiscount.TabIndex = 4;
            this.btnItemDiscount.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnItemDiscount, "Discount");
            this.btnItemDiscount.UseVisualStyleBackColor = true;
            this.btnItemDiscount.Visible = false;
            this.btnItemDiscount.Click += new System.EventHandler(this.btnItemDiscount_Click);
            // 
            // btnRemItem
            // 
            this.btnRemItem.FlatAppearance.BorderSize = 0;
            this.btnRemItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemItem.Image = global::SalesOrdersReport.Properties.Resources.negative;
            this.btnRemItem.Location = new System.Drawing.Point(469, 58);
            this.btnRemItem.Name = "btnRemItem";
            this.btnRemItem.Size = new System.Drawing.Size(30, 27);
            this.btnRemItem.TabIndex = 4;
            this.btnRemItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnRemItem, "Remove Item");
            this.btnRemItem.UseVisualStyleBackColor = true;
            this.btnRemItem.Click += new System.EventHandler(this.btnRemItem_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // panel1
            // 
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
            this.panel1.Location = new System.Drawing.Point(528, 4);
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
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.IndianRed;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnClose.Location = new System.Drawing.Point(771, 234);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(65, 38);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SeaShell;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblCustomerDetails);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Location = new System.Drawing.Point(528, 278);
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
            this.label9.ForeColor = System.Drawing.Color.DimGray;
            this.label9.Location = new System.Drawing.Point(3, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(145, 18);
            this.label9.TabIndex = 0;
            this.label9.Text = "Customer Details";
            // 
            // btnDiscount
            // 
            this.btnDiscount.BackColor = System.Drawing.Color.DarkSalmon;
            this.btnDiscount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDiscount.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDiscount.Location = new System.Drawing.Point(528, 234);
            this.btnDiscount.Name = "btnDiscount";
            this.btnDiscount.Size = new System.Drawing.Size(74, 38);
            this.btnDiscount.TabIndex = 3;
            this.btnDiscount.Text = "Discount";
            this.btnDiscount.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnDiscount.UseCompatibleTextRendering = true;
            this.btnDiscount.UseVisualStyleBackColor = false;
            this.btnDiscount.Click += new System.EventHandler(this.btnDiscount_Click);
            // 
            // btnHoldOrder
            // 
            this.btnHoldOrder.BackColor = System.Drawing.Color.Goldenrod;
            this.btnHoldOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHoldOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHoldOrder.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnHoldOrder.Location = new System.Drawing.Point(608, 234);
            this.btnHoldOrder.Name = "btnHoldOrder";
            this.btnHoldOrder.Size = new System.Drawing.Size(60, 38);
            this.btnHoldOrder.TabIndex = 3;
            this.btnHoldOrder.Text = "Hold";
            this.btnHoldOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnHoldOrder.UseCompatibleTextRendering = true;
            this.btnHoldOrder.UseVisualStyleBackColor = false;
            this.btnHoldOrder.Click += new System.EventHandler(this.btnHoldOrder_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // txtSalesOrderFilePath
            // 
            this.txtSalesOrderFilePath.Location = new System.Drawing.Point(151, 43);
            this.txtSalesOrderFilePath.Name = "txtSalesOrderFilePath";
            this.txtSalesOrderFilePath.Size = new System.Drawing.Size(214, 20);
            this.txtSalesOrderFilePath.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(30, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Sales Order File";
            // 
            // opnFileDialog
            // 
            this.opnFileDialog.Filter = "Excel File|*.xls;*.xlsx";
            // 
            // panelOrderControls
            // 
            this.panelOrderControls.Controls.Add(this.splitContainer1);
            this.panelOrderControls.Controls.Add(this.btnCreateInvOrd);
            this.panelOrderControls.Controls.Add(this.panel2);
            this.panelOrderControls.Controls.Add(this.btnDiscount);
            this.panelOrderControls.Controls.Add(this.panel1);
            this.panelOrderControls.Controls.Add(this.btnHoldOrder);
            this.panelOrderControls.Controls.Add(this.btnCnclInvOrd);
            this.panelOrderControls.Controls.Add(this.btnClose);
            this.panelOrderControls.Enabled = false;
            this.panelOrderControls.Location = new System.Drawing.Point(12, 123);
            this.panelOrderControls.Name = "panelOrderControls";
            this.panelOrderControls.Size = new System.Drawing.Size(836, 441);
            this.panelOrderControls.TabIndex = 12;
            // 
            // btnBrowseSalesOrderFile
            // 
            this.btnBrowseSalesOrderFile.FlatAppearance.BorderSize = 0;
            this.btnBrowseSalesOrderFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseSalesOrderFile.Image = global::SalesOrdersReport.Properties.Resources.open_archive;
            this.btnBrowseSalesOrderFile.Location = new System.Drawing.Point(371, 36);
            this.btnBrowseSalesOrderFile.Name = "btnBrowseSalesOrderFile";
            this.btnBrowseSalesOrderFile.Size = new System.Drawing.Size(30, 27);
            this.btnBrowseSalesOrderFile.TabIndex = 10;
            this.btnBrowseSalesOrderFile.UseVisualStyleBackColor = true;
            this.btnBrowseSalesOrderFile.Click += new System.EventHandler(this.btnSalesOrderFilePath_Click);
            // 
            // btnUpdateSalesOrder
            // 
            this.btnUpdateSalesOrder.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnUpdateSalesOrder.FlatAppearance.BorderSize = 0;
            this.btnUpdateSalesOrder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUpdateSalesOrder.Location = new System.Drawing.Point(704, 17);
            this.btnUpdateSalesOrder.Name = "btnUpdateSalesOrder";
            this.btnUpdateSalesOrder.Size = new System.Drawing.Size(139, 73);
            this.btnUpdateSalesOrder.TabIndex = 13;
            this.btnUpdateSalesOrder.Text = "Update Sales Order";
            this.btnUpdateSalesOrder.UseVisualStyleBackColor = false;
            this.btnUpdateSalesOrder.Click += new System.EventHandler(this.btnUpdateSalesOrder_Click);
            // 
            // CustomerInvoiceSellerOrderForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(855, 570);
            this.Controls.Add(this.btnUpdateSalesOrder);
            this.Controls.Add(this.txtSalesOrderFilePath);
            this.Controls.Add(this.btnBrowseSalesOrderFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBoxInvOrdNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtTmPckrInvOrdDate);
            this.Controls.Add(this.cmbBoxSellerCustomer);
            this.Controls.Add(this.cmbBoxProdCat);
            this.Controls.Add(this.lblInvoiceNumber);
            this.Controls.Add(this.cmbBoxProduct);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblInvoiceDate);
            this.Controls.Add(this.lblSelectName);
            this.Controls.Add(this.panelOrderControls);
            this.Name = "CustomerInvoiceSellerOrderForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CustomerInvoiceForm";
            this.Load += new System.EventHandler(this.CustomerInvoiceForm_Load);
            this.Shown += new System.EventHandler(this.CustomerInvoiceSellerOrderForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewProdListForSelection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewInvOrdProdList)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelOrderControls.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSelectName;
        private System.Windows.Forms.ComboBox cmbBoxSellerCustomer;
        private System.Windows.Forms.DataGridView dtGridViewProdListForSelection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbBoxProduct;
        private System.Windows.Forms.ComboBox cmbBoxProdCat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtTmPckrInvOrdDate;
        private System.Windows.Forms.Label lblInvoiceDate;
        private System.Windows.Forms.Label lblInvoiceNumber;
        private System.Windows.Forms.TextBox txtBoxInvOrdNumber;
        private System.Windows.Forms.Button btnCnclInvOrd;
        private System.Windows.Forms.Button btnCreateInvOrd;
        private System.Windows.Forms.DataGridView dtGridViewInvOrdProdList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnRemItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblCustomerDetails;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column6;
        private System.Windows.Forms.Label lblGrandTtl;
        private System.Windows.Forms.Label lblTaxAmount;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label lblSubTotal;
        private System.Windows.Forms.Label lblTotalQty;
        private System.Windows.Forms.Label lblNoOfItems;
        private System.Windows.Forms.Button btnDiscount;
        private System.Windows.Forms.Button btnHoldOrder;
        private System.Windows.Forms.Button btnItemDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column14;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnSelectAllToRemove;
        private System.Windows.Forms.Button btnBrowseSalesOrderFile;
        private System.Windows.Forms.TextBox txtSalesOrderFilePath;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.OpenFileDialog opnFileDialog;
        private System.Windows.Forms.Panel panelOrderControls;
        private System.Windows.Forms.Button btnUpdateSalesOrder;
    }
}