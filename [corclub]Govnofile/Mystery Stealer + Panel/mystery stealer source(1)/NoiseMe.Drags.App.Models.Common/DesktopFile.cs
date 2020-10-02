using ProtoBuf;
using System.ComponentModel;

namespace NoiseMe.Drags.App.Models.Common
{
	[ProtoContract(Name = "DesktopFile")]
	public class DesktopFile : INotifyPropertyChanged
	{
		private string _filename;

		private byte[] _fileData;

		[ProtoMember(1, Name = "Filename")]
		public string Filename
		{
			get
			{
				return _filename;
			}
			set
			{
				_filename = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Filename"));
			}
		}

		[ProtoMember(2, Name = "FileData")]
		public byte[] FileData
		{
			get
			{
				return _fileData;
			}
			set
			{
				_fileData = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FileData"));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
