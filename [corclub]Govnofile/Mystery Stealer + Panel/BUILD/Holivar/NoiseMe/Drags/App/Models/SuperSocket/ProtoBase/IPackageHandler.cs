using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000E9 RID: 233
	public interface IPackageHandler<TPackageInfo> where TPackageInfo : IPackageInfo
	{
		// Token: 0x0600073F RID: 1855
		void Handle(TPackageInfo package);
	}
}
