namespace NoiseMe.Drags.App.Models.WMI.Objects
{
	public class NetworkAdapter
	{
		public string Caption
		{
			get;
		}

		public string Description
		{
			get;
		}

		public bool? IsIpEnabled
		{
			get;
		}

		public string MacAddress
		{
			get;
		}

		public NetworkAdapter(string caption, string description, bool? enabled, string mac)
		{
			Caption = caption;
			Description = description;
			IsIpEnabled = enabled;
			MacAddress = mac;
		}
	}
}
