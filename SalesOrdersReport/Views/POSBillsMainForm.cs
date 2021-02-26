using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Models;

namespace SalesOrdersReport.Views
{
    public partial class POSBillsMainForm : Form
    {
        InvoicesModel ObjInvoicesModel;
        DataTable dtAllInvoices;
        String AllInvoicestatus = "<All>";
        INVOICESTATUS CurrInvoiceStatus;
        DateTime FilterFromDate, FilterToDate;
        Boolean IsFormLoaded = false;

        public POSBillsMainForm()
        {
            try
            {
                InitializeComponent();

                CommonFunctions.SetDataGridViewProperties(dtGridViewBills);
                CommonFunctions.SetDataGridViewProperties(dtGridViewBilledProducts);

                ObjInvoicesModel = new InvoicesModel();
                ObjInvoicesModel.Initialize();

                dTimePickerFrom.Value = DateTime.Today.AddDays(-30);
                dTimePickerTo.Value = DateTime.Today.AddDays(30);
                FilterFromDate = DateTime.MinValue;
                FilterToDate = DateTime.MinValue;
                CurrInvoiceStatus = INVOICESTATUS.Created;

                cmbBoxBillStatus.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbBoxBillStatus.Items.Clear();
                cmbBoxBillStatus.Items.Add(AllInvoicestatus);
                cmbBoxBillStatus.Items.Add(INVOICESTATUS.Created.ToString());
                cmbBoxBillStatus.Items.Add(INVOICESTATUS.Delivered.ToString());
                cmbBoxBillStatus.Items.Add(INVOICESTATUS.Paid.ToString());
                cmbBoxBillStatus.Items.Add(INVOICESTATUS.Cancelled.ToString());
                cmbBoxBillStatus.SelectedIndex = 1;

                btnViewEditBill.Enabled = false;

                LoadGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ctor()", ex);
            }
        }

        private void LoadGridView()
        {
            try
            {
                dtAllInvoices = GetInvoicesDataTable(FilterFromDate, FilterToDate, CurrInvoiceStatus, null);
                LoadBillsGridView();

                dtGridViewBilledProducts.DataSource = null;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadGridView()", ex);
            }
        }

        private void POSBillsMainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                this.MaximizeBox = true;
                this.WindowState = FormWindowState.Maximized;
                this.StartPosition = FormStartPosition.CenterScreen;

                IsFormLoaded = true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.POSBillsMainForm_Shown()", ex);
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

        void LoadBillsGridView()
        {
            try
            {
                dtGridViewBills.DataSource = dtAllInvoices.DefaultView;

                for (int i = 0; i < dtGridViewBills.Columns.Count; i++)
                {
                    if (dtGridViewBills.Columns[i].Name.Equals("InvoiceID") || 
                        dtGridViewBills.Columns[i].Name.Equals("OrderID") || dtGridViewBills.Columns[i].Name.Equals("CustomerID"))
                        dtGridViewBills.Columns[i].Visible = false;
                }
                dtGridViewBills.ClearSelection();

                lblOrdersCount.Text = $"[Displaying {dtGridViewBills.Rows.Count} of {dtAllInvoices.Rows.Count} Bills]";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadBillsGridView()", ex);
            }
        }

        void UpdateBillsOnClose(Int32 Mode, Object ObjAddUpdatedDetails = null)
        {
            try
            {
                switch (Mode)
                {
                    case 1:     //Add Invoice
                        break;
                    case 2:
                        break;
                    case 3:     //Reload Invoices
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateBillsOnClose()", ex);
            }
        }

        private void btnCreateBill_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new CreatePOSBillForm(-1, UpdateBillsOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnCreateBill_Click()", ex);
            }
        }

        private void btnViewEditInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewBills.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select an Bill to View", "View Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!dtGridViewBills.SelectedRows[0].Cells["Invoice Status"].Value.ToString().Equals(INVOICESTATUS.Created.ToString()))
                {
                    MessageBox.Show(this, "Unable to view a Delivered/Paid Invoice.", "View Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Int32 InvoiceID = Int32.Parse(dtGridViewBills.SelectedRows[0].Cells["InvoiceID"].Value.ToString());

                CommonFunctions.ShowDialog(new CreatePOSBillForm(InvoiceID, UpdateBillsOnClose), this);

                dtGridViewBills.ClearSelection();
                dtGridViewBilledProducts.DataSource = null;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnViewEditInvoice_Click()", ex);
            }
        }

        private void btnPrintBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewBills.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select Bill to Print", "Print Bill", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                BackgroundTask = 1;
#if DEBUG
                backgroundWorkerBills_DoWork(null, null);
                backgroundWorkerBills_RunWorkerCompleted(null, null);
#else
                ReportProgress = backgroundWorkerBills.ReportProgress;
                backgroundWorkerBills.RunWorkerAsync();
                backgroundWorkerBills.WorkerReportsProgress = true;
#endif
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnPrintBill_Click()", ex);
            }
        }

        private void btnSearchBill_Click(object sender, EventArgs e)
        {
            try
            {
                List<String> ListFindInFields = new List<String>()
                {
                    "Customer Name",
                    "Customer Mobile Number",
                    "Bill Number",
                    "Bill Status",
                    "Bill Date"
                };
                CommonFunctions.ShowDialog(new OrderInvQuotSearchForm(ListFindInFields, null, null), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnSearchBill_Click()", ex);
            }
        }

        private void btnDeleteBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewBills.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select an Invoice to Cancel", "Cancel Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!dtGridViewBills.SelectedRows[0].Cells["Invoice Status"].Value.ToString().Equals(INVOICESTATUS.Created.ToString()))
                {
                    MessageBox.Show(this, "Unable to cancel a Delivered/Paid Invoice.", "View Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Int32 InvoiceID = Int32.Parse(dtGridViewBills.SelectedRows[0].Cells["InvoiceID"].Value.ToString());
                if (ObjInvoicesModel.DeleteInvoiceDetails(InvoiceID) == 0)
                {
                    dtGridViewBilledProducts.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnDeleteBill_Click()", ex);
            }
        }

        private void btnReloadBills_Click(object sender, EventArgs e)
        {
            try
            {
                IsFormLoaded = false;
                CurrInvoiceStatus = INVOICESTATUS.Created;
                FilterFromDate = DateTime.MinValue;
                FilterToDate = DateTime.MinValue;
                checkBoxApplyFilter.Checked = false;
                cmbBoxBillStatus.SelectedIndex = 1;
                IsFormLoaded = true;

                LoadGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnReloadBills_Click()", ex);
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

        private void LoadGridViewBillItems(InvoiceDetails ObjInvoiceDetails)
        {
            try
            {
                dtGridViewBilledProducts.DataSource = null;
                if (ObjInvoiceDetails.ListInvoiceItems == null || ObjInvoiceDetails.ListInvoiceItems.Count == 0) return;

                DataTable dtInvoiceProducts = new DataTable();
                String[] ArrColumns = new String[] { "ProductID", "Product Name", "Sale Qty", "MRP", "Price",
                                                    "Gross Total", "Tax", "Net Total", "Item Status" };
                Type[] ArrColumnTypes = new Type[] { CommonFunctions.TypeInt32, CommonFunctions.TypeString, CommonFunctions.TypeDouble,
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
                dtGridViewBilledProducts.DataSource = dtInvoiceProducts.DefaultView;
                dtGridViewBilledProducts.Columns["ProductID"].Visible = false;
                dtGridViewBilledProducts.ClearSelection();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadGridViewBillItems()", ex);
            }
        }

        Int32 InvoiceIDSelected = -1;
        private void dtGridViewBills_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Int32 InvoiceID = Int32.Parse(dtGridViewBills.SelectedRows[0].Cells["InvoiceID"].Value.ToString());
                if (InvoiceIDSelected == InvoiceID)
                {
                    InvoiceIDSelected = -1;
                    dtGridViewBills.ClearSelection();
                    dtGridViewBilledProducts.DataSource = null;
                    return;
                }

                InvoiceDetails tmpInvoiceDetails = ObjInvoicesModel.FillInvoiceItemDetails(InvoiceID);
                LoadGridViewBillItems(tmpInvoiceDetails);
                InvoiceIDSelected = InvoiceID;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.dtGridViewBills_CellMouseClick()", ex);
            }
        }

        private void cmbBoxBillStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsFormLoaded) return;

                if(cmbBoxBillStatus.SelectedIndex > 0)
                {
                    CurrInvoiceStatus = (INVOICESTATUS)Enum.Parse(Type.GetType("SalesOrdersReport.Models.INVOICESTATUS"), cmbBoxBillStatus.SelectedItem.ToString());
                }
                else
                {
                    CurrInvoiceStatus = INVOICESTATUS.All;
                }
                LoadGridView();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxBillStatus_SelectedIndexChanged()", ex);
            }
        }

        Int32 ExportOption = -1;
        String ExportFolderPath = "";
        private void btnExportBill_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new ExportToExcelForm(ExportDataTypes.Invoices, null, ExportBills), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnExportBill_Click()", ex);
            }
        }

        private Int32 ExportBills(String FilePath, Object ObjDetails, Boolean Append)
        {
            try
            {
                ExportOption = (Int32)ObjDetails;
                ExportFolderPath = FilePath;

                if ((ExportOption & 2) > 0 && dtGridViewBills.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "No Invoice was selected. Please select an Invoice.", "Export Invoice", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return -1;
                }

                if ((ExportOption & 1) > 0)      //Export all displayed Invoices
                {
                    Int32 PendingInvoicesCount = 0, CompletedInvoiceCount = 0, CancelledInvoiceCount = 0;
                    for (int i = 0; i < dtGridViewBills.Rows.Count; i++)
                    {
                        String InvoiceStatus = dtGridViewBills.Rows[i].Cells["Invoice Status"].Value.ToString();
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
                backgroundWorkerBills_DoWork(null, null);
                backgroundWorkerBills_RunWorkerCompleted(null, null);
#else
                ReportProgress = backgroundWorkerBills.ReportProgress;
                backgroundWorkerBills.RunWorkerAsync();
                backgroundWorkerBills.WorkerReportsProgress = true;
#endif
                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ExportBills()", ex);
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

        private void backgroundWorkerBills_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                switch (BackgroundTask)
                {
                    case 1: //Print Invoice
                        {
                            CreatePOSBillForm.PrintBill("BILL00005");
                            //ReportType EnumReportType = ReportType.INVOICE;
                            //Boolean PrintOldBalance = false;
                            //Boolean CreateSummary = false;
                            //Int32 PrintCopies = 1;

                            //Int32 InvoiceID = Int32.Parse(dtGridViewBills.SelectedRows[0].Cells["InvoiceID"].Value.ToString());
                            //InvoiceDetails ObjInvoiceDetails = ObjInvoicesModel.GetInvoiceDetailsForInvoiceID(InvoiceID);
                            //CommonFunctions.PrintOrderInvoiceQuotation(EnumReportType, false, ObjInvoicesModel, new List<Object>() { ObjInvoiceDetails }, ObjInvoiceDetails.InvoiceDate, PrintCopies, CreateSummary, PrintOldBalance, ReportProgressFunc);
                        }
                        break;
                    case 2: //Print Quotation
                        {
                            ReportType EnumReportType = ReportType.QUOTATION;
                            Boolean PrintOldBalance = false;
                            Boolean CreateSummary = false;
                            Int32 PrintCopies = 1;

                            Int32 InvoiceID = Int32.Parse(dtGridViewBills.SelectedRows[0].Cells["InvoiceID"].Value.ToString());
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
                                for (int i = 0; i < dtGridViewBills.Rows.Count; i++)
                                {
                                    Int32 InvoiceID = Int32.Parse(dtGridViewBills.Rows[i].Cells["InvoiceID"].Value.ToString());
                                    InvoiceDetails tmpInvoiceDetails = ObjInvoicesModel.GetInvoiceDetailsForInvoiceID(InvoiceID);
                                    ListInvoicesToExport.Add(tmpInvoiceDetails);
                                }
                            }
                            else if ((ExportOption & 2) > 0) //Export only selected Invoice
                            {
                                Int32 InvoiceID = Int32.Parse(dtGridViewBills.SelectedRows[0].Cells["InvoiceID"].Value.ToString());
                                InvoiceDetails tmpInvoiceDetails = ObjInvoicesModel.GetInvoiceDetailsForInvoiceID(InvoiceID);
                                ListInvoicesToExport.Add(tmpInvoiceDetails);
                            }
                            String ExportedFilePath = CommonFunctions.ExportOrdInvQuotToExcel(EnumReportType, false,
                                        ((InvoiceDetails)ListInvoicesToExport[0]).InvoiceDate, ObjInvoicesModel, ListInvoicesToExport, ExportFolderPath,
                                        CreateSummary, PrintOldBalance, ReportProgressFunc);

                            MessageBox.Show(this, $"Exported Invoices file is created successfully at:{ExportedFilePath}", "Export Invoices", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.backgroundWorkerBills_DoWork()", ex);
            }
        }

        private void backgroundWorkerBills_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                CommonFunctions.UpdateProgressBar(e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.backgroundWorkerBills_ProgressChanged()", ex);
            }
        }

        private void backgroundWorkerBills_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
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
                CommonFunctions.ShowErrorDialog($"{this}.backgroundWorkerBills_RunWorkerCompleted()", ex);
            }
        }
    }
}
