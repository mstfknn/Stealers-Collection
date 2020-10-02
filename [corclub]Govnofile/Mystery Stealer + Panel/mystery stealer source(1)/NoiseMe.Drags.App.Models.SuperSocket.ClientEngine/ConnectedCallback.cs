using System;
using System.Net.Sockets;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	public delegate void ConnectedCallback(Socket socket, object state, SocketAsyncEventArgs e, Exception exception);
}
