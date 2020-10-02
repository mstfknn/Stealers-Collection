using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Common
{
	[ProtoContract(Name = "RemoteProcess")]
	public class RemoteProcess
	{
		[ProtoMember(1, Name = "ProcessID")]
		public int ProcessID
		{
			get;
			set;
		}

		[ProtoMember(2, Name = "ProcessName")]
		public string ProcessName
		{
			get;
			set;
		}

		[ProtoMember(3, Name = "ProcessCommandLine")]
		public string ProcessCommandLine
		{
			get;
			set;
		}

		[ProtoMember(4, Name = "ProcessUsername")]
		public string ProcessUsername
		{
			get;
			set;
		}
	}
}
