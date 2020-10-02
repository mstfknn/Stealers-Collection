using System;
using System.Collections;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Data.WMI
{
	// Token: 0x02000177 RID: 375
	public class WmiInstanceClassCollection : ICollection<WmiInstanceClass>, IEnumerable<WmiInstanceClass>, IEnumerable
	{
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x0000953E File Offset: 0x0000773E
		public int Count
		{
			get
			{
				return this._classes.Count;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x000031DF File Offset: 0x000013DF
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000309 RID: 777
		public WmiInstanceClass this[int classIndex]
		{
			get
			{
				return this._classes[classIndex];
			}
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00009559 File Offset: 0x00007759
		public WmiInstanceClassCollection()
		{
			this._classes = new List<WmiInstanceClass>();
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0000956C File Offset: 0x0000776C
		public WmiInstanceClassCollection(ICollection<WmiInstanceClass> collection)
		{
			this._classes = new List<WmiInstanceClass>(collection);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00009580 File Offset: 0x00007780
		public void Add(WmiInstanceClass item)
		{
			this._classes.Add(item);
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0000958E File Offset: 0x0000778E
		public void Clear()
		{
			this._classes.Clear();
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0000959B File Offset: 0x0000779B
		public bool Contains(WmiInstanceClass item)
		{
			return this._classes.Contains(item);
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x000095A9 File Offset: 0x000077A9
		public void CopyTo(WmiInstanceClass[] array, int arrayIndex)
		{
			this._classes.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x000095B8 File Offset: 0x000077B8
		public IEnumerator<WmiInstanceClass> GetEnumerator()
		{
			return this._classes.GetEnumerator();
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x000095CA File Offset: 0x000077CA
		public bool Remove(WmiInstanceClass item)
		{
			return this._classes.Remove(item);
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x000095B8 File Offset: 0x000077B8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._classes.GetEnumerator();
		}

		// Token: 0x040004A4 RID: 1188
		private readonly List<WmiInstanceClass> _classes;
	}
}
