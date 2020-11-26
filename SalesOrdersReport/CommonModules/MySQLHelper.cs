using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Data;
using System.Windows.Forms;
using SalesOrdersReport.CommonModules;

namespace SalesOrdersReport
{
    class MySQLHelper
    {
        public MySqlConnection ObjDbConnection;
        public MySqlConnectionStringBuilder ObjDbConnectionStringBuilder;
        public MySqlCommand ObjDbCommand;
        public string CurrentUser;
        public DateTime LoginTime;
        static MySQLHelper ObjMySqlHelper = new MySQLHelper();
        private MySQLHelper()
        {
            try
            {
                ObjDbConnection = new MySqlConnection();
                ObjDbConnectionStringBuilder = new MySqlConnectionStringBuilder();
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
                ObjDbConnectionStringBuilder.Add("Database", DBName);
                ObjDbConnectionStringBuilder.Add("Data Source", DBServer);
                ObjDbConnectionStringBuilder.Add("User Id", DBUsername);
                ObjDbConnectionStringBuilder.Add("Password", DBPassword);
                ObjDbConnectionStringBuilder.Add("persistsecurityinfo", "True");
                ObjDbConnectionStringBuilder.Add("Allow Zero Datetime", "True");
                ObjDbConnectionStringBuilder.Add("Allow User Variables", "True");

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

        public Int32 DeleteRow(string TableName, string ColumnName, string ColumnValue)
        {
            try
            {
                if (CheckTableExists(TableName))
                {
                    String DeleteQuery = "DELETE FROM " + TableName + " WHERE " + ColumnName + " = '" + ColumnValue + "'";
                    DeleteQuery += ";";

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
        public object ExecuteScalar(String Query)
        {
            try
            {
                ObjDbCommand.CommandText = Query;
                return ObjDbCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.ExecuteScalar()", ex);
                throw ex;
            }
        }

        //public DbDataReader ExecuteReader(String Query)
        //{
        //    try
        //    {
        //        ObjDbCommand.CommandText = Query;
        //        return ObjDbCommand.ExecuteReader();
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonFunctions.ShowErrorDialog("MySQLHelper.ExecuteReader()", ex);
        //        throw ex;
        //    }
        //}

        public MySqlDataReader ExecuteReader(String Query)
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
                MySqlDataReader dataReader = ExecuteReader(Query);
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


        public Int32 AlterTblAddColumn(String TableName, string ColumnName, string ColumnDataType)
        {
            try
            {
                if (CheckTableExists(TableName))
                {
                    String AlterTableQuery = " ALTER TABLE ";
                    AlterTableQuery += "`" + TableName + "`";
                    AlterTableQuery += " ADD COLUMN " + ColumnName + " " + ColumnDataType + ";";
                    ObjDbCommand.CommandText = AlterTableQuery;
                    return ObjDbCommand.ExecuteNonQuery();
                }
                return -1;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("MySQLHelper.AlterTblAddColumn()", ex);
                throw ex;
            }
        }



        public Int32 CreateTable(String TableName, List<String> Columns,
     Boolean DropIfExists = false, Boolean TruncateIfExists = false)
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
                CreateTableQuery += "`" + TableName + "`";
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
                CreateTableQuery += ") ENGINE = InnoDB DEFAULT CHARSET = utf8";

                //if (InMemory == true) CreateTableQuery += " ENGINE = MEMORY ";
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
