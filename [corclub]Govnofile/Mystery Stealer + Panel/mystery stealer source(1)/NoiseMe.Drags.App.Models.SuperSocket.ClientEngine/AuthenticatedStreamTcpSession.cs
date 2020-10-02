using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	public abstract class AuthenticatedStreamTcpSession : TcpClientSession
	{
		private class StreamAsyncState
		{
			public AuthenticatedStream Stream
			{
				get;
				set;
			}

			public Socket Client
			{
				get;
				set;
			}

			public PosList<ArraySegment<byte>> SendingItems
			{
				get;
				set;
			}
		}

		private AuthenticatedStream m_Stream;

		public SecurityOption Security
		{
			get;
			set;
		}

		public AuthenticatedStreamTcpSession()
		{
		}

		protected override void SocketEventArgsCompleted(object sender, SocketAsyncEventArgs e)
		{
			ProcessConnect(sender as Socket, null, e, null);
		}

		protected abstract void StartAuthenticatedStream(Socket client);

		protected override void OnGetSocket(SocketAsyncEventArgs e)
		{
			try
			{
				StartAuthenticatedStream(base.Client);
			}
			catch (Exception e2)
			{
				if (!IsIgnorableException(e2))
				{
					OnError(e2);
				}
			}
		}

		protected void OnAuthenticatedStreamConnected(AuthenticatedStream stream)
		{
			m_Stream = stream;
			OnConnected();
			if (base.Buffer.Array == null)
			{
				int num = ReceiveBufferSize;
				if (num <= 0)
				{
					num = 4096;
				}
				ReceiveBufferSize = num;
				base.Buffer = new ArraySegment<byte>(new byte[num]);
			}
			BeginRead();
		}

		private void OnDataRead(IAsyncResult result)
		{
			StreamAsyncState streamAsyncState = result.AsyncState as StreamAsyncState;
			if (streamAsyncState == null || streamAsyncState.Stream == null)
			{
				OnError(new NullReferenceException("Null state or stream."));
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
				if (!IsIgnorableException(e))
				{
					OnError(e);
				}
				if (EnsureSocketClosed(streamAsyncState.Client))
				{
					OnClosed();
				}
				return;
			}
			if (num == 0)
			{
				if (EnsureSocketClosed(streamAsyncState.Client))
				{
					OnClosed();
				}
			}
			else
			{
				OnDataReceived(base.Buffer.Array, base.Buffer.Offset, num);
				BeginRead();
			}
		}

		private void BeginRead()
		{
			StartRead();
		}

		private void StartRead()
		{
			Socket client = base.Client;
			if (client != null && m_Stream != null)
			{
				try
				{
					ArraySegment<byte> buffer = base.Buffer;
					m_Stream.BeginRead(buffer.Array, buffer.Offset, buffer.Count, OnDataRead, new StreamAsyncState
					{
						Stream = m_Stream,
						Client = client
					});
				}
				catch (Exception e)
				{
					if (!IsIgnorableException(e))
					{
						OnError(e);
					}
					if (EnsureSocketClosed(client))
					{
						OnClosed();
					}
				}
			}
		}

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

		protected override void Sendpublic(PosList<ArraySegment<byte>> items)
		{
			Socket client = base.Client;
			try
			{
				ArraySegment<byte> arraySegment = items[items.Position];
				m_Stream.BeginWrite(arraySegment.Array, arraySegment.Offset, arraySegment.Count, OnWriteComplete, new StreamAsyncState
				{
					Stream = m_Stream,
					Client = client,
					SendingItems = items
				});
			}
			catch (Exception e)
			{
				if (!IsIgnorableException(e))
				{
					OnError(e);
				}
				if (EnsureSocketClosed(client))
				{
					OnClosed();
				}
			}
		}

		private void OnWriteComplete(IAsyncResult result)
		{
			StreamAsyncState streamAsyncState = result.AsyncState as StreamAsyncState;
			if (streamAsyncState == null || streamAsyncState.Stream == null)
			{
				OnError(new NullReferenceException("State of Ssl stream is null."));
				return;
			}
			AuthenticatedStream stream = streamAsyncState.Stream;
			try
			{
				stream.EndWrite(result);
			}
			catch (Exception e)
			{
				if (!IsIgnorableException(e))
				{
					OnError(e);
				}
				if (EnsureSocketClosed(streamAsyncState.Client))
				{
					OnClosed();
				}
				return;
			}
			PosList<ArraySegment<byte>> sendingItems = streamAsyncState.SendingItems;
			int num = sendingItems.Position + 1;
			if (num < sendingItems.Count)
			{
				sendingItems.Position = num;
				Sendpublic(sendingItems);
			}
			else
			{
				try
				{
					m_Stream.Flush();
				}
				catch (Exception e2)
				{
					if (!IsIgnorableException(e2))
					{
						OnError(e2);
					}
					if (EnsureSocketClosed(streamAsyncState.Client))
					{
						OnClosed();
					}
					return;
				}
				OnSendingCompleted();
			}
		}

		public override void Close()
		{
			AuthenticatedStream stream = m_Stream;
			if (stream != null)
			{
				stream.Close();
				stream.Dispose();
				m_Stream = null;
			}
			base.Close();
		}
	}
}
