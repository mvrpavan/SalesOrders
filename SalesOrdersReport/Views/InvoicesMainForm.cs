using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Models;

namespace SalesOrdersReport.Views
{
    public partial class InvoicesMainForm : Form
    {
        InvoicesModel ObjInvoicesModel;
        DataTable dtAllInvoices;
        String AllInvoicestatus = "<All>";
        INVOICESTATUS CurrInvoiceStatus;
        DateTime FilterFromDate, FilterToDate;
        Boolean IsFormLoaded = false;
        Int32 OrderIDToEditInvoice;
        List<String> ListPaymentModes;

        public InvoicesMainForm(Int32 OrderIDToEditInvoice = -1)
        {
            try
            {
                InitializeComponent();

                CommonFunctions.SetDataGridViewProperties(dtGridViewInvoices);
                CommonFunctions.SetDataGridViewProperties(dtGridViewInvoicedProducts);

                ObjInvoicesModel = new InvoicesModel();
                ObjInvoicesModel.Initialize();

                //dTimePickerFrom.Value = DateTime.Today.AddDays(-30);
                //dTimePickerTo.Value = DateTime.Today.AddDays(30);
                dTimePickerFrom.Value = FilterFromDate = DateTime.Today;
                dTimePickerTo.Value = FilterToDate = DateTime.Today;
                //FilterFromDate = DateTime.MinValue;
                //FilterToDate = DateTime.MinValue;
                CurrInvoiceStatus = INVOICESTATUS.All;

                cmbBoxInvoiceStatus.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbBoxInvoiceStatus.Items.Clear();
                cmbBoxInvoiceStatus.Items.Add(AllInvoicestatus);
                cmbBoxInvoiceStatus.Items.Add(INVOICESTATUS.Created.ToString());
                cmbBoxInvoiceStatus.Items.Add(INVOICESTATUS.Delivered.ToString());
                cmbBoxInvoiceStatus.Items.Add(INVOICESTATUS.Paid.ToString());
                cmbBoxInvoiceStatus.Items.Add(INVOICESTATUS.Cancelled.ToString());
                cmbBoxInvoiceStatus.SelectedIndex = 1;

                cmbBoxLine.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbBoxLine.Items.Clear();
                cmbBoxLine.Items.Add(AllInvoicestatus);
                cmbBoxLine.Items.AddRange(CommonFunctions.ObjCustomerMasterModel.GetAllLineNames().ToArray());
                cmbBoxLine.SelectedIndex = 0;

                PaymentsModel ObjPaymentsModel = new PaymentsModel();
                ObjPaymentsModel.LoadPaymentModes();
                ListPaymentModes = ObjPaymentsModel.GetPaymentModesList();
                LoadGridView();

                if (OrderIDToEditInvoice > 0)
                {
                    this.OrderIDToEditInvoice = OrderIDToEditInvoice;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("InvoicesMainForm.ctor()", ex);
            }
        }

        private void LoadGridView()
        {
            try
            {
                if (cmbBoxLine.SelectedIndex == 0)
                    dtAllInvoices = GetInvoicesDataTable(FilterFromDate, FilterToDate, CurrInvoiceStatus, null);
                else
                    dtAllInvoices = GetInvoicesDataTable(FilterFromDate, FilterToDate, CurrInvoiceStatus, cmbBoxLine.SelectedItem.ToString());

                LoadInvoicesGridView();

                dtGridViewInvoicedProducts.DataSource = null;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadGridView()", ex);
            }
        }

        private void InvoicesMainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                this.MaximizeBox = true;
                this.WindowState = FormWindowState.Maximized;
                this.StartPosition = FormStartPosition.CenterScreen;

                IsFormLoaded = true;
                if (OrderIDToEditInvoice > 0)
                {
                    for (int i = 0; i < dtGridViewInvoices.Rows.Count; i++)
                    {
                        if (dtGridViewInvoices.Rows[i].Cells["OrderID"].Value.ToString().Equals(OrderIDToEditInvoice.ToString()))
                        {
                            dtGridViewInvoices.Rows[i].Selected = true;
                            break;
                        }
                    }
                    btnViewEditInvoice.PerformClick();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.InvoicesMainForm_Shown()", ex);
            }
        }

        DataTable GetInvoicesDataTable(DateTime FromDate, DateTime ToDate, INVOICESTATUS InvoiceStatus, String LineName)
        {
            try
            {
                DataTable dtInvoices = null;

                if (String.IsNullOrEmpty(LineName))
                    dtInvoices = ObjInvoicesModel.LoadInvoiceDetails(FromDate, ToDate, InvoiceStatus);
                else
                    dtInvoices = ObjInvoicesModel.LoadInvoiceDetails(FromDate, ToDate, InvoiceStatus, "Line", LineName);

                return dtInvoices;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetInvoicesDataTable()", ex);
                return null;
            }
        }

        async void LoadInvoicesGridView()
        {
            try
            {
                await System.Threading.Tasks.Task.Run(() =>
                {
                    dtGridViewInvoices.DataSource = dtAllInvoices.DefaultView;

                    for (int i = 0; i < dtGridViewInvoices.Columns.Count; i++)
                    {
                        if (dtGridViewInvoices.Columns[i].Name.Equals("InvoiceID") ||
                            dtGridViewInvoices.Columns[i].Name.Equals("OrderID") ||
                            dtGridViewInvoices.Columns[i].Name.Equals("CustomerID") ||
                            dtGridViewInvoices.Columns[i].Name.Equals("DeliveryLineID"))
                            dtGridViewInvoices.Columns[i].Visible = false;

                        if (ListPaymentModes.Contains(dtGridViewInvoices.Columns[i].Name))
                        {
                            dtGridViewInvoices.Columns[i].DefaultCellStyle.Format = "F";
                        }
                    }

                    //Add Totals Row at the bottom of Grid
                    dtGridViewInvoiceTotal.Rows.Clear();
                    dtGridViewInvoiceTotal.Columns.Clear();
                    dtGridViewInvoiceTotal.ColumnHeadersVisible = false;
                    CommonFunctions.SetDataGridViewProperties(dtGridViewInvoiceTotal);
                    dtGridViewInvoiceTotal.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    dtGridViewInvoiceTotal.DefaultCellStyle.Font = new System.Drawing.Font(dtGridViewInvoices.Font, System.Drawing.FontStyle.Bold);
                    foreach (DataGridViewColumn item in dtGridViewInvoices.Columns)
                    {
                        DataGridViewColumn newColumn = new DataGridViewColumn(item.CellTemplate);
                        newColumn.Name = item.Name;
                        newColumn.Width = item.Width;
                        newColumn.Visible = item.Visible;
                        dtGridViewInvoiceTotal.Columns.Add(newColumn);
                    }

                    Object[] ArrObjects = dtAllInvoices.NewRow().ItemArray;
                    ArrObjects[dtAllInvoices.Columns["Invoice Number"].Ordinal] = "Total";
                    List<String> ListSumColumns = new List<String>() { "Gross Invoice Amount", "Discount Amount", "Net Invoice Amount" };
                    ListSumColumns.AddRange(ListPaymentModes);
                    for (int i = 0; i < ListSumColumns.Count; i++)
                    {
                        String Value = dtAllInvoices.Compute($"Sum([{ListSumColumns[i]}])", "").ToString();
                        ArrObjects[dtAllInvoices.Columns[ListSumColumns[i]].Ordinal] = (String.IsNullOrEmpty(Value) ? 0.ToString("F") : Double.Parse(Value).ToString("F"));
                    }
                    dtGridViewInvoiceTotal.Rows.Add(ArrObjects);
                });

                lblOrdersCount.Text = $"[Displaying {dtGridViewInvoices.Rows.Count} of {dtAllInvoices.Rows.Count} Invoices]";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadInvoicesGridView()", ex);
            }
        }

        void UpdateInvoicesOnClose(Int32 Mode, Object ObjAddUpdatedDetails = null)
        {
            try
            {
                switch (Mode)
                {
                    case 1:     //Add Invoice
                        btnReloadInvoices.PerformClick();
                        break;
                    case 2:     //Reload Invoices
                        btnReloadInvoices.PerformClick();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateInvoicesOnClose()", ex);
            }
        }

        private void btnCreateInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new CreateInvoiceForm(-1, UpdateInvoicesOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnCreateInvoice_Click()", ex);
            }
        }

        private void btnViewEditInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewInvoices.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select an Invoice to View/Update", "View Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!dtGridViewInvoices.SelectedRows[0].Cells["Invoice Status"].Value.ToString().Equals(INVOICESTATUS.Created.ToString()))
                {
                    MessageBox.Show(this, "Unable to edit a Delivered/Paid Invoice.", "View Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Int32 InvoiceID = Int32.Parse(dtGridViewInvoices.SelectedRows[0].Cells["InvoiceID"].Value.ToString());

                CommonFunctions.ShowDialog(new CreateInvoiceForm(InvoiceID, UpdateInvoicesOnClose), this);

                dtGridViewInvoices.ClearSelection();
                dtGridViewInvoicedProducts.DataSource = null;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnViewEditInvoice_Click()", ex);
            }
        }

        private void btnPrintInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewInvoices.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select an Invoice to Print", "Print Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                BackgroundTask = 1;
#if DEBUG
                backgroundWorkerInvoices_DoWork(null, null);
                backgroundWorkerInvoices_RunWorkerCompleted(null, null);
#else
                ReportProgress = backgroundWorkerInvoices.ReportProgress;
                backgroundWorkerInvoices.RunWorkerAsync();
                backgroundWorkerInvoices.WorkerReportsProgress = true;
#endif
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnPrintInvoice_Click()", ex);
            }
        }

        private void btnPrintQuotation_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewInvoices.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select an Invoice to Print", "Print Quotation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                BackgroundTask = 2;
#if DEBUG
                backgroundWorkerInvoices_DoWork(null, null);
                backgroundWorkerInvoices_RunWorkerCompleted(null, null);
#else
                ReportProgress = backgroundWorkerInvoices.ReportProgress;
                backgroundWorkerInvoices.RunWorkerAsync();
                backgroundWorkerInvoices.WorkerReportsProgress = true;
#endif
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnPrintQuotation_Click()", ex);
            }
        }

        private void btnSearchInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                List<String> ListFindInFields = new List<String>()
                {
                    "Customer Name",
                    "Invoice Number",
                    "Invoice Status",
                    "Invoice Date"
                };
                CommonFunctions.ShowDialog(new OrderInvQuotSearchForm(ListFindInFields, null, null), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnSearchInvoice_Click()", ex);
            }
        }

        private void btnDeleteInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewInvoices.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select an Invoice to Cancel", "Cancel Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!dtGridViewInvoices.SelectedRows[0].Cells["Invoice Status"].Value.ToString().Equals(INVOICESTATUS.Created.ToString()))
                {
                    MessageBox.Show(this, "Unable to cancel a Delivered/Paid Invoice.", "View Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Int32 InvoiceID = Int32.Parse(dtGridViewInvoices.SelectedRows[0].Cells["InvoiceID"].Value.ToString());
                if (ObjInvoicesModel.DeleteInvoiceDetails(InvoiceID) == 0)
                {
                    dtGridViewInvoicedProducts.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnDeleteInvoice_Click()", ex);
            }
        }

        private void btnReloadInvoices_Click(object sender, EventArgs e)
        {
            try
            {
                //IsFormLoaded = false;
                //CurrInvoiceStatus = INVOICESTATUS.Created;
                //FilterFromDate = DateTime.MinValue;
                //FilterToDate = DateTime.MinValue;
                //checkBoxApplyFilter.Checked = false;
                //cmbBoxInvoiceStatus.SelectedIndex = 1;
                //cmbBoxLine.SelectedIndex = 0;
                //IsFormLoaded = true;

                LoadGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnReloadInvoices_Click()", ex);
            }
        }

        private void checkBoxApplyFilter_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!checkBoxApplyFilter.Checked)
                {
                    FilterFromDate = DateTime.MinValue;
                    FilterToDate = DateTime.MinValue;
                }
                else
                {
                    FilterFromDate = dTimePickerFrom.Value;
                    FilterToDate = dTimePickerTo.Value;
                }

                LoadGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.checkBoxApplyFilter_CheckedChanged()", ex);
            }
        }

        private void LoadGridViewInvoiceItems(InvoiceDetails ObjInvoiceDetails)
        {
            try
            {
                dtGridViewInvoicedProducts.DataSource = null;
                if (ObjInvoiceDetails.ListInvoiceItems == null || ObjInvoiceDetails.ListInvoiceItems.Count == 0) return;

                DataTable dtInvoiceProducts = new DataTable();
                String[] ArrColumns = new String[] { "ProductID", "Product Name", "Ordered Qty", "Sale Qty", "Price",
                                                    "Gross Total", "Tax", "Net Total", "Invoice Item Status" };
                Type[] ArrColumnTypes = new Type[] { CommonFunctions.TypeInt32, CommonFunctions.TypeString, CommonFunctions.TypeString,
                                                    CommonFunctions.TypeDouble, CommonFunctions.TypeDouble, CommonFunctions.TypeDouble,
                                                    CommonFunctions.TypeDouble, CommonFunctions.TypeDouble, CommonFunctions.TypeString };

                for (int i = 0; i < ArrColumns.Length; i++)
                {
                    dtInvoiceProducts.Columns.Add(ArrColumns[i], ArrColumnTypes[i]);
                }

                for (int i = 0; i < ObjInvoiceDetails.ListInvoiceItems.Count; i++)
                {
                    InvoiceItemDetails tmpInvoiceItem = ObjInvoiceDetails.ListInvoiceItems[i];
                    Object[] ArrObjects = new Object[]
                    {
                        tmpInvoiceItem.ProductID,
                        tmpInvoiceItem.ProductName,
                        tmpInvoiceItem.OrderQty,
                        tmpInvoiceItem.SaleQty,
                        tmpInvoiceItem.Price,
                        tmpInvoiceItem.TaxableValue,
                        tmpInvoiceItem.CGST + tmpInvoiceItem.SGST + tmpInvoiceItem.IGST,
                        tmpInvoiceItem.NetTotal,
                        tmpInvoiceItem.InvoiceItemStatus
                    };
                    dtInvoiceProducts.Rows.Add(ArrObjects);
                }

                dtInvoiceProducts.DefaultView.Sort = "Product Name";
                dtGridViewInvoicedProducts.DataSource = dtInvoiceProducts.DefaultView;
                dtGridViewInvoicedProducts.Columns["ProductID"].Visible = false;
                dtGridViewInvoicedProducts.ClearSelection();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadGridViewInvoiceItems()", ex);
            }
        }

        Int32 InvoiceIDSelected = -1;
        private void dtGridViewInvoices_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Int32 InvoiceID = Int32.Parse(dtGridViewInvoices.SelectedRows[0].Cells["InvoiceID"].Value.ToString());
                if (InvoiceIDSelected == InvoiceID)
                {
                    InvoiceIDSelected = -1;
                    dtGridViewInvoices.ClearSelection();
                    dtGridViewInvoicedProducts.DataSource = null;
                    return;
                }

                InvoiceDetails tmpInvoiceDetails = ObjInvoicesModel.FillInvoiceItemDetails(InvoiceID);
                LoadGridViewInvoiceItems(tmpInvoiceDetails);
                InvoiceIDSelected = InvoiceID;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewInvoices_CellMouseClick()", ex);
            }
        }

        private void cmbBoxInvoiceStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsFormLoaded) return;

                if(cmbBoxInvoiceStatus.SelectedIndex > 0)
                {
                    CurrInvoiceStatus = (INVOICESTATUS)Enum.Parse(Type.GetType("SalesOrdersReport.Models.INVOICESTATUS"), cmbBoxInvoiceStatus.SelectedItem.ToString());
                }
                else
                {
                    CurrInvoiceStatus = INVOICESTATUS.All;
                }
                LoadGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxInvoiceStatus_SelectedIndexChanged()", ex);
            }
        }

        private void cmbBoxLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsFormLoaded) return;

                LoadGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxLine_SelectedIndexChanged()", ex);
            }
        }

        Int32 ExportOption = -1;
        String ExportFolderPath = "";
        private void btnExportInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new ExportToExcelForm(ExportDataTypes.Invoices, null, ExportInvoices), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnExportInvoice_Click()", ex);
            }
        }

        private Int32 ExportInvoices(String FilePath, Object ObjDetails, Boolean Append)
        {
            try
            {
                ExportOption = (Int32)ObjDetails;
                ExportFolderPath = FilePath;

                if ((ExportOption & 2) > 0 && dtGridViewInvoices.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "No Invoice was selected. Please select an Invoice.", "Export Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return -1;
                }

                if ((ExportOption & 1) > 0)      //Export all displayed Invoices
                {
                    Int32 PendingInvoicesCount = 0, CompletedInvoiceCount = 0, CancelledInvoiceCount = 0;
                    for (int i = 0; i < dtGridViewInvoices.Rows.Count; i++)
                    {
                        String InvoiceStatus = dtGridViewInvoices.Rows[i].Cells["Invoice Status"].Value.ToString();
                        if (InvoiceStatus.Equals(INVOICESTATUS.Created)) PendingInvoicesCount++;
                        else if (InvoiceStatus.Equals(INVOICESTATUS.Cancelled)) CancelledInvoiceCount++;
                        else CompletedInvoiceCount++;
                    }

                    if ((PendingInvoicesCount > 0 && (CompletedInvoiceCount > 0 || CancelledInvoiceCount > 0))
                        || (CompletedInvoiceCount > 0 && CancelledInvoiceCount > 0))
                    {
                        MessageBox.Show(this, "Unable to export Invoices with multiple Invoice Status. Please filter Invoices with same status.", "Export Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        return 2;
                    }
                }

                BackgroundTask = 3;
#if DEBUG
                backgroundWorkerInvoices_DoWork(null, null);
                backgroundWorkerInvoices_RunWorkerCompleted(null, null);
#else
                ReportProgress = backgroundWorkerInvoices.ReportProgress;
                backgroundWorkerInvoices.RunWorkerAsync();
                backgroundWorkerInvoices.WorkerReportsProgress = true;
#endif
                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ExportInvoices()", ex);
                return -1;
            }
        }

        private Int32 ExportQuotation(String FilePath, Object ObjDetails, Boolean Append)
        {
            try
            {
                ExportOption = (Int32)ObjDetails;
                ExportFolderPath = FilePath;

                if ((ExportOption & 2) > 0 && dtGridViewInvoices.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "No Invoice was selected. Please select an Invoice.", "Export Qutotaion", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return -1;
                }

                if ((ExportOption & 1) > 0)      //Export all displayed Invoices
                {
                    Int32 PendingInvoicesCount = 0, CompletedInvoiceCount = 0, CancelledInvoiceCount = 0;
                    for (int i = 0; i < dtGridViewInvoices.Rows.Count; i++)
                    {
                        String InvoiceStatus = dtGridViewInvoices.Rows[i].Cells["Invoice Status"].Value.ToString();
                        if (InvoiceStatus.Equals(INVOICESTATUS.Created)) PendingInvoicesCount++;
                        else if (InvoiceStatus.Equals(INVOICESTATUS.Cancelled)) CancelledInvoiceCount++;
                        else CompletedInvoiceCount++;
                    }

                    if ((PendingInvoicesCount > 0 && (CompletedInvoiceCount > 0 || CancelledInvoiceCount > 0))
                        || (CompletedInvoiceCount > 0 && CancelledInvoiceCount > 0))
                    {
                        MessageBox.Show(this, "Unable to export Quotation with multiple Invoice Status. Please filter Invoices with same status.", "Export Quotation", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        return 2;
                    }
                }

                BackgroundTask = 5;
#if DEBUG
                backgroundWorkerInvoices_DoWork(null, null);
                backgroundWorkerInvoices_RunWorkerCompleted(null, null);
#else
                ReportProgress = backgroundWorkerInvoices.ReportProgress;
                backgroundWorkerInvoices.RunWorkerAsync();
                backgroundWorkerInvoices.WorkerReportsProgress = true;
#endif
                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ExportInvoices()", ex);
                return -1;
            }
        }


        Int32 BackgroundTask = 0;
        ReportProgressDel ReportProgress = null;

        private void ReportProgressFunc(Int32 ProgressState)
        {
            if (ReportProgress == null) return;
            ReportProgress(ProgressState);
        }

        private void backgroundWorkerInvoices_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                switch (BackgroundTask)
                {
                    case 1: //Print Invoice
                        {
                            ReportType EnumReportType = ReportType.INVOICE;
                            Boolean PrintOldBalance = false;
                            Boolean CreateSummary = false;
                            Int32 PrintCopies = 1;

                            Int32 InvoiceID = Int32.Parse(dtGridViewInvoices.SelectedRows[0].Cells["InvoiceID"].Value.ToString());
                            InvoiceDetails ObjInvoiceDetails = ObjInvoicesModel.GetInvoiceDetailsForInvoiceID(InvoiceID);
                            CommonFunctions.PrintOrderInvoiceQuotation(EnumReportType, false, ObjInvoicesModel, new List<Object>() { ObjInvoiceDetails }, ObjInvoiceDetails.InvoiceDate, PrintCopies, CreateSummary, PrintOldBalance, ReportProgressFunc);
                        }
                        break;
                    case 2: //Print Quotation
                        {
                            ReportType EnumReportType = ReportType.QUOTATION;
                            Boolean PrintOldBalance = false;
                            Boolean CreateSummary = false;
                            Int32 PrintCopies = 1;

                            Int32 InvoiceID = Int32.Parse(dtGridViewInvoices.SelectedRows[0].Cells["InvoiceID"].Value.ToString());
                            InvoiceDetails ObjInvoiceDetails = ObjInvoicesModel.GetInvoiceDetailsForInvoiceID(InvoiceID);
                            CommonFunctions.PrintOrderInvoiceQuotation(EnumReportType, false, ObjInvoicesModel, new List<Object>() { ObjInvoiceDetails }, ObjInvoiceDetails.InvoiceDate, PrintCopies, CreateSummary, PrintOldBalance, ReportProgressFunc);
                        }
                        break;
                    case 3: //Export Invoices
                        {
                            ReportType EnumReportType = ReportType.INVOICE;
                            Boolean PrintOldBalance = true;
                            Boolean CreateSummary = (CommonFunctions.ObjGeneralSettings.SummaryLocation == 0) || ((ExportOption & 4) > 0);

                            List<Object> ListInvoicesToExport = new List<Object>();
                            if ((ExportOption & 1) > 0)      //Export all displayed Invoices
                            {
                                for (int i = 0; i < dtGridViewInvoices.Rows.Count; i++)
                                {
                                    Int32 InvoiceID = Int32.Parse(dtGridViewInvoices.Rows[i].Cells["InvoiceID"].Value.ToString());
                                    InvoiceDetails tmpInvoiceDetails = ObjInvoicesModel.GetInvoiceDetailsForInvoiceID(InvoiceID);
                                    ListInvoicesToExport.Add(tmpInvoiceDetails);
                                }
                            }
                            else if ((ExportOption & 2) > 0) //Export only selected Invoice
                            {
                                Int32 InvoiceID = Int32.Parse(dtGridViewInvoices.SelectedRows[0].Cells["InvoiceID"].Value.ToString());
                                InvoiceDetails tmpInvoiceDetails = ObjInvoicesModel.GetInvoiceDetailsForInvoiceID(InvoiceID);
                                ListInvoicesToExport.Add(tmpInvoiceDetails);
                            }
                            String ExportedFilePath = CommonFunctions.ExportOrdInvQuotToExcel(EnumReportType, false,
                                        ((InvoiceDetails)ListInvoicesToExport[0]).InvoiceDate, ObjInvoicesModel, ListInvoicesToExport, ExportFolderPath,
                                        CreateSummary, PrintOldBalance, ReportProgressFunc,true);

                            MessageBox.Show(this, $"Exported Invoices file is created successfully at:{ExportedFilePath}", "Export Invoices", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case 4: //Mark Invoices as Delivered
                        MarkInvoicesAsDelivered();
                        break;
                    case 5: //Export Quotation
                        {
                            ReportType EnumReportType = ReportType.QUOTATION;
                            Boolean PrintOldBalance = true;
                            Boolean CreateSummary = (CommonFunctions.ObjGeneralSettings.SummaryLocation == 0) || ((ExportOption & 4) > 0);

                            List<Object> ListInvoicesToExport = new List<Object>();
                            if ((ExportOption & 1) > 0)      //Export all displayed Invoices
                            {
                                for (int i = 0; i < dtGridViewInvoices.Rows.Count; i++)
                                {
                                    Int32 InvoiceID = Int32.Parse(dtGridViewInvoices.Rows[i].Cells["InvoiceID"].Value.ToString());
                                    InvoiceDetails tmpInvoiceDetails = ObjInvoicesModel.GetInvoiceDetailsForInvoiceID(InvoiceID);
                                    ListInvoicesToExport.Add(tmpInvoiceDetails);
                                }
                            }
                            else if ((ExportOption & 2) > 0) //Export only selected Invoice
                            {
                                Int32 InvoiceID = Int32.Parse(dtGridViewInvoices.SelectedRows[0].Cells["InvoiceID"].Value.ToString());
                                InvoiceDetails tmpInvoiceDetails = ObjInvoicesModel.GetInvoiceDetailsForInvoiceID(InvoiceID);
                                ListInvoicesToExport.Add(tmpInvoiceDetails);
                            }
                            String ExportedFilePath = CommonFunctions.ExportOrdInvQuotToExcel(EnumReportType, false,
                                        ((InvoiceDetails)ListInvoicesToExport[0]).InvoiceDate, ObjInvoicesModel, ListInvoicesToExport, ExportFolderPath,
                                        CreateSummary, PrintOldBalance, ReportProgressFunc, true);

                            MessageBox.Show(this, $"Exported Quotation file is created successfully at:{ExportedFilePath}", "Export Quotation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.backgroundWorkerInvoices_DoWork()", ex);
            }
        }

        private void backgroundWorkerInvoices_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                CommonFunctions.UpdateProgressBar(e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.backgroundWorkerInvoices_ProgressChanged()", ex);
            }
        }

        private void backgroundWorkerInvoices_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                switch (BackgroundTask)
                {
                    case 1: //Print Invoice
                    case 2: //Print Quotation
                        break;
                    case 3:
                        ExportOption = -1; ExportFolderPath = "";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.backgroundWorkerInvoices_RunWorkerCompleted()", ex);
            }
        }

        private void MarkInvoicesAsDelivered()
        {
            try
            {
                List<Int32> ListInvoiceIDsToUpdate = new List<Int32>();

                DialogResult dialogResult = MessageBox.Show(this, "Do you want to update all Invoices or only selected Invoice as Delivered?\nYes: All Invoices\nNo: Only Selected Invoice",
                                        "Deliver Invoice", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
                if (dialogResult == DialogResult.Cancel) return;
                else if (dialogResult == DialogResult.Yes)
                {
                    dialogResult = MessageBox.Show(this, "This will update all displayed Invoices as Delivered. Please confirm.",
                                            "Deliver Invoice", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    if (dialogResult == DialogResult.Cancel) return;

                    for (int i = 0; i < dtGridViewInvoices.Rows.Count; i++)
                    {
                        Int32 InvoiceID = Int32.Parse(dtGridViewInvoices.Rows[i].Cells["InvoiceID"].Value.ToString());
                        if (ObjInvoicesModel.GetInvoiceDetailsForInvoiceID(InvoiceID).InvoiceStatus == INVOICESTATUS.Created)
                            ListInvoiceIDsToUpdate.Add(InvoiceID);
                    }
                }
                else
                {
                    if (dtGridViewInvoices.SelectedRows.Count == 0)
                    {
                        MessageBox.Show(this, "Please select an Invoice to Mark as Deilvered", "Deliver Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    dialogResult = MessageBox.Show(this, "This will update selected Invoice as Delivered. Please confirm.",
                            "Deliver Invoice", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    if (dialogResult == DialogResult.Cancel) return;

                    Int32 InvoiceID = Int32.Parse(dtGridViewInvoices.SelectedRows[0].Cells["InvoiceID"].Value.ToString());
                    if (ObjInvoicesModel.GetInvoiceDetailsForInvoiceID(InvoiceID).InvoiceStatus == INVOICESTATUS.Created)
                        ListInvoiceIDsToUpdate.Add(InvoiceID);
                }

                if (ListInvoiceIDsToUpdate.Count == 0)
                {
                    MessageBox.Show(this, "There are no Invoices to update as Delivered.", "Deliver Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Int32 RetVal = ObjInvoicesModel.MarkInvoicesAsDelivered(ListInvoiceIDsToUpdate);

                if (RetVal == 0)
                {
                    MessageBox.Show(this, "All displayed/selected Invoices are updated as Delivered successfully", "Deliver Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (RetVal == -1)
                {
                    MessageBox.Show(this, "All displayed/selected Invoices are updated as Delivered failed with unknown error", "Deliver Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.MarkInvoicesAsDelivered()", ex);
            }
        }

        private void dtGridViewInvoices_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                    dtGridViewInvoiceTotal.HorizontalScrollingOffset = e.NewValue;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewInvoices_Scroll()", ex);
            }
        }

        private void btnExportQuotation_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new ExportToExcelForm(ExportDataTypes.Invoices, null, ExportQuotation), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnExportQuotation_Click()", ex);
            }
        }

        private void btnMarkAsDelivered_Click(object sender, EventArgs e)
        {
            try
            {
                BackgroundTask = 4;
#if DEBUG
                backgroundWorkerInvoices_DoWork(null, null);
                backgroundWorkerInvoices_RunWorkerCompleted(null, null);
#else
                ReportProgress = backgroundWorkerInvoices.ReportProgress;
                backgroundWorkerInvoices.RunWorkerAsync();
                backgroundWorkerInvoices.WorkerReportsProgress = true;
#endif
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnMarkAsDelivered_Click()", ex);
            }
        }
    }
}
