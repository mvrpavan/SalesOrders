using SalesOrdersReport.CommonModules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesOrdersReport.Models;
using SalesOrdersReport.CommonModules;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace SalesOrdersReport.Views
{
    public partial class ReportsMainForm : Form
    {
        ReportModel ObjReportModel = null;
        int CountOfParamsAdded = 0;
        string CurrentParamValue = "";
        MySQLHelper ObjMySQLHelper;
        public ReportsMainForm()
        {
            InitializeComponent();
            ObjReportModel = new ReportModel();
            ObjMySQLHelper = MySQLHelper.GetMySqlHelperObj();
        }

        private void ReportsMainForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadReports();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUsersForm.UpdateReportOnClose()", ex);
            }
        }

        public void LoadReports()
        {
            try
            {
                ObjReportModel.LoadDefinedParamsDataTable();
                ObjReportModel.LoadReportsDataTable();
                FillReportNames();
                txtBoxReportDesc.Text = "";
                txtBoxParamValue.Text = "";
                //cmbBoxParamNames.Items.Add("Select Param Name");
                //cmbBoxParamNames.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUsersForm.LoadReports()", ex);
            }
        }

        public void FillReportNames()
        {
            try
            {
                cmbBoxReportName.Items.Clear();
                cmbBoxReportName.Items.Add("Select Report");
                List<string> ListReports = ObjReportModel.GetAllReportNames();
                if (ListReports != null)
                {
                    foreach (var item in ListReports)
                    {
                        cmbBoxReportName.Items.Add(item);
                    }
                }
                cmbBoxReportName.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUsersForm.FillReportNames()", ex);
            }
        }

        private void btnAddReport_Click(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.ShowDialog(new CreateReportForm(UpdateReportOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnAddReport_Click()", ex);
                throw;
            }
        }

        private void btnViewEditReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxReportName.SelectedIndex == 0)
                {
                    MessageBox.Show(this, "Please select a Report to Edit", "Edit Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                ReportDetails ObjReportDetails = ObjReportModel.GetReportDetailsFromName(cmbBoxReportName.Text.ToString());
                CommonFunctions.ShowDialog(new CreateReportForm(UpdateReportOnClose, false, ObjReportDetails), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnViewEditReport_Click()", ex);
                throw;
            }
        }

        private void btnDeleteReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxReportName.SelectedIndex == 0)
                {
                    MessageBox.Show(this, "Please select a Report to Delete", "Delete Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                Int32 ReportID = ObjReportModel.GetReportDetailsFromName(cmbBoxParamNames.SelectedItem.ToString()).ReportID;
                var Result = MessageBox.Show("Are sure to set the Report to Inactive State? ", "InActive Report", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //If the no button was pressed ...
                if (Result == DialogResult.No) return;
                if (ObjReportModel.DeleteReportDetails(ReportID) == 0)   //&&&&& How to show deleted Report
                {
                    UpdateReportOnClose(1);
                }
                
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnDeleteReport_Click()", ex);
                throw;
            }
        }

        private void btnReloadReports_Click(object sender, EventArgs e)
        {
            try
            {
                LoadReports();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnReloadReports_Click()", ex);
                throw;
            }
        }

        private void btnPrintReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewReportData.Rows.Count == 0)
                {
                    MessageBox.Show(this, "Please Generate the Report to Print.", "Print Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DialogResult dialogResult = MessageBox.Show(this, "Do you want to Print Report of "+ dtGridViewReportData.Rows.Count + " rows ?", "Print Report", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (dialogResult == DialogResult.No)
                {
                    return;
                }
                    BackgroundTask = 1;
#if DEBUG

                backgroundWorkerReports_DoWork(null, null);
                backgroundWorkerReports_RunWorkerCompleted(null, null);
#else
                ReportProgress = backgroundWorkerReports.ReportProgress;
               backgroundWorkerReports.RunWorkerAsync();
                backgroundWorkerReports.WorkerReportsProgress = true;
#endif
                //CommonFunctions.ShowDialog(new CreateReportForm(UpdateReportOnClose), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnAddReport_Click()", ex);
                throw;
            }
        }

        private void btnExportReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewReportData.Rows.Count == 0)
                {
                    MessageBox.Show(this, "Please Generate the Report to Export.", "Export Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                CommonFunctions.ShowDialog(new ExportToExcelForm(ExportDataTypes.Reports, UpdateReportOnClose, ExportReportsData), this);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnExportReport_Click()", ex);
                throw;
            }
        }

        private Int32 ProcessNExportFile(String ExcelFilePath, Boolean Append, out int RowsCompleted)
        {
            RowsCompleted = 0;
            try
            {
                DataTable dtTempReport = new DataTable();
                
                //List<string> ListOfColumnsToBeExcluded = new List<string>() { "INVOICEID", "QUOTATIONID", "ACCOUNTID", "PAYMENTMODEID" };
                // List<int> ListOfColumnIndexesNotAdded = new List<int>();
                foreach (DataGridViewColumn col in dtGridViewReportData.Columns)
                {
                    //if (!ListOfColumnsToBeExcluded.Contains(col.Name))
                    //{
                    dtTempReport.Columns.Add(col.Name);
                    //}
                    //else ListOfColumnIndexesNotAdded.Add(col.Index);
                }

                foreach (DataGridViewRow row in dtGridViewReportData.Rows)
                {
                    DataRow dRow = dtTempReport.NewRow();
                    int index = 0;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        //if (!ListOfColumnIndexesNotAdded.Contains(cell.ColumnIndex))
                        //{
                        dRow[index] = cell.Value;
                        index++;
                        //}
                    }
                    dtTempReport.Rows.Add(dRow);
                }

                dtTempReport.TableName = "ReportsDetails";
                Int32 RetVal = CommonFunctions.ExportDataTableToExcelFile(dtTempReport, ExcelFilePath, dtTempReport.TableName, Append);
                RowsCompleted = dtTempReport.Rows.Count;
                return RetVal;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnExportReport_Click()", ex);
                return -1 ;
            }
        }
        private Int32 ExportReportsData(String ExcelFilePath, Object ObjDetails, Boolean Append)
        {
            try
            {
                DialogResult result = MessageBox.Show(this, $"Export Reports data to Excel File. {dtGridViewReportData.Rows.Count} rows of Reports Data will be Exported.\n\nDo you want to continue to Export this data?",
                                "Export Status", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.No) return 1;

                // DataTable dtTempReport = new DataTable();
                // //List<string> ListOfColumnsToBeExcluded = new List<string>() { "INVOICEID", "QUOTATIONID", "ACCOUNTID", "PAYMENTMODEID" };
                //// List<int> ListOfColumnIndexesNotAdded = new List<int>();
                // foreach (DataGridViewColumn col in dtGridViewReportData.Columns)
                // {
                //     //if (!ListOfColumnsToBeExcluded.Contains(col.Name))
                //     //{
                //         dtTempReport.Columns.Add(col.Name);
                //     //}
                //     //else ListOfColumnIndexesNotAdded.Add(col.Index);
                // }

                // foreach (DataGridViewRow row in dtGridViewReportData.Rows)
                // {
                //     DataRow dRow = dtTempReport.NewRow();
                //     int index = 0;
                //     foreach (DataGridViewCell cell in row.Cells)
                //     {
                //         //if (!ListOfColumnIndexesNotAdded.Contains(cell.ColumnIndex))
                //         //{
                //             dRow[index] = cell.Value;
                //             index++;
                //         //}
                //     }
                //     dtTempReport.Rows.Add(dRow);
                // }

                // string ExportStatus = ""; dtTempReport.TableName = "ReportsDetails";
                // Int32 RetVal = CommonFunctions.ExportDataTableToExcelFile(dtTempReport, ExcelFilePath, dtTempReport.TableName, Append);
                string ExportStatus = "";int RowsCompleted = 0;
                Int32 RetVal = ProcessNExportFile(ExcelFilePath, Append, out RowsCompleted);
                if (RetVal < 0) ExportStatus += $"{(!String.IsNullOrEmpty(ExportStatus) ? "\n" : "")}Reports:: Failed export";
                //else ExportStatus += $"{(!String.IsNullOrEmpty(ExportStatus) ? "\n" : "")}Reports:: Exported:{dtTempReport.Rows.Count}";
                else ExportStatus += $"{(!String.IsNullOrEmpty(ExportStatus) ? "\n" : "")}Reports:: Exported:{RowsCompleted}";
                if (RetVal == 0)
                {
                    MessageBox.Show(this, $"Exported Reports data to Excel File. Following is the Export status:\n{ExportStatus}",
                                    "Export Status", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return 0;
                }
                else
                {
                    MessageBox.Show(this, $"Following error occurred while exporting Reports data to Excel File.\n{ExportStatus}\n\nPlease check.",
                                    "Export Status", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return -1;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ExportReportsData()", ex);
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

        private void backgroundWorkerReports_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                switch (BackgroundTask)
                {
                    case 1: //Print Report
                        {
                            String SelectedDateTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");
                            string SaveFilePath = Path.GetDirectoryName(Path.GetTempPath()) + "\\Report_" + SelectedDateTimeString + ".xlsx";
                            int RowsCompleted = 0;
                            ProcessNExportFile(SaveFilePath, false,out RowsCompleted);
                            CommonFunctions.PrintAfile(SaveFilePath);
                            if (File.Exists(SaveFilePath)) File.Delete(SaveFilePath);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.backgroundWorkerReports_DoWork()", ex);
            }
        }

        private void backgroundWorkerReports_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                CommonFunctions.UpdateProgressBar(e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.backgroundWorkerReports_ProgressChanged()", ex);
            }
        }

        private void backgroundWorkerReports_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                switch (BackgroundTask)
                {
                    case 1: //Print Order
                        break;
                    case 2:
                        //ExportOption = -1; ExportFolderPath = "";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.backgroundWorkerReports_RunWorkerCompleted()", ex);
            }
        }
        private void cmbBoxReportName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxReportName.SelectedIndex > 0)
                {
                    ReportDetails ObjReportDetails = ObjReportModel.GetReportDetailsFromName(cmbBoxReportName.SelectedItem.ToString());
                    if (ObjReportDetails != null)
                    {
                        cmbBoxParamNames.Items.Clear();
                        cmbBoxParamNames.Items.Add("Select Param Name");
                        List<string> ListAllParamNames = ObjReportModel.GetAllParamNamesFromListID(ObjReportDetails.ListPreDefinedParamID, ObjReportDetails.ListUserDefinedParamID);
                        if (ListAllParamNames != null)
                        {
                            foreach (var item in ListAllParamNames)
                            {
                                cmbBoxParamNames.Items.Add(item);
                            }
                        }
                        cmbBoxParamNames.SelectedIndex = 0;
                        txtBoxParamValue.Text = "";
                        txtBoxReportDesc.Text = ObjReportDetails.Description;
                        dateTimePickerParamValue.Value = DateTime.Today;
                        dtGridViewReportData.DataSource = null;
                        dtGridViewParams.DataSource = null;
                    }
                    else cmbBoxReportName.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxReportName_SelectedIndexChanged()", ex);
                throw;
            }
        }

        private void cmbBoxParamNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxParamNames.SelectedIndex > 0)
                {
                    DefinedParams ObjDefinedParams = ObjReportModel.GetDefinedParamDetailsFromParamName(cmbBoxParamNames.SelectedItem.ToString());

                    switch (ObjDefinedParams.ParamType.ToUpper())
                    {
                        case "DATE":
                            dateTimePickerParamValue.Value = DateTime.Parse((ObjDefinedParams.ParamValue == string.Empty) ? DateTime.Now.ToString() : ObjDefinedParams.ParamValue).Date;
                            cmbBoxParamValue.Enabled = false;
                            txtBoxParamValue.Enabled = false;
                            CurrentParamValue = dateTimePickerParamValue.Value.ToString();
                            break;
                        case "STRING":
                            cmbBoxParamValue.Items.Clear();
                            cmbBoxParamValue.Items.Add("Select Param Value");
                            cmbBoxParamValue.SelectedIndex = 0;
                            DataTable Dt = ObjReportModel.GetDtParamValues(ObjDefinedParams);
                            if (Dt != null)
                            {
                                foreach (DataRow item in Dt.Rows)
                                {
                                    cmbBoxParamValue.Items.Add(item.ItemArray[0]);
                                }
                                CurrentParamValue = cmbBoxParamValue.SelectedItem.ToString();
                            }
                            else CurrentParamValue = "";
                          
                            dateTimePickerParamValue.Enabled = false;
                            txtBoxParamValue.Enabled = false;
                            
                            break;
                        case "VALUE":
                            txtBoxParamValue.Text = CurrentParamValue = ObjDefinedParams.ParamValue;
                            cmbBoxParamValue.Enabled = false;
                            dateTimePickerParamValue.Enabled = false;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.cmbBoxParamNames_SelectedIndexChanged()", ex);
                throw;
            }
        }

        private void btnAddParamValue_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxParamNames.SelectedIndex == 0)
                {
                    MessageBox.Show(this, "Please select a Param Name to Add", "Add Param", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                bool entryFound = false;
                foreach (DataGridViewRow row in dtGridViewParams.Rows)
                {
                    object val1 = row.Cells[0].Value;

                    if (val1 != null && val1.ToString() == cmbBoxParamNames.SelectedItem.ToString() )
                    {
                        entryFound = true;
                        row.Cells[1].Value = CurrentParamValue;
                        break;
                    }
                }

                if (!entryFound)
                {
                    dtGridViewParams.Rows.Add(cmbBoxParamNames.SelectedItem.ToString(), CurrentParamValue);
                    CountOfParamsAdded += 1;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnAddParamValue_Click()", ex);
                throw;
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                if ((CountOfParamsAdded != cmbBoxParamNames.Items.Count - 1) || cmbBoxReportName.SelectedIndex == 0)
                {
                    MessageBox.Show(this, "Please add all Params required for this Report", "Incomplete Param Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                ReportDetails ObjReportDetails = ObjReportModel.GetReportDetailsFromName(cmbBoxReportName.SelectedItem.ToString());
                string Query = ObjReportDetails.Query;

                List<string> ListColumnNameParamStr = new List<string>(), ListColumnValues = new List<string>(), ListColumnDataType = new List<string>();
                foreach (DataGridViewRow row in dtGridViewParams.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        DefinedParams ObjDefinedParams = ObjReportModel.GetDefinedParamDetailsFromParamName(row.Cells[0].Value.ToString());
                        if (Query.Contains(row.Cells[0].Value.ToString()))
                        {
                            Query = Query.Replace(row.Cells[0].Value.ToString(), ObjDefinedParams.ActualColName);
                        }
                        string ValueStr = "@Value" + row.Cells[0].Value.ToString().Replace('@', ' ').Trim();
                        ListColumnNameParamStr.Add(ValueStr);
                        ListColumnValues.Add(row.Cells[1].Value.ToString());
                        ListColumnDataType.Add(ObjReportModel.GetMysqlDataTypeStrFromParamDataType(ObjDefinedParams.ParamType));
                    }
                }
                DataTable Dt = ObjMySQLHelper.BuildNReturnQueryResultWithParams(Query, ListColumnNameParamStr, ListColumnDataType, ListColumnValues);

                if (Dt == null)
                {
                    MessageBox.Show(this, "Error while Executing Query", "Error Query", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                dtGridViewReportData.DataSource = Dt;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnGenerateReport_Click()", ex);
                throw;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxParamNames.Items.Count > 0) cmbBoxParamNames.SelectedIndex = 0;
                if (cmbBoxParamValue.Items.Count > 0) cmbBoxParamValue.SelectedIndex = 0;
                cmbBoxReportName.SelectedIndex = 0;
                txtBoxParamValue.Text = "";
                txtBoxReportDesc.Text = "";
                dateTimePickerParamValue.Value = DateTime.Today;
                dtGridViewReportData.DataSource = null;
                dtGridViewParams.DataSource = null;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnReset_Click()", ex);
                throw;
            }
        }

        void UpdateReportOnClose(Int32 Mode)
        {
            try
            {
                switch (Mode)
                {
                    case 1:
                        LoadReports();
                        break;
                    case 2:
                        //CommonFunctions.ObjCustomerMasterModel.LoadAllCustomerMasterTables();
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUsersForm.UpdateReportOnClose()", ex);
            }
        }

        private void cmbBoxParamValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBoxParamValue.SelectedIndex > 0) CurrentParamValue = cmbBoxParamValue.SelectedItem.ToString();
                else CurrentParamValue = "";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ManageUsersForm.UpdateReportOnClose()", ex);
            }
        }
    }
}
