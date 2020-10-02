using System;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Common
{
	// Token: 0x02000166 RID: 358
	[ProtoContract(Name = "Event")]
	public class Event
	{
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x00008DC8 File Offset: 0x00006FC8
		// (set) Token: 0x06000B7D RID: 2941 RVA: 0x00008DD0 File Offset: 0x00006FD0
		[ProtoMember(1, Name = "type")]
		public string type { get; set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x00008DD9 File Offset: 0x00006FD9
		// (set) Token: 0x06000B7F RID: 2943 RVA: 0x00008DE1 File Offset: 0x00006FE1
		[ProtoMember(2, Name = "properties")]
		public Properties properties { get; set; }
	}
}
