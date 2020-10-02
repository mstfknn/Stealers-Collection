using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000E2 RID: 226
	public interface IBufferedPackageInfo
	{
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000732 RID: 1842
		IList<ArraySegment<byte>> Data { get; }
	}
}
