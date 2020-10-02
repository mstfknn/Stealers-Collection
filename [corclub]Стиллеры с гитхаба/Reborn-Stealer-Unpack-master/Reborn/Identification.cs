using System;
using System.IO;
using System.Management;
using Reborn.Helper;

namespace Reborn
{
	// Token: 0x02000015 RID: 21
	internal static class Identification
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00007698 File Offset: 0x00005898
		public static string GetWindowsVersion()
		{
			ManagementObjectSearcher arg_10_0 = new ManagementObjectSearcher("SELECT * FROM CIM_OperatingSystem");
			string text = string.Empty;
			using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = arg_10_0.Get().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					text = ((ManagementObject)enumerator.Current)["Caption"].ToString();
				}
			}
			if (text.Contains("8"))
			{
				return "Windows 8";
			}
			if (text.Contains("8.1"))
			{
				return "Windows 8.1";
			}
			if (text.Contains("10"))
			{
				return "Windows 10";
			}
			if (text.Contains("XP"))
			{
				return "Windows XP";
			}
			if (text.Contains("7"))
			{
				return "Windows 7";
			}
			if (text.Contains("Server"))
			{
				return "Windows Server";
			}
			return "Unknown";
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002C56 File Offset: 0x00000E56
		public static string GetId()
		{
			return Identification.DiskId("") + Identification.ProcessorId();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000777C File Offset: 0x0000597C
		private static string DiskId(string diskLetter = "")
		{
			if (diskLetter == string.Empty)
			{
				DriveInfo[] drives = DriveInfo.GetDrives();
				for (int i = 0; i < drives.Length; i++)
				{
					DriveInfo driveInfo = drives[i];
					if (driveInfo.IsReady)
					{
						diskLetter = driveInfo.RootDirectory.ToString();
						break;
					}
				}
			}
			if (diskLetter.EndsWith(":\\"))
			{
				diskLetter = diskLetter.Substring(0, diskLetter.Length - 2);
			}
			ManagementObject expr_7F = new ManagementObject("win32_logicaldisk.deviceid=\"" + diskLetter + ":\"");
			expr_7F.Get();
			string result = expr_7F["VolumeSerialNumber"].ToString();
			expr_7F.Dispose();
			return result;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00007828 File Offset: 0x00005A28
		private static string ProcessorId()
		{
			string result;
			try
			{
				ManagementObjectCollection arg_15_0 = new ManagementClass("win32_processor").GetInstances();
				string text = string.Empty;
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = arg_15_0.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						text = enumerator.Current.Properties["processorID"].Value.ToString();
					}
				}
				result = text;
			}
			catch
			{
				result = "-WRONGID-" + Misc.GetRandomString();
			}
			return result;
		}
	}
}
