using System;
using System.Net.Sockets;

namespace Havens.Models.Local.Extensions
{
	public delegate void ConnectedCallback(Socket socket, object state, SocketAsyncEventArgs e, Exception exception);
}
