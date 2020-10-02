using System;

namespace ProtoBuf.Meta
{
	// Token: 0x02000060 RID: 96
	internal sealed class MutableList : BasicList
	{
		// Token: 0x170000BD RID: 189
		public new object this[int index]
		{
			get
			{
				return this.head[index];
			}
			set
			{
				this.head[index] = value;
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00003ACE File Offset: 0x00001CCE
		public void RemoveLast()
		{
			this.head.RemoveLastWithMutate();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00003ADB File Offset: 0x00001CDB
		public void Clear()
		{
			this.head.Clear();
		}
	}
}
