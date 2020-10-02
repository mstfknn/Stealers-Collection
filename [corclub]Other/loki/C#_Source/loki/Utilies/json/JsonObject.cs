using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NoiseMe.Drags.App.Models.JSON
{
    public class JsonObject : JsonValue, IDictionary<string, JsonValue>, ICollection<KeyValuePair<string, JsonValue>>, IEnumerable<KeyValuePair<string, JsonValue>>, IEnumerable
    {
        private SortedDictionary<string, JsonValue> map;

        public override int Count => this.map.Count;

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

        public override JsonType JsonType => JsonType.Object;

        public ICollection<string> Keys => this.map.Keys;

        public ICollection<JsonValue> Values => this.map.Values;

        bool ICollection<KeyValuePair<string, JsonValue>>.IsReadOnly => false;

        public JsonObject(params KeyValuePair<string, JsonValue>[] items)
        {
            this.map = new SortedDictionary<string, JsonValue>(StringComparer.Ordinal);
            if (items != null)
            {
                AddRange(items);
            }
        }

        public JsonObject(IEnumerable<KeyValuePair<string, JsonValue>> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }
            this.map = new SortedDictionary<string, JsonValue>(StringComparer.Ordinal);
            AddRange(items);
        }

        public IEnumerator<KeyValuePair<string, JsonValue>> GetEnumerator()
        {
            return this.map.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.map.GetEnumerator();
        }

        public void Add(string key, JsonValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            this.map.Add(key, value);
        }

        public void Add(KeyValuePair<string, JsonValue> pair)
        {
            Add(pair.Key, pair.Value);
        }

        public void AddRange(IEnumerable<KeyValuePair<string, JsonValue>> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }
            foreach (KeyValuePair<string, JsonValue> item in items)
            {
                this.map.Add(item.Key, item.Value);
            }
        }

        public void AddRange(params KeyValuePair<string, JsonValue>[] items)
        {
            AddRange((IEnumerable<KeyValuePair<string, JsonValue>>)items);
        }

        public void Clear()
        {
            this.map.Clear();
        }

        bool ICollection<KeyValuePair<string, JsonValue>>.Contains(KeyValuePair<string, JsonValue> item)
        {
            return ((ICollection<KeyValuePair<string, JsonValue>>)this.map).Contains(item);
        }

        bool ICollection<KeyValuePair<string, JsonValue>>.Remove(KeyValuePair<string, JsonValue> item)
        {
            return ((ICollection<KeyValuePair<string, JsonValue>>)this.map).Remove(item);
        }

        public override bool ContainsKey(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            return this.map.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, JsonValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, JsonValue>>)this.map).CopyTo(array, arrayIndex);
        }

        public bool Remove(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            return this.map.Remove(key);
        }

        public override void Save(Stream stream, bool parsing)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            stream.WriteByte(123);
            foreach (KeyValuePair<string, JsonValue> item in this.map)
            {
                stream.WriteByte(34);
                byte[] bytes = Encoding.UTF8.GetBytes(EscapeString(item.Key));
                stream.Write(bytes, 0, bytes.Length);
                stream.WriteByte(34);
                stream.WriteByte(44);
                stream.WriteByte(32);
                if (item.Value == null)
                {
                    stream.WriteByte(110);
                    stream.WriteByte(117);
                    stream.WriteByte(108);
                    stream.WriteByte(108);
                }
                else
                {
                    item.Value.Save(stream, parsing);
                }
            }
            stream.WriteByte(125);
        }

        public bool TryGetValue(string key, out JsonValue value)
        {
            return this.map.TryGetValue(key, out value);
        }
    }
}
