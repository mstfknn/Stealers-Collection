using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Strange.Additions
{
	// Token: 0x02000003 RID: 3
	internal class Delete
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000021F8 File Offset: 0x000003F8
		public static void DeleteExe()
		{
			Process.Start(new ProcessStartInfo
			{
				Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + new FileInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath).Name + "\"",
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true,
				FileName = "cmd.exe"
			});
		}
	}
}
