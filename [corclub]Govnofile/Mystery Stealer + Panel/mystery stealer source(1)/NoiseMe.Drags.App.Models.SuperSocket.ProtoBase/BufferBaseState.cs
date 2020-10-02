using System.Threading;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public abstract class BufferBaseState : IBufferState
	{
		private int m_Reference;

		public int DecreaseReference()
		{
			return Interlocked.Decrement(ref m_Reference) - 1;
		}

		public void IncreaseReference()
		{
			Interlocked.Increment(ref m_Reference);
		}
	}
}
