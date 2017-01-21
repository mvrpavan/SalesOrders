using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace SalesOrdersReport
{
    class CommonFunctions
    {
        public static String ProductTitleText, InvoiceTitleText, InvoiceHeaderTitle, InvoiceHeaderSubTitle, InvoiceAddress, InvoicePhoneNumber, InvoiceMailID;
        public static Int32 InvoiceRowsFromTop, InvoiceAppendRowsAtBottom;

        public static void Initialize()
        {
            ProductTitleText = System.Configuration.ConfigurationManager.AppSettings["ProductTitleText"];
            InvoiceTitleText = System.Configuration.ConfigurationManager.AppSettings["InvoiceTitleText"];
            InvoiceRowsFromTop = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["InvoiceRowsFromTop"]);
            InvoiceAppendRowsAtBottom = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["InvoiceAppendRowsAtBottom"]);
            InvoiceHeaderTitle = System.Configuration.ConfigurationManager.AppSettings["InvoiceHeaderTitle"];
            InvoiceHeaderSubTitle = System.Configuration.ConfigurationManager.AppSettings["InvoiceHeaderSubTitle"];
            InvoiceAddress = System.Configuration.ConfigurationManager.AppSettings["InvoiceAddress"];
            InvoicePhoneNumber = System.Configuration.ConfigurationManager.AppSettings["InvoicePhoneNumber"];
            InvoiceMailID = System.Configuration.ConfigurationManager.AppSettings["InvoiceMailID"];
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

        #region "Save file related Methods"
        static Dictionary<String, String[]> DictSaveFileEntries = new Dictionary<String, String[]>();
        static String SaveFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\ApplicationSaveFile.txt";
        static Boolean SaveFileEntryModified = false;
        public static void LoadSaveFile()
        {
            try
            {
                if (!File.Exists(SaveFilePath)) return;

                StreamReader srSaveFile = new StreamReader(SaveFilePath);

                while (!srSaveFile.EndOfStream)
                {
                    String tmpLine = srSaveFile.ReadLine().Trim();
                    if (String.IsNullOrEmpty(tmpLine)) continue;

                    String[] Tokens = tmpLine.Split('\t');
                    if (Tokens.Length < 2) continue;

                    String Key = Tokens[0].Trim().ToUpper();
                    String[] Value = new String[Tokens.Length - 1];
                    Array.Copy(Tokens, 1, Value, 0, Value.Length);
                    if (DictSaveFileEntries.ContainsKey(Key))
                        DictSaveFileEntries[Key] = Value;
                    else
                        DictSaveFileEntries.Add(Key, Value);
                }
                srSaveFile.Close();
            }
            catch (Exception ex)
            {
                ShowErrorDialog("LoadSaveFile", ex);
            }
        }

        public static void UpdateSaveFileEntry(String Key, String[] Value)
        {
            try
            {
                if (DictSaveFileEntries.ContainsKey(Key.Trim().ToUpper()))
                    DictSaveFileEntries[Key.Trim().ToUpper()] = Value;
                else
                    DictSaveFileEntries.Add(Key.Trim().ToUpper(), Value);

                SaveFileEntryModified = true;
            }
            catch (Exception ex)
            {
                ShowErrorDialog("UpdateSaveFileEntry", ex);
            }
        }

        public static String[] GetSaveFileEntry(String Key)
        {
            try
            {
                if (DictSaveFileEntries.ContainsKey(Key.Trim().ToUpper()))
                    return DictSaveFileEntries[Key.Trim().ToUpper()];
            }
            catch (Exception ex)
            {
                ShowErrorDialog("GetSaveFileEntry", ex);
            }
            return null;
        }

        public static void WriteToSaveFile()
        {
            try
            {
                if (!SaveFileEntryModified) return;

                StreamWriter swSaveFile = new StreamWriter(SaveFilePath);

                foreach (var Pair in DictSaveFileEntries)
                {
                    swSaveFile.WriteLine(Pair.Key + '\t' + String.Join("\t", Pair.Value));
                }
                swSaveFile.Close();
            }
            catch (Exception ex)
            {
                ShowErrorDialog("WriteToSaveFile", ex);
            }
        }
        #endregion
    }
}
