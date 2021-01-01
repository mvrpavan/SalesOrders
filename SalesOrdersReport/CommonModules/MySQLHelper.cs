using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Data;
using System.Windows.Forms;

namespace SalesOrdersReport
{
    class MySQLHelper
    {
        public MySqlConnection ObjDbConnection;
        public MySqlConnectionStringBuilder ObjDbConnectionStringBuilder;
        public MySqlCommand ObjDbCommand;
        public string CurrentUser;
        static MySQLHelper ObjMySqlHelper = new MySQLHelper();
        private MySQLHelper()
        {
            try
            {
                CurrentUser = "";
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static MySQLHelper GetMySqlHelperObj()
        {
            try
            {
                return ObjMySqlHelper;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.GetMySqlHelperObj()", ex);
                throw ex;
            }
        }

        public void OpenConnection(String DBServer, String DBName, String DBUsername, String DBPassword)
        {
            try
            {
                ObjDbConnectionStringBuilder = new MySqlConnectionStringBuilder();
                ObjDbConnectionStringBuilder.Add("Database", DBName);
                ObjDbConnectionStringBuilder.Add("Data Source", DBServer);
                ObjDbConnectionStringBuilder.Add("User Id", DBUsername);
                ObjDbConnectionStringBuilder.Add("Password", DBPassword);
                ObjDbConnectionStringBuilder.Add("persistsecurityinfo", "True");
                ObjDbConnectionStringBuilder.Add("Allow Zero Datetime", "True");

                ObjDbConnection = new MySqlConnection();
                ObjDbConnection.ConnectionString = ObjDbConnectionStringBuilder.ConnectionString;
                ObjDbConnection.Open();

                ObjDbCommand = (MySqlCommand)ObjDbConnection.CreateCommand();
                ObjDbCommand.CommandTimeout = 99999;
                ObjDbCommand.CommandText = "set net_write_timeout=99999; set net_read_timeout=99999;";
                ObjDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.OpenConnection()", ex);
                throw ex;
            }
        }

        public void CloseConnection()
        {
            try
            {
                ObjDbConnection.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.CloseConnection()", ex);
                throw;
            }
        }


        public bool LoginCheck(string txtUserName, string txtPassword, MySqlConnection myConnection)
        {
            try
            {
                bool returnval = true;
                MySqlCommand myCommand = new MySqlCommand("SELECT Username,Password FROM Users WHERE Username = @Username AND Password = @Password", myConnection);

                MySqlParameter uName = new MySqlParameter("@Username", MySqlDbType.VarChar);
                MySqlParameter uPassword = new MySqlParameter("@Password", MySqlDbType.VarChar);

                uName.Value = txtUserName;
                uPassword.Value = txtPassword;

                myCommand.Parameters.Add(uName);
                myCommand.Parameters.Add(uPassword);

                //myCommand.Connection.Open();

                MySqlDataReader myReader = myCommand.ExecuteReader();

                if (myReader.Read() == true) CurrentUser = txtUserName;
                else returnval = false;
       
                myReader.Close();

                return returnval;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.LoginCheck()" , ex);
                throw ex;
            }
        }

        public Boolean CheckTableExists(String TableName)
        {
            try
            {
                String Database = ObjDbConnection.Database;
                String ChkTableExistsance;
                ChkTableExistsance = " SELECT count(*) CNT FROM information_schema.tables";
                ChkTableExistsance += " WHERE (TABLE_SCHEMA = " + "'" + Database + "')";
                ChkTableExistsance += " AND (TABLE_NAME = " + "'" + TableName + "')";
                ObjDbCommand.CommandText = ChkTableExistsance;
                MySqlDataReader reader = ObjDbCommand.ExecuteReader();

                reader.Read();
                String CNT = reader.GetString(0);
                reader.Close();

                return (Int32.Parse(CNT) > 0);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.CheckTableExists()", ex);
                throw ex;
            }
        }
        //public Int32 CreateTable(String TableName, List<FieldDetails> ListFields, Boolean InMemory = false, Boolean DropIfExists = false, Boolean TruncateIfExists = false)
        //{
        //    try
        //    {
        //        List<String> ListColumns = new List<String>();
        //        foreach (var Field in ListFields)
        //        {
        //            String DataType = Field.DataType.Trim().ToUpper();
        //            if (DataType.Contains("CHAR") || DataType.Contains("TEXT") || DataType.Contains("BLOB"))
        //            {
        //                ListColumns.Add(Field.Name + "," + DataType + "(" + Field.MaxLength + ")");
        //            }
        //            else
        //            {
        //                ListColumns.Add(Field.Name + "," + DataType);
        //            }
        //        }
        //        return CreateTable(TableName, ListColumns, InMemory, DropIfExists, TruncateIfExists);
        //    }
        //    catch (Exception)
        //    {
        //       // EventProcessorMain.WriteToLogFileFunc(String.Format("Error occured in {0}.CreateTable(ListFields)", this));
        //        throw;
        //    }
        //}

        public string GetUserID(string User)
        {
            try
            {
                return "";
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<string> GetAllUsers()
        {
            try
            {
                List<string> ListUsers = new List<string>();


                String CreateTableQuery = "SELECT USERNAME FROM USERS ";
                CreateTableQuery += ";";

                ObjDbCommand.CommandText = CreateTableQuery;
                MySqlDataReader dr = ObjDbCommand.ExecuteReader();
                while (dr.Read())
                {
                    ListUsers.Add(dr.GetValue(0).ToString());
                }
                dr.Close();
                return ListUsers;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.GetAllUsers()", ex);
                throw ex;
            }

        }
        public List<string> GetAllRoles()
        {
            try
            {
                List<string> ListRoles = new List<string>();
       
             
                String CreateTableQuery = "SELECT ROLENAME FROM ROLE ";
                CreateTableQuery += ";";

                 ObjDbCommand.CommandText = CreateTableQuery;
                 MySqlDataReader dr =  ObjDbCommand.ExecuteReader();
                while (dr.Read())
                {
                    ListRoles.Add(dr.GetValue(0).ToString());
                }
                dr.Close();
                return ListRoles;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.GetAllRoles()", ex);
                throw ex;
            }
            
        }
        public List<string> GetAllStatus()
        {
            try
            {
                List<string> ListStatus = new List<string>();


                String CreateTableQuery = "SELECT ROLENAME FROM ROLE ";
                CreateTableQuery += ";";

                ObjDbCommand.CommandText = CreateTableQuery;
                MySqlDataReader dr = ObjDbCommand.ExecuteReader();
                while (dr.Read())
                {
                    ListStatus.Add(dr.GetValue(0).ToString());
                }
                dr.Close();
                return ListStatus;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.GetAllStatus()", ex);
                throw ex;
            }
        }

        public List<string> GetAllStores()
        {
            try
            {
                List<string> ListStores = new List<string>();


                String CreateTableQuery = "SELECT STORENAME FROM STOREMASTER";
                CreateTableQuery += ";";

                ObjDbCommand.CommandText = CreateTableQuery;
                MySqlDataReader dr = ObjDbCommand.ExecuteReader();
                while (dr.Read())
                {
                    ListStores.Add(dr.GetValue(0).ToString());
                }
                dr.Close();
                return ListStores;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.GetAllStores()", ex);
                throw ex;
            }
        }
  


        public Int32 UpdateAnyTableDetails(string TableName, List<string> ListColumnNames, List<string> ListColumnValues , string WhereCondition)
        {
            try
            {
                if (CheckTableExists(TableName))
                {
                    String CreateTableQuery = "SET SQL_SAFE_UPDATES=0;" + "UPDATE " + TableName + " SET ";
                    for (int i = 0; i < ListColumnNames.Count; i++)
                    {
                        CreateTableQuery += ListColumnNames[i] + " = '" + ListColumnValues[i] + "', ";
                    }
                        CreateTableQuery = CreateTableQuery.Remove(CreateTableQuery.Length - 1, 1);
                        CreateTableQuery += " WHERE "+WhereCondition + ";";
                        ObjDbCommand.CommandText = CreateTableQuery;

                       return ObjDbCommand.ExecuteNonQuery();                   
                }
                return -1;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.UpdateAnyTableDetails()", ex);
                throw ex;
            }
        }
        public Int32 DeleteRow(string TableName,string ColumnName,string ColumnValue)
        {
            try
            {
                if (CheckTableExists(TableName))
                {
                    String CreateTableQuery = "DELETE FROM " + TableName + " WHERE " + ColumnName + " = '" + ColumnValue + "'";
                    CreateTableQuery += ";";

                    ObjDbCommand.CommandText = CreateTableQuery;
                    return ObjDbCommand.ExecuteNonQuery();
                }

                return -1;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.DeleteRow()", ex);
                throw ex;
            }
        }

        public Int32 CreateNewRole(string NewRoleName,string Privileges)
        {
            try
            {


                // String CreateRoleQuery = "INSERT INTO ROLE (ROLENAME,PRIVILEGE) VALUES ('" + NewRoleName + "','" + Privileges + "')";
                String CreateRoleQuery = "INSERT INTO ROLE (ROLENAME, PRIVILEGE) SELECT * FROM (SELECT ' " + NewRoleName + "','" + Privileges + "') AS tmp WHERE NOT EXISTS("
                                          + " SELECT ROLENAME FROM ROLE WHERE ROLENAME = '" + NewRoleName + "') LIMIT 1"; 
                CreateRoleQuery += ";";
                ObjDbCommand.CommandText = CreateRoleQuery;
                return ObjDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.CreateNewRole()", ex);
                throw ex;
            }
        }

        public Int32 CreateNewUser(string UserName, string Password,string PhoneNo,string Address,string RoleName)
        {
            try
            {

                String CreateTableQuery = "SELECT ROLEID FROM ROLE WHERE ROLENAME = '" + RoleName + "'";
                CreateTableQuery += ";";
                ObjDbCommand.CommandText = CreateTableQuery;
                string tmpRoleID = ObjDbCommand.ExecuteScalar().ToString();

                CreateTableQuery = "SELECT USERNAME FROM USERS WHERE USERNAME = '" + CreateTableQuery + "';";
                ObjDbCommand.CommandText = CreateTableQuery;
               string tmpUserName = ObjDbCommand.ExecuteScalar().ToString();
                if (tmpUserName != string.Empty)
                {
                    var Result = MessageBox.Show("User Name already exists.Do you want to add any way? ", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    // If the no button was pressed ...
                    if (Result == DialogResult.No) return 2;
              }
                CreateTableQuery = "INSERT INTO USERS (USERNAME,PASSWORD,PHONENO,ADDRESS,ROLEID) VALUES (@username, @password, @phoneno,@address,@roleid)";
                CreateTableQuery += ";";
                ObjDbCommand.CommandText = CreateTableQuery;
                ObjDbCommand.Parameters.Add("@username", MySqlDbType.VarChar).Value = UserName;
                ObjDbCommand.Parameters.Add("@password", MySqlDbType.VarChar).Value = Password;
                if (PhoneNo != "") ObjDbCommand.Parameters.Add("@phoneno", MySqlDbType.Int64).Value = int.Parse(PhoneNo);
                ObjDbCommand.Parameters.Add("@address", MySqlDbType.VarChar).Value = Address;
                ObjDbCommand.Parameters.Add("@roleid", MySqlDbType.Int16).Value = int.Parse(tmpRoleID);
                return ObjDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.CreateNewUser()", ex);
                throw ex;
            }
        }

        public Int32 CreateTable(String TableName, List<String> Columns,
        Boolean InMemory = false, Boolean DropIfExists = false, Boolean TruncateIfExists = false)
        {
            try
            {
                if (CheckTableExists(TableName))
                {
                    if (!DropIfExists && !TruncateIfExists) return 1;
                    if (DropIfExists && TruncateIfExists) TruncateIfExists = false;
                    if (DropIfExists) DropTable(TableName);
                    if (TruncateIfExists) DeleteTableContent(TableName);
                }

                String CreateTableQuery = " CREATE TABLE ";
                CreateTableQuery += "`" + ObjDbConnection.Database + "`" + "." + "`" + TableName + "`";
                CreateTableQuery += "  (";

                for (int i = 0; i < Columns.Count; ++i)
                {
                    String[] Tokens = Columns[i].Split(',');
                    if (Columns[i].ToUpper().Contains("Primary".ToUpper()))
                    {
                        CreateTableQuery += " " + Tokens[0];
                        CreateTableQuery += "(`" + Tokens[1] + "`)";
                    }
                    else
                    {
                        CreateTableQuery += "`" + Tokens[0] + "`";
                        CreateTableQuery += " " + String.Join(",", Tokens, 1, Tokens.Length - 1);

                        if (i < (Columns.Count - 1))
                            CreateTableQuery += ",";
                    }
                }
                CreateTableQuery += ")";

                if (InMemory == true) CreateTableQuery += " ENGINE = MEMORY ";
                CreateTableQuery += ";";

                ObjDbCommand.CommandText = CreateTableQuery;
                return ObjDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.CreateTable()", ex);
                throw ex;
            }
        }

        public Int32 DropTable(String TableName, Boolean IsTemporary = false)
        {
            try
            {
                if (!CheckTableExists(TableName)) return 0;

                String DropTableQry;
                DropTableQry = " DROP " + (IsTemporary ? "Temporary" : "") + " TABLE `" + TableName + "`;";
                ObjDbCommand.CommandText = DropTableQry;
                return ObjDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.DropTable()", ex);
                throw ex;
            }
        }

        public Int32 DeleteTableContent(String TableName)
        {
            try
            {
                String DeleteTableQry;
                DeleteTableQry = "TRUNCATE Table `" + TableName + "`;";
                ObjDbCommand.CommandText = DeleteTableQry;
                return ObjDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.DeleteTableContent()", ex);
                throw ex;
            }
        }

        public IEnumerable<String[]> ExecuteQuery(String Query)
        {
            ObjDbCommand.CommandText = Query;
            using (MySqlDataReader ObjDBDataReader = (MySqlDataReader)ObjDbCommand.ExecuteReader())
            {
                while (ObjDBDataReader.Read())
                {
                    String[] ArrValues = new String[ObjDBDataReader.FieldCount];
                    for (int i = 0; i < ObjDBDataReader.FieldCount; i++)
                    {
                        if (!Convert.IsDBNull(ObjDBDataReader[i]))
                            ArrValues[i] = ObjDBDataReader[i].ToString();
                    }
                    yield return ArrValues;
                }
            }
        }
        public Int32 ExecuteNonQuery(String Query)
        {
            try
            {
                ObjDbCommand.CommandText = Query;
                return ObjDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.ExecuteNonQuery()", ex);
                throw ex;
            }
        }

        public DbDataReader ExecuteReader(String Query)
        {
            try
            {
                ObjDbCommand.CommandText = Query;
                return ObjDbCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.ExecuteReader()", ex);
                throw ex;
            }
        }

        public Int32 CreateIndex(String TableName, String[] Columns, String IndexName)
        {
            try
            {
                String Query = "SELECT COUNT(1) IndexIsThere FROM INFORMATION_SCHEMA.STATISTICS"
                                + " WHERE table_schema = DATABASE() AND table_name = '" + TableName
                                + "' AND index_name = '" + IndexName + "'";

                Boolean IndexExists = false;
                DbDataReader dataReader = ExecuteReader(Query);
                if (dataReader.Read())
                {
                    if (Int32.Parse(dataReader[0].ToString()) > 0) IndexExists = true;
                }
                dataReader.Close();

                if (!IndexExists)
                {
                    ExecuteNonQuery("CREATE INDEX " + IndexName + " ON " + TableName + "(" + String.Join(",", Columns) + ");");
                    return 0;
                }

                return 1;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.CreateIndex()", ex);
                throw ex;
            }
        }

        public MySqlConnection GetDbConnection()
        {
            try
            {
                return ObjDbConnection;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.GetDbConnection()", ex);
                throw ex;
            }
        }

        //public Int32 ImportDataFromTextFileToDBTable(String TextFilePath, String TableName, List<FieldDetails> ListTableColumns = null,
        //        String FieldTerminator = ",", String LineTerminator = "\r\n", Int32 NumberOfLinesToSkip = 1)
        //{
        //    try
        //    {
        //        if (ListTableColumns != null)
        //        {
        //            //Create Table if it doesnt exist
        //            if (!CheckTableExists(TableName))
        //            {
        //                CreateTable(TableName, ListTableColumns);
        //            }
        //        }

        //        return BulkLoadToDBTable(TextFilePath, TableName, FieldTerminator, LineTerminator, NumberOfLinesToSkip);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public Int32 BulkLoadToDBTable(String TextFilePath, String TableName, String FieldTerminator = "\t", String LineTerminator = "\r\n",
            Int32 NumberOfLinesToSkip = 1, List<String> ListColumns = null)
        {
            try
            {
                MySqlBulkLoader objMySqlBulk = new MySqlBulkLoader((MySqlConnection)ObjDbConnection);
                objMySqlBulk.TableName = TableName;
                objMySqlBulk.Timeout = 600;
                objMySqlBulk.FieldTerminator = FieldTerminator;
                objMySqlBulk.LineTerminator = LineTerminator;
                objMySqlBulk.FileName = TextFilePath;
                objMySqlBulk.NumberOfLinesToSkip = NumberOfLinesToSkip;
                if (ListColumns != null) objMySqlBulk.Columns.AddRange(ListColumns);

                return objMySqlBulk.Load();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.BulkLoadToDBTable()", ex);
                throw ex;
            }
        }

        //public Int32 ImportDataFromExcelFileToDBTable(String ExcelFilePath, String TableName, String SheetName = null, List<FieldDetails> ListTableColumns = null)
        //{
        //    try
        //    {
        //        String TextFilePath = Path.GetTempFileName();
        //       // Int32 RecordsInFile = ExcelHelperModule.CreateTextFileFromExcelFile(ExcelFilePath, TextFilePath, SheetName, ListColumns: ListTableColumns);
        //        Int32 RecordsImported = ImportDataFromTextFileToDBTable(TextFilePath, TableName, ListTableColumns, FieldTerminator: "\t", NumberOfLinesToSkip: 0);

        //       // if (RecordsImported != RecordsInFile)
        //           // EventProcessorMain.WriteToLogFileFunc("Warning:" + RecordsImported + " out of " + RecordsInFile + " are imported to " + ObjDbConnection.Database + "." + TableName + "!!!");

        //        File.Delete(TextFilePath);

        //        return RecordsInFile;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public DataTable GetQueryResultInDataTable(String Query)
        {
            try
            {
                ObjDbCommand.CommandText = Query;
                DbDataAdapter dbAdapter = new MySqlDataAdapter((MySqlCommand)ObjDbCommand);
                DataTable dtResult = new DataTable();
                dbAdapter.Fill(dtResult);
                return dtResult;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.GetQueryResultInDataTable()", ex);
                throw ex;
            }
        }

        //public void InsertLogEntry(String SubModule, String Comment, EnumLogStatus logStatus)
        //{
        //    try
        //    {
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}


    }
}
