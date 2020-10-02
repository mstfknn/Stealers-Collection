using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000EF RID: 239
	public interface IReceiveFilter<out TPackageInfo> where TPackageInfo : IPackageInfo
	{
		// Token: 0x06000748 RID: 1864
		TPackageInfo Filter(BufferList data, out int rest);

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000749 RID: 1865
		IReceiveFilter<TPackageInfo> NextReceiveFilter { get; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600074A RID: 1866
		FilterState State { get; }

		// Token: 0x0600074B RID: 1867
		void Reset();
	}
}
