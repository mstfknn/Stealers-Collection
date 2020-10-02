using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public abstract class TerminatorReceiveFilter<TPackageInfo> : IReceiveFilter<TPackageInfo>, IPackageResolver<TPackageInfo> where TPackageInfo : IPackageInfo
	{
		private readonly SearchMarkState<byte> m_SearchState;

		public static readonly TPackageInfo NullPackageInfo;

		protected SearchMarkState<byte> SearchState => m_SearchState;

		public IReceiveFilter<TPackageInfo> NextReceiveFilter
		{
			get;
			protected set;
		}

		public FilterState State
		{
			get;
			protected set;
		}

		protected TerminatorReceiveFilter(byte[] terminator)
		{
			m_SearchState = new SearchMarkState<byte>(terminator);
		}

		public TPackageInfo Filter(BufferList data, out int rest)
		{
			rest = 0;
			ArraySegment<byte> last = data.Last;
			int matched = m_SearchState.Matched;
			if (last.Array.SearchMark(last.Offset, last.Count, m_SearchState, out int parsedLength) < 0)
			{
				return NullPackageInfo;
			}
			rest = last.Count - parsedLength;
			if (rest > 0)
			{
				data.SetLastItemLength(parsedLength);
			}
			return ResolvePackage(this.GetBufferStream(data));
		}

		public abstract TPackageInfo ResolvePackage(IBufferStream bufferStream);

		public void Reset()
		{
			m_SearchState.Matched = 0;
			State = FilterState.Normal;
			NextReceiveFilter = null;
		}
	}
}
