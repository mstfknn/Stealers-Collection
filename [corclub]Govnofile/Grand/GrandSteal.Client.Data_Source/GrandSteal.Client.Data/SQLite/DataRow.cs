using System;

namespace GrandSteal.Client.Data.SQLite
{
	// Token: 0x02000004 RID: 4
	public struct DataRow
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002144 File Offset: 0x00000344
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000214C File Offset: 0x0000034C
		public long RowID { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002155 File Offset: 0x00000355
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000215D File Offset: 0x0000035D
		public string[] Content { get; set; }
	}
}
