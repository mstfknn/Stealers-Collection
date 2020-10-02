namespace loki.sqlite.strings
{
    using System;
    using System.Globalization;
    using System.Text;
    using sqlite.nulls;

    public static class StringExtension
    {
        public static T ForceTo<T>(this object @this) => (T)Convert.ChangeType(@this, typeof(T));

        public static string Remove(this string input, string strToRemove) => input.IsNullOrEmpty() ? null : input.Replace(strToRemove, "");

        public static string Left(this string input, int minusRight = 1) => input.IsNullOrEmpty() || input.Length <= minusRight ? null : input.Substring(0, input.Length - minusRight);

        public static CultureInfo ToCultureInfo(this string culture, CultureInfo defaultCulture) => !culture.IsNullOrEmpty() ? defaultCulture : new CultureInfo(culture);

        public static string ToCamelCasing(this string value) => !string.IsNullOrEmpty(value) ? value.Substring(0, 1).ToUpper() + value.Substring(1, value.Length - 1) : value;

        public static double? ToDouble(this string value, string culture = "en-US")
        {
            try
            {
                return double.Parse(value, new CultureInfo(culture));
            }
            catch
            {
                return null;
            }
        }

        public static bool? ToBoolean(this string value) => bool.TryParse(value, out bool result) ? result : (bool?)null;

        public static int? ToInt32(this string value) => int.TryParse(value, out int result) ? result : (int?)null;

        public static long? ToInt64(this string value) => long.TryParse(value, out long result) ? result : (long?)null;

        public static string AddQueyString(this string url, string queryStringKey, string queryStringValue) => $"{url}{(url.Split('?').Length <= 1 ? "?" : "&")}{queryStringKey}={queryStringValue}";

        public static string FormatFirstLetterUpperCase(this string value, string culture = "en-US") => CultureInfo.GetCultureInfo(culture).TextInfo.ToTitleCase(value);

        public static string FillLeftWithZeros(this string value, int decimalDigits)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append(value);
                string[] array = value.Split(',');
                for (int i = array[array.Length - 1].Length; i < decimalDigits; i++)
                {
                    stringBuilder.Append("0");
                }
                value = stringBuilder.ToString();
            }
            return value;
        }

        public static string FormatWithDecimalDigits(this string value, bool removeCurrencySymbol, bool returnZero, int? decimalDigits)
        {
            if (!value.IsNullOrEmpty())
            {
                if (!value.IndexOf(",").Equals(-1))
                {
                    string[] array = value.Split(',');
                    if (array.Length.Equals(2) && array[1].Length > 0)
                    {
                        value = $"{array[0]},{array[1].Substring(0, (array[1].Length >= decimalDigits.Value) ? decimalDigits.Value : array[1].Length)}";
                    }
                }
                return !decimalDigits.HasValue ? value : value.FillLeftWithZeros(decimalDigits.Value);
            }
            return value;
        }

        public static string FormatWithoutDecimalDigits(this string value, bool removeCurrencySymbol, bool returnZero, int? decimalDigits, CultureInfo culture)
        {
            if (!removeCurrencySymbol)
            {
                return value;
            }
            value = value.Remove(culture.NumberFormat.CurrencySymbol).Trim();
            return value;
        }
    }
}