using ProtoBuf;
using System.ComponentModel;

namespace NoiseMe.Drags.App.Models.Communication
{
	[ProtoContract(Name = "CommunicationObject")]
	public class CommunicationObject : INotifyPropertyChanged
	{
		[ProtoMember(1, Name = "Version")]
		public string Version
		{
			get;
			set;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
