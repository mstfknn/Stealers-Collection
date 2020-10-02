using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000FB RID: 251
	public class SearchMarkState<T> where T : IEquatable<T>
	{
		// Token: 0x06000787 RID: 1927 RVA: 0x000064FF File Offset: 0x000046FF
		public SearchMarkState(T[] mark)
		{
			this.Mark = mark;
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x0000650E File Offset: 0x0000470E
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x00006516 File Offset: 0x00004716
		public T[] Mark { get; private set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x0000651F File Offset: 0x0000471F
		// (set) Token: 0x0600078B RID: 1931 RVA: 0x00006527 File Offset: 0x00004727
		public int Matched { get; set; }

		// Token: 0x0600078C RID: 1932 RVA: 0x00006530 File Offset: 0x00004730
		public void Change(T[] mark)
		{
			this.Mark = mark;
			this.Matched = 0;
		}
	}
}
