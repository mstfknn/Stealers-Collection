using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using NoiseMe.Drags.App.DTO.Linq;

namespace NoiseMe.Drags.App.Models.WebSocket4Net
{
	// Token: 0x02000094 RID: 148
	public static class Extensions
	{
		// Token: 0x060004E0 RID: 1248 RVA: 0x0000504F File Offset: 0x0000324F
		public static void AppendFormatWithCrCf(this StringBuilder builder, string format, object arg)
		{
			builder.AppendFormat(format, arg);
			builder.Append(Extensions.m_CrCf);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00005066 File Offset: 0x00003266
		public static void AppendFormatWithCrCf(this StringBuilder builder, string format, params object[] args)
		{
			builder.AppendFormat(format, args);
			builder.Append(Extensions.m_CrCf);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000507D File Offset: 0x0000327D
		public static void AppendWithCrCf(this StringBuilder builder, string content)
		{
			builder.Append(content);
			builder.Append(Extensions.m_CrCf);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00005093 File Offset: 0x00003293
		public static void AppendWithCrCf(this StringBuilder builder)
		{
			builder.Append(Extensions.m_CrCf);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000178BC File Offset: 0x00015ABC
		public static bool ParseMimeHeader(this string source, IDictionary<string, object> valueContainer, out string verbLine)
		{
			verbLine = string.Empty;
			string text = string.Empty;
			StringReader stringReader = new StringReader(source);
			string text2;
			while (!string.IsNullOrEmpty(text2 = stringReader.ReadLine()))
			{
				if (string.IsNullOrEmpty(verbLine))
				{
					verbLine = text2;
				}
				else if (text2.StartsWith("\t") && !string.IsNullOrEmpty(text))
				{
					object arg;
					if (!valueContainer.TryGetValue(text, out arg))
					{
						return false;
					}
					valueContainer[text] = arg + text2.Trim();
				}
				else
				{
					int num = text2.IndexOf(':');
					if (num >= 0)
					{
						string text3 = text2.Substring(0, num);
						if (!string.IsNullOrEmpty(text3))
						{
							text3 = text3.Trim();
						}
						string text4 = text2.Substring(num + 1);
						if (!string.IsNullOrEmpty(text4) && text4.StartsWith(" ") && text4.Length > 1)
						{
							text4 = text4.Substring(1);
						}
						if (!string.IsNullOrEmpty(text3))
						{
							object arg;
							if (!valueContainer.TryGetValue(text3, out arg))
							{
								valueContainer.Add(text3, text4);
							}
							else
							{
								valueContainer[text3] = arg + ", " + text4;
							}
							text = text3;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000179E4 File Offset: 0x00015BE4
		public static TValue GetValue<TValue>(this IDictionary<string, object> valueContainer, string name)
		{
			return valueContainer.GetValue(name, default(TValue));
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00017A04 File Offset: 0x00015C04
		public static TValue GetValue<TValue>(this IDictionary<string, object> valueContainer, string name, TValue defaultValue)
		{
			object obj;
			if (!valueContainer.TryGetValue(name, out obj))
			{
				return defaultValue;
			}
			return (TValue)((object)obj);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x000050A1 File Offset: 0x000032A1
		public static bool IsSimpleType(this Type type)
		{
			return type.IsValueType || type.IsPrimitive || Extensions.m_SimpleTypes.Contains(type) || Convert.GetTypeCode(type) != TypeCode.Object;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000050CE File Offset: 0x000032CE
		public static string GetOrigin(this Uri uri)
		{
			return uri.GetLeftPart(UriPartial.Authority);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000050D7 File Offset: 0x000032D7
		public static byte[] ComputeMD5Hash(this byte[] source)
		{
			return MD5.Create().ComputeHash(source);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000050E4 File Offset: 0x000032E4
		public static string CalculateChallenge(this string source)
		{
			return Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(source)));
		}

		// Token: 0x04000213 RID: 531
		private static readonly char[] m_CrCf = new char[]
		{
			'\r',
			'\n'
		};

		// Token: 0x04000214 RID: 532
		private const string m_Tab = "\t";

		// Token: 0x04000215 RID: 533
		private const char m_Colon = ':';

		// Token: 0x04000216 RID: 534
		private const string m_Space = " ";

		// Token: 0x04000217 RID: 535
		private const string m_ValueSeparator = ", ";

		// Token: 0x04000218 RID: 536
		private static Type[] m_SimpleTypes = new Type[]
		{
			typeof(string),
			typeof(decimal),
			typeof(DateTime),
			typeof(DateTimeOffset),
			typeof(TimeSpan),
			typeof(Guid)
		};
	}
}
