using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public interface IPipelineProcessor
	{
		BufferList Cache
		{
			get;
		}

		ProcessResult Process(ArraySegment<byte> segment);

		void Reset();
	}
}
