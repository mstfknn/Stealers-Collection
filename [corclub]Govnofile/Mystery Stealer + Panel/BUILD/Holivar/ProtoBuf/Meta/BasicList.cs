using System;
using System.Collections;

namespace ProtoBuf.Meta
{
	// Token: 0x02000061 RID: 97
	internal class BasicList : IEnumerable
	{
		// Token: 0x060002CF RID: 719 RVA: 0x00003AF0 File Offset: 0x00001CF0
		public void CopyTo(Array array, int offset)
		{
			this.head.CopyTo(array, offset);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000FE80 File Offset: 0x0000E080
		public int Add(object value)
		{
			return (this.head = this.head.Append(value)).Length - 1;
		}

		// Token: 0x170000BE RID: 190
		public object this[int index]
		{
			get
			{
				return this.head[index];
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00003AFF File Offset: 0x00001CFF
		public void Trim()
		{
			this.head = this.head.Trim();
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00003B12 File Offset: 0x00001D12
		public int Count
		{
			get
			{
				return this.head.Length;
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00003B1F File Offset: 0x00001D1F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new BasicList.NodeEnumerator(this.head);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00003B31 File Offset: 0x00001D31
		public BasicList.NodeEnumerator GetEnumerator()
		{
			return new BasicList.NodeEnumerator(this.head);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00003B3E File Offset: 0x00001D3E
		internal int IndexOf(BasicList.MatchPredicate predicate, object ctx)
		{
			return this.head.IndexOf(predicate, ctx);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00003B4D File Offset: 0x00001D4D
		internal int IndexOfString(string value)
		{
			return this.head.IndexOfString(value);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00003B5B File Offset: 0x00001D5B
		internal int IndexOfReference(object instance)
		{
			return this.head.IndexOfReference(instance);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000FEAC File Offset: 0x0000E0AC
		internal bool Contains(object value)
		{
			BasicList.NodeEnumerator enumerator = this.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (object.Equals(enumerator.Current, value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000FEE0 File Offset: 0x0000E0E0
		internal static BasicList GetContiguousGroups(int[] keys, object[] values)
		{
			if (keys == null)
			{
				throw new ArgumentNullException("keys");
			}
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (values.Length < keys.Length)
			{
				throw new ArgumentException("Not all keys are covered by values", "values");
			}
			BasicList basicList = new BasicList();
			BasicList.Group group = null;
			for (int i = 0; i < keys.Length; i++)
			{
				if (i == 0 || keys[i] != keys[i - 1])
				{
					group = null;
				}
				if (group == null)
				{
					group = new BasicList.Group(keys[i]);
					basicList.Add(group);
				}
				group.Items.Add(values[i]);
			}
			return basicList;
		}

		// Token: 0x04000138 RID: 312
		private static readonly BasicList.Node nil = new BasicList.Node(null, 0);

		// Token: 0x04000139 RID: 313
		protected BasicList.Node head = BasicList.nil;

		// Token: 0x02000062 RID: 98
		public struct NodeEnumerator : IEnumerator
		{
			// Token: 0x060002DD RID: 733 RVA: 0x00003B8A File Offset: 0x00001D8A
			internal NodeEnumerator(BasicList.Node node)
			{
				this.position = -1;
				this.node = node;
			}

			// Token: 0x060002DE RID: 734 RVA: 0x00003B9A File Offset: 0x00001D9A
			void IEnumerator.Reset()
			{
				this.position = -1;
			}

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x060002DF RID: 735 RVA: 0x00003BA3 File Offset: 0x00001DA3
			public object Current
			{
				get
				{
					return this.node[this.position];
				}
			}

			// Token: 0x060002E0 RID: 736 RVA: 0x0000FF6C File Offset: 0x0000E16C
			public bool MoveNext()
			{
				int length = this.node.Length;
				if (this.position <= length)
				{
					int num = this.position + 1;
					this.position = num;
					return num < length;
				}
				return false;
			}

			// Token: 0x0400013A RID: 314
			private int position;

			// Token: 0x0400013B RID: 315
			private readonly BasicList.Node node;
		}

		// Token: 0x02000063 RID: 99
		internal sealed class Node
		{
			// Token: 0x170000C1 RID: 193
			public object this[int index]
			{
				get
				{
					if (index >= 0 && index < this.length)
					{
						return this.data[index];
					}
					throw new ArgumentOutOfRangeException("index");
				}
				set
				{
					if (index >= 0 && index < this.length)
					{
						this.data[index] = value;
						return;
					}
					throw new ArgumentOutOfRangeException("index");
				}
			}

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x060002E3 RID: 739 RVA: 0x00003BFB File Offset: 0x00001DFB
			public int Length
			{
				get
				{
					return this.length;
				}
			}

			// Token: 0x060002E4 RID: 740 RVA: 0x00003C03 File Offset: 0x00001E03
			internal Node(object[] data, int length)
			{
				this.data = data;
				this.length = length;
			}

			// Token: 0x060002E5 RID: 741 RVA: 0x00003C19 File Offset: 0x00001E19
			public void RemoveLastWithMutate()
			{
				if (this.length == 0)
				{
					throw new InvalidOperationException();
				}
				this.length--;
			}

			// Token: 0x060002E6 RID: 742 RVA: 0x0000FFA4 File Offset: 0x0000E1A4
			public BasicList.Node Append(object value)
			{
				int num = this.length + 1;
				object[] array;
				if (this.data == null)
				{
					array = new object[10];
				}
				else if (this.length == this.data.Length)
				{
					array = new object[this.data.Length * 2];
					Array.Copy(this.data, array, this.length);
				}
				else
				{
					array = this.data;
				}
				array[this.length] = value;
				return new BasicList.Node(array, num);
			}

			// Token: 0x060002E7 RID: 743 RVA: 0x00010018 File Offset: 0x0000E218
			public BasicList.Node Trim()
			{
				if (this.length == 0 || this.length == this.data.Length)
				{
					return this;
				}
				object[] destinationArray = new object[this.length];
				Array.Copy(this.data, destinationArray, this.length);
				return new BasicList.Node(destinationArray, this.length);
			}

			// Token: 0x060002E8 RID: 744 RVA: 0x0001006C File Offset: 0x0000E26C
			internal int IndexOfString(string value)
			{
				for (int i = 0; i < this.length; i++)
				{
					if (value == (string)this.data[i])
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x060002E9 RID: 745 RVA: 0x000100A4 File Offset: 0x0000E2A4
			internal int IndexOfReference(object instance)
			{
				for (int i = 0; i < this.length; i++)
				{
					if (instance == this.data[i])
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x060002EA RID: 746 RVA: 0x000100D0 File Offset: 0x0000E2D0
			internal int IndexOf(BasicList.MatchPredicate predicate, object ctx)
			{
				for (int i = 0; i < this.length; i++)
				{
					if (predicate(this.data[i], ctx))
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x060002EB RID: 747 RVA: 0x00003C37 File Offset: 0x00001E37
			internal void CopyTo(Array array, int offset)
			{
				if (this.length > 0)
				{
					Array.Copy(this.data, 0, array, offset, this.length);
				}
			}

			// Token: 0x060002EC RID: 748 RVA: 0x00003C56 File Offset: 0x00001E56
			internal void Clear()
			{
				if (this.data != null)
				{
					Array.Clear(this.data, 0, this.data.Length);
				}
				this.length = 0;
			}

			// Token: 0x0400013C RID: 316
			private readonly object[] data;

			// Token: 0x0400013D RID: 317
			private int length;
		}

		// Token: 0x02000064 RID: 100
		// (Invoke) Token: 0x060002EE RID: 750
		internal delegate bool MatchPredicate(object value, object ctx);

		// Token: 0x02000065 RID: 101
		internal sealed class Group
		{
			// Token: 0x060002F1 RID: 753 RVA: 0x00003C7B File Offset: 0x00001E7B
			public Group(int first)
			{
				this.First = first;
				this.Items = new BasicList();
			}

			// Token: 0x0400013E RID: 318
			public readonly int First;

			// Token: 0x0400013F RID: 319
			public readonly BasicList Items;
		}
	}
}
