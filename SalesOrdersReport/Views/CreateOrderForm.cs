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
    partial class CreateOrderForm : Form
    {
        String FormTitle = "";
        List<ProductDetails> ListAllProducts, ListProducts;
        List<String> ListCustomerNames;
        CustomerDetails CurrCustomerDetails;
        DiscountGroupDetails CurrCustomerDiscountGroup;
        CustomerOrderInvoiceDetails CurrOrderDetails;
        Int32 CategoryColIndex = 0, ItemColIndex = 1, PriceColIndex = 2, QtyColIndex = 3, SelectColIndex = 4, OrdQtyColIndex = 3, SaleQtyColIndex = 4, CommentsColIndex = 4, ItemSelectionSelectColIndex = 5;
        Int32 PaddingSpace = 6;
        Char PaddingChar = CommonFunctions.PaddingChar, CurrencyChar = CommonFunctions.CurrencyChar;
        Int32 BackgroundTask = -1;
        List<Int32> ListSelectedRowIndexesToAdd = new List<Int32>();
        List<Int32> ListSelectedColIndexesToAdd = new List<Int32>();
        Dictionary<String, DataGridViewRow> DictItemsSelected = new Dictionary<String, DataGridViewRow>();
        Boolean ValueChanged = false;
        Double DiscountPerc = 0, DiscountValue = 0;
        List<Int32> ListSelectedRowIndexesToRemove = new List<Int32>();
        Int32 cmbBoxCustomerIndex = -1;
        Boolean LoadCompleted = false;
        Int32 cmbBoxOrderNumberIndex = -1;
        Double OriginalBalanceAmount = -1;
        Int32 CurrentOrderID = -1;

        ProductMasterModel ObjProductMasterModel = CommonFunctions.ObjProductMaster;
        CustomerMasterModel ObjCustomerMasterModel = CommonFunctions.ObjCustomerMasterModel;
        OrdersModel ObjOrdersModel;
        UpdateUsingObjectOnCloseDel UpdateObjectOnClose;
        List<OrderDetails> ListCustomerOrders;

        public CreateOrderForm(Int32 OrderID, UpdateUsingObjectOnCloseDel UpdateObjectOnClose)
        {
            try
            {
                InitializeComponent();
                CommonFunctions.ResetProgressBar();

                ObjOrdersModel = new OrdersModel();
                ObjOrdersModel.Initialize();

                if (OrderID < 0)
                {
                    FormTitle = "Create New Order";
                    btnCreateOrder.Text = "Create Order";
                    txtBoxOrderNumber.Text = ObjOrdersModel.GenerateNewOrderNumber();
                }
                else
                {
                    FormTitle = "View/Edit Order";
                    btnCreateOrder.Text = "Update Order";
                }

                lblSelectName.Text = "Choose Customer";
                lblOrderNumber.Text = "Order#";
                lblOrderDate.Text = "Order Date";

                lblOrderNumber.Visible = true;
                txtBoxOrderNumber.Enabled = true;
                txtBoxOrderNumber.Visible = true;
                txtBoxOrderNumber.ReadOnly = true;
                btnDiscount.Enabled = false;
                lblSelectOrderNum.Enabled = true;
                lblSelectOrderNum.Visible = true;
                cmbBoxOrderNumber.Enabled = true;
                cmbBoxOrderNumber.Visible = true;
                lblStatus.Text = "";

                btnEditBalanceAmount.Enabled = false;
                btnResetBalanceAmount.Enabled = false;
                lblStatus.Text = "Please choose Order date";
                dtGridViewOrdProdList.Columns[OrdQtyColIndex].ReadOnly = true;
                cmbBxOrdersDeliveryLine.Items.Add("Select Delivery Line");
                cmbBxOrdersDeliveryLine.Items.AddRange(CommonFunctions.ObjCustomerMasterModel.GetAllLineNames().ToArray());
                cmbBxOrdersDeliveryLine.SelectedIndex = 0;

                this.Text = FormTitle;
                picBoxLoading.Visible = false;
                dtGridViewProdListForSelection.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dtGridViewOrdProdList.SelectionMode = DataGridViewSelectionMode.CellSelect;
                this.UpdateObjectOnClose = UpdateObjectOnClose;
                CurrentOrderID = OrderID;

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ctor()", ex);
                throw;
            }
        }

        private void CreateOrderForm_Load(object sender, EventArgs e)
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

                CurrOrderDetails = null;
                CurrCustomerDetails = null;
                CurrCustomerDiscountGroup = null;
                ResetControls();

                if (CurrentOrderID > 0)
                {
                    OrderDetails ObjOrderDetails = ObjOrdersModel.GetOrderDetailsForOrderID(CurrentOrderID);
                    
                    //Load Selected Customer Details
                    cmbBoxCustomers.SelectedIndex = cmbBoxCustomers.Items.IndexOf(ObjOrderDetails.CustomerName);
                    cmbBoxCustomerIndex = cmbBoxCustomers.SelectedIndex;
                    CurrCustomerDetails = ObjCustomerMasterModel.GetCustomerDetails(ObjOrderDetails.CustomerID);
                    UpdateCustomerDetails();

                    ListCustomerOrders = ObjOrdersModel.GetOrderDetailsForCustomer(dtTmPckrOrdDate.Value, CurrCustomerDetails.CustomerID);
                    cmbBoxOrderNumber.Items.Clear();
                    cmbBoxOrderNumber.Items.Add("<New>");
                    cmbBoxOrderNumber.Items.AddRange(ListCustomerOrders.Select(s => s.OrderNumber).ToArray());
                    cmbBoxOrderNumber.SelectedIndex = ListCustomerOrders.FindIndex(s => s.OrderID == CurrentOrderID) + 1;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreateOrderForm_Load()", ex);
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

                if (CurrentOrderID < 0)
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

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsDataValidInGridViewOrdProdList())
                {
                    MessageBox.Show(this, "One or multiple errors in selected Item list. Please correct and retry.", "Item list Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Update Order Details to CurrSellerOrderDetails
                if (CurrOrderDetails == null) return;

                if (dtGridViewOrdProdList.Rows.Count == 0)
                {
                    MessageBox.Show(this, "There are no Items in the Order.\nPlease add atleast one Item to the Order.", "Create Order", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }

                Boolean IsValid = false;
                for (int i = 0; i < dtGridViewOrdProdList.Rows.Count; i++)
                {
                    if (Double.Parse(dtGridViewOrdProdList.Rows[i].Cells[OrdQtyColIndex].Value.ToString()) > 0)
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

                CurrOrderDetails.CurrOrderDetails.ListOrderItems.Clear();
                CurrOrderDetails.CurrOrderDetails.OrderItemCount = 0;

                Boolean OrderModified = false;
                foreach (DataGridViewRow item in dtGridViewOrdProdList.Rows)
                {
                    String ItemName = item.Cells[ItemColIndex].Value.ToString();
                    ProductDetails tmpProductDetails = ObjProductMasterModel.GetProductDetails(ItemName);
                    CurrOrderDetails.CurrOrderDetails.OrderItemCount += 1;
                    OrderItemDetails tmpOrderItemDetails = new OrderItemDetails()
                    {
                        ProductID = tmpProductDetails.ProductID,
                        ProductName = ItemName,
                        OrderQty = Double.Parse(item.Cells[SaleQtyColIndex].Value.ToString()),
                        Price = Double.Parse(item.Cells[PriceColIndex].Value.ToString()),
                        OrderItemStatus = ORDERITEMSTATUS.Ordered
                    };
                    CurrOrderDetails.CurrOrderDetails.ListOrderItems.Add(tmpOrderItemDetails);

                    if (!OrderModified && CurrOrderDetails.CurrOrderDetailsOrig != null)
                    {
                        if (CurrOrderDetails.CurrOrderDetailsOrig.ListOrderItems != null)
                        {
                            List<OrderItemDetails> ListOrderItems = CurrOrderDetails.CurrOrderDetailsOrig.ListOrderItems;
                            Int32 Index = ListOrderItems.FindIndex(s => s.ProductID == tmpProductDetails.ProductID);
                            if (Index >= 0 && Math.Abs(ListOrderItems[Index].OrderQty - tmpOrderItemDetails.OrderQty) > 0)
                            {
                                OrderModified = true;
                            }
                        }
                    }
                }
                CurrOrderDetails.CurrOrderDetails.OrderItemCount = CurrOrderDetails.CurrOrderDetails.ListOrderItems.Count;
                CurrOrderDetails.OrderItemCount = CurrOrderDetails.CurrOrderDetails.OrderItemCount;

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
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnCreateOrder_Click()", ex);
            }
        }

        void UpdateSalesOrder()
        {
            try
            {
                OrderDetails AddUpdatedOrderDetails = null;
                if (CurrOrderDetails.CurrOrderDetails.OrderID < 0)
                {
                    AddUpdatedOrderDetails = ObjOrdersModel.CreateNewOrderForCustomer(CurrCustomerDetails.CustomerID, 
                                            dtTmPckrOrdDate.Value, CurrOrderDetails.CurrOrderDetails.OrderNumber, CurrOrderDetails.CurrOrderDetails.DeliveryLineName,
                                            CurrOrderDetails.CurrOrderDetails.ListOrderItems);
                    UpdateObjectOnClose(1, AddUpdatedOrderDetails);
                }
                else
                {
                    AddUpdatedOrderDetails = ObjOrdersModel.UpdateOrderDetails(CurrOrderDetails.CurrOrderDetails);
                    UpdateObjectOnClose(2, AddUpdatedOrderDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateSalesOrder()", ex);
            }
        }

        void LoadSalesOrderForCurrCustomer()
        {
            try
            {
                OrderDetails CurrentOrderDetails = null;
                if (CurrentOrderID > 0)
                {
                    //Load Order details for selected OrderID
                    CurrentOrderDetails = ObjOrdersModel.GetOrderDetailsForOrderID(CurrentOrderID);
                    CurrOrderDetails.CurrOrderDetails = CurrentOrderDetails.Clone();
                    CurrOrderDetails.CurrOrderDetailsOrig = CurrentOrderDetails.Clone();
                    if (CurrentOrderDetails.OrderStatus == ORDERSTATUS.Cancelled)
                    {
                        MessageBox.Show(this, "You have selected a cancelled Order to view/update.", "Load Order", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                }
                else if (ListCustomerOrders != null && cmbBoxOrderNumberIndex == cmbBoxOrderNumber.SelectedIndex)
                {
                    CurrentOrderID = -1;
                }
                else
                {
                    //Create new Order for selected Customer
                    CustomerDetails ObjCustomerDetails = CommonFunctions.ObjCustomerMasterModel.GetCustomerDetails(cmbBoxCustomers.Items[cmbBoxCustomers.SelectedIndex].ToString());
                    ListCustomerOrders = ObjOrdersModel.GetOrderDetailsForCustomer(dtTmPckrOrdDate.Value, ObjCustomerDetails.CustomerID);
                    if (ListCustomerOrders != null && ListCustomerOrders.Count >= 0)
                    {
                        ListCustomerOrders.RemoveAll(e => e.OrderStatus != ORDERSTATUS.Created);
                        cmbBoxOrderNumber.Items.Clear();
                        cmbBoxOrderNumber.Items.Add("<New>");
                        cmbBoxOrderNumber.Items.AddRange(ListCustomerOrders.Select(se => se.OrderNumber).ToArray());
                        Int32 Index = ListCustomerOrders.FindIndex(e => e.OrderStatus == ORDERSTATUS.Created);
                        cmbBoxOrderNumberIndex = 0;
                        CurrentOrderID = -1;
                        cmbBoxOrderNumber.SelectedIndex = cmbBoxOrderNumberIndex;
                    }
                }

                if (CurrentOrderID < 0)
                {
                    txtBoxOrderNumber.Text = this.ObjOrdersModel.GenerateNewOrderNumber();

                    CurrOrderDetails.CurrOrderDetails = new OrderDetails();
                    CurrOrderDetails.CurrOrderDetails.OrderID = -1;
                    CurrOrderDetails.CurrOrderDetails.CustomerID = CurrCustomerDetails.CustomerID;
                    CurrOrderDetails.CurrOrderDetails.OrderNumber = txtBoxOrderNumber.Text;
                    CurrOrderDetails.CurrOrderDetails.ListOrderItems = new List<OrderItemDetails>();
                   if(cmbBxOrdersDeliveryLine.SelectedIndex>0) CurrOrderDetails.CurrOrderDetails.DeliveryLineName = cmbBxOrdersDeliveryLine.SelectedItem.ToString();
                    FormTitle = "Create New Order";
                    btnCreateOrder.Text = "Create New Order";
                }
                else
                {
                    txtBoxOrderNumber.Text = CurrOrderDetails.CurrOrderDetails.OrderNumber;
                    cmbBxOrdersDeliveryLine.SelectedItem = CurrOrderDetails.CurrOrderDetails.DeliveryLineName;
                    if (CurrOrderDetails.CurrOrderDetails.ListOrderItems == null || CurrOrderDetails.CurrOrderDetails.ListOrderItems.Count == 0)
                        ObjOrdersModel.FillOrderItemDetails(CurrOrderDetails.CurrOrderDetails);
                    foreach (var item in CurrOrderDetails.CurrOrderDetails.ListOrderItems)
                    {
                        if (item.OrderQty <= 0) continue;
                        if (item.OrderItemStatus != ORDERITEMSTATUS.Ordered) continue;

                        Object[] row = new Object[7];
                        row[CategoryColIndex] = ObjProductMasterModel.GetProductDetails(item.ProductID).CategoryName;
                        row[ItemColIndex] = item.ProductName;
                        row[PriceColIndex] = item.Price.ToString("F");
                        row[OrdQtyColIndex] = item.OrderQty; row[SaleQtyColIndex] = item.OrderQty; row[ItemSelectionSelectColIndex] = false;
                        row[CommentsColIndex] = item.Comments;
                        Int32 Index = dtGridViewOrdProdList.Rows.Add(row);
                        DictItemsSelected.Add(item.ProductName, dtGridViewOrdProdList.Rows[Index]);
                    }
                    this.FormTitle = "Update Order";
                    btnCreateOrder.Text = "Update Order";
                    txtBoxOrderNumber.Text = CurrOrderDetails.CurrOrderDetails.OrderNumber;
                }
                lblStatus.Text = "Add/Modify Items to Order";

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
                dtGridViewOrdProdList.Rows.Clear();
                dtGridViewProdListForSelection.Rows.Clear();
                ListSelectedRowIndexesToAdd.Clear();
                ListSelectedColIndexesToAdd.Clear();
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
                if (dtGridViewOrdProdList.Rows.Count == 0 && dtGridViewProdListForSelection.Rows.Count == 0) return;

                BackgroundTask = 4;
                DialogResult diagResult = MessageBox.Show(this, "Are you sure to cancel the changes made to Order?", "Cancel Order", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
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
                    DialogResult result = MessageBox.Show(this, "All changes made to this Order will be lost.\nAre you sure to close the window?", "Close Window", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                switch (BackgroundTask)
                {
                    case 2:     //Load Customer Order details
                        LoadSalesOrderForCurrCustomer();
                        break;
                    case 3:     //Update Customer Order details
                        UpdateSalesOrder();
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
                    case 2:     //Load Order for Current Customer
                        EnableItemsPanel(true);
                        picBoxLoading.Visible = false;
                        cmbBoxCustomers.Enabled = true;
                        cmbBoxCustomers.Focus();
                        break;
                    case 3:     //Update Seller Order for Current Seller
                        cmbBoxCustomers.SelectedIndex = -1;
                        ResetControls();
                        picBoxLoading.Visible = false;
                        EnableItemsPanel(false);
                        cmbBoxCustomers.Enabled = true;

                        MessageBox.Show(this, "Created/Updated Order successfully", "Sales Order", MessageBoxButtons.OK);
                        lblStatus.Text = "Choose a Seller to create/update Order";

                        CurrOrderDetails = null;
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
                //Category  Product# Name    Price   Quantity  Comments   Select

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
                                     Price.ToString("F"), 0, " " , false};

                    dtGridViewProdListForSelection.Rows.Add(row);
                }
                ListSelectedRowIndexesToAdd.Clear();
                ListSelectedColIndexesToAdd.Clear();
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
                    Object[] row = new Object[dtGridViewOrdProdList.Columns.Count];
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
                    row[CommentsColIndex] = ListSelectedRows[j].Cells[CommentsColIndex].Value;
                    ListSelectedRows[j].Cells[SelectColIndex].Value = false;
                    ListSelectedRows[j].Cells[QtyColIndex].Value = 0;

                    if (!DictItemsSelected.ContainsKey(row[ItemColIndex].ToString()))
                    {
                        Int32 RowIndex = dtGridViewOrdProdList.Rows.Add(row);
                        DictItemsSelected.Add(row[ItemColIndex].ToString(), dtGridViewOrdProdList.Rows[RowIndex]);
                    }
                    else
                    {
                        DictItemsSelected[row[ItemColIndex].ToString()].Cells[SaleQtyColIndex].Value = Double.Parse(DictItemsSelected[row[ItemColIndex].ToString()].Cells[SaleQtyColIndex].Value.ToString()) + Double.Parse(row[SaleQtyColIndex].ToString());
                    }
                }

                ListSelectedRowIndexesToAdd.Clear();
                ListSelectedColIndexesToAdd.Clear();
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
                if (dtGridViewOrdProdList.Rows.Count == 0) return;

                List<DataGridViewRow> ListSelectedRows = new List<DataGridViewRow>();
                if (ListSelectedRowIndexesToRemove.Count == 0)
                {
                    if (dtGridViewProdListForSelection.SelectedRows.Count > 0)
                        ListSelectedRows.Add(dtGridViewOrdProdList.SelectedRows[0]);
                    else return;
                }
                else
                {
                    foreach (Int32 index in ListSelectedRowIndexesToRemove)
                    {
                        ListSelectedRows.Add(dtGridViewOrdProdList.Rows[index]);
                    }
                }

                for (int j = 0; j < ListSelectedRows.Count; j++)
                {
                    dtGridViewOrdProdList.Rows.Remove(ListSelectedRows[j]);
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

        private void dtGridViewOrdProdList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != ItemSelectionSelectColIndex || e.RowIndex < 0) return;

                dtGridViewOrdProdList.CommitEdit(DataGridViewDataErrorContexts.Commit);

                if (Boolean.Parse(dtGridViewOrdProdList.Rows[e.RowIndex].Cells[ItemSelectionSelectColIndex].Value.ToString()))
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
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewOrdProdList_CellContentClick()", ex);
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
                NumItems = dtGridViewOrdProdList.Rows.Count; Quantity = 0; SubTotal = 0; Discount = 0; TaxAmount = 0; GrandTotal = 0;

                foreach (DataGridViewRow item in dtGridViewOrdProdList.Rows)
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
                    cmbBoxOrderNumberIndex = -1;
                    CurrOrderDetails = null;
                    CurrCustomerDetails = null;
                    CurrCustomerDiscountGroup = null;
                    lblCustomerDetails.Text = "";
                    return;
                }

                //Check if the Order is changed before changing Seller
                Boolean WarnUser = false;
                if (CurrOrderDetails != null)
                {
                    if (cmbBoxCustomers.SelectedIndex == cmbBoxCustomerIndex) return;

                    if (dtGridViewOrdProdList.Rows.Count != CurrOrderDetails.OrderItemCount)
                    {
                        WarnUser = true;
                    }
                    else
                    {
                        foreach (DataGridViewRow item in dtGridViewOrdProdList.Rows)
                        {
                            String ItemName = item.Cells[ItemColIndex].Value.ToString();
                            Double Qty = CurrOrderDetails.CurrOrderDetails.ListOrderItems.Find(s => s.ProductName.Equals(ItemName.ToUpper(), StringComparison.InvariantCulture)).OrderQty;

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
                CurrOrderDetails = null;
                CurrCustomerDetails = null;
                CurrCustomerDiscountGroup = null;
                CurrentOrderID = -1;
                cmbBoxOrderNumber.Items.Clear();
                cmbBoxOrderNumberIndex = -1;
                ListCustomerOrders = null;
                ResetControls();
                ObjOrdersModel.Initialize();

                //Load Selected Customer Details
                CurrCustomerDetails = ObjCustomerMasterModel.GetCustomerDetails(cmbBoxCustomers.SelectedItem.ToString().Trim());
                UpdateCustomerDetails();

                CurrOrderDetails = new CustomerOrderInvoiceDetails();
                CurrOrderDetails.CustomerName = CurrCustomerDetails.CustomerName;

                CurrCustomerDiscountGroup = ObjCustomerMasterModel.GetCustomerDiscount(CurrCustomerDetails.CustomerName);
                if (CurrCustomerDiscountGroup.DiscountType == DiscountTypes.PERCENT)
                    DiscountPerc = CurrCustomerDiscountGroup.Discount;
                else if (CurrCustomerDiscountGroup.DiscountType == DiscountTypes.ABSOLUTE)
                    DiscountValue = CurrCustomerDiscountGroup.Discount;

                picBoxLoading.Visible = true;
                lblStatus.Text = "Loading Customer Order data. Please wait...";
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

        private void dtGridViewOrdProdList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.ColumnIndex != OrdQtyColIndex && e.ColumnIndex != SaleQtyColIndex && e.ColumnIndex != PriceColIndex) || e.RowIndex < 0) return;

                DataGridViewCell cell = dtGridViewOrdProdList.Rows[e.RowIndex].Cells[e.ColumnIndex];
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

                dtGridViewOrdProdList.CommitEdit(DataGridViewDataErrorContexts.Commit);
                UpdateSummaryDetails();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewOrdProdList_CellEndEdit()", ex);
            }
        }

        private Boolean IsDataValidInGridViewOrdProdList()
        {
            try
            {
                for (int i = 0; i < dtGridViewOrdProdList.Rows.Count; i++)
                {
                    if (!String.IsNullOrEmpty(dtGridViewOrdProdList.Rows[i].Cells[OrdQtyColIndex].ErrorText)
                        || !String.IsNullOrEmpty(dtGridViewOrdProdList.Rows[i].Cells[SaleQtyColIndex].ErrorText)) return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.IsDataValidInGridViewOrdProdList()", ex);
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
                ListSelectedColIndexesToAdd.Clear();
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
                if (ListSelectedRowIndexesToRemove.Count != dtGridViewOrdProdList.Rows.Count) Checked = true;
                ListSelectedRowIndexesToRemove.Clear();
                Int32 Index = 0;
                foreach (DataGridViewRow item in dtGridViewOrdProdList.Rows)
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
                cmbBoxOrderNumber.Enabled = enable;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.EnableItemsPanel()", ex);
            }
        }

        private void cmbBoxOrderNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxOrderNumber.SelectedIndex < 0 || cmbBoxOrderNumber.SelectedIndex == cmbBoxOrderNumberIndex)
                    return;

                EnableItemsPanel(false);
                if (dtGridViewOrdProdList.Rows.Count > 0)
                {
                    DialogResult diagResult = MessageBox.Show(this, "All changes made to this Order will be lost.\nAre you sure to change the Invoice?", "Change Order", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (diagResult == DialogResult.No)
                    {
                        cmbBoxOrderNumber.SelectedIndex = cmbBoxOrderNumberIndex;
                        EnableItemsPanel(true);
                        return;
                    }
                }
                ResetControls();
                cmbBoxOrderNumberIndex = cmbBoxOrderNumber.SelectedIndex;

                if (cmbBoxOrderNumberIndex == 0)
                {
                    //Create New Order
                    txtBoxOrderNumber.Text = ObjOrdersModel.GenerateNewOrderNumber();
                    CurrentOrderID = -1;
                    OriginalBalanceAmount = -1;
                }
                else
                {
                    //Load item details from Order
                    txtBoxOrderNumber.Text = cmbBoxOrderNumber.SelectedItem.ToString();
                    CurrentOrderID = ListCustomerOrders.Find(s => s.OrderNumber.Equals(txtBoxOrderNumber.Text)).OrderID;
                }

                CurrOrderDetails = new CustomerOrderInvoiceDetails();
                CurrOrderDetails.CustomerName = CurrCustomerDetails.CustomerName;

                CurrCustomerDiscountGroup = ObjCustomerMasterModel.GetCustomerDiscount(CurrCustomerDetails.CustomerName);
                if (CurrCustomerDiscountGroup.DiscountType == DiscountTypes.PERCENT)
                    DiscountPerc = CurrCustomerDiscountGroup.Discount;
                else if (CurrCustomerDiscountGroup.DiscountType == DiscountTypes.ABSOLUTE)
                    DiscountValue = CurrCustomerDiscountGroup.Discount;

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
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxOrderNumber_SelectedIndexChanged()", ex);
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
                if (e.ColumnIndex < 3 || e.ColumnIndex > 4 || e.RowIndex < 0 || !ValueChanged) return;

                if (!ListSelectedRowIndexesToAdd.Contains(e.RowIndex)) ListSelectedRowIndexesToAdd.Add(e.RowIndex);
                if (!ListSelectedColIndexesToAdd.Contains(e.ColumnIndex)) ListSelectedColIndexesToAdd.Add(e.ColumnIndex);//only column index 3 and 4 shud be added
                if (ListSelectedColIndexesToAdd.Count < 2) return;
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
        private void dtTmPckrOrderDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtTmPckrInvOrdDateValue == dtTmPckrOrdDate.Value) return;

                EnableItemsPanel(false);
                if (dtGridViewOrdProdList.Rows.Count > 0)
                {
                    DialogResult diagResult = MessageBox.Show(this, $"All changes made to this Order will be lost.\nAre you sure to change the Date?", $"Change Order Date", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (diagResult == DialogResult.No)
                    {
                        dtTmPckrOrdDate.Value = dtTmPckrInvOrdDateValue;
                        EnableItemsPanel(true);
                        return;
                    }
                }
                ResetControls();
                dtTmPckrInvOrdDateValue = dtTmPckrOrdDate.Value;

                lblStatus.Text = "Choose a Customer to create/update Order";
                //dtTmPckrInvOrdDate.Enabled = false;
                cmbBoxCustomers.Enabled = true;
                cmbBoxCustomers.Focus();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dtTmPckrOrderDate_ValueChanged()", ex);
            }
        }
    }
}
