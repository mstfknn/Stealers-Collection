using System;

namespace NoiseMe.Drags.App.Models.LocalModels.Extensions.Nulls
{
	// Token: 0x0200013A RID: 314
	public static class IsNullExtension
	{
		// Token: 0x060009B0 RID: 2480 RVA: 0x00007546 File Offset: 0x00005746
		public static bool IsNotNull<T>(this T data)
		{
			return data != null;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00007551 File Offset: 0x00005751
		public static string IsNull(this string value, string defaultValue)
		{
			if (!string.IsNullOrEmpty(value))
			{
				return value;
			}
			return defaultValue;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0000755E File Offset: 0x0000575E
		public static bool IsNullOrEmpty(this string str)
		{
			return string.IsNullOrEmpty(str);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00007566 File Offset: 0x00005766
		public static bool IsNull(this bool? value, bool def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0000757A File Offset: 0x0000577A
		public static T IsNull<T>(this T value) where T : class
		{
			if (value == null)
			{
				return Activator.CreateInstance<T>();
			}
			return value;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0000758B File Offset: 0x0000578B
		public static T IsNull<T>(this T value, T def) where T : class
		{
			if (value != null)
			{
				return value;
			}
			if (def == null)
			{
				return Activator.CreateInstance<T>();
			}
			return def;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x000075A6 File Offset: 0x000057A6
		public static int IsNull(this int? value, int def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x000075BA File Offset: 0x000057BA
		public static long IsNull(this long? value, long def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x000075CE File Offset: 0x000057CE
		public static double IsNull(this double? value, double def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x000075E2 File Offset: 0x000057E2
		public static DateTime IsNull(this DateTime? value, DateTime def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x000075F6 File Offset: 0x000057F6
		public static Guid IsNull(this Guid? value, Guid def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}
	}
}
