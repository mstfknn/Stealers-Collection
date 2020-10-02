using System.Text;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	internal class DraftHybi00DataReader : ReaderBase
	{
		private byte? m_Type;

		private int m_TempLength;

		private int? m_Length;

		private const byte m_ClosingHandshakeType = byte.MaxValue;

		public DraftHybi00DataReader(ReaderBase previousCommandReader)
			: base(previousCommandReader)
		{
		}

		public override WebSocketCommandInfo GetCommandInfo(byte[] readBuffer, int offset, int length, out int left)
		{
			left = 0;
			int num = 0;
			if (!m_Type.HasValue)
			{
				byte value = readBuffer[offset];
				num = 1;
				m_Type = value;
			}
			if ((m_Type.Value & 0x80) == 0)
			{
				byte b = byte.MaxValue;
				for (int i = offset + num; i < offset + length; i++)
				{
					if (readBuffer[i] == b)
					{
						left = length - (i - offset + 1);
						if (base.BufferSegments.Count <= 0)
						{
							WebSocketCommandInfo result = new WebSocketCommandInfo(1.ToString(), Encoding.UTF8.GetString(readBuffer, offset + num, i - offset - num));
							Reset(clearBuffer: false);
							return result;
						}
						base.BufferSegments.AddSegment(readBuffer, offset + num, i - offset - num, toBeCopied: false);
						WebSocketCommandInfo result2 = new WebSocketCommandInfo(1.ToString(), base.BufferSegments.Decode(Encoding.UTF8));
						Reset(clearBuffer: true);
						return result2;
					}
				}
				AddArraySegment(readBuffer, offset + num, length - num);
				return null;
			}
			while (!m_Length.HasValue)
			{
				if (length <= num)
				{
					return null;
				}
				byte b2 = readBuffer[num];
				if (b2 == 0 && m_Type.Value == byte.MaxValue)
				{
					WebSocketCommandInfo result3 = new WebSocketCommandInfo(8.ToString());
					Reset(clearBuffer: true);
					return result3;
				}
				int num2 = b2 & 0x7F;
				m_TempLength = m_TempLength * 128 + num2;
				num++;
				if ((b2 & 0x80) != 128)
				{
					m_Length = m_TempLength;
					break;
				}
			}
			int num3 = m_Length.Value - base.BufferSegments.Count;
			int num4 = length - num;
			if (num4 < num3)
			{
				AddArraySegment(readBuffer, num, length - num);
				return null;
			}
			left = num4 - num3;
			if (base.BufferSegments.Count <= 0)
			{
				WebSocketCommandInfo result4 = new WebSocketCommandInfo(1.ToString(), Encoding.UTF8.GetString(readBuffer, offset + num, num3));
				Reset(clearBuffer: false);
				return result4;
			}
			base.BufferSegments.AddSegment(readBuffer, offset + num, num3, toBeCopied: false);
			WebSocketCommandInfo result5 = new WebSocketCommandInfo(base.BufferSegments.Decode(Encoding.UTF8));
			Reset(clearBuffer: true);
			return result5;
		}

		private void Reset(bool clearBuffer)
		{
			m_Type = null;
			m_Length = null;
			m_TempLength = 0;
			if (clearBuffer)
			{
				base.BufferSegments.ClearSegements();
			}
		}
	}
}
