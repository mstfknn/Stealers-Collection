using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace RecoverNordVPN
{
    class Program
    {
        static void Main(string[] args)
        {
            string commonAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            DirectoryInfo nordFolder = new DirectoryInfo(Path.Combine(commonAppDataFolder, "NordVPN"));

            if (!nordFolder.Exists)
            {
                Console.WriteLine("NordVPN directory not found!");
                return;
            }

            foreach (DirectoryInfo d in nordFolder.GetDirectories("NordVpn.exe*"))
            {
                Console.WriteLine($"Searching in {d.Name}");

                foreach (DirectoryInfo v in d.GetDirectories())
                {
                    Console.WriteLine($"\tFound version {v.Name}");

                    string userConfigPath = Path.Combine(v.FullName, "user.config");
                    if (File.Exists(userConfigPath))
                    {
                        var doc = new XmlDocument();
                        doc.Load(userConfigPath);

                        string encodedUsername = doc.SelectSingleNode("//setting[@name='Username']/value").InnerText;
                        string encodedPassword = doc.SelectSingleNode("//setting[@name='Password']/value").InnerText;

                        if(encodedUsername != null && !string.IsNullOrEmpty(encodedUsername))
                        {
                            Console.WriteLine($"\t\tUsername: {nordDecode(encodedUsername)}");
                        }

                        if (encodedPassword != null && !string.IsNullOrEmpty(encodedPassword))
                        {
                            Console.WriteLine($"\t\tPassword: {nordDecode(encodedPassword)}");
                        }
                    }
                }
            }
        }

        static string nordDecode(string s)
        {
            try
            {
                return Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(s), null, DataProtectionScope.LocalMachine));
            }
            catch
            {
                return "";
            }
        }
    }
}
