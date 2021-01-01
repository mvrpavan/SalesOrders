using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrdersReport
{
    class RunDBScript
    {
        MySQLHelper ObjMySQLHelper;

        public RunDBScript()
        {
            try
            {
                ObjMySQLHelper = MySQLHelper.GetMySqlHelperObj();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("RunDBScript.ctor()", ex);
                throw;
            }
        }

        public void CreateNecessaryTables()
        {
            try
            {
                CreateRoleTable();
                CreateUserTable();
                CreateProductMasterTables();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("RunDBScript.CreateNecessaryTables()", ex);
                throw ex;
            }
        }

        void CreateUserTable()
        {
            try
            {
                List<string> ListUserCol = new List<string>() { "USERID,int", "USERNAME,varchar(50)", "PASSWORD,varchar(50)", "ROLEID,int", "LASTLOGIN,datetime not Null", "LASTPASSWORDCHANGED,datetime not Null", "ACTIVE,tinyint(1)", "PHONENO,varchar(15)", "ADDRESS,varchar(100)" };
                ObjMySQLHelper.CreateTable("USERS", ListUserCol);
                ObjMySQLHelper.CreateNewUser("admin", "admin", "", "", "1");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("RunDBScript.CreateUserTable()", ex);
                throw ex;
            }
        }

        void CreateRoleTable()
        {
            try
            {
                List<string> ListRoleCol = new List<string>() {"ROLEID,int", "ROLENAME,varchar(50)", "PRIVILEGE,varchar(100)" };
                ObjMySQLHelper.CreateTable("ROLE", ListRoleCol);
                ObjMySQLHelper.CreateNewRole("admin", "all");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("RunDBScript.CreateRoleTable()", ex);
                throw ex;
            }
        }

        void CreateProductMasterTables()
        {
            try
            {
                List<String> TableColumns = new List<String>();
                #region Create ProductMaster Table
                TableColumns.Add("ProductID, smallint(5) unsigned NOT NULL AUTO_INCREMENT");
                TableColumns.Add("ProductSKU, varchar(15) NOT NULL");
                TableColumns.Add("ProductName, varchar(50) NOT NULL");
                TableColumns.Add("Description, varchar(200) NOT NULL");
                TableColumns.Add("CategoryID, smallint(5) unsigned NOT NULL");
                TableColumns.Add("SellingPrice, float DEFAULT NULL");
                TableColumns.Add("PurchasePrice, float DEFAULT NULL");
                TableColumns.Add("Units, smallint(5) DEFAULT NULL");
                TableColumns.Add("UnitsOfMeasurement, varchar(10) DEFAULT NULL");
                TableColumns.Add("SortName, varchar(50) NOT NULL");
                TableColumns.Add("TaxID, smallint(5) unsigned DEFAULT NULL");
                TableColumns.Add("ProductInvID, smallint(5) unsigned DEFAULT NULL");
                TableColumns.Add("Active, tinyint(4) NOT NULL DEFAULT '1'");
                TableColumns.Add("AddedDate, datetime NOT NULL");
                TableColumns.Add("LastUpdateDate, timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
                TableColumns.Add("PRIMARY KEY, ProductID");
                ObjMySQLHelper.CreateTable("ProductMaster", TableColumns);
                #endregion

                #region Create ProductCategoryMaster Table
                TableColumns.Clear();
                TableColumns.Add("CategoryID, smallint(5) unsigned NOT NULL AUTO_INCREMENT");
                TableColumns.Add("CategoryName, varchar(50) NOT NULL");
                TableColumns.Add("Description, varchar(200) NOT NULL");
                TableColumns.Add("Active, tinyint(4) NOT NULL DEFAULT '1'");
                TableColumns.Add("PRIMARY KEY, CategoryID");
                ObjMySQLHelper.CreateTable("ProductCategoryMaster", TableColumns);
                #endregion

                #region Create ProductInventory Table
                TableColumns.Clear();
                TableColumns.Add("ProductInvID, smallint(5) unsigned NOT NULL AUTO_INCREMENT");
                TableColumns.Add("StockName, varchar(50) NOT NULL");
                TableColumns.Add("Inventory, float NOT NULL");
                TableColumns.Add("Units, smallint(5) DEFAULT NULL");
                TableColumns.Add("UnitsOfMeasurement, varchar(10) DEFAULT NULL");
                TableColumns.Add("ReOrderStockLevel, float DEFAULT NULL");
                TableColumns.Add("ReOrderStockQty, float DEFAULT NULL");
                TableColumns.Add("LastPODate, datetime DEFAULT NULL");
                TableColumns.Add("LastUpdateDate, datetime DEFAULT NULL");
                TableColumns.Add("PRIMARY KEY, ProductInvID");
                ObjMySQLHelper.CreateTable("ProductInventory", TableColumns);
                #endregion

                #region Create TaxMaster Table
                TableColumns.Clear();
                TableColumns.Add("TaxID, smallint(5) unsigned NOT NULL AUTO_INCREMENT");
                TableColumns.Add("HSNCode, varchar(20) NOT NULL");
                TableColumns.Add("CGST, float DEFAULT NULL");
                TableColumns.Add("SGST, float DEFAULT NULL");
                TableColumns.Add("IGST, float DEFAULT NULL");
                TableColumns.Add("PRIMARY KEY, TaxID");
                ObjMySQLHelper.CreateTable("TaxMaster", TableColumns);
                #endregion

                #region Create PriceGroupMaster Table
                TableColumns.Clear();
                TableColumns.Add("PriceGroupID, smallint(5) unsigned NOT NULL AUTO_INCREMENT");
                TableColumns.Add("PriceGroupName, varchar(50) NOT NULL");
                TableColumns.Add("Description, varchar(200) DEFAULT NULL");
                TableColumns.Add("PriceColumn, varchar(50) NOT NULL");
                TableColumns.Add("Discount, float DEFAULT NULL");
                TableColumns.Add("DiscountType, varchar(50) DEFAULT NULL");
                TableColumns.Add("Default, tinyint(4) NOT NULL DEFAULT '1'");
                TableColumns.Add("PRIMARY KEY, PriceGroupID");
                ObjMySQLHelper.CreateTable("PriceGroupMaster", TableColumns);
                #endregion
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("RunDBScript.CreateProductMasterTables()", ex);
                throw ex;
            }
        }
    }
}
