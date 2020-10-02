using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Havens.Models.Local.Extensions;
using MailRy.Net;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine.Proxy
{
	// Token: 0x02000122 RID: 290
	public class HttpConnectProxy : ProxyConnectorBase
	{
		// Token: 0x06000907 RID: 2311 RVA: 0x00007024 File Offset: 0x00005224
		public HttpConnectProxy(EndPoint proxyEndPoint) : this(proxyEndPoint, 128, null)
		{
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00007033 File Offset: 0x00005233
		public HttpConnectProxy(EndPoint proxyEndPoint, string targetHostName) : this(proxyEndPoint, 128, targetHostName)
		{
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00007042 File Offset: 0x00005242
		public HttpConnectProxy(EndPoint proxyEndPoint, int receiveBufferSize, string targetHostName) : base(proxyEndPoint, targetHostName)
		{
			this.m_ReceiveBufferSize = receiveBufferSize;
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0001E788 File Offset: 0x0001C988
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

		// Token: 0x0600090B RID: 2315 RVA: 0x0001E804 File Offset: 0x0001CA04
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
			string s;
			if (targetEndPoint is DnsEndPoint)
			{
				DnsEndPoint dnsEndPoint = (DnsEndPoint)targetEndPoint;
				s = string.Format("CONNECT {0}:{1} HTTP/1.1\r\nHost: {0}:{1}\r\nProxy-Connection: Keep-Alive\r\n\r\n", dnsEndPoint.Host, dnsEndPoint.Port);
			}
			else
			{
				IPEndPoint ipendPoint = (IPEndPoint)targetEndPoint;
				s = string.Format("CONNECT {0}:{1} HTTP/1.1\r\nHost: {0}:{1}\r\nProxy-Connection: Keep-Alive\r\n\r\n", ipendPoint.Address, ipendPoint.Port);
			}
			byte[] bytes = ProxyConnectorBase.ASCIIEncoding.GetBytes(s);
			e.Completed += base.AsyncEventArgsCompleted;
			e.UserToken = new HttpConnectProxy.ConnectContext
			{
				Socket = socket,
				SearchState = new SearchMarkState<byte>(HttpConnectProxy.m_LineSeparator)
			};
			e.SetBuffer(bytes, 0, bytes.Length);
			base.StartSend(socket, e);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0001E8EC File Offset: 0x0001CAEC
		protected override void ProcessSend(SocketAsyncEventArgs e)
		{
			if (!base.ValidateAsyncResult(e))
			{
				return;
			}
			HttpConnectProxy.ConnectContext connectContext = (HttpConnectProxy.ConnectContext)e.UserToken;
			byte[] array = new byte[this.m_ReceiveBufferSize];
			e.SetBuffer(array, 0, array.Length);
			this.StartReceive(connectContext.Socket, e);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0001E934 File Offset: 0x0001CB34
		protected override void ProcessReceive(SocketAsyncEventArgs e)
		{
			if (!base.ValidateAsyncResult(e))
			{
				return;
			}
			HttpConnectProxy.ConnectContext connectContext = (HttpConnectProxy.ConnectContext)e.UserToken;
			int matched = connectContext.SearchState.Matched;
			int num = e.Buffer.SearchMark(e.Offset, e.BytesTransferred, connectContext.SearchState);
			if (num < 0)
			{
				int num2 = e.Offset + e.BytesTransferred;
				if (num2 >= this.m_ReceiveBufferSize)
				{
					base.OnException("receive buffer size has been exceeded");
					return;
				}
				e.SetBuffer(num2, this.m_ReceiveBufferSize - num2);
				this.StartReceive(connectContext.Socket, e);
				return;
			}
			else
			{
				int num3 = (matched > 0) ? (e.Offset - matched) : (e.Offset + num);
				if (e.Offset + e.BytesTransferred > num3 + HttpConnectProxy.m_LineSeparator.Length)
				{
					base.OnException("protocol error: more data has been received");
					return;
				}
				string text = new StringReader(ProxyConnectorBase.ASCIIEncoding.GetString(e.Buffer, 0, num3)).ReadLine();
				if (string.IsNullOrEmpty(text))
				{
					base.OnException("protocol error: invalid response");
					return;
				}
				int num4 = text.IndexOf(' ');
				if (num4 <= 0 || text.Length <= num4 + 2)
				{
					base.OnException("protocol error: invalid response");
					return;
				}
				string value = text.Substring(0, num4);
				if (!"HTTP/1.1".Equals(value))
				{
					base.OnException("protocol error: invalid protocol");
					return;
				}
				int num5 = text.IndexOf(' ', num4 + 1);
				if (num5 < 0)
				{
					base.OnException("protocol error: invalid response");
					return;
				}
				int num6;
				if (!int.TryParse(text.Substring(num4 + 1, num5 - num4 - 1), out num6) || num6 > 299 || num6 < 200)
				{
					base.OnException("the proxy server refused the connection");
					return;
				}
				base.OnCompleted(new ProxyEventArgs(connectContext.Socket, base.TargetHostHame));
				return;
			}
		}

		// Token: 0x04000378 RID: 888
		private const string m_RequestTemplate = "CONNECT {0}:{1} HTTP/1.1\r\nHost: {0}:{1}\r\nProxy-Connection: Keep-Alive\r\n\r\n";

		// Token: 0x04000379 RID: 889
		private const string m_ResponsePrefix = "HTTP/1.1";

		// Token: 0x0400037A RID: 890
		private const char m_Space = ' ';

		// Token: 0x0400037B RID: 891
		private static byte[] m_LineSeparator = ProxyConnectorBase.ASCIIEncoding.GetBytes("\r\n\r\n");

		// Token: 0x0400037C RID: 892
		private int m_ReceiveBufferSize;

		// Token: 0x02000123 RID: 291
		private class ConnectContext
		{
			// Token: 0x1700024F RID: 591
			// (get) Token: 0x0600090E RID: 2318 RVA: 0x00007053 File Offset: 0x00005253
			// (set) Token: 0x0600090F RID: 2319 RVA: 0x0000705B File Offset: 0x0000525B
			public Socket Socket { get; set; }

			// Token: 0x17000250 RID: 592
			// (get) Token: 0x06000910 RID: 2320 RVA: 0x00007064 File Offset: 0x00005264
			// (set) Token: 0x06000911 RID: 2321 RVA: 0x0000706C File Offset: 0x0000526C
			public SearchMarkState<byte> SearchState { get; set; }
		}
	}
}
