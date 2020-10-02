using System;
using System.Net;
using System.Net.Sockets;
using MailRy.Net;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x02000107 RID: 263
	public static class ConnectAsyncExtension
	{
		// Token: 0x060007F2 RID: 2034 RVA: 0x0001CC84 File Offset: 0x0001AE84
		private static void SocketAsyncEventCompleted(object sender, SocketAsyncEventArgs e)
		{
			e.Completed -= ConnectAsyncExtension.SocketAsyncEventCompleted;
			ConnectAsyncExtension.ConnectToken connectToken = (ConnectAsyncExtension.ConnectToken)e.UserToken;
			e.UserToken = null;
			connectToken.Callback(sender as Socket, connectToken.State, e, null);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x000067BD File Offset: 0x000049BD
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

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001CCD0 File Offset: 0x0001AED0
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

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001CD6C File Offset: 0x0001AF6C
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

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001CDD4 File Offset: 0x0001AFD4
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

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001CF08 File Offset: 0x0001B108
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

		// Token: 0x060007F8 RID: 2040 RVA: 0x000067F6 File Offset: 0x000049F6
		private static void ClearSocketAsyncEventArgs(SocketAsyncEventArgs e)
		{
			e.Completed -= ConnectAsyncExtension.SocketConnectCompleted;
			e.UserToken = null;
		}

		// Token: 0x02000108 RID: 264
		private class ConnectToken
		{
			// Token: 0x17000212 RID: 530
			// (get) Token: 0x060007F9 RID: 2041 RVA: 0x00006811 File Offset: 0x00004A11
			// (set) Token: 0x060007FA RID: 2042 RVA: 0x00006819 File Offset: 0x00004A19
			public object State { get; set; }

			// Token: 0x17000213 RID: 531
			// (get) Token: 0x060007FB RID: 2043 RVA: 0x00006822 File Offset: 0x00004A22
			// (set) Token: 0x060007FC RID: 2044 RVA: 0x0000682A File Offset: 0x00004A2A
			public ConnectedCallback Callback { get; set; }
		}

		// Token: 0x02000109 RID: 265
		private class DnsConnectState
		{
			// Token: 0x17000214 RID: 532
			// (get) Token: 0x060007FE RID: 2046 RVA: 0x00006833 File Offset: 0x00004A33
			// (set) Token: 0x060007FF RID: 2047 RVA: 0x0000683B File Offset: 0x00004A3B
			public IPAddress[] Addresses { get; set; }

			// Token: 0x17000215 RID: 533
			// (get) Token: 0x06000800 RID: 2048 RVA: 0x00006844 File Offset: 0x00004A44
			// (set) Token: 0x06000801 RID: 2049 RVA: 0x0000684C File Offset: 0x00004A4C
			public int NextAddressIndex { get; set; }

			// Token: 0x17000216 RID: 534
			// (get) Token: 0x06000802 RID: 2050 RVA: 0x00006855 File Offset: 0x00004A55
			// (set) Token: 0x06000803 RID: 2051 RVA: 0x0000685D File Offset: 0x00004A5D
			public int Port { get; set; }

			// Token: 0x17000217 RID: 535
			// (get) Token: 0x06000804 RID: 2052 RVA: 0x00006866 File Offset: 0x00004A66
			// (set) Token: 0x06000805 RID: 2053 RVA: 0x0000686E File Offset: 0x00004A6E
			public Socket Socket4 { get; set; }

			// Token: 0x17000218 RID: 536
			// (get) Token: 0x06000806 RID: 2054 RVA: 0x00006877 File Offset: 0x00004A77
			// (set) Token: 0x06000807 RID: 2055 RVA: 0x0000687F File Offset: 0x00004A7F
			public Socket Socket6 { get; set; }

			// Token: 0x17000219 RID: 537
			// (get) Token: 0x06000808 RID: 2056 RVA: 0x00006888 File Offset: 0x00004A88
			// (set) Token: 0x06000809 RID: 2057 RVA: 0x00006890 File Offset: 0x00004A90
			public object State { get; set; }

			// Token: 0x1700021A RID: 538
			// (get) Token: 0x0600080A RID: 2058 RVA: 0x00006899 File Offset: 0x00004A99
			// (set) Token: 0x0600080B RID: 2059 RVA: 0x000068A1 File Offset: 0x00004AA1
			public ConnectedCallback Callback { get; set; }

			// Token: 0x1700021B RID: 539
			// (get) Token: 0x0600080C RID: 2060 RVA: 0x000068AA File Offset: 0x00004AAA
			// (set) Token: 0x0600080D RID: 2061 RVA: 0x000068B2 File Offset: 0x00004AB2
			public EndPoint LocalEndPoint { get; set; }
		}
	}
}
