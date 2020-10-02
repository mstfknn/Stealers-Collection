using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMane
{
    class Program
    {
        static void Main(string[] args)
        {
            BrowserSearcher finder = new BrowserSearcher();
            List<Browser> browsers = finder.DetectBrowsers();
            CrackerPasswords cracker = new CrackerPasswords();
            cracker.CollectPasswords(browsers);
            Console.WriteLine(String.Join(",", browsers.Select(x => x.ToString()).ToArray()));
            Console.ReadKey();
        }
        public enum Browser
        {
            Chrome = 0,
            IE = 1,
            Mozilla = 2,
            Safary = 3,
            Yandex = 4,
            Edge = 5,
            Opera = 6
        }
    
}
}
