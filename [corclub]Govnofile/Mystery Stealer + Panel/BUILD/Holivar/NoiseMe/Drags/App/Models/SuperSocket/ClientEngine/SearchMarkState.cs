using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x02000110 RID: 272
	public class SearchMarkState<T> where T : IEquatable<T>
	{
		// Token: 0x06000836 RID: 2102 RVA: 0x000069B8 File Offset: 0x00004BB8
		public SearchMarkState(T[] mark)
		{
			this.Mark = mark;
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x000069C7 File Offset: 0x00004BC7
		// (set) Token: 0x06000838 RID: 2104 RVA: 0x000069CF File Offset: 0x00004BCF
		public T[] Mark { get; private set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x000069D8 File Offset: 0x00004BD8
		// (set) Token: 0x0600083A RID: 2106 RVA: 0x000069E0 File Offset: 0x00004BE0
		public int Matched { get; set; }
	}
}
