using SalesOrdersReport.CommonModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesOrdersReport
{
    class VendorDetails : IComparer<VendorDetails>
    {
        public String VendorName, Address, TINNumber, Phone, Line, PriceGroup, DiscountGroup;
        public Int32 LineIndex, PriceGroupIndex, DiscountGroupIndex;

        public int Compare(VendorDetails x, VendorDetails y)
        {
            return x.VendorName.ToUpper().CompareTo(y.VendorName.ToUpper());
        }
    }

    class VendorMaster
    {
        List<VendorDetails> ListVendorDetails;
        public List<DiscountGroupDetails> ListDiscountGroups;
        Int32 DefaultDiscountGroupIndex;

        public void Initialize()
        {
            try
            {
                ListVendorDetails = new List<VendorDetails>();
                ListDiscountGroups = new List<DiscountGroupDetails>();
                DefaultDiscountGroupIndex = -1;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorMaster.Initialize()", ex);
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
                CommonFunctions.ShowErrorDialog("VendorMaster.AddDiscountGroupToCache()", ex);
            }
        }

        public void AddVendorToCache(VendorDetails ObjVendorDetails, List<PriceGroupDetails> ListPriceGroups)
        {
            try
            {
                Int32 VendorIndex = ListVendorDetails.BinarySearch(ObjVendorDetails, ObjVendorDetails);
                if (VendorIndex < 0)
                {
                    ListVendorDetails.Insert(~VendorIndex, ObjVendorDetails);

                    ObjVendorDetails.LineIndex = CommonFunctions.ListVendorLines.FindIndex(e => e.Equals(ObjVendorDetails.Line, StringComparison.InvariantCultureIgnoreCase));
                    ObjVendorDetails.PriceGroupIndex = ListPriceGroups.FindIndex(e => e.PriceGrpName.Equals(ObjVendorDetails.PriceGroup, StringComparison.InvariantCultureIgnoreCase));
                    ObjVendorDetails.DiscountGroupIndex = ListDiscountGroups.FindIndex(e => e.Name.Equals(ObjVendorDetails.DiscountGroup, StringComparison.InvariantCultureIgnoreCase));
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorMaster.AddVendorToCache()", ex);
            }
        }

        public VendorDetails GetVendorDetails(String VendorName)
        {
            try
            {
                VendorDetails ObjVendorDetails = new VendorDetails();
                ObjVendorDetails.VendorName = VendorName;

                Int32 Index = ListVendorDetails.BinarySearch(ObjVendorDetails, ObjVendorDetails);
                if (Index < 0) return null;
                return ListVendorDetails[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorMaster.GetVendorDetails()", ex);
            }
            return null;
        }

        public Double GetVendorDiscount(String VendorName, Double Amount)
        {
            try
            {
                DiscountGroupDetails ObjDiscountGroupDetails = GetVendorDiscount(VendorName);
                if (ObjDiscountGroupDetails == null) return -1;

                return ObjDiscountGroupDetails.GetDiscountAmount(Amount);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorMaster.GetVendorDiscount()", ex);
            }
            return -1;
        }

        public DiscountGroupDetails GetVendorDiscount(String VendorName)
        {
            try
            {
                VendorDetails ObjVendorDetails = GetVendorDetails(VendorName);
                if (ObjVendorDetails == null) return null;
                Int32 DiscountGroupIndex = ObjVendorDetails.DiscountGroupIndex;
                if (DiscountGroupIndex < 0) DiscountGroupIndex = DefaultDiscountGroupIndex;

                return ListDiscountGroups[DiscountGroupIndex];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorMaster.GetVendorDiscount()", ex);
            }
            return null;
        }

        public List<String> GetVendorList()
        {
            try
            {
                return ListVendorDetails.Select(e => e.VendorName).ToList();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorMaster.GetVendorList()", ex);
            }
            return null;
        }
    }
}
