using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000E3 RID: 227
	public interface IBufferRecycler
	{
		// Token: 0x06000733 RID: 1843
		void Return(IList<KeyValuePair<ArraySegment<byte>, IBufferState>> buffers, int offset, int length);
	}
}
