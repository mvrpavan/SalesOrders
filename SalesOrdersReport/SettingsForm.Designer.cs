namespace SalesOrdersReport
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.ddlSummaryLocation = new System.Windows.Forms.ComboBox();
            this.lblSummaryLocation = new System.Windows.Forms.Label();
            this.tabPageInvoice = new System.Windows.Forms.TabPage();
            this.txtBoxFooterTextColorInv = new System.Windows.Forms.TextBox();
            this.txtBoxHeaderSubTitleColorInv = new System.Windows.Forms.TextBox();
            this.txtBoxHeaderTitleColorInv = new System.Windows.Forms.TextBox();
            this.txtBoxFooterTitleColorInv = new System.Windows.Forms.TextBox();
            this.btnFooterTextColorInv = new System.Windows.Forms.Button();
            this.btnHeaderSubTitleColorInv = new System.Windows.Forms.Button();
            this.btnHeaderTitleColorInv = new System.Windows.Forms.Button();
            this.btnFooterTitleColorInv = new System.Windows.Forms.Button();
            this.txtBoxAddressInv = new System.Windows.Forms.TextBox();
            this.txtBoxLastInvoiceNumberInv = new System.Windows.Forms.TextBox();
            this.txtBoxTINNumberInv = new System.Windows.Forms.TextBox();
            this.txtBoxVATPercentInv = new System.Windows.Forms.TextBox();
            this.txtBoxEMailIDInv = new System.Windows.Forms.TextBox();
            this.txtBoxPhoneNumberInv = new System.Windows.Forms.TextBox();
            this.txtBoxFooterTitleInv = new System.Windows.Forms.TextBox();
            this.txtBoxHeaderSubTitleInv = new System.Windows.Forms.TextBox();
            this.txtBoxHeaderTitleInv = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageQuotation = new System.Windows.Forms.TabPage();
            this.txtBoxHeaderSubTitleColorQuot = new System.Windows.Forms.TextBox();
            this.txtBoxHeaderTitleColorQuot = new System.Windows.Forms.TextBox();
            this.btnHeaderSubTitleColorQuot = new System.Windows.Forms.Button();
            this.btnHeaderTitleColorQuot = new System.Windows.Forms.Button();
            this.txtBoxFooterTextColorQuot = new System.Windows.Forms.TextBox();
            this.txtBoxFooterTitleColorQuot = new System.Windows.Forms.TextBox();
            this.btnFooterTextColorQuot = new System.Windows.Forms.Button();
            this.btnFooterTitleColorQuot = new System.Windows.Forms.Button();
            this.txtBoxAddressQuot = new System.Windows.Forms.TextBox();
            this.txtBoxLastQuotationNumberQuot = new System.Windows.Forms.TextBox();
            this.txtBoxTINNumberQuot = new System.Windows.Forms.TextBox();
            this.txtBoxEMailIDQuot = new System.Windows.Forms.TextBox();
            this.txtBoxPhoneNumberQuot = new System.Windows.Forms.TextBox();
            this.txtBoxFooterTitleQuot = new System.Windows.Forms.TextBox();
            this.txtBoxHeaderSubTitleQuot = new System.Windows.Forms.TextBox();
            this.txtBoxHeaderTitleQuot = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.btnApplySettings = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.colorDialogSettings = new System.Windows.Forms.ColorDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbBoxProductLines = new System.Windows.Forms.ComboBox();
            this.tabControlSettings.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.tabPageInvoice.SuspendLayout();
            this.tabPageQuotation.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Controls.Add(this.tabPageGeneral);
            this.tabControlSettings.Controls.Add(this.tabPageInvoice);
            this.tabControlSettings.Controls.Add(this.tabPageQuotation);
            this.tabControlSettings.Location = new System.Drawing.Point(12, 42);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.ShowToolTips = true;
            this.tabControlSettings.Size = new System.Drawing.Size(547, 331);
            this.tabControlSettings.TabIndex = 0;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPageGeneral.Controls.Add(this.ddlSummaryLocation);
            this.tabPageGeneral.Controls.Add(this.lblSummaryLocation);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneral.Size = new System.Drawing.Size(539, 305);
            this.tabPageGeneral.TabIndex = 3;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.ToolTipText = "General Settings";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // ddlSummaryLocation
            // 
            this.ddlSummaryLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSummaryLocation.FormattingEnabled = true;
            this.ddlSummaryLocation.Location = new System.Drawing.Point(247, 32);
            this.ddlSummaryLocation.Name = "ddlSummaryLocation";
            this.ddlSummaryLocation.Size = new System.Drawing.Size(121, 21);
            this.ddlSummaryLocation.TabIndex = 3;
            // 
            // lblSummaryLocation
            // 
            this.lblSummaryLocation.AutoSize = true;
            this.lblSummaryLocation.Location = new System.Drawing.Point(72, 35);
            this.lblSummaryLocation.Name = "lblSummaryLocation";
            this.lblSummaryLocation.Size = new System.Drawing.Size(94, 13);
            this.lblSummaryLocation.TabIndex = 2;
            this.lblSummaryLocation.Text = "Summary Location";
            // 
            // tabPageInvoice
            // 
            this.tabPageInvoice.BackColor = System.Drawing.Color.Transparent;
            this.tabPageInvoice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPageInvoice.Controls.Add(this.txtBoxFooterTextColorInv);
            this.tabPageInvoice.Controls.Add(this.txtBoxHeaderSubTitleColorInv);
            this.tabPageInvoice.Controls.Add(this.txtBoxHeaderTitleColorInv);
            this.tabPageInvoice.Controls.Add(this.txtBoxFooterTitleColorInv);
            this.tabPageInvoice.Controls.Add(this.btnFooterTextColorInv);
            this.tabPageInvoice.Controls.Add(this.btnHeaderSubTitleColorInv);
            this.tabPageInvoice.Controls.Add(this.btnHeaderTitleColorInv);
            this.tabPageInvoice.Controls.Add(this.btnFooterTitleColorInv);
            this.tabPageInvoice.Controls.Add(this.txtBoxAddressInv);
            this.tabPageInvoice.Controls.Add(this.txtBoxLastInvoiceNumberInv);
            this.tabPageInvoice.Controls.Add(this.txtBoxTINNumberInv);
            this.tabPageInvoice.Controls.Add(this.txtBoxVATPercentInv);
            this.tabPageInvoice.Controls.Add(this.txtBoxEMailIDInv);
            this.tabPageInvoice.Controls.Add(this.txtBoxPhoneNumberInv);
            this.tabPageInvoice.Controls.Add(this.txtBoxFooterTitleInv);
            this.tabPageInvoice.Controls.Add(this.txtBoxHeaderSubTitleInv);
            this.tabPageInvoice.Controls.Add(this.txtBoxHeaderTitleInv);
            this.tabPageInvoice.Controls.Add(this.label10);
            this.tabPageInvoice.Controls.Add(this.label9);
            this.tabPageInvoice.Controls.Add(this.label8);
            this.tabPageInvoice.Controls.Add(this.label7);
            this.tabPageInvoice.Controls.Add(this.label6);
            this.tabPageInvoice.Controls.Add(this.label5);
            this.tabPageInvoice.Controls.Add(this.label3);
            this.tabPageInvoice.Controls.Add(this.label2);
            this.tabPageInvoice.Controls.Add(this.label1);
            this.tabPageInvoice.Location = new System.Drawing.Point(4, 22);
            this.tabPageInvoice.Name = "tabPageInvoice";
            this.tabPageInvoice.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInvoice.Size = new System.Drawing.Size(539, 305);
            this.tabPageInvoice.TabIndex = 1;
            this.tabPageInvoice.Tag = "";
            this.tabPageInvoice.Text = "Invoice";
            this.tabPageInvoice.ToolTipText = "Modify Invoice Settings";
            this.tabPageInvoice.UseVisualStyleBackColor = true;
            // 
            // txtBoxFooterTextColorInv
            // 
            this.txtBoxFooterTextColorInv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoxFooterTextColorInv.Enabled = false;
            this.txtBoxFooterTextColorInv.Location = new System.Drawing.Point(422, 111);
            this.txtBoxFooterTextColorInv.Name = "txtBoxFooterTextColorInv";
            this.txtBoxFooterTextColorInv.Size = new System.Drawing.Size(21, 13);
            this.txtBoxFooterTextColorInv.TabIndex = 26;
            // 
            // txtBoxHeaderSubTitleColorInv
            // 
            this.txtBoxHeaderSubTitleColorInv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoxHeaderSubTitleColorInv.Enabled = false;
            this.txtBoxHeaderSubTitleColorInv.Location = new System.Drawing.Point(422, 59);
            this.txtBoxHeaderSubTitleColorInv.Name = "txtBoxHeaderSubTitleColorInv";
            this.txtBoxHeaderSubTitleColorInv.Size = new System.Drawing.Size(21, 13);
            this.txtBoxHeaderSubTitleColorInv.TabIndex = 26;
            // 
            // txtBoxHeaderTitleColorInv
            // 
            this.txtBoxHeaderTitleColorInv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoxHeaderTitleColorInv.Enabled = false;
            this.txtBoxHeaderTitleColorInv.Location = new System.Drawing.Point(422, 33);
            this.txtBoxHeaderTitleColorInv.Name = "txtBoxHeaderTitleColorInv";
            this.txtBoxHeaderTitleColorInv.Size = new System.Drawing.Size(21, 13);
            this.txtBoxHeaderTitleColorInv.TabIndex = 26;
            // 
            // txtBoxFooterTitleColorInv
            // 
            this.txtBoxFooterTitleColorInv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoxFooterTitleColorInv.Enabled = false;
            this.txtBoxFooterTitleColorInv.Location = new System.Drawing.Point(422, 85);
            this.txtBoxFooterTitleColorInv.Name = "txtBoxFooterTitleColorInv";
            this.txtBoxFooterTitleColorInv.Size = new System.Drawing.Size(21, 13);
            this.txtBoxFooterTitleColorInv.TabIndex = 26;
            // 
            // btnFooterTextColorInv
            // 
            this.btnFooterTextColorInv.Image = ((System.Drawing.Image)(resources.GetObject("btnFooterTextColorInv.Image")));
            this.btnFooterTextColorInv.Location = new System.Drawing.Point(388, 106);
            this.btnFooterTextColorInv.Name = "btnFooterTextColorInv";
            this.btnFooterTextColorInv.Size = new System.Drawing.Size(28, 23);
            this.btnFooterTextColorInv.TabIndex = 8;
            this.btnFooterTextColorInv.UseVisualStyleBackColor = true;
            this.btnFooterTextColorInv.Click += new System.EventHandler(this.btnFooterTextColorInv_Click);
            // 
            // btnHeaderSubTitleColorInv
            // 
            this.btnHeaderSubTitleColorInv.Image = ((System.Drawing.Image)(resources.GetObject("btnHeaderSubTitleColorInv.Image")));
            this.btnHeaderSubTitleColorInv.Location = new System.Drawing.Point(388, 54);
            this.btnHeaderSubTitleColorInv.Name = "btnHeaderSubTitleColorInv";
            this.btnHeaderSubTitleColorInv.Size = new System.Drawing.Size(28, 23);
            this.btnHeaderSubTitleColorInv.TabIndex = 4;
            this.btnHeaderSubTitleColorInv.UseVisualStyleBackColor = true;
            this.btnHeaderSubTitleColorInv.Click += new System.EventHandler(this.btnHeaderSubTitleColorInv_Click);
            // 
            // btnHeaderTitleColorInv
            // 
            this.btnHeaderTitleColorInv.Image = ((System.Drawing.Image)(resources.GetObject("btnHeaderTitleColorInv.Image")));
            this.btnHeaderTitleColorInv.Location = new System.Drawing.Point(388, 28);
            this.btnHeaderTitleColorInv.Name = "btnHeaderTitleColorInv";
            this.btnHeaderTitleColorInv.Size = new System.Drawing.Size(28, 23);
            this.btnHeaderTitleColorInv.TabIndex = 2;
            this.btnHeaderTitleColorInv.UseVisualStyleBackColor = true;
            this.btnHeaderTitleColorInv.Click += new System.EventHandler(this.btnHeaderTitleColorInv_Click);
            // 
            // btnFooterTitleColorInv
            // 
            this.btnFooterTitleColorInv.Image = ((System.Drawing.Image)(resources.GetObject("btnFooterTitleColorInv.Image")));
            this.btnFooterTitleColorInv.Location = new System.Drawing.Point(388, 80);
            this.btnFooterTitleColorInv.Name = "btnFooterTitleColorInv";
            this.btnFooterTitleColorInv.Size = new System.Drawing.Size(28, 23);
            this.btnFooterTitleColorInv.TabIndex = 6;
            this.btnFooterTitleColorInv.UseVisualStyleBackColor = true;
            this.btnFooterTitleColorInv.Click += new System.EventHandler(this.btnFooterTitleColorInv_Click);
            // 
            // txtBoxAddressInv
            // 
            this.txtBoxAddressInv.Location = new System.Drawing.Point(178, 108);
            this.txtBoxAddressInv.Multiline = true;
            this.txtBoxAddressInv.Name = "txtBoxAddressInv";
            this.txtBoxAddressInv.Size = new System.Drawing.Size(204, 51);
            this.txtBoxAddressInv.TabIndex = 7;
            // 
            // txtBoxLastInvoiceNumberInv
            // 
            this.txtBoxLastInvoiceNumberInv.Location = new System.Drawing.Point(178, 269);
            this.txtBoxLastInvoiceNumberInv.Name = "txtBoxLastInvoiceNumberInv";
            this.txtBoxLastInvoiceNumberInv.Size = new System.Drawing.Size(204, 20);
            this.txtBoxLastInvoiceNumberInv.TabIndex = 13;
            // 
            // txtBoxTINNumberInv
            // 
            this.txtBoxTINNumberInv.Location = new System.Drawing.Point(178, 243);
            this.txtBoxTINNumberInv.Name = "txtBoxTINNumberInv";
            this.txtBoxTINNumberInv.Size = new System.Drawing.Size(204, 20);
            this.txtBoxTINNumberInv.TabIndex = 12;
            // 
            // txtBoxVATPercentInv
            // 
            this.txtBoxVATPercentInv.Location = new System.Drawing.Point(178, 217);
            this.txtBoxVATPercentInv.Name = "txtBoxVATPercentInv";
            this.txtBoxVATPercentInv.Size = new System.Drawing.Size(204, 20);
            this.txtBoxVATPercentInv.TabIndex = 11;
            // 
            // txtBoxEMailIDInv
            // 
            this.txtBoxEMailIDInv.Location = new System.Drawing.Point(178, 191);
            this.txtBoxEMailIDInv.Name = "txtBoxEMailIDInv";
            this.txtBoxEMailIDInv.Size = new System.Drawing.Size(204, 20);
            this.txtBoxEMailIDInv.TabIndex = 10;
            // 
            // txtBoxPhoneNumberInv
            // 
            this.txtBoxPhoneNumberInv.Location = new System.Drawing.Point(178, 165);
            this.txtBoxPhoneNumberInv.Name = "txtBoxPhoneNumberInv";
            this.txtBoxPhoneNumberInv.Size = new System.Drawing.Size(204, 20);
            this.txtBoxPhoneNumberInv.TabIndex = 9;
            // 
            // txtBoxFooterTitleInv
            // 
            this.txtBoxFooterTitleInv.Location = new System.Drawing.Point(178, 82);
            this.txtBoxFooterTitleInv.Name = "txtBoxFooterTitleInv";
            this.txtBoxFooterTitleInv.Size = new System.Drawing.Size(204, 20);
            this.txtBoxFooterTitleInv.TabIndex = 5;
            // 
            // txtBoxHeaderSubTitleInv
            // 
            this.txtBoxHeaderSubTitleInv.Location = new System.Drawing.Point(178, 56);
            this.txtBoxHeaderSubTitleInv.Name = "txtBoxHeaderSubTitleInv";
            this.txtBoxHeaderSubTitleInv.Size = new System.Drawing.Size(204, 20);
            this.txtBoxHeaderSubTitleInv.TabIndex = 3;
            // 
            // txtBoxHeaderTitleInv
            // 
            this.txtBoxHeaderTitleInv.Location = new System.Drawing.Point(178, 30);
            this.txtBoxHeaderTitleInv.Name = "txtBoxHeaderTitleInv";
            this.txtBoxHeaderTitleInv.Size = new System.Drawing.Size(204, 20);
            this.txtBoxHeaderTitleInv.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(70, 272);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Last Invoice #";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(70, 246);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "TIN Number";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(70, 220);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "VAT Percent";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(70, 194);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Email Address";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(70, 168);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Phone Number";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(70, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Address";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(70, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Footer Title";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Header SubTitle";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Header Title";
            // 
            // tabPageQuotation
            // 
            this.tabPageQuotation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPageQuotation.Controls.Add(this.txtBoxHeaderSubTitleColorQuot);
            this.tabPageQuotation.Controls.Add(this.txtBoxHeaderTitleColorQuot);
            this.tabPageQuotation.Controls.Add(this.btnHeaderSubTitleColorQuot);
            this.tabPageQuotation.Controls.Add(this.btnHeaderTitleColorQuot);
            this.tabPageQuotation.Controls.Add(this.txtBoxFooterTextColorQuot);
            this.tabPageQuotation.Controls.Add(this.txtBoxFooterTitleColorQuot);
            this.tabPageQuotation.Controls.Add(this.btnFooterTextColorQuot);
            this.tabPageQuotation.Controls.Add(this.btnFooterTitleColorQuot);
            this.tabPageQuotation.Controls.Add(this.txtBoxAddressQuot);
            this.tabPageQuotation.Controls.Add(this.txtBoxLastQuotationNumberQuot);
            this.tabPageQuotation.Controls.Add(this.txtBoxTINNumberQuot);
            this.tabPageQuotation.Controls.Add(this.txtBoxEMailIDQuot);
            this.tabPageQuotation.Controls.Add(this.txtBoxPhoneNumberQuot);
            this.tabPageQuotation.Controls.Add(this.txtBoxFooterTitleQuot);
            this.tabPageQuotation.Controls.Add(this.txtBoxHeaderSubTitleQuot);
            this.tabPageQuotation.Controls.Add(this.txtBoxHeaderTitleQuot);
            this.tabPageQuotation.Controls.Add(this.label11);
            this.tabPageQuotation.Controls.Add(this.label12);
            this.tabPageQuotation.Controls.Add(this.label14);
            this.tabPageQuotation.Controls.Add(this.label15);
            this.tabPageQuotation.Controls.Add(this.label16);
            this.tabPageQuotation.Controls.Add(this.label18);
            this.tabPageQuotation.Controls.Add(this.label19);
            this.tabPageQuotation.Controls.Add(this.label20);
            this.tabPageQuotation.Location = new System.Drawing.Point(4, 22);
            this.tabPageQuotation.Name = "tabPageQuotation";
            this.tabPageQuotation.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageQuotation.Size = new System.Drawing.Size(539, 305);
            this.tabPageQuotation.TabIndex = 2;
            this.tabPageQuotation.Tag = "";
            this.tabPageQuotation.Text = "Quotation";
            this.tabPageQuotation.ToolTipText = "Modify Quotation Settings";
            this.tabPageQuotation.UseVisualStyleBackColor = true;
            // 
            // txtBoxHeaderSubTitleColorQuot
            // 
            this.txtBoxHeaderSubTitleColorQuot.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoxHeaderSubTitleColorQuot.Enabled = false;
            this.txtBoxHeaderSubTitleColorQuot.Location = new System.Drawing.Point(422, 59);
            this.txtBoxHeaderSubTitleColorQuot.Name = "txtBoxHeaderSubTitleColorQuot";
            this.txtBoxHeaderSubTitleColorQuot.Size = new System.Drawing.Size(21, 13);
            this.txtBoxHeaderSubTitleColorQuot.TabIndex = 29;
            // 
            // txtBoxHeaderTitleColorQuot
            // 
            this.txtBoxHeaderTitleColorQuot.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoxHeaderTitleColorQuot.Enabled = false;
            this.txtBoxHeaderTitleColorQuot.Location = new System.Drawing.Point(422, 33);
            this.txtBoxHeaderTitleColorQuot.Name = "txtBoxHeaderTitleColorQuot";
            this.txtBoxHeaderTitleColorQuot.Size = new System.Drawing.Size(21, 13);
            this.txtBoxHeaderTitleColorQuot.TabIndex = 30;
            // 
            // btnHeaderSubTitleColorQuot
            // 
            this.btnHeaderSubTitleColorQuot.Image = ((System.Drawing.Image)(resources.GetObject("btnHeaderSubTitleColorQuot.Image")));
            this.btnHeaderSubTitleColorQuot.Location = new System.Drawing.Point(388, 54);
            this.btnHeaderSubTitleColorQuot.Name = "btnHeaderSubTitleColorQuot";
            this.btnHeaderSubTitleColorQuot.Size = new System.Drawing.Size(28, 23);
            this.btnHeaderSubTitleColorQuot.TabIndex = 4;
            this.btnHeaderSubTitleColorQuot.UseVisualStyleBackColor = true;
            this.btnHeaderSubTitleColorQuot.Click += new System.EventHandler(this.btnHeaderSubTitleColorQuot_Click);
            // 
            // btnHeaderTitleColorQuot
            // 
            this.btnHeaderTitleColorQuot.Image = ((System.Drawing.Image)(resources.GetObject("btnHeaderTitleColorQuot.Image")));
            this.btnHeaderTitleColorQuot.Location = new System.Drawing.Point(388, 28);
            this.btnHeaderTitleColorQuot.Name = "btnHeaderTitleColorQuot";
            this.btnHeaderTitleColorQuot.Size = new System.Drawing.Size(28, 23);
            this.btnHeaderTitleColorQuot.TabIndex = 2;
            this.btnHeaderTitleColorQuot.UseVisualStyleBackColor = true;
            this.btnHeaderTitleColorQuot.Click += new System.EventHandler(this.btnHeaderTitleColorQuot_Click);
            // 
            // txtBoxFooterTextColorQuot
            // 
            this.txtBoxFooterTextColorQuot.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoxFooterTextColorQuot.Enabled = false;
            this.txtBoxFooterTextColorQuot.Location = new System.Drawing.Point(422, 111);
            this.txtBoxFooterTextColorQuot.Name = "txtBoxFooterTextColorQuot";
            this.txtBoxFooterTextColorQuot.Size = new System.Drawing.Size(21, 13);
            this.txtBoxFooterTextColorQuot.TabIndex = 25;
            // 
            // txtBoxFooterTitleColorQuot
            // 
            this.txtBoxFooterTitleColorQuot.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoxFooterTitleColorQuot.Enabled = false;
            this.txtBoxFooterTitleColorQuot.Location = new System.Drawing.Point(422, 85);
            this.txtBoxFooterTitleColorQuot.Name = "txtBoxFooterTitleColorQuot";
            this.txtBoxFooterTitleColorQuot.Size = new System.Drawing.Size(21, 13);
            this.txtBoxFooterTitleColorQuot.TabIndex = 25;
            // 
            // btnFooterTextColorQuot
            // 
            this.btnFooterTextColorQuot.Image = ((System.Drawing.Image)(resources.GetObject("btnFooterTextColorQuot.Image")));
            this.btnFooterTextColorQuot.Location = new System.Drawing.Point(388, 106);
            this.btnFooterTextColorQuot.Name = "btnFooterTextColorQuot";
            this.btnFooterTextColorQuot.Size = new System.Drawing.Size(28, 23);
            this.btnFooterTextColorQuot.TabIndex = 8;
            this.btnFooterTextColorQuot.UseVisualStyleBackColor = true;
            this.btnFooterTextColorQuot.Click += new System.EventHandler(this.btnFooterTextColorQuot_Click);
            // 
            // btnFooterTitleColorQuot
            // 
            this.btnFooterTitleColorQuot.Image = ((System.Drawing.Image)(resources.GetObject("btnFooterTitleColorQuot.Image")));
            this.btnFooterTitleColorQuot.Location = new System.Drawing.Point(388, 80);
            this.btnFooterTitleColorQuot.Name = "btnFooterTitleColorQuot";
            this.btnFooterTitleColorQuot.Size = new System.Drawing.Size(28, 23);
            this.btnFooterTitleColorQuot.TabIndex = 6;
            this.btnFooterTitleColorQuot.UseVisualStyleBackColor = true;
            this.btnFooterTitleColorQuot.Click += new System.EventHandler(this.btnFooterTitleColorQuot_Click);
            // 
            // txtBoxAddressQuot
            // 
            this.txtBoxAddressQuot.Location = new System.Drawing.Point(178, 108);
            this.txtBoxAddressQuot.Multiline = true;
            this.txtBoxAddressQuot.Name = "txtBoxAddressQuot";
            this.txtBoxAddressQuot.Size = new System.Drawing.Size(204, 51);
            this.txtBoxAddressQuot.TabIndex = 7;
            // 
            // txtBoxLastQuotationNumberQuot
            // 
            this.txtBoxLastQuotationNumberQuot.Location = new System.Drawing.Point(178, 243);
            this.txtBoxLastQuotationNumberQuot.Name = "txtBoxLastQuotationNumberQuot";
            this.txtBoxLastQuotationNumberQuot.Size = new System.Drawing.Size(204, 20);
            this.txtBoxLastQuotationNumberQuot.TabIndex = 12;
            // 
            // txtBoxTINNumberQuot
            // 
            this.txtBoxTINNumberQuot.Location = new System.Drawing.Point(178, 217);
            this.txtBoxTINNumberQuot.Name = "txtBoxTINNumberQuot";
            this.txtBoxTINNumberQuot.Size = new System.Drawing.Size(204, 20);
            this.txtBoxTINNumberQuot.TabIndex = 11;
            // 
            // txtBoxEMailIDQuot
            // 
            this.txtBoxEMailIDQuot.Location = new System.Drawing.Point(178, 191);
            this.txtBoxEMailIDQuot.Name = "txtBoxEMailIDQuot";
            this.txtBoxEMailIDQuot.Size = new System.Drawing.Size(204, 20);
            this.txtBoxEMailIDQuot.TabIndex = 10;
            // 
            // txtBoxPhoneNumberQuot
            // 
            this.txtBoxPhoneNumberQuot.Location = new System.Drawing.Point(178, 165);
            this.txtBoxPhoneNumberQuot.Name = "txtBoxPhoneNumberQuot";
            this.txtBoxPhoneNumberQuot.Size = new System.Drawing.Size(204, 20);
            this.txtBoxPhoneNumberQuot.TabIndex = 9;
            // 
            // txtBoxFooterTitleQuot
            // 
            this.txtBoxFooterTitleQuot.Location = new System.Drawing.Point(178, 82);
            this.txtBoxFooterTitleQuot.Name = "txtBoxFooterTitleQuot";
            this.txtBoxFooterTitleQuot.Size = new System.Drawing.Size(204, 20);
            this.txtBoxFooterTitleQuot.TabIndex = 5;
            // 
            // txtBoxHeaderSubTitleQuot
            // 
            this.txtBoxHeaderSubTitleQuot.Location = new System.Drawing.Point(178, 56);
            this.txtBoxHeaderSubTitleQuot.Name = "txtBoxHeaderSubTitleQuot";
            this.txtBoxHeaderSubTitleQuot.Size = new System.Drawing.Size(204, 20);
            this.txtBoxHeaderSubTitleQuot.TabIndex = 3;
            // 
            // txtBoxHeaderTitleQuot
            // 
            this.txtBoxHeaderTitleQuot.Location = new System.Drawing.Point(178, 30);
            this.txtBoxHeaderTitleQuot.Name = "txtBoxHeaderTitleQuot";
            this.txtBoxHeaderTitleQuot.Size = new System.Drawing.Size(204, 20);
            this.txtBoxHeaderTitleQuot.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(70, 246);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Last Quotation #";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(70, 220);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "TIN Number";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(70, 194);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(73, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Email Address";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(70, 168);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(78, 13);
            this.label15.TabIndex = 4;
            this.label15.Text = "Phone Number";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(70, 111);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(45, 13);
            this.label16.TabIndex = 11;
            this.label16.Text = "Address";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(70, 85);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(60, 13);
            this.label18.TabIndex = 10;
            this.label18.Text = "Footer Title";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(70, 59);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(84, 13);
            this.label19.TabIndex = 8;
            this.label19.Text = "Header SubTitle";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(70, 33);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(65, 13);
            this.label20.TabIndex = 9;
            this.label20.Text = "Header Title";
            // 
            // btnApplySettings
            // 
            this.btnApplySettings.Location = new System.Drawing.Point(162, 394);
            this.btnApplySettings.Name = "btnApplySettings";
            this.btnApplySettings.Size = new System.Drawing.Size(97, 23);
            this.btnApplySettings.TabIndex = 13;
            this.btnApplySettings.Text = "Apply Settings";
            this.btnApplySettings.UseVisualStyleBackColor = true;
            this.btnApplySettings.Click += new System.EventHandler(this.btnApplySettings_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(309, 394);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "Cancel";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(147, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Product Line";
            // 
            // cmbBoxProductLines
            // 
            this.cmbBoxProductLines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxProductLines.FormattingEnabled = true;
            this.cmbBoxProductLines.Location = new System.Drawing.Point(220, 13);
            this.cmbBoxProductLines.Name = "cmbBoxProductLines";
            this.cmbBoxProductLines.Size = new System.Drawing.Size(120, 21);
            this.cmbBoxProductLines.TabIndex = 16;
            this.cmbBoxProductLines.SelectedIndexChanged += new System.EventHandler(this.cmbBoxProductLines_SelectedIndexChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(573, 431);
            this.Controls.Add(this.cmbBoxProductLines);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabControlSettings);
            this.Controls.Add(this.btnApplySettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsForm_FormClosed);
            this.tabControlSettings.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            this.tabPageInvoice.ResumeLayout(false);
            this.tabPageInvoice.PerformLayout();
            this.tabPageQuotation.ResumeLayout(false);
            this.tabPageQuotation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.TabPage tabPageInvoice;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnApplySettings;
        private System.Windows.Forms.TabPage tabPageQuotation;
        private System.Windows.Forms.ColorDialog colorDialogSettings;
        private System.Windows.Forms.TextBox txtBoxHeaderSubTitleInv;
        private System.Windows.Forms.TextBox txtBoxHeaderTitleInv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxFooterTitleInv;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnFooterTitleColorInv;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBoxAddressInv;
        private System.Windows.Forms.Button btnFooterTextColorInv;
        private System.Windows.Forms.TextBox txtBoxPhoneNumberInv;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBoxEMailIDInv;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBoxVATPercentInv;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBoxTINNumberInv;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBoxLastInvoiceNumberInv;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnFooterTextColorQuot;
        private System.Windows.Forms.Button btnFooterTitleColorQuot;
        private System.Windows.Forms.TextBox txtBoxAddressQuot;
        private System.Windows.Forms.TextBox txtBoxLastQuotationNumberQuot;
        private System.Windows.Forms.TextBox txtBoxTINNumberQuot;
        private System.Windows.Forms.TextBox txtBoxEMailIDQuot;
        private System.Windows.Forms.TextBox txtBoxPhoneNumberQuot;
        private System.Windows.Forms.TextBox txtBoxFooterTitleQuot;
        private System.Windows.Forms.TextBox txtBoxHeaderSubTitleQuot;
        private System.Windows.Forms.TextBox txtBoxHeaderTitleQuot;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtBoxFooterTextColorQuot;
        private System.Windows.Forms.TextBox txtBoxFooterTitleColorQuot;
        private System.Windows.Forms.TextBox txtBoxFooterTextColorInv;
        private System.Windows.Forms.TextBox txtBoxFooterTitleColorInv;
        private System.Windows.Forms.TextBox txtBoxHeaderSubTitleColorInv;
        private System.Windows.Forms.TextBox txtBoxHeaderTitleColorInv;
        private System.Windows.Forms.Button btnHeaderSubTitleColorInv;
        private System.Windows.Forms.Button btnHeaderTitleColorInv;
        private System.Windows.Forms.TextBox txtBoxHeaderSubTitleColorQuot;
        private System.Windows.Forms.TextBox txtBoxHeaderTitleColorQuot;
        private System.Windows.Forms.Button btnHeaderSubTitleColorQuot;
        private System.Windows.Forms.Button btnHeaderTitleColorQuot;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.ComboBox ddlSummaryLocation;
        private System.Windows.Forms.Label lblSummaryLocation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbBoxProductLines;
    }
}