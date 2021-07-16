using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Data;
using System.IO;

namespace SalesOrdersReport.CommonModules
{
    public enum Types
    {
        String, Number
    }

    class MySQLHelper
    {
        MySqlConnection ObjDbConnection;
        MySqlConnectionStringBuilder ObjDbConnectionStringBuilder;
        public MySqlCommand ObjDbCommand;
        public string CurrentUser;
        public DateTime LoginTime;
        static MySQLHelper ObjMySqlHelper = new MySQLHelper();
        String DBServer, DBName, DBUsername, DBPassword, DBPort;

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

        public void OpenConnection(String DBServer, String DBName, String DBUsername, String DBPassword, String DBPort)
        {
            try
            {
                this.DBServer = DBServer;
                this.DBName = DBName;
                this.DBUsername = DBUsername;
                this.DBPassword = DBPassword;
                this.DBPort = DBPort;

                ObjDbConnectionStringBuilder = new MySqlConnectionStringBuilder();
                ObjDbConnectionStringBuilder.Add("Database", DBName.ToLower());
                ObjDbConnectionStringBuilder.Add("Data Source", DBServer.ToLower());
                ObjDbConnectionStringBuilder.Add("User Id", DBUsername);
                ObjDbConnectionStringBuilder.Add("Password", DBPassword);
                ObjDbConnectionStringBuilder.Add("Port", DBPort);
                ObjDbConnectionStringBuilder.Add("persistsecurityinfo", "True");
                ObjDbConnectionStringBuilder.Add("Allow Zero Datetime", "True");
                ObjDbConnectionStringBuilder.Add("Allow User Variables", "True");
                ObjDbConnectionStringBuilder.ConnectionTimeout = 999999;
                ObjDbConnectionStringBuilder.ConnectionLifeTime = 999999;

                ObjDbConnection = new MySqlConnection();
                ObjDbConnection.ConnectionString = ObjDbConnectionStringBuilder.ConnectionString;
                ObjDbConnection.Open();

                ObjDbCommand = (MySqlCommand)ObjDbConnection.CreateCommand();
                ObjDbCommand.CommandTimeout = 99999;
                ObjDbCommand.CommandText = "set net_write_timeout=99999; set net_read_timeout=99999; set wait_timeout=99999; set interactive_timeout=99999;";
                ObjDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //CommonFunctions.ShowErrorDialog("MySQLHelper.OpenConnection()", ex);
                throw ex;
            }
        }

        public void CheckAndReconnectToDB()
        {
            try
            {
                if (ObjDbConnection.State != ConnectionState.Open)
                {
                    OpenConnection(DBServer, DBName, DBUsername, DBPassword, DBPort);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CheckAndReconnectToDB()", ex);
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (AutoCloseConnection) ObjDbConnection.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.CloseConnection()", ex);
                throw;
            }
        }

        public Int32 BuildNExceuteQueryWithParams(string Query, List<string> ListColumnNames, List<string> ListColDataTypes, List<string> ListColumnValues)
        {
            try
            {
                CheckAndReconnectToDB();
                ObjDbCommand.CommandText = Query;
                ObjDbCommand.Parameters.Clear();
                for (int i = 0; i < ListColumnNames.Count; i++)
                {
                    ObjDbCommand.Parameters.Add(ListColumnNames[i].Replace(",", ""), MySQLHelper.GetMySqlDbType(ListColDataTypes[i])).Value = ListColumnValues[i];
                }

                return ObjDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.BuildNExceuteQueryWithParams()", ex);
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public DataTable BuildNReturnQueryResultWithParams(string Query, List<string> ListColumnNames, List<string> ListColDataTypes, List<string> ListColumnValues)
        {
            try
            {
                CheckAndReconnectToDB();
                ObjDbCommand.CommandText = Query;
                ObjDbCommand.Parameters.Clear();
                for (int i = 0; i < ListColumnNames.Count; i++)
                {
                    ObjDbCommand.Parameters.Add(ListColumnNames[i].Replace(",", ""), MySQLHelper.GetMySqlDbType(ListColDataTypes[i])).Value = ListColumnValues[i];
                }

                DbDataAdapter dbAdapter = new MySqlDataAdapter((MySqlCommand)ObjDbCommand);
                DataTable dtResult = new DataTable();
                dbAdapter.Fill(dtResult);
                return dtResult;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.BuildNReturnQueryResultWithParams()", ex);
                return null;
            }
            finally
            {
                CloseConnection();
            }
        }

        public Boolean CheckTableExists(String TableName)
        {
            try
            {
                CheckAndReconnectToDB();
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

        public Int32 UpdateTableDetails(String TableName, List<String> ListColumnNames, List<String> ListColumnValues, List<Types> ListColumnTypes, String WhereCondition)
        {
            try
            {
                if (CheckTableExists(TableName))
                {
                    String Query = "SET SQL_SAFE_UPDATES = 0;" + "UPDATE " + TableName + " SET ";
                    for (int i = 0; i < ListColumnNames.Count; i++)
                    {
                        if (ListColumnTypes[i] == Types.Number)
                            Query += ListColumnNames[i] + " = " + ListColumnValues[i] + ",";
                        else
                            Query += ListColumnNames[i] + " = '" + ListColumnValues[i] + "',";
                    }
                    Query = Query.Remove(Query.Length - 1, 1);
                    Query += " WHERE " + WhereCondition + ";";
                    ObjDbCommand.CommandText = Query;

                    return ObjDbCommand.ExecuteNonQuery();
                }
                return -1;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.UpdateTableDetails()", ex);
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public Int32 DeleteRow(String TableName, String ColumnName, String ColumnValue, Types ColumnType)
        {
            try
            {
                if (CheckTableExists(TableName))
                {
                    String DeleteQuery = "DELETE FROM " + TableName + " WHERE " + ColumnName;
                    if (ColumnType == Types.String) DeleteQuery += " = '" + ColumnValue + "';";
                    else DeleteQuery += " = " + ColumnValue + ";";

                    ObjDbCommand.CommandText = DeleteQuery;
                    return ObjDbCommand.ExecuteNonQuery();
                }

                return -1;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.DeleteRow()", ex);
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public Int32 DeleteRow(String TableName, String WhereCondition = "1 = 1")
        {
            try
            {
                if (CheckTableExists(TableName))
                {
                    String DeleteQuery = "DELETE FROM " + TableName + " WHERE " + WhereCondition;
                    ObjDbCommand.CommandText = DeleteQuery;
                    return ObjDbCommand.ExecuteNonQuery();
                }

                return -1;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.DeleteRow()", ex);
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public Int32 DecideWhetherInsertOrUpdate(String WhereCondition, String TableName, List<String> ListColumnNames, List<String> ListColumnValues, List<Types> ListColumnTypes)
        {
            try
            {
                Int32 ResultVal = 0;
                string Query = "Select Count(*) From " + TableName + " where " + WhereCondition;

                Int32 Count = int.Parse(ExecuteScalar(Query).ToString());
                if (Count == 0) ResultVal = InsertIntoTable(TableName, ListColumnNames, ListColumnValues, ListColumnTypes);
                else ResultVal = UpdateTableDetails(TableName, ListColumnNames, ListColumnValues, ListColumnTypes, WhereCondition);

                return ResultVal;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.DecideWhetherInsertOrUpdate()", ex);
                throw ex;
            }
        }

        public Int32 InsertIntoTable(String TableName, List<String> ListColumnNames, List<String> ListColumnValues, List<Types> ListColumnTypes)
        {
            try
            {
                if (!CheckTableExists(TableName)) return -1;

                String Query = $"Insert into {TableName}";
                if (ListColumnNames != null && ListColumnNames.Count > 0) Query += $"({String.Join(",", ListColumnNames)})";
                Query += " Values (";
                for (int i = 0; i < ListColumnValues.Count; i++)
                {
                    if (i > 0) Query += ",";
                    if (ListColumnTypes[i] == Types.Number)
                        Query += $"{ListColumnValues[i]}";
                    else
                        Query += $"'{ListColumnValues[i]}'";
                }
                Query += ");";
                ObjDbCommand.CommandText = Query;

                return ObjDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.InsertIntoTable()", ex);
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public Int32 CreateTable(String TableName, List<String> Columns,
                                Boolean InMemory = false, Boolean DropIfExists = true, Boolean TruncateIfExists = false)
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
                        CreateTableQuery += "(`" + Tokens[1].Trim() + "`)";
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
            finally
            {
                CloseConnection();
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
            finally
            {
                CloseConnection();
            }
        }

        public Int32 DeleteTableContent(String TableName)
        {
            try
            {
                if (!CheckTableExists(TableName)) return 0;

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
            finally
            {
                CloseConnection();
            }
        }

        public IEnumerable<String[]> ExecuteQuery(String Query)
        {
            CheckAndReconnectToDB();

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
                CheckAndReconnectToDB();
                ObjDbCommand.CommandText = Query;
                return ObjDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.ExecuteNonQuery()", ex);
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public object ExecuteScalar(String Query)
        {
            try
            {
                CheckAndReconnectToDB();
                ObjDbCommand.CommandText = Query;
                return ObjDbCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.ExecuteScalar()", ex);
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public DbDataReader ExecuteReader(String Query)
        {
            try
            {
                CheckAndReconnectToDB();
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
                if (!CheckTableExists(TableName)) return -1;

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
            finally
            {
                CloseConnection();
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
                CheckAndReconnectToDB();
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
            finally
            {
                CloseConnection();
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
                CheckAndReconnectToDB();
                ObjDbCommand.CommandText = Query;
                DbDataAdapter dbAdapter = new MySqlDataAdapter((MySqlCommand)ObjDbCommand);
                DataTable dtResult = new DataTable();
                dbAdapter.Fill(dtResult);
                return dtResult;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.GetQueryResultInDataTable()", ex);
                return null;
            }
            finally
            {
                CloseConnection();
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

        public static String GetDateStringForDB(DateTime dateTime)
        {
            try
            {
                return dateTime.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.GetDateStringForDB()", ex);
                throw ex;
            }
        }

        public static String GetDateTimeStringForDB(DateTime dateTime)
        {
            try
            {
                return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.GetDateTimeStringForDB()", ex);
                throw ex;
            }
        }

        public static string GetTimeStampStrForSearch(DateTime DateToBeProcessed, bool IsFromDate = true)
        {
            try
            {
                string TmpDate = "";
                if (IsFromDate) TmpDate = GetDateStringForDB(DateToBeProcessed) + " 00:00:00";
                else TmpDate = GetDateStringForDB(DateToBeProcessed) + " 23:59:59";
                return TmpDate;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.GetTimeStampStrForSearch()", ex);
                throw ex;
            }
        }

        public void ExecuteScriptFile(String ScriptFilePath)
        {
            try
            {
                String Query = "";
                StreamReader srFile = new StreamReader(ScriptFilePath);
                while (!srFile.EndOfStream)
                {
                    Query = srFile.ReadLine().Trim();
                    if (Query.StartsWith("--")) continue;
                    else if (String.IsNullOrEmpty(Query)) continue;

                    ExecuteNonQuery(Query);
                }
                srFile.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ExecuteScriptFile()", ex);
                throw;
            }
        }

        String IDValueTableName = "IDValueMaster";

        public String GetIDValue(String TableName)
        {
            try
            {
                String IDValue = "";
                String Query = $"Select IDValue from {IDValueTableName} Where TableName = '{TableName}';";
                Object result = ExecuteScalar(Query);
                if (result != null && result != DBNull.Value) IDValue = result.ToString();

                return IDValue;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetIDValue()", ex);
                throw;
            }
        }

        public void UpdateIDValue(String TableName, String IDValue)
        {
            try
            {
                String CurrIDValue = GetIDValue(TableName);
                if (String.IsNullOrEmpty(CurrIDValue))
                {
                    String Query = $"Insert into {IDValueTableName}(TableName, IDValue) Values('{TableName}', '{IDValue}');";
                    ExecuteNonQuery(Query);
                }
                else
                {
                    UpdateTableDetails(IDValueTableName, new List<string>() { "IDValue" }, new List<string>() { IDValue }, new List<Types>() { Types.String },
                                    $"TableName = '{TableName}'");
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateIDValue()", ex);
                throw;
            }
        }

        public static MySqlDbType GetMySqlDbType(string DataTypeStr)
        {
            try
            {
                MySqlDbType ObjMySqlDbType = MySqlDbType.VarChar;

                switch (DataTypeStr.ToUpper())
                {
                    case "DECIMAL": ObjMySqlDbType = MySqlDbType.Decimal; break;
                    case "BYTE": ObjMySqlDbType = MySqlDbType.Byte; break;
                    case "INT16": ObjMySqlDbType = MySqlDbType.Int16; break;
                    case "INT24": ObjMySqlDbType = MySqlDbType.Int24; break;
                    case "INT32": ObjMySqlDbType = MySqlDbType.Int32; break;
                    case "INT64": ObjMySqlDbType = MySqlDbType.Int64; break;
                    case "FLOAT": ObjMySqlDbType = MySqlDbType.Float; break;
                    case "DOUBLE": ObjMySqlDbType = MySqlDbType.Double; break;
                    case "TIMESTAMP": ObjMySqlDbType = MySqlDbType.Timestamp; break;
                    case "DATE": ObjMySqlDbType = MySqlDbType.Date; break;
                    case "TIME": ObjMySqlDbType = MySqlDbType.Time; break;
                    case "DATETIME": ObjMySqlDbType = MySqlDbType.DateTime; break;
                    case "YEAR": ObjMySqlDbType = MySqlDbType.Year; break;
                    case "NEWDATE": ObjMySqlDbType = MySqlDbType.Newdate; break;
                    case "VARSTRING": ObjMySqlDbType = MySqlDbType.VarString; break;
                    case "BIT": ObjMySqlDbType = MySqlDbType.Bit; break;
                    case "JSON": ObjMySqlDbType = MySqlDbType.JSON; break;
                    case "NEWDECIMAL": ObjMySqlDbType = MySqlDbType.NewDecimal; break;
                    case "ENUM": ObjMySqlDbType = MySqlDbType.Enum; break;
                    case "SET": ObjMySqlDbType = MySqlDbType.Set; break;
                    case "TINYBLOB": ObjMySqlDbType = MySqlDbType.TinyBlob; break;
                    case "MEDIUMBLOB": ObjMySqlDbType = MySqlDbType.MediumBlob; break;
                    case "LONGBLOB": ObjMySqlDbType = MySqlDbType.LongBlob; break;
                    case "BLOB": ObjMySqlDbType = MySqlDbType.Blob; break;
                    case "VARCHAR": ObjMySqlDbType = MySqlDbType.VarChar; break;
                    case "STRING": ObjMySqlDbType = MySqlDbType.String; break;
                    case "GEOMETRY": ObjMySqlDbType = MySqlDbType.Geometry; break;
                    case "UBYTE": ObjMySqlDbType = MySqlDbType.UByte; break;
                    case "UINT16": ObjMySqlDbType = MySqlDbType.UInt16; break;
                    case "UINT24": ObjMySqlDbType = MySqlDbType.UInt24; break;
                    case "UINT32": ObjMySqlDbType = MySqlDbType.UInt32; break;
                    case "UINT64": ObjMySqlDbType = MySqlDbType.UInt64; break;
                    case "BINARY": ObjMySqlDbType = MySqlDbType.Binary; break;
                    case "VARBINARY": ObjMySqlDbType = MySqlDbType.VarBinary; break;
                    case "TINYTEXT": ObjMySqlDbType = MySqlDbType.TinyText; break;
                    case "MEDIUMTEXT": ObjMySqlDbType = MySqlDbType.MediumText; break;
                    case "LONGTEXT": ObjMySqlDbType = MySqlDbType.LongText; break;
                    case "TEXT": ObjMySqlDbType = MySqlDbType.Text; break;
                    case "GUID": ObjMySqlDbType = MySqlDbType.Guid; break;

                }

                return ObjMySqlDbType;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.GetMySqlDbType()", ex);
                return MySqlDbType.VarChar;
            }
        }

        public Int32 GetLatestColValFromTable(string ColName, string TableName)
        {
            try
            {
                CheckAndReconnectToDB();

                String Query = "SELECT MAX(" + ColName + ") FROM " + TableName;
                Query += ";";
                ObjDbCommand.CommandText = Query;
                Object Val = ObjDbCommand.ExecuteScalar();

                return (Val == null) || (Val.ToString() == string.Empty) ? -1 : int.Parse(Val.ToString());
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.GetLatestColValFromTable()", ex);
            }
            finally
            {
                CloseConnection();
            }
            return 0;
        }

        public Int32 AlterTblColBasedOnMultipleRowsFrmAnotherTbl(String SourceTable, string DestinationTable, string ColumnNameFromAnotherTble, string DataType = "TINYTEXT")
        {
            try
            {
                string Query = "SELECT @S:= CONCAT('ALTER TABLE " + DestinationTable + " ADD COLUMN (', GROUP_CONCAT(" + "CONCAT(CHAR(96)," + ColumnNameFromAnotherTble + ",CHAR(96)), ' " + DataType + "'), ')') FROM " + SourceTable + ";"
                 + " PREPARE STMT FROM @S;"
                + " EXECUTE STMT;"
               + " DEALLOCATE PREPARE STMT;"
               + " SELECT * FROM " + DestinationTable + ";";

                return ExecuteNonQuery(Query);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("UserMasterModel.AlterTblColBasedOnRowsFrmAnotherTbl()", ex);
                throw ex;
            }
        }

        Boolean AutoCloseConnection = true;
        public void SetAutoCloseConnection(bool close)
        {
            AutoCloseConnection = close;
        }
    }
}
