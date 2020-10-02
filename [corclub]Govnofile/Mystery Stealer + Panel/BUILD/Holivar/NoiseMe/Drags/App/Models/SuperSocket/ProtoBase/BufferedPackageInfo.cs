using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000CD RID: 205
	public class BufferedPackageInfo<TKey> : IPackageInfo<TKey>, IPackageInfo, IBufferedPackageInfo
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00005F34 File Offset: 0x00004134
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x00005F3C File Offset: 0x0000413C
		public TKey Key { get; private set; }

		// Token: 0x060006A4 RID: 1700 RVA: 0x00005F45 File Offset: 0x00004145
		public BufferedPackageInfo(TKey key, IList<ArraySegment<byte>> data)
		{
			this.Key = key;
			this.Data = data;
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00005F5B File Offset: 0x0000415B
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x00005F63 File Offset: 0x00004163
		public IList<ArraySegment<byte>> Data { get; private set; }
	}
}
