using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NoiseMe.Drags.App.Models.JSON
{
	// Token: 0x0200013C RID: 316
	public class JsonArray : JsonValue, IList<JsonValue>, ICollection<JsonValue>, IEnumerable<JsonValue>, IEnumerable
	{
		// Token: 0x060009C6 RID: 2502 RVA: 0x0000768A File Offset: 0x0000588A
		public JsonArray(params JsonValue[] items)
		{
			this.list = new List<JsonValue>();
			this.AddRange(items);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x000076A4 File Offset: 0x000058A4
		public JsonArray(IEnumerable<JsonValue> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.list = new List<JsonValue>(items);
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x000076C6 File Offset: 0x000058C6
		public override int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x000031DF File Offset: 0x000013DF
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700026B RID: 619
		public sealed override JsonValue this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x000076F0 File Offset: 0x000058F0
		public override JsonType JsonType
		{
			get
			{
				return JsonType.Array;
			}
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x000076F3 File Offset: 0x000058F3
		public void Add(JsonValue item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.list.Add(item);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0000770F File Offset: 0x0000590F
		public void AddRange(IEnumerable<JsonValue> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.list.AddRange(items);
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0000772B File Offset: 0x0000592B
		public void AddRange(params JsonValue[] items)
		{
			if (items == null)
			{
				return;
			}
			this.list.AddRange(items);
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0000773D File Offset: 0x0000593D
		public void Clear()
		{
			this.list.Clear();
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0000774A File Offset: 0x0000594A
		public bool Contains(JsonValue item)
		{
			return this.list.Contains(item);
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00007758 File Offset: 0x00005958
		public void CopyTo(JsonValue[] array, int arrayIndex)
		{
			this.list.CopyTo(array, arrayIndex);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00007767 File Offset: 0x00005967
		public int IndexOf(JsonValue item)
		{
			return this.list.IndexOf(item);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00007775 File Offset: 0x00005975
		public void Insert(int index, JsonValue item)
		{
			this.list.Insert(index, item);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00007784 File Offset: 0x00005984
		public bool Remove(JsonValue item)
		{
			return this.list.Remove(item);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00007792 File Offset: 0x00005992
		public void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00020CAC File Offset: 0x0001EEAC
		public override void Save(Stream stream, bool parsing)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			stream.WriteByte(91);
			for (int i = 0; i < this.list.Count; i++)
			{
				JsonValue jsonValue = this.list[i];
				if (jsonValue != null)
				{
					jsonValue.Save(stream, parsing);
				}
				else
				{
					stream.WriteByte(110);
					stream.WriteByte(117);
					stream.WriteByte(108);
					stream.WriteByte(108);
				}
				if (i < this.Count - 1)
				{
					stream.WriteByte(44);
					stream.WriteByte(32);
				}
			}
			stream.WriteByte(93);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x000077A0 File Offset: 0x000059A0
		IEnumerator<JsonValue> IEnumerable<JsonValue>.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x000077A0 File Offset: 0x000059A0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x040003AC RID: 940
		private List<JsonValue> list;
	}
}
