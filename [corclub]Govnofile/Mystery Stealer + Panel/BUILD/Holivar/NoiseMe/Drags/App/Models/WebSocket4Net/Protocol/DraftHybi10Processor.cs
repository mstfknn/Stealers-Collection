using System;
using System.Collections.Generic;
using System.Text;
using NoiseMe.Drags.App.Models.SuperSocket.ClientEngine;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	// Token: 0x020000A6 RID: 166
	internal class DraftHybi10Processor : ProtocolProcessorBase
	{
		// Token: 0x060005BE RID: 1470 RVA: 0x0000590E File Offset: 0x00003B0E
		public DraftHybi10Processor() : base(WebSocketVersion.DraftHybi10, new CloseStatusCodeHybi10())
		{
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00005932 File Offset: 0x00003B32
		protected DraftHybi10Processor(WebSocketVersion version, ICloseStatusCode closeStatusCode, string originHeaderName) : base(version, closeStatusCode)
		{
			this.m_OriginHeaderName = originHeaderName;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00019580 File Offset: 0x00017780
		public override void SendHandshake(WebSocket websocket)
		{
			string text = Convert.ToBase64String(Encoding.ASCII.GetBytes(Guid.NewGuid().ToString().Substring(0, 16)));
			string value = (text + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11").CalculateChallenge();
			websocket.Items[this.m_ExpectedAcceptKey] = value;
			StringBuilder stringBuilder = new StringBuilder();
			if (websocket.HttpConnectProxy == null)
			{
				stringBuilder.AppendFormatWithCrCf("GET {0} HTTP/1.1", websocket.TargetUri.PathAndQuery);
			}
			else
			{
				stringBuilder.AppendFormatWithCrCf("GET {0} HTTP/1.1", websocket.TargetUri.ToString());
			}
			stringBuilder.Append("Host: ");
			stringBuilder.AppendWithCrCf(websocket.HandshakeHost);
			stringBuilder.AppendWithCrCf("Upgrade: websocket");
			stringBuilder.AppendWithCrCf("Connection: Upgrade");
			stringBuilder.Append("Sec-WebSocket-Version: ");
			stringBuilder.AppendWithCrCf(base.VersionTag);
			stringBuilder.Append("Sec-WebSocket-Key: ");
			stringBuilder.AppendWithCrCf(text);
			stringBuilder.Append(string.Format("{0}: ", this.m_OriginHeaderName));
			stringBuilder.AppendWithCrCf(websocket.Origin);
			if (!string.IsNullOrEmpty(websocket.SubProtocol))
			{
				stringBuilder.Append("Sec-WebSocket-Protocol: ");
				stringBuilder.AppendWithCrCf(websocket.SubProtocol);
			}
			List<KeyValuePair<string, string>> cookies = websocket.Cookies;
			if (cookies != null && cookies.Count > 0)
			{
				string[] array = new string[cookies.Count];
				for (int i = 0; i < cookies.Count; i++)
				{
					KeyValuePair<string, string> keyValuePair = cookies[i];
					array[i] = keyValuePair.Key + "=" + Uri.EscapeUriString(keyValuePair.Value);
				}
				stringBuilder.Append("Cookie: ");
				stringBuilder.AppendWithCrCf(string.Join(";", array));
			}
			if (websocket.CustomHeaderItems != null)
			{
				for (int j = 0; j < websocket.CustomHeaderItems.Count; j++)
				{
					KeyValuePair<string, string> keyValuePair2 = websocket.CustomHeaderItems[j];
					stringBuilder.AppendFormatWithCrCf("{0}: {1}", new object[]
					{
						keyValuePair2.Key,
						keyValuePair2.Value
					});
				}
			}
			stringBuilder.AppendWithCrCf();
			byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
			websocket.Client.Send(bytes, 0, bytes.Length);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00005959 File Offset: 0x00003B59
		public override ReaderBase CreateHandshakeReader(WebSocket websocket)
		{
			return new DraftHybi10HandshakeReader(websocket);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000197B8 File Offset: 0x000179B8
		private void SendMessage(WebSocket websocket, int opCode, string message)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(message);
			this.SendDataFragment(websocket, opCode, bytes, 0, bytes.Length);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00005961 File Offset: 0x00003B61
		private byte[] EncodeDataFrame(int opCode, byte[] playloadData, int offset, int length)
		{
			return this.EncodeDataFrame(opCode, true, playloadData, offset, length);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000197E0 File Offset: 0x000179E0
		private byte[] EncodeDataFrame(int opCode, bool isFinal, byte[] playloadData, int offset, int length)
		{
			int num = 4;
			byte[] array;
			if (length < 126)
			{
				array = new byte[2 + num + length];
				array[1] = (byte)length;
			}
			else if (length < 65536)
			{
				array = new byte[4 + num + length];
				array[1] = 126;
				array[2] = (byte)(length / 256);
				array[3] = (byte)(length % 256);
			}
			else
			{
				array = new byte[10 + num + length];
				array[1] = 127;
				int num2 = length;
				int num3 = 256;
				for (int i = 9; i > 1; i--)
				{
					array[i] = (byte)(num2 % num3);
					num2 /= num3;
					if (num2 == 0)
					{
						break;
					}
				}
			}
			if (isFinal)
			{
				array[0] = (byte)(opCode | 128);
			}
			else
			{
				array[0] = (byte)opCode;
			}
			array[1] = (array[1] | 128);
			this.GenerateMask(array, array.Length - num - length);
			if (length > 0)
			{
				this.MaskData(playloadData, offset, length, array, array.Length - length, array, array.Length - num - length);
			}
			return array;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000198CC File Offset: 0x00017ACC
		private void SendDataFragment(WebSocket websocket, int opCode, byte[] playloadData, int offset, int length)
		{
			byte[] array = this.EncodeDataFrame(opCode, playloadData, offset, length);
			TcpClientSession client = websocket.Client;
			if (client != null)
			{
				client.Send(array, 0, array.Length);
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0000596F File Offset: 0x00003B6F
		public override void SendData(WebSocket websocket, byte[] data, int offset, int length)
		{
			this.SendDataFragment(websocket, 2, data, offset, length);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000198FC File Offset: 0x00017AFC
		public override void SendData(WebSocket websocket, IList<ArraySegment<byte>> segments)
		{
			List<ArraySegment<byte>> list = new List<ArraySegment<byte>>(segments.Count);
			int num = segments.Count - 1;
			for (int i = 0; i < segments.Count; i++)
			{
				ArraySegment<byte> arraySegment = segments[i];
				list.Add(new ArraySegment<byte>(this.EncodeDataFrame((i == 0) ? 2 : 0, i == num, arraySegment.Array, arraySegment.Offset, arraySegment.Count)));
			}
			TcpClientSession client = websocket.Client;
			if (client != null)
			{
				client.Send(list);
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0000597D File Offset: 0x00003B7D
		public override void SendMessage(WebSocket websocket, string message)
		{
			this.SendMessage(websocket, 1, message);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001997C File Offset: 0x00017B7C
		public override void SendCloseHandshake(WebSocket websocket, int statusCode, string closeReason)
		{
			byte[] array = new byte[(string.IsNullOrEmpty(closeReason) ? 0 : Encoding.UTF8.GetMaxByteCount(closeReason.Length)) + 2];
			int num = statusCode / 256;
			int num2 = statusCode % 256;
			array[0] = (byte)num;
			array[1] = (byte)num2;
			if (websocket == null || websocket.State == WebSocketState.Closed)
			{
				return;
			}
			if (!string.IsNullOrEmpty(closeReason))
			{
				int bytes = Encoding.UTF8.GetBytes(closeReason, 0, closeReason.Length, array, 2);
				this.SendDataFragment(websocket, 8, array, 0, bytes + 2);
				return;
			}
			this.SendDataFragment(websocket, 8, array, 0, array.Length);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00005988 File Offset: 0x00003B88
		public override void SendPing(WebSocket websocket, string ping)
		{
			this.SendMessage(websocket, 9, ping);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00005994 File Offset: 0x00003B94
		public override void SendPong(WebSocket websocket, string pong)
		{
			this.SendMessage(websocket, 10, pong);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00019A0C File Offset: 0x00017C0C
		public override bool VerifyHandshake(WebSocket websocket, WebSocketCommandInfo handshakeInfo, out string description)
		{
			if (string.IsNullOrEmpty(handshakeInfo.Text))
			{
				description = "invalid handshake";
				return false;
			}
			string empty = string.Empty;
			if (!handshakeInfo.Text.ParseMimeHeader(websocket.Items, out empty))
			{
				description = "invalid handshake";
				return false;
			}
			if (!this.ValidateVerbLine(empty))
			{
				description = empty;
				return false;
			}
			if (!string.IsNullOrEmpty(websocket.SubProtocol))
			{
				string value = websocket.Items.GetValue("Sec-WebSocket-Protocol", string.Empty);
				if (!websocket.SubProtocol.Equals(value, StringComparison.OrdinalIgnoreCase))
				{
					description = "subprotocol doesn't match";
					return false;
				}
			}
			string value2 = websocket.Items.GetValue("Sec-WebSocket-Accept", string.Empty);
			if (!websocket.Items.GetValue(this.m_ExpectedAcceptKey, string.Empty).Equals(value2, StringComparison.OrdinalIgnoreCase))
			{
				description = "accept key doesn't match";
				return false;
			}
			description = string.Empty;
			return true;
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00003147 File Offset: 0x00001347
		public override bool SupportBinary
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x00003147 File Offset: 0x00001347
		public override bool SupportPingPong
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00019AE4 File Offset: 0x00017CE4
		private void GenerateMask(byte[] mask, int offset)
		{
			int num = Math.Min(offset + 4, mask.Length);
			for (int i = offset; i < num; i++)
			{
				mask[i] = (byte)DraftHybi10Processor.m_Random.Next(0, 255);
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00019B20 File Offset: 0x00017D20
		private void MaskData(byte[] rawData, int offset, int length, byte[] outputData, int outputOffset, byte[] mask, int maskOffset)
		{
			for (int i = 0; i < length; i++)
			{
				int num = offset + i;
				outputData[outputOffset++] = (rawData[num] ^ mask[maskOffset + i % 4]);
			}
		}

		// Token: 0x04000293 RID: 659
		private const string m_Magic = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

		// Token: 0x04000294 RID: 660
		private string m_ExpectedAcceptKey = "ExpectedAccept";

		// Token: 0x04000295 RID: 661
		private readonly string m_OriginHeaderName = "Sec-WebSocket-Origin";

		// Token: 0x04000296 RID: 662
		private static Random m_Random = new Random();

		// Token: 0x04000297 RID: 663
		private const string m_Error_InvalidHandshake = "invalid handshake";

		// Token: 0x04000298 RID: 664
		private const string m_Error_SubProtocolNotMatch = "subprotocol doesn't match";

		// Token: 0x04000299 RID: 665
		private const string m_Error_AcceptKeyNotMatch = "accept key doesn't match";
	}
}
