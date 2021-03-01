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
    partial class CreatePOSBillForm : Form
    {
        String FormTitle = "", OrderInvoice = "";
        List<ProductDetails> ListAllProducts, ListProducts;
        List<String> ListCustomerNames;
        //List<Int64> ListCustomerPhoneNumbers;
        List<string> ListCustomerPhoneNumbers;
        CustomerDetails CurrCustomerDetails;
        DiscountGroupDetails CurrCustomerDiscountGroup;
        CustomerOrderInvoiceDetails CurrOrderInvoiceDetails;
        Int32 CategoryColIndex = 0, ItemColIndex = 1, PriceColIndex = 2, QtyColIndex = 3, SelectColIndex = 4, SaleQtyColIndex = 3, ItemSelectionSelectColIndex = 4;
        Int32 PaddingSpace = 6;
        Char PaddingChar = CommonFunctions.PaddingChar, CurrencyChar = CommonFunctions.CurrencyChar;
        Int32 BackgroundTask = -1;
        List<Int32> ListSelectedRowIndexesToAdd = new List<Int32>();
        Dictionary<String, DataGridViewRow> DictItemsSelected = new Dictionary<String, DataGridViewRow>();
        Boolean ValueChanged = false;
        Double DiscountPerc = 0, DiscountValue = 0;
        List<Int32> ListSelectedRowIndexesToRemove = new List<Int32>();
        Int32 cmbBoxCustomerIndex = -1, cmbBoxPhoneNumberIndex = -1;
        Boolean LoadCompleted = false;
        Int32 cmbBoxInvoiceNumberIndex = -1;
        Int32 CurrentInvoiceID = -1;

        ProductMasterModel ObjProductMasterModel = CommonFunctions.ObjProductMaster;
        CustomerMasterModel ObjCustomerMasterModel = CommonFunctions.ObjCustomerMasterModel;
        InvoicesModel ObjInvoicesModel;
        UpdateUsingObjectOnCloseDel UpdateObjectOnClose;
        List<InvoiceDetails> ListCustomerInvoices;
        //Dictionary<Int64, CustomerDetails> DictPhoneNumberCustomerDetails = new Dictionary<Int64, CustomerDetails>();
        Dictionary<string, CustomerDetails> DictPhoneNumberCustomerDetails = new Dictionary<string, CustomerDetails>();
        PaymentsModel ObjPaymentsModel = new PaymentsModel();

        public CreatePOSBillForm(Int32 InvoiceID, UpdateUsingObjectOnCloseDel UpdateObjectOnClose)
        {
            try
            {
                InitializeComponent();
                CommonFunctions.ResetProgressBar();

                ObjInvoicesModel = new InvoicesModel();
                ObjInvoicesModel.Initialize();
                CurrentInvoiceID = InvoiceID;
                ObjPaymentsModel.LoadPaymentModes();

                if (InvoiceID < 0)
                {
                    FormTitle = "Create New Bill";
                    btnCreateBill.Text = "Create Bill";
                    txtBoxInvOrdNumber.Text = this.ObjInvoicesModel.GenerateNewBillNumber();
                }
                else
                {
                    FormTitle = "View/Edit Bill";
                    btnCreateBill.Text = "Update Bill";
                }

                OrderInvoice = "Bill";
                txtBoxInvOrdNumber.Enabled = true;
                txtBoxInvOrdNumber.ReadOnly = true;
                btnDiscount.Enabled = true;
                cmbBoxBillNumber.Enabled = true;
                cmbBoxBillNumber.Visible = true;
                dtGridViewInvOrdProdList.Columns[PriceColIndex].ReadOnly = false;

                this.Text = FormTitle;
                picBoxLoading.Visible = false;
                dtGridViewProdListForSelection.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dtGridViewInvOrdProdList.SelectionMode = DataGridViewSelectionMode.CellSelect;
                this.UpdateObjectOnClose = UpdateObjectOnClose;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ctor()", ex);
                throw;
            }
        }

        private void CreatePOSBillForm_Load(object sender, EventArgs e)
        {
            try
            {
                //Populate cmbBoxCustomers with Customer Names
                ListCustomerNames = ObjCustomerMasterModel.GetCustomerList();
                cmbBoxCustomers.DataSource = ListCustomerNames;
                cmbBoxCustomers.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbBoxCustomers.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbBoxCustomers.SelectedIndex = -1;

                //Populate cmbBoxPhoneNumbers with Phone Numbers
                List<CustomerDetails> ListCustomerDetails = ObjCustomerMasterModel.GetListCustomerCache();
                for (int i = 0; i < ListCustomerDetails.Count; i++)
                {
                    CustomerDetails tmpCustomerDetails = ListCustomerDetails[i].Clone();
                    if (tmpCustomerDetails.PhoneNo.Trim() == string.Empty) continue;
                    if (!DictPhoneNumberCustomerDetails.ContainsKey(tmpCustomerDetails.PhoneNo))
                    {
                        DictPhoneNumberCustomerDetails.Add(tmpCustomerDetails.PhoneNo, tmpCustomerDetails);
                    }
                }
                ListCustomerPhoneNumbers = DictPhoneNumberCustomerDetails.Keys.ToList();
                cmbBoxPhoneNumbers.DataSource = ListCustomerPhoneNumbers;
                cmbBoxPhoneNumbers.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbBoxPhoneNumbers.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbBoxPhoneNumbers.SelectedIndex = -1;

                cmbBoxCustomers.SelectedIndexChanged += cmbBoxCustomers_SelectedIndexChanged;
                cmbBoxPhoneNumbers.SelectedIndexChanged += cmbBoxPhoneNumbers_SelectedIndexChanged;

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

                CurrOrderInvoiceDetails = null;
                CurrCustomerDetails = null;
                CurrCustomerDiscountGroup = null;
                ResetControls();

                if (CurrentInvoiceID > 0)
                {
                    InvoiceDetails ObjInvoiceDetails = ObjInvoicesModel.GetInvoiceDetailsForInvoiceID(CurrentInvoiceID);

                    //Load Selected Customer Details
                    cmbBoxCustomers.SelectedIndex = cmbBoxCustomers.Items.IndexOf(ObjInvoiceDetails.CustomerName);
                    cmbBoxCustomerIndex = cmbBoxCustomers.SelectedIndex;
                    CurrCustomerDetails = ObjCustomerMasterModel.GetCustomerDetails(ObjInvoiceDetails.CustomerName);
                    UpdateCustomerDetails();

                    ListCustomerInvoices = ObjInvoicesModel.GetInvoiceDetailsForCustomer(dtTmPckrInvOrdDate.Value, CurrCustomerDetails.CustomerID);
                    cmbBoxBillNumber.Items.Clear();
                    cmbBoxBillNumber.Items.Add("<New>");
                    cmbBoxBillNumber.Items.AddRange(ListCustomerInvoices.Select(s => s.InvoiceNumber).ToArray());
                    cmbBoxBillNumber.SelectedIndex = ListCustomerInvoices.FindIndex(s => s.InvoiceID == CurrentInvoiceID) + 1;

                    if (ObjInvoiceDetails.InvoiceStatus == INVOICESTATUS.Delivered || ObjInvoiceDetails.InvoiceStatus == INVOICESTATUS.Paid)
                    {
                        EnableItemsPanel(false);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreatePOSBillForm_Load()", ex);
            }
        }

        private void CreatePOSBillForm_Shown(object sender, EventArgs e)
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

                cmbBoxCustomers.Focus();
                LoadCompleted = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreatePOSBillForm_Shown()", ex);
            }
        }

        ReportProgressDel ReportProgress = null;
        private void ReportProgressFunc(Int32 ProgressState)
        {
            if (ReportProgress == null) return;
            ReportProgress(ProgressState);
        }

        Boolean IsSaveBillClicked = false;
        private void btnCreateBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsDataValidInGridViewInvOrdProdList())
                {
                    MessageBox.Show(this, "One or multiple errors in selected Item list. Please correct and retry.", "Item list Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //Create Invoice sheet for this Customer order
                if (dtGridViewInvOrdProdList.Rows.Count == 0)
                {
                    MessageBox.Show(this, "There are no Items in the Bill.\nPlease add atleast one Item to the Bill.", "Create Bill", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }

                Boolean IsValid = false;
                for (int i = 0; i < dtGridViewInvOrdProdList.Rows.Count; i++)
                {
                    if (Double.Parse(dtGridViewInvOrdProdList.Rows[i].Cells[SaleQtyColIndex].Value.ToString()) > 0)
                    {
                        IsValid = true;
                        break;
                    }
                }

                if (!IsValid)
                {
                    MessageBox.Show(this, "There are no Items with Quantity more than 0.\nPlease add atleast one Item with valid Quantity.", "Create Bill", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }

                DialogResult diagResult = MessageBox.Show(this, "Confirm to Create Customer Bill", "Create Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (diagResult == DialogResult.No) return;

                CurrOrderInvoiceDetails.CurrInvoiceDetails.ListInvoiceItems.Clear();
                CurrOrderInvoiceDetails.CurrInvoiceDetails.InvoiceItemCount = 0;

                Boolean InvoiceModified = false;
                foreach (DataGridViewRow item in dtGridViewInvOrdProdList.Rows)
                {
                    String ItemName = item.Cells[ItemColIndex].Value.ToString();
                    ProductDetails tmpProductDetails = ObjProductMasterModel.GetProductDetails(ItemName);
                    CurrOrderInvoiceDetails.CurrInvoiceDetails.InvoiceItemCount += 1;
                    InvoiceItemDetails tmpInvoiceItemDetails = new InvoiceItemDetails()
                    {
                        ProductID = tmpProductDetails.ProductID,
                        ProductName = ItemName,
                        OrderQty = Double.Parse(item.Cells[SaleQtyColIndex].Value.ToString()),
                        SaleQty = Double.Parse(item.Cells[SaleQtyColIndex].Value.ToString()),
                        Price = Double.Parse(item.Cells[PriceColIndex].Value.ToString()),
                        InvoiceItemStatus = INVOICEITEMSTATUS.Invoiced
                    };
                    CurrOrderInvoiceDetails.CurrInvoiceDetails.ListInvoiceItems.Add(tmpInvoiceItemDetails);

                    if (!InvoiceModified && CurrOrderInvoiceDetails.CurrInvoiceDetailsOrig != null)
                    {
                        if (CurrOrderInvoiceDetails.CurrInvoiceDetailsOrig.ListInvoiceItems != null)
                        {
                            List<InvoiceItemDetails> ListInvoiceItems = CurrOrderInvoiceDetails.CurrInvoiceDetailsOrig.ListInvoiceItems;
                            Int32 Index = ListInvoiceItems.FindIndex(s => s.ProductID == tmpProductDetails.ProductID);
                            if (Index >= 0 && Math.Abs(ListInvoiceItems[Index].SaleQty - tmpInvoiceItemDetails.SaleQty) > 0)
                            {
                                InvoiceModified = true;
                            }
                        }
                    }
                }
                CurrOrderInvoiceDetails.CurrInvoiceDetails.InvoiceItemCount = CurrOrderInvoiceDetails.CurrInvoiceDetails.ListInvoiceItems.Count;
                CurrOrderInvoiceDetails.OrderItemCount = CurrOrderInvoiceDetails.CurrInvoiceDetails.InvoiceItemCount;

                picBoxLoading.Visible = true;
                BackgroundTask = 3;
                if (IsSaveBillClicked) BackgroundTask = 4;
                if (InvoiceModified) lblStatus.Text = "Updating Customer Bill, please wait...";
                else lblStatus.Text = "Creating Customer Bill, please wait...";
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
                CommonFunctions.ShowErrorDialog($"{this}.btnCreateBill_Click()", ex);
            }
        }
        
        InvoiceDetails AddUpdatedInvoiceDetails = null;
        void UpdateSalesInvoice()
        {
            try
            {
                if (CurrOrderInvoiceDetails.CurrInvoiceDetails.InvoiceID < 0)
                {
                    AddUpdatedInvoiceDetails = ObjInvoicesModel.CreateNewInvoiceForCustomer(CurrCustomerDetails.CustomerID, 
                                            CurrOrderInvoiceDetails.CurrInvoiceDetails.OrderID, DateTime.Now, 
                                            CurrOrderInvoiceDetails.CurrInvoiceDetails.InvoiceNumber, 
                                            CurrOrderInvoiceDetails.CurrInvoiceDetails.ListInvoiceItems, Double.Parse(lblDiscount.Text.Replace(CurrencyChar, ' ').Trim()));
                    UpdateObjectOnClose(1, AddUpdatedInvoiceDetails);
                }
                else
                {
                    AddUpdatedInvoiceDetails = ObjInvoicesModel.UpdateInvoiceDetails(CurrOrderInvoiceDetails.CurrInvoiceDetails);
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
                CommonFunctions.ShowErrorDialog($"{this}.ResetControls()", ex);
            }
        }

        private void btnCancelChanges_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewInvOrdProdList.Rows.Count == 0 && dtGridViewProdListForSelection.Rows.Count == 0) return;

                BackgroundTask = 4;
                DialogResult diagResult = MessageBox.Show(this, "Are you sure to cancel the Bill?", "Cancel Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
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
                CommonFunctions.ShowErrorDialog($"{this}.btnClose_Click()", ex);
            }
        }

        private void LoadBillForCustomer()
        {
            try
            {
                InvoiceDetails CurrentInvoiceDetails = null;
                if (CurrentInvoiceID > 0)
                {
                    //Load Invoice details for selected InvoiceID
                    CurrentInvoiceDetails = ObjInvoicesModel.GetInvoiceDetailsForInvoiceID(CurrentInvoiceID);
                    CurrOrderInvoiceDetails.CurrInvoiceDetails = CurrentInvoiceDetails.Clone();
                    CurrOrderInvoiceDetails.CurrInvoiceDetailsOrig = CurrentInvoiceDetails.Clone();
                    if (CurrentInvoiceDetails.InvoiceStatus == INVOICESTATUS.Cancelled)
                    {
                        MessageBox.Show(this, "You have selected a cancelled Bill to view/update.", "Load Bill", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                }
                else if (ListCustomerInvoices != null && cmbBoxInvoiceNumberIndex == cmbBoxBillNumber.SelectedIndex)
                {
                    CurrentInvoiceID = -1;
                }
                else
                {
                    //Check for existing Invoice or create new Invoice for selected Customer
                    CustomerDetails ObjCustomerDetails = CommonFunctions.ObjCustomerMasterModel.GetCustomerDetails(cmbBoxCustomers.Items[cmbBoxCustomers.SelectedIndex].ToString());
                    ListCustomerInvoices = ObjInvoicesModel.GetInvoiceDetailsForCustomer(dtTmPckrInvOrdDate.Value, ObjCustomerDetails.CustomerID);
                    if (ListCustomerInvoices != null && ListCustomerInvoices.Count >= 0)
                    {
                        ListCustomerInvoices.RemoveAll(e => e.InvoiceStatus != INVOICESTATUS.Created);
                        cmbBoxBillNumber.Items.Clear();
                        cmbBoxBillNumber.Items.Add("<New>");
                        cmbBoxBillNumber.Items.AddRange(ListCustomerInvoices.Select(se => se.InvoiceNumber).ToArray());
                        Int32 Index = ListCustomerInvoices.FindIndex(e => e.InvoiceStatus == INVOICESTATUS.Created);
                        cmbBoxInvoiceNumberIndex = 0;
                        CurrentInvoiceID = -1;
                        cmbBoxBillNumber.SelectedIndex = cmbBoxInvoiceNumberIndex;
                    }
                }

                if (CurrentInvoiceID < 0)
                {
                    txtBoxInvOrdNumber.Text = ObjInvoicesModel.GenerateNewBillNumber();

                    CurrOrderInvoiceDetails.CurrInvoiceDetails = new InvoiceDetails();
                    CurrOrderInvoiceDetails.CurrInvoiceDetails.InvoiceID = -1;
                    CurrOrderInvoiceDetails.CurrInvoiceDetails.CustomerID = CurrCustomerDetails.CustomerID;
                    CurrOrderInvoiceDetails.CurrInvoiceDetails.InvoiceNumber = txtBoxInvOrdNumber.Text;
                    CurrOrderInvoiceDetails.CurrInvoiceDetails.ListInvoiceItems = new List<InvoiceItemDetails>();

                    FormTitle = "Create New Bill";
                    btnCreateBill.Text = "Create Bill";
                }
                else
                {
                    txtBoxInvOrdNumber.Text = CurrOrderInvoiceDetails.CurrInvoiceDetails.InvoiceNumber;

                    if (CurrOrderInvoiceDetails.CurrInvoiceDetails.ListInvoiceItems == null || CurrOrderInvoiceDetails.CurrInvoiceDetails.ListInvoiceItems.Count == 0)
                        ObjInvoicesModel.FillInvoiceItemDetails(CurrOrderInvoiceDetails.CurrInvoiceDetails);
                    foreach (var item in CurrOrderInvoiceDetails.CurrInvoiceDetails.ListInvoiceItems)
                    {
                        if (item.OrderQty <= 0) continue;
                        if (item.InvoiceItemStatus != INVOICEITEMSTATUS.Invoiced) continue;

                        Object[] row = new Object[6];
                        row[CategoryColIndex] = ObjProductMasterModel.GetProductDetails(item.ProductID).CategoryName;
                        row[ItemColIndex] = item.ProductName;
                        row[PriceColIndex] = item.Price.ToString("F");
                        row[SaleQtyColIndex] = item.SaleQty;
                        row[ItemSelectionSelectColIndex] = false;

                        Int32 Index = dtGridViewInvOrdProdList.Rows.Add(row);
                        DictItemsSelected.Add(item.ProductName, dtGridViewInvOrdProdList.Rows[Index]);
                    }
                    FormTitle = "Update Bill";
                    btnCreateBill.Text = "Update Bill";
                }
                lblStatus.Text = "Add/Delete/Modify Items to Bill";

                UpdateSummaryDetails();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadBillForCustomer()", ex);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                switch (BackgroundTask)
                {
                    case 2:     //Load Bill details
                        LoadBillForCustomer();
                        break;
                    case 3:     //Update Bill details
                        UpdateSalesInvoice();
                        break;
                    case 4:     //Save Bill details
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
                        cmbBoxProdCat.Focus();
                        cmbBoxProdCat.SelectedIndex = 0;
                        //if (CurrentInvoiceID < 0)
                        //{
                        //    MessageBox.Show(this, "Loaded Invoice data for selected Customer", "Customer Invoice", MessageBoxButtons.OK);
                        //}
                        break;
                    case 3:     //Update or Create Bill
                        MessageBox.Show(this, "Created Customer Bill successfully", "Create Bill", MessageBoxButtons.OK);
                        DialogResult dialogResult = MessageBox.Show(this, "Is this Bill Paid?", "Create Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (dialogResult == DialogResult.Yes)
                        {
                            //Create Payment entry
                            PaymentDetails tmpPaymentDetails = new PaymentDetails() {
                                CustomerID = AddUpdatedInvoiceDetails.CustomerID,
                                AccountID = CommonFunctions.ObjAccountsMasterModel.GetAccDtlsFromCustID(AddUpdatedInvoiceDetails.CustomerID).AccountID,
                                Description = "POS Payment",
                                InvoiceID = AddUpdatedInvoiceDetails.InvoiceID,
                                InvoiceNumber = AddUpdatedInvoiceDetails.InvoiceNumber,
                                PaidOn = AddUpdatedInvoiceDetails.InvoiceDate,
                                Amount = AddUpdatedInvoiceDetails.NetInvoiceAmount,
                                UserID = CommonFunctions.ObjUserMasterModel.GetUserID(MySQLHelper.GetMySqlHelperObj().CurrentUser),
                                PaymentModeID = ObjPaymentsModel.GetPaymentMode("Cash").PaymentModeID,
                                CreationDate = DateTime.Now,
                                LastUpdateDate = DateTime.Now
                            };

                            CustomerAccountHistoryDetails tmpCustomerAccountHistoryDetails = new CustomerAccountHistoryDetails() {
                                AccountID = tmpPaymentDetails.AccountID,
                                SaleAmount = AddUpdatedInvoiceDetails.ListInvoiceItems.Sum(s => s.SaleQty * s.Price),
                                DiscountAmount = AddUpdatedInvoiceDetails.DiscountAmount,
                                CancelAmount = 0,
                                RefundAmount = 0,
                                BalanceAmount = 0,
                                NewBalanceAmount = 0,
                                AmountReceived  = AddUpdatedInvoiceDetails.NetInvoiceAmount,
                                NetSaleAmount = AddUpdatedInvoiceDetails.NetInvoiceAmount,
                                TotalTaxAmount = 0
                            };

                            ObjPaymentsModel.CreateNewPaymentDetails(ref tmpPaymentDetails, ref tmpCustomerAccountHistoryDetails);
                        }

                        dialogResult = MessageBox.Show(this, "Would you like to print the Bill?", "Create Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (dialogResult == DialogResult.Yes)
                        {
                            InvoicesModel.PrintBill(AddUpdatedInvoiceDetails.InvoiceID);
                        }

                        cmbBoxCustomers.SelectedIndex = -1;
                        cmbBoxPhoneNumbers.SelectedIndex = -1;
                        cmbBoxCustomers.Focus();
                        ResetControls();
                        picBoxLoading.Visible = false;
                        EnableItemsPanel(false);
                        MessageBox.Show(this, "Created Customer Bill successfully", "Sales Bill", MessageBoxButtons.OK);
                        lblStatus.Text = "Choose a Customer to create Bill";
                        cmbBoxBillNumber.Items.Clear();
                        txtBoxInvOrdNumber.Text = "";

                        CurrOrderInvoiceDetails = null;
                        CurrCustomerDetails = null;
                        CurrCustomerDiscountGroup = null;
                        break;
                    case 4:     //Create Bill
                        cmbBoxCustomers.SelectedIndex = -1;
                        cmbBoxPhoneNumbers.SelectedIndex = -1;
                        cmbBoxCustomers.Focus();
                        ResetControls();
                        picBoxLoading.Visible = false;
                        EnableItemsPanel(false);
                        MessageBox.Show(this, "Created Customer Bill successfully", "Sales Bill", MessageBoxButtons.OK);
                        lblStatus.Text = "Choose a Customer to create Bill";
                        cmbBoxBillNumber.Items.Clear();
                        txtBoxInvOrdNumber.Text = "";

                        CurrOrderInvoiceDetails = null;
                        CurrCustomerDetails = null;
                        CurrCustomerDiscountGroup = null;
                        IsSaveBillClicked = false;
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
                List<ProductDetails> tmpListProducts = new List<ProductDetails>();
                if (cmbBoxProduct.SelectedIndex == 0)
                    tmpListProducts.AddRange(ListProducts);
                else
                    tmpListProducts.AddRange(ListProducts.Where(s => s.ItemName.Equals(cmbBoxProduct.SelectedItem.ToString(), StringComparison.InvariantCultureIgnoreCase)));

                for (int i = 0; i < tmpListProducts.Count; i++)
                {
                    Double Price = CommonFunctions.ObjProductMaster.GetPriceForProduct(tmpListProducts[i].ItemName, CurrCustomerDetails.PriceGroupIndex);
                    //Price = Price * ((CommonFunctions.ObjProductMaster.GetTaxRatesForProduct(tmpListProducts[i].ItemName).Sum() + 100) / 100);

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
                    Object[] row = new Object[dtGridViewInvOrdProdList.Columns.Count];
                    row[CategoryColIndex] = ListSelectedRows[j].Cells[CategoryColIndex].Value;
                    row[ItemColIndex] = ListSelectedRows[j].Cells[ItemColIndex].Value;
                    row[PriceColIndex] = ListSelectedRows[j].Cells[PriceColIndex].Value;
                    row[ItemSelectionSelectColIndex] = false;
                    row[SaleQtyColIndex] = ListSelectedRows[j].Cells[QtyColIndex].Value;
                    ListSelectedRows[j].Cells[SelectColIndex].Value = false;
                    ListSelectedRows[j].Cells[QtyColIndex].Value = 0;

                    if (!DictItemsSelected.ContainsKey(row[ItemColIndex].ToString()))
                    {
                        Int32 RowIndex = dtGridViewInvOrdProdList.Rows.Add(row);
                        DictItemsSelected.Add(row[ItemColIndex].ToString(), dtGridViewInvOrdProdList.Rows[RowIndex]);
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
                if (dtGridViewInvOrdProdList.Rows.Count == 0) return;

                List<DataGridViewRow> ListSelectedRows = new List<DataGridViewRow>();
                if (ListSelectedRowIndexesToRemove.Count == 0)
                {
                    if (dtGridViewProdListForSelection.SelectedRows.Count > 0)
                        ListSelectedRows.Add(dtGridViewInvOrdProdList.SelectedRows[0]);
                    else return;
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
                CommonFunctions.ShowErrorDialog($"{this}.btnRemItem_Click()", ex);
            }
        }

        private void dtGridViewInvOrdProdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != ItemSelectionSelectColIndex || e.RowIndex < 0) return;

                dtGridViewInvOrdProdList.CommitEdit(DataGridViewDataErrorContexts.Commit);

                if (Boolean.Parse(dtGridViewInvOrdProdList.Rows[e.RowIndex].Cells[ItemSelectionSelectColIndex].Value.ToString()))
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
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewInvOrdProdList_CellContentClick()", ex);
            }
        }

        private void UpdateCustomerDetails()
        {
            try
            {
                CustomerDetails sellerDetails = CommonFunctions.ObjCustomerMasterModel.GetCustomerDetails(cmbBoxCustomers.SelectedValue.ToString());
                String CustomerDetails = sellerDetails.CustomerName + "\n" + sellerDetails.Address + "\n" + sellerDetails.PhoneNo;
                lblCustomerDetails.Text = CustomerDetails;
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
                NumItems = dtGridViewInvOrdProdList.Rows.Count; Quantity = 0; SubTotal = 0; Discount = 0; TaxAmount = 0; GrandTotal = 0;

                foreach (DataGridViewRow item in dtGridViewInvOrdProdList.Rows)
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
                    CurrOrderInvoiceDetails = null;
                    CurrCustomerDetails = null;
                    CurrCustomerDiscountGroup = null;
                    lblCustomerDetails.Text = "";
                    return;
                }

                //Check if the Invoice is changed before changing Customer
                Boolean WarnUser = false;
                if (CurrOrderInvoiceDetails != null)
                {
                    if (cmbBoxCustomers.SelectedIndex == cmbBoxCustomerIndex) return;

                    if (dtGridViewInvOrdProdList.Rows.Count != CurrOrderInvoiceDetails.OrderItemCount)
                    {
                        WarnUser = true;
                    }
                    else
                    {
                        foreach (DataGridViewRow item in dtGridViewInvOrdProdList.Rows)
                        {
                            String ItemName = item.Cells[ItemColIndex].Value.ToString();
                            Double Qty = CurrOrderInvoiceDetails.CurrOrderDetails.ListOrderItems.Find(s => s.ProductName.Equals(ItemName.ToUpper(), StringComparison.InvariantCulture)).OrderQty;

                            if (Qty != Double.Parse(item.Cells[SaleQtyColIndex].Value.ToString()))
                            {
                                WarnUser = true;
                                break;
                            }
                        }
                    }

                    if (WarnUser)
                    {
                        DialogResult diagResult = MessageBox.Show(this, "All Changes made to this Bill will be lost.\nAre you sure to change the Customer?", "Change Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        if (diagResult == DialogResult.No)
                        {
                            cmbBoxCustomers.SelectedIndex = cmbBoxCustomerIndex;
                            return;
                        }
                    }
                }
                cmbBoxCustomerIndex = cmbBoxCustomers.SelectedIndex;
                CurrOrderInvoiceDetails = null;
                CurrCustomerDetails = null;
                CurrCustomerDiscountGroup = null;
                CurrentInvoiceID = -1;
                cmbBoxBillNumber.Items.Clear();
                cmbBoxInvoiceNumberIndex = -1;
                ListCustomerInvoices = null;
                ResetControls();
                ObjInvoicesModel.Initialize();

                //Load Selected Customer Details
                CurrCustomerDetails = ObjCustomerMasterModel.GetCustomerDetails(cmbBoxCustomers.SelectedItem.ToString().Trim());
                UpdateCustomerDetails();

                if (!IsPhoneNumberChanged)
                {
                    IsCustomerNameChanged = true;
                    cmbBoxPhoneNumbers.SelectedIndex = ListCustomerPhoneNumbers.FindIndex(s => s == CurrCustomerDetails.PhoneNo);
                }
                IsPhoneNumberChanged = false;

                CurrOrderInvoiceDetails = new CustomerOrderInvoiceDetails();
                CurrOrderInvoiceDetails.CustomerName = CurrCustomerDetails.CustomerName;

                CurrCustomerDiscountGroup = ObjCustomerMasterModel.GetCustomerDiscount(CurrCustomerDetails.CustomerName);
                if (CurrCustomerDiscountGroup.DiscountType == DiscountTypes.PERCENT)
                    DiscountPerc = CurrCustomerDiscountGroup.Discount;
                else if (CurrCustomerDiscountGroup.DiscountType == DiscountTypes.ABSOLUTE)
                    DiscountValue = CurrCustomerDiscountGroup.Discount;

                picBoxLoading.Visible = true;
                lblStatus.Text = "Loading Bill data. Please wait...";
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

        Boolean IsPhoneNumberChanged = false, IsCustomerNameChanged = false;
        private void cmbBoxPhoneNumbers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxPhoneNumbers.SelectedIndex < 0 || !LoadCompleted)
                {
                    cmbBoxPhoneNumberIndex = -1;
                    cmbBoxInvoiceNumberIndex = -1;
                    CurrOrderInvoiceDetails = null;
                    CurrCustomerDetails = null;
                    CurrCustomerDiscountGroup = null;
                    lblCustomerDetails.Text = "";
                    return;
                }

                //Check if the Bill is changed before changing Customer
                Boolean WarnUser = false;
                if (CurrOrderInvoiceDetails != null && !IsCustomerNameChanged)
                {
                    if (cmbBoxPhoneNumbers.SelectedIndex == cmbBoxPhoneNumberIndex) return;

                    if (dtGridViewInvOrdProdList.Rows.Count != CurrOrderInvoiceDetails.OrderItemCount)
                    {
                        WarnUser = true;
                    }
                    else
                    {
                        foreach (DataGridViewRow item in dtGridViewInvOrdProdList.Rows)
                        {
                            String ItemName = item.Cells[ItemColIndex].Value.ToString();
                            Double Qty = CurrOrderInvoiceDetails.CurrOrderDetails.ListOrderItems.Find(s => s.ProductName.Equals(ItemName.ToUpper(), StringComparison.InvariantCulture)).OrderQty;

                            if (Qty != Double.Parse(item.Cells[SaleQtyColIndex].Value.ToString()))
                            {
                                WarnUser = true;
                                break;
                            }
                        }
                    }

                    if (WarnUser)
                    {
                        DialogResult diagResult = MessageBox.Show(this, "All Changes made to this Bill will be lost.\nAre you sure to change the Customer?", "Change Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                        if (diagResult == DialogResult.No)
                        {
                            cmbBoxPhoneNumbers.SelectedIndex = cmbBoxPhoneNumberIndex;
                            return;
                        }
                    }
                }
                cmbBoxPhoneNumberIndex = cmbBoxPhoneNumbers.SelectedIndex;

                if (!IsCustomerNameChanged)
                {
                    IsPhoneNumberChanged = true;
                    CustomerDetails tmpCustomerDetails = DictPhoneNumberCustomerDetails[cmbBoxPhoneNumbers.SelectedItem.ToString()];
                    cmbBoxCustomers.SelectedIndex = ListCustomerNames.FindIndex(s => s.Equals(tmpCustomerDetails.CustomerName, StringComparison.InvariantCultureIgnoreCase));
                }
                IsCustomerNameChanged = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxPhoneNumbers_SelectedIndexChanged()", ex);
            }

        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new CreateCustomerForm(null, UpdateCustomersOnClose, true), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnAddCustomer_Click()", ex);
            }
        }

        private void UpdateCustomersOnClose(Int32 Mode, object ObjAddUpdated)
        {
            try
            {
                switch (Mode)
                {
                    case 1:         //Add New Customer
                        CustomerDetails tmpCustomerDetails = (CustomerDetails)ObjAddUpdated;
                        ListCustomerNames.Add(tmpCustomerDetails.CustomerName);
                        if (tmpCustomerDetails.PhoneNo.Trim()!=string.Empty && !DictPhoneNumberCustomerDetails.ContainsKey(tmpCustomerDetails.PhoneNo))
                        {
                            DictPhoneNumberCustomerDetails.Add(tmpCustomerDetails.PhoneNo, tmpCustomerDetails);
                            ListCustomerPhoneNumbers.Add(tmpCustomerDetails.PhoneNo);
                        }

                        cmbBoxCustomers.SelectedIndexChanged -= cmbBoxCustomers_SelectedIndexChanged;
                        cmbBoxPhoneNumbers.SelectedIndexChanged -= cmbBoxPhoneNumbers_SelectedIndexChanged;
                        //cmbBoxCustomers.AutoCompleteCustomSource.AddRange(ListCustomerNames.ToArray());
                        //cmbBoxPhoneNumbers.AutoCompleteCustomSource.AddRange(ListCustomerPhoneNumbers.Select(e => e.ToString()).ToArray());
                        cmbBoxCustomers.AutoCompleteMode = AutoCompleteMode.None;
                        cmbBoxCustomers.AutoCompleteSource = AutoCompleteSource.None;
                        cmbBoxCustomers.DataSource = null;
                        cmbBoxCustomers.Items.Clear();
                        cmbBoxCustomers.Items.AddRange(ListCustomerNames.Select(e => e.ToString()).ToArray());
                        cmbBoxCustomers.DataSource = ListCustomerNames;
                        cmbBoxCustomers.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmbBoxCustomers.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmbBoxPhoneNumbers.AutoCompleteMode = AutoCompleteMode.None;
                        cmbBoxPhoneNumbers.AutoCompleteSource = AutoCompleteSource.None;
                        cmbBoxPhoneNumbers.DataSource = null;
                        cmbBoxPhoneNumbers.Items.Clear();
                        cmbBoxPhoneNumbers.Items.AddRange(ListCustomerPhoneNumbers.Select(e => e.ToString()).ToArray());
                        cmbBoxPhoneNumbers.DataSource = ListCustomerPhoneNumbers;
                        cmbBoxPhoneNumbers.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        cmbBoxPhoneNumbers.AutoCompleteSource = AutoCompleteSource.ListItems;

                        cmbBoxCustomers.SelectedIndexChanged += cmbBoxCustomers_SelectedIndexChanged;
                        cmbBoxPhoneNumbers.SelectedIndexChanged += cmbBoxPhoneNumbers_SelectedIndexChanged;
                        cmbBoxCustomers.SelectedIndex = ListCustomerNames.FindIndex(e => e == tmpCustomerDetails.CustomerName);
                        cmbBoxPhoneNumbers.SelectedIndex = ListCustomerPhoneNumbers.FindIndex(e => e == tmpCustomerDetails.PhoneNo);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateCustomersOnClose()", ex);
            }
        }

        private void dtGridViewInvOrdProdList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.ColumnIndex != SaleQtyColIndex && e.ColumnIndex != PriceColIndex) || e.RowIndex < 0) return;

                DataGridViewCell cell = dtGridViewInvOrdProdList.Rows[e.RowIndex].Cells[e.ColumnIndex];
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

                dtGridViewInvOrdProdList.CommitEdit(DataGridViewDataErrorContexts.Commit);
                UpdateSummaryDetails();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewInvOrdProdList_CellEndEdit()", ex);
            }
        }

        private Boolean IsDataValidInGridViewInvOrdProdList()
        {
            try
            {
                for (int i = 0; i < dtGridViewInvOrdProdList.Rows.Count; i++)
                {
                    if (!String.IsNullOrEmpty(dtGridViewInvOrdProdList.Rows[i].Cells[SaleQtyColIndex].ErrorText)) return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.IsDataValidInGridViewInvOrdProdList()", ex);
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
                if (ListSelectedRowIndexesToRemove.Count != dtGridViewInvOrdProdList.Rows.Count) Checked = true;
                ListSelectedRowIndexesToRemove.Clear();
                Int32 Index = 0;
                foreach (DataGridViewRow item in dtGridViewInvOrdProdList.Rows)
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
                //cmbBoxCustomers.Enabled = enable;
                cmbBoxProdCat.Enabled = enable;
                cmbBoxProduct.Enabled = enable;
                cmbBoxBillNumber.Enabled = enable;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.EnableItemsPanel()", ex);
            }
        }

        private void cmbBoxBillNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxBillNumber.SelectedIndex < 0 || cmbBoxBillNumber.SelectedIndex == cmbBoxInvoiceNumberIndex)
                    return;

                EnableItemsPanel(false);
                if (dtGridViewInvOrdProdList.Rows.Count > 0)
                {
                    DialogResult diagResult = MessageBox.Show(this, "All changes made to this Bill will be lost.\nAre you sure to change the Bill?", "Change Customer Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (diagResult == DialogResult.No)
                    {
                        cmbBoxBillNumber.SelectedIndex = cmbBoxInvoiceNumberIndex;
                        EnableItemsPanel(true);
                        return;
                    }
                }
                ResetControls();
                cmbBoxInvoiceNumberIndex = cmbBoxBillNumber.SelectedIndex;

                if (cmbBoxInvoiceNumberIndex == 0)
                {
                    //Create New Bill
                    txtBoxInvOrdNumber.Text = ObjInvoicesModel.GenerateNewBillNumber();
                    CurrentInvoiceID = -1;
                }
                else
                {
                    //Load item details from Bill
                    txtBoxInvOrdNumber.Text = cmbBoxBillNumber.SelectedItem.ToString();
                    CurrentInvoiceID = ListCustomerInvoices.Find(s => s.InvoiceNumber.Equals(txtBoxInvOrdNumber.Text)).InvoiceID;
                }

                CurrOrderInvoiceDetails = new CustomerOrderInvoiceDetails();
                CurrOrderInvoiceDetails.CustomerName = CurrCustomerDetails.CustomerName;

                CurrCustomerDiscountGroup = ObjCustomerMasterModel.GetCustomerDiscount(CurrCustomerDetails.CustomerName);
                if (CurrCustomerDiscountGroup.DiscountType == DiscountTypes.PERCENT)
                    DiscountPerc = CurrCustomerDiscountGroup.Discount;
                else if (CurrCustomerDiscountGroup.DiscountType == DiscountTypes.ABSOLUTE)
                    DiscountValue = CurrCustomerDiscountGroup.Discount;

                picBoxLoading.Visible = true;
                lblStatus.Text = "Loading Bill data. Please wait...";
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
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxBillNumber_SelectedIndexChanged()", ex);
            }
        }

        private void CustomerInvoiceSellerOrderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommonFunctions.WriteToSettingsFile();
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

        private void btnSaveBill_Click(object sender, EventArgs e)
        {
            try
            {
                IsSaveBillClicked = true;
                btnCreateBill.PerformClick();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnSaveBill_Click()", ex);
            }
        }
    }
}
