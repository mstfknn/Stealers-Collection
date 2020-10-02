namespace NoiseMe.Drags.App.Models.WMI.Objects
{
	public class Processor
	{
		public uint? ClockSpeed
		{
			get;
		}

		public ushort? Voltage
		{
			get;
		}

		public string Name
		{
			get;
		}

		public string Manufacturer
		{
			get;
		}

		public uint? NumberOfCores
		{
			get;
		}

		public string Id
		{
			get;
		}

		public Processor(uint? speed, ushort? volt, string name, string manufacturer, uint? cores, string id)
		{
			ClockSpeed = speed;
			Voltage = volt;
			Name = name;
			Manufacturer = manufacturer;
			NumberOfCores = cores;
			Id = id;
		}
	}
}
