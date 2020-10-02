namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public interface IBufferManager
	{
		byte[] GetBuffer(int size);

		void ReturnBuffer(byte[] buffer);
	}
}
