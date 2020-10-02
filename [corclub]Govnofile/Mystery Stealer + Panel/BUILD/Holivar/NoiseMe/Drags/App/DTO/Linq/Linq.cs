using System;
using System.Collections;
using System.Collections.Generic;
using MailRy.Linq;
using NoiseMe.Drags.App.Models.Delegates;

namespace NoiseMe.Drags.App.DTO.Linq
{
	// Token: 0x0200007A RID: 122
	public static class Linq
	{
		// Token: 0x0600041D RID: 1053 RVA: 0x000157C4 File Offset: 0x000139C4
		public static IEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> getter) where TKey : IComparable<TKey>
		{
			List<TSource> list = new List<TSource>();
			foreach (TSource item in source)
			{
				list.Add(item);
			}
			list.Sort(new DelegateComparer<TSource, TKey>(getter));
			return list;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00015820 File Offset: 0x00013A20
		public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			foreach (TSource arg in source)
			{
				if (predicate(arg))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00015890 File Offset: 0x00013A90
		public static bool Any<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000491B File Offset: 0x00002B1B
		public static IEnumerable<TResult> Cast<TResult>(this IEnumerable source)
		{
			foreach (object obj in source)
			{
				yield return (TResult)((object)obj);
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000158E8 File Offset: 0x00013AE8
		public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value)
		{
			foreach (TSource tsource in source)
			{
				if (tsource.Equals(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00015948 File Offset: 0x00013B48
		public static int Count<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			ICollection<TSource> collection;
			if ((collection = (source as ICollection<TSource>)) != null)
			{
				return collection.Count;
			}
			ICollection collection2;
			if ((collection2 = (source as ICollection)) != null)
			{
				return collection2.Count;
			}
			int num = 0;
			foreach (TSource tsource in source)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000159C0 File Offset: 0x00013BC0
		public static int Count<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predic)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predic == null)
			{
				throw new ArgumentNullException("predic");
			}
			ICollection<TSource> collection;
			if ((collection = (source as ICollection<TSource>)) != null)
			{
				return collection.Count;
			}
			ICollection collection2;
			if ((collection2 = (source as ICollection)) != null)
			{
				return collection2.Count;
			}
			int num = 0;
			foreach (TSource arg in source)
			{
				if (predic(arg))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000492B File Offset: 0x00002B2B
		public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source)
		{
			Hashtable set = new Hashtable();
			foreach (TSource tsource in source)
			{
				if (!set.ContainsKey(tsource))
				{
					set.Add(tsource, null);
					yield return tsource;
				}
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00015A54 File Offset: 0x00013C54
		public static TSource First<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			IList<TSource> list;
			if ((list = (source as IList<TSource>)) != null)
			{
				if (list.Count > 0)
				{
					return list[0];
				}
			}
			else
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						return enumerator.Current;
					}
				}
			}
			throw new InvalidOperationException("No elements");
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00015ACC File Offset: 0x00013CCC
		public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			foreach (TSource tsource in source)
			{
				if (predicate(tsource))
				{
					return tsource;
				}
			}
			throw new InvalidOperationException("No match");
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00015B44 File Offset: 0x00013D44
		public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			IList<TSource> list;
			if ((list = (source as IList<TSource>)) != null)
			{
				if (list.Count > 0)
				{
					return list[0];
				}
			}
			else
			{
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						return enumerator.Current;
					}
				}
			}
			return default(TSource);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00015BBC File Offset: 0x00013DBC
		public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			foreach (TSource tsource in source)
			{
				if (predicate(tsource))
				{
					return tsource;
				}
			}
			return default(TSource);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00015C34 File Offset: 0x00013E34
		public static TSource Min<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			Comparer<TSource> @default = Comparer<TSource>.Default;
			TSource tsource = default(TSource);
			if (tsource == null)
			{
				foreach (TSource tsource2 in source)
				{
					if (tsource2 != null && (tsource == null || @default.Compare(tsource2, tsource) < 0))
					{
						tsource = tsource2;
					}
				}
				return tsource;
			}
			bool flag = false;
			foreach (TSource tsource3 in source)
			{
				if (flag)
				{
					if (@default.Compare(tsource3, tsource) < 0)
					{
						tsource = tsource3;
					}
				}
				else
				{
					tsource = tsource3;
					flag = true;
				}
			}
			if (flag)
			{
				return tsource;
			}
			throw new InvalidOperationException("No elements");
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00015D18 File Offset: 0x00013F18
		public static TSource Max<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			Comparer<TSource> @default = Comparer<TSource>.Default;
			TSource tsource = default(TSource);
			if (tsource == null)
			{
				foreach (TSource tsource2 in source)
				{
					if (tsource2 != null && (tsource == null || @default.Compare(tsource2, tsource) > 0))
					{
						tsource = tsource2;
					}
				}
				return tsource;
			}
			bool flag = false;
			foreach (TSource tsource3 in source)
			{
				if (flag)
				{
					if (@default.Compare(tsource3, tsource) > 0)
					{
						tsource = tsource3;
					}
				}
				else
				{
					tsource = tsource3;
					flag = true;
				}
			}
			if (flag)
			{
				return tsource;
			}
			throw new InvalidOperationException("No elements");
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00015DFC File Offset: 0x00013FFC
		public static IEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			SortedDictionary<TKey, TSource> sortedDictionary = new SortedDictionary<TKey, TSource>();
			foreach (TSource tsource in source)
			{
				sortedDictionary.Add(keySelector(tsource), tsource);
			}
			return sortedDictionary.Values;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000493B File Offset: 0x00002B3B
		public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
		{
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			foreach (TSource arg in source)
			{
				yield return selector(arg);
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00015E58 File Offset: 0x00014058
		public static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			TSource result = default(TSource);
			long num = 0L;
			checked
			{
				foreach (TSource tsource in source)
				{
					if (predicate(tsource))
					{
						result = tsource;
						num += 1L;
					}
				}
				if (num == 0L)
				{
					throw new InvalidOperationException("No matches");
				}
				if (num != 1L)
				{
					throw new InvalidOperationException("More than one match.");
				}
				return result;
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00015EF0 File Offset: 0x000140F0
		public static int Sum(this IEnumerable<int> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			int num = 0;
			checked
			{
				foreach (int num2 in source)
				{
					num += num2;
				}
				return num;
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00004952 File Offset: 0x00002B52
		public static int Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
		{
			return source.Select(selector).Sum();
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00004960 File Offset: 0x00002B60
		public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
		{
			return source.ToList<TSource>().ToArray();
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00015F48 File Offset: 0x00014148
		public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			if (elementSelector == null)
			{
				throw new ArgumentNullException("elementSelector");
			}
			Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>(comparer);
			foreach (TSource arg in source)
			{
				dictionary.Add(keySelector(arg), elementSelector(arg));
			}
			return dictionary;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00015FD0 File Offset: 0x000141D0
		public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
		{
			List<TSource> list = new List<TSource>();
			foreach (TSource item in source)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000496D File Offset: 0x00002B6D
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			foreach (TSource tsource in source)
			{
				if (predicate(tsource))
				{
					yield return tsource;
				}
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}
	}
}
