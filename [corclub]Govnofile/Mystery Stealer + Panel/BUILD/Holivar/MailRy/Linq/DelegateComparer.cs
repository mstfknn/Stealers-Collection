using System;
using System.Collections.Generic;
using NoiseMe.Drags.App.Models.Delegates;

namespace MailRy.Linq
{
	// Token: 0x02000004 RID: 4
	internal class DelegateComparer<TSource, TKey> : IComparer<TSource> where TKey : IComparable<TKey>
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002193 File Offset: 0x00000393
		public DelegateComparer(Func<TSource, TKey> getter)
		{
			this.m_Getter = getter;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00009C00 File Offset: 0x00007E00
		public int Compare(TSource x, TSource y)
		{
			TKey tkey = this.m_Getter(x);
			return tkey.CompareTo(this.m_Getter(y));
		}

		// Token: 0x04000002 RID: 2
		private Func<TSource, TKey> m_Getter;
	}
}
