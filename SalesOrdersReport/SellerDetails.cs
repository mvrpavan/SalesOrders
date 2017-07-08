﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesOrdersReport
{
    class DiscountGroupDetails : IComparer<DiscountGroupDetails>
    {
        public String Name, Description;
        public Double Discount;
        public DiscountTypes DiscountType;
        public Boolean IsDefault;

        public int Compare(DiscountGroupDetails x, DiscountGroupDetails y)
        {
            return x.Name.ToUpper().CompareTo(y.Name.ToUpper());
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
    }

    class SellerDetails : IComparer<SellerDetails>
    {
        public String SellerName, Address, TINNumber, Phone, Line, PriceGroup, DiscountGroup;
        public Int32 LineIndex, PriceGroupIndex, DiscountGroupIndex;
        public Double OldBalance;

        public int Compare(SellerDetails x, SellerDetails y)
        {
            return x.SellerName.ToUpper().CompareTo(y.SellerName.ToUpper());
        }
    }

    class SellerMaster
    {
        List<SellerDetails> ListSellerDetails;
        public List<DiscountGroupDetails> ListDiscountGroups;
        Int32 DefaultDiscountGroupIndex;

        public void Initialize()
        {
            try
            {
                ListSellerDetails = new List<SellerDetails>();
                ListDiscountGroups = new List<DiscountGroupDetails>();
                DefaultDiscountGroupIndex = -1;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerMaster.Initialize()", ex);
            }
        }

        public void AddDiscountGroupToCache(DiscountGroupDetails ObjDiscountGroupDetails)
        {
            try
            {
                Int32 Index = ListDiscountGroups.BinarySearch(ObjDiscountGroupDetails, ObjDiscountGroupDetails);
                if (Index < 0)
                {
                    ListDiscountGroups.Insert(~Index, ObjDiscountGroupDetails);
                    DefaultDiscountGroupIndex = ListDiscountGroups.FindIndex(e => e.IsDefault);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerMaster.AddDiscountGroupToCache()", ex);
            }
        }

        public void AddSellerToCache(SellerDetails ObjSellerDetails, List<PriceGroupDetails> ListPriceGroups)
        {
            try
            {
                Int32 SellerIndex = ListSellerDetails.BinarySearch(ObjSellerDetails, ObjSellerDetails);
                if (SellerIndex < 0)
                {
                    ListSellerDetails.Insert(~SellerIndex, ObjSellerDetails);

                    ObjSellerDetails.LineIndex = CommonFunctions.ListLines.FindIndex(e => e.Equals(ObjSellerDetails.Line, StringComparison.InvariantCultureIgnoreCase));
                    ObjSellerDetails.PriceGroupIndex = ListPriceGroups.FindIndex(e => e.Name.Equals(ObjSellerDetails.PriceGroup, StringComparison.InvariantCultureIgnoreCase));
                    ObjSellerDetails.DiscountGroupIndex = ListDiscountGroups.FindIndex(e => e.Name.Equals(ObjSellerDetails.DiscountGroup, StringComparison.InvariantCultureIgnoreCase));
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerMaster.AddSellerToCache()", ex);
            }
        }

        public SellerDetails GetSellerDetails(String SellerName)
        {
            try
            {
                SellerDetails ObjSellerDetails = new SellerDetails();
                ObjSellerDetails.SellerName = SellerName;

                Int32 Index = ListSellerDetails.BinarySearch(ObjSellerDetails, ObjSellerDetails);
                if (Index < 0) return null;
                return ListSellerDetails[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerMaster.GetSellerDetails()", ex);
            }
            return null;
        }

        public Double GetSellerDiscount(String SellerName, Double Amount)
        {
            try
            {
                DiscountGroupDetails ObjDiscountGroupDetails = GetSellerDiscount(SellerName);
                if (ObjDiscountGroupDetails == null) return -1;

                return ObjDiscountGroupDetails.GetDiscountAmount(Amount);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerMaster.GetSellerDiscount()", ex);
            }
            return -1;
        }

        public DiscountGroupDetails GetSellerDiscount(String SellerName)
        {
            try
            {
                SellerDetails ObjSellerDetails = GetSellerDetails(SellerName);
                if (ObjSellerDetails == null) return null;
                Int32 DiscountGroupIndex = ObjSellerDetails.DiscountGroupIndex;
                if (DiscountGroupIndex < 0) DiscountGroupIndex = DefaultDiscountGroupIndex;

                return ListDiscountGroups[DiscountGroupIndex];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerMaster.GetSellerDiscount()", ex);
            }
            return null;
        }

        public List<String> GetSellerList()
        {
            try
            {
                return ListSellerDetails.Select(e => e.SellerName).ToList();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("SellerMaster.GetSellerList()", ex);
            }
            return null;
        }
    }
}
