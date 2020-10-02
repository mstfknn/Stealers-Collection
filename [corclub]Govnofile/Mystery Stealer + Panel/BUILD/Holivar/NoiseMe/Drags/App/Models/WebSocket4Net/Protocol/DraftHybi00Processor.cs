using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using NoiseMe.Drags.App.DTO.Linq;
using NoiseMe.Drags.App.Models.SuperSocket.ClientEngine;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	// Token: 0x020000A2 RID: 162
	internal class DraftHybi00Processor : ProtocolProcessorBase
	{
		// Token: 0x060005A3 RID: 1443 RVA: 0x00005872 File Offset: 0x00003A72
		public DraftHybi00Processor() : base(WebSocketVersion.DraftHybi00, new CloseStatusCodeHybi10())
		{
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00018E24 File Offset: 0x00017024
		static DraftHybi00Processor()
		{
			byte[] array = new byte[2];
			array[0] = byte.MaxValue;
			DraftHybi00Processor.CloseHandshake = array;
			for (int i = 33; i <= 126; i++)
			{
				char c = (char)i;
				if (char.IsLetter(c))
				{
					DraftHybi00Processor.m_CharLib.Add(c);
				}
				else if (char.IsDigit(c))
				{
					DraftHybi00Processor.m_DigLib.Add(c);
				}
			}
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00005880 File Offset: 0x00003A80
		public override ReaderBase CreateHandshakeReader(WebSocket websocket)
		{
			return new DraftHybi00HandshakeReader(websocket);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00018E9C File Offset: 0x0001709C
		public override bool VerifyHandshake(WebSocket websocket, WebSocketCommandInfo handshakeInfo, out string description)
		{
			byte[] data = handshakeInfo.Data;
			if (data.Length != data.Length)
			{
				description = "challenge length doesn't match";
				return false;
			}
			for (int i = 0; i < this.m_ExpectedChallenge.Length; i++)
			{
				if (data[i] != this.m_ExpectedChallenge[i])
				{
					description = "challenge doesn't match";
					return false;
				}
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
			description = string.Empty;
			return true;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00018F24 File Offset: 0x00017124
		public override void SendMessage(WebSocket websocket, string message)
		{
			byte[] array = new byte[Encoding.UTF8.GetMaxByteCount(message.Length) + 2];
			array[0] = 0;
			int bytes = Encoding.UTF8.GetBytes(message, 0, message.Length, array, 1);
			array[1 + bytes] = byte.MaxValue;
			websocket.Client.Send(array, 0, bytes + 2);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0000250E File Offset: 0x0000070E
		public override void SendData(WebSocket websocket, byte[] data, int offset, int length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0000250E File Offset: 0x0000070E
		public override void SendData(WebSocket websocket, IList<ArraySegment<byte>> segments)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00005888 File Offset: 0x00003A88
		public override void SendCloseHandshake(WebSocket websocket, int statusCode, string closeReason)
		{
			if (websocket == null || websocket.State == WebSocketState.Closed)
			{
				return;
			}
			websocket.Client.Send(DraftHybi00Processor.CloseHandshake, 0, DraftHybi00Processor.CloseHandshake.Length);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0000250E File Offset: 0x0000070E
		public override void SendPing(WebSocket websocket, string ping)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0000250E File Offset: 0x0000070E
		public override void SendPong(WebSocket websocket, string pong)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00018F7C File Offset: 0x0001717C
		public override void SendHandshake(WebSocket websocket)
		{
			string @string = Encoding.UTF8.GetString(this.GenerateSecKey());
			string string2 = Encoding.UTF8.GetString(this.GenerateSecKey());
			byte[] array = this.GenerateSecKey(8);
			this.m_ExpectedChallenge = this.GetResponseSecurityKey(@string, string2, array);
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
			stringBuilder.AppendWithCrCf(websocket.TargetUri.Host);
			stringBuilder.AppendWithCrCf("Upgrade: WebSocket");
			stringBuilder.AppendWithCrCf("Connection: Upgrade");
			stringBuilder.Append("Sec-WebSocket-Key1: ");
			stringBuilder.AppendWithCrCf(@string);
			stringBuilder.Append("Sec-WebSocket-Key2: ");
			stringBuilder.AppendWithCrCf(string2);
			stringBuilder.Append("Origin: ");
			stringBuilder.AppendWithCrCf(string.IsNullOrEmpty(websocket.Origin) ? websocket.TargetUri.Host : websocket.Origin);
			if (!string.IsNullOrEmpty(websocket.SubProtocol))
			{
				stringBuilder.Append("Sec-WebSocket-Protocol: ");
				stringBuilder.AppendWithCrCf(websocket.SubProtocol);
			}
			List<KeyValuePair<string, string>> cookies = websocket.Cookies;
			if (cookies != null && cookies.Count > 0)
			{
				string[] array2 = new string[cookies.Count];
				for (int i = 0; i < cookies.Count; i++)
				{
					KeyValuePair<string, string> keyValuePair = cookies[i];
					array2[i] = keyValuePair.Key + "=" + Uri.EscapeUriString(keyValuePair.Value);
				}
				stringBuilder.Append("Cookie: ");
				stringBuilder.AppendWithCrCf(string.Join(";", array2));
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
			stringBuilder.Append(Encoding.UTF8.GetString(array, 0, array.Length));
			byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
			websocket.Client.Send(bytes, 0, bytes.Length);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x000191CC File Offset: 0x000173CC
		private byte[] GetResponseSecurityKey(string secKey1, string secKey2, byte[] secKey3)
		{
			string s = Regex.Replace(secKey1, "[^0-9]", string.Empty);
			string s2 = Regex.Replace(secKey2, "[^0-9]", string.Empty);
			long num = long.Parse(s);
			long num2 = long.Parse(s2);
			int num3 = secKey1.Count((char c) => c == ' ');
			int num4 = secKey2.Count((char c) => c == ' ');
			int value = (int)(num / (long)num3);
			int value2 = (int)(num2 / (long)num4);
			byte[] bytes = BitConverter.GetBytes(value);
			Array.Reverse(bytes);
			byte[] bytes2 = BitConverter.GetBytes(value2);
			Array.Reverse(bytes2);
			byte[] array = new byte[bytes.Length + bytes2.Length + secKey3.Length];
			Array.Copy(bytes, 0, array, 0, bytes.Length);
			Array.Copy(bytes2, 0, array, bytes.Length, bytes2.Length);
			Array.Copy(secKey3, 0, array, bytes.Length + bytes2.Length, secKey3.Length);
			return array.ComputeMD5Hash();
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000192D8 File Offset: 0x000174D8
		private byte[] GenerateSecKey()
		{
			int totalLen = DraftHybi00Processor.m_Random.Next(10, 20);
			return this.GenerateSecKey(totalLen);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000192FC File Offset: 0x000174FC
		private byte[] GenerateSecKey(int totalLen)
		{
			int num = DraftHybi00Processor.m_Random.Next(1, totalLen / 2 + 1);
			int num2 = DraftHybi00Processor.m_Random.Next(3, totalLen - 1 - num);
			int num3 = totalLen - num - num2;
			byte[] array = new byte[totalLen];
			int num4 = 0;
			for (int i = 0; i < num; i++)
			{
				array[num4++] = 32;
			}
			for (int j = 0; j < num2; j++)
			{
				array[num4++] = (byte)DraftHybi00Processor.m_CharLib[DraftHybi00Processor.m_Random.Next(0, DraftHybi00Processor.m_CharLib.Count - 1)];
			}
			for (int k = 0; k < num3; k++)
			{
				array[num4++] = (byte)DraftHybi00Processor.m_DigLib[DraftHybi00Processor.m_Random.Next(0, DraftHybi00Processor.m_DigLib.Count - 1)];
			}
			return array.RandomOrder<byte>();
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x000031DF File Offset: 0x000013DF
		public override bool SupportBinary
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x000031DF File Offset: 0x000013DF
		public override bool SupportPingPong
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000282 RID: 642
		private static List<char> m_CharLib = new List<char>();

		// Token: 0x04000283 RID: 643
		private static List<char> m_DigLib = new List<char>();

		// Token: 0x04000284 RID: 644
		private static Random m_Random = new Random();

		// Token: 0x04000285 RID: 645
		public const byte StartByte = 0;

		// Token: 0x04000286 RID: 646
		public const byte EndByte = 255;

		// Token: 0x04000287 RID: 647
		public static byte[] CloseHandshake;

		// Token: 0x04000288 RID: 648
		private byte[] m_ExpectedChallenge;

		// Token: 0x04000289 RID: 649
		private const string m_Error_ChallengeLengthNotMatch = "challenge length doesn't match";

		// Token: 0x0400028A RID: 650
		private const string m_Error_ChallengeNotMatch = "challenge doesn't match";

		// Token: 0x0400028B RID: 651
		private const string m_Error_InvalidHandshake = "invalid handshake";
	}
}
