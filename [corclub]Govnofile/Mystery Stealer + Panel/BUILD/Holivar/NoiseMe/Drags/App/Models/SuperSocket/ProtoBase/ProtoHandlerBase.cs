using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000F6 RID: 246
	public abstract class ProtoHandlerBase : IProtoHandler
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x00006350 File Offset: 0x00004550
		// (set) Token: 0x0600075F RID: 1887 RVA: 0x00006358 File Offset: 0x00004558
		public IProtoDataEncoder DataEncoder { get; set; }

		// Token: 0x06000760 RID: 1888
		public abstract bool CanSend();

		// Token: 0x06000761 RID: 1889
		public abstract void Close(ICommunicationChannel channel, CloseReason reason);
	}
}
