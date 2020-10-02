using NoiseMe.Drags.App.Models.SuperSocket.ClientEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Common
{
	public class ArraySegmentList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable where T : IEquatable<T>
	{
		private IList<ArraySegmentEx<T>> m_Segments;

		private ArraySegmentEx<T> m_PrevSegment;

		private int m_PrevSegmentIndex;

		private int m_Count;

		public IList<ArraySegmentEx<T>> Segments => m_Segments;

		public T this[int index]
		{
			get
			{
				ArraySegmentEx<T> segment;
				int elementpublicIndex = GetElementpublicIndex(index, out segment);
				if (elementpublicIndex < 0)
				{
					throw new IndexOutOfRangeException();
				}
				return segment.Array[elementpublicIndex];
			}
			set
			{
				ArraySegmentEx<T> segment;
				int elementpublicIndex = GetElementpublicIndex(index, out segment);
				if (elementpublicIndex < 0)
				{
					throw new IndexOutOfRangeException();
				}
				segment.Array[elementpublicIndex] = value;
			}
		}

		public int Count => m_Count;

		public bool IsReadOnly => true;

		public int SegmentCount => m_Segments.Count;

		public ArraySegmentList()
		{
			m_Segments = new List<ArraySegmentEx<T>>();
		}

		private void CalculateSegmentsInfo(IList<ArraySegmentEx<T>> segments)
		{
			int num = 0;
			foreach (ArraySegmentEx<T> segment in segments)
			{
				if (segment.Count > 0)
				{
					segment.From = num;
					segment.To = num + segment.Count - 1;
					m_Segments.Add(segment);
					num += segment.Count;
				}
			}
			m_Count = num;
		}

		public int IndexOf(T item)
		{
			int num = 0;
			for (int i = 0; i < m_Segments.Count; i++)
			{
				ArraySegmentEx<T> arraySegmentEx = m_Segments[i];
				int offset = arraySegmentEx.Offset;
				for (int j = 0; j < arraySegmentEx.Count; j++)
				{
					if (arraySegmentEx.Array[j + offset].Equals(item))
					{
						return num;
					}
					num++;
				}
			}
			return -1;
		}

		public void Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		private int GetElementpublicIndex(int index, out ArraySegmentEx<T> segment)
		{
			segment = null;
			if (index < 0 || index > Count - 1)
			{
				return -1;
			}
			if (index == 0)
			{
				m_PrevSegment = m_Segments[0];
				m_PrevSegmentIndex = 0;
				segment = m_PrevSegment;
				return m_PrevSegment.Offset;
			}
			int num = 0;
			if (m_PrevSegment != null)
			{
				if (index >= m_PrevSegment.From)
				{
					if (index <= m_PrevSegment.To)
					{
						segment = m_PrevSegment;
						return m_PrevSegment.Offset + index - m_PrevSegment.From;
					}
					num = 1;
				}
				else
				{
					num = -1;
				}
			}
			int num2;
			int to;
			if (num != 0)
			{
				num2 = m_PrevSegmentIndex + num;
				ArraySegmentEx<T> arraySegmentEx = m_Segments[num2];
				if (index >= arraySegmentEx.From && index <= arraySegmentEx.To)
				{
					segment = arraySegmentEx;
					return arraySegmentEx.Offset + index - arraySegmentEx.From;
				}
				num2 += num;
				ArraySegmentEx<T> arraySegmentEx2 = m_Segments[num2];
				if (index >= arraySegmentEx2.From && index <= arraySegmentEx2.To)
				{
					m_PrevSegment = arraySegmentEx2;
					m_PrevSegmentIndex = num2;
					segment = arraySegmentEx2;
					return arraySegmentEx2.Offset + index - arraySegmentEx2.From;
				}
				if (num > 0)
				{
					num2++;
					to = m_Segments.Count - 1;
				}
				else
				{
					int num3 = num2 - 1;
					num2 = 0;
					to = num3;
				}
			}
			else
			{
				num2 = 0;
				to = m_Segments.Count - 1;
			}
			int segmentIndex = -1;
			ArraySegmentEx<T> arraySegmentEx3 = QuickSearchSegment(num2, to, index, out segmentIndex);
			if (arraySegmentEx3 != null)
			{
				m_PrevSegment = arraySegmentEx3;
				m_PrevSegmentIndex = segmentIndex;
				segment = m_PrevSegment;
				return arraySegmentEx3.Offset + index - arraySegmentEx3.From;
			}
			m_PrevSegment = null;
			return -1;
		}

		public ArraySegmentEx<T> QuickSearchSegment(int from, int to, int index, out int segmentIndex)
		{
			segmentIndex = -1;
			int num = to - from;
			switch (num)
			{
			case 0:
			{
				ArraySegmentEx<T> arraySegmentEx = m_Segments[from];
				if (index >= arraySegmentEx.From && index <= arraySegmentEx.To)
				{
					segmentIndex = from;
					return arraySegmentEx;
				}
				return null;
			}
			case 1:
			{
				ArraySegmentEx<T> arraySegmentEx = m_Segments[from];
				if (index >= arraySegmentEx.From && index <= arraySegmentEx.To)
				{
					segmentIndex = from;
					return arraySegmentEx;
				}
				arraySegmentEx = m_Segments[to];
				if (index >= arraySegmentEx.From && index <= arraySegmentEx.To)
				{
					segmentIndex = to;
					return arraySegmentEx;
				}
				return null;
			}
			default:
			{
				int num2 = from + num / 2;
				ArraySegmentEx<T> arraySegmentEx = m_Segments[num2];
				if (index >= arraySegmentEx.From)
				{
					if (index <= arraySegmentEx.To)
					{
						segmentIndex = num2;
						return arraySegmentEx;
					}
					return QuickSearchSegment(num2 + 1, to, index, out segmentIndex);
				}
				return QuickSearchSegment(from, num2 - 1, index, out segmentIndex);
			}
			}
		}

		public void Add(T item)
		{
			throw new NotSupportedException();
		}

		public void Clear()
		{
			throw new NotSupportedException();
		}

		public bool Contains(T item)
		{
			throw new NotSupportedException();
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			CopyTo(array, 0, arrayIndex, Math.Min(array.Length, Count - arrayIndex));
		}

		public bool Remove(T item)
		{
			throw new NotSupportedException();
		}

		public IEnumerator<T> GetEnumerator()
		{
			throw new NotSupportedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException();
		}

		public void RemoveSegmentAt(int index)
		{
			ArraySegmentEx<T> arraySegmentEx = m_Segments[index];
			int num = arraySegmentEx.To - arraySegmentEx.From + 1;
			m_Segments.RemoveAt(index);
			m_PrevSegment = null;
			if (index != m_Segments.Count)
			{
				for (int i = index; i < m_Segments.Count; i++)
				{
					m_Segments[i].From -= num;
					m_Segments[i].To -= num;
				}
			}
			m_Count -= num;
		}

		public void AddSegment(T[] array, int offset, int length)
		{
			AddSegment(array, offset, length, toBeCopied: false);
		}

		public void AddSegment(T[] array, int offset, int length, bool toBeCopied)
		{
			if (length > 0)
			{
				int count = m_Count;
				ArraySegmentEx<T> arraySegmentEx = null;
				arraySegmentEx = (toBeCopied ? new ArraySegmentEx<T>(array.CloneRange(offset, length), 0, length) : new ArraySegmentEx<T>(array, offset, length));
				arraySegmentEx.From = count;
				m_Count = count + arraySegmentEx.Count;
				arraySegmentEx.To = m_Count - 1;
				m_Segments.Add(arraySegmentEx);
			}
		}

		public void ClearSegements()
		{
			m_Segments.Clear();
			m_PrevSegment = null;
			m_Count = 0;
		}

		public T[] ToArrayData()
		{
			return ToArrayData(0, m_Count);
		}

		public T[] ToArrayData(int startIndex, int length)
		{
			T[] array = new T[length];
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int segmentIndex = 0;
			if (startIndex != 0)
			{
				ArraySegmentEx<T> arraySegmentEx = QuickSearchSegment(0, m_Segments.Count - 1, startIndex, out segmentIndex);
				num = startIndex - arraySegmentEx.From;
				if (arraySegmentEx == null)
				{
					throw new IndexOutOfRangeException();
				}
			}
			for (int i = segmentIndex; i < m_Segments.Count; i++)
			{
				ArraySegmentEx<T> arraySegmentEx2 = m_Segments[i];
				num2 = Math.Min(arraySegmentEx2.Count - num, length - num3);
				Array.Copy(arraySegmentEx2.Array, arraySegmentEx2.Offset + num, array, num3, num2);
				num3 += num2;
				if (num3 >= length)
				{
					break;
				}
				num = 0;
			}
			return array;
		}

		public void TrimEnd(int trimSize)
		{
			if (trimSize <= 0)
			{
				return;
			}
			int num = Count - trimSize - 1;
			int num2 = m_Segments.Count - 1;
			ArraySegmentEx<T> arraySegmentEx;
			while (true)
			{
				if (num2 >= 0)
				{
					arraySegmentEx = m_Segments[num2];
					if (arraySegmentEx.From <= num && num < arraySegmentEx.To)
					{
						break;
					}
					RemoveSegmentAt(num2);
					num2--;
					continue;
				}
				return;
			}
			arraySegmentEx.To = num;
			m_Count -= trimSize;
		}

		public int SearchLastSegment(SearchMarkState<T> state)
		{
			if (m_Segments.Count <= 0)
			{
				return -1;
			}
			ArraySegmentEx<T> arraySegmentEx = m_Segments[m_Segments.Count - 1];
			if (arraySegmentEx == null)
			{
				return -1;
			}
			int? num = arraySegmentEx.Array.SearchMark(arraySegmentEx.Offset, arraySegmentEx.Count, state.Mark);
			if (!num.HasValue)
			{
				return -1;
			}
			if (num.Value > 0)
			{
				state.Matched = 0;
				return num.Value - arraySegmentEx.Offset + arraySegmentEx.From;
			}
			state.Matched = -num.Value;
			return -1;
		}

		public int CopyTo(T[] to)
		{
			return CopyTo(to, 0, 0, Math.Min(m_Count, to.Length));
		}

		public int CopyTo(T[] to, int srcIndex, int toIndex, int length)
		{
			int num = 0;
			int num2 = 0;
			ArraySegmentEx<T> arraySegmentEx;
			int segmentIndex;
			if (srcIndex > 0)
			{
				arraySegmentEx = QuickSearchSegment(0, m_Segments.Count - 1, srcIndex, out segmentIndex);
			}
			else
			{
				arraySegmentEx = m_Segments[0];
				segmentIndex = 0;
			}
			int num3 = srcIndex - arraySegmentEx.From + arraySegmentEx.Offset;
			num2 = Math.Min(arraySegmentEx.Count - num3 + arraySegmentEx.Offset, length - num);
			Array.Copy(arraySegmentEx.Array, num3, to, num + toIndex, num2);
			num += num2;
			if (num >= length)
			{
				return num;
			}
			for (int i = segmentIndex + 1; i < m_Segments.Count; i++)
			{
				ArraySegmentEx<T> arraySegmentEx2 = m_Segments[i];
				num2 = Math.Min(arraySegmentEx2.Count, length - num);
				Array.Copy(arraySegmentEx2.Array, arraySegmentEx2.Offset, to, num + toIndex, num2);
				num += num2;
				if (num >= length)
				{
					break;
				}
			}
			return num;
		}
	}
	public class ArraySegmentList : ArraySegmentList<byte>
	{
		public string Decode(Encoding encoding)
		{
			return Decode(encoding, 0, base.Count);
		}

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
			int segmentIndex = 0;
			if (offset > 0)
			{
				QuickSearchSegment(0, segments.Count - 1, offset, out segmentIndex);
			}
			for (int i = segmentIndex; i < segments.Count; i++)
			{
				ArraySegmentEx<byte> arraySegmentEx = segments[i];
				if (i == num3)
				{
					flush = true;
				}
				int byteIndex = arraySegmentEx.Offset;
				int num4 = Math.Min(length - num, arraySegmentEx.Count);
				if (i == segmentIndex && offset > 0)
				{
					byteIndex = offset - arraySegmentEx.From + arraySegmentEx.Offset;
					num4 = Math.Min(arraySegmentEx.Count - offset + arraySegmentEx.From, num4);
				}
				decoder.Convert(arraySegmentEx.Array, byteIndex, num4, array, num2, array.Length - num2, flush, out int bytesUsed, out int charsUsed, out bool _);
				num2 += charsUsed;
				num += bytesUsed;
				if (num >= length)
				{
					break;
				}
			}
			return new string(array, 0, num2);
		}

		public void DecodeMask(byte[] mask, int offset, int length)
		{
			int num = mask.Length;
			int segmentIndex = 0;
			ArraySegmentEx<byte> arraySegmentEx = QuickSearchSegment(0, base.Segments.Count - 1, offset, out segmentIndex);
			int num2 = Math.Min(length, arraySegmentEx.Count - offset + arraySegmentEx.From);
			int num3 = offset - arraySegmentEx.From + arraySegmentEx.Offset;
			int num4 = 0;
			for (int i = num3; i < num3 + num2; i++)
			{
				arraySegmentEx.Array[i] = (byte)(arraySegmentEx.Array[i] ^ mask[num4++ % num]);
			}
			if (num4 >= length)
			{
				return;
			}
			for (int j = segmentIndex + 1; j < base.SegmentCount; j++)
			{
				ArraySegmentEx<byte> arraySegmentEx2 = base.Segments[j];
				num2 = Math.Min(length - num4, arraySegmentEx2.Count);
				for (int k = arraySegmentEx2.Offset; k < arraySegmentEx2.Offset + num2; k++)
				{
					arraySegmentEx2.Array[k] = (byte)(arraySegmentEx2.Array[k] ^ mask[num4++ % num]);
				}
				if (num4 >= length)
				{
					break;
				}
			}
		}
	}
}
