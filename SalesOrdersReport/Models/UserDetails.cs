using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrdersReport
{
    class UserDetails : IComparer<UserDetails>
    {
        public string UserName = "", FullName = "", EmailID = "";
        public int UserID = -1, RoleID = -1, StoreID = -1, CreatedBy = -1;
        public DateTime LastLogin = DateTime.MinValue, LastPasswordChanged = DateTime.MinValue, LastUpdateDate = DateTime.MinValue;
        public bool Active = true;
        public string RoleName = "", StoreName = "";
        public Int64 PhoneNo = 0;
        public int Compare(UserDetails x, UserDetails y)
        {
            return x.UserName.ToUpper().CompareTo(y.UserName.ToUpper());
        }
    }
    class RoleDetails : IComparer<RoleDetails>
    {
        public int RoleID = -1;
        public string RoleName = "", RoleDescription = "";
        public List<bool> ListOfPrivilegesAssigned = new List<bool>();
        public int Compare(RoleDetails x, RoleDetails y)
        {
            return x.RoleName.ToUpper().CompareTo(y.RoleName.ToUpper());
        }
    }

    class PrivilegeControlDetails
    {
        public string FormName = "", ControlName = "";
        public bool IsEnabled = true;
    }

    class PrivilegeDetails : IComparer<PrivilegeDetails>
    {
        public string PrivilegeId = "";
        public string PrivilegeName = "";
        public List<PrivilegeControlDetails> ListPrivilegeControlDetails = new List<PrivilegeControlDetails>();
        public int Compare(PrivilegeDetails x, PrivilegeDetails y)
        {
            return x.PrivilegeName.ToUpper().CompareTo(y.PrivilegeName.ToUpper());
        }
    }
    class StoreDetails : IComparer<StoreDetails>
    {
        public int StoreId = -1;
        public string StoreName = "Select Store";
        public string Address = "";
        public Int64 PhoneNo = 0;
        public string StoreExecutive = "";
        public int Compare(StoreDetails x, StoreDetails y)
        {
            return x.StoreName.ToUpper().CompareTo(y.StoreName.ToUpper());
        }
    }
}
