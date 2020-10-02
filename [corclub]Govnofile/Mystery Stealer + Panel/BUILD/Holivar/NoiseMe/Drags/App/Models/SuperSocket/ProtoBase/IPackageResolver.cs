using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000EC RID: 236
	public interface IPackageResolver<out TPackageInfo> where TPackageInfo : IPackageInfo
	{
		// Token: 0x06000741 RID: 1857
		TPackageInfo ResolvePackage(IBufferStream bufferStream);
	}
}
