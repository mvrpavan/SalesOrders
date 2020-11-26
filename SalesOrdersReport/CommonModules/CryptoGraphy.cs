using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SalesOrdersReport.CommonModules
{
    class CryptoGraphy
    {
        public static string HashSHA1(string value)
        {
            try
            {
                var sha1 = SHA1.Create();
                var inputBytes = Encoding.ASCII.GetBytes(value);
                var hash = sha1.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                for (var i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowErrorDialog("CryptoGraphy.HashSHA1()", ex);
                throw;
            }
        }
    }
}
