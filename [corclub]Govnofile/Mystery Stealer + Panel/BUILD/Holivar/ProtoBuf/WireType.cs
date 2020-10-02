using System;

namespace ProtoBuf
{
	// Token: 0x02000034 RID: 52
	public enum WireType
	{
		// Token: 0x040000D0 RID: 208
		None = -1,
		// Token: 0x040000D1 RID: 209
		Variant,
		// Token: 0x040000D2 RID: 210
		Fixed64,
		// Token: 0x040000D3 RID: 211
		String,
		// Token: 0x040000D4 RID: 212
		StartGroup,
		// Token: 0x040000D5 RID: 213
		EndGroup,
		// Token: 0x040000D6 RID: 214
		Fixed32,
		// Token: 0x040000D7 RID: 215
		SignedVariant = 8
	}
}
