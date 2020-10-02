using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Common
{
	[ProtoContract(Name = "Properties")]
	public class Properties
	{
		[ProtoMember(1, Name = "client_track_timestamp")]
		public string client_track_timestamp
		{
			get;
			set;
		}

		[ProtoMember(2, Name = "num_users_visible")]
		public int num_users_visible
		{
			get;
			set;
		}

		[ProtoMember(3, Name = "num_users_visible_with_mobile_indicator")]
		public int num_users_visible_with_mobile_indicator
		{
			get;
			set;
		}

		[ProtoMember(4, Name = "client_uuid")]
		public string client_uuid
		{
			get;
			set;
		}

		[ProtoMember(5, Name = "client_send_timestamp")]
		public string client_send_timestamp
		{
			get;
			set;
		}
	}
}
