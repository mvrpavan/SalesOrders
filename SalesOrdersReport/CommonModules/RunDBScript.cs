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
                CreateAccountsMasterTables();
                CreateVendorMasterTables();
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
                CreateOrderTables();
                CreateInvoiceTables();
                CreateTransactionTables();
                CreateInventoryTables();
                CreatePurchaseOrderInvoiceTables();
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

        private void CreateCustomerTable()
        {
            try
            {
                List<string> ListCustomerCol = new List<string>() { "CUSTOMERID,INT NOT NULL AUTO_INCREMENT", "CUSTOMERNAME,VARCHAR(100) NOT NULL", "ADDRESS,VARCHAR(1000) NULL", "PHONENO, BIGINT(20) NULL DEFAULT NULL", "LINEID,INT NULL DEFAULT NULL", "PRICEGROUPID,INT NULL DEFAULT NULL", "DISCOUNTGROUPID,INT NULL DEFAULT NULL", "GSTIN,VARCHAR(20) NULL", "STATEID, VARCHAR(5) NULL DEFAULT NULL", "ADDEDDATE,DATETIME NULL", "LASTUPDATEDATE,DATETIME NULL", "ACTIVE,BIT NULL", "ORDERDAYS, VARCHAR(20) NOT NULL DEFAULT '1'", "PRIMARY KEY,CUSTOMERID" };
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
                TableColumns.Add("ProductID, mediumint unsigned NOT NULL AUTO_INCREMENT");
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
                TableColumns.Add("ProductInvID, mediumint unsigned DEFAULT NULL");
                TableColumns.Add("VendorID, smallint unsigned DEFAULT NULL");
                TableColumns.Add("Active, tinyint(4) NOT NULL DEFAULT '1'");
                TableColumns.Add("AddedDate, timestamp NULL DEFAULT CURRENT_TIMESTAMP");
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
                TableColumns.Add("ProductInvID, int unsigned NOT NULL AUTO_INCREMENT");
                TableColumns.Add("StockName, varchar(50) NOT NULL");
                TableColumns.Add("Inventory, float NOT NULL");
                TableColumns.Add("Units, smallint(5) DEFAULT NULL");
                TableColumns.Add("UnitsOfMeasurement, varchar(10) DEFAULT NULL");
                TableColumns.Add("ReOrderStockLevel, float DEFAULT NULL");
                TableColumns.Add("ReOrderStockQty, float DEFAULT NULL");
                TableColumns.Add("LastPODate, datetime DEFAULT NULL");
                TableColumns.Add("LastUpdateDate, timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
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
                ObjMySQLHelper.CreateTable("PRICEGROUPMASTER", TableColumns);
                #endregion

                #region Create VendorMaster Table
                TableColumns.Clear();
                TableColumns.Add("VendorID, smallint(5) unsigned NOT NULL AUTO_INCREMENT");
                TableColumns.Add("VendorName, varchar(100) NOT NULL");
                TableColumns.Add("Address, varchar(100) NULL");
                TableColumns.Add("PhoneNo, varchar(20) NULL");
                TableColumns.Add("GSTIN, varchar(20) NULL");
                TableColumns.Add("StateID, smallint DEFAULT NULL");
                TableColumns.Add("Active, tinyint Not NULL DEFAULT '1'");
                TableColumns.Add("AddedDate, timestamp NULL DEFAULT CURRENT_TIMESTAMP");
                TableColumns.Add("LastUpdateDate, timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
                TableColumns.Add("PRIMARY KEY, VendorID");
                ObjMySQLHelper.CreateTable("VendorMaster", TableColumns);
                #endregion
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("RunDBScript.CreateProductMasterTables()", ex);
                throw ex;
            }
        }

        public void CreateInventoryTables()
        {
            try
            {
                List<String> TableColumns = new List<String>();

                #region Create ProductStockHistory Table
                TableColumns.Add("HistoryEntryID, int unsigned NOT NULL AUTO_INCREMENT");
                TableColumns.Add("ProductInvID, smallint unsigned NOT NULL");
                TableColumns.Add("Type, varchar(10) DEFAULT NULL");
                TableColumns.Add("OrderedQty, float DEFAULT NULL");
                TableColumns.Add("ReceivedQty, float DEFAULT NULL");
                TableColumns.Add("NetQty, float DEFAULT NULL");
                TableColumns.Add("PODate, datetime DEFAULT NULL");
                TableColumns.Add("UpdateDate, timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
                TableColumns.Add("PRIMARY KEY, HistoryEntryID");
                ObjMySQLHelper.CreateTable("ProductStockHistory", TableColumns);
                #endregion
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreateInventoryTables()", ex);
            }
        }

        public void CreatePurchaseOrderInvoiceTables()
        {
            try
            {
                #region Create PurchaseOrders Table
                List<String> TableColumns = new List<String>
                {
                    "PurchaseOrderID, smallint(5) unsigned NOT NULL AUTO_INCREMENT",
                    "PurchaseOrderNumber, varchar(20) NOT NULL",
                    "PurchaseOrderDate, datetime NOT NULL",
                    "VendorID, smallint(5) unsigned NULL",
                    "POItemCount, smallint(5) DEFAULT NULL",
                    "EstimateOrderAmount, float DEFAULT NULL",
                    "PurchaseOrderStatus, varchar(20) DEFAULT NULL",      //Placed/Completed/Cancelled/Void
                    "CreationDate, timestamp NULL DEFAULT CURRENT_TIMESTAMP",
                    "LastUpdatedDate, timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP",
                    "DateDelivered, datetime NULL",
                    "DateInvoiceCreated, datetime NULL",
                    "PRIMARY KEY, PurchaseOrderID"
                };
                ObjMySQLHelper.CreateTable("PurchaseOrders", TableColumns);

                TableColumns = new List<String>
                {
                    "PurchaseOrderItemID, smallint(5) unsigned NOT NULL AUTO_INCREMENT",
                    "PurchaseOrderID, smallint(5) unsigned NOT NULL",
                    "ProductInvID, smallint(5) NOT NULL",
                    "OrderedQty, float DEFAULT 0",
                    "ReceivedQty, float DEFAULT 0",
                    "Price, float DEFAULT NULL",
                    "POItemStatus, varchar(20) DEFAULT NULL",
                    "PRIMARY KEY, PurchaseOrderItemID"
                };
                ObjMySQLHelper.CreateTable("PurchaseOrderItems", TableColumns);
                #endregion
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreatePurchaseOrderInvoiceTables()", ex);
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

        public void CreateOrderTables()
        {
            try
            {
                List<String> TableColumns = new List<String>
                {
                    "OrderID, smallint(5) unsigned NOT NULL AUTO_INCREMENT",
                    "OrderNumber, varchar(20) NOT NULL",
                    "OrderDate, datetime NOT NULL",
                    "CustomerID, smallint(5) NOT NULL",
                    "OrderItemCount, smallint(5) DEFAULT 0",
                    "EstimateOrderAmount, float DEFAULT 0",
                    "OrderStatus, varchar(20) DEFAULT NULL",      //Placed/Completed/Cancelled/Void
                    "DateDelivered, datetime NULL",
                    "DateInvoiceCreated, datetime NULL",
                    "CreationDate, timestamp NULL DEFAULT CURRENT_TIMESTAMP",
                    "LastUpdatedDate, timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP",
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
                CommonFunctions.ShowErrorDialog($"{this}.CreateOrderTables()", ex);
            }
        }

        public void CreateInvoiceTables()
        {
            try
            {
                List<String> TableColumns = new List<String>
                {
                    "InvoiceID, smallint(5) unsigned NOT NULL AUTO_INCREMENT",
                    "InvoiceNumber, varchar(20) NOT NULL",
                    "InvoiceDate, datetime NOT NULL",
                    "CustomerID, Int unsigned NOT NULL",
                    "OrderID, bigint NOT NULL",
                    "InvoiceItemCount, smallint(5) DEFAULT 0",
                    "NetInvoiceAmount, float DEFAULT 0",
                    "InvoiceStatus, varchar(20) DEFAULT NULL",
                    "CreationDate, timestamp NULL DEFAULT CURRENT_TIMESTAMP",
                    "LastUpdatedDate, timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP",
                    "PRIMARY KEY, InvoiceID"
                };
                ObjMySQLHelper.CreateTable("Invoices", TableColumns);

                TableColumns = new List<String>
                {
                    "InvoiceItemID, smallint(5) unsigned NOT NULL AUTO_INCREMENT",
                    "InvoiceID, smallint(5) unsigned NOT NULL",
                    "ProductID, smallint(5) NOT NULL",
                    "OrderQty, float DEFAULT 0",
                    "SaleQty, float DEFAULT 0",
                    "Price, float DEFAULT 0",
                    "Discount, float DEFAULT 0",
                    "TaxableValue, float DEFAULT 0",
                    "CGST, float DEFAULT 0",
                    "SGST, float DEFAULT 0",
                    "IGST, float DEFAULT 0",
                    "NetTotal, float DEFAULT 0",
                    "InvoiceItemStatus, varchar(20) DEFAULT NULL",
                    "PRIMARY KEY, InvoiceItemID"
                };
                ObjMySQLHelper.CreateTable("InvoiceItems", TableColumns);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreateInvoiceTables()", ex);
            }
        }

        public void CreateAccountsMasterTables()
        {
            try
            {
                List<String> TableColumns = new List<String>
                {
                    "AccountID, mediumint unsigned NOT NULL AUTO_INCREMENT",
                    "CustomerID, mediumint unsigned NOT NULL",
                    "BalanceAmount, float DEFAULT 0",
                    "CreationDate, timestamp NULL DEFAULT CURRENT_TIMESTAMP",
                    "LastUpdatedDate, timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP",
                    "Active, tinyint(4) NOT NULL DEFAULT '1'",
                    "PRIMARY KEY, AccountID"
                };
                ObjMySQLHelper.CreateTable("ACCOUNTSMASTER", TableColumns);

                //TableColumns = new List<String>
                //{
                //    "TransactionTypeID, smallint(5) unsigned NOT NULL AUTO_INCREMENT",
                //    "TransactionType, varchar(20) NOT NULL",
                //    "Description, varchar(100) NULL",
                //    "PRIMARY KEY, TransactionTypeID"
                //};
                //ObjMySQLHelper.CreateTable("TransactionTypeMaster", TableColumns);

                TableColumns = new List<String>
                {
                    "PaymentModeID, smallint(5) unsigned NOT NULL AUTO_INCREMENT",
                    "PaymentMode, varchar(20) NOT NULL",
                    "Description, varchar(100) NULL",
                    "PRIMARY KEY, PaymentModeID"
                };
                ObjMySQLHelper.CreateTable("PaymentModeMaster", TableColumns);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreateAccountsMasterTables()", ex);
            }
        }

        public void CreateTransactionTables()
        {
            try
            {
                List<String> TableColumns = new List<String>
                {
                    "PaymentID, BIGINT unsigned NOT NULL AUTO_INCREMENT",
                    "PaymentDate, datetime NOT NULL",
                    "InvoiceID, BIGINT unsigned NULL",
                    "QuotationID, BIGINT unsigned NULL",
                    "AccountID, mediumint unsigned not NULL",
                    "PaymentModeID, smallint unsigned not NULL",
                    "PaymentAmount, float DEFAULT 0",
                    "Description, varchar(100) NULL",
                    "CreationDate, timestamp NULL DEFAULT CURRENT_TIMESTAMP",
                    "LastUpdateDate, timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP",
                    "UserID, smallint unsigned NULL",
                    "Active, tinyint(4) NOT NULL DEFAULT '1'", 
                    "PRIMARY KEY, PaymentID"
                };
                ObjMySQLHelper.CreateTable("PAYMENTS", TableColumns);

                TableColumns = new List<String>
                {
                    "ExpenseID, BIGINT unsigned NOT NULL AUTO_INCREMENT",
                    "PaymentDate, datetime NOT NULL",
                    "PaymentModeID, smallint unsigned not NULL",
                    "PaymentAmount, float DEFAULT 0",
                    "Description, varchar(100) NULL",
                    "CreationDate, timestamp NULL DEFAULT CURRENT_TIMESTAMP",
                    "LastUpdateDate, timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP",
                    "UserID, smallint unsigned NULL",
                    "PRIMARY KEY, ExpenseID"
                };
                ObjMySQLHelper.CreateTable("EXPENSES", TableColumns);


                TableColumns = new List<String>
                {
                    "HISTORYENTRYID, BIGINT UNSIGNED NOT NULL AUTO_INCREMENT",
                    "ACCOUNTID, MEDIUMINT UNSIGNED NOT NULL",
                    "PAYMENTID, BIGINT UNSIGNED NOT NULL",
                    "SALEAMOUNT, FLOAT DEFAULT 0",
                    "CANCELAMOUNT, FLOAT DEFAULT 0",
                    "RETURNAMOUNT, FLOAT DEFAULT 0",
                    "DISCOUNTAMOUNT, FLOAT DEFAULT 0",
                    "TOTALTAX, FLOAT DEFAULT 0",
                    "NETSALEAMOUNT, FLOAT DEFAULT 0",
                    "BALANCEAMOUNT, FLOAT DEFAULT 0",
                    "AMOUNTRECEIVED, FLOAT DEFAULT 0",
                    "NEWBALANCEAMOUNT, FLOAT DEFAULT 0",
                    "PRIMARY KEY, HISTORYENTRYID"
                };
                ObjMySQLHelper.CreateTable("CUSTOMERACCOUNTHISTORY", TableColumns);

                //TableColumns = new List<String>
                //{
                //    "UserTransactionID, BIGINT unsigned NOT NULL AUTO_INCREMENT",
                //    "TransactionDate, datetime NOT NULL",
                //    "UserID, Smallint unsigned NULL",
                //    "Description, varchar(100) NULL",
                //    "PRIMARY KEY, CashTransactionID"
                //};
                //ObjMySQLHelper.CreateTable("UserTransactions", TableColumns);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreateTransactionTables()", ex);
            }
        }

        public void CreateVendorMasterTables()
        {
            try
            {
                List<String> TableColumns = new List<String>
                {
                    "VendorID, SmallInt unsigned NOT NULL AUTO_INCREMENT",
                    "VendorName, Varchar(50) NOT NULL",
                    "Address, Varchar(1000) NULL",
                    "PhoneNo, Varchar(20) NULL",
                    "GSTIN, Varchar(20) NULL",
                    "StateID, SmallInt unsigned not NULL",
                    "PriceGroupID, SmallInt unsigned NULL",
                    "DiscountGroupID, SmallInt unsigned NULL",
                    "Active, tinyint(4) NOT NULL DEFAULT '1'",
                    "AddedDate, timestamp NULL DEFAULT CURRENT_TIMESTAMP",
                    "LastUpdateDate, timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP",
                    "PRIMARY KEY, VendorID"
                };
                ObjMySQLHelper.CreateTable("VendorMaster", TableColumns);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog($"{this}.CreateVendorMasterTables()", ex);
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
