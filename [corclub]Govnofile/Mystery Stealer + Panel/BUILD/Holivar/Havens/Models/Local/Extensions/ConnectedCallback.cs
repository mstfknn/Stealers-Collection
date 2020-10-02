using System;
using System.Net.Sockets;

namespace Havens.Models.Local.Extensions
{
	// Token: 0x02000008 RID: 8
	// (Invoke) Token: 0x06000012 RID: 18
	public delegate void ConnectedCallback(Socket socket, object state, SocketAsyncEventArgs e, Exception exception);
}
