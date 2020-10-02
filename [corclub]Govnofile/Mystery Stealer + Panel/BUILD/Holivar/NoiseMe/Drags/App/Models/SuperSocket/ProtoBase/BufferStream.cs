using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000D2 RID: 210
	public class BufferStream : Stream, IBufferStream
	{
		// Token: 0x060006D8 RID: 1752 RVA: 0x0001AE50 File Offset: 0x00019050
		public void Initialize(IList<ArraySegment<byte>> segments)
		{
			if (segments.Count <= 0)
			{
				throw new ArgumentException("The length of segments must be greater than zero.");
			}
			this.m_Segments = segments;
			this.m_CurrentSegmentIndex = 0;
			this.m_CurrentSegmentOffset = segments[0].Offset;
			this.m_Position = 0L;
			long num = 0L;
			for (int i = 0; i < segments.Count; i++)
			{
				num += (long)segments[i].Count;
			}
			this.m_Length = num;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00006039 File Offset: 0x00004239
		public static BufferStream GetCurrent()
		{
			return BufferStream.GetCurrent<BufferStream>();
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0001AECC File Offset: 0x000190CC
		public static TStream GetCurrent<TStream>() where TStream : BufferStream, new()
		{
			LocalDataStoreSlot namedDataSlot = Thread.GetNamedDataSlot("ThreadBufferListStream");
			TStream tstream = Thread.GetData(namedDataSlot) as TStream;
			if (tstream != null)
			{
				return tstream;
			}
			tstream = Activator.CreateInstance<TStream>();
			Thread.SetData(namedDataSlot, tstream);
			return tstream;
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x00006040 File Offset: 0x00004240
		public IList<ArraySegment<byte>> Buffers
		{
			get
			{
				return this.m_Segments;
			}
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00004685 File Offset: 0x00002885
		public Stream GetCurrentStream()
		{
			return this;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00006048 File Offset: 0x00004248
		public void Reset()
		{
			this.m_Segments = null;
			this.m_CurrentSegmentIndex = 0;
			this.m_CurrentSegmentOffset = 0;
			this.m_Length = 0L;
			this.m_Position = 0L;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001AF14 File Offset: 0x00019114
		public void Clear()
		{
			IList<ArraySegment<byte>> segments = this.m_Segments;
			if (segments != null)
			{
				segments.Clear();
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0000606F File Offset: 0x0000426F
		public override bool CanRead
		{
			get
			{
				return this.m_Position < this.m_Length;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00003147 File Offset: 0x00001347
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x000031DF File Offset: 0x000013DF
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0000250E File Offset: 0x0000070E
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0000607F File Offset: 0x0000427F
		public override long Length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00006087 File Offset: 0x00004287
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x0000608F File Offset: 0x0000428F
		public override long Position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001AF34 File Offset: 0x00019134
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this.m_CurrentSegmentOffset;
			int num2 = 0;
			int currentSegmentIndex = 0;
			for (int i = this.m_CurrentSegmentIndex; i < this.m_Segments.Count; i++)
			{
				ArraySegment<byte> arraySegment = this.m_Segments[i];
				if (i != this.m_CurrentSegmentIndex)
				{
					num = arraySegment.Offset;
				}
				int num3 = count - num2;
				int num4 = arraySegment.Count - (num - arraySegment.Offset);
				int num5 = Math.Min(num4, num3);
				Buffer.BlockCopy(arraySegment.Array, num, buffer, offset + num2, num5);
				num2 += num5;
				currentSegmentIndex = i;
				if (num2 >= count)
				{
					if (num4 > num3)
					{
						this.m_CurrentSegmentIndex = i;
						this.m_CurrentSegmentOffset = num + num5;
					}
					else
					{
						int num6 = i + 1;
						if (num6 >= this.m_Segments.Count)
						{
							this.m_CurrentSegmentIndex = i;
							this.m_CurrentSegmentOffset = num + num5;
						}
						else
						{
							this.m_CurrentSegmentIndex = num6;
							this.m_CurrentSegmentOffset = this.m_Segments[num6].Offset;
						}
					}
					this.m_Position += (long)num2;
					return num2;
				}
			}
			this.m_CurrentSegmentIndex = currentSegmentIndex;
			this.m_CurrentSegmentOffset = 0;
			this.m_Position = this.m_Length;
			return num2;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001B060 File Offset: 0x00019260
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (origin == SeekOrigin.End)
			{
				throw new ArgumentException("Cannot support seek from the end.");
			}
			if (origin == SeekOrigin.Begin)
			{
				this.m_CurrentSegmentIndex = 0;
				this.m_CurrentSegmentOffset = this.m_Segments[0].Offset;
				this.m_Position = 0L;
			}
			if (offset == 0L)
			{
				return this.m_Position;
			}
			long num = ((origin == SeekOrigin.Begin) ? 0L : this.m_Position) + offset;
			long num2 = this.m_Position;
			for (int i = this.m_CurrentSegmentIndex; i < this.m_Segments.Count; i++)
			{
				ArraySegment<byte> arraySegment = this.m_Segments[i];
				if (i == this.m_CurrentSegmentIndex)
				{
					int currentSegmentOffset = this.m_CurrentSegmentOffset;
					num2 += (long)(arraySegment.Count - (this.m_CurrentSegmentOffset - arraySegment.Offset));
				}
				else
				{
					int offset2 = arraySegment.Offset;
					num2 += (long)arraySegment.Count;
				}
				if (num2 >= num)
				{
					int num3 = (int)(num2 - num);
					this.m_CurrentSegmentIndex = i;
					this.m_CurrentSegmentOffset = arraySegment.Offset + arraySegment.Count - num3;
					this.m_Position = num;
					return num;
				}
			}
			throw new Exception("Exceed the stream's end");
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0000250E File Offset: 0x0000070E
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0000250E File Offset: 0x0000070E
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0000609A File Offset: 0x0000429A
		private void CheckInitialized()
		{
			if (this.m_Segments == null)
			{
				throw new InvalidOperationException("Not initialized");
			}
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001B174 File Offset: 0x00019374
		public IBufferStream Skip(int count)
		{
			this.CheckInitialized();
			if (count == 0)
			{
				return this;
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Count cannot be less than zero.");
			}
			if (this.Length < (long)count)
			{
				throw new ArgumentOutOfRangeException("count", "Cannot be greater than the length of all the buffers.");
			}
			this.Position += (long)count;
			return this;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001B1CC File Offset: 0x000193CC
		private IList<ArraySegment<byte>> Clone(int index, int segmentOffset, int length)
		{
			List<ArraySegment<byte>> list = new List<ArraySegment<byte>>();
			int num = length;
			IList<ArraySegment<byte>> segments = this.m_Segments;
			for (int i = index; i < segments.Count; i++)
			{
				ArraySegment<byte> arraySegment = segments[i];
				int offset = arraySegment.Offset;
				int num2 = arraySegment.Count;
				if (i == index)
				{
					offset = segmentOffset;
					num2 = arraySegment.Count - (segmentOffset - arraySegment.Offset);
				}
				num2 = Math.Min(num2, num);
				list.Add(new ArraySegment<byte>(arraySegment.Array, offset, num2));
				num -= num2;
				if (num <= 0)
				{
					break;
				}
			}
			return list;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001B258 File Offset: 0x00019458
		public IList<ArraySegment<byte>> Take(int length)
		{
			BufferList bufferList = this.m_Segments as BufferList;
			if (bufferList != null)
			{
				return bufferList.Clone(this.m_CurrentSegmentIndex, this.m_CurrentSegmentOffset, length);
			}
			return this.Clone(this.m_CurrentSegmentIndex, this.m_CurrentSegmentOffset, length);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001B29C File Offset: 0x0001949C
		public string ReadString(int length, Encoding encoding)
		{
			int num = length;
			char[] array = new char[encoding.GetMaxCharCount(length)];
			Decoder decoder = encoding.GetDecoder();
			int i = this.m_CurrentSegmentIndex;
			int num2 = this.m_CurrentSegmentOffset;
			int num3 = 0;
			while (i < this.m_Segments.Count)
			{
				ArraySegment<byte> arraySegment = this.m_Segments[i];
				if (i != this.m_CurrentSegmentIndex)
				{
					num2 = arraySegment.Offset;
				}
				int num4;
				if (num2 + length < arraySegment.Offset + arraySegment.Count)
				{
					int byteCount = length;
					int charCount = array.Length - num3;
					int num5;
					bool flag;
					decoder.Convert(arraySegment.Array, num2, byteCount, array, num3, charCount, true, out num4, out num5, out flag);
					num3 += num5;
				}
				else
				{
					int num6 = arraySegment.Count - (num2 - arraySegment.Offset);
					bool flush = num6 == length;
					int charCount2 = array.Length - num3;
					int num5;
					bool flag;
					decoder.Convert(arraySegment.Array, num2, num6, array, num3, charCount2, flush, out num4, out num5, out flag);
					num3 += num5;
				}
				length -= num4;
				num2 += num4;
				if (length == 0)
				{
					break;
				}
				if (i != this.m_Segments.Count - 1)
				{
					i++;
				}
			}
			this.m_CurrentSegmentIndex = i;
			this.m_CurrentSegmentOffset = num2;
			this.m_Position += (long)num;
			return new string(array, 0, num3);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x000060AF File Offset: 0x000042AF
		protected void FillBuffer(int length)
		{
			if (length > 8)
			{
				throw new ArgumentOutOfRangeException("length", "the length must between 1 and 8");
			}
			if (this.Read(this.m_Buffer, 0, length) != length)
			{
				throw new ArgumentOutOfRangeException("length", "there is no enough data to read");
			}
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x000060E6 File Offset: 0x000042E6
		public short ReadInt16()
		{
			return this.ReadInt16(false);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001B3E0 File Offset: 0x000195E0
		public short ReadInt16(bool littleEndian)
		{
			this.FillBuffer(2);
			byte[] buffer = this.m_Buffer;
			if (!littleEndian)
			{
				return (short)this.BigEndianFromBytes(buffer, 2);
			}
			return (short)this.LittleEndianFromBytes(buffer, 2);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x000060EF File Offset: 0x000042EF
		public ushort ReadUInt16()
		{
			return this.ReadUInt16(false);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001B414 File Offset: 0x00019614
		public ushort ReadUInt16(bool littleEndian)
		{
			this.FillBuffer(2);
			byte[] buffer = this.m_Buffer;
			if (!littleEndian)
			{
				return (ushort)this.BigEndianFromBytes(buffer, 2);
			}
			return (ushort)this.LittleEndianFromBytes(buffer, 2);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x000060F8 File Offset: 0x000042F8
		public int ReadInt32()
		{
			return this.ReadInt32(false);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001B448 File Offset: 0x00019648
		public int ReadInt32(bool littleEndian)
		{
			this.FillBuffer(4);
			byte[] buffer = this.m_Buffer;
			if (!littleEndian)
			{
				return (int)this.BigEndianFromBytes(buffer, 4);
			}
			return (int)this.LittleEndianFromBytes(buffer, 4);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00006101 File Offset: 0x00004301
		public uint ReadUInt32()
		{
			return this.ReadUInt32(false);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001B47C File Offset: 0x0001967C
		public uint ReadUInt32(bool littleEndian)
		{
			this.FillBuffer(4);
			byte[] buffer = this.m_Buffer;
			if (!littleEndian)
			{
				return (uint)this.BigEndianFromBytes(buffer, 4);
			}
			return (uint)this.LittleEndianFromBytes(buffer, 4);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0000610A File Offset: 0x0000430A
		public long ReadInt64()
		{
			return this.ReadInt64(false);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001B4B0 File Offset: 0x000196B0
		public long ReadInt64(bool littleEndian)
		{
			this.FillBuffer(8);
			byte[] buffer = this.m_Buffer;
			if (!littleEndian)
			{
				return this.BigEndianFromBytes(buffer, 8);
			}
			return this.LittleEndianFromBytes(buffer, 8);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00006113 File Offset: 0x00004313
		public ulong ReadUInt64()
		{
			return this.ReadUInt64(false);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001B4B0 File Offset: 0x000196B0
		public ulong ReadUInt64(bool littleEndian)
		{
			this.FillBuffer(8);
			byte[] buffer = this.m_Buffer;
			if (!littleEndian)
			{
				return (ulong)this.BigEndianFromBytes(buffer, 8);
			}
			return (ulong)this.LittleEndianFromBytes(buffer, 8);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001B4E0 File Offset: 0x000196E0
		private long BigEndianFromBytes(byte[] buffer, int bytesToConvert)
		{
			long num = 0L;
			for (int i = 0; i < bytesToConvert; i++)
			{
				num = (num << 8 | (long)((ulong)buffer[i]));
			}
			return num;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001B508 File Offset: 0x00019708
		private long LittleEndianFromBytes(byte[] buffer, int bytesToConvert)
		{
			long num = 0L;
			for (int i = 0; i < bytesToConvert; i++)
			{
				num = (num << 8 | (long)((ulong)buffer[bytesToConvert - 1 - i]));
			}
			return num;
		}

		// Token: 0x040002D2 RID: 722
		private IList<ArraySegment<byte>> m_Segments;

		// Token: 0x040002D3 RID: 723
		private long m_Position;

		// Token: 0x040002D4 RID: 724
		private int m_CurrentSegmentIndex;

		// Token: 0x040002D5 RID: 725
		private int m_CurrentSegmentOffset;

		// Token: 0x040002D6 RID: 726
		private long m_Length;

		// Token: 0x040002D7 RID: 727
		private byte[] m_Buffer = new byte[8];

		// Token: 0x040002D8 RID: 728
		private const string c_ThreadBufferSegmentStream = "ThreadBufferListStream";
	}
}
