using System;

namespace NoiseMe.Drags.App.Data.SQLite
{
	// Token: 0x0200017C RID: 380
	public struct FF
	{
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x000096BB File Offset: 0x000078BB
		// (set) Token: 0x06000C3D RID: 3133 RVA: 0x000096C3 File Offset: 0x000078C3
		public long ID { get; set; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x000096CC File Offset: 0x000078CC
		// (set) Token: 0x06000C3F RID: 3135 RVA: 0x000096D4 File Offset: 0x000078D4
		public string Type { get; set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x000096DD File Offset: 0x000078DD
		// (set) Token: 0x06000C41 RID: 3137 RVA: 0x000096E5 File Offset: 0x000078E5
		public string Name { get; set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x000096EE File Offset: 0x000078EE
		// (set) Token: 0x06000C43 RID: 3139 RVA: 0x000096F6 File Offset: 0x000078F6
		public string AstableName { get; set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x000096FF File Offset: 0x000078FF
		// (set) Token: 0x06000C45 RID: 3141 RVA: 0x00009707 File Offset: 0x00007907
		public long RootNum { get; set; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x00009710 File Offset: 0x00007910
		// (set) Token: 0x06000C47 RID: 3143 RVA: 0x00009718 File Offset: 0x00007918
		public string SqlStatement { get; set; }
	}
}
