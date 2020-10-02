using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace NoiseMe.Drags.App.Models.JSON
{
	// Token: 0x0200013F RID: 319
	public class JsonPrimitive : JsonValue
	{
		// Token: 0x060009F3 RID: 2547 RVA: 0x0000792F File Offset: 0x00005B2F
		public JsonPrimitive(bool value)
		{
			this.value = value;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00007943 File Offset: 0x00005B43
		public JsonPrimitive(byte value)
		{
			this.value = value;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00007957 File Offset: 0x00005B57
		public JsonPrimitive(char value)
		{
			this.value = value;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0000796B File Offset: 0x00005B6B
		public JsonPrimitive(decimal value)
		{
			this.value = value;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0000797F File Offset: 0x00005B7F
		public JsonPrimitive(double value)
		{
			this.value = value;
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00007993 File Offset: 0x00005B93
		public JsonPrimitive(float value)
		{
			this.value = value;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x000079A7 File Offset: 0x00005BA7
		public JsonPrimitive(int value)
		{
			this.value = value;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x000079BB File Offset: 0x00005BBB
		public JsonPrimitive(long value)
		{
			this.value = value;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x000079CF File Offset: 0x00005BCF
		public JsonPrimitive(sbyte value)
		{
			this.value = value;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x000079E3 File Offset: 0x00005BE3
		public JsonPrimitive(short value)
		{
			this.value = value;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x000079F7 File Offset: 0x00005BF7
		public JsonPrimitive(string value)
		{
			this.value = value;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00007A06 File Offset: 0x00005C06
		public JsonPrimitive(DateTime value)
		{
			this.value = value;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00007A1A File Offset: 0x00005C1A
		public JsonPrimitive(uint value)
		{
			this.value = value;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00007A2E File Offset: 0x00005C2E
		public JsonPrimitive(ulong value)
		{
			this.value = value;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00007A42 File Offset: 0x00005C42
		public JsonPrimitive(ushort value)
		{
			this.value = value;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00007A56 File Offset: 0x00005C56
		public JsonPrimitive(DateTimeOffset value)
		{
			this.value = value;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00007A6A File Offset: 0x00005C6A
		public JsonPrimitive(Guid value)
		{
			this.value = value;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00007A7E File Offset: 0x00005C7E
		public JsonPrimitive(TimeSpan value)
		{
			this.value = value;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x000079F7 File Offset: 0x00005BF7
		public JsonPrimitive(Uri value)
		{
			this.value = value;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x000079F7 File Offset: 0x00005BF7
		public JsonPrimitive(object value)
		{
			this.value = value;
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00007A92 File Offset: 0x00005C92
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x00020E98 File Offset: 0x0001F098
		public override JsonType JsonType
		{
			get
			{
				if (this.value == null)
				{
					return JsonType.String;
				}
				TypeCode typeCode = Type.GetTypeCode(this.value.GetType());
				switch (typeCode)
				{
				case TypeCode.Object:
				case TypeCode.Char:
					break;
				case TypeCode.DBNull:
					return JsonType.Number;
				case TypeCode.Boolean:
					return JsonType.Boolean;
				default:
					if (typeCode != TypeCode.DateTime && typeCode != TypeCode.String)
					{
						return JsonType.Number;
					}
					break;
				}
				return JsonType.String;
			}
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00020EEC File Offset: 0x0001F0EC
		public override void Save(Stream stream, bool parsing)
		{
			JsonType jsonType = this.JsonType;
			if (jsonType == JsonType.String)
			{
				stream.WriteByte(34);
				byte[] bytes = Encoding.UTF8.GetBytes(base.EscapeString(this.value.ToString()));
				stream.Write(bytes, 0, bytes.Length);
				stream.WriteByte(34);
				return;
			}
			if (jsonType != JsonType.Boolean)
			{
				byte[] bytes = Encoding.UTF8.GetBytes(this.GetFormattedString());
				stream.Write(bytes, 0, bytes.Length);
				return;
			}
			if ((bool)this.value)
			{
				stream.Write(JsonPrimitive.true_bytes, 0, 4);
				return;
			}
			stream.Write(JsonPrimitive.false_bytes, 0, 5);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00020F84 File Offset: 0x0001F184
		public string GetFormattedString()
		{
			JsonType jsonType = this.JsonType;
			if (jsonType != JsonType.String)
			{
				if (jsonType != JsonType.Number)
				{
					throw new InvalidOperationException();
				}
				string text;
				if (this.value is float || this.value is double)
				{
					text = ((IFormattable)this.value).ToString("R", NumberFormatInfo.InvariantInfo);
				}
				else
				{
					text = ((IFormattable)this.value).ToString("G", NumberFormatInfo.InvariantInfo);
				}
				if (text == "NaN" || text == "Infinity" || text == "-Infinity")
				{
					return "\"" + text + "\"";
				}
				return text;
			}
			else if (this.value is string || this.value == null)
			{
				string text2 = this.value as string;
				if (string.IsNullOrEmpty(text2))
				{
					return "null";
				}
				return text2.Trim(new char[]
				{
					'"'
				});
			}
			else
			{
				if (this.value is char)
				{
					return this.value.ToString();
				}
				throw new NotImplementedException("GetFormattedString from value type " + this.value.GetType());
			}
		}

		// Token: 0x040003AE RID: 942
		private object value;

		// Token: 0x040003AF RID: 943
		private static readonly byte[] true_bytes = Encoding.UTF8.GetBytes("true");

		// Token: 0x040003B0 RID: 944
		private static readonly byte[] false_bytes = Encoding.UTF8.GetBytes("false");
	}
}
