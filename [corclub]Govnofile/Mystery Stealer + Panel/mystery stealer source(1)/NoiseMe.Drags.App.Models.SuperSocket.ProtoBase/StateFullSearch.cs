using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public static class StateFullSearch
	{
		public static int IndexOf<T>(this IList<T> source, T target, int pos, int length) where T : IEquatable<T>
		{
			for (int i = pos; i < pos + length; i++)
			{
				if (source[i].Equals(target))
				{
					return i;
				}
			}
			return -1;
		}

		public static int? SearchMark<T>(this IList<T> source, T[] mark, out int parsedLength) where T : IEquatable<T>
		{
			return source.SearchMark(0, source.Count, mark, 0, out parsedLength);
		}

		public static int? SearchMark<T>(this IList<T> source, T[] mark) where T : IEquatable<T>
		{
			int parsedLength;
			return source.SearchMark(0, source.Count, mark, 0, out parsedLength);
		}

		public static int? SearchMark<T>(this IList<T> source, int offset, int length, T[] mark) where T : IEquatable<T>
		{
			int parsedLength;
			return source.SearchMark(offset, length, mark, 0, out parsedLength);
		}

		public static int? SearchMark<T>(this IList<T> source, int offset, int length, T[] mark, out int parsedLength) where T : IEquatable<T>
		{
			return source.SearchMark(offset, length, mark, 0, out parsedLength);
		}

		public static int? SearchMark<T>(this IList<T> source, int offset, int length, T[] mark, int matched) where T : IEquatable<T>
		{
			int parsedLength;
			return source.SearchMark(offset, length, mark, matched, out parsedLength);
		}

		public static int? SearchMark<T>(this IList<T> source, int offset, int length, T[] mark, int matched, out int parsedLength) where T : IEquatable<T>
		{
			int num = offset;
			int num2 = offset + length - 1;
			int num3 = matched;
			parsedLength = 0;
			if (matched > 0)
			{
				for (int i = num3; i < mark.Length; i++)
				{
					if (!source[num++].Equals(mark[i]))
					{
						break;
					}
					num3++;
					if (num > num2)
					{
						if (num3 == mark.Length)
						{
							parsedLength = mark.Length - matched;
							return offset;
						}
						return -num3;
					}
				}
				if (num3 == mark.Length)
				{
					parsedLength = mark.Length - matched;
					return offset;
				}
				num = offset;
				num3 = 0;
			}
			while (true)
			{
				num = source.IndexOf(mark[num3], num, length - num + offset);
				if (num < 0)
				{
					return null;
				}
				num3++;
				for (int j = num3; j < mark.Length; j++)
				{
					int num5 = num + j;
					if (num5 > num2)
					{
						return -num3;
					}
					if (!source[num5].Equals(mark[j]))
					{
						break;
					}
					num3++;
				}
				if (num3 == mark.Length)
				{
					break;
				}
				num++;
				num3 = 0;
			}
			parsedLength = num - offset + mark.Length;
			return num;
		}

		public static int SearchMark<T>(this IList<T> source, int offset, int length, SearchMarkState<T> searchState, out int parsedLength) where T : IEquatable<T>
		{
			int? num = source.SearchMark(offset, length, searchState.Mark, searchState.Matched, out parsedLength);
			if (!num.HasValue)
			{
				searchState.Matched = 0;
				return -1;
			}
			if (num.Value < 0)
			{
				searchState.Matched = -num.Value;
				return -1;
			}
			searchState.Matched = 0;
			return num.Value;
		}

		public static int StartsWith<T>(this IList<T> source, T[] mark) where T : IEquatable<T>
		{
			return source.StartsWith(0, source.Count, mark);
		}

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
				if (!source[num2].Equals(mark[i]))
				{
					return -1;
				}
			}
			return mark.Length;
		}

		public static bool EndsWith<T>(this IList<T> source, T[] mark) where T : IEquatable<T>
		{
			return source.EndsWith(0, source.Count, mark);
		}

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
