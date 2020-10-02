using System;
using System.Collections.Generic;
using NoiseMe.Drags.App.Models.WebSocket4Net.Common;
using NoiseMe.Drags.App.Models.WebSocket4Net.Protocol.FramePartReader;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	// Token: 0x020000A4 RID: 164
	internal class DraftHybi10DataReader : IClientCommandReader<WebSocketCommandInfo>
	{
		// Token: 0x060005B7 RID: 1463 RVA: 0x000058C2 File Offset: 0x00003AC2
		public DraftHybi10DataReader()
		{
			this.m_Frame = new WebSocketDataFrame(new ArraySegmentList());
			this.m_PartReader = DataFramePartReader.NewReader;
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x000058E5 File Offset: 0x00003AE5
		public int LeftBufferSize
		{
			get
			{
				return this.m_Frame.InnerData.Count;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x00004685 File Offset: 0x00002885
		public IClientCommandReader<WebSocketCommandInfo> NextCommandReader
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x000058F7 File Offset: 0x00003AF7
		protected void AddArraySegment(ArraySegmentList segments, byte[] buffer, int offset, int length, bool isReusableBuffer)
		{
			segments.AddSegment(buffer, offset, length, isReusableBuffer);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000193D4 File Offset: 0x000175D4
		public WebSocketCommandInfo GetCommandInfo(byte[] readBuffer, int offset, int length, out int left)
		{
			this.AddArraySegment(this.m_Frame.InnerData, readBuffer, offset, length, true);
			IDataFramePartReader dataFramePartReader;
			int num = this.m_PartReader.Process(this.m_LastPartLength, this.m_Frame, out dataFramePartReader);
			if (num < 0)
			{
				left = 0;
				return null;
			}
			left = num;
			if (left > 0)
			{
				this.m_Frame.InnerData.TrimEnd(left);
			}
			if (dataFramePartReader == null)
			{
				WebSocketCommandInfo result;
				if (this.m_Frame.IsControlFrame)
				{
					result = new WebSocketCommandInfo(this.m_Frame);
					this.m_Frame.Clear();
				}
				else if (this.m_Frame.FIN)
				{
					if (this.m_PreviousFrames != null && this.m_PreviousFrames.Count > 0)
					{
						this.m_PreviousFrames.Add(this.m_Frame);
						this.m_Frame = new WebSocketDataFrame(new ArraySegmentList());
						result = new WebSocketCommandInfo(this.m_PreviousFrames);
						this.m_PreviousFrames = null;
					}
					else
					{
						result = new WebSocketCommandInfo(this.m_Frame);
						this.m_Frame.Clear();
					}
				}
				else
				{
					if (this.m_PreviousFrames == null)
					{
						this.m_PreviousFrames = new List<WebSocketDataFrame>();
					}
					this.m_PreviousFrames.Add(this.m_Frame);
					this.m_Frame = new WebSocketDataFrame(new ArraySegmentList());
					result = null;
				}
				this.m_LastPartLength = 0;
				this.m_PartReader = DataFramePartReader.NewReader;
				return result;
			}
			this.m_LastPartLength = this.m_Frame.InnerData.Count - num;
			this.m_PartReader = dataFramePartReader;
			return null;
		}

		// Token: 0x0400028F RID: 655
		private List<WebSocketDataFrame> m_PreviousFrames;

		// Token: 0x04000290 RID: 656
		private WebSocketDataFrame m_Frame;

		// Token: 0x04000291 RID: 657
		private IDataFramePartReader m_PartReader;

		// Token: 0x04000292 RID: 658
		private int m_LastPartLength;
	}
}
