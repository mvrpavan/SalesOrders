using MySql.Data.MySqlClient;
using SalesOrdersReport.CommonModules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesOrdersReport
{
    class UserMasterModel
    {
        List<UserDetails> ListUserDetails;
        List<RoleDetails> ListRoleDetails;
        List<PrivilegeDetails> ListPrivilegeDetails;
        List<StoreDetails> ListStoreDetails;

        MySQLHelper ObjMySQLHelper = null;
        public void Initialize()
        {
            try
            {
                ListUserDetails = new List<UserDetails>();
                ListRoleDetails = new List<RoleDetails>();
                ListPrivilegeDetails = new List<PrivilegeDetails>();
                ListStoreDetails = new List<StoreDetails>();
                ObjMySQLHelper = MySQLHelper.GetMySqlHelperObj();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.Initialize()", ex);
            }
        }
        public Int32 LoginCheck(string txtUserName, string txtPassword, MySqlConnection myConnection)
        {
            try
            {

                int ReturnVal = 0;
                MySqlCommand MySQLCommand = new MySqlCommand("SELECT USERNAME,PASSWORD,USERGUID,ACTIVE FROM USERMASTER WHERE USERNAME = @Username", myConnection);
                MySqlParameter UsrName = new MySqlParameter("@Username", MySqlDbType.VarChar);

                UsrName.Value = txtUserName;
                MySQLCommand.Parameters.Add(UsrName);
                MySqlDataReader MySQLReader = MySQLCommand.ExecuteReader();

                if (MySQLReader.Read() == true)
                {
                    string dbPassword = Convert.ToString(MySQLReader["PASSWORD"]);
                    bool ActiveVal = MySQLReader["ACTIVE"].ToString().Trim() == "1" ? true : false;
                 
                    if (!ActiveVal)
                    {
                        return -2;
                    }
                    // Now we hash the UserGuid from the database with the password we wan't to check
                    // In the same way as when we saved it to the database in the first place.
                    string HashedPassword = CommonFunctions.GetHashedPassword(txtPassword, Guid.Parse(MySQLReader["USERGUID"].ToString()));
                    // if its correct password the result of the hash is the same as in the database
                    if (dbPassword == HashedPassword)
                    {
                        // The password is correct
                        ObjMySQLHelper.CurrentUser = txtUserName;
                        ObjMySQLHelper.LoginTime = DateTime.Now;
                    }
                    else
                    {
                        ReturnVal = -1;
                    }
                }
                else
                {
                    ReturnVal = -1;
                }

                MySQLReader.Close();

                return ReturnVal;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.LoginCheck()", ex);
                throw ex;
            }
        }
        public bool ValidateOldPassword(string OldPwdStr)
        {
            try
            {
                string Query = "SELECT PASSWORD,USERGUID FROM USERMASTER WHERE USERNAME = '" + CommonFunctions.CurrentUserName + "'";
                Query += ";";
                MySqlDataReader myReader = (MySqlDataReader)ObjMySQLHelper.ExecuteReader(Query);
                myReader.Read();
                string dbPassword = Convert.ToString(myReader["PASSWORD"]);
                // Now we hash the UserGuid from the database with the password we wan't to check
                // In the same way as when we saved it to the database in the first place.
                string HashedPassword = CommonFunctions.GetHashedPassword(OldPwdStr, Guid.Parse(myReader["USERGUID"].ToString()));
                // if its correct password the result of the hash is the same as in the database
                myReader.Close();
                if (dbPassword == HashedPassword)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.ValidateOldPassword()", ex);
                throw;
            }
        }

        public List<string> GetAllUsers()
        {
            try
            {
                List<string> ListUsers = new List<string>();
                ListUsers.AddRange(ListUserDetails.Select(e => e.UserName).ToList());
                return ListUsers;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.GetAllUsers()", ex);
                throw ex;
            }
        }

        public List<string> GetAllRoles()
        {
            try
            {
                List<string> ListRoles = new List<string>();
                ListRoles.AddRange(ListRoleDetails.Select(e => e.RoleName).ToList());
                return ListRoles;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.GetAllRoles()", ex);
                throw ex;
            }

        }

        public List<string> GetAllStatus()
        {
            try
            {
                List<string> ListStatus = new List<string>();
                String GetAllStatusQuery = "SELECT ROLENAME FROM ROLE ";
                GetAllStatusQuery += ";";
                MySqlDataReader dr = (MySqlDataReader)ObjMySQLHelper.ExecuteReader(GetAllStatusQuery);
                while (dr.Read())
                {
                    ListStatus.Add(dr.GetValue(0).ToString());
                }
                dr.Close();
                return ListStatus;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.GetAllStatus()", ex);
                throw ex;
            }
        }

        public List<string> GetAllStores()
        {
            try
            {
                List<string> ListStores = new List<string>();
                ListStores.AddRange(ListStoreDetails.Select(e => e.StoreName).ToList());
                return ListStores;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.GetAllStores()", ex);
                throw ex;
            }
        }

        public List<string> GetAllPrivilegeIDs()
        {
            try
            {
                List<string> ListPrivilegesIDs = new List<string>();
                if (ListPrivilegeDetails.Count == 0)
                {
                    CommonFunctions.ObjUserMasterModel.LoadPrivilegeMaster(ObjMySQLHelper.GetQueryResultInDataTable("SELECT * FROM PRIVILEGEMASTER;"));
                }
                ListPrivilegesIDs.AddRange(ListPrivilegeDetails.Select(e => e.PrivilegeId).ToList());
                return ListPrivilegesIDs;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.GetAllPrivilegeIDs()", ex);
                throw ex;
            }
        }
        public List<string> GetAllPrivilegeNames()
        {
            try
            {
                List<string> ListPrivileges = new List<string>();
                ListPrivileges.AddRange(ListPrivilegeDetails.Select(e => e.PrivilegeName).ToList());
                return ListPrivileges;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.GetAllPrivilegeNames()", ex);
                throw ex;
            }
        }

        public List<PrivilegeControlDetails> GetListOfPrivilegeControlDtlObjBasedOnPrivilegeName(string PrivilegeName)
        {
            try
            {
                PrivilegeControlDetails ObjPrivilegeControlDetails = new PrivilegeControlDetails();
                int Index = ListPrivilegeDetails.FindIndex(e => e.PrivilegeName.Equals(PrivilegeName, StringComparison.InvariantCultureIgnoreCase));
                if (Index >= 0)
                {
                    return ListPrivilegeDetails[Index].ListPrivilegeControlDetails;
                }
                return null;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.GetListOfPrivilegeControlDtlObjBasedOnPrivilegeName()", ex);
                throw ex;
            }
        }

        public List<bool> GetAllPrivilegeValuesAssignedForARole(string RoleName)
        {
            try
            {
                List<bool> ListOfPrivilegesAssigned = new List<bool>();

                int Index = ListRoleDetails.FindIndex(e => e.RoleName.Equals(RoleName, StringComparison.InvariantCultureIgnoreCase));
                if (Index >= 0)
                {
                    ListOfPrivilegesAssigned = ListRoleDetails[Index].ListOfPrivilegesAssigned;
                }

                return ListOfPrivilegesAssigned;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.GetAllPrivilegeValuesAssignedForARole()", ex);
                throw ex;
            }
        }

        public List<string> GetOnlyAssignedPrivilegeNamesForAnUser(string UserName)
        {
            try
            {
                List<string> ListPrivilege = new List<string>();
                UserDetails ObjUserDtls = GetUserDtlsObjBasedOnUsrName(UserName);
                List<bool> ListPrivilegeValues = GetAllPrivilegeValuesAssignedForARole(ObjUserDtls.RoleName);

                List<string> ListAllPrivilegeNames = GetAllPrivilegeNames();
                for (int i = 0; i < ListAllPrivilegeNames.Count; i++)
                {
                    if (!ListPrivilegeValues[i])
                    {
                        ListPrivilege.Add(ListAllPrivilegeNames[i]);
                    }
                }

                return ListPrivilege;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.GetOnlyAssignedPrivilegeNamesForAnUser()", ex);
                throw ex;
            }
        }

        public int GetUserID(string UserName)
        {
            try
            {
                UserDetails ObjUserDetails = new UserDetails();
                ObjUserDetails.UserName = UserName;
                int Index = ListUserDetails.BinarySearch(ObjUserDetails, ObjUserDetails);

                return Index < 0 ? -1 : ListUserDetails[Index].UserID;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.GetUserID()", ex);
                throw ex;
            }
        }

        public int GetStoreID(string StoreName)
        {
            try
            {
                StoreDetails ObjStoreDetails = new StoreDetails();
                ObjStoreDetails.StoreName = StoreName;
                int Index = ListStoreDetails.BinarySearch(ObjStoreDetails, ObjStoreDetails);

                return Index < 0 ? -1 : ListStoreDetails[Index].StoreId;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.GetStoreID()", ex);
                throw ex;
            }
        }

        public int GetRoleID(string RoleName)
        {
            try
            {

                RoleDetails ObjRoleDetails = new RoleDetails();
                ObjRoleDetails.RoleName = RoleName;
                if (ListRoleDetails.Count == 0)
                {
                    CommonFunctions.ObjUserMasterModel.LoadRoleMaster(ObjMySQLHelper.GetQueryResultInDataTable("SELECT * FROM ROLEMASTER;"));
                }
                int Index = ListRoleDetails.BinarySearch(ObjRoleDetails, ObjRoleDetails);
                if (Index < 0) MessageBox.Show("Error!! RoleName Not Found", "Error");
                return Index < 0 ? -1 : ListRoleDetails[Index].RoleID;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.GetRoleID()", ex);
                throw ex;
            }
        }

        public string GetPrivilegeID(string PrivilegeName)
        {
            try
            {
                PrivilegeDetails ObjPrivilegeDetails = new PrivilegeDetails();
                ObjPrivilegeDetails.PrivilegeName = PrivilegeName;
                int Index = ListPrivilegeDetails.BinarySearch(ObjPrivilegeDetails, ObjPrivilegeDetails);

                return Index < 0 ? "" : ListPrivilegeDetails[Index].PrivilegeId;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.GetPrivilegeID()", ex);
                throw ex;
            }
        }

        public string GetPrivilegeName(string PrivilegeID)
        {
            try
            {
                int Index = ListPrivilegeDetails.FindIndex(e => e.PrivilegeId.Equals(PrivilegeID));
                if (Index >= 0)
                {
                    return ListPrivilegeDetails[Index].PrivilegeName;
                }
                return "";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.GetPrivilegeName()", ex);
                throw ex;
            }
        }

        public int GetPrivilegeIndex(string PrivilegeID)
        {
            try
            {
                int Index = ListPrivilegeDetails.FindIndex(e => e.PrivilegeId.Equals(PrivilegeID));

                return Index;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.GetPrivilegeName()", ex);
                throw ex;
            }
        }

        public string GetUserName(int UserID)
        {
            try
            {
                int Index = ListUserDetails.FindIndex(e => e.UserID.Equals(UserID));
                if (Index >= 0)
                {
                    return ListUserDetails[Index].UserName;
                }
                return "";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.GetUserName()", ex);
                throw ex;
            }
        }

        public UserDetails GetUserDtlsObjBasedOnUsrName(string UserName)
        {
            try
            {
                UserDetails ObjUserDetails = new UserDetails();
                int Index = ListUserDetails.FindIndex(e => e.UserName.Equals(UserName, StringComparison.InvariantCultureIgnoreCase));
                if (Index >= 0)
                {
                    return ListUserDetails[Index];
                }
                return null;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.GetUserDtlsObjBasedOnUsrName()", ex);
                throw ex;
            }
        }


        public Int32 CreateNewStore(string StoreName, List<string> ListColumnNamesWthDataType = null, List<string> ListColumnValues = null)///move to respective module
        {
            try
            {
                String Query = "SELECT STOREID FROM STOREMASTER WHERE LOWER(STORENAME) = LOWER('" + StoreName + "')";
                Query += ";";
                object ObjStoreID = ObjMySQLHelper.ExecuteScalar(Query);

                int tmpStoreID = ObjStoreID == null ? -1 : (int)ObjStoreID;
                if (tmpStoreID != -1)
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

                //USERID, USERNAME, PASSWORD, FULLNAME, ROLEID, LASTLOGIN, LASTPASSWORDCHANGED, LASTUPDATEDATE, ACTIVE, PHONENO, CREATEDBY, USERGUID
                Query = "INSERT INTO STOREMASTER (STORENAME"
                    + string.Join(",", ListColumnNames)
                    + ")"
                    + "VALUES (@storename"
                    + string.Join(",", ListColumnNameParamStr)
                    + ")"
                    ;
                Query += ";";
                ObjMySQLHelper.ObjDbCommand.CommandText = Query;
                ObjMySQLHelper.ObjDbCommand.Parameters.Clear();
                ObjMySQLHelper.ObjDbCommand.Parameters.Add("@storename", MySqlDbType.VarChar).Value = StoreName;

                for (int i = 0; i < ListColumnNamesWthDataType.Count; i++)
                {
                    ObjMySQLHelper.ObjDbCommand.Parameters.Add(ListColumnNameParamStr[i].Replace(",", ""), CommonFunctions.GetMySqlDbType(ListColumnDataType[i])).Value = ListColumnValues[i];
                }


                return ObjMySQLHelper.ObjDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.CreateNewStore()", ex);
                throw ex;
            }
        }

        public Int32 CreateNewPrivilege(string NewPrivilegeID, string NewPrivilegeName)///move to respective module
        {
            try
            {
                String CreatePrivilegeQuery = "INSERT INTO PRIVILEGEMASTER (PRIVILEGEID,PRIVILEGENAME) VALUES ('" + NewPrivilegeID + "'" + ",'" + NewPrivilegeName + "'" + ")";
                CreatePrivilegeQuery += ";";
                return ObjMySQLHelper.ExecuteNonQuery(CreatePrivilegeQuery);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.CreateNewPrivilege()", ex);
                throw ex;
            }
        }

        public Int32 CreateNewPrivilegeControl(string FormName, String ControlName, bool Enabled, string PrivilegeID)
        {
            try
            {
                String CreatePrivilegeControlQuery = "INSERT INTO PRIVILEGECONTROLMASTER (FORMNAME,CONTROLNAME,ENABLED,PRIVILEGEID) VALUES ('" + FormName + "'" + ",'" + ControlName + "'" + "," + (Enabled == true ? "1" : "0") + ",'" + PrivilegeID + "'" + ")";
                CreatePrivilegeControlQuery += ";";
                return ObjMySQLHelper.ExecuteNonQuery(CreatePrivilegeControlQuery);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.CreateNewPrivilegeControl()", ex);
                throw ex;
            }
        }
        public DataTable FillMangeUserCacheGrid()
        {
            try
            {
                DataTable dt = new DataTable();
                // //USERID, USERNAME, PASSWORD, FULLNAME,EMAILID, ROLEID, LASTLOGIN, LASTPASSWORDCHANGED, LASTUPDATEDATE, ACTIVE, PHONENO, CREATEDBY, USERGUID
                String UserGridCacheQuery = "SELECT USERID,USERNAME,FULLNAME,EMAILID,ROLEID,LASTLOGIN,LASTPASSWORDCHANGED,LASTUPDATEDATE,IF(ACTIVE='1','true','false')ACTIVE,PHONENO,CREATEDBY FROM USERMASTER";
                UserGridCacheQuery += ";";
                dt = ObjMySQLHelper.GetQueryResultInDataTable(UserGridCacheQuery);
                return dt;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.FillMangeUserCacheGrid()", ex);
                throw ex;
            }
        }

        public List<PrivilegeControlDetails> GetPrivilegeControlDetailsForAnUser(string UserName)
        {
            try
            {
                List<PrivilegeControlDetails> ListPrivilegeControlDtls = new List<PrivilegeControlDetails>();
                UserDetails ObjUserDtls = GetUserDtlsObjBasedOnUsrName(UserName);
                List<bool> ListPrivilegeValues = GetAllPrivilegeValuesAssignedForARole(ObjUserDtls.RoleName);
                List<string> ListAllPrivilegeNames = GetAllPrivilegeNames();
                for (int i = 0; i < ListAllPrivilegeNames.Count; i++)
                {
                    List<PrivilegeControlDetails> TempListPrivilegeControl = new List<PrivilegeControlDetails>(GetListOfPrivilegeControlDtlObjBasedOnPrivilegeName(ListAllPrivilegeNames[i]));

                    foreach (PrivilegeControlDetails item in TempListPrivilegeControl)
                    {
                        if (!ListPrivilegeValues[i])
                        {
                            item.IsEnabled = false;
                        }
                        else item.IsEnabled = true;
                    }

                    ListPrivilegeControlDtls.AddRange(TempListPrivilegeControl);
                }
                return ListPrivilegeControlDtls;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.GetPrivilegeControlDetailsForAnUser()", ex);
                throw ex;
            }
        }

        public void LoadAllUserMasterTables()
        {
            try
            {

                string Query = "SELECT * FROM PRIVILEGEMASTER;";
                DataTable dtPrivilegeMaster = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                LoadPrivilegeMaster(dtPrivilegeMaster);

                Query = "SELECT * FROM PRIVILEGECONTROLMASTER;";  //CheckOnce   everytime its hould be load
                DataTable dtPrivilegeControlMaster = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                LoadPrivilegeControlMaster(dtPrivilegeControlMaster);

                Query = "SELECT * FROM ROLEMASTER;";
                DataTable dtRoleMaster = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                LoadRoleMaster(dtRoleMaster);

                Query = "SELECT * FROM STOREMASTER;";
                DataTable dtStoreMaster = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                LoadStoreMaster(dtStoreMaster);

                Query = "SELECT USERID,ROLEID,STOREID,USERNAME,FULLNAME,EMAILID,LASTLOGIN,LASTPASSWORDCHANGED,LASTUPDATEDATE,IF(ACTIVE='1','true','false')ACTIVE,PHONENO,CREATEDBY FROM USERMASTER;";
                DataTable dtUserMaster = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                LoadUserMaster(dtUserMaster);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.LoadAllUserMasterTables()", ex);
                throw ex;
            }
        }

        public void LoadPrivilegeMaster(DataTable dtPrivilegeMaster)
        {
            try
            {
                ListPrivilegeDetails.Clear();
                for (int i = 0; i < dtPrivilegeMaster.Rows.Count; i++)
                {
                    DataRow dr = dtPrivilegeMaster.Rows[i];

                    PrivilegeDetails ObjPrivilegeDetails = new PrivilegeDetails();
                    ObjPrivilegeDetails.PrivilegeId = dr["PRIVILEGEID"].ToString();
                    ObjPrivilegeDetails.PrivilegeName = dr["PRIVILEGENAME"].ToString().Trim();

                    AddPrivilegeDetailsToCache(ObjPrivilegeDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.LoadPrivilegeMaster()", ex);
                throw ex;
            }
        }
        void AddPrivilegeDetailsToCache(PrivilegeDetails ObjPrivilegeDetails)
        {
            try
            {
                Int32 PrivilegeIndex = ListPrivilegeDetails.BinarySearch(ObjPrivilegeDetails, ObjPrivilegeDetails);
                if (PrivilegeIndex < 0)
                {
                    ListPrivilegeDetails.Insert(~PrivilegeIndex, ObjPrivilegeDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.AddPrivilegeDetailsToCache()", ex);
            }
        }

        public void LoadStoreMaster(DataTable dtStoreMaster)
        {
            try
            {
                ListStoreDetails.Clear();
                for (int i = 0; i < dtStoreMaster.Rows.Count; i++)
                {
                    DataRow dr = dtStoreMaster.Rows[i];

                    StoreDetails ObjStoreDetails = new StoreDetails();
                    ObjStoreDetails.StoreId = dr["STOREID"] == null ? -1 : Int32.Parse(dr["STOREID"].ToString());
                    ObjStoreDetails.StoreName = dr["STORENAME"].ToString().Trim();
                    ObjStoreDetails.Address = dr["ADDRESS"].ToString().Trim();
                    ObjStoreDetails.PhoneNo = (dr["PHONENO"] == null || dr["PHONENO"].ToString().Trim() == "") ? 0 : Int64.Parse(dr["PHONENO"].ToString().Trim());
                    ObjStoreDetails.StoreExecutive = dr["STOREEXECUTIVE"].ToString().Trim();
                    AddStoreDetailsToCache(ObjStoreDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.LoadStoreMaster()", ex);
                throw ex;
            }
        }
        void AddStoreDetailsToCache(StoreDetails ObjStoreDetails)
        {
            try
            {

                Int32 StoreIndex = ListStoreDetails.BinarySearch(ObjStoreDetails, ObjStoreDetails);
                if (StoreIndex < 0)
                {
                    ListStoreDetails.Insert(~StoreIndex, ObjStoreDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.AddStoreDetailsToCache()", ex);
            }
        }

        public void LoadRoleMaster(DataTable dtRoleMaster)
        {
            try
            {
                ListRoleDetails.Clear();
                for (int i = 0; i < dtRoleMaster.Rows.Count; i++)
                {
                    DataRow dr = dtRoleMaster.Rows[i];

                    RoleDetails ObjRoleDetails = new RoleDetails();
                    ObjRoleDetails.RoleID = Int32.Parse(dr["ROLEID"].ToString());
                    ObjRoleDetails.RoleName = dr["ROLENAME"].ToString().Trim();
                    ObjRoleDetails.RoleDescription = dr["DESCRIPTION"].ToString();
                    for (int m = 0; m < ListPrivilegeDetails.Count; m++)
                    {
                        bool val = dr[ListPrivilegeDetails[m].PrivilegeId].ToString().Trim().ToUpper() == "YES" ? true : false;
                        if (!val)
                        {//get a compliment of the enabled value if privilege is not set
                            for (int k = 0; k < ListPrivilegeDetails[m].ListPrivilegeControlDetails.Count; k++)
                            {
                                if (ListPrivilegeDetails[m].ListPrivilegeControlDetails[k].IsEnabled)
                                {
                                    ListPrivilegeDetails[m].ListPrivilegeControlDetails[k].IsEnabled = false;
                                }
                                else ListPrivilegeDetails[m].ListPrivilegeControlDetails[k].IsEnabled = true;
                            }
                        }
                        ObjRoleDetails.ListOfPrivilegesAssigned.Add(val);
                    }

                    AddRoleDetailsToCache(ObjRoleDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.LoadRoleMaster()", ex);
                throw ex;
            }
        }

        void AddRoleDetailsToCache(RoleDetails ObjRoleDetails)
        {
            try
            {

                Int32 RoleIndex = ListRoleDetails.BinarySearch(ObjRoleDetails, ObjRoleDetails);
                if (RoleIndex < 0)
                {
                    ListRoleDetails.Insert(~RoleIndex, ObjRoleDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.AddRoleDetailsToCache()", ex);
            }
        }
        public void LoadPrivilegeControlMaster(DataTable dtPrivilegeControlMaster)
        {
            try
            {

                for (int i = 0; i < dtPrivilegeControlMaster.Rows.Count; i++)
                {
                    DataRow dr = dtPrivilegeControlMaster.Rows[i];

                    PrivilegeControlDetails ObjPrivilegeControlDetails = new PrivilegeControlDetails();
                    ObjPrivilegeControlDetails.FormName = dr["FORMNAME"].ToString().Trim();
                    ObjPrivilegeControlDetails.ControlName = dr["CONTROLNAME"].ToString().Trim();

                    ObjPrivilegeControlDetails.IsEnabled = dr["ENABLED"].ToString().Trim() == "1" ? true : false;
                    int PrivilegeIDIndex = GetPrivilegeIndex(dr["PRIVILEGEID"].ToString().Trim());
                    AddPrivilegeControlDetailsToCache(ObjPrivilegeControlDetails, PrivilegeIDIndex);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.LoadPrivilegeControlMaster()", ex);
                throw ex;
            }
        }

        void AddPrivilegeControlDetailsToCache(PrivilegeControlDetails ObjPrivilegeControlDetails, int IndexToBeInsertedAt)
        {
            try
            {
                if (IndexToBeInsertedAt >= 0)
                {
                    if (ListPrivilegeDetails[IndexToBeInsertedAt].ListPrivilegeControlDetails == null) ListPrivilegeDetails[IndexToBeInsertedAt].ListPrivilegeControlDetails = new List<PrivilegeControlDetails>();
                    ListPrivilegeDetails[IndexToBeInsertedAt].ListPrivilegeControlDetails.Add(ObjPrivilegeControlDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.AddPrivilegeControlDetailsToCache()", ex);
            }
        }

       

        public void LoadUserMaster(DataTable dtUserMaster)
        {
            try
            {
                ListUserDetails.Clear();
                for (int i = 0; i < dtUserMaster.Rows.Count; i++)
                {
                    DataRow dtRow = dtUserMaster.Rows[i];
                    UserDetails ObjUserDetails = new UserDetails();
                    ObjUserDetails.UserID = int.Parse(dtRow["USERID"].ToString().Trim());
                    ObjUserDetails.UserName = dtRow["USERNAME"].ToString();
                    ObjUserDetails.FullName = dtRow["FULLNAME"].ToString();
                    ObjUserDetails.EmailID = dtRow["EMAILID"].ToString();
                    ObjUserDetails.PhoneNo = ((dtRow["PHONENO"] == null) || dtRow["PHONENO"].ToString().Trim() == "") ? 0 : Int64.Parse(dtRow["PHONENO"].ToString().Trim());
                    ObjUserDetails.Active = bool.Parse(dtRow["ACTIVE"].ToString().Trim());
                    ObjUserDetails.CreatedBy = ((dtRow["CREATEDBY"] == null) || dtRow["CREATEDBY"].ToString().Trim() == "") ? 0 : int.Parse(dtRow["CREATEDBY"].ToString().Trim());
                    ObjUserDetails.LastLogin = ((dtRow["LASTLOGIN"] == null) || dtRow["LASTLOGIN"].ToString().Trim() == "") ? DateTime.MinValue : DateTime.Parse(dtRow["LASTLOGIN"].ToString());
                    ObjUserDetails.LastPasswordChanged = ((dtRow["LASTPASSWORDCHANGED"] == null) || dtRow["LASTPASSWORDCHANGED"].ToString().Trim() == "") ? DateTime.MinValue : DateTime.Parse(dtRow["LASTPASSWORDCHANGED"].ToString());
                    ObjUserDetails.LastUpdateDate = ((dtRow["LASTUPDATEDATE"] == null) || dtRow["LASTUPDATEDATE"].ToString().Trim() == "") ? DateTime.MinValue : DateTime.Parse(dtRow["LASTUPDATEDATE"].ToString());
                    ObjUserDetails.RoleID = int.Parse(dtRow["ROLEID"].ToString().Trim());
                    ObjUserDetails.RoleName = ListRoleDetails.Where(e => e.RoleID.Equals(ObjUserDetails.RoleID)).FirstOrDefault().RoleName;
                    ObjUserDetails.StoreID = ((dtRow["STOREID"] == null) || dtRow["STOREID"].ToString().Trim() == "") ? -1 : int.Parse(dtRow["STOREID"].ToString().Trim());
                    if (ObjUserDetails.StoreID != -1) ObjUserDetails.StoreName = ListStoreDetails.Where(e => e.StoreId.Equals(ObjUserDetails.StoreID)).FirstOrDefault().StoreName;

                    AddUserDataToCache(ObjUserDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.LoadUserMaster()", ex);
            }
        }
        void AddUserDataToCache(UserDetails ObjUserDetails)
        {
            try
            {
                Int32 Index = ListUserDetails.BinarySearch(ObjUserDetails, ObjUserDetails);
                if (Index < 0)
                {
                    ListUserDetails.Insert(~Index, ObjUserDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.AddUserDataToCache()", ex);
            }
        }

        public Int32 UpdateAnyTableDetails(string TableName, List<string> ListColumnNames, List<string> ListColumnValues, string WhereCondition)
        {
            try
            {
                if (ObjMySQLHelper.CheckTableExists(TableName))
                {
                    String UpdateAnyTableQuery = "SET SQL_SAFE_UPDATES=0;" + "UPDATE " + TableName + " SET ";
                    for (int i = 0; i < ListColumnNames.Count; i++)
                    {
                        if (ListColumnValues[i] != "NULL" && ListColumnNames[i] != "ACTIVE" && ListColumnNames[i] != "ISDEFAULT") UpdateAnyTableQuery += ListColumnNames[i] + " = '" + ListColumnValues[i] + "',";
                        else UpdateAnyTableQuery += ListColumnNames[i] + " = " + ListColumnValues[i] + ",";
                    }
                    UpdateAnyTableQuery = UpdateAnyTableQuery.Remove(UpdateAnyTableQuery.Length - 1, 1);
                    UpdateAnyTableQuery += " WHERE " + WhereCondition + ";";

                    return ObjMySQLHelper.ExecuteNonQuery(UpdateAnyTableQuery);
                }
                return -1;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.UpdateAnyTableDetails()", ex);
                throw ex;
            }
        }

        public Int32 CreateNewUser(string UserName, string Password, string FullName, bool Active, string RoleName, List<string> ListColumnNamesWthDataType = null, List<string> ListColumnValues = null)
        {
            try
            {
                Guid UserGuid = System.Guid.NewGuid(); ;

                string HashedPassword = CommonFunctions.GetHashedPassword(Password, UserGuid);
                int tmpRoleID = GetRoleID(RoleName);
                string Query = "SELECT USERNAME FROM USERMASTER WHERE LOWER(USERNAME) = LOWER('" + UserName + "');";
                ObjMySQLHelper.ObjDbCommand.CommandText = Query;
                object ObjUserName = ObjMySQLHelper.ObjDbCommand.ExecuteScalar();

                string tmpUserName = ObjUserName == null ? "" : ObjUserName.ToString();
                if (tmpUserName != string.Empty)
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

                //USERID, USERNAME, PASSWORD, FULLNAME, ROLEID, LASTLOGIN, LASTPASSWORDCHANGED, LASTUPDATEDATE, ACTIVE, PHONENO, CREATEDBY, USERGUID
                Query = "INSERT INTO USERMASTER (USERNAME,PASSWORD,FULLNAME,ROLEID,ACTIVE,USERGUID"
                    + string.Join(",", ListColumnNames)
                    + ")"
                    + "VALUES (@username, @password, @fullname,@roleid,@active,@userguid"
                    + string.Join(",", ListColumnNameParamStr)
                    + ")"
                    ;
                Query += ";";
                ObjMySQLHelper.ObjDbCommand.CommandText = Query;
                ObjMySQLHelper.ObjDbCommand.Parameters.Clear();
                ObjMySQLHelper.ObjDbCommand.Parameters.Add("@username", MySqlDbType.VarChar).Value = UserName;
                ObjMySQLHelper.ObjDbCommand.Parameters.Add("@password", MySqlDbType.VarChar).Value = HashedPassword;
                ObjMySQLHelper.ObjDbCommand.Parameters.Add("@fullname", MySqlDbType.VarChar).Value = FullName;
                ObjMySQLHelper.ObjDbCommand.Parameters.Add("@roleid", MySqlDbType.Int16).Value = tmpRoleID;
                ObjMySQLHelper.ObjDbCommand.Parameters.Add("@active", MySqlDbType.Bit).Value = Active;
                ObjMySQLHelper.ObjDbCommand.Parameters.Add("@userguid", MySqlDbType.VarChar).Value = UserGuid.ToString();
                for (int i = 0; i < ListColumnNamesWthDataType.Count; i++)
                {
                    ObjMySQLHelper.ObjDbCommand.Parameters.Add(ListColumnNameParamStr[i].Replace(",", ""), CommonFunctions.GetMySqlDbType(ListColumnDataType[i])).Value = ListColumnValues[i];
                }

                return ObjMySQLHelper.ObjDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.CreateNewUser()", ex);
                throw ex;
            }
        }

        public Int32 CreateNewRole(string RoleName, string Description, List<string> ListColumnNamesWthDataType = null, List<string> ListColumnValues = null)
        {
            try
            {
                String Query = "SELECT ROLEID FROM ROLEMASTER WHERE LOWER(ROLENAME) = LOWER('" + RoleName + "')";
                Query += ";";
                object ObjRoleID = ObjMySQLHelper.ExecuteScalar(Query);

                int tmpRoleID = ObjRoleID == null ? -1 : (int)ObjRoleID;
                if (tmpRoleID != -1)
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

                //USERID, USERNAME, PASSWORD, FULLNAME, ROLEID, LASTLOGIN, LASTPASSWORDCHANGED, LASTUPDATEDATE, ACTIVE, PHONENO, CREATEDBY, USERGUID
                Query = "INSERT INTO ROLEMASTER (ROLENAME,DESCRIPTION"
                    + string.Join(",", ListColumnNames)
                    + ")"
                    + "VALUES (@rolename, @description"
                    + string.Join(",", ListColumnNameParamStr)
                    + ")"
                    ;
                Query += ";";
                ObjMySQLHelper.ObjDbCommand.CommandText = Query;
                ObjMySQLHelper.ObjDbCommand.Parameters.Clear();
                ObjMySQLHelper.ObjDbCommand.Parameters.Add("@rolename", MySqlDbType.VarChar).Value = RoleName;
                ObjMySQLHelper.ObjDbCommand.Parameters.Add("@description", MySqlDbType.VarChar).Value = Description;

                for (int i = 0; i < ListColumnNamesWthDataType.Count; i++)
                {
                    ObjMySQLHelper.ObjDbCommand.Parameters.Add(ListColumnNameParamStr[i].Replace(",", ""), CommonFunctions.GetMySqlDbType(ListColumnDataType[i])).Value = ListColumnValues[i];
                }


                return ObjMySQLHelper.ObjDbCommand.ExecuteNonQuery(); 
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.CreateNewUser()", ex);
                throw ex;
            }
        }
        public List<UserDetails> GetListUserCache()
        {
            try
            {
                List<UserDetails> ListUserCache = new List<UserDetails>();
                ListUserCache.AddRange(ListUserDetails);

                return ListUserCache;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.GetListUserCache()", ex);
            }
            return null;
        }

        public StoreDetails GetStoreDetails(string StoreName)
        {
            try
            {
                StoreDetails ObjStoreDetails = new StoreDetails();
                ObjStoreDetails.StoreName = StoreName;
                int Index = ListStoreDetails.BinarySearch(ObjStoreDetails, ObjStoreDetails);

                return Index < 0 ? null : ListStoreDetails[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.GetStoreDetails()", ex);
            }
            return null;
        }



        public Int32 AlterTblColBasedOnMultipleRowsFrmAnotherTbl(String SourceTable, string DestinationTable, string ColumnNameFromAnotherTble)
        {
            try
            {
                string Query = "SELECT @S:= CONCAT('ALTER TABLE " + DestinationTable + " ADD COLUMN (', GROUP_CONCAT( " + ColumnNameFromAnotherTble + ", ' TINYTEXT'), ')') FROM " + SourceTable + ";"
                 + " PREPARE STMT FROM @S;"
                + " EXECUTE STMT;"
               + " DEALLOCATE PREPARE STMT;"
               + " SELECT * FROM " + DestinationTable + ";";

                return ObjMySQLHelper.ExecuteNonQuery(Query);

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.AlterTblColBasedOnRowsFrmAnotherTbl()", ex);
                throw ex;
            }
        }
    }
}
