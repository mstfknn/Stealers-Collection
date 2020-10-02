using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NoiseMe.Drags.App.Models.JSON
{
	// Token: 0x0200013E RID: 318
	public class JsonObject : JsonValue, IDictionary<string, JsonValue>, ICollection<KeyValuePair<string, JsonValue>>, IEnumerable<KeyValuePair<string, JsonValue>>, IEnumerable
	{
		// Token: 0x060009DC RID: 2524 RVA: 0x000077CC File Offset: 0x000059CC
		public JsonObject(params KeyValuePair<string, JsonValue>[] items)
		{
			this.map = new SortedDictionary<string, JsonValue>(StringComparer.Ordinal);
			if (items != null)
			{
				this.AddRange(items);
			}
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x000077EE File Offset: 0x000059EE
		public JsonObject(IEnumerable<KeyValuePair<string, JsonValue>> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.map = new SortedDictionary<string, JsonValue>(StringComparer.Ordinal);
			this.AddRange(items);
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x0000781B File Offset: 0x00005A1B
		public override int Count
		{
			get
			{
				return this.map.Count;
			}
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00007828 File Offset: 0x00005A28
		public IEnumerator<KeyValuePair<string, JsonValue>> GetEnumerator()
		{
			return this.map.GetEnumerator();
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00007828 File Offset: 0x00005A28
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.map.GetEnumerator();
		}

		// Token: 0x1700026E RID: 622
		public sealed override JsonValue this[string key]
		{
			get
			{
				return this.map[key];
			}
			set
			{
				this.map[key] = value;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00007857 File Offset: 0x00005A57
		public override JsonType JsonType
		{
			get
			{
				return JsonType.Object;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0000785A File Offset: 0x00005A5A
		public ICollection<string> Keys
		{
			get
			{
				return this.map.Keys;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00007867 File Offset: 0x00005A67
		public ICollection<JsonValue> Values
		{
			get
			{
				return this.map.Values;
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00007874 File Offset: 0x00005A74
		public void Add(string key, JsonValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.map.Add(key, value);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00007891 File Offset: 0x00005A91
		public void Add(KeyValuePair<string, JsonValue> pair)
		{
			this.Add(pair.Key, pair.Value);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00020D44 File Offset: 0x0001EF44
		public void AddRange(IEnumerable<KeyValuePair<string, JsonValue>> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			foreach (KeyValuePair<string, JsonValue> keyValuePair in items)
			{
				this.map.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x000078A7 File Offset: 0x00005AA7
		public void AddRange(params KeyValuePair<string, JsonValue>[] items)
		{
			this.AddRange(items);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x000078B0 File Offset: 0x00005AB0
		public void Clear()
		{
			this.map.Clear();
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000078BD File Offset: 0x00005ABD
		bool ICollection<KeyValuePair<string, JsonValue>>.Contains(KeyValuePair<string, JsonValue> item)
		{
			return ((ICollection<KeyValuePair<string, JsonValue>>)this.map).Contains(item);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x000078CB File Offset: 0x00005ACB
		bool ICollection<KeyValuePair<string, JsonValue>>.Remove(KeyValuePair<string, JsonValue> item)
		{
			return ((ICollection<KeyValuePair<string, JsonValue>>)this.map).Remove(item);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x000078D9 File Offset: 0x00005AD9
		public override bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.map.ContainsKey(key);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x000078F5 File Offset: 0x00005AF5
		public void CopyTo(KeyValuePair<string, JsonValue>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<string, JsonValue>>)this.map).CopyTo(array, arrayIndex);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00007904 File Offset: 0x00005B04
		public bool Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.map.Remove(key);
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x000031DF File Offset: 0x000013DF
		bool ICollection<KeyValuePair<string, JsonValue>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00020DAC File Offset: 0x0001EFAC
		public override void Save(Stream stream, bool parsing)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			stream.WriteByte(123);
			foreach (KeyValuePair<string, JsonValue> keyValuePair in this.map)
			{
				stream.WriteByte(34);
				byte[] bytes = Encoding.UTF8.GetBytes(base.EscapeString(keyValuePair.Key));
				stream.Write(bytes, 0, bytes.Length);
				stream.WriteByte(34);
				stream.WriteByte(44);
				stream.WriteByte(32);
				if (keyValuePair.Value == null)
				{
					stream.WriteByte(110);
					stream.WriteByte(117);
					stream.WriteByte(108);
					stream.WriteByte(108);
				}
				else
				{
					keyValuePair.Value.Save(stream, parsing);
				}
			}
			stream.WriteByte(125);
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00007920 File Offset: 0x00005B20
		public bool TryGetValue(string key, out JsonValue value)
		{
			return this.map.TryGetValue(key, out value);
		}

		// Token: 0x040003AD RID: 941
		private SortedDictionary<string, JsonValue> map;
	}
}
