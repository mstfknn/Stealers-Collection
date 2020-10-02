using System;
using System.Collections;
using System.Collections.Generic;
using NoiseMe.Drags.App.Models.SuperSocket.ClientEngine;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Common
{
	// Token: 0x020000B8 RID: 184
	public class ArraySegmentList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable where T : IEquatable<T>
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00005D16 File Offset: 0x00003F16
		public IList<ArraySegmentEx<T>> Segments
		{
			get
			{
				return this.m_Segments;
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00005D1E File Offset: 0x00003F1E
		public ArraySegmentList()
		{
			this.m_Segments = new List<ArraySegmentEx<T>>();
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001A034 File Offset: 0x00018234
		private void CalculateSegmentsInfo(IList<ArraySegmentEx<T>> segments)
		{
			int num = 0;
			foreach (ArraySegmentEx<T> arraySegmentEx in segments)
			{
				if (arraySegmentEx.Count > 0)
				{
					arraySegmentEx.From = num;
					arraySegmentEx.To = num + arraySegmentEx.Count - 1;
					this.m_Segments.Add(arraySegmentEx);
					num += arraySegmentEx.Count;
				}
			}
			this.m_Count = num;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001A0B4 File Offset: 0x000182B4
		public int IndexOf(T item)
		{
			int num = 0;
			for (int i = 0; i < this.m_Segments.Count; i++)
			{
				ArraySegmentEx<T> arraySegmentEx = this.m_Segments[i];
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

		// Token: 0x0600064E RID: 1614 RVA: 0x0000250E File Offset: 0x0000070E
		public void Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0000250E File Offset: 0x0000070E
		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170001B9 RID: 441
		public T this[int index]
		{
			get
			{
				ArraySegmentEx<T> arraySegmentEx;
				int elementpublicIndex = this.GetElementpublicIndex(index, out arraySegmentEx);
				if (elementpublicIndex < 0)
				{
					throw new IndexOutOfRangeException();
				}
				return arraySegmentEx.Array[elementpublicIndex];
			}
			set
			{
				ArraySegmentEx<T> arraySegmentEx;
				int elementpublicIndex = this.GetElementpublicIndex(index, out arraySegmentEx);
				if (elementpublicIndex < 0)
				{
					throw new IndexOutOfRangeException();
				}
				arraySegmentEx.Array[elementpublicIndex] = value;
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001A188 File Offset: 0x00018388
		private int GetElementpublicIndex(int index, out ArraySegmentEx<T> segment)
		{
			segment = null;
			if (index < 0 || index > this.Count - 1)
			{
				return -1;
			}
			if (index == 0)
			{
				this.m_PrevSegment = this.m_Segments[0];
				this.m_PrevSegmentIndex = 0;
				segment = this.m_PrevSegment;
				return this.m_PrevSegment.Offset;
			}
			int num = 0;
			if (this.m_PrevSegment != null)
			{
				if (index >= this.m_PrevSegment.From)
				{
					if (index <= this.m_PrevSegment.To)
					{
						segment = this.m_PrevSegment;
						return this.m_PrevSegment.Offset + index - this.m_PrevSegment.From;
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
				num2 = this.m_PrevSegmentIndex + num;
				ArraySegmentEx<T> arraySegmentEx = this.m_Segments[num2];
				if (index >= arraySegmentEx.From && index <= arraySegmentEx.To)
				{
					segment = arraySegmentEx;
					return arraySegmentEx.Offset + index - arraySegmentEx.From;
				}
				num2 += num;
				ArraySegmentEx<T> arraySegmentEx2 = this.m_Segments[num2];
				if (index >= arraySegmentEx2.From && index <= arraySegmentEx2.To)
				{
					this.m_PrevSegment = arraySegmentEx2;
					this.m_PrevSegmentIndex = num2;
					segment = arraySegmentEx2;
					return arraySegmentEx2.Offset + index - arraySegmentEx2.From;
				}
				if (num > 0)
				{
					num2++;
					to = this.m_Segments.Count - 1;
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
				to = this.m_Segments.Count - 1;
			}
			int prevSegmentIndex = -1;
			ArraySegmentEx<T> arraySegmentEx3 = this.QuickSearchSegment(num2, to, index, out prevSegmentIndex);
			if (arraySegmentEx3 != null)
			{
				this.m_PrevSegment = arraySegmentEx3;
				this.m_PrevSegmentIndex = prevSegmentIndex;
				segment = this.m_PrevSegment;
				return arraySegmentEx3.Offset + index - arraySegmentEx3.From;
			}
			this.m_PrevSegment = null;
			return -1;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001A330 File Offset: 0x00018530
		public ArraySegmentEx<T> QuickSearchSegment(int from, int to, int index, out int segmentIndex)
		{
			segmentIndex = -1;
			int num = to - from;
			if (num == 0)
			{
				ArraySegmentEx<T> arraySegmentEx = this.m_Segments[from];
				if (index >= arraySegmentEx.From && index <= arraySegmentEx.To)
				{
					segmentIndex = from;
					return arraySegmentEx;
				}
				return null;
			}
			else if (num == 1)
			{
				ArraySegmentEx<T> arraySegmentEx = this.m_Segments[from];
				if (index >= arraySegmentEx.From && index <= arraySegmentEx.To)
				{
					segmentIndex = from;
					return arraySegmentEx;
				}
				arraySegmentEx = this.m_Segments[to];
				if (index >= arraySegmentEx.From && index <= arraySegmentEx.To)
				{
					segmentIndex = to;
					return arraySegmentEx;
				}
				return null;
			}
			else
			{
				int num2 = from + num / 2;
				ArraySegmentEx<T> arraySegmentEx = this.m_Segments[num2];
				if (index < arraySegmentEx.From)
				{
					return this.QuickSearchSegment(from, num2 - 1, index, out segmentIndex);
				}
				if (index <= arraySegmentEx.To)
				{
					segmentIndex = num2;
					return arraySegmentEx;
				}
				return this.QuickSearchSegment(num2 + 1, to, index, out segmentIndex);
			}
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0000250E File Offset: 0x0000070E
		public void Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0000250E File Offset: 0x0000070E
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0000250E File Offset: 0x0000070E
		public bool Contains(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00005D31 File Offset: 0x00003F31
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.CopyTo(array, 0, arrayIndex, Math.Min(array.Length, this.Count - arrayIndex));
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x00005D4D File Offset: 0x00003F4D
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00003147 File Offset: 0x00001347
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0000250E File Offset: 0x0000070E
		public bool Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0000250E File Offset: 0x0000070E
		public IEnumerator<T> GetEnumerator()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0000250E File Offset: 0x0000070E
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001A408 File Offset: 0x00018608
		public void RemoveSegmentAt(int index)
		{
			ArraySegmentEx<T> arraySegmentEx = this.m_Segments[index];
			int num = arraySegmentEx.To - arraySegmentEx.From + 1;
			this.m_Segments.RemoveAt(index);
			this.m_PrevSegment = null;
			if (index != this.m_Segments.Count)
			{
				for (int i = index; i < this.m_Segments.Count; i++)
				{
					this.m_Segments[i].From -= num;
					this.m_Segments[i].To -= num;
				}
			}
			this.m_Count -= num;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00005D55 File Offset: 0x00003F55
		public void AddSegment(T[] array, int offset, int length)
		{
			this.AddSegment(array, offset, length, false);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001A4AC File Offset: 0x000186AC
		public void AddSegment(T[] array, int offset, int length, bool toBeCopied)
		{
			if (length <= 0)
			{
				return;
			}
			int count = this.m_Count;
			ArraySegmentEx<T> arraySegmentEx;
			if (!toBeCopied)
			{
				arraySegmentEx = new ArraySegmentEx<T>(array, offset, length);
			}
			else
			{
				arraySegmentEx = new ArraySegmentEx<T>(array.CloneRange(offset, length), 0, length);
			}
			arraySegmentEx.From = count;
			this.m_Count = count + arraySegmentEx.Count;
			arraySegmentEx.To = this.m_Count - 1;
			this.m_Segments.Add(arraySegmentEx);
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00005D61 File Offset: 0x00003F61
		public int SegmentCount
		{
			get
			{
				return this.m_Segments.Count;
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00005D6E File Offset: 0x00003F6E
		public void ClearSegements()
		{
			this.m_Segments.Clear();
			this.m_PrevSegment = null;
			this.m_Count = 0;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00005D89 File Offset: 0x00003F89
		public T[] ToArrayData()
		{
			return this.ToArrayData(0, this.m_Count);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001A518 File Offset: 0x00018718
		public T[] ToArrayData(int startIndex, int length)
		{
			T[] array = new T[length];
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (startIndex != 0)
			{
				ArraySegmentEx<T> arraySegmentEx = this.QuickSearchSegment(0, this.m_Segments.Count - 1, startIndex, out num3);
				num = startIndex - arraySegmentEx.From;
				if (arraySegmentEx == null)
				{
					throw new IndexOutOfRangeException();
				}
			}
			for (int i = num3; i < this.m_Segments.Count; i++)
			{
				ArraySegmentEx<T> arraySegmentEx2 = this.m_Segments[i];
				int num4 = Math.Min(arraySegmentEx2.Count - num, length - num2);
				Array.Copy(arraySegmentEx2.Array, arraySegmentEx2.Offset + num, array, num2, num4);
				num2 += num4;
				if (num2 >= length)
				{
					break;
				}
				num = 0;
			}
			return array;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001A5C4 File Offset: 0x000187C4
		public void TrimEnd(int trimSize)
		{
			if (trimSize <= 0)
			{
				return;
			}
			int num = this.Count - trimSize - 1;
			for (int i = this.m_Segments.Count - 1; i >= 0; i--)
			{
				ArraySegmentEx<T> arraySegmentEx = this.m_Segments[i];
				if (arraySegmentEx.From <= num && num < arraySegmentEx.To)
				{
					arraySegmentEx.To = num;
					this.m_Count -= trimSize;
					return;
				}
				this.RemoveSegmentAt(i);
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001A638 File Offset: 0x00018838
		public int SearchLastSegment(SearchMarkState<T> state)
		{
			if (this.m_Segments.Count <= 0)
			{
				return -1;
			}
			ArraySegmentEx<T> arraySegmentEx = this.m_Segments[this.m_Segments.Count - 1];
			if (arraySegmentEx == null)
			{
				return -1;
			}
			int? num = arraySegmentEx.Array.SearchMark(arraySegmentEx.Offset, arraySegmentEx.Count, state.Mark);
			if (num == null)
			{
				return -1;
			}
			if (num.Value > 0)
			{
				state.Matched = 0;
				return num.Value - arraySegmentEx.Offset + arraySegmentEx.From;
			}
			state.Matched = 0 - num.Value;
			return -1;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00005D98 File Offset: 0x00003F98
		public int CopyTo(T[] to)
		{
			return this.CopyTo(to, 0, 0, Math.Min(this.m_Count, to.Length));
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001A6D4 File Offset: 0x000188D4
		public int CopyTo(T[] to, int srcIndex, int toIndex, int length)
		{
			int num = 0;
			int num2;
			ArraySegmentEx<T> arraySegmentEx;
			if (srcIndex > 0)
			{
				arraySegmentEx = this.QuickSearchSegment(0, this.m_Segments.Count - 1, srcIndex, out num2);
			}
			else
			{
				arraySegmentEx = this.m_Segments[0];
				num2 = 0;
			}
			int num3 = srcIndex - arraySegmentEx.From + arraySegmentEx.Offset;
			int num4 = Math.Min(arraySegmentEx.Count - num3 + arraySegmentEx.Offset, length - num);
			Array.Copy(arraySegmentEx.Array, num3, to, num + toIndex, num4);
			num += num4;
			if (num >= length)
			{
				return num;
			}
			for (int i = num2 + 1; i < this.m_Segments.Count; i++)
			{
				ArraySegmentEx<T> arraySegmentEx2 = this.m_Segments[i];
				num4 = Math.Min(arraySegmentEx2.Count, length - num);
				Array.Copy(arraySegmentEx2.Array, arraySegmentEx2.Offset, to, num + toIndex, num4);
				num += num4;
				if (num >= length)
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x040002BA RID: 698
		private IList<ArraySegmentEx<T>> m_Segments;

		// Token: 0x040002BB RID: 699
		private ArraySegmentEx<T> m_PrevSegment;

		// Token: 0x040002BC RID: 700
		private int m_PrevSegmentIndex;

		// Token: 0x040002BD RID: 701
		private int m_Count;
	}
}
