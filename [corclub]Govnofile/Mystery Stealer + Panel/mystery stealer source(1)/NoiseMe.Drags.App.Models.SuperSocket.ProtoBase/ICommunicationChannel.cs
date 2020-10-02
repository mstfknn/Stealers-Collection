using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public interface ICommunicationChannel
	{
		void Send(ArraySegment<byte> segment);

		void Close(CloseReason reason);
	}
}
