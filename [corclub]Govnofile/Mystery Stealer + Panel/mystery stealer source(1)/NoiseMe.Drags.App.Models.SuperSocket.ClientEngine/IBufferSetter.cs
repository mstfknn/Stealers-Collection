using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	public interface IBufferSetter
	{
		void SetBuffer(ArraySegment<byte> bufferSegment);
	}
}
