using Havens.Models.Local.Extensions;
using MailRy.Net;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine.Proxy
{
	public class Socks5Connector : ProxyConnectorBase
	{
		private enum SocksState
		{
			NotAuthenticated,
			Authenticating,
			Authenticated,
			FoundLength,
			Connected
		}

		private class SocksContext
		{
			public Socket Socket
			{
				get;
				set;
			}

			public SocksState State
			{
				get;
				set;
			}

			public EndPoint TargetEndPoint
			{
				get;
				set;
			}

			public List<byte> ReceivedData
			{
				get;
				set;
			}

			public int ExpectedLength
			{
				get;
				set;
			}
		}

		private ArraySegment<byte> m_UserNameAuthenRequest;

		private static byte[] m_AuthenHandshake = new byte[4]
		{
			5,
			2,
			0,
			2
		};

		public Socks5Connector(EndPoint proxyEndPoint)
			: base(proxyEndPoint)
		{
		}

		public Socks5Connector(EndPoint proxyEndPoint, string username, string password)
			: base(proxyEndPoint)
		{
			if (string.IsNullOrEmpty(username))
			{
				throw new ArgumentNullException("username");
			}
			byte[] array = new byte[3 + ProxyConnectorBase.ASCIIEncoding.GetMaxByteCount(username.Length) + ((!string.IsNullOrEmpty(password)) ? ProxyConnectorBase.ASCIIEncoding.GetMaxByteCount(password.Length) : 0)];
			int num = 0;
			array[0] = 5;
			int bytes = ProxyConnectorBase.ASCIIEncoding.GetBytes(username, 0, username.Length, array, 2);
			if (bytes > 255)
			{
				throw new ArgumentException("the length of username cannot exceed 255", "username");
			}
			array[1] = (byte)bytes;
			num = bytes + 2;
			if (!string.IsNullOrEmpty(password))
			{
				bytes = ProxyConnectorBase.ASCIIEncoding.GetBytes(password, 0, password.Length, array, num + 1);
				if (bytes > 255)
				{
					throw new ArgumentException("the length of password cannot exceed 255", "password");
				}
				array[num] = (byte)bytes;
				num += bytes + 1;
			}
			else
			{
				array[num] = 0;
				num++;
			}
			m_UserNameAuthenRequest = new ArraySegment<byte>(array, 0, num);
		}

		public override void Connect(EndPoint remoteEndPoint)
		{
			if (remoteEndPoint == null)
			{
				throw new ArgumentNullException("remoteEndPoint");
			}
			if (!(remoteEndPoint is IPEndPoint) && !(remoteEndPoint is DnsEndPoint))
			{
				throw new ArgumentException("remoteEndPoint must be IPEndPoint or DnsEndPoint", "remoteEndPoint");
			}
			try
			{
				base.ProxyEndPoint.ConnectAsync((EndPoint)null, (Havens.Models.Local.Extensions.ConnectedCallback)((ProxyConnectorBase)this).ProcessConnect, (object)remoteEndPoint);
			}
			catch (Exception innerException)
			{
				OnException(new Exception("Failed to connect proxy server", innerException));
			}
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
				e.UserToken = new SocksContext
				{
					TargetEndPoint = (EndPoint)targetEndPoint,
					Socket = socket,
					State = SocksState.NotAuthenticated
				};
				e.Completed += base.AsyncEventArgsCompleted;
				e.SetBuffer(m_AuthenHandshake, 0, m_AuthenHandshake.Length);
				StartSend(socket, e);
			}
		}

		protected override void ProcessSend(SocketAsyncEventArgs e)
		{
			if (ValidateAsyncResult(e))
			{
				SocksContext socksContext = e.UserToken as SocksContext;
				if (socksContext.State == SocksState.NotAuthenticated)
				{
					e.SetBuffer(0, 2);
					StartReceive(socksContext.Socket, e);
				}
				else if (socksContext.State == SocksState.Authenticating)
				{
					e.SetBuffer(0, 2);
					StartReceive(socksContext.Socket, e);
				}
				else
				{
					e.SetBuffer(0, e.Buffer.Length);
					StartReceive(socksContext.Socket, e);
				}
			}
		}

		private bool ProcessAuthenticationResponse(Socket socket, SocketAsyncEventArgs e)
		{
			int num = e.BytesTransferred + e.Offset;
			if (num < 2)
			{
				e.SetBuffer(num, 2 - num);
				StartReceive(socket, e);
				return false;
			}
			if (num > 2)
			{
				OnException("received length exceeded");
				return false;
			}
			if (e.Buffer[0] != 5)
			{
				OnException("invalid protocol version");
				return false;
			}
			return true;
		}

		protected override void ProcessReceive(SocketAsyncEventArgs e)
		{
			if (!ValidateAsyncResult(e))
			{
				return;
			}
			SocksContext socksContext = (SocksContext)e.UserToken;
			if (socksContext.State == SocksState.NotAuthenticated)
			{
				if (ProcessAuthenticationResponse(socksContext.Socket, e))
				{
					switch (e.Buffer[1])
					{
					case 0:
						socksContext.State = SocksState.Authenticated;
						SendHandshake(e);
						break;
					case 2:
						socksContext.State = SocksState.Authenticating;
						AutheticateWithUserNamePassword(e);
						break;
					case byte.MaxValue:
						OnException("no acceptable methods were offered");
						break;
					default:
						OnException("protocol error");
						break;
					}
				}
				return;
			}
			if (socksContext.State == SocksState.Authenticating)
			{
				if (ProcessAuthenticationResponse(socksContext.Socket, e))
				{
					if (e.Buffer[1] == 0)
					{
						socksContext.State = SocksState.Authenticated;
						SendHandshake(e);
					}
					else
					{
						OnException("authentication failure");
					}
				}
				return;
			}
			byte[] array = new byte[e.BytesTransferred];
			Buffer.BlockCopy(e.Buffer, e.Offset, array, 0, e.BytesTransferred);
			socksContext.ReceivedData.AddRange(array);
			if (socksContext.ExpectedLength > socksContext.ReceivedData.Count)
			{
				StartReceive(socksContext.Socket, e);
			}
			else if (socksContext.State != SocksState.FoundLength)
			{
				int num;
				switch (socksContext.ReceivedData[3])
				{
				case 1:
					num = 10;
					break;
				case 3:
					num = 7 + socksContext.ReceivedData[4];
					break;
				default:
					num = 22;
					break;
				}
				if (socksContext.ReceivedData.Count < num)
				{
					socksContext.ExpectedLength = num;
					StartReceive(socksContext.Socket, e);
				}
				else if (socksContext.ReceivedData.Count > num)
				{
					OnException("response length exceeded");
				}
				else
				{
					OnGetFullResponse(socksContext);
				}
			}
			else if (socksContext.ReceivedData.Count > socksContext.ExpectedLength)
			{
				OnException("response length exceeded");
			}
			else
			{
				OnGetFullResponse(socksContext);
			}
		}

		private void OnGetFullResponse(SocksContext context)
		{
			List<byte> receivedData = context.ReceivedData;
			if (receivedData[0] != 5)
			{
				OnException("invalid protocol version");
				return;
			}
			byte b = receivedData[1];
			if (b == 0)
			{
				OnCompleted(new ProxyEventArgs(context.Socket));
				return;
			}
			string empty = string.Empty;
			switch (b)
			{
			case 2:
				empty = "connection not allowed by ruleset";
				break;
			case 3:
				empty = "network unreachable";
				break;
			case 4:
				empty = "host unreachable";
				break;
			case 5:
				empty = "connection refused by destination host";
				break;
			case 6:
				empty = "TTL expired";
				break;
			case 7:
				empty = "command not supported / protocol error";
				break;
			case 8:
				empty = "address type not supported";
				break;
			default:
				empty = "general failure";
				break;
			}
			OnException(empty);
		}

		private void SendHandshake(SocketAsyncEventArgs e)
		{
			SocksContext socksContext = e.UserToken as SocksContext;
			EndPoint targetEndPoint = socksContext.TargetEndPoint;
			int num = 0;
			byte[] array;
			int num2;
			if (targetEndPoint is IPEndPoint)
			{
				IPEndPoint iPEndPoint = targetEndPoint as IPEndPoint;
				num = iPEndPoint.Port;
				if (iPEndPoint.AddressFamily == AddressFamily.InterNetwork)
				{
					array = new byte[10]
					{
						0,
						0,
						0,
						1,
						0,
						0,
						0,
						0,
						0,
						0
					};
					Buffer.BlockCopy(iPEndPoint.Address.GetAddressBytes(), 0, array, 4, 4);
				}
				else
				{
					if (iPEndPoint.AddressFamily != AddressFamily.InterNetworkV6)
					{
						OnException("unknown address family");
						return;
					}
					array = new byte[22];
					array[3] = 4;
					Buffer.BlockCopy(iPEndPoint.Address.GetAddressBytes(), 0, array, 4, 16);
				}
				num2 = array.Length;
			}
			else
			{
				DnsEndPoint dnsEndPoint = targetEndPoint as DnsEndPoint;
				num = dnsEndPoint.Port;
				array = new byte[7 + ProxyConnectorBase.ASCIIEncoding.GetMaxByteCount(dnsEndPoint.Host.Length)];
				array[3] = 3;
				num2 = 5;
				num2 += ProxyConnectorBase.ASCIIEncoding.GetBytes(dnsEndPoint.Host, 0, dnsEndPoint.Host.Length, array, num2);
				num2 += 2;
			}
			array[0] = 5;
			array[1] = 1;
			array[2] = 0;
			array[num2 - 2] = (byte)(num / 256);
			array[num2 - 1] = (byte)(num % 256);
			e.SetBuffer(array, 0, num2);
			socksContext.ReceivedData = new List<byte>(num2 + 5);
			socksContext.ExpectedLength = 5;
			StartSend(socksContext.Socket, e);
		}

		private void AutheticateWithUserNamePassword(SocketAsyncEventArgs e)
		{
			Socket socket = ((SocksContext)e.UserToken).Socket;
			e.SetBuffer(m_UserNameAuthenRequest.Array, m_UserNameAuthenRequest.Offset, m_UserNameAuthenRequest.Count);
			StartSend(socket, e);
		}
	}
}
