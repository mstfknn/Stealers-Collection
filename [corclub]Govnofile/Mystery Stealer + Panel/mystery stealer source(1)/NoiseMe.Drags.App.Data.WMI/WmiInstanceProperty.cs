namespace NoiseMe.Drags.App.Data.WMI
{
	public class WmiInstanceProperty
	{
		public string Name
		{
			get;
		}

		public object Value
		{
			get;
		}

		public WmiInstanceProperty(string name, object value)
		{
			Name = name;
			Value = value;
		}
	}
}
