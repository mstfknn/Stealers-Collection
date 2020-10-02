using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000DF RID: 223
	public interface IHttpPackageInfo : IPackageInfo<string>, IPackageInfo
	{
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000729 RID: 1833
		HttpHeaderInfo Header { get; }
	}
}
