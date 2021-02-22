using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using SalesOrdersReport.CommonModules;

namespace SalesOrdersReport.Models
{
    class ProductLine
    {
        public String Name;
        public Settings ObjSettings;
        public XmlNode ProductLineNode;
        public ProductMasterModel ObjProductMaster;
        //public SellerMaster ObjSellerMaster;
        public VendorMaster ObjVendorMaster;
        MySQLHelper ObjMySQLHelper;

        public ProductLine()
        {
            try
            {
                ObjProductMaster = new ProductMasterModel();
                ObjProductMaster.Initialize();
                ObjMySQLHelper = MySQLHelper.GetMySqlHelperObj();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductLine.ctor()", ex);
            }
        }

        public Boolean LoadConfigDetailsFromNode(XmlNode ProductLineNode)
        {
            try
            {
                this.ProductLineNode = ProductLineNode;
                XMLFileUtils.GetAttributeValue(ProductLineNode, "Name", out Name);

                ObjSettings = new Settings();
                XmlNode GeneralNode;
                XMLFileUtils.GetChildNode(ProductLineNode, "General", out GeneralNode);
                ObjSettings.LoadSettingsFromNode(GeneralNode);

                XmlNode OrderNode;
                XMLFileUtils.GetChildNode(ProductLineNode, "Order", out OrderNode);
                ObjSettings.LoadSettingsFromNode(OrderNode);

                XmlNode InvoiceNode;
                XMLFileUtils.GetChildNode(ProductLineNode, "Invoice", out InvoiceNode);
                ObjSettings.LoadSettingsFromNode(InvoiceNode);

                XmlNode QuotationNode;
                XMLFileUtils.GetChildNode(ProductLineNode, "Quotation", out QuotationNode);
                ObjSettings.LoadSettingsFromNode(QuotationNode);

                XmlNode PurchaseOrderNode;
                XMLFileUtils.GetChildNode(ProductLineNode, "PurchaseOrder", out PurchaseOrderNode);
                ObjSettings.LoadSettingsFromNode(PurchaseOrderNode);

                return true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductLine.LoadConfigDetailsFromNode()", ex);
                return false;
            }
        }

        public void LoadAllProductMasterTables()
        {
            try
            {
                String Query = "Select * from PRICEGROUPMASTER Order by PriceGroupName;";
                DataTable dtPriceGroupMaster = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                ObjProductMaster.LoadPriceGroupMaster(dtPriceGroupMaster);

                Query = "Select * from TaxMaster Order by HSNCode;";
                DataTable dtTaxMaster = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                ObjProductMaster.LoadTaxMaster(dtTaxMaster);

                Query = "Select * from ProductCategoryMaster Order by CategoryID;";
                DataTable dtCategoryMaster = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                ObjProductMaster.LoadProductCategoryMaster(dtCategoryMaster);

                Query = "Select * from ProductInventory Order by StockName;";
                DataTable dtProductInventory = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                ObjProductMaster.LoadProductInventory(dtProductInventory);

                Query = "Select * from ProductMaster Order by ProductName;";
                DataTable dtProductMaster = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                ObjProductMaster.LoadProductMaster(dtProductMaster);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductLine.LoadProductMaster()", ex);
                throw ex;
            }
        }

        //public void LoadSellerMaster(DataTable dtSellerMaster, DataTable dtDiscountGroupMaster)
        //{
        //    try
        //    {
        //        ObjSellerMaster = new SellerMaster();
        //        ObjSellerMaster.Initialize();

        //        #region Load Line from Seller Master
        //        CommonFunctions.ListCustomerLines = new List<String>();
        //        Boolean ContainsBlanks = false;
        //        for (int i = 0; i < dtSellerMaster.Rows.Count; i++)
        //        {
        //            DataRow dtRow = dtSellerMaster.Rows[i];
        //            String Line = dtRow["Line"].ToString().Replace("<", "").Replace(">", "").ToUpper();
        //            if (Line.Trim().Length == 0) ContainsBlanks = true;
        //            else if (!CommonFunctions.ListCustomerLines.Contains(Line)) CommonFunctions.ListCustomerLines.Add(Line);
        //        }

        //        CommonFunctions.ListCustomerLines.Sort();
        //        CommonFunctions.ListCustomerLines.Insert(0, "<All>");
        //        if (ContainsBlanks) CommonFunctions.ListCustomerLines.Add("<Blanks>");
        //        #endregion

        //        #region Load Discount Groups
        //        for (int i = 0; i < dtDiscountGroupMaster.Rows.Count; i++)
        //        {
        //            DataRow dtRow = dtDiscountGroupMaster.Rows[i];
        //            //DiscountGroup	Discount	DiscountType	Default	Description

        //            DiscountGroupDetails ObjDiscountGroupDetails = new DiscountGroupDetails();
        //            ObjDiscountGroupDetails.Name = dtRow["DiscountGroup"].ToString().Trim();
        //            ObjDiscountGroupDetails.Discount = Double.Parse(dtRow["Discount"].ToString().Trim());
        //            ObjDiscountGroupDetails.DiscountType = PriceGroupDetails.GetDiscountType(dtRow["DiscountType"].ToString().Trim());
        //            ObjDiscountGroupDetails.IsDefault = (Int32.Parse(dtRow["Default"].ToString().Trim()) == 1);
        //            ObjDiscountGroupDetails.Description = dtRow["Description"].ToString().Trim();

        //            ObjSellerMaster.AddDiscountGroupToCache(ObjDiscountGroupDetails);
        //        }
        //        #endregion

        //        #region Load Sellers Details
        //        for (int i = 0; i < dtSellerMaster.Rows.Count; i++)
        //        {
        //            DataRow dtRow = dtSellerMaster.Rows[i];
        //            //SlNo	SellerName	Address	TINNumber	Phone	Line	OldBalance	PriceGroup	DiscountGroup

        //            SellerDetails ObjSellerDetails = new SellerDetails();
        //            ObjSellerDetails.Name = dtRow["SellerName"].ToString().Trim();
        //            ObjSellerDetails.Address = dtRow["Address"].ToString().Trim();
        //            ObjSellerDetails.TINNumber = dtRow["TINNumber"].ToString().Trim();
        //            ObjSellerDetails.Phone = dtRow["Phone"].ToString().Trim();
        //            ObjSellerDetails.Line = dtRow["Line"].ToString().Trim();
        //            ObjSellerDetails.OldBalance = 0;
        //            ObjSellerDetails.PriceGroup = "";
        //            ObjSellerDetails.DiscountGroup = "";
        //            ObjSellerDetails.State = "";
        //            ObjSellerDetails.StateCode = "";
        //            ObjSellerDetails.GSTIN = "";

        //            if (dtRow["OldBalance"] != DBNull.Value && dtRow["OldBalance"].ToString().Trim().Length > 0)
        //                ObjSellerDetails.OldBalance = Double.Parse(dtRow["OldBalance"].ToString().ToString());
        //            if (dtRow["PriceGroup"] != DBNull.Value && dtRow["PriceGroup"].ToString().Trim().Length > 0)
        //                ObjSellerDetails.PriceGroup = dtRow["PriceGroup"].ToString().Trim();
        //            if (dtRow["DiscountGroup"] != DBNull.Value && dtRow["DiscountGroup"].ToString().Trim().Length > 0)
        //                ObjSellerDetails.DiscountGroup = dtRow["DiscountGroup"].ToString().Trim();
        //            if (dtRow["State"] != DBNull.Value && dtRow["State"].ToString().Trim().Length > 0)
        //                ObjSellerDetails.State = dtRow["State"].ToString().Trim();
        //            if (dtRow["StateCode"] != DBNull.Value && dtRow["StateCode"].ToString().Trim().Length > 0)
        //                ObjSellerDetails.StateCode = dtRow["StateCode"].ToString().Trim();
        //            if (dtRow["GSTIN"] != DBNull.Value && dtRow["GSTIN"].ToString().Trim().Length > 0)
        //                ObjSellerDetails.GSTIN = dtRow["GSTIN"].ToString().Trim();

        //            ObjSellerMaster.AddSellerToCache(ObjSellerDetails, ObjProductMaster.ListPriceGroups);
        //        }
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonFunctions.ShowErrorDialog("ProductLine.LoadSellerMaster()", ex);
        //    }
        //}

        public void LoadVendorMaster(DataTable dtVendorMaster, DataTable dtDiscountGroupMaster)
        {
            try
            {
                ObjVendorMaster = new VendorMaster();
                ObjVendorMaster.Initialize();

                #region Load Line from Vendor Master
                CommonFunctions.ListVendorLines = new List<String>();
                Boolean ContainsBlanks = false;
                for (int i = 0; i < dtVendorMaster.Rows.Count; i++)
                {
                    DataRow dtRow = dtVendorMaster.Rows[i];
                    String Line = dtRow["Line"].ToString().Replace("<", "").Replace(">", "").ToUpper();
                    if (Line.Trim().Length == 0) ContainsBlanks = true;
                    else if (!CommonFunctions.ListVendorLines.Contains(Line)) CommonFunctions.ListVendorLines.Add(Line);
                }

                CommonFunctions.ListVendorLines.Sort();
                CommonFunctions.ListVendorLines.Insert(0, "<All>");
                if (ContainsBlanks) CommonFunctions.ListVendorLines.Add("<Blanks>");
                #endregion

                #region Load Discount Groups
                for (int i = 0; i < dtDiscountGroupMaster.Rows.Count; i++)
                {
                    DataRow dtRow = dtDiscountGroupMaster.Rows[i];
                    //DiscountGroup	Discount	DiscountType	Default	Description

                    DiscountGroupDetails ObjDiscountGroupDetails = new DiscountGroupDetails();
                    ObjDiscountGroupDetails.Name = dtRow["DiscountGroup"].ToString().Trim();
                    ObjDiscountGroupDetails.Discount = Double.Parse(dtRow["Discount"].ToString().Trim());
                    ObjDiscountGroupDetails.DiscountType = PriceGroupDetails.GetDiscountType(dtRow["DiscountType"].ToString().Trim());
                    ObjDiscountGroupDetails.IsDefault = (Int32.Parse(dtRow["Default"].ToString().Trim()) == 1);
                    ObjDiscountGroupDetails.Description = dtRow["Description"].ToString().Trim();

                    ObjVendorMaster.AddDiscountGroupToCache(ObjDiscountGroupDetails);
                }
                #endregion

                #region Load Vendor Details
                for (int i = 0; i < dtVendorMaster.Rows.Count; i++)
                {
                    DataRow dtRow = dtVendorMaster.Rows[i];
                    //SlNo	VendorName	Address	TINNumber	Phone	Line	OldBalance	PriceGroup	DiscountGroup

                    VendorDetails ObjVendorDetails = new VendorDetails();
                    ObjVendorDetails.VendorName = dtRow["VendorName"].ToString().Trim();
                    ObjVendorDetails.Address = dtRow["Address"].ToString().Trim();
                    ObjVendorDetails.TINNumber = dtRow["TINNumber"].ToString().Trim();
                    ObjVendorDetails.Phone = dtRow["Phone"].ToString().Trim();
                    ObjVendorDetails.Line = dtRow["Line"].ToString().Trim();
                    ObjVendorDetails.PriceGroup = "";
                    ObjVendorDetails.DiscountGroup = "";

                    if (dtRow["PriceGroup"] != DBNull.Value && dtRow["PriceGroup"].ToString().Trim().Length > 0)
                        ObjVendorDetails.PriceGroup = dtRow["PriceGroup"].ToString().Trim();
                    if (dtRow["DiscountGroup"] != DBNull.Value && dtRow["DiscountGroup"].ToString().Trim().Length > 0)
                        ObjVendorDetails.DiscountGroup = dtRow["DiscountGroup"].ToString().Trim();

                    ObjVendorMaster.AddVendorToCache(ObjVendorDetails, ObjProductMaster.ListPriceGroups);
                }
                #endregion
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductLine.LoadVendorMaster()", ex);
            }
        }
    }
}
