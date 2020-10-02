using NoiseMe.Drags.App.Models.WebSocket4Net.Common;
using NoiseMe.Drags.App.Models.WebSocket4Net.Protocol.FramePartReader;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	internal class DraftHybi10DataReader : IClientCommandReader<WebSocketCommandInfo>
	{
		private List<WebSocketDataFrame> m_PreviousFrames;

		private WebSocketDataFrame m_Frame;

		private IDataFramePartReader m_PartReader;

		private int m_LastPartLength;

		public int LeftBufferSize => m_Frame.InnerData.Count;

		public IClientCommandReader<WebSocketCommandInfo> NextCommandReader => this;

		public DraftHybi10DataReader()
		{
			m_Frame = new WebSocketDataFrame(new ArraySegmentList());
			m_PartReader = DataFramePartReader.NewReader;
		}

		protected void AddArraySegment(ArraySegmentList segments, byte[] buffer, int offset, int length, bool isReusableBuffer)
		{
			segments.AddSegment(buffer, offset, length, isReusableBuffer);
		}

		public WebSocketCommandInfo GetCommandInfo(byte[] readBuffer, int offset, int length, out int left)
		{
			AddArraySegment(m_Frame.InnerData, readBuffer, offset, length, isReusableBuffer: true);
			IDataFramePartReader nextPartReader;
			int num = m_PartReader.Process(m_LastPartLength, m_Frame, out nextPartReader);
			if (num < 0)
			{
				left = 0;
				return null;
			}
			left = num;
			if (left > 0)
			{
				m_Frame.InnerData.TrimEnd(left);
			}
			if (nextPartReader == null)
			{
				WebSocketCommandInfo result;
				if (m_Frame.IsControlFrame)
				{
					result = new WebSocketCommandInfo(m_Frame);
					m_Frame.Clear();
				}
				else if (m_Frame.FIN)
				{
					if (m_PreviousFrames != null && m_PreviousFrames.Count > 0)
					{
						m_PreviousFrames.Add(m_Frame);
						m_Frame = new WebSocketDataFrame(new ArraySegmentList());
						result = new WebSocketCommandInfo(m_PreviousFrames);
						m_PreviousFrames = null;
					}
					else
					{
						result = new WebSocketCommandInfo(m_Frame);
						m_Frame.Clear();
					}
				}
				else
				{
					if (m_PreviousFrames == null)
					{
						m_PreviousFrames = new List<WebSocketDataFrame>();
					}
					m_PreviousFrames.Add(m_Frame);
					m_Frame = new WebSocketDataFrame(new ArraySegmentList());
					result = null;
				}
				m_LastPartLength = 0;
				m_PartReader = DataFramePartReader.NewReader;
				return result;
			}
			m_LastPartLength = m_Frame.InnerData.Count - num;
			m_PartReader = nextPartReader;
			return null;
		}
	}
}
