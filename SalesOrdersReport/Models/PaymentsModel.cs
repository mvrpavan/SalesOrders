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
        public string InvoiceNumber = "", QuotationNumber = "";
        public string CustomerName = "";
        public DateTime PaidOn , LastUpdateDate;
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

        //&&&&& binary search?


    }
    class PaymentsModel
    {

        List<PaymentModeDetails> ListPaymentModes = new List<PaymentModeDetails>();
        List<PaymentDetails> ListPaymentDetails = new List<PaymentDetails>();
        MySQLHelper ObjMySQLHelper;
        string TempQueryStr = " a.*, b.CUSTOMERID, c.CUSTOMERNAME,d.INVOICENUMBER,e.USERNAME FROM PAYMENTS a "
                           + " Inner Join ACCOUNTSMASTER b on a.ACCOUNTID = b.ACCOUNTID "
                            + " Inner Join CUSTOMERMASTER c on b.CUSTOMERID = c.CUSTOMERID "
                            + " Inner Join INVOICES d on a.INVOICEID = d.INVOICEID "
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

        public PaymentDetails GetPaymentDetailsFromAccID(int AccID)
        {

            try
            {
                int Index = ListPaymentDetails.FindIndex(e => e.AccountID == AccID);
                return ListPaymentDetails[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetPaymentDetailsFromAccID", ex);
                return null;
            }
        }

        public DataTable GetPaytmentsDataTable(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                //FromDate.ToString("yyyy-MM-dd H:mm:ss")
                String Query = "";
                if (FromDate != DateTime.MinValue && ToDate == DateTime.MinValue) Query = "SELECT "+ TempQueryStr + " WHERE (a.CREATIONDATE >='" + FromDate.ToString("yyyy-MM-dd H:mm:ss");
                else if (FromDate == DateTime.MinValue && ToDate != DateTime.MinValue) Query = "SELECT " + TempQueryStr + " WHERE (a.CREATIONDATE <='" + ToDate.ToString("yyyy-MM-dd H:mm:ss");
                else if (FromDate != DateTime.MinValue && ToDate != DateTime.MinValue) Query = "SELECT " + TempQueryStr + " WHERE (a.CREATIONDATE BETWEEN '" + FromDate.ToString("yyyy-MM-dd H:mm:ss") + "' AND '" + ToDate.ToString("yyyy-MM-dd H:mm:ss") + "')";
                else Query = "SELECT "+ TempQueryStr ;

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
                    ObjPaymentDetails.PaidOn = DateTime.Parse(dr["CREATIONDATE"].ToString());
                    ObjPaymentDetails.LastUpdateDate = DateTime.Parse(dr["LASTUPDATEDATE"].ToString());
                    //ObjPaymentDetails.StaffName = CommonFunctions.ObjUserMasterModel.GetUserName(int.Parse(dr["USERID"].ToString()));
                    ObjPaymentDetails.StaffName = dr["USERNAME"].ToString();
                    ObjPaymentDetails.Active = dr["ACTIVE"].ToString() == "1" ? true : false;
                    ObjPaymentDetails.InvoiceID = int.Parse(dr["INVOICEID"].ToString());
                    //ObjPaymentDetails.InvoiceDate = DateTime.Parse(dr["INVOICEDATE"].ToString());
                    ObjPaymentDetails.InvoiceNumber = dr["INVOICENUMBER"].ToString();
                    ObjPaymentDetails.CustomerName= dr["CUSTOMERNAME"].ToString();
                    ObjPaymentDetails.StaffName= dr["USERNAME"].ToString();
                    ObjPaymentDetails.UserID = int.Parse(dr["USERID"].ToString());
                    ListPaymentDetails.Add(ObjPaymentDetails);
                }
            } 
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadPaymentDetails()", ex);
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
    }
}
