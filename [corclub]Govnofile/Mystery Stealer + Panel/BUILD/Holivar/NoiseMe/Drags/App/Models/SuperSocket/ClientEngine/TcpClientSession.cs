using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Havens.Models.Local.Extensions;
using MailRy.Net;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x0200011B RID: 283
	public abstract class TcpClientSession : ClientSession
	{
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x00006DEE File Offset: 0x00004FEE
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x00006DF6 File Offset: 0x00004FF6
		private protected string HostName { protected get; private set; }

		// Token: 0x060008BB RID: 2235 RVA: 0x00006DFF File Offset: 0x00004FFF
		public TcpClientSession()
		{
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x00006E07 File Offset: 0x00005007
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x00006E0F File Offset: 0x0000500F
		public override EndPoint LocalEndPoint
		{
			get
			{
				return base.LocalEndPoint;
			}
			set
			{
				if (this.m_InConnecting || base.IsConnected)
				{
					throw new Exception("You cannot set LocalEdnPoint after you start the connection.");
				}
				base.LocalEndPoint = value;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x00006E33 File Offset: 0x00005033
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x0001DC44 File Offset: 0x0001BE44
		public override int ReceiveBufferSize
		{
			get
			{
				return base.ReceiveBufferSize;
			}
			set
			{
				if (base.Buffer.Array != null)
				{
					throw new Exception("ReceiveBufferSize cannot be set after the socket has been connected!");
				}
				base.ReceiveBufferSize = value;
			}
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00006E3B File Offset: 0x0000503B
		protected virtual bool IsIgnorableException(Exception e)
		{
			return e is ObjectDisposedException || e is NullReferenceException;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00006E52 File Offset: 0x00005052
		protected bool IsIgnorableSocketError(int errorCode)
		{
			return errorCode == 10058 || errorCode == 10053 || errorCode == 10054 || errorCode == 995;
		}

		// Token: 0x060008C2 RID: 2242
		protected abstract void SocketEventArgsCompleted(object sender, SocketAsyncEventArgs e);

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001DC74 File Offset: 0x0001BE74
		public override void Connect(EndPoint remoteEndPoint)
		{
			if (remoteEndPoint == null)
			{
				throw new ArgumentNullException("remoteEndPoint");
			}
			DnsEndPoint dnsEndPoint = remoteEndPoint as DnsEndPoint;
			if (dnsEndPoint != null)
			{
				string host = dnsEndPoint.Host;
				if (!string.IsNullOrEmpty(host))
				{
					this.HostName = host;
				}
			}
			if (this.m_InConnecting)
			{
				throw new Exception("The socket is connecting, cannot connect again!");
			}
			if (base.Client != null)
			{
				throw new Exception("The socket is connected, you needn't connect again!");
			}
			if (base.Proxy != null)
			{
				base.Proxy.Completed += this.Proxy_Completed;
				base.Proxy.Connect(remoteEndPoint);
				this.m_InConnecting = true;
				return;
			}
			this.m_InConnecting = true;
			remoteEndPoint.ConnectAsync(this.LocalEndPoint, new ConnectedCallback(this.ProcessConnect), null);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0001DD28 File Offset: 0x0001BF28
		private void Proxy_Completed(object sender, ProxyEventArgs e)
		{
			base.Proxy.Completed -= this.Proxy_Completed;
			if (e.Connected)
			{
				SocketAsyncEventArgs socketAsyncEventArgs = null;
				if (e.TargetHostName != null)
				{
					socketAsyncEventArgs = new SocketAsyncEventArgs();
					socketAsyncEventArgs.RemoteEndPoint = new DnsEndPoint(e.TargetHostName, 0);
				}
				this.ProcessConnect(e.Socket, null, socketAsyncEventArgs, null);
				return;
			}
			this.OnError(new Exception("proxy error", e.Exception));
			this.m_InConnecting = false;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0001DDA4 File Offset: 0x0001BFA4
		protected void ProcessConnect(Socket socket, object state, SocketAsyncEventArgs e, Exception exception)
		{
			if (exception != null)
			{
				this.m_InConnecting = false;
				this.OnError(exception);
				if (e != null)
				{
					e.Dispose();
				}
				return;
			}
			if (e != null && e.SocketError != SocketError.Success)
			{
				this.m_InConnecting = false;
				this.OnError(new SocketException((int)e.SocketError));
				e.Dispose();
				return;
			}
			if (socket == null)
			{
				this.m_InConnecting = false;
				this.OnError(new SocketException(10053));
				return;
			}
			if (!socket.Connected)
			{
				this.m_InConnecting = false;
				SocketError errorCode = SocketError.HostUnreachable;
				try
				{
					errorCode = (SocketError)socket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Error);
				}
				catch (Exception)
				{
					errorCode = SocketError.HostUnreachable;
				}
				this.OnError(new SocketException((int)errorCode));
				return;
			}
			if (e == null)
			{
				e = new SocketAsyncEventArgs();
			}
			e.Completed += this.SocketEventArgsCompleted;
			base.Client = socket;
			this.m_InConnecting = false;
			try
			{
				this.LocalEndPoint = socket.LocalEndPoint;
			}
			catch
			{
			}
			EndPoint endPoint = (e.RemoteEndPoint != null) ? e.RemoteEndPoint : socket.RemoteEndPoint;
			if (string.IsNullOrEmpty(this.HostName))
			{
				this.HostName = this.GetHostOfEndPoint(endPoint);
			}
			else
			{
				DnsEndPoint dnsEndPoint = endPoint as DnsEndPoint;
				if (dnsEndPoint != null)
				{
					string host = dnsEndPoint.Host;
					if (!string.IsNullOrEmpty(host) && !this.HostName.Equals(host, StringComparison.OrdinalIgnoreCase))
					{
						this.HostName = host;
					}
				}
			}
			try
			{
				base.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
			}
			catch
			{
			}
			this.OnGetSocket(e);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0001DF3C File Offset: 0x0001C13C
		private string GetHostOfEndPoint(EndPoint endPoint)
		{
			DnsEndPoint dnsEndPoint = endPoint as DnsEndPoint;
			if (dnsEndPoint != null)
			{
				return dnsEndPoint.Host;
			}
			IPEndPoint ipendPoint = endPoint as IPEndPoint;
			if (ipendPoint != null && ipendPoint.Address != null)
			{
				return ipendPoint.Address.ToString();
			}
			return string.Empty;
		}

		// Token: 0x060008C7 RID: 2247
		protected abstract void OnGetSocket(SocketAsyncEventArgs e);

		// Token: 0x060008C8 RID: 2248 RVA: 0x00006E77 File Offset: 0x00005077
		protected bool EnsureSocketClosed()
		{
			return this.EnsureSocketClosed(null);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0001DF80 File Offset: 0x0001C180
		protected bool EnsureSocketClosed(Socket prevClient)
		{
			Socket socket = base.Client;
			if (socket == null)
			{
				return false;
			}
			bool result = true;
			if (prevClient != null && prevClient != socket)
			{
				socket = prevClient;
				result = false;
			}
			else
			{
				base.Client = null;
				this.m_IsSending = 0;
			}
			try
			{
				socket.Shutdown(SocketShutdown.Both);
			}
			catch
			{
			}
			finally
			{
				try
				{
					socket.Close();
				}
				catch
				{
				}
			}
			return result;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00006E80 File Offset: 0x00005080
		private bool DetectConnected()
		{
			if (base.Client != null)
			{
				return true;
			}
			this.OnError(new SocketException(10057));
			return false;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0001DFFC File Offset: 0x0001C1FC
		private IBatchQueue<ArraySegment<byte>> GetSendingQueue()
		{
			if (this.m_SendingQueue != null)
			{
				return this.m_SendingQueue;
			}
			IBatchQueue<ArraySegment<byte>> sendingQueue;
			lock (this)
			{
				if (this.m_SendingQueue != null)
				{
					sendingQueue = this.m_SendingQueue;
				}
				else
				{
					this.m_SendingQueue = new ConcurrentBatchQueue<ArraySegment<byte>>(Math.Max(base.SendingQueueSize, 1024), (ArraySegment<byte> t) => t.Array == null || t.Count == 0);
					sendingQueue = this.m_SendingQueue;
				}
			}
			return sendingQueue;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00006E9D File Offset: 0x0000509D
		private PosList<ArraySegment<byte>> GetSendingItems()
		{
			if (this.m_SendingItems == null)
			{
				this.m_SendingItems = new PosList<ArraySegment<byte>>();
			}
			return this.m_SendingItems;
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00006EB8 File Offset: 0x000050B8
		protected bool IsSending
		{
			get
			{
				return this.m_IsSending == 1;
			}
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0001E08C File Offset: 0x0001C28C
		public override bool TrySend(ArraySegment<byte> segment)
		{
			if (segment.Array == null || segment.Count == 0)
			{
				throw new Exception("The data to be sent cannot be empty.");
			}
			if (!this.DetectConnected())
			{
				return true;
			}
			bool result = this.GetSendingQueue().Enqueue(segment);
			if (Interlocked.CompareExchange(ref this.m_IsSending, 1, 0) != 0)
			{
				return result;
			}
			this.DequeueSend();
			return result;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0001E0E8 File Offset: 0x0001C2E8
		public override bool TrySend(IList<ArraySegment<byte>> segments)
		{
			if (segments == null || segments.Count == 0)
			{
				throw new ArgumentNullException("segments");
			}
			for (int i = 0; i < segments.Count; i++)
			{
				if (segments[i].Count == 0)
				{
					throw new Exception("The data piece to be sent cannot be empty.");
				}
			}
			if (!this.DetectConnected())
			{
				return true;
			}
			bool result = this.GetSendingQueue().Enqueue(segments);
			if (Interlocked.CompareExchange(ref this.m_IsSending, 1, 0) != 0)
			{
				return result;
			}
			this.DequeueSend();
			return result;
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0001E168 File Offset: 0x0001C368
		private void DequeueSend()
		{
			PosList<ArraySegment<byte>> sendingItems = this.GetSendingItems();
			if (!this.m_SendingQueue.TryDequeue(sendingItems))
			{
				this.m_IsSending = 0;
				return;
			}
			this.Sendpublic(sendingItems);
		}

		// Token: 0x060008D1 RID: 2257
		protected abstract void Sendpublic(PosList<ArraySegment<byte>> items);

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001E19C File Offset: 0x0001C39C
		protected void OnSendingCompleted()
		{
			PosList<ArraySegment<byte>> sendingItems = this.GetSendingItems();
			sendingItems.Clear();
			sendingItems.Position = 0;
			if (!this.m_SendingQueue.TryDequeue(sendingItems))
			{
				this.m_IsSending = 0;
				return;
			}
			this.Sendpublic(sendingItems);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00006EC3 File Offset: 0x000050C3
		public override void Close()
		{
			if (this.EnsureSocketClosed())
			{
				this.OnClosed();
			}
		}

		// Token: 0x04000361 RID: 865
		private bool m_InConnecting;

		// Token: 0x04000362 RID: 866
		private IBatchQueue<ArraySegment<byte>> m_SendingQueue;

		// Token: 0x04000363 RID: 867
		private PosList<ArraySegment<byte>> m_SendingItems;

		// Token: 0x04000364 RID: 868
		private int m_IsSending;
	}
}
