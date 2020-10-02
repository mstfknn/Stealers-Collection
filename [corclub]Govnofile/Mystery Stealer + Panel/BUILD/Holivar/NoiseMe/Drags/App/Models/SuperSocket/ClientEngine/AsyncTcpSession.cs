using System;
using System.Net.Sockets;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x02000111 RID: 273
	public class AsyncTcpSession : TcpClientSession
	{
		// Token: 0x0600083C RID: 2108 RVA: 0x000069F1 File Offset: 0x00004BF1
		protected override void SocketEventArgsCompleted(object sender, SocketAsyncEventArgs e)
		{
			if (e.LastOperation == SocketAsyncOperation.Connect)
			{
				base.ProcessConnect(sender as Socket, null, e, null);
				return;
			}
			this.ProcessReceive(e);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00006A13 File Offset: 0x00004C13
		protected override void SetBuffer(ArraySegment<byte> bufferSegment)
		{
			base.SetBuffer(bufferSegment);
			if (this.m_SocketEventArgs != null)
			{
				this.m_SocketEventArgs.SetBuffer(bufferSegment.Array, bufferSegment.Offset, bufferSegment.Count);
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001D1F8 File Offset: 0x0001B3F8
		protected override void OnGetSocket(SocketAsyncEventArgs e)
		{
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
			e.SetBuffer(base.Buffer.Array, base.Buffer.Offset, base.Buffer.Count);
			this.m_SocketEventArgs = e;
			this.OnConnected();
			this.StartReceive();
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00006A44 File Offset: 0x00004C44
		private void BeginReceive()
		{
			if (!base.Client.ReceiveAsync(this.m_SocketEventArgs))
			{
				this.ProcessReceive(this.m_SocketEventArgs);
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001D284 File Offset: 0x0001B484
		private void ProcessReceive(SocketAsyncEventArgs e)
		{
			if (e.SocketError != SocketError.Success)
			{
				if (base.EnsureSocketClosed())
				{
					this.OnClosed();
				}
				if (!base.IsIgnorableSocketError((int)e.SocketError))
				{
					this.OnError(new SocketException((int)e.SocketError));
				}
				return;
			}
			if (e.BytesTransferred == 0)
			{
				if (base.EnsureSocketClosed())
				{
					this.OnClosed();
				}
				return;
			}
			this.OnDataReceived(e.Buffer, e.Offset, e.BytesTransferred);
			this.StartReceive();
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001D2FC File Offset: 0x0001B4FC
		private void StartReceive()
		{
			Socket client = base.Client;
			if (client == null)
			{
				return;
			}
			bool flag;
			try
			{
				flag = client.ReceiveAsync(this.m_SocketEventArgs);
			}
			catch (SocketException ex)
			{
				int errorCode = ex.ErrorCode;
				if (!base.IsIgnorableSocketError(errorCode))
				{
					this.OnError(ex);
				}
				if (base.EnsureSocketClosed(client))
				{
					this.OnClosed();
				}
				return;
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
				return;
			}
			if (!flag)
			{
				this.ProcessReceive(this.m_SocketEventArgs);
			}
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001D3A0 File Offset: 0x0001B5A0
		protected override void Sendpublic(PosList<ArraySegment<byte>> items)
		{
			if (this.m_SocketEventArgsSend == null)
			{
				this.m_SocketEventArgsSend = new SocketAsyncEventArgs();
				this.m_SocketEventArgsSend.Completed += this.Sending_Completed;
			}
			bool flag;
			try
			{
				if (items.Count > 1)
				{
					if (this.m_SocketEventArgsSend.Buffer != null)
					{
						this.m_SocketEventArgsSend.SetBuffer(null, 0, 0);
					}
					this.m_SocketEventArgsSend.BufferList = items;
				}
				else
				{
					ArraySegment<byte> arraySegment = items[0];
					try
					{
						if (this.m_SocketEventArgsSend.BufferList != null)
						{
							this.m_SocketEventArgsSend.BufferList = null;
						}
					}
					catch
					{
					}
					this.m_SocketEventArgsSend.SetBuffer(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
				}
				flag = base.Client.SendAsync(this.m_SocketEventArgsSend);
			}
			catch (SocketException ex)
			{
				int errorCode = ex.ErrorCode;
				if (base.EnsureSocketClosed() && !base.IsIgnorableSocketError(errorCode))
				{
					this.OnError(ex);
				}
				return;
			}
			catch (Exception e)
			{
				if (base.EnsureSocketClosed() && this.IsIgnorableException(e))
				{
					this.OnError(e);
				}
				return;
			}
			if (!flag)
			{
				this.Sending_Completed(base.Client, this.m_SocketEventArgsSend);
			}
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001D4E4 File Offset: 0x0001B6E4
		private void Sending_Completed(object sender, SocketAsyncEventArgs e)
		{
			if (e.SocketError != SocketError.Success || e.BytesTransferred == 0)
			{
				if (base.EnsureSocketClosed())
				{
					this.OnClosed();
				}
				if (e.SocketError != SocketError.Success && !base.IsIgnorableSocketError((int)e.SocketError))
				{
					this.OnError(new SocketException((int)e.SocketError));
				}
				return;
			}
			base.OnSendingCompleted();
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00006A65 File Offset: 0x00004C65
		protected override void OnClosed()
		{
			if (this.m_SocketEventArgsSend != null)
			{
				this.m_SocketEventArgsSend.Dispose();
				this.m_SocketEventArgsSend = null;
			}
			if (this.m_SocketEventArgs != null)
			{
				this.m_SocketEventArgs.Dispose();
				this.m_SocketEventArgs = null;
			}
			base.OnClosed();
		}

		// Token: 0x04000341 RID: 833
		private SocketAsyncEventArgs m_SocketEventArgs;

		// Token: 0x04000342 RID: 834
		private SocketAsyncEventArgs m_SocketEventArgsSend;
	}
}
