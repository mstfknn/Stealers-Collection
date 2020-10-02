using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public abstract class BeginEndMarkReceiveFilter<TPackageInfo> : IReceiveFilter<TPackageInfo>, IPackageResolver<TPackageInfo> where TPackageInfo : IPackageInfo
	{
		private readonly SearchMarkState<byte> m_BeginSearchState;

		private readonly SearchMarkState<byte> m_EndSearchState;

		private bool m_FoundBegin;

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

		public BeginEndMarkReceiveFilter(byte[] beginMark, byte[] endMark)
		{
			m_BeginSearchState = new SearchMarkState<byte>(beginMark);
			m_EndSearchState = new SearchMarkState<byte>(endMark);
		}

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

		public void ChangeBeginMark(byte[] beginMark)
		{
			if (CheckChanged(m_BeginSearchState.Mark, beginMark))
			{
				m_BeginSearchState.Change(beginMark);
			}
		}

		public void ChangeEndMark(byte[] endMark)
		{
			if (CheckChanged(m_EndSearchState.Mark, endMark))
			{
				m_EndSearchState.Change(endMark);
			}
		}

		public abstract TPackageInfo ResolvePackage(IBufferStream bufferStream);

		public virtual TPackageInfo Filter(BufferList data, out int rest)
		{
			rest = 0;
			ArraySegment<byte> last = data.Last;
			byte[] array = last.Array;
			int offset = last.Offset;
			int count = last.Count;
			int parsedLength = 0;
			int num2;
			int length;
			if (!m_FoundBegin)
			{
				int num = array.SearchMark(offset, count, m_BeginSearchState, out parsedLength);
				if (num < 0)
				{
					if (m_BeginSearchState.Matched > 0 && data.Total == m_BeginSearchState.Matched)
					{
						return default(TPackageInfo);
					}
					State = FilterState.Error;
					return default(TPackageInfo);
				}
				if (num != offset)
				{
					State = FilterState.Error;
					return default(TPackageInfo);
				}
				m_FoundBegin = true;
				num2 = offset + parsedLength;
				if (offset + count <= num2)
				{
					return default(TPackageInfo);
				}
				length = offset + count - num2;
			}
			else
			{
				num2 = offset;
				length = count;
			}
			while (true)
			{
				int parsedLength2;
				int num3 = array.SearchMark(num2, length, m_EndSearchState, out parsedLength2);
				if (num3 < 0)
				{
					return default(TPackageInfo);
				}
				parsedLength += parsedLength2;
				rest = count - parsedLength;
				if (rest > 0)
				{
					data.SetLastItemLength(parsedLength);
				}
				TPackageInfo val = ResolvePackage(this.GetBufferStream(data));
				if ((object)val != (object)default(TPackageInfo))
				{
					Reset();
					return val;
				}
				if (rest <= 0)
				{
					break;
				}
				num2 = num3 + m_EndSearchState.Mark.Length;
				length = rest;
			}
			return default(TPackageInfo);
		}

		public void Reset()
		{
			m_BeginSearchState.Matched = 0;
			m_EndSearchState.Matched = 0;
			m_FoundBegin = false;
		}
	}
}
