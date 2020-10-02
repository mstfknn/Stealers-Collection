using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000E4 RID: 228
	internal class NullBufferRecycler : IBufferRecycler
	{
		// Token: 0x06000734 RID: 1844 RVA: 0x00002596 File Offset: 0x00000796
		public void Return(IList<KeyValuePair<ArraySegment<byte>, IBufferState>> buffers, int offset, int length)
		{
		}
	}
}
