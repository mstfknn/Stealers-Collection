using System;

namespace GrandSteal.Client.Data.SQLite
{
	// Token: 0x02000006 RID: 6
	public struct MasterEntry
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002188 File Offset: 0x00000388
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002190 File Offset: 0x00000390
		public long RowID { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002199 File Offset: 0x00000399
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000021A1 File Offset: 0x000003A1
		public string ItemType { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000021AA File Offset: 0x000003AA
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000021B2 File Offset: 0x000003B2
		public string ItemName { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000021BB File Offset: 0x000003BB
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000021C3 File Offset: 0x000003C3
		public string AstableName { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000021CC File Offset: 0x000003CC
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000021D4 File Offset: 0x000003D4
		public long RootNum { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000021DD File Offset: 0x000003DD
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000021E5 File Offset: 0x000003E5
		public string SqlStatement { get; set; }
	}
}
