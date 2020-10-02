using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000D8 RID: 216
	public interface IProtoDataEncoder
	{
		// Token: 0x0600070C RID: 1804
		void EncodeData(IOutputBuffer output, ArraySegment<byte> data);

		// Token: 0x0600070D RID: 1805
		void EncodeData(IOutputBuffer output, IList<ArraySegment<byte>> data);
	}
}
