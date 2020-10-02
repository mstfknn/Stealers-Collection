namespace NoiseMe.Drags.App.Models.WMI.Objects
{
	public class SecurityProduct
	{
		public string Name
		{
			get;
		}

		public string PathToExe
		{
			get;
		}

		public SecurityProduct(string name, string path)
		{
			Name = name;
			PathToExe = path;
		}
	}
}
