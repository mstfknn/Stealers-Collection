using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Havens.Models.Local.Extensions;
using MailRy.Net;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine.Proxy
{
	// Token: 0x02000128 RID: 296
	public class Socks5Connector : ProxyConnectorBase
	{
		// Token: 0x0600093A RID: 2362 RVA: 0x000071D6 File Offset: 0x000053D6
		public Socks5Connector(EndPoint proxyEndPoint) : base(proxyEndPoint)
		{
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0001EFE4 File Offset: 0x0001D1E4
		public Socks5Connector(EndPoint proxyEndPoint, string username, string password) : base(proxyEndPoint)
		{
			if (string.IsNullOrEmpty(username))
			{
				throw new ArgumentNullException("username");
			}
			byte[] array = new byte[3 + ProxyConnectorBase.ASCIIEncoding.GetMaxByteCount(username.Length) + (string.IsNullOrEmpty(password) ? 0 : ProxyConnectorBase.ASCIIEncoding.GetMaxByteCount(password.Length))];
			array[0] = 5;
			int bytes = ProxyConnectorBase.ASCIIEncoding.GetBytes(username, 0, username.Length, array, 2);
			if (bytes > 255)
			{
				throw new ArgumentException("the length of username cannot exceed 255", "username");
			}
			array[1] = (byte)bytes;
			int num = bytes + 2;
			if (!string.IsNullOrEmpty(password))
			{
				bytes = ProxyConnectorBase.ASCIIEncoding.GetBytes(password, 0, password.Length, array, num + 1);
				if (bytes > 255)
				{
					throw new ArgumentException("the length of password cannot exceed 255", "password");
				}
				array[num] = (byte)bytes;
				num += bytes + 1;
			}
			else
			{
				array[num] = 0;
				num++;
			}
			this.m_UserNameAuthenRequest = new ArraySegment<byte>(array, 0, num);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0001E788 File Offset: 0x0001C988
		public override void Connect(EndPoint remoteEndPoint)
		{
			if (remoteEndPoint == null)
			{
				throw new ArgumentNullException("remoteEndPoint");
			}
			if (!(remoteEndPoint is IPEndPoint) && !(remoteEndPoint is DnsEndPoint))
			{
				throw new ArgumentException("remoteEndPoint must be IPEndPoint or DnsEndPoint", "remoteEndPoint");
			}
			try
			{
				base.ProxyEndPoint.ConnectAsync(null, new ConnectedCallback(this.ProcessConnect), remoteEndPoint);
			}
			catch (Exception innerException)
			{
				base.OnException(new Exception("Failed to connect proxy server", innerException));
			}
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0001F0D8 File Offset: 0x0001D2D8
		protected override void ProcessConnect(Socket socket, object targetEndPoint, SocketAsyncEventArgs e, Exception exception)
		{
			if (exception != null)
			{
				base.OnException(exception);
				return;
			}
			if (e != null && !base.ValidateAsyncResult(e))
			{
				return;
			}
			if (socket == null)
			{
				base.OnException(new SocketException(10053));
				return;
			}
			if (e == null)
			{
				e = new SocketAsyncEventArgs();
			}
			e.UserToken = new Socks5Connector.SocksContext
			{
				TargetEndPoint = (EndPoint)targetEndPoint,
				Socket = socket,
				State = Socks5Connector.SocksState.NotAuthenticated
			};
			e.Completed += base.AsyncEventArgsCompleted;
			e.SetBuffer(Socks5Connector.m_AuthenHandshake, 0, Socks5Connector.m_AuthenHandshake.Length);
			base.StartSend(socket, e);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0001F170 File Offset: 0x0001D370
		protected override void ProcessSend(SocketAsyncEventArgs e)
		{
			if (!base.ValidateAsyncResult(e))
			{
				return;
			}
			Socks5Connector.SocksContext socksContext = e.UserToken as Socks5Connector.SocksContext;
			if (socksContext.State == Socks5Connector.SocksState.NotAuthenticated)
			{
				e.SetBuffer(0, 2);
				this.StartReceive(socksContext.Socket, e);
				return;
			}
			if (socksContext.State == Socks5Connector.SocksState.Authenticating)
			{
				e.SetBuffer(0, 2);
				this.StartReceive(socksContext.Socket, e);
				return;
			}
			e.SetBuffer(0, e.Buffer.Length);
			this.StartReceive(socksContext.Socket, e);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0001F1EC File Offset: 0x0001D3EC
		private bool ProcessAuthenticationResponse(Socket socket, SocketAsyncEventArgs e)
		{
			int num = e.BytesTransferred + e.Offset;
			if (num < 2)
			{
				e.SetBuffer(num, 2 - num);
				this.StartReceive(socket, e);
				return false;
			}
			if (num > 2)
			{
				base.OnException("received length exceeded");
				return false;
			}
			if (e.Buffer[0] != 5)
			{
				base.OnException("invalid protocol version");
				return false;
			}
			return true;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0001F24C File Offset: 0x0001D44C
		protected override void ProcessReceive(SocketAsyncEventArgs e)
		{
			if (!base.ValidateAsyncResult(e))
			{
				return;
			}
			Socks5Connector.SocksContext socksContext = (Socks5Connector.SocksContext)e.UserToken;
			if (socksContext.State == Socks5Connector.SocksState.NotAuthenticated)
			{
				if (!this.ProcessAuthenticationResponse(socksContext.Socket, e))
				{
					return;
				}
				byte b = e.Buffer[1];
				if (b == 0)
				{
					socksContext.State = Socks5Connector.SocksState.Authenticated;
					this.SendHandshake(e);
					return;
				}
				if (b == 2)
				{
					socksContext.State = Socks5Connector.SocksState.Authenticating;
					this.AutheticateWithUserNamePassword(e);
					return;
				}
				if (b == 255)
				{
					base.OnException("no acceptable methods were offered");
					return;
				}
				base.OnException("protocol error");
				return;
			}
			else if (socksContext.State == Socks5Connector.SocksState.Authenticating)
			{
				if (!this.ProcessAuthenticationResponse(socksContext.Socket, e))
				{
					return;
				}
				if (e.Buffer[1] == 0)
				{
					socksContext.State = Socks5Connector.SocksState.Authenticated;
					this.SendHandshake(e);
					return;
				}
				base.OnException("authentication failure");
				return;
			}
			else
			{
				byte[] array = new byte[e.BytesTransferred];
				Buffer.BlockCopy(e.Buffer, e.Offset, array, 0, e.BytesTransferred);
				socksContext.ReceivedData.AddRange(array);
				if (socksContext.ExpectedLength > socksContext.ReceivedData.Count)
				{
					this.StartReceive(socksContext.Socket, e);
					return;
				}
				if (socksContext.State != Socks5Connector.SocksState.FoundLength)
				{
					byte b2 = socksContext.ReceivedData[3];
					int num;
					if (b2 == 1)
					{
						num = 10;
					}
					else if (b2 == 3)
					{
						num = (int)(7 + socksContext.ReceivedData[4]);
					}
					else
					{
						num = 22;
					}
					if (socksContext.ReceivedData.Count < num)
					{
						socksContext.ExpectedLength = num;
						this.StartReceive(socksContext.Socket, e);
						return;
					}
					if (socksContext.ReceivedData.Count > num)
					{
						base.OnException("response length exceeded");
						return;
					}
					this.OnGetFullResponse(socksContext);
					return;
				}
				else
				{
					if (socksContext.ReceivedData.Count > socksContext.ExpectedLength)
					{
						base.OnException("response length exceeded");
						return;
					}
					this.OnGetFullResponse(socksContext);
					return;
				}
			}
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0001F410 File Offset: 0x0001D610
		private void OnGetFullResponse(Socks5Connector.SocksContext context)
		{
			List<byte> receivedData = context.ReceivedData;
			if (receivedData[0] != 5)
			{
				base.OnException("invalid protocol version");
				return;
			}
			byte b = receivedData[1];
			if (b == 0)
			{
				base.OnCompleted(new ProxyEventArgs(context.Socket));
				return;
			}
			string exception = string.Empty;
			switch (b)
			{
			case 2:
				exception = "connection not allowed by ruleset";
				break;
			case 3:
				exception = "network unreachable";
				break;
			case 4:
				exception = "host unreachable";
				break;
			case 5:
				exception = "connection refused by destination host";
				break;
			case 6:
				exception = "TTL expired";
				break;
			case 7:
				exception = "command not supported / protocol error";
				break;
			case 8:
				exception = "address type not supported";
				break;
			default:
				exception = "general failure";
				break;
			}
			base.OnException(exception);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001F4C8 File Offset: 0x0001D6C8
		private void SendHandshake(SocketAsyncEventArgs e)
		{
			Socks5Connector.SocksContext socksContext = e.UserToken as Socks5Connector.SocksContext;
			EndPoint targetEndPoint = socksContext.TargetEndPoint;
			int port;
			byte[] array;
			int num;
			if (targetEndPoint is IPEndPoint)
			{
				IPEndPoint ipendPoint = targetEndPoint as IPEndPoint;
				port = ipendPoint.Port;
				if (ipendPoint.AddressFamily == AddressFamily.InterNetwork)
				{
					array = new byte[10];
					array[3] = 1;
					Buffer.BlockCopy(ipendPoint.Address.GetAddressBytes(), 0, array, 4, 4);
				}
				else
				{
					if (ipendPoint.AddressFamily != AddressFamily.InterNetworkV6)
					{
						base.OnException("unknown address family");
						return;
					}
					array = new byte[22];
					array[3] = 4;
					Buffer.BlockCopy(ipendPoint.Address.GetAddressBytes(), 0, array, 4, 16);
				}
				num = array.Length;
			}
			else
			{
				DnsEndPoint dnsEndPoint = targetEndPoint as DnsEndPoint;
				port = dnsEndPoint.Port;
				array = new byte[7 + ProxyConnectorBase.ASCIIEncoding.GetMaxByteCount(dnsEndPoint.Host.Length)];
				array[3] = 3;
				num = 5;
				num += ProxyConnectorBase.ASCIIEncoding.GetBytes(dnsEndPoint.Host, 0, dnsEndPoint.Host.Length, array, num);
				num += 2;
			}
			array[0] = 5;
			array[1] = 1;
			array[2] = 0;
			array[num - 2] = (byte)(port / 256);
			array[num - 1] = (byte)(port % 256);
			e.SetBuffer(array, 0, num);
			socksContext.ReceivedData = new List<byte>(num + 5);
			socksContext.ExpectedLength = 5;
			base.StartSend(socksContext.Socket, e);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0001F624 File Offset: 0x0001D824
		private void AutheticateWithUserNamePassword(SocketAsyncEventArgs e)
		{
			Socket socket = ((Socks5Connector.SocksContext)e.UserToken).Socket;
			e.SetBuffer(this.m_UserNameAuthenRequest.Array, this.m_UserNameAuthenRequest.Offset, this.m_UserNameAuthenRequest.Count);
			base.StartSend(socket, e);
		}

		// Token: 0x04000388 RID: 904
		private ArraySegment<byte> m_UserNameAuthenRequest;

		// Token: 0x04000389 RID: 905
		private static byte[] m_AuthenHandshake = new byte[]
		{
			5,
			2,
			0,
			2
		};

		// Token: 0x02000129 RID: 297
		private enum SocksState
		{
			// Token: 0x0400038B RID: 907
			NotAuthenticated,
			// Token: 0x0400038C RID: 908
			Authenticating,
			// Token: 0x0400038D RID: 909
			Authenticated,
			// Token: 0x0400038E RID: 910
			FoundLength,
			// Token: 0x0400038F RID: 911
			Connected
		}

		// Token: 0x0200012A RID: 298
		private class SocksContext
		{
			// Token: 0x17000256 RID: 598
			// (get) Token: 0x06000945 RID: 2373 RVA: 0x000071F7 File Offset: 0x000053F7
			// (set) Token: 0x06000946 RID: 2374 RVA: 0x000071FF File Offset: 0x000053FF
			public Socket Socket { get; set; }

			// Token: 0x17000257 RID: 599
			// (get) Token: 0x06000947 RID: 2375 RVA: 0x00007208 File Offset: 0x00005408
			// (set) Token: 0x06000948 RID: 2376 RVA: 0x00007210 File Offset: 0x00005410
			public Socks5Connector.SocksState State { get; set; }

			// Token: 0x17000258 RID: 600
			// (get) Token: 0x06000949 RID: 2377 RVA: 0x00007219 File Offset: 0x00005419
			// (set) Token: 0x0600094A RID: 2378 RVA: 0x00007221 File Offset: 0x00005421
			public EndPoint TargetEndPoint { get; set; }

			// Token: 0x17000259 RID: 601
			// (get) Token: 0x0600094B RID: 2379 RVA: 0x0000722A File Offset: 0x0000542A
			// (set) Token: 0x0600094C RID: 2380 RVA: 0x00007232 File Offset: 0x00005432
			public List<byte> ReceivedData { get; set; }

			// Token: 0x1700025A RID: 602
			// (get) Token: 0x0600094D RID: 2381 RVA: 0x0000723B File Offset: 0x0000543B
			// (set) Token: 0x0600094E RID: 2382 RVA: 0x00007243 File Offset: 0x00005443
			public int ExpectedLength { get; set; }
		}
	}
}
