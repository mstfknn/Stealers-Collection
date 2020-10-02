namespace NoiseMe.Drags.App.Models.WMI.Objects
{
	public class HardDrive
	{
		public string Caption
		{
			get;
		}

		public string Model
		{
			get;
		}

		public uint? Signature
		{
			get;
		}

		public ulong? Capacity
		{
			get;
		}

		public HardDrive(string caption, string model, uint? signature, ulong? capacity)
		{
			Caption = caption;
			Model = model;
			Signature = signature;
			Capacity = capacity;
		}
	}
}
