using System;
using System.Collections.Generic;

namespace MailRy.Collections.Concurrent
{
	// Token: 0x02000005 RID: 5
	public class ConcurrentQueue<T>
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000021A2 File Offset: 0x000003A2
		public ConcurrentQueue()
		{
			this.m_Queue = new Queue<T>();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021C0 File Offset: 0x000003C0
		public ConcurrentQueue(int capacity)
		{
			this.m_Queue = new Queue<T>(capacity);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021DF File Offset: 0x000003DF
		public ConcurrentQueue(IEnumerable<T> collection)
		{
			this.m_Queue = new Queue<T>(collection);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00009C34 File Offset: 0x00007E34
		public void Enqueue(T item)
		{
			object syncRoot = this.m_SyncRoot;
			lock (syncRoot)
			{
				this.m_Queue.Enqueue(item);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00009C74 File Offset: 0x00007E74
		public bool TryDequeue(out T item)
		{
			object syncRoot = this.m_SyncRoot;
			bool result;
			lock (syncRoot)
			{
				if (this.m_Queue.Count <= 0)
				{
					item = default(T);
					result = false;
				}
				else
				{
					item = this.m_Queue.Dequeue();
					result = true;
				}
			}
			return result;
		}

		// Token: 0x04000003 RID: 3
		private Queue<T> m_Queue;

		// Token: 0x04000004 RID: 4
		private object m_SyncRoot = new object();
	}
}
