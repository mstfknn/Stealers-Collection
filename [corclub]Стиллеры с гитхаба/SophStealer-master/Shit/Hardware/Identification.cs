using System;
using Microsoft.Win32;
using System.Collections.Generic;

namespace Soph.Hardware
{
    internal static class Identification
    {
        public static string GetId()
        {
            try
            {
                using (RegistryKey registryKey1 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                {
                    using (RegistryKey registryKey2 = registryKey1.OpenSubKey("SOFTWARE\\Microsoft\\Cryptography"))
                    {
                        if (registryKey2 == null)
                            throw new KeyNotFoundException(string.Format("Key Not Found: {0}", (object)"SOFTWARE\\Microsoft\\Cryptography"));
                        object obj = registryKey2.GetValue("MachineGuid");
                        if (obj == null)
                            throw new IndexOutOfRangeException(string.Format("Index Not Found: {0}", (object)"MachineGuid"));
                        return obj.ToString().ToUpper().Replace("-", string.Empty);
                    }
                }
            }
            catch
            {
                return "HWID not found";
            }
        }
    }
}
