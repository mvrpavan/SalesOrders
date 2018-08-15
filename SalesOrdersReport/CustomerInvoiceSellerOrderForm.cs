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
        String MasterFilePath, FormTitle = "", SelectName = "", OrderInvoice = "";
        Boolean IsSellerOrder, IsCustomerInvoice;
        List<ProductDetails> ListAllProducts, ListProducts;
        List<String> ListSellerNames;
        SellerDetails CurrSellerDetails;
        DiscountGroupDetails CurrSellerDiscountGroup;
        SellerOrderDetails CurrSellerOrderDetails;
        Dictionary<String, Int32> DictItemToColIndexes, DictSellerToRowIndexes;
        List<SellerOrderDetails> ListSellerOrderDetails;
        Double SubTotal = 0, Quantity = 0, Discount = 0, TaxAmount = 0, GrandTotal = 0;
        Int32 NumItems = 0, CategoryColIndex = 0, ItemColIndex = 1, PriceColIndex = 2, QtyColIndex = 3, SelectColIndex = 4;
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

        public CustomerInvoiceSellerOrderForm(Boolean IsSellerOrder, Boolean IsCustomerInvoice)
        {
            try
            {
                InitializeComponent();
                MasterFilePath = CommonFunctions.MasterFilePath;
                CommonFunctions.ResetProgressBar();

                if (IsSellerOrder)
                {
                    FormTitle = "Seller Order";
                    SelectName = "Select Seller";
                    OrderInvoice = "Order";
                    txtBoxInvOrdNumber.Enabled = false;
                    btnCreateInvOrd.Text = "Create/Update " + OrderInvoice;
                    lblInvOrdFile.Text = "Sales Order File";
                }
                else if (IsCustomerInvoice)
                {
                    FormTitle = "Customer Invoice";
                    SelectName = "Select Customer";
                    OrderInvoice = "Invoice";
                    txtBoxInvOrdNumber.Enabled = true;
                    txtBoxInvOrdNumber.ReadOnly = true;
                    btnCreateInvOrd.Text = "Create " + OrderInvoice;
                    lblInvOrdFile.Text = "Sales Invoice File";
                }

                this.IsSellerOrder = IsSellerOrder;
                this.IsCustomerInvoice = IsCustomerInvoice;
                this.Text = FormTitle;
                lblInvoiceNumber.Text = OrderInvoice + "#";
                lblInvoiceDate.Text = OrderInvoice + " Date";
                lblSelectName.Text = SelectName;
                btnCnclInvOrd.Text = "Cancel/Void " + OrderInvoice;
                txtSalesOrderFilePath.ReadOnly = true;
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
                List<String> ListItems = new List<String>();
                ListItems.Add("<ALL>");
                ListItems.AddRange(ListAllProducts.Select(s => s.ItemName));
                cmbBoxProduct.DataSource = ListItems;
                cmbBoxProduct.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbBoxProduct.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbBoxProduct.SelectedIndex = -1;

                //if (IsCustomerInvoice)
                //{
                //    Int32 InvoiceNumber = CommonFunctions.ObjInvoiceSettings.LastNumber;
                //    InvoiceNumber++;
                //    txtBoxInvOrdNumber.Text = InvoiceNumber.ToString();
                //}
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.CustomerInvoiceForm_Load()", ex);
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
                        backgroundWorker1_RunWorkerCompleted(null, null);
                    }
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

        private void btnCnclInvOrd_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewInvOrdProdList.Rows.Count == 0 && dtGridViewProdListForSelection.Rows.Count == 0) return;

                DialogResult diagResult = MessageBox.Show(this, "Are you sure to cancel the order?\n(This will remove all items from the order and makes it void)", "Void Order", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (diagResult == DialogResult.No) return;

                dtGridViewProdListForSelection.Rows.Clear();
                for (int i = 0; i < CurrSellerOrderDetails.ListItemQuantity.Count; i++)
                {
                    CurrSellerOrderDetails.ListItemQuantity[i] = 0;
                }
                CurrSellerOrderDetails.OrderItemCount = 0;

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
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.btnCnclInvOrd_Click()", ex);
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
                        if (IsCustomerInvoice) LoadSalesInvoiceSheet();
                        break;
                    case 2:     //Load Seller Order details
                        LoadSalesOrderForCurrSeller();
                        break;
                    case 3:     //Update Seller Order details
                        if (IsSellerOrder) UpdateSalesOrderSheetForCurrSeller();
                        if (IsCustomerInvoice) CreateSalesInvoiceForCurrOrder();
                        break;
                    case 4:     //Create default Sales Invoice file
                        CreateDefaultSalesInvoiceSheet();
                        break;
                    default:
                        break;
                }

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
                CommonFunctions.ResetProgressBar();

                switch (BackgroundTask)
                {
                    case 1:     //Load all Sellers
                        EnableItemsPanel(true);
                        if (IsSellerOrder)
                        {
                            dtTmPckrInvOrdDate.Enabled = false;
                            btnBrowseSalesOrderFile.Enabled = false;
                            MessageBox.Show(this, "Completed loading of Sales Order data", "Sales Order", MessageBoxButtons.OK);
                        }

                        if (IsCustomerInvoice)
                        {
                            if (IsSalesInvoiceFileValid)
                            {
                                dtTmPckrInvOrdDate.Enabled = false;
                                btnBrowseSalesOrderFile.Enabled = false;
                                MessageBox.Show(this, "Completed loading of Sales Invoice data", "Sales Invoice", MessageBoxButtons.OK);
                            }
                            else
                                EnableItemsPanel(false);
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

                        UpdateSummaryDetails();
                        EnableItemsPanel(true);
                        MessageBox.Show(this, "Loaded Sales Order data for selected Seller", "Sales Order", MessageBoxButtons.OK);
                        break;
                    case 3:     //Update Seller Order for Current Seller or Create Sales Invoice
                        cmbBoxSellerCustomer.SelectedIndex = -1;
                        ResetControls();
                        if (IsSellerOrder)
                        {
                            EnableItemsPanel(false);
                            cmbBoxSellerCustomer.Enabled = true;
                            MessageBox.Show(this, "Created/Updated Sales Order successfully", "Sales Order", MessageBoxButtons.OK);
                        }

                        if (IsCustomerInvoice)
                        {
                            EnableItemsPanel(true);
                            MessageBox.Show(this, "Created Customer Invoice successfully", "Sales Invoice", MessageBoxButtons.OK);
                        }
                        break;
                    case 4:     //Create default Sales Invoice file
                        EnableItemsPanel(true);
                        dtTmPckrInvOrdDate.Enabled = false;
                        btnBrowseSalesOrderFile.Enabled = false;
                        MessageBox.Show(this, "Created default Sales Invoice file successfully", "Sales Invoice", MessageBoxButtons.OK);
                        break;
                    default:
                        break;
                }
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
                CustomerDetails += "\nBalance: " + sellerDetails.OldBalance.ToString("F");
                lblCustomerDetails.Text = CustomerDetails;
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
                NumItems = 0; Quantity = 0; SubTotal = 0; Discount = 0; TaxAmount = 0; GrandTotal = 0;
                foreach (DataGridViewRow item in dtGridViewInvOrdProdList.Rows)
                {
                    NumItems++;
                    Quantity += Double.Parse(item.Cells[QtyColIndex].Value.ToString());
                    SubTotal += Double.Parse(item.Cells[PriceColIndex].Value.ToString()) * Double.Parse(item.Cells[QtyColIndex].Value.ToString());
                }

                if (DiscountPerc != 0) Discount = (SubTotal * DiscountPerc / 100.0);
                if (DiscountValue != 0) Discount = DiscountValue;
                GrandTotal = SubTotal - Discount;

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
                if (cmbBoxSellerCustomer.SelectedIndex < 0)
                {
                    cmbBoxSellerCustomerIndex = -1;
                    CurrSellerOrderDetails = null;
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

                if (IsCustomerInvoice)
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
                    ResetControls();
                    UpdateCustomerDetails();
                    EnableItemsPanel(true);
                    Int32 InvoiceNumber = CommonFunctions.ObjInvoiceSettings.LastNumber;
                    InvoiceNumber++;
                    txtBoxInvOrdNumber.Text = InvoiceNumber.ToString();
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
                Double DiscountAmount = 0;
                if (objDiscForm.DiscountPerc != 0)
                {
                    DiscountPerc = objDiscForm.DiscountPerc;
                    DiscountAmount = (SubTotal * objDiscForm.DiscountPerc / 100.0);
                }
                if (objDiscForm.DiscountValue != 0)
                {
                    DiscountValue = objDiscForm.DiscountValue;
                    DiscountAmount = objDiscForm.DiscountValue;
                }
                lblDiscount.Text = CurrencyChar + DiscountAmount.ToString("F").PadLeft(PaddingSpace, PaddingChar);
                lblGrandTtl.Text = CurrencyChar + (SubTotal - DiscountAmount).ToString("F").PadLeft(PaddingSpace, PaddingChar);
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
                if (e.ColumnIndex != 3 || e.RowIndex < 0) return;

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
                
                if (IsCustomerInvoice)
                {
                    String FileName = "Invoice_" + dtTmPckrInvOrdDate.Value.ToString("dd-MM-yyyy") + ".xlsx";
                    txtSalesOrderFilePath.Text = Path.GetDirectoryName(MasterFilePath) + @"\" + FileName;
                }
                UpdateSummaryDetails();

                EnableItemsPanel(false);
                if (File.Exists(txtSalesOrderFilePath.Text)) EnableItemsPanel(true);

                LoadCompleted = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.CustomerInvoiceSellerOrderForm_Shown()", ex);
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
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.EnableItemsPanel()", ex);
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

                    if (IsCustomerInvoice)
                    {
                        MessageBox.Show(this, "Sales Invoice file (" + FileName + ") does not exist."
                                            + "\nPlease create Sales Invoice File for this Date, before creating any Invoice.",
                                            "Sales Invoice File", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    }
                    return;
                }
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

        List<String> ListCustomerInvoiceSheetNames = new List<String>();
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

                if (IsCustomerInvoice)
                {
                    String FileName = "Invoice_" + dtTmPckrInvOrdDate.Value.ToString("dd-MM-yyyy") + ".xlsx";
                    txtSalesOrderFilePath.Text = Path.GetDirectoryName(MasterFilePath) + @"\" + FileName;

                    List<String> ListCustomerInvoiceSheetNames = new List<String>();
                    if (!File.Exists(txtSalesOrderFilePath.Text))
                    {
                        DialogResult result = MessageBox.Show(this, "Sales Invoice file (" + FileName + ") does not exist."
                                            + "\nDo you want to create default Sales Invoice File for this Date?",
                                            "Sales Invoice File", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.No)
                        {
                            MessageBox.Show(this, "Please select a date with Sales Invoice file, before you create Customer Invoices.",
                                                "Sales Invoice File", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            return;
                        }

                        //Create an Invoice File with default ItemSummary and Seller Summary
                        BackgroundTask = 4;
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
                        //Get all Sheet Names other than ItemSummary and SellerSummary
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
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.dtTmPckrInvOrdDate_ValueChanged()", ex);
            }
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
                }
                #endregion

                #region Identify Sellers in SalesOrderSheet
                ListSellerOrderDetails = new List<SellerOrderDetails>();
                DictSellerToRowIndexes = new Dictionary<String, Int32>();
                Int32 RowCount = xlSalesOrderWorksheet.UsedRange.Rows.Count + 1;
                for (int i = StartRow + 1; i <= RowCount; i++)
                {
                    if (xlSalesOrderWorksheet.Cells[i, StartColumn + 1].Value == null) continue;
                    if (xlSalesOrderWorksheet.Cells[i, StartColumn + 2].Value == null) continue;
                    String SellerName = xlSalesOrderWorksheet.Cells[i, StartColumn + 2].Value;
                    SellerName = SellerName.Trim();
                    Int32 SellerIndex = ListSellerNames.FindIndex(e => e.Equals(SellerName, StringComparison.InvariantCultureIgnoreCase));
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

        Boolean IsSalesInvoiceFileValid = false;
        void LoadSalesInvoiceSheet()
        {
            try
            {
                Excel.Workbook ObjWorkbook = xlApp.Workbooks.Open(txtSalesOrderFilePath.Text);
                Int32 Count = 0;
                ListCustomerInvoiceSheetNames.Clear();
                for (int i = 1; i <= ObjWorkbook.Sheets.Count; i++)
                {
                    String SheetName = ObjWorkbook.Worksheets[i].Name;
                    if (SheetName.Equals("Item Summary", StringComparison.InvariantCultureIgnoreCase)
                        || SheetName.Equals("Seller Summary", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Count++;
                        continue;
                    }
                    ListCustomerInvoiceSheetNames.Add(SheetName);
                }

                ObjWorkbook.Close(SaveChanges: false);

                if (Count < 2)
                {
                    MessageBox.Show(this, "Invalid Sales Invoice file.\nPlease select Invoice file with Summary Sheets", "Sales Invoice File", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    IsSalesInvoiceFileValid = false;
                }
                else
                {
                    IsSalesInvoiceFileValid = true;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.LoadSalesInvoiceSheet()", ex);
            }
        }

        void CreateDefaultSalesInvoiceSheet()
        {
            try
            {
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

                xlWorkbook.Close(SaveChanges: true, Filename: txtSalesOrderFilePath.Text);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.CreateDefaultSalesInvoiceSheet()", ex);
            }
        }

        void CreateSalesInvoiceForCurrOrder()
        {
            try
            {
                ReportType EnumReportType = ReportType.INVOICE;
                Boolean PrintOldBalance = false;
                ReportSettings CurrReportSettings = null;
                String BillNumberText = "", SaveFileName = "";
                String OutputFolder = Path.GetDirectoryName(txtSalesOrderFilePath.Text);
                String SelectedDateTimeString = dtTmPckrInvOrdDate.Value.ToString("dd-MM-yyyy");
                switch (EnumReportType)
                {
                    case ReportType.INVOICE:
                        CurrReportSettings = CommonFunctions.ObjInvoiceSettings;
                        BillNumberText = "Invoice#";
                        SaveFileName = OutputFolder + "\\Invoice_" + SelectedDateTimeString + ".xlsx";
                        break;
                    case ReportType.QUOTATION:
                        CurrReportSettings = CommonFunctions.ObjQuotationSettings;
                        PrintOldBalance = true;
                        BillNumberText = "Quotation#";
                        SaveFileName = OutputFolder + "\\Quotation_" + SelectedDateTimeString + ".xlsx";
                        break;
                    default:
                        return;
                }
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(SaveFileName);
                Excel.Worksheet xlWorkSheet = xlWorkbook.Worksheets.Add(Type.Missing, xlWorkbook.Sheets[xlWorkbook.Sheets.Count]);

                Int32 InvoiceNumber = Int32.Parse(txtBoxInvOrdNumber.Text);
                Int32 ValidItemCount = dtGridViewInvOrdProdList.Rows.Count;
                Int32 ProgressBarCount = ValidItemCount;
                Int32 Counter = 0, SLNo = 0;
                Double Quantity;

                SLNo = 0;
                SellerDetails ObjCurrentSeller = CommonFunctions.ObjSellerMaster.GetSellerDetails(cmbBoxSellerCustomer.SelectedValue.ToString());
                DiscountGroupDetails ObjDiscountGroup = CommonFunctions.ObjSellerMaster.GetSellerDiscount(ObjCurrentSeller.Name);

                Invoice ObjInvoice = CommonFunctions.GetInvoiceTemplate(EnumReportType);
                ObjInvoice.SerialNumber = InvoiceNumber.ToString();
                ObjInvoice.InvoiceNumberText = BillNumberText;
                ObjInvoice.ObjSellerDetails = ObjCurrentSeller;
                ObjInvoice.CurrReportSettings = CurrReportSettings;
                ObjInvoice.DateOfInvoice = DateTime.Now;
                ObjInvoice.PrintOldBalance = PrintOldBalance;
                ObjInvoice.UseNumberToWordsFormula = false;
                ObjInvoice.ListProducts = new List<ProductDetailsForInvoice>();

                #region Change SheetName
                String SheetName = ObjCurrentSeller.Name.Replace(":", "").Replace("\\", "").Replace("/", "").
                        Replace("?", "").Replace("*", "").Replace("[", "").Replace("]", "");
                SheetName = ((SheetName.Length > 30) ? SheetName.Substring(0, 30) : SheetName);
                Int32 SheetSuffix = 0;
                Boolean ContainsCustomerSheet = false;
                if (ListCustomerInvoiceSheetNames.Count > 0)
                {
                    for (int i = 0; i < ListCustomerInvoiceSheetNames.Count; i++)
                    {
                        if (ListCustomerInvoiceSheetNames[i].Contains(SheetName))
                        {
                            String NumberStr = ListCustomerInvoiceSheetNames[i].Replace(SheetName, "").Trim();
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
                ObjInvoice.SheetName = SheetName;
                ListCustomerInvoiceSheetNames.Add(SheetName);
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
                    ObjProductDetailsForInvoice.Rate = CommonFunctions.ObjProductMaster.GetPriceForProduct(ObjProductDetails.ItemName, ObjCurrentSeller.PriceGroupIndex);

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
                ObjInvoice.CreateInvoice(xlWorkSheet);

                #region Update Seller Summary sheet with this Invoice data
                Excel.Worksheet xlSellerSummaryWorkSheet = CommonFunctions.GetWorksheet(xlWorkbook, "Seller Summary");
                Int32 SummaryStartRow = 2;
                Int32 CurrRow = ListCustomerInvoiceSheetNames.Count + SummaryStartRow;
                Int32 CurrCol = 0;
                CurrCol++; xlSellerSummaryWorkSheet.Cells[CurrRow, CurrCol].Value = ListCustomerInvoiceSheetNames.Count;
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
                xlRangeOldBalance.Value = ObjCurrentSeller.OldBalance;
                xlRangeCash.Formula = "=Round(" + xlRangeNetSale.Address[false, false] + ", 0)";
                xlRangeSale.NumberFormat = "#,##0.00"; xlRangeCancel.NumberFormat = "#,##0.00";
                xlRangeReturn.NumberFormat = "#,##0.00"; xlRangeDiscount.NumberFormat = "#,##0.00";
                xlRangeTotalTax.NumberFormat = "#,##0.00"; xlRangeNetSale.NumberFormat = "#,##0.00";
                xlRangeOldBalance.NumberFormat = "#,##0.00"; xlRangeCash.NumberFormat = "#,##0.00";
                #endregion

                xlApp.DisplayAlerts = true;
                Excel.Worksheet FirstWorksheet = xlWorkbook.Sheets[1];
                FirstWorksheet.Select();

                #region Print Invoice
                DialogResult result = MessageBox.Show(this, "Would you like to print the Invoice?", "Sales Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    xlWorkSheet.PrintOutEx();
                }
                #endregion

                #region Write InvoiceNumber to Settings File
                CurrReportSettings.LastNumber = InvoiceNumber;
                #endregion

                xlWorkbook.Close(SaveChanges: true);
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
