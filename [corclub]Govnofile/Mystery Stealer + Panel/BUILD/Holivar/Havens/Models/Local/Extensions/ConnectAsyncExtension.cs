using System;
using System.Net;
using System.Net.Sockets;
using MailRy.Net;

namespace Havens.Models.Local.Extensions
{
	// Token: 0x02000009 RID: 9
	public static class ConnectAsyncExtension
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00009CD4 File Offset: 0x00007ED4
		private static void SocketAsyncEventCompleted(object sender, SocketAsyncEventArgs e)
		{
			e.Completed -= ConnectAsyncExtension.SocketAsyncEventCompleted;
			ConnectAsyncExtension.ConnectToken connectToken = (ConnectAsyncExtension.ConnectToken)e.UserToken;
			e.UserToken = null;
			connectToken.Callback(sender as Socket, connectToken.State, e, null);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000223E File Offset: 0x0000043E
		private static SocketAsyncEventArgs CreateSocketAsyncEventArgs(EndPoint remoteEndPoint, ConnectedCallback callback, object state)
		{
			SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();
			socketAsyncEventArgs.UserToken = new ConnectAsyncExtension.ConnectToken
			{
				State = state,
				Callback = callback
			};
			socketAsyncEventArgs.RemoteEndPoint = remoteEndPoint;
			socketAsyncEventArgs.Completed += ConnectAsyncExtension.SocketAsyncEventCompleted;
			return socketAsyncEventArgs;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002277 File Offset: 0x00000477
		public static void ConnectAsync(this EndPoint remoteEndPoint, EndPoint localEndPoint, ConnectedCallback callback, object state)
		{
			remoteEndPoint.ConnectAsyncpublic(localEndPoint, callback, state);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00009D20 File Offset: 0x00007F20
		private static void ConnectAsyncpublic(this EndPoint remoteEndPoint, EndPoint localEndPoint, ConnectedCallback callback, object state)
		{
			if (remoteEndPoint is DnsEndPoint)
			{
				DnsEndPoint dnsEndPoint = (DnsEndPoint)remoteEndPoint;
				IAsyncResult asyncResult = Dns.BeginGetHostAddresses(dnsEndPoint.Host, new AsyncCallback(ConnectAsyncExtension.OnGetHostAddresses), new ConnectAsyncExtension.DnsConnectState
				{
					Port = dnsEndPoint.Port,
					Callback = callback,
					State = state,
					LocalEndPoint = localEndPoint
				});
				if (asyncResult.CompletedSynchronously)
				{
					ConnectAsyncExtension.OnGetHostAddresses(asyncResult);
					return;
				}
			}
			else
			{
				SocketAsyncEventArgs e = ConnectAsyncExtension.CreateSocketAsyncEventArgs(remoteEndPoint, callback, state);
				Socket socket = new Socket(remoteEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				if (localEndPoint != null)
				{
					socket.ExclusiveAddressUse = false;
					socket.Bind(localEndPoint);
				}
				socket.ConnectAsync(e);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00009DBC File Offset: 0x00007FBC
		private static IPAddress GetNextAddress(ConnectAsyncExtension.DnsConnectState state, out Socket attempSocket)
		{
			IPAddress ipaddress = null;
			attempSocket = null;
			int nextAddressIndex = state.NextAddressIndex;
			while (attempSocket == null)
			{
				if (nextAddressIndex >= state.Addresses.Length)
				{
					return null;
				}
				ipaddress = state.Addresses[nextAddressIndex++];
				if (ipaddress.AddressFamily == AddressFamily.InterNetworkV6)
				{
					attempSocket = state.Socket6;
				}
				else if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
				{
					attempSocket = state.Socket4;
				}
			}
			state.NextAddressIndex = nextAddressIndex;
			return ipaddress;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002282 File Offset: 0x00000482
		private static void CreateAttempSocket(ConnectAsyncExtension.DnsConnectState connectState)
		{
			if (Socket.OSSupportsIPv6)
			{
				connectState.Socket6 = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
			}
			connectState.Socket4 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00009E24 File Offset: 0x00008024
		private static void OnGetHostAddresses(IAsyncResult result)
		{
			ConnectAsyncExtension.DnsConnectState dnsConnectState = result.AsyncState as ConnectAsyncExtension.DnsConnectState;
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
			ConnectAsyncExtension.CreateAttempSocket(dnsConnectState);
			Socket socket;
			IPAddress nextAddress = ConnectAsyncExtension.GetNextAddress(dnsConnectState, out socket);
			if (nextAddress == null)
			{
				dnsConnectState.Callback(null, dnsConnectState.State, null, new SocketException(10047));
				return;
			}
			if (dnsConnectState.LocalEndPoint != null)
			{
				try
				{
					socket.ExclusiveAddressUse = false;
					socket.Bind(dnsConnectState.LocalEndPoint);
				}
				catch (Exception exception2)
				{
					dnsConnectState.Callback(null, dnsConnectState.State, null, exception2);
					return;
				}
			}
			SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();
			socketAsyncEventArgs.Completed += ConnectAsyncExtension.SocketConnectCompleted;
			IPEndPoint remoteEndPoint = new IPEndPoint(nextAddress, dnsConnectState.Port);
			socketAsyncEventArgs.RemoteEndPoint = remoteEndPoint;
			socketAsyncEventArgs.UserToken = dnsConnectState;
			if (!socket.ConnectAsync(socketAsyncEventArgs))
			{
				ConnectAsyncExtension.SocketConnectCompleted(socket, socketAsyncEventArgs);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00009F5C File Offset: 0x0000815C
		private static void SocketConnectCompleted(object sender, SocketAsyncEventArgs e)
		{
			ConnectAsyncExtension.DnsConnectState dnsConnectState = e.UserToken as ConnectAsyncExtension.DnsConnectState;
			if (e.SocketError == SocketError.Success)
			{
				ConnectAsyncExtension.ClearSocketAsyncEventArgs(e);
				dnsConnectState.Callback((Socket)sender, dnsConnectState.State, e, null);
				return;
			}
			if (e.SocketError != SocketError.HostUnreachable && e.SocketError != SocketError.ConnectionRefused)
			{
				ConnectAsyncExtension.ClearSocketAsyncEventArgs(e);
				dnsConnectState.Callback(null, dnsConnectState.State, e, null);
				return;
			}
			Socket socket;
			IPAddress nextAddress = ConnectAsyncExtension.GetNextAddress(dnsConnectState, out socket);
			if (nextAddress == null)
			{
				ConnectAsyncExtension.ClearSocketAsyncEventArgs(e);
				e.SocketError = SocketError.HostUnreachable;
				dnsConnectState.Callback(null, dnsConnectState.State, e, null);
				return;
			}
			e.RemoteEndPoint = new IPEndPoint(nextAddress, dnsConnectState.Port);
			if (!socket.ConnectAsync(e))
			{
				ConnectAsyncExtension.SocketConnectCompleted(socket, e);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022A8 File Offset: 0x000004A8
		private static void ClearSocketAsyncEventArgs(SocketAsyncEventArgs e)
		{
			e.Completed -= ConnectAsyncExtension.SocketConnectCompleted;
			e.UserToken = null;
		}

		// Token: 0x0200000A RID: 10
		private class ConnectToken
		{
			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600001E RID: 30 RVA: 0x000022C3 File Offset: 0x000004C3
			// (set) Token: 0x0600001F RID: 31 RVA: 0x000022CB File Offset: 0x000004CB
			public object State { get; set; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000020 RID: 32 RVA: 0x000022D4 File Offset: 0x000004D4
			// (set) Token: 0x06000021 RID: 33 RVA: 0x000022DC File Offset: 0x000004DC
			public ConnectedCallback Callback { get; set; }
		}

		// Token: 0x0200000B RID: 11
		private class DnsConnectState
		{
			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000023 RID: 35 RVA: 0x000022ED File Offset: 0x000004ED
			// (set) Token: 0x06000024 RID: 36 RVA: 0x000022F5 File Offset: 0x000004F5
			public IPAddress[] Addresses { get; set; }

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000025 RID: 37 RVA: 0x000022FE File Offset: 0x000004FE
			// (set) Token: 0x06000026 RID: 38 RVA: 0x00002306 File Offset: 0x00000506
			public int NextAddressIndex { get; set; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000027 RID: 39 RVA: 0x0000230F File Offset: 0x0000050F
			// (set) Token: 0x06000028 RID: 40 RVA: 0x00002317 File Offset: 0x00000517
			public int Port { get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000029 RID: 41 RVA: 0x00002320 File Offset: 0x00000520
			// (set) Token: 0x0600002A RID: 42 RVA: 0x00002328 File Offset: 0x00000528
			public Socket Socket4 { get; set; }

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x0600002B RID: 43 RVA: 0x00002331 File Offset: 0x00000531
			// (set) Token: 0x0600002C RID: 44 RVA: 0x00002339 File Offset: 0x00000539
			public Socket Socket6 { get; set; }

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x0600002D RID: 45 RVA: 0x00002342 File Offset: 0x00000542
			// (set) Token: 0x0600002E RID: 46 RVA: 0x0000234A File Offset: 0x0000054A
			public object State { get; set; }

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x0600002F RID: 47 RVA: 0x00002353 File Offset: 0x00000553
			// (set) Token: 0x06000030 RID: 48 RVA: 0x0000235B File Offset: 0x0000055B
			public ConnectedCallback Callback { get; set; }

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x06000031 RID: 49 RVA: 0x00002364 File Offset: 0x00000564
			// (set) Token: 0x06000032 RID: 50 RVA: 0x0000236C File Offset: 0x0000056C
			public EndPoint LocalEndPoint { get; set; }
		}
	}
}
