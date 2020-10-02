using System;
using System.Management;

namespace Strange.Additions
{
	// Token: 0x02000004 RID: 4
	internal class Hwid
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002260 File Offset: 0x00000460
		public static string Getid()
		{
			string result = "";
			try
			{
				string str = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 1);
				ManagementObject managementObject = new ManagementObject("win32_logicaldisk.deviceid=\"" + str + ":\"");
				managementObject.Get();
				string text = managementObject["VolumeSerialNumber"].ToString();
				result = text;
			}
			catch (Exception)
			{
			}
			return result;
		}
	}
}
