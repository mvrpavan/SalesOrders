using SalesOrdersReport.CommonModules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SalesOrdersReport.Models
{
    class VendorDetails : IComparer<VendorDetails>
    {
        public Int32 VendorID, StateID, PriceGroupID, DiscountGroupID;
        public String VendorName, Address, PhoneNo, GSTIN;
        public Int32 PriceGroupIndex, DiscountGroupIndex;
        public Boolean Active;
        public DateTime AddedDate, LastUpdateDate;

        public int Compare(VendorDetails x, VendorDetails y)
        {
            return x.VendorName.ToUpper().CompareTo(y.VendorName.ToUpper());
        }
    }

    class VendorMasterModel
    {
        List<VendorDetails> ListVendorDetails;
        public List<DiscountGroupDetails> ListDiscountGroups;
        Int32 DefaultDiscountGroupIndex;
        MySQLHelper ObjMySQLHelper;

        public void Initialize()
        {
            try
            {
                ListVendorDetails = new List<VendorDetails>();
                ListDiscountGroups = new List<DiscountGroupDetails>();
                DefaultDiscountGroupIndex = -1;
                ObjMySQLHelper = MySQLHelper.GetMySqlHelperObj();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorMaster.Initialize()", ex);
            }
        }

        public void LoadVendorMaster(DataTable dtVendorMaster)
        {
            try
            {
                #region Load Vendor Details
                for (int i = 0; i < dtVendorMaster.Rows.Count; i++)
                {
                    DataRow dtRow = dtVendorMaster.Rows[i];
                    //VendorID	VendorName	Address	PhoneNo	GSTIN	StateName   PriceGroup	DiscountGroup   Active  AddedDate LastUpdateDate

                    VendorDetails ObjVendorDetails = new VendorDetails();
                    ObjVendorDetails.VendorID = Int32.Parse(dtRow["VendorID"].ToString().Trim());
                    ObjVendorDetails.VendorName = dtRow["VendorName"].ToString().Trim();
                    ObjVendorDetails.Address = dtRow["Address"].ToString().Trim();
                    ObjVendorDetails.PhoneNo = dtRow["PhoneNo"].ToString().Trim();
                    ObjVendorDetails.GSTIN = dtRow["GSTIN"].ToString().Trim();
                    ObjVendorDetails.StateID = Int32.Parse(dtRow["StateID"].ToString().Trim());
                    ObjVendorDetails.PriceGroupID = (dtRow["PriceGroupID"] == DBNull.Value) ? -1 : Int32.Parse(dtRow["PriceGroupID"].ToString().Trim());
                    ObjVendorDetails.DiscountGroupID = (dtRow["DiscountGroupID"] == DBNull.Value) ? -1 : Int32.Parse(dtRow["DiscountGroupID"].ToString().Trim());
                    ObjVendorDetails.Active = dtRow["Active"].ToString().Trim().Equals("1");
                    ObjVendorDetails.AddedDate = DateTime.Parse(dtRow["AddedDate"].ToString().Trim());
                    ObjVendorDetails.LastUpdateDate = DateTime.Parse(dtRow["LastUpdateDate"].ToString().Trim());

                    AddVendorToCache(ObjVendorDetails);
                }
                #endregion
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductLine.LoadVendorMaster()", ex);
            }
        }

        public void AddVendorToCache(VendorDetails ObjVendorDetails)
        {
            try
            {
                Int32 VendorIndex = ListVendorDetails.BinarySearch(ObjVendorDetails, ObjVendorDetails);
                if (VendorIndex < 0)
                {
                    ListVendorDetails.Insert(~VendorIndex, ObjVendorDetails);

                    PriceGroupDetails priceGroupDetails = null;
                    Models.DiscountGroupDetails discountGroupDetails = null;
                    if (ObjVendorDetails.PriceGroupID > 0) priceGroupDetails = CommonFunctions.ObjCustomerMasterModel.GetPriceGrpDetails(ObjVendorDetails.PriceGroupID);
                    if (ObjVendorDetails.DiscountGroupID > 0) discountGroupDetails = CommonFunctions.ObjCustomerMasterModel.GetDiscountGrpDetails(ObjVendorDetails.DiscountGroupID);

                    ObjVendorDetails.PriceGroupIndex = ObjVendorDetails.DiscountGroupIndex = -1;
                    if (priceGroupDetails != null) ObjVendorDetails.PriceGroupIndex = CommonFunctions.ObjCustomerMasterModel.GetAllDiscountGrp().FindIndex(e => e.Equals(priceGroupDetails.PriceGrpName));
                    if (discountGroupDetails != null) ObjVendorDetails.DiscountGroupIndex = CommonFunctions.ObjCustomerMasterModel.GetAllDiscountGrp().FindIndex(e => e.Equals(discountGroupDetails.DiscountGrpName, StringComparison.InvariantCultureIgnoreCase));
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


        public VendorDetails GetVendorDetails(Int32 VendorID)
        {
            try
            {
                Int32 Index = ListVendorDetails.FindIndex(e => e.VendorID == VendorID);
                if (Index < 0) return null;
                return ListVendorDetails[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorMaster.GetVendorDetails(VendorID)", ex);
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

        public VendorDetails CreateNewVendor(VendorDetails ObjVendorDetails)
        {
            try
            {
                VendorDetails tmpVendorDetails = GetVendorDetails(ObjVendorDetails.VendorName);
                if (tmpVendorDetails != null)
                {
                    return tmpVendorDetails;
                }

                //Insert new Vendor to VendorMaster
                Int32 RetVal = ObjMySQLHelper.InsertIntoTable("VendorMaster",
                                                        new List<string>() { "VendorName", "Address", "PhoneNo", "GSTIN", "StateID", "Active" },
                                                        new List<string>() { ObjVendorDetails.VendorName, ObjVendorDetails.Address, ObjVendorDetails.PhoneNo,
                                                            ObjVendorDetails.GSTIN, ObjVendorDetails.StateID.ToString(), (ObjVendorDetails.Active ? "1" : "0") },
                                                        new List<Types>() { Types.String, Types.String, Types.String, Types.String, Types.Number, Types.Number });

                if (RetVal <= 0) return null;
                Int32 VendorID = Int32.Parse(ObjMySQLHelper.ExecuteScalar($"Select VendorID from VendorMaster Where VendorName= '{ObjVendorDetails.VendorName}';").ToString());
                ObjVendorDetails.VendorID = VendorID;
                AddVendorToCache(ObjVendorDetails);

                return GetVendorDetails(ObjVendorDetails.VendorName);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("VendorMaster.CreateNewVendor()", ex);
            }
            return null;
        }
    }
}
