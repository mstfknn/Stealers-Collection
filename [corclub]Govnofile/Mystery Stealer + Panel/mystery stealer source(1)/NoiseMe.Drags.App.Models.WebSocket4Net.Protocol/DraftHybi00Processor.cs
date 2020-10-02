using NoiseMe.Drags.App.DTO.Linq;
using NoiseMe.Drags.App.Models.SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	internal class DraftHybi00Processor : ProtocolProcessorBase
	{
		private static List<char> m_CharLib;

		private static List<char> m_DigLib;

		private static Random m_Random;

		public const byte StartByte = 0;

		public const byte EndByte = byte.MaxValue;

		public static byte[] CloseHandshake;

		private byte[] m_ExpectedChallenge;

		private const string m_Error_ChallengeLengthNotMatch = "challenge length doesn't match";

		private const string m_Error_ChallengeNotMatch = "challenge doesn't match";

		private const string m_Error_InvalidHandshake = "invalid handshake";

		public override bool SupportBinary => false;

		public override bool SupportPingPong => false;

		public DraftHybi00Processor()
			: base(WebSocketVersion.DraftHybi00, new CloseStatusCodeHybi10())
		{
		}

		static DraftHybi00Processor()
		{
			m_CharLib = new List<char>();
			m_DigLib = new List<char>();
			m_Random = new Random();
			CloseHandshake = new byte[2]
			{
				255,
				0
			};
			for (int i = 33; i <= 126; i++)
			{
				char c = (char)i;
				if (char.IsLetter(c))
				{
					m_CharLib.Add(c);
				}
				else if (char.IsDigit(c))
				{
					m_DigLib.Add(c);
				}
			}
		}

		public override ReaderBase CreateHandshakeReader(WebSocket websocket)
		{
			return new DraftHybi00HandshakeReader(websocket);
		}

		public override bool VerifyHandshake(WebSocket websocket, WebSocketCommandInfo handshakeInfo, out string description)
		{
			byte[] data = handshakeInfo.Data;
			if (data.Length != data.Length)
			{
				description = "challenge length doesn't match";
				return false;
			}
			for (int i = 0; i < m_ExpectedChallenge.Length; i++)
			{
				if (data[i] != m_ExpectedChallenge[i])
				{
					description = "challenge doesn't match";
					return false;
				}
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
			description = string.Empty;
			return true;
		}

		public override void SendMessage(WebSocket websocket, string message)
		{
			byte[] array = new byte[Encoding.UTF8.GetMaxByteCount(message.Length) + 2];
			array[0] = 0;
			int bytes = Encoding.UTF8.GetBytes(message, 0, message.Length, array, 1);
			array[1 + bytes] = byte.MaxValue;
			websocket.Client.Send(array, 0, bytes + 2);
		}

		public override void SendData(WebSocket websocket, byte[] data, int offset, int length)
		{
			throw new NotSupportedException();
		}

		public override void SendData(WebSocket websocket, IList<ArraySegment<byte>> segments)
		{
			throw new NotSupportedException();
		}

		public override void SendCloseHandshake(WebSocket websocket, int statusCode, string closeReason)
		{
			if (websocket != null && websocket.State != WebSocketState.Closed)
			{
				websocket.Client.Send(CloseHandshake, 0, CloseHandshake.Length);
			}
		}

		public override void SendPing(WebSocket websocket, string ping)
		{
			throw new NotSupportedException();
		}

		public override void SendPong(WebSocket websocket, string pong)
		{
			throw new NotSupportedException();
		}

		public override void SendHandshake(WebSocket websocket)
		{
			string @string = Encoding.UTF8.GetString(GenerateSecKey());
			string string2 = Encoding.UTF8.GetString(GenerateSecKey());
			byte[] array = GenerateSecKey(8);
			m_ExpectedChallenge = GetResponseSecurityKey(@string, string2, array);
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
					stringBuilder.AppendFormatWithCrCf("{0}: {1}", keyValuePair2.Key, keyValuePair2.Value);
				}
			}
			stringBuilder.AppendWithCrCf();
			stringBuilder.Append(Encoding.UTF8.GetString(array, 0, array.Length));
			byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
			websocket.Client.Send(bytes, 0, bytes.Length);
		}

		private byte[] GetResponseSecurityKey(string secKey1, string secKey2, byte[] secKey3)
		{
			string s = Regex.Replace(secKey1, "[^0-9]", string.Empty);
			string s2 = Regex.Replace(secKey2, "[^0-9]", string.Empty);
			long num = long.Parse(s);
			long num2 = long.Parse(s2);
			int num3 = secKey1.Count((char c) => c == ' ');
			int num4 = secKey2.Count((char c) => c == ' ');
			int value = (int)(num / num3);
			int value2 = (int)(num2 / num4);
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

		private byte[] GenerateSecKey()
		{
			int totalLen = m_Random.Next(10, 20);
			return GenerateSecKey(totalLen);
		}

		private byte[] GenerateSecKey(int totalLen)
		{
			int num = m_Random.Next(1, totalLen / 2 + 1);
			int num2 = m_Random.Next(3, totalLen - 1 - num);
			int num3 = totalLen - num - num2;
			byte[] array = new byte[totalLen];
			int num4 = 0;
			for (int i = 0; i < num; i++)
			{
				array[num4++] = 32;
			}
			for (int j = 0; j < num2; j++)
			{
				array[num4++] = (byte)m_CharLib[m_Random.Next(0, m_CharLib.Count - 1)];
			}
			for (int k = 0; k < num3; k++)
			{
				array[num4++] = (byte)m_DigLib[m_Random.Next(0, m_DigLib.Count - 1)];
			}
			return array.RandomOrder();
		}
	}
}
