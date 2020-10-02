using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000E8 RID: 232
	public interface IOutputBuffer
	{
		// Token: 0x0600073D RID: 1853
		void Add(ArraySegment<byte> item);

		// Token: 0x0600073E RID: 1854
		void AddRange(IList<ArraySegment<byte>> items);
	}
}
