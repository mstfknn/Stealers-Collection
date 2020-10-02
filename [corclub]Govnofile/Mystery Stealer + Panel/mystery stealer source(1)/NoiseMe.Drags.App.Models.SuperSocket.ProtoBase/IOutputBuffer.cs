using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public interface IOutputBuffer
	{
		void Add(ArraySegment<byte> item);

		void AddRange(IList<ArraySegment<byte>> items);
	}
}
