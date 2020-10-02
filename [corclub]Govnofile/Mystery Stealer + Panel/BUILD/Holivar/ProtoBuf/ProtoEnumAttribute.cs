using System;

namespace ProtoBuf
{
	// Token: 0x02000024 RID: 36
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public sealed class ProtoEnumAttribute : Attribute
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000027BE File Offset: 0x000009BE
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x000027C6 File Offset: 0x000009C6
		public int Value
		{
			get
			{
				return this.enumValue;
			}
			set
			{
				this.enumValue = value;
				this.hasValue = true;
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000027D6 File Offset: 0x000009D6
		public bool HasValue()
		{
			return this.hasValue;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000027DE File Offset: 0x000009DE
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x000027E6 File Offset: 0x000009E6
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x0400008D RID: 141
		private bool hasValue;

		// Token: 0x0400008E RID: 142
		private int enumValue;

		// Token: 0x0400008F RID: 143
		private string name;
	}
}
