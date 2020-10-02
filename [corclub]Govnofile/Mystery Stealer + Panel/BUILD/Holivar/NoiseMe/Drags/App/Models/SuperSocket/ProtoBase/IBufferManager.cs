using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000D3 RID: 211
	public interface IBufferManager
	{
		// Token: 0x060006FE RID: 1790
		byte[] GetBuffer(int size);

		// Token: 0x060006FF RID: 1791
		void ReturnBuffer(byte[] buffer);
	}
}
