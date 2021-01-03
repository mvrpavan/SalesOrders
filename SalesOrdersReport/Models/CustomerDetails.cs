﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrdersReport
{
    enum OrderDays
    {
        MONDAY, TUESDAY, WEDNESDAY, THURSDAY, FRIDAY, SATURDAY, SUNDAY, NONE
    }
    class ImportFromExcelCustomerCache
    {
        public string CustomerColName = "CustomerName", LineColName = "LineName", PriceGroupColName = "PriceGroupName", DiscountGroupColName = "DiscountGroupName", StateColName = "State";
        public string PGDefaultColName = "PG_Default", DGDefaultColName = "DG_Default", ActiveColName = "Active", AddressColName = "Address", GSTINColName = "GSTIN";
        public string AddedDateColName = "AddedDate", LastUpdateDateColName = "LastUpdateDate", PhoneNoColName = "Phone", OrderDaysColName = "OrderDays";
        public string PGDiscCountTypeColName = "PG_DiscountType", DGDiscCountTypeColName = "DG_DiscountType", SelectedPriceGrpColName = "PriceGroupColumnName";
        //CustomerName,LineName,PriceGroupName,DiscountGroupName,State,PG_Default,DG_Default,Active,Address,GSTIN,AddedDate,LastUpdateDate,Phone,OrderDays,PG_DiscountType,DG_DiscountType,PriceGroupColumnName
    }
    class CustomerDetails : IComparer<CustomerDetails>
    {
        public string CustomerName = "";
        public string LineName = "", PriceGroupName = "", DiscountGroupName = "", State = "", StateCode = "";
        public int CustomerID = -1, LineID = -1, StateID = -1, PriceGroupID = -1, DiscountGroupID = -1;
        public Int32 LineIndex, PriceGroupIndex, DiscountGroupIndex;
        public DateTime AddedDate = DateTime.MinValue, LastUpdateDate = DateTime.MinValue;
        public string Address = "";
        public bool Active = true;
        public string GSTIN = "";
        public Int64 PhoneNo = 0;
        public string OrderDaysAssigned = "";
        public int Compare(CustomerDetails x, CustomerDetails y)
        {
            return x.CustomerName.ToUpper().CompareTo(y.CustomerName.ToUpper());
        }
        public CustomerDetails Clone()
        {
            return (CustomerDetails)this.MemberwiseClone();
        }
      

        //        ID AddedDate
        //SellerName LastUpdateDate
        //Address
        //Phone
        //GSTIN
        //OldBalance
        //PriceGroupID
        //DiscountGroupID
        //StateID
        //LineID
        //Active


    }
    class DiscountGroupDetails1 : IComparer<DiscountGroupDetails1>
    {
        public int DiscountGrpID = -1;
        public String DiscountGrpName = "", Description = "";
        public Double Discount = 0.0;
        public DiscountTypes DiscountType;
        public Boolean IsDefault;

        public int Compare(DiscountGroupDetails1 x, DiscountGroupDetails1 y)
        {
            return x.DiscountGrpName.ToUpper().CompareTo(y.DiscountGrpName.ToUpper());
        }

        public Double GetDiscountAmount(Double Amount)
        {
            try
            {
                switch (DiscountType)
                {
                    case DiscountTypes.PERCENT: return Amount * Discount;
                    case DiscountTypes.ABSOLUTE: return Discount;
                    case DiscountTypes.NONE:
                    default: return 0;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("DiscountGroupDetails.GetDiscountAmount()", ex);
            }
            return 0;
        }

        public DiscountGroupDetails1 Clone()
        {
            try
            {
                return (DiscountGroupDetails1)this.MemberwiseClone();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("DiscountGroupDetails.Clone()", ex);
            }
            return null;
        }
    }
    class LineDetails : IComparer<LineDetails>
    {
        public int LineID = -1;
        public string LineName = "", LineDescription = "";
        public int Compare(LineDetails x, LineDetails y)
        {
            return x.LineName.ToUpper().CompareTo(y.LineName.ToUpper());
        }
        //        ID
        //LineName
        //Description

    }
    class StateDetails : IComparer<StateDetails>
    {
        public string State = "", StateCode = "";
        public int StateID = -1;
        public int Compare(StateDetails x, StateDetails y)
        {
            return x.State.ToUpper().CompareTo(y.State.ToUpper());
        }

    }
    //class PriceGroupDetails : IComparer<PriceGroupDetails>
    //{
    //    public int PriceGroupID = -1;
    //    public string PriceGroupName = "", PriceGroupDescription = "";
    //    public List<string> ListPriceGroupColumn = new List<string>();
    //    public float Discount = 0.0f;
    //    public bool Default = false;
    //    public string DiscountType = "Percent";
    //    public int Compare(PriceGroupDetails x, PriceGroupDetails y)
    //    {
    //        return x.PriceGroupName.ToUpper().CompareTo(y.PriceGroupName.ToUpper());
    //    }
    //    //      PriceGroupID

    //    //PriceGroupName
    //    //  Description

    //    //  PriceColumn
    //    //  Discount

    //    //  DiscountType
    //    //  Default
    //}
    //class DiscountGroupDetails : IComparer<DiscountGroupDetails>
    //{
    //    public int DiscountGroupID = -1;
    //    public string DiscountGroupName = "", DiscountGroupDescription = "";
    //    public float Discount = 0.0f;
    //    public bool Default = false;
    //    public string DiscountType = "Percent";
    //    public int Compare(DiscountGroupDetails x, DiscountGroupDetails y)
    //    {
    //        return x.DiscountGroupName.ToUpper().CompareTo(y.DiscountGroupName.ToUpper());
    //    }
    //    //    ID
    //    //DiscountGroupName
    //    //Discount
    //    //DiscountType
    //    //Default
    //    //Description
    //}
}
