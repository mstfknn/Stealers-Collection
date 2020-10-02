using System;
using System.Collections;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public sealed class BufferList : IList<ArraySegment<byte>>, ICollection<ArraySegment<byte>>, IEnumerable<ArraySegment<byte>>, IEnumerable
	{
		private List<ArraySegment<byte>> m_List = new List<ArraySegment<byte>>();

		private int m_Total;

		public ArraySegment<byte> Last
		{
			get
			{
				int count = Count;
				if (count == 0)
				{
					return default(ArraySegment<byte>);
				}
				return m_List[count - 1];
			}
		}

		public ArraySegment<byte> this[int index]
		{
			get
			{
				return m_List[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public int Total => m_Total;

		public int Count => m_List.Count;

		public bool IsReadOnly => true;

		public int IndexOf(ArraySegment<byte> item)
		{
			throw new NotSupportedException();
		}

		public void Insert(int index, ArraySegment<byte> item)
		{
			throw new NotSupportedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		public void Add(ArraySegment<byte> item)
		{
			m_List.Add(item);
			m_Total += item.Count;
		}

		public void SetLastItemLength(int length)
		{
			int index = m_List.Count - 1;
			ArraySegment<byte> arraySegment = m_List[index];
			m_List[index] = new ArraySegment<byte>(arraySegment.Array, arraySegment.Offset, length);
			m_Total += length - arraySegment.Count;
		}

		public void Clear()
		{
			m_List.Clear();
			m_Total = 0;
		}

		public bool Contains(ArraySegment<byte> item)
		{
			throw new NotSupportedException();
		}

		public void CopyTo(ArraySegment<byte>[] array, int arrayIndex)
		{
			throw new NotSupportedException();
		}

		public bool Remove(ArraySegment<byte> item)
		{
			throw new NotSupportedException();
		}

		public IEnumerator<ArraySegment<byte>> GetEnumerator()
		{
			int length = m_List.Count;
			for (int i = 0; i < length; i++)
			{
				yield return m_List[i];
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IList<ArraySegment<byte>> GetAllCachedItems()
		{
			return m_List;
		}

		public BufferList Clone(int index, int segmentOffset, int length)
		{
			BufferList bufferList = new BufferList();
			int num = length;
			List<ArraySegment<byte>> list = m_List;
			for (int i = index; i < list.Count; i++)
			{
				ArraySegment<byte> arraySegment = list[i];
				int offset = arraySegment.Offset;
				int val = arraySegment.Count;
				if (i == index)
				{
					offset = segmentOffset;
					val = arraySegment.Count - (segmentOffset - arraySegment.Offset);
				}
				val = Math.Min(val, num);
				bufferList.Add(new ArraySegment<byte>(arraySegment.Array, offset, val));
				num -= val;
				if (num <= 0)
				{
					break;
				}
			}
			return bufferList;
		}
	}
}
