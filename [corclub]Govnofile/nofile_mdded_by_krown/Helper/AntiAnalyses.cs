namespace who.Helper
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Management;
    using System.Runtime.InteropServices;

    class AntiAnalyses
    {
        public static string[] Processes = new string[]
            {
                "HttpAnalyzer", "Dumper", "Reflector", "Wireshark", "WPE",
                "ProcessExplorer", "IDA", "HTTP Debugger Pro", "The Wireshark Network Analyzer", "WinDbg", "Colasoft Capsa",
                "smsniff", "Olly", "OllyDbg", "WPE PRO", "Microsoft Network Monitor", "Fiddler",
                "SmartSniff", "Immunity Debugger" , "Process Explorer" , "PE Tools","AQtime", "DS-5 Debug", "Dbxtool",
                "Topaz", "FusionDebug", "NetBeans", "Rational Purify", ".NET Reflector", "Cheat Engine", "Sigma Engine"
            };


        public static void Proc()
        {
            foreach (var _ in Processes.Where(ProcName => Process.GetProcesses().Any(x => x.ProcessName.ToLower().Contains(ProcName))).Select(ProcName => new { }))
            {
                Environment.Exit(0);
            }
        }
        
        public static void VMDetect() // VM
        {
            using (var searcher = new ManagementObjectSearcher("Select * from Win32_ComputerSystem"))
            {
                using (ManagementObjectCollection items = searcher.Get())
                {
                    foreach (var _ in from ManagementBaseObject item in items
                                      let manufacturer = item["Manufacturer"].ToString().ToLower()
                                      where (manufacturer == "microsoft corporation" && item["Model"].ToString().ToUpperInvariant().Contains("VIRTUAL"))
|| manufacturer.Contains("vmware")
|| item["Model"].ToString() == "VirtualBox"
                                      select new { })
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }

        public static void SandboxieDetect()
        {
            if (GetModuleHandle("SbieDll.dll").ToInt32() != 0)
            {
                Environment.Exit(0);
            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, ref bool isDebuggerPresent);
    }
}