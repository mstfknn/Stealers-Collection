using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public class BufferedPackageInfo<TKey> : IPackageInfo<TKey>, IPackageInfo, IBufferedPackageInfo
	{
		public TKey Key
		{
			get;
			private set;
		}

		public IList<ArraySegment<byte>> Data
		{
			get;
			private set;
		}

		public BufferedPackageInfo(TKey key, IList<ArraySegment<byte>> data)
		{
			Key = key;
			Data = data;
		}
	}
	public class BufferedPackageInfo : BufferedPackageInfo<string>
	{
		public BufferedPackageInfo(string key, IList<ArraySegment<byte>> data)
			: base(key, data)
		{
		}
	}
}
