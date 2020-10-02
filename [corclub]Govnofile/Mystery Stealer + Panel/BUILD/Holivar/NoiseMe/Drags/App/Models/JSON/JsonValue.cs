using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using NoiseMe.Drags.App.Models.LocalModels.Extensions.Nulls;

namespace NoiseMe.Drags.App.Models.JSON
{
	// Token: 0x02000141 RID: 321
	public abstract class JsonValue : IEnumerable
	{
		// Token: 0x06000A0C RID: 2572 RVA: 0x00007AC4 File Offset: 0x00005CC4
		public static JsonValue Load(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			return JsonValue.Load(new StreamReader(stream, true));
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00007AE0 File Offset: 0x00005CE0
		public static JsonValue Load(TextReader textReader)
		{
			if (textReader == null)
			{
				throw new ArgumentNullException("textReader");
			}
			return JsonValue.ToJsonValue<object>(new JavaScriptReader(textReader).Read());
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00007B00 File Offset: 0x00005D00
		private static IEnumerable<KeyValuePair<string, JsonValue>> ToJsonPairEnumerable(IEnumerable<KeyValuePair<string, object>> kvpc)
		{
			foreach (KeyValuePair<string, object> keyValuePair in kvpc)
			{
				yield return new KeyValuePair<string, JsonValue>(keyValuePair.Key, JsonValue.ToJsonValue<object>(keyValuePair.Value));
			}
			IEnumerator<KeyValuePair<string, object>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00007B10 File Offset: 0x00005D10
		private static IEnumerable<JsonValue> ToJsonValueEnumerable(IEnumerable arr)
		{
			foreach (object ret in arr)
			{
				yield return JsonValue.ToJsonValue<object>(ret);
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x000210AC File Offset: 0x0001F2AC
		public static JsonValue ToJsonValue<T>(T ret)
		{
			if (ret == null)
			{
				return null;
			}
			T t;
			if ((t = ret) is bool)
			{
				bool value = (bool)((object)t);
				return new JsonPrimitive(value);
			}
			if ((t = ret) is byte)
			{
				byte value2 = (byte)((object)t);
				return new JsonPrimitive(value2);
			}
			if ((t = ret) is char)
			{
				char value3 = (char)((object)t);
				return new JsonPrimitive(value3);
			}
			if ((t = ret) is decimal)
			{
				decimal value4 = (decimal)((object)t);
				return new JsonPrimitive(value4);
			}
			if ((t = ret) is double)
			{
				double value5 = (double)((object)t);
				return new JsonPrimitive(value5);
			}
			if ((t = ret) is float)
			{
				float value6 = (float)((object)t);
				return new JsonPrimitive(value6);
			}
			if ((t = ret) is int)
			{
				int value7 = (int)((object)t);
				return new JsonPrimitive(value7);
			}
			if ((t = ret) is long)
			{
				long value8 = (long)((object)t);
				return new JsonPrimitive(value8);
			}
			if ((t = ret) is sbyte)
			{
				sbyte value9 = (sbyte)((object)t);
				return new JsonPrimitive(value9);
			}
			if ((t = ret) is short)
			{
				short value10 = (short)((object)t);
				return new JsonPrimitive(value10);
			}
			string value11;
			if ((value11 = (ret as string)) != null)
			{
				return new JsonPrimitive(value11);
			}
			if ((t = ret) is uint)
			{
				uint value12 = (uint)((object)t);
				return new JsonPrimitive(value12);
			}
			if ((t = ret) is ulong)
			{
				ulong value13 = (ulong)((object)t);
				return new JsonPrimitive(value13);
			}
			if ((t = ret) is ushort)
			{
				ushort value14 = (ushort)((object)t);
				return new JsonPrimitive(value14);
			}
			if ((t = ret) is DateTime)
			{
				DateTime value15 = (DateTime)((object)t);
				return new JsonPrimitive(value15);
			}
			if ((t = ret) is DateTimeOffset)
			{
				DateTimeOffset value16 = (DateTimeOffset)((object)t);
				return new JsonPrimitive(value16);
			}
			if ((t = ret) is Guid)
			{
				Guid value17 = (Guid)((object)t);
				return new JsonPrimitive(value17);
			}
			if ((t = ret) is TimeSpan)
			{
				TimeSpan value18 = (TimeSpan)((object)t);
				return new JsonPrimitive(value18);
			}
			Uri value19;
			if ((value19 = (ret as Uri)) != null)
			{
				return new JsonPrimitive(value19);
			}
			IEnumerable<KeyValuePair<string, object>> enumerable = ret as IEnumerable<KeyValuePair<string, object>>;
			if (enumerable != null)
			{
				return new JsonObject(JsonValue.ToJsonPairEnumerable(enumerable));
			}
			IEnumerable enumerable2 = ret as IEnumerable;
			if (enumerable2 != null)
			{
				return new JsonArray(JsonValue.ToJsonValueEnumerable(enumerable2));
			}
			if (!(ret is IEnumerable))
			{
				PropertyInfo[] properties = ret.GetType().GetProperties();
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				foreach (PropertyInfo propertyInfo in properties)
				{
					dictionary.Add(propertyInfo.Name, propertyInfo.GetValue(ret, null).IsNull("null"));
				}
				if (dictionary.Count > 0)
				{
					return new JsonObject(JsonValue.ToJsonPairEnumerable(dictionary));
				}
			}
			throw new NotSupportedException(string.Format("Unexpected parser return type: {0}", ret.GetType()));
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00007B20 File Offset: 0x00005D20
		public static JsonValue Parse(string jsonString)
		{
			if (jsonString == null)
			{
				throw new ArgumentNullException("jsonString");
			}
			return JsonValue.Load(new StringReader(jsonString));
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x00007B3B File Offset: 0x00005D3B
		public virtual int Count
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000A13 RID: 2579
		public abstract JsonType JsonType { get; }

		// Token: 0x17000277 RID: 631
		public virtual JsonValue this[int index]
		{
			get
			{
				throw new InvalidOperationException();
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000278 RID: 632
		public virtual JsonValue this[string key]
		{
			get
			{
				throw new InvalidOperationException();
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00007B3B File Offset: 0x00005D3B
		public virtual bool ContainsKey(string key)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00007B42 File Offset: 0x00005D42
		public virtual void Save(Stream stream, bool parsing)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.Save(new StreamWriter(stream), parsing);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00007B5F File Offset: 0x00005D5F
		public virtual void Save(TextWriter textWriter, bool parsing)
		{
			if (textWriter == null)
			{
				throw new ArgumentNullException("textWriter");
			}
			this.Savepublic(textWriter, parsing);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00021448 File Offset: 0x0001F648
		private void Savepublic(TextWriter w, bool saving)
		{
			switch (this.JsonType)
			{
			case JsonType.String:
				if (saving)
				{
					w.Write('"');
				}
				w.Write(this.EscapeString(((JsonPrimitive)this).GetFormattedString()));
				if (saving)
				{
					w.Write('"');
					return;
				}
				return;
			case JsonType.Object:
			{
				w.Write('{');
				bool flag = false;
				foreach (KeyValuePair<string, JsonValue> keyValuePair in ((JsonObject)this))
				{
					if (flag)
					{
						w.Write(", ");
					}
					w.Write('"');
					w.Write(this.EscapeString(keyValuePair.Key));
					w.Write("\": ");
					if (keyValuePair.Value == null)
					{
						w.Write("null");
					}
					else
					{
						keyValuePair.Value.Savepublic(w, saving);
					}
					flag = true;
				}
				w.Write('}');
				return;
			}
			case JsonType.Array:
			{
				w.Write('[');
				bool flag = false;
				foreach (JsonValue jsonValue in ((IEnumerable<JsonValue>)((JsonArray)this)))
				{
					if (flag)
					{
						w.Write(", ");
					}
					if (jsonValue != null)
					{
						jsonValue.Savepublic(w, saving);
					}
					else
					{
						w.Write("null");
					}
					flag = true;
				}
				w.Write(']');
				return;
			}
			case JsonType.Boolean:
				w.Write(this ? "true" : "false");
				return;
			}
			w.Write(((JsonPrimitive)this).GetFormattedString());
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x000215F4 File Offset: 0x0001F7F4
		public string ToString(bool saving = true)
		{
			StringWriter stringWriter = new StringWriter();
			this.Save(stringWriter, saving);
			return stringWriter.ToString();
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00007B3B File Offset: 0x00005D3B
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00021618 File Offset: 0x0001F818
		private bool NeedEscape(string src, int i)
		{
			char c = src[i];
			return c < ' ' || c == '"' || c == '\\' || (c >= '\ud800' && c <= '\udbff' && (i == src.Length - 1 || src[i + 1] < '\udc00' || src[i + 1] > '\udfff')) || (c >= '\udc00' && c <= '\udfff' && (i == 0 || src[i - 1] < '\ud800' || src[i - 1] > '\udbff')) || c == '\u2028' || c == '\u2029' || (c == '/' && i > 0 && src[i - 1] == '<');
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x000216E0 File Offset: 0x0001F8E0
		public string EscapeString(string src)
		{
			if (src == null)
			{
				return null;
			}
			for (int i = 0; i < src.Length; i++)
			{
				if (this.NeedEscape(src, i))
				{
					StringBuilder stringBuilder = new StringBuilder();
					if (i > 0)
					{
						stringBuilder.Append(src, 0, i);
					}
					return this.DoEscapeString(stringBuilder, src, i);
				}
			}
			return src;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0002172C File Offset: 0x0001F92C
		private string DoEscapeString(StringBuilder sb, string src, int cur)
		{
			int num = cur;
			for (int i = cur; i < src.Length; i++)
			{
				if (this.NeedEscape(src, i))
				{
					sb.Append(src, num, i - num);
					char c = src[i];
					if (c <= '"')
					{
						switch (c)
						{
						case '\b':
							sb.Append("\\b");
							break;
						case '\t':
							sb.Append("\\t");
							break;
						case '\n':
							sb.Append("\\n");
							break;
						case '\v':
							goto IL_D5;
						case '\f':
							sb.Append("\\f");
							break;
						case '\r':
							sb.Append("\\r");
							break;
						default:
							if (c != '"')
							{
								goto IL_D5;
							}
							sb.Append("\\\"");
							break;
						}
					}
					else if (c != '/')
					{
						if (c != '\\')
						{
							goto IL_D5;
						}
						sb.Append("\\\\");
					}
					else
					{
						sb.Append("\\/");
					}
					IL_FC:
					num = i + 1;
					goto IL_100;
					IL_D5:
					sb.Append("\\u");
					sb.Append(((int)src[i]).ToString("x04"));
					goto IL_FC;
				}
				IL_100:;
			}
			sb.Append(src, num, src.Length - num);
			return sb.ToString();
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00007B77 File Offset: 0x00005D77
		public static implicit operator JsonValue(bool value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00007B7F File Offset: 0x00005D7F
		public static implicit operator JsonValue(byte value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00007B87 File Offset: 0x00005D87
		public static implicit operator JsonValue(char value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00007B8F File Offset: 0x00005D8F
		public static implicit operator JsonValue(decimal value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00007B97 File Offset: 0x00005D97
		public static implicit operator JsonValue(double value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00007B9F File Offset: 0x00005D9F
		public static implicit operator JsonValue(float value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00007BA7 File Offset: 0x00005DA7
		public static implicit operator JsonValue(int value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00007BAF File Offset: 0x00005DAF
		public static implicit operator JsonValue(long value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00007BB7 File Offset: 0x00005DB7
		public static implicit operator JsonValue(sbyte value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00007BBF File Offset: 0x00005DBF
		public static implicit operator JsonValue(short value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00007BC7 File Offset: 0x00005DC7
		public static implicit operator JsonValue(string value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00007BCF File Offset: 0x00005DCF
		public static implicit operator JsonValue(uint value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00007BD7 File Offset: 0x00005DD7
		public static implicit operator JsonValue(ulong value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00007BDF File Offset: 0x00005DDF
		public static implicit operator JsonValue(ushort value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00007BE7 File Offset: 0x00005DE7
		public static implicit operator JsonValue(DateTime value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00007BEF File Offset: 0x00005DEF
		public static implicit operator JsonValue(DateTimeOffset value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00007BF7 File Offset: 0x00005DF7
		public static implicit operator JsonValue(Guid value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00007BFF File Offset: 0x00005DFF
		public static implicit operator JsonValue(TimeSpan value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00007C07 File Offset: 0x00005E07
		public static implicit operator JsonValue(Uri value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00007C0F File Offset: 0x00005E0F
		public static implicit operator bool(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToBoolean(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00007C34 File Offset: 0x00005E34
		public static implicit operator byte(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToByte(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00007C59 File Offset: 0x00005E59
		public static implicit operator char(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToChar(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00007C7E File Offset: 0x00005E7E
		public static implicit operator decimal(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToDecimal(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00007CA3 File Offset: 0x00005EA3
		public static implicit operator double(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToDouble(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00007CC8 File Offset: 0x00005EC8
		public static implicit operator float(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToSingle(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00007CED File Offset: 0x00005EED
		public static implicit operator int(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToInt32(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00007D12 File Offset: 0x00005F12
		public static implicit operator long(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToInt64(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00007D37 File Offset: 0x00005F37
		public static implicit operator sbyte(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToSByte(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00007D5C File Offset: 0x00005F5C
		public static implicit operator short(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToInt16(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00007D81 File Offset: 0x00005F81
		public static implicit operator string(JsonValue value)
		{
			if (value == null)
			{
				return null;
			}
			return value.ToString(true);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00007D8F File Offset: 0x00005F8F
		public static implicit operator uint(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToUInt32(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00007DB4 File Offset: 0x00005FB4
		public static implicit operator ulong(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToUInt64(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00007DD9 File Offset: 0x00005FD9
		public static implicit operator ushort(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToUInt16(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00007DFE File Offset: 0x00005FFE
		public static implicit operator DateTime(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (DateTime)((JsonPrimitive)value).Value;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00007E1E File Offset: 0x0000601E
		public static implicit operator DateTimeOffset(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (DateTimeOffset)((JsonPrimitive)value).Value;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00007E3E File Offset: 0x0000603E
		public static implicit operator TimeSpan(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (TimeSpan)((JsonPrimitive)value).Value;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00007E5E File Offset: 0x0000605E
		public static implicit operator Guid(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (Guid)((JsonPrimitive)value).Value;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00007E7E File Offset: 0x0000607E
		public static implicit operator Uri(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (Uri)((JsonPrimitive)value).Value;
		}
	}
}
