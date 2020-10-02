using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000EE RID: 238
	public interface IProtoHandler
	{
		// Token: 0x06000745 RID: 1861
		bool CanSend();

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000746 RID: 1862
		IProtoDataEncoder DataEncoder { get; }

		// Token: 0x06000747 RID: 1863
		void Close(ICommunicationChannel channel, CloseReason reason);
	}
}
