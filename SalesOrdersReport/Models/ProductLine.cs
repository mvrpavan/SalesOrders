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
        public VendorMasterModel ObjVendorMaster;
        MySQLHelper ObjMySQLHelper;

        public ProductLine()
        {
            try
            {
                ObjProductMaster = new ProductMasterModel();
                ObjProductMaster.Initialize();
                ObjVendorMaster = new VendorMasterModel();
                ObjVendorMaster.Initialize();
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
        public void LoadVendorMasterTable()
        {
            try
            {
                String Query = "Select * from VendorMaster Order by VendorName;";
                DataTable dtVendorMaster = ObjMySQLHelper.GetQueryResultInDataTable(Query);
                ObjVendorMaster.LoadVendorMaster(dtVendorMaster);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.LoadVendorMasterTable()", ex);
            }
        }
    }
}
