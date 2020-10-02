using System;

namespace NoiseMe.Drags.App.Data.SQLite
{
	// Token: 0x0200017A RID: 378
	public struct ROW
	{
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x00009677 File Offset: 0x00007877
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x0000967F File Offset: 0x0000787F
		public long ID { get; set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x00009688 File Offset: 0x00007888
		// (set) Token: 0x06000C37 RID: 3127 RVA: 0x00009690 File Offset: 0x00007890
		public string[] RowData { get; set; }
	}
}
