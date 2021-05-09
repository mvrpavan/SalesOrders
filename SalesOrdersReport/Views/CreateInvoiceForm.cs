using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SalesOrdersReport.Models;
using SalesOrdersReport.CommonModules;

namespace SalesOrdersReport.Views
{
    partial class CreateInvoiceForm : Form
    {
        String FormTitle = "";
        List<ProductDetails> ListAllProducts, ListProducts;
        List<String> ListCustomerNames;
        CustomerDetails CurrCustomerDetails;
        DiscountGroupDetails CurrCustomerDiscountGroup;
        CustomerOrderInvoiceDetails CurrInvoiceDetails;
        Int32 CategoryColIndex = 0, ItemColIndex = 1, PriceColIndex = 2, QtyColIndex = 3, SelectColIndex = 4, OrdQtyColIndex = 3, SaleQtyColIndex = 4, ItemSelectionSelectColIndex = 5;
        Int32 PaddingSpace = 6;
        Char PaddingChar = CommonFunctions.PaddingChar, CurrencyChar = CommonFunctions.CurrencyChar;
        Int32 BackgroundTask = -1;
        List<Int32> ListSelectedRowIndexesToAdd = new List<Int32>();
        Dictionary<String, DataGridViewRow> DictItemsSelected = new Dictionary<String, DataGridViewRow>();
        Boolean ValueChanged = false;
        Double DiscountPerc = 0, DiscountValue = 0;
        List<Int32> ListSelectedRowIndexesToRemove = new List<Int32>();
        Int32 cmbBoxCustomerIndex = -1;
        Boolean LoadCompleted = false;
        Int32 cmbBoxInvoiceNumberIndex = -1;
        Double OriginalBalanceAmount = -1;
        Int32 CurrentInvoiceID = -1;

        ProductMasterModel ObjProductMasterModel = CommonFunctions.ObjProductMaster;
        CustomerMasterModel ObjCustomerMasterModel = CommonFunctions.ObjCustomerMasterModel;
        InvoicesModel ObjInvoicesModel;
        UpdateUsingObjectOnCloseDel UpdateObjectOnClose;
        List<InvoiceDetails> ListCustomerInvoices;

        public CreateInvoiceForm(Int32 InvoiceID, UpdateUsingObjectOnCloseDel UpdateObjectOnClose)
        {
            try
            {
                InitializeComponent();
                CommonFunctions.ResetProgressBar();

                ObjInvoicesModel = new InvoicesModel();
                ObjInvoicesModel.Initialize();
                if (InvoiceID < 0)
                {
                    FormTitle = "Create New Invoice";
                    btnCreateInvoice.Text = "Create Invoice";
                    txtBoxInvNumber.Text = this.ObjInvoicesModel.GenerateNewInvoiceNumber();
                }
                else
                {
                    FormTitle = "View/Edit Invoice";
                    btnCreateInvoice.Text = "Update Invoice";
                }

                txtBoxInvNumber.Enabled = true;
                txtBoxInvNumber.ReadOnly = true;
                lblSelectName.Text = "Select Customer";
                lblInvoiceNumber.Text = "Invoice#";
                lblInvoiceDate.Text = "Invoice Date";
                btnDiscount.Enabled = true;
                lblSelectInvoiceNum.Enabled = true;
                lblSelectInvoiceNum.Visible = true;
                cmbBoxInvoiceNumber.Enabled = true;
                cmbBoxInvoiceNumber.Visible = true;
                lblStatus.Text = "Please choose Invoice date";
                dtGridViewInvProdList.Columns[PriceColIndex].ReadOnly = false;
                cmbBxInvoiceDeliveryLine.Items.Add("Select Delivery Line");
                cmbBxInvoiceDeliveryLine.Items.AddRange(CommonFunctions.ObjCustomerMasterModel.GetAllLineNames().ToArray());
                cmbBxInvoiceDeliveryLine.SelectedIndex = 0;

                this.Text = FormTitle;
                picBoxLoading.Visible = false;
                dtGridViewProdListForSelection.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dtGridViewInvProdList.SelectionMode = DataGridViewSelectionMode.CellSelect;
                this.UpdateObjectOnClose = UpdateObjectOnClose;
                CurrentInvoiceID = InvoiceID;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ctor()", ex);
                throw;
            }
        }

        private void CreateInvoiceForm_Load(object sender, EventArgs e)
        {
            try
            {
                //Populate cmbBoxCustomers with Customer Names
                ListCustomerNames = ObjCustomerMasterModel.GetCustomerList();
                cmbBoxCustomers.DataSource = ListCustomerNames;
                cmbBoxCustomers.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbBoxCustomers.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbBoxCustomers.SelectedIndex = -1;

                //Populate cmbBoxProdCat with Item Categories and cmbBoxProduct with Items
                List<String> ListItemCategories = new List<String>();
                ListItemCategories.Add("<ALL>");
                ListItemCategories.AddRange(CommonFunctions.ObjProductMaster.GetProductCategoryList());
                cmbBoxProdCat.DataSource = ListItemCategories;
                cmbBoxProdCat.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbBoxProdCat.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbBoxProdCat.SelectedIndex = -1;

                ListAllProducts = CommonFunctions.ObjProductMaster.GetProductListForCategory("<ALL>");
                ListAllProducts = ListAllProducts.OrderBy(p => p.ProductID).ToList();
                List<String> ListItems = new List<String>();
                ListItems.Add("<ALL>");
                ListItems.AddRange(ListAllProducts.Select(s => s.ItemName));
                cmbBoxProduct.DataSource = ListItems;
                cmbBoxProduct.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbBoxProduct.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbBoxProduct.SelectedIndex = -1;

                CurrInvoiceDetails = null;
                CurrCustomerDetails = null;
                CurrCustomerDiscountGroup = null;
                ResetControls();

                if (CurrentInvoiceID > 0)
                {
                    InvoiceDetails ObjInvoiceDetails = ObjInvoicesModel.GetInvoiceDetailsForInvoiceID(CurrentInvoiceID);

                    //Load Selected Customer Details
                    cmbBoxCustomers.SelectedIndex = cmbBoxCustomers.Items.IndexOf(ObjInvoiceDetails.CustomerName);
                    CurrCustomerDetails = ObjCustomerMasterModel.GetCustomerDetails(ObjInvoiceDetails.CustomerName);
                    UpdateCustomerDetails();

                    ListCustomerInvoices = ObjInvoicesModel.GetInvoiceDetailsForCustomer(dtTmPckrInvDate.Value, CurrCustomerDetails.CustomerID);
                    cmbBoxInvoiceNumber.Items.Clear();
                    cmbBoxInvoiceNumber.Items.Add("<New>");
                    cmbBoxInvoiceNumber.Items.AddRange(ListCustomerInvoices.Select(s => s.InvoiceNumber).ToArray());
                    cmbBoxInvoiceNumber.SelectedIndex = ListCustomerInvoices.FindIndex(s => s.InvoiceID == CurrentInvoiceID) + 1;

                    cmbBoxCustomerIndex = cmbBoxCustomers.SelectedIndex;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreateInvoiceForm_Load()", ex);
            }
        }

        private void EditCustomerInvoice()
        {
            try
            {
                if (CurrCustomerDetails == null)
                {
                    //Load Selected Customer Details
                    CurrCustomerDetails = ObjCustomerMasterModel.GetCustomerDetails(cmbBoxCustomers.SelectedItem.ToString().Trim());
                    UpdateCustomerDetails();
                }
                CurrInvoiceDetails = new CustomerOrderInvoiceDetails();
                CurrInvoiceDetails.CustomerName = CurrCustomerDetails.CustomerName;

                CurrCustomerDiscountGroup = ObjCustomerMasterModel.GetCustomerDiscount(CurrCustomerDetails.CustomerName);
                if (CurrCustomerDiscountGroup.DiscountType == DiscountTypes.PERCENT)
                    DiscountPerc = CurrCustomerDiscountGroup.Discount;
                else if (CurrCustomerDiscountGroup.DiscountType == DiscountTypes.ABSOLUTE)
                    DiscountValue = CurrCustomerDiscountGroup.Discount;

                picBoxLoading.Visible = true;
                lblStatus.Text = "Loading Invoice data. Please wait...";
                BackgroundTask = 2;
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
                CommonFunctions.ShowErrorDialog($"{this}.EditCustomerInvoice()", ex);
            }
        }

        private void CreateOrderForm_Shown(object sender, EventArgs e)
        {
            try
            {
                this.ControlBox = true;
                this.MaximizeBox = true;
                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.CenterScreen;

                if (CurrentInvoiceID < 0)
                {
                    ResetControls();
                    EnableItemsPanel(false);
                }

                LoadCompleted = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreateOrderForm_Shown()", ex);
            }
        }

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
                if (!IsDataValidInGridViewInvProdList())
                {
                    MessageBox.Show(this, "One or multiple errors in selected Item list. Please correct and retry.", "Item list Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Create Invoice sheet for this Customer order
                if (dtGridViewInvProdList.Rows.Count == 0)
                {
                    MessageBox.Show(this, "There are no Items in the Invoice.\nPlease add atleast one Item to the Invoice.", "Create Sales Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }

                Boolean IsValid = false;
                for (int i = 0; i < dtGridViewInvProdList.Rows.Count; i++)
                {
                    if (Double.Parse(dtGridViewInvProdList.Rows[i].Cells[SaleQtyColIndex].Value.ToString()) > 0)
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

                DialogResult diagResult = MessageBox.Show(this, "Confirm to Create Customer Invoice", "Create/Update Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (diagResult == DialogResult.No) return;

                CurrInvoiceDetails.CurrInvoiceDetails.ListInvoiceItems.Clear();
                CurrInvoiceDetails.CurrInvoiceDetails.InvoiceItemCount = 0;

                Boolean InvoiceModified = false;
                foreach (DataGridViewRow item in dtGridViewInvProdList.Rows)
                {
                    String ItemName = item.Cells[ItemColIndex].Value.ToString();
                    ProductDetails tmpProductDetails = ObjProductMasterModel.GetProductDetails(ItemName);
                    CurrInvoiceDetails.CurrInvoiceDetails.InvoiceItemCount += 1;
                    InvoiceItemDetails tmpInvoiceItemDetails = new InvoiceItemDetails()
                    {
                        ProductID = tmpProductDetails.ProductID,
                        ProductName = ItemName,
                        OrderQty = Double.Parse(item.Cells[OrdQtyColIndex].Value.ToString()),
                        SaleQty = Double.Parse(item.Cells[SaleQtyColIndex].Value.ToString()),
                        Price = Double.Parse(item.Cells[PriceColIndex].Value.ToString()),
                        InvoiceItemStatus = INVOICEITEMSTATUS.Invoiced
                    };
                    CurrInvoiceDetails.CurrInvoiceDetails.ListInvoiceItems.Add(tmpInvoiceItemDetails);

                    if (!InvoiceModified && CurrInvoiceDetails.CurrInvoiceDetailsOrig != null)
                    {
                        if (CurrInvoiceDetails.CurrInvoiceDetailsOrig.ListInvoiceItems != null)
                        {
                            List<InvoiceItemDetails> ListInvoiceItems = CurrInvoiceDetails.CurrInvoiceDetailsOrig.ListInvoiceItems;
                            Int32 Index = ListInvoiceItems.FindIndex(s => s.ProductID == tmpProductDetails.ProductID);
                            if (Index >= 0 && Math.Abs(ListInvoiceItems[Index].SaleQty - tmpInvoiceItemDetails.SaleQty) > 0)
                            {
                                InvoiceModified = true;
                            }
                        }
                    }
                }

                if (cmbBxInvoiceDeliveryLine.SelectedIndex > 0)
                {
                    CurrInvoiceDetails.CurrInvoiceDetails.DeliveryLineName = cmbBxInvoiceDeliveryLine.SelectedItem.ToString();
                    CurrInvoiceDetails.CurrInvoiceDetails.DeliveryLineID = CommonFunctions.ObjCustomerMasterModel.GetLineID(CurrInvoiceDetails.CurrInvoiceDetails.DeliveryLineName);
                }
                CurrInvoiceDetails.CurrInvoiceDetails.InvoiceItemCount = CurrInvoiceDetails.CurrInvoiceDetails.ListInvoiceItems.Count;
                CurrInvoiceDetails.OrderItemCount = CurrInvoiceDetails.CurrInvoiceDetails.InvoiceItemCount;

                picBoxLoading.Visible = true;
                BackgroundTask = 3;
                if (InvoiceModified) lblStatus.Text = "Updating Customer Invoice, please wait...";
                else lblStatus.Text = "Creating Customer Invoice, please wait...";
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
                CommonFunctions.ShowErrorDialog($"{this}.btnCreateInvOrd_Click()", ex);
            }
        }

        void UpdateSalesInvoice()
        {
            try
            {
                InvoiceDetails AddUpdatedInvoiceDetails = null;
                if (CurrInvoiceDetails.CurrInvoiceDetails.InvoiceID < 0)
                {
                    AddUpdatedInvoiceDetails = ObjInvoicesModel.CreateNewInvoiceForCustomer(CurrCustomerDetails.CustomerID, CurrInvoiceDetails.CurrInvoiceDetails.OrderID, dtTmPckrInvDate.Value, CurrInvoiceDetails.CurrInvoiceDetails.InvoiceNumber, CurrInvoiceDetails.CurrInvoiceDetails.DeliveryLineName, CurrInvoiceDetails.CurrInvoiceDetails.ListInvoiceItems, Double.Parse(lblDiscount.Text.Replace(CurrencyChar, ' ').Trim()));
                    UpdateObjectOnClose(1, AddUpdatedInvoiceDetails);
                }
                else
                {
                    AddUpdatedInvoiceDetails = ObjInvoicesModel.UpdateInvoiceDetails(CurrInvoiceDetails.CurrInvoiceDetails);
                    UpdateObjectOnClose(2, AddUpdatedInvoiceDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateSalesInvoice()", ex);
            }
        }

        void ResetControls()
        {
            try
            {
                cmbBoxProdCat.SelectedIndex = -1;
                cmbBoxProduct.SelectedIndex = -1;
                dtGridViewInvProdList.Rows.Clear();
                dtGridViewProdListForSelection.Rows.Clear();
                ListSelectedRowIndexesToAdd.Clear();
                ListSelectedRowIndexesToRemove.Clear();
                DictItemsSelected.Clear();
                DiscountPerc = 0; DiscountValue = 0;

                UpdateSummaryDetails();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ResetControls()", ex);
            }
        }

        private void btnCancelChanges_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewInvProdList.Rows.Count == 0 && dtGridViewProdListForSelection.Rows.Count == 0) return;

                BackgroundTask = 4;
                DialogResult diagResult = MessageBox.Show(this, "Are you sure to cancel the Invoice?", "Cancel Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (diagResult == DialogResult.No) return;

                backgroundWorker1_RunWorkerCompleted(null, null);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnCancelChanges_Click()", ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (DictItemsSelected.Count > 0)
                {
                    DialogResult result = MessageBox.Show(this, "All changes made to this Invoice will be lost.\nAre you sure to close the window?", "Close Window", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
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
                CommonFunctions.ShowErrorDialog($"{this}.btnClose_Click()", ex);
            }
        }

        private void LoadInvoiceForCustomer()
        {
            try
            {
                InvoiceDetails CurrentInvoiceDetails = null;
                if (CurrentInvoiceID > 0)
                {
                    //Load Invoice details for selected InvoiceID
                    CurrentInvoiceDetails = ObjInvoicesModel.GetInvoiceDetailsForInvoiceID(CurrentInvoiceID);
                    lblBalanceAmountValue.Text = CurrentInvoiceDetails.BalanceAmount.ToString("F");
                    CurrInvoiceDetails.CurrInvoiceDetails = CurrentInvoiceDetails.Clone();
                    CurrInvoiceDetails.CurrInvoiceDetailsOrig = CurrentInvoiceDetails.Clone();
                    if (CurrentInvoiceDetails.InvoiceStatus == INVOICESTATUS.Cancelled)
                    {
                        MessageBox.Show(this, "You have selected a cancelled Invoice to view/update.", "Load Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                }
                else if (ListCustomerInvoices != null && cmbBoxInvoiceNumberIndex == cmbBoxInvoiceNumber.SelectedIndex)
                {
                    CurrentInvoiceID = -1;
                }
                else
                {
                    //Check for existing Invoice or create new Invoice for selected Customer
                    CustomerDetails ObjCustomerDetails = CommonFunctions.ObjCustomerMasterModel.GetCustomerDetails(cmbBoxCustomers.Items[cmbBoxCustomers.SelectedIndex].ToString());
                    ListCustomerInvoices = ObjInvoicesModel.GetInvoiceDetailsForCustomer(dtTmPckrInvDate.Value, ObjCustomerDetails.CustomerID);
                    if(ListCustomerInvoices != null) ListCustomerInvoices.RemoveAll(e => e.InvoiceStatus != INVOICESTATUS.Created);
                    if (ListCustomerInvoices != null && ListCustomerInvoices.Count >= 0)
                    {
                        cmbBoxInvoiceNumber.Items.Clear();
                        cmbBoxInvoiceNumber.Items.Add("<New>");
                        cmbBoxInvoiceNumber.Items.AddRange(ListCustomerInvoices.Select(se => se.InvoiceNumber).ToArray());
                        Int32 Index = ListCustomerInvoices.FindIndex(e => e.InvoiceStatus == INVOICESTATUS.Created);
                        if (Index < 0)
                        {
                            cmbBoxInvoiceNumberIndex = 0;
                            CurrentInvoiceID = -1;
                        }
                        else
                        {
                            cmbBoxInvoiceNumberIndex = Index + 1;
                            CurrentInvoiceID = ListCustomerInvoices[Index].InvoiceID;
                            CurrInvoiceDetails.CurrInvoiceDetails = ListCustomerInvoices[Index].Clone();
                            CurrInvoiceDetails.CurrInvoiceDetailsOrig = ListCustomerInvoices[Index].Clone();
                            lblBalanceAmountValue.Text = ListCustomerInvoices[Index].BalanceAmount.ToString("F");
                        }
                        cmbBoxInvoiceNumber.SelectedIndex = cmbBoxInvoiceNumberIndex;
                    }
                }

                if (CurrentInvoiceID < 0)
                {
                    txtBoxInvNumber.Text = ObjInvoicesModel.GenerateNewInvoiceNumber();

                    CurrInvoiceDetails.CurrInvoiceDetails = new InvoiceDetails();
                    CurrInvoiceDetails.CurrInvoiceDetails.InvoiceID = -1;
                    CurrInvoiceDetails.CurrInvoiceDetails.CustomerID = CurrCustomerDetails.CustomerID;
                    AccountDetails ObjAccountDetails = CommonFunctions.ObjAccountsMasterModel.GetAccDtlsFromCustID(CurrCustomerDetails.CustomerID);
                    CurrInvoiceDetails.CurrInvoiceDetails.BalanceAmount = ObjAccountDetails.BalanceAmount;
                    lblBalanceAmountValue.Text = ObjAccountDetails.BalanceAmount.ToString("F");
                    CurrInvoiceDetails.CurrInvoiceDetails.InvoiceNumber = txtBoxInvNumber.Text;
                    CurrInvoiceDetails.CurrInvoiceDetails.ListInvoiceItems = new List<InvoiceItemDetails>();
                    if (cmbBxInvoiceDeliveryLine.SelectedIndex > 0) CurrInvoiceDetails.CurrInvoiceDetails.DeliveryLineName = cmbBxInvoiceDeliveryLine.SelectedItem.ToString();
                     FormTitle = "Create New Invoice";
                    btnCreateInvoice.Text = "Create Invoice";
                }
                else
                {
                    txtBoxInvNumber.Text = CurrInvoiceDetails.CurrInvoiceDetails.InvoiceNumber;
                    lblBalanceAmountValue.Text = CurrInvoiceDetails.CurrInvoiceDetails.BalanceAmount.ToString("F");
                    cmbBxInvoiceDeliveryLine.SelectedItem = CurrInvoiceDetails.CurrInvoiceDetails.DeliveryLineName;
                    if (CurrInvoiceDetails.CurrInvoiceDetails.ListInvoiceItems == null || CurrInvoiceDetails.CurrInvoiceDetails.ListInvoiceItems.Count == 0)
                        ObjInvoicesModel.FillInvoiceItemDetails(CurrInvoiceDetails.CurrInvoiceDetails);
                    foreach (var item in CurrInvoiceDetails.CurrInvoiceDetails.ListInvoiceItems)
                    {
                        if (item.OrderQty <= 0) continue;
                        if (item.InvoiceItemStatus != INVOICEITEMSTATUS.Invoiced) continue;

                        Object[] row = new Object[6];
                        row[CategoryColIndex] = ObjProductMasterModel.GetProductDetails(item.ProductID).CategoryName;
                        row[ItemColIndex] = item.ProductName;
                        row[PriceColIndex] = item.Price.ToString("F");
                        row[OrdQtyColIndex] = item.OrderQty;
                        row[SaleQtyColIndex] = item.OrderQty;
                        row[ItemSelectionSelectColIndex] = false;

                        Int32 Index = dtGridViewInvProdList.Rows.Add(row);
                        DictItemsSelected.Add(item.ProductName, dtGridViewInvProdList.Rows[Index]);
                    }
                    FormTitle = "Update Invoice";
                    btnCreateInvoice.Text = "Update Invoice";
                }
                lblStatus.Text = "Add/Delete/Modify Items to Invoice";

                UpdateSummaryDetails();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadInvoiceForCustomer()", ex);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                switch (BackgroundTask)
                {
                    case 2:     //Load Customer Invoice details
                        LoadInvoiceForCustomer();
                        break;
                    case 3:     //Update Customer Invoice details
                        UpdateSalesInvoice();
                        break;
                    default:
                        break;
                }
                ReportProgressFunc(100);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.backgroundWorker1_DoWork()", ex);
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
                    case 2:     //Load Order/Invoice for Current Customer
                        EnableItemsPanel(true);
                        picBoxLoading.Visible = false;
                        cmbBoxCustomers.Enabled = true;
                        cmbBoxCustomers.Focus();
                        break;
                    case 3:     //Update Seller Order for Current Seller or Create Sales Invoice
                        cmbBoxCustomers.SelectedIndex = -1;
                        ResetControls();
                        picBoxLoading.Visible = false;
                        EnableItemsPanel(false);
                        cmbBoxCustomers.Enabled = true;
                        DialogResult dialogResult =  MessageBox.Show(this, "Created Customer Invoice successfully.Do you want to Print Invoice?", "Sales Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                        if (dialogResult == DialogResult.Yes)
                        {
                            Int32 InvoiceID = ObjInvoicesModel.CurrInvoiceID;

                            InvoiceDetails ObjInvoiceDetails = ObjInvoicesModel.GetInvoiceDetailsForInvoiceID(InvoiceID);
                            ObjInvoiceDetails.BalanceAmount = Double.Parse(lblBalanceAmountValue.Text);
                            CommonFunctions.PrintOrderInvoiceQuotation(ReportType.INVOICE, false, ObjInvoicesModel, new List<Object>() { ObjInvoiceDetails }, ObjInvoiceDetails.InvoiceDate, 1, false, false, ReportProgressFunc);
                        }
                        lblStatus.Text = "Choose a Customer to create Sales Invoice";
                        cmbBoxInvoiceNumber.Items.Clear();
                        OriginalBalanceAmount = -1;
                        lblBalanceAmountValue.Text = "0.00";
                        txtBoxInvNumber.Text = "";

                        CurrInvoiceDetails = null;
                        CurrCustomerDetails = null;
                        CurrCustomerDiscountGroup = null;
                        break;
                    default:
                        break;
                }
                CommonFunctions.ResetProgressBar();
                BackgroundTask = -1;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.backgroundWorker1_RunWorkerCompleted()", ex);
            }
        }

        private void cmbBoxProdCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxProdCat.SelectedIndex < 0 || !LoadCompleted) return;

                ListProducts = CommonFunctions.ObjProductMaster.GetProductListForCategory(cmbBoxProdCat.SelectedItem.ToString());
                ListProducts = ListProducts.OrderBy(p => p.ProductID).ToList();
                List<String> ListItems = new List<String>();
                ListItems.Add("<ALL>");
                ListItems.AddRange(ListProducts.Select(s => s.ItemName));
                cmbBoxProduct.DataSource = ListItems;
                cmbBoxProduct.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxProdCat_SelectedIndexChanged()", ex);
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
                    Double Price = CommonFunctions.ObjProductMaster.GetPriceForProduct(tmpListProducts[i].ItemName, CurrCustomerDetails.PriceGroupIndex);
                   // Price = Price * ((CommonFunctions.ObjProductMaster.GetTaxRatesForProduct(tmpListProducts[i].ItemName).Sum() + 100) / 100);

                    Object[] row = { tmpListProducts[i].CategoryName, tmpListProducts[i].ItemName,
                                     Price.ToString("F"), 0, false};

                    dtGridViewProdListForSelection.Rows.Add(row);
                }
                ListSelectedRowIndexesToAdd.Clear();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxProduct_SelectedIndexChanged()", ex);
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
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewProdListForSelection_CellContentClick()", ex);
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
                    if (dtGridViewProdListForSelection.SelectedRows.Count > 0)
                        ListSelectedRows.Add(dtGridViewProdListForSelection.SelectedRows[0]);
                    else return;
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
                    Object[] row = new Object[dtGridViewInvProdList.Columns.Count];
                    //for (int i = 0; i < row.Length; i++)
                    //{
                    //    row[i] = ListSelectedRows[j].Cells[i].Value;
                    //}
                    row[CategoryColIndex] = ListSelectedRows[j].Cells[CategoryColIndex].Value;
                    row[ItemColIndex] = ListSelectedRows[j].Cells[ItemColIndex].Value;
                    row[PriceColIndex] = ListSelectedRows[j].Cells[PriceColIndex].Value;
                    row[ItemSelectionSelectColIndex] = false;
                    row[OrdQtyColIndex] = ListSelectedRows[j].Cells[QtyColIndex].Value;
                    row[SaleQtyColIndex] = ListSelectedRows[j].Cells[QtyColIndex].Value;
                    ListSelectedRows[j].Cells[SelectColIndex].Value = false;
                    ListSelectedRows[j].Cells[QtyColIndex].Value = 0;

                    if (!DictItemsSelected.ContainsKey(row[ItemColIndex].ToString()))
                    {
                        Int32 RowIndex = dtGridViewInvProdList.Rows.Add(row);
                        DictItemsSelected.Add(row[ItemColIndex].ToString(), dtGridViewInvProdList.Rows[RowIndex]);
                    }
                    else
                    {
                        DictItemsSelected[row[ItemColIndex].ToString()].Cells[SaleQtyColIndex].Value = Double.Parse(DictItemsSelected[row[ItemColIndex].ToString()].Cells[SaleQtyColIndex].Value.ToString()) + Double.Parse(row[SaleQtyColIndex].ToString());
                    }
                }

                ListSelectedRowIndexesToAdd.Clear();
                dtGridViewProdListForSelection.CommitEdit(DataGridViewDataErrorContexts.Commit);

                UpdateSummaryDetails();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnAddItem_Click()", ex);
            }
        }

        private void btnRemItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewInvProdList.Rows.Count == 0) return;

                List<DataGridViewRow> ListSelectedRows = new List<DataGridViewRow>();
                if (ListSelectedRowIndexesToRemove.Count == 0)
                {
                    if (dtGridViewProdListForSelection.SelectedRows.Count > 0)
                        ListSelectedRows.Add(dtGridViewInvProdList.SelectedRows[0]);
                    else return;
                }
                else
                {
                    foreach (Int32 index in ListSelectedRowIndexesToRemove)
                    {
                        ListSelectedRows.Add(dtGridViewInvProdList.Rows[index]);
                    }
                }

                for (int j = 0; j < ListSelectedRows.Count; j++)
                {
                    dtGridViewInvProdList.Rows.Remove(ListSelectedRows[j]);
                    DictItemsSelected.Remove(ListSelectedRows[j].Cells[ItemColIndex].Value.ToString());
                }
                ListSelectedRowIndexesToRemove.Clear();

                UpdateSummaryDetails();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnRemItem_Click()", ex);
            }
        }

        private void dtGridViewInvProdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != ItemSelectionSelectColIndex || e.RowIndex < 0) return;

                dtGridViewInvProdList.CommitEdit(DataGridViewDataErrorContexts.Commit);

                if (Boolean.Parse(dtGridViewInvProdList.Rows[e.RowIndex].Cells[ItemSelectionSelectColIndex].Value.ToString()))
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
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewInvProdList_CellContentClick()", ex);
            }
        }

        private void UpdateCustomerDetails()
        {
            try
            {
                String CustomerDetails = CurrCustomerDetails.CustomerName + "\n" + CurrCustomerDetails.Address + "\n" + CurrCustomerDetails.PhoneNo;
                //CustomerDetails += "\nBalance: " + sellerDetails.OldBalance.ToString("F");
                lblCustomerDetails.Text = CustomerDetails;
                if (OriginalBalanceAmount < 0)
                    lblBalanceAmountValue.Text = 0.ToString("F"); //sellerDetails.OldBalance.ToString("F");
                    //TODO: Get Old balance for the customer
                else
                    lblBalanceAmountValue.Text = OriginalBalanceAmount.ToString("F");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateCustomerDetails()", ex);
            }
        }

        private void UpdateSummaryDetails()
        {
            try
            {
                Int32 NumItems;
                Double SubTotal = 0, Quantity = 0, Discount = 0, TaxAmount = 0, GrandTotal = 0;
                NumItems = dtGridViewInvProdList.Rows.Count; Quantity = 0; SubTotal = 0; Discount = 0; TaxAmount = 0; GrandTotal = 0;

                foreach (DataGridViewRow item in dtGridViewInvProdList.Rows)
                {
                    Double Price = Double.Parse(item.Cells[PriceColIndex].Value.ToString());
                    Double Qty = Double.Parse(item.Cells[SaleQtyColIndex].Value.ToString());
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
                CommonFunctions.ShowErrorDialog($"{this}.UpdateSummaryDetails()", ex);
            }
        }

        private void cmbBoxCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxCustomers.SelectedIndex < 0 || !LoadCompleted)
                {
                    cmbBoxCustomerIndex = -1;
                    cmbBoxInvoiceNumberIndex = -1;
                    CurrInvoiceDetails = null;
                    CurrCustomerDetails = null;
                    CurrCustomerDiscountGroup = null;
                    lblCustomerDetails.Text = "";
                    return;
                }

                //Check if the Invoice is changed before changing Customer
                Boolean WarnUser = false;
                if (CurrInvoiceDetails != null)
                {
                    if (cmbBoxCustomers.SelectedIndex == cmbBoxCustomerIndex) return;

                    if (dtGridViewInvProdList.Rows.Count != CurrInvoiceDetails.OrderItemCount)
                    {
                        WarnUser = true;
                    }
                    else
                    {
                        foreach (DataGridViewRow item in dtGridViewInvProdList.Rows)
                        {
                            String ItemName = item.Cells[ItemColIndex].Value.ToString();
                            Double Qty = CurrInvoiceDetails.CurrOrderDetails.ListOrderItems.Find(s => s.ProductName.Equals(ItemName.ToUpper(), StringComparison.InvariantCulture)).OrderQty;

                            if (Qty != Double.Parse(item.Cells[SaleQtyColIndex].Value.ToString()))
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
                            cmbBoxCustomers.SelectedIndex = cmbBoxCustomerIndex;
                            return;
                        }
                    }
                }
                cmbBoxCustomerIndex = cmbBoxCustomers.SelectedIndex;
                CurrInvoiceDetails = null;
                CurrCustomerDetails = null;
                CurrCustomerDiscountGroup = null;
                CurrentInvoiceID = -1;
                cmbBoxInvoiceNumber.Items.Clear();
                cmbBoxInvoiceNumberIndex = -1;
                ListCustomerInvoices = null;
                ResetControls();
                ObjInvoicesModel.Initialize();

                EditCustomerInvoice();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxCustomers_SelectedIndexChanged()", ex);
            }
        }

        private void btnItemDiscount_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnItemDiscount_Click()", ex);
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
                CommonFunctions.ShowErrorDialog($"{this}.btnDiscount_Click()", ex);
            }
        }

        private void dtGridViewInvProdList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.ColumnIndex != OrdQtyColIndex && e.ColumnIndex != SaleQtyColIndex && e.ColumnIndex != PriceColIndex) || e.RowIndex < 0) return;

                DataGridViewCell cell = dtGridViewInvProdList.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ErrorText = null;
                if (e.ColumnIndex == PriceColIndex)
                {
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

                if (e.ColumnIndex == SaleQtyColIndex)
                {
                    Double result;
                    if (!Double.TryParse(cell.Value.ToString(), out result))
                    {
                        cell.ErrorText = "Must be a valid Sale Quantity";
                        return;
                    }

                    if (result < 0)
                    {
                        cell.ErrorText = "Must be a valid Sale Quantity";
                        return;
                    }
                }

                if (e.ColumnIndex == OrdQtyColIndex)
                {
                    String CellText = cell.Value.ToString();
                    Double result;
                    Boolean IsValid = true;
                    String[] Tokens = null;
                    if (CellText.Contains("+") || CellText.Contains("-"))
                        Tokens = CellText.Split(new Char[] { '+', '-' });
                    else
                        Tokens = new String[] { CellText };

                    for (int i = 0; i < Tokens.Length; i++)
                    {
                        if (String.IsNullOrEmpty(Tokens[i].Trim()) || !Double.TryParse(Tokens[i], out result))
                        {
                            IsValid = false;
                            break;
                        }
                    }

                    if (!IsValid)
                    {
                        cell.ErrorText = "Must be a valid Order Quantity";
                        return;
                    }
                }

                dtGridViewInvProdList.CommitEdit(DataGridViewDataErrorContexts.Commit);
                UpdateSummaryDetails();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewInvProdList_CellEndEdit()", ex);
            }
        }

        private Boolean IsDataValidInGridViewInvProdList()
        {
            try
            {
                for (int i = 0; i < dtGridViewInvProdList.Rows.Count; i++)
                {
                    if (!String.IsNullOrEmpty(dtGridViewInvProdList.Rows[i].Cells[OrdQtyColIndex].ErrorText)
                        || !String.IsNullOrEmpty(dtGridViewInvProdList.Rows[i].Cells[SaleQtyColIndex].ErrorText)) return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.IsDataValidInGridViewInvProdList()", ex);
                return false;
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
                CommonFunctions.ShowErrorDialog($"{this}.btnSelectAll_Click()", ex);
            }
        }

        private void btnSelectAllToRemove_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean Checked = false;
                if (ListSelectedRowIndexesToRemove.Count != dtGridViewInvProdList.Rows.Count) Checked = true;
                ListSelectedRowIndexesToRemove.Clear();
                Int32 Index = 0;
                foreach (DataGridViewRow item in dtGridViewInvProdList.Rows)
                {
                    item.Cells[ItemSelectionSelectColIndex].Value = Checked;
                    if (Checked) ListSelectedRowIndexesToRemove.Add(Index);
                    Index++;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnSelectAll_Click()", ex);
            }
        }

        void EnableItemsPanel(Boolean enable)
        {
            try
            {
                panelOrderControls.Enabled = enable;
                cmbBoxCustomers.Enabled = enable;
                cmbBoxProdCat.Enabled = enable;
                cmbBoxProduct.Enabled = enable;
                if (enable)
                {
                    cmbBoxProdCat.SelectedIndex = 0;
                    cmbBoxProduct.SelectedIndex = 0;
                }
                cmbBoxInvoiceNumber.Enabled = enable;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.EnableItemsPanel()", ex);
            }
        }

        private void cmbBoxInvoiceNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxInvoiceNumber.SelectedIndex < 0 || cmbBoxInvoiceNumber.SelectedIndex == cmbBoxInvoiceNumberIndex)
                    return;

                EnableItemsPanel(false);
                if (dtGridViewInvProdList.Rows.Count > 0)
                {
                    DialogResult diagResult = MessageBox.Show(this, "All changes made to this Invoice will be lost.\nAre you sure to change the Invoice?", "Change Customer Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (diagResult == DialogResult.No)
                    {
                        cmbBoxInvoiceNumber.SelectedIndex = cmbBoxInvoiceNumberIndex;
                        EnableItemsPanel(true);
                        return;
                    }
                }
                ResetControls();
                cmbBoxInvoiceNumberIndex = cmbBoxInvoiceNumber.SelectedIndex;

                if (CurrCustomerDetails == null)
                {
                    //Load Selected Customer Details
                    CurrCustomerDetails = ObjCustomerMasterModel.GetCustomerDetails(cmbBoxCustomers.SelectedItem.ToString().Trim());
                    UpdateCustomerDetails();
                }

                if (cmbBoxInvoiceNumberIndex == 0)
                {
                    //Create New Invoice
                    txtBoxInvNumber.Text = ObjInvoicesModel.GenerateNewInvoiceNumber();
                    CurrentInvoiceID = -1;

                    OriginalBalanceAmount = -1;
                    //UpdateCustomerDetails();
                    //UpdateSummaryDetails();
                    //EnableItemsPanel(true);
                    EditCustomerInvoice();
                }
                else
                {
                    //Load item details from Invoice
                    txtBoxInvNumber.Text = cmbBoxInvoiceNumber.SelectedItem.ToString();
                    CurrentInvoiceID = ListCustomerInvoices.Find(s => s.InvoiceNumber.Equals(txtBoxInvNumber.Text)).InvoiceID;
                    EditCustomerInvoice();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxInvoiceNumber_SelectedIndexChanged()", ex);
            }
        }

        private void btnEditBalanceAmount_Click(object sender, EventArgs e)
        {
            try
            {
                EditOldBalanceForm editOldBalanceForm = new EditOldBalanceForm(UpdateBalanceAmount, Double.Parse(lblBalanceAmountValue.Text));
                editOldBalanceForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnEditBalanceAmount_Click()", ex);
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
                CommonFunctions.ShowErrorDialog($"{this}.UpdateBalanceAmount()", ex);
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
                CommonFunctions.ShowErrorDialog($"{this}.btnResetBalanceAmountValue_Click()", ex);
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
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewProdListForSelection_CellEndEdit()", ex);
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
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewProdListForSelection_CellValueChanged()", ex);
            }
        }

        DateTime dtTmPckrInvDateValue;
        private void dtTmPckrInvDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtTmPckrInvDateValue == dtTmPckrInvDate.Value) return;

                EnableItemsPanel(false);
                if (dtGridViewInvProdList.Rows.Count > 0)
                {
                    DialogResult diagResult = MessageBox.Show(this, $"All changes made to this Invoice will be lost.\nAre you sure to change the Date?", $"Change Invoice Date", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (diagResult == DialogResult.No)
                    {
                        dtTmPckrInvDate.Value = dtTmPckrInvDateValue;
                        EnableItemsPanel(true);
                        return;
                    }
                }
                ResetControls();
                dtTmPckrInvDateValue = dtTmPckrInvDate.Value;

                lblStatus.Text = "Choose a Customer to create/update Invoice";
                //dtTmPckrInvOrdDate.Enabled = false;
                cmbBoxCustomers.Enabled = true;
                cmbBoxCustomers.Focus();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dtTmPckrInvDate_ValueChanged()", ex);
            }
        }
    }
}
