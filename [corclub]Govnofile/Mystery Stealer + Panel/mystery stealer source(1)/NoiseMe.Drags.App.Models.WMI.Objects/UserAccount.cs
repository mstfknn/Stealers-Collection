namespace NoiseMe.Drags.App.Models.WMI.Objects
{
	public class UserAccount
	{
		public string Name
		{
			get;
		}

		public string FullName
		{
			get;
		}

		public bool? IsDisabled
		{
			get;
		}

		public UserAccount(string name, string fullName, bool? disabled)
		{
			Name = name;
			FullName = fullName;
			IsDisabled = disabled;
		}
	}
}
