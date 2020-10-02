using System;

namespace Evrial.Cookies
{
	// Token: 0x0200001D RID: 29
	public class Cookie
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000047A8 File Offset: 0x000029A8
		// (set) Token: 0x06000046 RID: 70 RVA: 0x0000479C File Offset: 0x0000299C
		public string domain { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000047BC File Offset: 0x000029BC
		// (set) Token: 0x06000048 RID: 72 RVA: 0x000047B0 File Offset: 0x000029B0
		public string expirationDate { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000047D0 File Offset: 0x000029D0
		// (set) Token: 0x0600004A RID: 74 RVA: 0x000047C4 File Offset: 0x000029C4
		public string hostOnly { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000047E4 File Offset: 0x000029E4
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000047D8 File Offset: 0x000029D8
		public string name { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000047F8 File Offset: 0x000029F8
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000047EC File Offset: 0x000029EC
		public string path { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000051 RID: 81 RVA: 0x0000480C File Offset: 0x00002A0C
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00004800 File Offset: 0x00002A00
		public string secure { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00004820 File Offset: 0x00002A20
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00004814 File Offset: 0x00002A14
		public string value { get; set; }
	}
}
