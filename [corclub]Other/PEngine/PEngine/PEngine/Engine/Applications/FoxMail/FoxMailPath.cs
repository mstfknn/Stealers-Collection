namespace PEngine.Engine.Applications.FoxMail
{
    using Microsoft.Win32;
    using System;

    public class FoxMailPath
    {
        public static string GetFoxMail(string FoxPath)
        {
            try
            {
                using (var Fox = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32))
                {
                    using (RegistryKey Key = Fox.OpenSubKey(FoxPath, Environment.Is64BitOperatingSystem ? true : false))
                    {
                        string set = Key?.GetValue("").ToString();
                        return $@"{set.Remove(set.LastIndexOf("Foxmail.exe")).Replace("\"", "")}Storage\";
                    }
                }
            }
            catch { return null; }
        }
    }
}