using System;
using System.Net;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x0200010C RID: 268
	public interface IProxyConnector
	{
		// Token: 0x06000822 RID: 2082
		void Connect(EndPoint remoteEndPoint);

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000823 RID: 2083
		// (remove) Token: 0x06000824 RID: 2084
		event EventHandler<ProxyEventArgs> Completed;
	}
}
