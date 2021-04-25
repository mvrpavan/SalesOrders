using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Models;

namespace SalesOrdersReport.Views
{
    public partial class CreateReportForm : Form
    {
        UpdateOnCloseDel UpdateReportOnClose = null;
        MySQLHelper ObjMySQLHelper;
        ReportDetails ObjReportDetailsToBeEdited;
        ReportModel ObjReportModel = null;
        // List<string> ListAddedParamNames = new List<string>();
        // List<string> ListAddedParamValue = new List<string>();
        Dictionary<string, string> DictAddedParamNamesValues = new Dictionary<string, string>();
        List<int> ListAddedParamID = new List<int>();
        public CreateReportForm(UpdateOnCloseDel UpdateReportOnClose, bool CreateReport = true, ReportDetails ObjReportDtls = null)
        {
            try
            {
                InitializeComponent();
                this.UpdateReportOnClose = UpdateReportOnClose;
                ObjMySQLHelper = MySQLHelper.GetMySqlHelperObj();

                CommonFunctions.SetDataGridViewProperties(dtGridViewUserDefinedParameters);
                CommonFunctions.SetDataGridViewProperties(dtGridViewPredefinedParameters);
                CommonFunctions.SetDataGridViewProperties(dtGridViewQueryResult);
                string FormTitle = "";
                if (CreateReport)
                {
                    FormTitle = "Create Report";
                    btnCreateReport.Text = "Create Report";
                }
                else
                {
                    FormTitle = "Update Report";
                    btnCreateReport.Text = "Update Report";
                    ObjReportDetailsToBeEdited = ObjReportDtls;

                }
                this.Text = FormTitle;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreateReportForm()", ex);
                throw;
            }
        }

        private void CreateReportForm_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    ObjReportModel = new ReportModel();
                    ObjReportModel.LoadDefinedParamsDataTable();
                    //Load Predefined Grid
                    dtGridViewPredefinedParameters.DataSource = null;
                    dtGridViewPredefinedParameters.DataSource = ObjReportModel.GetAllParamNames();
                    //Load UserDefined Grid
                    dtGridViewUserDefinedParameters.DataSource = null;
                    dtGridViewUserDefinedParameters.DataSource = ObjReportModel.GetAllParamNames(false);
                    if (this.Text == "Update Report")
                    {
                        txtBoxReportName.Text = ObjReportDetailsToBeEdited.ReportName;
                        txtBoxReportName.Enabled = false;
                        txtBoxDescription.Text = ObjReportDetailsToBeEdited.Description;
                        txtBoxQuery.Text = ObjReportDetailsToBeEdited.Query;
                    }
                    lblQueryStatus.Text = "";
                    lblErrValidMsg.Visible = false;

                }
                catch (Exception ex)
                {
                    CommonFunctions.ShowErrorDialog($"{this}.CreatePaymentForm_Load()", ex);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreateReportForm_Load()", ex);
                throw;
            }
        }



        private void btnAddParameter_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtGridViewPredefinedParameters.SelectedRows.Count == 0)
                {
                    MessageBox.Show(this, "Please select a Param to Move.", "Move Param", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string ParamName = dtGridViewPredefinedParameters.SelectedRows[0].Cells[0].Value.ToString();
                string ParamValue = dtGridViewPredefinedParameters.SelectedRows[0].Cells[1].Value.ToString();

                DefinedParams ObjDefinedParams = ObjReportModel.GetDefinedParamDetailsFromParamName(ParamName);
                if (ObjDefinedParams != null)
                {
                    ListAddedParamID.Add(ObjDefinedParams.ParamID);
                }
                    if (!DictAddedParamNamesValues.ContainsKey(ParamName))
                {
                    DictAddedParamNamesValues.Add(ParamName, ParamValue);
                }
                int cursorPosition = txtBoxQuery.SelectionStart;
                string insertText = ParamName + " = " + ParamValue + Environment.NewLine;
                txtBoxQuery.Text = txtBoxQuery.Text.Insert(cursorPosition, insertText);
                //cursorPosition = cursorPosition + insertText.Length;
                txtBoxQuery.SelectionStart = cursorPosition + insertText.Length - 1;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnAddParameter_Click()", ex);
                throw;
            }
        }

        public void BuildQuery()
        {
            try
            {
                txtBoxQuery.Text = txtBoxQuery.Text.Trim();
                foreach (var item in DictAddedParamNamesValues)
                {
                    {
                        if (txtBoxQuery.Text.Contains(item.Key))
                        {
                            DefinedParams ObjDefinedParams = ObjReportModel.GetDefinedParamDetailsFromParamName(item.Key);
                            if (ObjDefinedParams != null)
                            {
                                //ListAddedParamID.Add(ObjDefinedParams.ParamID);
                                txtBoxQuery.Text = txtBoxQuery.Text.Replace(item.Key, ObjDefinedParams.ActualColName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.BuildQuery()", ex);
                throw ex;
            }
        }

        private void btnValidateQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBoxQuery.Text.Trim() == string.Empty)
                {
                    lblErrValidMsg.Visible = true;
                    lblErrValidMsg.Text = "Query Cant be empty!!";
                    return;
                }
                if (lblErrValidMsg.Visible == true)
                {
                    lblErrValidMsg.Visible = false;
                }
                txtBoxQuery.Text = txtBoxQuery.Text.Trim();
                string TempQeryStr = txtBoxQuery.Text;
                foreach (var item in DictAddedParamNamesValues)
                {
                    {
                        if (TempQeryStr.Contains(item.Key))
                        {
                            DefinedParams ObjDefinedParams = ObjReportModel.GetDefinedParamDetailsFromParamName(item.Key);
                            if (ObjDefinedParams != null)
                            {
                                ListAddedParamID.Add(ObjDefinedParams.ParamID);
                                //string tempActualCol = "";
                                //switch (ObjDefinedParams.ParamType.ToUpper())
                                //{
                                //    case "DATE":
                                //        tempActualCol="\'"
                                //        break;
                                //    case "STRING":
                                //        if (ObjDefinedParams.ParamValue != string.Empty) cmbBoxParamValue.SelectedItem = ObjDefinedParams.ParamValue;
                                //        else cmbBoxParamValue.SelectedIndex = 0;
                                //        dateTimePickerParamValue.Enabled = false;
                                //        txtBoxParamValue.Enabled = false;
                                //        CurrentParamValue = cmbBoxParamValue.SelectedItem.ToString();
                                //        break;
                                //    case "VALUE":
                                //        txtBoxParamValue.Text = CurrentParamValue = ObjDefinedParams.ParamValue;
                                //        cmbBoxParamValue.Enabled = false;
                                //        dateTimePickerParamValue.Enabled = false;
                                //        break;
                                //}
                                TempQeryStr = TempQeryStr.Replace(item.Key, ObjDefinedParams.ActualColName);
                            }
                        }
                    }


                }
                DataTable Dt = ObjMySQLHelper.GetQueryResultInDataTable(TempQeryStr);

                if (Dt != null)
                {
                    lblQueryStatus.Text = "Success";
                    dtGridViewQueryResult.DataSource = null;
                    dtGridViewQueryResult.DataSource = Dt;
                }
                else
                {
                    lblQueryStatus.Text = "Error!! Please Check Your Query";
                    dtGridViewQueryResult.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnValidateQuery_Click()", ex);
                throw;
            }
        }

        private void btnCreateReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBoxReportName.Text.Trim() == string.Empty)
                {
                    lblErrValidMsg.Visible = true;
                    lblErrValidMsg.Text = "Report Name Cant be empty!";
                    return;
                }

                if (txtBoxQuery.Text.Trim() == string.Empty)
                {
                    lblErrValidMsg.Visible = true;
                    lblErrValidMsg.Text = "Query Cant be empty!!";
                    return;
                }
           
                //if (lblQueryStatus.Text == string.Empty || lblQueryStatus.Text.Contains("Error"))
                //{
                //    lblErrValidMsg.Visible = true;
                //    lblErrValidMsg.Text = "Please Validate the Query First and then Proceed to Create Report";
                //    return;
                //}

                if (lblErrValidMsg.Visible == true)
                {
                    lblErrValidMsg.Visible = false;
                }
               // BuildQuery();
                if (this.Text == "Create Report") CreateReportTable();
                else EditReportTable();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnCreateReport_Click()", ex);
                throw;
            }
        }
        public void CreateReportTable()
        {
            try
            {
                List<string> ListColumnValues = new List<string>(), ListColumnNames = new List<string>();
                List<Types> ListTypes = new List<Types>();

                ListColumnValues.Add(txtBoxReportName.Text.Trim());
                ListColumnNames.Add("ReportName");
                ListTypes.Add(Types.String);

                ListColumnValues.Add(txtBoxDescription.Text.Trim());
                ListColumnNames.Add("Description");
                ListTypes.Add(Types.String);
               // txtBoxQuery.Text = txtBoxQuery.Text.Trim().Replace("'", @"\'");
                ListColumnValues.Add(txtBoxQuery.Text);
                ListColumnNames.Add("Query");
                ListTypes.Add(Types.String);
                if (ListAddedParamID.Count > 0)
                {
                    string tempStr = "";
                    for (int i = 0; i < ListAddedParamID.Count; i++)
                    {
                        tempStr += ListAddedParamID[i] + ",";
                    }
                    if (tempStr.Length > 0) tempStr = tempStr.Remove(tempStr.Length - 1, 1);
                    ListColumnValues.Add(tempStr);
                    ListColumnNames.Add("ParamID");
                    ListTypes.Add(Types.String);
                }

                string Now = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");

                ListColumnValues.Add(Now);
                ListColumnNames.Add("CreationDate");
                ListTypes.Add(Types.String);

                ListColumnValues.Add(Now);
                ListColumnNames.Add("LastUpdateDate");
                ListTypes.Add(Types.String);
                int ResultVal = -1;

                ResultVal = ObjMySQLHelper.InsertIntoTable("Reports", ListColumnNames, ListColumnValues, ListTypes);

                if (ResultVal <= 0) MessageBox.Show("Wasnt able to add data to reports", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (ResultVal == 2)
                {
                    MessageBox.Show("err.", "Error");
                }
                else
                {
                    MessageBox.Show("Created Report :: " + txtBoxReportName.Text.Trim() + " successfully", "Added  Report");
                    UpdateReportOnClose(Mode: 1);
                    DictAddedParamNamesValues.Clear();
                    DictAddedParamNamesValues = new Dictionary<string, string>();
                    ListAddedParamID.Clear();
                    ListAddedParamID = new List<int>();
                    RestClick();
                    //btnCancel.PerformClick();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreateReportTable()", ex);
            }
        }


        public void EditReportTable()
        {
            try
            {
                if (txtBoxQuery.Text.Trim() == string.Empty)
                {
                    lblErrValidMsg.Visible = true;
                    lblErrValidMsg.Text = "Query Cant be empty!!";
                    return;
                }
                //if (lblQueryStatus.Text == string.Empty || lblQueryStatus.Text.Contains("Error"))
                //{
                //    lblErrValidMsg.Visible = true;
                //    lblErrValidMsg.Text = "Please Validate the Query First and then Proceed to Create Report";
                //    return;
                //}
                if (lblErrValidMsg.Visible == true)
                {
                    lblErrValidMsg.Visible = false;
                }

                List<string> ListColumnValues = new List<string>(), ListTempColValues = new List<string>();
                List<string> ListColumnNames = new List<string>(), ListTempColNames = new List<string>();
                List<Types> ListTypes = new List<Types>();
                ListColumnValues.Add(txtBoxDescription.Text.Trim());
                ListColumnNames.Add("Description");
                ListTypes.Add(Types.String);

                ListColumnValues.Add(txtBoxQuery.Text.Trim());
                ListColumnNames.Add("Query");
                ListTypes.Add(Types.String);

                string Now = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
                ListColumnValues.Add(Now);
                ListColumnNames.Add("LastUpdateDate");
                ListTypes.Add(Types.String);

                string WhereCondition = "ReportID = '" + ObjReportDetailsToBeEdited.ReportID + "'";
                int ResultVal = ObjMySQLHelper.UpdateTableDetails("Reports", ListColumnNames, ListColumnValues, ListTypes, WhereCondition);
                if (ResultVal <= 0) MessageBox.Show("Wasnt able to update the payment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (ResultVal == 2)
                {
                    MessageBox.Show("err", "Error");
                }
                else
                {
                    MessageBox.Show("Updated Report :: " + ObjReportDetailsToBeEdited.ReportName + " successfully", "Updated  Report");

                    UpdateReportOnClose(Mode: 1);
                    //RestClick();
                    //btnCancel.PerformClick();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.EditReportTable()", ex);
            }
        }

        public void RestClick()
        {
            try
            {
                DictAddedParamNamesValues.Clear();
                DictAddedParamNamesValues = new Dictionary<string, string>();
                ListAddedParamID = new List<int>();
                txtBoxReportName.Text = "";
                txtBoxQuery.Text = "";
                txtBoxDescription.Text = "";
                //dtGridViewUserDefinedParameters.DataSource = null;
                //if (dtGridViewUserDefinedParameters.RowCount > 0) dtGridViewUserDefinedParameters.Rows.Clear();
                //if (dtGridViewUserDefinedParameters.ColumnCount > 0) dtGridViewUserDefinedParameters.Columns.Clear();

                //dtGridViewQueryResult.DataSource = null;
                //if (dtGridViewQueryResult.RowCount > 0) dtGridViewQueryResult.Rows.Clear();
                //if (dtGridViewQueryResult.ColumnCount > 0) dtGridViewQueryResult.Columns.Clear();


                if (lblErrValidMsg.Visible == true)
                {
                    lblErrValidMsg.Visible = false;
                }
                lblQueryStatus.Text = "";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.RestClick()", ex);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {

                //ListAddedParamNames.Clear();
                //ListAddedParamNames = new List<string>();
                //txtBoxReportName.Text = "";
                //txtBoxQuery.Text = "";
                //txtBoxDescription.Text = "";
                ////dtGridViewUserDefinedParameters.DataSource = null;
                ////if (dtGridViewUserDefinedParameters.RowCount > 0) dtGridViewUserDefinedParameters.Rows.Clear();
                ////if (dtGridViewUserDefinedParameters.ColumnCount > 0) dtGridViewUserDefinedParameters.Columns.Clear();

                ////dtGridViewQueryResult.DataSource = null;
                ////if (dtGridViewQueryResult.RowCount > 0) dtGridViewQueryResult.Rows.Clear();
                ////if (dtGridViewQueryResult.ColumnCount > 0) dtGridViewQueryResult.Columns.Clear();


                //if (lblErrValidMsg.Visible == true)
                //{
                //    lblErrValidMsg.Visible = false;
                //}
                //lblQueryStatus.Text = "";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.btnCancel_Click()", ex);
                throw;
            }
        }
    }
}
