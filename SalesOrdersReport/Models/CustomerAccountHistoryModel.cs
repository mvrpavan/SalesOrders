using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesOrdersReport.CommonModules;
using System.Data;

namespace SalesOrdersReport.Models
{
    class CustomerAccountHistoryDetails : IComparer<CustomerAccountHistoryDetails>
    {
        public Int32 AccountID = -1;
        public int HistEntryId = -1, PaymentID = -1;
        public Double SaleAmount = 0.0;
        public Double CancelAmount = 0.0;
        public Double RefundAmount = 0.0;
        public Double DiscountAmount = 0.0;
        public Double TotalTaxAmount = 0.0;
        public Double NetSaleAmount = 0.0;
        public Double BalanceAmount = 0.0;
        public Double AmountReceived = 0.0;
        public Double NewBalanceAmount = 0.0;
        public int Compare(CustomerAccountHistoryDetails x, CustomerAccountHistoryDetails y)
        {
            return x.PaymentID.CompareTo(y.PaymentID);
        }

    }
    class CustomerAccountHistoryModel
    {
        List<CustomerAccountHistoryDetails> ListCustomerAccountHistoryDetails = new List<CustomerAccountHistoryDetails>();
        MySQLHelper ObjMySQLHelper;
        public CustomerAccountHistoryModel()
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
        CustomerAccountHistoryDetails GetAccountHistoryDetailsFromAccID(string AccountID)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetAccountHistoryDetailsFromAccID()", ex);
                return null;
            }
        }

        public CustomerAccountHistoryDetails GetAccountHistoryDetailsFromPaymentID(int PaymentID)
        {
            try
            {
                CustomerAccountHistoryDetails ObjCustomerAccountHistoryDetails = new CustomerAccountHistoryDetails();
                ObjCustomerAccountHistoryDetails.PaymentID = PaymentID;
                int Index = ListCustomerAccountHistoryDetails.BinarySearch(ObjCustomerAccountHistoryDetails, ObjCustomerAccountHistoryDetails);
                return ListCustomerAccountHistoryDetails[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetAccountHistoryDetailsFromAccID()", ex);
                return null;
            }
        }
        public void LoadAccountHistoryModel()
        {
            try
            {
                string Query = "SELECT * FROM CUSTOMERACCOUNTHISTORY;";
                DataTable dtMaster = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                for (int i = 0; i < dtMaster.Rows.Count; i++)
                {
                    DataRow dr = dtMaster.Rows[i];

                    CustomerAccountHistoryDetails ObjCustomerAccountHistoryDetails = new CustomerAccountHistoryDetails();
                    ObjCustomerAccountHistoryDetails.HistEntryId = ((dr["HISTORYENTRYID"] == null) || dr["HISTORYENTRYID"].ToString().Trim() == "") ? -1 : int.Parse(dr["HISTORYENTRYID"].ToString().Trim());
                    ObjCustomerAccountHistoryDetails.PaymentID = int.Parse(dr["PAYMENTID"].ToString().Trim());
                    ObjCustomerAccountHistoryDetails.AccountID = int.Parse(dr["ACCOUNTID"].ToString());

                    ObjCustomerAccountHistoryDetails.SaleAmount = double.Parse(dr["SALEAMOUNT"].ToString());
                    ObjCustomerAccountHistoryDetails.CancelAmount = double.Parse(dr["CANCELAMOUNT"].ToString());
                    ObjCustomerAccountHistoryDetails.RefundAmount = double.Parse(dr["RETURNAMOUNT"].ToString());
                    ObjCustomerAccountHistoryDetails.DiscountAmount = double.Parse(dr["DISCOUNTAMOUNT"].ToString());
                    ObjCustomerAccountHistoryDetails.TotalTaxAmount = double.Parse(dr["TOTALTAX"].ToString());
                    ObjCustomerAccountHistoryDetails.NetSaleAmount = double.Parse(dr["NETSALEAMOUNT"].ToString());
                    ObjCustomerAccountHistoryDetails.BalanceAmount = double.Parse(dr["BALANCEAMOUNT"].ToString());
                    ObjCustomerAccountHistoryDetails.AmountReceived = double.Parse(dr["AMOUNTRECEIVED"].ToString());
                    ObjCustomerAccountHistoryDetails.NewBalanceAmount = double.Parse(dr["NEWBALANCEAMOUNT"].ToString());

                    int Index = ListCustomerAccountHistoryDetails.BinarySearch(ObjCustomerAccountHistoryDetails, ObjCustomerAccountHistoryDetails);
                    if (Index < 0) ListCustomerAccountHistoryDetails.Insert(~Index, ObjCustomerAccountHistoryDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadAccountHistoryModel()", ex);
            }
        }

        public CustomerAccountHistoryDetails CreateNewCustomerAccountHistoryEntry(CustomerAccountHistoryDetails ObjCustomerAccountHistoryDetails)
        {
            try
            {
                List<string> ListColumnValues = new List<string>(), ListTempColValues = new List<string>();
                List<string> ListColumnNames = new List<string>(), ListTempColNames = new List<string>();
                List<Types> ListTypes = new List<Types>();

                ListColumnValues.Add(ObjCustomerAccountHistoryDetails.AccountID.ToString());
                ListColumnNames.Add("ACCOUNTID");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(ObjCustomerAccountHistoryDetails.PaymentID.ToString());
                ListColumnNames.Add("PAYMENTID");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(ObjCustomerAccountHistoryDetails.SaleAmount.ToString());
                ListColumnNames.Add("SALEAMOUNT");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(ObjCustomerAccountHistoryDetails.CancelAmount.ToString());
                ListColumnNames.Add("CANCELAMOUNT");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(ObjCustomerAccountHistoryDetails.RefundAmount.ToString());
                ListColumnNames.Add("RETURNAMOUNT");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(ObjCustomerAccountHistoryDetails.DiscountAmount.ToString());
                ListColumnNames.Add("DISCOUNTAMOUNT");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(ObjCustomerAccountHistoryDetails.TotalTaxAmount.ToString());
                ListColumnNames.Add("TOTALTAX");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(ObjCustomerAccountHistoryDetails.NetSaleAmount.ToString());
                ListColumnNames.Add("NETSALEAMOUNT");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(ObjCustomerAccountHistoryDetails.AmountReceived.ToString());
                ListColumnNames.Add("AMOUNTRECEIVED");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(ObjCustomerAccountHistoryDetails.NewBalanceAmount.ToString());
                ListColumnNames.Add("NEWBALANCEAMOUNT");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(ObjCustomerAccountHistoryDetails.BalanceAmount.ToString());
                ListColumnNames.Add("BALANCEAMOUNT");
                ListTypes.Add(Types.Number);

                int ResultVal = ObjMySQLHelper.InsertIntoTable("CUSTOMERACCOUNTHISTORY", ListColumnNames, ListColumnValues, ListTypes);
                if (ResultVal <= 0) return null;

                Int32 HistoryEntryID = Int32.Parse(ObjMySQLHelper.ExecuteScalar($"Select HISTORYENTRYID from CUSTOMERACCOUNTHISTORY " +
                                            $"Where PaymentID = {ObjCustomerAccountHistoryDetails.PaymentID} " +
                                            $"and ACCOUNTID = {ObjCustomerAccountHistoryDetails.AccountID}").ToString());
                ObjCustomerAccountHistoryDetails.HistEntryId = HistoryEntryID;

                Int32 Index = ListCustomerAccountHistoryDetails.BinarySearch(ObjCustomerAccountHistoryDetails);
                if (Index < 0) ListCustomerAccountHistoryDetails.Insert(~Index, ObjCustomerAccountHistoryDetails);

                return ObjCustomerAccountHistoryDetails;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreateNewCustomerAccountHistoryEntry()", ex);
                return null;
            }
        }
    }
}
