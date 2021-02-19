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

    class PaymentsModel
    {
        List<PaymentModeDetails> ListPaymentModes;
        MySQLHelper ObjMySQLHelper;

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

        public DataTable GetPaytmentsDataTable(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                String Query = "";
                DataTable dtPayments = ObjMySQLHelper.GetQueryResultInDataTable(Query);

                return dtPayments;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetPaytmentsDataTable()", ex);
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
                    ListPaymentModes.Add(new PaymentModeDetails() {
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
