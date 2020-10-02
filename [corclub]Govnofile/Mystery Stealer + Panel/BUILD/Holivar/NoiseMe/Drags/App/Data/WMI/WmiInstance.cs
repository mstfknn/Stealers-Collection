using System;
using System.Collections.Generic;
using System.Management;

namespace NoiseMe.Drags.App.Data.WMI
{
	// Token: 0x02000178 RID: 376
	public static class WmiInstance
	{
		// Token: 0x06000C23 RID: 3107 RVA: 0x00024330 File Offset: 0x00022530
		private static ManagementObjectCollection GetObjectCollection(ObjectQuery query, ManagementScope scope)
		{
			ManagementObjectCollection result;
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(scope, query))
			{
				result = managementObjectSearcher.Get();
			}
			return result;
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x000095D8 File Offset: 0x000077D8
		private static ManagementObjectCollection GetObjectCollection(string query, ManagementScope scope)
		{
			return WmiInstance.GetObjectCollection(new ObjectQuery(query), scope);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0002436C File Offset: 0x0002256C
		private static ManagementObjectCollection GetObjectCollection(string wmiclass, string[] properties, string condition, ManagementScope scope)
		{
			string text = (properties == null) ? "*" : string.Join(",", properties);
			return WmiInstance.GetObjectCollection(string.IsNullOrEmpty(condition) ? ("SELECT " + text + " FROM " + wmiclass) : string.Concat(new string[]
			{
				"SELECT ",
				text,
				" FROM ",
				wmiclass,
				" WHERE ",
				condition
			}), scope);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x000095E6 File Offset: 0x000077E6
		private static ManagementObjectCollection GetObjectCollection(string wmiclass, string property, string condition, ManagementScope scope)
		{
			return WmiInstance.GetObjectCollection(wmiclass, new string[]
			{
				property
			}, condition, scope);
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x000243E0 File Offset: 0x000225E0
		private static WmiInstanceClassCollection ParseObjectCollection(ManagementObjectCollection objectCollection)
		{
			List<WmiInstanceClass> list = new List<WmiInstanceClass>();
			try
			{
				foreach (ManagementBaseObject managementBaseObject in objectCollection)
				{
					ManagementBaseObject managementBaseObject2 = (ManagementObject)managementBaseObject;
					List<WmiInstanceProperty> list2 = new List<WmiInstanceProperty>();
					foreach (PropertyData propertyData in managementBaseObject2.Properties)
					{
						list2.Add(new WmiInstanceProperty(propertyData.Name, propertyData.Value));
					}
					list.Add(new WmiInstanceClass(list2));
				}
			}
			catch (ManagementException)
			{
				return null;
			}
			if (objectCollection != null)
			{
				objectCollection.Dispose();
			}
			return new WmiInstanceClassCollection(list);
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x000095FA File Offset: 0x000077FA
		public static WmiInstanceClassCollection Query(string wmiclass, string[] properties, string condition, ManagementScope scope = null)
		{
			return WmiInstance.ParseObjectCollection(WmiInstance.GetObjectCollection(wmiclass, properties, condition, scope));
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0000960A File Offset: 0x0000780A
		public static WmiInstanceClassCollection Query(string wmiclass, string[] properties, ManagementScope scope = null)
		{
			return WmiInstance.Query(wmiclass, properties, null, scope);
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00009615 File Offset: 0x00007815
		public static WmiInstanceClassCollection Query(string wmiclass, ManagementScope scope = null)
		{
			return WmiInstance.Query(wmiclass, null, scope);
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0000961F File Offset: 0x0000781F
		public static WmiInstanceClassCollection CustomQuery(ObjectQuery query, ManagementScope scope = null)
		{
			return WmiInstance.ParseObjectCollection(WmiInstance.GetObjectCollection(query, scope));
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0000962D File Offset: 0x0000782D
		public static WmiInstanceClassCollection CustomQuery(string query, ManagementScope scope = null)
		{
			return WmiInstance.CustomQuery(new ObjectQuery(query), scope);
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x000244BC File Offset: 0x000226BC
		public static WmiInstanceProperty PropertyQuery(string wmiclass, string property, string condition, ManagementScope scope = null)
		{
			foreach (WmiInstanceClass wmiInstanceClass in WmiInstance.ParseObjectCollection(WmiInstance.GetObjectCollection(wmiclass, property, condition, scope)))
			{
				WmiInstanceProperty[] properties = wmiInstanceClass.Properties;
				int num = 0;
				if (num < properties.Length)
				{
					return properties[num];
				}
			}
			return null;
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0000963B File Offset: 0x0000783B
		public static WmiInstanceProperty PropertyQuery(string wmiclass, string property, ManagementScope scope = null)
		{
			return WmiInstance.PropertyQuery(wmiclass, property, null, scope);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00024524 File Offset: 0x00022724
		public static T PropertyQuery<T>(string wmiclass, string property, string condition, ManagementScope scope = null)
		{
			WmiInstanceProperty wmiInstanceProperty = WmiInstance.PropertyQuery(wmiclass, property, condition, scope);
			if (wmiInstanceProperty.Value != null)
			{
				return (T)((object)wmiInstanceProperty.Value);
			}
			return default(T);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00009646 File Offset: 0x00007846
		public static T PropertyQuery<T>(string wmiclass, string property, ManagementScope scope = null)
		{
			return WmiInstance.PropertyQuery<T>(wmiclass, property, null, scope);
		}

		// Token: 0x040004A5 RID: 1189
		public const string OPERATINGSYSTEM_CLASSNAME = "Win32_OperatingSystem";

		// Token: 0x040004A6 RID: 1190
		public const string USERACCOUNT_CLASSNAME = "Win32_UserAccount";

		// Token: 0x040004A7 RID: 1191
		public const string ANTIVIRUS_CLASSNAME = "AntivirusProduct";

		// Token: 0x040004A8 RID: 1192
		public const string ANTISPYWARE_CLASSNAME = "AntiSpyWareProduct";

		// Token: 0x040004A9 RID: 1193
		public const string FIREWALL_CLASSNAME = "FirewallProduct";

		// Token: 0x040004AA RID: 1194
		public const string NETWORKADAPTER_CLASSNAME = "Win32_NetworkAdapterConfiguration";

		// Token: 0x040004AB RID: 1195
		public const string BASEBOARD_CLASSNAME = "Win32_Baseboard";

		// Token: 0x040004AC RID: 1196
		public const string PHYSICALMEMORY_CLASSNAME = "Win32_PhysicalMemory";

		// Token: 0x040004AD RID: 1197
		public const string PROCESSOR_CLASSNAME = "Win32_Processor";

		// Token: 0x040004AE RID: 1198
		public const string VIDEOCONTROLLER_CLASSNAME = "Win32_VideoController";

		// Token: 0x040004AF RID: 1199
		public const string DISKDRIVE_CLASSNAME = "Win32_DiskDrive";

		// Token: 0x040004B0 RID: 1200
		public const string BIOS_CLASSNAME = "Win32_BIOS";
	}
}
