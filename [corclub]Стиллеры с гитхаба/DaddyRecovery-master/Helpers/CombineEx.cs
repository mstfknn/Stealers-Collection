namespace DaddyRecovery.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public static class CombineEx
    {
        /// <summary>
        /// Метод для проверки директории на существование
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static bool ExistsDir(string dir) => Directory.Exists(dir);

        /// <summary>
        /// Метод для проверки файла на существование
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool ExistsFile(string file) => File.Exists(file);

        /// <summary>
        /// Метод для проверки директории и файла на существование
        /// </summary>
        /// <param name="pathdir"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string Inizialze(string pathdir, string filename) => ExistsDir(pathdir) || ExistsFile(filename) ? Path.Combine(pathdir, filename) : string.Empty;

        /// <summary>
        /// Метод для перемещения файла в другую папку
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void MoveExeFile(string from, string to)
        {
            if (!string.IsNullOrWhiteSpace(from) || !string.IsNullOrWhiteSpace(to))
            {
                try
                {
                    File.Move(from, to);
                }
                catch { }
            }
        }

        /// <summary>
        /// Метод для удаления определённого файла
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool DeleteFile(string filename)
        {
            if (ExistsFile(filename))
            {
                try
                {
                    File.Delete(filename);
                    return true;
                }
                catch { return false; }
            }
            return false;
        }

        /// <summary>
        /// Метод для копирования определённ(ого)ых файл(а)ов в новую директорию
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool FileCopy(string from, string to, bool status)
        {
            if (ExistsFile(from))
            {
                try
                {
                    File.Copy(from, to, status);
                    return true;
                }
                catch { return false; }
            }
            return false;
        }

        /// <summary>
        /// Метод для распаковки файла(ов) из ресурсов в указанную папку
        /// </summary>
        /// <param name="pathdir"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static bool UnpackFromResources(string pathdir, byte[] bytes)
        {
            try
            {
                File.WriteAllBytes(pathdir, bytes);
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Метод для скрытия указанного файла 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static bool HideFile(string path, FileAttributes attributes)
        {
            if (ExistsFile(path))
            {
                try
                {
                    File.SetAttributes(path, attributes);
                    return true;
                }
                catch { return false; }
            }
            return false;
        }

        /// <summary>
        /// Метод для проверки на скрытие файла(ов)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsHideOrNo(string path)
        {
            try
            {
                return File.GetAttributes(path).HasFlag(FileAttributes.Hidden) ? true : false;
            }
            catch { return false; }
        }

        /// <summary>
        /// Метод для получения сведения о каталоге
        /// </summary>
        /// <param name="dir">Путь к каталогу</param>
        /// <returns>Информация</returns>
        public static string GetDirName(string dir)
        {
            try
            {
                return Path.GetDirectoryName(dir);
            }
            catch { return string.Empty; }
        }

        /// <summary>
        /// Метод для получения имени файла по пути
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetFileName(string file)
        {
            try
            {
                return Path.GetFileName(file);
            }
            catch { return string.Empty; }
        }

        /// <summary>
        /// Метод для получения имени файла без расширения
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetFileNameWithoutExtension(string file)
        {
            try
            {
                return Path.GetFileNameWithoutExtension(file);
            }
            catch { return string.Empty; }
        }

        /// <summary>
        /// Метод для комбинирования путей через массив string[]
        /// </summary>
        /// <param name="paths">Путь комбинация</param>
        /// <returns></returns>
        public static string CombinePath(params string[] paths)
        {
            try
            {
                return paths?.Length > 0 ? Path.Combine(paths) : string.Empty;
            }
            catch { return string.Empty; }
        }

        /// <summary>
        /// Метод для создания файла
        /// </summary>
        /// <param name="path">Путь к новому файлу</param>
        /// <param name="content">Текст для сохранения</param>
        /// <returns></returns>
        public static bool CreateFile(bool writer, string path, string content)
        {
            if (writer)
            {
                try
                {
                    File.WriteAllText(path, content);
                    return true;
                }
                catch { return false; }
            }
            else
            {
                try
                {
                    File.AppendAllText(path, content);
                    return true;
                }
                catch { return false; }
            }
        }

        public static bool CreateLineFile(string path, List<string> content)
        {
            try
            {
                File.WriteAllLines(path, content);
                return true;
            }
            catch { return false; }
        }

        public static string ReaderText(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception) { return string.Empty; }
        }

        /// <summary>
        /// Метод для создания/удаление папк(и)ок
        /// </summary>
        /// <param name="create">True - Создать папку | False - Удалить папк(и)у</param>
        /// <param name="dir">Путь к папке</param>
        /// <param name="attributes">Аттрибуты для папки</param>
        public static void CreateOrDeleteDirectoryEx(bool create, string dir, FileAttributes attributes = FileAttributes.Hidden)
        {
            if (create) // Создать директорию
            {
                if (!ExistsDir(dir)) // Если директории не существует идём дальше...
                {
                    try
                    {
                        var DInfo = new DirectoryInfo(dir);
                        DInfo.Create(); // Создаём папку
                        DInfo.Refresh(); // Обновляем папку (чтобы появилась)
                        DInfo.Attributes = FileAttributes.Directory | attributes; // Задаём аттрибуты на папку
                    }
                    catch (Exception) { }
                }
            }
            else // Удалить директории
            {
                try
                {
                    // Проходимся циклом по папкам c файлами и удаляем их
                    foreach (string files in Directory.EnumerateFiles(dir))
                    {
                        DeleteFile(files);
                    }
                    foreach (string directory in Directory.EnumerateDirectories(dir))
                    {
                        CreateOrDeleteDirectoryEx(false, directory);
                    }
                    Directory.Delete(dir, true);
                }
                catch (Exception) { }
            }
        }
    }
}