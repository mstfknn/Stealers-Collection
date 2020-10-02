using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000F3 RID: 243
	public class PackageInfo<TKey, TBody> : IPackageInfo<TKey>, IPackageInfo
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x000062CD File Offset: 0x000044CD
		// (set) Token: 0x06000750 RID: 1872 RVA: 0x000062D5 File Offset: 0x000044D5
		public TKey Key { get; private set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x000062DE File Offset: 0x000044DE
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x000062E6 File Offset: 0x000044E6
		public TBody Body { get; private set; }

		// Token: 0x06000753 RID: 1875 RVA: 0x000062EF File Offset: 0x000044EF
		public PackageInfo(TKey key, TBody body)
		{
			this.Key = key;
			this.Body = body;
		}
	}
}
