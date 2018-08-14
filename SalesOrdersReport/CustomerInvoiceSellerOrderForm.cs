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
        SellerOrderDetails CurrSellerOrderDetails;

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
                }
                else if (IsCustomerInvoice)
                {
                    FormTitle = "Customer Invoice";
                    SelectName = "Select Customer";
                    OrderInvoice = "Invoice";
                    txtBoxInvOrdNumber.Enabled = true;
                }

                this.IsSellerOrder = IsSellerOrder;
                this.IsCustomerInvoice = IsCustomerInvoice;
                this.Text = FormTitle;
                lblInvoiceNumber.Text = OrderInvoice + "#";
                lblInvoiceDate.Text = OrderInvoice + " Date";
                lblSelectName.Text = SelectName;
                btnCreateInvOrd.Text = "Create/Update " + OrderInvoice;
                btnCnclInvOrd.Text = "Cancel/Void " + OrderInvoice;
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
                splitContainer1.Enabled = false;

                //Populate cmbBoxSellerCustomer with Seller Names
                ListSellerNames = CommonFunctions.ObjSellerMaster.GetSellerList();
                cmbBoxSellerCustomer.DataSource = ListSellerNames;
                cmbBoxSellerCustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbBoxSellerCustomer.AutoCompleteSource = AutoCompleteSource.ListItems;

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
                
                splitContainer1.Enabled = true;

#if DEBUG
                //backgroundWorker1_DoWork(null, null);
#else
                ReportProgress = backgroundWorker1.ReportProgress;
                backgroundWorker1.RunWorkerAsync();
                backgroundWorker1.WorkerReportsProgress = true;
#endif
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

        Int32 BackgroundTask = -1;
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

                    foreach (DataGridViewRow item in dtGridViewInvOrdProdList.Rows)
                    {
                        String ItemName = item.Cells[ItemColIndex].Value.ToString();
                        CurrSellerOrderDetails.ListItemQuantity[DictItemToColIndexes[ItemName.ToUpper()]] = Double.Parse(item.Cells[QtyColIndex].Value.ToString());
                    }

                    BackgroundTask = 2;
#if DEBUG
                    backgroundWorker1_DoWork(null, null);
                    backgroundWorker1_RunWorkerCompleted(null, null);
#else
                ReportProgress = backgroundWorker1.ReportProgress;
                backgroundWorker1.RunWorkerAsync(2);
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
                Excel.Workbook xlSalesOrderWorkbook = xlApp.Workbooks.Open(txtSalesOrderFilePath.Text);
                Excel.Worksheet xlSalesOrderWorksheet = CommonFunctions.GetWorksheet(xlSalesOrderWorkbook, dtTmPckrInvOrdDate.Value.ToString("dd-MM-yyyy"));
                Int32 SellerRow = CurrSellerOrderDetails.SellerRowIndex, StartColumn = 1, DetailsCount = 5;

                Int32 ColumnCount = DetailsCount + CurrSellerOrderDetails.ListItemQuantity.Count;
                for (int i = StartColumn + DetailsCount; i <= ColumnCount; i++)
                {
                    if (CurrSellerOrderDetails.ListItemQuantity[i - (StartColumn + DetailsCount)] > 0)
                        xlSalesOrderWorksheet.Cells[SellerRow, i].Value = CurrSellerOrderDetails.ListItemQuantity[i - (StartColumn + DetailsCount)];
                    else
                    {
                        String Value = xlSalesOrderWorksheet.Cells[SellerRow, i].Value.ToString();
                        if (!String.IsNullOrEmpty(Value)) xlSalesOrderWorksheet.Cells[SellerRow, i].Value = 0;
                    }
                }

                xlSalesOrderWorkbook.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.UpdateSalesOrderSheetForCurrSeller()", ex);
            }
            finally
            {
                if (xlApp != null)
                {
                    xlApp.Quit();
                    CommonFunctions.ReleaseCOMObject(xlApp);
                }
            }
        }

        void ResetControls()
        {
            try
            {
                cmbBoxProdCat.SelectedIndex = -1;
                cmbBoxProduct.SelectedIndex = -1;
                cmbBoxSellerCustomer.SelectedIndex = -1;
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

                if (dtGridViewInvOrdProdList.Rows.Count > 0)
                {
                    DialogResult diagResult = MessageBox.Show(this, "Are you sure to cancel the order?\n(This will remove all items from the order and makes it void)", "Void Order", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (diagResult == DialogResult.No) return;
                    dtGridViewProdListForSelection.Rows.Clear();
                }

                ResetControls();
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
                    DialogResult result = MessageBox.Show(this, "All changes made to this order will be lost.\nAre you sure to close the window?", "Cancel Order", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.No) return;
                }

                this.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.btnClose_Click()", ex);
            }
        }

        Excel.Application xlApp;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            xlApp = new Excel.Application();
            try
            {
                switch (BackgroundTask)
                {
                    case 1:
                        LoadSalesOrderSheet();
                        MessageBox.Show(this, "Completed loading of Sales Order data", "Sales Order", MessageBoxButtons.OK);
                        break;
                    case 2:
                        UpdateSalesOrderSheetForCurrSeller();
                        MessageBox.Show(this, "Update Sales Order", "Sales Order", MessageBoxButtons.OK);
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
                    case 1:
                        panelOrderControls.Enabled = true;
                        cmbBoxProdCat.Enabled = true;
                        cmbBoxProduct.Enabled = true;
                        dtTmPckrInvOrdDate.Enabled = false;
                        txtSalesOrderFilePath.Enabled = false;
                        break;
                    case 2:
                        ResetControls();
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
                if (cmbBoxProdCat.SelectedIndex < 0) return;

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
                if (cmbBoxProduct.SelectedIndex < 0) return;

                //Load dtGridViewProdListForSelection based on item selection
                //Category  Product# Name    Price   Quantity    Select

                List<ProductDetails> tmpListProducts = new List<ProductDetails>();
                if (cmbBoxProduct.SelectedIndex == 0)
                    tmpListProducts.AddRange(ListProducts);
                else
                    tmpListProducts.AddRange(ListProducts.Where(s => s.ItemName.Equals(cmbBoxProduct.SelectedItem.ToString(), StringComparison.InvariantCultureIgnoreCase)));

                for (int i = 0; i < tmpListProducts.Count; i++)
                {
                    Object[] row = { tmpListProducts[i].CategoryName, tmpListProducts[i].ItemName,
                                     tmpListProducts[i].SellingPrice.ToString("F"), 0, false};

                    dtGridViewProdListForSelection.Rows.Add(row);
                }
                ListSelectedRowIndexesToAdd.Clear();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.cmbBoxProduct_SelectedIndexChanged()", ex);
            }
        }

        List<Int32> ListSelectedRowIndexesToAdd = new List<Int32>();
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

        Dictionary<String, DataGridViewRow> DictItemsSelected = new Dictionary<String, DataGridViewRow>();
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

        List<Int32> ListSelectedRowIndexesToRemove = new List<Int32>();
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

        Double SubTotal = 0, Quantity = 0, Discount = 0, TaxAmount = 0, GrandTotal = 0;
        Int32 NumItems = 0, ItemColIndex = 1, PriceColIndex = 2, QtyColIndex = 3, SelectColIndex = 4;
        Int32 PaddingSpace = 6;
        Char PaddingChar = ' ', CurrencyChar = '\u20B9';
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

        Int32 cmbBoxSellerCustomerIndex = -1;
        private void cmbBoxSellerCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxSellerCustomer.SelectedIndex < 0) return;

                UpdateCustomerDetails();

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

                    //Load Selected Seller Order Details
                    String SellerName = cmbBoxSellerCustomer.SelectedValue.ToString().Trim();
                    if (!DictSellerToRowIndexes.ContainsKey(SellerName.ToUpper()))
                    {
                        MessageBox.Show(this, "Selected Seller does not exist in Sales Order Sheet\nChoose another Seller or Recreate Sales Order with all Sellers", "Seller Order", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    dtGridViewInvOrdProdList.Rows.Clear();
                    DictItemsSelected.Clear();
                    Int32 SellerIndex = ListSellerOrderDetails.FindIndex(s => s.SellerName.Equals(SellerName, StringComparison.InvariantCultureIgnoreCase));
                    if (SellerIndex < 0)
                    {
                        MessageBox.Show(this, "Selected Seller does not exist in Seller Master\nChoose another Seller or Recreate Sales Order with all Sellers", "Seller Order", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    CurrSellerOrderDetails = ListSellerOrderDetails[SellerIndex];
                    foreach (ProductDetails item in ListAllProducts)
                    {
                        if (!DictItemToColIndexes.ContainsKey(item.ItemName.ToUpper())) continue;
                        Double Qty = CurrSellerOrderDetails.ListItemQuantity[DictItemToColIndexes[item.ItemName.ToUpper()]];
                        if (Qty <= 0) continue;

                        Object[] row = new Object[5];
                        row[0] = item.CategoryName; row[ItemColIndex] = item.ItemName;
                        row[PriceColIndex] = item.SellingPrice;
                        row[QtyColIndex] = Qty; row[SelectColIndex] = false;

                        Int32 Index = dtGridViewInvOrdProdList.Rows.Add(row);
                        DictItemsSelected.Add(item.ItemName, dtGridViewInvOrdProdList.Rows[Index]);
                    }

                    UpdateSummaryDetails();
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

        Double DiscountPerc = 0, DiscountValue = 0;
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
                String FileName = "SalesOrder_" + dtTmPckrInvOrdDate.Value.ToString("dd-MM-yyyy") + ".xlsx";
                txtSalesOrderFilePath.Text = Path.GetDirectoryName(MasterFilePath) + @"\" + FileName;
                UpdateSummaryDetails();

                panelOrderControls.Enabled = false;
                cmbBoxProdCat.Enabled = false;
                cmbBoxProduct.Enabled = false;
                if (!File.Exists(txtSalesOrderFilePath.Text))
                {
                    return;
                }

                panelOrderControls.Enabled = true;
                cmbBoxProdCat.Enabled = true;
                cmbBoxProduct.Enabled = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.CustomerInvoiceSellerOrderForm_Shown()", ex);
            }
        }

        Boolean ValueChanged = false;
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
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.btnSalesOrderFilePath_Click()", ex);
            }
        }

        private void dtTmPckrInvOrdDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                String FileName = "SalesOrder_" + dtTmPckrInvOrdDate.Value.ToString("dd-MM-yyyy") + ".xlsx";
                txtSalesOrderFilePath.Text = Path.GetDirectoryName(MasterFilePath) + @"\" + FileName;

                panelOrderControls.Enabled = false;
                cmbBoxProdCat.Enabled = false;
                cmbBoxProduct.Enabled = false;

                if (!File.Exists(txtSalesOrderFilePath.Text))
                {
                    MessageBox.Show(this, "Sales Order file (" + FileName + ") does not exist."
                                        + "\nPlease create Sales Order File for this Date, before creating any Orders.",
                                        "Sales Order File", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }

                //panelOrderControls.Enabled = true;
                //cmbBoxProdCat.Enabled = true;
                //cmbBoxProduct.Enabled = true;
                //dtTmPckrInvOrdDate.Enabled = false;
                //txtSalesOrderFilePath.Enabled = false;

                BackgroundTask = 1;
#if DEBUG
                backgroundWorker1_DoWork(null, null);
                backgroundWorker1_RunWorkerCompleted(null, null);
#else
                ReportProgress = backgroundWorker1.ReportProgress;
                backgroundWorker1.RunWorkerAsync(1);
                backgroundWorker1.WorkerReportsProgress = true;
#endif
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.dtTmPckrInvOrdDate_ValueChanged()", ex);
            }
        }

        Dictionary<String, Int32> DictItemToColIndexes, DictSellerToRowIndexes;
        List<SellerOrderDetails> ListSellerOrderDetails;
        private void LoadSalesOrderSheet()
        {
            try
            {
                /*Int32 Count = ListProducts.Count + 5;
                String Column = "";
                if (Count / 26 >= 26)
                {
                    Column = ((Char)('A' + ((Count / (26 * 26)) - 1))).ToString();
                    Column += ((Char)('A' + ((Count / 26) - 1))).ToString();
                    Column += ((Char)('A' + ((Count % 26) - 1))).ToString();
                }
                else if (Count >= 26)
                {
                    Column = ((Char)('A' + ((Count / 26) - 1))).ToString();
                    Column += ((Char)('A' + ((Count % 26) - 1))).ToString();
                }
                else
                {
                    Column = ('A' + ((Count % 26) - 1)).ToString();
                }

                DataTable dtSalesOrder = CommonFunctions.ReturnDataTableFromExcelWorksheet(dtTmPckrInvOrdDate.Value.ToString("dd-MM-yyyy"), txtSalesOrderFilePath.Text, "*", "A5:" + Column + "10000");

                return dtSalesOrder;*/

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
                    tmpSellerOrderDetails.ListItemQuantity = new List<Double>();
                    tmpSellerOrderDetails.SellerRowIndex = i;
                    tmpSellerOrderDetails.IsDirty = false;
                    Int32 index = 0;
                    Boolean NoOrder = false;
                    if (xlSalesOrderWorksheet.Cells[i, StartColumn + 1].Value != null)
                        NoOrder = (Double.Parse(xlSalesOrderWorksheet.Cells[i, StartColumn + 1].Value.ToString()) <= 0);
                    else NoOrder = true;

                    foreach (KeyValuePair<String, Int32> item in DictItemToColIndexes)
                    {
                        if (item.Value < 0 || NoOrder)
                        {
                            tmpSellerOrderDetails.ListItemQuantity.Add(0);
                        }
                        else
                        {
                            if (xlSalesOrderWorksheet.Cells[i, StartColumn + DetailsCount + index].Value == null)
                            {
                                tmpSellerOrderDetails.ListItemQuantity.Add(0);
                            }
                            else
                            {
                                String Qty = xlSalesOrderWorksheet.Cells[i, StartColumn + DetailsCount + index].Value.ToString().Trim();
                                if (!String.IsNullOrEmpty(Qty))
                                {
                                    Qty = Qty.Trim();
                                    Double result;
                                    if (Double.TryParse(Qty, out result))
                                        tmpSellerOrderDetails.ListItemQuantity.Add(result);
                                    else
                                    {
                                        MessageBox.Show(this, "Invalid Quantity in Sales Order sheet\nSeller:" + SellerName + ", Item:" + item.Key + ", Quantity:" + Qty + "\nIgnoring quantity for this item",
                                                        "Quantity Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        tmpSellerOrderDetails.ListItemQuantity.Add(0);
                                    }
                                }
                                else
                                {
                                    tmpSellerOrderDetails.ListItemQuantity.Add(0);
                                }
                            }
                        }
                        index++;
                    }

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

        private void btnUpdateSalesOrder_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerInvoiceSellerOrderForm.btnUpdateSalesOrder_Click()", ex);
            }
        }
    }

    class SellerOrderDetails
    {
        public String SellerName;
        public List<Double> ListItemQuantity;
        public Int32 SellerRowIndex;
        public Boolean IsDirty = false;
    }
}
