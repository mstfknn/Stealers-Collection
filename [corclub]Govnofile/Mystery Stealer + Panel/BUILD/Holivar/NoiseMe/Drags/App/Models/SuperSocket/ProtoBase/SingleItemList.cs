using System;
using System.Collections;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000FE RID: 254
	internal class SingleItemList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x060007A4 RID: 1956 RVA: 0x000065DA File Offset: 0x000047DA
		public SingleItemList(T item)
		{
			this.m_Item = item;
		}

		// Token: 0x17000201 RID: 513
		public T this[int index]
		{
			get
			{
				if (index != 0)
				{
					throw new IndexOutOfRangeException();
				}
				return this.m_Item;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00003147 File Offset: 0x00001347
		public int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00003147 File Offset: 0x00001347
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0000250E File Offset: 0x0000070E
		public void Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0000250E File Offset: 0x0000070E
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0000250E File Offset: 0x0000070E
		public bool Contains(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0000250E File Offset: 0x0000070E
		public void CopyTo(T[] array, int arrayIndex)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x000065FA File Offset: 0x000047FA
		public IEnumerator<T> GetEnumerator()
		{
			yield return this.m_Item;
			yield break;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0000250E File Offset: 0x0000070E
		public int IndexOf(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0000250E File Offset: 0x0000070E
		public void Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0000250E File Offset: 0x0000070E
		public bool Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0000250E File Offset: 0x0000070E
		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00006609 File Offset: 0x00004809
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400031A RID: 794
		private T m_Item;
	}
}
