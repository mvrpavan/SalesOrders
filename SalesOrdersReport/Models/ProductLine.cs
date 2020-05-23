using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;

namespace SalesOrdersReport
{
    class ProductLine
    {
        public String Name;
        public Settings ObjSettings;
        public XmlNode ProductLineNode;
        public ProductMaster ObjProductMaster;
        public SellerMaster ObjSellerMaster;
        public VendorMaster ObjVendorMaster;

        public Boolean LoadDetailsFromNode(XmlNode ProductLineNode)
        {
            try
            {
                this.ProductLineNode = ProductLineNode;
                XMLFileUtils.GetAttributeValue(ProductLineNode, "Name", out Name);

                ObjSettings = new Settings();
                XmlNode GeneralNode;
                XMLFileUtils.GetChildNode(ProductLineNode, "General", out GeneralNode);
                ObjSettings.LoadSettingsFromNode(GeneralNode);

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
                CommonFunctions.ShowErrorDialog("ProductLine.LoadDetailsFromNode()", ex);
                return false;
            }
        }

        public void LoadProductMaster(DataTable dtProductMaster, DataTable dtPriceGroupMaster, DataTable dtHSNMaster)
        {
            try
            {
                ObjProductMaster = new ProductMaster();
                ObjProductMaster.Initialize();

                for (int i = 0; i < dtPriceGroupMaster.Rows.Count; i++)
                {
                    DataRow dtRow = dtPriceGroupMaster.Rows[i];
                    //PriceGroup	Discount	DiscountType	Description	Default

                    PriceGroupDetails ObjPriceGroupDetails = new PriceGroupDetails();
                    ObjPriceGroupDetails.Name = dtRow["PriceGroup"].ToString().Trim();
                    ObjPriceGroupDetails.Discount = Double.Parse(dtRow["Discount"].ToString().Trim());
                    ObjPriceGroupDetails.DiscountType = PriceGroupDetails.GetDiscountType(dtRow["DiscountType"].ToString().Trim());
                    ObjPriceGroupDetails.IsDefault = (Int32.Parse(dtRow["Default"].ToString().Trim()) == 1);
                    ObjPriceGroupDetails.Description = dtRow["Description"].ToString();

                    ObjProductMaster.AddPriceGroupToCache(ObjPriceGroupDetails);
                }

                LoadHSNMasterData(dtHSNMaster);

                for (int i = 0; i < dtProductMaster.Rows.Count; i++)
                {
                    DataRow dtRow = dtProductMaster.Rows[i];
                    //SlNo	ItemName	VendorName	PurchasePrice	SellingPrice	Wholesale	Retail  StockName   HSNCode UnitsOfMeasurement
                    ProductDetails ObjProductDetails = new ProductDetails();
                    ObjProductDetails.ItemID = Int32.Parse(dtRow["SlNo"].ToString());
                    ObjProductDetails.ItemName = dtRow["ItemName"].ToString();
                    ObjProductDetails.CategoryName = dtRow["Category"].ToString();
                    ObjProductDetails.StockName = dtRow["StockName"].ToString();
                    ObjProductDetails.VendorName = dtRow["VendorName"].ToString();
                    ObjProductDetails.Units = Double.Parse(dtRow["Units"].ToString());
                    ObjProductDetails.PurchasePrice = Double.Parse(dtRow["PurchasePrice"].ToString());
                    ObjProductDetails.SellingPrice = Double.Parse(dtRow["SellingPrice"].ToString());
                    ObjProductDetails.HSNCode = dtRow["HSNCode"].ToString();
                    ObjProductDetails.UnitsOfMeasurement = dtRow["UnitOfMeasurement"].ToString();
                    ObjProductDetails.ListPrices = new Double[ObjProductMaster.ListPriceGroups.Count];
                    for (int j = 0; j < ObjProductDetails.ListPrices.Length; j++)
                    {
                        ObjProductDetails.ListPrices[j] = Double.NaN;
                        if (!dtRow.Table.Columns.Contains(ObjProductMaster.ListPriceGroups[j].Name)) continue;
                        if (dtRow[ObjProductMaster.ListPriceGroups[j].Name] == DBNull.Value) continue;
                        if (String.IsNullOrEmpty(dtRow[ObjProductMaster.ListPriceGroups[j].Name].ToString().Trim())) continue;
                        ObjProductDetails.ListPrices[j] = Double.Parse(dtRow[ObjProductMaster.ListPriceGroups[j].Name].ToString().Trim());
                    }

                    ObjProductMaster.AddProductToCache(ObjProductDetails);
                }

                ObjProductMaster.UpdateStockProductIndexes();
                ObjProductMaster.UpdateHSNProductIndexes();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductLine.LoadProductMaster()", ex);
            }
        }

        public void LoadSellerMaster(DataTable dtSellerMaster, DataTable dtDiscountGroupMaster)
        {
            try
            {
                ObjSellerMaster = new SellerMaster();
                ObjSellerMaster.Initialize();

                #region Load Line from Seller Master
                CommonFunctions.ListSellerLines = new List<String>();
                Boolean ContainsBlanks = false;
                for (int i = 0; i < dtSellerMaster.Rows.Count; i++)
                {
                    DataRow dtRow = dtSellerMaster.Rows[i];
                    String Line = dtRow["Line"].ToString().Replace("<", "").Replace(">", "").ToUpper();
                    if (Line.Trim().Length == 0) ContainsBlanks = true;
                    else if (!CommonFunctions.ListSellerLines.Contains(Line)) CommonFunctions.ListSellerLines.Add(Line);
                }

                CommonFunctions.ListSellerLines.Sort();
                CommonFunctions.ListSellerLines.Insert(0, "<All>");
                if (ContainsBlanks) CommonFunctions.ListSellerLines.Add("<Blanks>");
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

                    ObjSellerMaster.AddDiscountGroupToCache(ObjDiscountGroupDetails);
                }
                #endregion

                #region Load Sellers Details
                for (int i = 0; i < dtSellerMaster.Rows.Count; i++)
                {
                    DataRow dtRow = dtSellerMaster.Rows[i];
                    //SlNo	SellerName	Address	TINNumber	Phone	Line	OldBalance	PriceGroup	DiscountGroup

                    SellerDetails ObjSellerDetails = new SellerDetails();
                    ObjSellerDetails.Name = dtRow["SellerName"].ToString().Trim();
                    ObjSellerDetails.Address = dtRow["Address"].ToString().Trim();
                    ObjSellerDetails.TINNumber = dtRow["TINNumber"].ToString().Trim();
                    ObjSellerDetails.Phone = dtRow["Phone"].ToString().Trim();
                    ObjSellerDetails.Line = dtRow["Line"].ToString().Trim();
                    ObjSellerDetails.OldBalance = 0;
                    ObjSellerDetails.PriceGroup = "";
                    ObjSellerDetails.DiscountGroup = "";
                    ObjSellerDetails.State = "";
                    ObjSellerDetails.StateCode = "";
                    ObjSellerDetails.GSTIN = "";

                    if (dtRow["OldBalance"] != DBNull.Value && dtRow["OldBalance"].ToString().Trim().Length > 0)
                        ObjSellerDetails.OldBalance = Double.Parse(dtRow["OldBalance"].ToString().ToString());
                    if (dtRow["PriceGroup"] != DBNull.Value && dtRow["PriceGroup"].ToString().Trim().Length > 0)
                        ObjSellerDetails.PriceGroup = dtRow["PriceGroup"].ToString().Trim();
                    if (dtRow["DiscountGroup"] != DBNull.Value && dtRow["DiscountGroup"].ToString().Trim().Length > 0)
                        ObjSellerDetails.DiscountGroup = dtRow["DiscountGroup"].ToString().Trim();
                    if (dtRow["State"] != DBNull.Value && dtRow["State"].ToString().Trim().Length > 0)
                        ObjSellerDetails.State = dtRow["State"].ToString().Trim();
                    if (dtRow["StateCode"] != DBNull.Value && dtRow["StateCode"].ToString().Trim().Length > 0)
                        ObjSellerDetails.StateCode = dtRow["StateCode"].ToString().Trim();
                    if (dtRow["GSTIN"] != DBNull.Value && dtRow["GSTIN"].ToString().Trim().Length > 0)
                        ObjSellerDetails.GSTIN = dtRow["GSTIN"].ToString().Trim();

                    ObjSellerMaster.AddSellerToCache(ObjSellerDetails, ObjProductMaster.ListPriceGroups);
                }
                #endregion
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductLine.LoadSellerMaster()", ex);
            }
        }

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

        void LoadHSNMasterData(DataTable dtHSNMaster)
        {
            try
            {
                List<TaxGroupDetails> ListTaxGroupDetails = ObjProductMaster.ListTaxGroupDetails;
                ListTaxGroupDetails.Clear();

                String[] ArrTaxName = new String[] { "CGST", "SGST", "IGST" };
                String[] ArrTaxDesc = new String[] { "Central Goods and Service Tax", "State Goods and Service Tax", "Inter Goods and Service Tax" };
                for (int i = 0; i < ArrTaxName.Length; i++)
                {
                    TaxGroupDetails ObjTaxGroupDetails = new TaxGroupDetails();
                    ObjTaxGroupDetails.Name = ArrTaxName[i]; ObjTaxGroupDetails.Description = ArrTaxDesc[i];
                    ObjTaxGroupDetails.TaxRate = 0;
                    ListTaxGroupDetails.Add(ObjTaxGroupDetails);
                }

                Int32[] TaxColIndexes = new Int32[ListTaxGroupDetails.Count];
                for (int i = 0; i < TaxColIndexes.Length; i++)
                {
                    for (int j = 0; j < dtHSNMaster.Columns.Count; j++)
                    {
                        if (dtHSNMaster.Columns[j].ColumnName.Equals(ListTaxGroupDetails[i].Name, StringComparison.InvariantCultureIgnoreCase))
                        {
                            TaxColIndexes[i] = j;
                            break;
                        }
                    }
                }

                for (int i = 0; i < dtHSNMaster.Rows.Count; i++)
                {
                    DataRow dr = dtHSNMaster.Rows[i];

                    if (dr["HSNCode"] == DBNull.Value || String.IsNullOrEmpty(dr["HSNCode"].ToString())) continue;

                    HSNCodeDetails ObjHSNCodeDetails = new HSNCodeDetails();
                    ObjHSNCodeDetails.HSNCode = dr["HSNCode"].ToString();
                    ObjHSNCodeDetails.ListTaxRates = new Double[TaxColIndexes.Length];
                    for (int j = 0; j < TaxColIndexes.Length; j++)
                    {
                        ObjHSNCodeDetails.ListTaxRates[j] = Double.Parse(dr[ListTaxGroupDetails[j].Name].ToString());
                    }

                    ObjProductMaster.AddHSNCode(ObjHSNCodeDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductLine.LoadHSNMasterData()", ex);
            }
        }
    }
}
