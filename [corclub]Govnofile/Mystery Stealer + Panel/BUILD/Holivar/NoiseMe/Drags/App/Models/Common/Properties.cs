using System;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Common
{
	// Token: 0x02000165 RID: 357
	[ProtoContract(Name = "Properties")]
	public class Properties
	{
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x00008D73 File Offset: 0x00006F73
		// (set) Token: 0x06000B72 RID: 2930 RVA: 0x00008D7B File Offset: 0x00006F7B
		[ProtoMember(1, Name = "client_track_timestamp")]
		public string client_track_timestamp { get; set; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x00008D84 File Offset: 0x00006F84
		// (set) Token: 0x06000B74 RID: 2932 RVA: 0x00008D8C File Offset: 0x00006F8C
		[ProtoMember(2, Name = "num_users_visible")]
		public int num_users_visible { get; set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x00008D95 File Offset: 0x00006F95
		// (set) Token: 0x06000B76 RID: 2934 RVA: 0x00008D9D File Offset: 0x00006F9D
		[ProtoMember(3, Name = "num_users_visible_with_mobile_indicator")]
		public int num_users_visible_with_mobile_indicator { get; set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x00008DA6 File Offset: 0x00006FA6
		// (set) Token: 0x06000B78 RID: 2936 RVA: 0x00008DAE File Offset: 0x00006FAE
		[ProtoMember(4, Name = "client_uuid")]
		public string client_uuid { get; set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x00008DB7 File Offset: 0x00006FB7
		// (set) Token: 0x06000B7A RID: 2938 RVA: 0x00008DBF File Offset: 0x00006FBF
		[ProtoMember(5, Name = "client_send_timestamp")]
		public string client_send_timestamp { get; set; }
	}
}
