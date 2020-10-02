using Havens.Models.Local.Extensions;
using MailRy.Net;
using System;
using System.Net;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine.Proxy
{
	public class Socks4aConnector : Socks4Connector
	{
		private static Random m_Random = new Random();

		public Socks4aConnector(EndPoint proxyEndPoint, string userID)
			: base(proxyEndPoint, userID)
		{
		}

		public override void Connect(EndPoint remoteEndPoint)
		{
			DnsEndPoint dnsEndPoint = remoteEndPoint as DnsEndPoint;
			if (dnsEndPoint == null)
			{
				OnCompleted(new ProxyEventArgs(new Exception("The argument 'remoteEndPoint' must be a DnsEndPoint")));
			}
			else
			{
				try
				{
					base.ProxyEndPoint.ConnectAsync((EndPoint)null, (Havens.Models.Local.Extensions.ConnectedCallback)((ProxyConnectorBase)this).ProcessConnect, (object)dnsEndPoint);
				}
				catch (Exception innerException)
				{
					OnException(new Exception("Failed to connect proxy server", innerException));
				}
			}
		}

		protected override byte[] GetSendingBuffer(EndPoint targetEndPoint, out int actualLength)
		{
			DnsEndPoint dnsEndPoint = targetEndPoint as DnsEndPoint;
			byte[] array = new byte[Math.Max(8, ((!string.IsNullOrEmpty(base.UserID)) ? ProxyConnectorBase.ASCIIEncoding.GetMaxByteCount(base.UserID.Length) : 0) + 5 + 4 + ProxyConnectorBase.ASCIIEncoding.GetMaxByteCount(dnsEndPoint.Host.Length) + 1)];
			array[0] = 4;
			array[1] = 1;
			array[2] = (byte)(dnsEndPoint.Port / 256);
			array[3] = (byte)(dnsEndPoint.Port % 256);
			array[4] = 0;
			array[5] = 0;
			array[6] = 0;
			array[7] = (byte)m_Random.Next(1, 255);
			actualLength = 8;
			if (!string.IsNullOrEmpty(base.UserID))
			{
				actualLength += ProxyConnectorBase.ASCIIEncoding.GetBytes(base.UserID, 0, base.UserID.Length, array, actualLength);
			}
			array[actualLength++] = 0;
			actualLength += ProxyConnectorBase.ASCIIEncoding.GetBytes(dnsEndPoint.Host, 0, dnsEndPoint.Host.Length, array, actualLength);
			array[actualLength++] = 0;
			return array;
		}

		protected override void HandleFaultStatus(byte status)
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
