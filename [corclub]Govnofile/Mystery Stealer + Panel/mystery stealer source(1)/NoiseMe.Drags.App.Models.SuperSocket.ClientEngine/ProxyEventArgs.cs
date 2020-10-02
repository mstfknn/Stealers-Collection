using System;
using System.Net.Sockets;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	public class ProxyEventArgs : EventArgs
	{
		public bool Connected
		{
			get;
			private set;
		}

		public Socket Socket
		{
			get;
			private set;
		}

		public Exception Exception
		{
			get;
			private set;
		}

		public string TargetHostName
		{
			get;
			private set;
		}

		public ProxyEventArgs(Socket socket)
			: this(connected: true, socket, null, null)
		{
		}

		public ProxyEventArgs(Socket socket, string targetHostHame)
			: this(connected: true, socket, targetHostHame, null)
		{
		}

		public ProxyEventArgs(Exception exception)
			: this(connected: false, null, null, exception)
		{
		}

		public ProxyEventArgs(bool connected, Socket socket, string targetHostName, Exception exception)
		{
			Connected = connected;
			Socket = socket;
			TargetHostName = targetHostName;
			Exception = exception;
		}
	}
}
