using System;
using NoiseMe.Drags.App.Models.SuperSocket.ProtoBase;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x02000121 RID: 289
	public class PackageEventArgs<TPackageInfo> : EventArgs where TPackageInfo : IPackageInfo
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00006FEE File Offset: 0x000051EE
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x00006FF6 File Offset: 0x000051F6
		public TPackageInfo Package { get; private set; }

		// Token: 0x06000905 RID: 2309 RVA: 0x00006FFF File Offset: 0x000051FF
		public PackageEventArgs(TPackageInfo package)
		{
			this.Package = package;
		}
	}
}
