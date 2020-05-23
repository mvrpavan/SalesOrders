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
                CreateRoleTable();
                CreateUserTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CreateUserTable()
        {
            try
            {
                List<string> ListUserCol = new List<string>() { "USERID,int", "USERNAME,varchar(50)", "PASSWORD,varchar(50)", "ROLEID,int", "LASTLOGIN,datetime not Null", "LASTPASSWORDCHANGED,datetime not Null", "ACTIVE,tinyint(1)", "PHONENO,varchar(15)", "ADDRESS,varchar(100)" };
                tmpMySQLHelper.CreateTable("USERS", ListUserCol);
                tmpMySQLHelper.CreateNewUser("admin", "admin", "", "", "1");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void CreateRoleTable()
        {
            try
            {
                List<string> ListRoleCol = new List<string>() {"ROLEID,int", "ROLENAME,varchar(50)", "PRIVILEGE,varchar(100)" };
                tmpMySQLHelper.CreateTable("ROLE", ListRoleCol);
                tmpMySQLHelper.CreateNewRole("admin", "all");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
