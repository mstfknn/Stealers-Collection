using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine.Proxy
{
	public abstract class ProxyConnectorBase : IProxyConnector
	{
		protected static Encoding ASCIIEncoding = new ASCIIEncoding();

		private EventHandler<ProxyEventArgs> m_Completed;

		public EndPoint ProxyEndPoint
		{
			get;
			private set;
		}

		public string TargetHostHame
		{
			get;
			private set;
		}

		public event EventHandler<ProxyEventArgs> Completed
		{
			add
			{
				m_Completed = (EventHandler<ProxyEventArgs>)Delegate.Combine(m_Completed, value);
			}
			remove
			{
				m_Completed = (EventHandler<ProxyEventArgs>)Delegate.Remove(m_Completed, value);
			}
		}

		public ProxyConnectorBase(EndPoint proxyEndPoint)
			: this(proxyEndPoint, null)
		{
		}

		public ProxyConnectorBase(EndPoint proxyEndPoint, string targetHostHame)
		{
			ProxyEndPoint = proxyEndPoint;
			TargetHostHame = targetHostHame;
		}

		public abstract void Connect(EndPoint remoteEndPoint);

		protected void OnCompleted(ProxyEventArgs args)
		{
			if (m_Completed != null)
			{
				m_Completed(this, args);
			}
		}

		protected void OnException(Exception exception)
		{
			OnCompleted(new ProxyEventArgs(exception));
		}

		protected void OnException(string exception)
		{
			OnCompleted(new ProxyEventArgs(new Exception(exception)));
		}

		protected bool ValidateAsyncResult(SocketAsyncEventArgs e)
		{
			if (e.SocketError != 0)
			{
				SocketException ex = new SocketException((int)e.SocketError);
				OnCompleted(new ProxyEventArgs(new Exception(ex.Message, ex)));
				return false;
			}
			return true;
		}

		protected void AsyncEventArgsCompleted(object sender, SocketAsyncEventArgs e)
		{
			if (e.LastOperation == SocketAsyncOperation.Send)
			{
				ProcessSend(e);
			}
			else
			{
				ProcessReceive(e);
			}
		}

		protected void StartSend(Socket socket, SocketAsyncEventArgs e)
		{
			bool flag = false;
			try
			{
				flag = socket.SendAsync(e);
			}
			catch (Exception ex)
			{
				OnException(new Exception(ex.Message, ex));
				return;
			}
			if (!flag)
			{
				ProcessSend(e);
			}
		}

		protected virtual void StartReceive(Socket socket, SocketAsyncEventArgs e)
		{
			bool flag = false;
			try
			{
				flag = socket.ReceiveAsync(e);
			}
			catch (Exception ex)
			{
				OnException(new Exception(ex.Message, ex));
				return;
			}
			if (!flag)
			{
				ProcessReceive(e);
			}
		}

		protected abstract void ProcessConnect(Socket socket, object targetEndPoint, SocketAsyncEventArgs e, Exception exception);

		protected abstract void ProcessSend(SocketAsyncEventArgs e);

		protected abstract void ProcessReceive(SocketAsyncEventArgs e);
	}
}
