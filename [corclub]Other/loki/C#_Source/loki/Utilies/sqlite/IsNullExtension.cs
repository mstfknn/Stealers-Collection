namespace loki.sqlite.nulls
{
    using System;

    public static class IsNullExtension
    {
        public static bool IsNotNull<T>(this T data) => data != null;

        public static string IsNull(this string value, string defaultValue) => !string.IsNullOrEmpty(value) ? value : defaultValue;

        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        public static bool IsNull(this bool? value, bool def) => value ?? def;

        public static T IsNull<T>(this T value) where T : class => value ?? Activator.CreateInstance<T>();

        public static T IsNull<T>(this T value, T def) where T : class => value ?? def ?? Activator.CreateInstance<T>();

        public static int IsNull(this int? value, int def) => value ?? def;

        public static long IsNull(this long? value, long def) => value ?? def;

        public static double IsNull(this double? value, double def) => value ?? def;

        public static DateTime IsNull(this DateTime? value, DateTime def) => value ?? def;

        public static Guid IsNull(this Guid? value, Guid def) => value ?? def;
    }
}