using System;
using NoiseMe.Drags.App.Models.WebSocket4Net.Common;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	// Token: 0x020000AE RID: 174
	public abstract class ReaderBase : IClientCommandReader<WebSocketCommandInfo>
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x00005A75 File Offset: 0x00003C75
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x00005A7D File Offset: 0x00003C7D
		private protected WebSocket WebSocket { protected get; private set; }

		// Token: 0x0600060E RID: 1550 RVA: 0x00005A86 File Offset: 0x00003C86
		public ReaderBase(WebSocket websocket)
		{
			this.WebSocket = websocket;
			this.m_BufferSegments = new ArraySegmentList();
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x00005AA0 File Offset: 0x00003CA0
		protected ArraySegmentList BufferSegments
		{
			get
			{
				return this.m_BufferSegments;
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00005AA8 File Offset: 0x00003CA8
		public ReaderBase(ReaderBase previousCommandReader)
		{
			this.m_BufferSegments = previousCommandReader.BufferSegments;
		}

		// Token: 0x06000611 RID: 1553
		public abstract WebSocketCommandInfo GetCommandInfo(byte[] readBuffer, int offset, int length, out int left);

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00005ABC File Offset: 0x00003CBC
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x00005AC4 File Offset: 0x00003CC4
		public IClientCommandReader<WebSocketCommandInfo> NextCommandReader { get; set; }

		// Token: 0x06000614 RID: 1556 RVA: 0x00005ACD File Offset: 0x00003CCD
		protected void AddArraySegment(byte[] buffer, int offset, int length)
		{
			this.BufferSegments.AddSegment(buffer, offset, length, true);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00005ADE File Offset: 0x00003CDE
		protected void ClearBufferSegments()
		{
			this.BufferSegments.ClearSegements();
		}

		// Token: 0x040002AA RID: 682
		private readonly ArraySegmentList m_BufferSegments;
	}
}
