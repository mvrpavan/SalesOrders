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
                List<string> ListUserCol = new List<string>() { "USERID", "USERNAME", "PASSWORD", "ROLEID", "LASTLOGIN", "LASTPASSWORDCHANGED", "ACTIVE", "PHONENO", "ADDRESS" };
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
                List<string> ListRoleCol = new List<string>() {"ROLEID", "ROLENAME", "PRIVILEGE" };
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
