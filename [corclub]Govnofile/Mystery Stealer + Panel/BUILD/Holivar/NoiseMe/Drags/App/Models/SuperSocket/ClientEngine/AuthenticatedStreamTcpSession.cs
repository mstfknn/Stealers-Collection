using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x02000112 RID: 274
	public abstract class AuthenticatedStreamTcpSession : TcpClientSession
	{
		// Token: 0x06000845 RID: 2117 RVA: 0x000069E9 File Offset: 0x00004BE9
		public AuthenticatedStreamTcpSession()
		{
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x00006AA1 File Offset: 0x00004CA1
		// (set) Token: 0x06000847 RID: 2119 RVA: 0x00006AA9 File Offset: 0x00004CA9
		public SecurityOption Security { get; set; }

		// Token: 0x06000848 RID: 2120 RVA: 0x00006AB2 File Offset: 0x00004CB2
		protected override void SocketEventArgsCompleted(object sender, SocketAsyncEventArgs e)
		{
			base.ProcessConnect(sender as Socket, null, e, null);
		}

		// Token: 0x06000849 RID: 2121
		protected abstract void StartAuthenticatedStream(Socket client);

		// Token: 0x0600084A RID: 2122 RVA: 0x0001D540 File Offset: 0x0001B740
		protected override void OnGetSocket(SocketAsyncEventArgs e)
		{
			try
			{
				this.StartAuthenticatedStream(base.Client);
			}
			catch (Exception e2)
			{
				if (!this.IsIgnorableException(e2))
				{
					this.OnError(e2);
				}
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001D580 File Offset: 0x0001B780
		protected void OnAuthenticatedStreamConnected(AuthenticatedStream stream)
		{
			this.m_Stream = stream;
			this.OnConnected();
			if (base.Buffer.Array == null)
			{
				int num = this.ReceiveBufferSize;
				if (num <= 0)
				{
					num = 4096;
				}
				this.ReceiveBufferSize = num;
				base.Buffer = new ArraySegment<byte>(new byte[num]);
			}
			this.BeginRead();
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001D5DC File Offset: 0x0001B7DC
		private void OnDataRead(IAsyncResult result)
		{
			AuthenticatedStreamTcpSession.StreamAsyncState streamAsyncState = result.AsyncState as AuthenticatedStreamTcpSession.StreamAsyncState;
			if (streamAsyncState == null || streamAsyncState.Stream == null)
			{
				this.OnError(new NullReferenceException("Null state or stream."));
				return;
			}
			AuthenticatedStream stream = streamAsyncState.Stream;
			int num = 0;
			try
			{
				num = stream.EndRead(result);
			}
			catch (Exception e)
			{
				if (!this.IsIgnorableException(e))
				{
					this.OnError(e);
				}
				if (base.EnsureSocketClosed(streamAsyncState.Client))
				{
					this.OnClosed();
				}
				return;
			}
			if (num == 0)
			{
				if (base.EnsureSocketClosed(streamAsyncState.Client))
				{
					this.OnClosed();
				}
				return;
			}
			this.OnDataReceived(base.Buffer.Array, base.Buffer.Offset, num);
			this.BeginRead();
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00006AC3 File Offset: 0x00004CC3
		private void BeginRead()
		{
			this.StartRead();
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001D6A0 File Offset: 0x0001B8A0
		private void StartRead()
		{
			Socket client = base.Client;
			if (client == null || this.m_Stream == null)
			{
				return;
			}
			try
			{
				ArraySegment<byte> buffer = base.Buffer;
				this.m_Stream.BeginRead(buffer.Array, buffer.Offset, buffer.Count, new AsyncCallback(this.OnDataRead), new AuthenticatedStreamTcpSession.StreamAsyncState
				{
					Stream = this.m_Stream,
					Client = client
				});
			}
			catch (Exception e)
			{
				if (!this.IsIgnorableException(e))
				{
					this.OnError(e);
				}
				if (base.EnsureSocketClosed(client))
				{
					this.OnClosed();
				}
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001D744 File Offset: 0x0001B944
		protected override bool IsIgnorableException(Exception e)
		{
			if (base.IsIgnorableException(e))
			{
				return true;
			}
			if (e is IOException)
			{
				if (e.InnerException is ObjectDisposedException)
				{
					return true;
				}
				if (e.InnerException is IOException && e.InnerException.InnerException is ObjectDisposedException)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001D798 File Offset: 0x0001B998
		protected override void Sendpublic(PosList<ArraySegment<byte>> items)
		{
			Socket client = base.Client;
			try
			{
				ArraySegment<byte> arraySegment = items[items.Position];
				this.m_Stream.BeginWrite(arraySegment.Array, arraySegment.Offset, arraySegment.Count, new AsyncCallback(this.OnWriteComplete), new AuthenticatedStreamTcpSession.StreamAsyncState
				{
					Stream = this.m_Stream,
					Client = client,
					SendingItems = items
				});
			}
			catch (Exception e)
			{
				if (!this.IsIgnorableException(e))
				{
					this.OnError(e);
				}
				if (base.EnsureSocketClosed(client))
				{
					this.OnClosed();
				}
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001D83C File Offset: 0x0001BA3C
		private void OnWriteComplete(IAsyncResult result)
		{
			AuthenticatedStreamTcpSession.StreamAsyncState streamAsyncState = result.AsyncState as AuthenticatedStreamTcpSession.StreamAsyncState;
			if (streamAsyncState == null || streamAsyncState.Stream == null)
			{
				this.OnError(new NullReferenceException("State of Ssl stream is null."));
				return;
			}
			AuthenticatedStream stream = streamAsyncState.Stream;
			try
			{
				stream.EndWrite(result);
			}
			catch (Exception e)
			{
				if (!this.IsIgnorableException(e))
				{
					this.OnError(e);
				}
				if (base.EnsureSocketClosed(streamAsyncState.Client))
				{
					this.OnClosed();
				}
				return;
			}
			PosList<ArraySegment<byte>> sendingItems = streamAsyncState.SendingItems;
			int num = sendingItems.Position + 1;
			if (num < sendingItems.Count)
			{
				sendingItems.Position = num;
				this.Sendpublic(sendingItems);
				return;
			}
			try
			{
				this.m_Stream.Flush();
			}
			catch (Exception e2)
			{
				if (!this.IsIgnorableException(e2))
				{
					this.OnError(e2);
				}
				if (base.EnsureSocketClosed(streamAsyncState.Client))
				{
					this.OnClosed();
				}
				return;
			}
			base.OnSendingCompleted();
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001D930 File Offset: 0x0001BB30
		public override void Close()
		{
			AuthenticatedStream stream = this.m_Stream;
			if (stream != null)
			{
				stream.Close();
				stream.Dispose();
				this.m_Stream = null;
			}
			base.Close();
		}

		// Token: 0x04000343 RID: 835
		private AuthenticatedStream m_Stream;

		// Token: 0x02000113 RID: 275
		private class StreamAsyncState
		{
			// Token: 0x17000227 RID: 551
			// (get) Token: 0x06000853 RID: 2131 RVA: 0x00006ACB File Offset: 0x00004CCB
			// (set) Token: 0x06000854 RID: 2132 RVA: 0x00006AD3 File Offset: 0x00004CD3
			public AuthenticatedStream Stream { get; set; }

			// Token: 0x17000228 RID: 552
			// (get) Token: 0x06000855 RID: 2133 RVA: 0x00006ADC File Offset: 0x00004CDC
			// (set) Token: 0x06000856 RID: 2134 RVA: 0x00006AE4 File Offset: 0x00004CE4
			public Socket Client { get; set; }

			// Token: 0x17000229 RID: 553
			// (get) Token: 0x06000857 RID: 2135 RVA: 0x00006AED File Offset: 0x00004CED
			// (set) Token: 0x06000858 RID: 2136 RVA: 0x00006AF5 File Offset: 0x00004CF5
			public PosList<ArraySegment<byte>> SendingItems { get; set; }
		}
	}
}
