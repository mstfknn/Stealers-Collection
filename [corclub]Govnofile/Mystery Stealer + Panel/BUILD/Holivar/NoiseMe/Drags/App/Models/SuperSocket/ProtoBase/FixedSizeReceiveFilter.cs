using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000FA RID: 250
	public abstract class FixedSizeReceiveFilter<TPackageInfo> : IReceiveFilter<TPackageInfo>, IPackageResolver<TPackageInfo> where TPackageInfo : IPackageInfo
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x0000648C File Offset: 0x0000468C
		protected int Size
		{
			get
			{
				return this.m_Size;
			}
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00006494 File Offset: 0x00004694
		public FixedSizeReceiveFilter(int size)
		{
			this.m_OriginalSize = size;
			this.m_Size = size;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0001BE58 File Offset: 0x0001A058
		public virtual TPackageInfo Filter(BufferList data, out int rest)
		{
			rest = 0;
			int total = data.Total;
			if (total < this.m_Size)
			{
				return default(TPackageInfo);
			}
			if (total > this.m_Size)
			{
				rest = total - this.m_Size;
				data.SetLastItemLength(data.Last.Count - rest);
			}
			BufferStream bufferStream = this.GetBufferStream(data);
			if (!this.CanResolvePackage(bufferStream))
			{
				return default(TPackageInfo);
			}
			return this.ResolvePackage(bufferStream);
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x000064AA File Offset: 0x000046AA
		// (set) Token: 0x06000780 RID: 1920 RVA: 0x000064B2 File Offset: 0x000046B2
		public IReceiveFilter<TPackageInfo> NextReceiveFilter { get; protected set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x000064BB File Offset: 0x000046BB
		// (set) Token: 0x06000782 RID: 1922 RVA: 0x000064C3 File Offset: 0x000046C3
		public FilterState State { get; protected set; }

		// Token: 0x06000783 RID: 1923 RVA: 0x000064CC File Offset: 0x000046CC
		protected void ResetSize(int newSize)
		{
			this.m_Size = newSize;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x000064D5 File Offset: 0x000046D5
		public virtual void Reset()
		{
			if (this.m_Size != this.m_OriginalSize)
			{
				this.m_Size = this.m_OriginalSize;
			}
			this.NextReceiveFilter = null;
			this.State = FilterState.Normal;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00003147 File Offset: 0x00001347
		protected virtual bool CanResolvePackage(IBufferStream bufferStream)
		{
			return true;
		}

		// Token: 0x06000786 RID: 1926
		public abstract TPackageInfo ResolvePackage(IBufferStream bufferStream);

		// Token: 0x04000310 RID: 784
		private int m_OriginalSize;

		// Token: 0x04000311 RID: 785
		private int m_Size;
	}
}
