using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesOrdersReport.CommonModules;

namespace SalesOrdersReport.Models
{
    class AccountDetails
    {
        public int AccountID = -1;
        public Int32 CustomerID = -1;
        public bool Active = false;
        public double BalanceAmount = 0.0;
        public DateTime CreationDate = DateTime.MinValue, LastUpdatedDate = DateTime.MinValue; //ClosedDate = DateTime.MinValue;

    }
    class AccountsMasterModel
    {
        List<AccountDetails> ListAccountDetails = new List<AccountDetails>();
        MySQLHelper ObjMySQLHelper;
        public AccountsMasterModel()
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

        public AccountDetails GetAccDtlsFromCustID(Int32 CustID)
        {
            try
            {
                if (ListAccountDetails == null || ListAccountDetails.Count == 0) return null;
                Int32 Index = ListAccountDetails.FindIndex(e => e.CustomerID == CustID);
                if (Index < 0) return null;

                return ListAccountDetails[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetAccDtlsFromCustID()", ex);
                return null;
            }
        }

        public void LoadAccountDetails()
        {
            try
            {
                ListAccountDetails = new List<AccountDetails>();

                String Query = "SELECT ACCOUNTID,CUSTOMERID,ACTIVE,BALANCEAMOUNT,CREATIONDATE,LASTUPDATEDDATE FROM ACCOUNTSMASTER;";
                foreach (var item in ObjMySQLHelper.ExecuteQuery(Query))
                {
                    ListAccountDetails.Add(new AccountDetails()
                    {
                        AccountID = int.Parse(item[0]),
                        CustomerID = Int32.Parse(item[1]),
                        Active = item[2] == "1" ? true : false,
                        BalanceAmount = double.Parse(item[3]),
                        CreationDate = DateTime.Parse(item[4]),
                        LastUpdatedDate = DateTime.Parse(item[5])
                    });
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadAccountDetails()", ex);
            }
        }

        public Int32 CreateNewCustomerAccount(ref AccountDetails ObjAccountDetails)
        {
            try
            {
                AccountDetails tmpAccountDetails = GetAccDtlsFromCustID(ObjAccountDetails.CustomerID);
                if (tmpAccountDetails != null) return -2;

                Int32 RetVal = ObjMySQLHelper.InsertIntoTable("ACCOUNTSMASTER", 
                                                new List<string>() { "CustomerID", "Active", "BalanceAmount", "CreationDate", "LastUpdatedDate" },
                                                new List<string>() { ObjAccountDetails.CustomerID.ToString(), ObjAccountDetails.Active ? "1" : "0",
                                                ObjAccountDetails.BalanceAmount.ToString(), MySQLHelper.GetDateTimeStringForDB(ObjAccountDetails.CreationDate),
                                                MySQLHelper.GetDateTimeStringForDB(ObjAccountDetails.LastUpdatedDate) },
                                                new List<Types>() { Types.Number, Types.Number, Types.Number, Types.String, Types.String });
                if (RetVal <= 0) return -3;

                ObjAccountDetails.AccountID = Int32.Parse(ObjMySQLHelper.ExecuteScalar($"Select AccountID from ACCOUNTSMASTER Where CustomerID = {ObjAccountDetails.CustomerID} and Active = 1;").ToString());
                ListAccountDetails.Add(ObjAccountDetails);

                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreateNewCustomerAccount()", ex);
                return -1;
            }
        }

        public Int32 UpdateCustomerAccount(ref CustomerAccountHistoryDetails ObjCustomerAccountHistoryDetails)
        {
            try
            {
                //Insert into CustomerAccountHistory table
                CustomerAccountHistoryModel ObjAccountHistoryModel = new CustomerAccountHistoryModel();
                ObjCustomerAccountHistoryDetails.NewBalanceAmount = ObjCustomerAccountHistoryDetails.BalanceAmount
                                                                + ObjCustomerAccountHistoryDetails.NetSaleAmount
                                                                - ObjCustomerAccountHistoryDetails.AmountReceived;
                ObjCustomerAccountHistoryDetails = ObjAccountHistoryModel.CreateNewCustomerAccountHistoryEntry(ObjCustomerAccountHistoryDetails);
                if (ObjCustomerAccountHistoryDetails == null) return -2;

                //Update AccountsMaster table
                List<string> ListTempColValues = new List<string>(), ListTempColNames = new List<string>();
                ListTempColValues.Add(ObjCustomerAccountHistoryDetails.NewBalanceAmount.ToString());
                ListTempColNames.Add("BALANCEAMOUNT");

                ListTempColValues.Add(MySQLHelper.GetDateTimeStringForDB(DateTime.Now));
                ListTempColNames.Add("LASTUPDATEDDATE");

                string WhereCondition = "ACCOUNTID = '" + ObjCustomerAccountHistoryDetails.AccountID.ToString() + "'";
                Int32 ResultVal = ObjMySQLHelper.UpdateTableDetails("ACCOUNTSMASTER", ListTempColNames, ListTempColValues, 
                                    new List<Types>() { Types.Number, Types.String }, WhereCondition);

                return ResultVal;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateCustomerAccount()", ex);
                throw;
            }
        }
    }
}
