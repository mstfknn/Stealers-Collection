using System;
using System.Globalization;
using System.Text;
using GrandSteal.Client.Models.Extensions.Nulls;

namespace GrandSteal.Client.Models.Extensions.Strings
{
	// Token: 0x02000010 RID: 16
	public static class StringExtension
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00002358 File Offset: 0x00000558
		public static string Remove(this string input, string strToRemove)
		{
			if (input.IsNullOrEmpty())
			{
				return null;
			}
			return input.Replace(strToRemove, "");
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002370 File Offset: 0x00000570
		public static string Left(this string input, int minusRight = 1)
		{
			if (input.IsNullOrEmpty() || input.Length <= minusRight)
			{
				return null;
			}
			return input.Substring(0, input.Length - minusRight);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002394 File Offset: 0x00000594
		public static CultureInfo ToCultureInfo(this string culture, CultureInfo defaultCulture)
		{
			if (!culture.IsNullOrEmpty())
			{
				return defaultCulture;
			}
			return new CultureInfo(culture);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000023A6 File Offset: 0x000005A6
		public static string ToCamelCasing(this string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				return value.Substring(0, 1).ToUpper() + value.Substring(1, value.Length - 1);
			}
			return value;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003DB8 File Offset: 0x00001FB8
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

		// Token: 0x0600006F RID: 111 RVA: 0x00003DF8 File Offset: 0x00001FF8
		public static bool? ToBoolean(this string value)
		{
			bool value2 = false;
			if (bool.TryParse(value, out value2))
			{
				return new bool?(value2);
			}
			return null;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003E24 File Offset: 0x00002024
		public static int? ToInt32(this string value)
		{
			int value2 = 0;
			if (int.TryParse(value, out value2))
			{
				return new int?(value2);
			}
			return null;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003E50 File Offset: 0x00002050
		public static long? ToInt64(this string value)
		{
			long value2 = 0L;
			if (long.TryParse(value, out value2))
			{
				return new long?(value2);
			}
			return null;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003E7C File Offset: 0x0000207C
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

		// Token: 0x06000073 RID: 115 RVA: 0x000023D3 File Offset: 0x000005D3
		public static string FormatFirstLetterUpperCase(this string value, string culture = "en-US")
		{
			return CultureInfo.GetCultureInfo(culture).TextInfo.ToTitleCase(value);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003ED8 File Offset: 0x000020D8
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

		// Token: 0x06000075 RID: 117 RVA: 0x00003F38 File Offset: 0x00002138
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

		// Token: 0x06000076 RID: 118 RVA: 0x000023E6 File Offset: 0x000005E6
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
