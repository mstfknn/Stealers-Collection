using System;
using System.Threading;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000E6 RID: 230
	public abstract class BufferBaseState : IBufferState
	{
		// Token: 0x06000738 RID: 1848 RVA: 0x000062B0 File Offset: 0x000044B0
		public int DecreaseReference()
		{
			return Interlocked.Decrement(ref this.m_Reference) - 1;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x000062BF File Offset: 0x000044BF
		public void IncreaseReference()
		{
			Interlocked.Increment(ref this.m_Reference);
		}

		// Token: 0x040002F4 RID: 756
		private int m_Reference;
	}
}
