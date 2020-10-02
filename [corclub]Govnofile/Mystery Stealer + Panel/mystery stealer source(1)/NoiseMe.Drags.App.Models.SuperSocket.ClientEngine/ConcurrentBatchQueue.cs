using NoiseMe.Drags.App.Models.Delegates;
using System.Collections.Generic;
using System.Threading;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	public class ConcurrentBatchQueue<T> : IBatchQueue<T>
	{
		private class Entity
		{
			public int Count;

			public T[] Array
			{
				get;
				set;
			}
		}

		private object m_Entity;

		private Entity m_BackEntity;

		private static readonly T m_Null;

		private Func<T, bool> m_NullValidator;

		private bool m_Rebuilding;

		public bool IsEmpty => Count <= 0;

		public int Count => (m_Entity as Entity).Count;

		public ConcurrentBatchQueue()
			: this(16)
		{
		}

		public ConcurrentBatchQueue(int capacity)
			: this(new T[capacity])
		{
		}

		public ConcurrentBatchQueue(int capacity, Func<T, bool> nullValidator)
			: this(new T[capacity], nullValidator)
		{
		}

		public ConcurrentBatchQueue(T[] array)
			: this(array, (Func<T, bool>)((T t) => t == null))
		{
		}

		public ConcurrentBatchQueue(T[] array, Func<T, bool> nullValidator)
		{
			m_Entity = new Entity
			{
				Array = array
			};
			m_BackEntity = new Entity();
			m_BackEntity.Array = new T[array.Length];
			m_NullValidator = nullValidator;
		}

		public bool Enqueue(T item)
		{
			bool full;
			while (!(TryEnqueue(item, out full) | full))
			{
			}
			return !full;
		}

		private bool TryEnqueue(T item, out bool full)
		{
			full = false;
			EnsureNotRebuild();
			Entity entity = m_Entity as Entity;
			T[] array = entity.Array;
			int count = entity.Count;
			if (count >= array.Length)
			{
				full = true;
				return false;
			}
			if (entity != m_Entity)
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

		public bool Enqueue(IList<T> items)
		{
			bool full;
			while (!(TryEnqueue(items, out full) | full))
			{
			}
			return !full;
		}

		private bool TryEnqueue(IList<T> items, out bool full)
		{
			full = false;
			Entity entity = m_Entity as Entity;
			T[] array = entity.Array;
			int num = entity.Count;
			int count = items.Count;
			int num2 = num + count;
			if (num2 > array.Length)
			{
				full = true;
				return false;
			}
			if (entity != m_Entity)
			{
				return false;
			}
			if (Interlocked.CompareExchange(ref entity.Count, num2, num) != num)
			{
				return false;
			}
			foreach (T item in items)
			{
				array[num++] = item;
			}
			return true;
		}

		private void EnsureNotRebuild()
		{
			if (m_Rebuilding)
			{
				do
				{
					Thread.SpinWait(1);
				}
				while (m_Rebuilding);
			}
		}

		public bool TryDequeue(IList<T> outputItems)
		{
			Entity entity = m_Entity as Entity;
			if (entity.Count <= 0)
			{
				return false;
			}
			if (Interlocked.CompareExchange(ref m_Entity, m_BackEntity, entity) != entity)
			{
				return false;
			}
			Thread.SpinWait(1);
			int count = entity.Count;
			T[] array = entity.Array;
			int num = 0;
			while (true)
			{
				T arg = array[num];
				while (m_NullValidator(arg))
				{
					Thread.SpinWait(1);
					arg = array[num];
				}
				outputItems.Add(array[num]);
				array[num] = m_Null;
				if (entity.Count <= num + 1)
				{
					break;
				}
				num++;
			}
			entity.Count = 0;
			m_BackEntity = entity;
			return true;
		}
	}
}
