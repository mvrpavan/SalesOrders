using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesOrdersReport.CommonModules;

namespace SalesOrdersReport.Models
{
    class PaymentModeDetails
    {
        public Int32 PaymentModeID;
        public String PaymentMode, Description;

    }
    class PaymentDetails
    {
        public int PaymentId = -1, InvoiceID = -1, QuotationID = -1, AccountID = -1, PaymentModeID = -1, CustomerID = -1, UserID = -1;
        public string InvoiceNumber = "", QuotationNumber = "", CardNumber = "";
        public string CustomerName = "", CustPhoneNo = "";
        public DateTime PaidOn, LastUpdateDate, CreationDate;
        public String PaymentMode = "", Description = "", PaymentAgainst = "";
        public string StaffName = "";
        public double Amount = 0.0;
        public bool Active = true;
        //public DateTime InvoiceDate = DateTime.MinValue;
        //public int InvoiceItems = 0;
        //public double SaleAmount = 0.0;
        //public double NetSaleAmount = 0.0;
        //public double CancelAmount = 0.0;
        //public double ReturnAmount = 0.0;
        //public double DiscountAmount = 0.0;
        //public double TotalTax = 0.0;
    }
    class PaymentsModel
    {

        List<PaymentModeDetails> ListPaymentModes = new List<PaymentModeDetails>();
        List<PaymentDetails> ListPaymentDetails = new List<PaymentDetails>();
        MySQLHelper ObjMySQLHelper;
        string TempQueryStr = " a.*, b.CUSTOMERID, c.CUSTOMERNAME,c.PHONENO,d.INVOICENUMBER,e.USERNAME FROM PAYMENTS a "
                           + " Inner Join ACCOUNTSMASTER b on a.ACCOUNTID = b.ACCOUNTID "
                            + " Inner Join CUSTOMERMASTER c on b.CUSTOMERID = c.CUSTOMERID "
                            + " Inner Join Invoices d on a.INVOICEID = d.INVOICEID "
                            + " Inner Join USERMASTER e on a.USERID = e.USERID ";


   
        public PaymentsModel()
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

        public List<String> GetPaymentModesList()
        {
            try
            {
                if (ListPaymentModes == null || ListPaymentModes.Count == 0) return null;
                return ListPaymentModes.Select(e => e.PaymentMode).OrderBy(s => s).ToList();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetPaymentModesList()", ex);
                return null;
            }
        }


        public PaymentModeDetails GetPaymentModeDetails(Int32 PaymentModeID)
        {
            try
            {
                if (ListPaymentModes == null || ListPaymentModes.Count == 0) return null;
                Int32 Index = ListPaymentModes.FindIndex(e => e.PaymentModeID == PaymentModeID);
                if (Index < 0) return null;

                return ListPaymentModes[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetPaymentModeDetails(PaymentModeID)", ex);
                return null;
            }
        }

        public PaymentModeDetails GetPaymentModeDetails(String PaymentMode)
        {
            try
            {
                if (ListPaymentModes == null || ListPaymentModes.Count == 0) return null;
                Int32 Index = ListPaymentModes.FindIndex(e => e.PaymentMode == PaymentMode);
                if (Index < 0) return null;

                return ListPaymentModes[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetPaymentModeDetails(PaymentMode)", ex);
                return null;
            }
        }

        public List<PaymentDetails> GetPaymentDtlsCache()
        {
            try
            {
                return ListPaymentDetails;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetPaymentDtlsCache", ex);
                return null;
            }
        }

        //public PaymentDetails GetPaymentDetailsFromAccID(int AccID)
        //{

        //    try
        //    {
        //        int Index = ListPaymentDetails.FindIndex(e => e.AccountID == AccID);
        //        return ListPaymentDetails[Index];
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonFunctions.ShowErrorDialog($"{this}.GetPaymentDetailsFromAccID", ex);
        //        return null;
        //    }
        //}

        public DataTable GetPaytmentsDataTable(DateTime FromDate, DateTime ToDate, Int32 InvoiceID = -1, Boolean Active = true)
        {
            try
            {
                String Query = "";
                if (FromDate != DateTime.MinValue && ToDate == DateTime.MinValue) Query = "SELECT " + TempQueryStr + " WHERE (a.CREATIONDATE >= '" + MySQLHelper.GetTimeStampStrForSearch(FromDate) + "')";
                else if (FromDate == DateTime.MinValue && ToDate != DateTime.MinValue) Query = "SELECT " + TempQueryStr + " WHERE (a.CREATIONDATE <= '" + MySQLHelper.GetTimeStampStrForSearch(ToDate, false) + "')";
                else if (FromDate != DateTime.MinValue && ToDate != DateTime.MinValue) Query = "SELECT " + TempQueryStr + " WHERE (a.CREATIONDATE BETWEEN '" + MySQLHelper.GetTimeStampStrForSearch(FromDate) + "' AND '" + MySQLHelper.GetTimeStampStrForSearch(ToDate, false) + "')";
                else Query = "SELECT "+ TempQueryStr + " Where 1 = 1";

                if (InvoiceID > 0) Query += $" and a.InvoiceID = {InvoiceID}";
                Query += $" and a.Active = {(Active ? 1 : 0)}";

                DataTable dtPayments = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                LoadPaymentDetails(dtPayments);
                return dtPayments;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetPaytmentsDataTable()", ex);
                return null;
            }
        }

        public List<PaymentDetails> GetPaymentDetailsForInvoice(Int32 InvoiceID)
        {
            try
            {
                GetPaytmentsDataTable(DateTime.MinValue, DateTime.MinValue, InvoiceID);
                return ListPaymentDetails;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetPaymentDetailsForInvoice()", ex);
                return null;
            }
        }

        public Int32 DeletePaymentDetails(Int32 PaymentID)
        {
            try
            {
                ObjMySQLHelper.UpdateTableDetails("PAYMENTS", new List<string>() { "ACTIVE" }, new List<string>() { "0" },
                                            new List<Types>() { Types.Number }, $"PAYMENTID = {PaymentID}");


                ListPaymentDetails.Find(e => e.PaymentId == PaymentID).Active = false;

                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.DeletePaymentDetails()", ex);
                return -1;
            }
        }

        public void LoadPaymentDetails(DataTable dtPayments)
        {
            try
            {
                ListPaymentDetails = new List<PaymentDetails>();
                if (ListPaymentModes.Count == 0 || ListPaymentModes == null) LoadPaymentModes();
                for (int i = 0; i < dtPayments.Rows.Count; i++)
                {
                    DataRow dr = dtPayments.Rows[i];

                    PaymentDetails ObjPaymentDetails = new PaymentDetails();
                    ObjPaymentDetails.PaymentId = ((dr["PAYMENTID"] == null) || dr["PAYMENTID"].ToString().Trim() == "") ? -1 : int.Parse(dr["PAYMENTID"].ToString().Trim());
                    PaymentModeDetails ObjPayModeDtls = GetPaymentModeDetails(int.Parse(dr["PAYMENTMODEID"].ToString().Trim() == string.Empty ? "0" : dr["PAYMENTMODEID"].ToString().Trim()));
                    if (ObjPayModeDtls != null) ObjPaymentDetails.PaymentMode = ObjPayModeDtls.PaymentMode;
                    ObjPaymentDetails.Description = dr["DESCRIPTION"].ToString();
                    ObjPaymentDetails.AccountID = int.Parse(dr["ACCOUNTID"].ToString());
                    ObjPaymentDetails.PaidOn = DateTime.Parse(dr["PAYMENTDATE"].ToString());
                    ObjPaymentDetails.Amount = Double.Parse(dr["PAYMENTAMOUNT"].ToString());
                    ObjPaymentDetails.CreationDate = DateTime.Parse(dr["CREATIONDATE"].ToString());
                    ObjPaymentDetails.LastUpdateDate = DateTime.Parse(dr["LASTUPDATEDATE"].ToString());
                    ObjPaymentDetails.StaffName = dr["USERNAME"].ToString();
                    ObjPaymentDetails.CustPhoneNo = dr["PHONENO"].ToString();
                    ObjPaymentDetails.Active = dr["ACTIVE"].ToString() == "1" ? true : false;
                    ObjPaymentDetails.InvoiceID = int.Parse(dr["INVOICEID"].ToString());
                    ObjPaymentDetails.InvoiceNumber = dr["INVOICENUMBER"].ToString();
                    ObjPaymentDetails.CustomerName= dr["CUSTOMERNAME"].ToString();
                    ObjPaymentDetails.UserID = int.Parse(dr["USERID"].ToString());
                    ListPaymentDetails.Add(ObjPaymentDetails);
                }
            } 
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadPaymentDetails()", ex);
            }
        }


        public DataTable GetPaymentSummaryTable()
        {
            try
            {
                DataTable dt = new DataTable();
                String Query = "SELECT a.INVOICEID,a.INVOICENUMBER as 'INVOICE#',b.CUSTOMERNAME,c.LINENAME,a.GROSSINVOICEAMOUNT as SALE,a.NETINVOICEAMOUNT as 'NET SALE',a.DISCOUNTAMOUNT as DISCOUNT,e.BALANCEAMOUNT as OB "
                          + " FROM INVOICES a INNER JOIN CUSTOMERMASTER b on a.CUSTOMERID = b.CUSTOMERID "
                          + " INNER JOIN LINEMASTER c on a.DELIVERYLINEID = c.LINEID "
                          + " Inner Join ACCOUNTSMASTER e on e.CUSTOMERID = a.CUSTOMERID "
                          + " WHERE a.INVOICESTATUS = 'Created' OR a.INVOICESTATUS = 'DELIVERED'; ";

                dt = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                return dt;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadPaymentDetails()", ex);
                return null;
            }
        }

        public void LoadPaymentModes()
        {
            try
            {
                ListPaymentModes = new List<PaymentModeDetails>();

                String Query = "Select PaymentModeID, PaymentMode, Description from PaymentModeMaster Order by PaymentModeID;";
                foreach (var item in ObjMySQLHelper.ExecuteQuery(Query))
                {
                    ListPaymentModes.Add(new PaymentModeDetails()
                    {
                        PaymentModeID = Int32.Parse(item[0]),
                        PaymentMode = item[1],
                        Description = item[2]
                    });
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadPaymentModes()", ex);
            }
        }

        public List<string> GetAllPaymentsModeNames()
        {
            try
            {
                List<string> ListAllPaymentModesNames = ListPaymentModes.Select(e => e.PaymentMode).ToList();
                return ListAllPaymentModesNames;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetAllPaymentsModeNames()", ex);
                return null;
            }
        }
        public Int32 CreateNewPaymentDetails(ref PaymentDetails ObjPaymentDetails, ref CustomerAccountHistoryDetails ObjCustomerAccountHistoryDetails)
        {
            try
            {
                InvoicesModel ObjInvoicesModel = new InvoicesModel();
                ObjInvoicesModel.Initialize();

                List<string> ListColumnValues = new List<string>(), ListTempColValues = new List<string>();
                List<string> ListColumnNames = new List<string>(), ListTempColNames = new List<string>();
                List<Types> ListTypes = new List<Types>();

                ListColumnValues.Add(ObjPaymentDetails.AccountID.ToString());
                ListColumnNames.Add("ACCOUNTID");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(ObjPaymentDetails.PaymentModeID.ToString());
                ListColumnNames.Add("PAYMENTMODEID");
                ListTypes.Add(Types.Number);

                if (ObjPaymentDetails.InvoiceID <= 0) ObjPaymentDetails.InvoiceID = 0;
                ListColumnValues.Add(ObjPaymentDetails.InvoiceID.ToString());
                ListColumnNames.Add("INVOICEID");
                ListTypes.Add(Types.Number);

                if (ObjPaymentDetails.QuotationID <= 0) ObjPaymentDetails.QuotationID = 0;
                ListColumnValues.Add(ObjPaymentDetails.QuotationID.ToString());
                ListColumnNames.Add("QUOTATIONID");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(MySQLHelper.GetDateTimeStringForDB(ObjPaymentDetails.PaidOn));
                ListColumnNames.Add("PAYMENTDATE");
                ListTypes.Add(Types.String);

                ListColumnValues.Add("1");
                ListColumnNames.Add("ACTIVE");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(ObjPaymentDetails.Amount.ToString());
                ListColumnNames.Add("PAYMENTAMOUNT");
                ListTypes.Add(Types.Number);

                ListColumnValues.Add(ObjPaymentDetails.UserID.ToString());
                ListColumnNames.Add("USERID");
                ListTypes.Add(Types.Number);

                if (ObjPaymentDetails.Description.Trim() != string.Empty)
                {
                    ListColumnValues.Add(ObjPaymentDetails.Description.Trim());
                    ListColumnNames.Add("DESCRIPTION");
                    ListTypes.Add(Types.String);
                }

                string Now = MySQLHelper.GetDateTimeStringForDB(DateTime.Now);
                ListColumnValues.Add(Now);
                ListColumnNames.Add("LASTUPDATEDATE");
                ListTypes.Add(Types.String);

                ListColumnValues.Add(Now);
                ListColumnNames.Add("CREATIONDATE");
                ListTypes.Add(Types.String);

                int ResultVal = ObjMySQLHelper.InsertIntoTable("PAYMENTS", ListColumnNames, ListColumnValues, ListTypes);
                ObjPaymentDetails.PaymentId = Int32.Parse(ObjMySQLHelper.ExecuteScalar($"Select Max(PaymentID) PaymentID from PAYMENTS Where ACCOUNTID = {ObjPaymentDetails.AccountID}" +
                                            $" and INVOICEID = {ObjPaymentDetails.InvoiceID} and QUOTATIONID = {ObjPaymentDetails.QuotationID};").ToString());

                if (ObjPaymentDetails.InvoiceID > 0 && ObjCustomerAccountHistoryDetails.SaleAmount > 0) ObjInvoicesModel.MarkInvoicesAsPaid(new List<int>() { ObjPaymentDetails.InvoiceID });
                if (ResultVal <= 0) return -1;

                ObjCustomerAccountHistoryDetails.PaymentID = ObjPaymentDetails.PaymentId;
                CustomerAccountHistoryModel ObjAccountHistoryModel = new CustomerAccountHistoryModel();
                ObjCustomerAccountHistoryDetails = ObjAccountHistoryModel.CreateNewCustomerAccountHistoryEntry(ObjCustomerAccountHistoryDetails);
                if (ObjCustomerAccountHistoryDetails == null) return -2;

                ListTempColValues.Add(ObjCustomerAccountHistoryDetails.NewBalanceAmount.ToString());
                ListTempColNames.Add("BALANCEAMOUNT");

                ListTempColValues.Add(Now);
                ListTempColNames.Add("LASTUPDATEDDATE");

                string WhereCondition = "ACCOUNTID = '" + ObjPaymentDetails.AccountID.ToString() + "'";
                ResultVal = ObjMySQLHelper.UpdateTableDetails("ACCOUNTSMASTER", ListTempColNames, ListTempColValues, new List<Types>() { Types.Number, Types.String }, WhereCondition);

                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreateNewPaymentDetails()", ex);
                return -1;
            }
        }
    }
}
