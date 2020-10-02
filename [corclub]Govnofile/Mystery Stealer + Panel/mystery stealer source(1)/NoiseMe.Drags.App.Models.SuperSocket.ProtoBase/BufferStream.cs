using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public class BufferStream : Stream, IBufferStream
	{
		private IList<ArraySegment<byte>> m_Segments;

		private long m_Position;

		private int m_CurrentSegmentIndex;

		private int m_CurrentSegmentOffset;

		private long m_Length;

		private byte[] m_Buffer = new byte[8];

		private const string c_ThreadBufferSegmentStream = "ThreadBufferListStream";

		public IList<ArraySegment<byte>> Buffers => m_Segments;

		public override bool CanRead => m_Position < m_Length;

		public override bool CanSeek => true;

		public override bool CanWrite => false;

		public override long Length => m_Length;

		public override long Position
		{
			get
			{
				return m_Position;
			}
			set
			{
				Seek(value, SeekOrigin.Begin);
			}
		}

		public void Initialize(IList<ArraySegment<byte>> segments)
		{
			if (segments.Count <= 0)
			{
				throw new ArgumentException("The length of segments must be greater than zero.");
			}
			m_Segments = segments;
			m_CurrentSegmentIndex = 0;
			m_CurrentSegmentOffset = segments[0].Offset;
			m_Position = 0L;
			long num = 0L;
			for (int i = 0; i < segments.Count; i++)
			{
				num += segments[i].Count;
			}
			m_Length = num;
		}

		public static BufferStream GetCurrent()
		{
			return GetCurrent<BufferStream>();
		}

		public static TStream GetCurrent<TStream>() where TStream : BufferStream, new()
		{
			LocalDataStoreSlot namedDataSlot = Thread.GetNamedDataSlot("ThreadBufferListStream");
			TStream val = Thread.GetData(namedDataSlot) as TStream;
			if (val != null)
			{
				return val;
			}
			val = new TStream();
			Thread.SetData(namedDataSlot, val);
			return val;
		}

		public Stream GetCurrentStream()
		{
			return this;
		}

		public void Reset()
		{
			m_Segments = null;
			m_CurrentSegmentIndex = 0;
			m_CurrentSegmentOffset = 0;
			m_Length = 0L;
			m_Position = 0L;
		}

		public void Clear()
		{
			m_Segments?.Clear();
		}

		public override void Flush()
		{
			throw new NotSupportedException();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = m_CurrentSegmentOffset;
			int num2 = 0;
			int currentSegmentIndex = 0;
			for (int i = m_CurrentSegmentIndex; i < m_Segments.Count; i++)
			{
				ArraySegment<byte> arraySegment = m_Segments[i];
				if (i != m_CurrentSegmentIndex)
				{
					num = arraySegment.Offset;
				}
				int num3 = count - num2;
				int num4 = arraySegment.Count - (num - arraySegment.Offset);
				int num5 = Math.Min(num4, num3);
				Buffer.BlockCopy(arraySegment.Array, num, buffer, offset + num2, num5);
				num2 += num5;
				currentSegmentIndex = i;
				if (num2 < count)
				{
					continue;
				}
				if (num4 > num3)
				{
					m_CurrentSegmentIndex = i;
					m_CurrentSegmentOffset = num + num5;
				}
				else
				{
					int num6 = i + 1;
					if (num6 >= m_Segments.Count)
					{
						m_CurrentSegmentIndex = i;
						m_CurrentSegmentOffset = num + num5;
					}
					else
					{
						m_CurrentSegmentIndex = num6;
						m_CurrentSegmentOffset = m_Segments[num6].Offset;
					}
				}
				m_Position += num2;
				return num2;
			}
			m_CurrentSegmentIndex = currentSegmentIndex;
			m_CurrentSegmentOffset = 0;
			m_Position = m_Length;
			return num2;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
			case SeekOrigin.End:
				throw new ArgumentException("Cannot support seek from the end.");
			case SeekOrigin.Begin:
				m_CurrentSegmentIndex = 0;
				m_CurrentSegmentOffset = m_Segments[0].Offset;
				m_Position = 0L;
				break;
			}
			if (offset == 0L)
			{
				return m_Position;
			}
			long num = ((origin == SeekOrigin.Begin) ? 0 : m_Position) + offset;
			long num2 = m_Position;
			for (int i = m_CurrentSegmentIndex; i < m_Segments.Count; i++)
			{
				ArraySegment<byte> arraySegment = m_Segments[i];
				if (i == m_CurrentSegmentIndex)
				{
					int currentSegmentOffset = m_CurrentSegmentOffset;
					num2 += arraySegment.Count - (m_CurrentSegmentOffset - arraySegment.Offset);
				}
				else
				{
					int offset2 = arraySegment.Offset;
					num2 += arraySegment.Count;
				}
				if (num2 >= num)
				{
					int num3 = (int)(num2 - num);
					m_CurrentSegmentIndex = i;
					m_CurrentSegmentOffset = arraySegment.Offset + arraySegment.Count - num3;
					m_Position = num;
					return num;
				}
			}
			throw new Exception("Exceed the stream's end");
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		private void CheckInitialized()
		{
			if (m_Segments == null)
			{
				throw new InvalidOperationException("Not initialized");
			}
		}

		public IBufferStream Skip(int count)
		{
			CheckInitialized();
			if (count == 0)
			{
				return this;
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Count cannot be less than zero.");
			}
			if (Length < count)
			{
				throw new ArgumentOutOfRangeException("count", "Cannot be greater than the length of all the buffers.");
			}
			Position += count;
			return this;
		}

		private IList<ArraySegment<byte>> Clone(int index, int segmentOffset, int length)
		{
			List<ArraySegment<byte>> list = new List<ArraySegment<byte>>();
			int num = length;
			IList<ArraySegment<byte>> segments = m_Segments;
			for (int i = index; i < segments.Count; i++)
			{
				ArraySegment<byte> arraySegment = segments[i];
				int offset = arraySegment.Offset;
				int val = arraySegment.Count;
				if (i == index)
				{
					offset = segmentOffset;
					val = arraySegment.Count - (segmentOffset - arraySegment.Offset);
				}
				val = Math.Min(val, num);
				list.Add(new ArraySegment<byte>(arraySegment.Array, offset, val));
				num -= val;
				if (num <= 0)
				{
					break;
				}
			}
			return list;
		}

		public IList<ArraySegment<byte>> Take(int length)
		{
			BufferList bufferList = m_Segments as BufferList;
			if (bufferList != null)
			{
				return bufferList.Clone(m_CurrentSegmentIndex, m_CurrentSegmentOffset, length);
			}
			return Clone(m_CurrentSegmentIndex, m_CurrentSegmentOffset, length);
		}

		public string ReadString(int length, Encoding encoding)
		{
			int num = length;
			char[] array = new char[encoding.GetMaxCharCount(length)];
			Decoder decoder = encoding.GetDecoder();
			int num2 = m_CurrentSegmentIndex;
			int num3 = m_CurrentSegmentOffset;
			int num4 = 0;
			while (num2 < m_Segments.Count)
			{
				ArraySegment<byte> arraySegment = m_Segments[num2];
				if (num2 != m_CurrentSegmentIndex)
				{
					num3 = arraySegment.Offset;
				}
				int bytesUsed;
				int charsUsed;
				bool completed;
				if (num3 + length < arraySegment.Offset + arraySegment.Count)
				{
					int byteCount = length;
					int charCount = array.Length - num4;
					decoder.Convert(arraySegment.Array, num3, byteCount, array, num4, charCount, flush: true, out bytesUsed, out charsUsed, out completed);
					num4 += charsUsed;
				}
				else
				{
					int num5 = arraySegment.Count - (num3 - arraySegment.Offset);
					bool flush = num5 == length;
					int charCount2 = array.Length - num4;
					decoder.Convert(arraySegment.Array, num3, num5, array, num4, charCount2, flush, out bytesUsed, out charsUsed, out completed);
					num4 += charsUsed;
				}
				length -= bytesUsed;
				num3 += bytesUsed;
				if (length == 0)
				{
					break;
				}
				if (num2 != m_Segments.Count - 1)
				{
					num2++;
				}
			}
			m_CurrentSegmentIndex = num2;
			m_CurrentSegmentOffset = num3;
			m_Position += num;
			return new string(array, 0, num4);
		}

		protected void FillBuffer(int length)
		{
			if (length > 8)
			{
				throw new ArgumentOutOfRangeException("length", "the length must between 1 and 8");
			}
			if (Read(m_Buffer, 0, length) != length)
			{
				throw new ArgumentOutOfRangeException("length", "there is no enough data to read");
			}
		}

		public short ReadInt16()
		{
			return ReadInt16(littleEndian: false);
		}

		public short ReadInt16(bool littleEndian)
		{
			FillBuffer(2);
			byte[] buffer = m_Buffer;
			if (!littleEndian)
			{
				return (short)BigEndianFromBytes(buffer, 2);
			}
			return (short)LittleEndianFromBytes(buffer, 2);
		}

		public ushort ReadUInt16()
		{
			return ReadUInt16(littleEndian: false);
		}

		public ushort ReadUInt16(bool littleEndian)
		{
			FillBuffer(2);
			byte[] buffer = m_Buffer;
			if (!littleEndian)
			{
				return (ushort)BigEndianFromBytes(buffer, 2);
			}
			return (ushort)LittleEndianFromBytes(buffer, 2);
		}

		public int ReadInt32()
		{
			return ReadInt32(littleEndian: false);
		}

		public int ReadInt32(bool littleEndian)
		{
			FillBuffer(4);
			byte[] buffer = m_Buffer;
			if (!littleEndian)
			{
				return (int)BigEndianFromBytes(buffer, 4);
			}
			return (int)LittleEndianFromBytes(buffer, 4);
		}

		public uint ReadUInt32()
		{
			return ReadUInt32(littleEndian: false);
		}

		public uint ReadUInt32(bool littleEndian)
		{
			FillBuffer(4);
			byte[] buffer = m_Buffer;
			if (!littleEndian)
			{
				return (uint)BigEndianFromBytes(buffer, 4);
			}
			return (uint)LittleEndianFromBytes(buffer, 4);
		}

		public long ReadInt64()
		{
			return ReadInt64(littleEndian: false);
		}

		public long ReadInt64(bool littleEndian)
		{
			FillBuffer(8);
			byte[] buffer = m_Buffer;
			if (!littleEndian)
			{
				return BigEndianFromBytes(buffer, 8);
			}
			return LittleEndianFromBytes(buffer, 8);
		}

		public ulong ReadUInt64()
		{
			return ReadUInt64(littleEndian: false);
		}

		public ulong ReadUInt64(bool littleEndian)
		{
			FillBuffer(8);
			byte[] buffer = m_Buffer;
			if (!littleEndian)
			{
				return (ulong)BigEndianFromBytes(buffer, 8);
			}
			return (ulong)LittleEndianFromBytes(buffer, 8);
		}

		private long BigEndianFromBytes(byte[] buffer, int bytesToConvert)
		{
			long num = 0L;
			for (int i = 0; i < bytesToConvert; i++)
			{
				num = ((num << 8) | buffer[i]);
			}
			return num;
		}

		private long LittleEndianFromBytes(byte[] buffer, int bytesToConvert)
		{
			long num = 0L;
			for (int i = 0; i < bytesToConvert; i++)
			{
				num = ((num << 8) | buffer[bytesToConvert - 1 - i]);
			}
			return num;
		}
	}
}
