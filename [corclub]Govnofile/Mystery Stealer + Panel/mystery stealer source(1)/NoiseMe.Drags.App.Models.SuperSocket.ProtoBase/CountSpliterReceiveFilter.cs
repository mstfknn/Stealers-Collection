using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public abstract class CountSpliterReceiveFilter<TPackageInfo> : IReceiveFilter<TPackageInfo>, IPackageResolver<TPackageInfo> where TPackageInfo : IPackageInfo
	{
		private int m_SpliterFoundCount;

		private readonly SearchMarkState<byte> m_SpliterSearchState;

		private readonly int m_SpliterCount;

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

		protected CountSpliterReceiveFilter(byte[] spliter, int spliterCount)
		{
			m_SpliterSearchState = new SearchMarkState<byte>(spliter);
			m_SpliterCount = spliterCount;
		}

		public TPackageInfo Filter(BufferList data, out int rest)
		{
			rest = 0;
			ArraySegment<byte> last = data.Last;
			byte[] array = last.Array;
			int num = last.Offset;
			int num2 = last.Count;
			while (m_SpliterFoundCount < m_SpliterCount)
			{
				if (array.SearchMark(num, num2, m_SpliterSearchState, out int parsedLength) < 0)
				{
					return default(TPackageInfo);
				}
				m_SpliterFoundCount++;
				num += parsedLength;
				num2 -= parsedLength;
			}
			data.SetLastItemLength(num - last.Offset);
			Reset();
			rest = num2;
			return ResolvePackage(this.GetBufferStream(data));
		}

		public virtual void Reset()
		{
			m_SpliterFoundCount = 0;
			m_SpliterSearchState.Matched = 0;
		}

		public abstract TPackageInfo ResolvePackage(IBufferStream bufferStream);
	}
}
