using System;
using System.Collections.Generic;

namespace SalesOrdersReport
{
    enum DiscountTypes
    {
        PERCENT, ABSOLUTE, NONE
    }

    class PriceGroupDetails : IComparer<PriceGroupDetails>
    {
        public Int32 PriceGroupID;
        public String Name, Description, PriceColumn;
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

    class TaxGroupDetails : IComparer<TaxGroupDetails>
    {
        public String Name, Description;
        public Double TaxRate;

        public int Compare(TaxGroupDetails x, TaxGroupDetails y)
        {
            return x.Name.ToUpper().CompareTo(y.Name.ToUpper());
        }

        public Double GetTaxAmount(Double SellingPrice)
        {
            try
            {
                return SellingPrice * TaxRate / 100;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("TaxGroupDetails.GetTaxAmount()", ex);
            }
            return -1;
        }
    }

    class HSNCodeDetails : IComparer<HSNCodeDetails>
    {
        public Int32 TaxID;
        public String HSNCode;
        public Double[] ListTaxRates;

        public int Compare(HSNCodeDetails x, HSNCodeDetails y)
        {
            return x.HSNCode.ToUpper().CompareTo(y.HSNCode.ToUpper());
        }
    }

    class ProductDetails : IComparer<ProductDetails>
    {
        public Int32 ProductID, CategoryID, TaxID, ProductInvID;
        public String ProductSKU, ItemName, ProductDesc, SortName, StockName, VendorName, HSNCode, UnitsOfMeasurement, CategoryName;
        public Double PurchasePrice, SellingPrice, Units;
        public Int32 StockProductIndex, HSNCodeIndex;
        public Double[] ListPrices;
        public DateTime AddedDate, LastUpdateDate;
        public Boolean Active;

        public int Compare(ProductDetails x, ProductDetails y)
        {
            return x.ItemName.ToUpper().CompareTo(y.ItemName.ToUpper());
        }

        public void FillMissingPricesForPriceGroups(List<PriceGroupDetails> ListPriceGroups)
        {
            try
            {
                for (int i = 0; i < ListPriceGroups.Count; i++)
                {
                    if (Double.IsNaN(ListPrices[i]) || ListPrices[i] < 0)
                    {
                        ListPrices[i] = ListPriceGroups[i].GetPrice(SellingPrice);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("PriceGroupDetails.FillMissingPricesForPriceGroups()", ex);
            }
        }
    }

    class ProductInventoryDetails : IComparer<ProductInventoryDetails>
    {
        public Int32 ProductInvID;
        public String StockName, UnitsOfMeasurement;
        public List<Int32> ListProductIndexes;
        public Double Units = 1.0, Inventory = 0, OrderQty = 0, RecvdQty = 0, NetQty = 0, SaleQty = 0;
        public Double TotalCost = 0, TotalDiscount = 0, TotalTax = 0, NetCost = 0, ReOrderStockLevel = 0, ReOrderStockQty = 0;
        public Boolean IsUpdated = false, IsStockOverride = false;
        public DateTime LastPODate, LastUpdateDate;

        public int Compare(ProductInventoryDetails x, ProductInventoryDetails y)
        {
            return x.StockName.ToUpper().CompareTo(y.StockName.ToUpper());
        }
    }

    class ProductCategoryDetails : IComparer<ProductCategoryDetails>
    {
        public Int32 CategoryID;
        public String CategoryName, Description;
        public Boolean Active;

        public Int32 Compare(ProductCategoryDetails x, ProductCategoryDetails y)
        {
            return x.CategoryName.ToUpper().CompareTo(y.CategoryName.ToUpper());
        }
    }
}
