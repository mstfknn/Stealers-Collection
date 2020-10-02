using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Threading;
using NoiseMe.Drags.App.Models.SuperSocket.ProtoBase;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x02000120 RID: 288
	public abstract class EasyClientBase
	{
		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00006F20 File Offset: 0x00005120
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x00006F28 File Offset: 0x00005128
		protected IPipelineProcessor PipeLineProcessor { get; set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00006F31 File Offset: 0x00005131
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x00006F39 File Offset: 0x00005139
		public int ReceiveBufferSize { get; set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00006F42 File Offset: 0x00005142
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x00006F59 File Offset: 0x00005159
		public EndPoint LocalEndPoint
		{
			get
			{
				if (this.m_LocalEndPoint != null)
				{
					return this.m_LocalEndPoint;
				}
				return this.m_EndPointToBind;
			}
			set
			{
				this.m_EndPointToBind = value;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x00006F62 File Offset: 0x00005162
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x00006F6A File Offset: 0x0000516A
		public bool NoDelay { get; set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00006F73 File Offset: 0x00005173
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x00006F7B File Offset: 0x0000517B
		public SecurityOption Security { get; set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x00006F84 File Offset: 0x00005184
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x00006F8C File Offset: 0x0000518C
		public IProxyConnector Proxy { get; set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0001E2D4 File Offset: 0x0001C4D4
		public Socket Socket
		{
			get
			{
				IClientSession session = this.m_Session;
				if (session == null)
				{
					return null;
				}
				return session.Socket;
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00006F95 File Offset: 0x00005195
		public EasyClientBase()
		{
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x00006FA9 File Offset: 0x000051A9
		public bool IsConnected
		{
			get
			{
				return this.m_Connected;
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0001E2F4 File Offset: 0x0001C4F4
		private TcpClientSession GetUnderlyingSession()
		{
			SecurityOption security = this.Security;
			if (security == null || security.EnabledSslProtocols == SslProtocols.None)
			{
				return new AsyncTcpSession();
			}
			return new SslStreamTcpSession
			{
				Security = security
			};
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001E328 File Offset: 0x0001C528
		public void BeginConnect(EndPoint remoteEndPoint)
		{
			if (this.PipeLineProcessor == null)
			{
				throw new Exception("This client has not been initialized.");
			}
			TcpClientSession underlyingSession = this.GetUnderlyingSession();
			EndPoint endPointToBind = this.m_EndPointToBind;
			if (endPointToBind != null)
			{
				underlyingSession.LocalEndPoint = endPointToBind;
			}
			underlyingSession.NoDelay = this.NoDelay;
			if (this.Proxy != null)
			{
				underlyingSession.Proxy = this.Proxy;
			}
			underlyingSession.Connected += this.OnSessionConnected;
			underlyingSession.Error += this.OnSessionError;
			underlyingSession.Closed += this.OnSessionClosed;
			underlyingSession.DataReceived += this.OnSessionDataReceived;
			if (this.ReceiveBufferSize > 0)
			{
				underlyingSession.ReceiveBufferSize = this.ReceiveBufferSize;
			}
			this.m_Session = underlyingSession;
			underlyingSession.Connect(remoteEndPoint);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00006FB1 File Offset: 0x000051B1
		public void Send(byte[] data)
		{
			this.Send(new ArraySegment<byte>(data, 0, data.Length));
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001E3EC File Offset: 0x0001C5EC
		public void Send(ArraySegment<byte> segment)
		{
			IClientSession session = this.m_Session;
			if (!this.m_Connected || session == null)
			{
				throw new Exception("The socket is not connected.");
			}
			session.Send(segment);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0001E420 File Offset: 0x0001C620
		public void Send(List<ArraySegment<byte>> segments)
		{
			IClientSession session = this.m_Session;
			if (!this.m_Connected || session == null)
			{
				throw new Exception("The socket is not connected.");
			}
			session.Send(segments);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0001E454 File Offset: 0x0001C654
		public void Close()
		{
			IClientSession session = this.m_Session;
			if (session != null && this.m_Connected)
			{
				session.Close();
			}
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0001E47C File Offset: 0x0001C67C
		private void OnSessionDataReceived(object sender, DataEventArgs e)
		{
			ProcessResult processResult;
			try
			{
				processResult = this.PipeLineProcessor.Process(new ArraySegment<byte>(e.Data, e.Offset, e.Length));
			}
			catch (Exception e2)
			{
				this.OnError(e2);
				this.m_Session.Close();
				return;
			}
			if (processResult.State == ProcessState.Error)
			{
				this.m_Session.Close();
				return;
			}
			if (processResult.State == ProcessState.Cached)
			{
				IClientSession session = this.m_Session;
				if (session != null)
				{
					IBufferSetter bufferSetter = session as IBufferSetter;
					if (bufferSetter != null)
					{
						bufferSetter.SetBuffer(new ArraySegment<byte>(new byte[session.ReceiveBufferSize]));
					}
				}
			}
			if (processResult.Packages != null && processResult.Packages.Count > 0)
			{
				foreach (IPackageInfo package in processResult.Packages)
				{
					this.HandlePackage(package);
				}
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00006FC3 File Offset: 0x000051C3
		private void OnSessionError(object sender, ErrorEventArgs e)
		{
			if (!this.m_Connected)
			{
				this.m_ConnectEvent.Set();
			}
			this.OnError(e);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00006FE0 File Offset: 0x000051E0
		private void OnError(Exception e)
		{
			this.OnError(new ErrorEventArgs(e));
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001E57C File Offset: 0x0001C77C
		private void OnError(ErrorEventArgs args)
		{
			EventHandler<ErrorEventArgs> error = this.Error;
			if (error != null)
			{
				error(this, args);
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060008FA RID: 2298 RVA: 0x0001E59C File Offset: 0x0001C79C
		// (remove) Token: 0x060008FB RID: 2299 RVA: 0x0001E5D4 File Offset: 0x0001C7D4
		public event EventHandler<ErrorEventArgs> Error;

		// Token: 0x060008FC RID: 2300 RVA: 0x0001E60C File Offset: 0x0001C80C
		private void OnSessionClosed(object sender, EventArgs e)
		{
			this.m_Connected = false;
			this.m_LocalEndPoint = null;
			IPipelineProcessor pipeLineProcessor = this.PipeLineProcessor;
			if (pipeLineProcessor != null)
			{
				pipeLineProcessor.Reset();
			}
			EventHandler closed = this.Closed;
			if (closed != null)
			{
				closed(this, EventArgs.Empty);
			}
			this.m_ConnectEvent.Set();
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060008FD RID: 2301 RVA: 0x0001E65C File Offset: 0x0001C85C
		// (remove) Token: 0x060008FE RID: 2302 RVA: 0x0001E694 File Offset: 0x0001C894
		public event EventHandler Closed;

		// Token: 0x060008FF RID: 2303 RVA: 0x0001E6CC File Offset: 0x0001C8CC
		private void OnSessionConnected(object sender, EventArgs e)
		{
			this.m_Connected = true;
			TcpClientSession tcpClientSession = sender as TcpClientSession;
			if (tcpClientSession != null)
			{
				this.m_LocalEndPoint = tcpClientSession.LocalEndPoint;
			}
			this.m_ConnectEvent.Set();
			EventHandler connected = this.Connected;
			if (connected != null)
			{
				connected(this, EventArgs.Empty);
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000900 RID: 2304 RVA: 0x0001E718 File Offset: 0x0001C918
		// (remove) Token: 0x06000901 RID: 2305 RVA: 0x0001E750 File Offset: 0x0001C950
		public event EventHandler Connected;

		// Token: 0x06000902 RID: 2306
		protected abstract void HandlePackage(IPackageInfo package);

		// Token: 0x0400036A RID: 874
		private IClientSession m_Session;

		// Token: 0x0400036B RID: 875
		private AutoResetEvent m_ConnectEvent = new AutoResetEvent(false);

		// Token: 0x0400036C RID: 876
		private bool m_Connected;

		// Token: 0x0400036F RID: 879
		private EndPoint m_EndPointToBind;

		// Token: 0x04000370 RID: 880
		private EndPoint m_LocalEndPoint;
	}
}
