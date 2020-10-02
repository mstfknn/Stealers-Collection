using System;
using System.Collections;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000CF RID: 207
	public sealed class BufferList : IList<ArraySegment<byte>>, ICollection<ArraySegment<byte>>, IEnumerable<ArraySegment<byte>>, IEnumerable
	{
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0001ACA8 File Offset: 0x00018EA8
		public ArraySegment<byte> Last
		{
			get
			{
				int count = this.Count;
				if (count == 0)
				{
					return default(ArraySegment<byte>);
				}
				return this.m_List[count - 1];
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0000250E File Offset: 0x0000070E
		public int IndexOf(ArraySegment<byte> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0000250E File Offset: 0x0000070E
		public void Insert(int index, ArraySegment<byte> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0000250E File Offset: 0x0000070E
		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001D1 RID: 465
		public ArraySegment<byte> this[int index]
		{
			get
			{
				return this.m_List[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00005F84 File Offset: 0x00004184
		public void Add(ArraySegment<byte> item)
		{
			this.m_List.Add(item);
			this.m_Total += item.Count;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001ACD8 File Offset: 0x00018ED8
		public void SetLastItemLength(int length)
		{
			int index = this.m_List.Count - 1;
			ArraySegment<byte> arraySegment = this.m_List[index];
			this.m_List[index] = new ArraySegment<byte>(arraySegment.Array, arraySegment.Offset, length);
			this.m_Total += length - arraySegment.Count;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00005FA6 File Offset: 0x000041A6
		public void Clear()
		{
			this.m_List.Clear();
			this.m_Total = 0;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0000250E File Offset: 0x0000070E
		public bool Contains(ArraySegment<byte> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0000250E File Offset: 0x0000070E
		public void CopyTo(ArraySegment<byte>[] array, int arrayIndex)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x00005FBA File Offset: 0x000041BA
		public int Total
		{
			get
			{
				return this.m_Total;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00005FC2 File Offset: 0x000041C2
		public int Count
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00003147 File Offset: 0x00001347
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0000250E File Offset: 0x0000070E
		public bool Remove(ArraySegment<byte> item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00005FCF File Offset: 0x000041CF
		public IEnumerator<ArraySegment<byte>> GetEnumerator()
		{
			int length = this.m_List.Count;
			int num;
			for (int i = 0; i < length; i = num + 1)
			{
				yield return this.m_List[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00005FDE File Offset: 0x000041DE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00005FE6 File Offset: 0x000041E6
		public IList<ArraySegment<byte>> GetAllCachedItems()
		{
			return this.m_List;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001AD38 File Offset: 0x00018F38
		public BufferList Clone(int index, int segmentOffset, int length)
		{
			BufferList bufferList = new BufferList();
			int num = length;
			List<ArraySegment<byte>> list = this.m_List;
			for (int i = index; i < list.Count; i++)
			{
				ArraySegment<byte> arraySegment = list[i];
				int offset = arraySegment.Offset;
				int num2 = arraySegment.Count;
				if (i == index)
				{
					offset = segmentOffset;
					num2 = arraySegment.Count - (segmentOffset - arraySegment.Offset);
				}
				num2 = Math.Min(num2, num);
				bufferList.Add(new ArraySegment<byte>(arraySegment.Array, offset, num2));
				num -= num2;
				if (num <= 0)
				{
					break;
				}
			}
			return bufferList;
		}

		// Token: 0x040002CB RID: 715
		private List<ArraySegment<byte>> m_List = new List<ArraySegment<byte>>();

		// Token: 0x040002CC RID: 716
		private int m_Total;
	}
}
