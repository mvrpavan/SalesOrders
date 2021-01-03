using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrdersReport
{
    class RunDBScript
    {
        MySQLHelper tmpMySQLHelper;

        public void CreateNecessaryTables()
        {
            try
            {
                tmpMySQLHelper = MySQLHelper.GetMySqlHelperObj();
                CreatePrivilegeTable();
                CreatePrivilegeControlTable();
                CreateRoleTable();
                CreateStoreTable();
                CreateUserTable();
                //State
                CreateStateTable();
                //line
                CreateLineTable();
                //discount
                CreateDiscountGrpTable();
                //price
                CreatePriceGrpTable();
                //Customer
                CreateCustomerTable();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("RunDBScript.CreateNecessaryTables()", ex);
                throw;
            }
        }

        private void CreateStateTable()
        {
            try
            {
                List<string> ListStateCol = new List<string>() { "STATEID,INT NOT NULL AUTO_INCREMENT", "STATE,VARCHAR(50)", "STATECODE,VARCHAR(4)", "PRIMARY KEY,STATEID" };
                tmpMySQLHelper.CreateTable("STATEMASTER", ListStateCol);
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
                tmpMySQLHelper.CreateTable("LINEMASTER", ListLineCol);
                CommonFunctions.ObjCustomerMasterModel.CreateNewLine("Line0", "Super Line");
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
                tmpMySQLHelper.CreateTable("DISCOUNTGROUPMASTER", ListDisGrpCol);
                //List<string> ListColumnNamesWthDataType = new List<string> { "DISCOUNT,DECIMAL(4,2)", "DEFAULT,TINYTEXT", "DISCOUNTTYPE,VARCHAR", }, ListColumnValues = new List<string>() { "0", "YES", "ABSOLUTE" };
                //CommonFunctions.ObjCustomerMasterModel.CreateNewDiscountGrp("DisGrp1", "Super Dis Grp", ListColumnNamesWthDataType, ListColumnValues);
                CommonFunctions.ObjCustomerMasterModel.CreateNewDiscountGrp("DisGrp1", "Super Dis Grp");
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
               
                tmpMySQLHelper.CreateTable("PRICEGROUPMASTER", ListPriceGrpCol);
                //List<string> ListColumnNamesWthDataType = new List<string> { "DISCOUNT,DECIMAL(4,2)", "DEFAULT,TINYTEXT", "DISCOUNTTYPE,VARCHAR", }, ListColumnValues = new List<string>() { "0", "YES", "ABSOLUTE" };
                //CommonFunctions.ObjCustomerMasterModel.CreateNewDiscountGrp("DisGrp1", "Super Dis Grp", ListColumnNamesWthDataType, ListColumnValues);
                CommonFunctions.ObjCustomerMasterModel.CreateNewPriceGrp("PriceGrp1", "Super Price Grp");
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
                tmpMySQLHelper.CreateTable("CUSTOMERMASTER", ListCustomerCol);
                List<string> ListColumnNamesWthDataType = new List<string> { "LASTUPDATEDATE,DATETIME", "ADDEDDATE,DATETIME" }, ListColumnValues = new List<string>() { DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") };
                CommonFunctions.ObjCustomerMasterModel.CreateNewCustomer("customer0", "Line0", "DisGrp1", "PriceGrp1", true, ListColumnNamesWthDataType, ListColumnValues);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("RunDBScript.CreateCustomerTable()", ex);
                throw;
            }
        }

        private void CreateUserTable()
        {
            try
            {
                List<string> ListUserCol = new List<string>() { "USERID,INT NOT NULL AUTO_INCREMENT", "USERNAME,VARCHAR(100) NOT NULL", "PASSWORD,VARCHAR(100) NOT NULL", "FULLNAME,VARCHAR(100) NOT NULL", "ROLEID,INT NOT NULL", "EMAILID,VARCHAR(50) NULL", "PHONENO, BIGINT NULL", "STOREID, INT NULL DEFAULT NULL", "LASTLOGIN,DATETIME NULL", "LASTPASSWORDCHANGED,DATETIME NULL", "LASTUPDATEDATE,DATETIME NULL", "CREATEDBY, INT NULL DEFAULT 0", "ACTIVE,BIT NULL", "USERGUID,CHAR(38) NULL", "PRIMARY KEY,USERID" };
                tmpMySQLHelper.CreateTable("USERMASTER", ListUserCol);
                List<string> ListColumnNamesWthDataType = new List<string> { "LASTLOGIN,DATETIME", "CREATEDBY,INT" }, ListColumnValues = new List<string>() { DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"), "0" };
                CommonFunctions.ObjUserMasterModel.CreateNewUser("admin", "admin", "Administrator", true, "admin", ListColumnNamesWthDataType, ListColumnValues);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("RunDBScript.CreateUserTable()", ex);
                throw;
            }
        }

        private void CreateRoleTable()
        {
            try
            {           

                List<string> ListRoleCol = new List<string>() { "ROLEID,INT NOT NULL AUTO_INCREMENT", "ROLENAME,VARCHAR(50)", "DESCRIPTION,VARCHAR(50)", "PRIMARY KEY,ROLEID" };
                tmpMySQLHelper.CreateTable("ROLEMASTER", ListRoleCol);
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
                throw;
            }
        }

        private void CreateStoreTable()
        {
            try
            {
                List<string> ListStoreCol = new List<string>() { "STOREID,INT AUTO_INCREMENT NOT NULL", "STORENAME,VARCHAR(50) NOT NULL DEFAULT 1", "ADDRESS,VARCHAR(100) NULL", "PHONENO,BIGINT(20) NULL DEFAULT NULL", "STOREEXECUTIVE,VARCHAR(50) NULL", "PRIMARY KEY,STOREID" };
                tmpMySQLHelper.CreateTable("STOREMASTER", ListStoreCol);
                CommonFunctions.ObjUserMasterModel.CreateNewStore("Store1");
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("RunDBScript.CreateStoreTable()", ex);
                throw;
            }
        }

        private void CreatePrivilegeTable()
        {
            try
            {
                List<string> ListPrivilegeCol = new List<string>() { "PRIVILEGEID,VARCHAR(20) NOT NULL", "PRIVILEGENAME,VARCHAR(100)", "PRIMARY KEY,PRIVILEGEID" };
                tmpMySQLHelper.CreateTable("PRIVILEGEMASTER", ListPrivilegeCol);

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
                throw;
            }
        }

        private void CreatePrivilegeControlTable()
        {
            try
            {
                List<string> ListPrivilegeControlCol = new List<string>() { "CONTROLID,INT NOT NULL AUTO_INCREMENT", "FORMNAME,VARCHAR(100)", "CONTROLNAME,VARCHAR(100)", "ENABLED,BIT DEFAULT 1", "PRIVILEGEID,TINYTEXT", "PRIMARY KEY,CONTROLID" };
                tmpMySQLHelper.CreateTable("PRIVILEGECONTROLMASTER", ListPrivilegeControlCol);


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
                throw;
            }
        }
    }
}
