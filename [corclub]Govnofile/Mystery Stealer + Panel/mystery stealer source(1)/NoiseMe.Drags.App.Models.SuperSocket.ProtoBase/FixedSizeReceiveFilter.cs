namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public abstract class FixedSizeReceiveFilter<TPackageInfo> : IReceiveFilter<TPackageInfo>, IPackageResolver<TPackageInfo> where TPackageInfo : IPackageInfo
	{
		private int m_OriginalSize;

		private int m_Size;

		protected int Size => m_Size;

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

		public FixedSizeReceiveFilter(int size)
		{
			m_OriginalSize = size;
			m_Size = size;
		}

		public virtual TPackageInfo Filter(BufferList data, out int rest)
		{
			rest = 0;
			int total = data.Total;
			if (total < m_Size)
			{
				return default(TPackageInfo);
			}
			if (total > m_Size)
			{
				rest = total - m_Size;
				data.SetLastItemLength(data.Last.Count - rest);
			}
			BufferStream bufferStream = this.GetBufferStream(data);
			if (!CanResolvePackage(bufferStream))
			{
				return default(TPackageInfo);
			}
			return ResolvePackage(bufferStream);
		}

		protected void ResetSize(int newSize)
		{
			m_Size = newSize;
		}

		public virtual void Reset()
		{
			if (m_Size != m_OriginalSize)
			{
				m_Size = m_OriginalSize;
			}
			NextReceiveFilter = null;
			State = FilterState.Normal;
		}

		protected virtual bool CanResolvePackage(IBufferStream bufferStream)
		{
			return true;
		}

		public abstract TPackageInfo ResolvePackage(IBufferStream bufferStream);
	}
}
