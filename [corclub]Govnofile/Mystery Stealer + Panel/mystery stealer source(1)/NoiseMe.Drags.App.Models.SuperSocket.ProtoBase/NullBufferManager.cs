namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public class NullBufferManager : IBufferManager
	{
		public byte[] GetBuffer(int size)
		{
			return new byte[size];
		}

		public void ReturnBuffer(byte[] buffer)
		{
		}
	}
}
