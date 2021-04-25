using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesOrdersReport.CommonModules;

namespace SalesOrdersReport.Models
{

    public class ReportDetails : IComparer<ReportDetails>
    {
        public int ReportID = -1;
        public DateTime LastUpdateDate, CreationDate;
        public String ReportName = "", Description = "", Query = "";
        public List<int> ListPreDefinedParamID = new List<int>(), ListUserDefinedParamID = new List<int>();
        public bool Active = true;

        public int Compare(ReportDetails x, ReportDetails y)
        {
            return x.ReportName.ToUpper().CompareTo(y.ReportName.ToUpper());
        }

    }

    class DefinedParams : IComparer<DefinedParams>
    {
        public int ParamID = -1;
        public string ParamName = "";
        public string ParamType = "string"; // string,date,value
        public Types ParamDataType = Types.String;
        public string ActualColName = "";
        public string TableNameToLookInto = "";
        public bool IsPreDefinedParam = true;
        public string ParamValue = "";
        public int Compare(DefinedParams x, DefinedParams y)
        {
            return x.ParamName.ToUpper().CompareTo(y.ParamName.ToUpper());
        }

    }
    class ReportModel
    {

        List<ReportDetails> ListReportDetails = new List<ReportDetails>();
        MySQLHelper ObjMySQLHelper;
        List<DefinedParams> ListReportPreDefinedParams = new List<DefinedParams>();
        List<DefinedParams> ListReportUserDefinedParams = new List<DefinedParams>();
        string TempQueryStr = " a.*, b.CUSTOMERID, c.CUSTOMERNAME,c.PHONENO,d.INVOICENUMBER,e.USERNAME FROM PAYMENTS a "
                           + " Inner Join ACCOUNTSMASTER b on a.ACCOUNTID = b.ACCOUNTID "
                            + " Inner Join CUSTOMERMASTER c on b.CUSTOMERID = c.CUSTOMERID "
                            + " Inner Join Invoices d on a.INVOICEID = d.INVOICEID "
                            + " Inner Join USERMASTER e on a.USERID = e.USERID ";

        DataTable DtTempSummary = new DataTable();


        public ReportModel()
        {
            try
            {
                ObjMySQLHelper = MySQLHelper.GetMySqlHelperObj();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ctor()", ex);
            }
        }

        public List<String> GetAllReportNames()
        {
            try
            {
                if (ListReportDetails == null || ListReportDetails.Count == 0) return null;
                return ListReportDetails.Select(e => e.ReportName).OrderBy(s => s).ToList();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetAllReportNames()", ex);
                return null;
            }
        }

        public DataTable GetAllParamNames(bool ForPreDefinedOnly = true)
        {
            try
            {
                String WhereCondition = "";
                DataTable dtReports = null;
                if (ForPreDefinedOnly)
                {
                    WhereCondition = "IsPreDefinedParam = 1";
                }
                else
                {
                    WhereCondition = "IsPreDefinedParam = 0";
                }
                string Query = "SELECT ParamName As 'Param Name' FROM ReportDefinedParams Where " + WhereCondition + " ;";
                dtReports = ObjMySQLHelper.GetQueryResultInDataTable(Query);

                DataColumn newCol = new DataColumn("Param Value", typeof(string));
                if (!ForPreDefinedOnly) newCol.ReadOnly = false;
                newCol.AllowDBNull = true;
                dtReports.Columns.Add(newCol);
                
                foreach (DataRow row in dtReports.Rows)
                {
                    row["Param Value"] = "@Value" + row["Param Name"].ToString().Replace('@', ' ').Trim();
                }

                return dtReports;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetAllParamNames()", ex);
                return null;
            }
        }

        public List<string> GetAllParamNamesFromListID(List<int> ListPreDefinedParamsIDForThatReport, List<int> ListUserDefinedParamsIDForThatReport)
        {
            try
            {
                List<string> ListAllParamNames = new List<string>();
                if (ListPreDefinedParamsIDForThatReport != null || ListPreDefinedParamsIDForThatReport.Count != 0)
                {
                    for (int i = 0; i < ListPreDefinedParamsIDForThatReport.Count; i++)
                    {
                        int Index = ListReportPreDefinedParams.FindIndex(e => e.ParamID == ListPreDefinedParamsIDForThatReport[i]);
                        if (Index >= 0)
                        {
                            ListAllParamNames.Add(ListReportPreDefinedParams[Index].ParamName);
                        }
                    }
                }

                if (ListUserDefinedParamsIDForThatReport != null || ListUserDefinedParamsIDForThatReport.Count != 0)
                {
                    for (int i = 0; i < ListUserDefinedParamsIDForThatReport.Count; i++)
                    {
                        int Index = ListReportUserDefinedParams.FindIndex(e => e.ParamID == ListUserDefinedParamsIDForThatReport[i]);
                        if (Index >= 0)
                        {
                            ListAllParamNames.Add(ListReportUserDefinedParams[Index].ParamName);
                        }
                    }
                }
                return ListAllParamNames;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetAllParamNamesFromListID()", ex);
                return null;
            }
        }

        public DefinedParams GetDefinedParamDetailsFromParamName(string ParamName)
        {
            try
            {
                DefinedParams ObjDefinedParams = new DefinedParams();
                ObjDefinedParams.ParamName = ParamName.Trim();

                Int32 Index = ListReportPreDefinedParams.BinarySearch(ObjDefinedParams, ObjDefinedParams);
                if (Index < 0)
                {
                    Index = ListReportUserDefinedParams.BinarySearch(ObjDefinedParams, ObjDefinedParams);
                    if (Index < 0) return null;

                    return ListReportUserDefinedParams[Index];
                }
                return ListReportPreDefinedParams[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetDefinedParamDetailsFromParamName()", ex);
                return null;
            }
        }

        public DataTable GetDtParamValues(DefinedParams ObjDefinedParams)
        {
            try
            {
                DataTable dtReports = null;
                string Query = "Select " + ObjDefinedParams.ActualColName + " From " + ObjDefinedParams.TableNameToLookInto;
                dtReports = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                return dtReports;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetDtParamValues()", ex);
                return null;
            }
        }
        public DefinedParams GetDefinedParamDetailsFromParamID(int ParamID)
        {
            try
            {
                DefinedParams ObjDefinedParams = new DefinedParams();
                ObjDefinedParams.ParamID = ParamID;

                Int32 Index = ListReportPreDefinedParams.FindIndex(e=>e.ParamID== ParamID);
                if (Index < 0)
                {
                    Index = ListReportUserDefinedParams.FindIndex(e => e.ParamID == ParamID);
                    if (Index < 0) return null;

                    return ListReportUserDefinedParams[Index];
                }
                return ListReportPreDefinedParams[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetDefinedParamDetailsFromParamID()", ex);
                return null;
            }
        }

        public string GetMysqlDataTypeStrFromParamDataType(string ParamType)
        {
            try
            {
                string DataTypeStr = "";
                switch (ParamType.ToUpper())
                {
                    case "DATE":
                        DataTypeStr = "TIMESTAMP";
                        break;
                    case "STRING":
                        DataTypeStr = "VARCHAR";
                        break;
                    case "VALUE":
                        DataTypeStr = "FLOAT";
                        break;
                }
                return DataTypeStr;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetMysqlDataTypeStrFromParamDataType()", ex);
                return "";
            }
        }
        public ReportDetails GetReportDetailsFromName(string ReportName)
        {
            try
            {
                ReportName = ReportName.Trim();
                ReportDetails ObjReportDetails = new ReportDetails();
                ObjReportDetails.ReportName = ReportName;

                Int32 Index = ListReportDetails.BinarySearch(ObjReportDetails, ObjReportDetails);
                if (Index < 0) return null;
                return ListReportDetails[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetReportDetailsFromName()", ex);
                return null;
            }
        }

        public Int32 DeleteReportDetails(Int32 ReportID)
        {
            try
            {
                ObjMySQLHelper.UpdateTableDetails("Reports", new List<string>() { "ACTIVE" }, new List<string>() { "0" },
                                            new List<Types>() { Types.Number }, $"ReportID = {ReportID}");


                ListReportDetails.Find(e => e.ReportID == ReportID).Active = false;

                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.DeleteReportDetails()", ex);
                return -1;
            }
        }
        public DataTable LoadDefinedParamsDataTable()
        {
            try
            {
                String Query = "SELECT * FROM ReportDefinedParams;";
                DataTable dtPreDefinedParams = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                LoadDefinedParamsMaster(dtPreDefinedParams);
                return dtPreDefinedParams;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadDefinedParamsDataTable()", ex);
                return null;
            }
        }

        public void LoadDefinedParamsMaster(DataTable dtDefinedParams)
        {
            try
            {
                ListReportPreDefinedParams = new List<DefinedParams>();
                #region Load DefinedParams Details
                for (int i = 0; i < dtDefinedParams.Rows.Count; i++)
                {
                    DataRow dtRow = dtDefinedParams.Rows[i];
                    DefinedParams ObjDefinedParams = new DefinedParams();
                    ObjDefinedParams.ParamID = Int32.Parse(dtRow["ParamID"].ToString().Trim());
                    ObjDefinedParams.ParamName = dtRow["ParamName"].ToString().Trim();
                    ObjDefinedParams.ActualColName = dtRow["ActualColumnName"].ToString().Trim();
                    ObjDefinedParams.ParamType = dtRow["Type"].ToString().Trim();
                    ObjDefinedParams.ParamDataType = dtRow["DataType"].ToString().Trim().ToUpper() == "STRING" ? Types.String : Types.Number;
                    ObjDefinedParams.TableNameToLookInto = dtRow["TableNameToLookInto"].ToString().Trim();
                    ObjDefinedParams.ParamValue = dtRow["ParamValue"].ToString().Trim();
                    ObjDefinedParams.IsPreDefinedParam = dtRow["IsPreDefinedParam"].ToString() == "1" ? true : false;
                    if (ObjDefinedParams.IsPreDefinedParam) AddPreDefinedParamsToCache(ObjDefinedParams);
                    else AddUserDefinedParamsToCache(ObjDefinedParams);
                }
                #endregion
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadDefinedParamsMaster()", ex);
            }
        }

        public void AddPreDefinedParamsToCache(DefinedParams ObjDefinedParams)
        {
            try
            {
                Int32 ParamsIndex = ListReportPreDefinedParams.BinarySearch(ObjDefinedParams, ObjDefinedParams);
                if (ParamsIndex < 0)
                {
                    ListReportPreDefinedParams.Insert(~ParamsIndex, ObjDefinedParams);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.AddPreDefinedParamsToCache()", ex);
            }
        }

        public void AddUserDefinedParamsToCache(DefinedParams ObjDefinedParams)
        {
            try
            {
                Int32 ParamIndex = ListReportUserDefinedParams.BinarySearch(ObjDefinedParams, ObjDefinedParams);
                if (ParamIndex < 0)
                {
                    ListReportUserDefinedParams.Insert(~ParamIndex, ObjDefinedParams);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}..AddUserDefinedParamsToCache()", ex);
            }
        }

        //public DataTable LoadNGetUserDefinedParamsDataTable()
        //{
        //    try
        //    {
        //        String Query = "SELECT * FROM ReportUserDefinedParams;";
        //        DataTable dtUserDefinedParams = ObjMySQLHelper.GetQueryResultInDataTable(Query);
        //        LoadUserDefinedParamsMaster(dtUserDefinedParams);
        //        return dtUserDefinedParams;
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonFunctions.ShowErrorDialog($"{this}.LoadNGetUserDefinedParamsDataTable()", ex);
        //        return null;
        //    }
        //}

        //public void LoadUserDefinedParamsMaster(DataTable dtUserDefinedParams)
        //{
        //    try
        //    {

        //        ListReportUserDefinedParams = new List<DefinedParams>();
        //        #region Load Vendor Details
        //        for (int i = 0; i < dtUserDefinedParams.Rows.Count; i++)
        //        {
        //            DataRow dtRow = dtUserDefinedParams.Rows[i];
        //            DefinedParams ObjDefinedParams = new DefinedParams();
        //            ObjDefinedParams.ParamID = Int32.Parse(dtRow["UserDefinedParamID"].ToString().Trim());
        //            ObjDefinedParams.ParamName = dtRow["ParamName"].ToString().Trim();
        //            ObjDefinedParams.ActualColName = dtRow["ActualColumnName"].ToString().Trim();
        //            ObjDefinedParams.ParamType = dtRow["Type"].ToString().Trim();
        //            ObjDefinedParams.ParamDataType = dtRow["DataType"].ToString().Trim().ToUpper() == "STRING" ? Types.String : Types.Number;
        //            ObjDefinedParams.TableNameToLookInto = dtRow["TableNameToLookInto"].ToString().Trim();

        //            AddUserDefinedParamsToCache(ObjDefinedParams);
        //        }
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonFunctions.ShowErrorDialog($"{this}.LoadUserDefinedParamsMaster()", ex);
        //    }
        //}
        public DataTable LoadReportsDataTable()
        {
            try
            {
                String Query = "SELECT * FROM Reports;";
                DataTable dtReports = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                LoadReportDetails(dtReports);
                return dtReports;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadReportsDataTable()", ex);
                return null;
            }
        }

        public void LoadReportDetails(DataTable dtReports)
        {
            try
            {
                ListReportDetails = new List<ReportDetails>();
                for (int i = 0; i < dtReports.Rows.Count; i++)
                {
                    DataRow dr = dtReports.Rows[i];

                    ReportDetails ObjReportDetails = new ReportDetails();
                    ObjReportDetails.ReportID = ((dr["ReportID"] == null) || dr["ReportID"].ToString().Trim() == "") ? -1 : int.Parse(dr["ReportID"].ToString().Trim());
                    ObjReportDetails.ReportName = dr["ReportName"].ToString();
                    ObjReportDetails.Description = dr["Description"].ToString();
                    ObjReportDetails.Query = dr["Query"].ToString();
                    ObjReportDetails.CreationDate = DateTime.Parse(dr["CREATIONDATE"].ToString());
                    ObjReportDetails.LastUpdateDate = DateTime.Parse(dr["LASTUPDATEDATE"].ToString());
                    ObjReportDetails.Active = dr["ACTIVE"].ToString() == "1" ? true : false;
                    //string[] tempDefined = (dr["PreDefinedParamID"].ToString() == string.Empty) ? new string[0] : dr["PreDefinedParamID"].ToString().Split(',');
                    //for (int mn = 0; mn < tempDefined.Length; mn++)
                    //{
                    //    ObjReportDetails.ListPreDefinedParamID.Add(int.Parse(tempDefined[mn]));
                    //}
                    //tempDefined = (dr["UserDefinedParamID"].ToString() == string.Empty) ? new string[0] : dr["UserDefinedParamID"].ToString().Split(',');
                    //for (int mn = 0; mn < tempDefined.Length; mn++)
                    //{
                    //    ObjReportDetails.ListUserDefinedParamID.Add(int.Parse(tempDefined[mn]));
                    //}
                    string[] tempDefined = (dr["ParamID"].ToString() == string.Empty) ? new string[0] : dr["ParamID"].ToString().Split(',');
                    for (int mn = 0; mn < tempDefined.Length; mn++)
                    {
                        DefinedParams ObjDefinedParams = GetDefinedParamDetailsFromParamID(int.Parse(tempDefined[mn]));
                        if (ObjDefinedParams.IsPreDefinedParam) ObjReportDetails.ListPreDefinedParamID.Add(int.Parse(tempDefined[mn]));
                        else ObjReportDetails.ListUserDefinedParamID.Add(int.Parse(tempDefined[mn]));
                    }


                    int Index = ListReportDetails.BinarySearch(ObjReportDetails, ObjReportDetails);
                    if (Index < 0) ListReportDetails.Insert(~Index, ObjReportDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ListReportDetails()", ex);
            }
        }

    }
}
