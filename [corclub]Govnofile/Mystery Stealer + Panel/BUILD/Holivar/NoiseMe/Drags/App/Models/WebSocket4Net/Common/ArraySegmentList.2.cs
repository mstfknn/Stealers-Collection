using System;
using System.Collections.Generic;
using System.Text;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Common
{
	// Token: 0x020000B9 RID: 185
	public class ArraySegmentList : ArraySegmentList<byte>
	{
		// Token: 0x06000668 RID: 1640 RVA: 0x00005DB1 File Offset: 0x00003FB1
		public string Decode(Encoding encoding)
		{
			return this.Decode(encoding, 0, base.Count);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001A7B8 File Offset: 0x000189B8
		public string Decode(Encoding encoding, int offset, int length)
		{
			if (length == 0)
			{
				return string.Empty;
			}
			IList<ArraySegmentEx<byte>> segments = base.Segments;
			if (segments == null || segments.Count <= 0)
			{
				return string.Empty;
			}
			char[] array = new char[encoding.GetMaxCharCount(base.Count)];
			int num = 0;
			int num2 = 0;
			int num3 = segments.Count - 1;
			bool flush = false;
			Decoder decoder = encoding.GetDecoder();
			int num4 = 0;
			if (offset > 0)
			{
				base.QuickSearchSegment(0, segments.Count - 1, offset, out num4);
			}
			for (int i = num4; i < segments.Count; i++)
			{
				ArraySegmentEx<byte> arraySegmentEx = segments[i];
				if (i == num3)
				{
					flush = true;
				}
				int byteIndex = arraySegmentEx.Offset;
				int num5 = Math.Min(length - num, arraySegmentEx.Count);
				if (i == num4 && offset > 0)
				{
					byteIndex = offset - arraySegmentEx.From + arraySegmentEx.Offset;
					num5 = Math.Min(arraySegmentEx.Count - offset + arraySegmentEx.From, num5);
				}
				int num6;
				int num7;
				bool flag;
				decoder.Convert(arraySegmentEx.Array, byteIndex, num5, array, num2, array.Length - num2, flush, out num6, out num7, out flag);
				num2 += num7;
				num += num6;
				if (num >= length)
				{
					break;
				}
			}
			return new string(array, 0, num2);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001A8EC File Offset: 0x00018AEC
		public void DecodeMask(byte[] mask, int offset, int length)
		{
			int num = mask.Length;
			int num2 = 0;
			ArraySegmentEx<byte> arraySegmentEx = base.QuickSearchSegment(0, base.Segments.Count - 1, offset, out num2);
			int num3 = Math.Min(length, arraySegmentEx.Count - offset + arraySegmentEx.From);
			int num4 = offset - arraySegmentEx.From + arraySegmentEx.Offset;
			int num5 = 0;
			for (int i = num4; i < num4 + num3; i++)
			{
				arraySegmentEx.Array[i] = (arraySegmentEx.Array[i] ^ mask[num5++ % num]);
			}
			if (num5 >= length)
			{
				return;
			}
			for (int j = num2 + 1; j < base.SegmentCount; j++)
			{
				ArraySegmentEx<byte> arraySegmentEx2 = base.Segments[j];
				num3 = Math.Min(length - num5, arraySegmentEx2.Count);
				for (int k = arraySegmentEx2.Offset; k < arraySegmentEx2.Offset + num3; k++)
				{
					arraySegmentEx2.Array[k] = (arraySegmentEx2.Array[k] ^ mask[num5++ % num]);
				}
				if (num5 >= length)
				{
					return;
				}
			}
		}
	}
}
