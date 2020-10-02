using MailRy.Net;
using System;
using System.Net;
using System.Net.Sockets;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	public static class ConnectAsyncExtension
	{
		private class ConnectToken
		{
			public object State
			{
				get;
				set;
			}

			public ConnectedCallback Callback
			{
				get;
				set;
			}
		}

		private class DnsConnectState
		{
			public IPAddress[] Addresses
			{
				get;
				set;
			}

			public int NextAddressIndex
			{
				get;
				set;
			}

			public int Port
			{
				get;
				set;
			}

			public Socket Socket4
			{
				get;
				set;
			}

			public Socket Socket6
			{
				get;
				set;
			}

			public object State
			{
				get;
				set;
			}

			public ConnectedCallback Callback
			{
				get;
				set;
			}

			public EndPoint LocalEndPoint
			{
				get;
				set;
			}
		}

		private static void SocketAsyncEventCompleted(object sender, SocketAsyncEventArgs e)
		{
			e.Completed -= SocketAsyncEventCompleted;
			ConnectToken connectToken = (ConnectToken)e.UserToken;
			e.UserToken = null;
			connectToken.Callback(sender as Socket, connectToken.State, e, null);
		}

		private static SocketAsyncEventArgs CreateSocketAsyncEventArgs(EndPoint remoteEndPoint, ConnectedCallback callback, object state)
		{
			SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();
			socketAsyncEventArgs.UserToken = new ConnectToken
			{
				State = state,
				Callback = callback
			};
			socketAsyncEventArgs.RemoteEndPoint = remoteEndPoint;
			socketAsyncEventArgs.Completed += SocketAsyncEventCompleted;
			return socketAsyncEventArgs;
		}

		private static void ConnectAsyncpublic(this EndPoint remoteEndPoint, EndPoint localEndPoint, ConnectedCallback callback, object state)
		{
			if (remoteEndPoint is DnsEndPoint)
			{
				DnsEndPoint dnsEndPoint = (DnsEndPoint)remoteEndPoint;
				IAsyncResult asyncResult = Dns.BeginGetHostAddresses(dnsEndPoint.Host, OnGetHostAddresses, new DnsConnectState
				{
					Port = dnsEndPoint.Port,
					Callback = callback,
					State = state,
					LocalEndPoint = localEndPoint
				});
				if (asyncResult.CompletedSynchronously)
				{
					OnGetHostAddresses(asyncResult);
				}
				return;
			}
			SocketAsyncEventArgs e = CreateSocketAsyncEventArgs(remoteEndPoint, callback, state);
			Socket socket = new Socket(remoteEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			if (localEndPoint != null)
			{
				socket.ExclusiveAddressUse = false;
				socket.Bind(localEndPoint);
			}
			socket.ConnectAsync(e);
		}

		private static IPAddress GetNextAddress(DnsConnectState state, out Socket attempSocket)
		{
			IPAddress iPAddress = null;
			attempSocket = null;
			int num = state.NextAddressIndex;
			while (attempSocket == null)
			{
				if (num >= state.Addresses.Length)
				{
					return null;
				}
				iPAddress = state.Addresses[num++];
				if (iPAddress.AddressFamily == AddressFamily.InterNetworkV6)
				{
					attempSocket = state.Socket6;
				}
				else if (iPAddress.AddressFamily == AddressFamily.InterNetwork)
				{
					attempSocket = state.Socket4;
				}
			}
			state.NextAddressIndex = num;
			return iPAddress;
		}

		private static void OnGetHostAddresses(IAsyncResult result)
		{
			DnsConnectState dnsConnectState = result.AsyncState as DnsConnectState;
			IPAddress[] array;
			try
			{
				array = Dns.EndGetHostAddresses(result);
			}
			catch (Exception exception)
			{
				dnsConnectState.Callback(null, dnsConnectState.State, null, exception);
				return;
			}
			if (array == null || array.Length == 0)
			{
				dnsConnectState.Callback(null, dnsConnectState.State, null, new SocketException(11001));
				return;
			}
			dnsConnectState.Addresses = array;
			Socket attempSocket;
			IPAddress nextAddress = GetNextAddress(dnsConnectState, out attempSocket);
			if (nextAddress == null)
			{
				dnsConnectState.Callback(null, dnsConnectState.State, null, new SocketException(10047));
				return;
			}
			if (dnsConnectState.LocalEndPoint != null)
			{
				try
				{
					attempSocket.ExclusiveAddressUse = false;
					attempSocket.Bind(dnsConnectState.LocalEndPoint);
				}
				catch (Exception exception2)
				{
					dnsConnectState.Callback(null, dnsConnectState.State, null, exception2);
					return;
				}
			}
			SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();
			socketAsyncEventArgs.Completed += SocketConnectCompleted;
			IPEndPoint iPEndPoint = (IPEndPoint)(socketAsyncEventArgs.RemoteEndPoint = new IPEndPoint(nextAddress, dnsConnectState.Port));
			socketAsyncEventArgs.UserToken = dnsConnectState;
			if (!attempSocket.ConnectAsync(socketAsyncEventArgs))
			{
				SocketConnectCompleted(attempSocket, socketAsyncEventArgs);
			}
		}

		private static void SocketConnectCompleted(object sender, SocketAsyncEventArgs e)
		{
			DnsConnectState dnsConnectState = e.UserToken as DnsConnectState;
			if (e.SocketError == SocketError.Success)
			{
				ClearSocketAsyncEventArgs(e);
				dnsConnectState.Callback((Socket)sender, dnsConnectState.State, e, null);
				return;
			}
			if (e.SocketError != SocketError.HostUnreachable && e.SocketError != SocketError.ConnectionRefused)
			{
				ClearSocketAsyncEventArgs(e);
				dnsConnectState.Callback(null, dnsConnectState.State, e, null);
				return;
			}
			Socket attempSocket;
			IPAddress nextAddress = GetNextAddress(dnsConnectState, out attempSocket);
			if (nextAddress == null)
			{
				ClearSocketAsyncEventArgs(e);
				e.SocketError = SocketError.HostUnreachable;
				dnsConnectState.Callback(null, dnsConnectState.State, e, null);
				return;
			}
			e.RemoteEndPoint = new IPEndPoint(nextAddress, dnsConnectState.Port);
			if (!attempSocket.ConnectAsync(e))
			{
				SocketConnectCompleted(attempSocket, e);
			}
		}

		private static void ClearSocketAsyncEventArgs(SocketAsyncEventArgs e)
		{
			e.Completed -= SocketConnectCompleted;
			e.UserToken = null;
		}
	}
}
