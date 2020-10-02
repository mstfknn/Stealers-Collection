namespace PEngine.Main
{
    using PEngine.Helpers;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Ccleaner
    {
        public static void DeltaLogs(string NameFolder, bool True = true) => CombineEx.DeleteDir(CombineEx.CombinationEx(GlobalPath.User_Name, NameFolder), True);

        public static void CheckDir(bool tr)
        {
            try
            {
                if (File.Exists(GlobalPath.ZipAdd))
                {
                    CombineEx.DeleteDir(GlobalPath.User_Name, tr);
                }
            }
            catch { }
        }

        public static void CheckIsNullDirsAndFiles(string dir)
        {
            try
            {
                foreach (string d in Directory.EnumerateDirectories(dir))
                {
                    CheckIsNullDirsAndFiles(d);
                }

                foreach (string f in Directory.EnumerateFiles(dir))
                {
                    try
                    {
                        if (new FileInfo(f).Length == 0)
                        {
                            CombineEx.DeleteFile(f);
                        }
                    }
                    catch (UnauthorizedAccessException) { }
                    catch (IOException) { }
                }

                if (!Directory.EnumerateFileSystemEntries(dir).Any())
                {
                    CombineEx.DeleteDir(dir);
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (IOException) { }
        }

        private static readonly List<string> dllnames = new List<string>()
        {
            "System.Data.SQLite.dll",
            "Newtonsoft.Json.dll",
            "Ionic.Zip.dll",
            @"x86\SQLite.Interop.dll",
            @"x64\SQLite.Interop.dll"
        };

        public static void ClearDll()
        {
            for (int i = 0; i < dllnames.Count; i++)
            {
                CombineEx.DeleteFile(string.Concat(GlobalPath.GarbageTemp, dllnames[i]));
            }
        }
    }
}