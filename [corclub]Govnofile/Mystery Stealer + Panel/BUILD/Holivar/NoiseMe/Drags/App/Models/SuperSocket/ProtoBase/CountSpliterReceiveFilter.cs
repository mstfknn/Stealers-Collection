using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000F8 RID: 248
	public abstract class CountSpliterReceiveFilter<TPackageInfo> : IReceiveFilter<TPackageInfo>, IPackageResolver<TPackageInfo> where TPackageInfo : IPackageInfo
	{
		// Token: 0x0600076E RID: 1902 RVA: 0x0000640A File Offset: 0x0000460A
		protected CountSpliterReceiveFilter(byte[] spliter, int spliterCount)
		{
			this.m_SpliterSearchState = new SearchMarkState<byte>(spliter);
			this.m_SpliterCount = spliterCount;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001BD68 File Offset: 0x00019F68
		public TPackageInfo Filter(BufferList data, out int rest)
		{
			rest = 0;
			ArraySegment<byte> last = data.Last;
			byte[] array = last.Array;
			int num = last.Offset;
			int num2 = last.Count;
			while (this.m_SpliterFoundCount < this.m_SpliterCount)
			{
				int num3;
				if (array.SearchMark(num, num2, this.m_SpliterSearchState, out num3) < 0)
				{
					return default(TPackageInfo);
				}
				this.m_SpliterFoundCount++;
				num += num3;
				num2 -= num3;
			}
			data.SetLastItemLength(num - last.Offset);
			this.Reset();
			rest = num2;
			return this.ResolvePackage(this.GetBufferStream(data));
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x00006425 File Offset: 0x00004625
		// (set) Token: 0x06000771 RID: 1905 RVA: 0x0000642D File Offset: 0x0000462D
		public IReceiveFilter<TPackageInfo> NextReceiveFilter { get; protected set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x00006436 File Offset: 0x00004636
		// (set) Token: 0x06000773 RID: 1907 RVA: 0x0000643E File Offset: 0x0000463E
		public FilterState State { get; protected set; }

		// Token: 0x06000774 RID: 1908 RVA: 0x00006447 File Offset: 0x00004647
		public virtual void Reset()
		{
			this.m_SpliterFoundCount = 0;
			this.m_SpliterSearchState.Matched = 0;
		}

		// Token: 0x06000775 RID: 1909
		public abstract TPackageInfo ResolvePackage(IBufferStream bufferStream);

		// Token: 0x04000309 RID: 777
		private int m_SpliterFoundCount;

		// Token: 0x0400030A RID: 778
		private readonly SearchMarkState<byte> m_SpliterSearchState;

		// Token: 0x0400030B RID: 779
		private readonly int m_SpliterCount;
	}
}
