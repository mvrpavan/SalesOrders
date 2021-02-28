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
    partial class CreateOrderInvoiceForm : Form
    {
        String FormTitle = "", OrderInvoice = "";
        Boolean IsCustomerOrder, IsCustomerInvoice;
        List<ProductDetails> ListAllProducts, ListProducts;
        List<String> ListCustomerNames;
        CustomerDetails CurrCustomerDetails;
        DiscountGroupDetails CurrCustomerDiscountGroup;
        CustomerOrderInvoiceDetails CurrOrderInvoiceDetails;
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
        Int32 CurrentOrderInvoiceID = -1;

        ProductMasterModel ObjProductMasterModel = CommonFunctions.ObjProductMaster;
        CustomerMasterModel ObjCustomerMasterModel = CommonFunctions.ObjCustomerMasterModel;
        OrdersModel ObjOrdersModel;
        InvoicesModel ObjInvoicesModel;
        UpdateUsingObjectOnCloseDel UpdateObjectOnClose;
        List<InvoiceDetails> ListCustomerInvoices;

        public CreateOrderInvoiceForm(Int32 OrderInvoiceID, Boolean IsCustomerOrder, Boolean IsCustomerInvoice, UpdateUsingObjectOnCloseDel UpdateObjectOnClose)
        {
            try
            {
                InitializeComponent();
                CommonFunctions.ResetProgressBar();

                if (IsCustomerOrder)
                {
                    ObjOrdersModel = new OrdersModel();
                    ObjOrdersModel.Initialize();

                    if (OrderInvoiceID < 0)
                    {
                        FormTitle = "Create New Order";
                        btnCreateInvOrd.Text = "Create Order";
                        txtBoxInvOrdNumber.Text = this.ObjOrdersModel.GenerateNewOrderNumber();
                    }
                    else
                    {
                        FormTitle = "View/Edit Order";
                        btnCreateInvOrd.Text = "Update Order";
                    }
                    OrderInvoice = "Order";

                    lblSelectName.Text = "Choose Customer";
                    lblInvoiceNumber.Text = "Order#";
                    lblInvoiceDate.Text = "Order Date";

                    lblInvoiceNumber.Visible = true;
                    txtBoxInvOrdNumber.Enabled = true;
                    txtBoxInvOrdNumber.Visible = true;
                    txtBoxInvOrdNumber.ReadOnly = true;
                    btnDiscount.Enabled = false;
                    lblStatus.Text = "";

                    btnEditBalanceAmount.Enabled = false;
                    btnResetBalanceAmount.Enabled = false;
                    lblStatus.Text = "Please choose Invoice date";
                    dtGridViewInvOrdProdList.Columns[OrdQtyColIndex].ReadOnly = true;
                }
                else if (IsCustomerInvoice)
                {
                    ObjInvoicesModel = new InvoicesModel();
                    ObjInvoicesModel.Initialize();
                    if (OrderInvoiceID < 0)
                    {
                        FormTitle = "Create New Invoice";
                        btnCreateInvOrd.Text = "Create Invoice";
                        txtBoxInvOrdNumber.Text = this.ObjInvoicesModel.GenerateNewInvoiceNumber();
                    }
                    else
                    {
                        FormTitle = "View/Edit Invoice";
                        btnCreateInvOrd.Text = "Update Invoice";
                    }

                    OrderInvoice = "Invoice";
                    txtBoxInvOrdNumber.Enabled = true;
                    txtBoxInvOrdNumber.ReadOnly = true;
                    lblSelectName.Text = "Select Customer";
                    lblInvoiceNumber.Text = "Invoice#";
                    lblInvoiceDate.Text = "Invoice Date";
                    btnDiscount.Enabled = true;
                    lblSelectInvoiceNum.Enabled = true;
                    lblSelectInvoiceNum.Visible = true;
                    cmbBoxInvoiceNumber.Enabled = true;
                    cmbBoxInvoiceNumber.Visible = true;
                    lblStatus.Text = "Please choose Invoice date";
                    dtGridViewInvOrdProdList.Columns[PriceColIndex].ReadOnly = false;
                }

                this.IsCustomerOrder = IsCustomerOrder;
                this.IsCustomerInvoice = IsCustomerInvoice;
                this.Text = FormTitle;
                picBoxLoading.Visible = false;
                dtGridViewProdListForSelection.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dtGridViewInvOrdProdList.SelectionMode = DataGridViewSelectionMode.CellSelect;
                this.UpdateObjectOnClose = UpdateObjectOnClose;
                CurrentOrderInvoiceID = OrderInvoiceID;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ctor()", ex);
                throw;
            }
        }

        private void CreateOrderInvoiceForm_Load(object sender, EventArgs e)
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

                CurrOrderInvoiceDetails = null;
                CurrCustomerDetails = null;
                CurrCustomerDiscountGroup = null;
                ResetControls();

                if (CurrentOrderInvoiceID > 0)
                {
                    if(IsCustomerOrder)
                    {
                        OrderDetails ObjOrderDetails = ObjOrdersModel.GetOrderDetailsForOrderID(CurrentOrderInvoiceID);
                        cmbBoxCustomers.SelectedIndex = cmbBoxCustomers.Items.IndexOf(ObjOrderDetails.CustomerName);

                        EditCustomerOrder();
                    }
                    else if (IsCustomerInvoice)
                    {
                        InvoiceDetails ObjInvoiceDetails = ObjInvoicesModel.GetInvoiceDetailsForInvoiceID(CurrentOrderInvoiceID);

                        //Load Selected Customer Details
                        cmbBoxCustomers.SelectedIndex = cmbBoxCustomers.Items.IndexOf(ObjInvoiceDetails.CustomerName);
                        CurrCustomerDetails = ObjCustomerMasterModel.GetCustomerDetails(ObjInvoiceDetails.CustomerName);
                        UpdateCustomerDetails();

                        ListCustomerInvoices = ObjInvoicesModel.GetInvoiceDetailsForCustomer(dtTmPckrInvOrdDate.Value, CurrCustomerDetails.CustomerID);
                        cmbBoxInvoiceNumber.Items.Clear();
                        cmbBoxInvoiceNumber.Items.Add("<New>");
                        cmbBoxInvoiceNumber.Items.AddRange(ListCustomerInvoices.Select(s => s.InvoiceNumber).ToArray());
                        cmbBoxInvoiceNumber.SelectedIndex = ListCustomerInvoices.FindIndex(s => s.InvoiceID == CurrentOrderInvoiceID) + 1;
                    }
                    cmbBoxCustomerIndex = cmbBoxCustomers.SelectedIndex;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CustomerInvoiceForm_Load()", ex);
            }
        }

        private void EditCustomerOrder()
        {
            try
            {
                if (CurrCustomerDetails == null)
                {
                    //Load Selected Customer Details
                    CurrCustomerDetails = ObjCustomerMasterModel.GetCustomerDetails(cmbBoxCustomers.SelectedItem.ToString().Trim());
                    UpdateCustomerDetails();
                }
                CurrOrderInvoiceDetails = new CustomerOrderInvoiceDetails();
                CurrOrderInvoiceDetails.CustomerName = CurrCustomerDetails.CustomerName;

                CurrCustomerDiscountGroup = ObjCustomerMasterModel.GetCustomerDiscount(CurrCustomerDetails.CustomerName);
                if (CurrCustomerDiscountGroup.DiscountType == DiscountTypes.PERCENT)
                    DiscountPerc = CurrCustomerDiscountGroup.Discount;
                else if (CurrCustomerDiscountGroup.DiscountType == DiscountTypes.ABSOLUTE)
                    DiscountValue = CurrCustomerDiscountGroup.Discount;

                if (IsCustomerOrder)
                {
                    //CurrOrderInvoiceDetails.CurrOrderDetailsOrig = ObjOrdersModel.GetOrderDetailsForCustomer(dtTmPckrInvOrdDate.Value, CurrCustomerDetails.CustomerID);
                    //if (CurrOrderInvoiceDetails.CurrOrderDetailsOrig != null)
                    //{
                    //    CurrOrderInvoiceDetails.CurrOrderDetailsOrig = CurrOrderInvoiceDetails.CurrOrderDetailsOrig.Clone();
                    //    CurrOrderInvoiceDetails.CurrOrderDetails = CurrOrderInvoiceDetails.CurrOrderDetailsOrig.Clone();
                    //}
                    //else
                    //{
                    //    CurrentOrderInvoiceID = -1;
                    //}

                    picBoxLoading.Visible = true;
                    lblStatus.Text = "Loading Customer order data. Please wait...";
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
                else if (IsCustomerInvoice)
                {

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
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.EditCustomerOrder()", ex);
            }
        }

        private void CustomerInvoiceSellerOrderForm_Shown(object sender, EventArgs e)
        {
            try
            {
                this.ControlBox = true;
                this.MaximizeBox = true;
                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.CenterScreen;

                if (CurrentOrderInvoiceID < 0)
                {
                    ResetControls();
                    EnableItemsPanel(false);
                }

                LoadCompleted = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CustomerInvoiceSellerOrderForm_Shown()", ex);
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
                if (!IsDataValidInGridViewInvOrdProdList())
                {
                    MessageBox.Show(this, "One or multiple errors in selected Item list. Please correct and retry.", "Item list Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (IsCustomerOrder)
                {
                    //Update Order Details to CurrSellerOrderDetails
                    if (CurrOrderInvoiceDetails == null) return;

                    if (dtGridViewInvOrdProdList.Rows.Count == 0)
                    {
                        MessageBox.Show(this, "There are no Items in the Order.\nPlease add atleast one Item to the Order.", "Create Order", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    Boolean IsValid = false;
                    for (int i = 0; i < dtGridViewInvOrdProdList.Rows.Count; i++)
                    {
                        if (Double.Parse(dtGridViewInvOrdProdList.Rows[i].Cells[OrdQtyColIndex].Value.ToString()) > 0)
                        {
                            IsValid = true;
                            break;
                        }
                    }

                    if (!IsValid)
                    {
                        MessageBox.Show(this, "There are no Items with Quantity more than 0.\nPlease add atleast one Item with valid Quantity.", "Create Order", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    DialogResult diagResult = MessageBox.Show(this, "Confirm to Create/Update Customer Order", "Create Customer Order", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    if (diagResult == DialogResult.No) return;

                    CurrOrderInvoiceDetails.CurrOrderDetails.ListOrderItems.Clear();
                    CurrOrderInvoiceDetails.CurrOrderDetails.OrderItemCount = 0;

                    Boolean OrderModified = false;
                    foreach (DataGridViewRow item in dtGridViewInvOrdProdList.Rows)
                    {
                        String ItemName = item.Cells[ItemColIndex].Value.ToString();
                        ProductDetails tmpProductDetails = ObjProductMasterModel.GetProductDetails(ItemName);
                        CurrOrderInvoiceDetails.CurrOrderDetails.OrderItemCount += 1;
                        OrderItemDetails tmpOrderItemDetails = new OrderItemDetails()
                        {
                            ProductID = tmpProductDetails.ProductID,
                            ProductName = ItemName,
                            OrderQty = Double.Parse(item.Cells[SaleQtyColIndex].Value.ToString()),
                            Price = Double.Parse(item.Cells[PriceColIndex].Value.ToString()),
                            OrderItemStatus = ORDERITEMSTATUS.Ordered
                        };
                        CurrOrderInvoiceDetails.CurrOrderDetails.ListOrderItems.Add(tmpOrderItemDetails);

                        if (!OrderModified && CurrOrderInvoiceDetails.CurrOrderDetailsOrig != null)
                        {
                            if (CurrOrderInvoiceDetails.CurrOrderDetailsOrig.ListOrderItems != null)
                            {
                                List<OrderItemDetails> ListOrderItems = CurrOrderInvoiceDetails.CurrOrderDetailsOrig.ListOrderItems;
                                Int32 Index = ListOrderItems.FindIndex(s => s.ProductID == tmpProductDetails.ProductID);
                                if (Index >= 0 && Math.Abs(ListOrderItems[Index].OrderQty - tmpOrderItemDetails.OrderQty) > 0)
                                {
                                    OrderModified = true;
                                }
                            }
                        }
                    }
                    CurrOrderInvoiceDetails.CurrOrderDetails.OrderItemCount = CurrOrderInvoiceDetails.CurrOrderDetails.ListOrderItems.Count;
                    CurrOrderInvoiceDetails.OrderItemCount = CurrOrderInvoiceDetails.CurrOrderDetails.OrderItemCount;

                    picBoxLoading.Visible = true;
                    BackgroundTask = 3;
                    if (OrderModified) lblStatus.Text = "Updating Customer order, please wait...";
                    else lblStatus.Text = "Creating Customer order, please wait...";
#if DEBUG
                    backgroundWorker1_DoWork(null, null);
                    backgroundWorker1_RunWorkerCompleted(null, null);
#else
                    ReportProgress = backgroundWorker1.ReportProgress;
                    backgroundWorker1.RunWorkerAsync();
                    backgroundWorker1.WorkerReportsProgress = true;
#endif
                }

                if (IsCustomerInvoice)
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
                        if (Double.Parse(dtGridViewInvOrdProdList.Rows[i].Cells[SaleQtyColIndex].Value.ToString()) > 0)
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
                            OrderQty = Double.Parse(item.Cells[OrdQtyColIndex].Value.ToString()),
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
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnCreateInvOrd_Click()", ex);
            }
        }

        void UpdateSalesOrder()
        {
            try
            {
                OrderDetails AddUpdatedOrderDetails = null;
                if (CurrOrderInvoiceDetails.CurrOrderDetails.OrderID < 0)
                {
                    AddUpdatedOrderDetails = ObjOrdersModel.CreateNewOrderForCustomer(CurrCustomerDetails.CustomerID, 
                                            dtTmPckrInvOrdDate.Value, CurrOrderInvoiceDetails.CurrOrderDetails.OrderNumber, 
                                            CurrOrderInvoiceDetails.CurrOrderDetails.ListOrderItems);
                    UpdateObjectOnClose(1, AddUpdatedOrderDetails);
                }
                else
                {
                    AddUpdatedOrderDetails = ObjOrdersModel.UpdateOrderDetails(CurrOrderInvoiceDetails.CurrOrderDetails);
                    UpdateObjectOnClose(2, AddUpdatedOrderDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateSalesOrder()", ex);
            }
        }

        void UpdateSalesInvoice()
        {
            try
            {
                InvoiceDetails AddUpdatedInvoiceDetails = null;
                if (CurrOrderInvoiceDetails.CurrInvoiceDetails.InvoiceID < 0)
                {
                    AddUpdatedInvoiceDetails = ObjInvoicesModel.CreateNewInvoiceForCustomer(CurrCustomerDetails.CustomerID, CurrOrderInvoiceDetails.CurrInvoiceDetails.OrderID, dtTmPckrInvOrdDate.Value, CurrOrderInvoiceDetails.CurrInvoiceDetails.InvoiceNumber, CurrOrderInvoiceDetails.CurrInvoiceDetails.ListInvoiceItems, Double.Parse(lblDiscount.Text));
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

        void LoadSalesOrderForCurrCustomer()
        {
            try
            {
                OrderDetails CurrentOrderDetails = null;
                if (CurrentOrderInvoiceID > 0)
                {
                    //Load Order details for selected OrderID
                    CurrentOrderDetails = ObjOrdersModel.GetOrderDetailsForOrderID(CurrentOrderInvoiceID);
                    CurrOrderInvoiceDetails.CurrOrderDetails = CurrentOrderDetails.Clone();
                    CurrOrderInvoiceDetails.CurrOrderDetailsOrig = CurrentOrderDetails.Clone();
                }
                else
                {
                    //Create new Order for selected Customer
                    CustomerDetails ObjSellerDetails = CommonFunctions.ObjCustomerMasterModel.GetCustomerDetails(cmbBoxCustomers.Items[cmbBoxCustomers.SelectedIndex].ToString());
                    CurrentOrderDetails = ObjOrdersModel.GetOrderDetailsForCustomer(dtTmPckrInvOrdDate.Value, ObjSellerDetails.CustomerID);
                    if (CurrentOrderDetails != null)
                    {
                        CurrentOrderInvoiceID = CurrentOrderDetails.OrderID;
                        CurrOrderInvoiceDetails.CurrOrderDetails = CurrentOrderDetails.Clone();
                        CurrOrderInvoiceDetails.CurrOrderDetailsOrig = CurrentOrderDetails.Clone();
                    }
                }

                if (CurrOrderInvoiceDetails.CurrOrderDetails == null)
                {
                    txtBoxInvOrdNumber.Text = this.ObjOrdersModel.GenerateNewOrderNumber();

                    CurrOrderInvoiceDetails.CurrOrderDetails = new OrderDetails();
                    CurrOrderInvoiceDetails.CurrOrderDetails.OrderID = -1;
                    CurrOrderInvoiceDetails.CurrOrderDetails.CustomerID = CurrCustomerDetails.CustomerID;
                    CurrOrderInvoiceDetails.CurrOrderDetails.OrderNumber = txtBoxInvOrdNumber.Text;
                    CurrOrderInvoiceDetails.CurrOrderDetails.ListOrderItems = new List<OrderItemDetails>();

                    FormTitle = "Create New Order";
                    btnCreateInvOrd.Text = "Create New Order";
                }
                else
                {
                    if (CurrOrderInvoiceDetails.CurrOrderDetails.ListOrderItems == null || CurrOrderInvoiceDetails.CurrOrderDetails.ListOrderItems.Count == 0)
                        ObjOrdersModel.FillOrderItemDetails(CurrOrderInvoiceDetails.CurrOrderDetails);
                    foreach (var item in CurrOrderInvoiceDetails.CurrOrderDetails.ListOrderItems)
                    {
                        if (item.OrderQty <= 0) continue;
                        if (item.OrderItemStatus != ORDERITEMSTATUS.Ordered) continue;

                        Object[] row = new Object[6];
                        row[CategoryColIndex] = ObjProductMasterModel.GetProductDetails(item.ProductID).CategoryName;
                        row[ItemColIndex] = item.ProductName;
                        row[PriceColIndex] = item.Price.ToString("F");
                        row[OrdQtyColIndex] = item.OrderQty; row[SaleQtyColIndex] = item.OrderQty; row[ItemSelectionSelectColIndex] = false;

                        Int32 Index = dtGridViewInvOrdProdList.Rows.Add(row);
                        DictItemsSelected.Add(item.ProductName, dtGridViewInvOrdProdList.Rows[Index]);
                    }
                    btnCreateInvOrd.Text = "Update Order";
                    txtBoxInvOrdNumber.Text = CurrOrderInvoiceDetails.CurrOrderDetails.OrderNumber;
                }
                lblStatus.Text = "Add/Modify Items to Seller Sales order";

                UpdateSummaryDetails();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadSalesOrderForCurrCustomer()", ex);
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

                if (IsCustomerOrder)
                {
                    BackgroundTask = 4;
                    DialogResult diagResult = MessageBox.Show(this, "Are you sure to cancel the changes made to Order?", "Cancel Order", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (diagResult == DialogResult.No) return;

                    backgroundWorker1_RunWorkerCompleted(null, null);
                }

                if (IsCustomerInvoice)
                {
                    BackgroundTask = 4;
                    DialogResult diagResult = MessageBox.Show(this, "Are you sure to cancel the Bill?", "Cancel Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (diagResult == DialogResult.No) return;

                    backgroundWorker1_RunWorkerCompleted(null, null);
                }
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

        private void LoadInvoiceForCustomer()
        {
            try
            {
                InvoiceDetails CurrentInvoiceDetails = null;
                if (CurrentOrderInvoiceID > 0)
                {
                    //Load Invoice details for selected InvoiceID
                    CurrentInvoiceDetails = ObjInvoicesModel.GetInvoiceDetailsForInvoiceID(CurrentOrderInvoiceID);
                    CurrOrderInvoiceDetails.CurrInvoiceDetails = CurrentInvoiceDetails.Clone();
                    CurrOrderInvoiceDetails.CurrInvoiceDetailsOrig = CurrentInvoiceDetails.Clone();
                    if (CurrentInvoiceDetails.InvoiceStatus == INVOICESTATUS.Cancelled)
                    {
                        MessageBox.Show(this, "You have selected a cancelled Invoice to view/update.", "Load Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                }
                else if (ListCustomerInvoices != null && cmbBoxInvoiceNumberIndex == cmbBoxInvoiceNumber.SelectedIndex)
                {
                    CurrentOrderInvoiceID = -1;
                }
                else
                {
                    //Check for existing Invoice or create new Invoice for selected Customer
                    CustomerDetails ObjCustomerDetails = CommonFunctions.ObjCustomerMasterModel.GetCustomerDetails(cmbBoxCustomers.Items[cmbBoxCustomers.SelectedIndex].ToString());
                    ListCustomerInvoices = ObjInvoicesModel.GetInvoiceDetailsForCustomer(dtTmPckrInvOrdDate.Value, ObjCustomerDetails.CustomerID);
                    ListCustomerInvoices.RemoveAll(e => e.InvoiceStatus != INVOICESTATUS.Created);
                    if (ListCustomerInvoices != null && ListCustomerInvoices.Count >= 0)
                    {
                        cmbBoxInvoiceNumber.Items.Clear();
                        cmbBoxInvoiceNumber.Items.Add("<New>");
                        cmbBoxInvoiceNumber.Items.AddRange(ListCustomerInvoices.Select(se => se.InvoiceNumber).ToArray());
                        Int32 Index = ListCustomerInvoices.FindIndex(e => e.InvoiceStatus == INVOICESTATUS.Created);
                        if (Index < 0)
                        {
                            cmbBoxInvoiceNumberIndex = 0;
                            CurrentOrderInvoiceID = -1;
                        }
                        else
                        {
                            cmbBoxInvoiceNumberIndex = Index + 1;
                            CurrentOrderInvoiceID = ListCustomerInvoices[Index].InvoiceID;
                            CurrOrderInvoiceDetails.CurrInvoiceDetails = ListCustomerInvoices[Index].Clone();
                            CurrOrderInvoiceDetails.CurrInvoiceDetailsOrig = ListCustomerInvoices[Index].Clone();
                        }
                        cmbBoxInvoiceNumber.SelectedIndex = cmbBoxInvoiceNumberIndex;
                    }
                }

                if (CurrentOrderInvoiceID < 0)
                {
                    txtBoxInvOrdNumber.Text = ObjInvoicesModel.GenerateNewInvoiceNumber();

                    CurrOrderInvoiceDetails.CurrInvoiceDetails = new InvoiceDetails();
                    CurrOrderInvoiceDetails.CurrInvoiceDetails.InvoiceID = -1;
                    CurrOrderInvoiceDetails.CurrInvoiceDetails.CustomerID = CurrCustomerDetails.CustomerID;
                    CurrOrderInvoiceDetails.CurrInvoiceDetails.InvoiceNumber = txtBoxInvOrdNumber.Text;
                    CurrOrderInvoiceDetails.CurrInvoiceDetails.ListInvoiceItems = new List<InvoiceItemDetails>();

                    FormTitle = "Create New Invoice";
                    btnCreateInvOrd.Text = "Create Invoice";
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
                        row[OrdQtyColIndex] = item.OrderQty;
                        row[SaleQtyColIndex] = item.OrderQty;
                        row[ItemSelectionSelectColIndex] = false;

                        Int32 Index = dtGridViewInvOrdProdList.Rows.Add(row);
                        DictItemsSelected.Add(item.ProductName, dtGridViewInvOrdProdList.Rows[Index]);
                    }
                    FormTitle = "Update Invoice";
                    btnCreateInvOrd.Text = "Update Invoice";
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
                    case 2:     //Load Customer Order/Invoice details
                        if (IsCustomerOrder) LoadSalesOrderForCurrCustomer();
                        else if (IsCustomerInvoice) LoadInvoiceForCustomer();
                        break;
                    case 3:     //Update Customer Order/Invoice details
                        if (IsCustomerOrder) UpdateSalesOrder();
                        else if (IsCustomerInvoice) UpdateSalesInvoice();
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
                        if (CurrentOrderInvoiceID < 0)
                        {
                            if (IsCustomerOrder)
                            {
                                MessageBox.Show(this, "Loaded Order data for selected Customer", "Customer Order", MessageBoxButtons.OK);
                            }
                            else if (IsCustomerInvoice)
                            {
                                MessageBox.Show(this, "Loaded Invoice data for selected Customer", "Customer Invoice", MessageBoxButtons.OK);
                            }
                        }
                        break;
                    case 3:     //Update Seller Order for Current Seller or Create Sales Invoice
                        cmbBoxCustomers.SelectedIndex = -1;
                        ResetControls();
                        picBoxLoading.Visible = false;
                        EnableItemsPanel(false);
                        cmbBoxCustomers.Enabled = true;
                        if (IsCustomerOrder)
                        {
                            MessageBox.Show(this, "Created/Updated Sales Order successfully", "Sales Order", MessageBoxButtons.OK);
                            lblStatus.Text = "Choose a Seller to create/update Sales Order";
                        }

                        if (IsCustomerInvoice)
                        {
                            MessageBox.Show(this, "Created Customer Invoice successfully", "Sales Invoice", MessageBoxButtons.OK);
                            lblStatus.Text = "Choose a Customer to create Sales Invoice";
                            cmbBoxInvoiceNumber.Items.Clear();
                            OriginalBalanceAmount = -1;
                            lblBalanceAmountValue.Text = "0.00";
                            txtBoxInvOrdNumber.Text = "";
                        }
                        CurrOrderInvoiceDetails = null;
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
                    Price = Price * ((CommonFunctions.ObjProductMaster.GetTaxRatesForProduct(tmpListProducts[i].ItemName).Sum() + 100) / 100);

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
                //CustomerDetails += "\nBalance: " + sellerDetails.OldBalance.ToString("F");
                lblCustomerDetails.Text = CustomerDetails;
                if (IsCustomerOrder || IsCustomerInvoice || OriginalBalanceAmount < 0)
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

                if (IsCustomerOrder)
                {
                    //Check if the Order is changed before changing Seller
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
                            DialogResult diagResult = MessageBox.Show(this, "All Changes made to this Seller Order will be lost.\nAre you sure to change the Seller?", "Change Seller", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
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
                    CurrentOrderInvoiceID = -1;
                    ResetControls();

                    //Load Selected Seller Order Details
                    String SellerName = cmbBoxCustomers.SelectedValue.ToString().Trim();
                    CurrCustomerDetails = ObjCustomerMasterModel.GetCustomerDetails(SellerName);
                    UpdateCustomerDetails();

                    EditCustomerOrder();
                }
                else if (IsCustomerInvoice)
                {
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
                            DialogResult diagResult = MessageBox.Show(this, "All Changes made to this Seller Order will be lost.\nAre you sure to change the Seller?", "Change Seller", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
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
                    CurrentOrderInvoiceID = -1;
                    cmbBoxInvoiceNumber.Items.Clear();
                    cmbBoxInvoiceNumberIndex = -1;
                    ListCustomerInvoices = null;
                    ResetControls();
                    ObjInvoicesModel.Initialize();

                    EditCustomerOrder();
                }
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

        private void btnHoldOrder_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnHoldOrder_Click()", ex);
            }
        }

        private void dtGridViewInvOrdProdList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.ColumnIndex != OrdQtyColIndex && e.ColumnIndex != SaleQtyColIndex && e.ColumnIndex != PriceColIndex) || e.RowIndex < 0) return;

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
                    if (!String.IsNullOrEmpty(dtGridViewInvOrdProdList.Rows[i].Cells[OrdQtyColIndex].ErrorText)
                        || !String.IsNullOrEmpty(dtGridViewInvOrdProdList.Rows[i].Cells[SaleQtyColIndex].ErrorText)) return false;
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
                cmbBoxCustomers.Enabled = enable;
                cmbBoxProdCat.Enabled = enable;
                cmbBoxProduct.Enabled = enable;
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
                if (dtGridViewInvOrdProdList.Rows.Count > 0)
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
                    txtBoxInvOrdNumber.Text = ObjInvoicesModel.GenerateNewInvoiceNumber();
                    CurrentOrderInvoiceID = -1;

                    OriginalBalanceAmount = -1;
                    //UpdateCustomerDetails();
                    //UpdateSummaryDetails();
                    //EnableItemsPanel(true);
                    EditCustomerOrder();
                }
                else
                {
                    //Load item details from Invoice
                    txtBoxInvOrdNumber.Text = cmbBoxInvoiceNumber.SelectedItem.ToString();
                    CurrentOrderInvoiceID = ListCustomerInvoices.Find(s => s.InvoiceNumber.Equals(txtBoxInvOrdNumber.Text)).InvoiceID;
                    EditCustomerOrder();
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
                EditOldBalanceForm editOldBalanceForm = new EditOldBalanceForm(new CustomerInvoiceSellerOrderForm(IsCustomerOrder, IsCustomerInvoice), Double.Parse(lblBalanceAmountValue.Text));
                editOldBalanceForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnEditBalanceAmount_Click()", ex);
            }
        }

        private void CustomerInvoiceSellerOrderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommonFunctions.WriteToSettingsFile();
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

        DateTime dtTmPckrInvOrdDateValue;
        private void dtTmPckrInvOrdDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtTmPckrInvOrdDateValue == dtTmPckrInvOrdDate.Value) return;

                EnableItemsPanel(false);
                if (dtGridViewInvOrdProdList.Rows.Count > 0)
                {
                    DialogResult diagResult = MessageBox.Show(this, $"All changes made to this {OrderInvoice} will be lost.\nAre you sure to change the Date?", $"Change {OrderInvoice} Date", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (diagResult == DialogResult.No)
                    {
                        dtTmPckrInvOrdDate.Value = dtTmPckrInvOrdDateValue;
                        EnableItemsPanel(true);
                        return;
                    }
                }
                ResetControls();
                dtTmPckrInvOrdDateValue = dtTmPckrInvOrdDate.Value;

                if (IsCustomerOrder)
                {
                    lblStatus.Text = "Choose a Customer to create/update Order";
                }
                else
                {
                    lblStatus.Text = "Choose a Customer to create/update Invoice";
                }
                //dtTmPckrInvOrdDate.Enabled = false;
                cmbBoxCustomers.Enabled = true;
                cmbBoxCustomers.Focus();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dtTmPckrInvOrdDate_ValueChanged()", ex);
            }
        }
    }
}
