using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000FC RID: 252
	public static class StateFullSearch
	{
		// Token: 0x0600078D RID: 1933 RVA: 0x0001BED0 File Offset: 0x0001A0D0
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

		// Token: 0x0600078E RID: 1934 RVA: 0x00006540 File Offset: 0x00004740
		public static int? SearchMark<T>(this IList<T> source, T[] mark, out int parsedLength) where T : IEquatable<T>
		{
			return source.SearchMark(0, source.Count, mark, 0, out parsedLength);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001BF08 File Offset: 0x0001A108
		public static int? SearchMark<T>(this IList<T> source, T[] mark) where T : IEquatable<T>
		{
			int num;
			return source.SearchMark(0, source.Count, mark, 0, out num);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001BF28 File Offset: 0x0001A128
		public static int? SearchMark<T>(this IList<T> source, int offset, int length, T[] mark) where T : IEquatable<T>
		{
			int num;
			return source.SearchMark(offset, length, mark, 0, out num);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00006552 File Offset: 0x00004752
		public static int? SearchMark<T>(this IList<T> source, int offset, int length, T[] mark, out int parsedLength) where T : IEquatable<T>
		{
			return source.SearchMark(offset, length, mark, 0, out parsedLength);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001BF44 File Offset: 0x0001A144
		public static int? SearchMark<T>(this IList<T> source, int offset, int length, T[] mark, int matched) where T : IEquatable<T>
		{
			int num;
			return source.SearchMark(offset, length, mark, matched, out num);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001BF60 File Offset: 0x0001A160
		public static int? SearchMark<T>(this IList<T> source, int offset, int length, T[] mark, int matched, out int parsedLength) where T : IEquatable<T>
		{
			int num = offset;
			int num2 = offset + length - 1;
			int num3 = matched;
			parsedLength = 0;
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
							parsedLength = mark.Length - matched;
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
					parsedLength = mark.Length - matched;
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
			parsedLength = num - offset + mark.Length;
			return new int?(num);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001C088 File Offset: 0x0001A288
		public static int SearchMark<T>(this IList<T> source, int offset, int length, SearchMarkState<T> searchState, out int parsedLength) where T : IEquatable<T>
		{
			int? num = source.SearchMark(offset, length, searchState.Mark, searchState.Matched, out parsedLength);
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

		// Token: 0x06000795 RID: 1941 RVA: 0x00006560 File Offset: 0x00004760
		public static int StartsWith<T>(this IList<T> source, T[] mark) where T : IEquatable<T>
		{
			return source.StartsWith(0, source.Count, mark);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0001C0E8 File Offset: 0x0001A2E8
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

		// Token: 0x06000797 RID: 1943 RVA: 0x00006570 File Offset: 0x00004770
		public static bool EndsWith<T>(this IList<T> source, T[] mark) where T : IEquatable<T>
		{
			return source.EndsWith(0, source.Count, mark);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0001C13C File Offset: 0x0001A33C
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

		// Token: 0x06000799 RID: 1945 RVA: 0x0001C190 File Offset: 0x0001A390
		public static T[] CloneRange<T>(this IList<T> source, int offset, int length)
		{
			T[] array = source as T[];
			T[] array2;
			if (array != null)
			{
				array2 = new T[length];
				Array.Copy(array, offset, array2, 0, length);
				return array2;
			}
			array2 = new T[length];
			for (int i = 0; i < length; i++)
			{
				array2[i] = source[offset + i];
			}
			return array2;
		}
	}
}
