namespace NoiseMe.Drags.App.Models.WMI.Objects
{
	public class RamStick
	{
		public ulong? Capacity
		{
			get;
		}

		public uint? ClockSpeed
		{
			get;
		}

		public string Manufacturer
		{
			get;
		}

		public string SerialNumber
		{
			get;
		}

		public uint? PositionInRow
		{
			get;
		}

		public RamStick(ulong? capacity, uint? speed, string manufacturer, string serial, uint? pos)
		{
			Capacity = capacity;
			ClockSpeed = speed;
			Manufacturer = manufacturer;
			SerialNumber = serial;
			PositionInRow = pos;
		}
	}
}
