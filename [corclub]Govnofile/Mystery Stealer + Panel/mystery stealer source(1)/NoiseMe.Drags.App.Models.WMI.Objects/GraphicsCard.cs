namespace NoiseMe.Drags.App.Models.WMI.Objects
{
	public class GraphicsCard
	{
		public uint? MemoryCapacity
		{
			get;
		}

		public string Caption
		{
			get;
		}

		public string Description
		{
			get;
		}

		public string Name
		{
			get;
		}

		public GraphicsCard(uint? memory, string caption, string description, string name)
		{
			MemoryCapacity = memory;
			Caption = caption;
			Description = description;
			Name = name;
		}
	}
}
