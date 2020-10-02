using NoiseMe.Drags.App.Models.SuperSocket.ClientEngine;
using System;
using System.Text;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	internal class HandshakeReader : ReaderBase
	{
		private const string m_BadRequestPrefix = "HTTP/1.1 400 ";

		protected static readonly string BadRequestCode;

		protected static readonly byte[] HeaderTerminator;

		private SearchMarkState<byte> m_HeadSeachState;

		protected static WebSocketCommandInfo DefaultHandshakeCommandInfo
		{
			get;
			private set;
		}

		static HandshakeReader()
		{
			BadRequestCode = 400.ToString();
			HeaderTerminator = Encoding.UTF8.GetBytes("\r\n\r\n");
		}

		public HandshakeReader(WebSocket websocket)
			: base(websocket)
		{
			m_HeadSeachState = new SearchMarkState<byte>(HeaderTerminator);
		}

		public override WebSocketCommandInfo GetCommandInfo(byte[] readBuffer, int offset, int length, out int left)
		{
			left = 0;
			int num = m_HeadSeachState.Matched;
			int num2 = readBuffer.SearchMark(offset, length, m_HeadSeachState);
			if (num2 < 0)
			{
				AddArraySegment(readBuffer, offset, length);
				return null;
			}
			int num3 = num2 - offset;
			string empty = string.Empty;
			if (base.BufferSegments.Count > 0)
			{
				if (num3 > 0)
				{
					AddArraySegment(readBuffer, offset, num3);
					empty = base.BufferSegments.Decode(Encoding.UTF8);
					num = 0;
				}
				else
				{
					empty = base.BufferSegments.Decode(Encoding.UTF8, 0, base.BufferSegments.Count - num);
				}
			}
			else
			{
				empty = Encoding.UTF8.GetString(readBuffer, offset, num3);
				num = 0;
			}
			left = length - num3 - (HeaderTerminator.Length - num);
			base.BufferSegments.ClearSegements();
			m_HeadSeachState.Matched = 0;
			if (!empty.StartsWith("HTTP/1.1 400 ", StringComparison.OrdinalIgnoreCase))
			{
				return new WebSocketCommandInfo
				{
					Key = (-1).ToString(),
					Text = empty
				};
			}
			return new WebSocketCommandInfo
			{
				Key = 400.ToString(),
				Text = empty
			};
		}
	}
}
