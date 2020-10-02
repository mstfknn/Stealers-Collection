using System;
using System.IO;

namespace NoiseMe.Drags.App.Models.JSON
{
	// Token: 0x0200013D RID: 317
	public static class JsonExt
	{
		// Token: 0x060009DA RID: 2522 RVA: 0x000077B2 File Offset: 0x000059B2
		public static JsonValue FromJSON(this string json)
		{
			return JsonValue.Load(new StringReader(json));
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x000077BF File Offset: 0x000059BF
		public static string ToJSON<T>(this T instance)
		{
			return JsonValue.ToJsonValue<T>(instance);
		}
	}
}
