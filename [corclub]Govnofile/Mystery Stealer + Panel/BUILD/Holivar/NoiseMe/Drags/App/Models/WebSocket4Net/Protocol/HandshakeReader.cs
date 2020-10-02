using System;
using System.Text;
using NoiseMe.Drags.App.Models.SuperSocket.ClientEngine;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	// Token: 0x020000A7 RID: 167
	internal class HandshakeReader : ReaderBase
	{
		// Token: 0x060005D3 RID: 1491 RVA: 0x000059AC File Offset: 0x00003BAC
		public HandshakeReader(WebSocket websocket) : base(websocket)
		{
			this.m_HeadSeachState = new SearchMarkState<byte>(HandshakeReader.HeaderTerminator);
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x000059C5 File Offset: 0x00003BC5
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x000059CC File Offset: 0x00003BCC
		private protected static WebSocketCommandInfo DefaultHandshakeCommandInfo { protected get; private set; }

		// Token: 0x060005D6 RID: 1494 RVA: 0x00019B8C File Offset: 0x00017D8C
		public override WebSocketCommandInfo GetCommandInfo(byte[] readBuffer, int offset, int length, out int left)
		{
			left = 0;
			int num = this.m_HeadSeachState.Matched;
			int num2 = readBuffer.SearchMark(offset, length, this.m_HeadSeachState);
			if (num2 < 0)
			{
				base.AddArraySegment(readBuffer, offset, length);
				return null;
			}
			int num3 = num2 - offset;
			string text = string.Empty;
			if (base.BufferSegments.Count > 0)
			{
				if (num3 > 0)
				{
					base.AddArraySegment(readBuffer, offset, num3);
					text = base.BufferSegments.Decode(Encoding.UTF8);
					num = 0;
				}
				else
				{
					text = base.BufferSegments.Decode(Encoding.UTF8, 0, base.BufferSegments.Count - num);
				}
			}
			else
			{
				text = Encoding.UTF8.GetString(readBuffer, offset, num3);
				num = 0;
			}
			left = length - num3 - (HandshakeReader.HeaderTerminator.Length - num);
			base.BufferSegments.ClearSegements();
			this.m_HeadSeachState.Matched = 0;
			if (!text.StartsWith("HTTP/1.1 400 ", StringComparison.OrdinalIgnoreCase))
			{
				return new WebSocketCommandInfo
				{
					Key = -1.ToString(),
					Text = text
				};
			}
			return new WebSocketCommandInfo
			{
				Key = 400.ToString(),
				Text = text
			};
		}

		// Token: 0x0400029A RID: 666
		private const string m_BadRequestPrefix = "HTTP/1.1 400 ";

		// Token: 0x0400029B RID: 667
		protected static readonly string BadRequestCode = 400.ToString();

		// Token: 0x0400029C RID: 668
		protected static readonly byte[] HeaderTerminator = Encoding.UTF8.GetBytes("\r\n\r\n");

		// Token: 0x0400029D RID: 669
		private SearchMarkState<byte> m_HeadSeachState;
	}
}
