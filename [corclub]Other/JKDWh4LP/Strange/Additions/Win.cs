using System;
using System.Management;

namespace Strange.Additions
{
	// Token: 0x02000009 RID: 9
	internal class Win
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002448 File Offset: 0x00000648
		public static string GetWin()
		{
			string result;
			try
			{
				ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM CIM_OperatingSystem");
				string text = string.Empty;
				foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
				{
					text = managementBaseObject["Caption"].ToString();
				}
				if (text.Contains("8"))
				{
					result = "Windows 8";
				}
				else if (text.Contains("8.1"))
				{
					result = "Windows 8.1";
				}
				else if (text.Contains("10"))
				{
					result = "Windows 10";
				}
				else if (text.Contains("XP"))
				{
					result = "Windows XP";
				}
				else if (text.Contains("7"))
				{
					result = "Windows 7";
				}
				else
				{
					result = (text.Contains("Server") ? "Windows Server" : "Unknown");
				}
			}
			catch
			{
				result = "Unknown";
			}
			return result;
		}
	}
}
