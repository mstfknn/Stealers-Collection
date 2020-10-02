using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000F9 RID: 249
	public abstract class FixedHeaderReceiveFilter<TPackageInfo> : FixedSizeReceiveFilter<TPackageInfo> where TPackageInfo : IPackageInfo
	{
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0000645C File Offset: 0x0000465C
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x00006464 File Offset: 0x00004664
		public int HeaderSize { get; private set; }

		// Token: 0x06000778 RID: 1912 RVA: 0x0000646D File Offset: 0x0000466D
		protected FixedHeaderReceiveFilter(int headerSize) : base(headerSize)
		{
			this.HeaderSize = headerSize;
		}

		// Token: 0x06000779 RID: 1913
		protected abstract int GetBodyLengthFromHeader(IBufferStream bufferStream, int length);

		// Token: 0x0600077A RID: 1914 RVA: 0x0001BE04 File Offset: 0x0001A004
		protected override bool CanResolvePackage(IBufferStream bufferStream)
		{
			if (this.m_FoundHeader)
			{
				return true;
			}
			int bodyLengthFromHeader = this.GetBodyLengthFromHeader(bufferStream, this.HeaderSize);
			if (bodyLengthFromHeader < 0)
			{
				base.State = FilterState.Error;
				return false;
			}
			if (bodyLengthFromHeader == 0)
			{
				this.m_FoundHeader = true;
				return true;
			}
			base.ResetSize(this.HeaderSize + bodyLengthFromHeader);
			this.m_FoundHeader = true;
			return false;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0000647D File Offset: 0x0000467D
		public override void Reset()
		{
			this.m_FoundHeader = false;
			base.Reset();
		}

		// Token: 0x0400030E RID: 782
		private bool m_FoundHeader;
	}
}
