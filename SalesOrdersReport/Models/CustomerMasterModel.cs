using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesOrdersReport.CommonModules;
using System.Windows.Forms;
using System.Data;

namespace SalesOrdersReport.Models
{
    class CustomerMasterModel
    {
        List<CustomerDetails> ListCustomerDetails;
        List<LineDetails> ListLineDetails;
        List<PriceGroupDetails> ListPriceGroupDetails;
        List<DiscountGroupDetails1> ListDiscountGroupDetails;
        List<StateDetails> ListStateDetails;
        Int32 DefaultDiscountGroupIndex;

        MySQLHelper ObjMySQLHelper = null;
        public void Initialize()
        {
            try
            {
                ListCustomerDetails = new List<CustomerDetails>();
                ListLineDetails = new List<LineDetails>();
                ListPriceGroupDetails = new List<PriceGroupDetails>();
                ListDiscountGroupDetails = new List<DiscountGroupDetails1>();
                ListStateDetails = new List<StateDetails>();
                ObjMySQLHelper = MySQLHelper.GetMySqlHelperObj();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.Initialize()", ex);
            }
        }


   

        public void AddCustomerToCache(CustomerDetails ObjCustomerDetails, List<PriceGroupDetails> ListPriceGroups)
        {
            try
            {
                Int32 CustomerIndex = ListCustomerDetails.BinarySearch(ObjCustomerDetails, ObjCustomerDetails);
                if (CustomerIndex < 0)
                {
                    ListCustomerDetails.Insert(~CustomerIndex, ObjCustomerDetails);

                    ObjCustomerDetails.LineID = CommonFunctions.ListCustomerLines.FindIndex(e => e.Equals(ObjCustomerDetails.LineName, StringComparison.InvariantCultureIgnoreCase));
                    ObjCustomerDetails.PriceGroupID = ListPriceGroups.FindIndex(e => e.PriceGrpName.Equals(ObjCustomerDetails.PriceGroupName, StringComparison.InvariantCultureIgnoreCase));
                    ObjCustomerDetails.DiscountGroupID = ListDiscountGroupDetails.FindIndex(e => e.DiscountGrpName.Equals(ObjCustomerDetails.DiscountGroupName, StringComparison.InvariantCultureIgnoreCase));
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.AddCustomerToCache()", ex);
            }
        }
        public List<string> GetAllPriceGrp()
        {
            try
            {
                List<string> ListPriceGrp = new List<string>();
                ListPriceGrp.AddRange(ListPriceGroupDetails.Select(e => e.PriceGrpName).ToList());
                return ListPriceGrp;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.GetAllPriceGrp()", ex);
                throw ex;
            }
        }
        public List<string> GetAllLineNames()
        {
            try
            {
                List<string> ListNames = new List<string>();
                ListNames.AddRange(ListLineDetails.Select(e => e.LineName).ToList());
                return ListNames;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.GetAllLineNames()", ex);
                throw ex;
            }
        }

        public List<string> GetAllDiscGrp()
        {
            try
            {
                List<string> ListDiscGrp = new List<string>();
                ListDiscGrp.AddRange(ListDiscountGroupDetails.Select(e => e.DiscountGrpName).ToList());
                return ListDiscGrp;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.ListDiscGrp()", ex);
                throw ex;
            }
        }
        public List<CustomerDetails> GetListCustomerCache()
        {
            try
            {
                List<CustomerDetails> ListCustomerCache = new List<CustomerDetails>();
                ListCustomerCache.AddRange(ListCustomerDetails);

                return ListCustomerCache;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.GetListUserCache()", ex);
            }
            return null;
        }

        public List<string> GetAllStatesOfIndia()
        {
            try
            {
                List<string> ListOfStates = new List<string>();
                ListOfStates.AddRange(ListStateDetails.Select(e => e.State).ToList());

                //List<string> ListOfStates1 = new List<string>() { "Andhra Pradesh", "Arunachal Pradesh", "Assam", "Bihar", "Chhattisgarh", "Goa", "Gujarat", "Haryana", "Himachal Pradesh", "Jammu and Kashmir", "Jharkhand", "Karnataka", "Kerala", "Madhya Pradesh", "Maharashtra", "Manipur", "Meghalaya", "Mizoram", "Nagaland", "Odisha", "Punjab", "Rajasthan", "Sikkim", "Tamil Nadu", "Telangana", "Tripura", "Uttarakhand", "Uttar Pradesh", "West Bengal", "Andaman and Nicobar Islands", "Chandigarh", "Dadra and Nagar Haveli", "Daman and Diu", "Delhi", "Lakshadweep", "Puducherry" };

                return ListOfStates;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public LineDetails GetLineDetails(string LineName)
        {
            try
            {
                LineDetails ObjLineDetails = new LineDetails();
                ObjLineDetails.LineName = LineName;
                int Index = ListLineDetails.BinarySearch(ObjLineDetails, ObjLineDetails);

                return Index < 0 ? null : ListLineDetails[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.GetLineDetails()", ex);
            }
            return null;
        }
        public DiscountGroupDetails1 GetDiscountGrpDetails(string DiscountGrpName)
        {
            try
            {
                DiscountGroupDetails1 ObjDiscountGroupDetails = new DiscountGroupDetails1();
                ObjDiscountGroupDetails.DiscountGrpName = DiscountGrpName;
                int Index = ListDiscountGroupDetails.BinarySearch(ObjDiscountGroupDetails, ObjDiscountGroupDetails);

                return Index < 0 ? null : ListDiscountGroupDetails[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.GetDiscountGrpDetails()", ex);
            }
            return null;
        }

        public PriceGroupDetails GetPriceGrpDetails(string PriceGrpName)
        {
            try
            {
                PriceGroupDetails ObjPriceGroupDetails = new PriceGroupDetails();
                ObjPriceGroupDetails.PriceGrpName = PriceGrpName;
                int Index = ListPriceGroupDetails.BinarySearch(ObjPriceGroupDetails, ObjPriceGroupDetails);

                return Index < 0 ? null : ListPriceGroupDetails[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.GetPriceGrpDetails()", ex);
            }
            return null;
        }
        public CustomerDetails GetCustomerDetails(String CustomerName)
        {
            try
            {
                CustomerDetails ObjCustomerDetails = new CustomerDetails();
                ObjCustomerDetails.CustomerName = CustomerName.Trim();

                Int32 Index = ListCustomerDetails.BinarySearch(ObjCustomerDetails, ObjCustomerDetails);
                if (Index < 0) return null;
                return ListCustomerDetails[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.GetCustomerDetails()", ex);
            }
            return null;
        }

        public Double GetCustomerDiscount(String CustomerName, Double Amount)
        {
            try
            {
                DiscountGroupDetails1 ObjDiscountGroupDetails = GetCustomerDiscount(CustomerName);
                if (ObjDiscountGroupDetails == null) return -1;

                return ObjDiscountGroupDetails.GetDiscountAmount(Amount);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.GetCustomerDiscount()", ex);
            }
            return -1;
        }

        public DiscountGroupDetails1 GetCustomerDiscount(String CustomerName)
        {
            try
            {
                CustomerDetails ObjCustomerDetails = GetCustomerDetails(CustomerName);
                if (ObjCustomerDetails == null) return null;
                Int32 DiscountGroupIndex = ObjCustomerDetails.DiscountGroupIndex;
                if (DiscountGroupIndex < 0) DiscountGroupIndex = DefaultDiscountGroupIndex;

                return ListDiscountGroupDetails[DiscountGroupIndex];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.GetCustomerDiscount()", ex);
            }
            return null;
        }

        public void SetCustomerDiscount(String CustomerName, DiscountGroupDetails1 DiscountGroup)
        {
            try
            {
                CustomerDetails ObjCustomerDetails = GetCustomerDetails(CustomerName);
                if (ObjCustomerDetails == null) return;
                Int32 DiscountGroupIndex = ObjCustomerDetails.DiscountGroupIndex;
                if (DiscountGroupIndex < 0) DiscountGroupIndex = DefaultDiscountGroupIndex;

                ListDiscountGroupDetails[DiscountGroupIndex] = DiscountGroup;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.SetCustomerDiscount()", ex);
            }
        }

        public List<String> GetCustomerList()
        {
            try
            {
                return ListCustomerDetails.Select(e => e.CustomerName).ToList();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.GetCustomerList()", ex); 
            }
            return null;
        }
        public void AddAllStatesToMasterTable()
        {
            try
            {
                List<string> ListOfStates = new List<string>() { "Andhra Pradesh", "Arunachal Pradesh", "Assam", "Bihar", "Chhattisgarh", "Goa", "Gujarat", "Haryana", "Himachal Pradesh", "Jammu and Kashmir", "Jharkhand", "Karnataka", "Kerala", "Madhya Pradesh", "Maharashtra", "Manipur", "Meghalaya", "Mizoram", "Nagaland", "Odisha", "Punjab", "Rajasthan", "Sikkim", "Tamil Nadu", "Telangana", "Tripura", "Uttarakhand", "Uttar Pradesh", "West Bengal", "Andaman and Nicobar Islands", "Chandigarh", "Dadra and Nagar Haveli", "Daman and Diu", "Delhi", "Lakshadweep", "Puducherry" };
                List<string> ListOfStatesCode = new List<string>() { "AP", "AR", "AS", "BR", "CT", "GA", "GJ", "HR", "HP", "JK", "JH", "KA", "KL", "MP", "MH", "MN", "ML", "MZ", "NL", "OR", "PB", "RJ", "SK", "TN", "TG", "TR", "UT", "UP", "WB", "AN", "CH", "DN", "DD", "DL", "LD", "PY"};
                String CreateStateQuery = "";
                for (int i = 0; i < ListOfStates.Count; i++)
                {
                    CreateStateQuery = "";
                    CreateStateQuery = "INSERT INTO STATEMASTER (STATE,STATECODE) VALUES ('" + ListOfStates[i] + "'" + ",'" + ListOfStatesCode[i] + "'" + ")";
                    CreateStateQuery += ";";
                    ObjMySQLHelper.ExecuteNonQuery(CreateStateQuery);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.AddAllStatesToMasterTable()", ex);
                throw ex;
            }
        }

        public Int32 CreateNewLine(string LineName, string Description)
        {
            try
            {
                String Query = "SELECT LINEID FROM LINEMASTER WHERE LOWER(LINENAME) = LOWER('" + LineName + "')";
                Query += ";";
                object ObjLineID = ObjMySQLHelper.ExecuteScalar(Query);

                int tmpLineID = ObjLineID == null ? -1 : (int)ObjLineID;
                if (tmpLineID != -1)
                {
                    return 2;
                }
                Query = "INSERT INTO LINEMASTER (LINENAME,DESCRIPTION"
                    + ")"
                    + "VALUES (@linename, @description"
                    + ")"
                    ;
                Query += ";";
                ObjMySQLHelper.ObjDbCommand.CommandText = Query;
                ObjMySQLHelper.ObjDbCommand.Parameters.Clear();
                ObjMySQLHelper.ObjDbCommand.Parameters.Add("@linename", MySqlDbType.VarChar).Value = LineName;
                ObjMySQLHelper.ObjDbCommand.Parameters.Add("@description", MySqlDbType.VarChar).Value = Description;
                return ObjMySQLHelper.ObjDbCommand.ExecuteNonQuery(); 
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.CreateNewLine()", ex);
                throw ex;
            }
        }
        public int GetPriceGrpID(string PriceGrpName)
        {
            try
            {
                PriceGroupDetails ObjPriceGrpDetails = new PriceGroupDetails();
                ObjPriceGrpDetails.PriceGrpName = PriceGrpName;
                if (ListPriceGroupDetails.Count == 0)
                {
                    CommonFunctions.ObjCustomerMasterModel.LoadPriceGroupMaster(ObjMySQLHelper.GetQueryResultInDataTable("SELECT * FROM PRICEGROUPMASTER;"));
                }
                int Index = ListPriceGroupDetails.BinarySearch(ObjPriceGrpDetails, ObjPriceGrpDetails);
                if (Index < 0 && PriceGrpName!= "Select Price Group") MessageBox.Show("Error!! Price Group Name Not Found", "Error");
                return Index < 0 ? -1 : ListPriceGroupDetails[Index].PriceGroupID;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.GetPriceGrpID()", ex);
                throw ex;
            }
        }
        public int GetDisGrpID(string DiscGrpName)
        {
            try
            {
                DiscountGroupDetails1 ObjDiscGrpDetails = new DiscountGroupDetails1();
                ObjDiscGrpDetails.DiscountGrpName = DiscGrpName;
                if (ListDiscountGroupDetails.Count == 0)
                {
                    CommonFunctions.ObjCustomerMasterModel.LoadDiscountGroupMaster(ObjMySQLHelper.GetQueryResultInDataTable("SELECT * FROM DISCOUNTGROUPMASTER;"));
                }
                int Index = ListDiscountGroupDetails.BinarySearch(ObjDiscGrpDetails, ObjDiscGrpDetails);
                if (Index < 0 && DiscGrpName != "Select Discount Group") MessageBox.Show("Error!! Dicount Group Name Not Found", "Error");
                return Index < 0 ? -1 : ListDiscountGroupDetails[Index].DiscountGrpID;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.GetDisGrpID()", ex);
                throw ex;
            }
        }
        public int GetLineID(string LineName)
        {
            try
            {
                LineDetails ObjLineDetails = new LineDetails();
                ObjLineDetails.LineName = LineName;
                if (ListLineDetails.Count == 0)
                {
                    CommonFunctions.ObjCustomerMasterModel.LoadLineMaster(ObjMySQLHelper.GetQueryResultInDataTable("SELECT * FROM LINEMASTER;"));
                }
                int Index = ListLineDetails.BinarySearch(ObjLineDetails, ObjLineDetails);
                if (Index < 0 && LineName != "Select Line") MessageBox.Show("Error!! LineName Not Found", "Error");
                return Index < 0 ? -1 : ListLineDetails[Index].LineID;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.GetLineID()", ex);
                throw ex;
            }
        }
        public int GetStateID(string StateName)
        {
            try
            {
                StateDetails ObjStateDetails = new StateDetails();
                ObjStateDetails.State = StateName;
                if (ListStateDetails.Count == 0)
                {
                    CommonFunctions.ObjCustomerMasterModel.LoadStateMaster(ObjMySQLHelper.GetQueryResultInDataTable("SELECT * FROM STATEMASTER;"));
                }
                int Index = ListStateDetails.BinarySearch(ObjStateDetails, ObjStateDetails);
                if (Index < 0 && StateName != "Select State") MessageBox.Show("Error!! StateName Not Found", "Error");
                return Index < 0 ? -1 : ListStateDetails[Index].StateID;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.GetStateID()", ex);
                throw ex;
            }
        }
        public int GetOrderDaysCode(string SelectedOrderDay)
        {
            try
            {
                switch (SelectedOrderDay.ToUpper())
                {
                    case "MONDAY": return 1;
                    case "TUESDAY": return 2;
                    case "WEDNESDAY": return 3;
                    case "THURSDAY": return 4;
                    case "FRIDAY": return 5;
                    case "SATURDAY": return 6;
                    case "SUNDAY": return 7;
                    default: return 1;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.GetOrderDaysCode()", ex);
            }
            return 1;
        }

        public string GetOrderDaysFromCode(int OrderDayCode)
        {
            try
            {
                switch (OrderDayCode)
                {
                    case 1: return "MONDAY";
                    case 2: return "TUESDAY";
                    case 3: return "WEDNESDAY";
                    case 4: return "THURSDAY";
                    case 5: return "FRIDAY";
                    case 6: return "SATURDAY";
                    case 7: return "SUNDAY";
                    default: return "MONDAY";
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.GetOrderDaysFromCode()", ex);
            }
            return "MONDAY";
        }
        public Int32 CreateNewCustomer(string CustomerName, string LineName, string DisGrpName, string PriceGrpName, bool Active, List<string> ListColumnNamesWthDataType = null, List<string> ListColumnValues = null)
        {
            try
            {
                string Query = "SELECT CUSTOMERNAME FROM CUSTOMERMASTER WHERE LOWER(CUSTOMERNAME) = LOWER('" + CustomerName + "');";
                ObjMySQLHelper.ObjDbCommand.CommandText = Query;
                object ObjCustomerName = ObjMySQLHelper.ObjDbCommand.ExecuteScalar();

                string tmpCustomerName = ObjCustomerName == null ? "" : ObjCustomerName.ToString();
                if (tmpCustomerName != string.Empty)
                {
                    return 2;
                }
                int tmpLineID = GetLineID(LineName);
                int tmpDisGrpID = GetDisGrpID(DisGrpName);
                int tmpPriceGrpID = GetPriceGrpID(PriceGrpName);
                List<string> ListColumnNameParamStr = new List<string>(), ListColumnNames = new List<string>(), ListColumnDataType = new List<string>();
                if (ListColumnNamesWthDataType == null) ListColumnNamesWthDataType = new List<string>();
                if (ListColumnValues == null) ListColumnValues = new List<string>();
                for (int i = 0; i < ListColumnNamesWthDataType.Count; i++)
                {
                    string[] col = ListColumnNamesWthDataType[i].Split(',');
                    if (i == 0)
                    {
                        ListColumnNames.Add("," + col[0]);
                        ListColumnNameParamStr.Add("," + "@" + col[0].ToLower());
                    }
                    else
                    {
                        ListColumnNames.Add(col[0]);
                        ListColumnNameParamStr.Add("@" + col[0].ToLower());
                    }
                    ListColumnDataType.Add(col[1]);

                }

                Query = "INSERT INTO CUSTOMERMASTER (CUSTOMERNAME,LINEID,DISCOUNTGROUPID,PRICEGROUPID,ACTIVE"
                    + string.Join(",", ListColumnNames)
                    + ")"
                    + "VALUES (@customername, @lineid, @discountgrpid,@pricegrpid,@active"
                    + string.Join(",", ListColumnNameParamStr)
                    + ")"
                    ;
                Query += ";";
                ObjMySQLHelper.ObjDbCommand.CommandText = Query;
                ObjMySQLHelper.ObjDbCommand.Parameters.Clear();
                ObjMySQLHelper.ObjDbCommand.Parameters.Add("@customername", MySqlDbType.VarChar).Value = CustomerName;
                ObjMySQLHelper.ObjDbCommand.Parameters.Add("@lineid", MySqlDbType.Int16).Value = tmpLineID;
                ObjMySQLHelper.ObjDbCommand.Parameters.Add("@discountgrpid", MySqlDbType.Int16).Value = tmpDisGrpID;
                ObjMySQLHelper.ObjDbCommand.Parameters.Add("@pricegrpid", MySqlDbType.Int16).Value = tmpPriceGrpID;
                ObjMySQLHelper.ObjDbCommand.Parameters.Add("@active", MySqlDbType.Bit).Value = Active;

                for (int i = 0; i < ListColumnNamesWthDataType.Count; i++)
                {
                    ObjMySQLHelper.ObjDbCommand.Parameters.Add(ListColumnNameParamStr[i].Replace(",", ""), MySQLHelper.GetMySqlDbType(ListColumnDataType[i])).Value = ListColumnValues[i];
                }
                return ObjMySQLHelper.ObjDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.CreateNewCustomer()", ex);
                throw ex;
            }
        }
        public Int32 CreateNewDiscountGrp(string DiscountGroupName, string Description, List<string> ListColumnNamesWthDataType = null, List<string> ListColumnValues = null)
        {
            try
            {
                String Query = "SELECT DISCOUNTGROUPID FROM DISCOUNTGROUPMASTER WHERE LOWER(DISCOUNTGROUPNAME) = LOWER('" + DiscountGroupName + "')";
                Query += ";";
                object ObjDiscountGrpID = ObjMySQLHelper.ExecuteScalar(Query);

                int tmpDiscountGrpID = ObjDiscountGrpID == null ? -1 : (int)ObjDiscountGrpID;
                if (tmpDiscountGrpID != -1)
                {
                    return 2;
                }
                List<string> ListColumnNameParamStr = new List<string>(), ListColumnNames = new List<string>(), ListColumnDataType = new List<string>();
                if (ListColumnNamesWthDataType == null) ListColumnNamesWthDataType = new List<string>();
                if (ListColumnValues == null) ListColumnValues = new List<string>();
                for (int i = 0; i < ListColumnNamesWthDataType.Count; i++)
                {
                    string[] col = ListColumnNamesWthDataType[i].Split(',');
                    if (i == 0)
                    {
                        ListColumnNames.Add("," + col[0]);
                        ListColumnNameParamStr.Add("," + "@" + col[0].ToLower());
                    }
                    else
                    {
                        ListColumnNames.Add(col[0]);
                        ListColumnNameParamStr.Add("@" + col[0].ToLower());
                    }
                    ListColumnDataType.Add(col[1]);

                }

                //ID	DiscountGroupName	Description		Discount	DiscountType	Default
                Query = "INSERT INTO DISCOUNTGROUPMASTER (DISCOUNTGROUPNAME,DESCRIPTION"
                    + string.Join(",", ListColumnNames)
                    + ")"
                    + "VALUES (@discountgroupname, @description"
                    + string.Join(",", ListColumnNameParamStr)
                    + ")"
                    ;
                Query += ";";
                ObjMySQLHelper.ObjDbCommand.CommandText = Query;
                ObjMySQLHelper.ObjDbCommand.Parameters.Clear();
                ObjMySQLHelper.ObjDbCommand.Parameters.Add("@discountgroupname", MySqlDbType.VarChar).Value = DiscountGroupName;
                ObjMySQLHelper.ObjDbCommand.Parameters.Add("@description", MySqlDbType.VarChar).Value = Description;

                for (int i = 0; i < ListColumnNamesWthDataType.Count; i++)
                {
                    ObjMySQLHelper.ObjDbCommand.Parameters.Add(ListColumnNameParamStr[i].Replace(",", ""), MySQLHelper.GetMySqlDbType(ListColumnDataType[i])).Value = ListColumnValues[i];
                }

                return ObjMySQLHelper.ObjDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.CreateNewDiscountGrp()", ex);
                throw ex;
            }
        }
        public Int32 CreateNewPriceGrp(string PriceGroupName, string Description, List<string> ListColumnNamesWthDataType = null, List<string> ListColumnValues = null)
        {
            try
            {
                String Query = "SELECT PRICEGROUPID FROM PRICEGROUPMASTER WHERE LOWER(PRICEGROUPNAME) = LOWER('" + PriceGroupName + "')";
                Query += ";";
                object ObjPriceGrpID = ObjMySQLHelper.ExecuteScalar(Query);

                int tmpPriceGrpID = ObjPriceGrpID == null ? -1 : (int)ObjPriceGrpID;
                if (tmpPriceGrpID != -1)
                {
                    return 2;
                }
                List<string> ListColumnNameParamStr = new List<string>(), ListColumnNames = new List<string>(), ListColumnDataType = new List<string>();
                if (ListColumnNamesWthDataType == null) ListColumnNamesWthDataType = new List<string>();
                if (ListColumnValues == null) ListColumnValues = new List<string>();
                for (int i = 0; i < ListColumnNamesWthDataType.Count; i++)
                {
                    string[] col = ListColumnNamesWthDataType[i].Split(',');
                    if (i == 0)
                    {
                        ListColumnNames.Add("," + col[0]);
                        ListColumnNameParamStr.Add("," + "@" + col[0].ToLower());
                    }
                    else
                    {
                        ListColumnNames.Add(col[0]);
                        ListColumnNameParamStr.Add("@" + col[0].ToLower());
                    }
                    ListColumnDataType.Add(col[1]);

                }

                //ID	PriceGroupName	Description	PriceColumn	Discount	DiscountType	Default
                Query = "INSERT INTO PRICEGROUPMASTER (PRICEGROUPNAME,DESCRIPTION"
                    + string.Join(",", ListColumnNames)
                    + ")"
                    + " VALUES (@pricegroupname, @description"
                    + string.Join(",", ListColumnNameParamStr)
                    + ")"
                    ;
                Query += ";";
                ObjMySQLHelper.ObjDbCommand.CommandText = Query;
                ObjMySQLHelper.ObjDbCommand.Parameters.Clear();
                ObjMySQLHelper.ObjDbCommand.Parameters.Add("@pricegroupname", MySqlDbType.VarChar).Value = PriceGroupName;
                ObjMySQLHelper.ObjDbCommand.Parameters.Add("@description", MySqlDbType.VarChar).Value = Description;

                for (int i = 0; i < ListColumnNamesWthDataType.Count; i++)
                {
                    ObjMySQLHelper.ObjDbCommand.Parameters.Add(ListColumnNameParamStr[i].Replace(",", ""), MySQLHelper.GetMySqlDbType(ListColumnDataType[i])).Value = ListColumnValues[i];
                }

                return ObjMySQLHelper.ObjDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.CreateNewPriceGrp()", ex);
                throw ex;
            }
        }

        public void LoadAllCustomerMasterTables()
        {
            try
            {

                string Query = "SELECT * FROM LINEMASTER;";
                DataTable dtLineMaster = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                LoadLineMaster(dtLineMaster);

                Query = "SELECT * FROM DISCOUNTGROUPMASTER;";  
                DataTable dtDiscountGroupMaster = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                LoadDiscountGroupMaster(dtDiscountGroupMaster);

                Query = "SELECT * FROM PRICEGROUPMASTER;";
                DataTable dtPriceGroupMaster = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                LoadPriceGroupMaster(dtPriceGroupMaster);

                Query = "SELECT * FROM STATEMASTER;";
                DataTable dtStateMaster = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                LoadStateMaster(dtStateMaster);

                // Query = "SELECT CUSTOMERID,LINEID,DISCOUNTGROUPID,PRICEGROUPID,STATEID,CUSTOMERNAME,ADDRESS,PHONENO,GSTIN,ORDERDAYS,IF(ACTIVE='1','true','false')ACTIVE,ADDEDDATE,LASTUPDATEDATE FROM CUSTOMERMASTER;";
                Query = "SELECT * FROM CUSTOMERMASTER";
                DataTable dtCustomerMaster = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                LoadCustomerMaster(dtCustomerMaster);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.LoadAllCustomerMasterTables()", ex);
                throw ex;
            }
        }

        public void LoadLineMaster(DataTable dtLineMaster)
        {
            try
            {
                ListLineDetails.Clear();
                for (int i = 0; i < dtLineMaster.Rows.Count; i++)
                {
                    DataRow dr = dtLineMaster.Rows[i];

                    LineDetails ObjLineDetails = new LineDetails();
                    ObjLineDetails.LineID = ((dr["LINEID"] == null) || dr["LINEID"].ToString().Trim() == "") ? -1 : int.Parse(dr["LINEID"].ToString().Trim());
                    ObjLineDetails.LineName = dr["LINENAME"].ToString().Trim();
                    ObjLineDetails.LineDescription = dr["DESCRIPTION"].ToString();
                  

                    AddLineDetailsToCache(ObjLineDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.LoadLineMaster()", ex);
                throw ex;
            }
        }
        public void AddLineDetailsToCache(LineDetails ObjLineDetails)
        {
            try
            {
                Int32 LineIndex = ListLineDetails.BinarySearch(ObjLineDetails, ObjLineDetails);
                if (LineIndex < 0)
                {
                    ListLineDetails.Insert(~LineIndex, ObjLineDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.AddLineDetailsToCache()", ex);
            }
        }

        public void LoadStateMaster(DataTable dtStateMaster)
        {
            try
            {
                ListStateDetails.Clear();
                for (int i = 0; i < dtStateMaster.Rows.Count; i++)
                {
                    DataRow dr = dtStateMaster.Rows[i];

                    StateDetails ObjStateDetails = new StateDetails();
                    ObjStateDetails.StateID = dr["STATEID"] == null ? -1 : Int32.Parse(dr["STATEID"].ToString());
                    ObjStateDetails.State = dr["STATE"].ToString().Trim();
                    ObjStateDetails.StateCode = dr["STATECODE"].ToString().Trim();
                    AddStateDetailsToCache(ObjStateDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.LoadStateMaster()", ex);
                throw ex;
            }
        }
        public void AddStateDetailsToCache(StateDetails ObjStateDetails)
        {
            try
            {
                Int32 StateIndex = ListStateDetails.BinarySearch(ObjStateDetails, ObjStateDetails);
                if (StateIndex < 0)
                {
                    ListStateDetails.Insert(~StateIndex, ObjStateDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.AddStateDetailsToCache()", ex);
            }
        }

        public void LoadDiscountGroupMaster(DataTable dtDiscountGroupMaster)
        {
            try
            {
                ListDiscountGroupDetails.Clear();
                for (int i = 0; i < dtDiscountGroupMaster.Rows.Count; i++)
                {
                    DataRow dr = dtDiscountGroupMaster.Rows[i];

                    DiscountGroupDetails1 ObjDiscountGroupDetails = new DiscountGroupDetails1();
                    ObjDiscountGroupDetails.DiscountGrpID = ((dr["DISCOUNTGROUPID"] == null) || dr["DISCOUNTGROUPID"].ToString().Trim() == "") ? -1 : int.Parse(dr["DISCOUNTGROUPID"].ToString().Trim());
                    ObjDiscountGroupDetails.DiscountGrpName = dr["DISCOUNTGROUPNAME"].ToString().Trim();
                    ObjDiscountGroupDetails.DiscountType = PriceGroupDetails.GetDiscountType(dr["DISCOUNTTYPE"].ToString().Trim());
                    ObjDiscountGroupDetails.Discount = Double.Parse(dr["DISCOUNT"].ToString().Trim());
                    if (dr["ISDEFAULT"].ToString().Trim() != "") ObjDiscountGroupDetails.IsDefault = dr["ISDEFAULT"].ToString().Trim() == "1" ? true : false;
                    ObjDiscountGroupDetails.Description = dr["DESCRIPTION"].ToString();

                    AddDiscountGroupToCache(ObjDiscountGroupDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.LoadDiscountGroupMaster()", ex);
            }
        }
        //Already in discount,vendor etc class
        public void AddDiscountGroupToCache(DiscountGroupDetails1 ObjDiscountGroupDetails)
        {
            try
            {
                Int32 Index = ListDiscountGroupDetails.BinarySearch(ObjDiscountGroupDetails, ObjDiscountGroupDetails);
                if (Index < 0)
                {
                    ListDiscountGroupDetails.Insert(~Index, ObjDiscountGroupDetails);
                    DefaultDiscountGroupIndex = ListDiscountGroupDetails.FindIndex(e => e.IsDefault);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.AddDiscountGroupToCache()", ex);
            }
        }
        //&&&&& already in product
        public void LoadPriceGroupMaster(DataTable dtPriceGroupMaster)
        {
            try
            {
                ListPriceGroupDetails.Clear();
                for (int i = 0; i < dtPriceGroupMaster.Rows.Count; i++)
                {
                    DataRow dr = dtPriceGroupMaster.Rows[i];
                    PriceGroupDetails ObjPriceGroupDetails = new PriceGroupDetails();
                    ObjPriceGroupDetails.PriceGroupID= ((dr["PRICEGROUPID"] == null) || dr["PRICEGROUPID"].ToString().Trim() == "") ? -1 : int.Parse(dr["PRICEGROUPID"].ToString().Trim());
                    ObjPriceGroupDetails.PriceGrpName = dr["PRICEGROUPNAME"].ToString().Trim();
                    ObjPriceGroupDetails.Discount = Double.Parse(dr["DISCOUNT"].ToString().Trim());
                    ObjPriceGroupDetails.DiscountType = PriceGroupDetails.GetDiscountType(dr["DISCOUNTTYPE"].ToString().Trim());
                    if (dr["ISDEFAULT"].ToString().Trim() != "") ObjPriceGroupDetails.IsDefault = dr["ISDEFAULT"].ToString().Trim() == "1" ? true : false;
                    ObjPriceGroupDetails.PriceColumn = dr["PRICECOLUMN"].ToString().Trim();
                    ObjPriceGroupDetails.Description = dr["DESCRIPTION"].ToString();

                    AddPriceGroupToCache(ObjPriceGroupDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.LoadPriceGroupMaster()", ex);
            }
        }
        public void AddPriceGroupToCache(PriceGroupDetails ObjPriceGroupDetails)
        {
            try
            {
                Int32 Index = ListPriceGroupDetails.BinarySearch(ObjPriceGroupDetails, ObjPriceGroupDetails);
                if (Index < 0)
                {
                    ListPriceGroupDetails.Insert(~Index, ObjPriceGroupDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.AddPriceGroupToCache()", ex);
            }
        }

        public void LoadCustomerMaster(DataTable dtCustomerMaster)
        {
            try
            {
                ListCustomerDetails.Clear();
                for (int i = 0; i < dtCustomerMaster.Rows.Count; i++)
                {
                    DataRow dtRow = dtCustomerMaster.Rows[i];
                    CustomerDetails ObjCustomerDetails = new CustomerDetails();
                    ObjCustomerDetails.CustomerID = int.Parse(dtRow["CUSTOMERID"].ToString().Trim());
                    ObjCustomerDetails.CustomerName = dtRow["CUSTOMERNAME"].ToString();
                    ObjCustomerDetails.Address = dtRow["ADDRESS"].ToString();
                    ObjCustomerDetails.GSTIN = dtRow["GSTIN"].ToString();
                    ObjCustomerDetails.PhoneNo = ((dtRow["PHONENO"] == null) || dtRow["PHONENO"].ToString().Trim() == "") ? 0 : Int64.Parse(dtRow["PHONENO"].ToString().Trim());
                    ObjCustomerDetails.Active = bool.Parse(dtRow["ACTIVE"].ToString().Trim() == "1" ? "true" : "false");
                    ObjCustomerDetails.StateID = ((dtRow["STATEID"] == null) || dtRow["STATEID"].ToString().Trim() == "") ? -1 : int.Parse(dtRow["STATEID"].ToString().Trim());
                    ObjCustomerDetails.OrderDaysAssigned = dtRow["ORDERDAYS"].ToString();
                    ObjCustomerDetails.LastUpdateDate = ((dtRow["LASTUPDATEDATE"] == null) || dtRow["LASTUPDATEDATE"].ToString().Trim() == "") ? DateTime.MinValue : DateTime.Parse(dtRow["LASTUPDATEDATE"].ToString());
                    ObjCustomerDetails.AddedDate = ((dtRow["ADDEDDATE"] == null) || dtRow["ADDEDDATE"].ToString().Trim() == "") ? DateTime.MinValue : DateTime.Parse(dtRow["ADDEDDATE"].ToString());

                    ObjCustomerDetails.LineID = ((dtRow["LINEID"] == null) || dtRow["LINEID"].ToString().Trim() == "") ? -1 : int.Parse(dtRow["LINEID"].ToString().Trim());
                    ObjCustomerDetails.DiscountGroupID = ((dtRow["DISCOUNTGROUPID"] == null) || dtRow["DISCOUNTGROUPID"].ToString().Trim() == "") ? -1 : int.Parse(dtRow["DISCOUNTGROUPID"].ToString().Trim());
                    ObjCustomerDetails.PriceGroupID = ((dtRow["PRICEGROUPID"] == null) || dtRow["PRICEGROUPID"].ToString().Trim() == "") ? -1 : int.Parse(dtRow["PRICEGROUPID"].ToString().Trim());
                    if (ObjCustomerDetails.LineID != -1) ObjCustomerDetails.LineName = ListLineDetails.Where(e => e.LineID.Equals(ObjCustomerDetails.LineID)).FirstOrDefault().LineName;
                    if (ObjCustomerDetails.DiscountGroupID != -1) ObjCustomerDetails.DiscountGroupName = ListDiscountGroupDetails.Where(e => e.DiscountGrpID.Equals(ObjCustomerDetails.DiscountGroupID)).FirstOrDefault().DiscountGrpName;
                    if (ObjCustomerDetails.PriceGroupID != -1) ObjCustomerDetails.PriceGroupName = ListPriceGroupDetails.Where(e => e.PriceGroupID.Equals(ObjCustomerDetails.PriceGroupID)).FirstOrDefault().PriceGrpName;
                    if (ObjCustomerDetails.StateID != -1) ObjCustomerDetails.State = ListStateDetails.Where(e => e.StateID.Equals(ObjCustomerDetails.StateID)).FirstOrDefault().State;


                    AddCustomerDataToCache(ObjCustomerDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.LoadCustomerMaster()", ex);
            }
        }
        public void AddCustomerDataToCache(CustomerDetails ObjCustomerDetails)
        {
            try
            {
                Int32 Index = ListCustomerDetails.BinarySearch(ObjCustomerDetails, ObjCustomerDetails);
                if (Index < 0)
                {
                    ListCustomerDetails.Insert(~Index, ObjCustomerDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.AddCustomerDataToCache()", ex);
            }
        }

        public Int32 GetLatestColValFromTable(string ColName,string TableName)
        {
            try
            {
                String Query = "SELECT MAX("+ ColName+") FROM "+ TableName;
                Query += ";";
                ObjMySQLHelper.ObjDbCommand.CommandText = Query;
                Object Val = ObjMySQLHelper.ObjDbCommand.ExecuteScalar();

                return (Val == null) || (Val.ToString()==string.Empty) ? -1 : int.Parse(Val.ToString());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.GetLatestColValFromTable()", ex);
            }
            return 0;
        }

        public void FillLineDBFromCache(List<LineDetails> ListLineDtls=null)
        {
            try
            {
                List<LineDetails> listtemp = ListLineDtls;
                if (ListLineDtls == null) listtemp = ListLineDetails;
                string Query = "";
                for (int i = 0; i < listtemp.Count; i++)
                {
                    //Query = "INSERT INTO PRICEGROUPMASTER (PRICEGROUPNAME,DESCRIPTION)";
                    Query = "INSERT INTO LINEMASTER (LINEID,LINENAME,DESCRIPTION) VALUES (" + listtemp[i].LineID + ",'" + listtemp[i].LineName + "','" + listtemp[i].LineDescription + "')";
                    Query += ";";
                    ObjMySQLHelper.ObjDbCommand.CommandText = Query;
                    ObjMySQLHelper.ObjDbCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.FillLineDBFromCache()", ex);
            }
        }
        public void FillPriceGroupDBFromCache(List<PriceGroupDetails> ListPGDtls = null)
        {
            try
            {
                List<PriceGroupDetails> listtemp = ListPGDtls;
                if (ListPGDtls == null) listtemp = ListPriceGroupDetails;
                string Query = "";
                for (int i = 0; i < listtemp.Count; i++)
                {
                    //Query = "";
                    Query = "INSERT INTO PRICEGROUPMASTER (PRICEGROUPID,PRICEGROUPNAME,DESCRIPTION,DISCOUNT,DISCOUNTTYPE,ISDEFAULT,PRICECOLUMN) VALUES (" + listtemp[i].PriceGroupID + ",'" + listtemp[i].PriceGrpName + "','" + listtemp[i].Description + "'," + listtemp[i].Discount + ",'" + listtemp[i].DiscountType + "'," + (listtemp[i].IsDefault == true ? 1 : 0) + ",'" + listtemp[i].PriceColumn + "')";
                    Query += ";";
                    ObjMySQLHelper.ObjDbCommand.CommandText = Query;
                    ObjMySQLHelper.ObjDbCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.FillPriceGroupDBFromCache()", ex);
            }
        }
        public void FillDiscountGroupDBFromCache(List<DiscountGroupDetails1> ListDiscountGrpDtls = null)
        {
            try
            {
                List<DiscountGroupDetails1> listtemp = ListDiscountGrpDtls;
                if (ListDiscountGrpDtls == null) listtemp = ListDiscountGroupDetails;
                string Query = "";
                for (int i = 0; i < listtemp.Count; i++)
                {
                    Query = "INSERT INTO DISCOUNTGROUPMASTER (DISCOUNTGROUPID,DISCOUNTGROUPNAME,DESCRIPTION,DISCOUNT,DISCOUNTTYPE,ISDEFAULT) VALUES (" + listtemp[i].DiscountGrpID + ",'" + listtemp[i].DiscountGrpName + "','" + listtemp[i].Description + "'," + listtemp[i].Discount + ",'" + listtemp[i].DiscountType + "'," + (listtemp[i].IsDefault == true ? 1 : 0) + ")";
                    Query += ";";
                    ObjMySQLHelper.ObjDbCommand.CommandText = Query;
                    ObjMySQLHelper.ObjDbCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.FillDiscountGroupDBFromCache()", ex);
            }
        }

        public void FillCustomerDBFromCache(List<CustomerDetails> ListCustDtls = null)
        {
            try
            {
                List<CustomerDetails> listtemp = ListCustDtls;
                if (ListCustDtls == null) listtemp = ListCustomerDetails;
                string Query = "";
                for (int i = 0; i < listtemp.Count; i++)
                {
                    // "CUSTOMERID", "LINEID", "DISCOUNTGROUPID", "PRICEGROUPID", "STATEID", "CUSTOMERNAME", "ADDRESS", "STATE", "PHONENO", "GSTIN", "ORDERDAYS", "LINENAME", "DISCOUNTGROUPNAME", "PRICEGROUPNAME", "ACTIVE", "ADDEDDATE", "LASTUPDATEDATE" };
                    //Query = "INSERT INTO CUSTOMERMASTER (CUSTOMERID,LINEID,DISCOUNTGROUPID,PRICEGROUPID,STATEID,CUSTOMERNAME, ADDRESS, STATE, PHONENO, GSTIN, ORDERDAYS,LINENAME,DISCOUNTGROUPNAME,PRICEGROUPNAME,ACTIVE,ADDEDDATE,LASTUPDATEDATE) VALUES (" 
                    //                            + listtemp[i].CustomerID + "," + listtemp[i].LineID + "," + listtemp[i].DiscountGroupID + "," + listtemp[i].PriceGroupID 
                    //                            + "," + listtemp[i].StateID + ",'" + listtemp[i].CustomerName + "','" + listtemp[i].Address + "','" + listtemp[i].State
                    //                            + "'," + listtemp[i].PhoneNo + ",'" + listtemp[i].GSTIN + "','" + listtemp[i].OrderDaysAssigned + "','" + listtemp[i].LineName + "','" + listtemp[i].DiscountGroupName + "','" + listtemp[i].PriceGroupName + "'," + (listtemp[i].Active==true?1:0) 
                    //                            + ",'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")+"'"
                    //                            +")";

                    Query = "INSERT INTO CUSTOMERMASTER (CUSTOMERID,LINEID,DISCOUNTGROUPID,PRICEGROUPID,STATEID,CUSTOMERNAME, ADDRESS, PHONENO, GSTIN, ORDERDAYS,ACTIVE,ADDEDDATE,LASTUPDATEDATE) VALUES ("
                                             + listtemp[i].CustomerID + "," + listtemp[i].LineID + "," + listtemp[i].DiscountGroupID + "," + listtemp[i].PriceGroupID
                                             + "," + listtemp[i].StateID + ",'" + listtemp[i].CustomerName + "','" + listtemp[i].Address
                                             + "'," + listtemp[i].PhoneNo + ",'" + listtemp[i].GSTIN + "','" + listtemp[i].OrderDaysAssigned + "'," + (listtemp[i].Active == true ? 1 : 0)
                                             + ",'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'"
                                             + ")";
                    Query += ";";
                    ObjMySQLHelper.ObjDbCommand.CommandText = Query;
                    ObjMySQLHelper.ObjDbCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CustomerMasterModel.FillCustomerDBFromCache()", ex);
            }
        }
    }
}
