using NoiseMe.Drags.App.Models.Common;
using ProtoBuf;
using System.ComponentModel;

namespace NoiseMe.Drags.App.Models
{
	[ProtoContract(Name = "ClientSettings")]
	public class ClientSettings : INotifyPropertyChanged
	{
		private bool _grabBrowserCredentials;

		private bool _grabColdWallets;

		private bool _grabRdp;

		private bool _grabFtp;

		private bool _grabDesktopFiles;

		private bool _grabTelegram;

		private bool _grabDiscord;

		private bool _encryptFiles;

		private BindingList<RemoteTask> _tasks;

		private BindingList<string> _desktopExtensions;

		[ProtoMember(1, Name = "GrabBrowserCredentials")]
		public bool GrabBrowserCredentials
		{
			get
			{
				return _grabBrowserCredentials;
			}
			set
			{
				_grabBrowserCredentials = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GrabBrowserCredentials"));
			}
		}

		[ProtoMember(2, Name = "GrabColdWallets")]
		public bool GrabColdWallets
		{
			get
			{
				return _grabColdWallets;
			}
			set
			{
				_grabColdWallets = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GrabColdWallets"));
			}
		}

		[ProtoMember(3, Name = "GrabRdp")]
		public bool GrabRdp
		{
			get
			{
				return _grabRdp;
			}
			set
			{
				_grabRdp = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GrabRdp"));
			}
		}

		[ProtoMember(4, Name = "GrabFtp")]
		public bool GrabFtp
		{
			get
			{
				return _grabFtp;
			}
			set
			{
				_grabFtp = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GrabFtp"));
			}
		}

		[ProtoMember(5, Name = "GrabDesktopFiles")]
		public bool GrabDesktopFiles
		{
			get
			{
				return _grabDesktopFiles;
			}
			set
			{
				_grabDesktopFiles = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GrabDesktopFiles"));
			}
		}

		[ProtoMember(6, Name = "DesktopExtensions")]
		public BindingList<string> DesktopExtensions
		{
			get
			{
				return _desktopExtensions;
			}
			set
			{
				_desktopExtensions = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DesktopExtensions"));
			}
		}

		[ProtoMember(7, Name = "GrabTelegram")]
		public bool GrabTelegram
		{
			get
			{
				return _grabTelegram;
			}
			set
			{
				_grabTelegram = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GrabTelegram"));
			}
		}

		[ProtoMember(8, Name = "GrabDiscord")]
		public bool GrabDiscord
		{
			get
			{
				return _grabDiscord;
			}
			set
			{
				_grabDiscord = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GrabDiscord"));
			}
		}

		[ProtoMember(9, Name = "EncryptFiles")]
		public bool EncryptFiles
		{
			get
			{
				return _encryptFiles;
			}
			set
			{
				_encryptFiles = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EncryptFiles"));
			}
		}

		[ProtoMember(10, Name = "Tasks")]
		public BindingList<RemoteTask> Tasks
		{
			get
			{
				return _tasks;
			}
			set
			{
				_tasks = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tasks"));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
