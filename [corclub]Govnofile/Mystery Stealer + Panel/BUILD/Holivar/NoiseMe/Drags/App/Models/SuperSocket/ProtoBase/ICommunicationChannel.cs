using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000E7 RID: 231
	public interface ICommunicationChannel
	{
		// Token: 0x0600073B RID: 1851
		void Send(ArraySegment<byte> segment);

		// Token: 0x0600073C RID: 1852
		void Close(CloseReason reason);
	}
}
