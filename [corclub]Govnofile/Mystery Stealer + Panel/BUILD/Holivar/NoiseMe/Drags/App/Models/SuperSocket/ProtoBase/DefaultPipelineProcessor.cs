using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000D6 RID: 214
	public class DefaultPipelineProcessor<TPackageInfo> : IPipelineProcessor where TPackageInfo : IPackageInfo
	{
		// Token: 0x06000703 RID: 1795 RVA: 0x0001B534 File Offset: 0x00019734
		public DefaultPipelineProcessor(IReceiveFilter<TPackageInfo> receiveFilter, int maxPackageLength = 0)
		{
			this.m_ReceiveFilter = receiveFilter;
			this.m_FirstReceiveFilter = receiveFilter;
			this.m_ReceiveCache = new BufferList();
			this.m_MaxPackageLength = maxPackageLength;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001B56C File Offset: 0x0001976C
		private void PushResetData(ArraySegment<byte> raw, int rest)
		{
			ArraySegment<byte> item = new ArraySegment<byte>(raw.Array, raw.Offset + raw.Count - rest, rest);
			this.m_ReceiveCache.Add(item);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00006124 File Offset: 0x00004324
		private IList<IPackageInfo> GetNotNullOne(IList<IPackageInfo> left, IList<IPackageInfo> right)
		{
			if (left != null)
			{
				return left;
			}
			return right;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001B5A8 File Offset: 0x000197A8
		public virtual ProcessResult Process(ArraySegment<byte> segment)
		{
			BufferList bufferList = this.m_ReceiveCache;
			bufferList.Add(segment);
			int num = 0;
			IReceiveFilter<TPackageInfo> receiveFilter = this.m_ReceiveFilter;
			SingleItemList<IPackageInfo> singleItemList = null;
			List<IPackageInfo> list = null;
			int total;
			for (;;)
			{
				int count = bufferList.Last.Count;
				TPackageInfo tpackageInfo = receiveFilter.Filter(bufferList, out num);
				if (receiveFilter.State == FilterState.Error)
				{
					break;
				}
				if (this.m_MaxPackageLength > 0)
				{
					total = bufferList.Total;
					if (total > this.m_MaxPackageLength)
					{
						goto Block_3;
					}
				}
				IReceiveFilter<TPackageInfo> nextReceiveFilter = receiveFilter.NextReceiveFilter;
				if (tpackageInfo != null)
				{
					receiveFilter.Reset();
				}
				if (nextReceiveFilter != null)
				{
					receiveFilter = nextReceiveFilter;
					this.m_ReceiveFilter = receiveFilter;
				}
				if (tpackageInfo == null)
				{
					if (num <= 0)
					{
						goto IL_D4;
					}
					ArraySegment<byte> last = bufferList.Last;
					if (num != count)
					{
						this.PushResetData(segment, num);
					}
				}
				else
				{
					if (list != null)
					{
						list.Add(tpackageInfo);
					}
					else if (singleItemList == null)
					{
						singleItemList = new SingleItemList<IPackageInfo>(tpackageInfo);
					}
					else
					{
						if (list == null)
						{
							list = new List<IPackageInfo>();
						}
						list.Add(singleItemList[0]);
						list.Add(tpackageInfo);
						singleItemList = null;
					}
					if (tpackageInfo is IBufferedPackageInfo && (tpackageInfo as IBufferedPackageInfo).Data is BufferList)
					{
						bufferList = (this.m_ReceiveCache = new BufferList());
						if (num <= 0)
						{
							goto Block_14;
						}
					}
					else
					{
						this.m_ReceiveCache.Clear();
						if (num <= 0)
						{
							goto Block_15;
						}
					}
					this.PushResetData(segment, num);
				}
			}
			return ProcessResult.Create(ProcessState.Error);
			Block_3:
			return ProcessResult.Create(ProcessState.Error, string.Format("Max package length: {0}, current processed length: {1}", this.m_MaxPackageLength, total));
			IL_D4:
			return ProcessResult.Create(ProcessState.Cached, this.GetNotNullOne(list, singleItemList));
			Block_14:
			return ProcessResult.Create(ProcessState.Cached, this.GetNotNullOne(list, singleItemList));
			Block_15:
			return ProcessResult.Create(ProcessState.Completed, this.GetNotNullOne(list, singleItemList));
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0000612C File Offset: 0x0000432C
		public void Reset()
		{
			this.m_ReceiveCache.Clear();
			this.m_FirstReceiveFilter.Reset();
			if (this.m_ReceiveFilter != this.m_FirstReceiveFilter)
			{
				this.m_ReceiveFilter = this.m_FirstReceiveFilter;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0000615E File Offset: 0x0000435E
		public BufferList Cache
		{
			get
			{
				return this.m_ReceiveCache;
			}
		}

		// Token: 0x040002E3 RID: 739
		private IReceiveFilter<TPackageInfo> m_ReceiveFilter;

		// Token: 0x040002E4 RID: 740
		private IReceiveFilter<TPackageInfo> m_FirstReceiveFilter;

		// Token: 0x040002E5 RID: 741
		private BufferList m_ReceiveCache;

		// Token: 0x040002E6 RID: 742
		private int m_MaxPackageLength;
	}
}
