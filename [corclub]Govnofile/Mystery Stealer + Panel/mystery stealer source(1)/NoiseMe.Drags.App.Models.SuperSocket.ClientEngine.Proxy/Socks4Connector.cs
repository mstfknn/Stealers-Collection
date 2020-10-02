using Havens.Models.Local.Extensions;
using System;
using System.Net;
using System.Net.Sockets;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine.Proxy
{
	public class Socks4Connector : ProxyConnectorBase
	{
		private const int m_ValidResponseSize = 8;

		public string UserID
		{
			get;
			private set;
		}

		public Socks4Connector(EndPoint proxyEndPoint, string userID)
			: base(proxyEndPoint)
		{
			UserID = userID;
		}

		public override void Connect(EndPoint remoteEndPoint)
		{
			IPEndPoint iPEndPoint = remoteEndPoint as IPEndPoint;
			if (iPEndPoint == null)
			{
				OnCompleted(new ProxyEventArgs(new Exception("The argument 'remoteEndPoint' must be a IPEndPoint")));
			}
			else
			{
				try
				{
					base.ProxyEndPoint.ConnectAsync((EndPoint)null, (Havens.Models.Local.Extensions.ConnectedCallback)((ProxyConnectorBase)this).ProcessConnect, (object)iPEndPoint);
				}
				catch (Exception innerException)
				{
					OnException(new Exception("Failed to connect proxy server", innerException));
				}
			}
		}

		protected virtual byte[] GetSendingBuffer(EndPoint targetEndPoint, out int actualLength)
		{
			IPEndPoint iPEndPoint = targetEndPoint as IPEndPoint;
			byte[] addressBytes = iPEndPoint.Address.GetAddressBytes();
			byte[] array = new byte[Math.Max(8, ((!string.IsNullOrEmpty(UserID)) ? ProxyConnectorBase.ASCIIEncoding.GetMaxByteCount(UserID.Length) : 0) + 5 + addressBytes.Length)];
			array[0] = 4;
			array[1] = 1;
			array[2] = (byte)(iPEndPoint.Port / 256);
			array[3] = (byte)(iPEndPoint.Port % 256);
			Buffer.BlockCopy(addressBytes, 0, array, 4, addressBytes.Length);
			actualLength = 4 + addressBytes.Length;
			if (!string.IsNullOrEmpty(UserID))
			{
				actualLength += ProxyConnectorBase.ASCIIEncoding.GetBytes(UserID, 0, UserID.Length, array, actualLength);
			}
			array[actualLength++] = 0;
			return array;
		}

		protected override void ProcessConnect(Socket socket, object targetEndPoint, SocketAsyncEventArgs e, Exception exception)
		{
			if (exception != null)
			{
				OnException(exception);
			}
			else
			{
				if (e != null && !ValidateAsyncResult(e))
				{
					return;
				}
				if (socket == null)
				{
					OnException(new SocketException(10053));
					return;
				}
				if (e == null)
				{
					e = new SocketAsyncEventArgs();
				}
				int actualLength;
				byte[] sendingBuffer = GetSendingBuffer((EndPoint)targetEndPoint, out actualLength);
				e.SetBuffer(sendingBuffer, 0, actualLength);
				e.UserToken = socket;
				e.Completed += base.AsyncEventArgsCompleted;
				StartSend(socket, e);
			}
		}

		protected override void ProcessSend(SocketAsyncEventArgs e)
		{
			if (ValidateAsyncResult(e))
			{
				e.SetBuffer(0, 8);
				StartReceive((Socket)e.UserToken, e);
			}
		}

		protected override void ProcessReceive(SocketAsyncEventArgs e)
		{
			if (!ValidateAsyncResult(e))
			{
				return;
			}
			int num = e.Offset + e.BytesTransferred;
			if (num < 8)
			{
				e.SetBuffer(num, 8 - num);
				StartReceive((Socket)e.UserToken, e);
			}
			else if (num == 8)
			{
				byte b = e.Buffer[1];
				if (b == 90)
				{
					OnCompleted(new ProxyEventArgs((Socket)e.UserToken));
				}
				else
				{
					HandleFaultStatus(b);
				}
			}
			else
			{
				OnException("socks protocol error: size of response cannot be larger than 8");
			}
		}

		protected virtual void HandleFaultStatus(byte status)
		{
			string empty = string.Empty;
			switch (status)
			{
			case 91:
				empty = "request rejected or failed";
				break;
			case 92:
				empty = "request failed because client is not running identd (or not reachable from the server)";
				break;
			case 93:
				empty = "request failed because client's identd could not confirm the user ID string in the reques";
				break;
			default:
				empty = "request rejected for unknown error";
				break;
			}
			OnException(empty);
		}
	}
}
