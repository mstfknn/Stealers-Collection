using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000EB RID: 235
	public interface IPackageInfo<out TKey> : IPackageInfo
	{
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000740 RID: 1856
		TKey Key { get; }
	}
}
