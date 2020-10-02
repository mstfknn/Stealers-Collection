using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000D4 RID: 212
	public class NullBufferManager : IBufferManager
	{
		// Token: 0x06000700 RID: 1792 RVA: 0x0000611C File Offset: 0x0000431C
		public byte[] GetBuffer(int size)
		{
			return new byte[size];
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00002596 File Offset: 0x00000796
		public void ReturnBuffer(byte[] buffer)
		{
		}
	}
}
