using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace FileZillaRecover
{
    class Program
    {
        public static string[] strFilenames = { "sitemanager.xml", "recentservers.xml" };
        public static string strFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FileZilla");

        static void Read(string Path)
        {
            try
            {
                XmlDocument objXmlDocument = new XmlDocument();
                objXmlDocument.Load(Path);

                foreach (XmlNode objXmlNode in objXmlDocument.DocumentElement.ChildNodes[0].ChildNodes)
                {
                    Console.WriteLine("Host: " + objXmlNode.ChildNodes[0].InnerText);
                    Console.WriteLine("Port: " + objXmlNode.ChildNodes[1].InnerText);
                    Console.WriteLine("User: " + objXmlNode.ChildNodes[4].InnerText);
                    string encodedString = objXmlNode.ChildNodes[5].InnerText;
                    byte[] data = Convert.FromBase64String(encodedString);
                    string decodedString = Encoding.UTF8.GetString(data);
                    Console.WriteLine("Pass: " + decodedString);
                    Console.WriteLine("\n");
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        static void FZ()
        {
            foreach (string strFilename in strFilenames)
            {
                string strPath = Path.Combine(strFolderPath, strFilename);
                if (File.Exists(strPath))
                {
                    Read(strPath);
                }
            }
        }

        static void Main(string[] args)
        {
            FZ();
            Console.ReadKey();
        }
    }
}
