using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000D5 RID: 213
	public enum CloseReason
	{
		// Token: 0x040002DA RID: 730
		Unknown,
		// Token: 0x040002DB RID: 731
		ServerShutdown,
		// Token: 0x040002DC RID: 732
		ClientClosing,
		// Token: 0x040002DD RID: 733
		ServerClosing,
		// Token: 0x040002DE RID: 734
		ApplicationError,
		// Token: 0x040002DF RID: 735
		SocketError,
		// Token: 0x040002E0 RID: 736
		TimeOut,
		// Token: 0x040002E1 RID: 737
		ProtocolError,
		// Token: 0x040002E2 RID: 738
		publicError
	}
}
