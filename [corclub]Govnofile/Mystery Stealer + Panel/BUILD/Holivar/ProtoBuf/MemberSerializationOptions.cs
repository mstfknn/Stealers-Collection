using System;

namespace ProtoBuf
{
	// Token: 0x0200002A RID: 42
	[Flags]
	public enum MemberSerializationOptions
	{
		// Token: 0x0400009B RID: 155
		None = 0,
		// Token: 0x0400009C RID: 156
		Packed = 1,
		// Token: 0x0400009D RID: 157
		Required = 2,
		// Token: 0x0400009E RID: 158
		AsReference = 4,
		// Token: 0x0400009F RID: 159
		DynamicType = 8,
		// Token: 0x040000A0 RID: 160
		OverwriteList = 16,
		// Token: 0x040000A1 RID: 161
		AsReferenceHasValue = 32
	}
}
