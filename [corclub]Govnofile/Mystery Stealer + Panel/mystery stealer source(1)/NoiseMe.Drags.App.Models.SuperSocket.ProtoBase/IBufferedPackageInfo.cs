using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public interface IBufferedPackageInfo
	{
		IList<ArraySegment<byte>> Data
		{
			get;
		}
	}
}
