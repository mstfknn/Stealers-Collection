using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	// Token: 0x020000AA RID: 170
	internal abstract class ProtocolProcessorBase : IProtocolProcessor
	{
		// Token: 0x060005EF RID: 1519 RVA: 0x00019CA4 File Offset: 0x00017EA4
		public ProtocolProcessorBase(WebSocketVersion version, ICloseStatusCode closeStatusCode)
		{
			this.CloseStatusCode = closeStatusCode;
			this.Version = version;
			int num = (int)version;
			this.VersionTag = num.ToString();
		}

		// Token: 0x060005F0 RID: 1520
		public abstract void SendHandshake(WebSocket websocket);

		// Token: 0x060005F1 RID: 1521
		public abstract ReaderBase CreateHandshakeReader(WebSocket websocket);

		// Token: 0x060005F2 RID: 1522
		public abstract bool VerifyHandshake(WebSocket websocket, WebSocketCommandInfo handshakeInfo, out string description);

		// Token: 0x060005F3 RID: 1523
		public abstract void SendMessage(WebSocket websocket, string message);

		// Token: 0x060005F4 RID: 1524
		public abstract void SendCloseHandshake(WebSocket websocket, int statusCode, string closeReason);

		// Token: 0x060005F5 RID: 1525
		public abstract void SendPing(WebSocket websocket, string ping);

		// Token: 0x060005F6 RID: 1526
		public abstract void SendPong(WebSocket websocket, string pong);

		// Token: 0x060005F7 RID: 1527
		public abstract void SendData(WebSocket websocket, byte[] data, int offset, int length);

		// Token: 0x060005F8 RID: 1528
		public abstract void SendData(WebSocket websocket, IList<ArraySegment<byte>> segments);

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060005F9 RID: 1529
		public abstract bool SupportBinary { get; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060005FA RID: 1530
		public abstract bool SupportPingPong { get; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x000059D4 File Offset: 0x00003BD4
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x000059DC File Offset: 0x00003BDC
		public ICloseStatusCode CloseStatusCode { get; private set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x000059E5 File Offset: 0x00003BE5
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x000059ED File Offset: 0x00003BED
		public WebSocketVersion Version { get; private set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x000059F6 File Offset: 0x00003BF6
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x000059FE File Offset: 0x00003BFE
		private protected string VersionTag { protected get; private set; }

		// Token: 0x06000601 RID: 1537 RVA: 0x00019CD4 File Offset: 0x00017ED4
		protected virtual bool ValidateVerbLine(string verbLine)
		{
			string[] array = verbLine.Split(ProtocolProcessorBase.s_SpaceSpliter, 3, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length < 2)
			{
				return false;
			}
			if (!array[0].StartsWith("HTTP/"))
			{
				return false;
			}
			int num = 0;
			return int.TryParse(array[1], out num) && num == 101;
		}

		// Token: 0x0400029F RID: 671
		protected const string HeaderItemFormat = "{0}: {1}";

		// Token: 0x040002A3 RID: 675
		private static char[] s_SpaceSpliter = new char[]
		{
			' '
		};
	}
}
