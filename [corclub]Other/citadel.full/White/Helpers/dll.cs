using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;

namespace dLL
{
    class dll
    {
        public static void dLL(string url)
        {
            using (WebClient Client = new WebClient())
            {
                string path = Directory.GetCurrentDirectory();
                Client.DownloadFile(url, path + @"/ICSharpCode.SharpZipLib.dll");
            }

        }
    }
}
