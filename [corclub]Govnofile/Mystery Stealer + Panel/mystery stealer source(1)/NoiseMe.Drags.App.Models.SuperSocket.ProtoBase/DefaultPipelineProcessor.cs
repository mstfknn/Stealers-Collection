using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public class DefaultPipelineProcessor<TPackageInfo> : IPipelineProcessor where TPackageInfo : IPackageInfo
	{
		private IReceiveFilter<TPackageInfo> m_ReceiveFilter;

		private IReceiveFilter<TPackageInfo> m_FirstReceiveFilter;

		private BufferList m_ReceiveCache;

		private int m_MaxPackageLength;

		public BufferList Cache => m_ReceiveCache;

		public DefaultPipelineProcessor(IReceiveFilter<TPackageInfo> receiveFilter, int maxPackageLength = 0)
		{
			m_FirstReceiveFilter = (m_ReceiveFilter = receiveFilter);
			m_ReceiveCache = new BufferList();
			m_MaxPackageLength = maxPackageLength;
		}

		private void PushResetData(ArraySegment<byte> raw, int rest)
		{
			ArraySegment<byte> item = new ArraySegment<byte>(raw.Array, raw.Offset + raw.Count - rest, rest);
			m_ReceiveCache.Add(item);
		}

		private IList<IPackageInfo> GetNotNullOne(IList<IPackageInfo> left, IList<IPackageInfo> right)
		{
			if (left != null)
			{
				return left;
			}
			return right;
		}

		public virtual ProcessResult Process(ArraySegment<byte> segment)
		{
			BufferList bufferList = m_ReceiveCache;
			bufferList.Add(segment);
			int rest = 0;
			IReceiveFilter<TPackageInfo> receiveFilter = m_ReceiveFilter;
			SingleItemList<IPackageInfo> singleItemList = null;
			List<IPackageInfo> list = null;
			while (true)
			{
				int count = bufferList.Last.Count;
				TPackageInfo val = receiveFilter.Filter(bufferList, out rest);
				if (receiveFilter.State == FilterState.Error)
				{
					return ProcessResult.Create(ProcessState.Error);
				}
				if (m_MaxPackageLength > 0)
				{
					int total = bufferList.Total;
					if (total > m_MaxPackageLength)
					{
						return ProcessResult.Create(ProcessState.Error, $"Max package length: {m_MaxPackageLength}, current processed length: {total}");
					}
				}
				IReceiveFilter<TPackageInfo> nextReceiveFilter = receiveFilter.NextReceiveFilter;
				if (val != null)
				{
					receiveFilter.Reset();
				}
				if (nextReceiveFilter != null)
				{
					receiveFilter = (m_ReceiveFilter = nextReceiveFilter);
				}
				if (val == null)
				{
					if (rest > 0)
					{
						ArraySegment<byte> last = bufferList.Last;
						if (rest != count)
						{
							PushResetData(segment, rest);
						}
						continue;
					}
					return ProcessResult.Create(ProcessState.Cached, GetNotNullOne(list, singleItemList));
				}
				if (list != null)
				{
					list.Add(val);
				}
				else if (singleItemList == null)
				{
					singleItemList = new SingleItemList<IPackageInfo>(val);
				}
				else
				{
					if (list == null)
					{
						list = new List<IPackageInfo>();
					}
					list.Add(singleItemList[0]);
					list.Add(val);
					singleItemList = null;
				}
				if (val is IBufferedPackageInfo && (val as IBufferedPackageInfo).Data is BufferList)
				{
					bufferList = (m_ReceiveCache = new BufferList());
					if (rest <= 0)
					{
						return ProcessResult.Create(ProcessState.Cached, GetNotNullOne(list, singleItemList));
					}
				}
				else
				{
					m_ReceiveCache.Clear();
					if (rest <= 0)
					{
						break;
					}
				}
				PushResetData(segment, rest);
			}
			return ProcessResult.Create(ProcessState.Completed, GetNotNullOne(list, singleItemList));
		}

		public void Reset()
		{
			m_ReceiveCache.Clear();
			m_FirstReceiveFilter.Reset();
			if (m_ReceiveFilter != m_FirstReceiveFilter)
			{
				m_ReceiveFilter = m_FirstReceiveFilter;
			}
		}
	}
}
