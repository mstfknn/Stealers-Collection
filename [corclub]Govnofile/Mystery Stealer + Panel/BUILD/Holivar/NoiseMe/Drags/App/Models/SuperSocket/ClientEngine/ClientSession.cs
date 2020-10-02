using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x02000114 RID: 276
	public abstract class ClientSession : IClientSession, IBufferSetter
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x00006AFE File Offset: 0x00004CFE
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x00006B06 File Offset: 0x00004D06
		protected Socket Client { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x00006B0F File Offset: 0x00004D0F
		Socket IClientSession.Socket
		{
			get
			{
				return this.Client;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x00006B17 File Offset: 0x00004D17
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x00006B1F File Offset: 0x00004D1F
		public virtual EndPoint LocalEndPoint { get; set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x00006B28 File Offset: 0x00004D28
		// (set) Token: 0x06000860 RID: 2144 RVA: 0x00006B30 File Offset: 0x00004D30
		public bool IsConnected { get; private set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x00006B39 File Offset: 0x00004D39
		// (set) Token: 0x06000862 RID: 2146 RVA: 0x00006B41 File Offset: 0x00004D41
		public bool NoDelay { get; set; }

		// Token: 0x06000863 RID: 2147 RVA: 0x00006B4A File Offset: 0x00004D4A
		public ClientSession()
		{
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x00006B5D File Offset: 0x00004D5D
		// (set) Token: 0x06000865 RID: 2149 RVA: 0x00006B65 File Offset: 0x00004D65
		public int SendingQueueSize { get; set; }

		// Token: 0x06000866 RID: 2150
		public abstract void Connect(EndPoint remoteEndPoint);

		// Token: 0x06000867 RID: 2151
		public abstract bool TrySend(ArraySegment<byte> segment);

		// Token: 0x06000868 RID: 2152
		public abstract bool TrySend(IList<ArraySegment<byte>> segments);

		// Token: 0x06000869 RID: 2153 RVA: 0x00006B6E File Offset: 0x00004D6E
		public void Send(byte[] data, int offset, int length)
		{
			this.Send(new ArraySegment<byte>(data, offset, length));
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00006B7E File Offset: 0x00004D7E
		public void Send(ArraySegment<byte> segment)
		{
			if (this.TrySend(segment))
			{
				return;
			}
			do
			{
				Thread.SpinWait(1);
			}
			while (!this.TrySend(segment));
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00006B99 File Offset: 0x00004D99
		public void Send(IList<ArraySegment<byte>> segments)
		{
			if (this.TrySend(segments))
			{
				return;
			}
			do
			{
				Thread.SpinWait(1);
			}
			while (!this.TrySend(segments));
		}

		// Token: 0x0600086C RID: 2156
		public abstract void Close();

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600086D RID: 2157 RVA: 0x00006BB4 File Offset: 0x00004DB4
		// (remove) Token: 0x0600086E RID: 2158 RVA: 0x00006BCD File Offset: 0x00004DCD
		public event EventHandler Closed
		{
			add
			{
				this.m_Closed = (EventHandler)Delegate.Combine(this.m_Closed, value);
			}
			remove
			{
				this.m_Closed = (EventHandler)Delegate.Remove(this.m_Closed, value);
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0001D960 File Offset: 0x0001BB60
		protected virtual void OnClosed()
		{
			this.IsConnected = false;
			this.LocalEndPoint = null;
			EventHandler closed = this.m_Closed;
			if (closed != null)
			{
				closed(this, EventArgs.Empty);
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000870 RID: 2160 RVA: 0x00006BE6 File Offset: 0x00004DE6
		// (remove) Token: 0x06000871 RID: 2161 RVA: 0x00006BFF File Offset: 0x00004DFF
		public event EventHandler<ErrorEventArgs> Error
		{
			add
			{
				this.m_Error = (EventHandler<ErrorEventArgs>)Delegate.Combine(this.m_Error, value);
			}
			remove
			{
				this.m_Error = (EventHandler<ErrorEventArgs>)Delegate.Remove(this.m_Error, value);
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0001D994 File Offset: 0x0001BB94
		protected virtual void OnError(Exception e)
		{
			EventHandler<ErrorEventArgs> error = this.m_Error;
			if (error == null)
			{
				return;
			}
			error(this, new ErrorEventArgs(e));
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000873 RID: 2163 RVA: 0x00006C18 File Offset: 0x00004E18
		// (remove) Token: 0x06000874 RID: 2164 RVA: 0x00006C31 File Offset: 0x00004E31
		public event EventHandler Connected
		{
			add
			{
				this.m_Connected = (EventHandler)Delegate.Combine(this.m_Connected, value);
			}
			remove
			{
				this.m_Connected = (EventHandler)Delegate.Remove(this.m_Connected, value);
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0001D9BC File Offset: 0x0001BBBC
		protected virtual void OnConnected()
		{
			Socket client = this.Client;
			if (client != null)
			{
				try
				{
					if (client.NoDelay != this.NoDelay)
					{
						client.NoDelay = this.NoDelay;
					}
				}
				catch
				{
				}
			}
			this.IsConnected = true;
			EventHandler connected = this.m_Connected;
			if (connected == null)
			{
				return;
			}
			connected(this, EventArgs.Empty);
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000876 RID: 2166 RVA: 0x00006C4A File Offset: 0x00004E4A
		// (remove) Token: 0x06000877 RID: 2167 RVA: 0x00006C63 File Offset: 0x00004E63
		public event EventHandler<DataEventArgs> DataReceived
		{
			add
			{
				this.m_DataReceived = (EventHandler<DataEventArgs>)Delegate.Combine(this.m_DataReceived, value);
			}
			remove
			{
				this.m_DataReceived = (EventHandler<DataEventArgs>)Delegate.Remove(this.m_DataReceived, value);
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0001DA20 File Offset: 0x0001BC20
		protected virtual void OnDataReceived(byte[] data, int offset, int length)
		{
			EventHandler<DataEventArgs> dataReceived = this.m_DataReceived;
			if (dataReceived == null)
			{
				return;
			}
			this.m_DataArgs.Data = data;
			this.m_DataArgs.Offset = offset;
			this.m_DataArgs.Length = length;
			dataReceived(this, this.m_DataArgs);
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x00006C7C File Offset: 0x00004E7C
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x00006C84 File Offset: 0x00004E84
		public virtual int ReceiveBufferSize { get; set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x00006C8D File Offset: 0x00004E8D
		// (set) Token: 0x0600087C RID: 2172 RVA: 0x00006C95 File Offset: 0x00004E95
		public IProxyConnector Proxy { get; set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x00006C9E File Offset: 0x00004E9E
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x00006CA6 File Offset: 0x00004EA6
		protected ArraySegment<byte> Buffer { get; set; }

		// Token: 0x0600087F RID: 2175 RVA: 0x00006CAF File Offset: 0x00004EAF
		void IBufferSetter.SetBuffer(ArraySegment<byte> bufferSegment)
		{
			this.SetBuffer(bufferSegment);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00006CB8 File Offset: 0x00004EB8
		protected virtual void SetBuffer(ArraySegment<byte> bufferSegment)
		{
			this.Buffer = bufferSegment;
		}

		// Token: 0x04000348 RID: 840
		public const int DefaultReceiveBufferSize = 4096;

		// Token: 0x0400034E RID: 846
		private EventHandler m_Closed;

		// Token: 0x0400034F RID: 847
		private EventHandler<ErrorEventArgs> m_Error;

		// Token: 0x04000350 RID: 848
		private EventHandler m_Connected;

		// Token: 0x04000351 RID: 849
		private EventHandler<DataEventArgs> m_DataReceived;

		// Token: 0x04000352 RID: 850
		private DataEventArgs m_DataArgs = new DataEventArgs();
	}
}
