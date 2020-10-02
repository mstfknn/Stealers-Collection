using System;
using System.Collections.Generic;
using System.Threading;
using NoiseMe.Drags.App.Models.Delegates;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x02000103 RID: 259
	public class ConcurrentBatchQueue<T> : IBatchQueue<T>
	{
		// Token: 0x060007DA RID: 2010 RVA: 0x0000670D File Offset: 0x0000490D
		public ConcurrentBatchQueue() : this(16)
		{
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00006717 File Offset: 0x00004917
		public ConcurrentBatchQueue(int capacity) : this(new T[capacity])
		{
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00006725 File Offset: 0x00004925
		public ConcurrentBatchQueue(int capacity, Func<T, bool> nullValidator) : this(new T[capacity], nullValidator)
		{
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00006734 File Offset: 0x00004934
		public ConcurrentBatchQueue(T[] array) : this(array, (T t) => t == null)
		{
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001CA3C File Offset: 0x0001AC3C
		public ConcurrentBatchQueue(T[] array, Func<T, bool> nullValidator)
		{
			this.m_Entity = new ConcurrentBatchQueue<T>.Entity
			{
				Array = array
			};
			this.m_BackEntity = new ConcurrentBatchQueue<T>.Entity();
			this.m_BackEntity.Array = new T[array.Length];
			this.m_NullValidator = nullValidator;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001CA88 File Offset: 0x0001AC88
		public bool Enqueue(T item)
		{
			bool flag;
			while (!this.TryEnqueue(item, out flag) && !flag)
			{
			}
			return !flag;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001CAA8 File Offset: 0x0001ACA8
		private bool TryEnqueue(T item, out bool full)
		{
			full = false;
			this.EnsureNotRebuild();
			ConcurrentBatchQueue<T>.Entity entity = this.m_Entity as ConcurrentBatchQueue<T>.Entity;
			T[] array = entity.Array;
			int count = entity.Count;
			if (count >= array.Length)
			{
				full = true;
				return false;
			}
			if (entity != this.m_Entity)
			{
				return false;
			}
			if (Interlocked.CompareExchange(ref entity.Count, count + 1, count) != count)
			{
				return false;
			}
			array[count] = item;
			return true;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001CB0C File Offset: 0x0001AD0C
		public bool Enqueue(IList<T> items)
		{
			bool flag;
			while (!this.TryEnqueue(items, out flag) && !flag)
			{
			}
			return !flag;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001CB2C File Offset: 0x0001AD2C
		private bool TryEnqueue(IList<T> items, out bool full)
		{
			full = false;
			ConcurrentBatchQueue<T>.Entity entity = this.m_Entity as ConcurrentBatchQueue<T>.Entity;
			T[] array = entity.Array;
			int count = entity.Count;
			int count2 = items.Count;
			int num = count + count2;
			if (num > array.Length)
			{
				full = true;
				return false;
			}
			if (entity != this.m_Entity)
			{
				return false;
			}
			if (Interlocked.CompareExchange(ref entity.Count, num, count) != count)
			{
				return false;
			}
			foreach (T t in items)
			{
				array[count++] = t;
			}
			return true;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0000675C File Offset: 0x0000495C
		private void EnsureNotRebuild()
		{
			if (!this.m_Rebuilding)
			{
				return;
			}
			do
			{
				Thread.SpinWait(1);
			}
			while (this.m_Rebuilding);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001CBD4 File Offset: 0x0001ADD4
		public bool TryDequeue(IList<T> outputItems)
		{
			ConcurrentBatchQueue<T>.Entity entity = this.m_Entity as ConcurrentBatchQueue<T>.Entity;
			if (entity.Count <= 0)
			{
				return false;
			}
			if (Interlocked.CompareExchange(ref this.m_Entity, this.m_BackEntity, entity) != entity)
			{
				return false;
			}
			Thread.SpinWait(1);
			int count = entity.Count;
			T[] array = entity.Array;
			int num = 0;
			for (;;)
			{
				T arg = array[num];
				while (this.m_NullValidator(arg))
				{
					Thread.SpinWait(1);
					arg = array[num];
				}
				outputItems.Add(array[num]);
				array[num] = ConcurrentBatchQueue<T>.m_Null;
				if (entity.Count <= num + 1)
				{
					break;
				}
				num++;
			}
			entity.Count = 0;
			this.m_BackEntity = entity;
			return true;
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00006775 File Offset: 0x00004975
		public bool IsEmpty
		{
			get
			{
				return this.Count <= 0;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00006783 File Offset: 0x00004983
		public int Count
		{
			get
			{
				return (this.m_Entity as ConcurrentBatchQueue<T>.Entity).Count;
			}
		}

		// Token: 0x04000326 RID: 806
		private object m_Entity;

		// Token: 0x04000327 RID: 807
		private ConcurrentBatchQueue<T>.Entity m_BackEntity;

		// Token: 0x04000328 RID: 808
		private static readonly T m_Null;

		// Token: 0x04000329 RID: 809
		private Func<T, bool> m_NullValidator;

		// Token: 0x0400032A RID: 810
		private bool m_Rebuilding;

		// Token: 0x02000104 RID: 260
		private class Entity
		{
			// Token: 0x17000211 RID: 529
			// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00006795 File Offset: 0x00004995
			// (set) Token: 0x060007E9 RID: 2025 RVA: 0x0000679D File Offset: 0x0000499D
			public T[] Array { get; set; }

			// Token: 0x0400032C RID: 812
			public int Count;
		}
	}
}
