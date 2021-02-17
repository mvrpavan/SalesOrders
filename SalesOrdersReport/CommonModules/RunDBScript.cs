using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrdersReport.CommonModules
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

        public void CreateMasterTables()
        {
            try
            {
                CreatePrivilegeTable();
                CreatePrivilegeControlTable();
                CreateRoleTable();
				CreateStoreTable();
                CreateProductMasterTables();
                CreateStateTable();
                CreateLineTable();
                CreateDiscountGrpTable();
                CreateCustomerTable();
                CreateIDValueMasterTable();
                CreateUserTable();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("RunDBScript.CreateMasterTables()", ex);
                throw ex;
            }
        }

        public void CreateRunningTables()
        {
            try
            {
                CreateAllOrderRelatedTable();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("RunDBScript.CreateRunningTables()", ex);
                throw ex;
            }
        }

        private void CreateStateTable()
        {
            try
            {
                List<string> ListStateCol = new List<string>() { "STATEID,INT NOT NULL AUTO_INCREMENT", "STATE,VARCHAR(50)", "STATECODE,VARCHAR(4)", "PRIMARY KEY,STATEID" };
                ObjMySQLHelper.CreateTable("STATEMASTER", ListStateCol);
                CommonFunctions.ObjCustomerMasterModel.AddAllStatesToMasterTable();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("RunDBScript.CreateStateTable()", ex);
                throw;
            }
        }

        private void CreateLineTable()
        {
            try
            {
                List<string> ListLineCol = new List<string>() { "LINEID,INT NOT NULL AUTO_INCREMENT", "LINENAME,VARCHAR(50)", "DESCRIPTION,VARCHAR(50)", "PRIMARY KEY,LINEID" };
                ObjMySQLHelper.CreateTable("LINEMASTER", ListLineCol);
                //CommonFunctions.ObjCustomerMasterModel.CreateNewLine("Line0", "Super Line");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("RunDBScript.CreateLineTable()", ex);
                throw;
            }
        }

        private void CreateDiscountGrpTable()
        {
            try
            {
                List<string> ListDisGrpCol = new List<string>() { "DISCOUNTGROUPID,INT NOT NULL AUTO_INCREMENT", "DISCOUNTGROUPNAME,VARCHAR(100) NOT NULL", "DESCRIPTION,VARCHAR(50)", "DISCOUNT,DECIMAL(4,2) DEFAULT 0", "ISDEFAULT,BIT DEFAULT NULL", "DISCOUNTTYPE,VARCHAR(10) DEFAULT 'ABSOLUTE'", "PRIMARY KEY,DISCOUNTGROUPID" };
                ObjMySQLHelper.CreateTable("DISCOUNTGROUPMASTER", ListDisGrpCol);
                ////List<string> ListColumnNamesWthDataType = new List<string> { "DISCOUNT,DECIMAL(4,2)", "DEFAULT,TINYTEXT", "DISCOUNTTYPE,VARCHAR", }, ListColumnValues = new List<string>() { "0", "YES", "ABSOLUTE" };
                ////CommonFunctions.ObjCustomerMasterModel.CreateNewDiscountGrp("DisGrp1", "Super Dis Grp", ListColumnNamesWthDataType, ListColumnValues);
                //CommonFunctions.ObjCustomerMasterModel.CreateNewDiscountGrp("DisGrp1", "Super Dis Grp");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("RunDBScript.CreateDiscountGrpTable()", ex);
                throw;
            }
        }
        private void CreatePriceGrpTable()
        {
            try
            {
                List<string> ListPriceGrpCol = new List<string>() { "PRICEGROUPID,INT NOT NULL AUTO_INCREMENT", "PRICEGROUPNAME,VARCHAR(100) NOT NULL", "DESCRIPTION,VARCHAR(50)", "PRICEGROUPCOLUMNNAME, VARCHAR(30) DEFAULT  'Purchase Price'", "DISCOUNT,DECIMAL(4,2) DEFAULT 0", "ISDEFAULT,BIT DEFAULT NULL", "DISCOUNTTYPE,VARCHAR(10) DEFAULT 'ABSOLUTE'", "PRIMARY KEY,PRICEGROUPID" };
               
                ObjMySQLHelper.CreateTable("PRICEGROUPMASTER", ListPriceGrpCol);
                ////List<string> ListColumnNamesWthDataType = new List<string> { "DISCOUNT,DECIMAL(4,2)", "DEFAULT,TINYTEXT", "DISCOUNTTYPE,VARCHAR", }, ListColumnValues = new List<string>() { "0", "YES", "ABSOLUTE" };
                ////CommonFunctions.ObjCustomerMasterModel.CreateNewDiscountGrp("DisGrp1", "Super Dis Grp", ListColumnNamesWthDataType, ListColumnValues);
                //CommonFunctions.ObjCustomerMasterModel.CreateNewPriceGrp("PriceGrp1", "Super Price Grp");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("RunDBScript.CreatePriceGrpTable()", ex);
                throw;
            }
        }



//        ID AddedDate
//SellerName LastUpdateDate
//Address
//Phone
//GSTIN string

//PriceGroupID
//DiscountGroupID
//StateID
//LineID
//Active
//OrderDays CSV


        private void CreateCustomerTable()
        {
            try
            {
                List<string> ListCustomerCol = new List<string>() { "CUSTOMERID,INT NOT NULL AUTO_INCREMENT", "CUSTOMERNAME,VARCHAR(100) NOT NULL", "ADDRESS,VARCHAR(100) NULL", "PHONENO, BIGINT(20) NULL DEFAULT NULL", "LINEID,INT NULL DEFAULT NULL", "PRICEGROUPID,INT NULL DEFAULT NULL", "DISCOUNTGROUPID,INT NULL DEFAULT NULL", "GSTIN,VARCHAR(20) NULL", "STATEID, VARCHAR(5) NULL DEFAULT NULL", "ADDEDDATE,DATETIME NULL", "LASTUPDATEDATE,DATETIME NULL", "ACTIVE,BIT NULL", "ORDERDAYS, VARCHAR(20) NOT NULL DEFAULT '1'", "PRIMARY KEY,CUSTOMERID" };
                ObjMySQLHelper.CreateTable("CUSTOMERMASTER", ListCustomerCol);
                List<string> ListColumnNamesWthDataType = new List<string> { "LASTUPDATEDATE,DATETIME", "ADDEDDATE,DATETIME" }, ListColumnValues = new List<string>() { DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") };
               //CommonFunctions.ObjCustomerMasterModel.CreateNewCustomer("customer0", "Line0", "DisGrp1", "PriceGrp1", true, ListColumnNamesWthDataType, ListColumnValues);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("RunDBScript.CreateCustomerTable()", ex);
                throw;
            }
        }

        void CreateUserTable()
        {
            try
            {
                List<string> ListUserCol = new List<string>() { "USERID,INT NOT NULL AUTO_INCREMENT", "USERNAME,VARCHAR(100) NOT NULL", "PASSWORD,VARCHAR(100) NOT NULL", "FULLNAME,VARCHAR(100) NOT NULL", "ROLEID,INT NOT NULL", "EMAILID,VARCHAR(50) NULL", "PHONENO, BIGINT NULL", "STOREID, INT NULL DEFAULT NULL", "LASTLOGIN,DATETIME NULL", "LASTPASSWORDCHANGED,DATETIME NULL", "LASTUPDATEDATE,DATETIME NULL", "CREATEDBY, INT NULL DEFAULT 0", "ACTIVE,BIT NULL", "USERGUID,CHAR(38) NULL", "PRIMARY KEY,USERID" };
                ObjMySQLHelper.CreateTable("USERMASTER", ListUserCol);
				List<string> ListColumnNamesWthDataType = new List<string> { "LASTLOGIN,DATETIME", "CREATEDBY,INT" }, ListColumnValues = new List<string>() { DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"), "0" };
                CommonFunctions.ObjUserMasterModel.CreateNewUser("admin", "admin", "Administrator", true, "admin", ListColumnNamesWthDataType, ListColumnValues);
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

                List<string> ListRoleCol = new List<string>() { "ROLEID,INT NOT NULL AUTO_INCREMENT", "ROLENAME,VARCHAR(50)", "DESCRIPTION,VARCHAR(50)", "PRIMARY KEY,ROLEID" };
                ObjMySQLHelper.CreateTable("ROLEMASTER", ListRoleCol);
                CommonFunctions.ObjUserMasterModel.AlterTblColBasedOnMultipleRowsFrmAnotherTbl("PRIVILEGEMASTER", "ROLEMASTER", "PRIVILEGEID");
                List<string> ListColumnValues = new List<string>();
                List<string> ListColumnNamesWithDataType = new List<string>();
                List<string> ListTemp = CommonFunctions.ObjUserMasterModel.GetAllPrivilegeIDs();
                for (int i = 0; i < ListTemp.Count; i++)
                {
                    ListColumnValues.Add("YES");
                    ListColumnNamesWithDataType.Add(ListTemp[i] + ", TINYTEXT");
                }

                CommonFunctions.ObjUserMasterModel.CreateNewRole("admin", "Super User", ListColumnNamesWithDataType, ListColumnValues);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("RunDBScript.CreateRoleTable()", ex);
                throw ex;
            }
        }

        void CreateStoreTable()
        {
            try
            {
                List<string> ListStoreCol = new List<string>() { "STOREID,INT AUTO_INCREMENT NOT NULL", "STORENAME,VARCHAR(50) NOT NULL DEFAULT 1", "ADDRESS,VARCHAR(100) NULL", "PHONENO,BIGINT(20) NULL DEFAULT NULL", "STOREEXECUTIVE,VARCHAR(50) NULL", "PRIMARY KEY,STOREID" };
                ObjMySQLHelper.CreateTable("STOREMASTER", ListStoreCol);
               // CommonFunctions.ObjUserMasterModel.CreateNewStore("Store1");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("RunDBScript.CreateStoreTable()", ex);
                throw ex;
            }
        }

        void CreatePrivilegeTable()
        {
            try
            {
                List<string> ListPrivilegeCol = new List<string>() { "PRIVILEGEID,VARCHAR(20) NOT NULL", "PRIVILEGENAME,VARCHAR(100)", "PRIMARY KEY,PRIVILEGEID" };
                ObjMySQLHelper.CreateTable("PRIVILEGEMASTER", ListPrivilegeCol);

                CommonFunctions.ObjUserMasterModel.CreateNewPrivilege("P_1", "CreateUsers");
                CommonFunctions.ObjUserMasterModel.CreateNewPrivilege("P_2", "EditUsers");
                CommonFunctions.ObjUserMasterModel.CreateNewPrivilege("P_3", "CreateRole");
                CommonFunctions.ObjUserMasterModel.CreateNewPrivilege("P_4", "DefineRole");
                CommonFunctions.ObjUserMasterModel.CreateNewPrivilege("P_5", "DeleteUsers");
                CommonFunctions.ObjUserMasterModel.CreateNewPrivilege("P_6", "CreateStore");
                CommonFunctions.ObjUserMasterModel.CreateNewPrivilege("P_7", "EditStore");
                CommonFunctions.ObjUserMasterModel.CreateNewPrivilege("P_8", "CreateOrder");
                CommonFunctions.ObjUserMasterModel.CreateNewPrivilege("P_9", "CreateProducts");
                CommonFunctions.ObjUserMasterModel.CreateNewPrivilege("P_10", "ModifyProducts");
                CommonFunctions.ObjUserMasterModel.CreateNewPrivilege("P_11", "CreateInvoice");
                CommonFunctions.ObjUserMasterModel.CreateNewPrivilege("P_12", "DeleteInvoice");
                CommonFunctions.ObjUserMasterModel.CreateNewPrivilege("P_13", "DeleteProducts");
                CommonFunctions.ObjUserMasterModel.CreateNewPrivilege("P_14", "CancelOrder");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("RunDBScript.CreatePrivilegeTable()", ex);
                throw ex;
            }
        }

        void CreatePrivilegeControlTable()
        {
            try
            {
                List<string> ListPrivilegeControlCol = new List<string>() { "CONTROLID,INT NOT NULL AUTO_INCREMENT", "FORMNAME,VARCHAR(100)", "CONTROLNAME,VARCHAR(100)", "ENABLED,BIT DEFAULT 1", "PRIVILEGEID,TINYTEXT", "PRIMARY KEY,CONTROLID" };
                ObjMySQLHelper.CreateTable("PRIVILEGECONTROLMASTER", ListPrivilegeControlCol);

                CommonFunctions.ObjUserMasterModel.CreateNewPrivilegeControl("ManageUsersForm", "btnRedirectCreateUser",true, "P_1");
                CommonFunctions.ObjUserMasterModel.CreateNewPrivilegeControl("ManageUsersForm", "btnRedirectEditUser", true, "P_2");
                CommonFunctions.ObjUserMasterModel.CreateNewPrivilegeControl("ManageUsersForm", "btnRedirectCreateRole", true, "P_3");
                CommonFunctions.ObjUserMasterModel.CreateNewPrivilegeControl("ManageUsersForm", "btnDefineRole", true, "P_4");
                CommonFunctions.ObjUserMasterModel.CreateNewPrivilegeControl("ManageUsersForm", "btnRedirectDeleteUser", true, "P_5");
                CommonFunctions.ObjUserMasterModel.CreateNewPrivilegeControl("ManageUsersForm", "btnCreateStore", true, "P_6");
                CommonFunctions.ObjUserMasterModel.CreateNewPrivilegeControl("ManageUsersForm", "btnEditStore", true, "P_7");
                
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("RunDBScript.CreatePrivilegeControlTable()", ex);
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
                TableColumns.Add("PurchasePrice, float DEFAULT NULL");
                TableColumns.Add("WholesalePrice, float DEFAULT NULL");
                TableColumns.Add("RetailPrice, float DEFAULT NULL");
                TableColumns.Add("MaxRetailPrice, float DEFAULT NULL");
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
                TableColumns.Add("IsDefault, tinyint(4) NOT NULL DEFAULT '1'");
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

        public void CreateIDValueMasterTable()
        {
            try
            {
                List<String> TableColumns = new List<String>();
                TableColumns.Add("TableName, varchar(50) NOT NULL");
                TableColumns.Add("IDValue, varchar(200) NOT NULL");
                ObjMySQLHelper.CreateTable("IDValueMaster", TableColumns);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("RunDBScript.CreateIDValueMasterTable()", ex);
            }
        }

        public void CreateAllOrderRelatedTable()
        {
            try
            {
                List<String> TableColumns = new List<String>
                {
                    "OrderID, smallint(5) unsigned NOT NULL AUTO_INCREMENT",
                    "OrderNumber, varchar(20) NOT NULL",
                    "OrderDate, datetime NOT NULL",
                    "CreationDate, datetime NOT NULL",
                    "LastUpdatedDate, datetime NOT NULL",
                    "CustomerID, smallint(5) NOT NULL",
                    "EstimateOrderAmount, float DEFAULT NULL",
                    "OrderStatus, varchar(20) DEFAULT NULL",      //Placed/Completed/Cancelled/Void
                    "DateDelivered, datetime NOT NULL",
                    "DateInvoiceCreated, datetime NOT NULL",
                    "DateQuotationCreated, datetime NOT NULL",
                    "PRIMARY KEY, OrderID"
                };
                ObjMySQLHelper.CreateTable("Orders", TableColumns);

                TableColumns = new List<String>
                {
                    "OrderItemID, smallint(5) unsigned NOT NULL AUTO_INCREMENT",
                    "OrderID, smallint(5) unsigned NOT NULL",
                    "ProductID, smallint(5) NOT NULL",
                    "OrderQty, float DEFAULT NULL",
                    "Price, float DEFAULT NULL",
                    "OrderItemStatus, varchar(20) DEFAULT NULL",
                    "PRIMARY KEY, OrderItemID"
                };
                ObjMySQLHelper.CreateTable("OrderItems", TableColumns);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreateAllOrderRelatedTable()", ex);
            }
        }

        public void ExecuteOneTimeExecutionScript()
        {
            try
            {
                //Execute script files
                foreach (var file in CommonFunctions.ObjApplicationSettings.ListSQLScriptFiles)
                {
                    ObjMySQLHelper.ExecuteScriptFile(file);
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreateProductMasterTables()", ex);
            }
        }
    }
}
