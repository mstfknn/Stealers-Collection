// Cracked by Milfachs. t.me/milfachs or t.me/stroleyman.
// Telegram Channel - t.me/darkwanna

using System;
using System.Diagnostics;
using System.Management;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

internal class Class8
{
  private static bool smethod_0()
  {
    try
    {
      using (ManagementObjectCollection objectCollection = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem").Get())
      {
        foreach (ManagementBaseObject managementBaseObject in objectCollection)
        {
          try
          {
            string lower = managementBaseObject["Manufacturer"].ToString().ToLower();
            bool flag = managementBaseObject["Model"].ToString().ToLower().Contains("virtual");
            if (!(lower.Equals("microsoft corporation") & flag) && !lower.Contains("vmware"))
            {
              if (!managementBaseObject["Model"].ToString().Equals("VirtualBox"))
                continue;
            }
            return true;
          }
          catch (Exception ex)
          {
            return false;
          }
        }
      }
    }
    catch
    {
    }
    return false;
  }

  private static bool smethod_1(Process process_0)
  {
    try
    {
      bool bool_0 = false;
      Class8.CheckRemoteDebuggerPresent(process_0.Handle, ref bool_0);
      return bool_0;
    }
    catch (Exception ex)
    {
      return false;
    }
  }

  [SpecialName]
  private static bool smethod_2()
  {
    return SystemInformation.TerminalServerSession;
  }

  private static bool smethod_3()
  {
    return Process.GetProcessesByName("wsnm").Length != 0 || Class8.GetModuleHandle("SbieDll.dll").ToInt32() != 0;
  }

  public static bool smethod_4()
  {
    return Class8.smethod_1(Process.GetCurrentProcess()) || Class8.smethod_3() || (Class8.smethod_2() || Class8.smethod_0());
  }

  [DllImport("kernel32.dll", CharSet = CharSet.Unicode, BestFitMapping = false)]
  internal static extern IntPtr GetModuleHandle(string string_0);

  [DllImport("kernel32.dll", SetLastError = true)]
  [return: MarshalAs(UnmanagedType.Bool)]
  internal static extern bool CheckRemoteDebuggerPresent(IntPtr intptr_0, [MarshalAs(UnmanagedType.Bool)] ref bool bool_0);

  public Class8()
  {
    Class11.ARXWv9qzu32dU();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }
}
