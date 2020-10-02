using System;
using System.IO;

namespace Evrial.Stealer
{
	// Token: 0x0200000F RID: 15
	internal static class Helper
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002AFC File Offset: 0x00000CFC
		public static string GetRandomString()
		{
			string randomFileName = Path.GetRandomFileName();
			return randomFileName.Replace(".", "");
		}
	}
}
