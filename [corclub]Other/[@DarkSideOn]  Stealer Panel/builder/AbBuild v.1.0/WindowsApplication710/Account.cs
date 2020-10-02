using System;

namespace WindowsApplication710
{
	// Token: 0x0200000D RID: 13
	internal class Account
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003F10 File Offset: 0x00002110
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002162 File Offset: 0x00000362
		public string Username
		{
			get
			{
				return this._username;
			}
			set
			{
				this._username = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003F28 File Offset: 0x00002128
		// (set) Token: 0x06000057 RID: 87 RVA: 0x0000216B File Offset: 0x0000036B
		public string Password
		{
			get
			{
				return this._password;
			}
			set
			{
				this._password = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003F40 File Offset: 0x00002140
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002174 File Offset: 0x00000374
		public string Domain
		{
			get
			{
				return this._domain;
			}
			set
			{
				this._domain = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003F58 File Offset: 0x00002158
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000217D File Offset: 0x0000037D
		public AccountType Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002186 File Offset: 0x00000386
		public Account(AccountType Type, string Username, string Password)
		{
			this.Type = Type;
			this.Username = Username;
			this.Password = Password;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000021A3 File Offset: 0x000003A3
		public Account(AccountType Type, string Username, string Password, string Domain)
		{
			this.Type = Type;
			this.Username = Username;
			this.Password = Password;
			this.Domain = Domain;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000021C8 File Offset: 0x000003C8
		public Account(AccountType Type)
		{
			this.Type = Type;
		}

		// Token: 0x04000018 RID: 24
		private string _username;

		// Token: 0x04000019 RID: 25
		private string _password;

		// Token: 0x0400001A RID: 26
		private string _domain;

		// Token: 0x0400001B RID: 27
		private AccountType _type;
	}
}
