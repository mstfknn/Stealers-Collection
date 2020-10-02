using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x02000115 RID: 277
	public class DataEventArgs : EventArgs
	{
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x00006CC1 File Offset: 0x00004EC1
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x00006CC9 File Offset: 0x00004EC9
		public byte[] Data { get; set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x00006CD2 File Offset: 0x00004ED2
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x00006CDA File Offset: 0x00004EDA
		public int Offset { get; set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x00006CE3 File Offset: 0x00004EE3
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x00006CEB File Offset: 0x00004EEB
		public int Length { get; set; }
	}
}
