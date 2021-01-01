using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;


namespace SalesOrdersReport
{
    class ProductMasterModel
    {
        List<ProductDetails> ListProducts;
        List<ProductCategoryDetails> ListProductCategories;
        List<ProductInventoryDetails> ListProductInventoryDetails;
        public List<PriceGroupDetails> ListPriceGroups;
        List<HSNCodeDetails> ListHSNCodeDetails;
        public List<TaxGroupDetails> ListTaxGroupDetails;
        Int32 DefaultPriceGroupIndex;
        MySQLHelper ObjMySQLHelper = null;
        DataTable dtProcessedProductsFromExcel = null;
        List<String> ListPriceGroupColumns;

        public void Initialize()
        {
            try
            {
                ListProducts = new List<ProductDetails>();
                ListProductCategories = new List<ProductCategoryDetails>();
                ListProductInventoryDetails = new List<ProductInventoryDetails>();
                ListPriceGroups = new List<PriceGroupDetails>();
                ListHSNCodeDetails = new List<HSNCodeDetails>();
                ListTaxGroupDetails = new List<TaxGroupDetails>();
                ListPriceGroupColumns = new List<String>();
                DefaultPriceGroupIndex = -1;

                ObjMySQLHelper = MySQLHelper.GetMySqlHelperObj();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.Initialize()", ex);
            }
        }

        void AddPriceGroupToCache(PriceGroupDetails ObjPriceGroupDetails)
        {
            try
            {
                Int32 Index = ListPriceGroups.BinarySearch(ObjPriceGroupDetails, ObjPriceGroupDetails);
                if (Index < 0)
                {
                    ListPriceGroups.Insert(~Index, ObjPriceGroupDetails);
                }

                Index = ListPriceGroupColumns.FindIndex(e => e.Equals(ObjPriceGroupDetails.PriceColumn, StringComparison.InvariantCultureIgnoreCase));
                if (Index < 0) ListPriceGroupColumns.Insert(~Index, ObjPriceGroupDetails.PriceColumn);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.AddPriceGroupToCache()", ex);
            }
        }

        void AddProductToCache(ProductDetails ObjProductDetails, DataRow dtRow)
        {
            try
            {
                ObjProductDetails.ItemName = ObjProductDetails.ItemName.Trim();
                //if (String.IsNullOrEmpty(ObjProductDetails.StockName))
                //    ObjProductDetails.StockName = ObjProductDetails.ItemName;
                //ObjProductDetails.StockName = ObjProductDetails.StockName.Trim();

                //StockProductDetails ObjStockProductDetails = new StockProductDetails();
                //ObjStockProductDetails.StockName = ObjProductDetails.StockName;
                //AddStockProduct(ObjStockProductDetails);

                ObjProductDetails.ListPrices = new Double[ListPriceGroups.Count];
                for (int j = 0; j < ObjProductDetails.ListPrices.Length; j++)
                {
                    ObjProductDetails.ListPrices[j] = Double.NaN;
                    if (!dtRow.Table.Columns.Contains(ListPriceGroups[j].Name)) continue;
                    if (dtRow[ListPriceGroups[j].Name] == DBNull.Value) continue;
                    if (String.IsNullOrEmpty(dtRow[ListPriceGroups[j].Name].ToString().Trim())) continue;
                    ObjProductDetails.ListPrices[j] = Double.Parse(dtRow[ListPriceGroups[j].Name].ToString().Trim());
                }

                ObjProductDetails.CategoryName = GetCategoryDetails(ObjProductDetails.CategoryID).CategoryName;
                ObjProductDetails.StockName = GetProductInventoryDetails(ObjProductDetails.ProductInvID).StockName;
                ObjProductDetails.HSNCode = GetTaxDetails(ObjProductDetails.TaxID).HSNCode;

                Int32 ProductIndex = ListProducts.BinarySearch(ObjProductDetails, ObjProductDetails);
                if (ProductIndex < 0)
                {
                    ListProducts.Insert(~ProductIndex, ObjProductDetails);
                    ObjProductDetails.FillMissingPricesForPriceGroups(ListPriceGroups);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.AddProductToCache()", ex);
            }
        }

        void AddProductCategoryToCache(ProductCategoryDetails ObjProductCategoryDetails)
        {
            try
            {
                ObjProductCategoryDetails.CategoryName = ObjProductCategoryDetails.CategoryName.Trim();

                Int32 CategoryIndex = ListProductCategories.BinarySearch(ObjProductCategoryDetails, ObjProductCategoryDetails);
                if (CategoryIndex < 0)
                {
                    ListProductCategories.Insert(~CategoryIndex, ObjProductCategoryDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.AddProductCategoryToCache()", ex);
            }
        }

        public void CreateNewProductCategory(String CategoryName, String Description, Boolean Active)
        {
            try
            {
                //Insert new Category to CategoryMaster
                String Query = String.Format("Insert into ProductCategoryMaster (CategoryName, Description, Active) "
                                            + "VALUES ('{0}', '{1}', {2});", CategoryName, Description, Active);
                ObjMySQLHelper.ExecuteNonQuery(Query);

                LoadProductCategoryMaster(ObjMySQLHelper.GetQueryResultInDataTable("Select * from ProductCategoryMaster Order by CategoryID"));
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.CreateNewProductCategory()", ex);
            }
        }

        public void EditProductCategory(Int32 CategoryID, String CategoryName, String Description, Boolean Active)
        {
            try
            {
                String Query = "Update ProductCategoryMaster Set ";
                Query = String.Format("{0} CategoryName = '{1}', Description = '{2}', Active = {3}", Query, CategoryName, Description, Active);
                Query = String.Format("{0} Where CategoryID = {1};", Query, CategoryID);
                ObjMySQLHelper.ExecuteNonQuery(Query);

                LoadProductCategoryMaster(ObjMySQLHelper.GetQueryResultInDataTable("Select * from ProductCategoryMaster Order by CategoryID"));
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.EditProductCategory()", ex);
            }
        }

        public void DeleteProductCategory(Int32 CategoryID)
        {
            try
            {
                String Query = String.Format("Delete from ProductCategoryMaster Where CategoryID = {0};", CategoryID);
                ObjMySQLHelper.ExecuteNonQuery(Query);

                LoadProductCategoryMaster(ObjMySQLHelper.GetQueryResultInDataTable("Select * from ProductCategoryMaster Order by CategoryID"));
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.DeleteProductCategory()", ex);
            }
        }

        void AddProductInventoryDetails(ProductInventoryDetails ObjProductInventoryDetails)
        {
            try
            {
                //Add ObjProductInventoryDetails to ListProductInventoryDetails
                Int32 ProductIndex = ListProductInventoryDetails.BinarySearch(ObjProductInventoryDetails, ObjProductInventoryDetails);
                if (ProductIndex < 0)
                {
                    ObjProductInventoryDetails.ListProductIndexes = new List<Int32>();
                    ListProductInventoryDetails.Insert(~ProductIndex, ObjProductInventoryDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.AddProductInventoryDetails()", ex);
            }
        }

        void AddHSNCode(HSNCodeDetails ObjHSNCodeDetails)
        {
            try
            {
                //Add HSNCode to ListHSNCodeDetails
                Int32 HSNCodeIndex = ListHSNCodeDetails.BinarySearch(ObjHSNCodeDetails, ObjHSNCodeDetails);
                if (HSNCodeIndex < 0)
                {
                    ListHSNCodeDetails.Insert(~HSNCodeIndex, ObjHSNCodeDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.AddHSNCode()", ex);
            }
        }

        public void LoadProductMaster(DataTable dtProductMaster)
        {
            try
            {
                for (int i = 0; i < dtProductMaster.Rows.Count; i++)
                {
                    DataRow dtRow = dtProductMaster.Rows[i];
                    //SlNo	ItemName	VendorName	PurchasePrice	SellingPrice	Wholesale	Retail  StockName   HSNCode UnitsOfMeasurement
                    ProductDetails ObjProductDetails = new ProductDetails();
                    ObjProductDetails.ProductID = Int32.Parse(dtRow["ProductID"].ToString());
                    ObjProductDetails.ProductSKU = dtRow["ProductSKU"].ToString();
                    ObjProductDetails.ItemName = dtRow["ProductName"].ToString();
                    ObjProductDetails.ProductDesc = dtRow["Description"].ToString();
                    ObjProductDetails.CategoryID = Int32.Parse(dtRow["CategoryID"].ToString());
                    ObjProductDetails.ProductInvID = Int32.Parse(dtRow["ProductInvID"].ToString());
                    ObjProductDetails.TaxID = Int32.Parse(dtRow["TaxID"].ToString());
                    ObjProductDetails.Units = Double.Parse(dtRow["Units"].ToString());
                    ObjProductDetails.PurchasePrice = Double.Parse(dtRow["PurchasePrice"].ToString());
                    ObjProductDetails.SellingPrice = Double.Parse(dtRow["SellingPrice"].ToString());
                    ObjProductDetails.UnitsOfMeasurement = dtRow["UnitOfMeasurement"].ToString();
                    ObjProductDetails.SortName = dtRow["SortName"].ToString();
                    ObjProductDetails.Active = Boolean.Parse(dtRow["Active"].ToString());
                    ObjProductDetails.AddedDate = DateTime.Parse(dtRow["AddedDate"].ToString());
                    ObjProductDetails.LastUpdateDate = DateTime.Parse(dtRow["LastUpdateDate"].ToString());
                    AddProductToCache(ObjProductDetails, dtRow);
                }

                UpdateStockProductIndexes();
                UpdateHSNProductIndexes();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.LoadProductMaster()", ex);
            }
        }

        public void LoadProductCategoryMaster(DataTable dtProductCategoryMaster)
        {
            try
            {
                ListProductCategories.Clear();

                for (int i = 0; i < dtProductCategoryMaster.Rows.Count; i++)
                {
                    DataRow dtRow = dtProductCategoryMaster.Rows[i];

                    //CategoryID, CategoryName, Description, Active
                    ProductCategoryDetails ObjProductCategoryDetails = new ProductCategoryDetails();
                    ObjProductCategoryDetails.CategoryID = Int32.Parse(dtRow["CategoryID"].ToString());
                    ObjProductCategoryDetails.CategoryName = dtRow["CategoryName"].ToString();
                    ObjProductCategoryDetails.Description = dtRow["Description"].ToString();
                    ObjProductCategoryDetails.Active = Boolean.Parse(dtRow["Active"].ToString());

                    AddProductCategoryToCache(ObjProductCategoryDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.LoadProductCategoryMaster()", ex);
            }
        }

        public void LoadPriceGroupMaster(DataTable dtPriceGroupMaster)
        {
            try
            {
                ListPriceGroups.Clear(); ListPriceGroupColumns.Clear();

                for (int i = 0; i < dtPriceGroupMaster.Rows.Count; i++)
                {
                    DataRow dtRow = dtPriceGroupMaster.Rows[i];
                    //PriceGroup	Discount	DiscountType	Description	Default

                    PriceGroupDetails ObjPriceGroupDetails = new PriceGroupDetails();
                    ObjPriceGroupDetails.PriceGroupID = Int32.Parse(dtRow["PriceGroupID"].ToString().Trim());
                    ObjPriceGroupDetails.Name = dtRow["PriceGroupName"].ToString().Trim();
                    ObjPriceGroupDetails.Description = dtRow["Description"].ToString();
                    ObjPriceGroupDetails.PriceColumn = dtRow["PriceColumn"].ToString().Trim();
                    ObjPriceGroupDetails.Discount = Double.Parse(dtRow["Discount"].ToString().Trim());
                    ObjPriceGroupDetails.DiscountType = PriceGroupDetails.GetDiscountType(dtRow["DiscountType"].ToString().Trim());
                    ObjPriceGroupDetails.IsDefault = (Int32.Parse(dtRow["Default"].ToString().Trim()) == 1);

                    AddPriceGroupToCache(ObjPriceGroupDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.LoadProductMaster()", ex);
            }
        }

        public void LoadTaxMaster(DataTable dtTaxMaster)
        {
            try
            {
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
                    for (int j = 0; j < dtTaxMaster.Columns.Count; j++)
                    {
                        if (dtTaxMaster.Columns[j].ColumnName.Equals(ListTaxGroupDetails[i].Name, StringComparison.InvariantCultureIgnoreCase))
                        {
                            TaxColIndexes[i] = j;
                            break;
                        }
                    }
                }

                for (int i = 0; i < dtTaxMaster.Rows.Count; i++)
                {
                    DataRow dr = dtTaxMaster.Rows[i];

                    if (dr["HSNCode"] == DBNull.Value || String.IsNullOrEmpty(dr["HSNCode"].ToString())) continue;

                    HSNCodeDetails ObjHSNCodeDetails = new HSNCodeDetails();
                    ObjHSNCodeDetails.TaxID = Int32.Parse(dr["TaxID"].ToString());
                    ObjHSNCodeDetails.HSNCode = dr["HSNCode"].ToString();
                    ObjHSNCodeDetails.ListTaxRates = new Double[TaxColIndexes.Length];
                    for (int j = 0; j < TaxColIndexes.Length; j++)
                    {
                        ObjHSNCodeDetails.ListTaxRates[j] = Double.Parse(dr[ListTaxGroupDetails[j].Name].ToString());
                    }

                    AddHSNCode(ObjHSNCodeDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.LoadTaxMaster()", ex);
            }
        }

        public void LoadProductInventory(DataTable dtProductInventory)
        {
            try
            {
                for (int i = 0; i < dtProductInventory.Rows.Count; i++)
                {
                    DataRow dr = dtProductInventory.Rows[i];

                    ProductInventoryDetails ObjProductInventoryDetails = new ProductInventoryDetails();
                    ObjProductInventoryDetails.ProductInvID = Int32.Parse(dr["ProductInvID"].ToString());
                    ObjProductInventoryDetails.StockName = dr["StockName"].ToString().Trim();
                    ObjProductInventoryDetails.Inventory = Double.Parse(dr["Inventory"].ToString());
                    ObjProductInventoryDetails.Units = Double.Parse(dr["Units"].ToString());
                    ObjProductInventoryDetails.UnitsOfMeasurement = dr["UnitsOfMeasurement"].ToString();
                    ObjProductInventoryDetails.ReOrderStockLevel = Double.Parse(dr["ReOrderStockLevel"].ToString());
                    ObjProductInventoryDetails.ReOrderStockQty = Double.Parse(dr["ReOrderStockQty"].ToString());
                    ObjProductInventoryDetails.LastPODate = DateTime.Parse(dr["LastPODate"].ToString());
                    ObjProductInventoryDetails.LastUpdateDate = DateTime.Parse(dr["LastUpdateDate"].ToString());

                    AddProductInventoryDetails(ObjProductInventoryDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.LoadProductInventory()", ex);
            }

        }

        HSNCodeDetails GetHSNCodeDetails(String HSNCode)
        {
            try
            {
                HSNCodeDetails ObjHSNCodeDetails = new HSNCodeDetails();
                ObjHSNCodeDetails.HSNCode = HSNCode;
                Int32 HSNCodeIndex = ListHSNCodeDetails.BinarySearch(ObjHSNCodeDetails, ObjHSNCodeDetails);

                if (HSNCodeIndex < 0) return null;

                return ListHSNCodeDetails[HSNCodeIndex];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.GetHSNCodeDetails()", ex);
            }
            return null;
        }

        public HSNCodeDetails GetTaxDetails(Int32 TaxID)
        {
            try
            {
                Int32 TaxIndex = ListHSNCodeDetails.FindIndex(e => e.TaxID == TaxID);
                if (TaxIndex < 0) return null;

                return ListHSNCodeDetails[TaxIndex];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.GetTaxDetails()", ex);
            }
            return null;
        }

        public Double[] GetTaxRatesForProduct(String ItemName)
        {
            try
            {
                ProductDetails ObjProductDetails = GetProductDetails(ItemName);
                if (ObjProductDetails == null) return null;

                return ListHSNCodeDetails[ObjProductDetails.HSNCodeIndex].ListTaxRates;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.GetTaxRatesForProduct()", ex);
            }
            return null;
        }

        void UpdateStockProductIndexes()
        {
            try
            {
                for (int i = 0; i < ListProducts.Count; i++)
                {
                    ProductInventoryDetails ObjStockProductDetails = GetStockProductDetails(ListProducts[i].StockName);
                    ObjStockProductDetails.ListProductIndexes.Add(i);
                    ListProducts[i].StockProductIndex = ListProductInventoryDetails.IndexOf(ObjStockProductDetails);
                }

                DefaultPriceGroupIndex = ListPriceGroups.FindIndex(e => e.IsDefault);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.UpdateStockProductIndexes()", ex);
            }
        }

        void UpdateHSNProductIndexes()
        {
            try
            {
                for (int i = 0; i < ListProducts.Count; i++)
                {
                    HSNCodeDetails ObjHSNCodeDetails = new HSNCodeDetails();
                    ObjHSNCodeDetails.HSNCode = ListProducts[i].HSNCode;

                    Int32 HSNCodeIndex = ListHSNCodeDetails.BinarySearch(ObjHSNCodeDetails, ObjHSNCodeDetails);
                    if (HSNCodeIndex >= 0)
                    {
                        ListProducts[i].HSNCodeIndex = HSNCodeIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.UpdateHSNProductIndexes()", ex);
            }
        }

        public List<String> GetProductCategoryList()
        {
            try
            {
                List<String> ListCategories = new List<String>();
                ListCategories.AddRange(ListProductCategories.Select(e => e.CategoryName));
                ListCategories.Sort();
                return ListCategories;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.GetProductCategoryList()", ex);
                throw ex;
            }
        }

        public List<Int32> GetProductCategoryIDList()
        {
            try
            {
                List<Int32> ListCategories = new List<Int32>();
                ListCategories.AddRange(ListProductCategories.Select(e => e.CategoryID));
                ListCategories.Sort();
                return ListCategories;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.GetProductCategoryIDList()", ex);
                throw ex;
            }
        }

        public List<ProductDetails> GetProductListForCategory(String CategoryName)
        {
            try
            {
                List<ProductDetails> ListProductsForCategory = new List<ProductDetails>();
                if (CategoryName.Equals("<ALL>", StringComparison.InvariantCultureIgnoreCase))
                    ListProductsForCategory.AddRange(ListProducts);
                else
                    ListProductsForCategory.AddRange(ListProducts.Where(e => e.CategoryName.Equals(CategoryName, StringComparison.InvariantCultureIgnoreCase)));

                ListProductsForCategory = ListProductsForCategory.OrderBy(e => e.ItemName).ToList();
                return ListProductsForCategory;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.GetProductListForCategory()", ex);
            }
            return null;
        }

        public ProductDetails GetProductDetails(String ItemName)
        {
            try
            {
                ProductDetails ObjProduct = new ProductDetails();
                ObjProduct.ItemName = ItemName.Trim();
                Int32 Index = ListProducts.BinarySearch(ObjProduct, ObjProduct);

                if (Index < 0) return null;
                return ListProducts[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.GetProductDetails()", ex);
            }
            return null;
        }

        public ProductDetails GetProductDetails(Int32 ProductID)
        {
            try
            {
                Int32 Index = ListProducts.FindIndex(e => e.ProductID == ProductID);

                if (Index < 0) return null;
                return ListProducts[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.GetProductDetails(ProductID)", ex);
            }
            return null;
        }

        public ProductInventoryDetails GetStockProductDetails(String StockName)
        {
            try
            {
                ProductInventoryDetails ObjProductInventory = new ProductInventoryDetails();
                ObjProductInventory.StockName = StockName.Trim();
                Int32 Index = ListProductInventoryDetails.BinarySearch(ObjProductInventory, ObjProductInventory);

                if (Index < 0) return null;
                return ListProductInventoryDetails[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.GetStockProductDetails()", ex);
            }
            return null;
        }

        public ProductInventoryDetails GetProductInventoryDetails(Int32 ProductInvID)
        {
            try
            {
                Int32 Index = ListProductInventoryDetails.FindIndex(e => e.ProductInvID == ProductInvID);

                if (Index < 0) return null;
                return ListProductInventoryDetails[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.GetProductInventoryDetails()", ex);
            }
            return null;
        }

        public Double GetPriceForProduct(String ItemName, Int32 PriceGroupIndex)
        {
            try
            {
                ProductDetails ObjProduct = GetProductDetails(ItemName);
                if (ObjProduct == null) return -1;

                if (PriceGroupIndex < 0) PriceGroupIndex = DefaultPriceGroupIndex;

                return ObjProduct.ListPrices[PriceGroupIndex];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.GetPriceForProduct()", ex);
            }
            return -1;
        }

        public void ComputeStockNetData(String TransactionType)
        {
            try
            {
                for (int i = 0; i < ListProductInventoryDetails.Count; i++)
                {
                    ProductInventoryDetails ObjStockProductDetails = ListProductInventoryDetails[i];
                    if (ObjStockProductDetails.IsUpdated)
                    {
                        if (ObjStockProductDetails.IsStockOverride)
                        {
                            ObjStockProductDetails.NetQty = ObjStockProductDetails.RecvdQty;
                            ObjStockProductDetails.RecvdQty -= ObjStockProductDetails.Inventory;
                        }
                        else
                        {
                            switch (TransactionType.Trim().ToUpper())
                            {
                                case "SALE": ObjStockProductDetails.RecvdQty = -1 * ObjStockProductDetails.RecvdQty; break;
                                case "PURCHASE":
                                default: break;
                            }
                            ObjStockProductDetails.NetQty = ObjStockProductDetails.Inventory + ObjStockProductDetails.RecvdQty;
                        }
                        ObjStockProductDetails.NetCost = Math.Round(ObjStockProductDetails.TotalCost - ObjStockProductDetails.TotalDiscount + ObjStockProductDetails.TotalTax, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.ComputeStockNetData()", ex);
            }
        }

        public void ResetStockProducts(Boolean Flag = false)
        {
            try
            {
                for (int i = 0; i < ListProductInventoryDetails.Count; i++)
                {
                    ProductInventoryDetails ObjStockProductDetails = ListProductInventoryDetails[i];
                    ObjStockProductDetails.IsUpdated = Flag;
                    ObjStockProductDetails.IsStockOverride = false;
                    ObjStockProductDetails.NetQty = 0;
                    ObjStockProductDetails.SaleQty = 0;
                    ObjStockProductDetails.OrderQty = 0;
                    ObjStockProductDetails.RecvdQty = 0;
                    ObjStockProductDetails.Inventory = 0;
                    ObjStockProductDetails.NetCost = 0;
                    ObjStockProductDetails.TotalCost = 0;
                    ObjStockProductDetails.TotalDiscount = 0;
                    ObjStockProductDetails.TotalTax = 0;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.ResetStockProducts()", ex);
            }
        }

        public void LoadProductPastSalesFromStockHistoryFile(DataTable dtSalePurchaseHistory, DateTime AsOnDate, Int32 PeriodValue, TimePeriodUnits PeriodUnits)
        {
            try
            {
                #region Compute Date Range
                List<DateTime> ListPODates = dtSalePurchaseHistory.AsEnumerable().Select(s => s.Field<DateTime>("PO Date")).Distinct().ToList();
                ListPODates.Sort();

                DateTime FromDate = AsOnDate, ToDate = AsOnDate;
                switch (PeriodUnits)
                {
                    case TimePeriodUnits.Days:
                        Int32 ToDateIndex = -1;
                        for (int i = ListPODates.Count - 1; i >= 0; i--)
                        {
                            if (ListPODates[i] <= AsOnDate)
                            {
                                ToDate = ListPODates[i];
                                ToDateIndex = i;
                                break;
                            }
                        }

                        if (ToDateIndex < 0) return;
                        FromDate = ListPODates[((ToDateIndex - PeriodValue + 1) >= 0) ? (ToDateIndex - PeriodValue + 1) : 0];
                        break;
                    case TimePeriodUnits.Weeks:
                        ToDate = AsOnDate;
                        FromDate = AsOnDate.AddDays(PeriodValue * -7);
                        break;
                    case TimePeriodUnits.Months:
                        ToDate = AsOnDate;
                        FromDate = AsOnDate.AddMonths(PeriodValue * -1);
                        break;
                    case TimePeriodUnits.Years:
                        ToDate = AsOnDate;
                        FromDate = AsOnDate.AddYears(PeriodValue * -1);
                        break;
                    case TimePeriodUnits.None:
                    default: break;
                }
                #endregion

                DataRow[] drSalesPurchaseHistory = dtSalePurchaseHistory.Select("", "[Stock Name] asc");

                ProductInventoryDetails ObjStockProductDetails = null;
                String PreviousStockName = "";
                for (int i = 0; i < drSalesPurchaseHistory.Length; i++)
                {
                    DataRow dr = drSalesPurchaseHistory[i];
                    if (DateTime.Parse(dr["PO Date"].ToString()) < FromDate || DateTime.Parse(dr["PO Date"].ToString()) > ToDate) continue;

                    if (!PreviousStockName.Trim().Equals(dr["Stock Name"].ToString().Trim(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        ObjStockProductDetails = GetStockProductDetails(dr["Stock Name"].ToString().Trim());
                        PreviousStockName = ObjStockProductDetails.StockName;
                        if (ObjStockProductDetails == null) continue;
                    }

                    String TransactionType = dr["Type"].ToString().Trim().ToUpper();
                    if (TransactionType.Equals("SALE"))
                    {
                        ObjStockProductDetails.SaleQty += (-1 * Double.Parse(dr["Receive Qty"].ToString()));
                    }
                    else if (TransactionType.Equals("PURCHASE"))
                    {
                        ObjStockProductDetails.RecvdQty += Double.Parse(dr["Receive Qty"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.LoadProductPastSalesFromStockHistoryFile()", ex);
                throw;
            }
        }

        public void LoadProductInventoryFile(DataRow[] drProductInventory)
        {
            try
            {
                foreach (DataRow dr in drProductInventory)
                {
                    ProductInventoryDetails ObjStockProductDetails = GetStockProductDetails(dr["StockName"].ToString().Trim());
                    if (ObjStockProductDetails == null) continue;

                    if (dr["Stock"] != DBNull.Value) ObjStockProductDetails.Inventory = Double.Parse(dr["Stock"].ToString());
                    if (dr["Units"] != DBNull.Value) ObjStockProductDetails.Units = Double.Parse(dr["Units"].ToString());
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.LoadProductInventoryFile()", ex);
                throw;
            }
        }

        public void UpdateProductInventoryDataFromPO(DataRow[] drProducts, Boolean IsStockOverride)
        {
            try
            {
                foreach (DataRow dr in drProducts)
                {
                    ProductInventoryDetails ObjStockProductDetails = GetStockProductDetails(dr["Item Name"].ToString().Trim());
                    if (ObjStockProductDetails.IsStockOverride && IsStockOverride == false)
                    {
                        continue;
                    }
                    else if (IsStockOverride)
                    {
                        ObjStockProductDetails.OrderQty = Double.Parse(dr["Order Quantity"].ToString().Trim());
                        if (dr["Received Quantity"] == DBNull.Value) continue;
                        ObjStockProductDetails.RecvdQty = (Double.Parse(dr["Received Quantity"].ToString().Trim()) * ObjStockProductDetails.Units);
                        if (dr["Total"] != DBNull.Value)
                        {
                            ObjStockProductDetails.TotalCost = Double.Parse(dr["Total"].ToString().Trim());
                            ObjStockProductDetails.TotalTax = (Double.Parse(dr["Total"].ToString().Trim()) * Double.Parse(CommonFunctions.ObjPurchaseOrderSettings.VATPercent) / 100);
                        }
                    }
                    else
                    {
                        ObjStockProductDetails.OrderQty += Double.Parse(dr["Order Quantity"].ToString().Trim());
                        if (dr["Received Quantity"] == DBNull.Value) continue;
                        ObjStockProductDetails.RecvdQty += (Double.Parse(dr["Received Quantity"].ToString().Trim()) * ObjStockProductDetails.Units);
                        if (dr["Total"] != DBNull.Value)
                        {
                            ObjStockProductDetails.TotalCost += Double.Parse(dr["Total"].ToString().Trim());
                            ObjStockProductDetails.TotalTax += (Double.Parse(dr["Total"].ToString().Trim()) * Double.Parse(CommonFunctions.ObjPurchaseOrderSettings.VATPercent) / 100);
                        }
                    }
                    ObjStockProductDetails.IsUpdated = true;
                    ObjStockProductDetails.IsStockOverride |= IsStockOverride;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.UpdateProductInventoryDataFromPO()", ex);
                throw;
            }
        }

        public void UpdateProductInventoryDataFromInvoice(DataRow[] drProducts)
        {
            try
            {
                foreach (DataRow dr in drProducts)
                {
                    ProductDetails ObjProductDetails = GetProductDetails(dr["Item Name"].ToString().Trim());
                    if (ObjProductDetails == null) continue;
                    ProductInventoryDetails ObjStockProductDetails = ListProductInventoryDetails[ObjProductDetails.StockProductIndex];
                    Double value;
                    if (!String.IsNullOrEmpty(dr["Order Quantity"].ToString().Trim())
                        && Double.TryParse(dr["Order Quantity"].ToString().Trim(), out value))
                    {
                        ObjStockProductDetails.OrderQty += (value * ObjProductDetails.Units);
                    }
                    else if (!String.IsNullOrEmpty(dr["Order Quantity"].ToString().Trim())
                        && Double.TryParse(dr["Order Quantity"].ToString().Trim().Split(new char[] { ' ', '+', '-', '*', '/', '\\' })[0], out value))
                    {
                        ObjStockProductDetails.OrderQty += (value * ObjProductDetails.Units);
                    }
                    else
                    {
                        if (dr["Sales Quantity"] != DBNull.Value)
                            ObjStockProductDetails.OrderQty += (Double.Parse(dr["Sales Quantity"].ToString().Trim()) * ObjProductDetails.Units);
                    }
                    if (dr["Sales Quantity"] == DBNull.Value) continue;
                    ObjStockProductDetails.RecvdQty += (Double.Parse(dr["Sales Quantity"].ToString().Trim()) * ObjProductDetails.Units);
                    if (dr["Total"] != DBNull.Value)
                    {
                        ObjStockProductDetails.TotalCost += Double.Parse(dr["Total"].ToString().Trim());
                        ObjStockProductDetails.TotalTax += Double.Parse(dr["TotalTax"].ToString().Trim());
                        ObjStockProductDetails.TotalDiscount += Double.Parse(dr["Discount"].ToString().Trim());
                    }
                    ObjStockProductDetails.IsUpdated = true;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.UpdateProductInventoryDataFromInvoice()", ex);
                throw;
            }
        }

        public void UpdateProductInventoryFile(Excel.Application xlApp, DateTime SummaryCreationDate, String ProductInventoryFile)
        {
            try
            {
                Excel.Workbook xlProductInventory = xlApp.Workbooks.Open(ProductInventoryFile);
                Excel.Worksheet xlInventoryWorksheet = CommonFunctions.GetWorksheet(xlProductInventory, "Inventory");
                ProductMasterModel ObjProductMaster = CommonFunctions.ObjProductMaster;

                Int32 RowCount = xlInventoryWorksheet.UsedRange.Rows.Count, ColumnCount = xlInventoryWorksheet.UsedRange.Columns.Count;
                Int32 StartRow = 1, StockNameColPos = 2, StockColPos = 4, LastPODateColPos = 5, LastUpdateDateColPos = 6;
                for (int i = 0; i < ColumnCount; i++)
                {
                    if (xlInventoryWorksheet.Cells[1, 1 + i].Value == null) break;
                    String ColName = xlInventoryWorksheet.Cells[1, 1 + i].Value.ToString().Trim().ToUpper();
                    switch (ColName)
                    {
                        case "STOCKNAME": StockNameColPos = i + 1; break;
                        case "STOCK": StockColPos = i + 1; break;
                        case "LASTUPDATEDATE": LastUpdateDateColPos = i + 1; break;
                        case "LASTPODATE": LastPODateColPos = i + 1; break;
                        default: break;
                    }
                }

                for (int i = 1; i < RowCount; i++)
                {
                    String StockName = xlInventoryWorksheet.Cells[StartRow + i, StockNameColPos].Value.ToString().Trim();
                    ProductInventoryDetails ObjStockProductDetails = ObjProductMaster.GetStockProductDetails(StockName);
                    if (ObjStockProductDetails == null || !ObjStockProductDetails.IsUpdated) continue;
                    xlInventoryWorksheet.Cells[StartRow + i, StockColPos].Value = ObjStockProductDetails.NetQty;
                    xlInventoryWorksheet.Cells[StartRow + i, LastPODateColPos].Value = SummaryCreationDate.ToString("dd-MMM-yyyy");
                    xlInventoryWorksheet.Cells[StartRow + i, LastUpdateDateColPos].Value = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                }

                xlProductInventory.Save();
                xlProductInventory.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.UpdateProductInventoryFile()", ex);
                throw;
            }
        }

        public void UpdateProductStockHistoryFile(Excel.Application xlApp, DateTime SummaryCreationDate, String TransactionType, String ProductStockHistoryFile)
        {
            try
            {
                Excel.Workbook xlProductStockHistory;
                Excel.Worksheet xlStockHistoryWorksheet;

                String[] Header = new String[] { "PO Date", "Update Date", "Type", "Stock Name", "Order Qty", "Receive Qty", "Net Qty", "Total Cost", "Total Discount", "Total Tax", "Net Cost" };
                if (!File.Exists(ProductStockHistoryFile))
                {
                    xlProductStockHistory = xlApp.Workbooks.Add();
                    xlStockHistoryWorksheet = xlProductStockHistory.Worksheets.Add();
                    xlStockHistoryWorksheet.Name = "Stock History";
                    for (int i = 0; i < Header.Length; i++)
                    {
                        xlStockHistoryWorksheet.Cells[1, i + 1].Value = Header[i];
                    }

                    Excel.Range xlRange1 = xlStockHistoryWorksheet.Range[xlStockHistoryWorksheet.Cells[1, 1], xlStockHistoryWorksheet.Cells[1, Header.Length]];
                    xlRange1.Font.Bold = true;
                    SellerInvoiceForm.SetAllBorders(xlRange1);
                    xlProductStockHistory.SaveAs(ProductStockHistoryFile);

                    Excel.Worksheet xlSheet = CommonFunctions.GetWorksheet(xlProductStockHistory, "Sheet1");
                    if (xlSheet != null) xlSheet.Delete();
                    xlSheet = CommonFunctions.GetWorksheet(xlProductStockHistory, "Sheet2");
                    if (xlSheet != null) xlSheet.Delete();
                    xlSheet = CommonFunctions.GetWorksheet(xlProductStockHistory, "Sheet3");
                    if (xlSheet != null) xlSheet.Delete();
                }
                else
                {
                    xlProductStockHistory = xlApp.Workbooks.Open(ProductStockHistoryFile);
                    xlStockHistoryWorksheet = CommonFunctions.GetWorksheet(xlProductStockHistory, "Stock History");
                }

                ProductMasterModel ObjProductMaster = CommonFunctions.ObjProductMaster;

                Int32 RowCount = xlStockHistoryWorksheet.UsedRange.Rows.Count, ColumnCount = xlStockHistoryWorksheet.UsedRange.Columns.Count;
                Int32 StartRow = RowCount + 1, PODateColPos = 1, UpdateDateColPos = 2, TypeColPos = 3, StockNameColPos = 4,
                    OrderQtyColPos = 5, ReceiveQtyColPos = 6, NetQtyColPos = 7, TotalCostColPos = 8, TotalDiscountColPos = 9,
                    TotalTaxColPos = 10, NetCostColPos = 11;
                //PO Date\tUpdate Date\tType\tStock Name\tOrder Qty\tReceive Qty\tNet Qty\tTotal Cost\tTotal Discount\tTotal Tax\tNet Cost

                for (int i = 0; i < ColumnCount; i++)
                {
                    if (xlStockHistoryWorksheet.Cells[1, 1 + i].Value == null) break;
                    String ColName = xlStockHistoryWorksheet.Cells[1, 1 + i].Value.ToString().Trim().ToUpper();
                    switch (ColName)
                    {
                        case "PO DATE": PODateColPos = i + 1; break;
                        case "UPDATE DATE": UpdateDateColPos = i + 1; break;
                        case "TYPE": TypeColPos = i + 1; break;
                        case "STOCK NAME": StockNameColPos = i + 1; break;
                        case "ORDER QTY": OrderQtyColPos = i + 1; break;
                        case "RECEIVE QTY": ReceiveQtyColPos = i + 1; break;
                        case "NET QTY": NetQtyColPos = i + 1; break;
                        case "TOTAL COST": TotalCostColPos = i + 1; break;
                        case "TOTAL DISCOUNT": TotalDiscountColPos = i + 1; break;
                        case "TOTAL TAX": TotalTaxColPos = i + 1; break;
                        case "NET COST": NetCostColPos = i + 1; break;
                        default: break;
                    }
                }

                Int32 StockCounter = 0;
                for (int i = 0, j = 0; i < ObjProductMaster.ListProductInventoryDetails.Count; i++)
                {
                    ProductInventoryDetails ObjStockProduct = ObjProductMaster.ListProductInventoryDetails[i];
                    if (!ObjStockProduct.IsUpdated) continue;

                    xlStockHistoryWorksheet.Cells[StartRow + j, PODateColPos].Value = SummaryCreationDate.ToString("dd-MMM-yyyy");
                    xlStockHistoryWorksheet.Cells[StartRow + j, UpdateDateColPos].Value = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                    xlStockHistoryWorksheet.Cells[StartRow + j, TypeColPos].Value = TransactionType;
                    xlStockHistoryWorksheet.Cells[StartRow + j, StockNameColPos].Value = ObjStockProduct.StockName;
                    xlStockHistoryWorksheet.Cells[StartRow + j, OrderQtyColPos].Value = ObjStockProduct.OrderQty;
                    xlStockHistoryWorksheet.Cells[StartRow + j, ReceiveQtyColPos].Value = ObjStockProduct.RecvdQty;
                    xlStockHistoryWorksheet.Cells[StartRow + j, NetQtyColPos].Value = ObjStockProduct.NetQty;
                    xlStockHistoryWorksheet.Cells[StartRow + j, TotalCostColPos].Value = ObjStockProduct.TotalCost;
                    xlStockHistoryWorksheet.Cells[StartRow + j, TotalDiscountColPos].Value = ObjStockProduct.TotalDiscount;
                    xlStockHistoryWorksheet.Cells[StartRow + j, TotalTaxColPos].Value = ObjStockProduct.TotalTax;
                    xlStockHistoryWorksheet.Cells[StartRow + j, NetCostColPos].Value = ObjStockProduct.NetCost;
                    StockCounter = j;
                    j++;
                }

                Excel.Range xlRange = xlStockHistoryWorksheet.Range[xlStockHistoryWorksheet.Cells[StartRow, PODateColPos], xlStockHistoryWorksheet.Cells[StartRow + StockCounter, NetCostColPos]];
                SellerInvoiceForm.SetAllBorders(xlRange);

                xlProductStockHistory.Save();
                xlProductStockHistory.Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.UpdateProductStockHistoryFile()", ex);
                throw;
            }
        }

        public ProductCategoryDetails GetCategoryDetails(Int32 CategoryID)
        {
            try
            {
                Int32 Index = ListProductCategories.FindIndex(e => e.CategoryID == CategoryID);

                if (Index < 0) return null;
                else return ListProductCategories[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.GetCategoryDetails()", ex);
                throw;
            }
        }

        public ProductCategoryDetails GetCategoryDetails(String CategoryName)
        {
            try
            {
                Int32 Index = ListProductCategories.FindIndex(e => e.CategoryName.Equals(CategoryName.Trim(), StringComparison.InvariantCultureIgnoreCase));

                if (Index < 0) return null;
                else return ListProductCategories[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.GetCategoryDetails(CategoryName)", ex);
                throw;
            }
        }

        public void ImportProductsFromExcel(String ExcelFilePath, out Int32 NumValidProducts, out Int32 NumInvalidProducts, out Int32 NumExistingProducts)
        {
            try
            {
                NumValidProducts = 0;
                NumInvalidProducts = 0;
                NumExistingProducts = 0;

                DataTable dtTmpProductMaster = CommonFunctions.ReturnDataTableFromExcelWorksheet("ProductMaster", ExcelFilePath, "*");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.ImportProductsFromExcel()", ex);
                throw ex;
            }
        }

        public Int32 ImportProcessedProducts()
        {
            try
            {
                if (dtProcessedProductsFromExcel == null || dtProcessedProductsFromExcel.Rows.Count == 0) return 0;

                LoadProductMaster(dtProcessedProductsFromExcel);

                Int32 RowCount = dtProcessedProductsFromExcel.Rows.Count;
                dtProcessedProductsFromExcel.Clear();
                dtProcessedProductsFromExcel = null;

                return RowCount;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.ImportProcessedProducts()", ex);
                throw ex;
            }
        }

        public void ExportProductsToExcel(String ExcelFilePath)
        {
            try
            {

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.ExportProductsToExcel()", ex);
                throw ex;
            }
        }

        public List<String> GetHSNCodeList()
        {
            try
            {
                return ListHSNCodeDetails.Select(e => e.HSNCode).ToList();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.GetHSNCodeList()", ex);
                throw;
            }
        }

        public DataTable GetPriceColumnPricesForProduct(Int32 ProductID)
        {
            try
            {
                DataTable dtPrices = new DataTable();
                dtPrices.Columns.Add("PriceColumn", Type.GetType("System.String"));
                dtPrices.Columns.Add("Price", Type.GetType("System.Double"));

                if (ProductID > 0)
                {
                    ProductDetails tmpProductDetails = GetProductDetails(ProductID);

                    for (Int32 i = 0; i < ListPriceGroupColumns.Count; i++)
                    {
                        Int32 Index = ListPriceGroups.FindIndex(e => e.PriceColumn.Equals(ListPriceGroupColumns[i], StringComparison.InvariantCultureIgnoreCase));
                        if (Index < 0) continue;

                        DataRow dtRow = dtPrices.NewRow();
                        dtRow[0] = ListPriceGroupColumns[i];
                        dtRow[1] = tmpProductDetails.ListPrices[Index];

                        dtPrices.Rows.Add(dtRow);
                    }
                }
                else
                {
                    for (Int32 i = 0; i < ListPriceGroupColumns.Count; i++)
                    {
                        DataRow dtRow = dtPrices.NewRow();
                        dtRow[0] = ListPriceGroupColumns[i];
                        dtRow[1] = 0;

                        dtPrices.Rows.Add(dtRow);
                    }
                }

                return dtPrices;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.GetPriceColumnPricesForProduct()", ex);
                throw;
            }
        }
    }
}
