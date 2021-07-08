using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using SalesOrdersReport.CommonModules;
using SalesOrdersReport.Views;
using System.Windows.Forms;

namespace SalesOrdersReport.Models
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
        List<String> ListPriceGroupColumns;
        const String SKUPrefix = "SKU";
        string ProdInvColumnsQueryStr = "ProductInvID, StockName, Inventory, Units, UnitsOfMeasurement, ReOrderStockLevel, ReOrderStockQty, LastPODate, LastUpdateDate, CASE WHEN Active = 1 THEN 'true' ELSE 'false' END as Active";
        List<BarcodeDetails> ListBarcodeDetails;

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
                ListBarcodeDetails = new List<BarcodeDetails>();
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
                if (Index < 0) ListPriceGroups.Insert(~Index, ObjPriceGroupDetails);

                Index = ListPriceGroupColumns.FindIndex(e => e.Equals(ObjPriceGroupDetails.PriceColumn, StringComparison.InvariantCultureIgnoreCase));
                if (Index < 0) ListPriceGroupColumns.Insert(~Index, ObjPriceGroupDetails.PriceColumn);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.AddPriceGroupToCache()", ex);
            }
        }

        public List<HSNCodeDetails> GetAllHSNCodeDetails()
        {
            try
            {
                return ListHSNCodeDetails;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetAllHSNCodeDetails()", ex);
                return null;
            }
        }

        void AddProductToCache(ProductDetails ObjProductDetails)
        {
            try
            {
                ObjProductDetails.ItemName = ObjProductDetails.ItemName.Trim();
                ObjProductDetails.CategoryName = GetCategoryDetails(ObjProductDetails.CategoryID).CategoryName;
                ObjProductDetails.StockName = GetProductInventoryDetails(ObjProductDetails.ProductInvID).StockName;
                ObjProductDetails.HSNCode = GetTaxDetails(ObjProductDetails.TaxID).HSNCode;
                VendorDetails tmpVendorDetails = CommonFunctions.ObjVendorMaster.GetVendorDetails(ObjProductDetails.VendorID);
                //ObjProductDetails.VendorName = (tmpVendorDetails?.VendorName);
                ObjProductDetails.VendorName = (tmpVendorDetails == null) ? "" : tmpVendorDetails.VendorName;
                Int32 ProductIndex = ListProducts.BinarySearch(ObjProductDetails, ObjProductDetails);
                if (ProductIndex < 0)
                {
                    ListProducts.Insert(~ProductIndex, ObjProductDetails);
                    AddToBarcodeDetails(ObjProductDetails.ProductID);
                    //ObjProductDetails.FillMissingPricesForPriceGroups(ListPriceGroups);
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

        public ProductCategoryDetails CreateNewProductCategory(String CategoryName, String Description, Boolean Active)
        {
            try
            {
                if (GetCategoryDetails(CategoryName) != null) return null;

                //Insert new Category to CategoryMaster
                String Query = String.Format("Insert into ProductCategoryMaster (CategoryName, Description, Active) "
                                            + "VALUES ('{0}', '{1}', {2});", CategoryName, Description, (Active ? "1" : "0"));
                ObjMySQLHelper.ExecuteNonQuery(Query);

                LoadProductCategoryMaster(ObjMySQLHelper.GetQueryResultInDataTable("Select * from ProductCategoryMaster Order by CategoryID"));

                return GetCategoryDetails(CategoryName);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreateNewProductCategory()", ex);
                return null;
            }
        }

        public void EditProductCategory(Int32 CategoryID, String CategoryName, String Description, Boolean Active)
        {
            try
            {
                String Query = "Update ProductCategoryMaster Set ";
                Query = String.Format("{0} CategoryName = '{1}', Description = '{2}', Active = {3}", Query, CategoryName, Description, (Active ? "1" : "0"));
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
                ObjMySQLHelper.UpdateTableDetails("ProductCategoryMaster", new List<string>() { "Active" }, new List<string>() { "0" },
                                                new List<Types>() { Types.Number }, $"CategoryID = {CategoryID}");

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
                ListProducts.Clear();

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
                    ObjProductDetails.VendorID = Int32.Parse(dtRow["VendorID"].ToString());
                    ObjProductDetails.TaxID = Int32.Parse(dtRow["TaxID"].ToString());
                    ObjProductDetails.Units = Double.Parse(dtRow["Units"].ToString());
                    ObjProductDetails.PurchasePrice = Double.Parse(dtRow["PurchasePrice"].ToString());
                    ObjProductDetails.WholesalePrice = Double.Parse(dtRow["WholesalePrice"].ToString());
                    ObjProductDetails.RetailPrice = Double.Parse(dtRow["RetailPrice"].ToString());
                    ObjProductDetails.MaxRetailPrice = Double.Parse(dtRow["MaxRetailPrice"].ToString());
                    ObjProductDetails.UnitsOfMeasurement = dtRow["UnitsOfMeasurement"].ToString();
                    ObjProductDetails.SortName = dtRow["SortName"].ToString();
                    ObjProductDetails.Active = (Int32.Parse(dtRow["Active"].ToString()) == 1);
                    ObjProductDetails.AddedDate = DateTime.Parse(dtRow["AddedDate"].ToString());
                    ObjProductDetails.LastUpdateDate = DateTime.Parse(dtRow["LastUpdateDate"].ToString());
                    ObjProductDetails.ArrBarcodes = ((DBNull.Value == dtRow["Barcode"]) ? new String[0] : dtRow["Barcode"].ToString().Split('|'));
                    AddProductToCache(ObjProductDetails);
                }

                UpdateStockProductIndexes();
                UpdateHSNProductIndexes();
                //UpdateBarcodeIndexes();
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
                    ObjProductCategoryDetails.Active = (Int32.Parse(dtRow["Active"].ToString()) == 1);

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
                    ObjPriceGroupDetails.PriceGrpName = dtRow["PriceGroupName"].ToString().Trim();
                    ObjPriceGroupDetails.Description = dtRow["Description"].ToString();
                    ObjPriceGroupDetails.PriceColumn = dtRow["PriceColumn"].ToString().Trim();
                    ObjPriceGroupDetails.Discount = Double.Parse(dtRow["Discount"].ToString().Trim());
                    ObjPriceGroupDetails.DiscountType = PriceGroupDetails.GetDiscountType(dtRow["DiscountType"].ToString().Trim());
                    ObjPriceGroupDetails.IsDefault = (Int32.Parse(dtRow["IsDefault"].ToString().Trim()) == 1);

                    AddPriceGroupToCache(ObjPriceGroupDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadPriceGroupMaster()", ex);
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

                ListHSNCodeDetails.Clear();
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
        public DataTable LoadNGetProdInvDataTable(string ProductCategoryFilter="")
        {
            try
            {
                string Query = "";
                if (ProductCategoryFilter.Contains("like")) Query = "Select "+ ProdInvColumnsQueryStr + " from ProductInventory where ProductInvID in ( Select a.ProductInvID from ProductMaster a Left join ProductCategoryMaster b on a.CategoryId = b.CategoryId where b.CategoryName " + ProductCategoryFilter + " ) Order by StockName;";
                else if (ProductCategoryFilter != "" && ProductCategoryFilter != "ALL") Query = "Select  " + ProdInvColumnsQueryStr + " from ProductInventory where ProductInvID in ( Select a.ProductInvID from ProductMaster a Left join ProductCategoryMaster b on a.CategoryID = b.CategoryID where b.CategoryName = '" + ProductCategoryFilter + "' ) Order by StockName;";
                else Query = "Select  " + ProdInvColumnsQueryStr + " from ProductInventory Order by StockName;";
                DataTable dtProductInventory = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                LoadProductInventory(dtProductInventory);
                return dtProductInventory;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.LoadNGetProdInvDataTable()", ex);
                throw ex;
            }
        }

        public string GetProductCategoryQueryStr()
        {
            try
            {
                return "";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.GetProductCategoryQueryStr()", ex);
                return "";
            }
        }
        public void LoadProductInventory(DataTable dtProductInventory)
        {
            try
            {
                ListProductInventoryDetails.Clear();

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
                    ObjProductInventoryDetails.LastPODate = ((dr["LastPODate"] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(dr["LastPODate"].ToString()));
                    ObjProductInventoryDetails.LastUpdateDate = ((dr["LastUpdateDate"] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(dr["LastUpdateDate"].ToString()));
                    ObjProductInventoryDetails.Active = (dr["Active"].ToString() == "1" || dr["Active"].ToString() == "true") ? true : false;
                    AddProductInventoryDetails(ObjProductInventoryDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.LoadProductInventory()", ex);
            }
        }

        public HSNCodeDetails GetHSNCodeDetails(String HSNCode)
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

        //void UpdateBarcodeIndexes()
        //{
        //    try
        //    {
        //        ListBarcodeDetails.Clear();

        //        for (int i = 0; i < ListProducts.Count; i++)
        //        {
        //            foreach (var Barcode in ListProducts[i].ArrBarcodes)
        //            {
        //                AddToBarcodeDetails(Barcode, ListProducts[i].ProductID);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonFunctions.ShowErrorDialog($"{this}.UpdateBarcodeIndexes()", ex);
        //    }
        //}

        void AddToBarcodeDetails(Int32 ProductID)
        {
            try
            {
                ProductDetails tmpProductDetails = GetProductDetails(ProductID);
                if (tmpProductDetails.ArrBarcodes == null) return;
                foreach (var Barcode in tmpProductDetails.ArrBarcodes)
                {
                    BarcodeDetails ObjBarcodeDetails = new BarcodeDetails();
                    ObjBarcodeDetails.Barcode = Barcode;
                    Int32 BarcodeIndex = ListBarcodeDetails.BinarySearch(ObjBarcodeDetails, ObjBarcodeDetails);
                    if (BarcodeIndex < 0)
                    {
                        ListBarcodeDetails.Insert(~BarcodeIndex, ObjBarcodeDetails);
                        ObjBarcodeDetails.ListProductIDs = new List<Int32>();
                    }
                    else
                    {
                        ObjBarcodeDetails = ListBarcodeDetails[BarcodeIndex];
                    }
                    if (!ObjBarcodeDetails.ListProductIDs.Contains(ProductID)) ObjBarcodeDetails.ListProductIDs.Add(ProductID);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.AddToBarcodeDetails()", ex);
            }
        }

        public List<Int32> GetProductIDListForBarcode(String Barcode)
        {
            try
            {
                BarcodeDetails ObjBarcodeDetails = new BarcodeDetails();
                ObjBarcodeDetails.Barcode = Barcode;

                Int32 Index = ListBarcodeDetails.BinarySearch(ObjBarcodeDetails, ObjBarcodeDetails);
                if (Index < 0) return null;
                //List<Int32> ListProductIDs = new List<Int32>();
                //foreach (var index in ListBarcodeDetails[Index].ListProductIDs)
                //{
                //    ListProductIDs.Add(ListProducts[index].ProductID);
                //}

                return ListBarcodeDetails[Index].ListProductIDs;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetProductIDListForBarcode()", ex);
                return null;
            }
        }

        public List<String> GetAllBarcodes()
        {
            try
            {
                return ListBarcodeDetails.Select(e => e.Barcode).ToList();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetAllBarcodes()", ex);
                return null;
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
                if (CategoryName.Equals(Views.ProductsMainForm.AllKeyword, StringComparison.InvariantCultureIgnoreCase))
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
        public ProductInventoryDetails GetProductInventoryDetails(string StockName)
        {
            try
            {
                ProductInventoryDetails ObjProductInventoryDetails = new ProductInventoryDetails();
                ObjProductInventoryDetails.StockName=StockName;
                Int32 Index = ListProductInventoryDetails.BinarySearch(ObjProductInventoryDetails, ObjProductInventoryDetails);

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

                //return ObjProduct.ListPrices[PriceGroupIndex];

                switch (PriceGroupIndex)
                {
                    case 0: return ObjProduct.RetailPrice;
                    case 1: return ObjProduct.WholesalePrice;
                    case 2: return ObjProduct.MaxRetailPrice;
                    case 3: return ObjProduct.PurchasePrice;
                    default: return -1;
                }
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

        public bool CanStockBeDeleted(int ProductInventoryID)
        {
            try
            {
                string Query = $"Select * from ProductMaster where ProductInvID = "+ ProductInventoryID + ";";
                DataTable tempDt = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                if (tempDt != null && tempDt.Rows.Count > 0) return false;
                   
                return true;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.ResetStockProducts()", ex);
                return false;
            }
        }
        public Int32 AddProductInvDetailstoDB(ProductInventoryDetails tmpProductInventoryDetails)
        {
            try
            {
                String Query = $"Select ProductInvID from ProductInventory Where StockName = '{tmpProductInventoryDetails.StockName}'";
                DataTable dtInvDetails = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                if (dtInvDetails.Rows.Count == 0)
                {
                    Query = "Insert into ProductInventory(StockName, Inventory, Units, UnitsOfMeasurement, ReOrderStockLevel, ReOrderStockQty)";
                    Query += $" Values ('{tmpProductInventoryDetails.StockName}', {tmpProductInventoryDetails.Inventory}," +
                             $"{tmpProductInventoryDetails.Units}, '{tmpProductInventoryDetails.UnitsOfMeasurement}'," +
                             $"{tmpProductInventoryDetails.ReOrderStockLevel}, {tmpProductInventoryDetails.ReOrderStockQty},{tmpProductInventoryDetails.LastPODate})";
                    ObjMySQLHelper.ExecuteNonQuery(Query);
                }
                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMasterModel.AddProductInvDetailstoDB()", ex);
                return -1;
            }
        }
        public Int32 UpdateProductInventoryDatatoDB(ProductInventoryDetails ObjProductInventoryDetails)
        {
            try
            {
                ProductInventoryDetails ExistingObjProductInventoryDetails = GetProductInventoryDetails(ObjProductInventoryDetails.StockName);  
                Double OrderedQty = ObjProductInventoryDetails.Inventory - ExistingObjProductInventoryDetails.Inventory;
                Double ReceivedQty = OrderedQty;
                Double NetQty = ExistingObjProductInventoryDetails.Inventory + ReceivedQty;
                String EntryType = "Sale";
                int Active = (ObjProductInventoryDetails.Active == true) ? 1 : 0;
                if (OrderedQty < 0 && ReceivedQty > 0) EntryType = "Missing";

                DateTime Now = DateTime.Now;
                ObjMySQLHelper.InsertIntoTable("ProductStockHistory",
                    new List<String>() { "ProductInvID", "Type", "OrderedQty", "ReceivedQty", "NetQty", "PODate", "UpdateDate" },
                    new List<String>() { ObjProductInventoryDetails.ProductInvID.ToString(), EntryType, OrderedQty.ToString(), ReceivedQty.ToString(), NetQty.ToString(),
                            MySQLHelper.GetDateStringForDB(Now), MySQLHelper.GetDateStringForDB(DateTime.Now)},
                    new List<Types>() { Types.Number, Types.String, Types.Number, Types.Number, Types.Number, Types.String, Types.String });

                ObjMySQLHelper.UpdateTableDetails("ProductInventory", new List<String>() { "Inventory", "LastPODate" ,"Active"},
                    new List<String>() { NetQty.ToString(), MySQLHelper.GetDateStringForDB(Now) , Active.ToString() },
                    new List<Types>() { Types.Number, Types.String , Types.Number }, $"ProductInvID = {ObjProductInventoryDetails.ProductInvID}");

                return 0;

            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateProductInventoryDatatoDB", ex);
                return -1;
            }
        }

        public void UpdateProductInventoryDataFromInvoice(List<InvoiceItemDetails> ListInvoiceItems, DateTime InvoiceDate)
        {
            try
            {
                //Insert one record for each InvoiceItem in ProductStockHistory table
                for (int i = 0; i < ListInvoiceItems.Count; i++)
                {
                    if (ListInvoiceItems[i].InvoiceItemStatus != INVOICEITEMSTATUS.Invoiced) continue;

                    ProductDetails ObjProductDetails = GetProductDetails(ListInvoiceItems[i].ProductID);
                    Int32 ProductInvID = ObjProductDetails.ProductInvID;
                    ProductInventoryDetails ObjProductInventoryDetails = GetProductInventoryDetails(ProductInvID);
                    //Double OrderedQty = ObjProductInventoryDetails.ComputeInventory(ListInvoiceItems[i].OrderQty, ObjProductDetails.Units, ObjProductDetails.UnitsOfMeasurement);
                    Double OrderedQty = -1; bool isValid = false;
                    if (ListInvoiceItems[i].OrderQty != string.Empty) isValid = CommonFunctions.ValidateDoubleORIntVal(ListInvoiceItems[i].OrderQty);

                    if (isValid) OrderedQty = ObjProductInventoryDetails.ComputeInventory(Double.Parse(ListInvoiceItems[i].OrderQty), ObjProductDetails.Units, ObjProductDetails.UnitsOfMeasurement);
                    else OrderedQty = ObjProductInventoryDetails.ComputeInventory(ListInvoiceItems[i].SaleQty, ObjProductDetails.Units, ObjProductDetails.UnitsOfMeasurement);

                    Double ReceivedQty = -1 * ObjProductInventoryDetails.ComputeInventory(ListInvoiceItems[i].SaleQty, ObjProductDetails.Units, ObjProductDetails.UnitsOfMeasurement);
                    Double NetQty = ObjProductInventoryDetails.Inventory + ReceivedQty;
                    String EntryType = "Sale";
                    if (OrderedQty < 0 && ReceivedQty > 0) EntryType = "SaleRev";

                    ObjMySQLHelper.InsertIntoTable("ProductStockHistory",
                        new List<String>() { "ProductInvID", "Type", "OrderedQty", "ReceivedQty", "NetQty", "PODate", "UpdateDate" },
                        new List<String>() { ProductInvID.ToString(), EntryType, OrderedQty.ToString(), ReceivedQty.ToString(), NetQty.ToString(),
                            MySQLHelper.GetDateStringForDB(InvoiceDate), MySQLHelper.GetDateStringForDB(DateTime.Now)},
                        new List<Types>() { Types.Number, Types.String, Types.Number, Types.Number, Types.Number, Types.String, Types.String });

                    ObjMySQLHelper.UpdateTableDetails("ProductInventory", new List<String>() { "Inventory", "LastPODate" },
                        new List<String>() { NetQty.ToString(), MySQLHelper.GetDateStringForDB(InvoiceDate) },
                        new List<Types>() { Types.Number, Types.String }, $"ProductInvID = {ProductInvID}");
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateProductInventoryDataFromInvoice(InvoiceItemDetails)", ex);
                throw;
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
                    CommonFunctions.SetAllBorders(xlRange1);
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
                CommonFunctions.SetAllBorders(xlRange);

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
                        //dtRow[1] = tmpProductDetails.ListPrices[Index];

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

        public String GenerateNewSKUID()
        {
            try
            {
                String MaxProductSKU = ObjMySQLHelper.GetIDValue("ProductMaster");
                if (String.IsNullOrEmpty(MaxProductSKU)) MaxProductSKU = "0";
                //Object RetVal = ObjMySQLHelper.ExecuteScalar("Select ProductSKU from ProductMaster Where ProductID = (Select Max(ProductID) MaxProductID from ProductMaster);");
                //String MaxProductSKU = (RetVal == null) ? "0" : RetVal.ToString();
                return CommonFunctions.GenerateNextID(SKUPrefix, MaxProductSKU);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GenerateNewSKUID()", ex);
                throw;
            }
        }

        public List<String> GetStockProductsList()
        {
            try
            {
                List<String> ListStockProductNames = new List<String>();
                IEnumerable<String[]> StockProducts = ObjMySQLHelper.ExecuteQuery("Select Distinct StockName from ProductInventory Order by StockName;");
                foreach (var item in StockProducts)
                {
                    ListStockProductNames.Add(item[0]);
                }
                return ListStockProductNames;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.GetStockProductsList()", ex);
                return null;
            }
        }

        public HSNCodeDetails AddUpdateHSNCodeDetails(HSNCodeDetails ObjHSNCodeDetails)
        {
            try
            {
                if (ObjHSNCodeDetails.TaxID < 0)
                    return AddNewHSNCodeDetails(ObjHSNCodeDetails);
                else
                    return UpdateHSNCodeDetails(ObjHSNCodeDetails);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.AddUpdateHSNCodeDetails()", ex);
                return null;
            }
        }

        HSNCodeDetails AddNewHSNCodeDetails(HSNCodeDetails ObjHSNCodeDetails)
        {
            try
            {
                String Query = "";
                Query = "Insert into TaxMaster(HSNCode, CGST, SGST, IGST)";
                Query += $" Values ('{ObjHSNCodeDetails.HSNCode}', '{ObjHSNCodeDetails.ListTaxRates[0]}', '{ObjHSNCodeDetails.ListTaxRates[1]}', " +
                         $"{ObjHSNCodeDetails.ListTaxRates[2]})";
                ObjMySQLHelper.ExecuteNonQuery(Query);

                ObjHSNCodeDetails.TaxID = ((ListHSNCodeDetails.Count > 0) ? ListHSNCodeDetails.Max(e => e.TaxID) : 0) + 1;
                AddHSNCode(ObjHSNCodeDetails);
                //Int32 Index = ListHSNCodeDetails.BinarySearch(ObjHSNCodeDetails, ObjHSNCodeDetails);
                //ListHSNCodeDetails.Insert(~Index, ObjHSNCodeDetails);

                return ObjHSNCodeDetails;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.AddNewHSNCodeDetails()", ex);
                return null;
            }
        }

        HSNCodeDetails UpdateHSNCodeDetails(HSNCodeDetails ObjHSNCodeDetails)
        {
            try
            {
                HSNCodeDetails tmpObjHSNCodeDetails = GetTaxDetails(ObjHSNCodeDetails.TaxID);

                String Query = "";
                for (int i = 0; i < ListTaxGroupDetails.Count; i++)
                {
                    if (Math.Abs(tmpObjHSNCodeDetails.ListTaxRates[i] - ObjHSNCodeDetails.ListTaxRates[i]) > 0)
                    {
                        if (!String.IsNullOrEmpty(Query)) Query += ",";
                        Query += $" {ListTaxGroupDetails[i].Name} = {ObjHSNCodeDetails.ListTaxRates[i]}";
                        tmpObjHSNCodeDetails.ListTaxRates[i] = ObjHSNCodeDetails.ListTaxRates[i];
                    }
                }
                Query = $"Update TaxMaster Set {Query} Where TaxID = {ObjHSNCodeDetails.TaxID}";
                ObjMySQLHelper.ExecuteNonQuery(Query);

                return tmpObjHSNCodeDetails;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateHSNCodeDetails()", ex);
                return null;
            }
        }

        public ProductDetails AddUpdateProductDetails(ProductDetails tmpProductDetails, ProductInventoryDetails tmpProductInventoryDetails)
        {
            try
            {
                if (tmpProductDetails.ProductID < 0)
                {
                    return AddNewProductDetails(tmpProductDetails, tmpProductInventoryDetails);
                }
                else
                {
                    return UpdateProductDetails(tmpProductDetails, tmpProductInventoryDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.AddUpdateProductDetails()", ex);
            }
            return null;
        }

        ProductDetails AddNewProductDetails(ProductDetails tmpProductDetails, ProductInventoryDetails tmpProductInventoryDetails)
        {
            try
            {
                ProductDetails productDetails = GetProductDetails(tmpProductDetails.ItemName);
                if (productDetails != null) return null;

                String Query = "";
                Int32 InventoryIndex = ListProductInventoryDetails.FindIndex(e => e.StockName.Equals(tmpProductInventoryDetails.StockName, StringComparison.InvariantCultureIgnoreCase));
                if (InventoryIndex < 0)
                {
                    tmpProductInventoryDetails = AddNewProductInventoryDetails(tmpProductInventoryDetails);
                }
                else
                {
                    tmpProductInventoryDetails = ListProductInventoryDetails[InventoryIndex];
                }

                tmpProductDetails.ProductInvID = tmpProductInventoryDetails.ProductInvID;
                tmpProductDetails.CategoryID = ListProductCategories[ListProductCategories.FindIndex(e => e.CategoryName.Equals(tmpProductDetails.CategoryName, StringComparison.InvariantCultureIgnoreCase))].CategoryID;
                tmpProductDetails.AddedDate = DateTime.Now;
                tmpProductDetails.HSNCodeIndex = ListHSNCodeDetails.FindIndex(e => e.HSNCode.Equals(tmpProductDetails.HSNCode));
                tmpProductDetails.TaxID = ListHSNCodeDetails[tmpProductDetails.HSNCodeIndex].TaxID;
                tmpProductDetails.StockProductIndex = ListProductInventoryDetails.FindIndex(e => e.StockName.Equals(tmpProductInventoryDetails.StockName));
                VendorDetails tmpVendorDetails = CommonFunctions.ObjVendorMaster.GetVendorDetails(tmpProductDetails.VendorName);
                tmpProductDetails.VendorID = (tmpVendorDetails != null) ? tmpVendorDetails.VendorID : -1;

                Query = "Insert into ProductMaster(ProductSKU, ProductName, Description, CategoryID, Units, UnitsOfMeasurement, " +
                        "SortName, TaxID, ProductInvID, VendorID, Active, AddedDate, PurchasePrice, WholesalePrice, RetailPrice, MaxRetailPrice, Barcode)";
                Query += $" Values ('{tmpProductDetails.ProductSKU}', '{tmpProductDetails.ItemName}', '{tmpProductDetails.ProductDesc}', " +
                         $"{tmpProductDetails.CategoryID}, {tmpProductDetails.Units}, '{tmpProductDetails.UnitsOfMeasurement}', " +
                         $"'{tmpProductDetails.SortName}', {tmpProductDetails.TaxID}, {tmpProductDetails.ProductInvID}, {tmpProductDetails.VendorID}, " +
                         $"{(tmpProductDetails.Active ? 1 : 0)}, '{MySQLHelper.GetDateStringForDB(tmpProductDetails.AddedDate)}', " +
                         $"{tmpProductDetails.PurchasePrice}, {tmpProductDetails.WholesalePrice}, {tmpProductDetails.RetailPrice}, {tmpProductDetails.MaxRetailPrice}, " +
                         $"'{((tmpProductDetails.ArrBarcodes != null && tmpProductDetails.ArrBarcodes.Length > 0) ? String.Join("|", tmpProductDetails.ArrBarcodes) : "")}')";
                ObjMySQLHelper.ExecuteNonQuery(Query);
                ObjMySQLHelper.UpdateIDValue("ProductMaster", tmpProductDetails.ProductSKU);
                tmpProductDetails.ProductID = ((ListProducts.Count > 0) ? ListProducts.Max(e => e.ProductID) : 0) + 1;
                AddProductToCache(tmpProductDetails);

                return tmpProductDetails;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.AddNewProductDetails()", ex);
                return null;
            }
        }

        public ProductInventoryDetails AddNewProductInventoryDetails(ProductInventoryDetails tmpProductInventoryDetails)
        {
            try
            {
                //Check for existing StockName
                String Query = $"Select ProductInvID from ProductInventory Where StockName = '{tmpProductInventoryDetails.StockName}'";
                DataTable dtInvDetails = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                if (dtInvDetails.Rows.Count == 0)
                {
                    //Insert new record
                    Query = "Insert into ProductInventory(StockName, Inventory, Units, UnitsOfMeasurement, ReOrderStockLevel, ReOrderStockQty,LastPODate, LastUpdateDate)";
                    Query += $" Values ('{tmpProductInventoryDetails.StockName}', {tmpProductInventoryDetails.Inventory}," +
                             $"{tmpProductInventoryDetails.Units}, '{tmpProductInventoryDetails.UnitsOfMeasurement}'," +
                             $"{tmpProductInventoryDetails.ReOrderStockLevel}, {tmpProductInventoryDetails.ReOrderStockQty},'{ MySQLHelper.GetDateTimeStringForDB(tmpProductInventoryDetails.LastPODate)}','{ MySQLHelper.GetDateTimeStringForDB(tmpProductInventoryDetails.LastUpdateDate)}')";
                    ObjMySQLHelper.ExecuteNonQuery(Query);
                    //Query = "Select * from ProductInventory Order by StockName";
                    //LoadProductInventory(ObjMySQLHelper.GetQueryResultInDataTable(Query));
                }

                //Reload Product Inventory cache
                ListProductInventoryDetails.Clear();
                Query = $"Select * from ProductInventory Order by StockName";
                LoadProductInventory(ObjMySQLHelper.GetQueryResultInDataTable(Query));

                Int32 InventoryIndex = ListProductInventoryDetails.FindIndex(e => e.StockName.Equals(tmpProductInventoryDetails.StockName, StringComparison.InvariantCultureIgnoreCase));
                tmpProductInventoryDetails = ListProductInventoryDetails[InventoryIndex];

                return tmpProductInventoryDetails;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.AddNewProductInventoryDetails()", ex);
                return null;
            }
        }

        ProductDetails UpdateProductDetails(ProductDetails tmpProductDetails, ProductInventoryDetails tmpProductInventoryDetails)
        {
            try
            {
                ProductDetails CurProductDetails = GetProductDetails(tmpProductDetails.ProductID);

                tmpProductInventoryDetails = UpdateProductInventoryDetails(tmpProductInventoryDetails);
                tmpProductDetails.CategoryID = GetCategoryDetails(tmpProductDetails.CategoryName).CategoryID;
                tmpProductDetails.ProductInvID = tmpProductInventoryDetails.ProductInvID;
                tmpProductDetails.TaxID = GetHSNCodeDetails(tmpProductDetails.HSNCode).TaxID;

                if (CurProductDetails.Equals(tmpProductDetails)) return CurProductDetails;

                CurProductDetails.StockName = tmpProductInventoryDetails.StockName;
                CurProductDetails.ProductInvID = tmpProductInventoryDetails.ProductInvID;
                CurProductDetails.StockProductIndex = ListProductInventoryDetails.FindIndex(e => e.StockName.Equals(tmpProductDetails.StockName));

                CurProductDetails.ItemName = tmpProductDetails.ItemName;
                CurProductDetails.ProductDesc = tmpProductDetails.ProductDesc;
                if (!CurProductDetails.CategoryName.Equals(tmpProductDetails.CategoryName))
                {
                    CurProductDetails.CategoryName = tmpProductDetails.CategoryName;
                    CurProductDetails.CategoryID = tmpProductDetails.CategoryID;
                }

                CurProductDetails.Units = tmpProductDetails.Units;
                CurProductDetails.UnitsOfMeasurement = tmpProductDetails.UnitsOfMeasurement;
                CurProductDetails.SortName = tmpProductDetails.SortName;

                if (!CurProductDetails.HSNCode.Equals(tmpProductDetails.HSNCode))
                {
                    CurProductDetails.HSNCode = tmpProductDetails.HSNCode;
                    CurProductDetails.TaxID = tmpProductDetails.TaxID;
                    CurProductDetails.HSNCodeIndex = ListHSNCodeDetails.FindIndex(e => e.TaxID == CurProductDetails.TaxID);
                }

                CurProductDetails.VendorID = tmpProductDetails.VendorID;
                CurProductDetails.Active = tmpProductDetails.Active;
                CurProductDetails.PurchasePrice = tmpProductDetails.PurchasePrice;
                CurProductDetails.WholesalePrice = tmpProductDetails.WholesalePrice;
                CurProductDetails.RetailPrice = tmpProductDetails.RetailPrice;
                CurProductDetails.MaxRetailPrice = tmpProductDetails.MaxRetailPrice;

                if (!String.Join("|", CurProductDetails.ArrBarcodes).Equals(String.Join("|", tmpProductDetails.ArrBarcodes)))
                {
                    foreach (var Barcode in CurProductDetails.ArrBarcodes)
                    {
                        List<Int32> ListProductIDs = GetProductIDListForBarcode(Barcode);
                        ListProductIDs.Remove(CurProductDetails.ProductID);
                    }
                    CurProductDetails.ArrBarcodes = tmpProductDetails.ArrBarcodes;
                    AddToBarcodeDetails(CurProductDetails.ProductID);
                }

                String Query = "Update ProductMaster Set ";
                Query += $" ProductName = '{CurProductDetails.ItemName}', Description = '{CurProductDetails.ProductDesc}', CategoryID = {CurProductDetails.CategoryID}," +
                         $" Units = '{CurProductDetails.Units}', UnitsOfMeasurement = '{CurProductDetails.UnitsOfMeasurement}', SortName = '{CurProductDetails.SortName}'," +
                         $" TaxID = {CurProductDetails.TaxID}, ProductInvID = {CurProductDetails.ProductInvID}, VendorID = {CurProductDetails.VendorID}, Active = {(CurProductDetails.Active ? 1 : 0)}," +
                         $" PurchasePrice = {CurProductDetails.PurchasePrice}, WholesalePrice = {CurProductDetails.WholesalePrice}," +
                         $" RetailPrice = {CurProductDetails.RetailPrice}, MaxRetailPrice = {CurProductDetails.MaxRetailPrice}," +
                         $" Barcode = '{(CurProductDetails.ArrBarcodes.Length > 0 ? String.Join("|", CurProductDetails.ArrBarcodes) : "")}'";
                Query += $" Where ProductID = {CurProductDetails.ProductID}";
                ObjMySQLHelper.ExecuteNonQuery(Query);

                return CurProductDetails;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateProductDetails()", ex);
            }
            return tmpProductDetails;
        }

        ProductInventoryDetails UpdateProductInventoryDetails(ProductInventoryDetails tmpProductInventoryDetails)
        {
            try
            {
                Int32 Index = ListProductInventoryDetails.FindIndex(e => e.StockName.Equals(tmpProductInventoryDetails.StockName, StringComparison.InvariantCultureIgnoreCase));

                ProductInventoryDetails CurrProductInventoryDetails = null;
                if (Index < 0)
                {
                    CurrProductInventoryDetails = AddNewProductInventoryDetails(tmpProductInventoryDetails);
                }
                else
                {
                    CurrProductInventoryDetails = ListProductInventoryDetails[Index];
                    if (CurrProductInventoryDetails.Equals(tmpProductInventoryDetails)) return CurrProductInventoryDetails;

                    CurrProductInventoryDetails.Inventory = tmpProductInventoryDetails.Inventory;
                    CurrProductInventoryDetails.ReOrderStockLevel = tmpProductInventoryDetails.ReOrderStockLevel;
                    CurrProductInventoryDetails.ReOrderStockQty = tmpProductInventoryDetails.ReOrderStockQty;
                    CurrProductInventoryDetails.Units = tmpProductInventoryDetails.Units;
                    CurrProductInventoryDetails.UnitsOfMeasurement = tmpProductInventoryDetails.UnitsOfMeasurement;

                    String Query = "Update ProductInventory Set ";
                    Query += $" StockName = '{CurrProductInventoryDetails.StockName}', Inventory = {CurrProductInventoryDetails.Inventory}," +
                             $" Units = '{CurrProductInventoryDetails.Units}', UnitsOfMeasurement = '{CurrProductInventoryDetails.UnitsOfMeasurement}'," +
                             $" ReOrderStockLevel = {CurrProductInventoryDetails.ReOrderStockLevel}, ReOrderStockQty = {CurrProductInventoryDetails.ReOrderStockQty}";
                    Query += $" Where ProductInvID = {CurrProductInventoryDetails.ProductInvID}";
                    ObjMySQLHelper.ExecuteNonQuery(Query);
                }

                return CurrProductInventoryDetails;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateProductInventoryDetails()", ex);
                return null;
            }
        }


        public void DeleteProduct(Int32 ProductID)
        {
            try
            {
                //String Query = String.Format("Update ProductMaster Set Active = 0 Where ProductID = {0};", ProductID);
                //ObjMySQLHelper.ExecuteNonQuery(Query);

                ObjMySQLHelper.UpdateTableDetails("ProductMaster", new List<string>() { "Active" }, new List<string>() { "0" },
                    new List<Types>() { Types.Number }, $"ProductID = {ProductID}");

                GetProductDetails(ProductID).Active = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.DeleteProduct()", ex);
            }
        }

        public Int32 DeleteHSNCodeDetails(String HSNCode)
        {
            try
            {
                HSNCodeDetails tmpHSNCodeDetails = GetHSNCodeDetails(HSNCode);
                if (ListProducts.Count(e => e.TaxID == tmpHSNCodeDetails.TaxID) > 0) return -1;

                Int32 Result = ObjMySQLHelper.DeleteRow("TaxMaster", "TaxID", tmpHSNCodeDetails.TaxID.ToString(), Types.Number);

                if (Result > 0) ListHSNCodeDetails.Remove(tmpHSNCodeDetails);

                return Result;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.DeleteHSNCodeDetails()", ex);
                return -2;
            }
        }

        String[] ArrSheetNamesToImport = new String[] { "Products", "Categories", "Inventory", "HSNCodes", "Vendors" };
        String[][] ArrSheetColumns = new String[5][]
        {
                    new String[] { "ProductName", "Description", "Category", "PurchasePrice", "WholesalePrice", "RetailPrice", "MaxRetailPrice", "Units", "UnitsOfMeasurement", "SortName", "HSNCode", "StockName", "VendorName", "Active" },
                    new String[] { "CategoryName", "Description" },
                    new String[] { "StockName", "Inventory", "Units", "UnitsOfMeasurement", "ReOrderStockLevel", "ReOrderStockQty" },
                    new String[] { "HSNCode", "CGST", "SGST", "IGST" },
                    new String[] { "VendorName", "Address", "PhoneNo", "GSTIN", "State", "Active" },
        };
        DataTable dtProductsToImport, dtProductCategoriesToImport, dtProductInventoryToImport, dtHSNCodesToImport, dtVendorsToImport;
        DataTable dtProductsToUpdate, dtProductInventoryToUpdate;
        Boolean[] ArrDataToImport = null;

        public String ValidateExcelFileToImport(String ExcelFilePathToImport, Boolean[] ArrDataToImport)
        {
            try
            {
                this.ArrDataToImport = ArrDataToImport;
                Excel.Application xlApp = new Excel.Application();
                String MissingData = "";
                try
                {
                    Excel.Workbook ExcelFileToImport = xlApp.Workbooks.Open(ExcelFilePathToImport);

                    #region Check for required worksheets
                    for (int i = 0; i < ArrSheetNamesToImport.Length; i++)
                    {
                        if (ArrDataToImport[i] == false) continue;

                        if (CommonFunctions.GetWorksheet(ExcelFileToImport, ArrSheetNamesToImport[i]) == null)
                        {
                            if (!String.IsNullOrEmpty(MissingData)) MissingData += ",";
                            MissingData += ArrSheetNamesToImport[i];
                        }
                    }

                    if (!String.IsNullOrEmpty(MissingData))
                    {
                        return $"Unable to find following WorkSheets in Excel file:\n{MissingData}";
                    }
                    ExcelFileToImport.Close(SaveChanges: false);
                    #endregion
                }
                catch (Exception ex)
                {
                    CommonFunctions.ShowErrorDialog($"{this}.ValidateExcelFileToImport()", ex);
                    return $"Error:{ex.Message}";
                }
                finally
                {
                    xlApp.Quit();
                    CommonFunctions.ReleaseCOMObject(xlApp);
                }

                #region Check for columns in each required sheet
                MissingData = "";
                for (int i = 0; i < ArrSheetNamesToImport.Length; i++)
                {
                    String MissingCols = "";
                    DataTable dtTable = CommonFunctions.ReturnDataTableFromExcelWorksheet(ArrSheetNamesToImport[i], ExcelFilePathToImport, "*");
                    String[] ArrDtColumns = new String[dtTable.Columns.Count];
                    for (int j = 0; j < dtTable.Columns.Count; j++)
                    {
                        ArrDtColumns[j] = dtTable.Columns[j].ColumnName.ToUpper().Trim();
                    }

                    for (int j = 0; j < ArrSheetColumns[i].Length; j++)
                    {
                        if (!ArrDtColumns.Contains(ArrSheetColumns[i][j].ToUpper()))
                        {
                            if (String.IsNullOrEmpty(MissingCols))
                            {
                                MissingCols = $"\nMissing Columns in {ArrSheetNamesToImport[i]}: {ArrSheetColumns[i][j]}";
                            }
                            else
                            {
                                MissingCols += $", {ArrSheetColumns[i][j]}";
                            }
                        }
                    }

                    if (!String.IsNullOrEmpty(MissingCols)) MissingData += "\n" + MissingCols;
                    else
                    {
                        if (i == 0) dtProductsToImport = dtTable;
                        else if (i == 1) dtProductCategoriesToImport = dtTable;
                        else if (i == 2) dtProductInventoryToImport = dtTable;
                        else if (i == 3) dtHSNCodesToImport = dtTable;
                        else if (i == 4) dtVendorsToImport = dtTable;
                    }
                }

                if (!String.IsNullOrEmpty(MissingData))
                {
                    return $"Unable to find following columns in WorkSheets:{MissingData}";
                }
                #endregion

                return "";
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ValidateExcelFileToImport()", ex);
                return $"Error:{ex.Message}";
            }
        }

        public Int32 ProcessStocksDataFromExcelFile(string ExcelFilePath,out String Errmsg, out String ProcessStatus,  out Int32 ExistingProductsInventoryCount)
        {
            ProcessStatus = ""; Errmsg="";  ExistingProductsInventoryCount = 0;
            try
            {
                Excel.Application xlApp = new Excel.Application();
                dtProductInventoryToImport = CommonFunctions.ReturnDataTableFromExcelWorksheet("Stock Details", ExcelFilePath, "*");
                if (dtProductInventoryToImport == null)
                {
                    Errmsg = "Provided Stock Summary file doesn't contain \"Stock Details\" Sheet.\nPlease provide correct file.";
                    return -1;
                }
                //Int32 ExistingProductInventoryCount = 0;
                Int32 TotalRecordCount = 0;
                dtProductInventoryToUpdate = new DataTable();
                foreach (DataColumn item in dtProductInventoryToImport.Columns)
                {
                    dtProductInventoryToUpdate.Columns.Add(new DataColumn(item.ColumnName, item.DataType));
                }
                for (int i = dtProductInventoryToImport.Rows.Count - 1; i >= 0; i--)
                // foreach (DataRow item in dtStockSummary.Rows)
                {
                    ProductInventoryDetails tmpProductInventoryDetails = GetStockProductDetails(dtProductInventoryToImport.Rows[i]["StockName"].ToString());
                    if (tmpProductInventoryDetails != null)
                    {
                        ExistingProductsInventoryCount++;
                        //dtProductInventoryToUpdate.Rows.Add(item.ItemArray.ToArray());
                        // item.Delete();
                        dtProductInventoryToUpdate.Rows.Add(dtProductInventoryToImport.Rows[i].ItemArray.ToArray());//up
                        dtProductInventoryToImport.Rows[i].Delete();//in
                    }
                }

                dtProductInventoryToImport.AcceptChanges();

                ProcessStatus += $"{(!String.IsNullOrEmpty(ProcessStatus) ? "\n" : "")}Inventory:: New:{dtProductInventoryToImport.Rows.Count} Existing:{ExistingProductsInventoryCount}";
                TotalRecordCount += dtProductInventoryToImport.Rows.Count;

                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ProcessStocksDataFromExcelFile()", ex);
                return -1;
            }
        }
        public Int32 ImportStocksDataToDatabase(out String ImportStatus, Int32 ExistingProductsInventoryCount, ReportProgressDel ReportProgress)
        {
            ImportStatus = "";
            try
            {
                if (dtProductInventoryToImport == null) return -1;

                Int32 ErrorInventoryCount = 0;

                //Import Inventory

                Int32 ErrorProductsInventoryUpdateCount = 0;
                //Int32 ErrorProductsInventoryUpdateCount = 0, ErrorInventoryCount = 0;
                if (ExistingProductsInventoryCount == 0) dtProductInventoryToImport.AcceptChanges();
                else
                {
                    foreach (DataRow item in dtProductInventoryToUpdate.Rows)
                    {
                        ProductInventoryDetails tmpProductInventoryDetails = GetStockProductDetails(item["StockName"].ToString());
                        if (tmpProductInventoryDetails == null)
                        {
                            ErrorProductsInventoryUpdateCount++;
                            continue;
                        }
                        tmpProductInventoryDetails = tmpProductInventoryDetails.Clone();
                        tmpProductInventoryDetails.Inventory = (item["Inventory"].ToString() == "") ? 0 : Double.Parse(item["Inventory"].ToString());
                        tmpProductInventoryDetails.Units = (item["Units"].ToString() == "") ? 0 : Double.Parse(item["Units"].ToString());
                        tmpProductInventoryDetails.UnitsOfMeasurement = item["UnitsOfMeasurement"].ToString();
                        tmpProductInventoryDetails.ReOrderStockLevel = (item["ReOrderStockLevel"].ToString() == "") ? 0 : Double.Parse(item["ReOrderStockLevel"].ToString());
                        tmpProductInventoryDetails.Active = bool.Parse(item["Active"].ToString());

                        UpdateProductInventoryDatatoDB(tmpProductInventoryDetails);
                    }
                }

                foreach (DataRow item in dtProductInventoryToImport.Rows)
                {
                    ProductInventoryDetails tmpProductInventoryDetails = new ProductInventoryDetails()
                    {
                        StockName = item["StockName"].ToString(),
                        Inventory = (item["Inventory"].ToString() == "") ? 0 : Double.Parse(item["Inventory"].ToString()),
                        Units = (item["Units"].ToString() == "") ? 0 : Double.Parse(item["Units"].ToString()),
                        UnitsOfMeasurement = item["UnitsOfMeasurement"].ToString(),
                        ReOrderStockLevel = (item["ReOrderStockLevel"].ToString() == "") ? 0 : Double.Parse(item["ReOrderStockLevel"].ToString()),
                        ReOrderStockQty = (item["ReOrderStockQty"].ToString() == "") ? 0 : Double.Parse(item["ReOrderStockQty"].ToString()),
                        LastPODate = DateTime.Now
                    };

                    if (AddProductInvDetailstoDB(tmpProductInventoryDetails) == 1) ErrorInventoryCount++;
                }

                ImportStatus += $"{(!String.IsNullOrEmpty(ImportStatus) ? "\n" : "")}Inventory:: Imported:{dtProductInventoryToImport.Rows.Count - ErrorInventoryCount} Updated:{ExistingProductsInventoryCount - ErrorProductsInventoryUpdateCount} Error:{ErrorInventoryCount + ErrorProductsInventoryUpdateCount}";
                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ImportStocksDataToDatabase()", ex);
                return -1;
            }
            finally
            {
                dtProductInventoryToImport  = null;
                dtProductInventoryToUpdate = null;
            }
        }
        public Int32 ProcessProductsDataFromExcelFile(out String ProcessStatus, out Int32 ExistingProductsCount, out Int32 ExistingProductsInventoryCount)
        {
            ProcessStatus = ""; ExistingProductsCount = 0; ExistingProductsInventoryCount = 0;
            try
            {
                Int32 ExistingProductCount = 0, ExistingProductCategoryCount = 0, ExistingProductInventoryCount = 0, ExistingHSNCount = 0;
                Int32 TotalRecordCount = 0, ExistingVendorCount = 0;

                //Check Products
                if (ArrDataToImport[0])
                {
                    dtProductsToUpdate = new DataTable();
                    foreach (DataColumn item in dtProductsToImport.Columns)
                    {
                        dtProductsToUpdate.Columns.Add(new DataColumn(item.ColumnName, item.DataType));
                    }
                    foreach (DataRow item in dtProductsToImport.Rows)
                    {
                        ProductDetails tmpProductDetails = GetProductDetails(item["ProductName"].ToString());
                        if (tmpProductDetails != null)
                        {
                            ExistingProductCount++;
                            dtProductsToUpdate.Rows.Add(item.ItemArray.ToArray());
                            item.Delete();
                        }
                    }
                    ExistingProductsCount = ExistingProductCount;
                    dtProductsToImport.AcceptChanges();
                    ProcessStatus += $"{(!String.IsNullOrEmpty(ProcessStatus) ? "\n" : "")}Products:: New:{dtProductsToImport.Rows.Count} Existing:{ExistingProductCount}";
                    TotalRecordCount += dtProductsToImport.Rows.Count;
                }

                //Check Product Categories
                if (ArrDataToImport[1])
                {
                    foreach (DataRow item in dtProductCategoriesToImport.Rows)
                    {
                        ProductCategoryDetails tmpProductCategoryDetails = GetCategoryDetails(item["CategoryName"].ToString());
                        if (tmpProductCategoryDetails != null)
                        {
                            ExistingProductCategoryCount++;
                            item.Delete();
                        }
                    }
                    dtProductCategoriesToImport.AcceptChanges();
                    ProcessStatus += $"{(!String.IsNullOrEmpty(ProcessStatus) ? "\n" : "")}Categories:: New:{dtProductCategoriesToImport.Rows.Count} Existing:{ExistingProductCategoryCount}";
                    TotalRecordCount += dtProductCategoriesToImport.Rows.Count;
                }

                //Check Product Inventory
                if (ArrDataToImport[2])
                {
                    dtProductInventoryToUpdate = new DataTable();
                    foreach (DataColumn item in dtProductInventoryToImport.Columns)
                    {
                        dtProductInventoryToUpdate.Columns.Add(new DataColumn(item.ColumnName, item.DataType));
                    }
                    foreach (DataRow item in dtProductInventoryToImport.Rows)
                    {
                        ProductInventoryDetails tmpProductInventoryDetails = GetStockProductDetails(item["StockName"].ToString());
                        if (tmpProductInventoryDetails != null)
                        {
                            ExistingProductInventoryCount++;
                            dtProductInventoryToUpdate.Rows.Add(item.ItemArray.ToArray());
                            item.Delete();
                        }
                    }
                    ExistingProductsInventoryCount = ExistingProductInventoryCount;
                    dtProductInventoryToImport.AcceptChanges();
                    ProcessStatus += $"{(!String.IsNullOrEmpty(ProcessStatus) ? "\n" : "")}Inventory:: New:{dtProductInventoryToImport.Rows.Count} Existing:{ExistingProductInventoryCount}";
                    TotalRecordCount += dtProductInventoryToImport.Rows.Count;
                }

                //Check HSN Codes
                if (ArrDataToImport[3])
                {
                    foreach (DataRow item in dtHSNCodesToImport.Rows)
                    {
                        HSNCodeDetails tmpHSNCodeDetails = GetHSNCodeDetails(item["HSNCode"].ToString());
                        if (tmpHSNCodeDetails != null)
                        {
                            ExistingHSNCount++;
                            item.Delete();
                        }
                    }
                    dtHSNCodesToImport.AcceptChanges();
                    ProcessStatus += $"{(!String.IsNullOrEmpty(ProcessStatus) ? "\n" : "")}HSNCodes:: New:{dtHSNCodesToImport.Rows.Count} Existing:{ExistingHSNCount}";
                    TotalRecordCount += dtHSNCodesToImport.Rows.Count;
                }

                //Check Vendors
                if (ArrDataToImport[4])
                {
                    foreach (DataRow item in dtVendorsToImport.Rows)
                    {
                        VendorDetails tmpVendorDetails = CommonFunctions.ObjVendorMaster.GetVendorDetails(item["VendorName"].ToString());
                        if (tmpVendorDetails != null)
                        {
                            ExistingVendorCount++;
                            item.Delete();
                        }
                    }
                    dtVendorsToImport.AcceptChanges();
                    ProcessStatus += $"{(!String.IsNullOrEmpty(ProcessStatus) ? "\n" : "")}Vendors:: New:{dtVendorsToImport.Rows.Count} Existing:{ExistingVendorCount}";
                    TotalRecordCount += dtVendorsToImport.Rows.Count;
                }

                if ((TotalRecordCount + ExistingProductsCount) > 0) return 0;
                else return 1;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ProcessProductsDataFromExcelFile()", ex);
                ProcessStatus = $"Error:{ex.Message}";
                return -1;
            }
        }
       

        public Int32 ImportProductsDataToDatabase(out String ImportStatus, Int32 ExistingProductsCount, Int32 ExistingProductsInventoryCount, ReportProgressDel ReportProgress)
        {
            ImportStatus = "";
            try
            {
                if (dtProductsToImport == null && dtProductCategoriesToImport == null && dtProductInventoryToImport == null && dtHSNCodesToImport == null) return -1;

                Int32 ErrorVendorsCount = 0, ErrorHSNCodesCount = 0, ErrorInventoryCount = 0, ErrorCategoriesCount = 0, ErrorProductsCount = 0;
                Int32 ReportProgressCount = ArrDataToImport.Count(e => e == true), CurrReportProgressCount = 0;
                //Import Vendors
                if (ArrDataToImport[4])
                {
                    DateTime NowDate = DateTime.Now;
                    foreach (DataRow item in dtVendorsToImport.Rows)
                    {
                        VendorDetails tmpVendorDetails = new VendorDetails()
                        {
                            VendorName = item["VendorName"].ToString(),
                            Address = item["Address"].ToString(),
                            PhoneNo = item["PhoneNo"].ToString(),
                            GSTIN = item["GSTIN"].ToString(),
                            StateID = CommonFunctions.ObjCustomerMasterModel.GetStateID(item["State"].ToString()),
                            Active = Boolean.Parse(item["Active"].ToString()),
                            LastUpdateDate = NowDate ,
                            AddedDate = NowDate
                        };

                        if (CommonFunctions.ObjVendorMaster.CreateNewVendor(tmpVendorDetails) == null) ErrorVendorsCount++;
                    }

                    ImportStatus += $"{(!String.IsNullOrEmpty(ImportStatus) ? "\n" : "")}Vendors:: Imported:{dtVendorsToImport.Rows.Count - ErrorVendorsCount} Error:{ErrorVendorsCount}";
                    CurrReportProgressCount++; ReportProgress((Int32)(100 * 1.0 * CurrReportProgressCount / ReportProgressCount));
                }

                //Import HSN Codes
                if (ArrDataToImport[3])
                {
                    foreach (DataRow item in dtHSNCodesToImport.Rows)
                    {
                        HSNCodeDetails tmpHSNCodeDetails = new HSNCodeDetails();
                        tmpHSNCodeDetails.HSNCode = item["HSNCode"].ToString();
                        tmpHSNCodeDetails.ListTaxRates = new Double[ListTaxGroupDetails.Count];
                        for (int i = 0; i < ListTaxGroupDetails.Count; i++)
                        {
                            tmpHSNCodeDetails.ListTaxRates[i] = Double.Parse(item[ListTaxGroupDetails[i].Name].ToString());
                        }

                        if (AddNewHSNCodeDetails(tmpHSNCodeDetails) == null) ErrorHSNCodesCount++;
                    }

                    ImportStatus += $"{(!String.IsNullOrEmpty(ImportStatus) ? "\n" : "")}HSNCodes:: Imported:{dtHSNCodesToImport.Rows.Count - ErrorHSNCodesCount} Error:{ErrorHSNCodesCount}";
                    CurrReportProgressCount++; ReportProgress((Int32)(100 * 1.0 * CurrReportProgressCount / ReportProgressCount));
                }

                //Import Inventory
                if (ArrDataToImport[2])
                {
                    Int32 ErrorProductsInventoryUpdateCount = 0;
                    if (ExistingProductsInventoryCount == 0) dtProductInventoryToImport.AcceptChanges();
                    else
                    {
                        foreach (DataRow item in dtProductInventoryToUpdate.Rows)
                        {
                            ProductInventoryDetails tmpProductInventoryDetails = GetStockProductDetails(item["StockName"].ToString());
                            if (tmpProductInventoryDetails == null)
                            {
                                ErrorProductsInventoryUpdateCount++;
                                continue;
                            }
                            tmpProductInventoryDetails = tmpProductInventoryDetails.Clone();
                            tmpProductInventoryDetails.Inventory = Double.Parse(item["Inventory"].ToString());
                            tmpProductInventoryDetails.Units = Double.Parse(item["Units"].ToString());
                            tmpProductInventoryDetails.UnitsOfMeasurement = item["UnitsOfMeasurement"].ToString();
                            tmpProductInventoryDetails.ReOrderStockLevel = Double.Parse(item["ReOrderStockLevel"].ToString());
                            tmpProductInventoryDetails.ReOrderStockQty = Double.Parse(item["ReOrderStockQty"].ToString());
                            tmpProductInventoryDetails.LastUpdateDate = DateTime.Now;
                            UpdateProductInventoryDetails(tmpProductInventoryDetails);
                        }
                    }
                    DateTime NowDate = DateTime.Now;
                    foreach (DataRow item in dtProductInventoryToImport.Rows)
                    {
                        ProductInventoryDetails tmpProductInventoryDetails = new ProductInventoryDetails()
                        {
                            StockName = item["StockName"].ToString(),
                            Inventory = Double.Parse(item["Inventory"].ToString()),
                            Units = Double.Parse(item["Units"].ToString()),
                            UnitsOfMeasurement = item["UnitsOfMeasurement"].ToString(),
                            ReOrderStockLevel = Double.Parse(item["ReOrderStockLevel"].ToString()),
                            ReOrderStockQty = Double.Parse(item["ReOrderStockQty"].ToString()),
                            LastPODate = NowDate,
                            LastUpdateDate = NowDate
                        };

                        if (AddNewProductInventoryDetails(tmpProductInventoryDetails) == null) ErrorInventoryCount++;
                    }

                    ImportStatus += $"{(!String.IsNullOrEmpty(ImportStatus) ? "\n" : "")}Inventory:: Imported:{dtProductInventoryToImport.Rows.Count - ErrorInventoryCount} Updated:{ExistingProductsInventoryCount - ErrorProductsInventoryUpdateCount} Error:{ErrorInventoryCount + ErrorProductsInventoryUpdateCount}";
                    CurrReportProgressCount++; ReportProgress((Int32)(100 * 1.0 * CurrReportProgressCount / ReportProgressCount));
                }

                //Import Categories
                if (ArrDataToImport[1])
                {
                    foreach (DataRow item in dtProductCategoriesToImport.Rows)
                    {
                        if (CreateNewProductCategory(item["CategoryName"].ToString(), item["Description"].ToString(), true) == null)
                        {
                            ErrorCategoriesCount++;
                        }
                    }

                    ImportStatus += $"{(!String.IsNullOrEmpty(ImportStatus) ? "\n" : "")}Inventory:: Imported:{dtProductCategoriesToImport.Rows.Count - ErrorCategoriesCount} Error:{ErrorCategoriesCount}";
                    CurrReportProgressCount++; ReportProgress((Int32)(100 * 1.0 * CurrReportProgressCount / ReportProgressCount));
                }

                //Import Products
                if (ArrDataToImport[0])
                {
                    Int32 ErrorProductsUpdateCount = 0;
                    if (ExistingProductsCount == 0) dtProductsToImport.AcceptChanges();
                    else
                    {
                        foreach (DataRow item in dtProductsToUpdate.Rows)
                        {
                            ProductDetails tmpProductDetails = GetProductDetails(item["ProductName"].ToString());
                            if (tmpProductDetails == null)
                            {
                                ErrorProductsUpdateCount++;
                                continue;
                            }
                            tmpProductDetails = tmpProductDetails.Clone();
                            tmpProductDetails.PurchasePrice = Double.Parse(item["PurchasePrice"].ToString());
                            tmpProductDetails.WholesalePrice = Double.Parse(item["WholesalePrice"].ToString());
                            tmpProductDetails.RetailPrice = Double.Parse(item["RetailPrice"].ToString());
                            tmpProductDetails.MaxRetailPrice = Double.Parse(item["MaxRetailPrice"].ToString());

                            UpdateProductPriceDetails(tmpProductDetails);
                        }
                    }

                    foreach (DataRow item in dtProductsToImport.Rows)
                    {
                        ProductDetails tmpProductDetails = new ProductDetails()
                        {
                            ProductSKU = GenerateNewSKUID(),
                            ItemName = item["ProductName"].ToString(),
                            ProductDesc = item["Description"].ToString(),
                            CategoryName = item["Category"].ToString(),
                            PurchasePrice = Double.Parse(item["PurchasePrice"].ToString()),
                            WholesalePrice = Double.Parse(item["WholesalePrice"].ToString()),
                            RetailPrice = Double.Parse(item["RetailPrice"].ToString()),
                            MaxRetailPrice = Double.Parse(item["MaxRetailPrice"].ToString()),
                            Units = Double.Parse(item["Units"].ToString()),
                            UnitsOfMeasurement = item["UnitsOfMeasurement"].ToString(),
                            SortName = item["SortName"].ToString(),
                            HSNCode = item["HSNCode"].ToString(),
                            StockName = item["StockName"].ToString(),
                            VendorName = item["VendorName"].ToString(),
                            Active = Boolean.Parse(item["Active"].ToString() == "1" ? "true" : "false"),
                            ArrBarcodes = item["Barcode"].ToString().Split(',').ToArray()
                        };
                        ProductInventoryDetails tmpProductInventoryDetails = GetStockProductDetails(tmpProductDetails.StockName);

                        if (AddNewProductDetails(tmpProductDetails, tmpProductInventoryDetails) == null) ErrorProductsCount++;
                    }

                    ImportStatus += $"{(!String.IsNullOrEmpty(ImportStatus) ? "\n" : "")}Products:: Imported:{dtProductsToImport.Rows.Count - ErrorProductsCount} Updated:{ExistingProductsCount - ErrorProductsUpdateCount} Error:{ErrorProductsCount + ErrorProductsUpdateCount}";
                    CurrReportProgressCount++; ReportProgress((Int32)(100 * 1.0 * CurrReportProgressCount / ReportProgressCount));
                }

                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ImportProductsDataToDatabase()", ex);
                return -1;
            }
            finally
            {
                ArrDataToImport = null;
                dtProductsToImport = dtProductsToUpdate = dtProductCategoriesToImport = dtProductInventoryToImport = dtHSNCodesToImport = null;
                dtVendorsToImport = dtProductInventoryToUpdate = null;
            }
        }

        public Int32 ExportProductsDataToExcel(List<Int32> ListProductIDs, Boolean[] ObjDetails, String ExcelFilePath, Boolean Append, out String ExportStatus)
        {
            ExportStatus = "";
            try
            {
                //Export Inventory
                if (ObjDetails[1])
                {
                    DataTable dtDataToExport = new DataTable(ArrSheetNamesToImport[2]);
                    for (int i = 0; i < ArrSheetColumns[2].Length; i++)
                    {
                        dtDataToExport.Columns.Add(new DataColumn(ArrSheetColumns[2][i]));
                    }

                    HashSet<Int32> HashProdInvIDs = new HashSet<Int32>();
                    for (int i = 0; i < ListProductIDs.Count; i++)
                    {
                        ProductDetails tmpProductDetails = GetProductDetails(ListProductIDs[i]);
                        if (tmpProductDetails == null) continue;
                        if (HashProdInvIDs.Contains(tmpProductDetails.ProductInvID)) continue;
                        ProductInventoryDetails tmpProductInventoryDetails = GetProductInventoryDetails(tmpProductDetails.ProductInvID);

                        Object[] ArrItems = new Object[]
                        {
                            tmpProductInventoryDetails.StockName,
                            tmpProductInventoryDetails.Inventory,
                            tmpProductInventoryDetails.Units,
                            tmpProductInventoryDetails.UnitsOfMeasurement,
                            tmpProductInventoryDetails.ReOrderStockLevel,
                            tmpProductInventoryDetails.ReOrderStockQty
                        };

                        dtDataToExport.Rows.Add(ArrItems);
                    }

                    Int32 RetVal = CommonFunctions.ExportDataTableToExcelFile(dtDataToExport, ExcelFilePath, dtDataToExport.TableName, Append);
                    if (RetVal < 0) ExportStatus += $"{(!String.IsNullOrEmpty(ExportStatus) ? "\n" : "")}Inventory:: Failed export";
                    else ExportStatus += $"{(!String.IsNullOrEmpty(ExportStatus) ? "\n" : "")}Inventory:: Exported:{dtDataToExport.Rows.Count}";

                    Append = true;
                }

                //Export Products
                if (ObjDetails[0])
                {
                    DataTable dtDataToExport = new DataTable(ArrSheetNamesToImport[0]);
                    for (int i = 0; i < ArrSheetColumns[0].Length; i++)
                    {
                        dtDataToExport.Columns.Add(new DataColumn(ArrSheetColumns[0][i]));
                    }

                    for (int i = 0; i < ListProductIDs.Count; i++)
                    {
                        ProductDetails tmpProductDetails = GetProductDetails(ListProductIDs[i]);
                        if (tmpProductDetails == null) continue;

                        Object[] ArrItems = new Object[]
                        {
                            tmpProductDetails.ItemName,
                            tmpProductDetails.ProductDesc,
                            tmpProductDetails.CategoryName,
                            tmpProductDetails.PurchasePrice,
                            tmpProductDetails.WholesalePrice,
                            tmpProductDetails.RetailPrice,
                            tmpProductDetails.MaxRetailPrice,
                            tmpProductDetails.Units,
                            tmpProductDetails.UnitsOfMeasurement,
                            tmpProductDetails.SortName,
                            tmpProductDetails.HSNCode,
                            tmpProductDetails.StockName,
                            tmpProductDetails.VendorName,
                            tmpProductDetails.Active
                        };

                        dtDataToExport.Rows.Add(ArrItems);
                    }

                    Int32 RetVal = CommonFunctions.ExportDataTableToExcelFile(dtDataToExport, ExcelFilePath, dtDataToExport.TableName, Append);
                    if (RetVal < 0) ExportStatus += $"{(!String.IsNullOrEmpty(ExportStatus) ? "\n" : "")}Products:: Failed export";
                    else ExportStatus += $"{(!String.IsNullOrEmpty(ExportStatus) ? "\n" : "")}Products:: Exported {dtDataToExport.Rows.Count} products data";
                }

                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ExportProductsDataToExcel()", ex);
                return -1;
            }
        }

        public void UpdateProductPriceDetails(ProductDetails tmpProductDetails)
        {
            try
            {
                ProductDetails ObjProductDetails = GetProductDetails(tmpProductDetails.ProductID);

                ObjMySQLHelper.UpdateTableDetails("ProductMaster", new List<string>() { "PurchasePrice", "WholesalePrice", "RetailPrice", "MaxRetailPrice" },
                                        new List<String>() { tmpProductDetails.PurchasePrice.ToString(), tmpProductDetails.WholesalePrice.ToString(),
                                        tmpProductDetails.RetailPrice.ToString(),tmpProductDetails.MaxRetailPrice.ToString()},
                                        new List<Types> { Types.Number, Types.Number, Types.Number, Types.Number },
                                        $"ProductID = {ObjProductDetails.ProductID}");

                ObjProductDetails.PurchasePrice = tmpProductDetails.PurchasePrice;
                ObjProductDetails.WholesalePrice = tmpProductDetails.WholesalePrice;
                ObjProductDetails.RetailPrice = tmpProductDetails.RetailPrice;
                ObjProductDetails.MaxRetailPrice = tmpProductDetails.MaxRetailPrice;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.UpdateProductPriceDetails()", ex);
                throw;
            }
        }
    }
}
