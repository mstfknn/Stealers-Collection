using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000FD RID: 253
	public abstract class TerminatorReceiveFilter<TPackageInfo> : IReceiveFilter<TPackageInfo>, IPackageResolver<TPackageInfo> where TPackageInfo : IPackageInfo
	{
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x00006580 File Offset: 0x00004780
		protected SearchMarkState<byte> SearchState
		{
			get
			{
				return this.m_SearchState;
			}
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00006588 File Offset: 0x00004788
		protected TerminatorReceiveFilter(byte[] terminator)
		{
			this.m_SearchState = new SearchMarkState<byte>(terminator);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001C1E0 File Offset: 0x0001A3E0
		public TPackageInfo Filter(BufferList data, out int rest)
		{
			rest = 0;
			ArraySegment<byte> last = data.Last;
			int matched = this.m_SearchState.Matched;
			int num;
			if (last.Array.SearchMark(last.Offset, last.Count, this.m_SearchState, out num) < 0)
			{
				return TerminatorReceiveFilter<TPackageInfo>.NullPackageInfo;
			}
			rest = last.Count - num;
			if (rest > 0)
			{
				data.SetLastItemLength(num);
			}
			return this.ResolvePackage(this.GetBufferStream(data));
		}

		// Token: 0x0600079D RID: 1949
		public abstract TPackageInfo ResolvePackage(IBufferStream bufferStream);

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0000659C File Offset: 0x0000479C
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x000065A4 File Offset: 0x000047A4
		public IReceiveFilter<TPackageInfo> NextReceiveFilter { get; protected set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x000065AD File Offset: 0x000047AD
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x000065B5 File Offset: 0x000047B5
		public FilterState State { get; protected set; }

		// Token: 0x060007A2 RID: 1954 RVA: 0x000065BE File Offset: 0x000047BE
		public void Reset()
		{
			this.m_SearchState.Matched = 0;
			this.State = FilterState.Normal;
			this.NextReceiveFilter = null;
		}

		// Token: 0x04000316 RID: 790
		private readonly SearchMarkState<byte> m_SearchState;

		// Token: 0x04000317 RID: 791
		public static readonly TPackageInfo NullPackageInfo;
	}
}
