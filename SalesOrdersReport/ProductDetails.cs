using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

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
        public String HSNCode;
        public Double[] ListTaxRates;

        public int Compare(HSNCodeDetails x, HSNCodeDetails y)
        {
            return x.HSNCode.ToUpper().CompareTo(y.HSNCode.ToUpper());
        }
    }

    class ProductDetails : IComparer<ProductDetails>
    {
        public String ItemName, StockName, VendorName, HSNCode, UnitsOfMeasurement, CategoryName;
        public Double PurchasePrice, SellingPrice, Units;
        public Int32 StockProductIndex, HSNCodeIndex;
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
        public Double Units = 1.0, Inventory = 0, OrderQty = 0, RecvdQty = 0, NetQty = 0, SaleQty = 0;
        public Double TotalCost = 0, TotalDiscount = 0, TotalTax = 0, NetCost = 0;
        public Boolean IsUpdated = false, IsStockOverride = false;

        public int Compare(StockProductDetails x, StockProductDetails y)
        {
            return x.StockName.ToUpper().CompareTo(y.StockName.ToUpper());
        }
    }

    class ProductMaster
    {
        List<ProductDetails> ListProducts;
        public List<StockProductDetails> ListStockProducts;
        public List<PriceGroupDetails> ListPriceGroups;
        public List<HSNCodeDetails> ListHSNCodeDetails;
        public List<TaxGroupDetails> ListTaxGroupDetails;
        Int32 DefaultPriceGroupIndex;

        public void Initialize()
        {
            try
            {
                ListProducts = new List<ProductDetails>();
                ListStockProducts = new List<StockProductDetails>();
                ListPriceGroups = new List<PriceGroupDetails>();
                ListHSNCodeDetails = new List<HSNCodeDetails>();
                ListTaxGroupDetails = new List<TaxGroupDetails>();
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
                ObjProductDetails.ItemName = ObjProductDetails.ItemName.Trim();
                if (String.IsNullOrEmpty(ObjProductDetails.StockName))
                    ObjProductDetails.StockName = ObjProductDetails.ItemName;
                ObjProductDetails.StockName = ObjProductDetails.StockName.Trim();

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

        public void AddHSNCode(HSNCodeDetails ObjHSNCodeDetails)
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
                CommonFunctions.ShowErrorDialog("ProductMaster.AddHSNCode()", ex);
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
                CommonFunctions.ShowErrorDialog("ProductMaster.GetHSNCodeDetails()", ex);
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
                CommonFunctions.ShowErrorDialog("ProductMaster.GetTaxRatesForProduct()", ex);
            }
            return null;
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

        public void UpdateHSNProductIndexes()
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
                CommonFunctions.ShowErrorDialog("ProductMaster.UpdateHSNProductIndexes()", ex);
            }
        }

        public List<String> GetProductCategoryList()
        {
            try
            {
                List<String> ListCategories = new List<String>();
                ListCategories.AddRange(ListProducts.Select(e => e.CategoryName).Distinct());
                return ListCategories;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMaster.GetProductCategoryList()", ex);
            }
            return null;
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
                CommonFunctions.ShowErrorDialog("ProductMaster.GetProductListForCategory()", ex);
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
                CommonFunctions.ShowErrorDialog("ProductMaster.GetProductDetails()", ex);
            }
            return null;
        }

        public StockProductDetails GetStockProductDetails(String StockName)
        {
            try
            {
                StockProductDetails ObjStockProduct = new StockProductDetails();
                ObjStockProduct.StockName = StockName.Trim();
                Int32 Index = ListStockProducts.BinarySearch(ObjStockProduct, ObjStockProduct);

                if (Index < 0) return null;
                return ListStockProducts[Index];
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMaster.GetStockProductDetails()", ex);
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

        public void ComputeStockNetData(String TransactionType)
        {
            try
            {
                for (int i = 0; i < ListStockProducts.Count; i++)
                {
                    StockProductDetails ObjStockProductDetails = ListStockProducts[i];
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
                CommonFunctions.ShowErrorDialog("ProductMaster.ComputeStockNetData()", ex);
            }
        }

        public void ResetStockProducts(Boolean Flag = false)
        {
            try
            {
                for (int i = 0; i < ListStockProducts.Count; i++)
                {
                    StockProductDetails ObjStockProductDetails = ListStockProducts[i];
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
                CommonFunctions.ShowErrorDialog("ProductMaster.ResetStockProducts()", ex);
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

                StockProductDetails ObjStockProductDetails = null;
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
                CommonFunctions.ShowErrorDialog("ProductMaster.LoadProductPastSalesFromStockHistoryFile()", ex);
                throw;
            }
        }

        public void LoadProductInventoryFile(DataRow[] drProductInventory)
        {
            try
            {
                foreach (DataRow dr in drProductInventory)
                {
                    StockProductDetails ObjStockProductDetails = GetStockProductDetails(dr["StockName"].ToString().Trim());
                    if (ObjStockProductDetails == null) continue;

                    if (dr["Stock"] != DBNull.Value) ObjStockProductDetails.Inventory = Double.Parse(dr["Stock"].ToString());
                    if (dr["Units"] != DBNull.Value) ObjStockProductDetails.Units = Double.Parse(dr["Units"].ToString());
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("ProductMaster.LoadProductInventoryFile()", ex);
                throw;
            }
        }

        public void UpdateProductInventoryDataFromPO(DataRow[] drProducts, Boolean IsStockOverride)
        {
            try
            {
                foreach (DataRow dr in drProducts)
                {
                    StockProductDetails ObjStockProductDetails = GetStockProductDetails(dr["Item Name"].ToString().Trim());
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
                CommonFunctions.ShowErrorDialog("ProductMaster.UpdateProductInventoryDataFromPO()", ex);
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
                    StockProductDetails ObjStockProductDetails = ListStockProducts[ObjProductDetails.StockProductIndex];
                    ObjStockProductDetails.OrderQty += (Double.Parse(dr["Order Quantity"].ToString().Trim()) * ObjProductDetails.Units);
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
                CommonFunctions.ShowErrorDialog("ProductMaster.UpdateProductInventoryDataFromInvoice()", ex);
                throw;
            }
        }

        public void UpdateProductInventoryFile(Excel.Application xlApp, DateTime SummaryCreationDate, String ProductInventoryFile)
        {
            try
            {
                Excel.Workbook xlProductInventory = xlApp.Workbooks.Open(ProductInventoryFile);
                Excel.Worksheet xlInventoryWorksheet = CommonFunctions.GetWorksheet(xlProductInventory, "Inventory");
                ProductMaster ObjProductMaster = CommonFunctions.ObjProductMaster;

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
                    StockProductDetails ObjStockProductDetails = ObjProductMaster.GetStockProductDetails(StockName);
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
                CommonFunctions.ShowErrorDialog("ProductMaster.UpdateProductInventoryFile()", ex);
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

                ProductMaster ObjProductMaster = CommonFunctions.ObjProductMaster;

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
                for (int i = 0, j = 0; i < ObjProductMaster.ListStockProducts.Count; i++)
                {
                    StockProductDetails ObjStockProduct = ObjProductMaster.ListStockProducts[i];
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
                CommonFunctions.ShowErrorDialog("ProductMaster.UpdateProductStockHistoryFile()", ex);
                throw;
            }
        }
    }
}
