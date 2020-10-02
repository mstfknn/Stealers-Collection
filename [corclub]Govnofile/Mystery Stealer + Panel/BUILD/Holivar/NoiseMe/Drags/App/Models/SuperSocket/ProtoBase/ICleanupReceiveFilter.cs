using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000F0 RID: 240
	public interface ICleanupReceiveFilter<out TPackageInfo>
	{
		// Token: 0x0600074C RID: 1868
		TPackageInfo ResolvePackage(BufferList data);
	}
}
