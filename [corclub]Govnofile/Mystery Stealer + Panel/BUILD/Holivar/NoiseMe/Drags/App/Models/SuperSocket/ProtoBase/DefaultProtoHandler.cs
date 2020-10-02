using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000D7 RID: 215
	public class DefaultProtoHandler : ProtoHandlerBase
	{
		// Token: 0x06000709 RID: 1801 RVA: 0x00003147 File Offset: 0x00001347
		public override bool CanSend()
		{
			return true;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00006166 File Offset: 0x00004366
		public override void Close(ICommunicationChannel channel, CloseReason reason)
		{
			channel.Close(reason);
		}
	}
}
