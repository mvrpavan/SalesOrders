using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace SalesOrdersReport
{
    class CommonFunctions
    {
        public static String ProductTitle, InvoiceTitle;
        public static Int32 InvoiceRowsFromTop;

        public static void Initialize()
        {
            ProductTitle = System.Configuration.ConfigurationManager.AppSettings["ProductTitle"];
            InvoiceTitle = System.Configuration.ConfigurationManager.AppSettings["InvoiceTitle"];
            InvoiceRowsFromTop = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["InvoiceRowsFromTop"]);
        }

        public static void ShowErrorDialog(String Method, Exception ex)
        {
            MessageBox.Show("Following Error Occured in " + Method + "():\n" + ex.Message, "Exception occured", MessageBoxButtons.OK);
        }

        public static void ReleaseCOMObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                ShowErrorDialog("ReleaseCOMObject", ex);
            }
            finally
            {
                GC.Collect();
            }
        }

        #region "Get DataTable from Worksheet of an Excel file "
        public static DataTable ReturnDataTableFromExcelWorksheet(String SheetName, String FilePath, String ColumnNames)
        {
            try
            {
                String strConnectionString, strCommandText;
                OleDbConnection conOleDbCon;
                OleDbCommand cmdCommand;

                if (System.IO.Path.GetExtension(FilePath).Equals(".xlsx", StringComparison.InvariantCultureIgnoreCase))
                {
                    strConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1';";
                }
                else
                {
                    strConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';";
                }

                //strConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=Excel 14.0";
                strCommandText = "Select " + ColumnNames + " from [" + SheetName + "$]";
                conOleDbCon = new OleDbConnection(strConnectionString);
                cmdCommand = new OleDbCommand(strCommandText, conOleDbCon);
                conOleDbCon.Open();

                OleDbDataAdapter dapAdapter = new OleDbDataAdapter(strCommandText, conOleDbCon);
                DataTable dtbInput = new DataTable();
                dapAdapter.Fill(dtbInput);
                return dtbInput;
            }
            catch (Exception)
            {
                Console.WriteLine("Error Occured in CommonFunctions.ReturnDataTableFromExcelWorksheet()");
                throw;
            }
        }
        #endregion
    }
}
