using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public interface IProtoDataEncoder
	{
		void EncodeData(IOutputBuffer output, ArraySegment<byte> data);

		void EncodeData(IOutputBuffer output, IList<ArraySegment<byte>> data);
	}
}
