﻿using System;
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
    }
}