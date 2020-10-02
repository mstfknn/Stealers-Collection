using System;
using System.Net;
using System.Net.Sockets;
using Havens.Models.Local.Extensions;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine.Proxy
{
	// Token: 0x02000127 RID: 295
	public class Socks4Connector : ProxyConnectorBase
	{
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x0000718F File Offset: 0x0000538F
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x00007197 File Offset: 0x00005397
		public string UserID { get; private set; }

		// Token: 0x06000933 RID: 2355 RVA: 0x000071A0 File Offset: 0x000053A0
		public Socks4Connector(EndPoint proxyEndPoint, string userID) : base(proxyEndPoint)
		{
			this.UserID = userID;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0001EDA0 File Offset: 0x0001CFA0
		public override void Connect(EndPoint remoteEndPoint)
		{
			IPEndPoint ipendPoint = remoteEndPoint as IPEndPoint;
			if (ipendPoint == null)
			{
				base.OnCompleted(new ProxyEventArgs(new Exception("The argument 'remoteEndPoint' must be a IPEndPoint")));
				return;
			}
			try
			{
				base.ProxyEndPoint.ConnectAsync(null, new ConnectedCallback(this.ProcessConnect), ipendPoint);
			}
			catch (Exception innerException)
			{
				base.OnException(new Exception("Failed to connect proxy server", innerException));
			}
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0001EE10 File Offset: 0x0001D010
		protected virtual byte[] GetSendingBuffer(EndPoint targetEndPoint, out int actualLength)
		{
			IPEndPoint ipendPoint = targetEndPoint as IPEndPoint;
			byte[] addressBytes = ipendPoint.Address.GetAddressBytes();
			byte[] array = new byte[Math.Max(8, (string.IsNullOrEmpty(this.UserID) ? 0 : ProxyConnectorBase.ASCIIEncoding.GetMaxByteCount(this.UserID.Length)) + 5 + addressBytes.Length)];
			array[0] = 4;
			array[1] = 1;
			array[2] = (byte)(ipendPoint.Port / 256);
			array[3] = (byte)(ipendPoint.Port % 256);
			Buffer.BlockCopy(addressBytes, 0, array, 4, addressBytes.Length);
			actualLength = 4 + addressBytes.Length;
			if (!string.IsNullOrEmpty(this.UserID))
			{
				actualLength += ProxyConnectorBase.ASCIIEncoding.GetBytes(this.UserID, 0, this.UserID.Length, array, actualLength);
			}
			byte[] array2 = array;
			int num = actualLength;
			actualLength = num + 1;
			array2[num] = 0;
			return array;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0001EEE0 File Offset: 0x0001D0E0
		protected override void ProcessConnect(Socket socket, object targetEndPoint, SocketAsyncEventArgs e, Exception exception)
		{
			if (exception != null)
			{
				base.OnException(exception);
				return;
			}
			if (e != null && !base.ValidateAsyncResult(e))
			{
				return;
			}
			if (socket == null)
			{
				base.OnException(new SocketException(10053));
				return;
			}
			if (e == null)
			{
				e = new SocketAsyncEventArgs();
			}
			int count;
			byte[] sendingBuffer = this.GetSendingBuffer((EndPoint)targetEndPoint, out count);
			e.SetBuffer(sendingBuffer, 0, count);
			e.UserToken = socket;
			e.Completed += base.AsyncEventArgsCompleted;
			base.StartSend(socket, e);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x000071B0 File Offset: 0x000053B0
		protected override void ProcessSend(SocketAsyncEventArgs e)
		{
			if (!base.ValidateAsyncResult(e))
			{
				return;
			}
			e.SetBuffer(0, 8);
			this.StartReceive((Socket)e.UserToken, e);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0001EF60 File Offset: 0x0001D160
		protected override void ProcessReceive(SocketAsyncEventArgs e)
		{
			if (!base.ValidateAsyncResult(e))
			{
				return;
			}
			int num = e.Offset + e.BytesTransferred;
			if (num < 8)
			{
				e.SetBuffer(num, 8 - num);
				this.StartReceive((Socket)e.UserToken, e);
				return;
			}
			if (num != 8)
			{
				base.OnException("socks protocol error: size of response cannot be larger than 8");
				return;
			}
			byte b = e.Buffer[1];
			if (b == 90)
			{
				base.OnCompleted(new ProxyEventArgs((Socket)e.UserToken));
				return;
			}
			this.HandleFaultStatus(b);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0001ED50 File Offset: 0x0001CF50
		protected virtual void HandleFaultStatus(byte status)
		{
			string exception = string.Empty;
			switch (status)
			{
			case 91:
				exception = "request rejected or failed";
				break;
			case 92:
				exception = "request failed because client is not running identd (or not reachable from the server)";
				break;
			case 93:
				exception = "request failed because client's identd could not confirm the user ID string in the reques";
				break;
			default:
				exception = "request rejected for unknown error";
				break;
			}
			base.OnException(exception);
		}

		// Token: 0x04000387 RID: 903
		private const int m_ValidResponseSize = 8;
	}
}
