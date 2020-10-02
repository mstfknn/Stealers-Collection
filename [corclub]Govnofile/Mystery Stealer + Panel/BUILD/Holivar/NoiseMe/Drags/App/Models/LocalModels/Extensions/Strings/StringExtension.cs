using System;
using System.Globalization;
using System.Text;
using NoiseMe.Drags.App.Models.LocalModels.Extensions.Nulls;

namespace NoiseMe.Drags.App.Models.LocalModels.Extensions.Strings
{
	// Token: 0x02000139 RID: 313
	public static class StringExtension
	{
		// Token: 0x060009A2 RID: 2466 RVA: 0x00007482 File Offset: 0x00005682
		public static T ForceTo<T>(this object @this)
		{
			return (T)((object)Convert.ChangeType(@this, typeof(T)));
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00007499 File Offset: 0x00005699
		public static string Remove(this string input, string strToRemove)
		{
			if (input.IsNullOrEmpty())
			{
				return null;
			}
			return input.Replace(strToRemove, "");
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x000074B1 File Offset: 0x000056B1
		public static string Left(this string input, int minusRight = 1)
		{
			if (input.IsNullOrEmpty() || input.Length <= minusRight)
			{
				return null;
			}
			return input.Substring(0, input.Length - minusRight);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x000074D5 File Offset: 0x000056D5
		public static CultureInfo ToCultureInfo(this string culture, CultureInfo defaultCulture)
		{
			if (!culture.IsNullOrEmpty())
			{
				return defaultCulture;
			}
			return new CultureInfo(culture);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x000074E7 File Offset: 0x000056E7
		public static string ToCamelCasing(this string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				return value.Substring(0, 1).ToUpper() + value.Substring(1, value.Length - 1);
			}
			return value;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x000202FC File Offset: 0x0001E4FC
		public static double? ToDouble(this string value, string culture = "en-US")
		{
			double? result;
			try
			{
				result = new double?(double.Parse(value, new CultureInfo(culture)));
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0002033C File Offset: 0x0001E53C
		public static bool? ToBoolean(this string value)
		{
			bool value2 = false;
			if (bool.TryParse(value, out value2))
			{
				return new bool?(value2);
			}
			return null;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00020368 File Offset: 0x0001E568
		public static int? ToInt32(this string value)
		{
			int value2 = 0;
			if (int.TryParse(value, out value2))
			{
				return new int?(value2);
			}
			return null;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00020394 File Offset: 0x0001E594
		public static long? ToInt64(this string value)
		{
			long value2 = 0L;
			if (long.TryParse(value, out value2))
			{
				return new long?(value2);
			}
			return null;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x000203C0 File Offset: 0x0001E5C0
		public static string AddQueyString(this string url, string queryStringKey, string queryStringValue)
		{
			string text;
			if (url.Split(new char[]
			{
				'?'
			}).Length > 1)
			{
				text = "&";
			}
			else
			{
				text = "?";
			}
			return string.Concat(new string[]
			{
				url,
				text,
				queryStringKey,
				"=",
				queryStringValue
			});
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00007514 File Offset: 0x00005714
		public static string FormatFirstLetterUpperCase(this string value, string culture = "en-US")
		{
			return CultureInfo.GetCultureInfo(culture).TextInfo.ToTitleCase(value);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0002041C File Offset: 0x0001E61C
		public static string FillLeftWithZeros(this string value, int decimalDigits)
		{
			if (!string.IsNullOrEmpty(value))
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(value);
				string[] array = value.Split(new char[]
				{
					','
				});
				for (int i = array[array.Length - 1].Length; i < decimalDigits; i++)
				{
					stringBuilder.Append("0");
				}
				value = stringBuilder.ToString();
			}
			return value;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0002047C File Offset: 0x0001E67C
		public static string FormatWithDecimalDigits(this string value, bool removeCurrencySymbol, bool returnZero, int? decimalDigits)
		{
			if (value.IsNullOrEmpty())
			{
				return value;
			}
			if (!value.IndexOf(",").Equals(-1))
			{
				string[] array = value.Split(new char[]
				{
					','
				});
				if (array.Length.Equals(2) && array[1].Length > 0)
				{
					value = array[0] + "," + array[1].Substring(0, (array[1].Length >= decimalDigits.Value) ? decimalDigits.Value : array[1].Length);
				}
			}
			if (decimalDigits == null)
			{
				return value;
			}
			return value.FillLeftWithZeros(decimalDigits.Value);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00007527 File Offset: 0x00005727
		public static string FormatWithoutDecimalDigits(this string value, bool removeCurrencySymbol, bool returnZero, int? decimalDigits, CultureInfo culture)
		{
			if (removeCurrencySymbol)
			{
				value = value.Remove(culture.NumberFormat.CurrencySymbol).Trim();
			}
			return value;
		}
	}
}
