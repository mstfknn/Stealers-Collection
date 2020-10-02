using MailRy.Linq;
using NoiseMe.Drags.App.Models.Delegates;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.DTO.Linq
{
	public static class Linq
	{
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
			foreach (TSource item in source)
			{
				if (predicate(item))
				{
					return true;
				}
			}
			return false;
		}

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
					TSource current = enumerator.Current;
					return true;
				}
			}
			return false;
		}

		public static IEnumerable<TResult> Cast<TResult>(this IEnumerable source)
		{
			foreach (object item in source)
			{
				yield return (TResult)item;
			}
		}

		public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value)
		{
			foreach (TSource item in source)
			{
				if (item.Equals(value))
				{
					return true;
				}
			}
			return false;
		}

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
			foreach (TSource item in source)
			{
				TSource val = item;
				num++;
			}
			return num;
		}

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
			foreach (TSource item in source)
			{
				if (predic(item))
				{
					num++;
				}
			}
			return num;
		}

		public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source)
		{
			Hashtable set = new Hashtable();
			foreach (TSource item in source)
			{
				if (!set.ContainsKey(item))
				{
					set.Add(item, null);
					yield return item;
				}
			}
		}

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
			foreach (TSource item in source)
			{
				if (predicate(item))
				{
					return item;
				}
			}
			throw new InvalidOperationException("No match");
		}

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
			foreach (TSource item in source)
			{
				if (predicate(item))
				{
					return item;
				}
			}
			return default(TSource);
		}

		public static TSource Min<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			Comparer<TSource> @default = Comparer<TSource>.Default;
			TSource val = default(TSource);
			if (val == null)
			{
				foreach (TSource item in source)
				{
					if (item != null && (val == null || @default.Compare(item, val) < 0))
					{
						val = item;
					}
				}
				return val;
			}
			bool flag = false;
			foreach (TSource item2 in source)
			{
				if (flag)
				{
					if (@default.Compare(item2, val) < 0)
					{
						val = item2;
					}
				}
				else
				{
					val = item2;
					flag = true;
				}
			}
			if (flag)
			{
				return val;
			}
			throw new InvalidOperationException("No elements");
		}

		public static TSource Max<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			Comparer<TSource> @default = Comparer<TSource>.Default;
			TSource val = default(TSource);
			if (val == null)
			{
				foreach (TSource item in source)
				{
					if (item != null && (val == null || @default.Compare(item, val) > 0))
					{
						val = item;
					}
				}
				return val;
			}
			bool flag = false;
			foreach (TSource item2 in source)
			{
				if (flag)
				{
					if (@default.Compare(item2, val) > 0)
					{
						val = item2;
					}
				}
				else
				{
					val = item2;
					flag = true;
				}
			}
			if (flag)
			{
				return val;
			}
			throw new InvalidOperationException("No elements");
		}

		public static IEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			SortedDictionary<TKey, TSource> sortedDictionary = new SortedDictionary<TKey, TSource>();
			foreach (TSource item in source)
			{
				sortedDictionary.Add(keySelector(item), item);
			}
			return sortedDictionary.Values;
		}

		public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
		{
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			foreach (TSource item in source)
			{
				yield return selector(item);
			}
		}

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
			foreach (TSource item in source)
			{
				if (predicate(item))
				{
					result = item;
					num = checked(num + 1);
				}
			}
			switch (num)
			{
			case 0L:
				throw new InvalidOperationException("No matches");
			default:
				throw new InvalidOperationException("More than one match.");
			case 1L:
				return result;
			}
		}

		public static int Sum(this IEnumerable<int> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			int num = 0;
			foreach (int item in source)
			{
				num = checked(num + item);
			}
			return num;
		}

		public static int Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
		{
			return source.Select(selector).Sum();
		}

		public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
		{
			return source.ToList().ToArray();
		}

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
			foreach (TSource item in source)
			{
				dictionary.Add(keySelector(item), elementSelector(item));
			}
			return dictionary;
		}

		public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
		{
			List<TSource> list = new List<TSource>();
			foreach (TSource item in source)
			{
				list.Add(item);
			}
			return list;
		}

		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			foreach (TSource item in source)
			{
				if (predicate(item))
				{
					yield return item;
				}
			}
		}
	}
}
