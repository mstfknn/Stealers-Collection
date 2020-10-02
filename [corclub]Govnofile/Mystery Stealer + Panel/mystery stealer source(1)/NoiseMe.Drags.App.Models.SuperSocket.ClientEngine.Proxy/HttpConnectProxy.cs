using Havens.Models.Local.Extensions;
using MailRy.Net;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine.Proxy
{
	public class HttpConnectProxy : ProxyConnectorBase
	{
		private class ConnectContext
		{
			public Socket Socket
			{
				get;
				set;
			}

			public SearchMarkState<byte> SearchState
			{
				get;
				set;
			}
		}

		private const string m_RequestTemplate = "CONNECT {0}:{1} HTTP/1.1\r\nHost: {0}:{1}\r\nProxy-Connection: Keep-Alive\r\n\r\n";

		private const string m_ResponsePrefix = "HTTP/1.1";

		private const char m_Space = ' ';

		private static byte[] m_LineSeparator;

		private int m_ReceiveBufferSize;

		static HttpConnectProxy()
		{
			m_LineSeparator = ProxyConnectorBase.ASCIIEncoding.GetBytes("\r\n\r\n");
		}

		public HttpConnectProxy(EndPoint proxyEndPoint)
			: this(proxyEndPoint, 128, null)
		{
		}

		public HttpConnectProxy(EndPoint proxyEndPoint, string targetHostName)
			: this(proxyEndPoint, 128, targetHostName)
		{
		}

		public HttpConnectProxy(EndPoint proxyEndPoint, int receiveBufferSize, string targetHostName)
			: base(proxyEndPoint, targetHostName)
		{
			m_ReceiveBufferSize = receiveBufferSize;
		}

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
				base.ProxyEndPoint.ConnectAsync((EndPoint)null, (Havens.Models.Local.Extensions.ConnectedCallback)((ProxyConnectorBase)this).ProcessConnect, (object)remoteEndPoint);
			}
			catch (Exception innerException)
			{
				OnException(new Exception("Failed to connect proxy server", innerException));
			}
		}

		protected override void ProcessConnect(Socket socket, object targetEndPoint, SocketAsyncEventArgs e, Exception exception)
		{
			if (exception != null)
			{
				OnException(exception);
			}
			else
			{
				if (e != null && !ValidateAsyncResult(e))
				{
					return;
				}
				if (socket == null)
				{
					OnException(new SocketException(10053));
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
					IPEndPoint iPEndPoint = (IPEndPoint)targetEndPoint;
					s = string.Format("CONNECT {0}:{1} HTTP/1.1\r\nHost: {0}:{1}\r\nProxy-Connection: Keep-Alive\r\n\r\n", iPEndPoint.Address, iPEndPoint.Port);
				}
				byte[] bytes = ProxyConnectorBase.ASCIIEncoding.GetBytes(s);
				e.Completed += base.AsyncEventArgsCompleted;
				e.UserToken = new ConnectContext
				{
					Socket = socket,
					SearchState = new SearchMarkState<byte>(m_LineSeparator)
				};
				e.SetBuffer(bytes, 0, bytes.Length);
				StartSend(socket, e);
			}
		}

		protected override void ProcessSend(SocketAsyncEventArgs e)
		{
			if (ValidateAsyncResult(e))
			{
				ConnectContext connectContext = (ConnectContext)e.UserToken;
				byte[] array = new byte[m_ReceiveBufferSize];
				e.SetBuffer(array, 0, array.Length);
				StartReceive(connectContext.Socket, e);
			}
		}

		protected override void ProcessReceive(SocketAsyncEventArgs e)
		{
			if (!ValidateAsyncResult(e))
			{
				return;
			}
			ConnectContext connectContext = (ConnectContext)e.UserToken;
			int matched = connectContext.SearchState.Matched;
			int num = e.Buffer.SearchMark(e.Offset, e.BytesTransferred, connectContext.SearchState);
			if (num < 0)
			{
				int num2 = e.Offset + e.BytesTransferred;
				if (num2 >= m_ReceiveBufferSize)
				{
					OnException("receive buffer size has been exceeded");
					return;
				}
				e.SetBuffer(num2, m_ReceiveBufferSize - num2);
				StartReceive(connectContext.Socket, e);
				return;
			}
			int num3 = (matched > 0) ? (e.Offset - matched) : (e.Offset + num);
			if (e.Offset + e.BytesTransferred > num3 + m_LineSeparator.Length)
			{
				OnException("protocol error: more data has been received");
				return;
			}
			string text = new StringReader(ProxyConnectorBase.ASCIIEncoding.GetString(e.Buffer, 0, num3)).ReadLine();
			if (string.IsNullOrEmpty(text))
			{
				OnException("protocol error: invalid response");
				return;
			}
			int num4 = text.IndexOf(' ');
			if (num4 <= 0 || text.Length <= num4 + 2)
			{
				OnException("protocol error: invalid response");
				return;
			}
			string value = text.Substring(0, num4);
			if (!"HTTP/1.1".Equals(value))
			{
				OnException("protocol error: invalid protocol");
				return;
			}
			int num5 = text.IndexOf(' ', num4 + 1);
			int result;
			if (num5 < 0)
			{
				OnException("protocol error: invalid response");
			}
			else if (!int.TryParse(text.Substring(num4 + 1, num5 - num4 - 1), out result) || result > 299 || result < 200)
			{
				OnException("the proxy server refused the connection");
			}
			else
			{
				OnCompleted(new ProxyEventArgs(connectContext.Socket, base.TargetHostHame));
			}
		}
	}
}
