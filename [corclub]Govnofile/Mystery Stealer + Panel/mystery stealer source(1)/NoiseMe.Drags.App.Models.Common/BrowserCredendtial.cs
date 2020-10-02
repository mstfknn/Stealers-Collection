using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel;

namespace NoiseMe.Drags.App.Models.Common
{
	[ProtoContract(Name = "BrowserCredendtial")]
	public class BrowserCredendtial : INotifyPropertyChanged, IEqualityComparer<BrowserCredendtial>
	{
		private string _login;

		private string _password;

		private string _uRL;

		[ProtoMember(1, Name = "Login")]
		public string Login
		{
			get
			{
				return _login;
			}
			set
			{
				_login = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Login"));
			}
		}

		[ProtoMember(2, Name = "Password")]
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

		[ProtoMember(3, Name = "URL")]
		public string URL
		{
			get
			{
				return _uRL;
			}
			set
			{
				_uRL = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("URL"));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public bool Equals(BrowserCredendtial x, BrowserCredendtial y)
		{
			if (x.Login == y.Login && x.Password == y.Password)
			{
				return x.URL == y.URL;
			}
			return false;
		}

		public int GetHashCode(BrowserCredendtial obj)
		{
			int num = 37;
			num *= 397;
			if (!string.IsNullOrEmpty(obj.Login))
			{
				num += obj.Login.GetHashCode();
			}
			if (!string.IsNullOrEmpty(obj.Password))
			{
				num += obj.Password.GetHashCode();
			}
			if (!string.IsNullOrEmpty(obj.URL))
			{
				num += obj.URL.GetHashCode();
			}
			return num;
		}
	}
}
