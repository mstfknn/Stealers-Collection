using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	public abstract class ClientSession : IClientSession, IBufferSetter
	{
		public const int DefaultReceiveBufferSize = 4096;

		private EventHandler m_Closed;

		private EventHandler<ErrorEventArgs> m_Error;

		private EventHandler m_Connected;

		private EventHandler<DataEventArgs> m_DataReceived;

		private DataEventArgs m_DataArgs = new DataEventArgs();

		protected Socket Client
		{
			get;
			set;
		}

		Socket IClientSession.Socket => Client;

		public virtual EndPoint LocalEndPoint
		{
			get;
			set;
		}

		public bool IsConnected
		{
			get;
			private set;
		}

		public bool NoDelay
		{
			get;
			set;
		}

		public int SendingQueueSize
		{
			get;
			set;
		}

		public virtual int ReceiveBufferSize
		{
			get;
			set;
		}

		public IProxyConnector Proxy
		{
			get;
			set;
		}

		protected ArraySegment<byte> Buffer
		{
			get;
			set;
		}

		public event EventHandler Closed
		{
			add
			{
				m_Closed = (EventHandler)Delegate.Combine(m_Closed, value);
			}
			remove
			{
				m_Closed = (EventHandler)Delegate.Remove(m_Closed, value);
			}
		}

		public event EventHandler<ErrorEventArgs> Error
		{
			add
			{
				m_Error = (EventHandler<ErrorEventArgs>)Delegate.Combine(m_Error, value);
			}
			remove
			{
				m_Error = (EventHandler<ErrorEventArgs>)Delegate.Remove(m_Error, value);
			}
		}

		public event EventHandler Connected
		{
			add
			{
				m_Connected = (EventHandler)Delegate.Combine(m_Connected, value);
			}
			remove
			{
				m_Connected = (EventHandler)Delegate.Remove(m_Connected, value);
			}
		}

		public event EventHandler<DataEventArgs> DataReceived
		{
			add
			{
				m_DataReceived = (EventHandler<DataEventArgs>)Delegate.Combine(m_DataReceived, value);
			}
			remove
			{
				m_DataReceived = (EventHandler<DataEventArgs>)Delegate.Remove(m_DataReceived, value);
			}
		}

		public ClientSession()
		{
		}

		public abstract void Connect(EndPoint remoteEndPoint);

		public abstract bool TrySend(ArraySegment<byte> segment);

		public abstract bool TrySend(IList<ArraySegment<byte>> segments);

		public void Send(byte[] data, int offset, int length)
		{
			Send(new ArraySegment<byte>(data, offset, length));
		}

		public void Send(ArraySegment<byte> segment)
		{
			if (!TrySend(segment))
			{
				do
				{
					Thread.SpinWait(1);
				}
				while (!TrySend(segment));
			}
		}

		public void Send(IList<ArraySegment<byte>> segments)
		{
			if (!TrySend(segments))
			{
				do
				{
					Thread.SpinWait(1);
				}
				while (!TrySend(segments));
			}
		}

		public abstract void Close();

		protected virtual void OnClosed()
		{
			IsConnected = false;
			LocalEndPoint = null;
			m_Closed?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnError(Exception e)
		{
			m_Error?.Invoke(this, new ErrorEventArgs(e));
		}

		protected virtual void OnConnected()
		{
			Socket client = Client;
			if (client != null)
			{
				try
				{
					if (client.NoDelay != NoDelay)
					{
						client.NoDelay = NoDelay;
					}
				}
				catch
				{
				}
			}
			IsConnected = true;
			m_Connected?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnDataReceived(byte[] data, int offset, int length)
		{
			EventHandler<DataEventArgs> dataReceived = m_DataReceived;
			if (dataReceived != null)
			{
				m_DataArgs.Data = data;
				m_DataArgs.Offset = offset;
				m_DataArgs.Length = length;
				dataReceived(this, m_DataArgs);
			}
		}

		void IBufferSetter.SetBuffer(ArraySegment<byte> bufferSegment)
		{
			SetBuffer(bufferSegment);
		}

		protected virtual void SetBuffer(ArraySegment<byte> bufferSegment)
		{
			Buffer = bufferSegment;
		}
	}
}
