using System;
using System.ComponentModel;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Common
{
	// Token: 0x0200016B RID: 363
	[ProtoContract(Name = "RdpCredential")]
	public class RdpCredential : INotifyPropertyChanged
	{
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x00008F3C File Offset: 0x0000713C
		// (set) Token: 0x06000B9C RID: 2972 RVA: 0x00008F44 File Offset: 0x00007144
		[ProtoMember(1, Name = "Target")]
		public string Target
		{
			get
			{
				return this._target;
			}
			set
			{
				this._target = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Target"));
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x00008F68 File Offset: 0x00007168
		// (set) Token: 0x06000B9E RID: 2974 RVA: 0x00008F70 File Offset: 0x00007170
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

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x00008F94 File Offset: 0x00007194
		// (set) Token: 0x06000BA0 RID: 2976 RVA: 0x00008F9C File Offset: 0x0000719C
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

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000BA1 RID: 2977 RVA: 0x000231A8 File Offset: 0x000213A8
		// (remove) Token: 0x06000BA2 RID: 2978 RVA: 0x000231E0 File Offset: 0x000213E0
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x04000479 RID: 1145
		private string _target;

		// Token: 0x0400047A RID: 1146
		private string _username;

		// Token: 0x0400047B RID: 1147
		private string _password;
	}
}
