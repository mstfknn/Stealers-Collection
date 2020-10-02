using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Common
{
	[ProtoContract(Name = "Event")]
	public class Event
	{
		[ProtoMember(1, Name = "type")]
		public string type
		{
			get;
			set;
		}

		[ProtoMember(2, Name = "properties")]
		public Properties properties
		{
			get;
			set;
		}
	}
}
