using NoiseMe.Drags.App.Models.Delegates;
using System;
using System.Collections.Generic;

namespace MailRy.Linq
{
	internal class DelegateComparer<TSource, TKey> : IComparer<TSource> where TKey : IComparable<TKey>
	{
		private Func<TSource, TKey> m_Getter;

		public DelegateComparer(Func<TSource, TKey> getter)
		{
			m_Getter = getter;
		}

		public int Compare(TSource x, TSource y)
		{
			return m_Getter(x).CompareTo(m_Getter(y));
		}
	}
}
