namespace SalesOrdersReport.Views
{
    partial class AddProductForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxSKUID = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBoxRetailPrice = new System.Windows.Forms.TextBox();
            this.txtBoxWholePrice = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtBoxMaxRetailPrice = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBoxPurchasePrice = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.chkBoxChooseExistingStock = new System.Windows.Forms.CheckBox();
            this.txtBoxStockName = new System.Windows.Forms.TextBox();
            this.cmbBoxStockList = new System.Windows.Forms.ComboBox();
            this.cmbBoxStockMeasurementUnitList = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtBoxReOrderQty = new System.Windows.Forms.TextBox();
            this.txtBoxReOrderLevel = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtBoxStockQty = new System.Windows.Forms.TextBox();
            this.txtBoxStockUnits = new System.Windows.Forms.TextBox();
            this.chkBoxIsActive = new System.Windows.Forms.CheckBox();
            this.btnEditHSNCode = new System.Windows.Forms.Button();
            this.btnAddHSNCode = new System.Windows.Forms.Button();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.cmbBoxHSNCodeList = new System.Windows.Forms.ComboBox();
            this.cmbBoxMeasurementUnitList = new System.Windows.Forms.ComboBox();
            this.txtBoxSortName = new System.Windows.Forms.TextBox();
            this.cmbBoxCategoryList = new System.Windows.Forms.ComboBox();
            this.txtBoxUnits = new System.Windows.Forms.TextBox();
            this.txtBoxProductDesc = new System.Windows.Forms.TextBox();
            this.txtBoxProductName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errorProviderAddProductForm = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnCreateUpdateClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderAddProductForm)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SKU";
            // 
            // txtBoxSKUID
            // 
            this.txtBoxSKUID.Location = new System.Drawing.Point(100, 37);
            this.txtBoxSKUID.Name = "txtBoxSKUID";
            this.txtBoxSKUID.ReadOnly = true;
            this.txtBoxSKUID.Size = new System.Drawing.Size(121, 20);
            this.txtBoxSKUID.TabIndex = 1000;
            this.txtBoxSKUID.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.chkBoxIsActive);
            this.groupBox1.Controls.Add(this.btnEditHSNCode);
            this.groupBox1.Controls.Add(this.btnAddHSNCode);
            this.groupBox1.Controls.Add(this.btnAddCategory);
            this.groupBox1.Controls.Add(this.cmbBoxHSNCodeList);
            this.groupBox1.Controls.Add(this.cmbBoxMeasurementUnitList);
            this.groupBox1.Controls.Add(this.txtBoxSortName);
            this.groupBox1.Controls.Add(this.cmbBoxCategoryList);
            this.groupBox1.Controls.Add(this.txtBoxUnits);
            this.groupBox1.Controls.Add(this.txtBoxProductDesc);
            this.groupBox1.Controls.Add(this.txtBoxProductName);
            this.groupBox1.Controls.Add(this.txtBoxSKUID);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(536, 528);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Product Details";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtBoxRetailPrice);
            this.groupBox3.Controls.Add(this.txtBoxWholePrice);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.txtBoxMaxRetailPrice);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtBoxPurchasePrice);
            this.groupBox3.Location = new System.Drawing.Point(7, 414);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(529, 104);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Price Details";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(305, 60);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(61, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "Retail Price";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Wholesale Price";
            // 
            // txtBoxRetailPrice
            // 
            this.txtBoxRetailPrice.Location = new System.Drawing.Point(372, 57);
            this.txtBoxRetailPrice.Name = "txtBoxRetailPrice";
            this.txtBoxRetailPrice.Size = new System.Drawing.Size(121, 20);
            this.txtBoxRetailPrice.TabIndex = 3;
            this.txtBoxRetailPrice.Validating += new System.ComponentModel.CancelEventHandler(this.Value_Validating);
            // 
            // txtBoxWholePrice
            // 
            this.txtBoxWholePrice.Location = new System.Drawing.Point(93, 57);
            this.txtBoxWholePrice.Name = "txtBoxWholePrice";
            this.txtBoxWholePrice.Size = new System.Drawing.Size(121, 20);
            this.txtBoxWholePrice.TabIndex = 1;
            this.txtBoxWholePrice.Validating += new System.ComponentModel.CancelEventHandler(this.Value_Validating);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(279, 34);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(87, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "Max. Retail Price";
            // 
            // txtBoxMaxRetailPrice
            // 
            this.txtBoxMaxRetailPrice.Location = new System.Drawing.Point(372, 31);
            this.txtBoxMaxRetailPrice.Name = "txtBoxMaxRetailPrice";
            this.txtBoxMaxRetailPrice.Size = new System.Drawing.Size(121, 20);
            this.txtBoxMaxRetailPrice.TabIndex = 2;
            this.txtBoxMaxRetailPrice.Validating += new System.ComponentModel.CancelEventHandler(this.Value_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Purchase Price";
            // 
            // txtBoxPurchasePrice
            // 
            this.txtBoxPurchasePrice.Location = new System.Drawing.Point(93, 31);
            this.txtBoxPurchasePrice.Name = "txtBoxPurchasePrice";
            this.txtBoxPurchasePrice.Size = new System.Drawing.Size(121, 20);
            this.txtBoxPurchasePrice.TabIndex = 0;
            this.txtBoxPurchasePrice.Validating += new System.ComponentModel.CancelEventHandler(this.Value_Validating);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.chkBoxChooseExistingStock);
            this.groupBox2.Controls.Add(this.txtBoxStockName);
            this.groupBox2.Controls.Add(this.cmbBoxStockList);
            this.groupBox2.Controls.Add(this.cmbBoxStockMeasurementUnitList);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.txtBoxReOrderQty);
            this.groupBox2.Controls.Add(this.txtBoxReOrderLevel);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txtBoxStockQty);
            this.groupBox2.Controls.Add(this.txtBoxStockUnits);
            this.groupBox2.Location = new System.Drawing.Point(6, 225);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(524, 183);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stock Details";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 57);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Existing Stock";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(22, 31);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Stock Name";
            // 
            // chkBoxChooseExistingStock
            // 
            this.chkBoxChooseExistingStock.AutoSize = true;
            this.chkBoxChooseExistingStock.Location = new System.Drawing.Point(309, 30);
            this.chkBoxChooseExistingStock.Name = "chkBoxChooseExistingStock";
            this.chkBoxChooseExistingStock.Size = new System.Drawing.Size(127, 17);
            this.chkBoxChooseExistingStock.TabIndex = 2;
            this.chkBoxChooseExistingStock.Text = "Choose From Existing";
            this.chkBoxChooseExistingStock.UseVisualStyleBackColor = true;
            this.chkBoxChooseExistingStock.CheckedChanged += new System.EventHandler(this.chkBoxChooseExistingStock_CheckedChanged);
            // 
            // txtBoxStockName
            // 
            this.txtBoxStockName.Location = new System.Drawing.Point(94, 28);
            this.txtBoxStockName.Name = "txtBoxStockName";
            this.txtBoxStockName.Size = new System.Drawing.Size(195, 20);
            this.txtBoxStockName.TabIndex = 1;
            this.txtBoxStockName.Validating += new System.ComponentModel.CancelEventHandler(this.Name_Validating);
            // 
            // cmbBoxStockList
            // 
            this.cmbBoxStockList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxStockList.FormattingEnabled = true;
            this.cmbBoxStockList.Location = new System.Drawing.Point(94, 54);
            this.cmbBoxStockList.Name = "cmbBoxStockList";
            this.cmbBoxStockList.Size = new System.Drawing.Size(195, 21);
            this.cmbBoxStockList.TabIndex = 3;
            this.cmbBoxStockList.SelectedIndexChanged += new System.EventHandler(this.cmbBoxStockList_SelectedIndexChanged);
            // 
            // cmbBoxStockMeasurementUnitList
            // 
            this.cmbBoxStockMeasurementUnitList.CausesValidation = false;
            this.cmbBoxStockMeasurementUnitList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxStockMeasurementUnitList.FormattingEnabled = true;
            this.cmbBoxStockMeasurementUnitList.Location = new System.Drawing.Point(373, 104);
            this.cmbBoxStockMeasurementUnitList.Name = "cmbBoxStockMeasurementUnitList";
            this.cmbBoxStockMeasurementUnitList.Size = new System.Drawing.Size(121, 21);
            this.cmbBoxStockMeasurementUnitList.TabIndex = 6;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(257, 107);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(110, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Units of Measurement";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(278, 133);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(89, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "ReOrder Quantity";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 133);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(76, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "ReOrder Level";
            // 
            // txtBoxReOrderQty
            // 
            this.txtBoxReOrderQty.Location = new System.Drawing.Point(373, 130);
            this.txtBoxReOrderQty.Name = "txtBoxReOrderQty";
            this.txtBoxReOrderQty.Size = new System.Drawing.Size(121, 20);
            this.txtBoxReOrderQty.TabIndex = 8;
            this.txtBoxReOrderQty.Validating += new System.ComponentModel.CancelEventHandler(this.Value_Validating);
            // 
            // txtBoxReOrderLevel
            // 
            this.txtBoxReOrderLevel.Location = new System.Drawing.Point(94, 130);
            this.txtBoxReOrderLevel.Name = "txtBoxReOrderLevel";
            this.txtBoxReOrderLevel.Size = new System.Drawing.Size(121, 20);
            this.txtBoxReOrderLevel.TabIndex = 7;
            this.txtBoxReOrderLevel.Validating += new System.ComponentModel.CancelEventHandler(this.Value_Validating);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(42, 84);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(46, 13);
            this.label19.TabIndex = 0;
            this.label19.Text = "Quantity";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(57, 107);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(31, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Units";
            // 
            // txtBoxStockQty
            // 
            this.txtBoxStockQty.Location = new System.Drawing.Point(94, 81);
            this.txtBoxStockQty.Name = "txtBoxStockQty";
            this.txtBoxStockQty.Size = new System.Drawing.Size(121, 20);
            this.txtBoxStockQty.TabIndex = 4;
            this.txtBoxStockQty.Validating += new System.ComponentModel.CancelEventHandler(this.Value_Validating);
            // 
            // txtBoxStockUnits
            // 
            this.txtBoxStockUnits.Location = new System.Drawing.Point(94, 104);
            this.txtBoxStockUnits.Name = "txtBoxStockUnits";
            this.txtBoxStockUnits.Size = new System.Drawing.Size(121, 20);
            this.txtBoxStockUnits.TabIndex = 5;
            this.txtBoxStockUnits.Validating += new System.ComponentModel.CancelEventHandler(this.Value_Validating);
            // 
            // chkBoxIsActive
            // 
            this.chkBoxIsActive.AutoSize = true;
            this.chkBoxIsActive.Location = new System.Drawing.Point(379, 62);
            this.chkBoxIsActive.Name = "chkBoxIsActive";
            this.chkBoxIsActive.Size = new System.Drawing.Size(56, 17);
            this.chkBoxIsActive.TabIndex = 2;
            this.chkBoxIsActive.Text = "Active";
            this.chkBoxIsActive.UseVisualStyleBackColor = true;
            // 
            // btnEditHSNCode
            // 
            this.btnEditHSNCode.FlatAppearance.BorderSize = 0;
            this.btnEditHSNCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditHSNCode.Image = global::SalesOrdersReport.Properties.Resources.edit_;
            this.btnEditHSNCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditHSNCode.Location = new System.Drawing.Point(259, 196);
            this.btnEditHSNCode.Name = "btnEditHSNCode";
            this.btnEditHSNCode.Size = new System.Drawing.Size(26, 23);
            this.btnEditHSNCode.TabIndex = 11;
            this.btnEditHSNCode.UseVisualStyleBackColor = true;
            this.btnEditHSNCode.Click += new System.EventHandler(this.btnEditHSNCode_Click);
            // 
            // btnAddHSNCode
            // 
            this.btnAddHSNCode.FlatAppearance.BorderSize = 0;
            this.btnAddHSNCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddHSNCode.Image = global::SalesOrdersReport.Properties.Resources.add;
            this.btnAddHSNCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddHSNCode.Location = new System.Drawing.Point(227, 196);
            this.btnAddHSNCode.Name = "btnAddHSNCode";
            this.btnAddHSNCode.Size = new System.Drawing.Size(26, 23);
            this.btnAddHSNCode.TabIndex = 10;
            this.btnAddHSNCode.UseVisualStyleBackColor = true;
            this.btnAddHSNCode.Click += new System.EventHandler(this.btnAddHSNCode_Click);
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.FlatAppearance.BorderSize = 0;
            this.btnAddCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCategory.Image = global::SalesOrdersReport.Properties.Resources.add;
            this.btnAddCategory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddCategory.Location = new System.Drawing.Point(295, 113);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(26, 23);
            this.btnAddCategory.TabIndex = 5;
            this.btnAddCategory.UseVisualStyleBackColor = true;
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            // 
            // cmbBoxHSNCodeList
            // 
            this.cmbBoxHSNCodeList.CausesValidation = false;
            this.cmbBoxHSNCodeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxHSNCodeList.FormattingEnabled = true;
            this.cmbBoxHSNCodeList.Location = new System.Drawing.Point(100, 198);
            this.cmbBoxHSNCodeList.Name = "cmbBoxHSNCodeList";
            this.cmbBoxHSNCodeList.Size = new System.Drawing.Size(121, 21);
            this.cmbBoxHSNCodeList.TabIndex = 9;
            // 
            // cmbBoxMeasurementUnitList
            // 
            this.cmbBoxMeasurementUnitList.CausesValidation = false;
            this.cmbBoxMeasurementUnitList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxMeasurementUnitList.FormattingEnabled = true;
            this.cmbBoxMeasurementUnitList.Location = new System.Drawing.Point(379, 142);
            this.cmbBoxMeasurementUnitList.Name = "cmbBoxMeasurementUnitList";
            this.cmbBoxMeasurementUnitList.Size = new System.Drawing.Size(121, 21);
            this.cmbBoxMeasurementUnitList.TabIndex = 7;
            // 
            // txtBoxSortName
            // 
            this.txtBoxSortName.Location = new System.Drawing.Point(100, 168);
            this.txtBoxSortName.Name = "txtBoxSortName";
            this.txtBoxSortName.Size = new System.Drawing.Size(195, 20);
            this.txtBoxSortName.TabIndex = 8;
            this.txtBoxSortName.Validating += new System.ComponentModel.CancelEventHandler(this.Name_Validating);
            // 
            // cmbBoxCategoryList
            // 
            this.cmbBoxCategoryList.CausesValidation = false;
            this.cmbBoxCategoryList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxCategoryList.FormattingEnabled = true;
            this.cmbBoxCategoryList.Location = new System.Drawing.Point(100, 115);
            this.cmbBoxCategoryList.Name = "cmbBoxCategoryList";
            this.cmbBoxCategoryList.Size = new System.Drawing.Size(189, 21);
            this.cmbBoxCategoryList.TabIndex = 4;
            // 
            // txtBoxUnits
            // 
            this.txtBoxUnits.Location = new System.Drawing.Point(100, 142);
            this.txtBoxUnits.Name = "txtBoxUnits";
            this.txtBoxUnits.Size = new System.Drawing.Size(64, 20);
            this.txtBoxUnits.TabIndex = 6;
            this.txtBoxUnits.Validating += new System.ComponentModel.CancelEventHandler(this.Value_Validating);
            // 
            // txtBoxProductDesc
            // 
            this.txtBoxProductDesc.Location = new System.Drawing.Point(100, 89);
            this.txtBoxProductDesc.Name = "txtBoxProductDesc";
            this.txtBoxProductDesc.Size = new System.Drawing.Size(400, 20);
            this.txtBoxProductDesc.TabIndex = 3;
            // 
            // txtBoxProductName
            // 
            this.txtBoxProductName.Location = new System.Drawing.Point(100, 63);
            this.txtBoxProductName.Name = "txtBoxProductName";
            this.txtBoxProductName.Size = new System.Drawing.Size(195, 20);
            this.txtBoxProductName.TabIndex = 1;
            this.txtBoxProductName.TextChanged += new System.EventHandler(this.txtBoxProductName_TextChanged);
            this.txtBoxProductName.Validating += new System.ComponentModel.CancelEventHandler(this.Name_Validating);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(263, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Units of Measurement";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Category";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(37, 201);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "HSN Code";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(37, 171);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Sort Name";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(63, 145);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Units";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Description";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Product Name";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(43, 569);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(138, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "Crreate and Continue";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(431, 569);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorProviderAddProductForm
            // 
            this.errorProviderAddProductForm.ContainerControl = this;
            // 
            // btnCreateUpdateClose
            // 
            this.btnCreateUpdateClose.Location = new System.Drawing.Point(233, 569);
            this.btnCreateUpdateClose.Name = "btnCreateUpdateClose";
            this.btnCreateUpdateClose.Size = new System.Drawing.Size(139, 23);
            this.btnCreateUpdateClose.TabIndex = 1;
            this.btnCreateUpdateClose.Text = "Create and Close";
            this.btnCreateUpdateClose.UseVisualStyleBackColor = true;
            this.btnCreateUpdateClose.Click += new System.EventHandler(this.btnCreateUpdateClose_Click);
            // 
            // AddProductForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(560, 658);
            this.Controls.Add(this.btnCreateUpdateClose);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddProductForm";
            this.Text = "Title";
            this.Shown += new System.EventHandler(this.AddProductForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderAddProductForm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxSKUID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbBoxCategoryList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxProductDesc;
        private System.Windows.Forms.TextBox txtBoxProductName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxUnits;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBoxSortName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnAddCategory;
        private System.Windows.Forms.ComboBox cmbBoxMeasurementUnitList;
        private System.Windows.Forms.ComboBox cmbBoxHSNCodeList;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkBoxIsActive;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtBoxStockName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkBoxChooseExistingStock;
        private System.Windows.Forms.ComboBox cmbBoxStockList;
        private System.Windows.Forms.ComboBox cmbBoxStockMeasurementUnitList;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtBoxStockUnits;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtBoxReOrderQty;
        private System.Windows.Forms.TextBox txtBoxReOrderLevel;
        private System.Windows.Forms.Button btnAddHSNCode;
        private System.Windows.Forms.ErrorProvider errorProviderAddProductForm;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCreateUpdateClose;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBoxPurchasePrice;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBoxRetailPrice;
        private System.Windows.Forms.TextBox txtBoxWholePrice;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtBoxMaxRetailPrice;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtBoxStockQty;
        private System.Windows.Forms.Button btnEditHSNCode;
    }
}