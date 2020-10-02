using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	internal class NullBufferRecycler : IBufferRecycler
	{
		public void Return(IList<KeyValuePair<ArraySegment<byte>, IBufferState>> buffers, int offset, int length)
		{
		}
	}
}
