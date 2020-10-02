using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Protocol
{
	// Token: 0x020000A9 RID: 169
	public interface IProtocolProcessor
	{
		// Token: 0x060005E2 RID: 1506
		void SendHandshake(WebSocket websocket);

		// Token: 0x060005E3 RID: 1507
		bool VerifyHandshake(WebSocket websocket, WebSocketCommandInfo handshakeInfo, out string description);

		// Token: 0x060005E4 RID: 1508
		ReaderBase CreateHandshakeReader(WebSocket websocket);

		// Token: 0x060005E5 RID: 1509
		void SendMessage(WebSocket websocket, string message);

		// Token: 0x060005E6 RID: 1510
		void SendData(WebSocket websocket, byte[] data, int offset, int length);

		// Token: 0x060005E7 RID: 1511
		void SendData(WebSocket websocket, IList<ArraySegment<byte>> segments);

		// Token: 0x060005E8 RID: 1512
		void SendCloseHandshake(WebSocket websocket, int statusCode, string closeReason);

		// Token: 0x060005E9 RID: 1513
		void SendPing(WebSocket websocket, string ping);

		// Token: 0x060005EA RID: 1514
		void SendPong(WebSocket websocket, string pong);

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060005EB RID: 1515
		bool SupportBinary { get; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060005EC RID: 1516
		bool SupportPingPong { get; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060005ED RID: 1517
		ICloseStatusCode CloseStatusCode { get; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060005EE RID: 1518
		WebSocketVersion Version { get; }
	}
}
