using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000F7 RID: 247
	public abstract class BeginEndMarkReceiveFilter<TPackageInfo> : IReceiveFilter<TPackageInfo>, IPackageResolver<TPackageInfo> where TPackageInfo : IPackageInfo
	{
		// Token: 0x06000763 RID: 1891 RVA: 0x00006361 File Offset: 0x00004561
		public BeginEndMarkReceiveFilter(byte[] beginMark, byte[] endMark)
		{
			this.m_BeginSearchState = new SearchMarkState<byte>(beginMark);
			this.m_EndSearchState = new SearchMarkState<byte>(endMark);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001BBC4 File Offset: 0x00019DC4
		private bool CheckChanged(byte[] oldMark, byte[] newMark)
		{
			if (oldMark.Length != newMark.Length)
			{
				return true;
			}
			for (int i = 0; i < oldMark.Length; i++)
			{
				if (oldMark[i] != newMark[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00006381 File Offset: 0x00004581
		public void ChangeBeginMark(byte[] beginMark)
		{
			if (!this.CheckChanged(this.m_BeginSearchState.Mark, beginMark))
			{
				return;
			}
			this.m_BeginSearchState.Change(beginMark);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x000063A4 File Offset: 0x000045A4
		public void ChangeEndMark(byte[] endMark)
		{
			if (!this.CheckChanged(this.m_EndSearchState.Mark, endMark))
			{
				return;
			}
			this.m_EndSearchState.Change(endMark);
		}

		// Token: 0x06000767 RID: 1895
		public abstract TPackageInfo ResolvePackage(IBufferStream bufferStream);

		// Token: 0x06000768 RID: 1896 RVA: 0x0001BBF4 File Offset: 0x00019DF4
		public virtual TPackageInfo Filter(BufferList data, out int rest)
		{
			rest = 0;
			ArraySegment<byte> last = data.Last;
			byte[] array = last.Array;
			int offset = last.Offset;
			int count = last.Count;
			int num = 0;
			int num3;
			int length;
			if (!this.m_FoundBegin)
			{
				int num2 = array.SearchMark(offset, count, this.m_BeginSearchState, out num);
				if (num2 < 0)
				{
					if (this.m_BeginSearchState.Matched > 0 && data.Total == this.m_BeginSearchState.Matched)
					{
						return default(TPackageInfo);
					}
					this.State = FilterState.Error;
					return default(TPackageInfo);
				}
				else
				{
					if (num2 != offset)
					{
						this.State = FilterState.Error;
						return default(TPackageInfo);
					}
					this.m_FoundBegin = true;
					num3 = offset + num;
					if (offset + count <= num3)
					{
						return default(TPackageInfo);
					}
					length = offset + count - num3;
				}
			}
			else
			{
				num3 = offset;
				length = count;
			}
			TPackageInfo tpackageInfo;
			for (;;)
			{
				int num5;
				int num4 = array.SearchMark(num3, length, this.m_EndSearchState, out num5);
				if (num4 < 0)
				{
					break;
				}
				num += num5;
				rest = count - num;
				if (rest > 0)
				{
					data.SetLastItemLength(num);
				}
				tpackageInfo = this.ResolvePackage(this.GetBufferStream(data));
				if (tpackageInfo != default(TPackageInfo))
				{
					goto Block_9;
				}
				if (rest <= 0)
				{
					goto IL_15C;
				}
				num3 = num4 + this.m_EndSearchState.Mark.Length;
				length = rest;
			}
			return default(TPackageInfo);
			Block_9:
			this.Reset();
			return tpackageInfo;
			IL_15C:
			return default(TPackageInfo);
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x000063C7 File Offset: 0x000045C7
		// (set) Token: 0x0600076A RID: 1898 RVA: 0x000063CF File Offset: 0x000045CF
		public IReceiveFilter<TPackageInfo> NextReceiveFilter { get; protected set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x000063D8 File Offset: 0x000045D8
		// (set) Token: 0x0600076C RID: 1900 RVA: 0x000063E0 File Offset: 0x000045E0
		public FilterState State { get; protected set; }

		// Token: 0x0600076D RID: 1901 RVA: 0x000063E9 File Offset: 0x000045E9
		public void Reset()
		{
			this.m_BeginSearchState.Matched = 0;
			this.m_EndSearchState.Matched = 0;
			this.m_FoundBegin = false;
		}

		// Token: 0x04000304 RID: 772
		private readonly SearchMarkState<byte> m_BeginSearchState;

		// Token: 0x04000305 RID: 773
		private readonly SearchMarkState<byte> m_EndSearchState;

		// Token: 0x04000306 RID: 774
		private bool m_FoundBegin;
	}
}
