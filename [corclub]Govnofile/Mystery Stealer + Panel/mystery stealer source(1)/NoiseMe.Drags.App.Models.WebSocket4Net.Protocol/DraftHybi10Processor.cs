using System;
using System.Collections.Generic;
using System.Text;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	internal class DraftHybi10Processor : ProtocolProcessorBase
	{
		private const string m_Magic = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

		private string m_ExpectedAcceptKey = "ExpectedAccept";

		private readonly string m_OriginHeaderName = "Sec-WebSocket-Origin";

		private static Random m_Random = new Random();

		private const string m_Error_InvalidHandshake = "invalid handshake";

		private const string m_Error_SubProtocolNotMatch = "subprotocol doesn't match";

		private const string m_Error_AcceptKeyNotMatch = "accept key doesn't match";

		public override bool SupportBinary => true;

		public override bool SupportPingPong => true;

		public DraftHybi10Processor()
			: base(WebSocketVersion.DraftHybi10, new CloseStatusCodeHybi10())
		{
		}

		protected DraftHybi10Processor(WebSocketVersion version, ICloseStatusCode closeStatusCode, string originHeaderName)
			: base(version, closeStatusCode)
		{
			m_OriginHeaderName = originHeaderName;
		}

		public override void SendHandshake(WebSocket websocket)
		{
			string text = Convert.ToBase64String(Encoding.ASCII.GetBytes(Guid.NewGuid().ToString().Substring(0, 16)));
			string value = (text + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11").CalculateChallenge();
			websocket.Items[m_ExpectedAcceptKey] = value;
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
			stringBuilder.Append($"{m_OriginHeaderName}: ");
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
					stringBuilder.AppendFormatWithCrCf("{0}: {1}", keyValuePair2.Key, keyValuePair2.Value);
				}
			}
			stringBuilder.AppendWithCrCf();
			byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
			websocket.Client.Send(bytes, 0, bytes.Length);
		}

		public override ReaderBase CreateHandshakeReader(WebSocket websocket)
		{
			return new DraftHybi10HandshakeReader(websocket);
		}

		private void SendMessage(WebSocket websocket, int opCode, string message)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(message);
			SendDataFragment(websocket, opCode, bytes, 0, bytes.Length);
		}

		private byte[] EncodeDataFrame(int opCode, byte[] playloadData, int offset, int length)
		{
			return EncodeDataFrame(opCode, isFinal: true, playloadData, offset, length);
		}

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
				for (int num4 = 9; num4 > 1; num4--)
				{
					array[num4] = (byte)(num2 % num3);
					num2 /= num3;
					if (num2 == 0)
					{
						break;
					}
				}
			}
			if (isFinal)
			{
				array[0] = (byte)(opCode | 0x80);
			}
			else
			{
				array[0] = (byte)opCode;
			}
			array[1] = (byte)(array[1] | 0x80);
			GenerateMask(array, array.Length - num - length);
			if (length > 0)
			{
				MaskData(playloadData, offset, length, array, array.Length - length, array, array.Length - num - length);
			}
			return array;
		}

		private void SendDataFragment(WebSocket websocket, int opCode, byte[] playloadData, int offset, int length)
		{
			byte[] array = EncodeDataFrame(opCode, playloadData, offset, length);
			websocket.Client?.Send(array, 0, array.Length);
		}

		public override void SendData(WebSocket websocket, byte[] data, int offset, int length)
		{
			SendDataFragment(websocket, 2, data, offset, length);
		}

		public override void SendData(WebSocket websocket, IList<ArraySegment<byte>> segments)
		{
			List<ArraySegment<byte>> list = new List<ArraySegment<byte>>(segments.Count);
			int num = segments.Count - 1;
			for (int i = 0; i < segments.Count; i++)
			{
				ArraySegment<byte> arraySegment = segments[i];
				list.Add(new ArraySegment<byte>(EncodeDataFrame((i == 0) ? 2 : 0, i == num, arraySegment.Array, arraySegment.Offset, arraySegment.Count)));
			}
			websocket.Client?.Send(list);
		}

		public override void SendMessage(WebSocket websocket, string message)
		{
			SendMessage(websocket, 1, message);
		}

		public override void SendCloseHandshake(WebSocket websocket, int statusCode, string closeReason)
		{
			byte[] array = new byte[((!string.IsNullOrEmpty(closeReason)) ? Encoding.UTF8.GetMaxByteCount(closeReason.Length) : 0) + 2];
			int num = statusCode / 256;
			int num2 = statusCode % 256;
			array[0] = (byte)num;
			array[1] = (byte)num2;
			if (websocket != null && websocket.State != WebSocketState.Closed)
			{
				if (!string.IsNullOrEmpty(closeReason))
				{
					int bytes = Encoding.UTF8.GetBytes(closeReason, 0, closeReason.Length, array, 2);
					SendDataFragment(websocket, 8, array, 0, bytes + 2);
				}
				else
				{
					SendDataFragment(websocket, 8, array, 0, array.Length);
				}
			}
		}

		public override void SendPing(WebSocket websocket, string ping)
		{
			SendMessage(websocket, 9, ping);
		}

		public override void SendPong(WebSocket websocket, string pong)
		{
			SendMessage(websocket, 10, pong);
		}

		public override bool VerifyHandshake(WebSocket websocket, WebSocketCommandInfo handshakeInfo, out string description)
		{
			if (string.IsNullOrEmpty(handshakeInfo.Text))
			{
				description = "invalid handshake";
				return false;
			}
			string verbLine = string.Empty;
			if (!handshakeInfo.Text.ParseMimeHeader(websocket.Items, out verbLine))
			{
				description = "invalid handshake";
				return false;
			}
			if (!ValidateVerbLine(verbLine))
			{
				description = verbLine;
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
			if (!websocket.Items.GetValue(m_ExpectedAcceptKey, string.Empty).Equals(value2, StringComparison.OrdinalIgnoreCase))
			{
				description = "accept key doesn't match";
				return false;
			}
			description = string.Empty;
			return true;
		}

		private void GenerateMask(byte[] mask, int offset)
		{
			int num = Math.Min(offset + 4, mask.Length);
			for (int i = offset; i < num; i++)
			{
				mask[i] = (byte)m_Random.Next(0, 255);
			}
		}

		private void MaskData(byte[] rawData, int offset, int length, byte[] outputData, int outputOffset, byte[] mask, int maskOffset)
		{
			for (int i = 0; i < length; i++)
			{
				int num = offset + i;
				outputData[outputOffset++] = (byte)(rawData[num] ^ mask[maskOffset + i % 4]);
			}
		}
	}
}
