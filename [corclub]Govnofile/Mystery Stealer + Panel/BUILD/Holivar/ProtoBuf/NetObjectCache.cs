using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ProtoBuf.Meta;

namespace ProtoBuf
{
	// Token: 0x0200001F RID: 31
	internal sealed class NetObjectCache
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00002662 File Offset: 0x00000862
		private MutableList List
		{
			get
			{
				if (this.underlyingList == null)
				{
					this.underlyingList = new MutableList();
				}
				return this.underlyingList;
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000B250 File Offset: 0x00009450
		internal object GetKeyedObject(int key)
		{
			if (key-- == 0)
			{
				if (this.rootObject == null)
				{
					throw new ProtoException("No root object assigned");
				}
				return this.rootObject;
			}
			else
			{
				BasicList list = this.List;
				if (key < 0 || key >= list.Count)
				{
					throw new ProtoException("Internal error; a missing key occurred");
				}
				object obj = list[key];
				if (obj == null)
				{
					throw new ProtoException("A deferred key does not have a value yet");
				}
				return obj;
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000B2B4 File Offset: 0x000094B4
		internal void SetKeyedObject(int key, object value)
		{
			if (key-- != 0)
			{
				MutableList list = this.List;
				if (key < list.Count)
				{
					object obj = list[key];
					if (obj == null)
					{
						list[key] = value;
						return;
					}
					if (obj != value)
					{
						throw new ProtoException("Reference-tracked objects cannot change reference");
					}
				}
				else if (key != list.Add(value))
				{
					throw new ProtoException("Internal error; a key mismatch occurred");
				}
				return;
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.rootObject != null && this.rootObject != value)
			{
				throw new ProtoException("The root object cannot be reassigned");
			}
			this.rootObject = value;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000B344 File Offset: 0x00009544
		internal int AddObjectKey(object value, out bool existing)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value == this.rootObject)
			{
				existing = true;
				return 0;
			}
			string text = value as string;
			BasicList list = this.List;
			int num;
			if (text == null)
			{
				if (this.objectKeys == null)
				{
					this.objectKeys = new Dictionary<object, int>(NetObjectCache.ReferenceComparer.Default);
					num = -1;
				}
				else if (!this.objectKeys.TryGetValue(value, out num))
				{
					num = -1;
				}
			}
			else if (this.stringKeys == null)
			{
				this.stringKeys = new Dictionary<string, int>();
				num = -1;
			}
			else if (!this.stringKeys.TryGetValue(text, out num))
			{
				num = -1;
			}
			if (!(existing = (num >= 0)))
			{
				num = list.Add(value);
				if (text == null)
				{
					this.objectKeys.Add(value, num);
				}
				else
				{
					this.stringKeys.Add(text, num);
				}
			}
			return num + 1;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000B410 File Offset: 0x00009610
		internal void RegisterTrappedObject(object value)
		{
			if (this.rootObject == null)
			{
				this.rootObject = value;
				return;
			}
			if (this.underlyingList != null)
			{
				for (int i = this.trapStartIndex; i < this.underlyingList.Count; i++)
				{
					this.trapStartIndex = i + 1;
					if (this.underlyingList[i] == null)
					{
						this.underlyingList[i] = value;
						return;
					}
				}
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000B478 File Offset: 0x00009678
		internal void Clear()
		{
			this.trapStartIndex = 0;
			this.rootObject = null;
			if (this.underlyingList != null)
			{
				this.underlyingList.Clear();
			}
			if (this.stringKeys != null)
			{
				this.stringKeys.Clear();
			}
			if (this.objectKeys != null)
			{
				this.objectKeys.Clear();
			}
		}

		// Token: 0x04000074 RID: 116
		internal const int Root = 0;

		// Token: 0x04000075 RID: 117
		private MutableList underlyingList;

		// Token: 0x04000076 RID: 118
		private object rootObject;

		// Token: 0x04000077 RID: 119
		private int trapStartIndex;

		// Token: 0x04000078 RID: 120
		private Dictionary<string, int> stringKeys;

		// Token: 0x04000079 RID: 121
		private Dictionary<object, int> objectKeys;

		// Token: 0x02000020 RID: 32
		private sealed class ReferenceComparer : IEqualityComparer<object>
		{
			// Token: 0x060000A2 RID: 162 RVA: 0x000022E5 File Offset: 0x000004E5
			private ReferenceComparer()
			{
			}

			// Token: 0x060000A3 RID: 163 RVA: 0x0000267D File Offset: 0x0000087D
			bool IEqualityComparer<object>.Equals(object x, object y)
			{
				return x == y;
			}

			// Token: 0x060000A4 RID: 164 RVA: 0x00002683 File Offset: 0x00000883
			int IEqualityComparer<object>.GetHashCode(object obj)
			{
				return RuntimeHelpers.GetHashCode(obj);
			}

			// Token: 0x0400007A RID: 122
			public static readonly NetObjectCache.ReferenceComparer Default = new NetObjectCache.ReferenceComparer();
		}
	}
}
