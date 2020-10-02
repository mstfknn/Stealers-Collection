namespace PEngine.Sticks
{
    using PEngine.Helpers;
    using System;
    using System.Diagnostics;
    using System.Management;
    using System.Windows.Forms;

    internal class AntiVM
    {
        private static bool GetDetectVirtualMachine()
        {
            using (ManagementObjectCollection mob = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem").Get())
            {
                foreach (ManagementBaseObject xa in mob)
                {
                    try
                    {
                        string str = xa["Manufacturer"].ToString().ToLower();
                        bool strTo = xa["Model"].ToString().ToUpperInvariant().Contains("VIRTUAL");
                        if ((str.Equals("microsoft corporation") && strTo) || str.Contains("vmware") || xa["Model"].ToString().Equals("VirtualBox"))
                        {
                            return true;
                        }
                    }
                    catch (ArgumentNullException) { return false; }
                }
            }
            return false;
        }
        private static bool IsRdpAvailable => SystemInformation.TerminalServerSession == true ? true : false;
        private static bool SBieDLL() => Process.GetProcessesByName("wsnm").Length <= 0 && NativeMethods.GetModuleHandle("SbieDll.dll").ToInt32() == 0 ? false : true;
        public static bool GetCheckVMBot() => SBieDLL() || IsRdpAvailable || GetDetectVirtualMachine() ? true : false;
    }
}