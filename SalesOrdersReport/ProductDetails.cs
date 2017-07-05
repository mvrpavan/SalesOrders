using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesOrdersReport
{
    enum DiscountTypes
    {
        PERCENT, ABSOLUTE, NONE
    }

    class PriceGroupDetails : IComparer<PriceGroupDetails>
    {
        public String Name, Description;
        public Double Discount;
        public DiscountTypes DiscountType;
        public Boolean IsDefault;

        public int Compare(PriceGroupDetails x, PriceGroupDetails y)
        {
            return x.Name.ToUpper().CompareTo(y.Name.ToUpper());
        }

        public static DiscountTypes GetDiscountType(String DiscountTypeName)
        {
            try
            {
                switch (DiscountTypeName.ToUpper().Trim())
                {
                    case "ABSOLUTE": return DiscountTypes.ABSOLUTE;
                    case "PERCENT": return DiscountTypes.PERCENT;
                    default: return DiscountTypes.NONE;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("PriceGroupDetails.GetDiscountType()", ex);
                return DiscountTypes.NONE;
            }
        }

        public Double GetPrice(Double SellingPrice)
        {
            try
            {
                switch (DiscountType)
                {
                    case DiscountTypes.PERCENT: return (SellingPrice * (1 - Discount) / 100);
                    case DiscountTypes.ABSOLUTE: return (SellingPrice - Discount);
                    case DiscountTypes.NONE:
                    default: return -1;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("PriceGroupDetails.GetPrice()", ex);
            }
            return -1;
        }
    }

    class ProductDetails : IComparer<ProductDetails>
    {
        public String ItemName, StockName, VendorName;
        public Double PurchasePrice, SellingPrice;
        public Int32 StockProductIndex;
        public Double[] ListPrices;
        Boolean[] ArrPriceFilledFlag;

        public int Compare(ProductDetails x, ProductDetails y)
        {
            return x.ItemName.ToUpper().CompareTo(y.ItemName.ToUpper());
        }

        public void FillMissingPricesForPriceGroups(List<PriceGroupDetails> ListPriceGroups)
        {
            try
            {
                ArrPriceFilledFlag = new Boolean[ListPriceGroups.Count];

                for (int i = 0; i < ListPriceGroups.Count; i++)
                {
                    ArrPriceFilledFlag[i] = false;
                    if (Double.IsNaN(ListPrices[i]) || ListPrices[i] < 0)
                    {
                        ListPrices[i] = ListPriceGroups[i].GetPrice(SellingPrice);
                        ArrPriceFilledFlag[i] = true;
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("PriceGroupDetails.FillMissingPricesForPriceGroups()", ex);
            }
        }
    }

    class StockProductDetails : IComparer<StockProductDetails>
    {
        public String StockName;
        public List<Int32> ListProductIndexes;

        public int Compare(StockProductDetails x, StockProductDetails y)
        {
            return x.StockName.ToUpper().CompareTo(y.StockName.ToUpper());
        }
    }

    class ProductMaster
    {
        List<ProductDetails> ListProducts;
        List<StockProductDetails> ListStockProducts;
        public List<PriceGroupDetails> ListPriceGroups;
        Int32 DefaultPriceGroupIndex;

        public void Initialize()
        {
            try
            {
                ListProducts = new List<ProductDetails>();
                ListStockProducts = new List<StockProductDetails>();
                ListPriceGroups = new List<PriceGroupDetails>();
                DefaultPriceGroupIndex = -1;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMaster.Initialize()", ex);
            }
        }

        public void AddPriceGroupToCache(PriceGroupDetails ObjPriceGroupDetails)
        {
            try
            {
                Int32 Index = ListPriceGroups.BinarySearch(ObjPriceGroupDetails, ObjPriceGroupDetails);
                if (Index < 0)
                {
                    ListPriceGroups.Insert(~Index, ObjPriceGroupDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMaster.AddPriceGroupToCache()", ex);
            }
        }

        public void AddProductToCache(ProductDetails ObjProductDetails)
        {
            try
            {
                StockProductDetails ObjStockProductDetails = new StockProductDetails();
                ObjStockProductDetails.StockName = ObjProductDetails.StockName;
                AddStockProduct(ObjStockProductDetails);

                Int32 ProductIndex = ListProducts.BinarySearch(ObjProductDetails, ObjProductDetails);
                if (ProductIndex < 0)
                {
                    ListProducts.Insert(~ProductIndex, ObjProductDetails);
                    ObjProductDetails.FillMissingPricesForPriceGroups(ListPriceGroups);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMaster.AddProductToCache()", ex);
            }
        }

        void AddStockProduct(StockProductDetails ObjStockProductDetails)
        {
            try
            {
                //Add StockProduct to ListStockProducts
                Int32 ProductIndex = ListStockProducts.BinarySearch(ObjStockProductDetails, ObjStockProductDetails);
                if (ProductIndex < 0)
                {
                    ObjStockProductDetails.ListProductIndexes = new List<Int32>();
                    ListStockProducts.Insert(~ProductIndex, ObjStockProductDetails);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMaster.AddStockProduct()", ex);
            }
        }

        public void UpdateStockProductIndexes()
        {
            try
            {
                for (int i = 0; i < ListProducts.Count; i++)
                {
                    StockProductDetails ObjStockProductDetails = new StockProductDetails();
                    ObjStockProductDetails.StockName = ListProducts[i].StockName;

                    Int32 StockProductIndex = ListStockProducts.BinarySearch(ObjStockProductDetails, ObjStockProductDetails);
                    if (StockProductIndex >= 0)
                    {
                        ListStockProducts[StockProductIndex].ListProductIndexes.Add(i);
                        ListProducts[i].StockProductIndex = StockProductIndex;
                    }
                }

                DefaultPriceGroupIndex = ListPriceGroups.FindIndex(e => e.IsDefault);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMaster.UpdateStockProductIndexes()", ex);
            }
        }

        public ProductDetails GetProductDetails(String ItemName)
        {
            try
            {
                ProductDetails ObjProduct = new ProductDetails();
                ObjProduct.ItemName = ItemName;
                Int32 Index = ListProducts.BinarySearch(ObjProduct, ObjProduct);

                if (Index < 0) return null;
                return ListProducts[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMaster.GetProductDetails()", ex);
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
                CommonFunctions.ShowErrorDialog("ProductMaster.GetPriceForProduct()", ex);
            }
            return -1;
        }
    }
}
