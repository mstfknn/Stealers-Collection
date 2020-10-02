using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace Evrial.Hardware
{
	// Token: 0x0200001B RID: 27
	internal static class Identification
	{
		// Token: 0x06000043 RID: 67 RVA: 0x000041F8 File Offset: 0x000023F8
		public static string GetId()
		{
			string result;
			try
			{
				using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Cryptography"))
					{
						if (registryKey2 == null)
						{
							throw new KeyNotFoundException(string.Format("Key Not Found: {0}", "SOFTWARE\\Microsoft\\Cryptography"));
						}
						object value = registryKey2.GetValue("MachineGuid");
						if (value == null)
						{
							throw new IndexOutOfRangeException(string.Format("Index Not Found: {0}", "MachineGuid"));
						}
						result = value.ToString().ToUpper().Replace("-", string.Empty);
					}
				}
			}
			catch
			{
				result = "HWID not found";
			}
			return result;
		}
	}
}
