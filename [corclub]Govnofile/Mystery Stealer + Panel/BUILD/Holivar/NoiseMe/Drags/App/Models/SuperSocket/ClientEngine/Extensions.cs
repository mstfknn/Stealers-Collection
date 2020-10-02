using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x0200010A RID: 266
	public static class Extensions
	{
		// Token: 0x0600080F RID: 2063 RVA: 0x0001BED0 File Offset: 0x0001A0D0
		public static int IndexOf<T>(this IList<T> source, T target, int pos, int length) where T : IEquatable<T>
		{
			for (int i = pos; i < pos + length; i++)
			{
				T t = source[i];
				if (t.Equals(target))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x000068BB File Offset: 0x00004ABB
		public static int? SearchMark<T>(this IList<T> source, T[] mark) where T : IEquatable<T>
		{
			return source.SearchMark(0, source.Count, mark, 0);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x000068CC File Offset: 0x00004ACC
		public static int? SearchMark<T>(this IList<T> source, int offset, int length, T[] mark) where T : IEquatable<T>
		{
			return source.SearchMark(offset, length, mark, 0);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001CFD4 File Offset: 0x0001B1D4
		public static int? SearchMark<T>(this IList<T> source, int offset, int length, T[] mark, int matched) where T : IEquatable<T>
		{
			int num = offset;
			int num2 = offset + length - 1;
			int num3 = matched;
			if (matched > 0)
			{
				int i = num3;
				while (i < mark.Length)
				{
					T t = source[num++];
					if (!t.Equals(mark[i]))
					{
						break;
					}
					num3++;
					if (num > num2)
					{
						if (num3 == mark.Length)
						{
							return new int?(offset);
						}
						return new int?(0 - num3);
					}
					else
					{
						i++;
					}
				}
				if (num3 == mark.Length)
				{
					return new int?(offset);
				}
				num = offset;
				num3 = 0;
			}
			for (;;)
			{
				num = source.IndexOf(mark[num3], num, length - num + offset);
				if (num < 0)
				{
					break;
				}
				num3++;
				for (int j = num3; j < mark.Length; j++)
				{
					int num4 = num + j;
					if (num4 > num2)
					{
						goto Block_7;
					}
					T t = source[num4];
					if (!t.Equals(mark[j]))
					{
						break;
					}
					num3++;
				}
				if (num3 == mark.Length)
				{
					goto Block_9;
				}
				num++;
				num3 = 0;
			}
			return null;
			Block_7:
			return new int?(0 - num3);
			Block_9:
			return new int?(num);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001D0DC File Offset: 0x0001B2DC
		public static int SearchMark<T>(this IList<T> source, int offset, int length, SearchMarkState<T> searchState) where T : IEquatable<T>
		{
			int? num = source.SearchMark(offset, length, searchState.Mark, searchState.Matched);
			if (num == null)
			{
				searchState.Matched = 0;
				return -1;
			}
			if (num.Value < 0)
			{
				searchState.Matched = 0 - num.Value;
				return -1;
			}
			searchState.Matched = 0;
			return num.Value;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x000068D8 File Offset: 0x00004AD8
		public static int StartsWith<T>(this IList<T> source, T[] mark) where T : IEquatable<T>
		{
			return source.StartsWith(0, source.Count, mark);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001C0E8 File Offset: 0x0001A2E8
		public static int StartsWith<T>(this IList<T> source, int offset, int length, T[] mark) where T : IEquatable<T>
		{
			int num = offset + length - 1;
			for (int i = 0; i < mark.Length; i++)
			{
				int num2 = offset + i;
				if (num2 > num)
				{
					return i;
				}
				T t = source[num2];
				if (!t.Equals(mark[i]))
				{
					return -1;
				}
			}
			return mark.Length;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x000068E8 File Offset: 0x00004AE8
		public static bool EndsWith<T>(this IList<T> source, T[] mark) where T : IEquatable<T>
		{
			return source.EndsWith(0, source.Count, mark);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001C13C File Offset: 0x0001A33C
		public static bool EndsWith<T>(this IList<T> source, int offset, int length, T[] mark) where T : IEquatable<T>
		{
			if (mark.Length > length)
			{
				return false;
			}
			for (int i = 0; i < Math.Min(length, mark.Length); i++)
			{
				if (!mark[i].Equals(source[offset + length - mark.Length + i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001D13C File Offset: 0x0001B33C
		public static T[] CloneRange<T>(this T[] source, int offset, int length)
		{
			T[] array = new T[length];
			Array.Copy(source, offset, array, 0, length);
			return array;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001D15C File Offset: 0x0001B35C
		public static T[] RandomOrder<T>(this T[] source)
		{
			int num = source.Length / 2;
			for (int i = 0; i < num; i++)
			{
				int num2 = Extensions.m_Random.Next(0, source.Length - 1);
				int num3 = Extensions.m_Random.Next(0, source.Length - 1);
				if (num2 != num3)
				{
					T t = source[num3];
					source[num3] = source[num2];
					source[num2] = t;
				}
			}
			return source;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x000068F8 File Offset: 0x00004AF8
		public static string GetValue(this NameValueCollection collection, string key)
		{
			return collection.GetValue(key, string.Empty);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001D1C4 File Offset: 0x0001B3C4
		public static string GetValue(this NameValueCollection collection, string key, string defaultValue)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key");
			}
			if (collection == null)
			{
				return defaultValue;
			}
			string text = collection[key];
			if (text == null)
			{
				return defaultValue;
			}
			return text;
		}

		// Token: 0x04000339 RID: 825
		private static Random m_Random = new Random();
	}
}
