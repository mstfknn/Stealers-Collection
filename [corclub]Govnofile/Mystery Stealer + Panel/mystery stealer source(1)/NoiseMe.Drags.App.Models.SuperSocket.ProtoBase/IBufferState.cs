namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public interface IBufferState
	{
		int DecreaseReference();

		void IncreaseReference();
	}
}
