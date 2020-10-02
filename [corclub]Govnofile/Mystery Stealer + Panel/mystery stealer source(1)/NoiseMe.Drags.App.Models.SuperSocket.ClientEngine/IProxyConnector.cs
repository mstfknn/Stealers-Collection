using System;
using System.Net;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	public interface IProxyConnector
	{
		event EventHandler<ProxyEventArgs> Completed;

		void Connect(EndPoint remoteEndPoint);
	}
}
