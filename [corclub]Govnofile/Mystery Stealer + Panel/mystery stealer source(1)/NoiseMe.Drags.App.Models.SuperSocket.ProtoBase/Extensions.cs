using NoiseMe.Drags.App.DTO.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public static class Extensions
	{
		public static string GetString(this Encoding encoding, IList<ArraySegment<byte>> data)
		{
			int byteCount = data.Sum((ArraySegment<byte> x) => x.Count);
			char[] array = new char[encoding.GetMaxCharCount(byteCount)];
			Decoder decoder = encoding.GetDecoder();
			int num = 0;
			int num2 = data.Count - 1;
			int bytesUsed = 0;
			int charsUsed = 0;
			bool completed = false;
			for (int i = 0; i < data.Count; i++)
			{
				ArraySegment<byte> arraySegment = data[i];
				decoder.Convert(arraySegment.Array, arraySegment.Offset, arraySegment.Count, array, num, array.Length - num, i == num2, out bytesUsed, out charsUsed, out completed);
				num += charsUsed;
			}
			return new string(array, 0, num);
		}

		public static string GetString(this Encoding encoding, IList<ArraySegment<byte>> data, int offset, int length)
		{
			char[] array = new char[encoding.GetMaxCharCount(length)];
			Decoder decoder = encoding.GetDecoder();
			int num = 0;
			int num2 = 0;
			int bytesUsed = 0;
			int charsUsed = 0;
			bool completed = false;
			int num3 = 0;
			for (int i = 0; i < data.Count; i++)
			{
				ArraySegment<byte> arraySegment = data[i];
				int num4 = arraySegment.Offset;
				int num5 = arraySegment.Count;
				bool flush = false;
				if (num2 == 0)
				{
					int num6 = num3 + arraySegment.Count - 1;
					if (offset > num6)
					{
						num3 = num6 + 1;
						continue;
					}
					int num7 = offset - num3;
					num4 += num7;
					num5 -= num7;
					if (num5 >= length)
					{
						num5 = length;
						flush = true;
					}
				}
				else
				{
					int num8 = length - num2;
					if (num8 <= num5)
					{
						num5 = num8;
						flush = true;
					}
				}
				decoder.Convert(arraySegment.Array, num4, num5, array, num, array.Length - num, flush, out bytesUsed, out charsUsed, out completed);
				num += charsUsed;
				num2 += bytesUsed;
			}
			return new string(array, 0, num);
		}

		public static BufferStream GetBufferStream<TPackageInfo>(this IReceiveFilter<TPackageInfo> receiveFilter, IList<ArraySegment<byte>> data) where TPackageInfo : IPackageInfo
		{
			return receiveFilter.GetBufferStream<BufferStream, TPackageInfo>(data);
		}

		public static BufferStream GetBufferStream<TStream, TPackageInfo>(this IReceiveFilter<TPackageInfo> receiveFilter, IList<ArraySegment<byte>> data) where TStream : BufferStream, new()where TPackageInfo : IPackageInfo
		{
			BufferStream current = BufferStream.GetCurrent<BufferStream>();
			current.Initialize(data);
			return current;
		}

		public static void CopyTo(this IList<ArraySegment<byte>> packageData, ArraySegment<byte> data)
		{
			packageData.CopyTo(data, 0, data.Count);
		}

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
					continue;
				}
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
