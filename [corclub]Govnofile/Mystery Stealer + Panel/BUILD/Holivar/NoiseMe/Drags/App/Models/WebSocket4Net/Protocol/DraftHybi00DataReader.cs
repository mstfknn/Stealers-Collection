using System;
using System.Text;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	// Token: 0x020000A0 RID: 160
	internal class DraftHybi00DataReader : ReaderBase
	{
		// Token: 0x0600059D RID: 1437 RVA: 0x00005807 File Offset: 0x00003A07
		public DraftHybi00DataReader(ReaderBase previousCommandReader) : base(previousCommandReader)
		{
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00018A48 File Offset: 0x00016C48
		public override WebSocketCommandInfo GetCommandInfo(byte[] readBuffer, int offset, int length, out int left)
		{
			left = 0;
			int num = 0;
			if (this.m_Type == null)
			{
				byte value = readBuffer[offset];
				num = 1;
				this.m_Type = new byte?(value);
			}
			if ((this.m_Type.Value & 128) == 0)
			{
				byte maxValue = byte.MaxValue;
				int i = offset + num;
				while (i < offset + length)
				{
					if (readBuffer[i] == maxValue)
					{
						left = length - (i - offset + 1);
						if (base.BufferSegments.Count <= 0)
						{
							WebSocketCommandInfo result = new WebSocketCommandInfo(1.ToString(), Encoding.UTF8.GetString(readBuffer, offset + num, i - offset - num));
							this.Reset(false);
							return result;
						}
						base.BufferSegments.AddSegment(readBuffer, offset + num, i - offset - num, false);
						WebSocketCommandInfo result2 = new WebSocketCommandInfo(1.ToString(), base.BufferSegments.Decode(Encoding.UTF8));
						this.Reset(true);
						return result2;
					}
					else
					{
						i++;
					}
				}
				base.AddArraySegment(readBuffer, offset + num, length - num);
				return null;
			}
			while (this.m_Length == null)
			{
				if (length <= num)
				{
					return null;
				}
				byte b = readBuffer[num];
				if (b == 0 && this.m_Type.Value == 255)
				{
					WebSocketCommandInfo result3 = new WebSocketCommandInfo(8.ToString());
					this.Reset(true);
					return result3;
				}
				int num2 = (int)(b & 127);
				this.m_TempLength = this.m_TempLength * 128 + num2;
				num++;
				if ((b & 128) != 128)
				{
					this.m_Length = new int?(this.m_TempLength);
					break;
				}
			}
			int num3 = this.m_Length.Value - base.BufferSegments.Count;
			int num4 = length - num;
			if (num4 < num3)
			{
				base.AddArraySegment(readBuffer, num, length - num);
				return null;
			}
			left = num4 - num3;
			if (base.BufferSegments.Count <= 0)
			{
				WebSocketCommandInfo result4 = new WebSocketCommandInfo(1.ToString(), Encoding.UTF8.GetString(readBuffer, offset + num, num3));
				this.Reset(false);
				return result4;
			}
			base.BufferSegments.AddSegment(readBuffer, offset + num, num3, false);
			WebSocketCommandInfo result5 = new WebSocketCommandInfo(base.BufferSegments.Decode(Encoding.UTF8));
			this.Reset(true);
			return result5;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00005810 File Offset: 0x00003A10
		private void Reset(bool clearBuffer)
		{
			this.m_Type = null;
			this.m_Length = null;
			this.m_TempLength = 0;
			if (clearBuffer)
			{
				base.BufferSegments.ClearSegements();
			}
		}

		// Token: 0x0400027A RID: 634
		private byte? m_Type;

		// Token: 0x0400027B RID: 635
		private int m_TempLength;

		// Token: 0x0400027C RID: 636
		private int? m_Length;

		// Token: 0x0400027D RID: 637
		private const byte m_ClosingHandshakeType = 255;
	}
}
