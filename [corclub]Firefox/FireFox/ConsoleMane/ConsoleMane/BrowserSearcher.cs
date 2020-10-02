using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using static ConsoleMane.Program;

namespace ConsoleMane

{
    class BrowserSearcher
    {
        public List<Browser> DetectBrowsers()
        {
            List<Browser> browsers = new List<Browser>();
            var filePath = GetPathToBrowsers();

            string[] directories = Directory.GetDirectories(filePath + @"/AppData/Local");

            fillBrowserList("Mozilla", Browser.Mozilla, browsers, directories);
            fillBrowserList("Opera", Browser.Opera, browsers, directories);
            fillBrowserList("Google", Browser.Chrome, browsers, directories);
            fillBrowserList("Yandex", Browser.Yandex, browsers, directories);
            fillBrowserList("MicrosoftEdge", Browser.Edge, browsers, directories);

            return browsers;
        }

        public static string GetPathToBrowsers()
        {
            string pathWithEnv = @"%USERPROFILE%";
            string filePath = Environment.ExpandEnvironmentVariables(pathWithEnv);
            return filePath;
        }

        private static void fillBrowserList(string name, Browser browser, List<Browser> browsers, string[] directories)
        {
            if (directories.Where(x => x.Contains(name)).Count() > 0)
            {
                browsers.Add(browser);
            }
        }

        private bool is64bit()
        {
            return Marshal.SizeOf(typeof(IntPtr)) == 8;
        }
    }
}