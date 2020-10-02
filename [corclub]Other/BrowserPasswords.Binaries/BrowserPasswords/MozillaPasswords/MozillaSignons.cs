using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using Microsoft.VisualBasic.CompilerServices;

namespace BrowserPasswords.MozillaPasswords
{
	// Token: 0x0200002B RID: 43
	public class MozillaSignons : IEnumerable<MozillaPassword>
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000502C File Offset: 0x0000322C
		public MozillaProfile.MozillaVersion Version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005044 File Offset: 0x00003244
		private void ReadTxt(MozillaSoft soft, string profile_path, MozillaProfile.MozillaVersion version)
		{
			string path;
			switch (version)
			{
			case MozillaProfile.MozillaVersion.Txt2c:
				path = Path.Combine(profile_path, "signons.txt");
				break;
			case MozillaProfile.MozillaVersion.Txt2d:
				path = Path.Combine(profile_path, "signons2.txt");
				break;
			case MozillaProfile.MozillaVersion.Txt2e:
				path = Path.Combine(profile_path, "signons3.txt");
				break;
			default:
				throw new InvalidOperationException("Not a text file");
			}
			if (File.Exists(path))
			{
				StreamReader streamReader = new StreamReader(path);
				streamReader.ReadLine();
				string text = string.Empty;
				text = streamReader.ReadLine();
				while (Operators.CompareString(text, ".", false) != 0)
				{
					this.no_save_sites.Add(text);
					text = streamReader.ReadLine();
				}
				string domain = string.Empty;
				for (text = streamReader.ReadLine(); text != null; text = streamReader.ReadLine())
				{
					if (Operators.CompareString(text, string.Empty, false) == 0)
					{
						break;
					}
					string sitename = text;
					for (;;)
					{
						string text2 = streamReader.ReadLine();
						if (text2 == null || Operators.CompareString(text2, ".", false) == 0)
						{
							break;
						}
						string username = streamReader.ReadLine();
						string password_field = streamReader.ReadLine().Substring(1);
						string text3 = streamReader.ReadLine();
						if (text3.StartsWith("~"))
						{
							text3 = text3.Substring(1);
						}
						if (version >= MozillaProfile.MozillaVersion.Txt2d)
						{
							domain = streamReader.ReadLine();
						}
						if (version >= MozillaProfile.MozillaVersion.Txt2e)
						{
							streamReader.ReadLine();
						}
						this.password_list.Add(new MozillaPassword(soft, profile_path, sitename, text2, password_field, username, text3, domain, string.Empty));
					}
				}
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000051C0 File Offset: 0x000033C0
		private void ReadSqlite(MozillaSoft soft, string profile_path, MozillaProfile.MozillaVersion version)
		{
			string text = Path.Combine(profile_path, "signons.sqlite");
			if (File.Exists(text))
			{
				using (SQLiteConnection sqliteConnection = new SQLiteConnection(string.Format("Data Source=\"{0}\";Version=3;", text)))
				{
					sqliteConnection.Open();
					using (SQLiteCommand sqliteCommand = new SQLiteCommand("SELECT hostname FROM moz_disabledHosts", sqliteConnection))
					{
						using (SQLiteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
						{
							while (sqliteDataReader.Read())
							{
								this.no_save_sites.Add(sqliteDataReader.GetString(0));
							}
						}
					}
					using (SQLiteCommand sqliteCommand2 = new SQLiteCommand("SELECT hostname,httpRealm,formSubmitURL,usernameField,passwordField,encryptedUsername,encryptedPassword FROM moz_logins", sqliteConnection))
					{
						using (SQLiteDataReader sqliteDataReader2 = sqliteCommand2.ExecuteReader())
						{
							while (sqliteDataReader2.Read())
							{
								this.password_list.Add(new MozillaPassword(soft, profile_path, sqliteDataReader2.GetValue(0).ToString(), sqliteDataReader2.GetValue(3).ToString(), sqliteDataReader2.GetValue(4).ToString(), sqliteDataReader2.GetValue(5).ToString(), sqliteDataReader2.GetValue(6).ToString(), sqliteDataReader2.GetValue(1).ToString(), sqliteDataReader2.GetValue(2).ToString()));
							}
							sqliteDataReader2.Close();
						}
					}
					sqliteConnection.Close();
				}
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005384 File Offset: 0x00003584
		public MozillaSignons(MozillaSoft soft, string profile_path, MozillaProfile.MozillaVersion version)
		{
			this.no_save_sites = new List<string>();
			this.password_list = new List<MozillaPassword>();
			switch (version)
			{
			case MozillaProfile.MozillaVersion.Txt2c:
			case MozillaProfile.MozillaVersion.Txt2d:
			case MozillaProfile.MozillaVersion.Txt2e:
				this.ReadTxt(soft, profile_path, version);
				break;
			case MozillaProfile.MozillaVersion.Sqlite:
				this.ReadSqlite(soft, profile_path, version);
				break;
			}
			this.m_Version = version;
		}

		// Token: 0x1700007C RID: 124
		public MozillaPassword this[int index]
		{
			get
			{
				MozillaPassword result;
				if (index >= 0 && index < this.password_list.Count)
				{
					result = this.password_list[index];
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005418 File Offset: 0x00003618
		public IEnumerator<MozillaPassword> GetEnumerator1()
		{
			return (IEnumerator<MozillaPassword>)this.password_list.GetEnumerator();
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005440 File Offset: 0x00003640
		public IEnumerator GetEnumerator()
		{
			return (IEnumerator)this.password_list.GetEnumerator();
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00005468 File Offset: 0x00003668
		public int NoPasswordSiteCount
		{
			get
			{
				return this.no_save_sites.Count;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00005484 File Offset: 0x00003684
		public int PasswordCount
		{
			get
			{
				return this.password_list.Count;
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000054A0 File Offset: 0x000036A0
		public string GetNoPasswordSite(int index)
		{
			string result;
			if (index >= 0 && index < this.no_save_sites.Count)
			{
				result = this.no_save_sites[index];
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x04000121 RID: 289
		private List<string> no_save_sites;

		// Token: 0x04000122 RID: 290
		private List<MozillaPassword> password_list;

		// Token: 0x04000123 RID: 291
		private MozillaProfile.MozillaVersion m_Version;
	}
}
