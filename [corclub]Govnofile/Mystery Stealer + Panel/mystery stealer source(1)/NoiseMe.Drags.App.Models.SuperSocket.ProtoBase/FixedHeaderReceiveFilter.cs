namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public abstract class FixedHeaderReceiveFilter<TPackageInfo> : FixedSizeReceiveFilter<TPackageInfo> where TPackageInfo : IPackageInfo
	{
		private bool m_FoundHeader;

		public int HeaderSize
		{
			get;
			private set;
		}

		protected FixedHeaderReceiveFilter(int headerSize)
			: base(headerSize)
		{
			HeaderSize = headerSize;
		}

		protected abstract int GetBodyLengthFromHeader(IBufferStream bufferStream, int length);

		protected override bool CanResolvePackage(IBufferStream bufferStream)
		{
			if (m_FoundHeader)
			{
				return true;
			}
			int bodyLengthFromHeader = GetBodyLengthFromHeader(bufferStream, HeaderSize);
			if (bodyLengthFromHeader < 0)
			{
				base.State = FilterState.Error;
				return false;
			}
			if (bodyLengthFromHeader == 0)
			{
				m_FoundHeader = true;
				return true;
			}
			ResetSize(HeaderSize + bodyLengthFromHeader);
			m_FoundHeader = true;
			return false;
		}

		public override void Reset()
		{
			m_FoundHeader = false;
			base.Reset();
		}
	}
}
