using System;
using System.Collections.Generic;
using System.Text;
using NoiseMe.Drags.App.DTO.Linq;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000DA RID: 218
	public static class Extensions
	{
		// Token: 0x0600070F RID: 1807 RVA: 0x0001B75C File Offset: 0x0001995C
		public static string GetString(this Encoding encoding, IList<ArraySegment<byte>> data)
		{
			int byteCount = data.Sum((ArraySegment<byte> x) => x.Count);
			char[] array = new char[encoding.GetMaxCharCount(byteCount)];
			Decoder decoder = encoding.GetDecoder();
			int num = 0;
			int num2 = data.Count - 1;
			int num3 = 0;
			int num4 = 0;
			bool flag = false;
			for (int i = 0; i < data.Count; i++)
			{
				ArraySegment<byte> arraySegment = data[i];
				decoder.Convert(arraySegment.Array, arraySegment.Offset, arraySegment.Count, array, num, array.Length - num, i == num2, out num3, out num4, out flag);
				num += num4;
			}
			return new string(array, 0, num);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001B814 File Offset: 0x00019A14
		public static string GetString(this Encoding encoding, IList<ArraySegment<byte>> data, int offset, int length)
		{
			char[] array = new char[encoding.GetMaxCharCount(length)];
			Decoder decoder = encoding.GetDecoder();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			bool flag = false;
			int num5 = 0;
			int i = 0;
			while (i < data.Count)
			{
				ArraySegment<byte> arraySegment = data[i];
				int num6 = arraySegment.Offset;
				int num7 = arraySegment.Count;
				bool flush = false;
				if (num2 == 0)
				{
					int num8 = num5 + arraySegment.Count - 1;
					if (offset > num8)
					{
						num5 = num8 + 1;
					}
					else
					{
						int num9 = offset - num5;
						num6 += num9;
						num7 -= num9;
						if (num7 >= length)
						{
							num7 = length;
							flush = true;
							goto IL_9C;
						}
						goto IL_9C;
					}
				}
				else
				{
					int num10 = length - num2;
					if (num10 <= num7)
					{
						num7 = num10;
						flush = true;
						goto IL_9C;
					}
					goto IL_9C;
				}
				IL_C6:
				i++;
				continue;
				IL_9C:
				decoder.Convert(arraySegment.Array, num6, num7, array, num, array.Length - num, flush, out num3, out num4, out flag);
				num += num4;
				num2 += num3;
				goto IL_C6;
			}
			return new string(array, 0, num);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00006177 File Offset: 0x00004377
		public static BufferStream GetBufferStream<TPackageInfo>(this IReceiveFilter<TPackageInfo> receiveFilter, IList<ArraySegment<byte>> data) where TPackageInfo : IPackageInfo
		{
			return receiveFilter.GetBufferStream(data);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00006180 File Offset: 0x00004380
		public static BufferStream GetBufferStream<TStream, TPackageInfo>(this IReceiveFilter<TPackageInfo> receiveFilter, IList<ArraySegment<byte>> data) where TStream : BufferStream, new() where TPackageInfo : IPackageInfo
		{
			BufferStream current = BufferStream.GetCurrent<BufferStream>();
			current.Initialize(data);
			return current;
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0000618E File Offset: 0x0000438E
		public static void CopyTo(this IList<ArraySegment<byte>> packageData, ArraySegment<byte> data)
		{
			packageData.CopyTo(data, 0, data.Count);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001B904 File Offset: 0x00019B04
		public static void CopyTo(this IList<ArraySegment<byte>> packageData, ArraySegment<byte> data, int srcOffset, int length)
		{
			int num = srcOffset;
			int num2 = length;
			for (int i = 0; i < packageData.Count; i++)
			{
				ArraySegment<byte> arraySegment = packageData[i];
				if (arraySegment.Count <= num)
				{
					num -= arraySegment.Count;
				}
				else
				{
					int num3 = Math.Min(num2, arraySegment.Count - num);
					Array.Copy(arraySegment.Array, num, data.Array, data.Offset, num3);
					num2 -= num3;
					if (num2 <= 0)
					{
						break;
					}
				}
			}
		}
	}
}
