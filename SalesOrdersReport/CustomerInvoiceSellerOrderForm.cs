using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace SalesOrdersReport
{
    public partial class CustomerInvoiceSellerOrderForm : Form
    {
        String MasterFilePath, FormTitle = "", OrderInvoice = "";
        Boolean IsSellerOrder, IsCustomerBill, IsUpdateCustomerBill;
        List<ProductDetails> ListAllProducts, ListProducts;
        List<String> ListSellerNames;
        SellerDetails CurrSellerDetails;
        DiscountGroupDetails CurrSellerDiscountGroup;
        SellerOrderDetails CurrSellerOrderDetails;
        Dictionary<String, Int32> DictItemToColIndexes, DictSellerToRowIndexes;
        List<SellerOrderDetails> ListSellerOrderDetails;
        Int32 CategoryColIndex = 0, ItemColIndex = 1, PriceColIndex = 2, QtyColIndex = 3, SelectColIndex = 4;
        Int32 PaddingSpace = 6;
        Char PaddingChar = ' ', CurrencyChar = '\u20B9';
        Int32 BackgroundTask = -1;
        Excel.Application xlApp;
        List<Int32> ListSelectedRowIndexesToAdd = new List<Int32>();
        Dictionary<String, DataGridViewRow> DictItemsSelected = new Dictionary<String, DataGridViewRow>();
        Boolean ValueChanged = false;
        Double DiscountPerc = 0, DiscountValue = 0;
        List<Int32> ListSelectedRowIndexesToRemove = new List<Int32>();
        Int32 cmbBoxSellerCustomerIndex = -1;
        Boolean LoadCompleted = false;
        Boolean IsSalesInvoiceFileValid = false;
        Dictionary<String, Tuple<String, DataTable>> DictCurrCustomerBillNumItemDetails = null;
        Int32 cmbBoxBillNumberIndex = -1;
        Double OriginalBalanceAmount = -1;

        public CustomerInvoiceSellerOrderForm(Boolean IsSellerOrder, Boolean IsCustomerBill, Boolean IsUpdateCustomerBill = false)
        {
            try
            {
                InitializeComponent();
                MasterFilePath = CommonFunctions.MasterFilePath;
                CommonFunctions.ResetProgressBar();

                if (IsSellerOrder)
                {
                    FormTitle = "Create/Update Seller Order";
                    OrderInvoice = "Order";
                    txtBoxInvOrdNumber.Enabled = false;
                    btnCreateInvOrd.Text = "Create/Update Order";
                    btnCnclInvOrd.Text = "Void Order";

                    lblSelectName.Text = "Select Seller";
                    lblInvOrdFile.Text = "Sales Order File";
                    lblInvoiceNumber.Text = "Order#";
                    lblInvoiceDate.Text = "Order Date";

                    lblInvoiceNumber.Visible = false;
                    txtBoxInvOrdNumber.Visible = false;
                    btnDiscount.Enabled = false;
                    txtBoxSalesQuotFilePath.Visible = false;
                    lblQuotFile.Visible = false;
                    btnBrowseSalesQuotFile.Visible = false;
                    lblStatus.Text = "Please choose Sales Order date";

                    btnEditBalanceAmount.Enabled = false;
                    btnResetBalanceAmount.Enabled = false;
                }
                else if (IsCustomerBill)
                {
                    FormTitle = "Create Customer Bill";
                    OrderInvoice = "Bill";
                    txtBoxInvOrdNumber.Enabled = true;
                    txtBoxInvOrdNumber.ReadOnly = true;
                    btnCreateInvOrd.Text = "Create/Update Bill";
                    btnCnclInvOrd.Text = "Void Bill";
                    lblSelectName.Text = "Select Customer";
                    lblInvOrdFile.Text = "Invoice File";
                    lblQuotFile.Text = "Quotation File";
                    lblInvoiceNumber.Text = "Bill#";
                    lblInvoiceDate.Text = "Bill Date";
                    btnDiscount.Enabled = true;
                    txtBoxSalesQuotFilePath.Enabled = true;
                    txtBoxSalesQuotFilePath.ReadOnly = true;
                    btnBrowseSalesQuotFile.Enabled = false;
                    btnCnclInvOrd.Enabled = false;
                    btnCnclInvOrd.BackColor = Color.LightGray;
                    btnCnclInvOrd.ForeColor = Color.White;
                    lblSelectBillNum.Enabled = true;
                    lblSelectBillNum.Visible = true;
                    cmbBoxBillNumber.Enabled = true;
                    cmbBoxBillNumber.Visible = true;
                    lblStatus.Text = "Please choose Invoice/Quotation date";
                    dtGridViewInvOrdProdList.Columns[PriceColIndex].ReadOnly = false;
                }

                this.IsSellerOrder = IsSellerOrder;
                this.IsCustomerBill = IsCustomerBill;
                this.IsUpdateCustomerBill = IsUpdateCustomerBill;
                this.Text = FormTitle;
                txtSalesOrderFilePath.ReadOnly = true;
                btnBrowseSalesOrderFile.Enabled = false;
                picBoxLoading.Visible = false;
                dtGridViewProdListForSelection.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dtGridViewInvOrdProdList.SelectionMode = DataGridViewSelectionMode.CellSelect;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.ctor()", ex);
                throw;
            }
        }

        private void CustomerInvoiceForm_Load(object sender, EventArgs e)
        {
            try
            {
                //Populate cmbBoxSellerCustomer with Seller Names
                ListSellerNames = CommonFunctions.ObjSellerMaster.GetSellerList();
                cmbBoxSellerCustomer.DataSource = ListSellerNames;
                cmbBoxSellerCustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbBoxSellerCustomer.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbBoxSellerCustomer.SelectedIndex = -1;

                //Populate cmbBoxProdCat with Item Categories and cmbBoxProduct with Items
                List<String> ListItemCategories = new List<String>();
                ListItemCategories.Add("<ALL>");
                ListItemCategories.AddRange(CommonFunctions.ObjProductMaster.GetProductCategoryList());
                cmbBoxProdCat.DataSource = ListItemCategories;
                cmbBoxProdCat.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbBoxProdCat.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbBoxProdCat.SelectedIndex = -1;

                ListAllProducts = CommonFunctions.ObjProductMaster.GetProductListForCategory("<ALL>");
                ListAllProducts = ListAllProducts.OrderBy(p => p.ItemID).ToList();
                List<String> ListItems = new List<String>();
                ListItems.Add("<ALL>");
                ListItems.AddRange(ListAllProducts.Select(s => s.ItemName));
                cmbBoxProduct.DataSource = ListItems;
                cmbBoxProduct.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbBoxProduct.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbBoxProduct.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.CustomerInvoiceForm_Load()", ex);
            }
        }

        private void CustomerInvoiceSellerOrderForm_Shown(object sender, EventArgs e)
        {
            try
            {
                this.MaximizeBox = true;
                this.WindowState = FormWindowState.Maximized;
                this.StartPosition = FormStartPosition.CenterScreen;
                if (IsSellerOrder)
                {
                    String FileName = "SalesOrder_" + dtTmPckrInvOrdDate.Value.ToString("dd-MM-yyyy") + ".xlsx";
                    txtSalesOrderFilePath.Text = Path.GetDirectoryName(MasterFilePath) + @"\" + FileName;
                }

                if (IsCustomerBill)
                {
                    txtSalesOrderFilePath.Text = "";
                    if (CommonFunctions.ObjGeneralSettings.IsCustomerBillGenFormatInvoice)
                    {
                        String FileName = "Invoice_" + dtTmPckrInvOrdDate.Value.ToString("dd-MM-yyyy") + ".xlsx";
                        txtSalesOrderFilePath.Text = Path.GetDirectoryName(MasterFilePath) + @"\" + FileName;
                    }

                    txtBoxSalesQuotFilePath.Text = "";
                    if (CommonFunctions.ObjGeneralSettings.IsCustomerBillGenFormatQuotation)
                    {
                        String FileName = "Quotation_" + dtTmPckrInvOrdDate.Value.ToString("dd-MM-yyyy") + ".xlsx";
                        txtBoxSalesQuotFilePath.Text = Path.GetDirectoryName(MasterFilePath) + @"\" + FileName;
                    }
                }

                ResetControls();
                EnableItemsPanel(false);

                LoadCompleted = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.CustomerInvoiceSellerOrderForm_Shown()", ex);
            }
        }

        delegate void ReportProgressDel(Int32 ProgressState);
        ReportProgressDel ReportProgress = null;

        private void ReportProgressFunc(Int32 ProgressState)
        {
            if (ReportProgress == null) return;
            ReportProgress(ProgressState);
        }

        private void btnCreateInvOrd_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsSellerOrder)
                {
                    //Update Order Details to CurrSellerOrderDetails
                    if (CurrSellerOrderDetails == null) return;

                    DialogResult diagResult = MessageBox.Show(this, "Confirm to Create/Update Seller Order", "Create Seller Order", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    if (diagResult == DialogResult.No) return;

                    for (int i = 0; i < CurrSellerOrderDetails.ListItemQuantity.Count; i++)
                    {
                        CurrSellerOrderDetails.ListItemQuantity[i] = 0;
                    }

                    foreach (DataGridViewRow item in dtGridViewInvOrdProdList.Rows)
                    {
                        String ItemName = item.Cells[ItemColIndex].Value.ToString();
                        CurrSellerOrderDetails.ListItemQuantity[DictItemToColIndexes[ItemName.ToUpper()]] = Double.Parse(item.Cells[QtyColIndex].Value.ToString());
                    }
                    CurrSellerOrderDetails.OrderItemCount = CurrSellerOrderDetails.ListItemQuantity.Count(s => s > 0);

                    picBoxLoading.Visible = true;
                    lblStatus.Text = "Creating/Updating Seller order in Sales order file. Please wait...";
                    BackgroundTask = 3;
                    if (CurrSellerOrderDetails.ListItemQuantity.Zip(CurrSellerOrderDetails.ListItemOrigQuantity, (x, y) => Math.Abs(x - y)).Sum() > 0)
                    {
#if DEBUG
                        backgroundWorker1_DoWork(null, null);
                        backgroundWorker1_RunWorkerCompleted(null, null);
#else
                        ReportProgress = backgroundWorker1.ReportProgress;
                        backgroundWorker1.RunWorkerAsync();
                        backgroundWorker1.WorkerReportsProgress = true;
#endif
                    }
                    else
                    {
                        BackgroundTask = 7;
#if DEBUG
                        backgroundWorker1_DoWork(null, null);
                        backgroundWorker1_RunWorkerCompleted(null, null);
#else
                        ReportProgress = backgroundWorker1.ReportProgress;
                        backgroundWorker1.RunWorkerAsync();
                        backgroundWorker1.WorkerReportsProgress = true;
#endif
                    }
                }

                if (IsCustomerBill)
                {
                    //Create Invoice sheet for this Customer order
                    if (dtGridViewInvOrdProdList.Rows.Count == 0)
                    {
                        MessageBox.Show(this, "There are no Items in the Invoice.\nPlease add atleast one Item to the Invoice.", "Create Sales Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    Boolean IsValid = false;
                    for (int i = 0; i < dtGridViewInvOrdProdList.Rows.Count; i++)
                    {
                        if (Double.Parse(dtGridViewInvOrdProdList.Rows[i].Cells[QtyColIndex].Value.ToString()) > 0)
                        {
                            IsValid = true;
                            break;
                        }
                    }

                    if (!IsValid)
                    {
                        MessageBox.Show(this, "There are no Items with Quantity more than 0.\nPlease add atleast one Item with valid Quantity.", "Create Sales Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    DialogResult diagResult = MessageBox.Show(this, "Confirm to Create Customer Invoice", "Create Sales Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    if (diagResult == DialogResult.No) return;

                    picBoxLoading.Visible = true;
                    lblStatus.Text = "Creating Customer Bill in Sales Invoice/Quotation file. Please wait...";
                    BackgroundTask = 3;
#if DEBUG
                    backgroundWorker1_DoWork(null, null);
                    backgroundWorker1_RunWorkerCompleted(null, null);
#else
                    ReportProgress = backgroundWorker1.ReportProgress;
                    backgroundWorker1.RunWorkerAsync();
                    backgroundWorker1.WorkerReportsProgress = true;
#endif
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.btnCreateInvOrd_Click()", ex);
            }
        }

        void UpdateSalesOrderSheetForCurrSeller()
        {
            try
            {
                if (CurrSellerOrderDetails == null) return;

                Excel.Workbook xlSalesOrderWorkbook = xlApp.Workbooks.Open(txtSalesOrderFilePath.Text);
                Excel.Worksheet xlSalesOrderWorksheet = CommonFunctions.GetWorksheet(xlSalesOrderWorkbook, dtTmPckrInvOrdDate.Value.ToString("dd-MM-yyyy"));
                Int32 SellerRow = CurrSellerOrderDetails.SellerRowIndex, StartColumn = 1, DetailsCount = 5;

                Int32 ColumnCount = DetailsCount + CurrSellerOrderDetails.ListItemQuantity.Count;
                for (int i = StartColumn + DetailsCount; i <= ColumnCount; i++)
                {
                    Int32 ItemIndex = i - (StartColumn + DetailsCount);
                    if (CurrSellerOrderDetails.ListItemQuantity[ItemIndex] != CurrSellerOrderDetails.ListItemOrigQuantity[ItemIndex])
                    {
                        if (CurrSellerOrderDetails.ListItemQuantity[ItemIndex] > 0)
                            xlSalesOrderWorksheet.Cells[SellerRow, i].Value = CurrSellerOrderDetails.ListItemQuantity[ItemIndex];
                        else
                            xlSalesOrderWorksheet.Cells[SellerRow, i].Value = "";
                    }
                }

                xlSalesOrderWorkbook.Close(SaveChanges: true);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.UpdateSalesOrderSheetForCurrSeller()", ex);
            }
        }

        void LoadSalesOrderForCurrSeller()
        {
            try
            {
                if (CurrSellerOrderDetails == null) return;

                Excel.Workbook xlSalesOrderWorkbook = xlApp.Workbooks.Open(txtSalesOrderFilePath.Text);
                Excel.Worksheet xlSalesOrderWorksheet = CommonFunctions.GetWorksheet(xlSalesOrderWorkbook, dtTmPckrInvOrdDate.Value.ToString("dd-MM-yyyy"));
                Int32 SellerRow = CurrSellerOrderDetails.SellerRowIndex, StartColumn = 1, DetailsCount = 5;

                CurrSellerOrderDetails.ListItemQuantity = new List<Double>();
                Int32 ColumnCount = DetailsCount + DictItemToColIndexes.Count;

                for (int i = StartColumn + DetailsCount; i <= ColumnCount; i++)
                {
                    String Value = null;
                    if (xlSalesOrderWorksheet.Cells[SellerRow, i].Value != null)
                        Value = xlSalesOrderWorksheet.Cells[SellerRow, i].Value.ToString();

                    if (String.IsNullOrEmpty(Value))
                    {
                        CurrSellerOrderDetails.ListItemQuantity.Add(0);
                    }
                    else
                    {
                        Double result;
                        if (Double.TryParse(Value, out result))
                            CurrSellerOrderDetails.ListItemQuantity.Add(result);
                        else
                        {
                            KeyValuePair<String, Int32> item = DictItemToColIndexes.ElementAt(i - (StartColumn + DetailsCount));
                            MessageBox.Show(this, "Invalid Quantity in Sales Order sheet\nSeller:" + CurrSellerOrderDetails.SellerName + ", Item:" + item.Key + ", Quantity:" + Value + "\nIgnoring quantity for this item",
                                            "Quantity Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            CurrSellerOrderDetails.ListItemQuantity.Add(0);
                        }
                    }
                }
                CurrSellerOrderDetails.ListItemOrigQuantity = CurrSellerOrderDetails.ListItemQuantity.ToList();
                xlSalesOrderWorkbook.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.LoadSalesOrderForCurrSeller()", ex);
            }
        }

        void LoadInvQuotDataForCurrSeller()
        {
            try
            {
                String BillFilePath = "";
                Int32 CustomerNameRow = -1, CustomerNameCol = -1;
                Int32 BillNumRow = -1, BillNumCol = -1;
                Invoice objInvoice = null;

                if (CommonFunctions.ObjGeneralSettings.IsCustomerBillGenFormatQuotation)
                {
                    BillFilePath = txtBoxSalesQuotFilePath.Text;
                    CustomerNameRow = 1; CustomerNameCol = 2;
                    BillNumRow = 2; BillNumCol = 6;
                    objInvoice = CommonFunctions.GetInvoiceTemplate(ReportType.QUOTATION);
                }
                else if (CommonFunctions.ObjGeneralSettings.IsCustomerBillGenFormatInvoice)
                {
                    BillFilePath = txtSalesOrderFilePath.Text;
                    CustomerNameRow = 2; CustomerNameCol = 3;
                    BillNumRow = 2; BillNumCol = 10;
                    objInvoice = CommonFunctions.GetInvoiceTemplate(ReportType.INVOICE);
                }

                DictCurrCustomerBillNumItemDetails = new Dictionary<String, Tuple<String, DataTable>>();
                Excel.Workbook xlInvQuotWorkbook = xlApp.Workbooks.Open(BillFilePath);
                List<String> ListBills = new List<String>();
                List<String> ListSheets = new List<String>();
                foreach (Excel.Worksheet sheet in xlInvQuotWorkbook.Worksheets)
                {
                    if (sheet.Name.Equals("Item Summary") || sheet.Name.Equals("Seller Summary")) continue;

                    String CustomerName = sheet.Cells[CustomerNameRow, CustomerNameCol].Value;
                    String BillNumber = sheet.Cells[BillNumRow, BillNumCol].Value.ToString();
                    if (CustomerName.Equals(CurrSellerDetails.Name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        ListBills.Add(BillNumber); ListSheets.Add(sheet.Name);
                    }
                }
                xlInvQuotWorkbook.Close();

                //Load item details from Invoice/Quotation
                for (int i = 0; i < ListBills.Count; i++)
                {
                    DataTable dtBillItems = objInvoice.LoadInvoice(ListSheets[i], BillFilePath);
                    DictCurrCustomerBillNumItemDetails.Add(ListBills[i], new Tuple<String, DataTable>(ListSheets[i], dtBillItems));
                }

                cmbBoxBillNumber.Items.Clear();
                if (ListBills.Count > 0) cmbBoxBillNumber.Items.AddRange(ListBills.ToArray());
                cmbBoxBillNumber.Items.Add("<New>");
                cmbBoxBillNumber.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.LoadInvQuotDataForCurrSeller()", ex);
            }
        }

        void ResetControls()
        {
            try
            {
                cmbBoxProdCat.SelectedIndex = -1;
                cmbBoxProduct.SelectedIndex = -1;
                dtGridViewInvOrdProdList.Rows.Clear();
                dtGridViewProdListForSelection.Rows.Clear();
                ListSelectedRowIndexesToAdd.Clear();
                ListSelectedRowIndexesToRemove.Clear();
                DictItemsSelected.Clear();
                DiscountPerc = 0; DiscountValue = 0;

                UpdateSummaryDetails();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.ResetControls()", ex);
            }
        }

        private void btnCancelChanges_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewInvOrdProdList.Rows.Count == 0 && dtGridViewProdListForSelection.Rows.Count == 0) return;

                if (IsSellerOrder)
                {
                    BackgroundTask = 4;
                    DialogResult diagResult = MessageBox.Show(this, "Are you sure to cancel the changes made to Order?", "Cancel Order", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (diagResult == DialogResult.No) return;

                    backgroundWorker1_RunWorkerCompleted(null, null);
                }

                if (IsCustomerBill || IsUpdateCustomerBill)
                {
                    BackgroundTask = 4;
                    DialogResult diagResult = MessageBox.Show(this, "Are you sure to cancel the Bill?", "Cancel Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (diagResult == DialogResult.No) return;

                    backgroundWorker1_RunWorkerCompleted(null, null);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.btnCancelChanges_Click()", ex);
            }
        }

        private void btnVoidInvOrd_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewInvOrdProdList.Rows.Count == 0 && dtGridViewProdListForSelection.Rows.Count == 0) return;

                if (IsSellerOrder)
                {
                    DialogResult diagResult = MessageBox.Show(this, "Are you sure to cancel the order?\n(This will remove all items from the order and makes it void)", "Void Order", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (diagResult == DialogResult.No) return;

                    dtGridViewProdListForSelection.Rows.Clear();

                    for (int i = 0; i < CurrSellerOrderDetails.ListItemQuantity.Count; i++)
                    {
                        CurrSellerOrderDetails.ListItemQuantity[i] = 0;
                    }
                    CurrSellerOrderDetails.OrderItemCount = 0;

                    picBoxLoading.Visible = true;
                    lblStatus.Text = "Cancelling Seller order in Sales order file. Please wait...";
                    BackgroundTask = 3;
                    if (CurrSellerOrderDetails.ListItemOrigQuantity.Sum() > 0)
                    {
#if DEBUG
                    backgroundWorker1_DoWork(null, null);
                    backgroundWorker1_RunWorkerCompleted(null, null);
#else
                        ReportProgress = backgroundWorker1.ReportProgress;
                        backgroundWorker1.RunWorkerAsync();
                        backgroundWorker1.WorkerReportsProgress = true;
#endif
                    }
                    else
                    {
                        backgroundWorker1_RunWorkerCompleted(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.btnVoidInvOrd_Click()", ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (DictItemsSelected.Count > 0)
                {
                    DialogResult result = MessageBox.Show(this, "All changes made to this " + OrderInvoice + " will be lost.\nAre you sure to close the window?", "Close Window", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.No) return;
                }
                else
                {
                    DialogResult result = MessageBox.Show(this, "Are you sure to close the window?", "Close Window", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.No) return;
                }

                this.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.btnClose_Click()", ex);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            xlApp = new Excel.Application();
            try
            {
                switch (BackgroundTask)
                {
                    case 1:     //Load Sales Order Sheet
                        if (IsSellerOrder) LoadSalesOrderSheet();
                        break;
                    case 2:     //Load Seller Order details
                        LoadSalesOrderForCurrSeller();
                        break;
                    case 3:     //Update Seller Order details
                        if (IsSellerOrder)
                            UpdateSalesOrderSheetForCurrSeller();
                        if (IsCustomerBill)
                        {
                            if (CommonFunctions.ObjGeneralSettings.IsCustomerBillGenFormatInvoice)
                                CreateSalesInvoiceForCurrOrder(ReportType.INVOICE);
                            if (CommonFunctions.ObjGeneralSettings.IsCustomerBillGenFormatQuotation)
                                CreateSalesInvoiceForCurrOrder(ReportType.QUOTATION);
                        }
                        break;
                    case 5:     //Create default or load Invoice and Quotation file
                        //Create default Invoice or Quotation file
                        for (int i = 0; i < DefaultSaleReportTypesToCreate.Count; i++)
                        {
                            CreateDefaultSalesInvoiceSheet(DefaultSaleReportTypesToCreate[i]);
                        }

                        //Load Invoice or Quotation file
                        IsSalesInvoiceFileValid = true;
                        for (int i = 0; i < DefaultSaleReportTypesToLoad.Count; i++)
                        {
                            LoadSalesInvoiceSheet(DefaultSaleReportTypesToLoad[i]);
                        }
                        break;
                    case 6:     //Load Seller Invoice/Quotation details
                        LoadInvQuotDataForCurrSeller();
                        break;
                    case 7:
                        if (IsSellerOrder)
                            CreateSalesInvoiceForCurrOrder(ReportType.QUOTATION, false, true);
                        break;
                    default:
                        break;
                }
                ReportProgressFunc(100);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.backgroundWorker1_DoWork()", ex);
            }
            finally
            {
                xlApp.Quit();
                CommonFunctions.ReleaseCOMObject(xlApp);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CommonFunctions.UpdateProgressBar(e.ProgressPercentage);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                switch (BackgroundTask)
                {
                    case 1:     //Load all Sellers
                        EnableItemsPanel(false);
                        picBoxLoading.Visible = false;
                        if (IsSellerOrder)
                        {
                            dtTmPckrInvOrdDate.Enabled = false;
                            btnBrowseSalesOrderFile.Enabled = false;
                            MessageBox.Show(this, "Completed loading of Sales Order data", "Sales Order", MessageBoxButtons.OK);
                            lblStatus.Text = "Choose a Seller to create/update Sales Order";
                            cmbBoxSellerCustomer.Enabled = true;
                            cmbBoxSellerCustomer.Focus();
                        }
                        break;
                    case 2:     //Load Seller Order for Current Seller
                        if (CurrSellerOrderDetails.OrderItemCount == 0)
                        {
                            CurrSellerOrderDetails.ListItemQuantity = new List<Double>();
                            CurrSellerOrderDetails.ListItemOrigQuantity = new List<Double>();
                            for (int i = 0; i < DictItemToColIndexes.Count; i++)
                            {
                                CurrSellerOrderDetails.ListItemQuantity.Add(0);
                                CurrSellerOrderDetails.ListItemOrigQuantity.Add(0);
                            }
                        }
                        else
                        {
                            foreach (ProductDetails item in ListAllProducts)
                            {
                                if (!DictItemToColIndexes.ContainsKey(item.ItemName.ToUpper())) continue;
                                Double Qty = CurrSellerOrderDetails.ListItemQuantity[DictItemToColIndexes[item.ItemName.ToUpper()]];
                                if (Qty <= 0) continue;

                                Object[] row = new Object[5];
                                row[CategoryColIndex] = item.CategoryName; row[ItemColIndex] = item.ItemName;
                                Double Price = CommonFunctions.ObjProductMaster.GetPriceForProduct(item.ItemName, CurrSellerDetails.PriceGroupIndex);
                                row[PriceColIndex] = Price.ToString("F");
                                row[QtyColIndex] = Qty; row[SelectColIndex] = false;

                                Int32 Index = dtGridViewInvOrdProdList.Rows.Add(row);
                                DictItemsSelected.Add(item.ItemName, dtGridViewInvOrdProdList.Rows[Index]);
                            }
                        }
                        lblStatus.Text = "Add/Modify Items to Seller Sales order";

                        UpdateSummaryDetails();
                        EnableItemsPanel(true);
                        picBoxLoading.Visible = false;
                        MessageBox.Show(this, "Loaded Sales Order data for selected Seller", "Sales Order", MessageBoxButtons.OK);
                        break;
                    case 3:     //Update Seller Order for Current Seller or Create Sales Invoice
                    case 7:
                        cmbBoxSellerCustomer.SelectedIndex = -1;
                        ResetControls();
                        picBoxLoading.Visible = false;
                        EnableItemsPanel(false);
                        cmbBoxSellerCustomer.Enabled = true;
                        if (IsSellerOrder)
                        {
                            MessageBox.Show(this, "Created/Updated Sales Order successfully", "Sales Order", MessageBoxButtons.OK);
                            lblStatus.Text = "Choose a Seller to create/update Sales Order";
                        }

                        if (IsCustomerBill)
                        {
                            MessageBox.Show(this, "Created Customer Invoice successfully", "Sales Invoice", MessageBoxButtons.OK);
                            lblStatus.Text = "Choose a Customer to create Sales Invoice";
                            cmbBoxBillNumber.Items.Clear();
                            OriginalBalanceAmount = -1;
                            lblBalanceAmountValue.Text = "0.00";
                            txtBoxInvOrdNumber.Text = "";
                        }
                        CurrSellerOrderDetails = null;
                        CurrSellerDetails = null;
                        CurrSellerDiscountGroup = null;
                        break;
                    case 4:     //Cancel Seller Order for Current Seller or Cancel Sales Invoice
                        cmbBoxSellerCustomer.SelectedIndex = -1;
                        ResetControls();
                        picBoxLoading.Visible = false;
                        EnableItemsPanel(false);
                        cmbBoxSellerCustomer.Enabled = true;
                        if (IsSellerOrder)
                        {
                            MessageBox.Show(this, "Cancelled all changes made to Sales Order", "Cancel Sales Order", MessageBoxButtons.OK);
                            lblStatus.Text = "Choose a Seller to create/update Sales Order";
                        }

                        if (IsCustomerBill)
                        {
                            MessageBox.Show(this, "Customer Bill Cancelled", "Sales Invoice/Quotation", MessageBoxButtons.OK);
                            lblStatus.Text = "Choose a Customer to create Sales Invoice/Quotation";
                            cmbBoxBillNumber.Items.Clear();
                            OriginalBalanceAmount = -1;
                            lblBalanceAmountValue.Text = "0.00";
                            txtBoxInvOrdNumber.Text = "";
                        }
                        CurrSellerOrderDetails = null;
                        CurrSellerDetails = null;
                        CurrSellerDiscountGroup = null;
                        break;
                    case 5:
                        picBoxLoading.Visible = false;
                        if (IsCustomerBill)
                        {
                            if (DefaultSaleReportTypesToLoad.Count > 0)
                            {
                                if (IsSalesInvoiceFileValid)
                                {
                                    dtTmPckrInvOrdDate.Enabled = false;
                                    btnBrowseSalesOrderFile.Enabled = false;
                                    EnableItemsPanel(false);
                                    cmbBoxSellerCustomer.Enabled = true;
                                    if (DefaultSaleReportTypesToCreate.Count == 0)
                                    {
                                        MessageBox.Show(this, "Completed loading of Sales Invoice/Quotation data", "Sales Invoice/Quotation", MessageBoxButtons.OK);
                                        lblStatus.Text = "Choose a Customer to create Sales Invoice/Quotation";
                                        cmbBoxSellerCustomer.Focus();
                                    }
                                }
                                else
                                {
                                    EnableItemsPanel(false);
                                    lblStatus.Text = "Please choose a valid Sales Invoice/Quotation file";
                                }
                            }

                            if (DefaultSaleReportTypesToCreate.Count > 0)
                            {
                                dtTmPckrInvOrdDate.Enabled = false;
                                btnBrowseSalesOrderFile.Enabled = false;
                                EnableItemsPanel(false);
                                cmbBoxSellerCustomer.Enabled = true;
                                if (DefaultSaleReportTypesToLoad.Count == 0)
                                {
                                    MessageBox.Show(this, "Created default Sales Invoice/Quotation file successfully", "Sales Invoice/Quotation", MessageBoxButtons.OK);
                                    lblStatus.Text = "Choose a Customer to create Sales Invoice/Quotation";
                                    cmbBoxSellerCustomer.Focus();
                                }
                            }

                            if (DefaultSaleReportTypesToCreate.Count > 0 && DefaultSaleReportTypesToLoad.Count > 0)
                            {
                                dtTmPckrInvOrdDate.Enabled = true;
                                btnBrowseSalesOrderFile.Enabled = true;
                                EnableItemsPanel(false);
                                if (IsSalesInvoiceFileValid)
                                {
                                    dtTmPckrInvOrdDate.Enabled = false;
                                    btnBrowseSalesOrderFile.Enabled = false;
                                    cmbBoxSellerCustomer.Enabled = true;
                                    cmbBoxSellerCustomer.Focus();

                                    if (DefaultSaleReportTypesToLoad[0] == ReportType.INVOICE)
                                        MessageBox.Show(this, "Completed loading of Sales Invoice data", "Sales Invoice", MessageBoxButtons.OK);
                                    if (DefaultSaleReportTypesToLoad[0] == ReportType.QUOTATION)
                                        MessageBox.Show(this, "Completed loading of Sales Quotation data", "Sales Quotation", MessageBoxButtons.OK);
                                }
                                else
                                {
                                    if (DefaultSaleReportTypesToLoad[0] == ReportType.INVOICE)
                                        lblStatus.Text = "Please choose a valid Sales Invoice file";
                                    if (DefaultSaleReportTypesToLoad[0] == ReportType.QUOTATION)
                                        lblStatus.Text = "Please choose a valid Sales Quotation file";
                                }

                                if (DefaultSaleReportTypesToCreate[0] == ReportType.INVOICE)
                                    MessageBox.Show(this, "Created default Sales Invoice file successfully", "Sales Invoice", MessageBoxButtons.OK);
                                if (DefaultSaleReportTypesToCreate[0] == ReportType.QUOTATION)
                                    MessageBox.Show(this, "Created default Sales Quotation file successfully", "Sales Quotation", MessageBoxButtons.OK);

                                lblStatus.Text = "Choose a Customer to create Sales Invoice & Quotation";
                            }
                        }
                        break;
                    case 6:
                        lblStatus.Text = "Add/Modify Items to Customer Bill";

                        UpdateSummaryDetails();
                        EnableItemsPanel(true);
                        picBoxLoading.Visible = false;
                        MessageBox.Show(this, "Loaded Bill data for selected Customer", "Customer Bill", MessageBoxButtons.OK);
                        break;
                    default:
                        break;
                }
                CommonFunctions.ResetProgressBar();
                BackgroundTask = -1;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.backgroundWorker1_RunWorkerCompleted()", ex);
            }
        }

        private void cmbBoxProdCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxProdCat.SelectedIndex < 0 || !LoadCompleted) return;

                ListProducts = CommonFunctions.ObjProductMaster.GetProductListForCategory(cmbBoxProdCat.SelectedItem.ToString());
                ListProducts = ListProducts.OrderBy(p => p.ItemID).ToList();
                List<String> ListItems = new List<String>();
                ListItems.Add("<ALL>");
                ListItems.AddRange(ListProducts.Select(s => s.ItemName));
                cmbBoxProduct.DataSource = ListItems;
                cmbBoxProduct.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.cmbBoxProdCat_SelectedIndexChanged()", ex);
            }
        }

        private void cmbBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dtGridViewProdListForSelection.Rows.Clear();
                if (ListProducts == null) return;
                if (cmbBoxProduct.SelectedIndex < 0 || !LoadCompleted) return;

                //Load dtGridViewProdListForSelection based on item selection
                //Category  Product# Name    Price   Quantity    Select

                List<ProductDetails> tmpListProducts = new List<ProductDetails>();
                if (cmbBoxProduct.SelectedIndex == 0)
                    tmpListProducts.AddRange(ListProducts);
                else
                    tmpListProducts.AddRange(ListProducts.Where(s => s.ItemName.Equals(cmbBoxProduct.SelectedItem.ToString(), StringComparison.InvariantCultureIgnoreCase)));

                for (int i = 0; i < tmpListProducts.Count; i++)
                {
                    Double Price = CommonFunctions.ObjProductMaster.GetPriceForProduct(tmpListProducts[i].ItemName, CurrSellerDetails.PriceGroupIndex);
                    Price = Price * ((CommonFunctions.ObjProductMaster.GetTaxRatesForProduct(tmpListProducts[i].ItemName).Sum() + 100) / 100);

                    Object[] row = { tmpListProducts[i].CategoryName, tmpListProducts[i].ItemName,
                                     Price.ToString("F"), 0, false};

                    dtGridViewProdListForSelection.Rows.Add(row);
                }
                ListSelectedRowIndexesToAdd.Clear();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.cmbBoxProduct_SelectedIndexChanged()", ex);
            }
        }

        private void dtGridViewProdListForSelection_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != SelectColIndex || e.RowIndex < 0) return;

                dtGridViewProdListForSelection.CommitEdit(DataGridViewDataErrorContexts.Commit);

                if (Boolean.Parse(dtGridViewProdListForSelection.Rows[e.RowIndex].Cells[SelectColIndex].Value.ToString()))
                {
                    if (!ListSelectedRowIndexesToAdd.Contains(e.RowIndex)) ListSelectedRowIndexesToAdd.Add(e.RowIndex);
                }
                else
                {
                    ListSelectedRowIndexesToAdd.Remove(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.dtGridViewProdListForSelection_CellContentClick()", ex);
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewProdListForSelection.Rows.Count == 0) return;

                List<DataGridViewRow> ListSelectedRows = new List<DataGridViewRow>();
                if (ListSelectedRowIndexesToAdd.Count == 0)
                {
                    ListSelectedRows.Add(dtGridViewProdListForSelection.SelectedRows[0]);
                }
                else
                {
                    foreach (Int32 index in ListSelectedRowIndexesToAdd)
                    {
                        ListSelectedRows.Add(dtGridViewProdListForSelection.Rows[index]);
                    }
                }

                for (int j = 0; j < ListSelectedRows.Count; j++)
                {
                    Object[] row = new Object[dtGridViewProdListForSelection.Columns.Count];
                    for (int i = 0; i < row.Length; i++)
                    {
                        row[i] = ListSelectedRows[j].Cells[i].Value;
                    }
                    row[SelectColIndex] = false;
                    ListSelectedRows[j].Cells[SelectColIndex].Value = false;
                    ListSelectedRows[j].Cells[QtyColIndex].Value = 0;

                    if (!DictItemsSelected.ContainsKey(row[ItemColIndex].ToString()))
                    {
                        Int32 RowIndex = dtGridViewInvOrdProdList.Rows.Add(row);
                        DictItemsSelected.Add(row[ItemColIndex].ToString(), dtGridViewInvOrdProdList.Rows[RowIndex]);
                    }
                    else
                    {
                        DictItemsSelected[row[ItemColIndex].ToString()].Cells[QtyColIndex].Value = Double.Parse(DictItemsSelected[row[ItemColIndex].ToString()].Cells[QtyColIndex].Value.ToString()) + Double.Parse(row[QtyColIndex].ToString());
                    }
                }

                ListSelectedRowIndexesToAdd.Clear();
                dtGridViewProdListForSelection.CommitEdit(DataGridViewDataErrorContexts.Commit);

                UpdateSummaryDetails();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.btnAddItem_Click()", ex);
            }
        }

        private void btnRemItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewInvOrdProdList.Rows.Count == 0) return;

                List<DataGridViewRow> ListSelectedRows = new List<DataGridViewRow>();
                if (ListSelectedRowIndexesToRemove.Count == 0)
                {
                    ListSelectedRows.Add(dtGridViewInvOrdProdList.SelectedRows[0]);
                }
                else
                {
                    foreach (Int32 index in ListSelectedRowIndexesToRemove)
                    {
                        ListSelectedRows.Add(dtGridViewInvOrdProdList.Rows[index]);
                    }
                }

                for (int j = 0; j < ListSelectedRows.Count; j++)
                {
                    dtGridViewInvOrdProdList.Rows.Remove(ListSelectedRows[j]);
                    DictItemsSelected.Remove(ListSelectedRows[j].Cells[ItemColIndex].Value.ToString());
                }
                ListSelectedRowIndexesToRemove.Clear();

                UpdateSummaryDetails();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.btnRemItem_Click()", ex);
            }
        }

        private void dtGridViewInvOrdProdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != SelectColIndex || e.RowIndex < 0) return;

                dtGridViewInvOrdProdList.CommitEdit(DataGridViewDataErrorContexts.Commit);

                if (Boolean.Parse(dtGridViewInvOrdProdList.Rows[e.RowIndex].Cells[SelectColIndex].Value.ToString()))
                {
                    if (!ListSelectedRowIndexesToRemove.Contains(e.RowIndex)) ListSelectedRowIndexesToRemove.Add(e.RowIndex);
                }
                else
                {
                    ListSelectedRowIndexesToRemove.Remove(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.dtGridViewInvOrdProdList_CellContentClick()", ex);
            }
        }

        private void UpdateCustomerDetails()
        {
            try
            {
                SellerDetails sellerDetails = CommonFunctions.ObjSellerMaster.GetSellerDetails(cmbBoxSellerCustomer.SelectedValue.ToString());
                String CustomerDetails = sellerDetails.Name + "\n" + sellerDetails.Address + "\n" + sellerDetails.Phone;
                //CustomerDetails += "\nBalance: " + sellerDetails.OldBalance.ToString("F");
                lblCustomerDetails.Text = CustomerDetails;
                if (IsSellerOrder || cmbBoxBillNumber.SelectedItem.ToString().Equals("<New>") || OriginalBalanceAmount < 0)
                    lblBalanceAmountValue.Text = sellerDetails.OldBalance.ToString("F");
                else
                    lblBalanceAmountValue.Text = OriginalBalanceAmount.ToString("F");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.UpdateCustomerDetails()", ex);
            }
        }

        private void UpdateSummaryDetails()
        {
            try
            {
                Int32 NumItems;
                Double SubTotal = 0, Quantity = 0, Discount = 0, TaxAmount = 0, GrandTotal = 0;
                NumItems = dtGridViewInvOrdProdList.Rows.Count; Quantity = 0; SubTotal = 0; Discount = 0; TaxAmount = 0; GrandTotal = 0;

                foreach (DataGridViewRow item in dtGridViewInvOrdProdList.Rows)
                {
                    Double Price = Double.Parse(item.Cells[PriceColIndex].Value.ToString());
                    Double Qty = Double.Parse(item.Cells[QtyColIndex].Value.ToString());
                    Double Total = (Price * Qty);
                    Double tmpDiscount = 0;
                    Quantity += Qty;
                    if (DiscountPerc > 0) tmpDiscount = (Total * DiscountPerc / 100.0);
                    if (DiscountValue > 0) tmpDiscount = (DiscountValue / NumItems);
                    SubTotal += Total;
                    Total -= tmpDiscount;
                    Discount += tmpDiscount;

                    Double[] TaxRates = CommonFunctions.ObjProductMaster.GetTaxRatesForProduct(item.Cells[ItemColIndex].Value.ToString());
                    TaxAmount += (Total - Total / ((100 + TaxRates.Sum()) / 100));// (Total * TaxRates.Sum() / 100);
                }
                SubTotal = SubTotal - TaxAmount;
                GrandTotal = SubTotal - Discount + TaxAmount;

                lblNoOfItems.Text = NumItems.ToString().PadLeft(PaddingSpace, PaddingChar);
                lblTotalQty.Text = Quantity.ToString().PadLeft(PaddingSpace, PaddingChar);
                lblSubTotal.Text = CurrencyChar + SubTotal.ToString("F").PadLeft(PaddingSpace, PaddingChar);
                lblDiscount.Text = CurrencyChar + Discount.ToString("F").PadLeft(PaddingSpace, PaddingChar);
                lblTaxAmount.Text = CurrencyChar + TaxAmount.ToString("F").PadLeft(PaddingSpace, PaddingChar);
                lblGrandTtl.Text = CurrencyChar + GrandTotal.ToString("F").PadLeft(PaddingSpace, PaddingChar);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.UpdateSummaryDetails()", ex);
            }
        }

        private void cmbBoxSellerCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxSellerCustomer.SelectedIndex < 0 || !LoadCompleted)
                {
                    cmbBoxSellerCustomerIndex = -1;
                    cmbBoxBillNumberIndex = -1;
                    CurrSellerOrderDetails = null;
                    CurrSellerDetails = null;
                    CurrSellerDiscountGroup = null;
                    lblCustomerDetails.Text = "";
                    return;
                }

                if (IsSellerOrder)
                {
                    if (DictSellerToRowIndexes == null)
                    {
                        cmbBoxSellerCustomerIndex = cmbBoxSellerCustomer.SelectedIndex;
                        return;
                    }

                    //Check if the Order is changed before changing Seller
                    Boolean WarnUser = false;
                    if (CurrSellerOrderDetails != null)
                    {
                        if (cmbBoxSellerCustomer.SelectedIndex == cmbBoxSellerCustomerIndex) return;

                        if (dtGridViewInvOrdProdList.Rows.Count != CurrSellerOrderDetails.ListItemQuantity.Count(s => s > 0))
                        {
                            WarnUser = true;
                        }
                        else
                        {
                            foreach (DataGridViewRow item in dtGridViewInvOrdProdList.Rows)
                            {
                                String ItemName = item.Cells[ItemColIndex].Value.ToString();
                                Double Qty = CurrSellerOrderDetails.ListItemQuantity[DictItemToColIndexes[ItemName.ToUpper()]];

                                if (Qty != Double.Parse(item.Cells[QtyColIndex].Value.ToString()))
                                {
                                    WarnUser = true;
                                    break;
                                }
                            }
                        }

                        if (WarnUser)
                        {
                            DialogResult diagResult = MessageBox.Show(this, "All Changes made to this Seller Order will be lost.\nAre you sure to change the Seller?", "Change Seller", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                            if (diagResult == DialogResult.No)
                            {
                                cmbBoxSellerCustomer.SelectedIndex = cmbBoxSellerCustomerIndex;
                                return;
                            }
                        }
                    }
                    cmbBoxSellerCustomerIndex = cmbBoxSellerCustomer.SelectedIndex;
                    CurrSellerOrderDetails = null;
                    CurrSellerDetails = null;
                    CurrSellerDiscountGroup = null;
                    ResetControls();

                    //Load Selected Seller Order Details
                    String SellerName = cmbBoxSellerCustomer.SelectedValue.ToString().Trim();
                    if (!DictSellerToRowIndexes.ContainsKey(SellerName.ToUpper()))
                    {
                        MessageBox.Show(this, "Selected Seller does not exist in Sales Order Sheet\nChoose another Seller or Recreate Sales Order with all Sellers", "Seller Order", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Int32 SellerIndex = ListSellerOrderDetails.FindIndex(s => s.SellerName.Equals(SellerName, StringComparison.InvariantCultureIgnoreCase));
                    if (SellerIndex < 0)
                    {
                        MessageBox.Show(this, "Selected Seller does not exist in Seller Master\nChoose another Seller or Recreate Sales Order with all Sellers", "Seller Order", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    UpdateCustomerDetails();
                    CurrSellerOrderDetails = ListSellerOrderDetails[SellerIndex];
                    CurrSellerDetails = CommonFunctions.ObjSellerMaster.GetSellerDetails(CurrSellerOrderDetails.SellerName);
                    CurrSellerDiscountGroup = CommonFunctions.ObjSellerMaster.GetSellerDiscount(CurrSellerDetails.Name);
                    if (CurrSellerDiscountGroup.DiscountType == DiscountTypes.PERCENT)
                        DiscountPerc = CurrSellerDiscountGroup.Discount;
                    else if (CurrSellerDiscountGroup.DiscountType == DiscountTypes.ABSOLUTE)
                        DiscountValue = CurrSellerDiscountGroup.Discount;
                    CurrSellerOrderDetails.ListItemQuantity = null;
                    CurrSellerOrderDetails.ListItemOrigQuantity = null;

                    picBoxLoading.Visible = true;
                    lblStatus.Text = "Loading Seller order data from Sales order file. Please wait...";
                    BackgroundTask = 2;
                    if (CurrSellerOrderDetails.OrderItemCount > 0)
                    {
#if DEBUG
                        backgroundWorker1_DoWork(null, null);
                        backgroundWorker1_RunWorkerCompleted(null, null);
#else
                        ReportProgress = backgroundWorker1.ReportProgress;
                        backgroundWorker1.RunWorkerAsync();
                        backgroundWorker1.WorkerReportsProgress = true;
#endif
                    }
                    else
                    {
                        backgroundWorker1_RunWorkerCompleted(null, null);
                    }
                }

                if (IsCustomerBill)
                {
                    //Check if the Order is changed before changing Customer
                    if (cmbBoxSellerCustomer.SelectedIndex == cmbBoxSellerCustomerIndex) return;

                    EnableItemsPanel(false);
                    if (dtGridViewInvOrdProdList.Rows.Count > 0)
                    {
                        DialogResult diagResult = MessageBox.Show(this, "All Changes made to this Customer Invoice will be lost.\nAre you sure to change the Customer?", "Change Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        if (diagResult == DialogResult.No)
                        {
                            cmbBoxSellerCustomer.SelectedIndex = cmbBoxSellerCustomerIndex;
                            EnableItemsPanel(true);
                            return;
                        }
                    }

                    cmbBoxSellerCustomerIndex = cmbBoxSellerCustomer.SelectedIndex;
                    String SellerName = cmbBoxSellerCustomer.SelectedValue.ToString();
                    CurrSellerDetails = CommonFunctions.ObjSellerMaster.GetSellerDetails(SellerName);
                    CurrSellerDiscountGroup = CommonFunctions.ObjSellerMaster.GetSellerDiscount(CurrSellerDetails.Name);
                    if (CurrSellerDiscountGroup.DiscountType == DiscountTypes.PERCENT)
                        DiscountPerc = CurrSellerDiscountGroup.Discount;
                    else if (CurrSellerDiscountGroup.DiscountType == DiscountTypes.ABSOLUTE)
                        DiscountValue = CurrSellerDiscountGroup.Discount;
                    cmbBoxBillNumberIndex = -1;

                    ResetControls();

                    picBoxLoading.Visible = true;
                    lblStatus.Text = "Loading Seller order data from Invoice/Quotation file. Please wait...";
                    BackgroundTask = 6;
#if DEBUG
                    backgroundWorker1_DoWork(null, null);
                    backgroundWorker1_RunWorkerCompleted(null, null);
#else
                    ReportProgress = backgroundWorker1.ReportProgress;
                    backgroundWorker1.RunWorkerAsync();
                    backgroundWorker1.WorkerReportsProgress = true;
#endif
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.cmbBoxSellerCustomer_SelectedIndexChanged()", ex);
            }
        }

        private void btnItemDiscount_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.btnItemDiscount_Click()", ex);
            }
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            try
            {
                DiscountForm objDiscForm = new DiscountForm();
                objDiscForm.DiscountPerc = DiscountPerc;
                objDiscForm.DiscountValue = DiscountValue;
                DialogResult diagRes = objDiscForm.ShowDialog(this);
                if (diagRes == DialogResult.Cancel) return;

                DiscountPerc = 0; DiscountValue = 0;
                if (objDiscForm.DiscountPerc > -999)
                {
                    DiscountPerc = objDiscForm.DiscountPerc;
                }
                if (objDiscForm.DiscountValue > -999)
                {
                    DiscountValue = objDiscForm.DiscountValue;
                }

                UpdateSummaryDetails();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.btnDiscount_Click()", ex);
            }
        }

        private void btnHoldOrder_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.btnHoldOrder_Click()", ex);
            }
        }

        private void dtGridViewInvOrdProdList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.ColumnIndex != QtyColIndex && e.ColumnIndex != PriceColIndex) || e.RowIndex < 0) return;

                if (e.ColumnIndex == PriceColIndex)
                {
                    DataGridViewCell cell = dtGridViewInvOrdProdList.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ErrorText = null;
                    Double result;
                    if (!Double.TryParse(cell.Value.ToString(), out result))
                    {
                        cell.ErrorText = "Must be a valid Price";
                        return;
                    }

                    if (result < 0)
                    {
                        cell.ErrorText = "Must be a valid Price";
                        return;
                    }
                    cell.Value = result.ToString("F");
                }

                dtGridViewInvOrdProdList.CommitEdit(DataGridViewDataErrorContexts.Commit);
                UpdateSummaryDetails();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.dtGridViewInvOrdProdList_CellEndEdit()", ex);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean Checked = false;
                if (ListSelectedRowIndexesToAdd.Count != dtGridViewProdListForSelection.Rows.Count) Checked = true;
                ListSelectedRowIndexesToAdd.Clear();
                Int32 Index = 0;
                foreach (DataGridViewRow item in dtGridViewProdListForSelection.Rows)
                {
                    item.Cells[SelectColIndex].Value = Checked;
                    if (Checked) ListSelectedRowIndexesToAdd.Add(Index);
                    Index++;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.btnSelectAll_Click()", ex);
            }
        }

        private void btnSelectAllToRemove_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean Checked = false;
                if (ListSelectedRowIndexesToRemove.Count != dtGridViewInvOrdProdList.Rows.Count) Checked = true;
                ListSelectedRowIndexesToRemove.Clear();
                Int32 Index = 0;
                foreach (DataGridViewRow item in dtGridViewInvOrdProdList.Rows)
                {
                    item.Cells[SelectColIndex].Value = Checked;
                    if (Checked) ListSelectedRowIndexesToRemove.Add(Index);
                    Index++;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.btnSelectAll_Click()", ex);
            }
        }

        void EnableItemsPanel(Boolean enable)
        {
            try
            {
                panelOrderControls.Enabled = enable;
                cmbBoxSellerCustomer.Enabled = enable;
                cmbBoxProdCat.Enabled = enable;
                cmbBoxProduct.Enabled = enable;
                cmbBoxBillNumber.Enabled = enable;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.EnableItemsPanel()", ex);
            }
        }

        private void cmbBoxBillNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxBillNumber.SelectedIndex < 0 || !LoadCompleted || cmbBoxBillNumber.SelectedIndex == cmbBoxBillNumberIndex)
                    return;

                if (CurrSellerDetails == null) return;

                EnableItemsPanel(false);
                if (dtGridViewInvOrdProdList.Rows.Count > 0)
                {
                    DialogResult diagResult = MessageBox.Show(this, "All changes made to this Customer Bill will be lost.\nAre you sure to change the Bill?", "Change Customer Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (diagResult == DialogResult.No)
                    {
                        cmbBoxBillNumber.SelectedIndex = cmbBoxBillNumberIndex;
                        EnableItemsPanel(true);
                        return;
                    }
                }
                ResetControls();
                cmbBoxBillNumberIndex = cmbBoxBillNumber.SelectedIndex;

                String InvoiceNumber = "", BillNumber = cmbBoxBillNumber.SelectedItem.ToString();
                if (BillNumber.Equals("<New>"))
                {
                    if (CommonFunctions.ObjGeneralSettings.SummaryLocation == 0)
                    {
                        InvoiceNumber = "Inv:" + (CommonFunctions.ObjInvoiceSettings.LastNumber + 1).ToString();
                    }
                    if (CommonFunctions.ObjGeneralSettings.SummaryLocation == 1)
                    {
                        InvoiceNumber = "Quot:" + (CommonFunctions.ObjQuotationSettings.LastNumber + 1).ToString();
                    }
                    txtBoxInvOrdNumber.Text = InvoiceNumber;

                    OriginalBalanceAmount = -1;
                    UpdateCustomerDetails();
                    UpdateSummaryDetails();
                    EnableItemsPanel(true);
                    return;
                }

                //Load item details from Invoice/Quotation
                Int32 OrdQtyColNdx = -1, PriceColIndex = -1, BalanceAmountColIndex = -1;

                if (CommonFunctions.ObjGeneralSettings.IsCustomerBillGenFormatQuotation)
                {
                    OrdQtyColNdx = 2;
                    PriceColIndex = 4;
                    BalanceAmountColIndex = 5;
                }
                else if (CommonFunctions.ObjGeneralSettings.IsCustomerBillGenFormatInvoice)
                {
                    OrdQtyColNdx = 3;
                    PriceColIndex = 6;
                }

                DataTable dtBillItems = DictCurrCustomerBillNumItemDetails[cmbBoxBillNumber.SelectedItem.ToString()].Item2;

                foreach (DataRow dtRow in dtBillItems.Rows)
                {
                    Int32 ItemIndex = ListAllProducts.FindIndex(prod => prod.ItemName.Equals(dtRow[1].ToString(), StringComparison.InvariantCultureIgnoreCase));
                    if (ItemIndex < 0) continue;
                    ProductDetails item = ListAllProducts[ItemIndex];

                    Object[] row = new Object[5];
                    row[CategoryColIndex] = item.CategoryName; row[ItemColIndex] = item.ItemName;
                    Double Price = Double.Parse(dtRow[PriceColIndex].ToString());
                    row[this.PriceColIndex] = Price.ToString("F");
                    row[QtyColIndex] = dtRow[OrdQtyColNdx]; row[SelectColIndex] = false;

                    Int32 Index = dtGridViewInvOrdProdList.Rows.Add(row);
                    DictItemsSelected.Add(item.ItemName, dtGridViewInvOrdProdList.Rows[Index]);
                }

                OriginalBalanceAmount = -1;
                if (BalanceAmountColIndex >= 0)
                    OriginalBalanceAmount = Double.Parse(dtBillItems.Rows[dtBillItems.Rows.Count - 2][BalanceAmountColIndex].ToString());
                else
                    OriginalBalanceAmount = CommonFunctions.ObjSellerMaster.GetSellerDetails(cmbBoxSellerCustomer.SelectedValue.ToString()).OldBalance;

                InvoiceNumber = ""; BillNumber = cmbBoxBillNumber.SelectedItem.ToString();
                if (CommonFunctions.ObjGeneralSettings.SummaryLocation == 0)
                {
                    InvoiceNumber = "Inv:" + BillNumber;
                }
                if (CommonFunctions.ObjGeneralSettings.SummaryLocation == 1)
                {
                    InvoiceNumber = "Quot:" + BillNumber;
                }
                txtBoxInvOrdNumber.Text = InvoiceNumber;

                UpdateCustomerDetails();
                UpdateSummaryDetails();
                EnableItemsPanel(true);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.cmbBoxBillNumber_SelectedIndexChanged()", ex);
            }
        }

        private void btnEditBalanceAmount_Click(object sender, EventArgs e)
        {
            try
            {
                EditOldBalanceForm editOldBalanceForm = new EditOldBalanceForm(this, Double.Parse(lblBalanceAmountValue.Text));
                editOldBalanceForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.btnEditBalanceAmount_Click()", ex);
            }
        }

        public void UpdateBalanceAmount(Double NewBalanceAmount)
        {
            try
            {
                lblBalanceAmountValue.Text = NewBalanceAmount.ToString("F");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.UpdateBalanceAmount()", ex);
            }
        }

        private void btnResetBalanceAmountValue_Click(object sender, EventArgs e)
        {
            try
            {
                lblBalanceAmountValue.Text = OriginalBalanceAmount.ToString("F");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.btnResetBalanceAmountValue_Click()", ex);
            }
        }

        private void dtGridViewProdListForSelection_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != 3 || e.RowIndex < 0 || !ValueChanged) return;

                if (!ListSelectedRowIndexesToAdd.Contains(e.RowIndex)) ListSelectedRowIndexesToAdd.Add(e.RowIndex);

                btnAddItem_Click(null, null);

                ValueChanged = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.dtGridViewProdListForSelection_CellEndEdit()", ex);
            }
        }

        private void dtGridViewProdListForSelection_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != 3 || e.RowIndex < 0) return;

                Double result;
                DataGridViewCell cell = dtGridViewProdListForSelection.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ErrorText = null;
                if (!Double.TryParse(cell.Value.ToString(), out result))
                {
                    cell.ErrorText = "Must be a number";
                    return;
                }

                ValueChanged = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.dtGridViewProdListForSelection_CellValueChanged()", ex);
            }
        }

        private void btnSalesOrderFilePath_Click(object sender, EventArgs e)
        {
            try
            {
                opnFileDialog.InitialDirectory = Path.GetDirectoryName(MasterFilePath);
                opnFileDialog.Multiselect = false;
                DialogResult result = opnFileDialog.ShowDialog(this);
                if (result == System.Windows.Forms.DialogResult.Cancel) return;
                txtSalesOrderFilePath.Text = opnFileDialog.FileName;

                if (!File.Exists(txtSalesOrderFilePath.Text))
                {
                    String FileName = Path.GetFileName(txtSalesOrderFilePath.Text);
                    if (IsSellerOrder)
                    {
                        MessageBox.Show(this, "Sales Order file (" + FileName + ") does not exist."
                                            + "\nPlease create Sales Order File for this Date, before creating any Orders.",
                                            "Sales Order File", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }

                    if (IsCustomerBill)
                    {
                        MessageBox.Show(this, "Sales Invoice file (" + FileName + ") does not exist."
                                            + "\nPlease create Sales Invoice File for this Date, before creating any Invoice.",
                                            "Sales Invoice File", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                    return;
                }

                picBoxLoading.Visible = true;
                lblStatus.Text = "Loading data from Sales Order file. Please wait...";
                BackgroundTask = 1;
#if DEBUG
                backgroundWorker1_DoWork(null, null);
                backgroundWorker1_RunWorkerCompleted(null, null);
#else
                ReportProgress = backgroundWorker1.ReportProgress;
                backgroundWorker1.RunWorkerAsync();
                backgroundWorker1.WorkerReportsProgress = true;
#endif
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.btnSalesOrderFilePath_Click()", ex);
            }
        }

        List<String> ListCustomerInvoiceSheetNames = new List<String>(), ListCustomerQuotationSheetNames = new List<String>();
        private void dtTmPckrInvOrdDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                EnableItemsPanel(false);

                if (IsSellerOrder)
                {
                    String FileName = "SalesOrder_" + dtTmPckrInvOrdDate.Value.ToString("dd-MM-yyyy") + ".xlsx";
                    txtSalesOrderFilePath.Text = Path.GetDirectoryName(MasterFilePath) + @"\" + FileName;

                    if (!File.Exists(txtSalesOrderFilePath.Text))
                    {
                        MessageBox.Show(this, "Sales Order file (" + FileName + ") does not exist."
                                            + "\nPlease create Sales Order File for this Date, before creating any Orders.",
                                            "Sales Order File", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    picBoxLoading.Visible = true;
                    lblStatus.Text = "Loading data from Sales Order file, please wait.....";
                    BackgroundTask = 1;
#if DEBUG
                    backgroundWorker1_DoWork(null, null);
                    backgroundWorker1_RunWorkerCompleted(null, null);
#else
                    ReportProgress = backgroundWorker1.ReportProgress;
                    backgroundWorker1.RunWorkerAsync();
                    backgroundWorker1.WorkerReportsProgress = true;
#endif
                }

                if (IsCustomerBill)
                {
                    if (CommonFunctions.ObjGeneralSettings.IsCustomerBillGenFormatInvoice)
                        CheckSalesInvoiceQuotationFile(ReportType.INVOICE);
                    if (CommonFunctions.ObjGeneralSettings.IsCustomerBillGenFormatQuotation)
                        CheckSalesInvoiceQuotationFile(ReportType.QUOTATION);

                    //Create an Invoice File with default ItemSummary and Seller Summary
                    picBoxLoading.Visible = true;
                    BackgroundTask = 5;
#if DEBUG
                    backgroundWorker1_DoWork(null, null);
                    backgroundWorker1_RunWorkerCompleted(null, null);
#else
                    ReportProgress = backgroundWorker1.ReportProgress;
                    backgroundWorker1.RunWorkerAsync();
                    backgroundWorker1.WorkerReportsProgress = true;
#endif
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.dtTmPckrInvOrdDate_ValueChanged()", ex);
            }
        }

        List<ReportType> DefaultSaleReportTypesToCreate = new List<ReportType>();
        List<ReportType> DefaultSaleReportTypesToLoad = new List<ReportType>();
        private Boolean CheckSalesInvoiceQuotationFile(ReportType enumReportType)
        {
            try
            {
                String InvoiceQuotation = "";
                String FileName = "";
                if (enumReportType == ReportType.INVOICE)
                {
                    ListCustomerInvoiceSheetNames = new List<String>();
                    InvoiceQuotation = "Invoice";
                    FileName = Path.GetDirectoryName(MasterFilePath) + @"\" + InvoiceQuotation + "_" + dtTmPckrInvOrdDate.Value.ToString("dd-MM-yyyy") + ".xlsx";
                    txtSalesOrderFilePath.Text = FileName;
                }
                if (enumReportType == ReportType.QUOTATION)
                {
                    ListCustomerQuotationSheetNames = new List<String>();
                    InvoiceQuotation = "Quotation";
                    FileName = Path.GetDirectoryName(MasterFilePath) + @"\" + InvoiceQuotation + "_" + dtTmPckrInvOrdDate.Value.ToString("dd-MM-yyyy") + ".xlsx";
                    txtBoxSalesQuotFilePath.Text = FileName;
                }

                if (!File.Exists(FileName))
                {
                    DialogResult result = MessageBox.Show(this, "Sales " + InvoiceQuotation + " file (" + FileName + ") does not exist."
                                        + "\nDo you want to create default Sales " + InvoiceQuotation + " File for this Date?",
                                        "Sales " + InvoiceQuotation + " File", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.No)
                    {
                        MessageBox.Show(this, "Please select a date with Sales " + InvoiceQuotation + " file, before you create Customer Bills.",
                                            "Sales " + InvoiceQuotation + " File", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        return false;
                    }

                    DefaultSaleReportTypesToCreate.Add(enumReportType);
                }
                else
                {
                    DefaultSaleReportTypesToLoad.Add(enumReportType);
                }

                return true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.CheckSalesInvoiceQuotationFile()", ex);
            }
            return false;
        }

        private void LoadSalesOrderSheet()
        {
            try
            {
                Excel.Workbook xlSalesOrderWorkbook = xlApp.Workbooks.Open(txtSalesOrderFilePath.Text);
                Excel.Worksheet xlSalesOrderWorksheet = CommonFunctions.GetWorksheet(xlSalesOrderWorkbook, dtTmPckrInvOrdDate.Value.ToString("dd-MM-yyyy"));
                Int32 StartRow = 5, StartColumn = 1, DetailsCount = 5;

                #region Identify Items in SalesOrderSheet
                DictItemToColIndexes = new Dictionary<String, Int32>();
                Int32 ColumnCount = xlSalesOrderWorksheet.UsedRange.Columns.Count;
                Int32 RowCount = xlSalesOrderWorksheet.UsedRange.Rows.Count + 1;
                Int32 ProgressBarCount = ((ColumnCount - (StartColumn + DetailsCount)) + (RowCount - (StartRow + 1)));
                for (int i = StartColumn + DetailsCount; i <= ColumnCount; i++)
                {
                    String ItemName = xlSalesOrderWorksheet.Cells[StartRow, i].Value;
                    ItemName = ItemName.Trim();
                    Int32 ItemIndex = ListAllProducts.FindIndex(e => e.ItemName.Equals(ItemName, StringComparison.InvariantCultureIgnoreCase));
                    if (ItemIndex < 0)
                    {
                        DictItemToColIndexes.Add(ItemName.ToUpper(), -1);
                        MessageBox.Show(this, "Item:" + ItemName + " not found in ItemMaster, Skipping this Item", "Item error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else DictItemToColIndexes.Add(ListAllProducts[ItemIndex].ItemName.ToUpper(), i - (StartColumn + DetailsCount));
                    ReportProgressFunc((i - (StartColumn + DetailsCount)) * 100 / ProgressBarCount);
                }
                #endregion

                #region Identify Sellers in SalesOrderSheet
                ListSellerOrderDetails = new List<SellerOrderDetails>();
                DictSellerToRowIndexes = new Dictionary<String, Int32>();
                for (int i = StartRow + 1; i <= RowCount; i++)
                {
                    if (xlSalesOrderWorksheet.Cells[i, StartColumn + 1].Value == null) continue;
                    if (xlSalesOrderWorksheet.Cells[i, StartColumn + 2].Value == null) continue;
                    String SellerName = xlSalesOrderWorksheet.Cells[i, StartColumn + 2].Value;
                    SellerName = SellerName.Trim();
                    Int32 SellerIndex = ListSellerNames.FindIndex(e => e.Equals(SellerName, StringComparison.InvariantCultureIgnoreCase));
                    ReportProgressFunc((i - (StartColumn + 1) + (ColumnCount - (StartColumn + DetailsCount))) * 100 / ProgressBarCount);
                    if (SellerIndex < 0)
                    {
                        MessageBox.Show(this, "Seller:" + SellerName + " not found in SellerMaster, Skipping this seller", "Seller error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    DictSellerToRowIndexes.Add(ListSellerNames[SellerIndex].ToUpper(), i);
                    SellerOrderDetails tmpSellerOrderDetails = new SellerOrderDetails();
                    tmpSellerOrderDetails.SellerName = ListSellerNames[SellerIndex];
                    tmpSellerOrderDetails.SellerRowIndex = i;
                    if (xlSalesOrderWorksheet.Cells[i, StartColumn + 1].Value != null)
                        tmpSellerOrderDetails.OrderItemCount = Int32.Parse(xlSalesOrderWorksheet.Cells[i, StartColumn + 1].Value.ToString());
                    else tmpSellerOrderDetails.OrderItemCount = 0;

                    ListSellerOrderDetails.Add(tmpSellerOrderDetails);
                }
                #endregion

                xlSalesOrderWorkbook.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.LoadSalesOrderSheet()", ex);
            }
        }

        void LoadSalesInvoiceSheet(ReportType enumReportType)
        {
            try
            {
                String FilePath = "";
                if (enumReportType == ReportType.INVOICE)
                {
                    FilePath = txtSalesOrderFilePath.Text;
                    ListCustomerInvoiceSheetNames.Clear();
                }
                if (enumReportType == ReportType.QUOTATION)
                {
                    FilePath = txtBoxSalesQuotFilePath.Text;
                    ListCustomerQuotationSheetNames.Clear();
                }
                Excel.Workbook ObjWorkbook = xlApp.Workbooks.Open(FilePath);
                Int32 Count = 0;
                Int32 ProgressbarCount = ObjWorkbook.Sheets.Count;
                for (int i = 1; i <= ObjWorkbook.Sheets.Count; i++)
                {
                    String SheetName = ObjWorkbook.Worksheets[i].Name;
                    if (SheetName.Equals("Item Summary", StringComparison.InvariantCultureIgnoreCase)
                        || SheetName.Equals("Seller Summary", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Count++;
                        continue;
                    }
                    if (enumReportType == ReportType.INVOICE) ListCustomerInvoiceSheetNames.Add(SheetName);
                    if (enumReportType == ReportType.QUOTATION) ListCustomerQuotationSheetNames.Add(SheetName);
                    ReportProgressFunc(i * 100 / ProgressbarCount);
                }

                ObjWorkbook.Close(SaveChanges: false);

                if (Count < 2
                    && ((CommonFunctions.ObjGeneralSettings.SummaryLocation == 0 && enumReportType == ReportType.INVOICE)
                    || (CommonFunctions.ObjGeneralSettings.SummaryLocation == 1 && enumReportType == ReportType.QUOTATION)))
                {
                    if (enumReportType == ReportType.INVOICE)
                        MessageBox.Show(this, "Invalid Sales Invoice file.\nPlease select Invoice file with Summary Sheets", "Sales Invoice File", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    if (enumReportType == ReportType.QUOTATION)
                        MessageBox.Show(this, "Invalid Sales Quotation file.\nPlease select Quotation file with Summary Sheets", "Sales Quotation File", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    IsSalesInvoiceFileValid &= false;
                }
                else
                {
                    IsSalesInvoiceFileValid &= true;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.LoadSalesInvoiceSheet()", ex);
            }
        }

        void CreateDefaultSalesInvoiceSheet(ReportType enumReportType)
        {
            try
            {
                String FileName = "";
                if (enumReportType == ReportType.INVOICE) FileName = txtSalesOrderFilePath.Text;
                if (enumReportType == ReportType.QUOTATION) FileName = txtBoxSalesQuotFilePath.Text;
                ReportProgressFunc(0);
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Add();

                #region Print Seller Summary Sheet
                Int32 SummaryStartRow = 0, CurrRow = 0, CurrCol = 0;
                Excel.Worksheet xlSellerSummaryWorkSheet = xlWorkbook.Worksheets.Add(xlWorkbook.Sheets[1]);
                xlSellerSummaryWorkSheet.Name = "Seller Summary";

                SummaryStartRow++;
                Excel.Range xlRange1 = xlSellerSummaryWorkSheet.Cells[SummaryStartRow, 1];
                xlRange1.Value = "Date";
                xlRange1.Font.Bold = true;
                xlRange1 = xlSellerSummaryWorkSheet.Cells[SummaryStartRow, 2];
                xlRange1.Value = dtTmPckrInvOrdDate.Value.ToString("dd-MMM-yyyy");
                xlRange1 = xlSellerSummaryWorkSheet.Range[xlSellerSummaryWorkSheet.Cells[SummaryStartRow, 2], xlSellerSummaryWorkSheet.Cells[SummaryStartRow, 3]];
                xlRange1.Merge();
                xlRange1.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

                CurrRow = SummaryStartRow + 1;
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Sl#";
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Line";
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Bill#";
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Seller Name";
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Sale";
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Cancel";
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Return";
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Discount";
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Total Tax";
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Net Sale";
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "OB";
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = "Cash";
                Int32 LastCol = CurrCol;
                xlRange1 = xlSellerSummaryWorkSheet.Range[xlSellerSummaryWorkSheet.Cells[CurrRow, 1], xlSellerSummaryWorkSheet.Cells[CurrRow, LastCol]];
                xlRange1.Font.Bold = true;
                ReportProgressFunc(50);
                #endregion

                #region Print Item Summary Sheet
                SummaryStartRow = 0;
                Double Total = 0;
                Excel.Worksheet xlSummaryWorkSheet = xlWorkbook.Worksheets.Add(xlWorkbook.Sheets[1]);
                xlSummaryWorkSheet.Name = "Item Summary";
                xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 1].Value = "Sl.No.";
                xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 2].Value = "Item Name";
                xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 3].Value = "Vendor Name";
                xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 4].Value = "Quantity";
                xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 5].Value = "Price";
                xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 6].Value = "Total";
                xlRange1 = xlSummaryWorkSheet.Range[xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 1], xlSummaryWorkSheet.Cells[SummaryStartRow + 1, 6]];
                xlRange1.Font.Bold = true;

                for (int i = 0; i < ListAllProducts.Count; i++)
                {
                    xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 1].Value = (i + 1).ToString();
                    xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 2].Value = ListAllProducts[i].ItemName;
                    xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 3].Value = ListAllProducts[i].VendorName;
                    xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 4].Value = "0";
                    xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 5].Value = ListAllProducts[i].PurchasePrice;
                    xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 5].NumberFormat = "#,##0.00";
                    xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 6].Value = "0";
                    xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 6].NumberFormat = "#,##0.00";
                    Total += Double.Parse(xlSummaryWorkSheet.Cells[i + SummaryStartRow + 2, 6].Value.ToString());
                    ReportProgressFunc(50 + (i * 50 / ListAllProducts.Count));
                }

                Excel.Range tmpxlRange = xlSummaryWorkSheet.Cells[ListAllProducts.Count + SummaryStartRow + 2, 5];
                tmpxlRange.Value = "Total";
                tmpxlRange.Font.Bold = true;

                tmpxlRange = xlSummaryWorkSheet.Cells[ListAllProducts.Count + SummaryStartRow + 2, 6];
                tmpxlRange.Value = Total;
                tmpxlRange.Font.Bold = true;
                tmpxlRange.NumberFormat = "#,##0.00";
                xlSummaryWorkSheet.UsedRange.Columns.AutoFit();
                xlApp.DisplayAlerts = false;
                xlApp.DisplayAlerts = true;
                #endregion

                if (xlWorkbook.Sheets["Sheet1"] != null) xlWorkbook.Sheets["Sheet1"].Delete();
                if (xlWorkbook.Sheets["Sheet2"] != null) xlWorkbook.Sheets["Sheet2"].Delete();
                if (xlWorkbook.Sheets["Sheet3"] != null) xlWorkbook.Sheets["Sheet3"].Delete();

                xlWorkbook.Close(SaveChanges: true, Filename: FileName);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.CreateDefaultSalesInvoiceSheet()", ex);
            }
        }

        void CreateSalesInvoiceForCurrOrder(ReportType EnumReportType, Boolean CreateSummarySheet = true, Boolean IsDummyBill = false)
        {
            try
            {
                Boolean PrintOldBalance = false;
                ReportSettings CurrReportSettings = null;
                String BillNumberText = "", SaveFileName = "";
                String OutputFolder = Path.GetDirectoryName(MasterFilePath);
                String SelectedDateTimeString = dtTmPckrInvOrdDate.Value.ToString("dd-MM-yyyy");
                Boolean PrintBill = false;
                String InvoiceQuotation = "";
                List<String> ListSheetNames = null;
                Boolean CreateSummary = false;
                switch (EnumReportType)
                {
                    case ReportType.INVOICE:
                        CurrReportSettings = CommonFunctions.ObjInvoiceSettings;
                        BillNumberText = "Invoice#";
                        SaveFileName = OutputFolder + "\\Invoice_" + SelectedDateTimeString + ".xlsx";
                        InvoiceQuotation = "Invoice";
                        PrintBill = CommonFunctions.ObjGeneralSettings.IsCustomerBillPrintFormatInvoice;
                        ListSheetNames = ListCustomerInvoiceSheetNames;
                        CreateSummary = (CommonFunctions.ObjGeneralSettings.SummaryLocation == 0);
                        break;
                    case ReportType.QUOTATION:
                        CurrReportSettings = CommonFunctions.ObjQuotationSettings;
                        PrintOldBalance = true;
                        BillNumberText = "Quotation#";
                        SaveFileName = OutputFolder + "\\Quotation_" + SelectedDateTimeString + ".xlsx";
                        InvoiceQuotation = "Quotation";
                        PrintBill = CommonFunctions.ObjGeneralSettings.IsCustomerBillPrintFormatQuotation;
                        ListSheetNames = ListCustomerQuotationSheetNames;
                        CreateSummary = (CommonFunctions.ObjGeneralSettings.SummaryLocation == 1);
                        break;
                    default:
                        return;
                }
                CreateSummary &= CreateSummarySheet;
                if (IsDummyBill)
                {
                    SaveFileName = Path.GetDirectoryName(SaveFileName) + "\\" + Path.GetFileNameWithoutExtension(SaveFileName) + "_Dummy" + Path.GetExtension(SaveFileName);
                }

                Boolean IsExistingBill = (IsDummyBill ? false : !cmbBoxBillNumber.SelectedItem.ToString().Equals("<New>"));
                Int32 InvoiceNumber = CurrReportSettings.LastNumber + 1;
                if (IsExistingBill) InvoiceNumber = Int32.Parse(cmbBoxBillNumber.SelectedItem.ToString());
                Int32 ValidItemCount = dtGridViewInvOrdProdList.Rows.Count;
                Int32 ProgressBarCount = ValidItemCount;
                Int32 Counter = 0, SLNo = 0;
                Double Quantity, Price;

                SLNo = 0;
                SellerDetails ObjCurrentSeller = CommonFunctions.ObjSellerMaster.GetSellerDetails(cmbBoxSellerCustomer.SelectedValue.ToString());
                DiscountGroupDetails ObjDiscountGroup = CommonFunctions.ObjSellerMaster.GetSellerDiscount(ObjCurrentSeller.Name);

                Invoice ObjInvoice = CommonFunctions.GetInvoiceTemplate(EnumReportType);
                ObjInvoice.SerialNumber = (IsDummyBill ? "Dummy" : InvoiceNumber.ToString());
                ObjInvoice.InvoiceNumberText = BillNumberText;
                ObjInvoice.ObjSellerDetails = ObjCurrentSeller.Clone();
                ObjInvoice.ObjSellerDetails.OldBalance = Double.Parse(lblBalanceAmountValue.Text);
                ObjInvoice.CurrReportSettings = CurrReportSettings;
                ObjInvoice.DateOfInvoice = dtTmPckrInvOrdDate.Value;
                ObjInvoice.PrintOldBalance = PrintOldBalance;
                ObjInvoice.UseNumberToWordsFormula = false;
                ObjInvoice.ListProducts = new List<ProductDetailsForInvoice>();

                Excel.Workbook xlWorkbook = (IsDummyBill ? xlApp.Workbooks.Add() : xlApp.Workbooks.Open(SaveFileName));
                Excel.Worksheet xlWorkSheet = null;

                #region Change SheetName
                String SheetName = "";
                if (IsExistingBill)
                {
                    SheetName = DictCurrCustomerBillNumItemDetails[InvoiceNumber.ToString()].Item1;
                    xlWorkSheet = CommonFunctions.GetWorksheet(xlWorkbook, SheetName);
                    xlApp.DisplayAlerts = false;
                    xlWorkSheet.Cells.Clear();
                    xlApp.DisplayAlerts = true;
                }
                else
                {
                    xlWorkSheet = xlWorkbook.Worksheets.Add(Type.Missing, xlWorkbook.Sheets[xlWorkbook.Sheets.Count]);

                    SheetName = ObjCurrentSeller.Name.Replace(":", "").Replace("\\", "").Replace("/", "").
                            Replace("?", "").Replace("*", "").Replace("[", "").Replace("]", "");
                    SheetName = ((SheetName.Length > 30) ? SheetName.Substring(0, 30) : SheetName);
                    Int32 SheetSuffix = 0;
                    Boolean ContainsCustomerSheet = false;
                    if (ListSheetNames.Count > 0)
                    {
                        for (int i = 0; i < ListSheetNames.Count; i++)
                        {
                            if (ListSheetNames[i].Contains(SheetName))
                            {
                                String NumberStr = ListSheetNames[i].Replace(SheetName, "").Trim();
                                Int32 Number;
                                if (Int32.TryParse(NumberStr, out Number))
                                {
                                    SheetSuffix = Math.Max(Number, SheetSuffix);
                                }
                                ContainsCustomerSheet = true;
                            }
                        }

                        if (ContainsCustomerSheet || SheetSuffix > 0)
                        {
                            SheetSuffix++;
                            SheetName += " " + SheetSuffix;
                        }
                    }
                    ListSheetNames.Add(SheetName);
                }
                ObjInvoice.SheetName = SheetName;
                #endregion

                #region Print Invoice Items
                String ItemName;
                for (int i = 0; i < dtGridViewInvOrdProdList.Rows.Count; i++)
                {
                    Counter++;
                    ReportProgressFunc((Counter * 100) / ProgressBarCount);

                    Quantity = Double.Parse(dtGridViewInvOrdProdList.Rows[i].Cells[QtyColIndex].Value.ToString());
                    ItemName = dtGridViewInvOrdProdList.Rows[i].Cells[ItemColIndex].Value.ToString();
                    if (Double.Parse(dtGridViewInvOrdProdList.Rows[i].Cells[QtyColIndex].Value.ToString()) == 0) continue;
                    Price = Double.Parse(dtGridViewInvOrdProdList.Rows[i].Cells[PriceColIndex].Value.ToString());
                    Price = Price / ((CommonFunctions.ObjProductMaster.GetTaxRatesForProduct(ItemName).Sum() + 100) / 100);

                    SLNo++;
                    ProductDetailsForInvoice ObjProductDetailsForInvoice = new ProductDetailsForInvoice();
                    ProductDetails ObjProductDetails = CommonFunctions.ObjProductMaster.GetProductDetails(ItemName);
                    if (ObjProductDetails == null)
                    {
                        xlWorkbook.Close();
                        CommonFunctions.ReleaseCOMObject(xlWorkbook);
                        MessageBox.Show(this, "Unable to find Item \"" + ItemName + "\" in ItemMaster", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    ObjProductDetailsForInvoice.SerialNumber = SLNo;
                    ObjProductDetailsForInvoice.Description = ObjProductDetails.ItemName;
                    ObjProductDetailsForInvoice.HSNCode = ObjProductDetails.HSNCode;
                    ObjProductDetailsForInvoice.UnitsOfMeasurement = ObjProductDetails.UnitsOfMeasurement;
                    ObjProductDetailsForInvoice.OrderQuantity = Quantity;
                    ObjProductDetailsForInvoice.SaleQuantity = Quantity;
                    ObjProductDetailsForInvoice.Rate = Price; //CommonFunctions.ObjProductMaster.GetPriceForProduct(ObjProductDetails.ItemName, ObjCurrentSeller.PriceGroupIndex);

                    Double[] TaxRates = CommonFunctions.ObjProductMaster.GetTaxRatesForProduct(ObjProductDetails.ItemName);
                    ObjProductDetailsForInvoice.CGSTDetails = new TaxDetails();
                    ObjProductDetailsForInvoice.CGSTDetails.TaxRate = TaxRates[0] / 100;
                    ObjProductDetailsForInvoice.SGSTDetails = new TaxDetails();
                    ObjProductDetailsForInvoice.SGSTDetails.TaxRate = TaxRates[1] / 100;
                    ObjProductDetailsForInvoice.IGSTDetails = new TaxDetails();
                    ObjProductDetailsForInvoice.IGSTDetails.TaxRate = TaxRates[2] / 100;
                    ObjProductDetailsForInvoice.DiscountGroup = ObjDiscountGroup.Clone();
                    if (DiscountPerc > 0)
                    {
                        ObjProductDetailsForInvoice.DiscountGroup.DiscountType = DiscountTypes.PERCENT;
                        ObjProductDetailsForInvoice.DiscountGroup.Discount = DiscountPerc;
                    }
                    else if (DiscountValue > 0)
                    {
                        ObjProductDetailsForInvoice.DiscountGroup.DiscountType = DiscountTypes.ABSOLUTE;
                        ObjProductDetailsForInvoice.DiscountGroup.Discount = (DiscountValue / dtGridViewInvOrdProdList.Rows.Count);
                    }
                    ObjInvoice.ListProducts.Add(ObjProductDetailsForInvoice);
                }
                #endregion

                #region Discount
                if (EnumReportType == ReportType.INVOICE)
                {
                    ObjInvoice.CreateInvoice(xlWorkSheet);
                }
                else if (EnumReportType == ReportType.QUOTATION)
                {
                    //Override Discount and rollback after creating Quotation
                    DiscountGroupDetails OrigDiscountGroup = ObjDiscountGroup.Clone();
                    if (DiscountPerc > 0)
                    {
                        ObjDiscountGroup.DiscountType = DiscountTypes.PERCENT;
                        ObjDiscountGroup.Discount = DiscountPerc;
                    }
                    else if (DiscountValue > 0)
                    {
                        ObjDiscountGroup.DiscountType = DiscountTypes.ABSOLUTE;
                        ObjDiscountGroup.Discount = DiscountValue;
                    }
                    ObjInvoice.CreateInvoice(xlWorkSheet);
                    ObjDiscountGroup.DiscountType = OrigDiscountGroup.DiscountType;
                    ObjDiscountGroup.Discount = OrigDiscountGroup.Discount;
                }
                #endregion

                #region Update Seller Summary sheet with this Invoice data
                if (CreateSummary)
                {
                    Excel.Worksheet xlSellerSummaryWorkSheet = CommonFunctions.GetWorksheet(xlWorkbook, "Seller Summary");
                    //Int32 SummaryStartRow = 2;
                    Int32 CurrRow = xlSellerSummaryWorkSheet.UsedRange.Rows.Count + 1;// ListSheetNames.Count + SummaryStartRow;
                    if (IsExistingBill)
                    {
                        for (int i = 2; i <= xlSellerSummaryWorkSheet.UsedRange.Rows.Count; i++)
                        {
                            if (xlSellerSummaryWorkSheet.Cells[i, 3].Value.ToString().Equals(InvoiceNumber.ToString()))
                            {
                                CurrRow = i;
                                break;
                            }
                        }
                    }
                    Int32 CurrCol = 0;
                    CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = CurrRow - 2;// ListSheetNames.Count;
                    CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = ObjCurrentSeller.Line;
                    CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = InvoiceNumber;
                    CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = ObjCurrentSeller.Name;
                    CurrCol++; Excel.Range xlRangeSale = xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    CurrCol++; Excel.Range xlRangeCancel = xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    CurrCol++; Excel.Range xlRangeReturn = xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    CurrCol++; Excel.Range xlRangeDiscount = xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    CurrCol++; Excel.Range xlRangeTotalTax = xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    CurrCol++; Excel.Range xlRangeNetSale = xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    CurrCol++; Excel.Range xlRangeOldBalance = xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    CurrCol++; Excel.Range xlRangeCash = xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol];
                    xlRangeSale.Formula = ObjInvoice.TotalSalesValue;
                    xlRangeDiscount.Formula = ObjInvoice.TotalDiscount;
                    xlRangeTotalTax.Formula = ObjInvoice.TotalTax;
                    xlRangeNetSale.Formula = "=Round(" + xlRangeSale.Address[false, false]
                                                + "-" + xlRangeCancel.Address[false, false]
                                                + "-" + xlRangeReturn.Address[false, false]
                                                + "-" + xlRangeDiscount.Address[false, false]
                                                + "+" + xlRangeTotalTax.Address[false, false] + ", 0)";
                    xlRangeOldBalance.Value = ObjInvoice.ObjSellerDetails.OldBalance;
                    xlRangeSale.NumberFormat = "#,##0.00"; xlRangeCancel.NumberFormat = "#,##0.00";
                    xlRangeReturn.NumberFormat = "#,##0.00"; xlRangeDiscount.NumberFormat = "#,##0.00";
                    xlRangeTotalTax.NumberFormat = "#,##0.00"; xlRangeNetSale.NumberFormat = "#,##0.00";
                    xlRangeOldBalance.NumberFormat = "#,##0.00"; xlRangeCash.NumberFormat = "#,##0.00";
                }
                #endregion

                xlApp.DisplayAlerts = true;
                Excel.Worksheet FirstWorksheet = xlWorkbook.Sheets[1];
                FirstWorksheet.Select();

                #region Print Invoice
                if (PrintBill && CommonFunctions.ObjGeneralSettings.InvoiceQuotPrintCopies > 0)
                {
                    DialogResult result = MessageBox.Show(this, "Would you like to print the " + InvoiceQuotation + "?", "Sales " + InvoiceQuotation, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        xlWorkSheet.PrintOutEx(Type.Missing, Type.Missing, CommonFunctions.ObjGeneralSettings.InvoiceQuotPrintCopies);
                    }
                }
                #endregion

                #region Write InvoiceNumber to Settings File
                if (!IsExistingBill && !IsDummyBill) CurrReportSettings.LastNumber = InvoiceNumber;
                #endregion

                xlWorkbook.Close(SaveChanges: !IsDummyBill);
                CommonFunctions.ReleaseCOMObject(xlWorkbook);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.CreateSalesInvoiceForCurrOrder()", ex);
            }
        }
    }

    class SellerOrderDetails
    {
        public String SellerName;
        public List<Double> ListItemQuantity, ListItemOrigQuantity;
        public Int32 SellerRowIndex = -1, OrderItemCount = 0;
    }
}
