using SalesOrdersReport.CommonModules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesOrdersReport
{
    enum DiscountTypes
    {
        PERCENT, ABSOLUTE, NONE
    }

    class PriceGroupDetails : IComparer<PriceGroupDetails>
    {
        public Int32 PriceGroupID;
        public String PriceGrpName, Description, PriceColumn;
        public Double Discount;
        public DiscountTypes DiscountType;
        public Boolean IsDefault;

        public int Compare(PriceGroupDetails x, PriceGroupDetails y)
        {
            return x.PriceGrpName.ToUpper().CompareTo(y.PriceGrpName.ToUpper());
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

        public int Compare(TaxGroupDetails x, TaxGroupDetails y)
        {
            return x.Name.ToUpper().CompareTo(y.Name.ToUpper());
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

    class BarcodeDetails : IComparer<BarcodeDetails>
    {
        public String Barcode;
        public List<Int32> ListProductIDs;

        public int Compare(BarcodeDetails x, BarcodeDetails y)
        {
            return x.Barcode.CompareTo(y.Barcode);
        }
    }

    class ProductDetails : IComparer<ProductDetails>, IEquatable<ProductDetails>
    {
        public Int32 ProductID, CategoryID, TaxID, ProductInvID, VendorID;
        public String ProductSKU, ItemName, ProductDesc, SortName, StockName, VendorName, HSNCode, UnitsOfMeasurement, CategoryName;
        public Double PurchasePrice, WholesalePrice, RetailPrice, MaxRetailPrice, Units;
        public Int32 StockProductIndex, HSNCodeIndex;
        public DateTime AddedDate, LastUpdateDate;
        public Boolean Active;
        public String[] ArrBarcodes;

        public int Compare(ProductDetails x, ProductDetails y)
        {
            return x.ItemName.ToUpper().CompareTo(y.ItemName.ToUpper());
        }

        //public void FillMissingPricesForPriceGroups(List<PriceGroupDetails> ListPriceGroups)
        //{
        //    try
        //    {
        //        for (int i = 0; i < ListPriceGroups.Count; i++)
        //        {
        //            if (Double.IsNaN(ListPrices[i]) || ListPrices[i] < 0)
        //            {
        //                ListPrices[i] = ListPriceGroups[i].GetPrice(RetailPrice);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonFunctions.ShowErrorDialog("PriceGroupDetails.FillMissingPricesForPriceGroups()", ex);
        //    }
        //}

        public ProductDetails Clone()
        {
            try
            {
                ProductDetails tmpProductDetails = (ProductDetails)this.MemberwiseClone();
                //tmpProductDetails.ListPrices = this.ListPrices.ToArray();
                tmpProductDetails.ArrBarcodes = this.ArrBarcodes.ToArray();

                return tmpProductDetails;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.Clone()", ex);
                return null;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ProductDetails);
        }

        public bool Equals(ProductDetails other)
        {
            return other != null &&
                   CategoryID == other.CategoryID &&
                   TaxID == other.TaxID &&
                   ProductInvID == other.ProductInvID &&
                   VendorID == other.VendorID &&
                   ProductSKU == other.ProductSKU &&
                   ItemName == other.ItemName &&
                   ProductDesc == other.ProductDesc &&
                   SortName == other.SortName &&
                   StockName == other.StockName &&
                   VendorName == other.VendorName &&
                   HSNCode == other.HSNCode &&
                   UnitsOfMeasurement == other.UnitsOfMeasurement &&
                   CategoryName == other.CategoryName &&
                   PurchasePrice == other.PurchasePrice &&
                   WholesalePrice == other.WholesalePrice &&
                   RetailPrice == other.RetailPrice &&
                   MaxRetailPrice == other.MaxRetailPrice &&
                   Units == other.Units &&
                   Active == other.Active &&
                   ArrBarcodes == other.ArrBarcodes;
        }

        public override int GetHashCode()
        {
            var hashCode = 213535748;
            hashCode = hashCode * -1521134295 + CategoryID.GetHashCode();
            hashCode = hashCode * -1521134295 + TaxID.GetHashCode();
            hashCode = hashCode * -1521134295 + ProductInvID.GetHashCode();
            hashCode = hashCode * -1521134295 + VendorID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProductSKU);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ItemName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProductDesc);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SortName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(StockName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(VendorName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(HSNCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UnitsOfMeasurement);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CategoryName);
            hashCode = hashCode * -1521134295 + PurchasePrice.GetHashCode();
            hashCode = hashCode * -1521134295 + WholesalePrice.GetHashCode();
            hashCode = hashCode * -1521134295 + RetailPrice.GetHashCode();
            hashCode = hashCode * -1521134295 + MaxRetailPrice.GetHashCode();
            hashCode = hashCode * -1521134295 + Units.GetHashCode();
            hashCode = hashCode * -1521134295 + Active.GetHashCode();
            hashCode = hashCode * -1521134295 + ArrBarcodes.GetHashCode();
            return hashCode;
        }
    }

    class ProductInventoryDetails : IComparer<ProductInventoryDetails>, IEquatable<ProductInventoryDetails>
    {
        public Int32 ProductInvID;
        public String StockName, UnitsOfMeasurement;
        public List<Int32> ListProductIndexes;
        public Double Units = 1.0, Inventory = 0, OrderQty = 0, RecvdQty = 0, NetQty = 0, SaleQty = 0;
        public Double TotalCost = 0, TotalDiscount = 0, TotalTax = 0, NetCost = 0, ReOrderStockLevel = 0, ReOrderStockQty = 0;
        public Boolean IsUpdated = false, IsStockOverride = false;
        public DateTime LastPODate, LastUpdateDate;
        public bool Active = true;

        public int Compare(ProductInventoryDetails x, ProductInventoryDetails y)
        {
            return x.StockName.ToUpper().CompareTo(y.StockName.ToUpper());
        }

        public ProductInventoryDetails Clone()
        {
            ProductInventoryDetails tmpProductInventoryDetails = (ProductInventoryDetails)this.MemberwiseClone();
            tmpProductInventoryDetails.ListProductIndexes = this.ListProductIndexes.ToList();

            return tmpProductInventoryDetails;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ProductInventoryDetails);
        }

        public bool Equals(ProductInventoryDetails other)
        {
            return other != null &&
                   StockName == other.StockName &&
                   UnitsOfMeasurement == other.UnitsOfMeasurement &&
                   Units == other.Units &&
                   Inventory == other.Inventory &&
                   ReOrderStockLevel == other.ReOrderStockLevel &&
                   ReOrderStockQty == other.ReOrderStockQty;
        }

        public override int GetHashCode()
        {
            var hashCode = 1550938422;
            hashCode = hashCode * -1521134295 + ProductInvID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(StockName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UnitsOfMeasurement);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<int>>.Default.GetHashCode(ListProductIndexes);
            hashCode = hashCode * -1521134295 + Units.GetHashCode();
            hashCode = hashCode * -1521134295 + Inventory.GetHashCode();
            hashCode = hashCode * -1521134295 + OrderQty.GetHashCode();
            hashCode = hashCode * -1521134295 + RecvdQty.GetHashCode();
            hashCode = hashCode * -1521134295 + NetQty.GetHashCode();
            hashCode = hashCode * -1521134295 + SaleQty.GetHashCode();
            hashCode = hashCode * -1521134295 + TotalCost.GetHashCode();
            hashCode = hashCode * -1521134295 + TotalDiscount.GetHashCode();
            hashCode = hashCode * -1521134295 + TotalTax.GetHashCode();
            hashCode = hashCode * -1521134295 + NetCost.GetHashCode();
            hashCode = hashCode * -1521134295 + ReOrderStockLevel.GetHashCode();
            hashCode = hashCode * -1521134295 + ReOrderStockQty.GetHashCode();
            hashCode = hashCode * -1521134295 + IsUpdated.GetHashCode();
            hashCode = hashCode * -1521134295 + IsStockOverride.GetHashCode();
            hashCode = hashCode * -1521134295 + LastPODate.GetHashCode();
            hashCode = hashCode * -1521134295 + LastUpdateDate.GetHashCode();
            return hashCode;
        }

        public Double ComputeInventory(Double Quantity, Double ProductUnits, String ProductUOM)
        {
            try
            {
                ProductUOM = ProductUOM.ToUpper();
                if (UnitsOfMeasurement.ToUpper().Equals(ProductUOM)) return Quantity * Units;

                switch (UnitsOfMeasurement.ToUpper())
                {
                    case "KG": if (ProductUOM.Equals("GM")) return Quantity / 1000 * Units; break;
                    case "LITRE": if (ProductUOM.Equals("ML")) return Quantity / 1000 * Units; break;
                    default: return Quantity * Units;
                }
                return 0;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.ComputeInventory()", ex);
                return -1;
            }
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
