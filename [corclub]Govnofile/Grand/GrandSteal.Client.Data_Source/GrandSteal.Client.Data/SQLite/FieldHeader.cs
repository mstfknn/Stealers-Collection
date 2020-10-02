using System;

namespace GrandSteal.Client.Data.SQLite
{
	// Token: 0x02000005 RID: 5
	public struct FieldHeader
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002166 File Offset: 0x00000366
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000216E File Offset: 0x0000036E
		public long Size { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002177 File Offset: 0x00000377
		// (set) Token: 0x06000024 RID: 36 RVA: 0x0000217F File Offset: 0x0000037F
		public long Type { get; set; }
	}
}
