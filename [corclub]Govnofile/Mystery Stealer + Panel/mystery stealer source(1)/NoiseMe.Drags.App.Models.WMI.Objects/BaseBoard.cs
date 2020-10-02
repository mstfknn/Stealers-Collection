namespace NoiseMe.Drags.App.Models.WMI.Objects
{
	public class BaseBoard
	{
		public string Model
		{
			get;
		}

		public string Product
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

		public string SerialNumber
		{
			get;
		}

		public BaseBoard(string model, string product, string name, string manufacturer, string serial)
		{
			Model = model;
			Product = product;
			Name = name;
			Manufacturer = manufacturer;
			SerialNumber = serial;
		}
	}
}
