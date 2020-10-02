using System;
using System.Net;
using Havens.Models.Local.Extensions;
using MailRy.Net;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine.Proxy
{
	// Token: 0x02000125 RID: 293
	public class Socks4aConnector : Socks4Connector
	{
		// Token: 0x06000927 RID: 2343 RVA: 0x00007148 File Offset: 0x00005348
		public Socks4aConnector(EndPoint proxyEndPoint, string userID) : base(proxyEndPoint, userID)
		{
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0001EBC4 File Offset: 0x0001CDC4
		public override void Connect(EndPoint remoteEndPoint)
		{
			DnsEndPoint dnsEndPoint = remoteEndPoint as DnsEndPoint;
			if (dnsEndPoint == null)
			{
				base.OnCompleted(new ProxyEventArgs(new Exception("The argument 'remoteEndPoint' must be a DnsEndPoint")));
				return;
			}
			try
			{
				base.ProxyEndPoint.ConnectAsync(null, new ConnectedCallback(this.ProcessConnect), dnsEndPoint);
			}
			catch (Exception innerException)
			{
				base.OnException(new Exception("Failed to connect proxy server", innerException));
			}
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0001EC34 File Offset: 0x0001CE34
		protected override byte[] GetSendingBuffer(EndPoint targetEndPoint, out int actualLength)
		{
			DnsEndPoint dnsEndPoint = targetEndPoint as DnsEndPoint;
			byte[] array = new byte[Math.Max(8, (string.IsNullOrEmpty(base.UserID) ? 0 : ProxyConnectorBase.ASCIIEncoding.GetMaxByteCount(base.UserID.Length)) + 5 + 4 + ProxyConnectorBase.ASCIIEncoding.GetMaxByteCount(dnsEndPoint.Host.Length) + 1)];
			array[0] = 4;
			array[1] = 1;
			array[2] = (byte)(dnsEndPoint.Port / 256);
			array[3] = (byte)(dnsEndPoint.Port % 256);
			array[4] = 0;
			array[5] = 0;
			array[6] = 0;
			array[7] = (byte)Socks4aConnector.m_Random.Next(1, 255);
			actualLength = 8;
			if (!string.IsNullOrEmpty(base.UserID))
			{
				actualLength += ProxyConnectorBase.ASCIIEncoding.GetBytes(base.UserID, 0, base.UserID.Length, array, actualLength);
			}
			byte[] array2 = array;
			int num = actualLength;
			actualLength = num + 1;
			array2[num] = 0;
			actualLength += ProxyConnectorBase.ASCIIEncoding.GetBytes(dnsEndPoint.Host, 0, dnsEndPoint.Host.Length, array, actualLength);
			byte[] array3 = array;
			num = actualLength;
			actualLength = num + 1;
			array3[num] = 0;
			return array;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0001ED50 File Offset: 0x0001CF50
		protected override void HandleFaultStatus(byte status)
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

		// Token: 0x04000383 RID: 899
		private static Random m_Random = new Random();
	}
}
