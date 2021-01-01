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
            this.dtGridViewPrices = new System.Windows.Forms.DataGridView();
            this.PriceColumnInProdMaster = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.label13 = new System.Windows.Forms.Label();
            this.txtBoxStockUnits = new System.Windows.Forms.TextBox();
            this.chkBoxIsActive = new System.Windows.Forms.CheckBox();
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
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewPrices)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
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
            this.groupBox1.Size = new System.Drawing.Size(536, 605);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Product Details";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dtGridViewPrices);
            this.groupBox3.Location = new System.Drawing.Point(7, 414);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(529, 185);
            this.groupBox3.TabIndex = 1001;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Price Details";
            // 
            // dtGridViewPrices
            // 
            this.dtGridViewPrices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGridViewPrices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PriceColumnInProdMaster,
            this.Price});
            this.dtGridViewPrices.Location = new System.Drawing.Point(6, 19);
            this.dtGridViewPrices.Name = "dtGridViewPrices";
            this.dtGridViewPrices.Size = new System.Drawing.Size(517, 160);
            this.dtGridViewPrices.TabIndex = 0;
            // 
            // PriceColumnInProdMaster
            // 
            this.PriceColumnInProdMaster.HeaderText = "Price Column";
            this.PriceColumnInProdMaster.Name = "PriceColumnInProdMaster";
            this.PriceColumnInProdMaster.ReadOnly = true;
            this.PriceColumnInProdMaster.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PriceColumnInProdMaster.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Price
            // 
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
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
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txtBoxStockUnits);
            this.groupBox2.Location = new System.Drawing.Point(6, 225);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(524, 183);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stock Details";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 80);
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
            this.chkBoxChooseExistingStock.Location = new System.Drawing.Point(94, 54);
            this.chkBoxChooseExistingStock.Name = "chkBoxChooseExistingStock";
            this.chkBoxChooseExistingStock.Size = new System.Drawing.Size(127, 17);
            this.chkBoxChooseExistingStock.TabIndex = 13;
            this.chkBoxChooseExistingStock.Text = "Choose From Existing";
            this.chkBoxChooseExistingStock.UseVisualStyleBackColor = true;
            // 
            // txtBoxStockName
            // 
            this.txtBoxStockName.Location = new System.Drawing.Point(94, 28);
            this.txtBoxStockName.Name = "txtBoxStockName";
            this.txtBoxStockName.Size = new System.Drawing.Size(195, 20);
            this.txtBoxStockName.TabIndex = 12;
            // 
            // cmbBoxStockList
            // 
            this.cmbBoxStockList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxStockList.FormattingEnabled = true;
            this.cmbBoxStockList.Location = new System.Drawing.Point(94, 77);
            this.cmbBoxStockList.Name = "cmbBoxStockList";
            this.cmbBoxStockList.Size = new System.Drawing.Size(195, 21);
            this.cmbBoxStockList.TabIndex = 14;
            // 
            // cmbBoxStockMeasurementUnitList
            // 
            this.cmbBoxStockMeasurementUnitList.CausesValidation = false;
            this.cmbBoxStockMeasurementUnitList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxStockMeasurementUnitList.FormattingEnabled = true;
            this.cmbBoxStockMeasurementUnitList.Location = new System.Drawing.Point(373, 104);
            this.cmbBoxStockMeasurementUnitList.Name = "cmbBoxStockMeasurementUnitList";
            this.cmbBoxStockMeasurementUnitList.Size = new System.Drawing.Size(121, 21);
            this.cmbBoxStockMeasurementUnitList.TabIndex = 16;
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
            this.txtBoxReOrderQty.TabIndex = 18;
            // 
            // txtBoxReOrderLevel
            // 
            this.txtBoxReOrderLevel.Location = new System.Drawing.Point(94, 130);
            this.txtBoxReOrderLevel.Name = "txtBoxReOrderLevel";
            this.txtBoxReOrderLevel.Size = new System.Drawing.Size(121, 20);
            this.txtBoxReOrderLevel.TabIndex = 17;
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
            // txtBoxStockUnits
            // 
            this.txtBoxStockUnits.Location = new System.Drawing.Point(94, 104);
            this.txtBoxStockUnits.Name = "txtBoxStockUnits";
            this.txtBoxStockUnits.Size = new System.Drawing.Size(121, 20);
            this.txtBoxStockUnits.TabIndex = 15;
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
            // btnAddHSNCode
            // 
            this.btnAddHSNCode.FlatAppearance.BorderSize = 0;
            this.btnAddHSNCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddHSNCode.Image = global::SalesOrdersReport.Properties.Resources.add;
            this.btnAddHSNCode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddHSNCode.Location = new System.Drawing.Point(227, 196);
            this.btnAddHSNCode.Name = "btnAddHSNCode";
            this.btnAddHSNCode.Size = new System.Drawing.Size(26, 23);
            this.btnAddHSNCode.TabIndex = 11;
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
            this.btnAddCategory.TabIndex = 4;
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
            this.cmbBoxHSNCodeList.TabIndex = 10;
            // 
            // cmbBoxMeasurementUnitList
            // 
            this.cmbBoxMeasurementUnitList.CausesValidation = false;
            this.cmbBoxMeasurementUnitList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxMeasurementUnitList.FormattingEnabled = true;
            this.cmbBoxMeasurementUnitList.Location = new System.Drawing.Point(379, 142);
            this.cmbBoxMeasurementUnitList.Name = "cmbBoxMeasurementUnitList";
            this.cmbBoxMeasurementUnitList.Size = new System.Drawing.Size(121, 21);
            this.cmbBoxMeasurementUnitList.TabIndex = 8;
            // 
            // txtBoxSortName
            // 
            this.txtBoxSortName.Location = new System.Drawing.Point(100, 168);
            this.txtBoxSortName.Name = "txtBoxSortName";
            this.txtBoxSortName.Size = new System.Drawing.Size(195, 20);
            this.txtBoxSortName.TabIndex = 9;
            // 
            // cmbBoxCategoryList
            // 
            this.cmbBoxCategoryList.CausesValidation = false;
            this.cmbBoxCategoryList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxCategoryList.FormattingEnabled = true;
            this.cmbBoxCategoryList.Location = new System.Drawing.Point(100, 115);
            this.cmbBoxCategoryList.Name = "cmbBoxCategoryList";
            this.cmbBoxCategoryList.Size = new System.Drawing.Size(189, 21);
            this.cmbBoxCategoryList.TabIndex = 3;
            // 
            // txtBoxUnits
            // 
            this.txtBoxUnits.Location = new System.Drawing.Point(100, 142);
            this.txtBoxUnits.Name = "txtBoxUnits";
            this.txtBoxUnits.Size = new System.Drawing.Size(64, 20);
            this.txtBoxUnits.TabIndex = 7;
            // 
            // txtBoxProductDesc
            // 
            this.txtBoxProductDesc.Location = new System.Drawing.Point(100, 89);
            this.txtBoxProductDesc.Name = "txtBoxProductDesc";
            this.txtBoxProductDesc.Size = new System.Drawing.Size(400, 20);
            this.txtBoxProductDesc.TabIndex = 2;
            // 
            // txtBoxProductName
            // 
            this.txtBoxProductName.Location = new System.Drawing.Point(100, 63);
            this.txtBoxProductName.Name = "txtBoxProductName";
            this.txtBoxProductName.Size = new System.Drawing.Size(195, 20);
            this.txtBoxProductName.TabIndex = 1;
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
            this.btnOK.Location = new System.Drawing.Point(112, 623);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 19;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(437, 623);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // AddProductForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(560, 658);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddProductForm";
            this.Text = "Title";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridViewPrices)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxSKUID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbBoxCategoryList;
        private System.Windows.Forms.TextBox txtBoxProductName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxProductDesc;
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
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dtGridViewPrices;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceColumnInProdMaster;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
    }
}