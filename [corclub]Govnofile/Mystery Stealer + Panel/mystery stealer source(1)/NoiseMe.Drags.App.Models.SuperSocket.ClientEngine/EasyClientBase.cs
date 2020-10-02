using NoiseMe.Drags.App.Models.SuperSocket.ProtoBase;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Threading;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	public abstract class EasyClientBase
	{
		private IClientSession m_Session;

		private AutoResetEvent m_ConnectEvent = new AutoResetEvent(initialState: false);

		private bool m_Connected;

		private EndPoint m_EndPointToBind;

		private EndPoint m_LocalEndPoint;

		protected IPipelineProcessor PipeLineProcessor
		{
			get;
			set;
		}

		public int ReceiveBufferSize
		{
			get;
			set;
		}

		public EndPoint LocalEndPoint
		{
			get
			{
				if (m_LocalEndPoint != null)
				{
					return m_LocalEndPoint;
				}
				return m_EndPointToBind;
			}
			set
			{
				m_EndPointToBind = value;
			}
		}

		public bool NoDelay
		{
			get;
			set;
		}

		public SecurityOption Security
		{
			get;
			set;
		}

		public IProxyConnector Proxy
		{
			get;
			set;
		}

		public Socket Socket => m_Session?.Socket;

		public bool IsConnected => m_Connected;

		public event EventHandler<ErrorEventArgs> Error;

		public event EventHandler Closed;

		public event EventHandler Connected;

		public EasyClientBase()
		{
		}

		private TcpClientSession GetUnderlyingSession()
		{
			SecurityOption security = Security;
			if (security == null || security.EnabledSslProtocols == SslProtocols.None)
			{
				return new AsyncTcpSession();
			}
			return new SslStreamTcpSession
			{
				Security = security
			};
		}

		public void BeginConnect(EndPoint remoteEndPoint)
		{
			if (PipeLineProcessor == null)
			{
				throw new Exception("This client has not been initialized.");
			}
			TcpClientSession underlyingSession = GetUnderlyingSession();
			EndPoint endPointToBind = m_EndPointToBind;
			if (endPointToBind != null)
			{
				underlyingSession.LocalEndPoint = endPointToBind;
			}
			underlyingSession.NoDelay = NoDelay;
			if (Proxy != null)
			{
				underlyingSession.Proxy = Proxy;
			}
			underlyingSession.Connected += OnSessionConnected;
			underlyingSession.Error += OnSessionError;
			underlyingSession.Closed += OnSessionClosed;
			underlyingSession.DataReceived += OnSessionDataReceived;
			if (ReceiveBufferSize > 0)
			{
				underlyingSession.ReceiveBufferSize = ReceiveBufferSize;
			}
			m_Session = underlyingSession;
			underlyingSession.Connect(remoteEndPoint);
		}

		public void Send(byte[] data)
		{
			Send(new ArraySegment<byte>(data, 0, data.Length));
		}

		public void Send(ArraySegment<byte> segment)
		{
			IClientSession session = m_Session;
			if (!m_Connected || session == null)
			{
				throw new Exception("The socket is not connected.");
			}
			session.Send(segment);
		}

		public void Send(List<ArraySegment<byte>> segments)
		{
			IClientSession session = m_Session;
			if (!m_Connected || session == null)
			{
				throw new Exception("The socket is not connected.");
			}
			session.Send(segments);
		}

		public void Close()
		{
			IClientSession session = m_Session;
			if (session != null && m_Connected)
			{
				session.Close();
			}
		}

		private void OnSessionDataReceived(object sender, DataEventArgs e)
		{
			ProcessResult processResult;
			try
			{
				processResult = PipeLineProcessor.Process(new ArraySegment<byte>(e.Data, e.Offset, e.Length));
			}
			catch (Exception e2)
			{
				OnError(e2);
				m_Session.Close();
				return;
			}
			if (processResult.State == ProcessState.Error)
			{
				m_Session.Close();
				return;
			}
			if (processResult.State == ProcessState.Cached)
			{
				IClientSession session = m_Session;
				if (session != null)
				{
					(session as IBufferSetter)?.SetBuffer(new ArraySegment<byte>(new byte[session.ReceiveBufferSize]));
				}
			}
			if (processResult.Packages != null && processResult.Packages.Count > 0)
			{
				foreach (IPackageInfo package in processResult.Packages)
				{
					HandlePackage(package);
				}
			}
		}

		private void OnSessionError(object sender, ErrorEventArgs e)
		{
			if (!m_Connected)
			{
				m_ConnectEvent.Set();
			}
			OnError(e);
		}

		private void OnError(Exception e)
		{
			OnError(new ErrorEventArgs(e));
		}

		private void OnError(ErrorEventArgs args)
		{
			this.Error?.Invoke(this, args);
		}

		private void OnSessionClosed(object sender, EventArgs e)
		{
			m_Connected = false;
			m_LocalEndPoint = null;
			PipeLineProcessor?.Reset();
			this.Closed?.Invoke(this, EventArgs.Empty);
			m_ConnectEvent.Set();
		}

		private void OnSessionConnected(object sender, EventArgs e)
		{
			m_Connected = true;
			TcpClientSession tcpClientSession = sender as TcpClientSession;
			if (tcpClientSession != null)
			{
				m_LocalEndPoint = tcpClientSession.LocalEndPoint;
			}
			m_ConnectEvent.Set();
			this.Connected?.Invoke(this, EventArgs.Empty);
		}

		protected abstract void HandlePackage(IPackageInfo package);
	}
}
